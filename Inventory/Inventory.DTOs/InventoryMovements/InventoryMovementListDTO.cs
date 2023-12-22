using Inventory.DTOs.Products;
using Inventory.DTOs.Users;
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
    }
}