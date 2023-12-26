using Inventory.DTOs.Categories;
using Inventory.Entities;

namespace Inventory.DTOs.Products
{
    public class ProductListDTO
    {
        public int Id { get; set; }
        public CategoryListDTO? Category { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ProductListDTO()
        {
            
        }

        public ProductListDTO(Product product)
        {
            Id          = product.Id;
            Category    = (product.Category is not null) ? new CategoryListDTO(product.Category) : null;
            Name        = product.Name;
            Description = product.Description;
            Price       = product.Price;
        }

        public Product ToEntity()
        {
            return new Product
            {
                Id          = Id,
                Category    = Category?.ToEntity(),
                Name        = Name,
                Description = Description,
                Price       = Price
            };
        }
    }
}