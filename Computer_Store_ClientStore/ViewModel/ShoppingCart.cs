using Computer_Store_Models;

namespace Computer_Store_ClientStore.ViewModel
{
    public class ShoppingCart
    {
        public int ProductId { get; set; }
        public ProductDTO Product { get; set; }
        public int ProductPriceId { get; set; }
        public ProductPriceDTO ProductPirce { get; set; }
        public int Count { get; set; }
    }
}
