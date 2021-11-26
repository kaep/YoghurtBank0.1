using System.Threading.Tasks;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public interface IUserRepository
    {
        Task<UserDetailsDTO> FindUserAsync(int userId);
        Task<UserDetailsDTO> AddUserAsync(UserCreateDTO user);
       
    }
}
