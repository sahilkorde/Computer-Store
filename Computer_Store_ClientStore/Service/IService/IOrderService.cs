using Computer_Store_Models;

namespace Computer_Store_ClientStore.Service.IService
{
    public interface IOrderService
    {
        public Task<IEnumerable<OrderDTO>> getAll(string? userId);
        public Task<OrderDTO> get(int orderId);
        public Task<OrderDTO> Create(StripePaymentDTO paymentDTO);
        public Task<OrderHeaderDTO> MarkPaymentSuccssful(OrderHeaderDTO orderHeader);
    }
}
