using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using networthapi.Models;

namespace networthapi.Services
{
    public class ExchangeRate : IExchangeRate
    {
        private readonly IOptions<AppSettings> _config;
        private readonly IHttpClientFactory _clientFactory;

        public ExchangeRate(IOptions<AppSettings> config, IHttpClientFactory clientFactory)
        {
            _config = config;
            _clientFactory = clientFactory;
        }

        public async Task<decimal> GetRate(int from, int to)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5005/api/convert?from=" + from + "&to=" + to);
            request.Headers.Add("Accept", "application/json");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var rate = await response.Content
                    .ReadAsAsync<ExchangeRateResponse>();
                return rate.Rate;
            }
            else
            {
                // TODO: Add in error handling
                return 0;
            }
        }
    }
}
