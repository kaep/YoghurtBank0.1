using Xunit;
using YoghurtBank.Services;
using YoghurtBank.Infrastructure;
using YoghurtBank.Data.Model;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ServicesTests {

    public class UserRepositoryTests : IDisposable
    {
        private readonly UserRepository _repository;
        private readonly YoghurtContext _context;
        private bool disposed;

        public UserRepositoryTests()
        {
            //copy-pasta fra rasmus github:
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<YoghurtContext>();
            builder.UseSqlite(connection);
            var context = new YoghurtContext(builder.Options);
            context.Database.EnsureCreated();

            User Mette = new Student {Id = 1, Username = "Mette", CollaborationRequests = new List<CollaborationRequest>{}}; 
            User Sofia = new Student {Id = 2, Username = "Sofia", CollaborationRequests = new List<CollaborationRequest>{}};
            User Jens = new Supervisor {Id = 3, Username = "Jens", CollaborationRequests = new List<CollaborationRequest>{}};
            User Line = new Supervisor {Id = 4, Username = "Line", CollaborationRequests = new List<CollaborationRequest>{}};

            context.Users.AddRange(
                Mette, Sofia, Jens, Line
            );

            //add data here if neccesary

            context.SaveChanges(); 

            _context = context;
            _repository = new UserRepository(_context);

        }

        [Fact]
        public void TestFindUser()
        {   
            Assert.Equal(2,2);            
        }
        
        [Fact]
        public void TestAddUser()
        {

        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                // TODO: dispose managed state (managed objects)
                }
                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}