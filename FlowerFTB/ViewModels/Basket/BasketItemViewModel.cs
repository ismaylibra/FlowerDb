using FlowerFTB.Models;

namespace FlowerFTB.ViewModels.Basket
{
    public class BasketItemViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public int CategoryId { get; set; }
        public string? ImageUrl { get; set; }
        public string? Content { get; set; }
        public Category? Category { get; set; }
        public int Discount { get; set; }
        public int Count { get; set; }
    }
}
