namespace Assignment.WebApi.Models;

public class CreateOrderModel
{
    //public int OrderId { get; set; }
    //public int ProductId { get; set; }
    //public double Price { get; set; }
    //public int Quantity { get; set; }
    public string CustomerId { get; set; }
    public DateTime Date { get; set; }
    public IEnumerable<ShoppingCartModel> ShoppingCarts { get; set; }
}