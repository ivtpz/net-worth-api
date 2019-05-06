using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using exchangerates.Models;
using exchangerates.Services;

namespace exchange_rates.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ConvertController : ControllerBase
    {
        private readonly ExchangeRateData _exchangeRateData;

        public ConvertController()
        {
            _exchangeRateData = new ExchangeRateData();
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
                return new ExchangeRate
                {
                    Name = toCurrency.Name,
                    Symbol = toCurrency.Name,
                    ID = toCurrency.ID,
                    Rate = decimal.Round(toRate / fromRate, 2, MidpointRounding.AwayFromZero)
                };
            }
            catch(System.IndexOutOfRangeException e)
            {
                Console.WriteLine(e);
                return StatusCode(404);
            }

        }

    }

    [Route("api/[controller]")]
    [ApiController]
    public class CurrenciesController : ControllerBase
    {
        private readonly ExchangeRateData _exchangeRateData;

        public CurrenciesController()
        {
            _exchangeRateData = new ExchangeRateData();
        }
        // GET api/currencies
        [HttpGet]
        public ActionResult<Currency[]> Get()
        {
            return _exchangeRateData.Currencies;
        }
    }
}
