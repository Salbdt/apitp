using Inventory.Entities;

namespace Inventory.Services.Interfaces.CRUD
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> ExistsAsync(string email);
        Task<User> LoginAsync(string email, string password);
        Task<bool> UpdateAsync(int id, string email, string password, User entity);
    }
}