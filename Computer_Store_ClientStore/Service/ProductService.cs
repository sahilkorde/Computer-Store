using Computer_Store_ClientStore.Service.IService;
using Computer_Store_Models;
using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Computer_Store_ClientStore.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public ProductService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }
        public async Task<ProductDTO> get(int id)
        {
            var response = await _httpClient.GetAsync($"/api/product/{id}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var product = JsonConvert.DeserializeObject<ProductDTO>(content);
                product.ImageUrl = BaseServerUrl + product.ImageUrl;
                return product;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<ProductDTO>> getAll(string? search, List<int>? catid, int? minprice, int? maxprice)
        {
            string catlist = null;
            if(catid != null) { 
                catlist = string.Join(",", catid);
            }

            var response = await _httpClient.GetAsync($"/api/product?search={ search}&catid={ catlist}&minprice={ minprice}&maxprice={ maxprice}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<IEnumerable<ProductDTO>>(content);
                foreach(var prod in products)
                {
                    prod.ImageUrl = BaseServerUrl + prod.ImageUrl;
                }
                return products;
            }
            return new List<ProductDTO>();
        }
    }
}
