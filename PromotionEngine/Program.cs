using System;
using System.Collections.Generic;
using System.Linq;
using PromotionEngine.Models;

namespace PromotionEngine
{
    class Program
    {
        static double orderValue = 0;

        static void Main(string[] args)
        {

            Console.WriteLine("Hello World!");

            PromotionEngine();

            Console.ReadLine();
        }

        private static void PromotionEngine()
        {
            var cart = GetCart();

            //TODO: This can be made under the repetitive function

            //TODO: Recursive function 
            // Get First Unit
            // Calculate the total size of First Unit 
            // Remove the First Unit from cart 

            var cartDetails = CalculateItemsInCart(cart);

            orderValue =  ApplyingPromotionTypes(cartDetails);

            //TODO: Handle this promotion type here

            // var unitPrice = GetUnitPriceForSKU(a);

            // var orderValue = b * unitPrice


            //TODO: Make sure to write code which is open for extension.
            //Promotion Types //TODO: Add promotion types 
        }

        //TODO: Make use of Solid principles
        private static double ApplyingPromotionTypes(List<CartDetail> cartDetails) //TODO: Dictionary can be changed to List
        {
            //TODO: Handle this later on
            // C + D = 30
            // 1* C + 1*D, 30
            // 3 * A = 120
            // A = 40%A 
            var promotionTypes = new List<PromotionType>();
            promotionTypes.Add(
                //TODO: Handle this later on
                // new PromotionType
                // {
                //     CartDetails = new List<CartDetail> {
                //     new CartDetail {SKUId = 'C', NoOfUnits = 1},
                //     new CartDetail {SKUId = 'D', NoOfUnits = 1}},
                //     Price = 30
                // } 
                 new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'A', NoOfUnits = 3}},
                    Price = 30
                });

            promotionTypes.ForEach(promotionType => 
            {
                promotionType.CartDetails.ForEach(promotionTypeCartDetail=> 
                {
                    if(cartDetails.Any(cartDetail=> cartDetail.SKUId == promotionTypeCartDetail.SKUId && cartDetail.NoOfUnits >= promotionTypeCartDetail.NoOfUnits))
                    {
                        //TODO: Write Elimination logic.
                        //TODO: This logic needs to be handled for CartItemUnit > PromotionTypeCartItemUnit

                        cartDetails.RemoveAll(x=> x.SKUId == promotionTypeCartDetail.SKUId);
                        orderValue += orderValue + promotionType.Price;
                    }
                });
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

        private static float GetUnitPriceForSKU(char productId) //TODO: Take care of static keyword
        {
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
            var cart = new List<char> { 'A', 'B', 'C', 'A', 'B', 'A' }; //TODO: Variable 

            return cart;
        }
    }
}
