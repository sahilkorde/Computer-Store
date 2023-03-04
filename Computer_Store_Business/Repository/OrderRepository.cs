using AutoMapper;
using Computer_Store_Business.Repository.IRepository;
using Computer_Store_Common;
using Computer_Store_DataAccess;
using Computer_Store_DataAccess.Data;
using Computer_Store_DataAccess.ViewModel;
using Computer_Store_Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Business.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        public OrderRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<OrderHeaderDTO> CancelOrder(int id)
        {
            var orderHeader = await _db.OrderHeaders.FindAsync(id);
            if (orderHeader == null)
            {
                return new OrderHeaderDTO();
            }
            if(orderHeader.Status == SD.Status_Pending)
            {
                orderHeader.Status = SD.Status_Cancelled;
                await _db.SaveChangesAsync();
            }
            if(orderHeader.Status == SD.Status_Confirmed)
            {
                var options = new Stripe.RefundCreateOptions
                {
                    Reason = Stripe.RefundReasons.RequestedByCustomer,
                    PaymentIntent = orderHeader.paymentIntentId
                };
                var service = new Stripe.RefundService();
                Stripe.Refund refund = service.Create(options);
                orderHeader.Status = SD.Status_Refunded;
                await _db.SaveChangesAsync();
            }
            return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeader);
        }

        public async Task<OrderDTO> Create(OrderDTO objDTO)
        {
            try
            {
                var order = _mapper.Map<OrderDTO, Order>(objDTO);
                _db.OrderHeaders.Add(order.OrderHeader);
                await _db.SaveChangesAsync();
                foreach(var details in order.OrderDetails)
                {
                    details.OrderHeaderId = order.OrderHeader.Id;
                }
                _db.OrderDetails.AddRange(order.OrderDetails);
                await _db.SaveChangesAsync();
                return new OrderDTO()
                {
                    OrderHeader = _mapper.Map<OrderHeader, OrderHeaderDTO>(order.OrderHeader),
                    OrderDetails = _mapper.Map<IEnumerable<OrderDetails>, IEnumerable<OrderDetailsDTO>>(order.OrderDetails).ToList()
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return objDTO;
        }

        public async Task<int> Delete(int id)
        {
            var OrderHeader = await _db.OrderHeaders.FirstOrDefaultAsync(u => u.Id == id);
            if(OrderHeader != null)
            {
                IEnumerable<OrderDetails> orderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == id);
                _db.OrderDetails.RemoveRange(orderDetails);
                _db.OrderHeaders.Remove(OrderHeader);
                return _db.SaveChanges();
            }
            return 0;
        }

        public async Task<OrderDTO> get(int id)
        {
            Order order = new()
            {
                OrderHeader = _db.OrderHeaders.FirstOrDefault(u => u.Id == id),
                OrderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == id)
            };
            if(order != null)
            {
                return _mapper.Map<Order, OrderDTO>(order);
            }
            return new OrderDTO();
        }

        public async Task<IEnumerable<OrderDTO>> getAll(string? userId = null, string? status = null)
        {
            List<Order> OrderFromdb = new List<Order>();
            IEnumerable<OrderHeader> orderHeaderList = _db.OrderHeaders;
            IEnumerable<OrderDetails> orderDetailList = _db.OrderDetails;
            foreach(OrderHeader header in orderHeaderList)
            {
                Order order = new()
                {
                    OrderHeader = header,
                    OrderDetails = _db.OrderDetails.Where(u => u.OrderHeaderId == header.Id)
                };
                OrderFromdb.Add(order);
            }
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDTO>>(OrderFromdb);
        }

        public async Task<OrderHeaderDTO> MarkPaymentSuccessful(int id, string PaymentIntentId)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            if(data == null)
            {
                return new OrderHeaderDTO();
            }
            if(data.Status == SD.Status_Pending)
            {
                data.paymentIntentId = PaymentIntentId;
                data.Status = SD.Status_Confirmed;
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(data);
            }
            return new OrderHeaderDTO();
        }

        public async Task<OrderHeaderDTO> Updateheader(OrderHeaderDTO objDTO)
        {
            if(objDTO!= null)
            {
                var orderHeaderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == objDTO.Id);
                orderHeaderFromDb.Name = objDTO.Name;
                orderHeaderFromDb.PhoneNumber = objDTO.PhoneNumber;
                orderHeaderFromDb.Carrier = objDTO.Carrier;
                orderHeaderFromDb.Tracking = objDTO.Tracking;
                orderHeaderFromDb.AddressType = objDTO.AddressType;
                orderHeaderFromDb.StreetAddress = objDTO.StreetAddress;
                orderHeaderFromDb.City = objDTO.City;
                orderHeaderFromDb.State = objDTO.State;
                orderHeaderFromDb.PostalCode= objDTO.PostalCode;
                orderHeaderFromDb.Status= objDTO.Status;
                
                
                await _db.SaveChangesAsync();
                return _mapper.Map<OrderHeader, OrderHeaderDTO>(orderHeaderFromDb);
            }
            return new OrderHeaderDTO();
        }

        public async Task<bool> UpdateOrderStatus(int id, string status)
        {
            var data = await _db.OrderHeaders.FindAsync(id);
            if (data == null)
            {
                return false;
            }
            data.Status = status;
            if(status == SD.Status_Shipped) { 
                data.ShippingDate = DateTime.Now;
            }
            await _db.SaveChangesAsync();
            return true;
        }
    }
}
