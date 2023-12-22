using Inventory.DTOs.Categories;

namespace Inventory.DTOs.Products
{
    public class ProductListDTO
    {
        public int Id { get; set; }
        public CategoryListDTO? Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}