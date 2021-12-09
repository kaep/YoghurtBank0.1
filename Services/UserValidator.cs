
namespace YoghurtBank.Data.Model
{
    public class UserValidator : IUserValidator
    {
        private readonly IUserRepository _repo; 

        public UserValidator(IUserRepository repo)
        {
            _repo = repo; 
        }


        public async Task<bool> UserWithEmailExists(string email)
        {
            return (await _repo.FindUserByEmail(email) != null); 
        }
    }
}