using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<object> GetLatestAsync(string baseCurrency);
        Task<decimal> ConvertAsync(string from, string to, decimal amount);
        Task<object> GetHistoricalAsync(
            string baseCurrency, DateTime from, DateTime to,
            int page, int pageSize);
    }
}
