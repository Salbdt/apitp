using Inventory.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    public class BaseController<T> : ControllerBase where T: class
    {
        protected readonly IBaseService<T> _service;
        protected ObjectResult? _result;

        public BaseController(IBaseService<T> service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var items = await _service.GetAllAsync();

            if (items.Count == 0)
                _result = NotFound("Resultado de la búsqueda: No se encontraron elementos");
            else
                _result = Ok(items);

            return _result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            
            if (item is null)
                _result = NotFound("Resultado de la búsqueda: Elemento no encontrado");
            else
                _result = Ok(item);

            return _result;
        }
    }
}