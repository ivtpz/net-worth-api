using System;
namespace networthapi.Models
{
    public class NetWorth
    {
        public decimal AssetsTotal { get; set; }
        //{
        //    get
        //    {

        //        return this.Sum(this.Assets);
        //    }
        //}

        public decimal LiabilitiesTotal { get; set; }
        //{
        //    get
        //    {
        //        return this.Sum(this.Liabilities);
        //    }
        //}

        public decimal NetWorthTotal { get; set; }
        //{
        //    get
        //    {
        //        return this.AssetsTotal - this.LiabilitiesTotal;
        //    }
        //}
        public Holdings ConvertedHoldings { get; set; }

    }
}
