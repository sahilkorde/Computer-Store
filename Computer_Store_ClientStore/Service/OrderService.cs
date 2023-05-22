using Computer_Store_ClientStore.Service.IService;
using Computer_Store_Models;
using Newtonsoft.Json;
using System.Text;

namespace Computer_Store_ClientStore.Service
{
    public class OrderService : IOrderService
    {
        private readonly HttpClient _httpClient;
        private IConfiguration _configuration;
        private string BaseServerUrl;
        public OrderService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            BaseServerUrl = _configuration.GetSection("BaseServerUrl").Value;
        }

        public async Task<OrderDTO> Create(StripePaymentDTO paymentDTO)
        {
            var content = JsonConvert.SerializeObject(paymentDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/order/Create", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if(response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderDTO>(responseResult);
                return result;
            }
            return new OrderDTO();
        }

        public async Task<OrderDTO> get(int orderId)
        {
            var response = await _httpClient.GetAsync($"/api/order/get/{orderId}");
            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var order = JsonConvert.DeserializeObject<OrderDTO>(content);
                return order;
            }
            else
            {
                var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(content);
                throw new Exception(errorModel.ErrorMessage);
            }
        }

        public async Task<IEnumerable<OrderDTO>> getAll(string? userId)
        {
            var response = await _httpClient.GetAsync($"/api/order/getAll?userid={userId}");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var orders = JsonConvert.DeserializeObject<IEnumerable<OrderDTO>>(content);
                return orders;
            }
            return new List<OrderDTO>();
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccssful(OrderHeaderDTO orderHeader)
        {
            var content = JsonConvert.SerializeObject(orderHeader);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/order/paymentsuccessful", bodyContent);
            string responseResult = response.Content.ReadAsStringAsync().Result;
            if (response.IsSuccessStatusCode)
            {
                var result = JsonConvert.DeserializeObject<OrderHeaderDTO>(responseResult);
                return result;
            }
            var errorModel = JsonConvert.DeserializeObject<ErrorModelDTO>(responseResult);
            throw new Exception(errorModel.ErrorMessage);
        }
    }
}
