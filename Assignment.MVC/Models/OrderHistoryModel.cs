namespace Assignment.MVC.Models
{
    public class OrderHistoryModel
    {
        public int OrderId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
