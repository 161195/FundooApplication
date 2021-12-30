using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Entity
{
    public class User
    {
        /// <summary>
        /// defining ID as a primary key to enter into database.
        /// </summary>
        /// <value>
        /// key=Id
        /// </value>
        [Key]                  
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long UserId                  
        {
            get; set;
        }
        //feeding this attributes into UserTable.
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
        [DataType(DataType.DateTime)]
        public DateTime? Createdat { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? Modified { get; set; }
        //Adding ? to make field nullable 
        public ICollection<Note> Note { get; set; }
    }
}
