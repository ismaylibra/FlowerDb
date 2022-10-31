using FlowerFTB.Models;
using Microsoft.EntityFrameworkCore;

namespace FlowerFTB.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Slider> Sliders { get; set; }  
    }
}
