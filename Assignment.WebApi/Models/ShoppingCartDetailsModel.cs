namespace Assignment.WebApi.Models;

public class ShoppingCartDetailsModel
{
    public ShoppingCartDetailsModel()
    {
        
    }

    public ShoppingCartDetailsModel(int cartId, int productId, string name, double price, int count)
    {
        CartId = cartId;
        ProductId = productId;
        Name = name;
        Price = price;
        Count = count;
    }
    public int CartId { get; set; }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
}