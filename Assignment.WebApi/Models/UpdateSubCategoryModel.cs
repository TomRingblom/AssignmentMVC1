namespace Assignment.WebApi.Models;

public class UpdateSubCategoryModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
}