using Inventory.DTOs.Products;
using Inventory.DTOs.Users;
using Inventory.Entities;
using Inventory.Entities.Enums;

namespace Inventory.DTOs.InventoryMovements
{
    public class InventoryMovementListDTO
    {
        public int Id { get; set; }
        public ProductListDTO? Product { get; set; }
        public UserListDTO? User { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime MovementDate { get; set; }

        public InventoryMovementListDTO()
        {
            
        }

        public InventoryMovementListDTO(InventoryMovement inventoryMovement)
        {
            Id              = inventoryMovement.Id;
            Product         = (inventoryMovement.Product is not null) ? new ProductListDTO(inventoryMovement.Product) : null;
            User            = (inventoryMovement.User is not null) ? new UserListDTO(inventoryMovement.User) : null;
            Quantity        = inventoryMovement.Quantity;
            MovementType    = inventoryMovement.MovementType;
            MovementDate    = inventoryMovement.MovementDate;
        }

        public InventoryMovement ToEntity()
        {
            return new InventoryMovement
            {
                Id              = Id,
                Product         = Product?.ToEntity(),
                User            = User?.ToEntity(),
                Quantity        = Quantity,
                MovementType    = MovementType,
                MovementDate    = MovementDate
            };
        }
    }
}