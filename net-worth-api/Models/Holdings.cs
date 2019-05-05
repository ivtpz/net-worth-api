using System;
using System.Collections.Generic;

namespace networthapi.Models
{
    public class Holdings
    {
        public IEnumerable<Asset> Assets { get; set; }
        public IEnumerable<Liability> Liabilities { get; set; }
        public int ResultCurrencyId { get; set; }
        public int BaseCurrencyId { get; set; }

    }
}
