using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class CartsProducts
    {
        [Key]
        public int Id { get; set; }
        public int CartId {  get; set; }

        [ForeignKey("CartId")]
        public Carts Carts { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Products Products { get; set; }
        public int Quantity {  get; set; }
    }
}
