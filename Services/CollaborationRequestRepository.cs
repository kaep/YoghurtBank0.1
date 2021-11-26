using YoghurtBank.Data.Model;
using System.Net;

namespace YoghurtBank.Services 
{
    public class CollaborationRequestRepository : ICollaborationRequestRepository
    {
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