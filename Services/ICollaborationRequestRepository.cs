
namespace YoghurtBank.Services
{
    public interface ICollaborationRequestRepository
    {
        Task<(Status status, CollaborationRequestDetailsDTO dto)> CreateAsync(CollaborationRequestCreateDTO request);
        Task<(Status status, CollaborationRequestDetailsDTO dto)> FindById(int id); //aka GET
        Task<(Status status, int id)> DeleteAsync(int id); 

        Task<(Status status, IReadOnlyCollection<CollaborationRequestDetailsDTO> requests)> FindRequestsByIdeaAsync(int ideaId);

        Task<(Status status, CollaborationRequestDetailsDTO dto)> UpdateAsync(int id, CollaborationRequestUpdateDTO updateRequest); 
        
        Task<(Status status, IReadOnlyCollection<CollaborationRequestDetailsDTO> requests)> FindRequestsBySupervisorAsync(int supervisorId);
        Task<(Status status, IReadOnlyCollection<CollaborationRequestDetailsDTO> requests)> FindRequestsByStudentAsync(int studentId);
    }
}