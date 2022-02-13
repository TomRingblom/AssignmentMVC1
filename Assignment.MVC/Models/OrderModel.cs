namespace Assignment.MVC.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public List<OrderItemModel> OrderItems { get; set; }
    }
}
