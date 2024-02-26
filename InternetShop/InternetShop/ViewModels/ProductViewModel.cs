namespace InternetShop.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Seller { get; set; }
        public string PhoneNumber { get; set; }
        public ICollection<string> Images { get; set; }
    }
}
