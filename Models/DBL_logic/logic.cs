using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;

namespace Daily_Test_Management_project.Models.DBL_logic
{
    public class logic
    {
        string massage;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        private string conn = ConfigurationManager.ConnectionStrings["DTM"].ConnectionString;
        //private SqlDataAdapter _sqlDataAdapter;
        private SqlDataReader _sqlDataReader;
        private DataTable _datatable;
        string useriD;
        
        public int Login(UserLogin userLogin)
        {
            
            var id=0;

            //string password = PasswordEncryption.Decode(userLogin.);
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("SpUserLogin", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@userID", userLogin.UserID);
                        //cmd.Parameters.AddWithValue("@Password", PasswordEncryption.Decode(userLogin.Password));
                        cmd.Parameters.AddWithValue("@Password", userLogin.Password);
                        
                        con.Open();
                        _sqlDataReader= cmd.ExecuteReader();

                        if(userLogin.UserID=="Admin")
                        {
                            if (_sqlDataReader.Read())
                            {

                                id = (int)_sqlDataReader[0];
                                useriD = _sqlDataReader[1].ToString();

                            }
                        }
                        else
                        {
                            if(_sqlDataReader.Read())
                            {

                                id = (int)_sqlDataReader[7];
                                useriD = _sqlDataReader[1].ToString();

                            }
                        }

                        
                        
                        
                    }
                }
            }
            catch(Exception ex)
            {
                massage = ex.Message.ToString();
            }

            return id;
        }
        bool msg;
        public bool AddTask(UserDailyTask _user,int id)
        {

            

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("Sp_DaliyAddTask", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@TechstackID", _user.TechstackID);
                        cmd.Parameters.AddWithValue("@StatusID", _user.StatusID);
                        cmd.Parameters.AddWithValue("@Module_ID", _user.ModuleID);
                        cmd.Parameters.AddWithValue("@DeveloperID", _user.DeveloperID);
                        cmd.Parameters.AddWithValue("@DepertmentID", _user.DepartmentID);
                        cmd.Parameters.AddWithValue("@jiraID", _user.JiraID);
                        cmd.Parameters.AddWithValue("@Decsription", _user.Description);
                        cmd.Parameters.AddWithValue("@startDate", _user.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", _user.EndDate);
                        cmd.Parameters.AddWithValue("@Remark", _user.Remark);
                        cmd.Parameters.AddWithValue("@userid", id);
                        //cmd.Parameters.AddWithValue("@cretedBy", useriD);
                        cmd.Parameters.AddWithValue("@flag", "Insert");
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        msg = a > 0 ? true: false;
                    }
                }
            }
            catch (Exception ex)
            {

                massage = ex.Message.ToString();
                return msg = false;
            }

            return msg;
        }
        public bool  uppdateTask(UserDailyTask _user,int id)
        {
            bool msg;

            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("Sp_DaliyAddTask", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.AddWithValue("@ID", _user.ID);
                        cmd.Parameters.AddWithValue("@TechstackID", _user.TechstackID);
                        cmd.Parameters.AddWithValue("@StatusID", _user.StatusID);
                        cmd.Parameters.AddWithValue("@Module_ID", _user.ModuleID);
                        cmd.Parameters.AddWithValue("@DeveloperID", _user.DeveloperID);
                        cmd.Parameters.AddWithValue("@DepertmentID", _user.DepartmentID);
                        cmd.Parameters.AddWithValue("@jiraID", _user.JiraID);
                        cmd.Parameters.AddWithValue("@Decsription", _user.Description);
                        cmd.Parameters.AddWithValue("@startDate", _user.StartDate);
                        cmd.Parameters.AddWithValue("@EndDate", _user.EndDate);
                        cmd.Parameters.AddWithValue("@Remark", _user.Remark);
                        cmd.Parameters.AddWithValue("@userid",id);
                        //cmd.Parameters.AddWithValue("@modifiedBy", useriD);
                        //cmd.Parameters.AddWithValue("@cretedDate", DateTime.Now);
                        cmd.Parameters.AddWithValue("@flag", "update");
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        msg = a > 0 ? true : false ;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in code" + ex.Message);
            }

            return msg;
        }

        public DataSet DropDownTechstack()
        {
            
            DataSet datset = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter cmd = new SqlDataAdapter("SpDropDownList", con))
                    {
                        cmd.SelectCommand.CommandType = CommandType.StoredProcedure;
                        
                        
                        cmd.Fill(datset);
                        


                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return datset ;
        }

        public List<Reportlist> ReportlistExcel(DateTime CreatedDate)
        {

            List<Reportlist> list = new List<Reportlist>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("sp_listDailyTaskManagement", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@createdate", CreatedDate);
                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            list.Add(new Reportlist()
                            {
                                ID = (int)rw[0],
                                JiraID = rw[1].ToString(),
                                StartDate = (DateTime)rw[2],
                                EndDate = (DateTime)rw[3],
                                Description = rw[4].ToString(),
                                Remark = rw[5].ToString(),
                                TechstackID = rw[6].ToString(),
                                StatusID = rw[7].ToString(),
                                ModuleID = rw[8].ToString(),
                                DeveloperID = rw[9].ToString(),
                                DepartmentID = rw[10].ToString()
                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }

        public List<Reportlist>Reportlist()
        {
            
            List<Reportlist> list = new List<Reportlist>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("sp_listDailyTaskManagementExcel", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                       
                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach(DataRow rw in _datatable.Rows)
                        {
                            list.Add(new Reportlist()
                            {
                                ID = (int)rw[0],
                                JiraID =rw[1].ToString(),
                                StartDate = (DateTime)rw[2],
                                EndDate =(DateTime)rw[3],
                                Description =rw[4].ToString(),
                                Remark =rw[5].ToString(),
                                TechstackID =rw[6].ToString(),
                                StatusID =rw[7].ToString(),
                                ModuleID =rw[8].ToString(),
                                DeveloperID =rw[9].ToString(),
                                DepartmentID =rw[10].ToString()
                            });

                        }
                       
                        
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }




        public List<UserDailyTask> Reportlistid(int id)
        {
            List<UserDailyTask> _userlist  = new List<UserDailyTask>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("spReportBYid", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@id", id);
                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            _userlist.Add(new UserDailyTask()
                            {
                                ID = (int)rw[0],
                                TechstackID = (int)rw[1],
                                StatusID = (int)rw[2],
                                ModuleID = (int)rw[3],
                                DeveloperID = (int)rw[4],
                                DepartmentID = (int)rw[5],
                                JiraID = rw[6].ToString(),
                                Description = rw[7].ToString(),
                                StartDate = (DateTime)rw[8],
                                EndDate = (DateTime)rw[9],
                                Remark = rw[10].ToString(),

                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return _userlist;
        }


        public List<Reportlist> ReordsFilter(DateTime createdDate,DateTime Enddate)
        {
            List<Reportlist> list = new List<Reportlist>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("Sp_cretaeddate", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@cretaddate", createdDate);
                        adp.SelectCommand.Parameters.AddWithValue("@Enddate", Enddate);
                        //adp.SelectCommand.Parameters.AddWithValue("@Enddate", Enddate);
                        
                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            list.Add(new Reportlist()
                            {

                                ID = (int)rw[0],
                                JiraID = rw[1].ToString(),
                                StartDate = (DateTime)rw[2],
                                EndDate = (DateTime)rw[3],
                                Description = rw[4].ToString(),
                                Remark = rw[5].ToString(),
                                TechstackID = rw[6].ToString(),
                                StatusID = rw[7].ToString(),
                                ModuleID = rw[8].ToString(),
                                DeveloperID = rw[9].ToString(),
                                DepartmentID = rw[10].ToString()

                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }

        string[] role = new string[1];
        public string[] ROleBAsed(string username)
        {
            
            using (SqlConnection con = new SqlConnection(conn))
            {
                using (SqlDataAdapter adp = new SqlDataAdapter("Rolebased", con))
                {
                    adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adp.SelectCommand.Parameters.AddWithValue("@userID", username);
                    DataTable tb = new DataTable();
                    adp.Fill(tb);

                    foreach (DataRow rw in tb.Rows)
                    {
                       role[0]  = rw[0].ToString();
                    }
                }
            }
            return role;

        }

        public List<Reportlist> ReordsFilterDeveloper(DateTime createdDate ,int developerID)
        {
            List<Reportlist> list = new List<Reportlist>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("Sp_filterReocrdDeveloper", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@createdate", createdDate);
                        adp.SelectCommand.Parameters.AddWithValue("@developeID", developerID);
                        //adp.SelectCommand.Parameters.AddWithValue("@Enddate", Enddate);

                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            list.Add(new Reportlist()
                            {

                                ID = (int)rw[0],
                                JiraID = rw[1].ToString(),
                                StartDate = (DateTime)rw[2],
                                EndDate = (DateTime)rw[3],
                                Description = rw[4].ToString(),
                                Remark = rw[5].ToString(),
                                TechstackID = rw[6].ToString(),
                                StatusID = rw[7].ToString(),
                                ModuleID = rw[8].ToString(),
                                DeveloperID = rw[9].ToString(),
                                DepartmentID = rw[10].ToString()

                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }


        public List<Reportlist> ReportlistAdmin()
        {

            List<Reportlist> list = new List<Reportlist>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("sp_listDailyTaskManagementReports", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                       
                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            list.Add(new Reportlist()
                            {
                                ID = (int)rw[0],
                                JiraID = rw[1].ToString(),
                                StartDate = (DateTime)rw[2],
                                EndDate = (DateTime)rw[3],
                                Description = rw[4].ToString(),
                                Remark = rw[5].ToString(),
                                TechstackID = rw[6].ToString(),
                                StatusID = rw[7].ToString(),
                                ModuleID = rw[8].ToString(),
                                DeveloperID = rw[9].ToString(),
                                DepartmentID = rw[10].ToString()
                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }




        public string CreateUSer(CreateUSer user,Developer d)
        {
            string msg;
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("CreateUSer", con))
                    {
                        string password = PasswordEncryption.Encode(user.Password);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@USerID", user.userID);
                        cmd.Parameters.AddWithValue("@Password", password);
                        cmd.Parameters.AddWithValue("@developerName", d.DeveloperName );
                        cmd.Parameters.AddWithValue("@AccessModule", user.AccessModule);
                        cmd.Parameters.AddWithValue("@Active", user.Active);
                        cmd.Parameters.AddWithValue("@createdBy", user.CreatedBy);
                        cmd.Parameters.AddWithValue("@cratedDate", user.CreatedDate);
                        cmd.Parameters.AddWithValue("@roleID", user.RoleID);
                        con.Open();
                        int a = cmd.ExecuteNonQuery();
                        msg = a > 0 ? "UserID Created Successfully " : "Something went wrong..";

                    }
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            
            return msg;
        }

        

        public int userExist(string USerID)
        {
            int a=0;
            if(USerID.Trim()=="")
            {
                a = 0;
            }
            else
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlCommand cmd = new SqlCommand("spCheckUsernameForAnswer", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@username", USerID);
                        con.Open();
                        SqlDataReader rd = cmd.ExecuteReader();
                        while (rd.Read())
                        {
                            a = Convert.ToBoolean(rd[0]) ? 1 : 2;
                        }
                    }
                }
            }
           
            return a;
        }




        public List<Reportlist> SerachByDeveloper(string Developername)
        {
            List<Reportlist> list = new List<Reportlist>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("SPSearchBYdevelopername", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Developername", Developername);
                        
                        //adp.SelectCommand.Parameters.AddWithValue("@Enddate", Enddate);

                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            list.Add(new Reportlist()
                            {

                                ID = (int)rw[0],
                                JiraID = rw[1].ToString(),
                                StartDate = (DateTime)rw[2],
                                EndDate = (DateTime)rw[3],
                                Description = rw[4].ToString(),
                                Remark = rw[5].ToString(),
                                TechstackID = rw[6].ToString(),
                                StatusID = rw[7].ToString(),
                                ModuleID = rw[8].ToString(),
                                DeveloperID = rw[9].ToString(),
                                DepartmentID = rw[10].ToString()

                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }




        public List<Reportlist> StausFilter(string status)
        {
            List<Reportlist> list = new List<Reportlist>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("Sp_modification_FilterRecords", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@Name", status);

                        //adp.SelectCommand.Parameters.AddWithValue("@Enddate", Enddate);

                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            list.Add(new Reportlist()
                            {

                                ID = (int)rw[0],
                                JiraID = rw[1].ToString(),
                                StartDate = (DateTime)rw[2],
                                EndDate = (DateTime)rw[3],
                                Description = rw[4].ToString(),
                                Remark = rw[5].ToString(),
                                TechstackID = rw[6].ToString(),
                                StatusID = rw[7].ToString(),
                                ModuleID = rw[8].ToString(),
                                DeveloperID = rw[9].ToString(),
                                DepartmentID = rw[10].ToString()

                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }


        public List<Reportlist> CreatedStausFilter(string status,DateTime fromDate,DateTime Todate, string techstack)
        {
            List<Reportlist> list = new List<Reportlist>();
            try
            {
               
                
                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        using (SqlDataAdapter adp = new SqlDataAdapter("Sp_createddate_status__four_option", con))
                        {
                            adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                            adp.SelectCommand.Parameters.AddWithValue("@status", status);
                            adp.SelectCommand.Parameters.AddWithValue("@Fromdate", fromDate);
                            adp.SelectCommand.Parameters.AddWithValue("@ToDate", Todate);
                            adp.SelectCommand.Parameters.AddWithValue("@techstack", techstack);
                            _datatable = new DataTable();
                            adp.Fill(_datatable);
                            foreach (DataRow rw in _datatable.Rows)
                            {
                                list.Add(new Reportlist()
                                {

                                    ID = (int)rw[0],
                                    JiraID = rw[1].ToString(),
                                    StartDate = (DateTime)rw[2],
                                    EndDate = (DateTime)rw[3],
                                    Description = rw[4].ToString(),
                                    Remark = rw[5].ToString(),
                                    TechstackID = rw[6].ToString(),
                                    StatusID = rw[7].ToString(),
                                    ModuleID = rw[8].ToString(),
                                    DeveloperID = rw[9].ToString(),
                                    DepartmentID = rw[10].ToString()

                                });

                            }


                        }
                    }
                
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return list;
        }

        public List<Developer> DropDownlistDeveloper(int StatckID)
        {
            List<Developer> dev = new List<Developer>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("Sp_Developername", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@TechStackID", StatckID);

                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            dev.Add(new Developer()
                            {

                                DeveloperID = (int)rw[0],
                                DeveloperName = rw[1].ToString()

                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return dev;

        }


        public List<DeveloperTechstack> DropDownlistDeveloperudate(int id)
        {
            List<DeveloperTechstack> dev = new List<DeveloperTechstack>();
            try
            {
                using (SqlConnection con = new SqlConnection(conn))
                {
                    using (SqlDataAdapter adp = new SqlDataAdapter("Sp_Developername_update", con))
                    {
                        adp.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adp.SelectCommand.Parameters.AddWithValue("@id", id);

                        _datatable = new DataTable();
                        adp.Fill(_datatable);
                        foreach (DataRow rw in _datatable.Rows)
                        {
                            dev.Add(new DeveloperTechstack()
                            {

                                TechstackID = (int)rw[0],
                                TechStack = rw[1].ToString(),
                                DeveloperID = (int)rw[2],
                                DeveloperName = rw[3].ToString()

                            });

                        }


                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return dev;

        }
    }


}