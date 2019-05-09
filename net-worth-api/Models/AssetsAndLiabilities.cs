using System;
using System.Collections.Generic;

namespace networthapi.Models
{
    public class AssetsAndLiabilities
    {
        public IEnumerable<Asset> Assets { get; set; }
        public IEnumerable<Liability> Liabilities { get; set; }
    }
}
