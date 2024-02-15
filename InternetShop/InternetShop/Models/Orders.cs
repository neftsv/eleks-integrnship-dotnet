namespace InternetShop.Models
{
    public class Orders
    {
        public int Id { get; set; }
        public int UserId {  get; set; }
        public List<int>? CartProductsId {  get; set; }
        public decimal TotalPrice {  get; set; }
        public DateTime Date {  get; set; }
    }
}