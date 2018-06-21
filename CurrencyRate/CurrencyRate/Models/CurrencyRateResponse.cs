using System;

namespace CurrencyRate.Models
{
    public class CurrencyRateResponse
    {
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
