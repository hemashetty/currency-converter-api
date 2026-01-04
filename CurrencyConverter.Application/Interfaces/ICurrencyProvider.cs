using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Application.DTOs;

namespace CurrencyConverter.Application.Interfaces
{
    public interface ICurrencyProvider
    {
        Task<LatestRatesDto> GetLatestAsync(string baseCurrency);
        Task<decimal> ConvertAsync(string from, string to, decimal amount);
        Task<HistoricalRatesDto> GetHistoricalAsync(
            string baseCurrency, DateTime from, DateTime to);
    }
}
