using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class BaseService<T> : IBaseService<T> where T: class
    {
        protected readonly IBaseRepository<T> _repository;

        public BaseService(IBaseRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<T?> CreateAsync(T entity)
        {
            return await _repository.CreateAsync(entity);
        }

        public async Task<List<T>> GetAllAsync()
        {
            var items = await _repository.GetAllAsync();

            return items.ToList();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }
        
        public async Task<bool> UpdateAsync(int id, T entity)
        {
            return await _repository.UpdateAsync(id, entity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}