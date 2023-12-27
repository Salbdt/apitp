using Inventory.DTOs.Products;
using Inventory.Entities;
using Inventory.Services;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController<Product>
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(ProductPutDTO productPutDTO)
        {
            var item = await _productService.CreateAsync(productPutDTO.ToEntity());

            if (item is null)
                _result = BadRequest("Resultado de la inserción: Item no creado");       
            else
                _result = Ok(new ProductListDTO(item));
                

            return _result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<ProductListDTO> productListDTOs = new List<ProductListDTO>();

            var items = await _productService.GetAllAsync();

            if (items.Count == 0)
                _result = NotFound("Resultado de la búsqueda: No se encontraron items");
            else
            {
                foreach (var item in items)
                    productListDTOs.Add(new ProductListDTO(item));

                _result = Ok(productListDTOs);
            }

            return _result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _productService.GetByIdAsync(id);

            if (item is null)
                _result = NotFound("Resultado de la búsqueda: Item no encontrado");
            else
                _result = Ok(new ProductListDTO(item));

            return _result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductPutDTO productPutDTO)
        {
            var item = await _productService.UpdateAsync(id, productPutDTO.ToEntity());

            if (!item)
                _result = BadRequest($"Resultado de la actualización: Item no encontrado");
            else
                _result = Ok($"Resultado de la actualización: Item {id} actualizado");

            return _result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _productService.DeleteAsync(id);

            if (!item)
                _result = BadRequest("Resultado de la eliminación: Item no eliminado");
            else
                _result = Ok($"Resultado de la eliminación: Item {id} eliminado");

            return _result;
        }
    }
}