using Inventory.DTOs.Categories;
using Inventory.Entities;

namespace Inventory.DTOs.Products
{
    public class ProductPutDTO
    {
        public int CategoryId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }

        public ProductPutDTO()
        {
            
        }

        public ProductPutDTO(Product product)
        {
            CategoryId  = (product.Category is not null) ? product.Category.Id : 0;
            Name        = product.Name;
            Description = product.Description;
            Price       = product.Price;                        
        }

        public Product ToEntity()
        {
            Category? category = null;
            if (CategoryId != 0)
                category = new Category
                {
                    Id = CategoryId
                };

            return new Product
            {
                Category    = category,
                Name        = Name,
                Description = Description,
                Price       = Price
            };
        }
    }
}