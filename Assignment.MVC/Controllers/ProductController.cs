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
                    viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7158/api/Product");

                viewModel.Products = await client.GetFromJsonAsync<IEnumerable<ProductModel>>("https://localhost:7158/api/Product/" + $"?subcategory={input}");
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
                viewModel.Product = await client.GetFromJsonAsync<ProductModel>("https://localhost:7158/api/Product/" + $"{productId}");
            }

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Details(ProductDetailsModel model)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            var viewModel = new ShoppingCartModel();
            viewModel.Count = 1;
            viewModel.UserId = claim.Value;
            viewModel.ProductId = model.Product.Id;
            viewModel.Price = model.Product.Price;

            using (var client = new HttpClient())
            {
                var responseTask = client.GetAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" + $"?productId={viewModel.ProductId}");

                var result = responseTask.Result;

                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    await client.PostAsJsonAsync("https://localhost:7158/api/ShoppingCart", viewModel);
                }
                else 
                {
                    var cartFromApi = new ShoppingCartModel();
                    cartFromApi = await client.GetFromJsonAsync<ShoppingCartModel>("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" + $"?productId={viewModel.ProductId}");
                    viewModel.Count = cartFromApi.Count += viewModel.Count;
                    viewModel.Id = cartFromApi.Id;
                    await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.Id}", viewModel);
                }
                
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
