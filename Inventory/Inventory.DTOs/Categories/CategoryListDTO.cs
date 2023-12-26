using Inventory.Entities;

namespace Inventory.DTOs.Categories
{
    public class CategoryListDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public CategoryListDTO(Category category)
        {
            Id          = category.Id;
            Name        = category.Name;
            Description = category.Description;
        }
        
        public Category ToEntity()
        {
            return new Category
            {
                Id          = Id,
                Name        = Name,
                Description = Description
            };
        }
    }
}