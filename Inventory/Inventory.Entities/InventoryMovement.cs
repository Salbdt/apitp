using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}