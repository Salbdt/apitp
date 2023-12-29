using Inventory.DTOs.Users;
using Inventory.Entities;

namespace Inventory.DTOs.Products
{
    public class ProductBySellerListDTO
    {
        public List<ProductListDTO> Products { get; set; }
        public UserListDTO? User { get; set; }

        public ProductBySellerListDTO()
        {
            Products = new List<ProductListDTO>();
        }

        public ProductBySellerListDTO(List<Product> products, User user)
        {
            Products = new List<ProductListDTO>();

            foreach (Product product in products)
                Products.Add(new ProductListDTO(product));
            
            User = (user is not null) ? new UserListDTO(user) : null;
        }

        public User? GetUser()
        {
            return new User
            {
                Id          = User.Id,
                Name        = User.Name
            };
        }

        public List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();

            foreach (ProductListDTO productListDTO in Products)
            {
                products.Add(new Product
                {
                    Id          = productListDTO.Id,
                    Category    = productListDTO.Category?.ToEntity(),
                    Name        = productListDTO.Name,
                    Description = productListDTO.Description,
                    Price       = productListDTO.Price
                }
                );
            }
            
            return products;
        }
    }
}