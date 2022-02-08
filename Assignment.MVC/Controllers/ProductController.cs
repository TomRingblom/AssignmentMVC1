using Assignment.MVC.Models;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.MVC.Controllers
{
    public class ProductController : Controller
    {
        public async Task<IActionResult> Index(string input)
        {
            var viewModel = new ProductVM();
            viewModel.Products = new List<ProductModel>();

            using (var client = new HttpClient())
            {
                if (input == null)
                    viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7158/api/Product");

                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7158/api/Product/" + $"?subcategory={input}");
            }

            return View(viewModel);
        }

        public async Task<IActionResult> Details(int? id)
        {
            var viewModel = new ProductVM();
            viewModel.Product = new ProductModel();

            if (id == null || id == 0)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                viewModel.Product = await client.GetFromJsonAsync<ProductModel>("https://localhost:7158/api/Product/" + $"{id}");
            }

            return View(viewModel);
        }
    }
}
