using Inventory.DTOs.Roles;
using Inventory.DTOs.Users;
using Inventory.Entities;

namespace Inventory.Services.Mappers
{
    public class UserMapper
    {
        // ListDTO
        public static UserListDTO ToListDTO(User user)
        {
            RoleListDTO? roleListDTO = null;
            if (user.Role is not null)
                roleListDTO = RoleMapper.ToListDTO(user.Role);

            return new UserListDTO
            {
                Id = user.Id,
                Role = roleListDTO,
                Name = user.Name,
                Email = user.Email
            };
        }

        public static User ToEntity(UserListDTO userListDTO)
        {
            Role? role = null;
            if (userListDTO.Role is not null)
                role = RoleMapper.ToEntity(userListDTO.Role);

            return new User
            {
                Id = userListDTO.Id,
                Role = role,
                Name = userListDTO.Name,
                Email = userListDTO.Email
            };
        }

        // PutDTO
        public static UserPutDTO ToPutDTO(User user)
        {
            int roleID = 0;
            if (user.Role is not null)
                roleID = user.Role.Id;

            return new UserPutDTO
            {
                Name = user.Name,
                RoleId = roleID,
                Email = user.Email
            };
        }

        public static User ToEntity(UserPutDTO userPutDTO)
        {
            return new User
            {
                Name = userPutDTO.Name,
                Role = new Role
                {
                    Id = userPutDTO.RoleId
                },
                Email = userPutDTO.Email
            };
        }
    }
}