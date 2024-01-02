using Inventory.DTOs.Categories;
using Inventory.Entities;
using Inventory.Services.Interfaces.CRUD;
using Microsoft.AspNetCore.Mvc;

namespace Inventory.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController<Category>
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> Post(CategoryPutDTO categoryPutDTO)
        {
            var item = await _categoryService.CreateAsync(categoryPutDTO.ToEntity());

            if (item is null)
                _result = BadRequest("Resultado de la inserción: Item no creado");       
            else
                _result = Ok(new CategoryListDTO(item));
                

            return _result;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            List<CategoryListDTO> categoryListDTOs = new List<CategoryListDTO>();

            var items = await _categoryService.GetAllAsync();

            if (items.Count == 0)
                _result = NotFound("Resultado de la búsqueda: No se encontraron items");
            else
            {
                foreach (var item in items)
                    categoryListDTOs.Add(new CategoryListDTO(item));

                _result = Ok(categoryListDTOs);
            }

            return _result;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _categoryService.GetByIdAsync(id);

            if (item is null)
                _result = NotFound("Resultado de la búsqueda: Item no encontrado");
            else
                _result = Ok(new CategoryListDTO(item));

            return _result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryPutDTO categoryPutDTO)
        {
            var item = await _categoryService.UpdateAsync(id, categoryPutDTO.ToEntity());

            if (!item)
                _result = BadRequest($"Resultado de la actualización: Item no encontrado");
            else
                _result = Ok($"Resultado de la actualización: Item {id} actualizado");

            return _result;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var item = await _categoryService.DeleteAsync(id);

            if (!item)
                _result = BadRequest("Resultado de la eliminación: Item no eliminado");
            else
                _result = Ok($"Resultado de la eliminación: Item {id} eliminado");

            return _result;
        }
    }
}