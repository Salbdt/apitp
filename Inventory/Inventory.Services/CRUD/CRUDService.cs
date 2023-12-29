using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Services.Interfaces.CRUD;

namespace Inventory.Services.CRUD
{
    public class CRUDService<T> : IBaseService<T> where T: class
    {
        protected readonly ICRUDRepository<T> _repository;

        public CRUDService(ICRUDRepository<T> repository)
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

            return items;
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