using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using exchange_rates.Controllers;
using exchange_rates.Models; 
using Moq;

namespace ExchangeRateTests
{
    [TestFixture]
    public class WhenGettingExchangeRate
    {

        private readonly ConvertController _convertController;

        public WhenGettingExchangeRate()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(x => x[It.IsAny<string>()]).Returns("../../../../exchange-rates/data.json");
            _convertController = new ConvertController(configMock.Object);
        }

        [Test]
        public void Should_Get_Conversion_Rate_Between_Currencies()
        {
            var result = _convertController.Get(1, 3).Result as OkObjectResult;
            var expected = new ExchangeRate
            {
                Name = "GBP",
                Symbol = "£",
                ID = 3,
                Rate = 0.76m
            };
            var exchangeRate = result.Value as ExchangeRate;
            Assert.AreEqual(expected.Name, exchangeRate.Name);
            Assert.AreEqual(expected.Symbol, exchangeRate.Symbol);
            Assert.AreEqual(expected.ID, exchangeRate.ID);
            Assert.AreEqual(expected.Rate, exchangeRate.Rate);

        }

        [Test]
        public void Should_Error_If_Currency_Id_Does_Not_Exist()
        {
            var result = _convertController.Get(1, 13);
            var httpStatus = result.Result as StatusCodeResult;
            Assert.AreEqual(400, httpStatus.StatusCode);
        }
    }

    [TestFixture]
    public class WhenGettingExchangeRates
    {

        private readonly CurrenciesController _currencyController;

        public WhenGettingExchangeRates()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(x => x[It.IsAny<string>()]).Returns("../../../../exchange-rates/data.json");
            _currencyController = new CurrenciesController(configMock.Object);
        }

        [Test]
        public void Should_Get_All_Currencies()
        {
            var result = _currencyController.Get().Result as OkObjectResult;
            var currencies = result.Value as Currency[];
            Assert.IsInstanceOf<Currency>(currencies[0]);
            Assert.AreEqual(10, currencies.Length);
        }

    }
}
