using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Models
{
    public class StripePaymentDTO
    {
        public StripePaymentDTO() 
        {
            SuccessURL = "OrderConfirmation";
            CancelURL = "Summary";
        }

        public OrderDTO Order { get; set; }
        public string SuccessURL { get; set; }
        public string CancelURL { get; set; }
    }
}
