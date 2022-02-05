using Assignment.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Assignment.WebApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {}
    
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<ProductSubCategoryEntity> ProductSubCategories { get; set; }
}