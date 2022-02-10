using Assignment.WebApi.Models.Entities;

namespace Assignment.WebApi.Models.ViewModels
{
    public class ShoppingCartVM
    {
        public List<ShoppingCartModel> ListCart { get; set; }
        public List<ProductModel> Product { get; set; }
    }
}
