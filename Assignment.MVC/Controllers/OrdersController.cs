using Assignment.MVC.Models;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace Assignment.MVC.Controllers
{
    //public class OrdersController : Controller
    //{
    //    public async Task<IActionResult> Index()
    //    {
    //        using (var client = new HttpClient())
    //        {
    //            var claimsIdentity = (ClaimsIdentity)User.Identity;
    //            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

    //            var viewModel = new OrderHistoryVM();

    //            var responseTask = client.GetAsync("https://localhost:7158/api/Order?id=" + $"{claim.Value}");
    //            var result = responseTask.Result;

    //            if (result.StatusCode == HttpStatusCode.NoContent)
    //            {
    //                return View(viewModel);
    //            }
    //            else
    //            {
    //                viewModel.OrderItems = await client.GetFromJsonAsync<IEnumerable<OrderModel>>("https://localhost:7158/api/Order?id=" + $"{claim.Value}");
    //                //foreach (var price in viewModel.OrderItems)
    //                //{
    //                //    viewModel. += price.OrderItems.Count * price.OrderItems.;
    //                //}
    //            }

    //            return View(viewModel);
    //        }
    //        //return View();
    //    }
    //}
}
