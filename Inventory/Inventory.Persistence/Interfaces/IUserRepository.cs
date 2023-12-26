using Inventory.Entities;

namespace Inventory.Persistence.Interfaces
{
    public interface IUserRepository : IBaseRepository<User>
    {
        Task<bool> UpdateAsync(int id, string email, string password, User entity);
    }
}