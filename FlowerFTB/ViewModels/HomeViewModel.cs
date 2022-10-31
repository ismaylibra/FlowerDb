using FlowerFTB.Models;

namespace FlowerFTB.ViewModels
{
    public class HomeViewModel
    {
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<SliderImage> SliderImages { get; set; } = new List<SliderImage>();
        public Slider? Slider { get; set; }


    }
}
