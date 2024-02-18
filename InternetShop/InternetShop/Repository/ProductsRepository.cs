using InternetShop.Interface;
using InternetShop.Models;
using InternetShop.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;

namespace InternetShop.Repository
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly List<Products> _context;
        public ProductsRepository()
        {
            var data = new List<Products>();

            Random rnd = new Random();

            List<Categories> categories = new List<Categories>();

            for (int i = 1; i < 6; i++)
            {
                categories.Add(new Categories
                {
                    Id = i,
                    Name = "Category: " + i.ToString(),
                });
            }

            for (int i = 0; i < 200; i++)
            {
                data.Add(new Products
                {
                    Id = i,
                    Name = rnd.Next(1,10).ToString(),
                    UserId = i,
                    Price = (decimal)rnd.Next(10000, 20000) / 100,
                    Images = new List<Images>()
                    {
                        new Images()
                        {
                            Url = "https://cdn1.vectorstock.com/i/1000x1000/81/75/temporary-rubber-stamp-vector-17998175.jpg"
                        }
                    },
                    CategoryId = categories[rnd.Next(0, 5)].Id.ToString(),
                });
            }

            _context = data;
        }

        public async Task<ICollection<Products>> GetAllProductsAsync()
        {
            return _context;
        }

        public async Task<ICollection<Products>> GetProductsByCategoryAsync(ICollection<Products> products, ICollection<int> categoryId)
        {

            var sortedData= new List<Products>();
            foreach (var category in categoryId)
            {
                var data = products.Where(d => d.CategoryId == category.ToString()).ToList();
                sortedData.AddRange(data);
            }

            return sortedData;
        }

        public async Task<ProductsViewModel> PaginationProductsAsync(int page, ICollection<Products> products)
        {
            int pageSize = 18;

            Random rnd = new Random();

            List<Categories> categories = new List<Categories>();

            for (int i = 1; i < 6; i++)
            {
                categories.Add(new Categories
                {
                    Id = i,
                    Name = "Category: " + i.ToString(),
                });
            }

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

        public async Task<ICollection<Products>> SearchAsync(ICollection<Products> products, string productName)
        {
            return products.Where(p => p.Name == productName).ToList();
        }
    }
}
