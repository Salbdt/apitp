using Inventory.Entities;
using Inventory.Persistence.Interfaces.CRUD;
using Inventory.Services.Interfaces.CRUD;

namespace Inventory.Services.CRUD
{
    public class InventoryMovementService : CRUDService<InventoryMovement>, IInventoryMovementService
    {
        private readonly IInventoryMovementRepository _inventoryMovementRepository;

        public InventoryMovementService(IInventoryMovementRepository repository) : base(repository)
        {
            _inventoryMovementRepository = repository;
        }

        public new async Task<InventoryMovement?> CreateAsync(InventoryMovement entity)
        {
            InventoryMovement? inventoryMovement = null;
            if (entity.Product is not null && entity.Product.Id != 0 && entity.User is not null && entity.User.Id != 0 && entity.Quantity > 0)
                inventoryMovement = await base.CreateAsync(entity);

            return inventoryMovement;
        }        
    }
}