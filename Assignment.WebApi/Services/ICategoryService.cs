using Assignment.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebApi.Services;

public interface ICategoryService
{
    Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesAsync();
}