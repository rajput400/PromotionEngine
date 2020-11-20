using PromotionEngine;
using Xunit;

namespace PromotionEngineTest
{
    public class PromotionEngineUnitTest
    {
        [Fact]
        public void Test_PromotionEngine_Scenario1()
        {
            //TODO: Setup the Test Data

            //Act
            var orderValue = Program.PromotionEngine();

            //Assert
            Assert.Equal(30, orderValue);
            //TODO: Order Values needs to be updated based on the test scenario.
        }
    }
}
