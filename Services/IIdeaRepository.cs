
namespace YoghurtBank.Services
{
    public interface IIdeaRepository
    {
        Task<(Status status, IEnumerable<IdeaDTO> list)> FindIdeasBySupervisorIdAsync(int userId);

        Task<(Status status, IdeaDetailsDTO dto)> FindIdeaDetailsAsync(int IdeaId);

        Task<(Status status, IdeaDetailsDTO dto)> CreateAsync(IdeaCreateDTO idea);

        Task<(Status status , int? id)> DeleteAsync(int id);
        Task<(Status status, IdeaDetailsDTO dto)> UpdateAsync(int id, IdeaUpdateDTO update);

        Task<(Status status, IReadOnlyCollection<IdeaDetailsDTO> ideas)> ReadAllAsync();
    }
}
