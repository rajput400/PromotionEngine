using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class PromotionType
    {
        public List<CartDetail> CartDetails { get; set; }

        public double? Price { get; set; }

        public double? Percentage { get; set; }
    }
}