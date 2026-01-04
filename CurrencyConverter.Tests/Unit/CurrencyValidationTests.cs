using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Application.Exceptions;
using CurrencyConverter.Application.Services;
using CurrencyConverter.Application.Factories;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace CurrencyConverter.Tests.Unit
{
    public class CurrencyValidationTests
    {
        [Fact]
        public async Task Convert_WithBlockedCurrency_ShouldThrowException()
        {
            // Arrange
            var cache = new MemoryCache(new MemoryCacheOptions());

            var providerMock = new Mock<Application.Interfaces.ICurrencyProvider>();
            var factory = new CurrencyProviderFactory(providerMock.Object);

            var service = new CurrencyService(cache, factory);

            // Act & Assert
            await Assert.ThrowsAsync<UnsupportedCurrencyException>(() =>
                service.ConvertAsync("EUR", "TRY", 10));
        }
    }
}
