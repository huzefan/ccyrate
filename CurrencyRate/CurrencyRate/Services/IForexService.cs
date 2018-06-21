using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyRate.Models;

namespace CurrencyRate.Services
{
    public interface IForexService
    {
        //Task<CurrencyRateResponse> GetExchangeRate(string baseCurrency, string targetCurrency);
        CurrencyRateResponse GetExchangeRate(string baseCurrency, string targetCurrency);
    }
}
