using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CurrencyConverter.Application.Interfaces;
using CurrencyConverter.Application.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.RateLimiting;

namespace CurrencyConverter.Api.Controllers
{
   
    [Authorize(Roles = "Admin,User")]
    [EnableRateLimiting("fixed")]
    [ApiController]
    [Route("api/v1")]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _service;

        public CurrencyController(ICurrencyService service)
        {
            _service = service;
        }
        [HttpGet("rates/latest")]
        public async Task<IActionResult> GetLatest([FromQuery] string @base)
        {
            try
            {
                var result = await _service.GetLatestAsync(@base);
                return Ok(result);
            }
            catch (UnsupportedCurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("convert")]
        public async Task<IActionResult> Convert(
            string from,
            string to,
            decimal amount)
        {
            try
            {
                var result = await _service.ConvertAsync(from, to, amount);
                return Ok(result);
            }
            catch (UnsupportedCurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("rates/historical")]
        public async Task<IActionResult> GetHistorical(
            string @base,
            DateTime from,
            DateTime to,
            int page = 1,
            int pageSize = 10)
        {
            try
            {
                var result = await _service.GetHistoricalAsync(
                    @base, from, to, page, pageSize);
                return Ok(result);
            }
            catch (UnsupportedCurrencyException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }

}

