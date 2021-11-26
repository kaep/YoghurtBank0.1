using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoghurtBank.Data.Model;

namespace YoghurtBank.Services
{
    public class UserRepository : IUserRepository
    {
        //der skal være et readonly felt med en dbcontext, vi skal først installere EF core i projektet 
        //private readonly ProjectContext _context;
        public Task<UserDetailsDTO> AddUserAsync(UserCreateDTO user)
        {
            throw new NotImplementedException();
        }

        public Task<UserDetailsDTO> FindUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
