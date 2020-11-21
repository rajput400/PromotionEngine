using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Models;

namespace PromotionEngine
{
    public class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            PromotionEngine(null, null); //TODO: Check how to pass the correct value.

            Console.ReadLine();
        }

        public static double PromotionEngine(List<char> cart, List<PromotionType> promotionTypes)
        {
            double orderValue = 0;
            // var cart = GetCart(); //TODO: Remove this.

            //TODO: This can be made under the repetitive function

            //TODO: Recursive function 
            // Get First Unit
            // Calculate the total size of First Unit 
            // Remove the First Unit from cart 

            var cartDetails = CalculateItemsInCart(cart);

            orderValue = ApplyingPromotionTypes(cartDetails, promotionTypes, orderValue);

            orderValue = CalculateCartItemsOrderValue(cartDetails, orderValue);

            //TODO: Handle this promotion type here

            // var unitPrice = GetUnitPriceForSKU(a);

            // var orderValue = b * unitPrice


            //TODO: Make sure to write code which is open for extension.
            //Promotion Types //TODO: Add promotion types 

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
        private static double ApplyingPromotionTypes(List<CartDetail> cartDetails, List<PromotionType> promotionTypes, double orderValue) //TODO: Dictionary can be changed to List
        {
            //TODO: Handle this later on
            // C + D = 30
            // 1* C + 1*D, 30
            // 3 * A = 120
            // A = 40%A 

            //NOW: TestData
            // var promotionTypes = new List<PromotionType>
            // {
            //     new PromotionType
            //     {
            //         CartDetails = new List<CartDetail> {
            //         new CartDetail {SKUId = 'A', NoOfUnits = 3}},
            //         Price = 90
            //     },
            //     new PromotionType
            //     {
            //         CartDetails = new List<CartDetail> {
            //         new CartDetail {SKUId = 'B', NoOfUnits = 2}},
            //         Price = 50
            //     }
            // }; //TODO: Handle this scenario  A = 40%A 

            promotionTypes.ForEach(promotionType =>
            {
                var temp = promotionType.CartDetails.All(promotionTypeCartDetail => cartDetails.Any(cartDetail => cartDetail.SKUId == promotionTypeCartDetail.SKUId && cartDetail.NoOfUnits >= promotionTypeCartDetail.NoOfUnits));
                if (temp) //TODO: Handle this Temp variable.
                {
                    //TODO: Handle mutually exclusive promotion types.
                    //TODO: Handle this logic based on A = 40%A
                    promotionType.CartDetails.ForEach(promotionTypeCartDetail =>
                    {
                        do
                        {
                            var updatedUnits = cartDetails.FirstOrDefault(x => x.SKUId == promotionTypeCartDetail.SKUId).NoOfUnits - promotionTypeCartDetail.NoOfUnits;
                            cartDetails.RemoveAll(cartDetail => cartDetail.SKUId == promotionTypeCartDetail.SKUId);
                            if (updatedUnits > 0)
                                cartDetails.Add(new CartDetail { SKUId = promotionTypeCartDetail.SKUId, NoOfUnits = updatedUnits });
                            orderValue += promotionType.Price;

                        } while (cartDetails.FirstOrDefault(x => x.SKUId == promotionTypeCartDetail.SKUId).NoOfUnits >= promotionTypeCartDetail.NoOfUnits);
                        //TODO: Handle this First or Default to any.
                    });
                }
            });

            return orderValue;
        }

        private static List<CartDetail> CalculateItemsInCart(List<char> cart)
        {
            var cartDetails = new List<CartDetail>(); //TODO: Take care of the variable name.

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

        //TODO: Can be made this into dictionary.
        private static float GetUnitPriceForSKU(char productId) //TODO: Take care of static keyword
        {
            //TODO: Test Data
            //TODO: Take care of Break statement.
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
            } //TODO: Can be made this into dictionary
        }

        private static List<char> GetCart()
        {
            //TODO: Test Data
            var cart = new List<char> { 'A', 'B', 'C', 'A', 'B', 'A' }; //TODO: Variable 

            return cart;
        }
    }
}
