using FlowerFTB.DAL;
using FlowerFTB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerFTB.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;
        public ProductController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            var products = _dbContext.Products.ToList();
            var categories = _dbContext.Categories.ToList();

            HomeViewModel hvm = new HomeViewModel()
            {
                Products = products,
                Categories = categories
            };
            return View(hvm);
        }
        public IActionResult Details(int? id)
        {

            if (id is null) return BadRequest();

            var product = _dbContext.Products.Include(x => x.Category).SingleOrDefault(x => x.Id == id);

            if (product is null) return NotFound();

            return View(product);
        }
    }
}
