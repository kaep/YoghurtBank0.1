using System.Net;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface ICollaborationRequestRepository
    {
        (HttpStatusCode, Task<UserDetailsDTO>) AddCollaboratinRequestAsync(CollaborationRequestCreateDTO requestCreateDTO);
        

        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByIdeaAsync(int ideaId);
      

        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId);
    }
}