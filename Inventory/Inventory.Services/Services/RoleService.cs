using Inventory.DTOs.Roles;
using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;
using Inventory.Services.Mappers;

namespace Inventory.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role>? CreateAsync(Role entity)
        {
            return null;
        }

        public async Task<List<Role>> GetAllAsync()
        {
            var items = await _roleRepository.GetAllAsync();

            return items.ToList();
        }

        public async Task<Role>? GetByIdAsync(int id)
        {
            Role role = await _roleRepository.GetByIdAsync(id);

            return (role.Id != 0) ? role : null;
        }
        
        public async Task<bool> UpdateAsync(int id, Role entity)
        {
            return false;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return false;
        }
    }
}