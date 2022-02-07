namespace Assignment.WebApi.Models;

public class ProductModel
{
    public ProductModel()
    {
        
    }

    public ProductModel(int id, string name, string description, double price, string categoryName, string subCategoryName)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CategoryName = categoryName;
        SubCategoryName = subCategoryName;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public string CategoryName { get; set; }
    public string SubCategoryName { get; set; }
}