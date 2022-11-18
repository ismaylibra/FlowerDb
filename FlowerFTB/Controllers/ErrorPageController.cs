using FlowerFTB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlowerFTB.Controllers
{
    public class ErrorPageController : Controller
    {
        public IActionResult ErrorAction(int statusCode)

        {
            ErrorViewModel error = new ErrorViewModel()
            {
                StatusCode = HttpContext.Response.StatusCode,
                Title = HttpContext.Response.Headers.ToString()
        };
            return View();
        }
    }
}
