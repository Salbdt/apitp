using Inventory.Entities;

namespace Inventory.DTOs.InventoryStocks
{
    public class InventoryStockPutDTO
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public InventoryStockPutDTO()
        {
            
        }

        public InventoryStockPutDTO(InventoryStock inventoryStock)
        {
            ProductId   = (inventoryStock.Product is not null) ? inventoryStock.Product.Id : 0;
            Quantity    = inventoryStock.Quantity;                       
        }

        public InventoryStock ToEntity()
        {
            Product? product = null;
            if (ProductId != 0)
                product = new Product
                {
                    Id = ProductId
                };

            return new InventoryStock
            {
                Product     = product,
                Quantity    = Quantity
            };
        }
    }
}