using System;
using System.Collections.Generic;
using System.Linq;

using networthapi.Models;

namespace networthapi.Services
{
    public class NetWorthCalculator : INetWorthCalculator
    {
        public NetWorth GetNetWorth(Holdings holdings, decimal rate)
        {
            NetWorth result = new NetWorth
            {
                ConvertedHoldings = ConvertHoldings(holdings, rate)

            };
            result.LiabilitiesTotal = Sum(result.ConvertedHoldings.Liabilities);
            result.AssetsTotal = Sum(result.ConvertedHoldings.Assets);
            result.NetWorthTotal = result.AssetsTotal - result.LiabilitiesTotal;
            return result;
        }

        private decimal Sum(IEnumerable<Resource> resources)
        {
            decimal sum = 0;
            foreach(Resource resource in resources)
            {
                sum += resource.Value;
            };
            return sum;
        }

        private Holdings ConvertHoldings(Holdings holdings, decimal rate)
        {
            Holdings convertedHoldings = new Holdings
            {
                Assets = holdings.Assets.Select((asset) =>
                    new Asset
                    {
                        Name = asset.Name,
                        Value = asset.Value * rate,
                        InterestRate = asset.InterestRate,
                        Type = asset.Type
                    }),
                Liabilities = holdings.Liabilities.Select((liability) =>
                    new Liability
                    {
                        Name = liability.Name,
                        Value = liability.Value * rate,
                        InterestRate = liability.InterestRate,
                        MonthlyPayment = liability.MonthlyPayment * rate,
                        Type = liability.Type
                    }),
                ResultCurrencyId = holdings.ResultCurrencyId,
                BaseCurrencyId = holdings.BaseCurrencyId
            };
            return convertedHoldings;
        }
    }
}
