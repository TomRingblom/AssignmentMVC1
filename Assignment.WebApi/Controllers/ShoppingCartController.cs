﻿using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ShoppingCartController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ShoppingCartModel>>> GetShoppingCarts()
        {
            var categories = new List<ShoppingCartModel>();
            foreach (var item in await _context.ShoppingCarts.ToListAsync())
            {
                categories.Add(new ShoppingCartModel(item.ProductId, item.Count, item.UserId, item.Price));
            }

            return categories;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ShoppingCartModel>>> GetShoppingCart(string id, int? productId)
        {
            
            if (productId == null)
            {
                var shoppinCart = new List<ShoppingCartModel>();
                foreach (var item in await _context.ShoppingCarts.Where(x => x.UserId == id).ToListAsync())
                {
                    shoppinCart.Add(new ShoppingCartModel(item.ProductId, item.Count, item.UserId, item.Price));
                }
                if (shoppinCart == null)
                    return NoContent();

                return Ok(shoppinCart);
            }
            else
            {
                var shoppinCart = new ShoppingCartEntity();
                shoppinCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == id && x.ProductId == productId);
                if (shoppinCart == null)
                    return NoContent();

                return Ok(shoppinCart);
            }

            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingCart(int id, UpdateShoppingCartModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
                return BadRequest();

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
                return NotFound();

            shoppingCart.Count = model.Count;

            _context.Entry(shoppingCart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<ShoppingCartEntity>> CreateShoppingCart(CreateShoppingCartModel model)
        {
            if (ModelState.IsValid)
            {
                var createShoppingCart = new ShoppingCartEntity(model.ProductId, model.Count, model.UserId, model.Price);
                _context.ShoppingCarts.Add(createShoppingCart);
                await _context.SaveChangesAsync();

                var addedShoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.Id == createShoppingCart.Id);

                return CreatedAtAction("GetShoppingCart", new { id = createShoppingCart.Id }, new ShoppingCartModel(addedShoppingCart.Id, addedShoppingCart.ProductId, addedShoppingCart.Count, addedShoppingCart.UserId, addedShoppingCart.Price));
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(int id)
        {
            var findShoppingCartToDelete = await _context.ShoppingCarts.FindAsync(id);
            if (findShoppingCartToDelete == null)
                return NotFound();

            _context.ShoppingCarts.Remove(findShoppingCartToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
