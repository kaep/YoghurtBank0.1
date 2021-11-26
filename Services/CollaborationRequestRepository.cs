namespace YoghurtBank.Services 
{
    public class CollaborationRequestRepository : ICollaborationRequestRepository
    {
        Task<IEnumerable<CollaborationRequestDetailsDTO>> FindRequestsByIdeaAsync(int ideaId) 
        {
            throw new NotImplementedException();
        }
        Task<IEnumerable<CollaborationRequestDetailsDTO>> FindRequestsByUserAsync(int userId) 
        {
            throw new NotImplementedException();

        }
        Task<UserDetailsDTO> AddCollaboratinRequestAsync(CollaborationRequestCreateDTO requestCreateDTO) 
        {
            throw new NotImplementedException();
        }
    }
}