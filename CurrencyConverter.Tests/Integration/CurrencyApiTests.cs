using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CurrencyConverter.Tests.Integration
{
    public class CurrencyApiTests
     : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public CurrencyApiTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task ConvertEndpoint_ReturnsSuccess()
        {
            // Act
            var response = await _client.GetAsync(
                "/api/v1/convert?from=EUR&to=USD&amount=10");

            // Assert
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
