using Inventory.DTOs.Users;
using Inventory.Entities;
using Inventory.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        public UserController(UserService service) : base(service)
        {            
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserPutDTO userCreateDTO)
        {
            /*
            // var categoryToCreate = new Category
            // {
            //     Name = categoryToCreateDTO.Name,
            //     Description = categoryToCreateDTO.Description,
            //     CreatedAt = DateTime.Now
            // };
            var categoryToCreate = _mapper.Map<Category>(categoryToCreateDTO);
            categoryToCreate.CreatedAt = DateTime.Now;

            var categoryCreated = await _categoryRepository.AddAsync(categoryToCreate);

            // var categoryCreatedDTO = new CategoryToListDTO
            // {
            //     Id = categoryCreated.Id,
            //     Name = categoryCreated.Name,
            //     Description = categoryCreated.Description,
            //     CreatedAt = categoryCreated.CreatedAt,
            //     UpdatedAt = categoryCreated.UpdatedAt
            // };
            var categoryCreatedDTO = _mapper.Map<CategoryToListDTO>(categoryCreated);

            return Ok(categoryCreatedDTO);
            */
            return null;
        }
    }
}