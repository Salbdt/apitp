using Inventory.Entities;

namespace Inventory.DTOs.Users
{
    public class UserPutDTO
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public UserPutDTO(User user)
        {
            Name        = user.Name;
            RoleId      = (user.Role is not null) ? user.Role.Id : 0;
            Email       = user.Email;
            Password    = user.Password;   
        }

        public User ToEntity()
        {
            Role? role = null;
            if (RoleId != 0)
                role = new Role
                {
                    Id = RoleId
                };

            return new User
            {
                Role        = role,
                Name        = Name,
                Email       = Email,
                Password    = Password
            };
        }
    }
}