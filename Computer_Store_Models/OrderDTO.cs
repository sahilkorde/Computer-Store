using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Models
{
    public class OrderDTO
    {
        public OrderHeaderDTO OrderHeader { get; set; }
        public List<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
