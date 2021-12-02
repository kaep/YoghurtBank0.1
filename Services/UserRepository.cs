using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YoghurtBank.Data.Model;
using YoghurtBank.Infrastructure;
using System.Net;

namespace YoghurtBank.Services
{
    public class UserRepository : IUserRepository
    {
        //der skal være et readonly felt med en dbcontext, vi skal først installere EF core i projektet 
        private readonly IYoghurtContext _context;

        public UserRepository(IYoghurtContext context)
        {
            _context = context;
        }

        public (HttpStatusCode, Task<UserDetailsDTO>) AddUserAsync(UserCreateDTO user)
        {
            User newUser;
            if (user.UserType == "Student")
            {
                newUser = new Student(UserName = user.UserName);
            }
            else 
            {
                newUser = new Supervisor(Id = null, UserName = user.UserName);
            }
            
            var returnUser = await _context.Users.Add(newUser);
            return (HttpStatusCode.OK, returnUser);
        }

        public (HttpStatusCode, Task<UserDetailsDTO>) FindUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userID);
            if (user == null)
            {
                return (HttpStatusCode.NotFound, user);
            }
            else 
            {
                return (HttpStatusCode.OK, user);
            }
        }
    }
}
