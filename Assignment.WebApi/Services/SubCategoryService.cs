using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly ApplicationDbContext _context;

    public SubCategoryService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ActionResult<IEnumerable<SubCategoryModel>>> GetSubCategoriesAsync()
    {
        var categories = new List<SubCategoryModel>();
        foreach (var item in await _context.ProductSubCategories.ToListAsync())
        {
            categories.Add(new SubCategoryModel(item.Id, item.Name));
        }

        return categories;
    }
}