using Inventory.DTOs.Roles;
using Inventory.DTOs.Users;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserListDTO> CreateAsync(UserPutDTO entity)
        {
            return null;
        }

        public async Task<List<UserListDTO>> GetAllAsync()
        {
            List<UserListDTO> usersDTO = new List<UserListDTO>();
            RoleListDTO roleDTO;

            // Llamamos al repositorio para obtener un Enumerable
            var users = await _userRepository.GetAllAsync();

            // Transformamos el enumerable en una lista DTO
            foreach (var user in users)
            {
                if (user.Role is not null)
                {
                    roleDTO = new RoleListDTO
                    {
                    Id = user.Role.Id,
                    Name = user.Role.Name
                    };
                }
                else
                {
                    roleDTO = new RoleListDTO();
                }


                usersDTO.Add(
                    new UserListDTO
                    {
                        Id = user.Id,
                        Role = roleDTO,
                        Name = user.Name,
                        Email = user.Email
                    }
                );
            }

            return usersDTO;
        }

        public async Task<UserListDTO> GetByIdAsync(int id)
        {
            UserListDTO userDTO = null;

            // Llamamos al repositorio para obtener un Role
            User user = await _userRepository.GetByIdAsync(id);


            // Transformamos el enumerable en una lista DTO
            if (user.Id != 0)
            {
                RoleListDTO roleDTO = new RoleListDTO();

                if (user.Role is not null)
                {
                    roleDTO = new RoleListDTO
                    {
                        Id = user.Role.Id,
                        Name = user.Role.Name
                    };
                }

                userDTO = new UserListDTO
                {
                    Id = user.Id,
                    Role = roleDTO,
                    Name = user.Name,
                    Email = user.Email
                };
            }

            return userDTO;
        }
        
        public async Task<bool> UpdateAsync(int id, UserPutDTO entity)
        {
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return false;
        }
    }
}