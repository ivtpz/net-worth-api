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
            result.LiabilitiesTotal = decimal.Round(Sum(holdings.Liabilities) * rate, 2, MidpointRounding.AwayFromZero);
            result.AssetsTotal = decimal.Round(Sum(holdings.Assets) * rate, 2, MidpointRounding.AwayFromZero);
            result.NetWorthTotal = result.AssetsTotal - result.LiabilitiesTotal;
            return result;
        }

        public ProjectedWorth GetProjectedWorth(AssetsAndLiabilities assetsAndLiabilities) 
        {
            Console.WriteLine(assetsAndLiabilities);
            ProjectedWorth projection = new ProjectedWorth
            {
                projections = new decimal[20]
            };
            var EOYAssets = assetsAndLiabilities.Assets;
            var EOYLiabilities = assetsAndLiabilities.Liabilities;

            projection.projections[0] = Sum(EOYAssets) - Sum(EOYLiabilities);

            for (int year = 1; year < 20; year++)
            {
                EOYAssets = EOYAssets.Select((asset) =>
                    new Asset
                    {
                        Value = asset.Value * (1 + asset.InterestRate),
                        InterestRate = asset.InterestRate
                    });
                decimal EOYAssetsTotal = Sum(EOYAssets);

                decimal paymentsMade = EOYLiabilities.Aggregate(0m, (acc, liability) => acc + GetEOYValue(liability).paymentsMade);

                EOYLiabilities = EOYLiabilities.Select((liability) =>
                    new Liability
                    {
                        MonthlyPayment = liability.MonthlyPayment,
                        InterestRate = liability.InterestRate,
                        Value = GetEOYValue(liability).value
                    }
                );
                decimal EOYLiabilitiesTotal = Sum(EOYLiabilities);

                projection.projections[year] = decimal.Round(EOYAssetsTotal - paymentsMade - EOYLiabilitiesTotal, 2, MidpointRounding.AwayFromZero);
            }

            return projection;
        }

        public class LiabilityResult
        {
            public decimal value;
            public decimal paymentsMade;
        }

        private LiabilityResult GetEOYValue(Liability liability)
        {
            decimal value = liability.Value;
            decimal paymentsMade = 0;
            for(int month = 0; month < 12; month++)
            {
                if (value > 0)
                {
                    paymentsMade += Math.Min(liability.MonthlyPayment, value);
                    value = (value - liability.MonthlyPayment) * (1 + (liability.InterestRate / 12));
                }
            }
            return new LiabilityResult
            {
                value = Math.Max(value, 0),
                paymentsMade = paymentsMade
            };
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
