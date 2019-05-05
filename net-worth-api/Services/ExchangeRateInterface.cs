using System;
using System.Threading.Tasks;

namespace networthapi.Services
{
    public interface IExchangeRate
    {
        Task<decimal> GetRate(int id);
    }
}
