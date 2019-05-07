using NUnit.Framework;
using networthapi.Services;
using networthapi.Models;
using System.Linq;

namespace NetWorthTests
{
    [TestFixture]
    public class WhenCalculatingNetWorth
    {
        private readonly NetWorthCalculator _netWorthCalculator;
        private readonly Asset[] _assets;
        private readonly Liability[] _liabilities;

        public WhenCalculatingNetWorth()
        {
            _netWorthCalculator = new NetWorthCalculator();

            _assets = new Asset[]
            {
                new Asset
                {
                    Name = "Asset1",
                    Value = 847.43m,
                    InterestRate = 0m,
                    Type = AssetType.CashOrInvestment,
                    Id = 1

                },
                 new Asset
                {
                    Name = "Asset2",
                    Value = 42.05m,
                    InterestRate = 0.04m,
                    Type = AssetType.LongTerm,
                    Id = 2

                }
            };

            _liabilities = new Liability[]
            {
                new Liability
                {
                    Name = "Liability1",
                    Value = 450.01m,
                    InterestRate = 0.12m,
                    Type = LiabilityType.ShortTerm,
                    Id = 1,
                    MonthlyPayment = 75m
                },
                 new Liability
                {
                    Name = "Liability2",
                    Value = 856.3m,
                    InterestRate = 0.05m,
                    Type = LiabilityType.LongTerm,
                    Id = 2

                }
            };
        }

        [Test]
        public void Should_Total_Assets()
        {
            var holdings = new Holdings
            {
                Assets = _assets,
                Liabilities = new Liability[] { }
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 1);
            Assert.AreEqual(889.48m, result.AssetsTotal);
        }

        [Test]
        public void Should_Total_Liabilities()
        {
            var holdings = new Holdings
            {
                Assets = new Asset[] { },
                Liabilities = _liabilities
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 1);
            Assert.AreEqual(1306.31m, result.LiabilitiesTotal);
        }

        [Test]
        public void Should_Return_Zero()
        {
            var holdings = new Holdings
            {
                Assets = new Asset[] { },
                Liabilities = new Liability[] { }
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 1);
            Assert.AreEqual(0m, result.AssetsTotal);
        }

        [Test]
        public void Should_Calculate_Negative_Net_Worth()
        {
            var holdings = new Holdings
            {
                Assets =_assets,
                Liabilities = _liabilities
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 1);
            Assert.AreEqual(-416.83m, result.NetWorthTotal);
        }

        [Test]
        public void Should_Calculate_Positive_Net_Worth()
        {
            var assets = _assets.Concat(new Asset[] {
                new Asset
                {
                    Name = "Asset3",
                    Value = 8473.44m,
                    InterestRate = 0.01m,
                    Type = AssetType.LongTerm,
                    Id = 3
                }
            });
            var holdings = new Holdings
            {
                Assets = assets,
                Liabilities = _liabilities
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 1);
            Assert.AreEqual(8056.61m, result.NetWorthTotal);
        }

        [Test]
        public void Should_Convert_Assets()
        {

            var holdings = new Holdings
            {
                Assets = _assets,
                Liabilities = _liabilities
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 0.74m);
            Assert.AreEqual(627.10m, result.ConvertedHoldings.Assets.ElementAt(0).Value);
            Assert.AreEqual(31.12m, result.ConvertedHoldings.Assets.ElementAt(1).Value);

        }

        [Test]
        public void Should_Convert_Liabilities()
        {

            var holdings = new Holdings
            {
                Assets = _assets,
                Liabilities = _liabilities
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 0.74m);
            Assert.AreEqual(333.01m, result.ConvertedHoldings.Liabilities.ElementAt(0).Value);
            Assert.AreEqual(633.66m, result.ConvertedHoldings.Liabilities.ElementAt(1).Value);

        }

        [Test]
        public void Should_Convert_Totals()
        {

            var holdings = new Holdings
            {
                Assets = _assets,
                Liabilities = _liabilities
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 0.74m);
            Assert.AreEqual(658.22m, result.AssetsTotal);
            Assert.AreEqual(966.67m, result.LiabilitiesTotal);
            Assert.AreEqual(-308.45m, result.NetWorthTotal);

        }

        [Test]
        public void Should_Preserve_Order()
        {

            var holdings = new Holdings
            {
                Assets = _assets,
                Liabilities = _liabilities
            };
            var result = _netWorthCalculator.GetNetWorth(holdings, 0.74m);
            Assert.AreEqual(
                _assets.ElementAt(0).Id, 
                result.ConvertedHoldings.Assets.ElementAt(0).Id);
            Assert.AreEqual(
                _assets.ElementAt(1).Id,
                result.ConvertedHoldings.Assets.ElementAt(1).Id);
            Assert.AreEqual(
                _liabilities.ElementAt(0).Id,
                result.ConvertedHoldings.Liabilities.ElementAt(0).Id);
            Assert.AreEqual(
                _liabilities.ElementAt(1).Id,
                result.ConvertedHoldings.Liabilities.ElementAt(1).Id);

        }
    }
}
