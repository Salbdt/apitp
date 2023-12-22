using Inventory.DTOs.Categories;
using Inventory.DTOs.Products;
using Inventory.Entities;

namespace Inventory.Services.Mappers
{
    public class ProductMapper
    {
        // ListDTO
        public static ProductListDTO ToListDTO(Product product)
        {
            CategoryListDTO? categoryListDTO = null;
            if (product.Category is not null)
                categoryListDTO = CategoryMapper.ToListDTO(product.Category);

            return new ProductListDTO
            {
                Id = product.Id,
                Category = categoryListDTO,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }

        public static Product ToEntity(ProductListDTO productListDTO)
        {
            Category? category = null;
            if (productListDTO.Category is not null)
                category = CategoryMapper.ToEntity(productListDTO.Category);

            return new Product
            {
                Id = productListDTO.Id,
                Category = category,
                Name = productListDTO.Name,
                Description = productListDTO.Description,
                Price = productListDTO.Price
            };
        }

        // PutDTO
        public static ProductPutDTO ToPutDTO(Product product)
        {
            int categoryId = 0;
            if (product.Category is not null)
                categoryId = product.Category.Id;

            return new ProductPutDTO
            {
                Name = product.Name,
                CategoryId = categoryId,
                Description = product.Description
            };
        }

        public static Product ToEntity(ProductPutDTO productPutDTO)
        {
            return new Product
            {
                Name = productPutDTO.Name,
                Category = new Category
                {
                    Id = productPutDTO.CategoryId
                },
                Description = productPutDTO.Description
            };
        }
    }
}