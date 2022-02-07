using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities;

public class ProductEntity
{
    public ProductEntity()
    {
        
    }

    public ProductEntity(string name, string description, double price, int subCategoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        SubCategoryId = subCategoryId;
    }
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    [StringLength(250)]
    public string Description { get; set; }
    [Required]
    [Column(TypeName = "money")]
    public double Price { get; set; }
    [Required]
    public int SubCategoryId { get; set; }
    public SubCategoryEntity SubCategory { get; set; }
}