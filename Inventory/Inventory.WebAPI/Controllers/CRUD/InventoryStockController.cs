using Inventory.DTOs.InventoryStocks;
using Inventory.Entities;
using Inventory.Services.Interfaces.CRUD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryStockController : BaseController<InventoryStock>
    {
        private readonly IInventoryStockService _inventoryStockService;

        public InventoryStockController(IInventoryStockService inventoryStockService)
        {
            _inventoryStockService = inventoryStockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InventoryStockListDTO> inventoryStockListDTOs = new List<InventoryStockListDTO>();

            var items = await _inventoryStockService.GetAllAsync();

            if (items.Count == 0)
                _result = NotFound("Resultado de la búsqueda: No se encontraron items");
            else
            {
                foreach (var item in items)
                    inventoryStockListDTOs.Add(new InventoryStockListDTO(item));

                _result = Ok(inventoryStockListDTOs);
            }

            return _result;
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetById(int productId)
        {
            var item = await _inventoryStockService.GetByIdAsync(productId);

            if (item is null)
                _result = NotFound("Resultado de la búsqueda: Item no encontrado");
            else
                _result = Ok(new InventoryStockListDTO(item));

            return _result;
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update(int productId, InventoryStockPutDTO productPutDTO)
        {
            var item = await _inventoryStockService.UpdateAsync(productId, productPutDTO.ToEntity());

            if (!item)
                _result = BadRequest($"Resultado de la actualización: Item no encontrado");
            else
                _result = Ok($"Resultado de la actualización: Item {productId} actualizado");

            return _result;
        }
    }
}