using System.Collections.Generic;
using System.Linq;

namespace PromotionEngine.Models
{
    public class PromotionEngineCheckoutProcess : IPromotionEngineCheckoutProcess
    {
        public double CalculateTotalOrderValue(List<char> cart, List<PromotionType> promotionTypes)
        {
            double orderValue = 0;

            var cartDetails = CalculateItemsInCart(cart);

            orderValue = ApplyingPromotionTypes(cartDetails, promotionTypes, orderValue);

            orderValue = CalculateCartItemsOrderValue(cartDetails, orderValue);

            //TODO: Make sure to write code which is open for extension.

            return orderValue;
        }

        private static double CalculateCartItemsOrderValue(List<CartDetail> cartDetails, double orderValue)
        {
            cartDetails.ForEach(cartDetail =>
            {
                var itemId = cartDetail.SKUId;
                var itemUnits = cartDetail.NoOfUnits;

                var unitPrice = GetUnitPriceForSKU(itemId);
                orderValue += (unitPrice * itemUnits);
            });

            return orderValue;
        }

        //TODO: Make use of Solid principles
        private static double ApplyingPromotionTypes(List<CartDetail> cartDetails, List<PromotionType> promotionTypes, double orderValue)
        {
            promotionTypes.ForEach(promotionType =>
            {
                if (promotionType.CartDetails.All(promotionTypeCartDetail => cartDetails.Any(cartDetail => cartDetail.SKUId == promotionTypeCartDetail.SKUId && cartDetail.NoOfUnits >= promotionTypeCartDetail.NoOfUnits)))
                {
                    //TODO: Handle mutually exclusive promotion types.
                    promotionType.CartDetails.ForEach(promotionTypeCartDetail =>
                    {
                        do
                        {
                            var updatedUnits = cartDetails.FirstOrDefault(x => x.SKUId == promotionTypeCartDetail.SKUId).NoOfUnits - promotionTypeCartDetail.NoOfUnits;
                            cartDetails.RemoveAll(cartDetail => cartDetail.SKUId == promotionTypeCartDetail.SKUId);
                            if (updatedUnits > 0)
                                cartDetails.Add(new CartDetail { SKUId = promotionTypeCartDetail.SKUId, NoOfUnits = updatedUnits });

                            if (promotionTypeCartDetail.Equals(promotionType.CartDetails.Last()))
                            {
                                if (promotionType.Price != null)
                                    orderValue += promotionType.Price.Value;
                                else
                                    orderValue += GetUnitPriceForSKU(promotionTypeCartDetail.SKUId) * (promotionType.Percentage.Value / 100);
                            }


                        } while (cartDetails.FirstOrDefault(x => x.SKUId == promotionTypeCartDetail.SKUId)?.NoOfUnits >= promotionTypeCartDetail.NoOfUnits);
                    });
                }
            });

            return orderValue;
        }

        private static List<CartDetail> CalculateItemsInCart(List<char> cart)
        {
            var cartDetails = new List<CartDetail>();

            for (int i = 0; i < cart.Count;) //TODO: Using do while loop
            {
                var item = cart.First();
                var itemCount = cart.Count(x => x == item);
                cart.RemoveAll(x => x == item);
                cartDetails.Add(new CartDetail { SKUId = item, NoOfUnits = itemCount });

                //TODO: Issue is with decreasing size list.
            }

            return cartDetails;
        }

        //TODO: Add Unit test for empty scenarios.
        //TODO: Can be made this into dictionary.
        private static float GetUnitPriceForSKU(char productId) //TODO: Take care of static keyword
        {
            //TODO: Test Data
            switch (productId)
            {
                case 'A':
                    return 50;

                case 'B':
                    return 30;

                case 'C':
                    return 20;

                case 'D':
                    return 15;

                default:
                    return 50; //TODO: Handle this later on
            }
        }
    }
}