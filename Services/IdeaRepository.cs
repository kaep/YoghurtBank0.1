using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using Microsoft.EntityFrameworkCore;
using YoghurtBank.Data.Model;
using YoghurtBank.Infrastructure;

namespace YoghurtBank.Services
{
    public class IdeaRepository : IIdeaRepository
    {
        private readonly IYoghurtContext _context;

        public IdeaRepository(IYoghurtContext context) {
            _context = context;
        }
        public (HttpStatusCode, int) CreateIdea(IdeaCreateDTO idea)
        {
            throw new NotImplementedException();
        }
        
        public (HttpStatusCode, Task<IdeaDetailsDTO>) AddIdeaAsync(IdeaCreateDTO idea)
        {
            throw new NotImplementedException();
        }
        

        public async Task<(HttpStatusCode, IdeaDetailsDTO)> FindIdeaDetailsAsync(int IdeaId)
        {
           /*var idea = await _context.Ideas.Where(i => i.Id == IdeaId).Select(i =>
           new IdeaDetailsDTO() 
           {

               Id = i.Id,
               Title = i.Title,
               Subject = i.Subject,
               Posted = i.Posted,
               Description = i.Description,
               AmountOfCollaborators = i.AmountOfCollaborators,
               Open = i.Open,
               TimeToComplete = i.TimeToComplete,
               StartDate = i.StartDate
           }
           );*/

           var idea = await _context.Ideas.FindAsync(IdeaId);


            throw new NotImplementedException();
        }

        public (HttpStatusCode, IEnumerable<IdeaDTO>) FindIdeaBySupervisorAsync(int userId)
        {
           
            var ideas = _context.Ideas.Where(i => i.Id == userId).Select(i =>
            new IdeaDTO {
                Id = i.Id,
                Title = i.Title,
                Subject = i.Subject,
                Type = i.Type
            }).ToList();
           
            if (ideas == null) 
            {
                return (HttpStatusCode.NotFound, ideas);
            } else 
            {
                throw new NotImplementedException();
            }
            
        }
    }
}
