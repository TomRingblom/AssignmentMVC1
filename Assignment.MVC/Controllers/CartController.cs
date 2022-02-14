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

                var responseTask = client.GetAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" + "?key=Banana");
                var result = responseTask.Result;

                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    return View(viewModel);
                }
                else
                {
                    viewModel.ListCart = await client.GetFromJsonAsync<IEnumerable<ShoppingCartDetailsModel>>("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" + "?key=Banana");
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
                                                       $"{viewModel.UserId}" + $"?productId={viewModel.ProductId}" + "?key=Banana");

                    var result = responseTask.Result;

                    if (result.StatusCode == HttpStatusCode.NoContent)
                    {
                        await client.PostAsJsonAsync("https://localhost:7158/api/ShoppingCart" + "?key=Banana", viewModel);
                    }
                    else
                    {
                        var cartFromApi = new ShoppingCartModel();
                        cartFromApi = await client.GetFromJsonAsync<ShoppingCartModel>(
                            "https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}" +
                            $"?productId={viewModel.ProductId}" + "?key=Banana");
                        viewModel.Count = cartFromApi.Count += viewModel.Count;
                        viewModel.CartId = cartFromApi.CartId;
                        await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.CartId}" + "?key=Banana",
                            viewModel);
                    }

                }

                return RedirectToAction(nameof(Index));
            } 
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> DetailsPost(CreateOrderModel model)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            using (var client = new HttpClient())
            {
                var viewModel = new CreateOrderModel();
                viewModel.CustomerId = claim.Value;
                viewModel.ShoppingCarts = await client.GetFromJsonAsync<IEnumerable<ShoppingCartModel>>("https://localhost:7158/api/ShoppingCart/Summary?id=" + $"{viewModel.CustomerId}" + "?key=Banana");
                await client.PostAsJsonAsync("https://localhost:7158/api/Order" + "?key=Banana", viewModel);
            }

            return RedirectToAction("Index"); // Byt denna sen
        }

        public async Task<IActionResult> Add(int cartId)
        {
            using (var client = new HttpClient())
            {
                await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/AddOneToCart?id=" + $"{cartId}" + "?key=Banana", cartId);
            }

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Sub(int cartId)
        {
            using (var client = new HttpClient())
            {
                await client.PutAsJsonAsync("https://localhost:7158/api/ShoppingCart/SubOneToCart?id=" + $"{cartId}" + "?key=Banan", cartId);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
