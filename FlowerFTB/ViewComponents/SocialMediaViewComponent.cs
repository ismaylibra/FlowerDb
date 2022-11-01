using FlowerFTB.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlowerFTB.ViewComponents
{
    public class SocialMediaViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public SocialMediaViewComponent(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var social = await _dbContext.SocialMedias.ToListAsync();

            return View(social);
        }
    }
}
