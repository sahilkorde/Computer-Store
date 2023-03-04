using Computer_Store_Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;

namespace Computer_Store_API.Controllers
{
    [Route("api/[controller]/[action]")]
    public class StripePaymentController : Controller
    {
        private readonly IConfiguration _configuration;
        public StripePaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        [HttpPost]
        [Authorize]
        [ActionName("Create")]
        public async Task<IActionResult> create([FromBody] StripePaymentDTO paymentDTO)
        {
            try { 
                var domain = _configuration.GetValue<string>("Computer_store_URL");

                var options = new SessionCreateOptions
                {
                    SuccessUrl = domain+paymentDTO.SuccessURL,
                    CancelUrl= domain+paymentDTO.CancelURL,
                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    PaymentMethodTypes = new List<string> { "card" }
                };
                foreach(var item in paymentDTO.Order.OrderDetails)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "inr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.Product.Name
                            }
                        },
                        Quantity = item.Count
                    };
                    options.LineItems.Add(sessionLineItem);
                }
                var service = new SessionService();
                Session session =  service.Create(options);
                return Ok(new SuccessModelDTO()
                {
                    Data = session.Id
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModelDTO()
                {
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
