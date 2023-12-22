using Inventory.DTOs.Products;
using Inventory.DTOs.Users;

namespace Inventory.DTOs.InventoryStocks
{
    public class InventoryStockPutDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}