using Inventory.DTOs.Users;
using Inventory.Entities;
using Inventory.Services.Interfaces;
using Inventory.Services.Interfaces.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController<User>
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public UserController(IUserService userService, ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Post(UserPutDTO userPutDTO)
        {
            var exists = await _userService.ExistsAsync(userPutDTO.Email);

            if (exists)
                _result = BadRequest("Resultado de registro: Email ya registrado");
            else
            {
                var item = await _userService.CreateAsync(userPutDTO.ToEntity());

                if (item is null)
                    _result = BadRequest("Resultado de registro: Item no registrado");       
                else
                    _result = Ok(new UserListDTO(item));
            }

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

        [HttpPost("Login/{email};{password}")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var item = await _userService.LoginAsync(email, password);

            if (item is null)
                _result = Unauthorized("Resultado de ingreso: No autorizado");
            else
            {
                var token = _tokenService.CreateToken(item);

                _result = Ok(new UserLoginDTO(item, token));

            }

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
                _result = BadRequest("Resultado de la eliminación: Item no eliminado");
            else
                _result = Ok($"Resultado de la eliminación: Item {id} eliminado");

            return _result;
        }
    }
}