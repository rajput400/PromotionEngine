using Microsoft.Extensions.Logging;

namespace PromotionEngine.Models
{
    public static class UnitPriceForSKU
    {
        public static int GetPrice(char itemId, ILogger logger)
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
                {
                    logger.LogWarning($"Ignoring the Invalid char '{itemId}' for calculating the order value.");
                    return 0;
                }
            }
        }
    }
}