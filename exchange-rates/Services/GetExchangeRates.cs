using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using exchange_rates.Models;

namespace exchange_rates.Services
{
    public class ExchangeRateData
    {
        public ExchangeRateData(IConfiguration config)
        {
            using (StreamReader r = File.OpenText(config["DataLocation"]))
            {
                string json = r.ReadToEnd();
                ExchangeRates = JsonConvert.DeserializeObject<ExchangeRate[]>(json);
                Currencies = JsonConvert.DeserializeObject<Currency[]>(json);
            }
        }
        public ExchangeRate[] ExchangeRates { get; }
        public Currency[] Currencies { get; }
    }
}
