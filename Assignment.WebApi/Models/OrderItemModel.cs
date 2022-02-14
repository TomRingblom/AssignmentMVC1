using Assignment.WebApi.Models.Entities;

namespace Assignment.WebApi.Models
{
    public class OrderItemModel
    {
        public OrderItemModel()
        {
            
        }

        public OrderItemModel(ProductEntity products)
        {
            Products = products;
        }
        public ProductEntity Products { get; set; }
    }
}
