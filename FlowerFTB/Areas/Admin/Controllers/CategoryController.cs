using FlowerFTB.Areas.Admin.Models;
using FlowerFTB.DAL;
using FlowerFTB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FlowerFTB.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
            _context = context;
        }

        public async  Task<IActionResult> Index()
        {
            var categories = await _context.Categories.ToListAsync();
            return View(categories);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null) return NotFound();

            var category = await _context.Categories.FindAsync(id);

            if (category is null) return NotFound();

            return View(category);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateModel category)
        {
            if (!ModelState.IsValid) return View();

            var existName = await _context.Categories.AnyAsync(x => x.Name.ToLower().Equals(category.Name.ToLower()));

            if (existName)
            {
                ModelState.AddModelError("name", "Daxil etdiyiniz ad artıq mövcuddur. Başqa alternativləri yoxlayın");
                return View();
            }

            var categoryEntity = new Category
            {
                Name = category.Name,
                Description = category.Description
            };
            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null)  return NotFound();

            var category = await _context.Categories.FindAsync(id);

            if (category is null) return NotFound();
            return View(new CategoryUpdateModel
            {
                Name = category.Name,
                Description = category.Description
            });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, CategoryUpdateModel model)
        {
            if (id is null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View();
            }

            var category = await _context.Categories.FindAsync(id);

            if (category is null) return NotFound();
            var isExistName = await _context.Categories.AnyAsync(c => c.Name.ToLower() == model.Name.ToLower() && c.Id != id);
            if (isExistName)
            {
                ModelState.AddModelError("Name", "Daxil etdiyiniz adda kateqoriya  mövcuddur..!");
                return View(model);
            }
            category.Name = model.Name;
            category.Description = model.Description;
            await _context.SaveChangesAsync();
            
           
            return View();
        }
        
        public async Task<IActionResult> Delete (int? id)
        {
            if (id is null) return NotFound();

            var category = await _context.Categories.FindAsync(id);

            if (category is null) return NotFound();

            _context.Categories.Remove(category);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));

        }
    }
}
