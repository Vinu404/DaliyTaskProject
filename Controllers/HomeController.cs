using Daily_Test_Management_project.Models;
using Daily_Test_Management_project.Models.DBL_logic;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Mvc;
using PagedList;
using System.Drawing;
using DocumentFormat.OpenXml.Spreadsheet;
using OfficeOpenXml.Table;
using OfficeOpenXml.Style;
using Syncfusion.JavaScript;
using System.Configuration;

namespace Daily_Test_Management_project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private logic _lg = new logic();


        List<TechStach> teches = new List<TechStach>();
        List<Developer> developer = new List<Developer>();
        List<Department> deprt = new List<Department>();
        List<Module> _module = new List<Module>();
        List<status> _status = new List<status>();
        

        public ActionResult Index()
        {
            return View();
        }





        public ActionResult ADDTASk()
        {
            List();
            return View();
        }

        [HttpPost]
        public ActionResult AddDaliTask(UserDailyTask _dailyTask)
        { 
            List();
            
            bool msg;
            try
            {
                if (Session["id"] != null)
                {
                    msg = _lg.AddTask(_dailyTask, (int)Session["id"]);

                    if (msg)
                    {
                        
                        return Json(new
                        {
                            Success = "1",
                            message = "Task created Successfully",
                            url = ConfigurationManager.AppSettings["ReportsAdmin"].ToString()
                        }, JsonRequestBehavior.AllowGet);

                    }
                    else
                    {

                        return Json(new
                        {
                            Success = "0",
                            message = "FAILED..!Create Another Task",
                            url = ConfigurationManager.AppSettings["AddTask"].ToString()
                        }, JsonRequestBehavior.AllowGet);

                    }
                }
                else
                {
                    return Json(new { Success = "", message = "SessionID Expired", url = ConfigurationManager.AppSettings["Logout"].ToString() }, JsonRequestBehavior.AllowGet);

                }


            }
            catch (Exception ex)
            {
                MassageException.Errorlog((string)Session["id"], this.ControllerContext, ex);
                return Json(new { Success = "-1", message = "Something Went at runtime", /*url = "/User/Login"*/ }, JsonRequestBehavior.AllowGet);
            }


        }


        public ActionResult Modification(int? page, int? pageSize)
        {
            List();
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            int defaSize = (pageSize ?? 10);
            ViewBag.psize = defaSize;
            ViewBag.PageSize = new List<SelectListItem>()
    {
                new SelectListItem() { Value="10", Text= "10" },



     };
            var _rplist = _lg.ReportlistAdmin().OrderByDescending(x => x.ID).ToPagedList(pageIndex, defaSize);
            return View(_rplist);
        }

        [HttpGet]
        public JsonResult DataAdminModfication()
        {
            var _rplist = _lg.ReportlistAdmin();
            return Json(_rplist, JsonRequestBehavior.AllowGet);
        }

        public ActionResult update(int? id)
        {
            try
            {
                UserDailyTask userDailyTask = new UserDailyTask();
                var _list = _lg.Reportlistid((int)id);
                List();
                foreach (var item in _list)
                {
                    //Session["UID"] = item.ID;

                    userDailyTask.ID = item.ID;
                    userDailyTask.TechstackID = item.TechstackID;
                    userDailyTask.JiraID = item.JiraID;
                    //userDailyTask.DeveloperID = item.DeveloperID;
                    userDailyTask.DepartmentID = item.DepartmentID;
                    userDailyTask.ModuleID = item.ModuleID;
                    userDailyTask.Description = item.Description;
                    userDailyTask.StartDate = item.StartDate;
                    userDailyTask.EndDate = item.EndDate;
                    userDailyTask.StatusID = item.StatusID;
                    userDailyTask.Remark = item.Remark;

                }
                return View(userDailyTask);
            }
            catch(Exception)
            {
                return RedirectToAction("Index");
            }



        }
        
        [HttpPost]
        public ActionResult update(UserDailyTask task)
        {

            bool msg;
            try
            {
                if (Session["id"] != null)
                {
                    msg = _lg.uppdateTask(task, (int)Session["id"]);

                    if (msg)
                    {
                        ModelState.Clear();
                        return Json(new
                        {
                            Success = "1",
                            message = "Task Update Successfully",
                            url = ConfigurationManager.AppSettings["modification"].ToString()
                        }, JsonRequestBehavior.AllowGet) ;

                    }
                    else
                    {

                        return Json(new
                        {
                            Success = "0",
                            message = "FAILED..!Update Another Task",
                            url = ConfigurationManager.AppSettings["AddTask"].ToString()
                        }, JsonRequestBehavior.AllowGet) ;

                    }
                }
                else
                {
                    return Json(new { Success = "", message = "SessionID Expired", url = ConfigurationManager.AppSettings["Logout"].ToString() }, JsonRequestBehavior.AllowGet);

                }


            }
            catch (Exception ex)
            {
                MassageException.Errorlog((string)Session["id"], this.ControllerContext, ex);
                return Json(new { Success = "-1", message = "Something Went at runtime", /*url = "/User/Login"*/ }, JsonRequestBehavior.AllowGet);
            }


        }

        


        public ActionResult ReportsAdmin(int? page, int? pageSize)
        {
            DataSet ds = _lg.DropDownTechstack();
            List<status> list = new List<status>();
            List<TechStach> stack = new List<TechStach>();
            foreach (DataRow row in ds.Tables[4].Rows)
            {
                list.Add(new status()
                {
                    StatusID = (int)row[0],
                    Status = row[1].ToString()
                });
            }
            foreach (DataRow row in ds.Tables[5].Rows)
            {
                stack.Add(new TechStach()
                {
                    TechstackID = (int)row[0],
                    TechStack = row[1].ToString()
                });
            }
            SelectList list1 = new SelectList(list, "Status", "Status");
            ViewBag.statuslist = list1;

            SelectList techstack = new SelectList(stack, "TechStack", "TechStack");
            ViewBag.TechStack = techstack;
            //int pageSize = 3;
            //int pageNumber = (page ?? 1);

            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            //Ddefault size is 5 otherwise take pageSize value  
            int defaSize = (pageSize ?? 10);

            ViewBag.psize = defaSize;

            //Dropdownlist code for PageSize selection  
            //In View Attach this  
            ViewBag.PageSize = new List<SelectListItem>()
    {

        new SelectListItem() { Value="10", Text= "10" },
        new SelectListItem() { Value="5", Text= "5" },

     };
            IPagedList<Reportlist> _rplist = _lg.ReportlistAdmin().OrderByDescending(x => x.ID).ToPagedList(pageIndex, defaSize);


            //Create a instance of our DataContext  

            return View(_rplist);
        }








        [HttpPost]
        public ActionResult RecordsFilter(DateTime createdDate, DateTime Enddate)
        {

            List<Reportlist> list = _lg.ReordsFilter(createdDate, Enddate);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RecordsFilterDeveloper(DateTime createdDate)
        {
            if (Session["id"] != null)
            {
                List<Reportlist> list = _lg.ReordsFilterDeveloper(createdDate, (int)Session["id"]);
                return Json(list, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Logout", "UserLogin");
            }
        }



        [HttpPost]
        public ActionResult SerchDeveloper(string Developername)
        {
            List<Reportlist> list = _lg.SerachByDeveloper(Developername);
            return Json(list, JsonRequestBehavior.AllowGet);
        }




        public void List()
        {
            DataSet ds = _lg.DropDownTechstack();
            foreach (DataRow row in ds.Tables[0].Rows)
            {
                teches.Add(new TechStach()
                {
                    TechstackID = (int)row[0],
                    TechStack = row[1].ToString()
                });
            }


            foreach (DataRow row in ds.Tables[1].Rows)
            {
                deprt.Add(new Department()
                {
                    DepartmentID = (int)row[0],
                    DepartmentName = row[1].ToString()
                });
            }
            foreach (DataRow row in ds.Tables[2].Rows)
            {
                _module.Add(new Module()
                {
                    Module_ID = (int)row[0],
                    Module_Name = row[1].ToString()
                });
            }
            foreach (DataRow row in ds.Tables[3].Rows)
            {
                _status.Add(new status()
                {
                    StatusID = (int)row[0],
                    Status = row[1].ToString()
                });
            }
            Session["Techstack"] = teches;
            //Session["developer"] = developer;
            Session["Deptment"] = deprt;
            Session["module"] = _module;
            Session["status"] = _status;
        }


        public void ExportToExcel()
        {
            List<Reportlist> _rplist = _lg.Reportlist();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage pck = new ExcelPackage();

            ExcelWorksheet ws = pck.Workbook.Worksheets.Add("Reports");


            ws.Cells["A1"].Value = "Communication";
            ws.Cells["B1"].Value = "Col1";

            ws.Cells["A2"].Value = "reports";
            ws.Cells["B2"].Value = "report1";

            ws.Cells["A3"].Value = "Date";
            ws.Cells["B3"].Value = string.Format("{0:dd MMMM yyyy} at {0:H: mm tt}", DateTimeOffset.Now);

            ws.Cells["A6"].Value = "TechStack";
            ws.Cells["B6"].Value = "JIRA ID";
            ws.Cells["C6"].Value = "DeveloperName";
            ws.Cells["D6"].Value = "DepartmentName";
            ws.Cells["E6"].Value = "Module";
            ws.Cells["F6"].Value = "Description";
            ws.Cells["G6"].Value = "Startdate";
            ws.Cells["H6"].Value = "EndDate";
            ws.Cells["I6"].Value = "Status";
            ws.Cells["j6"].Value = "Remark";


            int rowStart = 7;
            //ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.DarkUp;
            //ws.Row(rowStart).Style.Fill.BackgroundColor.SetColor(ColorTranslator.FromHtml(string.Format("red")));
            foreach (var item in _rplist)
            {
                ws.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                ws.Cells[string.Format("A{0}", rowStart)].Value = item.TechstackID;
                ws.Cells[string.Format("B{0}", rowStart)].Value = item.JiraID;
                ws.Cells[string.Format("C{0}", rowStart)].Value = item.DeveloperID;
                ws.Cells[string.Format("D{0}", rowStart)].Value = item.DepartmentID;
                ws.Cells[string.Format("E{0}", rowStart)].Value = item.ModuleID;
                ws.Cells[string.Format("F{0}", rowStart)].Value = item.Description;
                ws.Cells[string.Format("G{0}", rowStart)].Value = item.StartDate;
                ws.Cells[string.Format("H{0}", rowStart)].Value = item.EndDate;
                ws.Cells[string.Format("I{0}", rowStart)].Value = item.StatusID;
                ws.Cells[string.Format("J{0}", rowStart)].Value = item.Remark;
                rowStart++;
            }
            ws.Cells["A:K"].AutoFitColumns();
            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string attach = "attachment;filename=Reports.xlsx";

            Response.AddHeader("content-disposition", attach);

            Response.BinaryWrite(pck.GetAsByteArray());
            Response.End();

        }

        public void ExcelSheet()
        {
            List<Reportlist> _rplist = _lg.Reportlist();
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            ExcelPackage excel = new ExcelPackage();
            ExcelWorksheet excelWorksheet = excel.Workbook.Worksheets.Add(DateTime.Now.ToString("MMMM yyyy"));
            

            System.Drawing.Color brown = ColorTranslator.FromHtml("#974521");
            System.Drawing.Color skyn = ColorTranslator.FromHtml("#ffecb3");
            System.Drawing.Color green = ColorTranslator.FromHtml("#b3ffb3");
            excelWorksheet.View.ShowGridLines = false;
            //excelWorksheet.Column(9).Width = 10;
            //excelWorksheet.Cells[excelWorksheet.Dimension.Address].AutoFitColumns();
            excelWorksheet.Columns[7,11].Width = 20.3;
            excelWorksheet.Columns[4,6].Width = 17.3;
            
            excelWorksheet.Row(2).Height = 30;

            var date = DateTime.Now.ToString("MMMM yy");
             //excelWorksheet.Column(9).Width = 23.3;
            using (ExcelRange rng1 = excelWorksheet.Cells[2, 4, 2, 11])
            {
                rng1.Value = "MONTHLY RESOURCE UTILITY DASHBOARD " +"("+ date +")" ;
                rng1.Style.Font.Size = 22;
                //rng1.Style.Font.Bold = true;
                rng1.Style.Font.Name = "Algerian";


                rng1.Merge = true;
                rng1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                rng1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                rng1.Style.Fill.BackgroundColor.SetColor(brown);
                rng1.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                rng1.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                rng1.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                rng1.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                rng1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                rng1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;               
                rng1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                rng1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }

            //using (ExcelRange rng = excelWorksheet.Cells[3, 3, 3, 3])
            //{
            //    rng.Value = "MONTHLY RESOURCE UTILITY DASHBOARD ( Apr 23 )";
            //    rng.Style.Font.Size = 20;
            //    rng.Style.Font.Bold = true;
            //    rng.Style.Font.Italic = true;
            //    rng.Merge = true;
            //    rng.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            //    rng.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            //    rng.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            //    rng.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
            //}
            //using(ExcelRange range = excelWorksheet.Cells["D4:K8"])
            //{
            //    ExcelTable excelTable = excelWorksheet.Tables.Add(range, "Reportstable");
            //    ExcelTableCollection tblcollection = excelWorksheet.Tables;
               

            //    excelTable.Columns[0].Name = "Technology";
            //    excelTable.Columns[1].Name = "Team Strength";
            //    excelTable.Columns[2].Name = "Projected Man hours";
            //    excelTable.Columns[3].Name = "Leave hours";
            //    excelTable.Columns[4].Name = "Available Man hours";
            //    excelTable.Columns[5].Name = "Utilization";
            //    excelTable.Columns[6].Name = "Pending to Utilize";
            //}
            
            using(ExcelRange ra = excelWorksheet.Cells["D4:E4"])
            {
                ra.Merge = true;
                excelWorksheet.Row(4).Height = 30;
                ra.Style.Font.Size = 11;
                ra.Value = "Technology";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["F4:F4"])
            {
                ra.Merge = true;
                excelWorksheet.Row(4).Height = 30;
                ra.Style.Font.Size = 11;
                ra.Value = "Team Strength";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["G4:G4"])
            {
                ra.Merge = true;
                excelWorksheet.Row(4).Height = 30;
                ra.Style.Font.Size = 11;
                ra.Value = "Projected Man hours";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["H4:H4"])
            {
                ra.Merge = true;
                excelWorksheet.Row(4).Height = 30;
                ra.Style.Font.Size = 11;
                ra.Value = "Leave hours";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["I4:I4"])
            {
                ra.Merge = true;
                excelWorksheet.Row(4).Height = 30;
                ra.Value = "Available Man hours";
                ra.Style.Font.Bold = true;
                ra.Style.Font.Size = 11;
            }

            using (ExcelRange ra = excelWorksheet.Cells["J4:J4"])
            {
                ra.Merge = true;
                excelWorksheet.Row(4).Height = 30;
                ra.Value = "Utilization";
                ra.Style.Font.Bold = true;
                ra.Style.Font.Size = 11;
            }
            using (ExcelRange ra = excelWorksheet.Cells["K4:K4"])
            {
                ra.Merge = true;
                excelWorksheet.Row(4).Height = 30;
                ra.Value = "Pending to Utilize";
                ra.Style.Font.Bold = true;
                ra.Style.Font.Size = 11;
            }

            using (ExcelRange range1 = excelWorksheet.Cells["D4:k8"])
            {
                range1.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                range1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                range1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
            using (ExcelRange range1 = excelWorksheet.Cells["D4:k4"])
            {
                range1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range1.Style.Fill.BackgroundColor.SetColor(skyn);
            }

            
            using (ExcelRange range4 = excelWorksheet.Cells[5, 4, 7, 4])
            {
                range4.Merge = true;
                range4.Value = "Tack Stack";

            }
            using (ExcelRange rng = excelWorksheet.Cells["E5:E8"])
            {
                var vals = new string[] { "Tech Support + SQL", "Full Stack", "BA" };
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }
            using (ExcelRange rng = excelWorksheet.Cells["F5:F8"])
            {
                var vals = new int[] {2,2,1 };
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            using (ExcelRange rng = excelWorksheet.Cells["G5:G5"])
            {
                excelWorksheet.Cells["G5"].Formula = "($Logic.E3)";
                excelWorksheet.Cells["G6"].Formula = "($Logic.E4)";
                excelWorksheet.Cells["G7"].Formula = "($Logic.E5)";
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            using (ExcelRange rng = excelWorksheet.Cells["H5:H8"])
            {
                var vals = new int[] {0,24,0};
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            using (ExcelRange rng = excelWorksheet.Cells["I5:I7"])
            {
                excelWorksheet.Cells["I5"].Formula = "(G5-H5)";
                excelWorksheet.Cells["I6"].Formula = "(G6-H6)";
                excelWorksheet.Cells["I7"].Formula = "(G7-H7)";
               
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            using (ExcelRange rng = excelWorksheet.Cells["J5:J8"])
            {
                var vals = new int[] {244,228,125};
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            using (ExcelRange rng = excelWorksheet.Cells["K5:K7"])
            {
                excelWorksheet.Cells["K5"].Formula = "(I5-J5)";
                excelWorksheet.Cells["K6"].Formula = "(I6-J6)";
                excelWorksheet.Cells["K7"].Formula = "(I7-J7)";

                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(green);
            }
            using (ExcelRange rng = excelWorksheet.Cells["D8:K8"])
            {
                rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(skyn);
                rng.Style.Font.Bold = true;
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }

            using (ExcelRange ra = excelWorksheet.Cells["D8:D8"])
            {
               
                
                ra.Style.Font.Size = 11;
                ra.Value = "Total(In Hour)";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["E8:E8"])
            {
               
                ra.Style.Font.Size = 11;
                ra.Value = "";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["F8:F8"])
            {
                
                ra.Style.Font.Size = 11;
                excelWorksheet.Cells["F8"].Formula = "SUM(F5:F7)";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["G8:G8"])
            {
                
                ra.Style.Font.Size = 11;
                excelWorksheet.Cells["G8"].Formula = "SUM(G5:G7)";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["H8:H8"])
            {
               
                ra.Style.Font.Size = 10;
                excelWorksheet.Cells["H8"].Formula = "SUM(H5:H7)";
                ra.Style.Font.Bold = true;
            }
            using (ExcelRange ra = excelWorksheet.Cells["I8:I8"])
            {

                excelWorksheet.Cells["I8"].Formula = "SUM(I5:I7)";
                ra.Style.Font.Bold = true;
                ra.Style.Font.Size = 11;
            }

            using (ExcelRange ra = excelWorksheet.Cells["J8:J8"])
            {
             
                ra.Value = 597;
                ra.Style.Font.Bold = true;
                ra.Style.Font.Size = 11;
            }
            using (ExcelRange ra = excelWorksheet.Cells["K8:K8"])
            {
                
                ra.Value = 39;
                ra.Style.Font.Bold = true;
                ra.Style.Font.Size = 11;
            }


            

            using (ExcelRange range1 = excelWorksheet.Cells["B12:L12"])
            {
                range1.Style.Font.Bold = true;
                range1.Style.Font.Size = 11;
                range1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range1.Style.Fill.BackgroundColor.SetColor(skyn);
                
            }
            excelWorksheet.Cells["B12"].Value = "TechStack";
            excelWorksheet.Cells["C12"].Value = "JIRA ID";
            excelWorksheet.Cells["D12"].Value = "DeveloperName";
            excelWorksheet.Cells["E12"].Value = "DepartmentName";
            excelWorksheet.Cells["F12"].Value = "Module";
            excelWorksheet.Cells["G12"].Value = "Description";
            excelWorksheet.Cells["H12"].Value = "Effort ( In hours)";
            excelWorksheet.Cells["I12"].Value = "Startdate";
            excelWorksheet.Cells["J12"].Value = "EndDate";
            excelWorksheet.Cells["k12"].Value = "Status";
            excelWorksheet.Cells["L12"].Value = "Remark";

            using (ExcelRange range1 = excelWorksheet.Cells["B12:L12"])
            {


                range1.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                range1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                range1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                range1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            }

            int rowStart = 13;
            excelWorksheet.Columns[8, 11].Width = 27.3;
            excelWorksheet.Columns[2, 6].Width = 19.3;
            excelWorksheet.Columns[12].Width = 26.3;
            excelWorksheet.Columns[7].Width = 40.5;

           

            foreach (var item in _rplist)
            {
                //excelWorksheet.Cells[string.Format("I5")].Formula = "SUMIF(B{0}):B{0}"+item.TechstackID+")";
                var totaldyas = (item.EndDate.Date - item.StartDate.Date).Days;
                
                using (ExcelRange range1 = excelWorksheet.Cells[string.Format("B{0}:L{0}", rowStart)])
                {


                    range1.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    range1.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    range1.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    range1.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    range1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                    range1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                    range1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    range1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

                }
                excelWorksheet.Cells[string.Format("I{0}",rowStart)].Style.Numberformat.Format = "dd-MM-yyyy";
                excelWorksheet.Cells[string.Format("J{0}",rowStart)].Style.Numberformat.Format = "dd-MM-yyyy";
                
                //excelWorksheet.Row(rowStart).Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;

                excelWorksheet.Cells[string.Format("B{0}", rowStart)].Value = item.TechstackID;
               excelWorksheet.Cells[string.Format("C{0}", rowStart)].Value = item.JiraID;
               excelWorksheet.Cells[string.Format("D{0}", rowStart)].Value = item.DeveloperID;
               excelWorksheet.Cells[string.Format("E{0}", rowStart)].Value = item.DepartmentID;
               excelWorksheet.Cells[string.Format("F{0}", rowStart)].Value = item.ModuleID;
               excelWorksheet.Cells[string.Format("G{0}", rowStart)].Value = item.Description;
               excelWorksheet.Cells[string.Format("H{0}", rowStart)].Formula = totaldyas+"*6";
               excelWorksheet.Cells[string.Format("I{0}", rowStart)].Value = item.StartDate;
               excelWorksheet.Cells[string.Format("J{0}", rowStart)].Value = item.EndDate;
               excelWorksheet.Cells[string.Format("K{0}", rowStart)].Value = item.StatusID;
               excelWorksheet.Cells[string.Format("L{0}", rowStart)].Value = item.Remark;
               
               
               
                rowStart++;
            }

            excelWorksheet.Cells["B12:L12"].AutoFilter = true;


            excelWorksheet.Row(10).Height = 26;
            using (ExcelRange rng1 = excelWorksheet.Cells[10, 2, 10, 13])
            {
                rng1.Value = "Technology wise / Developer wise project timelines";
                rng1.Style.Font.Size = 18;
                //rng1.Style.Font.Bold = true;
                rng1.Style.Font.Name = "Algerian";

                rng1.Merge = true;
                rng1.Style.Font.Color.SetColor(System.Drawing.Color.White);
                rng1.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                rng1.Style.Fill.BackgroundColor.SetColor(brown);
                rng1.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                rng1.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                rng1.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;
                rng1.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Medium;

                rng1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                rng1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                rng1.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                rng1.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
            }
            excelWorksheet.Protection.IsProtected = false;
            excelWorksheet.Protection.AllowSelectLockedCells = false;

            //Excel Logic Sheet start
            ExcelWorksheet excelWorksheet2 = excel.Workbook.Worksheets.Add("Logic");

            excelWorksheet2.Row(2).Height = 100;
            excelWorksheet2.Columns[3,8].Width = 20.3;
            excelWorksheet2.Columns[2].Width = 15.3;

            using (ExcelRange rang = excelWorksheet2.Cells["B2:H5"])
            {
                
                rang.Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                rang.Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                rang.Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                rang.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
               

            }
            using (ExcelRange rng = excelWorksheet2.Cells["B2:H2"])
            {

                rng.Style.Font.Bold = true;
                rng.Style.Font.Size = 11;
                rng.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                rng.Style.Fill.BackgroundColor.SetColor(skyn);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                rng.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Top;
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                rng.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                
                excelWorksheet2.Cells["D2"].Value = "Team Strength";
                excelWorksheet2.Cells["E2"].Value = "Available Man hours(Monthly)Available Man hours(Monthly)";
                excelWorksheet2.Cells["F2"].Value = "No. of working days(Monthly)No.of working days(Monthly)";
                excelWorksheet2.Cells["G12"].Value = "No. of working hours(Daily)No.of working hours(Daily)";
                excelWorksheet2.Cells["H2"].Value = "Leaves";
                
               
                //var vals = new object[] {"","", "Team Strength", "Available Man hours(Monthly)Available Man hours(Monthly)", "No. of working days(Monthly)No. of working days(Monthly)","No. of working hours(Daily)No. of working hours(Daily)", "Leaves" };
                //rng.LoadFromCollection(vals);
                //rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
            }
            using (ExcelRange range4 = excelWorksheet2.Cells["B3:B5"])
            {
                range4.Merge = true;
                range4.Value = "Tack Stack";
                range4.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                range4.Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;

            }
            using (ExcelRange rng = excelWorksheet2.Cells["C3:C5"])
            {
                var vals = new object[] { "Tech Support & SQL", "Full Stack", "BA" };
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
            }
            using (ExcelRange rng = excelWorksheet2.Cells["D3:D5"])
            {
                var vals = new int[] {2,2,1 };
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                
            }
            using (ExcelRange rng = excelWorksheet2.Cells["E3:E5"])
            {
                excelWorksheet2.Cells["E3"].Formula = "(F3*G3*D3)";
                excelWorksheet2.Cells["E4"].Formula = "(F4*G4*D4)";
                excelWorksheet2.Cells["E5"].Formula = "(F5*G5*D5)";

                
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                
            }
            using (ExcelRange rng = excelWorksheet2.Cells["F3:F5"])
            {
                var vals = new int[] {22,22,22};
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                
            }
            using (ExcelRange rng = excelWorksheet2.Cells["G3:G5"])
            {
                var vals = new int[] { 6, 6, 6 };
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
               
            }
            using (ExcelRange rng = excelWorksheet2.Cells["H3:H5"])
            {
                var vals = new int[] { 0,5,0 };
                rng.LoadFromCollection(vals);
                rng.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                
            }
            //Execel Logic Sheet end
            Response.Clear();
            Response.ClearHeaders();
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string attach = "attachment;filename=Reports.xlsx";

            Response.AddHeader("content-disposition", attach);

            Response.BinaryWrite(excel.GetAsByteArray());
            Response.End();

        }


        public JsonResult DropdownDevloper(int stackId)
        {
            List<Developer> list = _lg.DropDownlistDeveloper(stackId);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DropdownDevloperUpdate(int id)
        {
            List<DeveloperTechstack> list = _lg.DropDownlistDeveloperudate(id);
            return Json(list, JsonRequestBehavior.AllowGet);
        }



        public ActionResult Reports()
        {
            List<UserDailyTask> list = _lg.Reportlistid((int)Session["id"]);
            return View(list);
        }
    }

}
