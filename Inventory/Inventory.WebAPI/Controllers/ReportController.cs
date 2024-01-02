using Inventory.DTOs.Products;
using Inventory.Services.Interfaces;
using Inventory.Services.Interfaces.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api")]
    public class ReportController : ControllerBase
    {
        private ObjectResult? _result;
        private readonly IReportService _reportService;
        private readonly IUserService _userService;

        public ReportController(IReportService reportService, IUserService userService)
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