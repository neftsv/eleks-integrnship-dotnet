using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
    public class Orders
    {
        [Key]
        public int Id { get; set; }
        public string UserId {  get; set; }

        [ForeignKey("UserId")]
        public Users Users { get; set; }
        public virtual List<OredersProducts> OredersProducts {  get; set; }
        public decimal TotalPrice {  get; set; }
        public DateTime Date {  get; set; }
    }
}
