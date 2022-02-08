namespace Assignment.WebApi.Models.Entities
{
    public class ShoppingCartEntity
    {
        public ShoppingCartEntity()
        {
            
        }

        public ShoppingCartEntity(int productId, int count, string userId)
        {
            ProductId = productId;
            Count = count;
            UserId = userId;
        }
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string UserId { get; set; }
    }
}
