using Blazored.LocalStorage;
using Computer_Store_ClientStore.Service.IService;
using Computer_Store_ClientStore.ViewModel;
using Computer_Store_Common;

namespace Computer_Store_ClientStore.Service
{
    public class CartSerivce : ICartService
    {
        private readonly ILocalStorageService _localStorage;
        public event Action OnChange;
        public CartSerivce(ILocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }
        public async Task DecreamentCart(ShoppingCart shoppingCart)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);
            for(int i=0; i<cart.Count; i++)
            {
                if (cart[i].ProductId == shoppingCart.ProductId && cart[i].ProductPriceId == shoppingCart.ProductPriceId)
                {
                    if (cart[i].Count == 1 || shoppingCart.Count == 0)
                    {
                        cart.RemoveAt(i);
                    }
                    else
                    {
                        cart[i].Count -= shoppingCart.Count;
                    }
                }
            }
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
            OnChange.Invoke();
        }

        public async Task IcreamentCart(ShoppingCart shopingCart)
        {
            var cart = await _localStorage.GetItemAsync<List<ShoppingCart>>(SD.ShoppingCart);
            bool itemInCart = false;
            if(cart == null)
            {
                cart = new List<ShoppingCart>();
            }
            foreach (var item in cart)
            {
                if (item.ProductId == shopingCart.ProductId && item.ProductPriceId == shopingCart.ProductPriceId)
                {
                    itemInCart = true;
                    item.Count += shopingCart.Count;
                }
            }
            if(!itemInCart)
            {
                cart.Add(new ShoppingCart()
                {
                    ProductId = shopingCart.ProductId,
                    ProductPriceId= shopingCart.ProductPriceId,
                    Count = shopingCart.Count
                });
            }
            
            await _localStorage.SetItemAsync(SD.ShoppingCart, cart);
            OnChange.Invoke();
        }
    }
}
