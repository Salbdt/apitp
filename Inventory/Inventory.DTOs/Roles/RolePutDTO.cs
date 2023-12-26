using Inventory.Entities;

namespace Inventory.DTOs.Roles
{
    public class RolePutDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public RolePutDTO(Role role)
        {
            Name        = role.Name;
            Description = role.Description;
        }

        public Role ToEntity()
        {
            return new Role
            {
                Name        = Name,
                Description = Description
            };
        }
    }
}