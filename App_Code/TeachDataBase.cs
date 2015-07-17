﻿using System;
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

public struct OneCoursePlanData
{
    public int targetDomain;
    public string targetLong;
    public List<string> targetShort;
}

public struct ClassNameDataList
{
    public int cID;
    public string cName;
    public int cUnit;
    public string cTeacher;
    public int cTeacherID;
}

public struct CourseNameDataList
{
    public int cID;
    public string cName;
    public int cUnit;
    public int cCategory;
}

public struct CPTDataList
{
    public int sID;
    public int sCPTID;
    public string sClassName;
    public string sTeacherName;
    public string sCourseName;
    public DateTime sStartPeriod;
    public DateTime sEndPeriod;
    public int sUnit;
}

/// <summary>
/// Summary description for TeachDataBase
/// </summary>
public class TeachDataBase
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
	public TeachDataBase()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }
    public int createCoursePlanTemplate(int Unit, int ClassID, int TeacherID, int CourseID, string StartPeriod, string EndPeriod, List<string[]> targetContent)
    {
        int returnValue = 0;
        Int64 CPTID = 0;
        Int64 CPTLID = 0;
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO CoursePlanTemplate (ClassID, TeacherID, CourseNameID, StartPeriod, EndPeriod, Unit, CreateDateTime, isDeleted)" +
                    "VALUES (@ClassID, @TeacherID, @CourseNameID, @StartPeriod, @EndPeriod, @Unit, @CreateDateTime, 0)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ClassID", SqlDbType.Int).Value = ClassID;
                cmd.Parameters.Add("@TeacherID", SqlDbType.BigInt).Value = Int64.Parse(TeacherID.ToString());
                cmd.Parameters.Add("@CourseNameID", SqlDbType.Int).Value = CourseID;
                cmd.Parameters.Add("@StartPeriod", SqlDbType.Date).Value = DateTime.Parse(StartPeriod);
                cmd.Parameters.Add("@EndPeriod", SqlDbType.Date).Value = DateTime.Parse(EndPeriod);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Unit;
                cmd.Parameters.Add("@CreateDateTime", SqlDbType.DateTime).Value = System.DateTime.Now.ToString();
                cmd.ExecuteNonQuery();
                sql = "SELECT @@IDENTITY AS CPTID";
                cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CPTID = Int64.Parse(dr["CPTID"].ToString());
                }
                dr.Close();

                for (int i = 0; i < targetContent.Count; i++)
                {
                    sql = "INSERT INTO CoursePlanTemplateLong (Domain, TargetContent, CPTID, CreateDateTime, isDeleted) VALUES (@Domain, @TargetContent, @CPTID, @CreateDateTime, 0)";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@Domain", SqlDbType.TinyInt).Value = int.Parse(targetContent[i][0]);
                    cmd.Parameters.Add("@TargetContent", SqlDbType.NVarChar).Value = targetContent[i][1];
                    cmd.Parameters.Add("@CPTID", SqlDbType.BigInt).Value = CPTID;
                    cmd.Parameters.Add("@CreateDateTime", SqlDbType.DateTime).Value = System.DateTime.Now.ToString();
                    cmd.ExecuteNonQuery();

                    sql = "SELECT @@IDENTITY AS CPTLID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CPTLID = Int64.Parse(dr["CPTLID"].ToString());
                    }
                    dr.Close();

                    sql = "INSERT INTO CoursePlanTemplateShort (TargetContent, CPTID, CPTLID, CreateDateTime, isDeleted) VALUES (@TargetContent, @CPTID, @CPTLID, @CreateDateTime, 0)";
                    string targetShort = targetContent[i][2].ToString();
                    string[] shortArray = targetShort.Split('＠');
                    for (int n = 0; n < shortArray.Length; n++)
                    {
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TargetContent", SqlDbType.NVarChar).Value = shortArray[n];
                        cmd.Parameters.Add("@CPTID", SqlDbType.BigInt).Value = CPTID;
                        cmd.Parameters.Add("@CPTLID", SqlDbType.BigInt).Value = CPTLID;
                        cmd.Parameters.Add("@CreateDateTime", SqlDbType.DateTime).Value = System.DateTime.Now.ToString();
                        cmd.ExecuteNonQuery();
                    }
                }
                Sqlconn.Close();
                returnValue = 1;
            }
            catch (Exception e)
            {
                string msg = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }

    public int setCoursePlanTemplate(setCoursePlan CoursePlanTemplate)
    {
        int returnValue = 0;
        Int64 Column = 0;
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "";
                SqlCommand cmd = new SqlCommand();
                for (int i = 0; i < CoursePlanTemplate.targetDomain.Count; i++)
                {
                    if (CoursePlanTemplate.targetDomain[i].LongColumn != 0)
                    {
                        sql = "UPDATE CoursePlanTemplateLong SET TargetContent=@TargetContent WHERE CPTLID=@CPTLID AND CPTID=@CPTID AND isDeleted=0";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@CPTLID", SqlDbType.BigInt).Value = CoursePlanTemplate.targetDomain[i].LongColumn;
                        cmd.Parameters.Add("@CPTID", SqlDbType.BigInt).Value = CoursePlanTemplate.targetDomain[i].ISPColumn;
                        cmd.Parameters.Add("@TargetContent", SqlDbType.NVarChar).Value = CoursePlanTemplate.targetDomain[i].Content;
                        cmd.ExecuteNonQuery();

                        returnValue=this.setShortISPCoursePlanTemplate(CoursePlanTemplate.targetDomain[i].ShortTarget);
                    }
                    else if (CoursePlanTemplate.targetDomain[i].Content.Length > 0)
                    {
                        sql = "INSERT INTO CoursePlanTemplateLong(Domain, TargetContent, CPTID) VALUES (@Domain, @TargetContent, @CPTID)";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@Domain", SqlDbType.TinyInt).Value = CoursePlanTemplate.targetDomain[i].Domain;
                        cmd.Parameters.Add("@CPTID", SqlDbType.BigInt).Value = CoursePlanTemplate.targetDomain[i].ISPColumn;
                        cmd.Parameters.Add("@TargetContent", SqlDbType.NVarChar).Value = CoursePlanTemplate.targetDomain[i].Content;
                        returnValue = cmd.ExecuteNonQuery();

                        if (returnValue != 0)
                        {
                            sql = "select IDENT_CURRENT('CoursePlanTemplateLong') AS ISPLong";
                            cmd = new SqlCommand(sql, Sqlconn);
                            SqlDataReader dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                Column = Int64.Parse(dr["ISPLong"].ToString());
                            }
                            dr.Close();
                            returnValue = this.createGroupISPShort(CoursePlanTemplate.Column, Column, CoursePlanTemplate.targetDomain[i].ShortTarget);
                        }

                    }
                }

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string item = e.Message;
                returnValue = -1;
            }

        }
        return returnValue;
    }
    private int setShortISPCoursePlanTemplate(List<ISPShort> ShortPlan)
    {
        int returnValue = 0;
        List<int> SQLreturnValue = new List<int>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "";
                SqlCommand cmd = new SqlCommand();
                for (int i = 0; i < ShortPlan.Count; i++)
                {
                    sql = "UPDATE CoursePlanTemplateShort SET TargetContent=@TargetContent WHERE CPTSID=@CPTSID AND CPTLID=@CPTLID AND isDeleted=0";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@CPTSID", SqlDbType.BigInt).Value = ShortPlan[i].ShortColumn;
                    cmd.Parameters.Add("@CPTLID", SqlDbType.BigInt).Value = ShortPlan[i].LongColumn;
                    cmd.Parameters.Add("@TargetContent", SqlDbType.NVarChar).Value = ShortPlan[i].Content;
                    SQLreturnValue.Add((int)cmd.ExecuteNonQuery());              
                }
                returnValue = Convert.ToInt32(!SQLreturnValue.Contains(0));
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string item = e.Message;
                returnValue = -1;
            }

        }
        return returnValue;
    }

    private int createGroupISPShort(Int64 ISPContent, Int64 Column, List<ISPShort> ShortPlan)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "";
                SqlCommand cmd;
                for (int i = 0; i < ShortPlan.Count; i++)
                {
                    sql = "INSERT INTO CoursePlanTemplateShort(TargetContent, CPTID, CPTLID) VALUES (@TargetContent, @CPTID, @CPTLID)";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@TargetContent", SqlDbType.NVarChar).Value = ShortPlan[i].Content;
                    cmd.Parameters.Add("@CPTID", SqlDbType.BigInt).Value = ISPContent;
                    cmd.Parameters.Add("@CPTLID", SqlDbType.BigInt).Value = Column;
                    returnValue = cmd.ExecuteNonQuery();
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }
    public int delCoursePlanTemplate(Int64 LongColumn)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE CoursePlanTemplateLong SET isDeleted=1 WHERE CPTLID=@CPTLID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CPTLID", SqlDbType.BigInt).Value = LongColumn;
                returnValue=cmd.ExecuteNonQuery();

                sql = "UPDATE CoursePlanTemplateShort SET isDeleted=1 WHERE CPTLID=@CPTLID AND isDeleted=0";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CPTLID", SqlDbType.BigInt).Value = LongColumn;
                returnValue = cmd.ExecuteNonQuery();
                
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string item = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }
    public setCoursePlan getCoursePlan(string CPTID) {
        setCoursePlan returnValue = new setCoursePlan();
        returnValue.targetDomain = new List<ISPLong>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT CoursePlanTemplate.*,StaffDatabase.StaffName,ClassName.ClassIDName,CourseName.CourseIDName " +
                    "FROM CoursePlanTemplate INNER JOIN StaffDatabase ON CoursePlanTemplate.TeacherID=StaffDatabase.StaffID " +
                    "INNER JOIN ClassName ON CoursePlanTemplate.ClassID=ClassName.ClassID "+
                    "INNER JOIN CourseName ON CoursePlanTemplate.CourseNameID=CourseName.CourseID "+
                    "WHERE CoursePlanTemplate.isDeleted=0 AND CPTID=@ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = CPTID;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.Column = Int64.Parse(dr["CPTID"].ToString());
                    returnValue.unitNum = int.Parse(dr["Unit"].ToString());
                    returnValue.courseIDName = dr["CourseIDName"].ToString();
                    returnValue.courseID = int.Parse(dr["CourseNameID"].ToString());
                    returnValue.classIDName = dr["ClassIDName"].ToString();
                    returnValue.classID = int.Parse(dr["ClassID"].ToString());
                    returnValue.teacherIDName = dr["StaffName"].ToString();
                    returnValue.teacherID = int.Parse(dr["TeacherID"].ToString());
                    returnValue.startPlanDate=DateTime.Parse(dr["StartPeriod"].ToString());
                    returnValue.endPlanDate=DateTime.Parse(dr["EndPeriod"].ToString());
                    returnValue.targetDomain = new List<ISPLong>();
                }
                dr.Close();

                sql = "SELECT CoursePlanTemplateLong.CPTLID, CoursePlanTemplateLong.CPTID, CoursePlanTemplateLong.TargetContent, CoursePlanTemplateLong.Domain " +
                    "FROM CoursePlanTemplate INNER JOIN CoursePlanTemplateLong ON CoursePlanTemplate.CPTID=CoursePlanTemplateLong.CPTID "+
                    "WHERE CoursePlanTemplate.isDeleted=0 AND CoursePlanTemplateLong.isDeleted=0 AND CoursePlanTemplate.CPTID=@ID ORDER BY CoursePlanTemplateLong.CPTLID";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Int64.Parse(CPTID);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ISPLong ItemLong = new ISPLong();
                    ItemLong.ShortTarget = new List<ISPShort>();
                    ItemLong.ISPColumn = Int64.Parse(dr["CPTID"].ToString());
                    ItemLong.LongColumn = Int64.Parse(dr["CPTLID"].ToString());
                    ItemLong.Domain = int.Parse(dr["Domain"].ToString());
                    ItemLong.Content = dr["TargetContent"].ToString();
                    ItemLong.ShortTarget = this.getTeachModelShort(Int64.Parse(CPTID), Int64.Parse(ItemLong.LongColumn.ToString()));
                    returnValue.targetDomain.Add(ItemLong);
                }
                dr.Close();


                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.Column=-1;
                returnValue.courseIDName=e.Message.ToString();
            }

        }
        return returnValue;
    }
    private List<ISPShort> getTeachModelShort(Int64 ISPColumn, Int64 LongColumn)
    {
        List<ISPShort> returnValue = new List<ISPShort>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT CoursePlanTemplateShort.CPTLID,CoursePlanTemplateShort.CPTSID, CoursePlanTemplateShort.CPTID, CoursePlanTemplateShort.TargetContent " +
                             "FROM CoursePlanTemplateShort INNER JOIN CoursePlanTemplateLong ON CoursePlanTemplateShort.CPTLID=CoursePlanTemplateLong.CPTLID " +
                             "WHERE CoursePlanTemplateShort.isDeleted=0 AND CoursePlanTemplateLong.isDeleted=0 AND CoursePlanTemplateShort.CPTID=@ID " +
                             "AND CoursePlanTemplateShort.CPTLID=@LongColumn ORDER BY CoursePlanTemplateShort.CPTSID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = ISPColumn;
                cmd.Parameters.Add("@LongColumn", SqlDbType.BigInt).Value = LongColumn;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ISPShort addValue = new ISPShort();
                    addValue.LongColumn = Int64.Parse(dr["CPTLID"].ToString());
                    addValue.ShortColumn = Int64.Parse(dr["CPTSID"].ToString());
                    addValue.Content = dr["TargetContent"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                ISPShort addValue = new ISPShort();
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }
    public List<string> SearchClassDataBaseCount(int cID, string cName, int cUnit)
    {
        List<string> returnValue = new List<string>();
        DataBase Base = new DataBase();
        int Count = 0;
        string LimitsID = "";
        if (cID != 0)
        {
            LimitsID = " AND ClassID=(@ClassID) ";
        }
        string LimitsName = "";
        if (cName.Length > 0 && cName != "")
        {
            LimitsName = " AND ClassIDName LIKE (@ClassIDName) ";
        }
        string Limitsunit = "";
        if (cUnit != 0)
        {
            Limitsunit = " AND Unit =(@Unit) ";
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM ClassName WHERE isDeleted=0 " + LimitsID + LimitsName + Limitsunit;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ClassID", SqlDbType.NVarChar).Value = cID;
                cmd.Parameters.Add("@ClassIDName", SqlDbType.NVarChar).Value = cName + "%";
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = cUnit;
                Count = (int)cmd.ExecuteScalar();

                returnValue.Add(Count.ToString());

                returnValue.Add(cID.ToString());
                returnValue.Add(cName);
                returnValue.Add(cUnit.ToString());
                //returnValue.Add(cTeacherID.toString());
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.Add("-1");
                returnValue.Add(e.Message.ToString());
            }

        }
        return returnValue;
    }

    public List<ClassNameDataList> SearchClassDataBase(int indexpage, int cID, string cName, int cUnit)
    {
        List<ClassNameDataList> returnValue = new List<ClassNameDataList>();
        DataBase Base = new DataBase();
        string LimitsID = "";
        if (cID != 0)
        {
            LimitsID = " AND ClassID=(@ClassID) ";
        }
        string LimitsName = "";
        if (cName.Length > 0 && cName != "")
        {
            LimitsName = " AND ClassIDName LIKE (@ClassIDName) ";
        }
        string Limitsunit = "";
        if (cUnit != 0)
        {
            Limitsunit = " AND ClassName.Unit =(@Unit) ";
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ClassName.ClassID DESC) AS RowNum,"+
                            "StaffDatabase.StaffName AS TeacherName,TeacherID,ClassID,ClassName.ClassIDName,ClassName.Unit FROM ClassName " +
                            "INNER JOIN StaffDatabase ON ClassName.TeacherID=StaffDatabase.StaffID WHERE ClassName.isDeleted=0 " + 
                            LimitsID + LimitsName + Limitsunit + " ) AS NewTable " +
                            "WHERE RowNum >= (@indexpage-" + this.PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = Int64.Parse(cID.ToString());
                cmd.Parameters.Add("@ClassIDName", SqlDbType.NVarChar).Value = cName + "%";
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = cUnit;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ClassNameDataList addValue = new ClassNameDataList();
                    addValue.cID = int.Parse(dr["ClassID"].ToString());
                    addValue.cName = dr["ClassIDName"].ToString();
                    addValue.cUnit = int.Parse(dr["Unit"].ToString());
                    addValue.cTeacher = dr["TeacherName"].ToString();
                    addValue.cTeacherID = int.Parse(dr["TeacherID"].ToString());
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                ClassNameDataList addValue = new ClassNameDataList();
                addValue.cID = -1;
                addValue.cName = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }

    public List<string> SearchCourseDataBaseCount(int cID, string cName, int cUnit)
    {
        List<string> returnValue = new List<string>();
        DataBase Base = new DataBase();
        int Count = 0;
        string LimitsID = "";
        if (cID != 0)
        {
            LimitsID = " AND CourseID=(@CourseID) ";
        }
        string LimitsName = "";
        if (cName.Length > 0 && cName != "")
        {
            LimitsName = " AND CourseIDName LIKE (@CourseIDName) ";
        }
        string Limitsunit = "";
        if (cUnit != 0)
        {
            Limitsunit = " AND Unit =(@Unit) ";
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM CourseName WHERE isDeleted=0 " + LimitsID + LimitsName + Limitsunit;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CourseID", SqlDbType.NVarChar).Value = cID;
                cmd.Parameters.Add("@CourseIDName", SqlDbType.NVarChar).Value = cName + "%";
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = cUnit;
                Count = (int)cmd.ExecuteScalar();

                returnValue.Add(Count.ToString());

                returnValue.Add(cID.ToString());
                returnValue.Add(cName);
                returnValue.Add(cUnit.ToString());
                //returnValue.Add(cTeacherID.toString());
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.Add("-1");
                returnValue.Add(e.Message.ToString());
            }

        }
        return returnValue;
    }

    public List<CourseNameDataList> SearchCourseDataBase(int indexpage, int cID, string cName, int cUnit)
    {
        List<CourseNameDataList> returnValue = new List<CourseNameDataList>();
        DataBase Base = new DataBase();
        string LimitsID = "";
        if (cID != 0)
        {
            LimitsID = " AND CourseID=(@CourseID) ";
        }
        string LimitsName = "";
        if (cName.Length > 0 && cName != "")
        {
            LimitsName = " AND CourseIDName LIKE (@CourseIDName) ";
        }
        string Limitsunit = "";
        if (cUnit != 0)
        {
            Limitsunit = " AND Unit =(@Unit) ";
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY CourseID ASC) AS RowNum," +
                            "CourseID,CourseIDName,Unit,CourseCategory FROM CourseName " +
                            "WHERE isDeleted=0 " + LimitsID + LimitsName + Limitsunit + " ) AS NewTable " +
                            "WHERE RowNum >= (@indexpage-" + this.PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@CourseID", SqlDbType.BigInt).Value = Int64.Parse(cID.ToString());
                cmd.Parameters.Add("@CourseIDName", SqlDbType.NVarChar).Value = cName + "%";
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = cUnit;

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CourseNameDataList addValue = new CourseNameDataList();
                    addValue.cID = int.Parse(dr["CourseID"].ToString());
                    addValue.cName = dr["CourseIDName"].ToString();
                    addValue.cUnit = int.Parse(dr["Unit"].ToString());
                    addValue.cCategory = int.Parse(dr["CourseCategory"].ToString());
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CourseNameDataList addValue = new CourseNameDataList();
                addValue.cID = -1;
                addValue.cName = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }
    private string SearchCPTDataConditionReturn(SearchTeachCaseItem SearchData)
    {
        string ConditionReturn = "";
        if (SearchData.txtClassName != null)
        {
            ConditionReturn += " AND ClassName.ClassIDName LIKE (@ClassIDName) ";
        }
        if ( SearchData.txtTeacherName != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName LIKE(@TeacherName) ";
        }
        if ( SearchData.txtCourseName != null)
        {
            ConditionReturn += " AND CourseName.CourseIDName LIKE (@CourseIDName) ";
        }
        if (SearchData.txtSPeriod != null && SearchData.txtEPeriod != null && SearchData.txtSPeriod != "1900-01-01" && SearchData.txtEPeriod != "1900-01-01")
        {
            ConditionReturn += " AND StartPeriod >= @StartPeriod AND EndPeriod <= @EndPeriod ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserData = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (UserData[2] != "0" && UserData[2] != null)
        {
            ConditionReturn += " AND CoursePlanTemplate.Unit =(@Unit) ";
        }
        return ConditionReturn;
    }
    public string[] SearchCPTDataBaseCount(SearchTeachCaseItem SearchData)
    {
        string[] returnValue = new string[2];
        DataBase Base = new DataBase();
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserData = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        string ConditionReturn = this.SearchCPTDataConditionReturn(SearchData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT Count(CoursePlanTemplate.CPTID) AS sCount " +
                            "FROM CoursePlanTemplate " +
                            "INNER JOIN StaffDatabase ON StaffDatabase.isDeleted=0 AND CoursePlanTemplate.TeacherID=StaffDatabase.StaffID " +
                            "INNER JOIN ClassName ON ClassName.isDeleted=0 AND CoursePlanTemplate.ClassID=ClassName.ClassID " +
                            "INNER JOIN CourseName ON CourseName.isDeleted=0 AND CoursePlanTemplate.ClassID=CourseName.CourseID " +
                             "WHERE CoursePlanTemplate.isDeleted=0" + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ClassIDName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchData.txtClassName) + "%";
                cmd.Parameters.Add("@TeacherName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchData.txtTeacherName) + "%";
                cmd.Parameters.Add("@CourseIDName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchData.txtCourseName) + "%";
                cmd.Parameters.Add("@StartPeriod", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchData.txtSPeriod);
                cmd.Parameters.Add("@EndPeriod", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchData.txtEPeriod);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(UserData[2]);
                returnValue[0] = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0]="-1";
                returnValue[1]=e.Message.ToString();
            }
        }
        return returnValue;
    }

    public List<CPTDataList> SearchCPTDataBase(int indexpage, SearchTeachCaseItem SearchData)
    {
        List<CPTDataList> returnValue = new List<CPTDataList>();
        DataBase Base = new DataBase();
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserData = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        string ConditionReturn = this.SearchCPTDataConditionReturn(SearchData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY CoursePlanTemplate.CPTID ASC) AS RowNum, CoursePlanTemplate.CPTID," +
                            "ClassName.ClassIDName AS ClassIDName," +
                            "StaffDatabase.StaffName AS TeacherName,"+
                            "CourseName.CourseIDName AS CourseIDName," +
                            "CoursePlanTemplate.StartPeriod, CoursePlanTemplate.EndPeriod, CoursePlanTemplate.Unit " +
                            "FROM CoursePlanTemplate " +
                            "INNER JOIN ClassName ON ClassName.isDeleted=0 AND CoursePlanTemplate.ClassID=ClassName.ClassID " +
                            "INNER JOIN StaffDatabase ON StaffDatabase.isDeleted=0 AND CoursePlanTemplate.TeacherID=StaffDatabase.StaffID " +
                            "INNER JOIN CourseName ON CourseName.isDeleted=0 AND CoursePlanTemplate.CourseNameID=CourseName.CourseID " +
                            ConditionReturn +" ) "+
                             "AS NewTable " +
                            "WHERE RowNum >= (@indexpage-" + this.PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@ClassIDName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchData.txtClassName) + "%";
                cmd.Parameters.Add("@TeacherName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchData.txtTeacherName) + "%";
                cmd.Parameters.Add("@CourseIDName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchData.txtCourseName) + "%";
                cmd.Parameters.Add("@StartPeriod", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchData.txtSPeriod);
                cmd.Parameters.Add("@EndPeriod", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchData.txtEPeriod);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(UserData[2]);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CPTDataList addValue = new CPTDataList();
                    addValue.sID = int.Parse(dr["RowNum"].ToString());
                    addValue.sCPTID = int.Parse(dr["CPTID"].ToString());
                    addValue.sClassName = dr["ClassIDName"].ToString();
                    addValue.sTeacherName = dr["TeacherName"].ToString();
                    addValue.sCourseName = dr["CourseIDName"].ToString();
                    addValue.sStartPeriod = DateTime.Parse(dr["StartPeriod"].ToString());
                    addValue.sEndPeriod = DateTime.Parse(dr["EndPeriod"].ToString());
                    addValue.sUnit = int.Parse(dr["Unit"].ToString());
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CPTDataList addValue = new CPTDataList();
                addValue.sCPTID = -1;
                addValue.sClassName = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }
    public string[] createCaseISP(Int64 StuCaseDataID, CreateTeachISP StudentISP)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        CaseDataBase CData = new CaseDataBase();
        StudentResult getCaseData = CData.getStudentData(StuCaseDataID.ToString());
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO CaseISPstate (Unit, StudentID, PhysicalAndMentalDisabilityHandbook, DisabilityCategory1, DisabilityGrade1, DisabilityCategory2, DisabilityGrade2, "+
                    "DisabilityCategory3, DisabilityGrade3, NoDisabilityHandbook, ApplyDisabilityHandbook, DisabilityProve, AidsManagement, AidsManagementTextAge, "+
                    "HearingAids_R, AidsBrand_R, AidsOptionalTime_R, AidsOptionalLocation_R, EEarHospital_R, EEarImplants_R, EEarOpen_R, HearingAids_L, AidsBrand_L, "+
                    "AidsOptionalTime_L, AidsOptionalLocation_L, EEarHospital_L, EEarImplants_L, EEarOpen_L, AcceptClass, MedicalEducation, MedicalEducationText1, "+
                    "MedicalEducationText2, MedicalEducationText3, MedicalEducationText4, MedicalEducationText5, MedicalEducationText6, MedicalEducationText7," +
                    "ExecutionTimeSince, ExecutionTimeUntil, ParticipantDate1, ParticipantParent1, ParticipantTeache1, ParticipantSocialWorker1, ParticipantAudiologist1, "+
                    "ParticipantHead1, ParticipantProfessionals1, ParticipantDate2, ParticipantParent2, ParticipantTeache2, ParticipantSocialWorker2, ParticipantAudiologist2, "+
                    "ParticipantHead2, ParticipantProfessionals2, PlanWriter1, PlanWriteFrameDate1, PlanWriter2, PlanWriteFrameDate2, PlanWriter3, PlanWriteFrameDate3) " +
                    "VALUES (@Unit, @StudentID, @PhysicalAndMentalDisabilityHandbook, @DisabilityCategory1, @DisabilityGrade1, @DisabilityCategory2, @DisabilityGrade2, "+
                    "@DisabilityCategory3, @DisabilityGrade3, @NoDisabilityHandbook, @ApplyDisabilityHandbook, @DisabilityProve, @AidsManagement, @AidsManagementTextAge, "+
                    "@HearingAids_R, @AidsBrand_R, @AidsOptionalTime_R, @AidsOptionalLocation_R, @EEarHospital_R, @EEarImplants_R, @EEarOpen_R, @HearingAids_L, @AidsBrand_L, "+
                    "@AidsOptionalTime_L, @AidsOptionalLocation_L, @EEarHospital_L, @EEarImplants_L, @EEarOpen_L, @AcceptClass, @MedicalEducation, @MedicalEducationText1, "+
                    "@MedicalEducationText2, @MedicalEducationText3, @MedicalEducationText4, @MedicalEducationText5, @MedicalEducationText6, @MedicalEducationText7," +
                    "@ExecutionTimeSince, @ExecutionTimeUntil, @ParticipantDate1, @ParticipantParent1, @ParticipantTeache1, @ParticipantSocialWorker1, @ParticipantAudiologist1, "+
                    "@ParticipantHead1, @ParticipantProfessionals1, @ParticipantDate2, @ParticipantParent2, @ParticipantTeache2, @ParticipantSocialWorker2, "+
                    "@ParticipantAudiologist2, @ParticipantHead2, @ParticipantProfessionals2, @PlanWriter1, @PlanWriteFrameDate1, @PlanWriter2, @PlanWriteFrameDate2, @PlanWriter3, @PlanWriteFrameDate3)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(getCaseData.StudentData.caseStatu);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(getCaseData.StudentData.studentID);
                cmd.Parameters.Add("@PhysicalAndMentalDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.manualWhether);
                cmd.Parameters.Add("@DisabilityCategory1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualCategory1);
                cmd.Parameters.Add("@DisabilityGrade1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualGrade1);
                cmd.Parameters.Add("@DisabilityCategory2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualCategory2);
                cmd.Parameters.Add("@DisabilityGrade2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualGrade2);
                cmd.Parameters.Add("@DisabilityCategory3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualCategory3);
                cmd.Parameters.Add("@DisabilityGrade3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualGrade3);
                cmd.Parameters.Add("@NoDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.manualNo);
                cmd.Parameters.Add("@ApplyDisabilityHandbook", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualUnit);
                cmd.Parameters.Add("@DisabilityProve", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.studentManualImg);
                cmd.Parameters.Add("@AidsManagement", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.assistmanage);
                cmd.Parameters.Add("@AidsManagementTextAge", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.Accessory);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.BrandR1);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.BuyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.BuyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.InsertHospitalR);
                cmd.Parameters.Add("@EEarImplants_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.InsertDateR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.OpenHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.BrandL1);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.BuyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.BuyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.InsertHospitalL);
                cmd.Parameters.Add("@EEarImplants_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.InsertDateL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.OpenHzDateL);
                cmd.Parameters.Add("@AcceptClass", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.edu);
                cmd.Parameters.Add("@MedicalEducation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.edu1);
                cmd.Parameters.Add("@MedicalEducationText1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF1);
                cmd.Parameters.Add("@MedicalEducationText2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF2);
                cmd.Parameters.Add("@MedicalEducationText3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF3);
                cmd.Parameters.Add("@MedicalEducationText4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF4);
                cmd.Parameters.Add("@MedicalEducationText5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF5);
                cmd.Parameters.Add("@MedicalEducationText6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF6);
                cmd.Parameters.Add("@MedicalEducationText7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF7);
                cmd.Parameters.Add("@ExecutionTimeSince", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                cmd.Parameters.Add("@ExecutionTimeUntil", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.endPlanDate);
                cmd.Parameters.Add("@ParticipantDate1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.ServiceDate1);
                cmd.Parameters.Add("@ParticipantParent1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.Parent1);
                cmd.Parameters.Add("@ParticipantTeache1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Teacher1);
                cmd.Parameters.Add("@ParticipantSocialWorker1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Sociality1);
                cmd.Parameters.Add("@ParticipantAudiologist1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.ListenTutor1);
                cmd.Parameters.Add("@ParticipantHead1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Manager1);
                cmd.Parameters.Add("@ParticipantProfessionals1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.RelationalPeople1);
                cmd.Parameters.Add("@ParticipantDate2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.ServiceDate2); ;
                cmd.Parameters.Add("@ParticipantParent2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.Parent2);
                cmd.Parameters.Add("@ParticipantTeache2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Teacher2);
                cmd.Parameters.Add("@ParticipantSocialWorker2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Sociality2);
                cmd.Parameters.Add("@ParticipantAudiologist2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.ListenTutor2);
                cmd.Parameters.Add("@ParticipantHead2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Manager2);
                cmd.Parameters.Add("@ParticipantProfessionals2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.RelationalPeople2);
                cmd.Parameters.Add("@PlanWriter1", SqlDbType.Int).Value = HttpContext.Current.User.Identity.Name;
                cmd.Parameters.Add("@PlanWriteFrameDate1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                cmd.Parameters.Add("@PlanWriter2", SqlDbType.Int).Value = HttpContext.Current.User.Identity.Name;
                cmd.Parameters.Add("@PlanWriteFrameDate2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                cmd.Parameters.Add("@PlanWriter3", SqlDbType.Int).Value = HttpContext.Current.User.Identity.Name;
                cmd.Parameters.Add("@PlanWriteFrameDate3", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] == "1")
                {
                    sql = "select IDENT_CURRENT('CaseISPstate') AS TID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["TID"].ToString();
                    }
                    dr.Close();
                }
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
    }
    public setTeachISPAllData getTeachISP(Int64 StudentISP)
    {
        setTeachISPAllData returnValue = new setTeachISPAllData();
        returnValue.ISP1Data = new setTeachISP1();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT CaseISPstate.*, StudentDatabase.StudentName, StudentDatabase.StudentSex, StudentDatabase.ContactRelation2, StudentDatabase.ContactName2, "+
                    "StudentDatabase.ContactTel_company2, StudentDatabase.ContactTel_home2, StudentDatabase.ContactPhone2, StudentDatabase.ContactFax2, StudentDatabase.StudentBirthday " +
                    "FROM CaseISPstate INNER JOIN StudentDatabase ON CaseISPstate.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                    "WHERE CaseISPstate.ID=@ID AND CaseISPstate.isDeleted=0 AND StudentDatabase.isDeleted=0";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentISP;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ISP1Data.Column =Int64.Parse(dr["ID"].ToString());
                    returnValue.ISP1Data.Unit = dr["Unit"].ToString();
                    returnValue.ISP1Data.studentID = dr["StudentID"].ToString();
                    returnValue.ISP1Data.studentName = dr["StudentName"].ToString();
                    returnValue.ISP1Data.studentSex = dr["StudentSex"].ToString();
                    returnValue.ISP1Data.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    // dr["ContactRelation2"].ToString();
                    returnValue.ISP1Data.LegalrepresentativeName = dr["ContactName2"].ToString();
                    returnValue.ISP1Data.LegalrepresentativePhone = dr["ContactTel_company2"].ToString();
                    returnValue.ISP1Data.LegalrepresentativePhoneHome = dr["ContactTel_home2"].ToString();
                    returnValue.ISP1Data.LegalrepresentativePhoneMobile = dr["ContactPhone2"].ToString();
                    returnValue.ISP1Data.LegalrepresentativePhoneFax = dr["ContactFax2"].ToString();
                    returnValue.ISP1Data.manualWhether = dr["PhysicalAndMentalDisabilityHandbook"].ToString();
                    returnValue.ISP1Data.manualCategory1 = dr["DisabilityCategory1"].ToString();
                    returnValue.ISP1Data.manualGrade1 = dr["DisabilityGrade1"].ToString();
                    returnValue.ISP1Data.manualCategory2 = dr["DisabilityCategory2"].ToString();
                    returnValue.ISP1Data.manualGrade2 = dr["DisabilityGrade2"].ToString();
                    returnValue.ISP1Data.manualCategory3 = dr["DisabilityCategory3"].ToString();
                    returnValue.ISP1Data.manualGrade3 = dr["DisabilityGrade3"].ToString();
                    returnValue.ISP1Data.manualNo = dr["NoDisabilityHandbook"].ToString();
                    returnValue.ISP1Data.manualUnit = dr["ApplyDisabilityHandbook"].ToString();
                    returnValue.ISP1Data.studentManualImg = dr["DisabilityProve"].ToString();
                    returnValue.ISP1Data.assistmanage = dr["AidsManagement"].ToString();
                    returnValue.ISP1Data.Accessory = dr["AidsManagementTextAge"].ToString();
                    returnValue.ISP1Data.assistmanageR = dr["HearingAids_R"].ToString();
                    returnValue.ISP1Data.BrandR1 = dr["AidsBrand_R"].ToString();
                    returnValue.ISP1Data.BuyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.BuyingPlaceR = dr["AidsOptionalLocation_R"].ToString();
                    returnValue.ISP1Data.InsertHospitalR = dr["EEarHospital_R"].ToString();
                    returnValue.ISP1Data.InsertDateR = DateTime.Parse(dr["EEarImplants_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.OpenHzDateR = DateTime.Parse(dr["EEarOpen_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.assistmanageL = dr["HearingAids_L"].ToString();
                    returnValue.ISP1Data.BrandL1 = dr["AidsBrand_L"].ToString();
                    returnValue.ISP1Data.BuyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.BuyingPlaceL = dr["AidsOptionalLocation_L"].ToString();
                    returnValue.ISP1Data.InsertHospitalL = dr["EEarHospital_L"].ToString();
                    returnValue.ISP1Data.InsertDateL = dr["EEarImplants_L"].ToString();
                    returnValue.ISP1Data.OpenHzDateL = dr["EEarOpen_L"].ToString();
                    returnValue.ISP1Data.edu = dr["AcceptClass"].ToString();
                    returnValue.ISP1Data.edu1 = dr["MedicalEducation"].ToString();
                    returnValue.ISP1Data.PandF1 = dr["MedicalEducationText1"].ToString();
                    returnValue.ISP1Data.PandF2 = dr["MedicalEducationText2"].ToString();
                    returnValue.ISP1Data.PandF3 = dr["MedicalEducationText3"].ToString();
                    returnValue.ISP1Data.PandF4 = dr["MedicalEducationText4"].ToString();
                    returnValue.ISP1Data.PandF5 = dr["MedicalEducationText5"].ToString();
                    returnValue.ISP1Data.PandF6 = dr["MedicalEducationText6"].ToString();
                    returnValue.ISP1Data.PandF7 = dr["MedicalEducationText7"].ToString();
                    returnValue.ISP1Data.startPlanDate = DateTime.Parse(dr["ExecutionTimeSince"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.endPlanDate = DateTime.Parse(dr["ExecutionTimeUntil"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.ServiceDate1 = DateTime.Parse(dr["ParticipantDate1"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.Parent1 = dr["ParticipantParent1"].ToString();
                    returnValue.ISP1Data.Teacher1 = dr["ParticipantTeache1"].ToString();
                    returnValue.ISP1Data.Sociality1 = dr["ParticipantSocialWorker1"].ToString();
                    returnValue.ISP1Data.ListenTutor1 = dr["ParticipantAudiologist1"].ToString();
                    returnValue.ISP1Data.Manager1 = dr["ParticipantHead1"].ToString();
                    returnValue.ISP1Data.RelationalPeople1 = dr["ParticipantProfessionals1"].ToString();
                    returnValue.ISP1Data.ServiceDate2 = DateTime.Parse(dr["ParticipantDate2"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ISP1Data.Parent2 = dr["ParticipantParent2"].ToString();
                    returnValue.ISP1Data.Teacher2 = dr["ParticipantTeache2"].ToString();
                    returnValue.ISP1Data.Sociality2 = dr["ParticipantSocialWorker2"].ToString();
                    returnValue.ISP1Data.ListenTutor2 = dr["ParticipantAudiologist2"].ToString();
                    returnValue.ISP1Data.Manager2 = dr["ParticipantHead2"].ToString();
                    returnValue.ISP1Data.RelationalPeople2 = dr["ParticipantProfessionals2"].ToString();

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.ISP1Data = new setTeachISP1();
                returnValue.ISP1Data.studentID = "-1";
                returnValue.ISP1Data.studentName = e.Message.ToString();
            }
        }
        return returnValue;
    }

    public int setTeachISPPage1(setTeachISP1 StudentISP)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE CaseISPstate SET PhysicalAndMentalDisabilityHandbook=@PhysicalAndMentalDisabilityHandbook, DisabilityCategory1=@DisabilityCategory1, "+
                    "DisabilityGrade1=@DisabilityGrade1, DisabilityCategory2=@DisabilityCategory2, DisabilityGrade2=@DisabilityGrade2, DisabilityCategory3=@DisabilityCategory3, "+
                    "DisabilityGrade3=@DisabilityGrade3, NoDisabilityHandbook=@NoDisabilityHandbook, ApplyDisabilityHandbook=@ApplyDisabilityHandbook, "+
                    "DisabilityProve=@DisabilityProve, AidsManagement=@AidsManagement, AidsManagementTextAge=@AidsManagementTextAge, HearingAids_R=@HearingAids_R, "+
                    "AidsBrand_R=@AidsBrand_R, AidsOptionalTime_R=@AidsOptionalTime_R, AidsOptionalLocation_R=@AidsOptionalLocation_R, EEarHospital_R=@EEarHospital_R, "+
                    "EEarImplants_R=@EEarImplants_R, EEarOpen_R=@EEarOpen_R, HearingAids_L=@HearingAids_L, AidsBrand_L=@AidsBrand_L, AidsOptionalTime_L=@AidsOptionalTime_L, "+
                    "AidsOptionalLocation_L=@AidsOptionalLocation_L, EEarHospital_L=@EEarHospital_L, EEarImplants_L=@EEarImplants_L, EEarOpen_L=@EEarOpen_L, "+
                    "AcceptClass=@AcceptClass, MedicalEducation=@MedicalEducation, MedicalEducationText1=@MedicalEducationText1, MedicalEducationText2=@MedicalEducationText2, "+
                    "MedicalEducationText3=@MedicalEducationText3, MedicalEducationText4=@MedicalEducationText4, MedicalEducationText5=@MedicalEducationText5, "+
                    "MedicalEducationText6=@MedicalEducationText6, MedicalEducationText7=@MedicalEducationText7,  ExecutionTimeSince=@ExecutionTimeSince, " +
                    "ExecutionTimeUntil=@ExecutionTimeUntil, ParticipantDate1=@ParticipantDate1, ParticipantParent1=@ParticipantParent1, ParticipantTeache1=@ParticipantTeache1, "+
                    "ParticipantSocialWorker1=@ParticipantSocialWorker1, ParticipantAudiologist1=@ParticipantAudiologist1, ParticipantHead1=@ParticipantHead1, "+
                    "ParticipantProfessionals1=@ParticipantProfessionals1, ParticipantDate2=@ParticipantDate2, ParticipantParent2=@ParticipantParent2, "+
                    "ParticipantTeache2=@ParticipantTeache2, ParticipantSocialWorker2=@ParticipantSocialWorker2, ParticipantAudiologist2=@ParticipantAudiologist2, "+
                    "ParticipantHead2=@ParticipantHead2, ParticipantProfessionals2=@ParticipantProfessionals2 "+
                    "WHERE ID=@ID AND StudentID=@StudentID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentISP.Column;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentISP.studentID;
                cmd.Parameters.Add("@PhysicalAndMentalDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.manualWhether);
                cmd.Parameters.Add("@DisabilityCategory1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualCategory1);
                cmd.Parameters.Add("@DisabilityGrade1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualGrade1);
                cmd.Parameters.Add("@DisabilityCategory2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualCategory2);
                cmd.Parameters.Add("@DisabilityGrade2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualGrade2);
                cmd.Parameters.Add("@DisabilityCategory3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualCategory3);
                cmd.Parameters.Add("@DisabilityGrade3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualGrade3);
                cmd.Parameters.Add("@NoDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.manualNo);
                cmd.Parameters.Add("@ApplyDisabilityHandbook", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.manualUnit);
                cmd.Parameters.Add("@DisabilityProve", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.studentManualImg);
                cmd.Parameters.Add("@AidsManagement", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.assistmanage);
                cmd.Parameters.Add("@AidsManagementTextAge", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.Accessory);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.BrandR1);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.BuyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.BuyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.InsertHospitalR);
                cmd.Parameters.Add("@EEarImplants_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.InsertDateR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.OpenHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentISP.BrandL1);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.BuyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.BuyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.InsertHospitalL);
                cmd.Parameters.Add("@EEarImplants_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.InsertDateL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.OpenHzDateL);
                cmd.Parameters.Add("@AcceptClass", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.edu);
                cmd.Parameters.Add("@MedicalEducation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.edu1);
                cmd.Parameters.Add("@MedicalEducationText1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF1);
                cmd.Parameters.Add("@MedicalEducationText2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF2);
                cmd.Parameters.Add("@MedicalEducationText3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF3);
                cmd.Parameters.Add("@MedicalEducationText4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF4);
                cmd.Parameters.Add("@MedicalEducationText5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF5);
                cmd.Parameters.Add("@MedicalEducationText6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF6);
                cmd.Parameters.Add("@MedicalEducationText7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.PandF7);
                cmd.Parameters.Add("@ExecutionTimeSince", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                cmd.Parameters.Add("@ExecutionTimeUntil", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.endPlanDate);
                cmd.Parameters.Add("@ParticipantDate1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.ServiceDate1);
                cmd.Parameters.Add("@ParticipantParent1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.Parent1);
                cmd.Parameters.Add("@ParticipantTeache1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Teacher1);
                cmd.Parameters.Add("@ParticipantSocialWorker1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Sociality1);
                cmd.Parameters.Add("@ParticipantAudiologist1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.ListenTutor1);
                cmd.Parameters.Add("@ParticipantHead1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Manager1);
                cmd.Parameters.Add("@ParticipantProfessionals1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.RelationalPeople1);
                cmd.Parameters.Add("@ParticipantDate2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.ServiceDate2); ;
                cmd.Parameters.Add("@ParticipantParent2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.Parent2);
                cmd.Parameters.Add("@ParticipantTeache2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Teacher2);
                cmd.Parameters.Add("@ParticipantSocialWorker2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Sociality2);
                cmd.Parameters.Add("@ParticipantAudiologist2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.ListenTutor2);
                cmd.Parameters.Add("@ParticipantHead2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentISP.Manager2);
                cmd.Parameters.Add("@ParticipantProfessionals2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentISP.RelationalPeople2);
                cmd.Parameters.Add("@PlanWriter1", SqlDbType.Int).Value = HttpContext.Current.User.Identity.Name;
                cmd.Parameters.Add("@PlanWriteFrameDate1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                cmd.Parameters.Add("@PlanWriter2", SqlDbType.Int).Value = HttpContext.Current.User.Identity.Name;
                cmd.Parameters.Add("@PlanWriteFrameDate2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                cmd.Parameters.Add("@PlanWriter3", SqlDbType.Int).Value = HttpContext.Current.User.Identity.Name;
                cmd.Parameters.Add("@PlanWriteFrameDate3", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentISP.startPlanDate);
                returnValue = cmd.ExecuteNonQuery();
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }

    public int setTeachISPPage2(setTeachISP2 StudentISP)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE CaseISPstate SET PlanWriter1=@PlanWriter1, PlanWriteFrameDate1=@PlanWriteFrameDate1, PlanWriteExecutor1=@PlanWriteExecutor1, "+
                    "PlanRevise1=@PlanRevise1, PlanReviseDate1=@PlanReviseDate1, PlanReviseExecutor1=@PlanReviseExecutor1, EconomicNeed=@EconomicNeed, " +
                    "EconomicNeedText=@EconomicNeedText, EconomicNeedResource=@EconomicNeedResource, EconomicNeedMode=@EconomicNeedMode, " +
                    "EconomicNeedExecutionTime=@EconomicNeedExecutionTime, EconomicNeedExecutor=@EconomicNeedExecutor, EconomicNeedTrackDate=@EconomicNeedTrackDate, " +
                    "EconomicNeedResults=@EconomicNeedResults, EconomicNeedSituation=@EconomicNeedSituation, Services=@Services, ServicesText=@ServicesText, " +
                    "ServicesResource=@ServicesResource, ServicesMode=@ServicesMode, ServicesExecutionTime=@ServicesExecutionTime, ServicesExecutor=@ServicesExecutor, " +
                    "ServicesTrackDate=@ServicesTrackDate, ServicesResults=@ServicesResults, ServicesSituation=@ServicesSituation, ServicesSituationText=@ServicesSituationText, " +
                    "Medical=@Medical, MedicalText=@MedicalText, MedicalResource=@MedicalResource, MedicalMode=@MedicalMode, MedicalExecutionTime=@MedicalExecutionTime, " +
                    "MedicalExecutor=@MedicalExecutor, MedicalTrackDate=@MedicalTrackDate, MedicalResults=@MedicalResults, MedicalSituation=@MedicalSituation, " +
                    "MedicalSituationText=@MedicalSituationText, Education=@Education, EducationText=@EducationText, EducationResource=@EducationResource, EducationMode=@EducationMode, " +
                    "EducationExecutionTime=@EducationExecutionTime, EducationExecutor=@EducationExecutor, EducationTrackDate=@EducationTrackDate, EducationResults=@EducationResults, " +
                    "EducationSituation=@EducationSituation, EducationSituationText=@EducationSituationText " +
                    "WHERE ID=@ID AND StudentID=@StudentID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = "";
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = StudentISP.studentID;
                cmd.Parameters.Add("@PlanWriter1", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@PlanWriteFrameDate1", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@PlanWriteExecutor1", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@PlanRevise1", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@PlanReviseDate1", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@PlanReviseExecutor1", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@EconomicNeed", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedText", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedResource", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedMode", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedExecutionTime", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedExecutor", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedTrackDate", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedResults", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EconomicNeedSituation", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Services", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesText", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesResource", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesMode", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesExecutionTime", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesExecutor", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesTrackDate", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesResults", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesSituation", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ServicesSituationText", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Medical", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalText", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalResource", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalMode", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalExecutionTime", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalExecutor", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalTrackDate", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalResults", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalSituation", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@MedicalSituationText", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Education", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationText", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationResource", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationMode", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationExecutionTime", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationExecutor", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationTrackDate", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationResults", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationSituation", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EducationSituationText", SqlDbType.NVarChar).Value = "";
                returnValue = cmd.ExecuteNonQuery();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }
    public int setTeachISPPage3(setTeachISP3 StudentISP)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE CaseISPstate SET PlanWriter2=@PlanWriter2, PlanWriteFrameDate2=@PlanWriteFrameDate2, PlanWriteExecutor2=@PlanWriteExecutor2, PlanRevise2=@PlanRevise2, " +
                    "PlanReviseDate2=@PlanReviseDate2, PlanReviseExecutor2=@PlanReviseExecutor2, AudiometryRight=@AudiometryRight, AudiometryLeft=@AudiometryLeft, " +
                    "Naked250L=@Naked250L, Naked250R=@Naked250R, Naked250=@Naked250, Naked500L=@Naked500L, Naked500R=@Naked500R, Naked500=@Naked500, Naked1000L=@Naked1000L, " +
                    "Naked1000R=@Naked1000R, Naked1000=@Naked1000, Naked2000L=@Naked2000L, Naked2000R=@Naked2000R, Naked2000=@Naked2000, Naked4000L=@Naked4000L, " +
                    "Naked4000R=@Naked4000R, Naked4000=@Naked4000, Naked8000L=@Naked8000L, Naked8000R=@Naked8000R, Naked8000=@Naked8000, NakedAverageL=@NakedAverageL, " +
                    "NakedAverageR=@NakedAverageR, NakedAverage=@NakedAverage, After250L=@After250L, After250R=@After250R, After250=@After250, After500L=@After500L, " +
                    "After500R=@After500R, After500=@After500, After1000L=@After1000L, After1000R=@After1000R, After1000=@After1000, After2000L=@After2000L, " +
                    "After2000R=@After2000R, After2000=@After2000, After4000L=@After4000L, After4000R=@After4000R, After4000=@After4000, After8000L=@After8000L, " +
                    "After8000R=@After8000R, After8000=@After8000, AfterAverageL=@AfterAverageL, AfterAverageR=@AfterAverageR, AfterAverage=@AfterAverage, " +
                    "AudiometryOther=@AudiometryOther, AudiometryAssessmentBy=@AudiometryAssessmentBy, AudiometryAssessmentDate=@AudiometryAssessmentDate, " +
                    "AudiometryAssessmentScoring=@AudiometryAssessmentScoring, Hearing1State=@Hearing1State, Hearing1Explain=@Hearing1Explain, Hearing2State=@Hearing2State, " +
                    "Hearing2Explain=@Hearing2Explain, Hearing3State=@Hearing3State, Hearing3Explain=@Hearing3Explain, Hearing4State=@Hearing4State, " +
                    "Hearing4Explain=@Hearing4Explain, HearingAssessmentBy=@HearingAssessmentBy, HearingAssessmentDate=@HearingAssessmentDate, " +
                    "HearingAssessmentScoring1=@HearingAssessmentScoring1, HearingAssessmentScoring2=@HearingAssessmentScoring2, " +
                    "HearingAssessmentScoring3=@HearingAssessmentScoring3, HearingAssessmentScoring4=@HearingAssessmentScoring4, " +
                    "AidsState1=@AidsState1, AidsState2=@AidsState2, AidsState3=@AidsState3, AidsState4=@AidsState4, AidsState5=@AidsState5, " +
                    "AidsAssessmentBy=@AidsAssessmentBy, AidsAssessmentDate=@AidsAssessmentDate, AidsAssessmentScoring=@AidsAssessmentScoring, Summary=@Summary " +
                    "WHERE ID=@ID AND StudentID=@StudentID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentISP.Column;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@PlanWriter2", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PlanWriteFrameDate2", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@PlanWriteExecutor2", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PlanRevise2", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PlanReviseDate2", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@PlanReviseExecutor2", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@AudiometryRight", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AudiometryLeft", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked250L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked250R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked250", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked500L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked500R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked500", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked1000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked1000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked1000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked2000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked2000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked2000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked4000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked4000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked4000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked8000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked8000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Naked8000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@NakedAverageL", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@NakedAverageR", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@NakedAverage", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After250L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After250R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After250", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After500L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After500R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After500", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After1000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After1000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After1000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After2000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After2000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After2000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After4000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After4000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After4000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After8000L", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After8000R", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@After8000", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AfterAverageL", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AfterAverageR", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AfterAverage", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AudiometryOther", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@AudiometryAssessmentBy", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AudiometryAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@AudiometryAssessmentScoring", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Hearing1State", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Hearing1Explain", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Hearing2State", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Hearing2Explain", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Hearing3State", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Hearing3Explain", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Hearing4State", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Hearing4Explain", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@HearingAssessmentBy", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@HearingAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@HearingAssessmentScoring1", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@HearingAssessmentScoring2", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@HearingAssessmentScoring3", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@HearingAssessmentScoring4", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@AidsState1", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AidsState2", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AidsState3", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AidsState4", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AidsState5", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AidsAssessmentBy", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@AidsAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@AidsAssessmentScoring", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Summary", SqlDbType.NVarChar).Value = "";
                returnValue = cmd.ExecuteNonQuery();
                int LCount=Chk.CheckIntFunction(StudentISP.HISP.Count);
                for (int i = 0; i < LCount; i++)
                {
                    Int64 LColumn = 0;
                    Int64 SColumn = 0;
                    //長目標有資料，Update
                    if (StudentISP.HISP[i].LongColumn != null && StudentISP.HISP[i].LongColumn != 0) 
                    {
                        LColumn = StudentISP.HISP[i].LongColumn;
                        sql = "UPDATE HearingISPLong SET TargetContentLong=@TargetContentLong WHERE CaseISPID=@CaseISPID AND HISPLID=@HISPLID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@HISPLID", SqlDbType.BigInt).Value = LColumn;
                        cmd.Parameters.Add("@CaseISPID", SqlDbType.BigInt).Value = "";
                        cmd.Parameters.Add("@TargetContentLong", SqlDbType.NVarChar).Value = "";
                        returnValue = cmd.ExecuteNonQuery();

                        int SCount = Chk.CheckIntFunction(StudentISP.HISP[i].ShortTarget.Count);
                        for (int j = 0; j < SCount; j++)
                        {
                            //短目標有資料，Update
                            if (StudentISP.HISP[i].ShortTarget[j].ShortColumn != null && StudentISP.HISP[i].ShortTarget[j].ShortColumn != 0) 
                            {
                                SColumn = StudentISP.HISP[i].ShortTarget[j].ShortColumn;
                                sql = "UPDATE HearingISPShort SET HISPLID=@HISPLID, TargetContentShort=@TargetContentShort, DateStart=@DateStart, DateEnd=@DateEnd, " +
                                    "EffectiveDate=@EffectiveDate, EffectiveMode=@EffectiveMode, EffectiveResult=@EffectiveResult, Decide=@Decide " +
                                    "WHERE HISPLID=@HISPLID AND HISPSID=@HISPSID ";
                                cmd = new SqlCommand(sql, Sqlconn);
                                cmd.Parameters.Add("@HISPLID", SqlDbType.BigInt).Value = LColumn;
                                cmd.Parameters.Add("@HISPSID", SqlDbType.BigInt).Value = SColumn;
                                cmd.Parameters.Add("@TargetContentShort", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@DateStart", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@DateEnd", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@EffectiveDate", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@EffectiveMode", SqlDbType.Int).Value = "";
                                cmd.Parameters.Add("@EffectiveResult", SqlDbType.Int).Value = "";
                                cmd.Parameters.Add("@Decide", SqlDbType.Int).Value = "";
                                returnValue = cmd.ExecuteNonQuery();
                            }
                            //短目標無資料，create
                            else {
                                returnValue = this.createHISPShort(LColumn);
                            }
                        }
                    }
                    else
                    {  //長目標無資料，create
                         LColumn =this.createHISPLong(StudentISP.Column);
                         returnValue = this.createHISPShort(LColumn);
                    }
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }
    private Int64 createHISPLong(Int64 CaseISPID)
    {
        Int64 returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO HearingISPLong(Domain, CaseISPID, TargetContentLong) VALUES (@Domain, @CaseISPID, @TargetContentLong)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Domain", SqlDbType.TinyInt).Value = "";
                cmd.Parameters.Add("@CaseISPID", SqlDbType.BigInt).Value = "";
                cmd.Parameters.Add("@TargetContentLong", SqlDbType.NVarChar).Value = "";
                returnValue = cmd.ExecuteNonQuery();

                if (returnValue != 0)
                {
                    sql = "select IDENT_CURRENT('HearingISPLong') AS ISPLong";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue = Int64.Parse(dr["ISPLong"].ToString());
                    }
                    dr.Close();
                }

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }

    private int createHISPShort(Int64 LID)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO HearingISPShort(HISPLID, TargetContentShort, DateStart, DateEnd, EffectiveDate, EffectiveMode, EffectiveResult, Decide) " +
                    "VALUES (@HISPLID,@TargetContentShort,@DateStart,@DateEnd,@EffectiveDate,@EffectiveMode,@EffectiveResult,@Decide)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@HISPLID", SqlDbType.BigInt).Value = LID;
                cmd.Parameters.Add("@TargetContentShort", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@DateStart", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@DateEnd", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@EffectiveDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@EffectiveMode", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@EffectiveResult", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Decide", SqlDbType.Int).Value = "";
                returnValue = cmd.ExecuteNonQuery();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }
    public int setTeachISPPage4(setTeachISP4 StudentISP)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE CaseISPstate SET PlanWriter3=@PlanWriter3, PlanWriteFrameDate3=@PlanWriteFrameDate3, PlanWriteExecutor3=@PlanWriteExecutor3, " +
                    "PlanRevise3=@PlanRevise3, PlanReviseDate3=@PlanReviseDate3, PlanReviseExecutor3=@PlanReviseExecutor3, HearingAssessment=@HearingAssessment, " +
                    "HearingAssessmentBy=@HearingAssessmentBy, HearingAssessmentDate=@HearingAssessmentDate, HearingAssessmentTool=@HearingAssessmentTool, " +
                    "VocabularyAssessment=@VocabularyAssessment, VocabularyAssessmentBy=@VocabularyAssessmentBy, VocabularyAssessmentDate=@VocabularyAssessmentDate, " +
                    "VocabularyAssessmentTool=@VocabularyAssessmentTool, LanguageAssessment=@LanguageAssessment, LanguageAssessmentBy=@LanguageAssessmentBy, " +
                    "LanguageAssessmentDate=@LanguageAssessmentDate, LanguageAssessmentTool=@LanguageAssessmentTool, intelligenceAssessment=@intelligenceAssessment, " +
                    "intelligenceAssessmentBy=@intelligenceAssessmentBy, intelligenceAssessmentDate=@intelligenceAssessmentDate, " +
                    "intelligenceAssessmentTool=@intelligenceAssessmentTool, OtherAssessment=@OtherAssessment, OtherAssessmentBy=@OtherAssessmentBy, " +
                    "OtherAssessmentDate=@OtherAssessmentDate, OtherAssessmentTool=@OtherAssessmentTool, Hearing=@Hearing, CognitiveAbility=@CognitiveAbility, " +
                    "ConnectAbility=@ConnectAbility, ActAbility=@ActAbility, Relationship=@Relationship, EmotionalManagement=@EmotionalManagement, " +
                    "SensoryFunction=@SensoryFunction, HealthState=@HealthState, DailyLiving=@DailyLiving, LearningAchievement=@LearningAchievement, " +
                    "Advantage=@Advantage, WeakCapacity=@WeakCapacity " +
                     "WHERE ID=@ID AND StudentID=@StudentID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = "";
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@PlanWriter3", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PlanWriteFrameDate3", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@PlanWriteExecutor3", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PlanRevise3", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@PlanReviseDate3", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@PlanReviseExecutor3", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@HearingAssessment", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@HearingAssessmentBy", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@HearingAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@HearingAssessmentTool", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@VocabularyAssessment", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@VocabularyAssessmentBy", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@VocabularyAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@VocabularyAssessmentTool", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@LanguageAssessment", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@LanguageAssessmentBy", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@LanguageAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@LanguageAssessmentTool", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@IntelligenceAssessment", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@IntelligenceAssessmentBy", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@IntelligenceAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@IntelligenceAssessmentTool", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@OtherAssessment", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@OtherAssessmentBy", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@OtherAssessmentDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@OtherAssessmentTool", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Hearing", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@CognitiveAbility", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ConnectAbility", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@ActAbility", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Relationship", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@EmotionalManagement", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@SensoryFunction", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@HealthState", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@DailyLiving", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@LearningAchievement", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@Advantage", SqlDbType.NVarChar).Value = "";
                cmd.Parameters.Add("@WeakCapacity", SqlDbType.NVarChar).Value = "";
                returnValue = cmd.ExecuteNonQuery();

                int LCount = Chk.CheckIntFunction(StudentISP.TISP.Count);
                for (int i = 0; i < LCount; i++)
                {
                    Int64 LColumn = 0;
                    Int64 SColumn = 0;
                    //長目標有資料，Update
                    if (StudentISP.TISP[i].LongColumn != null && StudentISP.TISP[i].LongColumn != 0)
                    {
                        LColumn = StudentISP.TISP[i].LongColumn;
                        sql = "UPDATE TeacherISPLong SET TargetContentLong=@TargetContentLong WHERE CaseISPID=@CaseISPID  AND TISPLID=@TISPLID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TISPLID", SqlDbType.BigInt).Value = LColumn;
                        cmd.Parameters.Add("@CaseISPID", SqlDbType.BigInt).Value = "";
                        cmd.Parameters.Add("@TargetContentLong", SqlDbType.NVarChar).Value = "";
                        returnValue = cmd.ExecuteNonQuery();

                        int SCount = Chk.CheckIntFunction(StudentISP.TISP[i].ShortTarget.Count);
                        for (int j = 0; j < SCount; j++)
                        {
                            //短目標有資料，Update
                            if (StudentISP.TISP[i].ShortTarget[j].ShortColumn != null && StudentISP.TISP[i].ShortTarget[j].ShortColumn != 0)
                            {
                                SColumn = StudentISP.TISP[i].ShortTarget[j].ShortColumn;
                                sql = "UPDATE TeacherISPShort SET HISPLID=@HISPLID, TargetContentShort=@TargetContentShort, DateStart=@DateStart, DateEnd=@DateEnd, " +
                                    "EffectiveDate=@EffectiveDate, EffectiveMode=@EffectiveMode, EffectiveResult=@EffectiveResult, Decide=@Decide " +
                                    "WHERE TISPLID=@TISPLID AND TISPSID=@TISPSID ";
                                cmd = new SqlCommand(sql, Sqlconn);
                                cmd.Parameters.Add("@TISPLID", SqlDbType.BigInt).Value = LColumn;
                                cmd.Parameters.Add("@TISPSID", SqlDbType.BigInt).Value = SColumn;
                                cmd.Parameters.Add("@TargetContentShort", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@DateStart", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@DateEnd", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@EffectiveDate", SqlDbType.Date).Value = "";
                                cmd.Parameters.Add("@EffectiveMode", SqlDbType.Int).Value = "";
                                cmd.Parameters.Add("@EffectiveResult", SqlDbType.Int).Value = "";
                                cmd.Parameters.Add("@Decide", SqlDbType.Int).Value = "";
                                returnValue = cmd.ExecuteNonQuery();
                            }
                            //短目標無資料，create
                            else
                            {
                                returnValue = this.createTISPShort(LColumn);
                            }
                        }
                    }
                    else
                    {  //長目標無資料，create
                        LColumn = this.createTISPLong(StudentISP.Column);
                        returnValue = this.createTISPShort(LColumn);
                    }
                }
                  Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }


    private Int64 createTISPLong(Int64 CaseISPID)
    {
        Int64 returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO TeacherISPLong(Domain, CaseISPID, TargetContentLong) VALUES (@Domain, @CaseISPID, @TargetContentLong)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Domain", SqlDbType.TinyInt).Value = "";
                cmd.Parameters.Add("@CaseISPID", SqlDbType.BigInt).Value = "";
                cmd.Parameters.Add("@TargetContentLong", SqlDbType.NVarChar).Value = "";
                returnValue = cmd.ExecuteNonQuery();

                if (returnValue != 0)
                {
                    sql = "select IDENT_CURRENT('TeacherISPLong') AS ISPLong";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue = Int64.Parse(dr["ISPLong"].ToString());
                    }
                    dr.Close();
                }

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }

    private int createTISPShort(Int64 LID)
    {
        int returnValue = 0;
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO TeacherISPShort(TISPLID, TargetContentShort, DateStart, DateEnd, EffectiveDate, EffectiveMode, EffectiveResult, Decide) " +
                    "VALUES (@TISPLID,@TargetContentShort,@DateStart,@DateEnd,@EffectiveDate,@EffectiveMode,@EffectiveResult,@Decide)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@TISPLID", SqlDbType.BigInt).Value = LID;
                cmd.Parameters.Add("@TargetContentShort", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@DateStart", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@DateEnd", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@EffectiveDate", SqlDbType.Date).Value = "";
                cmd.Parameters.Add("@EffectiveMode", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@EffectiveResult", SqlDbType.Int).Value = "";
                cmd.Parameters.Add("@Decide", SqlDbType.Int).Value = "";
                returnValue = cmd.ExecuteNonQuery();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue = -1;
            }
        }
        return returnValue;
    }
    public int SearchTeachISPCount(SearchStudentISP searchISPData) {
        int returnValue = 0;
        string DateBase = "1900-01-01";
        DataBase Base = new DataBase();
        int Count = 0;
        string LimitsID = "";
        if (searchISPData.txtstudentID != null)
        {
            LimitsID = " AND StudentDatabase.StudentID=(@StudentID) ";
        }
        string LimitsName = "";
        if (searchISPData.txtstudentName != null)
        {
            LimitsName = " AND StudentDatabase.StudentName like (@StudentName) ";
        }
        string LimitsSex = "";
        int sSex = 0;
        if (searchISPData.txtstudentSex != null && searchISPData.txtstudentSex != "0")
        {
            LimitsSex = " AND StudentDatabase.StudentSex=(@StudentSex) ";
            sSex = int.Parse(searchISPData.txtstudentSex);
        }
        string LimitsBirthday = "";
        DateTime sBirthdaystart = new DateTime(1900, 01, 01);
        DateTime sBirthdayend = new DateTime(1900, 01, 01);
        if (searchISPData.txtbirthdaystart != null && searchISPData.txtbirthdayend != null && searchISPData.txtbirthdaystart != DateBase && searchISPData.txtbirthdayend != DateBase)
        {
            LimitsBirthday = " AND StudentDatabase.StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
            sBirthdaystart = DateTime.Parse(searchISPData.txtbirthdaystart);
            sBirthdayend = DateTime.Parse(searchISPData.txtbirthdayend);
        }
        string LimitsTeachName = "";
        if (searchISPData.txtteachername != null )
        {
            LimitsTeachName = " AND CaseStatu like (@CaseStatu) ";
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM CaseISPstate INNER JOIN StudentDatabase ON CaseISPstate.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 "+
                            LimitsID + LimitsName + LimitsSex + LimitsBirthday + LimitsTeachName +
                            "WHERE CaseISPstate.isDeleted=0 AND StudentDatabase.isDeleted=0";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchISPData.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = searchISPData.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(searchISPData.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = sBirthdaystart;
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = sBirthdayend;
                returnValue = (int)cmd.ExecuteScalar();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string item = e.Message.ToString();
                returnValue=-1;
            }
        }
        return returnValue;
    }

    public List<SearchStudentResult> SearchTeachISP(int indexpage, SearchStudentISP searchISPData)
    {
        List<SearchStudentResult> returnValue = new List<SearchStudentResult>();
        
        string DateBase = "1900-01-01";
        DataBase Base = new DataBase();
        int Count = 0;
        string LimitsID = "";
        if (searchISPData.txtstudentID != null)
        {
            LimitsID = " AND StudentDatabase.StudentID=(@StudentID) ";
        }
        string LimitsName = "";
        if (searchISPData.txtstudentName != null)
        {
            LimitsName = " AND StudentDatabase.StudentName like (@StudentName) ";
        }
        string LimitsSex = "";
        int sSex = 0;
        if (searchISPData.txtstudentSex != null && searchISPData.txtstudentSex != "0")
        {
            LimitsSex = " AND StudentDatabase.StudentSex=(@StudentSex) ";
            sSex = int.Parse(searchISPData.txtstudentSex);
        }
        string LimitsBirthday = "";
        DateTime sBirthdaystart = new DateTime(1900, 01, 01);
        DateTime sBirthdayend = new DateTime(1900, 01, 01);
        if (searchISPData.txtbirthdaystart != null && searchISPData.txtbirthdayend != null && searchISPData.txtbirthdaystart != DateBase && searchISPData.txtbirthdayend != DateBase)
        {
            LimitsBirthday = " AND StudentDatabase.StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
            sBirthdaystart = DateTime.Parse(searchISPData.txtbirthdaystart);
            sBirthdayend = DateTime.Parse(searchISPData.txtbirthdayend);
        }
        string LimitsTeachName = "";
        if (searchISPData.txtteachername != null)
        {
            LimitsTeachName = " AND CaseStatu like (@CaseStatu) ";
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY CaseISPstate.ID DESC) " +
                 "AS RowNum, CaseISPstate.*, StudentDatabase.StudentName, StudentDatabase.StudentSex, StudentDatabase.StudentBirthday, StudentDatabase.ContactName2, StudentDatabase.ContactPhone2, StudentDatabase.ContactTel_home2 " +
                 "FROM CaseISPstate INNER JOIN StudentDatabase ON CaseISPstate.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                            LimitsID + LimitsName + LimitsSex + LimitsBirthday + LimitsTeachName + " WHERE CaseISPstate.isDeleted=0 AND StudentDatabase.isDeleted=0 ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchISPData.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = searchISPData.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(searchISPData.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = sBirthdaystart;
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = sBirthdayend;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchStudentResult addValue = new SearchStudentResult();
                    addValue.ID = Int64.Parse(dr["ID"].ToString());
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtLegalRepresentative = dr["ContactName2"].ToString();
                    addValue.txtLegalRepresentativePhone = dr["ContactPhone2"].ToString();
                    addValue.txtLegalRepresentativeTel = dr["ContactTel_home2"].ToString();
                    addValue.txtstudentSex = int.Parse(dr["StudentSex"].ToString());
                    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string item = e.Message.ToString();
                //returnValue = -1;
            }
        }
        return returnValue;
    }
}