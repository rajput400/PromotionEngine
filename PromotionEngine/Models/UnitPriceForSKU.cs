namespace PromotionEngine.Models
{
    //TODO: Add Unit test for empty scenarios.
    //TODO: Can be made this into dictionary.
    public static class UnitPriceForSKU
    {
        public static int GetPrice(char itemId)
        {
            //TODO: Test Data
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
                    return 50; //TODO: Handle this later on
            }
        }
    }
}