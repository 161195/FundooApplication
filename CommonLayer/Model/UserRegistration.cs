using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class UserRegistration
    {
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email Id is required")]
        [DataType(DataType.Text)]
        [Display(Name = "EmailId")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [StringLength(30, MinimumLength = 6, ErrorMessage = "Password contain six character")]
        public string Password { get; set; }

    }
}
