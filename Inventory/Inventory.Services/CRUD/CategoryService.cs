using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Services.Interfaces.CRUD;

namespace Inventory.Services.CRUD
{
    public class CategoryService : CRUDService<Category>, ICategoryService
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