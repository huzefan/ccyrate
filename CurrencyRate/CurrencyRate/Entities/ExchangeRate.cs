using System;
using System.ComponentModel.DataAnnotations;

namespace CurrencyRate.Entities
{
    public class ExchangeRate
    {
        [Key]
        public int Id { get; set; }
        public string BaseCurrency { get; set; }
        public string TargetCurrency { get; set; }
        public decimal Rate { get; set; }
        public int Timestamp { get; set; }
        public DateTime Date { get; set; }
    }
}
