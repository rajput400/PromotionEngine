namespace PromotionEngine.Models
{
    public static class UnitPriceForSKU
    {
        public static int GetPrice(char itemId)
        {
            switch (itemId)
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
                    return 0;
            }
        }
    }
}