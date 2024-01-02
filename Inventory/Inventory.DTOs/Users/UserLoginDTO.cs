using Inventory.DTOs.Roles;
using Inventory.Entities;

namespace Inventory.DTOs.Users
{
    public class UserLoginDTO
    {
        public int Id { get; set; } = 0;
        public RoleListDTO? Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public UserLoginDTO()
        {
                   
        }

        public UserLoginDTO(User user, string token)
        {
            Role    = (user.Role is not null) ? new RoleListDTO(user.Role) : null;
            Id      = user.Id;
            Name    = user.Name;
            Email   = user.Email;      
            Token   = token;      
        }

        public User ToEntity()
        {
            return new User
            {
                Role    = Role?.ToEntity(),
                Id      = Id,
                Name    = Name,
                Email   = Email
            };
        }
    }
}