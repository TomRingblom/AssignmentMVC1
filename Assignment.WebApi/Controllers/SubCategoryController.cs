using Assignment.WebApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Controllers
{
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
            foreach (var item in await _context.ProductSubCategories.ToListAsync())
            {
                categories.Add(new SubCategoryModel(item.Id, item.Name));
            }

            return Ok(categories);
        }
    }
}
