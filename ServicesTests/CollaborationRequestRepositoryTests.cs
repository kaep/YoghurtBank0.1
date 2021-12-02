using Xunit; 
using YoghurtBank.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

            var student1 = new Student
            {
                Username = "Henning",
                CollaborationRequests = new List<CollaborationRequest>()
            };
            var super1 = new Supervisor
            {
                CollaborationRequests = new List<CollaborationRequest>(),
                ideas = new List<Idea>(),
                Username = "Partyman"
            };
            var collabRequest1 = new CollaborationRequest{
                Id = 1,
                Requester = student1,
                Requestee = super1, 
                Application = "Yes",
                Status = CollaborationRequestStatus.Waiting
            };
            context.Users.Add(student1);
            context.Users.Add(super1);
            context.CollaborationRequests.Add(collabRequest1);
            context.SaveChanges();
            _context = context;
            _repo = new CollaborationRequestRepository(_context);
        }

        [Fact]
        public void FindById_returns_collabRequest1()
        {
            #region Arrange
                        

            #endregion

            #region Act
            var result = _repo.FindById(1).Result;
            #endregion

            #region Assert
            Assert.Equal(1, result.StudentId);
            Assert.Equal(1, result.SupervisorId);
            Assert.Equal(CollaborationRequestStatus.Waiting, result.Status);
            Assert.Equal("Yes", result.Application);
            #endregion
        }
        
    }
}