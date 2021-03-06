using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Models.BusinessLogic
{
    public class PromotionEngineCheckoutProcess : IPromotionEngineCheckoutProcess
    {
        private readonly IPromotionTypes promotionTypes;
        private readonly ILogger<PromotionEngineCheckoutProcess> logger;

        public PromotionEngineCheckoutProcess(IPromotionTypes promotionTypes, ILogger<PromotionEngineCheckoutProcess> logger)
        {
            this.promotionTypes = promotionTypes;
            this.logger = logger;
        }

        public double CalculateTotalOrderValue(List<char> cart)
        {
            double orderValue = 0;

            var cartDetails = CalculateNoOfItemsInCart(cart);

            var getPromotionTypes = promotionTypes.GetPromotionTypes();

            orderValue = ApplyingPromotionTypes(cartDetails, getPromotionTypes, orderValue);

            orderValue = CalculateCartItemsOrderValue(cartDetails, orderValue);

            return orderValue;
        }

        public List<CartDetail> CalculateNoOfItemsInCart(List<char> cart)
        {
            var cartDetails = new List<CartDetail>();

            for (int i = 0; i < cart.Count;)
            {
                var item = cart.First();
                var itemCount = cart.Count(x => x == item);
                cart.RemoveAll(x => x == item);
                cartDetails.Add(new CartDetail { SKUId = item, NoOfUnits = itemCount });
            }

            return cartDetails;
        }

        public double ApplyingPromotionTypes(List<CartDetail> cartDetails, List<PromotionType> promotionTypes, double orderValue)
        {
            promotionTypes.ForEach(promotionType =>
            {
                if (promotionType.CartDetails.All(promotionTypeCartDetail => cartDetails.Any(cartDetail => cartDetail.SKUId == promotionTypeCartDetail.SKUId && cartDetail.NoOfUnits >= promotionTypeCartDetail.NoOfUnits)))
                {
                    promotionType.CartDetails.ForEach(promotionTypeCartDetail =>
                    {
                        do
                        {
                            var updatedUnits = cartDetails.FirstOrDefault(x => x.SKUId == promotionTypeCartDetail.SKUId).NoOfUnits - promotionTypeCartDetail.NoOfUnits;
                            cartDetails.RemoveAll(cartDetail => cartDetail.SKUId == promotionTypeCartDetail.SKUId);
                            if (updatedUnits > 0)
                                cartDetails.Add(new CartDetail { SKUId = promotionTypeCartDetail.SKUId, NoOfUnits = updatedUnits });

                            logger.LogInformation($"Applying Promotion type to Cart Item: {promotionTypeCartDetail.SKUId}");

                            if (promotionTypeCartDetail.Equals(promotionType.CartDetails.Last()))
                            {
                                if (promotionType.Price != null)
                                    orderValue += promotionType.Price.Value;
                                else
                                    orderValue += UnitPriceForSKU.GetPrice(promotionTypeCartDetail.SKUId, logger) * (promotionType.Percentage.Value / 100);
                            }

                        } while (cartDetails.FirstOrDefault(x => x.SKUId == promotionTypeCartDetail.SKUId)?.NoOfUnits >= promotionTypeCartDetail.NoOfUnits);
                    });
                }
            });

            logger.LogInformation($"Order Value from PromotionType: {orderValue}");
            return orderValue;
        }

        public double CalculateCartItemsOrderValue(List<CartDetail> cartDetails, double orderValue)
        {
            cartDetails.ForEach(cartDetail =>
           {
               var itemId = cartDetail.SKUId;
               var itemUnits = cartDetail.NoOfUnits;

               var unitPrice = UnitPriceForSKU.GetPrice(itemId, logger);
               orderValue += (unitPrice * itemUnits);
           });

            return orderValue;
        }
    }
}