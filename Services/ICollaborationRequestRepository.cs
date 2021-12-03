using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface ICollaborationRequestRepository
    {

        Task<CollaborationRequestDetailsDTO> CreateAsync(CollaborationRequestCreateDTO request);
        Task<CollaborationRequestDetailsDTO> FindById(int id);
        Task< CollaborationRequestDetailsDTO> AddCollaborationRequestAsync(
            CollaborationRequestCreateDTO requestCreateDTO);
        

        Task<IEnumerable<CollaborationRequestDetailsDTO>> FindRequestsByIdeaAsync(int ideaId);
      

        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId);
    }
}