using Assignment.MVC.Models;
using Assignment.MVC.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Assignment.MVC.Controllers
{
    public class CategoryController : Controller
    {
        public async Task<IActionResult> CategoryMenuItems()
        {
            var viewModel = new CategoryVM();
            viewModel.Categories = new List<CategoryModel>();

            using (var client = new HttpClient())
            {
                viewModel.Categories = await client.GetFromJsonAsync<IEnumerable<CategoryModel>>("https://localhost:7158/api/category?key=Banana");
            }

            return View(viewModel);
        }
    }
}
