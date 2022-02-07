﻿using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var products = new List<ProductModel>();
            foreach (var item in await _context.Products.Include(x => x.SubCategory).ThenInclude(c => c.Category).ToArrayAsync())
            {
                products.Add(new ProductModel(item.Id, item.Name, item.Description, item.Price, item.SubCategory.Category.Name, item.SubCategory.Name));
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProduct(int id)
        {
            var findProduct = await _context.Products.Include(x => x.SubCategory)
                .ThenInclude(c => c.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (findProduct == null)
                return NotFound();

            return Ok(new ProductModel(findProduct.Id, findProduct.Name, findProduct.Description, findProduct.Price, findProduct.SubCategory.Category.Name, findProduct.SubCategory.Name));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductModel product)
        {
            if (!ModelState.IsValid  || id != product.Id)
                return BadRequest();

            var findProduct = await _context.Products.FindAsync(id);
            if (findProduct == null)
                return NotFound();

            findProduct.Name = product.Name;
            findProduct.Description = product.Description;
            findProduct.Price = product.Price;
            findProduct.SubCategoryId = product.SubCategoryId;

            _context.Entry(findProduct).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<ProductEntity>> CreateProduct(CreateProductModel product)
        {
            if (ModelState.IsValid)
            {
                var subCategory = await _context.ProductSubCategories.FindAsync(product.SubCategoryId);
                if (subCategory == null)
                    return new BadRequestObjectResult(new ErrorMessage { StatusCode = 400, Error = "Invalid or no subcategory id provided." });

                var createProduct = new ProductEntity(product.Name, product.Description, product.Price, product.SubCategoryId);
                _context.Products.Add(createProduct);
                await _context.SaveChangesAsync();

                var addedProduct = await _context.Products.Include(x => x.SubCategory.Category).FirstOrDefaultAsync(x => x.Id == createProduct.Id);

                return CreatedAtAction("GetProduct", new { id = createProduct.Id }, new ProductModel(addedProduct.Id, addedProduct.Name, addedProduct.Description, addedProduct.Price, addedProduct.SubCategory.Category.Name, addedProduct.SubCategory.Name));
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var findProductToDelete = await _context.Products.FindAsync(id);
            if (findProductToDelete == null)
                return NotFound();

            _context.Products.Remove(findProductToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
