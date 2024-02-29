using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShop.Models
{
	public class Delivery
	{
		[Key]
		public int Id { get; set; }
		public string DeliveryType { get; set; }
		public virtual List<Orders> Orders { get; set; }
	}
}
