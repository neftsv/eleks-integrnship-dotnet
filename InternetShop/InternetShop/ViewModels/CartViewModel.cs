namespace InternetShop.ViewModels
{
    public class CartViewModel
    {
        public decimal TotalPrice { get; set; }
        public ICollection<Items> Items { get; set; }
    }
    
    public class Items
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
