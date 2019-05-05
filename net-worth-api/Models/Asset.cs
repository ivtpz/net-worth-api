using System;
namespace networthapi.Models
{
    public enum AssetType { CashOrInvestment, LongTerm }

    public class Asset : Resource
    {
        public AssetType Type { get; set; }
    }
}
