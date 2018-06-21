using Microsoft.Extensions.Caching.Memory;
using System;
using CurrencyRate.Models;

namespace CurrencyRate.Services
{
    public class ForexService : IForexService
    {
        private readonly IMemoryCache _cache;
        private const int _decimalRounding = 5;
        private const string _cacheKey = "forex";

        public ForexService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public CurrencyRateResponse GetExchangeRate(string baseCurrency, string targetCurrency)
        {
            var forexResult = _cache.Get<ForexResult>(_cacheKey);

            return new CurrencyRateResponse
            {
                BaseCurrency = baseCurrency,
                TargetCurrency = targetCurrency,
                ExchangeRate = GetCurrencyRate(baseCurrency, targetCurrency, forexResult),
                Timestamp = TimestampToDateTime(forexResult.Timestamp)
            };
        }

        private static DateTime TimestampToDateTime(double unixTimeStamp)
        {
            var dt = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dt = dt.AddSeconds(unixTimeStamp).ToLocalTime();
            return dt;
        }

        private static decimal GetCurrencyRate(string baseCurrency, string targetCurrency, ForexResult forexResult)
        {
            decimal rate = 0;
            if (forexResult.Base.Equals(baseCurrency))
            {
                rate = forexResult.Rates[targetCurrency];
            }
            else if (forexResult.Base.Equals(targetCurrency))
            {
                rate = 1 / forexResult.Rates[baseCurrency];
            }
            else
            {
                var baseRate = 1 / forexResult.Rates[baseCurrency];
                var targetRate = 1 / forexResult.Rates[targetCurrency];
                rate = 1 * baseRate * 1 / targetRate;
            }

            return Math.Round(rate, _decimalRounding);
        }
    }
}
