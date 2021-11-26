using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoghurtBank.Data.Model;
using System.Net;

namespace YoghurtBank.Services
{
    public class UserRepository : IUserRepository
    {
        //der skal være et readonly felt med en dbcontext, vi skal først installere EF core i projektet 
        //private readonly ProjectContext _context;

        public (HttpStatusCode, Task<UserDetailsDTO>) AddUserAsync(UserCreateDTO user)
        {
            throw new NotImplementedException();
        }

        public (HttpStatusCode, Task<UserDetailsDTO>) FindUserByIdAsync(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
