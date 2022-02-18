using Assignment.WebApi.Filters;
using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Controllers
{
    [ApiKeyAuth]
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
            return Ok(await _context.ShoppingCarts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<ShoppingCartModel>>> GetShoppingCartDetails(string id, int? productId)
        {
            
            if (productId == null)
            {
                var shoppingCart = new List<ShoppingCartDetailsModel>();

                var products = await _context.ShoppingCarts
                    .Include(x => x.ProductEntity)
                    .Where(x => x.UserId == id).ToListAsync();


                foreach (var item in products)
                {
                    shoppingCart.Add(new ShoppingCartDetailsModel(item.Id, item.ProductEntity.Id, item.ProductEntity.Name, item.ProductEntity.Price, item.Count));
                }
                if (shoppingCart == null)
                    return NoContent();

                return Ok(shoppingCart);
            }
            else
            {
                var shoppingCart = new ShoppingCartEntity();
                shoppingCart = await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == id && x.ProductId == productId);
                if (shoppingCart == null)
                    return NoContent();

                return Ok(shoppingCart);
            }
        }
        [HttpGet("Summary")]
        public async Task<ActionResult<IEnumerable<ShoppingCartModel>>> GetShoppingCartSummary(string id)
        {
            var shoppingCart = new List<ShoppingCartModel>();

            var products = await _context.ShoppingCarts
                .Include(x => x.ProductEntity)
                .Where(x => x.UserId == id).ToListAsync();


            foreach (var item in products)
            {
                shoppingCart.Add(new ShoppingCartModel(item.Id, item.ProductEntity.Id, item.Count, item.UserId, item.ProductEntity.Price));
            }

            return Ok(shoppingCart);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateShoppingCart(int id, UpdateShoppingCartModel model)
        {
            if (!ModelState.IsValid || id != model.Id)
                return BadRequest();

            var shoppingCart = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCart == null)
                return NotFound();

            shoppingCart.ProductId = model.ProductId;
            shoppingCart.Count = model.Count;
            shoppingCart.UserId = model.UserId;
            shoppingCart.Price = model.Price;
            
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

                return Ok(createShoppingCart);
            }

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShoppingCart(int id)
        {
            var shoppingCartToDelete = await _context.ShoppingCarts.FindAsync(id);
            if (shoppingCartToDelete == null)
                return NotFound();

            _context.ShoppingCarts.Remove(shoppingCartToDelete);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
