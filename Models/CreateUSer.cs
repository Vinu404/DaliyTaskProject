using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Daily_Test_Management_project.Models
{
    public class CreateUSer
    {
        
        public int ID { get; set; }
        [Required(ErrorMessage = "UserID is required")]
        public string userID { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Access Module is required")]
        public string AccessModule { get; set; }
        [Required(ErrorMessage = "Active is required")]
        public bool Active { get; set; }
        [Required(ErrorMessage = "CreateBy is required")]
        public string CreatedBy { get; set; }

        [Required(ErrorMessage = "Created Date  is required")]
        public DateTime CreatedDate { get; set; }

        [Required(ErrorMessage = "Developer Name  is required")]
        public int developerID { get; set; }
        [Required(ErrorMessage = "Role   is required")]
        public int RoleID { get; set; }
    }
}