using Inventory.DTOs.Roles;
using Inventory.Entities;

namespace Inventory.DTOs.Users
{
    public class UserListDTO
    {
        public int Id { get; set; } = 0;
        public RoleListDTO? Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public UserListDTO(User user)
        {
            Id      = user.Id;
            Role    = (user.Role is not null) ? new RoleListDTO(user.Role) : null;
            Name    = user.Name;
            Email   = user.Email;            
        }

        public User ToEntity()
        {
            return new User
            {
                Id      = Id,
                Role    = Role?.ToEntity(),
                Name    = Name,
                Email   = Email
            };
        }
    }
}