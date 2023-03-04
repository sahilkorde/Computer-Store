using Computer_Store_Client.ViewModel;

namespace Computer_Store_Client.Service.IService
{
    public interface ICartService
    {
        Task DecreamentCart(ShoppingCart shoppingCart);
        Task IcreamentCart(ShoppingCart shopingCart);
    }
}
