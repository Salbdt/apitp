using Inventory.Entities;
using Inventory.Entities.Enums;

namespace Inventory.DTOs.InventoryMovements
{
    public class InventoryMovementPutDTO
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public string MovementType { get; set; } = string.Empty;

        public InventoryMovementPutDTO()
        {
            
        }

        public InventoryMovementPutDTO(InventoryMovement inventoryMovement)
        {
            ProductId       = (inventoryMovement.Product is not null) ? inventoryMovement.Product.Id : 0;
            UserId          = (inventoryMovement.User is not null) ? inventoryMovement.User.Id : 0;
            Quantity        = inventoryMovement.Quantity;
            MovementType    = inventoryMovement.MovementType.ToString();
        }

        public InventoryMovement ToEntity()
        {
            Product? product = null;
            if (ProductId != 0)
                product = new Product
                {
                    Id = ProductId
                };
                
            User? user = null;
            if (UserId != 0)
                user = new User
                {
                    Id = UserId
                };

            return new InventoryMovement
            {
                Product         = product,
                User            = user,
                Quantity        = this.Quantity,
                MovementType    = InventoryMovement.GetMovementType(this.MovementType)
            };
        }
    }
}