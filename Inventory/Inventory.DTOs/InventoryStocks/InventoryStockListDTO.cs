using Inventory.DTOs.Products;
using Inventory.DTOs.Users;

namespace Inventory.DTOs.InventoryStocks
{
    public class InventoryStockListDTO
    {
        public int Id { get; set; }
        public ProductListDTO? Product { get; set; }
        public int Quantity { get; set; }
    }
}