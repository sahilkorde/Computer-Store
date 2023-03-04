using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Computer_Store_Models
{
    public class SignInRequestDTO
    {
        [Required(ErrorMessage ="Username is required")]
        [RegularExpression("^[a-zA-Z0-9_.-]+@[a-zA-Z0-9-]+.[a-zA-Z0-9-.]+$", ErrorMessage = "Invalid Email Address")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
