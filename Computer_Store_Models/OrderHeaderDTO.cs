using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Models
{
    public class OrderHeaderDTO
    {
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        [Required]
        [Display(Name = "Order Total")]
        public double OrderTotal { get; set; }
        [Required]
        public DateTime OrderDate { get; set; }
        [Required]
        [Display(Name ="Shipping Date")]
        public DateTime ShippingDate { get; set; }
        [Required]
        public string Status { get; set; }

        //Stripe payment
        public string? SessionId { get; set; }
        public string? paymentIntentId { get; set; }

        [Required]
        [Display(Name ="Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name ="Phone Number")]
        public string PhoneNumber { get; set; }
        [Required]
        [Display(Name = "Address Type")]
        public string AddressType { get; set; }
        [Required]
        [Display(Name ="Street Address")]
        public string StreetAddress { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public String City { get; set; }
        [Required]
        [Display(Name ="Postal Code")]
        public String PostalCode { get; set; }

        [Required]
        [Display(Name ="Email")]
        public string Email { get; set; }

        public string? Tracking { get; set; }
        public string? Carrier { get; set; }
    }
}
