using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities;

public class OrderEntity
{
    public OrderEntity()
    {
        
    }

    public OrderEntity(string customerId, DateTime orderDate)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    public string CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
}