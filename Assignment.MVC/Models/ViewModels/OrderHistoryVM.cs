namespace Assignment.MVC.Models.ViewModels
{
    public class OrderHistoryVM
    {
        public IEnumerable<OrderHistoryModel> ListCart { get; set; }
        public double CartTotal { get; set; }
        public string UserId { get; set; }
    }
}
