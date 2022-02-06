using Assignment.WebApi.Models;
using Assignment.WebApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryService _subCategoryService;

        public SubCategoryController(ISubCategoryService subCategoryService)
        {
            _subCategoryService = subCategoryService;
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<SubCategoryModel>>> GetSubCategories()
        {
            return await _subCategoryService.GetSubCategoriesAsync();
        }
    }
}
