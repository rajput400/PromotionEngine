using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public interface IPromotionEngineCheckoutProcess
    {
        double CalculateTotalOrderValue(List<char> cart, List<PromotionType> promotionTypes); //TODO: Promotion Types can be avoided
    }
}