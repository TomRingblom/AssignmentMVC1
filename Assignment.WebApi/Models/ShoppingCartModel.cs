namespace Assignment.WebApi.Models;

public class ShoppingCartModel
{
    public ShoppingCartModel()
    {
        
    }

    public ShoppingCartModel(int productId, int count, string userId, double price)
    {
        ProductId = productId;
        Count = count;
        UserId = userId;
        Price = price;
    }

    public ShoppingCartModel(int id, int productId, int count, string userId, double price)
    {
        Id = id;
        ProductId = productId;
        Count = count;
        UserId = userId;
        Price = price;
    }
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string UserId { get; set; }
    public double Price { get; set; }

}