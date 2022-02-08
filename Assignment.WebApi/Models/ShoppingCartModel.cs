namespace Assignment.WebApi.Models;

public class ShoppingCartModel
{
    public ShoppingCartModel()
    {
        
    }

    public ShoppingCartModel(int id, int productId, int count, string userId)
    {
        Id = id;
        ProductId = productId;
        Count = count;
        UserId = userId;
    }
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public string UserId { get; set; }
}