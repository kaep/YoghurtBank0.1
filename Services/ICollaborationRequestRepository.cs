using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface ICollaborationRequestRepository
    {

        Task<CollaborationRequestDetailsDTO> CreateAsync(CollaborationRequestCreateDTO request);
        Task<CollaborationRequestDetailsDTO> FindById(int id); //aka GET
        Task<int> DeleteAsync(int id); //returværdi skal overvejes -> det skal nok være noget status-agtigt

        Task<IEnumerable<CollaborationRequestDetailsDTO>> FindRequestsByIdeaAsync(int ideaId);

        Task<CollaborationRequestDetailsDTO> UpdateAsync(int id, CollaborationRequestUpdateDTO updateRequest); //return value? like delete
        
        
        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId);
    }
}