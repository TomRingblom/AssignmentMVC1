namespace Assignment.WebApi.Models;

public class SubCategoryModel
{
    public SubCategoryModel()
    {
        
    }
    public SubCategoryModel(int id, string name, int categoryId)
    {
        Id = id;
        Name = name;
        CategoryId = categoryId;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
}