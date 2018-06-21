using CurrencyRate.Models;

namespace CurrencyRate.Services
{
    public interface IForexService
    {
        CurrencyRateResponse GetExchangeRate(string baseCurrency, string targetCurrency);
    }
}
