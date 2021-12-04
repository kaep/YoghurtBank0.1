using YoghurtBank.Data.Model;
using YoghurtBank.Infrastructure;
using Xunit;
using YoghurtBank.Services;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Collections.Generic;
using System;

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

            var supervisor1 = new Supervisor{Id = 1, Username = "Torben", CollaborationRequests = new List<CollaborationRequest>(), ideas = new List<Idea>()};
            var supervisor2 = new Supervisor{Id = 2, Username = "Preben", CollaborationRequests = new List<CollaborationRequest>(), ideas = new List<Idea>()};
            
            var idea1 = new Idea{Id = 1, Creator = supervisor2, Posted = DateTime.Now, Subject = "Big Data", Title = "Big data is good", Description = "Big data gives value", AmountOfCollaborators = 3, Open = true, TimeToComplete = DateTime.Now-DateTime.Today, StartDate = DateTime.Now, Type = IdeaType.Bachelor};
            var idea2 = new Idea{Id = 2, Creator = supervisor1, Posted = DateTime.Now, Subject = "Data Intelligence", Title = "Data Intelligence is good", Description = "Data Intelligence gives value", AmountOfCollaborators = 1, Open = true, TimeToComplete = DateTime.Now-DateTime.Today, StartDate = DateTime.Now, Type = IdeaType.PhD};
            var idea3 = new Idea{Id = 3, Creator = supervisor2, Posted = DateTime.Now, Subject = "DevOps", Title = "DevOps is good", Description = "DevOps gives value", AmountOfCollaborators = 2, Open = true, TimeToComplete = DateTime.Now-DateTime.Today, StartDate = DateTime.Now, Type = IdeaType.Project};
            var idea4 = new Idea{Id = 4, Creator = supervisor1, Posted = DateTime.Now, Subject = "Requirements Elicitation", Title = "Requirements Elicitation is good", Description = "Requirements Elicitation gives value", AmountOfCollaborators = 5, Open = true, TimeToComplete = DateTime.Now-DateTime.Today, StartDate = DateTime.Now, Type = IdeaType.Masters};

            supervisor1.ideas.Add(idea2);
            supervisor1.ideas.Add(idea4);
            supervisor2.ideas.Add(idea1);
            supervisor2.ideas.Add(idea3);

            context.Ideas.AddRange(idea1, idea2, idea3, idea4);

            context.SaveChanges();
            _context = context;
            _repo = new IdeaRepository(_context);
            
        }


        /*[Fact]
        public void AddIdeaAsyncReturnsSomething(){

            TimeSpan ts1 = TimeSpan. FromDays(12);
            Type bach = IdeaType.Bachelor;
            var idea = new IdeaCreateDTO{CreatorId = 1, Title = "BigDataHandling", Subject =  "Database Managment", Description = "Interesting topic with the best supervisor (B-dog)", AmountOfCollaborators = 3, Open = true, TimeToComplete = null, StartDate = 2022-01-03, Type = null};
            var output = _repo.AddIdea(idea);

            Assert.Equal(HttpStatusCode.Created, output);
           
        }*/

        [Fact]
        public void FIndIdeaBySupervisor_given_invalid_UserId_returns_notFound_HttpStatusCode_and_invalid_ideas()
        {
            int supervisorid = 5;
            var ideasFromSupervisorId = _repo.FindIdeaBySupervisorAsync(supervisorid);
            List<IdeaDTO> expectedIdeas = new List<IdeaDTO>();

            /*var ideaDTO1 = new IdeaDTO {
                Id = 1,
                Title = "Big data is good",
                Subject = "Big Data",
                Type = IdeaType.Bachelor
            };

            var ideaDTO2 = new IdeaDTO {
                Id = 3,
                Title = "DevOps is good",
                Subject = "DevOps",
                Type = IdeaType.Project
            };*/

            var ideaDTO = new IdeaDTO{};

            expectedIdeas.Add(ideaDTO);

            var expected = (HttpStatusCode.NotFound, expectedIdeas);
            
            Assert.Equal(ideasFromSupervisorId, expected);

        }


        //Se evt. Ondfisk implementation.
        public void Dispose()
        {
            _context.Dispose();
        }
    }

}