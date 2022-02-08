namespace Assignment.WebApi.Models;

public class CreateShoppingCartModel
{
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string UserId { get; set; }
}