namespace Assignment.MVC.Models;

public class ShoppingCartDetailsModel
{
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
}