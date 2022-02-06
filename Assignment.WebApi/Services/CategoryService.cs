using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Services;

public class CategoryService : ICategoryService
{
    private readonly ApplicationDbContext _context;

    public CategoryService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategoriesAsync()
    {
        var categories = new List<CategoryModel>();
        foreach (var item in await _context.ProductCategories.ToListAsync())
        {
            categories.Add(new CategoryModel(item.Id, item.Name));
        }

        return categories;
    }
}