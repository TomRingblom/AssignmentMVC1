namespace Assignment.WebApi.Models;

public class ProductModel
{
    public ProductModel()
    {
        
    }

    public ProductModel(int id, string name, string description, double price)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
    }

    public ProductModel(int id, string name, string description, double price, int subCategoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        SubCategoryId = subCategoryId;
    }

    public ProductModel(int id, string name, string description, double price, int subCategoryId, string categoryName, string subCategoryName)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        SubCategoryId = subCategoryId;
        CategoryName = categoryName;
        SubCategoryName = subCategoryName;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int SubCategoryId { get; set; }
    public string CategoryName { get; set; }
    public string SubCategoryName { get; set; }
}