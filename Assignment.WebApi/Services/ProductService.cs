using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Services;

public class ProductService : IProductService
{
    private readonly ApplicationDbContext _context;

    public ProductService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<ActionResult<IEnumerable<ProductModel>>> GetProductsAsync()
    {
        var products = new List<ProductModel>();
        foreach (var item in await _context.Products.ToListAsync())
        {
            products.Add(new ProductModel(item.Id, item.Name, item.Description, item.Price));
        }

        return products;
    }
}