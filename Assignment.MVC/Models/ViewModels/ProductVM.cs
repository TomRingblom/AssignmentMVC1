namespace Assignment.MVC.Models.ViewModels;

public class ProductVM
{
    public IEnumerable<ProductModel> Products { get; set; }
    public ProductModel Product { get; set; }
}