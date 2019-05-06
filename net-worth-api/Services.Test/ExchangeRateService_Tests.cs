using NUnit.Framework;
using networthapi.Services;

namespace networthapi.UnitTests.Services
{
    [TestFixture]
    public class ExchangeRateService_Tests
    {
        public ExchangeRateService_Tests()
        {
            _exchangeRateService = new ExchangeRate();
        }
    }
}
