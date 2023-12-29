using Inventory.Entities.Enums;

namespace Inventory.Entities
{
    public class InventoryMovement
    {
        public int Id { get; set; }
        public Product? Product { get; set; }
        public User? User { get; set; }
        public int Quantity { get; set; }
        public MovementType MovementType { get; set; }
        public DateTime MovementDate { get; set; }

        public static MovementType GetMovementType(string movementTypeText)
        {
            MovementType movementType = MovementType.COMPRA;
            if (movementTypeText.ToUpper().Equals("COMPRA"))
                movementType = MovementType.COMPRA;
            else if (movementTypeText.ToUpper().Equals("VENTA"))
                movementType = MovementType.VENTA;

            return movementType;
        }
    }
}