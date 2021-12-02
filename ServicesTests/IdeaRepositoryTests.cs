using YoghurtBank.Data.Model;
using YoghurtBank.Infrastructure;
using Xunit;
using YoghurtBank.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Net;


namespace YoghurtBank.ServicesTests {

    public class IdeaRepositoryTests : IDisposable
    {
        private readonly YoghurtContext _context;
        private readonly IdeaRepository _repo;

        public IdeaRepositoryTests() 
        {
            var connection = new SqliteConnection("Filename=:memory:");
            connection.Open();
            var builder = new DbContextOptionsBuilder<YoghurtContext>();
            builder.UseSqlite(connection);
            var context = new YoghurtContext(builder.Options);
            context.Database.EnsureCreated();

            //tilføj elementer til database - tror jeg.

            context.SaveChanges();
            _context = context;
            _repo = new IdeaRepository(_context);
            
        }


        [Fact]
        public void AddIdeaAsyncReturnsSomething(){

            TimeSpan ts1 = TimeSpan. FromDays(12);
            Type bach = IdeaType.Bachelor;
            var idea = new IdeaCreateDTO{CreatorId = 1, Title = "BigDataHandling", Subject =  "Database Managment", Description = "Interesting topic with the best supervisor (B-dog)", AmountOfCollaborators = 3, Open = true, TimeToComplete = null, StartDate = 2022-01-03, Type = null};
            var output = _repo.AddIdea(idea);

            Assert.Equal(HttpStatusCode.Created, output);
           
        }

        [Fact]
        public void FIndIdeaBySupervisor_given_returns()
        {
        
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }

}