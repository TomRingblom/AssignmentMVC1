using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignment.WebApi.Models.Entities;

public class ProductEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    public string Description { get; set; }
    [Required]
    [Column(TypeName = "money")]
    public double Price { get; set; }
    [Required]
    public int SubCategoryId { get; set; }
    public ICollection<ProductSubCategoryEntity> SubCategories { get; set; }
}