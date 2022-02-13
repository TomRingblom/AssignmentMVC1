using Assignment.WebApi.Models.Entities;

namespace Assignment.WebApi.Models
{
    public class OrderItemModel
    {
        public OrderItemModel()
        {
            
        }

        public OrderItemModel(ProductEntity products, int quantity)
        {
            Products = products;
            Quantity = quantity;
        }
        public ProductEntity Products { get; set; }
        public int Quantity { get; set; }
    }
}
