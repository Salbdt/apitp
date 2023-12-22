namespace Inventory.Services.Interfaces
{
    public interface IBaseService<T> where T: class // Entity
    {
        Task<T>? CreateAsync(T entity);
        Task<List<T>> GetAllAsync();
        Task<T>? GetByIdAsync(int id);
        Task<bool> UpdateAsync(int id, T entity);
        Task<bool> DeleteAsync(int id);
    }
}