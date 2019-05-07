using NUnit.Framework;
using exchangerates.Services;
using exchangerates.Models;
using Moq;
using System.IO;

namespace ExchangeRateTests
{
    [TestFixture]
    public class WhenGettingExchangeRate
    {

        private readonly ExchangeRateData _exchangeRateData;

        public WhenGettingExchangeRate()
        {
            _exchangeRateData = new ExchangeRateData();
        }

        [Test]
        public void Should_Get_ExchangeRates()
        {
            Assert.IsInstanceOf<ExchangeRate>(_exchangeRateData.ExchangeRates[0]);
        }

    }

}
