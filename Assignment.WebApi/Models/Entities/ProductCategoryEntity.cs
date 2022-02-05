using System.ComponentModel.DataAnnotations;

namespace Assignment.WebApi.Models.Entities;

public class ProductCategoryEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public ICollection<ProductSubCategoryEntity> SubCategories { get; set; }
}