using Computer_Store_Models;

namespace Computer_Store_ClientStore.Service.IService
{
    public interface IProductService
    {
        public Task<IEnumerable<ProductDTO>> getAll(string? search = null, List<int>? catid = null, int? minprice = null, int? maxprice = null);
        public Task<ProductDTO> get(int id);
    }
}
