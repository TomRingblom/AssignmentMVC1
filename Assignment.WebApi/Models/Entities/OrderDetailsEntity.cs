using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Assignment.WebApi.Models.Entities;

public class OrderDetailsEntity
{
    [Key] 
    public int Id { get; set; }
    public int OrderId { get; set; }
    [ForeignKey("OrderId")]
    public OrderEntity Order { get; set; }
    [Required]
    public int ProductId { get; set; }
    [ForeignKey("ProductId")]
    [ValidateNever]
    public ProductEntity Product { get; set; }
    [Required]
    public double Price { get; set; }
    [Required]
    public int Quantity { get; set; }
}