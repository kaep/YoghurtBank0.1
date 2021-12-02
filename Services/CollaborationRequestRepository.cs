using YoghurtBank.Data.Model;
using System.Net;
using YoghurtBank.Infrastructure;

namespace YoghurtBank.Services 
{
   
    public class CollaborationRequestRepository : ICollaborationRequestRepository
    {
        private readonly IYoghurtContext _context;
        public CollaborationRequestRepository(IYoghurtContext context)
        {
            _context = context;
        }
        public (HttpStatusCode, Task<UserDetailsDTO>) AddCollaboratinRequestAsync(CollaborationRequestCreateDTO requestCreateDTO)
        {
            throw new NotImplementedException();
        }

        public (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByIdeaAsync(int ideaId)
        {
            throw new NotImplementedException();
        }

        public (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}