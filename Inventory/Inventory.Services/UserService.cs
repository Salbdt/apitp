using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository repository) : base(repository)
        {
            _userRepository = repository;
        }
        
        public async Task<bool> UpdateAsync(int id, string email, string password, User entity)
        {
            return await _userRepository.UpdateAsync(id, email, password, entity);
        }
    }
}