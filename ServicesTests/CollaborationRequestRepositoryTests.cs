using Xunit; 
using YoghurtBank.Infrastructure;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using YoghurtBank.Services;
using YoghurtBank.Data.Model;
using System.Linq;
using System.Threading.Tasks;

namespace ServicesTests
{
    public class CollaborationRequestRepositoryTests : IDisposable
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
            var Idea1 = new Idea {
                Id = 1,
                Subject = "Harry Pooter",
                Title = "A",
                Description = "Vewy nice",
                AmountOfCollaborators = 12,
                Creator = super1,
                Open = true,
                Posted = DateTime.Now,
                StartDate = DateTime.Now,
                TimeToComplete = DateTime.Now-DateTime.Today,
                Type = IdeaType.Bachelor
            };

            var Idea2 = new Idea {
                Id = 2,
                Subject = "Vuldemurt",
                Title = "B",
                Description = "Erhamgerd",
                AmountOfCollaborators = 9,
                Creator = super1,
                Open = true,
                Posted = DateTime.Now,
                StartDate = DateTime.Now,
                TimeToComplete = DateTime.Now-DateTime.Today,
                Type = IdeaType.Project
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
            context.SaveChanges();
            _context = context;
            _repo = new CollaborationRequestRepository(_context);
        }

        [Fact]
        public void ikkeasynk()
        {
            var collabrequest = new CollaborationRequestCreateDTO
            {
                StudentId = 1,
                SupervisorId = 2,
                Application = "Heya",
                IdeaId = 1
            };
            var created = _repo.Create(collabrequest);
            
            Assert.Equal("Heya", created.Application);
        }
        

        [Fact]
        public async Task Create_fuckdethele()
        {
            var collabrequest = new CollaborationRequestCreateDTO
            {
                StudentId = 1,
                SupervisorId = 2,
                Application = "Heya",
                IdeaId = 1
            };
            var created = await _repo.CreateAsync(collabrequest);
            
            Assert.Equal("Heya", created.Application);

        }

        [Fact]
        public async void FindById1_returns_collabRequest1()
        {
            #region Arrange
                        

            #endregion

            #region Act
            var result = await _repo.FindById(1);
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
        public async void FindRequestsByIdeaAsync_Given_return_gandalf()
        {
            #region Arrange

            #endregion

            #region Act

            var request = await _repo.FindRequestsByIdeaAsync(1);
            #endregion

            #region Assert
            Assert.NotEmpty(request);
            Assert.Equal(2, request.Count());

            #endregion
        }


        [Fact]
        public async void AddAsync_given_collabrequest_returns_collabrequest()
        {
            #region Arrange

            var collabrequest = new CollaborationRequestCreateDTO
            {
                StudentId = 1,
                SupervisorId = 2,
                Application = "Heya",
                IdeaId = 1
            };
            
            #endregion

            #region Act

            var result = await _repo.AddCollaborationRequestAsync(collabrequest);

            #endregion

            #region Assert
            Assert.NotNull(result);
            Assert.Equal("Heya", result.Application);
            #endregion

        }

        private bool disposed;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        
    }
}