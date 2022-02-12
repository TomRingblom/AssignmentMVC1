using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetOrderDetails(string id)
        {
            if (id == null)
                return BadRequest();

            var getUserOrders = from product in _context.Products
                                 join orderD in _context.OrderDetails on product.Id equals orderD.ProductId
                                 join order in _context.Orders on orderD.OrderId equals order.Id
                                 where order.CustomerId == id
                                 select new { Product = product, Order = order, OrderDetails = orderD };


            var orderDetails = new List<ShowOrdersModel>();
            foreach (var order in getUserOrders)
            {
                orderDetails.Add(new ShowOrdersModel {
                    OrderId = order.Order.Id,
                    ProductName = order.Product.Name,
                    Price = order.OrderDetails.Price,
                    Quantity = order.OrderDetails.Quantity,
                    OrderDate = order.Order.OrderDate
                });
            }
            return Ok(orderDetails);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailsEntity>> CreateOrder(CreateOrderModel model)
        {
            if (ModelState.IsValid)
            {
                var createOrder = new OrderEntity(model.CustomerId, DateTime.Now);
                _context.Orders.Add(createOrder);
                await _context.SaveChangesAsync();


                var cartsToEntity = new List<ShoppingCartEntity>();
                foreach (var cart in model.ShoppingCarts)
                {
                    cartsToEntity.Add(new ShoppingCartEntity(cart.CartId, cart.ProductId, cart.Count, cart.UserId, cart.Price));
                    var createOrderDetails = new OrderDetailsEntity(createOrder.Id, cart.ProductId, cart.Price, cart.Count);
                    _context.OrderDetails.Add(createOrderDetails);
                    await _context.SaveChangesAsync();
                }

                _context.ShoppingCarts.RemoveRange(cartsToEntity);
                await _context.SaveChangesAsync();

                return Ok();
                //var addedOrder = await _context.Orders.FirstOrDefaultAsync(x => x.Id == createOrder.Id);
                //return CreatedAtAction("GetProduct", new { id = createProduct.Id }, new ProductModel(addedProduct.Id, addedProduct.Name, addedProduct.Description, addedProduct.Price, addedProduct.SubCategoryId, addedProduct.SubCategory.Category.Name, addedProduct.SubCategory.Name));
            }

            return BadRequest();
        }
    }
}
