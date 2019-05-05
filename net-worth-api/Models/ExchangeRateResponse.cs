using System;
namespace networthapi.Models

{
    public class ExchangeRateResponse
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public string Symbol { get; set; }
        public int ID { get; set; }
    }
}
