using Inventory.DTOs.Products;
using Inventory.Entities;

namespace Inventory.DTOs.InventoryStocks
{
    public class InventoryStockListDTO
    {
        public int Id { get; set; }
        public ProductListDTO? Product { get; set; }
        public int Quantity { get; set; }

        public InventoryStockListDTO()
        {
            
        }

        public InventoryStockListDTO(InventoryStock inventoryStock)
        {
            Id          = inventoryStock.Id;
            Product     = (inventoryStock.Product is not null) ? new ProductListDTO(inventoryStock.Product) : null;
            Quantity    = inventoryStock.Quantity;
        }

        public InventoryStock ToEntity()
        {
            return new InventoryStock
            {
                Id          = Id,
                Product     = Product?.ToEntity(),
                Quantity    = Quantity
            };
        }
    }
}