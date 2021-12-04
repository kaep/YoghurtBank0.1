
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
            #region Setup

            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<YoghurtContext>();
            builder.UseSqlite(connection);
            var context = new YoghurtContext(builder.Options);
            context.Database.EnsureCreated();
            
            var student1 = new Student
            {
                Id = 1,
                UserName = "Henning",
                CollaborationRequests = new List<CollaborationRequest>()
            };
            var super1 = new Supervisor
            {
                Id = 2,
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                UserName = "Partyman"
            };
            var Idea1 = new Idea
            {
                Id = 1,
                Subject = "Harry Pooter",
                Title = "A",
                Description = "Vewy nice",
                AmountOfCollaborators = 12,
                Creator = super1,
                Open = true,
                Posted = DateTime.Now,
                StartDate = DateTime.Now,
                TimeToComplete = DateTime.Now - DateTime.Today,
                Type = IdeaType.Bachelor
            };

            var Idea2 = new Idea
            {
                Id = 2,
                Subject = "Vuldemurt",
                Title = "B",
                Description = "Erhamgerd",
                AmountOfCollaborators = 9,
                Creator = super1,
                Open = true,
                Posted = DateTime.Now,
                StartDate = DateTime.Now,
                TimeToComplete = DateTime.Now - DateTime.Today,
                Type = IdeaType.Project
            };


            var collabRequest1 = new CollaborationRequest
            {
                Id = 1,
                Requester = student1,
                Requestee = super1,
                Application = "Yes",
                Status = CollaborationRequestStatus.Waiting,
                Idea = Idea1
            };

            var collabRequest2 = new CollaborationRequest
            {
                Id = 2,
                Requester = student1,
                Requestee = super1,
                Application = "No",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea1
            };

            var collabRequest3 = new CollaborationRequest
            {
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

            #endregion
        }


        [Fact]
        public async Task CreateAsync_given_request_returns_it()
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

            var created = await _repo.CreateAsync(collabrequest);

            #endregion

            #region Assert

            Assert.Equal("Heya", created.Application);

            #endregion
        }

        [Fact]
        public async Task FindById1_returns_collabRequest1()
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
        public async Task FindRequestsByIdeaAsync_given_id_1_returns_two_requests()
        {
            #region Arrange

            #endregion

            #region Act

            var requests = await _repo.FindRequestsByIdeaAsync(1);

            #endregion

            #region Assert

            Assert.NotEmpty(requests);
            Assert.Equal(2, requests.Count());
            Assert.Equal("Yes", requests.ElementAt(0).Application);
            Assert.Equal("No", requests.ElementAt(1).Application);

            #endregion
        }


        [Fact]
        public async Task AddAsync_given_collabrequest_returns_collabrequest()
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

            var result = await _repo.CreateAsync(collabrequest);

            #endregion

            #region Assert

            Assert.NotNull(result);
            Assert.Equal("Heya", result.Application);

            #endregion
        }

        [Fact]
        public async Task DeleteAsync_given_valid_id_deletes_requestid_and_returns_id()
        {
            var result = await _repo.DeleteAsync(1);
            var entity = _context.CollaborationRequests.Find(1);

            Assert.Equal(1, result);
            Assert.Null(entity);
        }


        //TODO denne skal rettes når vi har fundet en god status at returnere
        [Fact]
        public async Task DeleteAsync_given_invalid_id_returns_minusone()
        {
            //make sure that it doesnt exists
            var entity = _context.CollaborationRequests.Find(500);
            Assert.Null(entity);

            var result = await _repo.DeleteAsync(500);
            Assert.Equal(-1, result);
        }

        [Fact]
        public async Task UpdateAsync_given_existing_request_updates_it()
        {
            //get initial status
            var status = _context.CollaborationRequests.Find(1).Status;


            var update = new CollaborationRequestUpdateDTO
            {
                Id = 1,
                Status = CollaborationRequestStatus.ApprovedByStudent
            };

            #region Act

            var result = await _repo.UpdateAsync(update.Id, update);

            #endregion

            #region Assert

            Assert.Equal(CollaborationRequestStatus.Waiting, status);
            Assert.NotNull(result);
            Assert.Equal(CollaborationRequestStatus.ApprovedByStudent, result.Status);

            #endregion
        }

        //TODO change this when return value of method is not null but instead is a status
        [Fact]
        public async Task UpdateAsync_given_non_existing_id_returns_null()
        {
            #region Arrange

            var id = 500;
            var update = new CollaborationRequestUpdateDTO
            {
                Id = id,
                Status = CollaborationRequestStatus.ApprovedByStudent
            };
            #endregion

            #region Act

            var result = await _repo.UpdateAsync(500, update);
            var entity = await _context.CollaborationRequests.FindAsync(500);
            #endregion

            #region Assert
            Assert.Null(entity);
            Assert.Null(result);
            #endregion
        }


        #region DisposeStuff

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

        #endregion
    }
}