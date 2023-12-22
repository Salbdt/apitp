using Inventory.DTOs.Roles;

namespace Inventory.DTOs.Users
{
    public class UserPutDTO
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}