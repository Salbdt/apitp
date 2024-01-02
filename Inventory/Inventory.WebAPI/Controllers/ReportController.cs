using Inventory.DTOs.Products;
using Inventory.Services;
using Inventory.Services.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ReportController : ControllerBase
    {
        private ObjectResult? _result;
        private readonly ReportService _reportService;
        private readonly UserService _userService;

        public ReportController(ReportService reportService, UserService userService)
        {
            _reportService = reportService;
            _userService = userService;
        }

        [HttpGet("GetAllProductsBySeller/{userId}")]
        public async Task<IActionResult> GetAllProductsBySeller(int userId)
        {
            var user = await _userService.GetByIdAsync(userId);

            if (user is null)
                _result = NotFound("Resultado de la búsqueda: Item no encontrado");
            else
            {
                var items = await _reportService.GetAllProductsBySellerAsync(userId);

                if (items.Count == 0)
                    _result = NotFound("Resultado de la búsqueda: No se encontraron items");
                else
                    _result = Ok(new ProductBySellerListDTO(items, user));
            }

            return _result;
        }
    }
}