namespace Assignment.WebApi.Models;

public class CreateOrderModel
{
    public string CustomerId { get; set; }
    public IEnumerable<ShoppingCartModel> ShoppingCarts { get; set; }
}