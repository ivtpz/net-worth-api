using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using networthapi.Models;
using networthapi.Services;


namespace networthapi.Controllers
{
    [Route("api/[controller]")]
    public class NetWorthCalculatorController : Controller
    {
        private readonly INetWorthCalculator _netWorthCalculator;
        private readonly IExchangeRate _exchangeRate;

        public NetWorthCalculatorController(
            INetWorthCalculator netWorthCalculator,
            IExchangeRate exchangeRate)
        {
            _netWorthCalculator = netWorthCalculator;
            _exchangeRate = exchangeRate;
        }

        // PUT api/networthcalculator
        [HttpPut]
        public async Task<ActionResult<NetWorth>> Put([FromBody]Holdings holdings)
        {
            decimal rate;
            if (holdings.BaseCurrencyId != holdings.ResultCurrencyId) 
            {
                rate = await _exchangeRate.GetRate(holdings.BaseCurrencyId, holdings.ResultCurrencyId);
            }
            else
            {
                rate = 1;
            }
            if (rate != 0)
            {
                return Ok(_netWorthCalculator.GetNetWorth(holdings, rate));
            }
            else
            {
                return StatusCode(400);
            }
        }

    }
}
