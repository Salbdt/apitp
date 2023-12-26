using Inventory.Entities;

namespace Inventory.DTOs.Categories
{
    public class CategoryPutDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public CategoryPutDTO()
        {
            
        }

        public CategoryPutDTO(Category category)
        {
            Name        = category.Name;
            Description = category.Description;
        }

        public Category ToEntity()
        {
            return new Category
            {
                Name        = Name,
                Description = Description
            };
        }
    }
}