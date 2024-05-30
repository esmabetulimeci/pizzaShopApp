using Application.Services.ExchangeRate;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace pizzaShopApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly TcmbService _tcmbService;

        public ExchangeRatesController(TcmbService tcmbService)
        {
            _tcmbService = tcmbService;
        }

        [HttpGet("{currencyCode}")]
        public async Task<ActionResult<decimal>> GetExchangeRate(string currencyCode)
        {
            try
            {
                var exchangeRate = await _tcmbService.GetExchangeRateAsync(currencyCode);
                return Ok(exchangeRate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
