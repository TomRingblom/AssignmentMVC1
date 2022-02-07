namespace Assignment.WebApi.Models;

public class UpdateProductModel
{
    public UpdateProductModel()
    {
        
    }

    public UpdateProductModel(int id, string name, string description, double price, int subCategoryId)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        SubCategoryId = subCategoryId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int SubCategoryId { get; set; }
}