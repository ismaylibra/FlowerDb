using FlowerFTB.DAL;
using FlowerFTB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerFTB.ViewComponents
{
    public class DiscountProductViewComponent: ViewComponent
    {
       private readonly AppDbContext _dbContext;

        public DiscountProductViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Product> discount = await _dbContext.Products.Where(x => x.Discount != null).Include(x => x.Category).ToListAsync();
            return View(discount);
        }
    }
}
 