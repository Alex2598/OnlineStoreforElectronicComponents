using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreforElectronicComponents.Models
{
    [Table("Component")]
    public class Component
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? ComponentName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? Package { get; set; }
        [Required]
        public double Price { get; set; }
        public string? Image { get; set; }
        [Required]
        public int TypeOfComponentId { get; set; }
        public TypeOfComponent TypeOfComponent { get; set; }
        public List<OrderDetail> OrderDetail { get; set; }
        public List<CartDetail> CartDetail { get; set; }
        public Stock Stock { get; set; }

        [NotMapped]
        public string TypeOfComponentName { get; set; }
        [NotMapped]
        public int Quantity { get; set; }


    }
}
