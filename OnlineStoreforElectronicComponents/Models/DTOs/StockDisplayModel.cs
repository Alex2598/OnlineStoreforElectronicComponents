namespace OnlineStoreforElectronicComponents.Models.DTOs
{
    public class StockDisplayModel
    {
        public int Id { get; set; }
        public int ComponentId { get; set; }
        public int Quantity { get; set; }
        public string? ComponentName { get; set; }
    }
}
