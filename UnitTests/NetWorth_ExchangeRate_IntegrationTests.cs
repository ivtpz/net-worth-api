using NUnit.Framework;
using networthapi.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.Protected;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Text;

namespace IntegrationTests
{
    [TestFixture]
    public class WhenFetchingExchangeRate
    {
        private ExchangeRate _exchangeRate;

        private void SetUp(HttpStatusCode code) 
        {
            var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            handlerMock
                .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               // prepare the expected response of the mocked http call
               .ReturnsAsync(new HttpResponseMessage()
               {
                   StatusCode = code,
                   Content = new StringContent(
                       "{\"id\":1, \"rate\": 0.82, \"symbol\": \"$\", \"name\": \"USD\"}",
                       Encoding.UTF8,
                       "application/json"
                       ),
               })
               .Verifiable();
            var httpClient = new HttpClient(handlerMock.Object);
            var configMock = new Mock<IConfiguration>();
            configMock.SetupGet(x => x[It.IsAny<string>()]).Returns("http://mock.com");

            _exchangeRate = new ExchangeRate(configMock.Object, httpClient);

        }

        [Test]
        public async Task Return_Results()
        {
            SetUp(HttpStatusCode.OK);
            var result = await _exchangeRate.GetRate(2, 1);
            Assert.AreEqual(0.82, result);
        }

        [Test]
        public async Task Return_Zero_On_Error()
        {
            SetUp(HttpStatusCode.BadGateway);
            var result = await _exchangeRate.GetRate(2, 1);
            Assert.AreEqual(0, result);
        }

    }
}
