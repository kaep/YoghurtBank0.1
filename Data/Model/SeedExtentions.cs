using Microsoft.EntityFrameworkCore;
using YoghurtBank.Infrastructure;

namespace YoghurtBank.Data.Model
{
    public static class SeedExtensions
    {
        public static async Task<IHost> SeedAsync(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<YoghurtContext>();

                await SeedAsync(context);
            }
            return host;
        }
            
        
        private static async Task SeedAsync(YoghurtContext context)
        {
            await context.Database.MigrateAsync();
            var student1 = new Student
            {
                Id = 1,
                UserName = "Henning",
                CollaborationRequests = new List<CollaborationRequest>(),
                Email = "HenningG@gmail.com"
            };

            var student2 = new Student
            {
                Id = 2,
                UserName = "Mads",
                CollaborationRequests = new List<CollaborationRequest>(),
                Email = "Minmail@webspeed.dk"
            };

            var student3 = new Student
            {
                Id = 3,
                UserName = "Sasha",
                CollaborationRequests = new List<CollaborationRequest>(),
                Email = "slarsen@mails.com"
            };

            var super1 = new Supervisor
            {
                Id = 4,
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                UserName = "Roberto O'boto",
                Email = "Roboto@university.com"
            };

            var super2 = new Supervisor
            {
                Id = 5,
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                UserName = "Morten",
                Email = "Morten@gmail.com"
            };
            

            var Idea1 = new Idea
            {
                Id = 1,
                Subject = "Algorithms and Data Structures",
                Title = "Algorithmic Problem Solving",
                Description = "In this project, students will be working with lorem ipsum dolor",
                AmountOfCollaborators = 3,
                Creator = super1,
                Open = true,
                Posted = DateTime.UtcNow,
                StartDate = new DateTime(2022, 28, 4),
                TimeToComplete = new DateTime(2022, 21, 7) - new DateTime(2022, 28, 4),
                Type = IdeaType.Project
            };

            var Idea2 = new Idea
            {
                Id = 2,
                Subject = "Work/life balance",
                Title = "Work life balance at ITU",
                Description = "In this project it will be investigated how students at ITU combine work, sparetime and family time",
                AmountOfCollaborators = 9,
                Creator = super2,
                Open = true,
                Posted = DateTime.UtcNow,
                StartDate = new DateTime(2022, 11, 3),
                TimeToComplete = new DateTime(2022, 21, 5) - new DateTime(2022, 11, 3),
                Type = IdeaType.Project
            };

            var Idea3 = new Idea
            {
                Id = 3,
                Subject = "Medicine",
                Title = "Tranquilizers and their effects on persian cats in captivity",
                Description = "In this project, students will be....",
                AmountOfCollaborators = 2,
                Creator = super1,
                Open = true,
                Posted = DateTime.UtcNow,
                StartDate = new DateTime(2022, 1, 1),
                TimeToComplete = new DateTime(2022, 1, 6) - new DateTime(2022, 1, 1),
                Type = IdeaType.PhD
            };

            var Idea4 = new Idea
            {
                Id = 4,
                Subject = "Lorem ipsum and its many uses",
                Title = "Amet luctus at, scelerisque a augue.",
                Description = "Proin sed suscipit nisi. Fusce volutpat eros eget consectetur faucibus. Nunc vel accumsan nunc.",
                AmountOfCollaborators = 3,
                Creator = super2,
                Open = true,
                Posted = new DateTime(2021, 31, 10),
                StartDate = new DateTime(2022, 1, 1),
                TimeToComplete = new DateTime(2022, 1, 12) - new DateTime(2022, 1, 1),
                Type = IdeaType.Bachelor
            };

            var Idea5 = new Idea
            {
                Id = 5,
                Subject = "Lorem ipsum",
                Title = "Suspendisse nisl nisl, imperdiet sit.",
                Description = "In eget dui et tellus accumsan pellentesque. Fusce volutpat eros eget consectetur faucibus.  Praesent id lectus sagittis, condimentum ex ut, porta orci. Mauris gravida sed leo non feugiat. Duis vitae aliquam massa, quis convallis nunc. Praesent diam dolor, bibendum et cursus dapibus, egestas id purus. Phasellus ut pellentesque massa.",
                AmountOfCollaborators = 1,
                Creator = super2,
                Open = true,
                Posted = new DateTime(2021, 15, 9),
                StartDate = new DateTime(2022, 15, 3),
                TimeToComplete = new DateTime(2022, 15, 5) - new DateTime(2022, 15, 3),
                Type = IdeaType.Masters
            };

            var collabRequest1 = new CollaborationRequest
            {
                Id = 1,
                Requester = student1,
                Requestee = super1,
                Application = "I love data structures sooooo much",
                Status = CollaborationRequestStatus.Waiting,
                Idea = Idea1
            };

            var collabRequest2 = new CollaborationRequest
            {
                Id = 2,
                Requester = student1,
                Requestee = super1,
                Application = "I find algorithms to be interesting",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea1
            };

            var collabRequest3 = new CollaborationRequest
            {
                Id = 3,
                Requester = student1,
                Requestee = super2,
                Application = "I think that it would be good to fix some th eproblems with work/life balance",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea2
            };

            var collabRequest4 = new CollaborationRequest
            {
                Id = 4,
                Requester = student3,
                Requestee = super2,
                Application = "LAtin example text is like, so interesting!",
                Status = CollaborationRequestStatus.Declined,
                Idea = Idea4
            };

            var collabRequest5 = new CollaborationRequest
            {
                Id = 5,
                Requester = student2,
                Requestee = super1,
                Application = "Lol, i take medicines sometimes, so i think i would be fun to work eith it ",
                Status = CollaborationRequestStatus.ApprovedByStudent,
                Idea = Idea3
            };

            var collabRequest6 = new CollaborationRequest
            {
                Id = 6,
                Requester = student1,
                Requestee = super1,
                Application = "i would really like to work on this project because...",
                Status = CollaborationRequestStatus.Waiting,
                Idea = Idea3
            };


            if (!await context.Users.AnyAsync() && !await context.Ideas.AnyAsync() && !await context.CollaborationRequests.AnyAsync())
            {
                context.Users.Add(student1);
                context.Users.Add(student2);
                context.Users.Add(student3);
                context.Users.Add(super1);
                context.Users.Add(super2);
                context.Ideas.Add(Idea1);
                context.Ideas.Add(Idea2);
                context.Ideas.Add(Idea3);
                context.Ideas.Add(Idea4);
                context.Ideas.Add(Idea5);
                context.CollaborationRequests.Add(collabRequest1);
                context.CollaborationRequests.Add(collabRequest2);
                context.CollaborationRequests.Add(collabRequest3);
                context.CollaborationRequests.Add(collabRequest4);
                context.CollaborationRequests.Add(collabRequest5);
                context.CollaborationRequests.Add(collabRequest6);
                await context.SaveChangesAsync();
            }
        }
    }
}




