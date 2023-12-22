using Inventory.DTOs.Roles;

namespace Inventory.DTOs.Users
{
    public class UserListDTO
    {
        public int Id { get; set; } = 0;
        public RoleListDTO? Role { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}