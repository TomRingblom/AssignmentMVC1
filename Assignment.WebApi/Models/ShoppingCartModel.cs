namespace Assignment.WebApi.Models;

public class ShoppingCartModel
{
    public ShoppingCartModel()
    {
        
    }

    public ShoppingCartModel(int id, int productId, int count, string userId, double price)
    {
        CartId = id;
        ProductId = productId;
        Count = count;
        UserId = userId;
        Price = price;
    }

    public int CartId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string UserId { get; set; }
    public double Price { get; set; }

}