using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLayer.Model
{
    public class ChangePassword
    {
        //public long UserId
        //{
        //    get; set;
        //}
        public string EmailId { get; set; }
        public string Password { get; set; }
        public string NewPassWord { get; set; }
        public string ConfirmPassWord { get; set; }
    }
}
