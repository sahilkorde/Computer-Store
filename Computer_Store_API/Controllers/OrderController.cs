using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Computer_Store_Business.Repository;
using Computer_Store_Business.Repository.IRepository;
using Computer_Store_Models;
using Stripe.Checkout;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Computer_Store_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public readonly IOrderRepository _orderRepository;
        public readonly IEmailSender _emailSender;
        public OrderController(IOrderRepository orderRepository, IEmailSender email)
        {
            _orderRepository = orderRepository;
            _emailSender = email;
        }
        [HttpGet]
        public async Task<IActionResult> getAll()
        {
            return Ok(await _orderRepository.getAll());
        }

        [HttpGet("{OrderHeaderId}")]
        public async Task<IActionResult> get(int? OrderheaderId)
        {
            if(OrderheaderId == null || OrderheaderId == 0)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode= StatusCodes.Status400BadRequest
                });
            }
            var orderHeader = await _orderRepository.get(OrderheaderId.Value);
            if(orderHeader == null)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = "Invalid Id",
                    StatusCode = StatusCodes.Status404NotFound
                });
            }
            return Ok(orderHeader);
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> create([FromBody] StripePaymentDTO paymentDTO)
        {
            paymentDTO.Order.OrderHeader.OrderDate= DateTime.Now;
            var result = await _orderRepository.Create(paymentDTO.Order);
            return Ok(result);
        }

        [HttpPost]
        [ActionName("paymentsuccessful")]
        public async Task<IActionResult> PaymentSuccessful([FromBody] OrderHeaderDTO orderHeaderDTO)
        {
            var service = new SessionService();
            var sessionDetails = service.Get(orderHeaderDTO.SessionId);
            if (sessionDetails.PaymentStatus == "paid")
            {
                var result = await _orderRepository.MarkPaymentSuccessful(orderHeaderDTO.Id, sessionDetails.PaymentIntentId);
                await _emailSender.SendEmailAsync(orderHeaderDTO.Email, "Computer order Confirmed", "New order has been created : " + orderHeaderDTO.Id);
                if (result == null)
                {
                    return BadRequest(new ErrorModelDTO()
                    {
                        ErrorMessage = "Can not mark payment as successful"
                    });
                }
                return Ok(result);
            }

            return BadRequest();
        }
    }
}
