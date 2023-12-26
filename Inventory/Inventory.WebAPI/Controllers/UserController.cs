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

            if (item is null)
                _result = BadRequest("Resultado de la inserción: Item no creado");       
            else
                _result = Ok(new UserListDTO(item));
                

            return _result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<UserListDTO> userListDTOs = new List<UserListDTO>();

            var items = await _userService.GetAllAsync();

            if (items.Count == 0)
                _result = NotFound("Resultado de la búsqueda: No se encontraron items");
            else
            {
                foreach (var item in items)
                    userListDTOs.Add(new UserListDTO(item));

                _result = Ok(userListDTOs);
            }

            return _result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _userService.GetByIdAsync(id);

            if (item is null)
                _result = NotFound("Resultado de la búsqueda: Item no encontrado");
            else
                _result = Ok(new UserListDTO(item));

            return _result;
        }

        [HttpPut("{id};{email};{password}")]
        public async Task<IActionResult> Update(int id, string email, string password, UserPutDTO userPutDTO)
        {
            var item = await _userService.UpdateAsync(id, email, password, userPutDTO.ToEntity());

            if (!item)
                _result = BadRequest($"Resultado de la actualización: Item no encontrado");
            else
                _result = Ok($"Resultado de la actualización: Item {id} actualizado");

            return _result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _userService.DeleteAsync(id);

            if (!item)
                _result = BadRequest("Resultado de la eliminación: Item no encontrado");
            else
                _result = Ok($"Resultado de la eliminación: Item {id} eliminado");

            return _result;
        }
    }
}