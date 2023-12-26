using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<Role?> CreateAsync(Role entity)
        { // No implementado
            return await _roleRepository.CreateAsync(entity);
        }

        public async Task<List<Role>> GetAllAsync()
        {
            var items = await _roleRepository.GetAllAsync();

            return items.ToList();
        }

        public async Task<Role?> GetByIdAsync(int id)
        {
            return await _roleRepository.GetByIdAsync(id);
        }
        
        public async Task<bool> UpdateAsync(int id, Role entity)
        { // No implementado
            return await _roleRepository.UpdateAsync(id, entity);
        }

        public async Task<bool> DeleteAsync(int id)
        { // No implementado
            return await _roleRepository.DeleteAsync(id);
        }
    }
}