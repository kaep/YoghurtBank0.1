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
using System.Threading.Tasks;
using System.Net;

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
            context.SaveChanges(); 

            _context = context;
            _repository = new UserRepository(_context);
        }

        [Fact]
        public void TestFindUserByID()
        {   
            var expectedUser = new UserDetailsDTO {Id = 1, Username = "Mette", UserType = "Student"};
            var output = _repository.FindUserByIdAsync(1);
            Assert.Equal(expectedUser, output.Item2.Result);            
        }

        [Fact]
        public void FindUser_given_id_7_returns_NotFound()
        {
            var output = _repository.FindUserByIdAsync(7);
            var actual = output.Item1; 
            Assert.Equal(HttpStatusCode.NotFound, actual);
        }
        
        [Fact]
        public void AddUserAsyncReturnsUserDetailsDTO()
        {
            var user = new UserCreateDTO{Username = "Hanne", UserType = "Student"};
            var output = _repository.AddUserAsync(user);

            Assert.Equal(HttpStatusCode.Created, output.Item1);
            Assert.Equal(output.Item2.Result.Id, 5);
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