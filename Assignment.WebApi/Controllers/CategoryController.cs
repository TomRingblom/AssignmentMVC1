using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            var categories = new List<CategoryModel>();
            foreach (var item in await _context.Categories.ToListAsync())
            {
                categories.Add(new CategoryModel(item.Id, item.Name));
            }

            return categories;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategory(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);

            if (category == null)
                return NotFound();

            return Ok(new CategoryModel(category.Id, category.Name));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, UpdateCategoryModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
                return BadRequest();

            var findCategory = await _context.Categories.FindAsync(id);
            if (findCategory == null)
                return NotFound();

            findCategory.Name = model.Name;

            _context.Entry(findCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryEntity>> CreateCategory(CreateCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var subCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
                if (subCategory != null)
                    return new BadRequestObjectResult(new ErrorMessage { StatusCode = 400, Error = "Category name already exist." });

                var createCategory = new CategoryEntity(model.Name);
                _context.Categories.Add(createCategory);
                await _context.SaveChangesAsync();

                var addedCategory = await _context.Categories.FirstOrDefaultAsync(x => x.Id == createCategory.Id);

                return CreatedAtAction("GetCategory", new { id = createCategory.Id }, new CategoryModel(addedCategory.Id, addedCategory.Name));
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var findCategoryToDelete = await _context.Categories.FindAsync(id);
            if (findCategoryToDelete == null)
                return NotFound();

            _context.Categories.Remove(findCategoryToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
