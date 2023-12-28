using Inventory.DTOs.InventoryMovements;
using Inventory.Entities;
using Inventory.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryMovementController : BaseController<InventoryMovement>
    {
        private readonly InventoryMovementService _inventoryMovementService;

        public InventoryMovementController(InventoryMovementService inventoryMovementService)
        {
            _inventoryMovementService = inventoryMovementService;
        }

        [HttpPost("CreatePurchase")]
        public async Task<IActionResult> Post(InventoryMovementPutDTO productPutDTO)
        {
            var item = await _inventoryMovementService.CreateAsync(productPutDTO.ToEntity());

            if (item is null)
                _result = BadRequest("Resultado de la inserción: Item no creado");       
            else
                _result = Ok(new InventoryMovementListDTO(item));
                

            return _result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<InventoryMovementListDTO> productListDTOs = new List<InventoryMovementListDTO>();

            var items = await _inventoryMovementService.GetAllAsync();

            if (items.Count == 0)
                _result = NotFound("Resultado de la búsqueda: No se encontraron items");
            else
            {
                foreach (var item in items)
                    productListDTOs.Add(new InventoryMovementListDTO(item));

                _result = Ok(productListDTOs);
            }

            return _result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _inventoryMovementService.GetByIdAsync(id);

            if (item is null)
                _result = NotFound("Resultado de la búsqueda: Item no encontrado");
            else
                _result = Ok(new InventoryMovementListDTO(item));

            return _result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, InventoryMovementPutDTO productPutDTO)
        {
            var item = await _inventoryMovementService.UpdateAsync(id, productPutDTO.ToEntity());

            if (!item)
                _result = BadRequest($"Resultado de la actualización: Item no encontrado");
            else
                _result = Ok($"Resultado de la actualización: Item {id} actualizado");

            return _result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _inventoryMovementService.DeleteAsync(id);

            if (!item)
                _result = BadRequest("Resultado de la eliminación: Item no eliminado");
            else
                _result = Ok($"Resultado de la eliminación: Item {id} eliminado");

            return _result;
        }        
    }
}