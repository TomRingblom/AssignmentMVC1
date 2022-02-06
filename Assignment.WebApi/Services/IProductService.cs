using Assignment.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.WebApi.Services;

public interface IProductService
{
    Task<ActionResult<IEnumerable<ProductModel>>> GetProductsAsync();
}