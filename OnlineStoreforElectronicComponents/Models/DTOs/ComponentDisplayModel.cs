namespace OnlineStoreforElectronicComponents.Models.DTOs
{
    public class ComponentDisplayModel
    {
        public IEnumerable<Component> Components { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public string STerm { get; set; } = "";
        public int CategoryId { get; set; } = 0;
    }
}
