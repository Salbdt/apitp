using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class CategoryService : BaseService<Category>, ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository repository) : base(repository)
        {
            _categoryRepository = repository;
        }

        public new async Task<Category?> CreateAsync(Category entity)
        {
            Category? category = null;
            if (entity.Name != "")
                category = await base.CreateAsync(entity);

            return category;
        }
    }
}