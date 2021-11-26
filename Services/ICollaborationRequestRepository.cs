using System;
using System.Collections.Generic;
using System.Text;
using System.Net.HttpStatusCode;

namespace YoghurtBank.Services
{
    public class ICollaborationRequestRepository
    {
        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByIdeaAsync(int ideaId);
        (HttpStatusCode, Task<IEnumerable<CollaborationRequestDetailsDTO>>) FindRequestsByUserAsync(int userId);
        (HttpStatusCode, Task<UserDetailsDTO>) AddCollaboratinRequestAsync(CollaborationRequestCreateDTO requestCreateDTO);
    }
}
