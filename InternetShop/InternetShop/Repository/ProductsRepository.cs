using InternetShop.Data;
using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace InternetShop.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly AppDbContext _context;

        public ProductsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Products>> GetAllProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }


        public async Task<ProductsViewModel> PaginationProductsAsync(int page, ICollection<Products> products)
        {
            int pageSize = 18;

            var categories = await _context.Categories.ToListAsync();

            var totalCount = products.Count();
            var items = products
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            var viewModel = new ProductsViewModel
            {
                Items = items,
                PageNumber = page,
                TotalPages = totalPages,
                Categories = categories,
            };

            return viewModel;
        }
    }
}
