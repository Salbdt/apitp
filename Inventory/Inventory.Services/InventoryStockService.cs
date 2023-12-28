using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class InventoryStockService : BaseService<InventoryStock>, IInventoryStockService
    {
        private readonly IInventoryStockRepository _inventoryStockRepository;

        public InventoryStockService(IInventoryStockRepository repository) : base(repository)
        {
            _inventoryStockRepository = repository;
        }

        public new async Task<InventoryStock?> CreateAsync(InventoryStock entity)
        {
            InventoryStock? inventoryStock = null;
            if (entity.Product is not null && entity.Product.Id != 0 && entity.Quantity != 0)
                inventoryStock = await base.CreateAsync(entity);

            return inventoryStock;
        }
    }
}