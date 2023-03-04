using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Models
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Please Enter Name ..")]
        public string Name { get; set; }
    }
}
