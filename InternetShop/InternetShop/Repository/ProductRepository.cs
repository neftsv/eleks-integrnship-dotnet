using InternetShop.Data;
using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddCartToUser(Carts cart)
        {
            _context.Add(cart);
            return Save();
        }

        public bool AddProductToCart(CartsProducts products)
        {
            _context.Add(products);
            return Save();
        }

        public async Task<ProductViewModel> GetProductInfoAsync(int productId)
        {
            return await _context.UsersProducts.Where(up => up.ProductId == productId)
                .Select(up => new ProductViewModel
                {
                    Id = up.ProductId,
                    Name = up.Products.Name,
                    Description = up.Products.Description,
                    Price = up.Products.Price,
                    Seller = $"{up.Users.Surname} {up.Users.Name} {up.Users.Patronimic}",
                    PhoneNumber = up.Users.PhoneNumber,
                    Images = up.Products.Images.Select(i => i.Url).ToList(),
                })
                .FirstOrDefaultAsync();
        }

        public async Task<Users> GetUserAsync(string userName)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == userName);
        }

        public async Task<Carts> GetUserCartAsync(int userId)
        {
            return await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
