<%@ WebHandler Language="C#" Class="teacher_schedule" %>

using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

public class teacher_schedule : IHttpHandler {

    protected List<TeachSchedule> returnvalue;
    
    //protected string connect = @"data source='(local)';Database='a2012tbtw';Trusted_Connection=False;User ID='a2012tbtw';Password='ogdvu524'";
    protected string connect = "";//@"data source='(local)';Initial Catalog='HearingImpaired';Integrated Security=True;";


    public struct TeachSchedule
    {
        public int ID;
        public string Date;
        public string StartTime;
        public string EndTime;
        public int TeacherID;
        public string TeacherName;
        public string ClassID;
        public List<StudentIDAndName> Students;
        public string Content;
        public int Unit;
    }
    public struct StudentIDAndName
    {
        public Int64 ID;
        public string Name;
    }

    public void ProcessRequest(HttpContext context)
    {
        DataBase myBase = new DataBase();
        connect = myBase.GetConnString();
        //設定輸出格式為json格式
        context.Response.ContentType = "application/json";
        context.Request.ContentEncoding = System.Text.Encoding.UTF8;
        string type = context.Request.Form["type"].ToString();
        if (type == "getEventData")
        {
            int sUnit = int.Parse(context.Request.Form["sUnit"]);
            string sDate = context.Request.Form["sDate"].ToString();
            string eDate = context.Request.Form["eDate"].ToString();
            string TeacherID = (context.Request.Form["TeacherID"] + "").ToString();
            string ClassNameID = (context.Request.Form["ClassNameID"] + "").ToString();
            returnvalue = getTeacherSchudule(sDate, eDate, sUnit, TeacherID, ClassNameID);
            context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(returnvalue));
            context.Response.End();
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

    public List<TeachSchedule> getTeacherSchudule(string sDate, string eDate, int sUnit, string TeacherID,string ClassNameID)
    {
        CheckDataTypeClass Chk = new CheckDataTypeClass();
        List<TeachSchedule> returnValue = new List<TeachSchedule>();
        DataBase Base = new DataBase();
        string ConditionReturn = "";
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        Audiometry AudiometryData = new Audiometry();
        if (int.Parse(AudiometryData._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn = " AND a.Unit= " + UserFile[2] + " ";
        }

        /*string Limitsunit = "";
        if (sUnit != 0)
        {
            Limitsunit = " AND AudiometryAppointment.Unit=(@Unit) ";
        }*/
        if (TeacherID != "")
        {
            ConditionReturn += " and TeacherID = @TeacherID ";
        }
        if (ClassNameID != "0" && ClassNameID != "")
        {
            ConditionReturn += " and ClassID = @ClassID ";
        
        }

        string LimitsDate = "";
        LimitsDate = " AND a.Date BETWEEN (@StartDate) AND (@EndDate) ";
        DateTime aStartDate = DateTime.Parse(sDate);
        DateTime aEndDate = DateTime.Parse(eDate);

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "select * from TeacherSchudule a " +
                            " left join (select staffid as bid, staffname as TeacherName from staffDatabase ) b on a.teacherid = b.bid "+
                            "WHERE isDeleted=0 " + ConditionReturn + LimitsDate;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = DateTime.Parse(sDate.ToString());
                cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = DateTime.Parse(eDate.ToString());
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = sUnit;
                cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value =  Chk.CheckStringtoIntFunction( TeacherID);
                cmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ClassNameID);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TeachSchedule addValue = new TeachSchedule();
                    
                    addValue.ID = int.Parse(dr["ID"].ToString());
                    addValue.Date = DateTime.Parse(dr["Date"].ToString()).ToString("yyyy-MM-dd");
                    addValue.StartTime = dr["StartTime"].ToString();
                    addValue.EndTime = dr["EndTime"].ToString();
                    addValue.TeacherID = int.Parse(dr["TeacherID"].ToString());
                    addValue.TeacherName = dr["TeacherName"].ToString();
                    addValue.ClassID = dr["ClassID"].ToString();

                    addValue.Students = getTeacherScheduleStudents(addValue.ID);
                    
                    addValue.Unit = int.Parse(dr["Unit"].ToString());
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                TeachSchedule addValue = new TeachSchedule();
                addValue.ID = -1;
                addValue.Content = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    private List<StudentIDAndName> getTeacherScheduleStudents(int TeacherScheduleID)
    {
        List<StudentIDAndName> returnValue = new List<StudentIDAndName>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "select b.StudentID,b.StudentName from TeacherSchuduleStudent a left join StudentDatabase b on a.StudentID = b.StudentID" +
                            " WHERE TeacherScheduleID=@TeacherScheduleID" ;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);

                cmd.Parameters.Add("@TeacherScheduleID", SqlDbType.Int).Value = TeacherScheduleID;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StudentIDAndName addValue = new StudentIDAndName();
                    addValue.ID = Int64.Parse(dr["StudentID"].ToString());
                    addValue.Name = dr["StudentName"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StudentIDAndName addValue = new StudentIDAndName();
                addValue.ID = -1;
                
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

}