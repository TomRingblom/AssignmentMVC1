namespace Assignment.WebApi.Models;

public class ProductOrderModel
{
    public ProductOrderModel()
    {
        
    }

    public ProductOrderModel(int id, string name, string description, double price, int quantity)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Quantity = quantity;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}