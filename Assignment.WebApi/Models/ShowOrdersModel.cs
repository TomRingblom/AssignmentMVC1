﻿namespace Assignment.WebApi.Models
{
    public class ShowOrdersModel
    {
        public int OrderId { get; set; }
        public List<ProductModel> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }

    }
}
