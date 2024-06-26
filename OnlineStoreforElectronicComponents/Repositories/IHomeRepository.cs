namespace OnlineStoreforElectronicComponents
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Component>> GetComponents(string sTerm = "", int categoryId = 0);
        Task<IEnumerable<Category>> Categories();
    }
}