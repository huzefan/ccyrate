using Microsoft.EntityFrameworkCore;
using CurrencyRate.Entities;

namespace CurrencyRate
{
    public class ForexContext : DbContext
    {
        public ForexContext(DbContextOptions<ForexContext> options) : base(options)
        {

        }

        public DbSet<ExchangeRate> ExchangeRates { get; set; }
    }
}
