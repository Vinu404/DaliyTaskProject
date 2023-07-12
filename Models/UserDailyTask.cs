using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Daily_Test_Management_project.Models
{
    public class UserDailyTask
    {
        public int ID{ get; set; }
        [Display(Name ="TECh STACK")]
        [Required(ErrorMessage ="TECH STACK IS REQUIRED")]
        public int TechstackID{ get; set; }
        [Display(Name = "STATUS")]
        [Required(ErrorMessage = "STATUS IS REQUIRED")]
        public int StatusID{ get; set; }
        [Display(Name = "MODULE")]
        [Required(ErrorMessage = "MODULE IS REQUIRED")]
        public int ModuleID{ get; set; }
        [Display(Name = "DEVELOPER")]
        [Required(ErrorMessage = "DEVELOPER NAME IS REQUIRED")]
        public int DeveloperID{ get; set; }
        [Display(Name = "DEPARTMENT")]
        [Required(ErrorMessage = "DEPARTMENT NAME IS REQUIRED")]
        public int DepartmentID{ get; set; }
        [Display(Name = "JIRAID")]
        [Required(ErrorMessage = "JIRAID IS REQUIRED")]
        public string JiraID{ get; set; }


        [Display(Name = "DESCRIPTION")]
        [Required(ErrorMessage = "DESCRIPTION NAME IS REQUIRED")]
        public string Description{ get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [Display(Name = "START DATE")]
        [Required(ErrorMessage = "STARTDATE IS REQUIRED")]
        public DateTime StartDate{ get; set; }

        //[DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]


        [Display(Name = "ENDDATE")]
        [Required(ErrorMessage = "ENDTDATE IS REQUIRED")]
        public DateTime EndDate{ get; set; }

        [Display(Name = "REMARK")]
        [Required(ErrorMessage = "REMARK IS REQUIRED")]
        public string Remark{ get; set; }
        public string Flag{ get; set; }

        
    }
}