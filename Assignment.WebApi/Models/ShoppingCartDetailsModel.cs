namespace Assignment.WebApi.Models;

public class ShoppingCartDetailsModel
{
    public ShoppingCartDetailsModel()
    {
        
    }

    public ShoppingCartDetailsModel(int productId, string name, double price, int count)
    {
        ProductId = productId;
        Name = name;
        Price = price;
        Count = count;
    }
    public int ProductId { get; set; }
    public string Name { get; set; }
    public double Price { get; set; }
    public int Count { get; set; }
}