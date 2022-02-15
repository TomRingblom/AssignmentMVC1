using System.Net;
using System.Security.Claims;
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
                    viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7158/api/product?key=Banana");
                else
                    viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7158/api/product?key=Banana&subcategory=" + $"{input}");
            }

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? productId)
        {
            var viewModel = new ProductDetailsModel();
            viewModel.Product = new ProductModel();

            if (productId == null || productId == 0)
            {
                return NotFound();
            }

            using (var client = new HttpClient())
            {
                viewModel.Product = await client.GetFromJsonAsync<ProductModel>($"https://localhost:7158/api/product/{productId}?key=Banana");
            }

            return View(viewModel);
        }
    }
}
