using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Daily_Test_Management_project.Models
{
    public class Reportlist
    {
        public int ID { get; set; }
       
        public string TechstackID { get; set; }
        
        public string StatusID { get; set; }
       
        public string ModuleID { get; set; }
       
        public string DeveloperID { get; set; }
        
        public string DepartmentID { get; set; }
        
        public string JiraID { get; set; }
        

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        public DateTime cretatedDate { get; set; }

        public string Remark { get; set; }
    }
}