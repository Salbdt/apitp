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
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(UserPutDTO userPutDTO)
        {
            var item = await _userService.CreateAsync(userPutDTO.ToEntity());

            if (item is not null)
            {
                _result = Ok(new UserListDTO(item));
            }          
            else
                _result = BadRequest("Resultado: Elemento no encontrado");

            return _result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UserListDTO> userListDTOs = new List<UserListDTO>();

            var items = await _userService.GetAllAsync();

            if (items.Count > 0)
            {
                foreach (var item in items)
                    userListDTOs.Add(new UserListDTO(item));

                _result = Ok(userListDTOs);
            }          
            else
                _result = NotFound("Resultado de la búsqueda: No se encontraron elementos");

            return _result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _userService.GetByIdAsync(id);

            if (item is not null)
                _result = Ok(new UserListDTO(item));       
            else
                _result = NotFound("Resultado de la búsqueda: Elemento no encontrado");

            return _result;
        }
    }
}