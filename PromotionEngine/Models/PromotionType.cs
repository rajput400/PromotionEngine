using System.Collections.Generic;

namespace PromotionEngine.Models
{
    public class PromotionType
    {
        public List<CartDetail> CartDetails { get; set; }

        public float Price { get; set; }
    }
}