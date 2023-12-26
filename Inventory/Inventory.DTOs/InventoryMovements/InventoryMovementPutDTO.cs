using Inventory.Entities.Enums;

namespace Inventory.DTOs.InventoryMovements
{
    public class InventoryMovementPutDTO
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
    }
}