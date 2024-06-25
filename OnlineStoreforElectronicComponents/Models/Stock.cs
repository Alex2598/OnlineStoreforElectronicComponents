using System.ComponentModel.DataAnnotations.Schema;

namespace OnlineStoreforElectronicComponents.Models
{
    [Table("Stock")]
    public class Stock
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }

        public Component? Component { get; set; }
    }
}
