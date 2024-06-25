using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreforElectronicComponents.Models
{
    [Table("OrderDetail")]
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderId { get; set; }
        [Required]
        public int ComponentId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }
        public Order Order { get; set; }
        public Component Component { get; set; }
    }
}
