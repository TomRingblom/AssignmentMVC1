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
    }
}
