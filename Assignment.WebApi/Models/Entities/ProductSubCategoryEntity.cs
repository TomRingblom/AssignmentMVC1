﻿using System.ComponentModel.DataAnnotations;

namespace Assignment.WebApi.Models.Entities;

public class ProductSubCategoryEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [Required]
    public int CategoryId { get; set; }
    public ProductCategoryEntity Category { get; set; }
    //public ICollection<ProductEntity> Products { get; set; }
}