namespace Assignment.MVC.Models;

public class ShoppingCartModel
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string UserId { get; set; }
    public double Price { get; set; }
}