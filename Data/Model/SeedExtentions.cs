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

            var super1 = new Supervisor
            {
                Id = 2,
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                UserName = "Partyman",
                Email = "Partyman@gmail.com"
            };

             var super2 = new Supervisor
            {
                Id = 3,
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                UserName = "Morten",
                Email = "Morten@gmail.com"
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
                Posted = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                TimeToComplete = DateTime.UtcNow - DateTime.Today,
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
                Posted = DateTime.UtcNow,
                StartDate = DateTime.UtcNow,
                TimeToComplete = DateTime.UtcNow - DateTime.Today,
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

            var collabRequest4 = new CollaborationRequest
            {
                Id = 4,
                Requester = student1,
                Requestee = super2,
                Application = "Hail Hydra",
                Status = CollaborationRequestStatus.ApprovedBySupervisor,
                Idea = Idea2
            };


            if (!await context.Users.AnyAsync() && !await context.Ideas.AnyAsync() && !await context.CollaborationRequests.AnyAsync())
            {
                context.Users.Add(student1);
                context.Users.Add(super1);
                context.Users.Add(super2);
                context.Ideas.Add(Idea1);
                context.Ideas.Add(Idea2);
                context.CollaborationRequests.Add(collabRequest1);
                context.CollaborationRequests.Add(collabRequest2);
                context.CollaborationRequests.Add(collabRequest3);
                context.CollaborationRequests.Add(collabRequest4);
                await context.SaveChangesAsync();
            }
        }
    }
}




