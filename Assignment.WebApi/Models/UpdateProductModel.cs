﻿namespace Assignment.WebApi.Models;

public class UpdateProductModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public int SubCategoryId { get; set; }
}