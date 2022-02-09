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
                viewModel.ListCart = new List<ShoppingCartModel>();
                viewModel.UserId = claim.Value;

                var responseTask = client.GetAsync("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}");
                //responseTask.Wait();

                var result = responseTask.Result;

                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    return View(viewModel);
                }
                else
                {
                    viewModel.ListCart = await client.GetFromJsonAsync<IEnumerable<ShoppingCartModel>>("https://localhost:7158/api/ShoppingCart/" + $"{viewModel.UserId}");
                    
                    foreach (var item in viewModel.ListCart)
                    {
                        viewModel.Product = await client.GetFromJsonAsync<ProductModel>("https://localhost:7158/api/Product/" + $"{item.Id}");
                    }
                }
                return View(viewModel);
            }
        }
    }
}
