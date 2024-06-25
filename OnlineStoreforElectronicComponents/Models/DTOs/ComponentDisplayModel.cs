namespace OnlineStoreforElectronicComponents.Models.DTOs
{
    public class ComponentDisplayModel
    {
        public IEnumerable<Component> Components { get; set; }
        public IEnumerable<TypeOfComponent> TypeOfComponents { get; set; }
        public string STerm { get; set; } = "";
        public int TypeOfComponentId { get; set; } = 0;
    }
}
