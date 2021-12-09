
namespace YoghurtBank.Data.Model
{
    public record UserDTO(int Id, string UserName);

    public record UserDetailsDTO
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string UserType { get; set; }
        [EmailAddress]
        public string Email { get; init; }
    }

    public record UserCreateDTO
    {
        public string UserName { get; init; }
        public string UserType { get; init; }
        [EmailAddress]
        public string Email { get; init; }
    }


}