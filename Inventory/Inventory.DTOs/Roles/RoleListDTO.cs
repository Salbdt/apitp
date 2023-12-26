using Inventory.Entities;

namespace Inventory.DTOs.Roles
{
    public class RoleListDTO
    {
        public int Id { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public RoleListDTO()
        {
            
        }

        public RoleListDTO(Role role)
        {
            Id          = role.Id;
            Name        = role.Name;
            Description = role.Description;
        }

        public Role ToEntity()
        {
            return new Role
            {
                Id          = Id,
                Name        = Name,
                Description = Description
            };
        }
    }
}