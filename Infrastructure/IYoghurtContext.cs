using Microsoft.EntityFrameworkCore;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Infrastructure
{
    public interface IYoghurtContext : IDisposable
    {
        public DbSet<User> Users {get;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        
    }
}