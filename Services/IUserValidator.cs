
namespace YoghurtBank.Data.Model
{
    public interface IUserValidator
    {

        Task<bool> UserWithEmailExists(string email); 

    }
}