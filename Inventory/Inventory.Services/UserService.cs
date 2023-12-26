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

        public async Task<User?> CreateAsync(User entity)
        {
            return await _userRepository.CreateAsync(entity);
        }

        public async Task<List<User>> GetAllAsync()
        {
            var items = await _userRepository.GetAllAsync();

            return items.ToList();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }
        
        public async Task<bool> UpdateAsync(int id, string email, string password, User entity)
        {
            return await _userRepository.UpdateAsync(id, email, password, entity);
        }
        
        public async Task<bool> UpdateAsync(int id, User entity)
        { // No implementado
            return await _userRepository.UpdateAsync(id, entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _userRepository.DeleteAsync(id);
        }
    }
}