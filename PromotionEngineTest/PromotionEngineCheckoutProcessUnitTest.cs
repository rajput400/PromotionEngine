using System.Collections.Generic;
using PromotionEngine.Models;
using Xunit;

namespace PromotionEngineTest
{
    public class PromotionEngineCheckoutProcessUnitTest
    {
        [Fact]
        public void Test_PromotionEngine_Scenario1()
        {
            //Assign
            var cart = new List<char> { 'A', 'B', 'C' }; //TODO: Variable

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess();
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(100, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario2()
        {
            //Assign
            var cart = new List<char> { 'A', 'B', 'C', 'A', 'A', 'A', 'A', 'B', 'B', 'B', 'B' }; //TODO: Variable

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess();
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(370, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario3()
        {
            var cart = new List<char> { 'A', 'B', 'C', 'D', 'A', 'A', 'B', 'B', 'B', 'B' }; //TODO: Variable

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess();
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(280, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario4()
        {
            var cart = new List<char> { 'A', 'B', 'C' }; //TODO: Variable

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess();
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(70, orderValue);
        }
    }

}
