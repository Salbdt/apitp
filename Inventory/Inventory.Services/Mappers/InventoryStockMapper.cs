using Inventory.DTOs.InventoryStocks;
using Inventory.DTOs.Products;
using Inventory.Entities;

namespace Inventory.Services.Mappers
{
    public class InventoryStockMapper
    {
        // ListDTO
        public static InventoryStockListDTO ToListDTO(InventoryStock inventoryStock)
        {
            ProductListDTO? productListDTO = null;
            if (inventoryStock.Product is not null)
                productListDTO = ProductMapper.ToListDTO(inventoryStock.Product);

            return new InventoryStockListDTO
            {
                Id = inventoryStock.Id,
                Product = productListDTO,
                Quantity = inventoryStock.Quantity
            };
        }

        public static InventoryStock ToEntity(InventoryStockListDTO inventoryStockListDTO)
        {
            Product? product = null;
            if (inventoryStockListDTO.Product is not null)
                product = ProductMapper.ToEntity(inventoryStockListDTO.Product);

            return new InventoryStock
            {
                Id = inventoryStockListDTO.Id,
                Product = product,
                Quantity = inventoryStockListDTO.Quantity,
            };
        }

        // PutDTO
        public static InventoryStockPutDTO ToPutDTO(InventoryStock inventoryStock)
        {
            int productId = 0;
            if (inventoryStock.Product is not null)
                productId = inventoryStock.Product.Id;

            return new InventoryStockPutDTO
            {
                ProductId = productId,
                Quantity = inventoryStock.Quantity,
            };
        }

        public static InventoryStock ToEntity(InventoryStockPutDTO inventoryStockPutDTO)
        {
            return new InventoryStock
            {
                Product = new Product
                {
                    Id = inventoryStockPutDTO.ProductId
                },
                Quantity = inventoryStockPutDTO.Quantity,
            };
        }
    }
}