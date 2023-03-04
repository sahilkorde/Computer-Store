using Computer_Store_ClientStore.ViewModel;

namespace Computer_Store_ClientStore.Service.IService
{
    public interface ICartService
    {
        public event Action OnChange;
        Task DecreamentCart(ShoppingCart shoppingCart);
        Task IcreamentCart(ShoppingCart shopingCart);
    }
}
