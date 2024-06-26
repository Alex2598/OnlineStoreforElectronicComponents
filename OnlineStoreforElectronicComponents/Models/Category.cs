using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreforElectronicComponents.Models
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string CategoryName { get; set; }
        public List<Component> Components { get; set; }
    }
}
