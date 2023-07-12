using Daily_Test_Management_project.Models;
using Daily_Test_Management_project.Models.DBL_logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Threading;
using System.Drawing;
using Daily_Test_Management_project.Controllers;
using System.Net.Configuration;
using System.Net.Mail;
using System.Net;
using System.Configuration;
using System.IO;

namespace Daily_Test_Management_project.Controllers
{
    public class UserController : Controller
    {
        private logic _lg = new logic();
        // GET: User
        public ActionResult UserLogin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ValidateLogin(UserLogin userLogin)
        {
            string msg;
            int data = _lg.Login(userLogin);
            string Url = ConfigurationManager.AppSettings["UserLogin"].ToString();
            if (data == 0)
            {
                msg = "Wrong username and password!..";
                
                return Json(new { status = "0", Message=msg, url= Url},JsonRequestBehavior.AllowGet);
            }
            else
            {
                FormsAuthentication.SetAuthCookie(userLogin.UserID, false);
                Session["id"] = data;
                Session["DTM_UserID"] = userLogin.UserID;
                string url = ConfigurationManager.AppSettings["Login"].ToString();
                return Json(new
                {
                    status = "1",
                    Message = "Login Successfully..",
                    url = url
                }, JsonRequestBehavior.AllowGet) ;
            }

           

            

        }
        HomeController home = new HomeController();

       [Authorize]
        public ActionResult CreateUSer()
        {
            
            return View();
        }

       

        [HttpPost]
        public ActionResult CreateUSer(CreateUSer createUSer,Developer d)
        {
           
            if (ModelState.IsValid)
            {
                string msg = _lg.CreateUSer(createUSer, d);

                if (msg != "")
                {
                    TempData["Usermsg"] = msg;
                    return View();
                }
            }
                   
            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("UserLogin");
        }


        [HttpPost]
        public JsonResult UserExist(string userID)
          {
            System.Threading.Thread.Sleep(200);
            int a = _lg.userExist(userID);
            
            return Json(a, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult StausFilter(string status)
        {
            var list = _lg.StausFilter(status);
            return Json(list,JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CreateDateStausFilter(string status,DateTime fromdate ,DateTime Todate,string stackID)
        {
            var list = _lg.CreatedStausFilter(status,fromdate,Todate,stackID);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Email()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendEmail(EmailModel email)
        {
            try
            {
                //Read SMTP section from Web.Config.
                SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");

                using (MailMessage mm = new MailMessage(smtpSection.From, email.Email))
                {
                    mm.Subject = email.Subject;
                    mm.Body = email.Body;

                    mm.IsBodyHtml = true;

                    //if (email.Attach.ContentLength > 0)
                    //{
                    //    string filename = Path.GetFileName(email.Attach.FileName);
                    //    mm.Attachments.Add(new Attachment(email.Attach.InputStream, filename));
                    //}

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = smtpSection.Network.Host;
                        smtp.EnableSsl = smtpSection.Network.EnableSsl;
                        NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCred;
                        smtp.Port = smtpSection.Network.Port;
                        
                        smtp.Send(mm);
                        return Json(new { status = 1, message = "Email sent" });
                    }
                   
                }
            }
            catch (Exception ex)
            {
                return Json(new { status = 0, message = "Failed.." });
            }
            
            
        }


        [HttpPost]
        public ActionResult Email(EmailModel email)
        {
            MailMessage mail = new MailMessage();
            mail.To.Add(email.Email);
            mail.From = new MailAddress("2001vinayvemul@gmail.com");
            mail.Subject = email.Subject;
            mail.Body = email.Body;
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential("2001vinayvemul@gmail.com", "ajjebawydwsqcuhm");
            smtp.Send(mail);

            return Json(new { status = 1, msg = "email sent" });

        }
    }
}