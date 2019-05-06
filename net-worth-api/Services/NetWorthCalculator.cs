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
                        Value = decimal.Round(asset.Value * rate, 2, MidpointRounding.AwayFromZero),
                        InterestRate = asset.InterestRate,
                        Type = asset.Type,
                        Id = asset.Id
                    }),
                Liabilities = holdings.Liabilities.Select((liability) =>
                    new Liability
                    {
                        Name = liability.Name,
                        Value = decimal.Round(liability.Value * rate, 2, MidpointRounding.AwayFromZero),
                        InterestRate = liability.InterestRate,
                        MonthlyPayment = decimal.Round(liability.MonthlyPayment * rate, 2, MidpointRounding.AwayFromZero),
                        Type = liability.Type,
                        Id = liability.Id
                    }),
                ResultCurrencyId = holdings.ResultCurrencyId,
                BaseCurrencyId = holdings.ResultCurrencyId
            };
            return convertedHoldings;
        }
    }
}
