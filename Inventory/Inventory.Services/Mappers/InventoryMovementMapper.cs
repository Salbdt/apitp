using Inventory.DTOs.InventoryMovements;
using Inventory.DTOs.Products;
using Inventory.DTOs.Users;
using Inventory.Entities;

namespace Inventory.Services.Mappers
{
    public class InventoryMovementMapper
    {
        // ListDTO
        public static InventoryMovementListDTO ToListDTO(InventoryMovement inventoryMovement)
        {
            ProductListDTO? productListDTO = null;
            if (inventoryMovement.Product is not null)
                productListDTO = ProductMapper.ToListDTO(inventoryMovement.Product);

            UserListDTO? userListDTO = null;            
            if (inventoryMovement.User is not null)
                userListDTO = UserMapper.ToListDTO(inventoryMovement.User);

            return new InventoryMovementListDTO
            {
                Id = inventoryMovement.Id,
                Product = productListDTO,
                User = userListDTO,
                Quantity = inventoryMovement.Quantity,
                MovementType = inventoryMovement.MovementType,
                MovementDate = inventoryMovement.MovementDate
            };
        }

        public static InventoryMovement ToEntity(InventoryMovementListDTO inventoryMovementListDTO)
        {
            Product? product = null;
            if (inventoryMovementListDTO.Product is not null)
                product = ProductMapper.ToEntity(inventoryMovementListDTO.Product);
            
            User? user = null;
            if (inventoryMovementListDTO.User is not null)
                user = UserMapper.ToEntity(inventoryMovementListDTO.User);

            return new InventoryMovement
            {
                Id = inventoryMovementListDTO.Id,
                Product = product,
                User = user,
                Quantity = inventoryMovementListDTO.Quantity,
                MovementType = inventoryMovementListDTO.MovementType,
                MovementDate = inventoryMovementListDTO.MovementDate
            };
        }

        // PutDTO
        public static InventoryMovementPutDTO ToPutDTO(InventoryMovement inventoryMovement)
        {
            int productId = 0;
            if (inventoryMovement.Product is not null)
                productId = inventoryMovement.Product.Id;

            int userId = 0;
            if (inventoryMovement.User is not null)
                userId = inventoryMovement.User.Id;

            return new InventoryMovementPutDTO
            {
                ProductId = productId,
                UserId = userId,
                Quantity = inventoryMovement.Quantity,
                MovementType = inventoryMovement.MovementType,
            };
        }

        public static InventoryMovement ToEntity(InventoryMovementPutDTO inventoryMovementPutDTO)
        {
            return new InventoryMovement
            {
                Product = new Product
                {
                    Id = inventoryMovementPutDTO.ProductId
                },
                User = new User
                {
                    Id = inventoryMovementPutDTO.UserId
                },
                Quantity = inventoryMovementPutDTO.Quantity,
                MovementType = inventoryMovementPutDTO.MovementType,
            };
        }
    }
}