using System.Threading.Tasks;
using YoghurtBank.Data.Model;
using System.Net;

namespace YoghurtBank.Services
{
    public interface IUserRepository
    {
        (HttpStatusCode, Task<UserDetailsDTO>) FindUserByIdAsync(int userId);
        (HttpStatusCode, Task<UserDetailsDTO>) AddUserAsync(UserCreateDTO user);
       
    }
}
