namespace OnlineStoreforElectronicComponents.Repositories
{
    public interface ICartRepository
    {
        Task<Order> GetOrderForUser(string userId);
        Task<int> AddItem(int componentId, int qty);
        Task<int> RemoveItem(int componentId);
        Task<ShoppingCart> GetUserCart();
        Task<int> GetCartItemCount(string userId = "");
        Task<ShoppingCart> GetCart(string userId);
        Task<bool> DoCheckout(CheckoutModel model);
    }
}
