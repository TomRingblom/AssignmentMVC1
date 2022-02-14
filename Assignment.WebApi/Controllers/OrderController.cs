﻿using Assignment.WebApi.Filters;
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

            var userOrders = new List<OrderModel>();
            var orders = await _context.Orders.Where(x => x.CustomerId == id).ToListAsync();

            foreach (var o in orders)
            {
                OrderModel order = new();
                order.Items = new List<ProductOrderModel>();
                order.OrderDate = o.OrderDate;
                order.OrderId = o.Id;

                var productsWithOrderDetails = await _context.OrderDetails
                    .Include(x => x.Product)
                    .Where(y => y.OrderId == order.OrderId)
                    .ToArrayAsync();

                foreach (var p in productsWithOrderDetails)
                {
                    order.TotalPrice += p.Product.Price * p.Quantity;
                    order.Items.Add(new ProductOrderModel(p.Product.Id, p.Product.Name, p.Product.Description, p.Product.Price, p.Quantity));
                }
                userOrders.Add(order);
            }
            return Ok(userOrders);
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
