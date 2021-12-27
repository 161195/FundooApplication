using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class deleteOperation
    {    
        //checking this attributes into UserTable.
        [Required(ErrorMessage = "First Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [DataType(DataType.Text)]
        [Display(Name = "LastName")]
        public string LastName { get; set; }      
    }
}
