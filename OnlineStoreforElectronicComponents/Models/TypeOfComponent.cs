using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OnlineStoreforElectronicComponents.Models
{
    [Table("TypeOfComponent")]
    public class TypeOfComponent
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string TypeOfComponentName { get; set; }
        public List<Component> Components { get; set; }
    }
}
