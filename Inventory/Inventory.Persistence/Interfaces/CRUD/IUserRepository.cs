using Inventory.Entities;

namespace Inventory.Persistence.Interfaces.CRUD
{
    public interface IUserRepository : ICRUDRepository<User>
    {
        Task<bool> ExistsAsync(string email);
        Task<User> LoginAsync(string email, string password);
        Task<bool> UpdateAsync(int id, string email, string password, User entity);
    }
}