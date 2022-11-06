using FlowerFTB.DAL;
using FlowerFTB.ViewModels.Basket;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace FlowerFTB.Controllers
{
    public class BasketController : Controller
    {
        private readonly AppDbContext? _dbContext;
        public BasketController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var basketItems = GetBasketItems();
            return View(basketItems);
        }
        public async Task<IActionResult> AddToBasket(int? productId)
        {
            if (productId is null) return BadRequest();
            var product = await _dbContext.Products.Where(p => p.Id == productId).FirstOrDefaultAsync();
            if (product != null) NotFound();


            var basket = Request.Cookies["basket"];
            var basketItems = new List<BasketItemViewModel>();


            var basketItem = new BasketItemViewModel
            {
                Id = product.Id,
                Name = product.Name,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Content = product.Content,
                Count = 1
            };

            if (basket is null)
            {

                basketItems.Add(basketItem);
            }
            else
            {

                basketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basket);
                var existProduct = basketItems.Where(b => b.Id == product.Id).FirstOrDefault();
                if (existProduct is null)
                {
                    basketItems.Add(basketItem);
                }
                else
                {
                    existProduct.Count += 1;
                }

            }
            CookieOptions option = new CookieOptions();
            option.Expires = DateTime.Now.AddYears(1);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems), option);
            Response.Cookies.Append("basket", JsonConvert.SerializeObject(basketItems));
            return Ok();

        }
        public async Task<IActionResult> GetBasketCount()
        {
            var basketItems = GetBasketItems();
            return Ok(basketItems.Count);
        }
        private List<BasketItemViewModel> GetBasketItems()
        {
            var basket = Request.Cookies["basket"];
            var basketItems = basket is not null
                    ? JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basket)
                    : new List<BasketItemViewModel>();
            return basketItems;
        }

    }
}
