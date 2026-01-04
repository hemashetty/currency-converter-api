using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyConverter.Application.Services;
using CurrencyConverter.Application.Factories;
using Microsoft.Extensions.Caching.Memory;
using Moq;
using Xunit;

namespace CurrencyConverter.Tests.Unit
{
    public class CurrencyServiceTests
    {
        [Fact]
        public async Task Convert_ValidCurrencies_ReturnsAmount()
        {
            // Arrange
            var cache = new MemoryCache(new MemoryCacheOptions());

            var providerMock = new Mock<Application.Interfaces.ICurrencyProvider>();
            providerMock
                .Setup(p => p.ConvertAsync("EUR", "USD", 10))
                .ReturnsAsync(11.5m);

            var factory = new CurrencyProviderFactory(providerMock.Object);
            var service = new CurrencyService(cache, factory);

            // Act
            var result = await service.ConvertAsync("EUR", "USD", 10);

            // Assert
            Assert.Equal(11.5m, result);
        }
    }
}
