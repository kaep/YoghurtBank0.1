using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface ICollaborationRequestRepository
    {
        Task<(HttpStatusCode, CollaborationRequestDetailsDTO)> AddCollaborationRequestAsync(
            CollaborationRequestCreateDTO requestCreateDTO);
        

        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByIdeaAsync(int ideaId);
      

        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId);
    }
}