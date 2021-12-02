using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
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
        public (HttpStatusCode, Task<IdeaDetailsDTO>) CreateIdea(IdeaCreateDTO idea)
        {
            throw new NotImplementedException();
        }
        
        public (HttpStatusCode, Task<IdeaDetailsDTO>) AddIdeaAsync(IdeaCreateDTO idea)
        {
            throw new NotImplementedException();
        }

        /*public async Task<(HttpStatusCode, Task<IEnumerable<IdeaDTO>>)> FindIdeaBySupervisorAsync(int userId)
        {
           //var idea = await _context.Ideas.FindAsync(userId);
           
           var idea = await _context.Ideas.FindAsync(userId).Select(new)
           
           var idea = from i in _context.Ideas
                    where i.Id == userId
                    select new IdeaDTO (
                        i.Id,
                        i.Title,
                        i.Subject,
                        i.Type
                    );

           if (idea == null) {
               return (HttpStatusCode.NotFound, idea);
           } else 
           {
                throw new NotImplementedException();
           }             
            
            
        }*/

        public (HttpStatusCode, Task<IdeaDetailsDTO>) FindIdeaDetailsAsync(int IdeaId)
        {
            throw new NotImplementedException();
        }
    }
}
