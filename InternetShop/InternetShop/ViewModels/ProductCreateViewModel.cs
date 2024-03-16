namespace InternetShop.ViewModels
{
    public class ProductCreateViewModel
    {
        public int Id { get; set; }
        public IEnumerable<IFormFile> Images { get; set; }
        public int UserId { get; set; }
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

    }
}
