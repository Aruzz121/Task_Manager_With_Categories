using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.DTOs;

namespace TaskManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }

        // GET /api/categories
        [HttpGet]
        public async Task<ActionResult<List<CategoryDTO>>> GetCategories()
        {
            var categories = await _context.Categories
                .Select(c => new CategoryDTO { Id = c.Id, Name = c.Name })
                .ToListAsync();

            return Ok(categories);
        }

        // POST /api/categories
        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CreateCategoryDTO dto)
        {
            var name = dto.Name?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name))
                return BadRequest(new { message = "El nombre de la categoría no puede estar vacío." });

            var exists = await _context.Categories.AnyAsync(c => c.Name.ToLower() == name.ToLower());
            if (exists)
                return BadRequest(new { message = $"Ya existe una categoría llamada '{name}'." });

            var category = new TaskManager.Models.Category { Name = name };
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            var result = new CategoryDTO { Id = category.Id, Name = category.Name };
            return CreatedAtAction(nameof(GetCategories), new { id = result.Id }, result);
        }
    }
}
