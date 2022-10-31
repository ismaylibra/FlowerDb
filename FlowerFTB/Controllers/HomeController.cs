using FlowerFTB.DAL;
using FlowerFTB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace FlowerFTB.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext? _dbContext;
        public HomeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var sliderImages = _dbContext.SliderImages.ToList();
            var slider = _dbContext.Sliders.SingleOrDefault();
            var categories = _dbContext.Categories.ToList();
            var products = _dbContext.Products.ToList();

            var hvmodel = new HomeViewModel()
            {
                Slider = slider,
                SliderImages = sliderImages,
                Categories = categories,
                Products =  products
            };
            return View(hvmodel);
        }
    }
}
