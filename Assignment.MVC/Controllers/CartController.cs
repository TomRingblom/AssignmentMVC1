using System.Net;
using System.Security.Claims;
using Assignment.MVC.Models;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.MVC.Controllers
{
    public class CartController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var viewModel = new ShoppingCartVM();
                viewModel.ListCart = new List<ShoppingCartDetailsModel>();
                viewModel.UserId = claim.Value;

                var responseTask = client.GetAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}");
                var result = responseTask.Result;

                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    return View(viewModel);
                }
                else
                {
                    viewModel.ListCart = await client.GetFromJsonAsync<IEnumerable<ShoppingCartDetailsModel>>("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}");
                    foreach (var price in viewModel.ListCart)
                    {
                        viewModel.CartTotal += price.Count * price.Price;
                    }
                }
                return View(viewModel);
            }
        }

        public async Task<IActionResult> Details(ProductDetailsModel model)
        {
            if (ModelState.IsValid)
            {
                var claimsIdentity = (ClaimsIdentity) User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var viewModel = new ShoppingCartModel();
                viewModel.Count = 1;
                viewModel.UserId = claim.Value;
                viewModel.ProductId = model.Product.Id;
                viewModel.Price = model.Product.Price;

                using (var client = new HttpClient())
                {
                    var responseTask = client.GetAsync("https://localhost:7158/api/ShoppingCart/" +
                                                       $"{viewModel.UserId}" + $"?productId={viewModel.ProductId}");

                    var result = responseTask.Result;

                    if (result.StatusCode == HttpStatusCode.NoContent)
                    {
                        await client.PostAsJsonAsync("https://localhost:7158/api/ShoppingCart", viewModel);
                    }
                    else
                    {
                        var cartFromApi = new ShoppingCartModel();
                        cartFromApi = await client.GetFromJsonAsync<ShoppingCartModel>(
                            "https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" +
                            $"?productId={viewModel.ProductId}");
                        viewModel.Count = cartFromApi.Count += viewModel.Count;
                        viewModel.Id = cartFromApi.Id;
                        await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.Id}",
                            viewModel);
                    }

                }

                return RedirectToAction(nameof(Index));
            }
            else
                return BadRequest();
        }
        public async Task<IActionResult> Add(int cartId)
        {
            using (var client = new HttpClient())
            {
                await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/AddOneToCart?id=" + $"{cartId}", cartId);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Sub(int cartId)
        {
            using (var client = new HttpClient())
            {
                await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/SubOneToCart?id=" + $"{cartId}", cartId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
