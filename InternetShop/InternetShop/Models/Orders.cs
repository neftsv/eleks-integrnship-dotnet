using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public int UserId {  get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public virtual List<OrdersProducts> OrdersProducts {  get; set; }
        public decimal TotalPrice {  get; set; }
        public DateTime Date {  get; set; }
    }
}
