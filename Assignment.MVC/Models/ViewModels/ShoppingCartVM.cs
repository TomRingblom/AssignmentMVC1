﻿namespace Assignment.MVC.Models.ViewModels;

public class ShoppingCartVM
{
    public IEnumerable<ShoppingCartModel> ListCart { get; set; }
    public IEnumerable<ProductModel> Product { get; set; }
    public double CartTotal { get; set; }
    public string UserId { get; set; }
}