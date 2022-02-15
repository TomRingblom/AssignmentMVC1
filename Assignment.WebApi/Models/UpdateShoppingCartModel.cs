namespace Assignment.WebApi.Models;

public class UpdateShoppingCartModel
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string UserId { get; set; }
    public double Price { get; set; }
}