using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public interface IPromotionEngineCheckoutProcess
    {
        double CalculateTotalOrderValue(List<char> cart);

        List<CartDetail> CalculateNoOfItemsInCart(List<char> cart);

        double ApplyingPromotionTypes(List<CartDetail> cartDetails, List<PromotionType> promotionTypes, double orderValue);

        double CalculateCartItemsOrderValue(List<CartDetail> cartDetails, double orderValue);
    }
}