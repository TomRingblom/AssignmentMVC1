using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Models.Entities;

[Index(nameof(Name), IsUnique = true)]
public class CategoryEntity
{
    public CategoryEntity()
    {
        
    }

    public CategoryEntity(string name)
    {
        Name = name;
    }
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(50)]
    public string Name { get; set; }
    //public ICollection<ProductSubCategoryEntity> SubCategories { get; set; }
}