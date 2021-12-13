

namespace YoghurtBank.Services
{
    public interface IUserRepository
    {
        Task<(Status status, UserDetailsDTO dto)>? FindUserByIdAsync(int userId);

        Task<(Status status, UserDetailsDTO dto)> CreateAsync(UserCreateDTO user);

        Task<(Status status, int? id)> DeleteAsync(int id);

        Task<(Status status, IReadOnlyCollection<UserDetailsDTO> supervisors)> GetAllSupervisors(); 

    }
}
