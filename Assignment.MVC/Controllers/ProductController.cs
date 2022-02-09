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
                //responseTask.Wait();

                var result = responseTask.Result;

                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    await client.PostAsJsonAsync("https://localhost:7158/api/ShoppingCart", viewModel);

                    
                }
                else //web api sent error response 
                {
                    //log response status here..

                    var cartFromApi = new ShoppingCartDetailsModel();
                    cartFromApi.ShoppingCart = await client.GetFromJsonAsync<ShoppingCartModel>("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" + $"?productId={viewModel.ProductId}");
                    viewModel.Count = IncrementCount(cartFromApi.ShoppingCart, viewModel.Count);
                    viewModel.Id = cartFromApi.ShoppingCart.Id;
                    await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.Id}", viewModel);
                }

                //var cartFromApi = new ShoppingCartDetailsModel();
                //cartFromApi.ShoppingCart = new ShoppingCartModel();
                //cartFromApi.ShoppingCart = await client.GetFromJsonAsync<ShoppingCartModel>("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" + $"?productId={viewModel.ProductId}");
                
            }

            return RedirectToAction(nameof(Index));
        }
        public int IncrementCount(ShoppingCartModel shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }

        public int DecrementCount(ShoppingCartModel shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }
    }
}
