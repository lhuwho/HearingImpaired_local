<%@ WebHandler Language="C#" Class="AudiometryAppointment" %>

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

public class AudiometryAppointment : IHttpHandler {
    protected List<AADataList> returnvalue;
    //protected string connect = @"data source='(local)';Database='a2012tbtw';Trusted_Connection=False;User ID='a2012tbtw';Password='ogdvu524'";
    protected string connect = @"data source='(local)';Initial Catalog='HearingImpaired';Integrated Security=True;";
    
    
    public struct AADataList
    {
        public Int64 aID;
        public string aDate;
        public string aStartTime;
        public string aEndTime;
        public int aPeopleID;
        public string aPeopleName;
        public int aStudentID;
        public string aStudentName;
        public string aStudentState;
        public string aItem;
        public string aItemExplain;
        public string aContent;
        public int aState;
        public int aAssess1;
        public int aAssess2;
        public int aUnit;
    }
    
    public void ProcessRequest (HttpContext context) {
        //設定輸出格式為json格式
        context.Response.ContentType = "application/json";
        context.Request.ContentEncoding = System.Text.Encoding.UTF8;
        string type = context.Request.Form["type"].ToString();
        if (type == "getEventData")
        {
            int sUnit = int.Parse(context.Request.Form["sUnit"]);
            string sDate = context.Request.Form["sDate"].ToString();
            string eDate = context.Request.Form["eDate"].ToString();
            returnvalue = getAudiometryAppointment(sDate, eDate, sUnit);
            context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(returnvalue));
            context.Response.End();
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public List<AADataList> getAudiometryAppointment(string sDate, string eDate, int sUnit)
    {
        List<AADataList> returnValue = new List<AADataList>();
        DataBase Base = new DataBase();
        string ConditionReturn="";
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        Audiometry AudiometryData = new Audiometry();
        if (int.Parse(AudiometryData._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn = " AND AudiometryAppointment.Unit= " + UserFile[2] + " ";
        }
        
        /*string Limitsunit = "";
        if (sUnit != 0)
        {
            Limitsunit = " AND AudiometryAppointment.Unit=(@Unit) ";
        }*/

        string LimitsDate = "";
        LimitsDate = " AND AudiometryAppointment.ReserveDate BETWEEN (@aStartDate) AND (@aEndDate) ";
        DateTime aStartDate = DateTime.Parse(sDate);
        DateTime aEndDate = DateTime.Parse(eDate);

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT AudiometryAppointment.*,StudentDatabase.StudentName,StudentDatabase.CaseStatu,StaffDatabase.StaffName AS ReserveName FROM AudiometryAppointment " +
                            "LEFT JOIN StudentDatabase ON AudiometryAppointment.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                            "LEFT JOIN StaffDatabase ON AudiometryAppointment.ReserveID=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                            "WHERE AudiometryAppointment.isDeleted=0 " + ConditionReturn + LimitsDate;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@aStartDate", SqlDbType.DateTime).Value = DateTime.Parse(sDate.ToString());
                cmd.Parameters.Add("@aEndDate", SqlDbType.DateTime).Value = DateTime.Parse(eDate.ToString());
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = sUnit;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AADataList addValue = new AADataList();
                    addValue.aID = Int64.Parse(dr["ID"].ToString());
                    addValue.aDate = DateTime.Parse(dr["ReserveDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.aStartTime = dr["StartTime"].ToString();
                    addValue.aEndTime = dr["EndTime"].ToString();
                    addValue.aPeopleID = int.Parse(dr["ReserveID"].ToString());
                    addValue.aPeopleName = dr["ReserveName"].ToString();
                    addValue.aStudentID = int.Parse(dr["StudentID"].ToString());
                    addValue.aStudentName = dr["StudentName"].ToString();
                    addValue.aStudentState = dr["CaseStatu"].ToString();
                    addValue.aItem = dr["CheckItem"].ToString();
                    addValue.aItemExplain = dr["ItemOtherExplain"].ToString();
                    addValue.aContent = dr["CheckContent"].ToString();
                    addValue.aState = int.Parse(dr["State"].ToString());
                    addValue.aAssess1 = int.Parse(dr["AssessWho1"].ToString());
                    addValue.aAssess2 = int.Parse(dr["AssessWho2"].ToString());
                    addValue.aUnit = int.Parse(dr["Unit"].ToString());
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                AADataList addValue = new AADataList();
                addValue.aID = -1;
                addValue.aContent = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

}