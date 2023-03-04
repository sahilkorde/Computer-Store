using Computer_Store_Models;
using System.ComponentModel.DataAnnotations;

namespace Computer_Store_Client.ViewModel
{
    public class DetailsVM
    {
        public DetailsVM() {
            ProductPrice= new();
            Count = 1;
        }
        [Range(1, int.MaxValue, ErrorMessage ="Please Enter a value greater than 0")]
        public int Count { get; set; }
        [Required]
        public int SelectedProductId { get; set; }
        public ProductPriceDTO ProductPrice { get; set; }
    }
}
