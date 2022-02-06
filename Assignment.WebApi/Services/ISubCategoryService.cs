using Assignment.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebApi.Services;

public interface ISubCategoryService
{
    Task<ActionResult<IEnumerable<SubCategoryModel>>> GetSubCategoriesAsync();
}