using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Application.DTOs;
using System.Net.Http.Json;
using CurrencyConverter.Application.Interfaces;

namespace CurrencyConverter.Infrastructure.Providers
{
    public class FrankfurterCurrencyProvider : ICurrencyProvider
    {
        private readonly HttpClient _httpClient;
        public FrankfurterCurrencyProvider(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<decimal> ConvertAsync(string from, string to, decimal amount)
        {
            {
                var response =
                    await _httpClient.GetFromJsonAsync<ConversionResponse>(
                        $"latest?amount={amount}&from={from}&to={to}");

                return response.Rates[to];
            }
        }

        public async Task<HistoricalRatesDto> GetHistoricalAsync(string baseCurrency, DateTime from, DateTime to)
        {
            return await _httpClient.GetFromJsonAsync<HistoricalRatesDto>(
        $"{from:yyyy-MM-dd}..{to:yyyy-MM-dd}?base={baseCurrency}");
        }

        public async Task<LatestRatesDto> GetLatestAsync(string baseCurrency)
        {

            return await _httpClient.GetFromJsonAsync<LatestRatesDto>(
                $"latest?base={baseCurrency}");
        }
       
    }
}
