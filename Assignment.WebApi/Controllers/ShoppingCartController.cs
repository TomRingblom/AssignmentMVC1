using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Assignment.WebApi.Models.ViewModels;
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
                var shoppingCart = new List<ShoppingCartDetailsModel>();
                var products = from product in _context.Products
                    join shopp in _context.ShoppingCarts on product.Id equals shopp.ProductId
                    where shopp.UserId == id
                    select new {Product = product, Cart = shopp};


                foreach (var item in products)
                {
                    shoppingCart.Add(new ShoppingCartDetailsModel(item.Cart.Id, item.Product.Id, item.Product.Name, item.Product.Price, item.Cart.Count));
                }
                if (shoppingCart == null)
                    return NoContent();

                return Ok(shoppingCart);
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

        [HttpPut("AddOneToCart")]
        public async Task<IActionResult> AddOneToCart(int id)
        {
            var addOneToCart = await _context.ShoppingCarts.FindAsync(id);
            addOneToCart.Count += 1;
            _context.Entry(addOneToCart).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("SubOneToCart")]
        public async Task<IActionResult> SubOneToCart(int id)
        {
            var addOneToCart = await _context.ShoppingCarts.FindAsync(id);
            addOneToCart.Count -= 1;
            _context.Entry(addOneToCart).State = EntityState.Modified;
            if (addOneToCart.Count == 0)
            {
                _context.Remove(addOneToCart);
            }
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
