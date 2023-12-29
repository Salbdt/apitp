using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Services.Interfaces.CRUD;

namespace Inventory.Services.CRUD
{
    public class ProductService : CRUDService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository) : base(repository)
        {
            _productRepository = repository;
        }

        public new async Task<Product?> CreateAsync(Product entity)
        {
            Product? product = null;
            if (entity.Category is not null && entity.Category.Id != 0 && entity.Name != "")
                product = await base.CreateAsync(entity);

            return product;
        }
    }
}