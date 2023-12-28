using Inventory.Entities;
using Inventory.Persistence.Interfaces;
using Inventory.Services.Interfaces;

namespace Inventory.Services
{
    public class InventoryMovementService : BaseService<InventoryMovement>, IInventoryMovementService
    {
        private readonly IInventoryMovementRepository _inventoryMovementRepository;

        public InventoryMovementService(IInventoryMovementRepository repository) : base(repository)
        {
            _inventoryMovementRepository = repository;
        }

        public new async Task<InventoryMovement?> CreateAsync(InventoryMovement entity)
        {
            InventoryMovement? inventoryMovement = null;
            if (entity.Product is not null && entity.Product.Id != 0 && entity.User is not null && entity.User.Id != 0 && entity.Quantity != 0)
                inventoryMovement = await base.CreateAsync(entity);

            return inventoryMovement;
        }        
    }
}