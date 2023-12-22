using Inventory.DTOs.Categories;
using Inventory.Entities;

namespace Inventory.Services.Mappers
{
    public class CategoryMapper
    {
        // ListDTO
        public static CategoryListDTO ToListDTO(Category category)
        {
            return new CategoryListDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static Category ToEntity(CategoryListDTO categoryListDTO)
        {
            return new Category
            {
                Id = categoryListDTO.Id,
                Name = categoryListDTO.Name,
                Description = categoryListDTO.Description
            };
        }

        // PutDTO
        public static CategoryPutDTO ToPutDTO(Category category)
        {
            return new CategoryPutDTO
            {
                Name = category.Name,
                Description = category.Description
            };
        }

        public static Category ToEntity(CategoryPutDTO categoryPutDTO)
        {
            return new Category
            {
                Name = categoryPutDTO.Name,
                Description = categoryPutDTO.Description
            };
        }
    }
}