using FlowerFTB.DAL;
using FlowerFTB.Data;
using FlowerFTB.Services;
using FlowerFTB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerFTB.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext? _dbContext;
        private readonly IMailService _mailService;
        public HomeController(AppDbContext dbContext, IMailService mailService)
        {
            _dbContext = dbContext;
            _mailService = mailService;
        }
        public async  Task<IActionResult> Index()
        {
           await _mailService.SendEmailAsync(new Data.RequestEmail { Body = "Hello", ToEmail = "ismayiliib@code.edu.az", Subject = "For Test" });
            Response.Cookies.Append("cookie", "Test", new CookieOptions { Expires = DateTimeOffset.Now.AddHours(1) });
            var sliderImages = await _dbContext.SliderImages.ToListAsync();
            var slider = await _dbContext.Sliders.SingleOrDefaultAsync();
            var categories = await _dbContext.Categories.ToListAsync();
            var products = await _dbContext.Products.ToListAsync();

            var hvmodel = new HomeViewModel()
            {
                Slider = slider,
                SliderImages = sliderImages,
                Categories = categories,
                Products =  products
            };
            return View(hvmodel);
        }

        public IActionResult Search(string searchText)
        {
            var products = _dbContext.Products.Where(x=> x.Name.ToLower().Contains(searchText)).ToList();
            return PartialView("_SearchProductPartialView", products);
        }
    }
       
}
