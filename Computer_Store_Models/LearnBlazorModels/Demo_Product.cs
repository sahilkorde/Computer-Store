using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Models.LearnBlazorModels
{
    public class Demo_Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public double Price { get; set; }
        public List<Demo_ProductProp> ProductProps { get; set; }
    }
}
