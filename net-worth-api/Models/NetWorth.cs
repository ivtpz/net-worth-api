using System;
namespace networthapi.Models
{
    public class NetWorth
    {
        public decimal AssetsTotal { get; set; }
        public decimal LiabilitiesTotal { get; set; }
        public decimal NetWorthTotal { get; set; }
        public Holdings ConvertedHoldings { get; set; }

    }
}
