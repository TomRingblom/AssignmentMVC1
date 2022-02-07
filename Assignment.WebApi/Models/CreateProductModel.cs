namespace Assignment.WebApi.Models;

public class CreateProductModel
{
    public CreateProductModel()
    {
        
    }

    public CreateProductModel(string name, string description, double price, int subCategoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        SubCategoryId = subCategoryId;
    }

    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int SubCategoryId { get; set; }
}