using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class LoginResponse
    {
        public long Id
        {
            get; set;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public DateTime? Createdat { get; set; }
        public DateTime? Modified { get; set; }
        public string token { get; set; }
       
    }
}
