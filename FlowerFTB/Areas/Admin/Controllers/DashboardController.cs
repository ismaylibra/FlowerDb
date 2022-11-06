using Microsoft.AspNetCore.Mvc;

namespace FlowerFTB.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
