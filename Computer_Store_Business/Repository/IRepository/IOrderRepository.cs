using Computer_Store_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Business.Repository.IRepository
{
    public interface IOrderRepository
    {
        public Task<OrderDTO> get(int id);
        public Task<OrderDTO> Create(OrderDTO objDTO);
        public Task<int> Delete(int id);
        public Task<IEnumerable<OrderDTO>> getAll(string? userId = null, string? status = null);
        public Task<OrderHeaderDTO> Updateheader(OrderHeaderDTO objDTO);
        public Task<OrderHeaderDTO> MarkPaymentSuccessful(int id, string PaymentIntentId);
        public Task<bool> UpdateOrderStatus(int id, string status);
        public Task<OrderHeaderDTO> CancelOrder(int id);
    }
}
