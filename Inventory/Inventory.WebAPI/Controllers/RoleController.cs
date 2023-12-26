using Inventory.DTOs.Roles;
using Inventory.Entities;
using Inventory.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : BaseController<Role>
    {
        private readonly RoleService _roleService;

        public RoleController(RoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<RoleListDTO> roleListDTOs = new List<RoleListDTO>();

            var items = await _roleService.GetAllAsync();

            if (items.Count > 0)
            {
                foreach (var item in items)
                    roleListDTOs.Add(new RoleListDTO(item));

                _result = Ok(roleListDTOs);
            }          
            else
                _result = NotFound("Resultado de la búsqueda: No se encontraron elementos");

            return _result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _roleService.GetByIdAsync(id);

            if (item is not null)
                _result = Ok(new RoleListDTO(item));       
            else
                _result = NotFound("Resultado de la búsqueda: Elemento no encontrado");

            return _result;
        }
    }
}