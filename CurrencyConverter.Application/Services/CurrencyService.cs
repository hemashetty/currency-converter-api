using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Application.Factories;
using CurrencyConverter.Application.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using CurrencyConverter.Application.Exceptions;
namespace CurrencyConverter.Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private static readonly HashSet<string> BlockedCurrencies =
            new() { "TRY", "PLN", "THB", "MXN" };

        private readonly IMemoryCache _cache;
        private readonly CurrencyProviderFactory _factory;

        public CurrencyService(
            IMemoryCache cache,
            CurrencyProviderFactory factory)
        {
            _cache = cache;
            _factory = factory;
        }

        public async Task<object> GetLatestAsync(string baseCurrency)
        {
            Validate(baseCurrency);

            return await _cache.GetOrCreateAsync(
                $"latest-{baseCurrency}",
                _ => _factory.GetProvider()
                             .GetLatestAsync(baseCurrency));
        }

        public async Task<decimal> ConvertAsync(
            string from,
            string to,
            decimal amount)
        {
            Validate(from, to);

            return await _factory.GetProvider()
                                 .ConvertAsync(from, to, amount);
        }

        public async Task<object> GetHistoricalAsync(
            string baseCurrency,
            DateTime from,
            DateTime to,
            int page,
            int pageSize)
        {
            Validate(baseCurrency);

            var data = await _factory.GetProvider()
                                     .GetHistoricalAsync(
                                         baseCurrency, from, to);

            return data.Rates
                       .Skip((page - 1) * pageSize)
                       .Take(pageSize);
        }

        private void Validate(params string[] currencies)
        {
            if (currencies.Any(c => BlockedCurrencies.Contains(c)))
            {
                throw new UnsupportedCurrencyException(
                    "Currency not supported");
            }
        }
    }
}
