using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Infrastructure
{
    public interface IYoghurtContext : IDisposable
    {
        public DbSet<User> Users {get;}
        public DbSet<Idea> Ideas { get; }
        public DbSet<CollaborationRequest> CollaborationRequests { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        int SaveChanges();

    }
}