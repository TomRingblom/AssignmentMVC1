namespace Assignment.MVC.Models.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCartDetailsModel> ListCart { get; set; }
    public double CartTotal { get; set; }
    public string UserId { get; set; }
}