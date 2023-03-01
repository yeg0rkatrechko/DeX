using Models;
using Services;
using Xunit;
namespace ServiceTests
{
    public class CurrencyServiceTests
    {
        [Fact]
        public async Task ConvertCurrencySuccessfulTest()
        {
            //Arrange
            CurrencyService currencyService = new CurrencyService();
            var data = new CurrencyToConvert
            {
                Key = "t7s4RKqNiEPFWv4kLe2NyTi8UJRw6W",
                InputCurrency = "USD",
                OutputCurrency = "EUR",
                Amount = 50
            };

            //Act
            var response = await currencyService.ConvertCurrency(data);

            //Assert
            Assert.Equal("0", response.Error);
        }
    }
}
