namespace Assignment.WebApi.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public List<OrderItemModel> Items { get; set; } 
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
