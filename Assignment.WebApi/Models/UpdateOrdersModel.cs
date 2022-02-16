namespace Assignment.WebApi.Models;

public class UpdateOrdersModel
{
    public int OrderId { get; set; }
    public int OrderDetailsToUpdate { get; set; }
    public int ProductId { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; }
}