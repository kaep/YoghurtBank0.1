

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

        public async Task<(Status status, UserDetailsDTO dto)> CreateAsync(UserCreateDTO user)
        {
            if (user.UserType == "Student")
            {
                var res = await CreateStudent(user);
                return (Status.Created, res.user);
            }
            else if (user.UserType == "Supervisor")
            {
                var res = await CreateSupervisor(user);
                return (Status.Created, res.user);
            }
            else
            {
                return (Status.BadRequest, null); 
            }
        }

        //TODO: i removed the http status code, and had to make user nullable
        public async Task<(Status status, UserDetailsDTO? dto)> FindUserByIdAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);

            if (user == null)
            {
                return (Status.NotFound, null);
            }
            else
            {
                UserDetailsDTO userDetailsDTO = null;

                if (user.GetType() == typeof(Student))
                {
                    userDetailsDTO = new UserDetailsDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        UserType = "Student",
                        Email = user.Email
                    };
                }
                else
                {
                    userDetailsDTO = new UserDetailsDTO
                    {
                        Id = user.Id,
                        UserName = user.UserName,
                        UserType = "Supervisor",
                        Email = user.Email
                    };
                }

                return (Status.Found, (userDetailsDTO));
            }
        }

        public async Task<(Status status, int? id)> DeleteAsync(int id)
        {
            var entity = await _context.Users.FindAsync(id);
            if (entity == null)
            {
                return (Status.NotFound, null); //BAD - what to do with the -1
            }
            _context.Users.Remove(entity);
            await _context.SaveChangesAsync();
            return (Status.Deleted, entity.Id);
        }


        public async Task<(Status status, IReadOnlyCollection<UserDetailsDTO> supervisors)> GetAllSupervisors()
        {
            // var users = await _context.Users.Where(c => c.GetType() == typeof(Supervisor))
            // .Select(u => new UserDetailsDTO
            // {
            //     Id = u.Id,
            //     UserName = u.UserName,
            //     UserType = "Supervisor",
            //     Email = u.Email

            // }).ToListAsync();


            //Det her er meget grimt, plz få det til at virke med linq 
            var returnlist = new List<UserDetailsDTO>();
            foreach(var user in _context.Users)
            {
                if(user.GetType() == typeof(Supervisor))
                {
                    returnlist.Add(new UserDetailsDTO{Id = user.Id, UserName = user.UserName, UserType = "Supervisor", Email = user.Email});
                }
            }

            return (Status.Found, returnlist.AsReadOnly());
        }

        private async Task<(Status status, UserDetailsDTO user)> CreateStudent(UserCreateDTO user)
        {
            var entity = new Student
            {
                UserName = user.UserName,
                CollaborationRequests = new List<CollaborationRequest>(),
                Email = user.Email
            };
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return (Status.Created, new UserDetailsDTO
            {
                Id = entity.Id,
                UserName = entity.UserName,
                UserType = "Student",
                Email = entity.Email
            });
        }

        private async Task<(Status status, UserDetailsDTO user)> CreateSupervisor(UserCreateDTO user)
        {
            var entity = new Supervisor
            {
                UserName = user.UserName,
                CollaborationRequests = new List<CollaborationRequest>(),
                Ideas = new List<Idea>(),
                Email = user.Email
            };
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            return (Status.Created, new UserDetailsDTO
            {
                Id = entity.Id,
                UserName = entity.UserName,
                UserType = "Supervisor",
                Email = entity.Email
            });
        }
        
    }
}
