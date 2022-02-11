using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities;

public class OrderEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
}