using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Daily_Test_Management_project.Models
{
    public class MassageException
    {
        public static void Errorlog(string userID, ControllerContext context, Exception ex)
        {


            string userId = userID == null ? "Session Ended" : userID.ToString();
            string controllername = context.RouteData.Values["controller"].ToString();
            string methodname = context.RouteData.Values["action"].ToString();
            string lineno = ex.StackTrace.Substring(ex.StackTrace.Length - 8, 8);
            string exMSg = ex.Message;
            DataSet dsDbResponse = new DataSet();
            using (SqlConnection sqlconn = new SqlConnection(ConfigurationManager.AppSettings["DTM"]))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "Sp_Error_handle";
                    cmd.Connection = sqlconn;
                    cmd.Parameters.AddWithValue("@USERID", userID);
                    cmd.Parameters.AddWithValue("@CONTROLLER_NAME", controllername);
                    cmd.Parameters.AddWithValue("@METHOD_NAME",methodname);
                    cmd.Parameters.AddWithValue("@LINE_NUMBER", lineno);
                    cmd.Parameters.AddWithValue("@ERROR_MESSAGE", exMSg);
                    cmd.CommandTimeout = 0;

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dsDbResponse);
                    }
                }
            }

        }
    }


   
    

    
}