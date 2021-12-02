using Xunit; 
using YoghurtBank.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using YoghurtBank.Services;
using YoghurtBank.Data.Model;
using System.Linq;

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

            var Idea1 = new Idea {
                Id = 1,
                Subject = "Harry Pooter",
                Title = "A",
                Description = "Vewy nice"
            };

            var Idea2 = new Idea {
                Id = 2

            };

            var student1 = new Student
            {
                Id = 1,
                Username = "Henning",
                CollaborationRequests = new List<CollaborationRequest>()
            };
            var super1 = new Supervisor
            {
                Id = 2,
                CollaborationRequests = new List<CollaborationRequest>(),
                ideas = new List<Idea>(),
                Username = "Partyman"
            };
            var collabRequest1 = new CollaborationRequest{
                Id = 1,
                Requester = student1,
                Requestee = super1, 
                Application = "Yes",
                Status = CollaborationRequestStatus.Waiting,
                Idea = Idea1
            };

            var collabRequest2 = new CollaborationRequest{
                Id = 2,
                Requester = student1,
                Requestee = super1,
                Application = "Yes",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea1
                
            };

            var collabRequest3 = new CollaborationRequest{
                Id = 3,
                Requester = student1,
                Requestee = super1,
                Application = "Yes",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea2
                
            };


            context.Users.Add(student1);
            context.Users.Add(super1);
            context.Ideas.Add(Idea1);
            context.Ideas.Add(Idea2);
            context.CollaborationRequests.Add(collabRequest1);
            context.CollaborationRequests.Add(collabRequest2);
            context.CollaborationRequests.Add(collabRequest3);
            context.SaveChangesAsync();
            _context = context;
            _repo = new CollaborationRequestRepository(_context);
        }

        [Fact]
        public void FindById1_returns_collabRequest1()
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
        [Fact]
        public void FindByInvalidId_Returns_Null()
        {
            #region Arrange
                        

            #endregion

            #region Act
            var result = _repo.FindById(1337).Result;
            #endregion

            #region Assert
            Assert.Null(result);

            #endregion
        }

        [Fact]
        public void FindRequestsByIdeaAsync_Given_return_gandalf()
        {
            #region Arrange

            #endregion

            #region Act
            var request = _repo.FindRequestsByIdeaAsync(1).Result;

            #endregion

            #region Assert

            Assert.Equal(2, request.Count());

            #endregion
        }
        
    }
}