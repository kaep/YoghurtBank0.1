using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public class IdeaRepository : IIdeaRepository
    {
        //der skal være et readonly felt med en dbcontext, vi skal først installere EF core i projektet 
        //private readonly ProjectContext _context;
        public (HttpStatusCode, int) CreateIdea(IdeaCreateDTO idea)
        {
            throw new NotImplementedException();
        }

        public (HttpStatusCode, Task<IEnumerable<IdeaDTO>>) FindIdeaBySupervisorAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public (HttpStatusCode, Task<IdeaDetailsDTO>) FindIdeaDetailsAsync(int IdeaId)
        {
            throw new NotImplementedException();
        }
    }
}
