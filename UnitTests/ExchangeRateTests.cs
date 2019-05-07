using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using exchange_rates.Services;
using exchange_rates.Models;
using Moq;

namespace ExchangeRateTests
{
    [TestFixture]
    public class WhenReadingExchangeRate
    {

        private readonly ExchangeRateData _exchangeRateData;

        public WhenReadingExchangeRate()
        {
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(x => x[It.IsAny<string>()]).Returns("../../../../exchange-rates/data.json");
            _exchangeRateData = new ExchangeRateData(configMock.Object);
        }

        [Test]
        public void Should_Get_ExchangeRates()
        {
            Assert.IsInstanceOf<ExchangeRate>(_exchangeRateData.ExchangeRates[0]);
        }

        [Test]
        public void Should_Get_Currencies()
        {
            Assert.IsInstanceOf<Currency>(_exchangeRateData.ExchangeRates[0]);
        }

    }

}
