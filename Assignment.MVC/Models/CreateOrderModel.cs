using Assignment.MVC.Models;

namespace Assignment.MVC.Models;

public class CreateOrderModel
{
    public string CustomerId { get; set; }
    public IEnumerable<ShoppingCartModel> ShoppingCarts { get; set; }
}