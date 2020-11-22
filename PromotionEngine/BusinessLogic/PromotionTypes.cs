using System.Collections.Generic;

namespace PromotionEngine.Models.BusinessLogic
{
    public class PromotionTypes : IPromotionTypes
    {
        public List<PromotionType> GetPromotionTypes()
        {
            var promotionTypes = new List<PromotionType>
            {
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'A', NoOfUnits = 3}},
                    Price = 130
                },
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'B', NoOfUnits = 2}},
                    Price = 45
                },
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'C', NoOfUnits = 1},
                    new CartDetail {SKUId = 'D', NoOfUnits = 1}},
                    Price = 30
                }
            };

            return promotionTypes;
        }
    }
}