using System.IO;
using Newtonsoft.Json;
using exchangerates.Models;

namespace exchangerates.Services
{
    public class ExchangeRateData
    {
        public ExchangeRateData()
        {
            using (StreamReader r = File.OpenText("./data.json"))
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
