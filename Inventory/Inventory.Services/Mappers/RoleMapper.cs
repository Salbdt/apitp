using Inventory.DTOs.Roles;
using Inventory.Entities;

namespace Inventory.Services.Mappers
{
    public static class RoleMapper
    {
        // ListDTO
        public static RoleListDTO ToListDTO(Role role)
        {
            return new RoleListDTO
            {
                Id = role.Id,
                Name = role.Name,
                Description = role.Description
            };
        }

        public static Role ToEntity(RoleListDTO roleListDTO)
        {
            return new Role
            {
                Id = roleListDTO.Id,
                Name = roleListDTO.Name,
                Description = roleListDTO.Description
            };
        }

        // PutDTO
        public static RolePutDTO ToPutDTO(Role role)
        {
            return new RolePutDTO
            {
                Name = role.Name,
                Description = role.Description
            };
        }

        public static Role ToEntity(RolePutDTO roleListDTO)
        {
            return new Role
            {
                Name = roleListDTO.Name,
                Description = roleListDTO.Description
            };
        }
    }
}