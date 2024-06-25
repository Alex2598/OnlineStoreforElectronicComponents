namespace OnlineStoreforElectronicComponents
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Component>> GetComponents(string sTerm = "", int typeOfComponentId = 0);
        Task<IEnumerable<TypeOfComponent>> TypeOfComponents();
    }
}