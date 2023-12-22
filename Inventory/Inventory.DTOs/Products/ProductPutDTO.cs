using Inventory.DTOs.Categories;

namespace Inventory.DTOs.Products
{
    public class ProductPutDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }        
    }
}