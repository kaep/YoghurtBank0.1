using Microsoft.EntityFrameworkCore;

namespace YoghurtBank.Infrastructure
{
    public class YoghurtContext : DbContext, IYoghurtContext
    {
        public YoghurtContext(DbContextOptions<YoghurtContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}