using Assignment.WebApi.Filters;
using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public SubCategoryController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryModel>>> GetSubCategories()
        {
            var categories = new List<SubCategoryModel>();
            foreach (var item in await _context.SubCategories.ToListAsync())
            {
                categories.Add(new SubCategoryModel(item.Id, item.Name, item.CategoryId));
            }

            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetSubCategory(int id)
        {
            var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == id);

            if (subCategory == null)
                return NotFound();

            return Ok(new SubCategoryModel(subCategory.Id, subCategory.Name, subCategory.CategoryId));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubCategory(int id, UpdateSubCategoryModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
                return BadRequest();

            var findSubCategory = await _context.SubCategories.FindAsync(id);
            if (findSubCategory == null)
                return NotFound();

            findSubCategory.Name = model.Name;
            findSubCategory.CategoryId = model.CategoryId;

            _context.Entry(findSubCategory).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<SubCategoryEntity>> CreateSubCategory(CreateSubCategoryModel model)
        {
            if (ModelState.IsValid)
            {
                var subCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Name == model.Name);
                if (subCategory != null)
                    return new BadRequestObjectResult(new ErrorMessage { StatusCode = 400, Error = "SubCategory name already exist." });

                var createSubCategory = new SubCategoryEntity(model.Name, model.CategoryId);
                _context.SubCategories.Add(createSubCategory);
                await _context.SaveChangesAsync();

                var addedSubCategory = await _context.SubCategories.FirstOrDefaultAsync(x => x.Id == createSubCategory.Id);

                return CreatedAtAction("GetSubCategory", new { id = createSubCategory.Id }, new SubCategoryModel(addedSubCategory.Id, addedSubCategory.Name, addedSubCategory.CategoryId));
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubCategory(int id)
        {
            var findSubCategoryToDelete = await _context.SubCategories.FindAsync(id);
            if (findSubCategoryToDelete == null)
                return NotFound();

            _context.SubCategories.Remove(findSubCategoryToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
