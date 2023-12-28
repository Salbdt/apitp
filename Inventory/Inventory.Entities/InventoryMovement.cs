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
            MovementType movementType = MovementType.INGRESO;
            if (movementTypeText.Equals("INGRESO"))
                movementType = MovementType.INGRESO;
            else if (movementTypeText.Equals("EGRESO"))
                movementType = MovementType.EGRESO;

            return movementType;
        }
    }
}