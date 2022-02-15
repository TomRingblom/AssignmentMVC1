using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities;

public class OrderEntity
{
    public OrderEntity()
    {
        
    }

    public OrderEntity(string customerId, DateTime orderDate, DateTime orderChangeDate)
    {
        CustomerId = customerId;
        OrderDate = orderDate;
        OrderChangeDate = orderChangeDate;
    }

    [Key]
    public int Id { get; set; }
    [Required]
    public string CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public DateTime OrderChangeDate { get; set; }

}