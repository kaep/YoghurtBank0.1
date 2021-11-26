using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoghurtBank.Data.Model
{
    public record UserDTO(int Id, string Username);

    public record UserDetailsDTO
    {
        int Id { get; set; }
        string Username { get; set; }
        ICollection<CollaborationRequest> Collaborations { get; set; }  
    }

    public record UserCreateDTO 
    {
        public string Username { get; init; }
        public string UserType { get; init; }
    }

}