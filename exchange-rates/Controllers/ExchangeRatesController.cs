using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using exchange_rates.Models;
using exchange_rates.Services;

namespace exchange_rates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly ExchangeRateData _exchangeRateData;

        public ConvertController(IConfiguration config)
        {
            _exchangeRateData = new ExchangeRateData(config);
        }

        // GET api/convert?from=1&to=2
        [HttpGet]
        public ActionResult<ExchangeRate> Get(
            [FromQuery(Name = "from")] int from, 
            [FromQuery(Name = "to")] int to)
        {
            try
            {
                var fromCurrency = _exchangeRateData.ExchangeRates[from];
                var toCurrency = _exchangeRateData.ExchangeRates[to];
                decimal fromRate = fromCurrency.Rate;
                decimal toRate = toCurrency.Rate;
                return Ok(new ExchangeRate
                {
                    Name = toCurrency.Name,
                    Symbol = toCurrency.Symbol,
                    ID = toCurrency.ID,
                    Rate = toRate / fromRate
                });
            }
            catch(System.IndexOutOfRangeException e)
            {
                // Should log error here
                Console.WriteLine(e);
                return StatusCode(400);
            }

        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ExchangeRateData _exchangeRateData;

        public CurrenciesController(IConfiguration config)
        {
            _exchangeRateData = new ExchangeRateData(config);
        }
        // GET api/currencies
        [HttpGet]
        public ActionResult<Currency[]> Get()
        {
            return Ok(_exchangeRateData.Currencies);
        }
    }
}
