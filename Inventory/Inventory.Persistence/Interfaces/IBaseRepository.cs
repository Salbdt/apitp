namespace Inventory.Persistence.Interfaces
{
    public interface IBaseRepository<T> where T: class
    {
        Task<T?> CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}