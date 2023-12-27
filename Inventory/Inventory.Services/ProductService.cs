using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository repository) : base(repository)
        {
            _productRepository = repository;
        }

        public async Task<Product?> CreateAsync(Product entity)
        {
            Product? product = null;
            if (entity.Category is null || entity.Category.Id == 0 || entity.Name != "")
                product = await base.CreateAsync(entity);

            return product;
        }
    }
}