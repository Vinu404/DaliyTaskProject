using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Daily_Test_Management_project.Models
{
    public class UserLogin
    {
        public int id { get; set; }
        [Required(ErrorMessage ="UserID is Required")]
        public string UserID { get; set; }
        [Required(ErrorMessage = "Password is Required")]
        public string Password { get; set; }
        public string Active { get; set; }
       
    }
}