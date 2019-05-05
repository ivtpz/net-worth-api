using System;
using networthapi.Models;

namespace networthapi.Services
{
    public interface INetWorthCalculator
    {
        NetWorth GetNetWorth(Holdings holdings, decimal rate);
    }
}
