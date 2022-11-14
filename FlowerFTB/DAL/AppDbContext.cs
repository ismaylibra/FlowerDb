using FlowerFTB.Models;
using FlowerFTB.Models.IdentityModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace FlowerFTB.DAL
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }  
        public DbSet<SocialMedia> SocialMedias { get; set; }   
        
    }
}
