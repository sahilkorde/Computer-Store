using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Models
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        public bool ShopFavorite { get; set; }
        public bool CostomerFavorite { get; set; }
        public string MainFeature { get; set; }
        public string ImageUrl { get; set; }

        [Range(1, int.MaxValue, ErrorMessage ="Please select Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public CategoryDTO Category { get; set; }
        public ICollection<ProductPriceDTO> ProductPrices { get; set; }
    }
}
