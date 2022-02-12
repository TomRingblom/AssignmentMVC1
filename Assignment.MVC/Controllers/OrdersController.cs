using Assignment.MVC.Models;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Assignment.MVC.Controllers
{
    public class OrdersController : Controller
    {
        public async Task<IActionResult> Index()
        {
            using (var client = new HttpClient())
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                var viewModel = new OrderHistoryVM();
                viewModel.UserId = claim.Value;

                var responseTask = client.GetAsync("https://localhost:7158/api/Order?id=" + $"{viewModel.UserId}");
                var result = responseTask.Result;

                if (result.StatusCode == HttpStatusCode.NoContent)
                {
                    return View(viewModel);
                }
                else
                {
                    viewModel.ListCart = await client.GetFromJsonAsync<IEnumerable<OrderHistoryModel>>("https://localhost:7158/api/Order?id=" + $"{viewModel.UserId}");
                    foreach (var price in viewModel.ListCart)
                    {
                        viewModel.CartTotal += price.Quantity * price.Price;
                    }
                }
                return View(viewModel);
            }
            return View();
        }
    }
}
