using System.Collections.Generic;
using System.Linq;
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
            var cartItems = new List<char> { 'A', 'B', 'C' };
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypes());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cartItems);

            //Assert
            Assert.Equal(100, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario2()
        {
            //Assign
            var cartItems = new List<char> { 'A', 'B', 'C', 'A', 'A', 'A', 'A', 'B', 'B', 'B', 'B' };
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypes());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cartItems);

            //Assert
            Assert.Equal(370, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario3()
        {
            //Act
            var cartItems = new List<char> { 'A', 'B', 'C', 'D', 'A', 'A', 'B', 'B', 'B', 'B' };
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypes());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cartItems);

            //Assert
            Assert.Equal(280, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario4()
        {
            //Act
            var cartItems = new List<char> { 'A', 'B', 'C' };
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypeWithPercentage());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cartItems);

            //Assert
            Assert.Equal(70, orderValue);
        }

        [Fact]
        public void Test_PromotionEngine_Scenario5_PromotionTypeWithPercentageForSameItem()
        {
            //Act
            var cartItems = new List<char> { 'A','A','A'};
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypeWithPercentageForSameItem());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cartItems);

            //Assert
            Assert.Equal(50, orderValue);
        }

        [Theory(DisplayName = "Test_CalculateTotalOrderValue_WithInvalidString")]
        [InlineData("A,;,!", 50)]
        [InlineData(" ", 0)]
        [InlineData("1,2,3,4", 0)]
        [InlineData("F,E,G", 0)]
        [InlineData("A3C", 70)]
        public void Test_CalculateTotalOrderValue_WithInvalidString(string inputCartString, double expectedOrderValue)
        {
            //Act
            var cartItems = inputCartString.Replace(',', ' ').Replace(" ", "").ToCharArray().ToList();
            ;
            var promotionTypesMock = new Mock<IPromotionTypes>();
            promotionTypesMock.Setup(x => x.GetPromotionTypes()).Returns(GetPromotionTypes());

            //Act
            var promotionEngineProcess = new PromotionEngineCheckoutProcess(promotionTypesMock.Object);
            var orderValue = promotionEngineProcess.CalculateTotalOrderValue(cartItems);

            //Assert
            Assert.Equal(expectedOrderValue, orderValue);
        }

        #region Helper Methods

        private List<PromotionType> GetPromotionTypes()
        {
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

        private List<PromotionType> GetPromotionTypeWithPercentageForSameItem()
        {
            var promotionTypes = new List<PromotionType>
            {
                 new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'A', NoOfUnits = 2}},
                    Price = 30
                },
                new PromotionType
                {
                    CartDetails = new List<CartDetail> {
                    new CartDetail {SKUId = 'A', NoOfUnits = 1}},
                    Percentage = 40,
                }
            };
            return promotionTypes;
        }

        #endregion
    }

}
