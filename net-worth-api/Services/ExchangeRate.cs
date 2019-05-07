using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using networthapi.Models;

namespace networthapi.Services
{
    public class ExchangeRate : IExchangeRate
    {
        private readonly string _exchangeRateAPI;
        private readonly HttpClient _httpClient;

        public ExchangeRate(IConfiguration config, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _exchangeRateAPI = config["ExchangeRatesAPI"];
        }

        public async Task<decimal> GetRate(int from, int to)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_exchangeRateAPI}/api/convert?from={from}&to={to}");
            request.Headers.Add("Accept", "application/json");
            
            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var rate = await response.Content
                    .ReadAsAsync<ExchangeRateResponse>();
                return rate.Rate;
            }
            else
            {
                // Should log error here
                return 0;
            }
        }
    }
}
