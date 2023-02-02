using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAL.ViewModel
{
    public class UserVM
    {
        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username:")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password:")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember password?")]
        public bool RememberMe { get; set; }
    }
}
