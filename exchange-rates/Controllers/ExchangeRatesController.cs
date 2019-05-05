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
    public class ExchangeRatesController : ControllerBase
    {
        private readonly ExchangeRateData _exchangeRateData;

        public ExchangeRatesController()
        {
            _exchangeRateData = new ExchangeRateData();
        }
        // GET api/exchangerates
        [HttpGet]
        public ActionResult<ExchangeRate[]> Get()
        {
            return _exchangeRateData.ExchangeRates;
        }

        // GET api/exchangerates/5
        [HttpGet("{id}")]
        public ActionResult<ExchangeRate> Get(int id)
        {
            return _exchangeRateData.ExchangeRates[id];
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
