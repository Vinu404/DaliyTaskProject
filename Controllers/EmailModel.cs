using System.Web;

namespace Daily_Test_Management_project.Controllers
{
    public class EmailModel
    {
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Body { get;  set; }
        

        public HttpPostedFileBase Attach { get; set; }
    }
}