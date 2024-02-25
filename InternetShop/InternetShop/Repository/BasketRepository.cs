using InternetShop.Data;
using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Repository
{
    public class BasketRepository : IBasketRepository
    {
        private readonly ApplicationDbContext _context;

        public BasketRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool DeleteProduct(CartsProducts cartsProducts)
        {
            _context.Remove(cartsProducts);
            return Save();
        }

        public async Task<int> GetCartItemCount(string userName)
        {
            return await _context.CartsProducts.Where(cp => cp.Carts.User.Email == userName).CountAsync();
        }

        public async Task<CartsProducts> GetCartProductAsync(int cartsProductsId)
        {
            return await _context.CartsProducts.FirstOrDefaultAsync(cp => cp.Id == cartsProductsId);
        }

        public async Task<CartViewModel> GetUserCratProductsAsync(string userName)
        {
            var model = new CartViewModel();
            var items = await _context.CartsProducts.Where(cp => cp.Carts.User.Email == userName)
                .Select(cp => new Items
                {
                    Id = cp.Id,
                    ProductId = cp.ProductId,
                    ProductName = cp.Products.Name,
                    Quantity = cp.Quantity,
                    Price = cp.Products.Price,
                })
                .ToListAsync();

            //for (int i = 0; i < 4; i++)
            //{
            //    items.AddRange(items);
            //}
            

            decimal totalPrice = 0;
            foreach (var item in items)
            {
                totalPrice += Math.Round(item.Price * item.Quantity, 2);
            }

            model.Items= items;
            model.TotalPrice = totalPrice;
            return model;
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
