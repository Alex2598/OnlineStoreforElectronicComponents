using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreforElectronicComponents.Models
{
    [Table("CartDetail")]
    public class CartDetail
    {
        public int Id { get; set; }
        [Required]
        public int ShoppingCartId { get; set; }
        [Required]
        public int ComponentId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public double UnitPrice { get; set; }   
        public Component Component { get; set; }
        public ShoppingCart ShoppingCart { get; set; }
    }
}
