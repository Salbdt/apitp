using Inventory.Entities;

namespace Inventory.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
        Task<bool> UpdateAsync(int id, string email, string password, User entity);
    }
}