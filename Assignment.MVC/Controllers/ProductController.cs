using Assignment.MVC.Models;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.MVC.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var viewModel = new ProductVM();
            viewModel.Products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7158/api/Product");
            }

            return View(viewModel);
        }
    }
}
