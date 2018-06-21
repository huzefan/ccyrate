using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurrencyRate.Models;

namespace CurrencyRate.Services
{
    public class BackgroundForexService : IHostedService
    {
        private readonly ForexContext _context;
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const int _refreshIntervalInSeconds = 1000 * 60;
        private const string _baseAddress = "http://data.fixer.io/";
        private const string _apiKey = "6fd5710643a5bbeb84bc5d16ba911341";
        private readonly string _requestUrl = $"api/latest?access_key={_apiKey}";
        private const string _cacheKey = "forex";

        public BackgroundForexService(ForexContext context, IMemoryCache cache)
        {
            _context = context;
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri(_baseAddress)
            };
            _cache = cache;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var forexData = await FetchForexData();
                CacheForexRates(_cacheKey, forexData);
                AuditCurrencyRates(forexData);

                await Task.Delay(_refreshIntervalInSeconds, cancellationToken);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        private void CacheForexRates(string key, ForexResult forexResult)
        {
            _cache.Set(key, forexResult);
        }

        private async Task<ForexResult> FetchForexData()
        {
            var response = await _httpClient.GetAsync(_requestUrl);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<ForexResult>(await response.Content.ReadAsStringAsync());
        }

        private void AuditCurrencyRates(ForexResult forexResult)
        {
            foreach (var item in forexResult.Rates)
            {
                _context.ExchangeRates.Add(new Entities.ExchangeRate
                {
                    BaseCurrency = forexResult.Base,
                    TargetCurrency = item.Key,
                    Rate = item.Value,
                    Timestamp = forexResult.Timestamp,
                    Date = forexResult.Date
                });
            }

            _context.SaveChanges();
        }
    }
}
