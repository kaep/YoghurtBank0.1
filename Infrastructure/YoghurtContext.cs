using Microsoft.EntityFrameworkCore;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Infrastructure
{
    public class YoghurtContext : DbContext, IYoghurtContext
    {
        public YoghurtContext(DbContextOptions<YoghurtContext> options) : base(options) { }
        public DbSet<User> Users {get; set;}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        
        }
    }
}