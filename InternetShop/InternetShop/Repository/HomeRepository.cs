using InternetShop.Data;
using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace InternetShop.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<HomeViewModel>> GetAllCategoriesAsync()
        {
            return await _context.Categories.Select(c => new HomeViewModel
            {
                Id = c.Id,
                Name = c.Name,
            }).ToListAsync();
        }
    }
}
