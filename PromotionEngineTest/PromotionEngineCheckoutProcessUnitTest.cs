using System.Collections.Generic;
using Moq;
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
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypes());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(100, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario2()
        {
            //Assign
            var cart = new List<char> { 'A', 'B', 'C', 'A', 'A', 'A', 'A', 'B', 'B', 'B', 'B' }; //TODO: Variable
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypes());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(370, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario3()
        {
            //Act
            var cart = new List<char> { 'A', 'B', 'C', 'D', 'A', 'A', 'B', 'B', 'B', 'B' }; //TODO: Variable
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypes());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(280, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario4()
        {
            //Act
            var cart = new List<char> { 'A', 'B', 'C' }; //TODO: Variable
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypeWithPercentage());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cart);

            //Assert
            Assert.Equal(70, orderValue);
        }

        #region Helper Methods

        private List<PromotionType> GetPromotionTypes()
        {

            //TODO: Setup the Test Data
            var promotionTypes = new List<PromotionType>
            {
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'A', NoOfUnits = 3}},
                    Price = 130
                },
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'B', NoOfUnits = 2}},
                    Price = 45
                },
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'C', NoOfUnits = 1},
                    new CartDetail {SKUId = 'D', NoOfUnits = 1}},
                    Price = 30
                }
            };
            return promotionTypes;
        }

        private List<PromotionType> GetPromotionTypeWithPercentage()
        {
            //TODO: Setup the Test Data
            var promotionTypes = new List<PromotionType>
            {
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'A', NoOfUnits = 1}},
                    Percentage = 40,
                },
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'B', NoOfUnits = 2}},
                    Price = 45
                },
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'C', NoOfUnits = 1},
                    new CartDetail {SKUId = 'D', NoOfUnits = 1}},
                    Price = 30
                }
            };
            return promotionTypes;
        }

        #endregion
    }

}
