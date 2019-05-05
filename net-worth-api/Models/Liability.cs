using System;
namespace networthapi.Models
{
    public enum LiabilityType { ShortTerm, LongTerm }

    public class Liability : Resource
    {
        public decimal MonthlyPayment { get; set; }
        public LiabilityType Type { get; set; }
    }
}
