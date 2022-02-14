namespace Assignment.MVC.Models
{
    public class OrderModel
    {
        public int OrderId { get; set; }
        public List<ProductModel> Items { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
