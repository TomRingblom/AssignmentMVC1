namespace Assignment.MVC.Models.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCartDetailsModel> ListCart { get; set; }
    public IEnumerable<ProductModel> Product { get; set; }
    public double CartTotal { get; set; }
    public int CountTotal { get; set; }
    public string UserId { get; set; }
}