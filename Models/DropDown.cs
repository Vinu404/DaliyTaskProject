using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Daily_Test_Management_project.Models
{
    public class TechStach
    {
        public int TechstackID { get; set; }
        public string TechStack { get; set; }
    }

    public class Developer
    {
        public int DeveloperID { get; set; }
        public string DeveloperName { get; set; }
    }
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
    }
    public class Module
    {
        public int Module_ID { get; set; }
        public string Module_Name { get; set; }
    }
    public class status
    {
        public int StatusID { get; set; }
        public string Status { get; set; }
    }

    public class DeveloperTechstack
    {
        public int DeveloperID { get; set; }
        public string DeveloperName { get; set; }
        public int TechstackID { get; set; }
        public string TechStack { get; set; }
    }

}