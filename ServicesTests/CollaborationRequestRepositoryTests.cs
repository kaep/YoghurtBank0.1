using Xunit; 
using YoghurtBank.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using YoghurtBank.Services;
using YoghurtBank.Data.Model;

namespace ServicesTests
{
    public class CollaborationRequestRepositoryTests
    {
        private readonly IYoghurtContext _context;
        private readonly CollaborationRequestRepository _repo; 
        
        public CollaborationRequestRepositoryTests() 
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<YoghurtContext>();
            builder.UseSqlite(connection);
            var context = new YoghurtContext(builder.Options);
            context.Database.EnsureCreated();

            var student1 = new Student();
            var super1 = new Supervisor();
            var collabRequest1 = new CollaborationRequest{
                Id = 1,
                Requester = student1,
                Requestee = super1, 
                Application = "Yes",
                Status = CollaborationRequestStatus.Waiting
            };
            context.CollaborationRequests.Add(collabRequest1);
            context.SaveChanges();
            _context = context;
            _repo = new CollaborationRequestRepository(_context);
        }

        [Fact]
        public void FindById_returns_collabRequest1()
        {   
                       
        }
        
    }
}