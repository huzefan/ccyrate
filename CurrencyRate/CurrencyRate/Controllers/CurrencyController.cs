using Microsoft.AspNetCore.Mvc;
using CurrencyRate.Services;

namespace CurrencyRate.Controllers
{
    [Route("api/[controller]")]
    public class CurrencyController
    {
        private readonly IForexService _forexService;

        public CurrencyController(IForexService forexService)
        {
            _forexService = forexService;
        }

        [HttpGet("{baseCurrency}/{targetCurrency}")]
        public IActionResult Get(string baseCurrency, string targetCurrency)
        {
            return new OkObjectResult(_forexService.GetExchangeRate(baseCurrency, targetCurrency));
        }

        [HttpGet]
        public IActionResult Get()
        {
            return new OkObjectResult("Currency rate service. Usage - /api/currency/USD/CAD");
        }
    }
}
