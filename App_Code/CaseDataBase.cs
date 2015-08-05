using System;
using System.Data;
using System.Collections.Generic;
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
using System.Data;

public struct OneCaseData { 
   public DateTime AssessDate;
    public DateTime ConsultationDate;
    public DateTime UpDate;
    public string FileName;
    public int Unit;
    public int StuID;
    public string StuName;
    public string StuTWID;
    public string censusAddressZip;
    public string censusAddressCity;
    public string censusAddress;
    public string AddressZip;
    public string AddressCity;
    public string Address;
    public DateTime StuBirthday;
    public int StuSex;
    public DateTime FirstClassDate;
    public DateTime GuaranteeDate;
    public DateTime EndReasonDate;
    public int EndReasonType;
    public string EndReason;
    public DateTime SendDateSince;
    public DateTime SendDateUntil;
    public int NomembershipType;
    public string NomembershipReason;
    public string StuPhoto;
    public List<string[]> PrimaryContact;
    public string StuEmail;
    public int SourceType;
    public int SourceName;

}

/// <summary>
/// Summary description for CaseDataBase
/// </summary>
public class CaseDataBase
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
    public string[] _StaffhaveRoles = new string[5] { "0", "0", "0", "0", "0" };
    public CaseDataBase()
    {
        //
        // TODO: Add constructor logic here
        //
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.caseStu[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.caseStu[1];//跨區與否
    }
 
    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }

    public List<string> getStudentDataName(string sID)
    {
        
        List<string> returnValue = new List<string>();
        returnValue.Add(sID);
        returnValue.Add("");
        returnValue.Add("0");
        returnValue.Add("");
        if (sID.Length > 0)
        {
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    Sqlconn.Open();
                    string sql = "SELECT StudentID,StudentName,Unit,StudentAvatar FROM StudentDatabase WHERE StudentID=(@StudentID)";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = sID;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["StudentName"].ToString();
                        returnValue[2] = dr["Unit"].ToString();
                        returnValue[3] = dr["StudentAvatar"].ToString();
                    }
                    dr.Close();
                    Sqlconn.Close();
                }
                catch (Exception e)
                {
                    string item = e.Message;
                    returnValue[0] = "-1";
                    returnValue[1] = item;
                    returnValue[2] = "0";
                    returnValue[3] = "";
                }

            }
        }
        return returnValue;
    }
    public List<SearchStudentResult> getAllStudentDataList(int item)
    {
        List<SearchStudentResult> returnValue = new List<SearchStudentResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = "";
        if (item != 0)
        {
            ConditionReturn = " AND CaseStatu=(@CaseStatu) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND Unit=@Unit" + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = item;
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchStudentResult addValue = new SearchStudentResult();
                    addValue.ID = int.Parse(dr["ID"].ToString());
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtstudentStatu = dr["CaseStatu"].ToString();
                    /*addValue.txtLegalRepresentative = dr["ContactName2"].ToString();
                    addValue.txtLegalRepresentativePhone = dr["ContactPhone2"].ToString();
                    addValue.txtLegalRepresentativeTel = dr["ContactTel_home2"].ToString();
                    addValue.txtstudentSex = int.Parse(dr["StudentSex"].ToString());
                    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());*/
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchStudentResult addValue = new SearchStudentResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public List<string> SearchStudent(string SearchString)
    {
        List<string> returnValue = new List<string>();
        string atom = "";

        DataBase Base = new DataBase();
       
        
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT top 10 StudentID,StudentName FROM StudentDatabase WHERE isDeleted=0 AND StudentID like  '%' + @StudentID + '%' or " +
                    "StudentName like '%' + @StudentName + '%'";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = SearchString;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchString;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    atom = dr["StudentName"].ToString() + "(" + dr["StudentID"].ToString() + ")";
                   
                    returnValue.Add(atom);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                
            }
        }
        return returnValue;
    }
    private string SearchStudentConditionReturn(SearchStudent SearchStructure, int type)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND StudentID=(@StudentID) ";
        }
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtstudentSex != null && SearchStructure.txtstudentSex != "0")
        {
            ConditionReturn += " AND StudentSex=(@StudentSex) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }

        if (SearchStructure.txtcaseStatu != null && SearchStructure.txtcaseStatu != "0")
        {
            ConditionReturn += " AND CaseStatu =(@CaseStatu) ";
        }
        if (SearchStructure.txtjoindaystart != null && SearchStructure.txtjoindayend != null && SearchStructure.txtjoindaystart != DateBase && SearchStructure.txtjoindayend != DateBase)
        {
            ConditionReturn += " AND GuaranteeDate BETWEEN (@sGuaranteeDateStart) AND (@sGuaranteeDateEnd) ";
        }
        if (SearchStructure.txtendReasonDatestart != null && SearchStructure.txtendReasonDateend != null && SearchStructure.txtendReasonDatestart != DateBase && SearchStructure.txtendReasonDateend != DateBase)
        {
            ConditionReturn += " AND CompletedDate BETWEEN (@sCompletedDateStart) AND (@sCompletedDateEnd) ";
        }
        if (SearchStructure.txtendReasonType != null && SearchStructure.txtendReasonType != "0")
        {
            ConditionReturn += " AND CompletedType =(@CompletedType) ";
        }
        if (SearchStructure.txtnomembershipType != null && SearchStructure.txtnomembershipType != "0")
        {
            ConditionReturn += " AND NomembershipType =(@NomembershipType) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0  && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        if (type == 0)
        {
            ConditionReturn += " AND CaseStatu2 =" + type + " ";
        }
        return ConditionReturn;
    }
    private string SearchHearingLossPreschoolCondition(SearchStudent SearchStructure, int type)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND StudentID=(@StudentID) ";
        }
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtstudentSex != null && SearchStructure.txtstudentSex != "0")
        {
            ConditionReturn += " AND StudentSex=(@StudentSex) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }

        if (SearchStructure.txtcaseStatu != null && SearchStructure.txtcaseStatu != "0")
        {
            ConditionReturn += " AND CaseStatu =(@CaseStatu) ";
        }
        if (SearchStructure.txtjoindaystart != null && SearchStructure.txtjoindayend != null && SearchStructure.txtjoindaystart != DateBase && SearchStructure.txtjoindayend != DateBase)
        {
            ConditionReturn += " AND GuaranteeDate BETWEEN (@sGuaranteeDateStart) AND (@sGuaranteeDateEnd) ";
        }
        if (SearchStructure.txtendReasonDatestart != null && SearchStructure.txtendReasonDateend != null && SearchStructure.txtendReasonDatestart != DateBase && SearchStructure.txtendReasonDateend != DateBase)
        {
            ConditionReturn += " AND CompletedDate BETWEEN (@sCompletedDateStart) AND (@sCompletedDateEnd) ";
        }
        if (SearchStructure.txtendReasonType != null && SearchStructure.txtendReasonType != "0")
        {
            ConditionReturn += " AND CompletedType =(@CompletedType) ";
        }
        if (SearchStructure.txtnomembershipType != null && SearchStructure.txtnomembershipType != "0")
        {
            ConditionReturn += " AND NomembershipType =(@NomembershipType) ";
        }
        //if (SearchStructure.txtAcademicYearstart != null && SearchStructure.txtAcademicYearend != null && SearchStructure.txtAcademicYearstart != DateBase && SearchStructure.txtAcademicYearend != DateBase)
        //{
        //    ConditionReturn += " AND AcademicYear BETWEEN (@AcademicYearstart) AND (@AcademicYearend) "; // 教學管理使用 不知是否會與其他衝突
        //}
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        if (type == 0)
        {
            ConditionReturn += " AND CaseStatu2 =" + type + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchHearingLossPreschoolCount(SearchStudent SearchStructure, int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchHearingLossPreschoolCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM StudentDatabase WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value ="%"+ Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtcaseStatu);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sGuaranteeDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtjoindaystart);
                cmd.Parameters.Add("@sGuaranteeDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtjoindayend);
                cmd.Parameters.Add("@sCompletedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDatestart);
                cmd.Parameters.Add("@sCompletedDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDateend);
                cmd.Parameters.Add("@CompletedType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtendReasonType);
                cmd.Parameters.Add("@NomembershipType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtnomembershipType);
                returnValue[0] = cmd.ExecuteScalar().ToString();
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

    private string SearchAchievementAssessmentCondition(SearchStudent SearchStructure, int type)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtstudentSex != null && SearchStructure.txtstudentSex != "0")
        {
            ConditionReturn += " AND StudentSex=(@StudentSex) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStructure.txtAcademicYearstart != null && SearchStructure.txtAcademicYearend != null )
        {
            ConditionReturn += " AND AcademicYear BETWEEN (@AcademicYearstart) AND (@AcademicYearend) "; 
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND b.Unit =" + UserFile[2] + " ";
        }
        if (type == 0)
        {
            ConditionReturn += " AND b.CaseStatu2 =" + type + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchAchievementAssessmentCount(SearchStudent SearchStructure, int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAchievementAssessmentCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM AchievementAssessment a left join studentDatabase b on a.studentid = b.id  WHERE isnull(a.isDeleted,0) = 0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);  
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtcaseStatu);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@AcademicYearstart", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtAcademicYearstart);
                cmd.Parameters.Add("@AcademicYearend", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtAcademicYearend);
                returnValue[0] = cmd.ExecuteScalar().ToString();
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

    public List<SearchAchievementAssessment> SearchAchievementAssessment(int indexpage, SearchStudent SearchStructure, int type)
    {

        List<SearchAchievementAssessment> returnValue = new List<SearchAchievementAssessment>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAchievementAssessmentCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                //string sql = " SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StudentDatabase.StudentID DESC) " +
                // "AS RowNum, StudentDatabase.* " +
                // "FROM StudentDatabase WHERE isDeleted=0 " + ConditionReturn + " ) " +
                // "AS NewTable " +
                // "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";
                string sql = " SELECT * from (select ROW_NUMBER() OVER (ORDER BY a.AcademicYear DESC) as RowNum , a.ID , a.AcademicYear,a.StudentAge,a.StudentMonth , b.StudentName , b.StudentSex, b.StudentBirthday  ";
                sql += " FROM AchievementAssessment a left join studentDatabase b on a.studentid = b.id  WHERE isnull(a.isDeleted,0) = 0 " + ConditionReturn + ") AS NewTable ";

                sql += " where  RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";



                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@AcademicYearstart", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtAcademicYearstart);
                cmd.Parameters.Add("@AcademicYearend", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtAcademicYearend);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchAchievementAssessment addValue = new SearchAchievementAssessment();
                    addValue.RowNum = dr["rownum"].ToString();
                    addValue.txtAcademicYear = dr["AcademicYear"].ToString();
                    addValue.ID = int.Parse(dr["ID"].ToString());
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtstudentSex = int.Parse(dr["StudentSex"].ToString());
                    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    addValue.StudentAge = dr["StudentAge"].ToString();
                    addValue.StudentMonth = dr["StudentMonth"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchAchievementAssessment addValue = new SearchAchievementAssessment();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }



    public string[] SearchCaseStudyCount(SearchStudent SearchStructure, int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAchievementAssessmentCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM CaseStudy a left join studentDatabase b on a.studentid = b.id  WHERE isnull(a.isDeleted,0) = 0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtcaseStatu);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);

                returnValue[0] = cmd.ExecuteScalar().ToString();
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

    public List<SearchAchievementAssessment> SearchCaseStudy(int indexpage, SearchStudent SearchStructure, int type)
    {

        List<SearchAchievementAssessment> returnValue = new List<SearchAchievementAssessment>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAchievementAssessmentCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT * from (select ROW_NUMBER() OVER (ORDER BY isnull( a.WriteDate,'') DESC) as RowNum , a.ID  ,a.WriteDate , b.StudentName ,   b.StudentSex, b.StudentBirthday  ";
                // string sql = " SELECT * from (select ROW_NUMBER() OVER (ORDER BY isnull( a.WriteDate,'') DESC) as RowNum , a.ID , b.StudentName ,  CASE (b.StudentSex) WHEN '1' THEN '男'  WHEN '2 THEN '女' END as StudentSex , b.StudentBirthday  ";
                sql += " FROM CaseStudy a left join studentDatabase b on a.studentid = b.id  WHERE isnull(a.isDeleted,0) = 0 " + ConditionReturn + ") AS NewTable ";

                sql += " where  RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";



                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);


                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchAchievementAssessment addValue = new SearchAchievementAssessment();
                    addValue.RowNum = dr["rownum"].ToString();

                    addValue.ID = int.Parse(dr["ID"].ToString());
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtstudentSex = int.Parse(dr["StudentSex"].ToString());
                    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    addValue.WriteDate = DateTime.Parse(dr["WriteDate"].ToString());
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchAchievementAssessment addValue = new SearchAchievementAssessment();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }




    public string[] SearchStudentCount(SearchStudent SearchStructure,int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentConditionReturn(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM StudentDatabase WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value ="%"+ Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtcaseStatu);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sGuaranteeDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtjoindaystart);
                cmd.Parameters.Add("@sGuaranteeDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtjoindayend);
                cmd.Parameters.Add("@sCompletedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDatestart);
                cmd.Parameters.Add("@sCompletedDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDateend);
                cmd.Parameters.Add("@CompletedType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtendReasonType);
                cmd.Parameters.Add("@NomembershipType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtnomembershipType);
                returnValue[0] = cmd.ExecuteScalar().ToString();
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
    public List<SearchStudentResult> SearchStudent(int indexpage, SearchStudent SearchStructure,int type)
    {
        List<SearchStudentResult> returnValue = new List<SearchStudentResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentConditionReturn(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StudentDatabase.StudentID DESC) " +
                 "AS RowNum, StudentDatabase.* " +
                 "FROM StudentDatabase WHERE isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtcaseStatu);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sGuaranteeDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtjoindaystart);
                cmd.Parameters.Add("@sGuaranteeDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtjoindayend);
                cmd.Parameters.Add("@sCompletedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDatestart);
                cmd.Parameters.Add("@sCompletedDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDateend);
                cmd.Parameters.Add("@CompletedType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtendReasonType);
                cmd.Parameters.Add("@NomembershipType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtnomembershipType);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchStudentResult addValue = new SearchStudentResult();
                    addValue.ID = int.Parse(dr["ID"].ToString());
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtstudentStatu = dr["CaseStatu"].ToString();
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
                SearchStudentResult addValue = new SearchStudentResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] createStudentDataBase(CreateStudent SearchStructure)
    {
        string[] returnValue = new string[3];
        returnValue[0] = "0";
        returnValue[1] = "0";
        returnValue[2] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                int CaseStatu = this.CaseStatusFunction(SearchStructure);
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string caseUnit = "0";
                if (SearchStructure.Unit != null && SearchStructure.Unit != "0")
                {
                    caseUnit = SearchStructure.Unit;
                }
                else
                {
                    caseUnit = CreateFileName[2];
                }
                /*string[] CreateFileName = new string[4];
                CreateFileName = sDB.getStaffDataName2(HttpContext.Current.User.Identity.Name);*/
                string sql = "INSERT INTO StudentDatabase(EvaluationDate, ConsultDate, Updated, CaseStatu, WriteName, Unit, StudentID, StudentName, StudentIdentity, " +
                    "AddressZip1, AddressCity1, AddressOther1, AddressZip2, AddressCity2, AddressOther2, StudentBirthday, StudentSex, ClassDate, GuaranteeDate, " +
                    "CompletedDate, CompletedType, CompletedReason, ShortEndDateSince, ShortEndDateUntil, NomembershipType, NomembershipReason, StudentAvatar, " +
                    "CaregiversDaytime, CaregiversDaytimeText, CaregiversNight, CaregiversNightText, ContactRelation1, ContactName1, ContactTel_company1, " +
                    "ContactTel_home1, ContactPhone1, ContactFax1, ContactRelation2, ContactName2, ContactTel_company2, ContactTel_home2, ContactPhone2, ContactFax2, " +
                    "ContactRelation3, ContactName3, ContactTel_company3, ContactTel_home3, ContactPhone3, ContactFax3, ContactRelation4, ContactName4, ContactTel_company4, " +
                    "ContactTel_home4, ContactPhone4, ContactFax4, StudentEmail, ReferralSourceType, ReferralSource, PhysicalAndMentalDisabilityHandbook, DisabilityCategory1, " +
                    "DisabilityGrade1, DisabilityCategory2, DisabilityGrade2, DisabilityCategory3, DisabilityGrade3, NoDisabilityHandbook, ApplyDisabilityHandbook, " +
                    "DisabilityProve, Notify, Notify_Unit, Notify_Member, Notify_Tel, Notify_Date, Notify_City, EconomyState, EconomyLow, FamilyAppellation1, FamilyName1, " +
                    "FamilyBirthday1, FamilyEducation1, FamilyProfession1, FamilyLive1, FamilyHearing1, FamilyHealth1, FamilyHealthText1, FamilyAppellation2, FamilyName2, " +
                    "FamilyBirthday2, FamilyEducation2, FamilyProfession2, FamilyLive2, FamilyHearing2, FamilyHealth2, FamilyHealthText2, FamilyAppellation3, FamilyName3, " +
                    "FamilyBirthday3, FamilyEducation3, FamilyProfession3, FamilyLive3, FamilyHearing3, FamilyHealth3, FamilyHealthText3, FamilyAppellation4, FamilyName4, " +
                    "FamilyBirthday4, FamilyEducation4, FamilyProfession4, FamilyLive4, FamilyHearing4, FamilyHealth4, FamilyHealthText4, FamilyAppellation5, FamilyName5, " +
                    "FamilyBirthday5, FamilyEducation5, FamilyProfession5, FamilyLive5, FamilyHearing5, FamilyHealth5, FamilyHealthText5, FamilyAppellation6, FamilyName6, " +
                    "FamilyBirthday6, FamilyEducation6, FamilyProfession6, FamilyLive6, FamilyHearing6, FamilyHealth6, FamilyHealthText6, FamilyAppellation7, FamilyName7, " +
                    "FamilyBirthday7, FamilyEducation7, FamilyProfession7, FamilyLive7, FamilyHearing7, FamilyHealth7, FamilyHealthText7, FamilyAppellation8, FamilyName8, " +
                    "FamilyBirthday8, FamilyEducation8, FamilyProfession8, FamilyLive8, FamilyHearing8, FamilyHealth8, FamilyHealthText8, FamilyLanguage, FamilyLanguageText, " +
                    "FamilyConnectLanguage, FamilyConnectLanguageText, FamilyProfile1, FamilyProfile2, FamilyProfile3, FamilyProfile4, FamilyProfile5, FamilyProfileWriteName, " +
                    "FamilyProfileWriteDate, CreateFileBy, UpFileBy, UpFileDate ) " +
                    "VALUES " +
                    "(@EvaluationDate, @ConsultDate, (getdate()), @CaseStatu, @WriteName, @Unit, @StudentID, @StudentName, @StudentIdentity, @AddressZip1, @AddressCity1, " +
                    "@AddressOther1, @AddressZip2, @AddressCity2, @AddressOther2, @StudentBirthday, @StudentSex, @ClassDate, @GuaranteeDate, @CompletedDate, @CompletedType, " +
                    "@CompletedReason, @ShortEndDateSince, @ShortEndDateUntil, @NomembershipType, @NomembershipReason, @StudentAvatar, @CaregiversDaytime, " +
                    "@CaregiversDaytimeText, @CaregiversNight, @CaregiversNightText, @ContactRelation1, @ContactName1, @ContactTel_company1, @ContactTel_home1, " +
                    "@ContactPhone1, @ContactFax1, @ContactRelation2, @ContactName2, @ContactTel_company2, @ContactTel_home2, @ContactPhone2, @ContactFax2, " +
                    "@ContactRelation3, @ContactName3, @ContactTel_company3, @ContactTel_home3, @ContactPhone3, @ContactFax3, @ContactRelation4, @ContactName4, " +
                    "@ContactTel_company4, @ContactTel_home4, @ContactPhone4, @ContactFax4, @StudentEmail, @ReferralSourceType, @ReferralSource, " +
                    "@PhysicalAndMentalDisabilityHandbook, @DisabilityCategory1, @DisabilityGrade1, @DisabilityCategory2, @DisabilityGrade2, @DisabilityCategory3, " +
                    "@DisabilityGrade3, @NoDisabilityHandbook, @ApplyDisabilityHandbook, @DisabilityProve, @Notify, @Notify_Unit, @Notify_Member, @Notify_Tel, " +
                    "@Notify_Date, @Notify_City, @EconomyState, @EconomyLow, @FamilyAppellation1, @FamilyName1, @FamilyBirthday1, @FamilyEducation1, @FamilyProfession1, " +
                    "@FamilyLive1, @FamilyHearing1, @FamilyHealth1, @FamilyHealthText1, @FamilyAppellation2, @FamilyName2, @FamilyBirthday2, @FamilyEducation2, " +
                    "@FamilyProfession2, @FamilyLive2, @FamilyHearing2, @FamilyHealth2, @FamilyHealthText2, @FamilyAppellation3, @FamilyName3, @FamilyBirthday3, " +
                    "@FamilyEducation3, @FamilyProfession3, @FamilyLive3, @FamilyHearing3, @FamilyHealth3, @FamilyHealthText3, @FamilyAppellation4, @FamilyName4, " +
                    "@FamilyBirthday4, @FamilyEducation4, @FamilyProfession4, @FamilyLive4, @FamilyHearing4, @FamilyHealth4, @FamilyHealthText4, @FamilyAppellation5, " +
                    "@FamilyName5, @FamilyBirthday5, @FamilyEducation5, @FamilyProfession5, @FamilyLive5, @FamilyHearing5, @FamilyHealth5, @FamilyHealthText5, " +
                    "@FamilyAppellation6, @FamilyName6, @FamilyBirthday6, @FamilyEducation6, @FamilyProfession6, @FamilyLive6, @FamilyHearing6, @FamilyHealth6, " +
                    "@FamilyHealthText6, @FamilyAppellation7, @FamilyName7, @FamilyBirthday7, @FamilyEducation7, @FamilyProfession7, @FamilyLive7, @FamilyHearing7, " +
                    "@FamilyHealth7, @FamilyHealthText7, @FamilyAppellation8, @FamilyName8, @FamilyBirthday8, @FamilyEducation8, @FamilyProfession8, @FamilyLive8, " +
                    "@FamilyHearing8, @FamilyHealth8, @FamilyHealthText8, @FamilyLanguage, @FamilyLanguageText, @FamilyConnectLanguage, @FamilyConnectLanguageText, " +
                    "@FamilyProfile1, @FamilyProfile2, @FamilyProfile3, @FamilyProfile4, @FamilyProfile5, @FamilyProfileWriteName, @FamilyProfileWriteDate ,@CreateFileBy, @UpFileBy, (getDate()))";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);    

                cmd.Parameters.Add("@EvaluationDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.assessDate);
                cmd.Parameters.Add("@ConsultDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.consultationDate);
                cmd.Parameters.Add("@Updated", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.upDate);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckIntFunction(CaseStatu);
                cmd.Parameters.Add("@WriteName", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(caseUnit);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.studentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.studentName);
                cmd.Parameters.Add("@StudentIdentity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.studentTWID);
                cmd.Parameters.Add("@AddressZip1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.censusAddressZip);
                cmd.Parameters.Add("@AddressCity1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.censusAddressCity);
                cmd.Parameters.Add("@AddressOther1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.censusAddress);
                cmd.Parameters.Add("@AddressZip2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.addressZip);
                cmd.Parameters.Add("@AddressCity2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.addressCity);
                cmd.Parameters.Add("@AddressOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.address);
                cmd.Parameters.Add("@StudentBirthday", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.studentbirthday);
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.studentSex);
                cmd.Parameters.Add("@ClassDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.firstClassDate);
                cmd.Parameters.Add("@GuaranteeDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.guaranteeDate);
                cmd.Parameters.Add("@CompletedDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.endReasonDate);
                cmd.Parameters.Add("@CompletedType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.endReasonType);
                cmd.Parameters.Add("@CompletedReason", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.endReason);
                cmd.Parameters.Add("@ShortEndDateSince", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.sendDateSince);
                cmd.Parameters.Add("@ShortEndDateUntil", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.sendDateUntil);
                cmd.Parameters.Add("@NomembershipType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.nomembershipType);
                cmd.Parameters.Add("@NomembershipReason", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.nomembershipReason);
                cmd.Parameters.Add("@StudentAvatar", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.studentPhoto);
                cmd.Parameters.Add("@CaregiversDaytime", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.wCare);
                cmd.Parameters.Add("@CaregiversDaytimeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.wCareName);
                cmd.Parameters.Add("@CaregiversNight", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.bCare);
                cmd.Parameters.Add("@CaregiversNightText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.bCareName);
                cmd.Parameters.Add("@ContactRelation1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPRelation1);
                cmd.Parameters.Add("@ContactName1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPName1);
                cmd.Parameters.Add("@ContactTel_company1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPTel1);
                cmd.Parameters.Add("@ContactPhone1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPPhone1);
                cmd.Parameters.Add("@ContactTel_home1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPHPhone1);
                cmd.Parameters.Add("@ContactFax1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPFax1);
                cmd.Parameters.Add("@ContactRelation2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPRelation2);
                cmd.Parameters.Add("@ContactName2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPName2);
                cmd.Parameters.Add("@ContactTel_company2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPTel2);
                cmd.Parameters.Add("@ContactPhone2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPPhone2);
                cmd.Parameters.Add("@ContactTel_home2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPHPhone2);
                cmd.Parameters.Add("@ContactFax2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPFax2);
                cmd.Parameters.Add("@ContactRelation3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPRelation3);
                cmd.Parameters.Add("@ContactName3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPName3);
                cmd.Parameters.Add("@ContactTel_company3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPTel3);
                cmd.Parameters.Add("@ContactPhone3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPPhone3);
                cmd.Parameters.Add("@ContactTel_home3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPHPhone3);
                cmd.Parameters.Add("@ContactFax3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPFax3);
                cmd.Parameters.Add("@ContactRelation4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPRelation4);
                cmd.Parameters.Add("@ContactName4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPName4);
                cmd.Parameters.Add("@ContactTel_company4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPTel4);
                cmd.Parameters.Add("@ContactPhone4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPPhone4);
                cmd.Parameters.Add("@ContactTel_home4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPHPhone4);
                cmd.Parameters.Add("@ContactFax4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fPFax4);
                cmd.Parameters.Add("@StudentEmail", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.email);
                cmd.Parameters.Add("@ReferralSourceType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.sourceType);
                cmd.Parameters.Add("@ReferralSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.sourceName);
                cmd.Parameters.Add("@PhysicalAndMentalDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.manualWhether);
                cmd.Parameters.Add("@DisabilityCategory1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.manualCategory1);
                cmd.Parameters.Add("@DisabilityGrade1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.manualGrade1);
                cmd.Parameters.Add("@DisabilityCategory2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.manualCategory2);
                cmd.Parameters.Add("@DisabilityGrade2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.manualGrade2);
                cmd.Parameters.Add("@DisabilityCategory3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.manualCategory3);
                cmd.Parameters.Add("@DisabilityGrade3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.manualGrade3);
                cmd.Parameters.Add("@NoDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.manualNo);
                cmd.Parameters.Add("@ApplyDisabilityHandbook", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.manualUnit);
                cmd.Parameters.Add("@DisabilityProve", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.studentManualImg);
                cmd.Parameters.Add("@Notify", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.notificationWhether);
                cmd.Parameters.Add("@Notify_Unit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.notificationUnit);
                cmd.Parameters.Add("@Notify_Member", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.notificationManage);
                cmd.Parameters.Add("@Notify_Tel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.notificationTel);
                cmd.Parameters.Add("@Notify_Date", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.notificationDate);
                cmd.Parameters.Add("@Notify_City", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.notificationCity);
                cmd.Parameters.Add("@EconomyState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.economy);
                cmd.Parameters.Add("@EconomyLow", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.economyLow);

                cmd.Parameters.Add("@FamilyAppellation1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation1);
                cmd.Parameters.Add("@FamilyName1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName1);
                cmd.Parameters.Add("@FamilyBirthday1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday1);
                cmd.Parameters.Add("@FamilyEducation1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU1);
                cmd.Parameters.Add("@FamilyProfession1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession1);
                cmd.Parameters.Add("@FamilyLive1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive1);
                cmd.Parameters.Add("@FamilyHearing1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing1);
                cmd.Parameters.Add("@FamilyHealth1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy1);
                cmd.Parameters.Add("@FamilyHealthText1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText01);
                cmd.Parameters.Add("@FamilyAppellation2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation2);
                cmd.Parameters.Add("@FamilyName2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName2);
                cmd.Parameters.Add("@FamilyBirthday2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday2);
                cmd.Parameters.Add("@FamilyEducation2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU2);
                cmd.Parameters.Add("@FamilyProfession2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession2);
                cmd.Parameters.Add("@FamilyLive2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive2);
                cmd.Parameters.Add("@FamilyHearing2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing2);
                cmd.Parameters.Add("@FamilyHealth2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy2);
                cmd.Parameters.Add("@FamilyHealthText2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText02);
                cmd.Parameters.Add("@FamilyAppellation3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation3);
                cmd.Parameters.Add("@FamilyName3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName3);
                cmd.Parameters.Add("@FamilyBirthday3", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday3);
                cmd.Parameters.Add("@FamilyEducation3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU3);
                cmd.Parameters.Add("@FamilyProfession3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession3);
                cmd.Parameters.Add("@FamilyLive3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive3);
                cmd.Parameters.Add("@FamilyHearing3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing3);
                cmd.Parameters.Add("@FamilyHealth3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy3);
                cmd.Parameters.Add("@FamilyHealthText3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText03);
                cmd.Parameters.Add("@FamilyAppellation4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation4);
                cmd.Parameters.Add("@FamilyName4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName4);
                cmd.Parameters.Add("@FamilyBirthday4", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday4);
                cmd.Parameters.Add("@FamilyEducation4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU4);
                cmd.Parameters.Add("@FamilyProfession4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession4);
                cmd.Parameters.Add("@FamilyLive4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive4);
                cmd.Parameters.Add("@FamilyHearing4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing4);
                cmd.Parameters.Add("@FamilyHealth4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy4);
                cmd.Parameters.Add("@FamilyHealthText4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText04);
                cmd.Parameters.Add("@FamilyAppellation5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation5);
                cmd.Parameters.Add("@FamilyName5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName5);
                cmd.Parameters.Add("@FamilyBirthday5", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday5);
                cmd.Parameters.Add("@FamilyEducation5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU5);
                cmd.Parameters.Add("@FamilyProfession5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession5);
                cmd.Parameters.Add("@FamilyLive5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive5);
                cmd.Parameters.Add("@FamilyHearing5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing5);
                cmd.Parameters.Add("@FamilyHealth5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy5);
                cmd.Parameters.Add("@FamilyHealthText5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText05);
                cmd.Parameters.Add("@FamilyAppellation6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation6);
                cmd.Parameters.Add("@FamilyName6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName6);
                cmd.Parameters.Add("@FamilyBirthday6", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday6);
                cmd.Parameters.Add("@FamilyEducation6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU6);
                cmd.Parameters.Add("@FamilyProfession6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession6);
                cmd.Parameters.Add("@FamilyLive6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive6);
                cmd.Parameters.Add("@FamilyHearing6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing6);
                cmd.Parameters.Add("@FamilyHealth6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy6);
                cmd.Parameters.Add("@FamilyHealthText6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText06);
                cmd.Parameters.Add("@FamilyAppellation7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation7);
                cmd.Parameters.Add("@FamilyName7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName7);
                cmd.Parameters.Add("@FamilyBirthday7", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday7);
                cmd.Parameters.Add("@FamilyEducation7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU7);
                cmd.Parameters.Add("@FamilyProfession7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession7);
                cmd.Parameters.Add("@FamilyLive7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive7);
                cmd.Parameters.Add("@FamilyHearing7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing7);
                cmd.Parameters.Add("@FamilyHealth7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy7);
                cmd.Parameters.Add("@FamilyHealthText7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText07);
                cmd.Parameters.Add("@FamilyAppellation8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fAppellation8);
                cmd.Parameters.Add("@FamilyName8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fName8);
                cmd.Parameters.Add("@FamilyBirthday8", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fBirthday8);
                cmd.Parameters.Add("@FamilyEducation8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fEDU8);
                cmd.Parameters.Add("@FamilyProfession8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fProfession8);
                cmd.Parameters.Add("@FamilyLive8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fLive8);
                cmd.Parameters.Add("@FamilyHearing8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHearing8);
                cmd.Parameters.Add("@FamilyHealth8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.fHealthy8);
                cmd.Parameters.Add("@FamilyHealthText8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyText08);
                cmd.Parameters.Add("@FamilyLanguage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.lang1);
                cmd.Parameters.Add("@FamilyLanguageText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.lang1t01);
                cmd.Parameters.Add("@FamilyConnectLanguage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.lang2);
                cmd.Parameters.Add("@FamilyConnectLanguageText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.lang2t01);
                
                cmd.Parameters.Add("@FamilyProfile1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.familyEcological);
                cmd.Parameters.Add("@FamilyProfile2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.famailySituation);
                cmd.Parameters.Add("@FamilyProfile3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.famailyMedical);
                cmd.Parameters.Add("@FamilyProfile4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.famailyActionSituation);
                cmd.Parameters.Add("@FamilyProfile5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.fswAssess);
                cmd.Parameters.Add("@FamilyProfileWriteName", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.socialID);
                cmd.Parameters.Add("@FamilyProfileWriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.socialDate);

                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] == "1")
                {
                    
                    string FieldName = "StudentDB_" + caseUnit;
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('StudentDatabase') AS TID "+
                          "UPDATE AutomaticNumberTable SET " + FieldName + "=" + FieldName + "+1 WHERE ID=1 ";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["TID"].ToString());
                    }
                    dr.Close();

                     /*string stuIDName ="";
                    sql = "SELECT Count(*) AS QCOUNT FROM StudentDatabase WHERE Unit=(@Unit) ";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(caseUnit);
                    string stuNumber = cmd.ExecuteScalar().ToString();*/

                    string stuNumber = this.getUnitAutoNumber(FieldName);
                    string stuIDName = caseUnit + stuNumber.PadLeft(4, '0') + "0";

                    sql = "UPDATE StudentDatabase SET StudentID=(@StudentID) WHERE ID=(@TID) ";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@TID", SqlDbType.BigInt).Value = Column;
                    cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(stuIDName);
                    returnValue[0] = cmd.ExecuteNonQuery().ToString();
                    returnValue[1] = Column.ToString();
                    returnValue[2] = stuIDName;
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
    public string getUnitAutoNumber(string FieldName)
    {
        string returnValue = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT " + FieldName + " FROM AutomaticNumberTable WHERE ID=1 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                returnValue = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                /*returnValue[0] = "-1";*/
                //returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
        
    }
    private int CaseStatusFunction(CreateStudent StructureData)
    {
        int returnValue = 1;
        if (StructureData.consultationDate != null)
        {
            returnValue = 1;
        }
        if (StructureData.guaranteeDate != null)
        {
            returnValue = 2;
        }
        if (StructureData.sendDateSince != null && StructureData.sendDateUntil != null)
        {
            returnValue = 3;
        }
        if (StructureData.endReasonDate != null)
        {
            returnValue = 4;
        }
        return returnValue;
    }
    public StudentResult getStudentData(string StudentID)
    {
        StudentResult returnValue = new StudentResult();
        returnValue.StudentData = new CreateStudent();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND ID= @ID";////修正SQL式[  StudentID= (@ID) ] → [ ID = @ID ] by Awho
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.Column = Int64.Parse(dr["ID"].ToString());
                    CreateStudent addValue = new CreateStudent();
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> StaffData = sDB.getStaffDataName(dr["WriteName"].ToString());
                    addValue.fillInName = StaffData[1];

                    addValue.ID = dr["ID"].ToString();
                    addValue.assessDate = dr["EvaluationDate"].ToString();
                    addValue.consultationDate = dr["ConsultDate"].ToString();
                    addValue.upDate = dr["Updated"].ToString();
                    addValue.caseStatu = dr["CaseStatu"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.studentID = dr["StudentID"].ToString();
                    addValue.studentName = dr["StudentName"].ToString();
                    addValue.studentTWID = dr["StudentIdentity"].ToString();
                    addValue.censusAddressZip = dr["AddressZip1"].ToString();
                    addValue.censusAddressCity = dr["AddressCity1"].ToString();
                    addValue.censusAddress = dr["AddressOther1"].ToString();
                    addValue.addressZip = dr["AddressZip2"].ToString();
                    addValue.addressCity = dr["AddressCity2"].ToString();
                    addValue.address = dr["AddressOther2"].ToString();
                    addValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    addValue.studentSex = dr["StudentSex"].ToString();
                    addValue.firstClassDate = DateTime.Parse(dr["ClassDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.guaranteeDate = DateTime.Parse(dr["GuaranteeDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.endReasonDate = DateTime.Parse(dr["CompletedDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.endReasonType = dr["CompletedType"].ToString();
                    addValue.endReason = dr["CompletedReason"].ToString();
                    addValue.sendDateSince = DateTime.Parse(dr["ShortEndDateSince"].ToString()).ToString("yyyy-MM-dd");
                    addValue.sendDateUntil = DateTime.Parse(dr["ShortEndDateUntil"].ToString()).ToString("yyyy-MM-dd");
                    addValue.nomembershipType = dr["NomembershipType"].ToString();
                    addValue.nomembershipReason = dr["NomembershipReason"].ToString();
                    addValue.studentPhoto = dr["StudentAvatar"].ToString();
                    addValue.wCare = dr["CaregiversDaytime"].ToString();
                    addValue.wCareName = dr["CaregiversDaytimeText"].ToString();
                    addValue.bCare = dr["CaregiversNight"].ToString();
                    addValue.bCareName = dr["CaregiversNightText"].ToString();
                    addValue.fPRelation1 = dr["ContactRelation1"].ToString();
                    addValue.fPName1 = dr["ContactName1"].ToString();
                    addValue.fPTel1 = dr["ContactTel_company1"].ToString();
                    addValue.fPPhone1 = dr["ContactPhone1"].ToString();
                    addValue.fPHPhone1 = dr["ContactTel_home1"].ToString();
                    addValue.fPFax1 = dr["ContactFax1"].ToString();
                    addValue.fPRelation2 = dr["ContactRelation2"].ToString();
                    addValue.fPName2 = dr["ContactName2"].ToString();
                    addValue.fPTel2 = dr["ContactTel_company2"].ToString();
                    addValue.fPPhone2 = dr["ContactPhone2"].ToString();
                    addValue.fPHPhone2 = dr["ContactTel_home2"].ToString();
                    addValue.fPFax2 = dr["ContactFax2"].ToString();
                    addValue.fPRelation3 = dr["ContactRelation3"].ToString();
                    addValue.fPName3 = dr["ContactName3"].ToString();
                    addValue.fPTel3 = dr["ContactTel_company3"].ToString();
                    addValue.fPPhone3 = dr["ContactPhone3"].ToString();
                    addValue.fPHPhone3 = dr["ContactTel_home3"].ToString();
                    addValue.fPFax3 = dr["ContactFax3"].ToString();
                    addValue.fPRelation4 = dr["ContactRelation4"].ToString();
                    addValue.fPName4 = dr["ContactName4"].ToString();
                    addValue.fPTel4 = dr["ContactTel_company4"].ToString();
                    addValue.fPPhone4 = dr["ContactPhone4"].ToString();
                    addValue.fPHPhone4 = dr["ContactTel_home4"].ToString();
                    addValue.fPFax4 = dr["ContactFax4"].ToString();
                    addValue.email = dr["StudentEmail"].ToString();
                    addValue.sourceType = dr["ReferralSourceType"].ToString();
                    addValue.sourceName = dr["ReferralSource"].ToString();
                    addValue.manualWhether = dr["PhysicalAndMentalDisabilityHandbook"].ToString();
                    addValue.manualCategory1 = dr["DisabilityCategory1"].ToString();
                    addValue.manualGrade1 = dr["DisabilityGrade1"].ToString();
                    addValue.manualCategory2 = dr["DisabilityCategory2"].ToString();
                    addValue.manualGrade2 = dr["DisabilityGrade2"].ToString();
                    addValue.manualCategory3 = dr["DisabilityCategory3"].ToString();
                    addValue.manualGrade3 = dr["DisabilityGrade3"].ToString();
                    addValue.manualNo = dr["NoDisabilityHandbook"].ToString();
                    addValue.manualUnit = dr["ApplyDisabilityHandbook"].ToString();
                    addValue.studentManualImg = dr["DisabilityProve"].ToString();
                    addValue.notificationWhether = dr["Notify"].ToString();
                    addValue.notificationUnit = dr["Notify_Unit"].ToString();
                    addValue.notificationManage = dr["Notify_Member"].ToString();
                    addValue.notificationTel = dr["Notify_Tel"].ToString();
                    addValue.notificationDate = DateTime.Parse(dr["Notify_Date"].ToString()).ToString("yyyy-MM-dd");
                    addValue.notificationCity = dr["Notify_City"].ToString();
                    addValue.economy = dr["EconomyState"].ToString();
                    addValue.economyLow = dr["EconomyLow"].ToString();
                    addValue.fAppellation1 = dr["FamilyAppellation1"].ToString();
                    addValue.fName1 = dr["FamilyName1"].ToString();
                    addValue.fBirthday1 = DateTime.Parse(dr["FamilyBirthday1"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU1 = dr["FamilyEducation1"].ToString();
                    addValue.fProfession1 = dr["FamilyProfession1"].ToString();
                    addValue.fLive1 = dr["FamilyLive1"].ToString();
                    addValue.fHearing1 = dr["FamilyHearing1"].ToString();
                    addValue.fHealthy1 = dr["FamilyHealth1"].ToString();
                    addValue.familyText01 = dr["FamilyHealthText1"].ToString();
                    addValue.fAppellation2 = dr["FamilyAppellation2"].ToString();
                    addValue.fName2 = dr["FamilyName2"].ToString();
                    addValue.fBirthday2 = DateTime.Parse(dr["FamilyBirthday2"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU2 = dr["FamilyEducation2"].ToString();
                    addValue.fProfession2 = dr["FamilyProfession2"].ToString();
                    addValue.fLive2 = dr["FamilyLive2"].ToString();
                    addValue.fHearing2 = dr["FamilyHearing2"].ToString();
                    addValue.fHealthy2 = dr["FamilyHealth2"].ToString();
                    addValue.familyText02 = dr["FamilyHealthText2"].ToString();
                    addValue.fAppellation3 = dr["FamilyAppellation3"].ToString();
                    addValue.fName3 = dr["FamilyName3"].ToString();
                    addValue.fBirthday3 = DateTime.Parse(dr["FamilyBirthday3"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU3 = dr["FamilyEducation3"].ToString();
                    addValue.fProfession3 = dr["FamilyProfession3"].ToString();
                    addValue.fLive3 = dr["FamilyLive3"].ToString();
                    addValue.fHearing3 = dr["FamilyHearing3"].ToString();
                    addValue.fHealthy3 = dr["FamilyHealth3"].ToString();
                    addValue.familyText03 = dr["FamilyHealthText3"].ToString();
                    addValue.fAppellation4 = dr["FamilyAppellation4"].ToString();
                    addValue.fName4 = dr["FamilyName4"].ToString();
                    addValue.fBirthday4 = DateTime.Parse(dr["FamilyBirthday4"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU4 = dr["FamilyEducation4"].ToString();
                    addValue.fProfession4 = dr["FamilyProfession4"].ToString();
                    addValue.fLive4 = dr["FamilyLive4"].ToString();
                    addValue.fHearing4 = dr["FamilyHearing4"].ToString();
                    addValue.fHealthy4 = dr["FamilyHealth4"].ToString();
                    addValue.familyText04 = dr["FamilyHealthText4"].ToString();
                    addValue.fAppellation5 = dr["FamilyAppellation5"].ToString();
                    addValue.fName5 = dr["FamilyName5"].ToString();
                    addValue.fBirthday5 = DateTime.Parse(dr["FamilyBirthday5"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU5 = dr["FamilyEducation5"].ToString();
                    addValue.fProfession5 = dr["FamilyProfession5"].ToString();
                    addValue.fLive5 = dr["FamilyLive5"].ToString();
                    addValue.fHearing5 = dr["FamilyHearing5"].ToString();
                    addValue.fHealthy5 = dr["FamilyHealth5"].ToString();
                    addValue.familyText05 = dr["FamilyHealthText5"].ToString();
                    addValue.fAppellation6 = dr["FamilyAppellation6"].ToString();
                    addValue.fName6 = dr["FamilyName6"].ToString();
                    addValue.fBirthday6 = DateTime.Parse(dr["FamilyBirthday6"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU6 = dr["FamilyEducation6"].ToString();
                    addValue.fProfession6 = dr["FamilyProfession6"].ToString();
                    addValue.fLive6 = dr["FamilyLive6"].ToString();
                    addValue.fHearing6 = dr["FamilyHearing6"].ToString();
                    addValue.fHealthy6 = dr["FamilyHealth6"].ToString();
                    addValue.familyText06 = dr["FamilyHealthText6"].ToString();
                    addValue.fAppellation7 = dr["FamilyAppellation7"].ToString();
                    addValue.fName7 = dr["FamilyName7"].ToString();
                    addValue.fBirthday7 = DateTime.Parse(dr["FamilyBirthday7"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU7 = dr["FamilyEducation7"].ToString();
                    addValue.fProfession7 = dr["FamilyProfession7"].ToString();
                    addValue.fLive7 = dr["FamilyLive7"].ToString();
                    addValue.fHearing7 = dr["FamilyHearing7"].ToString();
                    addValue.fHealthy7 = dr["FamilyHealth7"].ToString();
                    addValue.familyText07 = dr["FamilyHealthText7"].ToString();
                    addValue.fAppellation8 = dr["FamilyAppellation8"].ToString();
                    addValue.fName8 = dr["FamilyName8"].ToString();
                    addValue.fBirthday8 = DateTime.Parse(dr["FamilyBirthday8"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU8 = dr["FamilyEducation8"].ToString();
                    addValue.fProfession8 = dr["FamilyProfession8"].ToString();
                    addValue.fLive8 = dr["FamilyLive8"].ToString();
                    addValue.fHearing8 = dr["FamilyHearing8"].ToString();
                    addValue.fHealthy8 = dr["FamilyHealth8"].ToString();
                    addValue.familyText08 = dr["FamilyHealthText8"].ToString();
                    addValue.lang1 = dr["FamilyLanguage"].ToString();
                    addValue.lang1t01 = dr["FamilyLanguageText"].ToString();
                    addValue.lang2 = dr["FamilyConnectLanguage"].ToString();
                    addValue.lang2t01 = dr["FamilyConnectLanguageText"].ToString();
                    addValue.familyEcological = dr["FamilyProfile1"].ToString();
                    addValue.famailySituation = dr["FamilyProfile2"].ToString();
                    addValue.famailyMedical = dr["FamilyProfile3"].ToString();
                    addValue.famailyActionSituation = dr["FamilyProfile4"].ToString();
                    addValue.fswAssess = dr["FamilyProfile5"].ToString();

                    StaffData = sDB.getStaffDataName(dr["FamilyProfileWriteName"].ToString());
                    addValue.socialName = StaffData[1];
                    addValue.socialDate = DateTime.Parse(dr["FamilyProfileWriteDate"].ToString()).ToString("yyyy-MM-dd");

                    returnValue.StudentData = addValue;

                    StudentHearingInformation addValue2 = new StudentHearingInformation();
                    addValue2.history = dr["StudentDevelop"].ToString();
                    addValue2.history02t01 = dr["StudentDevelopText21"].ToString();
                    addValue2.history02t02 = dr["StudentDevelopText22"].ToString();
                    addValue2.history03t01 = dr["StudentDevelopText31"].ToString();
                    addValue2.history06t01 = dr["StudentDevelopText61"].ToString();
                    addValue2.history06t02 = dr["StudentDevelopText62"].ToString();
                    addValue2.history08t01 = dr["StudentDevelopText81"].ToString();
                    addValue2.history10t01 = dr["StudentDevelopText10"].ToString();
                    addValue2.history11t01 = dr["StudentDevelopText11"].ToString();
                    addValue2.history12t01 = dr["StudentDevelopText12"].ToString();
                    addValue2.problems01t01 = dr["HearingProblemAge"].ToString();
                    addValue2.problems01t02 = dr["HearingProblemMonth"].ToString();
                    addValue2.hearingQ = dr["HearingProblem"].ToString();
                    addValue2.hearingQText = dr["HearingProblemText"].ToString();
                    addValue2.problems02t01 = dr["DiagnoseAge"].ToString();
                    addValue2.problems02t02 = dr["DiagnoseMonth"].ToString();
                    addValue2.problems02t03 = dr["DiagnoseAgency"].ToString();
                    addValue2.problems02t04 = dr["DiagnoseR"].ToString();
                    addValue2.problems02t05 = dr["DiagnoseL"].ToString();
                    addValue2.hearingcheck = dr["NewbornHearing"].ToString();
                    addValue2.hearingYescheck = dr["NewbornHearingInspection"].ToString();
                    addValue2.hearingYesPlace = dr["NewbornHearingInspectionAgency"].ToString();
                    addValue2.hearingYesResultR = dr["NewbornHearingInspectionDiagnoseR"].ToString();
                    addValue2.hearingYesResultL = dr["NewbornHearingInspectionDiagnoseL"].ToString();
                    addValue2.sleepcheck = dr["AuditoryElectrophysiology1"].ToString();
                    addValue2.sleepcheckTime1 = DateTime.Parse(dr["AuditoryElectrophysiology_Date1"].ToString()).ToString("yyyy-MM-dd");//dr["AuditoryElectrophysiology_Date1"].ToString();
                    addValue2.sleepcheckPlace1 = dr["AuditoryElectrophysiology_Agency1"].ToString();
                    addValue2.sleepcheckCheckItem1 = dr["AuditoryElectrophysiology_Item1"].ToString();
                    addValue2.sleepcheckResultL1 = dr["AuditoryElectrophysiology_Diagnose1R"].ToString();
                    addValue2.sleepcheckResultR1 = dr["AuditoryElectrophysiology_Diagnose1L"].ToString();
                    addValue2.sleepcheckTime2 = DateTime.Parse(dr["AuditoryElectrophysiology_Date2"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace2 = dr["AuditoryElectrophysiology_Agency2"].ToString();
                    addValue2.sleepcheckCheckItem2 = dr["AuditoryElectrophysiology_Item2"].ToString();
                    addValue2.sleepcheckResultL2 = dr["AuditoryElectrophysiology_Diagnose2R"].ToString();
                    addValue2.sleepcheckResultR2 = dr["AuditoryElectrophysiology_Diagnose2L"].ToString();
                    addValue2.sleepcheckTime3 = DateTime.Parse(dr["AuditoryElectrophysiology_Date3"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace3 = dr["AuditoryElectrophysiology_Agency3"].ToString();
                    addValue2.sleepcheckCheckItem3 = dr["AuditoryElectrophysiology_Item3"].ToString();
                    addValue2.sleepcheckResultL3 = dr["AuditoryElectrophysiology_Diagnose3R"].ToString();
                    addValue2.sleepcheckResultR3 = dr["AuditoryElectrophysiology_Diagnose3L"].ToString();
                    addValue2.sleepcheckTime4 = DateTime.Parse(dr["AuditoryElectrophysiology_Date4"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace4 = dr["AuditoryElectrophysiology_Agency4"].ToString();
                    addValue2.sleepcheckCheckItem4 = dr["AuditoryElectrophysiology_Item4"].ToString();
                    addValue2.sleepcheckResultL4 = dr["AuditoryElectrophysiology_Diagnose4R"].ToString();
                    addValue2.sleepcheckResultR4 = dr["AuditoryElectrophysiology_Diagnose4L"].ToString();
                    addValue2.sleepcheckTime5 = DateTime.Parse(dr["AuditoryElectrophysiology_Date5"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace5 = dr["AuditoryElectrophysiology_Agency5"].ToString();
                    addValue2.sleepcheckCheckItem5 = dr["AuditoryElectrophysiology_Item5"].ToString();
                    addValue2.sleepcheckResultL5 = dr["AuditoryElectrophysiology_Diagnose5R"].ToString();
                    addValue2.sleepcheckResultR5 = dr["AuditoryElectrophysiology_Diagnose5L"].ToString();

                    addValue2.ctmri = dr["CTorMRI"].ToString();
                    addValue2.ctmriTime = DateTime.Parse(dr["CTorMRI_Date1"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.ctmriPlace = dr["CTorMRI_Agency1"].ToString();
                    addValue2.ctmriResultL = dr["CTorMRI_DiagnoseR"].ToString();
                    addValue2.ctmriResultR = dr["CTorMRI_DiagnoseL"].ToString();
                    addValue2.gene = dr["GeneScreening"].ToString();
                    addValue2.geneTime = DateTime.Parse(dr["GeneScreening_Date"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.genePlace = dr["GeneScreening_Agency"].ToString();
                    addValue2.geneResult = dr["GeneScreening_Item"].ToString();
                    addValue2.familyhistory = dr["FamilyHearingProblemHistory"].ToString();
                    addValue2.familyhistoryText = dr["FamilyHearingProblemHistoryText"].ToString();
                    addValue2.changehistory = dr["HearingChangeHistory"].ToString();
                    addValue2.changehistoryText = dr["HearingChangeHistoryText"].ToString();
                    addValue2.assistmanage = dr["AidsManagement"].ToString();
                    addValue2.accessory = dr["AidsManagementTextAge"].ToString();
                    addValue2.assistmanageR = dr["HearingAids_R"].ToString();
                    addValue2.brandR = dr["AidsBrand_R"].ToString();
                    addValue2.modelR = dr["AidsModel_R"].ToString();
                    addValue2.buyingPlaceR = dr["AidsOptionalLocation_R"].ToString();
                    addValue2.buyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.insertHospitalR = dr["EEarHospital_R"].ToString();
                    addValue2.openHzDateR = DateTime.Parse(dr["EEarOpen_R"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.assistmanageL = dr["HearingAids_L"].ToString();
                    addValue2.brandL = dr["AidsBrand_L"].ToString();
                    addValue2.modelL = dr["AidsModel_L"].ToString();
                    addValue2.buyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.buyingPlaceL = dr["AidsOptionalLocation_L"].ToString();
                    addValue2.insertHospitalL = dr["EEarHospital_L"].ToString();
                    addValue2.openHzDateL = DateTime.Parse(dr["EEarOpen_L"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.allassis = dr["AllDayAids"].ToString();
                    addValue2.allassisNoText = dr["AllDayAidsText"].ToString();
                    addValue2.assis1 = dr["ActiveReaction"].ToString();
                    addValue2.assis1NoText = dr["ActiveReactionText"].ToString();
                    addValue2.assis2 = dr["BasicCare"].ToString();
                    addValue2.assis2NoText = dr["BasicCareText"].ToString();
                    addValue2.assisFM = dr["UseFMsystem"].ToString();
                    addValue2.assisFMBrand = dr["UseFMsystemBrand"].ToString();
                    addValue2.assessnotes = dr["HearingAssessmentSummary"].ToString();
                    addValue2.assessnotes1 = dr["EarEndoscopy"].ToString();
                    addValue2.assessnotes102Text = dr["EarEndoscopyAbnormalText"].ToString();
                    addValue2.problems11t02 = dr["PureToneText"].ToString();
                    addValue2.assessnotes2 = dr["Tympanogram"].ToString();
                    addValue2.problems11t03 = dr["SATorSDT"].ToString();
                    addValue2.problems11t04 = dr["SpeechRecognition"].ToString();
                    addValue2.problems11t05 = dr["AidsSystem"].ToString();
                    addValue2.problems11t06 = dr["HearingInspection"].ToString();
                    addValue2.problems11t07 = dr["HearingOther"].ToString();
                    addValue2.inspectorID = dr["Audiologist"].ToString();
                    addValue2.inspectorDate = DateTime.Parse(dr["SurveyingDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.HearingData = addValue2;


                    StudentTeachingInformation addValue4 = new StudentTeachingInformation();
                    addValue4.case1 = dr["CaseFamilyCooperative"].ToString();
                    addValue4.case1t04 = dr["EntrustOthers"].ToString();
                    addValue4.case1t05 = dr["CooperativeDifficulty"].ToString();
                    addValue4.case2 = dr["RearingAttitude"].ToString();
                    addValue4.case21 = dr["RearingAttitude1"].ToString();
                    addValue4.case21t01 = dr["RearingAttitude1Text"].ToString();
                    addValue4.case22 = dr["RearingAttitude2"].ToString();
                    addValue4.case22t01 = dr["RearingAttitude2Text"].ToString();
                    addValue4.case23 = dr["RearingAttitude3"].ToString();
                    addValue4.case23t01 = dr["RearingAttitude3Text"].ToString();
                    addValue4.case24 = dr["RearingAttitude4"].ToString();
                    addValue4.case24t01 = dr["RearingAttitude4Text"].ToString();
                    addValue4.case25 = dr["RearingAttitude5"].ToString();
                    addValue4.case25t01 = dr["RearingAttitude5Text"].ToString();
                    addValue4.case26 = dr["RearingAttitude6"].ToString();
                    addValue4.case26t01 = dr["RearingAttitude6Text"].ToString();
                    addValue4.case3 = dr["Dispositional"].ToString();
                    addValue4.case31 = dr["Dispositional1"].ToString();
                    addValue4.case31t01 = dr["Dispositional1Text"].ToString();
                    addValue4.case32 = dr["Dispositional2"].ToString();
                    addValue4.case32t01 = dr["Dispositional2Text"].ToString();
                    addValue4.case33 = dr["Dispositional3"].ToString();
                    addValue4.case33t01 = dr["Dispositional3Text"].ToString();
                    addValue4.case34 = dr["Dispositional4"].ToString();
                    addValue4.case34t01 = dr["Dispositional4Text"].ToString();
                    addValue4.attend = dr["Attendance"].ToString();
                    addValue4.attend01t01 = dr["AttendanceText"].ToString();
                    addValue4.accompany = dr["TeachEscorts"].ToString();
                    addValue4.accompany01t01 = dr["TeachEscortsText"].ToString();
                    addValue4.teach = dr["AfterTeach"].ToString();
                    addValue4.teach01t01 = dr["AfterTeachText"].ToString();
                    addValue4.caseQ = dr["MajorProblem"].ToString();
                    addValue4.caseQ01t01 = dr["MajorProblemText"].ToString();
                    addValue4.OtherRemark1 = dr["RemarkOther"].ToString();
                    addValue4.case4 = dr["LearningAttitude"].ToString();
                    addValue4.case5 = dr["Temperament"].ToString();
                    addValue4.case6 = dr["Activity"].ToString();
                    addValue4.case7 = dr["Focus"].ToString();
                    addValue4.case8 = dr["Persistence"].ToString();
                    addValue4.case9 = dr["CommunicationBehavior"].ToString();
                    addValue4.case10 = dr["AuditorySkill"].ToString();
                    addValue4.case11 = dr["Acknowledge"].ToString();
                    addValue4.case12 = dr["Language"].ToString();
                    addValue4.case12t01 = dr["LanguageText"].ToString();
                    addValue4.case13 = dr["Action"].ToString();
                    addValue4.wear = dr["WearAids"].ToString();
                    addValue4.mind = dr["SpiritAndCoordinate"].ToString();
                    addValue4.mind01t01 = dr["SpiritAndCoordinateOther"].ToString();
                    addValue4.connectwish = dr["Communication"].ToString();
                    addValue4.studywish = dr["LearningDesire"].ToString();
                    addValue4.related = dr["AttachmentData"].ToString();
                    addValue4.related01t01 = dr["AttachmentDataText"].ToString();
                    addValue4.OtherRemark2 = dr["RemarkOther2"].ToString();
                    addValue4.case14 = dr["ExistingResources"].ToString();
                    addValue4.trusteeship = dr["InterventionAgencies"].ToString();
                    addValue4.case14t01 = dr["HospitalIntervention"].ToString();
                    addValue4.proceed = dr["RelatedIntervention"].ToString();
                    addValue4.proceedt01 = dr["RelatedInterventionText"].ToString();
                    addValue4.preschools = dr["InNurserySchool"].ToString();
                    addValue4.case15 = dr["HearingRehabilitation"].ToString();
                    addValue4.Rehabilitation1 = dr["CaseNeed"].ToString();
                    addValue4.Rehabilitation2 = dr["CaseFamilyEnthusiasm"].ToString();
                    addValue4.Rehabilitation3 = dr["Rehousing"].ToString();
                    addValue4.Rehabilitation3Text = dr["RehousingText"].ToString();
                    addValue4.case16 = dr["CaseFamilyCourseProposal"].ToString();
                    addValue4.OtherRemark3 = dr["CaseFamilyCourseProposalText"].ToString();
                    addValue4.case17 = dr["CaseCourseProposal"].ToString();
                    addValue4.OtherRemark4 = dr["CaseCourseProposalText"].ToString();
                    addValue4.teacherID = dr["Teacher"].ToString();
                    addValue4.teacherDate = DateTime.Parse(dr["TeacherDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.TeachData = addValue4;

                }
                dr.Close();


                List<StudentBodyInformation> addValue3 = new List<StudentBodyInformation>();


                sql = "SELECT * FROM StudentHeightWeightRecord WHERE isDeleted=0 AND StudentID=(@ID)";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = returnValue.StudentData.studentID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StudentBodyInformation addValue3item = new StudentBodyInformation();
                    addValue3item.RecordID = dr["hwID"].ToString();
                    addValue3item.RecordDate = DateTime.Parse(dr["hwDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue3item.studentID = dr["StudentID"].ToString();
                    addValue3item.RecordHeight = dr["StudentHeight"].ToString();
                    addValue3item.RecordWeight = dr["StudentWeight"].ToString();
                    addValue3item.RecordRemark = dr["hwRemark"].ToString();
                    addValue3.Add(addValue3item);
                }
                dr.Close();
                returnValue.BodyData = addValue3;
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message;
            }
        }
        return returnValue;
    }

    public StudentResult getStudentDataWho(string StudentID)
    {
        StudentResult returnValue = new StudentResult();
        returnValue.StudentData = new CreateStudent();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND StudentID= @ID";////修正SQL式[  StudentID= (@ID) ] → [ ID = @ID ] by Awho
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.Column = Int64.Parse(dr["ID"].ToString());
                    CreateStudent addValue = new CreateStudent();
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> StaffData = sDB.getStaffDataName(dr["WriteName"].ToString());
                    addValue.fillInName = StaffData[1];

                    addValue.ID = dr["ID"].ToString();
                    addValue.assessDate = dr["EvaluationDate"].ToString();
                    addValue.consultationDate = dr["ConsultDate"].ToString();
                    addValue.upDate = dr["Updated"].ToString();
                    addValue.caseStatu = dr["CaseStatu"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.studentID = dr["StudentID"].ToString();
                    addValue.studentName = dr["StudentName"].ToString();
                    addValue.studentTWID = dr["StudentIdentity"].ToString();
                    addValue.censusAddressZip = dr["AddressZip1"].ToString();
                    addValue.censusAddressCity = dr["AddressCity1"].ToString();
                    addValue.censusAddress = dr["AddressOther1"].ToString();
                    addValue.addressZip = dr["AddressZip2"].ToString();
                    addValue.addressCity = dr["AddressCity2"].ToString();
                    addValue.address = dr["AddressOther2"].ToString();
                    addValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    addValue.studentSex = dr["StudentSex"].ToString();
                    addValue.firstClassDate = DateTime.Parse(dr["ClassDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.guaranteeDate = DateTime.Parse(dr["GuaranteeDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.endReasonDate = DateTime.Parse(dr["CompletedDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.endReasonType = dr["CompletedType"].ToString();
                    addValue.endReason = dr["CompletedReason"].ToString();
                    addValue.sendDateSince = DateTime.Parse(dr["ShortEndDateSince"].ToString()).ToString("yyyy-MM-dd");
                    addValue.sendDateUntil = DateTime.Parse(dr["ShortEndDateUntil"].ToString()).ToString("yyyy-MM-dd");
                    addValue.nomembershipType = dr["NomembershipType"].ToString();
                    addValue.nomembershipReason = dr["NomembershipReason"].ToString();
                    addValue.studentPhoto = dr["StudentAvatar"].ToString();
                    addValue.wCare = dr["CaregiversDaytime"].ToString();
                    addValue.wCareName = dr["CaregiversDaytimeText"].ToString();
                    addValue.bCare = dr["CaregiversNight"].ToString();
                    addValue.bCareName = dr["CaregiversNightText"].ToString();
                    addValue.fPRelation1 = dr["ContactRelation1"].ToString();
                    addValue.fPName1 = dr["ContactName1"].ToString();
                    addValue.fPTel1 = dr["ContactTel_company1"].ToString();
                    addValue.fPPhone1 = dr["ContactPhone1"].ToString();
                    addValue.fPHPhone1 = dr["ContactTel_home1"].ToString();
                    addValue.fPFax1 = dr["ContactFax1"].ToString();
                    addValue.fPRelation2 = dr["ContactRelation2"].ToString();
                    addValue.fPName2 = dr["ContactName2"].ToString();
                    addValue.fPTel2 = dr["ContactTel_company2"].ToString();
                    addValue.fPPhone2 = dr["ContactPhone2"].ToString();
                    addValue.fPHPhone2 = dr["ContactTel_home2"].ToString();
                    addValue.fPFax2 = dr["ContactFax2"].ToString();
                    addValue.fPRelation3 = dr["ContactRelation3"].ToString();
                    addValue.fPName3 = dr["ContactName3"].ToString();
                    addValue.fPTel3 = dr["ContactTel_company3"].ToString();
                    addValue.fPPhone3 = dr["ContactPhone3"].ToString();
                    addValue.fPHPhone3 = dr["ContactTel_home3"].ToString();
                    addValue.fPFax3 = dr["ContactFax3"].ToString();
                    addValue.fPRelation4 = dr["ContactRelation4"].ToString();
                    addValue.fPName4 = dr["ContactName4"].ToString();
                    addValue.fPTel4 = dr["ContactTel_company4"].ToString();
                    addValue.fPPhone4 = dr["ContactPhone4"].ToString();
                    addValue.fPHPhone4 = dr["ContactTel_home4"].ToString();
                    addValue.fPFax4 = dr["ContactFax4"].ToString();
                    addValue.email = dr["StudentEmail"].ToString();
                    addValue.sourceType = dr["ReferralSourceType"].ToString();
                    addValue.sourceName = dr["ReferralSource"].ToString();
                    addValue.manualWhether = dr["PhysicalAndMentalDisabilityHandbook"].ToString();
                    addValue.manualCategory1 = dr["DisabilityCategory1"].ToString();
                    addValue.manualGrade1 = dr["DisabilityGrade1"].ToString();
                    addValue.manualCategory2 = dr["DisabilityCategory2"].ToString();
                    addValue.manualGrade2 = dr["DisabilityGrade2"].ToString();
                    addValue.manualCategory3 = dr["DisabilityCategory3"].ToString();
                    addValue.manualGrade3 = dr["DisabilityGrade3"].ToString();
                    addValue.manualNo = dr["NoDisabilityHandbook"].ToString();
                    addValue.manualUnit = dr["ApplyDisabilityHandbook"].ToString();
                    addValue.studentManualImg = dr["DisabilityProve"].ToString();
                    addValue.notificationWhether = dr["Notify"].ToString();
                    addValue.notificationUnit = dr["Notify_Unit"].ToString();
                    addValue.notificationManage = dr["Notify_Member"].ToString();
                    addValue.notificationTel = dr["Notify_Tel"].ToString();
                    addValue.notificationDate = DateTime.Parse(dr["Notify_Date"].ToString()).ToString("yyyy-MM-dd");
                    addValue.notificationCity = dr["Notify_City"].ToString();
                    addValue.economy = dr["EconomyState"].ToString();
                    addValue.economyLow = dr["EconomyLow"].ToString();
                    addValue.fAppellation1 = dr["FamilyAppellation1"].ToString();
                    addValue.fName1 = dr["FamilyName1"].ToString();
                    addValue.fBirthday1 = DateTime.Parse(dr["FamilyBirthday1"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU1 = dr["FamilyEducation1"].ToString();
                    addValue.fProfession1 = dr["FamilyProfession1"].ToString();
                    addValue.fLive1 = dr["FamilyLive1"].ToString();
                    addValue.fHearing1 = dr["FamilyHearing1"].ToString();
                    addValue.fHealthy1 = dr["FamilyHealth1"].ToString();
                    addValue.familyText01 = dr["FamilyHealthText1"].ToString();
                    addValue.fAppellation2 = dr["FamilyAppellation2"].ToString();
                    addValue.fName2 = dr["FamilyName2"].ToString();
                    addValue.fBirthday2 = DateTime.Parse(dr["FamilyBirthday2"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU2 = dr["FamilyEducation2"].ToString();
                    addValue.fProfession2 = dr["FamilyProfession2"].ToString();
                    addValue.fLive2 = dr["FamilyLive2"].ToString();
                    addValue.fHearing2 = dr["FamilyHearing2"].ToString();
                    addValue.fHealthy2 = dr["FamilyHealth2"].ToString();
                    addValue.familyText02 = dr["FamilyHealthText2"].ToString();
                    addValue.fAppellation3 = dr["FamilyAppellation3"].ToString();
                    addValue.fName3 = dr["FamilyName3"].ToString();
                    addValue.fBirthday3 = DateTime.Parse(dr["FamilyBirthday3"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU3 = dr["FamilyEducation3"].ToString();
                    addValue.fProfession3 = dr["FamilyProfession3"].ToString();
                    addValue.fLive3 = dr["FamilyLive3"].ToString();
                    addValue.fHearing3 = dr["FamilyHearing3"].ToString();
                    addValue.fHealthy3 = dr["FamilyHealth3"].ToString();
                    addValue.familyText03 = dr["FamilyHealthText3"].ToString();
                    addValue.fAppellation4 = dr["FamilyAppellation4"].ToString();
                    addValue.fName4 = dr["FamilyName4"].ToString();
                    addValue.fBirthday4 = DateTime.Parse(dr["FamilyBirthday4"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU4 = dr["FamilyEducation4"].ToString();
                    addValue.fProfession4 = dr["FamilyProfession4"].ToString();
                    addValue.fLive4 = dr["FamilyLive4"].ToString();
                    addValue.fHearing4 = dr["FamilyHearing4"].ToString();
                    addValue.fHealthy4 = dr["FamilyHealth4"].ToString();
                    addValue.familyText04 = dr["FamilyHealthText4"].ToString();
                    addValue.fAppellation5 = dr["FamilyAppellation5"].ToString();
                    addValue.fName5 = dr["FamilyName5"].ToString();
                    addValue.fBirthday5 = DateTime.Parse(dr["FamilyBirthday5"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU5 = dr["FamilyEducation5"].ToString();
                    addValue.fProfession5 = dr["FamilyProfession5"].ToString();
                    addValue.fLive5 = dr["FamilyLive5"].ToString();
                    addValue.fHearing5 = dr["FamilyHearing5"].ToString();
                    addValue.fHealthy5 = dr["FamilyHealth5"].ToString();
                    addValue.familyText05 = dr["FamilyHealthText5"].ToString();
                    addValue.fAppellation6 = dr["FamilyAppellation6"].ToString();
                    addValue.fName6 = dr["FamilyName6"].ToString();
                    addValue.fBirthday6 = DateTime.Parse(dr["FamilyBirthday6"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU6 = dr["FamilyEducation6"].ToString();
                    addValue.fProfession6 = dr["FamilyProfession6"].ToString();
                    addValue.fLive6 = dr["FamilyLive6"].ToString();
                    addValue.fHearing6 = dr["FamilyHearing6"].ToString();
                    addValue.fHealthy6 = dr["FamilyHealth6"].ToString();
                    addValue.familyText06 = dr["FamilyHealthText6"].ToString();
                    addValue.fAppellation7 = dr["FamilyAppellation7"].ToString();
                    addValue.fName7 = dr["FamilyName7"].ToString();
                    addValue.fBirthday7 = DateTime.Parse(dr["FamilyBirthday7"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU7 = dr["FamilyEducation7"].ToString();
                    addValue.fProfession7 = dr["FamilyProfession7"].ToString();
                    addValue.fLive7 = dr["FamilyLive7"].ToString();
                    addValue.fHearing7 = dr["FamilyHearing7"].ToString();
                    addValue.fHealthy7 = dr["FamilyHealth7"].ToString();
                    addValue.familyText07 = dr["FamilyHealthText7"].ToString();
                    addValue.fAppellation8 = dr["FamilyAppellation8"].ToString();
                    addValue.fName8 = dr["FamilyName8"].ToString();
                    addValue.fBirthday8 = DateTime.Parse(dr["FamilyBirthday8"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fEDU8 = dr["FamilyEducation8"].ToString();
                    addValue.fProfession8 = dr["FamilyProfession8"].ToString();
                    addValue.fLive8 = dr["FamilyLive8"].ToString();
                    addValue.fHearing8 = dr["FamilyHearing8"].ToString();
                    addValue.fHealthy8 = dr["FamilyHealth8"].ToString();
                    addValue.familyText08 = dr["FamilyHealthText8"].ToString();
                    addValue.lang1 = dr["FamilyLanguage"].ToString();
                    addValue.lang1t01 = dr["FamilyLanguageText"].ToString();
                    addValue.lang2 = dr["FamilyConnectLanguage"].ToString();
                    addValue.lang2t01 = dr["FamilyConnectLanguageText"].ToString();
                    addValue.familyEcological = dr["FamilyProfile1"].ToString();
                    addValue.famailySituation = dr["FamilyProfile2"].ToString();
                    addValue.famailyMedical = dr["FamilyProfile3"].ToString();
                    addValue.famailyActionSituation = dr["FamilyProfile4"].ToString();
                    addValue.fswAssess = dr["FamilyProfile5"].ToString();

                    StaffData = sDB.getStaffDataName(dr["FamilyProfileWriteName"].ToString());
                    addValue.socialName = StaffData[1];
                    addValue.socialDate = DateTime.Parse(dr["FamilyProfileWriteDate"].ToString()).ToString("yyyy-MM-dd");

                    returnValue.StudentData = addValue;

                    StudentHearingInformation addValue2 = new StudentHearingInformation();
                    addValue2.history = dr["StudentDevelop"].ToString();
                    addValue2.history02t01 = dr["StudentDevelopText21"].ToString();
                    addValue2.history02t02 = dr["StudentDevelopText22"].ToString();
                    addValue2.history03t01 = dr["StudentDevelopText31"].ToString();
                    addValue2.history06t01 = dr["StudentDevelopText61"].ToString();
                    addValue2.history06t02 = dr["StudentDevelopText62"].ToString();
                    addValue2.history08t01 = dr["StudentDevelopText81"].ToString();
                    addValue2.history10t01 = dr["StudentDevelopText10"].ToString();
                    addValue2.history11t01 = dr["StudentDevelopText11"].ToString();
                    addValue2.history12t01 = dr["StudentDevelopText12"].ToString();
                    addValue2.problems01t01 = dr["HearingProblemAge"].ToString();
                    addValue2.problems01t02 = dr["HearingProblemMonth"].ToString();
                    addValue2.hearingQ = dr["HearingProblem"].ToString();
                    addValue2.hearingQText = dr["HearingProblemText"].ToString();
                    addValue2.problems02t01 = dr["DiagnoseAge"].ToString();
                    addValue2.problems02t02 = dr["DiagnoseMonth"].ToString();
                    addValue2.problems02t03 = dr["DiagnoseAgency"].ToString();
                    addValue2.problems02t04 = dr["DiagnoseR"].ToString();
                    addValue2.problems02t05 = dr["DiagnoseL"].ToString();
                    addValue2.hearingcheck = dr["NewbornHearing"].ToString();
                    addValue2.hearingYescheck = dr["NewbornHearingInspection"].ToString();
                    addValue2.hearingYesPlace = dr["NewbornHearingInspectionAgency"].ToString();
                    addValue2.hearingYesResultR = dr["NewbornHearingInspectionDiagnoseR"].ToString();
                    addValue2.hearingYesResultL = dr["NewbornHearingInspectionDiagnoseL"].ToString();
                    addValue2.sleepcheck = dr["AuditoryElectrophysiology1"].ToString();
                    addValue2.sleepcheckTime1 = DateTime.Parse(dr["AuditoryElectrophysiology_Date1"].ToString()).ToString("yyyy-MM-dd");//dr["AuditoryElectrophysiology_Date1"].ToString();
                    addValue2.sleepcheckPlace1 = dr["AuditoryElectrophysiology_Agency1"].ToString();
                    addValue2.sleepcheckCheckItem1 = dr["AuditoryElectrophysiology_Item1"].ToString();
                    addValue2.sleepcheckResultL1 = dr["AuditoryElectrophysiology_Diagnose1R"].ToString();
                    addValue2.sleepcheckResultR1 = dr["AuditoryElectrophysiology_Diagnose1L"].ToString();
                    addValue2.sleepcheckTime2 = DateTime.Parse(dr["AuditoryElectrophysiology_Date2"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace2 = dr["AuditoryElectrophysiology_Agency2"].ToString();
                    addValue2.sleepcheckCheckItem2 = dr["AuditoryElectrophysiology_Item2"].ToString();
                    addValue2.sleepcheckResultL2 = dr["AuditoryElectrophysiology_Diagnose2R"].ToString();
                    addValue2.sleepcheckResultR2 = dr["AuditoryElectrophysiology_Diagnose2L"].ToString();
                    addValue2.sleepcheckTime3 = DateTime.Parse(dr["AuditoryElectrophysiology_Date3"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace3 = dr["AuditoryElectrophysiology_Agency3"].ToString();
                    addValue2.sleepcheckCheckItem3 = dr["AuditoryElectrophysiology_Item3"].ToString();
                    addValue2.sleepcheckResultL3 = dr["AuditoryElectrophysiology_Diagnose3R"].ToString();
                    addValue2.sleepcheckResultR3 = dr["AuditoryElectrophysiology_Diagnose3L"].ToString();
                    addValue2.sleepcheckTime4 = DateTime.Parse(dr["AuditoryElectrophysiology_Date4"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace4 = dr["AuditoryElectrophysiology_Agency4"].ToString();
                    addValue2.sleepcheckCheckItem4 = dr["AuditoryElectrophysiology_Item4"].ToString();
                    addValue2.sleepcheckResultL4 = dr["AuditoryElectrophysiology_Diagnose4R"].ToString();
                    addValue2.sleepcheckResultR4 = dr["AuditoryElectrophysiology_Diagnose4L"].ToString();
                    addValue2.sleepcheckTime5 = DateTime.Parse(dr["AuditoryElectrophysiology_Date5"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sleepcheckPlace5 = dr["AuditoryElectrophysiology_Agency5"].ToString();
                    addValue2.sleepcheckCheckItem5 = dr["AuditoryElectrophysiology_Item5"].ToString();
                    addValue2.sleepcheckResultL5 = dr["AuditoryElectrophysiology_Diagnose5R"].ToString();
                    addValue2.sleepcheckResultR5 = dr["AuditoryElectrophysiology_Diagnose5L"].ToString();

                    addValue2.ctmri = dr["CTorMRI"].ToString();
                    addValue2.ctmriTime = DateTime.Parse(dr["CTorMRI_Date1"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.ctmriPlace = dr["CTorMRI_Agency1"].ToString();
                    addValue2.ctmriResultL = dr["CTorMRI_DiagnoseR"].ToString();
                    addValue2.ctmriResultR = dr["CTorMRI_DiagnoseL"].ToString();
                    addValue2.gene = dr["GeneScreening"].ToString();
                    addValue2.geneTime = DateTime.Parse(dr["GeneScreening_Date"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.genePlace = dr["GeneScreening_Agency"].ToString();
                    addValue2.geneResult = dr["GeneScreening_Item"].ToString();
                    addValue2.familyhistory = dr["FamilyHearingProblemHistory"].ToString();
                    addValue2.familyhistoryText = dr["FamilyHearingProblemHistoryText"].ToString();
                    addValue2.changehistory = dr["HearingChangeHistory"].ToString();
                    addValue2.changehistoryText = dr["HearingChangeHistoryText"].ToString();
                    addValue2.assistmanage = dr["AidsManagement"].ToString();
                    addValue2.accessory = dr["AidsManagementTextAge"].ToString();
                    addValue2.assistmanageR = dr["HearingAids_R"].ToString();
                    addValue2.brandR = dr["AidsBrand_R"].ToString();
                    addValue2.modelR = dr["AidsModel_R"].ToString();
                    addValue2.buyingPlaceR = dr["AidsOptionalLocation_R"].ToString();
                    addValue2.buyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.insertHospitalR = dr["EEarHospital_R"].ToString();
                    addValue2.openHzDateR = DateTime.Parse(dr["EEarOpen_R"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.assistmanageL = dr["HearingAids_L"].ToString();
                    addValue2.brandL = dr["AidsBrand_L"].ToString();
                    addValue2.modelL = dr["AidsModel_L"].ToString();
                    addValue2.buyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.buyingPlaceL = dr["AidsOptionalLocation_L"].ToString();
                    addValue2.insertHospitalL = dr["EEarHospital_L"].ToString();
                    addValue2.openHzDateL = DateTime.Parse(dr["EEarOpen_L"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.allassis = dr["AllDayAids"].ToString();
                    addValue2.allassisNoText = dr["AllDayAidsText"].ToString();
                    addValue2.assis1 = dr["ActiveReaction"].ToString();
                    addValue2.assis1NoText = dr["ActiveReactionText"].ToString();
                    addValue2.assis2 = dr["BasicCare"].ToString();
                    addValue2.assis2NoText = dr["BasicCareText"].ToString();
                    addValue2.assisFM = dr["UseFMsystem"].ToString();
                    addValue2.assisFMBrand = dr["UseFMsystemBrand"].ToString();
                    addValue2.assessnotes = dr["HearingAssessmentSummary"].ToString();
                    addValue2.assessnotes1 = dr["EarEndoscopy"].ToString();
                    addValue2.assessnotes102Text = dr["EarEndoscopyAbnormalText"].ToString();
                    addValue2.problems11t02 = dr["PureToneText"].ToString();
                    addValue2.assessnotes2 = dr["Tympanogram"].ToString();
                    addValue2.problems11t03 = dr["SATorSDT"].ToString();
                    addValue2.problems11t04 = dr["SpeechRecognition"].ToString();
                    addValue2.problems11t05 = dr["AidsSystem"].ToString();
                    addValue2.problems11t06 = dr["HearingInspection"].ToString();
                    addValue2.problems11t07 = dr["HearingOther"].ToString();
                    addValue2.inspectorID = dr["Audiologist"].ToString();
                    addValue2.inspectorDate = DateTime.Parse(dr["SurveyingDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.HearingData = addValue2;


                    StudentTeachingInformation addValue4 = new StudentTeachingInformation();
                    addValue4.case1 = dr["CaseFamilyCooperative"].ToString();
                    addValue4.case1t04 = dr["EntrustOthers"].ToString();
                    addValue4.case1t05 = dr["CooperativeDifficulty"].ToString();
                    addValue4.case2 = dr["RearingAttitude"].ToString();
                    addValue4.case21 = dr["RearingAttitude1"].ToString();
                    addValue4.case21t01 = dr["RearingAttitude1Text"].ToString();
                    addValue4.case22 = dr["RearingAttitude2"].ToString();
                    addValue4.case22t01 = dr["RearingAttitude2Text"].ToString();
                    addValue4.case23 = dr["RearingAttitude3"].ToString();
                    addValue4.case23t01 = dr["RearingAttitude3Text"].ToString();
                    addValue4.case24 = dr["RearingAttitude4"].ToString();
                    addValue4.case24t01 = dr["RearingAttitude4Text"].ToString();
                    addValue4.case25 = dr["RearingAttitude5"].ToString();
                    addValue4.case25t01 = dr["RearingAttitude5Text"].ToString();
                    addValue4.case26 = dr["RearingAttitude6"].ToString();
                    addValue4.case26t01 = dr["RearingAttitude6Text"].ToString();
                    addValue4.case3 = dr["Dispositional"].ToString();
                    addValue4.case31 = dr["Dispositional1"].ToString();
                    addValue4.case31t01 = dr["Dispositional1Text"].ToString();
                    addValue4.case32 = dr["Dispositional2"].ToString();
                    addValue4.case32t01 = dr["Dispositional2Text"].ToString();
                    addValue4.case33 = dr["Dispositional3"].ToString();
                    addValue4.case33t01 = dr["Dispositional3Text"].ToString();
                    addValue4.case34 = dr["Dispositional4"].ToString();
                    addValue4.case34t01 = dr["Dispositional4Text"].ToString();
                    addValue4.attend = dr["Attendance"].ToString();
                    addValue4.attend01t01 = dr["AttendanceText"].ToString();
                    addValue4.accompany = dr["TeachEscorts"].ToString();
                    addValue4.accompany01t01 = dr["TeachEscortsText"].ToString();
                    addValue4.teach = dr["AfterTeach"].ToString();
                    addValue4.teach01t01 = dr["AfterTeachText"].ToString();
                    addValue4.caseQ = dr["MajorProblem"].ToString();
                    addValue4.caseQ01t01 = dr["MajorProblemText"].ToString();
                    addValue4.OtherRemark1 = dr["RemarkOther"].ToString();
                    addValue4.case4 = dr["LearningAttitude"].ToString();
                    addValue4.case5 = dr["Temperament"].ToString();
                    addValue4.case6 = dr["Activity"].ToString();
                    addValue4.case7 = dr["Focus"].ToString();
                    addValue4.case8 = dr["Persistence"].ToString();
                    addValue4.case9 = dr["CommunicationBehavior"].ToString();
                    addValue4.case10 = dr["AuditorySkill"].ToString();
                    addValue4.case11 = dr["Acknowledge"].ToString();
                    addValue4.case12 = dr["Language"].ToString();
                    addValue4.case12t01 = dr["LanguageText"].ToString();
                    addValue4.case13 = dr["Action"].ToString();
                    addValue4.wear = dr["WearAids"].ToString();
                    addValue4.mind = dr["SpiritAndCoordinate"].ToString();
                    addValue4.mind01t01 = dr["SpiritAndCoordinateOther"].ToString();
                    addValue4.connectwish = dr["Communication"].ToString();
                    addValue4.studywish = dr["LearningDesire"].ToString();
                    addValue4.related = dr["AttachmentData"].ToString();
                    addValue4.related01t01 = dr["AttachmentDataText"].ToString();
                    addValue4.OtherRemark2 = dr["RemarkOther2"].ToString();
                    addValue4.case14 = dr["ExistingResources"].ToString();
                    addValue4.trusteeship = dr["InterventionAgencies"].ToString();
                    addValue4.case14t01 = dr["HospitalIntervention"].ToString();
                    addValue4.proceed = dr["RelatedIntervention"].ToString();
                    addValue4.proceedt01 = dr["RelatedInterventionText"].ToString();
                    addValue4.preschools = dr["InNurserySchool"].ToString();
                    addValue4.case15 = dr["HearingRehabilitation"].ToString();
                    addValue4.Rehabilitation1 = dr["CaseNeed"].ToString();
                    addValue4.Rehabilitation2 = dr["CaseFamilyEnthusiasm"].ToString();
                    addValue4.Rehabilitation3 = dr["Rehousing"].ToString();
                    addValue4.Rehabilitation3Text = dr["RehousingText"].ToString();
                    addValue4.case16 = dr["CaseFamilyCourseProposal"].ToString();
                    addValue4.OtherRemark3 = dr["CaseFamilyCourseProposalText"].ToString();
                    addValue4.case17 = dr["CaseCourseProposal"].ToString();
                    addValue4.OtherRemark4 = dr["CaseCourseProposalText"].ToString();
                    addValue4.teacherID = dr["Teacher"].ToString();
                    addValue4.teacherDate = DateTime.Parse(dr["TeacherDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.TeachData = addValue4;

                }
                dr.Close();


                List<StudentBodyInformation> addValue3 = new List<StudentBodyInformation>();


                sql = "SELECT * FROM StudentHeightWeightRecord WHERE isDeleted=0 AND StudentID=(@ID)";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.NVarChar).Value = returnValue.StudentData.studentID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StudentBodyInformation addValue3item = new StudentBodyInformation();
                    addValue3item.RecordID = dr["hwID"].ToString();
                    addValue3item.RecordDate = DateTime.Parse(dr["hwDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue3item.studentID = dr["StudentID"].ToString();
                    addValue3item.RecordHeight = dr["StudentHeight"].ToString();
                    addValue3item.RecordWeight = dr["StudentWeight"].ToString();
                    addValue3item.RecordRemark = dr["hwRemark"].ToString();
                    addValue3.Add(addValue3item);
                }
                dr.Close();
                returnValue.BodyData = addValue3;
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message;
            }
        }
        return returnValue;
    }

    public StudentData1 getStudentData1(string ID)
    {
//        select A.StudentID, A.StudentName,A.StudentIdentity,A.ConsultDate,A.EvaluationDate,A.Updated,
//A.StudentBirthday,B.UnitName, C.StatusName,D.SexName,
//(A.AddressZip1+E.CityName + A.AddressOther1) as '居住地' ,
//(A.AddressZip2+F.CityName + A.AddressOther2) as '戶籍地' 
//from StudentDatabase A 
//left join StaffUnit B on A.Unit=B.ID
//left join CaseStatus C on A.caseStatu=C.ID 
//left join StudentSex D on A.StudentSex=D.ID 
//left join CityName E on A.AddressCity1=E.ID
//left join CityName F on A.AddressCity2=F.ID
//where isDeleted=0


        StudentData1 returnValue = new StudentData1();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> StaffData = sDB.getStaffDataName(dr["WriteName"].ToString());
                    returnValue.fillInName = StaffData[1];

                    returnValue.ID = dr["ID"].ToString();
                    returnValue.assessDate = DateTime.Parse(dr["EvaluationDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.consultationDate = DateTime.Parse(dr["ConsultDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.upDate = DateTime.Parse(dr["Updated"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.caseStatu = dr["CaseStatu"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentTWID = dr["StudentIdentity"].ToString();
                    returnValue.censusAddressZip = dr["AddressZip1"].ToString();
                    returnValue.censusAddressCity = dr["AddressCity1"].ToString();
                    returnValue.censusAddress = dr["AddressOther1"].ToString();
                    returnValue.addressZip = dr["AddressZip2"].ToString();
                    returnValue.addressCity = dr["AddressCity2"].ToString();
                    returnValue.address = dr["AddressOther2"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.studentSex = dr["StudentSex"].ToString();
                    returnValue.firstClassDate = DateTime.Parse(dr["ClassDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.guaranteeDate = DateTime.Parse(dr["GuaranteeDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.endReasonDate = DateTime.Parse(dr["CompletedDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.endReasonType = dr["CompletedType"].ToString();
                    returnValue.endReason = dr["CompletedReason"].ToString();
                    returnValue.sendDateSince = DateTime.Parse(dr["ShortEndDateSince"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sendDateUntil = DateTime.Parse(dr["ShortEndDateUntil"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.nomembershipType = dr["NomembershipType"].ToString();
                    returnValue.nomembershipReason = dr["NomembershipReason"].ToString();
                    returnValue.studentPhoto = dr["StudentAvatar"].ToString();
                    returnValue.wCare = dr["CaregiversDaytime"].ToString();
                    returnValue.wCareName = dr["CaregiversDaytimeText"].ToString();
                    returnValue.bCare = dr["CaregiversNight"].ToString();
                    returnValue.bCareName = dr["CaregiversNightText"].ToString();

                    returnValue.fPRelation1 = dr["ContactRelation1"].ToString();
                    returnValue.fPName1 = dr["ContactName1"].ToString();
                    returnValue.fPTel1 = dr["ContactTel_company1"].ToString();
                    returnValue.fPPhone1 = dr["ContactPhone1"].ToString();
                    returnValue.fPHPhone1 = dr["ContactTel_home1"].ToString();
                    returnValue.fPFax1 = dr["ContactFax1"].ToString();
                    returnValue.fPRelation2 = dr["ContactRelation2"].ToString();
                    returnValue.fPName2 = dr["ContactName2"].ToString();
                    returnValue.fPTel2 = dr["ContactTel_company2"].ToString();
                    returnValue.fPPhone2 = dr["ContactPhone2"].ToString();
                    returnValue.fPHPhone2 = dr["ContactTel_home2"].ToString();
                    returnValue.fPFax2 = dr["ContactFax2"].ToString();
                    returnValue.fPRelation3 = dr["ContactRelation3"].ToString();
                    returnValue.fPName3 = dr["ContactName3"].ToString();
                    returnValue.fPTel3 = dr["ContactTel_company3"].ToString();
                    returnValue.fPPhone3 = dr["ContactPhone3"].ToString();
                    returnValue.fPHPhone3 = dr["ContactTel_home3"].ToString();
                    returnValue.fPFax3 = dr["ContactFax3"].ToString();
                    returnValue.fPRelation4 = dr["ContactRelation4"].ToString();
                    returnValue.fPName4 = dr["ContactName4"].ToString();
                    returnValue.fPTel4 = dr["ContactTel_company4"].ToString();
                    returnValue.fPPhone4 = dr["ContactPhone4"].ToString();
                    returnValue.fPHPhone4 = dr["ContactTel_home4"].ToString();
                    returnValue.fPFax4 = dr["ContactFax4"].ToString();
                    returnValue.email = dr["StudentEmail"].ToString();
                    returnValue.sourceType = dr["ReferralSourceType"].ToString();
                    returnValue.sourceName = dr["ReferralSource"].ToString();
                    returnValue.manualWhether = dr["PhysicalAndMentalDisabilityHandbook"].ToString();
                    returnValue.manualCategory1 = dr["DisabilityCategory1"].ToString();
                    returnValue.manualGrade1 = dr["DisabilityGrade1"].ToString();
                    returnValue.manualCategory2 = dr["DisabilityCategory2"].ToString();
                    returnValue.manualGrade2 = dr["DisabilityGrade2"].ToString();
                    returnValue.manualCategory3 = dr["DisabilityCategory3"].ToString();
                    returnValue.manualGrade3 = dr["DisabilityGrade3"].ToString();
                    returnValue.manualNo = dr["NoDisabilityHandbook"].ToString();
                    returnValue.manualUnit = dr["ApplyDisabilityHandbook"].ToString();
                    returnValue.studentManualImg = dr["DisabilityProve"].ToString();
                    returnValue.notificationWhether = dr["Notify"].ToString();
                    returnValue.notificationUnit = dr["Notify_Unit"].ToString();
                    returnValue.notificationManage = dr["Notify_Member"].ToString();
                    returnValue.notificationTel = dr["Notify_Tel"].ToString();
                    returnValue.notificationDate = DateTime.Parse(dr["Notify_Date"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.notificationCity = dr["Notify_City"].ToString();
                    returnValue.economy = dr["EconomyState"].ToString();
                    returnValue.economyLow = dr["EconomyLow"].ToString();
                    

                }
                dr.Close();
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message.ToString();
            }
        }
        return returnValue;
    }

    public StudentData2 getStudentData2(string ID)
    {
        StudentData2 returnValue = new StudentData2();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataBase sDB = new StaffDataBase();
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.fAppellation1 = dr["FamilyAppellation1"].ToString();
                    returnValue.fName1 = dr["FamilyName1"].ToString();
                    returnValue.fBirthday1 = DateTime.Parse(dr["FamilyBirthday1"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU1 = dr["FamilyEducation1"].ToString();
                    returnValue.fProfession1 = dr["FamilyProfession1"].ToString();
                    returnValue.fLive1 = dr["FamilyLive1"].ToString();
                    returnValue.fHearing1 = dr["FamilyHearing1"].ToString();
                    returnValue.fHealthy1 = dr["FamilyHealth1"].ToString();
                    returnValue.familyText01 = dr["FamilyHealthText1"].ToString();
                    returnValue.fAppellation2 = dr["FamilyAppellation2"].ToString();
                    returnValue.fName2 = dr["FamilyName2"].ToString();
                    returnValue.fBirthday2 = DateTime.Parse(dr["FamilyBirthday2"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU2 = dr["FamilyEducation2"].ToString();
                    returnValue.fProfession2 = dr["FamilyProfession2"].ToString();
                    returnValue.fLive2 = dr["FamilyLive2"].ToString();
                    returnValue.fHearing2 = dr["FamilyHearing2"].ToString();
                    returnValue.fHealthy2 = dr["FamilyHealth2"].ToString();
                    returnValue.familyText02 = dr["FamilyHealthText2"].ToString();
                    returnValue.fAppellation3 = dr["FamilyAppellation3"].ToString();
                    returnValue.fName3 = dr["FamilyName3"].ToString();
                    returnValue.fBirthday3 = DateTime.Parse(dr["FamilyBirthday3"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU3 = dr["FamilyEducation3"].ToString();
                    returnValue.fProfession3 = dr["FamilyProfession3"].ToString();
                    returnValue.fLive3 = dr["FamilyLive3"].ToString();
                    returnValue.fHearing3 = dr["FamilyHearing3"].ToString();
                    returnValue.fHealthy3 = dr["FamilyHealth3"].ToString();
                    returnValue.familyText03 = dr["FamilyHealthText3"].ToString();
                    returnValue.fAppellation4 = dr["FamilyAppellation4"].ToString();
                    returnValue.fName4 = dr["FamilyName4"].ToString();
                    returnValue.fBirthday4 = DateTime.Parse(dr["FamilyBirthday4"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU4 = dr["FamilyEducation4"].ToString();
                    returnValue.fProfession4 = dr["FamilyProfession4"].ToString();
                    returnValue.fLive4 = dr["FamilyLive4"].ToString();
                    returnValue.fHearing4 = dr["FamilyHearing4"].ToString();
                    returnValue.fHealthy4 = dr["FamilyHealth4"].ToString();
                    returnValue.familyText04 = dr["FamilyHealthText4"].ToString();
                    returnValue.fAppellation5 = dr["FamilyAppellation5"].ToString();
                    returnValue.fName5 = dr["FamilyName5"].ToString();
                    returnValue.fBirthday5 = DateTime.Parse(dr["FamilyBirthday5"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU5 = dr["FamilyEducation5"].ToString();
                    returnValue.fProfession5 = dr["FamilyProfession5"].ToString();
                    returnValue.fLive5 = dr["FamilyLive5"].ToString();
                    returnValue.fHearing5 = dr["FamilyHearing5"].ToString();
                    returnValue.fHealthy5 = dr["FamilyHealth5"].ToString();
                    returnValue.familyText05 = dr["FamilyHealthText5"].ToString();
                    returnValue.fAppellation6 = dr["FamilyAppellation6"].ToString();
                    returnValue.fName6 = dr["FamilyName6"].ToString();
                    returnValue.fBirthday6 = DateTime.Parse(dr["FamilyBirthday6"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU6 = dr["FamilyEducation6"].ToString();
                    returnValue.fProfession6 = dr["FamilyProfession6"].ToString();
                    returnValue.fLive6 = dr["FamilyLive6"].ToString();
                    returnValue.fHearing6 = dr["FamilyHearing6"].ToString();
                    returnValue.fHealthy6 = dr["FamilyHealth6"].ToString();
                    returnValue.familyText06 = dr["FamilyHealthText6"].ToString();
                    returnValue.fAppellation7 = dr["FamilyAppellation7"].ToString();
                    returnValue.fName7 = dr["FamilyName7"].ToString();
                    returnValue.fBirthday7 = DateTime.Parse(dr["FamilyBirthday7"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU7 = dr["FamilyEducation7"].ToString();
                    returnValue.fProfession7 = dr["FamilyProfession7"].ToString();
                    returnValue.fLive7 = dr["FamilyLive7"].ToString();
                    returnValue.fHearing7 = dr["FamilyHearing7"].ToString();
                    returnValue.fHealthy7 = dr["FamilyHealth7"].ToString();
                    returnValue.familyText07 = dr["FamilyHealthText7"].ToString();
                    returnValue.fAppellation8 = dr["FamilyAppellation8"].ToString();
                    returnValue.fName8 = dr["FamilyName8"].ToString();
                    returnValue.fBirthday8 = DateTime.Parse(dr["FamilyBirthday8"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fEDU8 = dr["FamilyEducation8"].ToString();
                    returnValue.fProfession8 = dr["FamilyProfession8"].ToString();
                    returnValue.fLive8 = dr["FamilyLive8"].ToString();
                    returnValue.fHearing8 = dr["FamilyHearing8"].ToString();
                    returnValue.fHealthy8 = dr["FamilyHealth8"].ToString();
                    returnValue.familyText08 = dr["FamilyHealthText8"].ToString();
                    returnValue.lang1 = dr["FamilyLanguage"].ToString();
                    returnValue.lang1t01 = dr["FamilyLanguageText"].ToString();
                    returnValue.lang2 = dr["FamilyConnectLanguage"].ToString();
                    returnValue.lang2t01 = dr["FamilyConnectLanguageText"].ToString();


                }
                dr.Close();
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message.ToString();
            }
        }
        return returnValue;
    }
    public StudentData4 getStudentData4(string ID)
    {
        StudentData4 returnValue = new StudentData4();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataBase sDB = new StaffDataBase();

                    returnValue.ID = dr["ID"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.familyEcological = dr["FamilyProfile1"].ToString();
                    returnValue.famailySituation = dr["FamilyProfile2"].ToString();
                    returnValue.famailyMedical = dr["FamilyProfile3"].ToString();
                    returnValue.famailyActionSituation = dr["FamilyProfile4"].ToString();
                    returnValue.fswAssess = dr["FamilyProfile5"].ToString();

                    List<string>  StaffData = sDB.getStaffDataName(dr["FamilyProfileWriteName"].ToString());
                    returnValue.socialName = StaffData[1];
                    returnValue.socialDate = DateTime.Parse(dr["FamilyProfileWriteDate"].ToString()).ToString("yyyy-MM-dd");


                }
                dr.Close();
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message.ToString();
            }
        }
        return returnValue;
    }
    public List<StudentWelfareResource> getStudentData3(string ID)
    {

        List<StudentWelfareResource> returnValue = new List<StudentWelfareResource>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StudentWelfareResource.*, ResourceCard.ORGName, ResourceCard.ORGItem, ResourceCard.Category FROM StudentWelfareResource " +
                             "INNER JOIN StudentDatabase ON StudentWelfareResource.StudentID=StudentDatabase.StudentID " +
                             "INNER JOIN ResourceCard ON StudentWelfareResource.ORGID=ResourceCard.ID " +
                             "WHERE StudentWelfareResource.isDeleted=0 AND StudentDatabase.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StudentWelfareResource addValue = new StudentWelfareResource();
                    addValue.wID = dr["wID"].ToString();
                    addValue.studentID = dr["StudentID"].ToString();
                    addValue.resourceDate = DateTime.Parse(dr["ORGDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.resourceID = dr["ORGID"].ToString();
                    addValue.resourceName = dr["ORGName"].ToString();
                    addValue.resourceItem = dr["ORGItem"].ToString();
                    addValue.resourceType = dr["Category"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StudentWelfareResource addValue = new StudentWelfareResource();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
            return returnValue;
        } 
    }

    public StudentHearingInformation getStudentHearingInfo(string ID)
    {
        StudentHearingInformation returnValue = new StudentHearingInformation();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.history = dr["StudentDevelop"].ToString();
                    returnValue.history02t01 = dr["StudentDevelopText21"].ToString();
                    returnValue.history02t02 = dr["StudentDevelopText22"].ToString();
                    returnValue.history03t01 = dr["StudentDevelopText31"].ToString();
                    returnValue.history06t01 = dr["StudentDevelopText61"].ToString();
                    returnValue.history06t02 = dr["StudentDevelopText62"].ToString();
                    returnValue.history08t01 = dr["StudentDevelopText81"].ToString();
                    returnValue.history10t01 = dr["StudentDevelopText10"].ToString();
                    returnValue.history11t01 = dr["StudentDevelopText11"].ToString();
                    returnValue.history12t01 = dr["StudentDevelopText12"].ToString();
                    returnValue.problems01t01 = dr["HearingProblemAge"].ToString();
                    returnValue.problems01t02 = dr["HearingProblemMonth"].ToString();
                    returnValue.hearingQ = dr["HearingProblem"].ToString();
                    returnValue.hearingQText = dr["HearingProblemText"].ToString();
                    returnValue.problems02t01 = dr["DiagnoseAge"].ToString();
                    returnValue.problems02t02 = dr["DiagnoseMonth"].ToString();
                    returnValue.problems02t03 = dr["DiagnoseAgency"].ToString();
                    returnValue.problems02t04 = dr["DiagnoseR"].ToString();
                    returnValue.problems02t05 = dr["DiagnoseL"].ToString();
                    returnValue.hearingcheck = dr["NewbornHearing"].ToString();
                    returnValue.hearingYescheck = dr["NewbornHearingInspection"].ToString();
                    returnValue.hearingYesPlace = dr["NewbornHearingInspectionAgency"].ToString();
                    returnValue.hearingYesResultR = dr["NewbornHearingInspectionDiagnoseR"].ToString();
                    returnValue.hearingYesResultL = dr["NewbornHearingInspectionDiagnoseL"].ToString();
                    returnValue.sleepcheck = dr["AuditoryElectrophysiology1"].ToString();
                    returnValue.sleepcheckTime1 = DateTime.Parse(dr["AuditoryElectrophysiology_Date1"].ToString()).ToString("yyyy-MM-dd");//dr["AuditoryElectrophysiology_Date1"].ToString();
                    returnValue.sleepcheckPlace1 = dr["AuditoryElectrophysiology_Agency1"].ToString();
                    returnValue.sleepcheckCheckItem1 = dr["AuditoryElectrophysiology_Item1"].ToString();
                    returnValue.sleepcheckResultL1 = dr["AuditoryElectrophysiology_Diagnose1R"].ToString();
                    returnValue.sleepcheckResultR1 = dr["AuditoryElectrophysiology_Diagnose1L"].ToString();
                    returnValue.sleepcheckTime2 = DateTime.Parse(dr["AuditoryElectrophysiology_Date2"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sleepcheckPlace2 = dr["AuditoryElectrophysiology_Agency2"].ToString();
                    returnValue.sleepcheckCheckItem2 = dr["AuditoryElectrophysiology_Item2"].ToString();
                    returnValue.sleepcheckResultL2 = dr["AuditoryElectrophysiology_Diagnose2R"].ToString();
                    returnValue.sleepcheckResultR2 = dr["AuditoryElectrophysiology_Diagnose2L"].ToString();
                    returnValue.sleepcheckTime3 = DateTime.Parse(dr["AuditoryElectrophysiology_Date3"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sleepcheckPlace3 = dr["AuditoryElectrophysiology_Agency3"].ToString();
                    returnValue.sleepcheckCheckItem3 = dr["AuditoryElectrophysiology_Item3"].ToString();
                    returnValue.sleepcheckResultL3 = dr["AuditoryElectrophysiology_Diagnose3R"].ToString();
                    returnValue.sleepcheckResultR3 = dr["AuditoryElectrophysiology_Diagnose3L"].ToString();
                    returnValue.sleepcheckTime4 = DateTime.Parse(dr["AuditoryElectrophysiology_Date4"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sleepcheckPlace4 = dr["AuditoryElectrophysiology_Agency4"].ToString();
                    returnValue.sleepcheckCheckItem4 = dr["AuditoryElectrophysiology_Item4"].ToString();
                    returnValue.sleepcheckResultL4 = dr["AuditoryElectrophysiology_Diagnose4R"].ToString();
                    returnValue.sleepcheckResultR4 = dr["AuditoryElectrophysiology_Diagnose4L"].ToString();
                    returnValue.sleepcheckTime5 = DateTime.Parse(dr["AuditoryElectrophysiology_Date5"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sleepcheckPlace5 = dr["AuditoryElectrophysiology_Agency5"].ToString();
                    returnValue.sleepcheckCheckItem5 = dr["AuditoryElectrophysiology_Item5"].ToString();
                    returnValue.sleepcheckResultL5 = dr["AuditoryElectrophysiology_Diagnose5R"].ToString();
                    returnValue.sleepcheckResultR5 = dr["AuditoryElectrophysiology_Diagnose5L"].ToString();

                    returnValue.ctmri = dr["CTorMRI"].ToString();
                    returnValue.ctmriTime = DateTime.Parse(dr["CTorMRI_Date1"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ctmriPlace = dr["CTorMRI_Agency1"].ToString();
                    returnValue.ctmriResultL = dr["CTorMRI_DiagnoseR"].ToString();
                    returnValue.ctmriResultR = dr["CTorMRI_DiagnoseL"].ToString();
                    returnValue.gene = dr["GeneScreening"].ToString();
                    returnValue.geneTime = DateTime.Parse(dr["GeneScreening_Date"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.genePlace = dr["GeneScreening_Agency"].ToString();
                    returnValue.geneResult = dr["GeneScreening_Item"].ToString();
                    returnValue.familyhistory = dr["FamilyHearingProblemHistory"].ToString();
                    returnValue.familyhistoryText = dr["FamilyHearingProblemHistoryText"].ToString();
                    returnValue.changehistory = dr["HearingChangeHistory"].ToString();
                    returnValue.changehistoryText = dr["HearingChangeHistoryText"].ToString();
                    returnValue.assistmanage = dr["AidsManagement"].ToString();
                    returnValue.accessory = dr["AidsManagementTextAge"].ToString();
                    returnValue.assistmanageR = dr["HearingAids_R"].ToString();
                    returnValue.brandR = dr["AidsBrand_R"].ToString();
                    returnValue.modelR = dr["AidsModel_R"].ToString();
                    returnValue.buyingPlaceR = dr["AidsOptionalLocation_R"].ToString();
                    returnValue.buyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.insertHospitalR = dr["EEarHospital_R"].ToString();
                    returnValue.openHzDateR = DateTime.Parse(dr["EEarOpen_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.assistmanageL = dr["HearingAids_L"].ToString();
                    returnValue.brandL = dr["AidsBrand_L"].ToString();
                    returnValue.modelL = dr["AidsModel_L"].ToString();
                    returnValue.buyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.buyingPlaceL = dr["AidsOptionalLocation_L"].ToString();
                    returnValue.insertHospitalL = dr["EEarHospital_L"].ToString();
                    returnValue.openHzDateL = DateTime.Parse(dr["EEarOpen_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.allassis = dr["AllDayAids"].ToString();
                    returnValue.allassisNoText = dr["AllDayAidsText"].ToString();
                    returnValue.assis1 = dr["ActiveReaction"].ToString();
                    returnValue.assis1NoText = dr["ActiveReactionText"].ToString();
                    returnValue.assis2 = dr["BasicCare"].ToString();
                    returnValue.assis2NoText = dr["BasicCareText"].ToString();
                    returnValue.assisFM = dr["UseFMsystem"].ToString();
                    returnValue.assisFMBrand = dr["UseFMsystemBrand"].ToString();
                    returnValue.assessnotes = dr["HearingAssessmentSummary"].ToString();
                    returnValue.assessnotes1 = dr["EarEndoscopy"].ToString();
                    returnValue.assessnotes102Text = dr["EarEndoscopyAbnormalText"].ToString();
                    returnValue.problems11t02 = dr["PureToneText"].ToString();
                    returnValue.assessnotes2 = dr["Tympanogram"].ToString();
                    returnValue.problems11t03 = dr["SATorSDT"].ToString();
                    returnValue.problems11t04 = dr["SpeechRecognition"].ToString();
                    returnValue.problems11t05 = dr["AidsSystem"].ToString();
                    returnValue.problems11t06 = dr["HearingInspection"].ToString();
                    returnValue.problems11t07 = dr["HearingOther"].ToString();

                    StaffDataBase sDB = new StaffDataBase();
                    List<string> StaffData = sDB.getStaffDataName(dr["Audiologist"].ToString());
                    returnValue.inspectorID = dr["Audiologist"].ToString();
                    returnValue.inspectorName = StaffData[1];
                    returnValue.inspectorDate = DateTime.Parse(dr["SurveyingDate"].ToString()).ToString("yyyy-MM-dd");
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message.ToString();
            }
            return returnValue;
        } 
    }
    public StudentTeachingInformation getStudentData7(string ID) {
        StudentTeachingInformation returnValue = new StudentTeachingInformation();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StudentDatabase WHERE isDeleted=0 AND ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.case1 = dr["CaseFamilyCooperative"].ToString();
                    returnValue.case1t04 = dr["EntrustOthers"].ToString();
                    returnValue.case1t05 = dr["CooperativeDifficulty"].ToString();
                    returnValue.case2 = dr["RearingAttitude"].ToString();
                    returnValue.case21 = dr["RearingAttitude1"].ToString();
                    returnValue.case21t01 = dr["RearingAttitude1Text"].ToString();
                    returnValue.case22 = dr["RearingAttitude2"].ToString();
                    returnValue.case22t01 = dr["RearingAttitude2Text"].ToString();
                    returnValue.case23 = dr["RearingAttitude3"].ToString();
                    returnValue.case23t01 = dr["RearingAttitude3Text"].ToString();
                    returnValue.case24 = dr["RearingAttitude4"].ToString();
                    returnValue.case24t01 = dr["RearingAttitude4Text"].ToString();
                    returnValue.case25 = dr["RearingAttitude5"].ToString();
                    returnValue.case25t01 = dr["RearingAttitude5Text"].ToString();
                    returnValue.case26 = dr["RearingAttitude6"].ToString();
                    returnValue.case26t01 = dr["RearingAttitude6Text"].ToString();
                    returnValue.case3 = dr["Dispositional"].ToString();
                    returnValue.case31 = dr["Dispositional1"].ToString();
                    returnValue.case31t01 = dr["Dispositional1Text"].ToString();
                    returnValue.case32 = dr["Dispositional2"].ToString();
                    returnValue.case32t01 = dr["Dispositional2Text"].ToString();
                    returnValue.case33 = dr["Dispositional3"].ToString();
                    returnValue.case33t01 = dr["Dispositional3Text"].ToString();
                    returnValue.case34 = dr["Dispositional4"].ToString();
                    returnValue.case34t01 = dr["Dispositional4Text"].ToString();
                    returnValue.attend = dr["Attendance"].ToString();
                    returnValue.attend01t01 = dr["AttendanceText"].ToString();
                    returnValue.accompany = dr["TeachEscorts"].ToString();
                    returnValue.accompany01t01 = dr["TeachEscortsText"].ToString();
                    returnValue.teach = dr["AfterTeach"].ToString();
                    returnValue.teach01t01 = dr["AfterTeachText"].ToString();
                    returnValue.caseQ = dr["MajorProblem"].ToString();
                    returnValue.caseQ01t01 = dr["MajorProblemText"].ToString();
                    returnValue.OtherRemark1 = dr["RemarkOther"].ToString();
                    returnValue.case4 = dr["LearningAttitude"].ToString();
                    returnValue.case5 = dr["Temperament"].ToString();
                    returnValue.case6 = dr["Activity"].ToString();
                    returnValue.case7 = dr["Focus"].ToString();
                    returnValue.case8 = dr["Persistence"].ToString();
                    returnValue.case9 = dr["CommunicationBehavior"].ToString();
                    returnValue.case10 = dr["AuditorySkill"].ToString();
                    returnValue.case11 = dr["Acknowledge"].ToString();
                    returnValue.case12 = dr["Language"].ToString();
                    returnValue.case12t01 = dr["LanguageText"].ToString();
                    returnValue.case13 = dr["Action"].ToString();
                    returnValue.wear = dr["WearAids"].ToString();
                    returnValue.mind = dr["SpiritAndCoordinate"].ToString();
                    returnValue.mind01t01 = dr["SpiritAndCoordinateOther"].ToString();
                    returnValue.connectwish = dr["Communication"].ToString();
                    returnValue.studywish = dr["LearningDesire"].ToString();
                    returnValue.related = dr["AttachmentData"].ToString();
                    returnValue.related01t01 = dr["AttachmentDataText"].ToString();
                    returnValue.OtherRemark2 = dr["RemarkOther2"].ToString();
                    returnValue.case14 = dr["ExistingResources"].ToString();
                    returnValue.trusteeship = dr["InterventionAgencies"].ToString();
                    returnValue.case14t01 = dr["HospitalIntervention"].ToString();
                    returnValue.proceed = dr["RelatedIntervention"].ToString();
                    returnValue.proceedt01 = dr["RelatedInterventionText"].ToString();
                    returnValue.preschools = dr["InNurserySchool"].ToString();
                    returnValue.case15 = dr["HearingRehabilitation"].ToString();
                    returnValue.Rehabilitation1 = dr["CaseNeed"].ToString();
                    returnValue.Rehabilitation2 = dr["CaseFamilyEnthusiasm"].ToString();
                    returnValue.Rehabilitation3 = dr["Rehousing"].ToString();
                    returnValue.Rehabilitation3Text = dr["RehousingText"].ToString();
                    returnValue.case16 = dr["CaseFamilyCourseProposal"].ToString();
                    returnValue.OtherRemark3 = dr["CaseFamilyCourseProposalText"].ToString();
                    returnValue.case17 = dr["CaseCourseProposal"].ToString();
                    returnValue.OtherRemark4 = dr["CaseCourseProposalText"].ToString();
                    returnValue.teacherDate = DateTime.Parse(dr["TeacherDate"].ToString()).ToString("yyyy-MM-dd");

                    StaffDataBase sDB = new StaffDataBase();
                    List<string> StaffData = sDB.getStaffDataName(dr["Teacher"].ToString());
                    returnValue.teacherID = dr["Teacher"].ToString();
                    returnValue.teacherName = StaffData[1];
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message.ToString();
            }
            return returnValue;
        } 
    }

    public List<StudentBodyInformation> getStudentData8(string ID)
    {
        List<StudentBodyInformation> returnValue = new List<StudentBodyInformation>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StudentHeightWeightRecord.* FROM StudentHeightWeightRecord "+
                             "INNER JOIN StudentDatabase ON StudentHeightWeightRecord.StudentID=StudentDatabase.StudentID WHERE StudentHeightWeightRecord.isDeleted=0 AND StudentDatabase.ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StudentBodyInformation addValue = new StudentBodyInformation();
                    addValue.RecordID = dr["hwID"].ToString();
                    addValue.RecordDate = DateTime.Parse(dr["hwDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.studentID = dr["StudentID"].ToString();
                    addValue.RecordHeight = dr["StudentHeight"].ToString();
                    addValue.RecordWeight = dr["StudentWeight"].ToString();
                    addValue.RecordRemark = dr["hwRemark"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StudentBodyInformation addValue = new StudentBodyInformation();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
            return returnValue;
        }
    }
    public string[] delStudentData3(string wID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE StudentWelfareResource SET isDeleted=1 " +
                              "WHERE wID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(wID);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
            return returnValue;
        }
    }

    public string[] setStudentPhotoData(CreateStudent StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        string upPhoto = "";
        if (StudentData.studentPhoto != null) {
            upPhoto += " StudentAvatar=@StudentAvatar, ";
        }
        if (StudentData.studentManualImg != null)
        {
            upPhoto += " DisabilityProve=@DisabilityProve, ";
        }
        if (StudentData.familyEcological != null)
        {
            upPhoto += " FamilyProfile1=@FamilyProfile1, ";
        }
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);

                string sql = "UPDATE StudentDatabase SET " + upPhoto + " UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE StudentID=@StudentID AND ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentData.ID;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentID);
                cmd.Parameters.Add("@StudentAvatar", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentPhoto);
                cmd.Parameters.Add("@DisabilityProve", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentManualImg);
                cmd.Parameters.Add("@FamilyProfile1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyEcological);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                returnValue[1] = StudentData.ID.ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }//2015/5/10 aaron fix function name
    public string[] setStudentBaseData(CreateStudent StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        int CaseStatu = this.CaseStatusFunction(StudentData);
        string upPhoto = "";
        if (StudentData.studentPhoto != null)
        {
            upPhoto += " StudentAvatar=@StudentAvatar, ";
        }
        if (StudentData.studentManualImg != null)
        {
            upPhoto += " DisabilityProve=@DisabilityProve, ";
        }
        StudentData1 oldStudentDataBase1=this.getStudentData1(StudentData.ID);
        StudentHearingInformation oldStudentDataBase5 = this.getStudentHearingInfo(StudentData.ID);
        if (oldStudentDataBase1.endReasonDate == "1900-01-01" && oldStudentDataBase1.endReasonDate != Chk.CheckStringFunction(StudentData.endReasonDate) && Chk.CheckStringFunction(StudentData.endReasonDate).Length > 0)
        {
            /*已結案轉入離會生*/
            CreateStudentTracked StudentTracked = new CreateStudentTracked();
            StudentTracked.sUnit = StudentData.Unit;
            StudentTracked.studentID = StudentData.studentID;
            StudentTracked.email = StudentData.email;
            StudentTracked.addressZip = StudentData.addressZip;
            StudentTracked.addressCity = StudentData.addressCity;
            StudentTracked.address = StudentData.address;
            StudentTracked.Tel = StudentData.fPHPhone2;
            StudentTracked.manualCategory1 = StudentData.manualCategory1;
            StudentTracked.manualGrade1 = StudentData.manualGrade1;
            StudentTracked.manualCategory2 = StudentData.manualCategory2;
            StudentTracked.manualGrade2 = StudentData.manualGrade2;
            StudentTracked.manualCategory3 = StudentData.manualCategory3;
            StudentTracked.manualGrade3 = StudentData.manualGrade3;
            StudentTracked.assistmanageR = oldStudentDataBase5.assistmanageR;
            StudentTracked.brandR = oldStudentDataBase5.brandR;
            StudentTracked.modelR = oldStudentDataBase5.modelR;
            StudentTracked.buyingtimeR = oldStudentDataBase5.buyingtimeR;
            StudentTracked.buyingPlaceR = oldStudentDataBase5.buyingPlaceR;
            StudentTracked.insertHospitalR = oldStudentDataBase5.insertHospitalR;
            StudentTracked.openHzDateR = oldStudentDataBase5.openHzDateR;
            StudentTracked.assistmanageL = oldStudentDataBase5.assistmanageL;
            StudentTracked.brandL = oldStudentDataBase5.brandL;
            StudentTracked.modelL = oldStudentDataBase5.modelL;
            StudentTracked.buyingtimeL = oldStudentDataBase5.buyingtimeL;
            StudentTracked.buyingPlaceL = oldStudentDataBase5.buyingPlaceL;
            StudentTracked.insertHospitalL = oldStudentDataBase5.insertHospitalL;
            StudentTracked.openHzDateL = oldStudentDataBase5.openHzDateL;
            this.createStudentTrackedDataBase(StudentTracked);
            OtherDataBase oDB = new OtherDataBase();
            StaffDataBase sDB = new StaffDataBase();
            List<int> item = new List<int>();
            item.Add(18);
            item.Add(17);
            int[] days = {30,90,180};
            List<StaffDataList> SDL = sDB.getAllStaffDataList(item);
            foreach (StaffDataList atom in SDL)
            {
                if (atom.sUnit == StudentData.Unit)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        CreateRemind Remind = new CreateRemind();
                        Remind.executionDate = Chk.CheckStringtoDateFunction(StudentData.endReasonDate).AddDays(days[i]).ToShortDateString();
                        Remind.recipientID = atom.sID;
                        Remind.executionContent = StudentData.studentName + " 離會後 - 第 " + (i + 1).ToString() + " 次追蹤";
                        //cmd.Parameters.Add("@Executor", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RemindSystemData.recipientID);
                        //cmd.Parameters.Add("@RemindContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(RemindSystemData.executionContent);
                        //cmd.Parameters.Add("@RemindDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.executionDate);
                        //cmd.Parameters.Add("@CompleteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.fulfillmentDate);
                        oDB.CreateRemindSystem(Remind);
                    }
                }
            }

        }
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = "UPDATE StudentDatabase SET Unit=@Unit,EvaluationDate=@EvaluationDate, ConsultDate=@ConsultDate, Updated=(getdate()), CaseStatu=@CaseStatu, " +
                 "StudentName=@StudentName, StudentIdentity=@StudentIdentity, AddressZip1=@AddressZip1, " +
                    "AddressCity1=@AddressCity1, AddressOther1=@AddressOther1, AddressZip2=@AddressZip2, AddressCity2=@AddressCity2, AddressOther2=@AddressOther2, " +
                    "StudentBirthday=@StudentBirthday, StudentSex=@StudentSex, ClassDate=@ClassDate, GuaranteeDate= @GuaranteeDate, CompletedDate=@CompletedDate, " +
                    "CompletedType=@CompletedType, CompletedReason=@CompletedReason, ShortEndDateSince=@ShortEndDateSince, ShortEndDateUntil=@ShortEndDateUntil, " +
                    "NomembershipType=@NomembershipType, NomembershipReason=@NomembershipReason, CaregiversDaytime=@CaregiversDaytime, " +
                    "CaregiversDaytimeText=@CaregiversDaytimeText, CaregiversNight=@CaregiversNight, CaregiversNightText=@CaregiversNightText, ContactRelation1=@ContactRelation1, " +
                    "ContactName1=@ContactName1, ContactTel_company1=@ContactTel_company1, ContactTel_home1=@ContactTel_home1, ContactPhone1=@ContactPhone1, " +
                    "ContactFax1=@ContactFax1, ContactRelation2=@ContactRelation2, ContactName2=@ContactName2, ContactTel_company2=@ContactTel_company2, " +
                    "ContactTel_home2=@ContactTel_home2, ContactPhone2=@ContactPhone2, ContactFax2=@ContactFax2, ContactRelation3=@ContactRelation3, ContactName3=@ContactName3, " +
                    "ContactTel_company3=@ContactTel_company3, ContactTel_home3=@ContactTel_home3, ContactPhone3=@ContactPhone3, ContactFax3=@ContactFax3, " +
                    "ContactRelation4=@ContactRelation4, ContactName4=@ContactName4, ContactTel_company4=@ContactTel_company4, ContactTel_home4=@ContactTel_home4, " +
                    "ContactPhone4=@ContactPhone4, ContactFax4=@ContactFax4, StudentEmail=@StudentEmail, ReferralSourceType=@ReferralSourceType, ReferralSource=@ReferralSource, " +
                    "PhysicalAndMentalDisabilityHandbook=@PhysicalAndMentalDisabilityHandbook, DisabilityCategory1= @DisabilityCategory1, DisabilityGrade1=@DisabilityGrade1, " +
                    "DisabilityCategory2=@DisabilityCategory2, DisabilityGrade2=@DisabilityGrade2, DisabilityCategory3=@DisabilityCategory3, DisabilityGrade3=@DisabilityGrade3, " +
                    "NoDisabilityHandbook=@NoDisabilityHandbook, ApplyDisabilityHandbook=@ApplyDisabilityHandbook, Notify=@Notify, " +
                    "Notify_Unit=@Notify_Unit, Notify_Member=@Notify_Member, Notify_Tel=@Notify_Tel, Notify_Date=@Notify_Date, Notify_City=@Notify_City, EconomyState=@EconomyState, " +
                    "EconomyLow=@EconomyLow ," + upPhoto + " UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE StudentID=@StudentID AND ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentData.ID;
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.Unit);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentID);
                cmd.Parameters.Add("@EvaluationDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.assessDate);
                cmd.Parameters.Add("@ConsultDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.consultationDate);
                cmd.Parameters.Add("@Updated", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.upDate);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckIntFunction(CaseStatu);
                //cmd.Parameters.Add("@WriteName", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.fillInName);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentName);
                cmd.Parameters.Add("@StudentIdentity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentTWID);
                cmd.Parameters.Add("@AddressZip1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.censusAddressZip);
                cmd.Parameters.Add("@AddressCity1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.censusAddressCity);
                cmd.Parameters.Add("@AddressOther1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.censusAddress);
                cmd.Parameters.Add("@AddressZip2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.addressZip);
                cmd.Parameters.Add("@AddressCity2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.addressCity);
                cmd.Parameters.Add("@AddressOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.address);
                cmd.Parameters.Add("@StudentBirthday", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.studentbirthday);
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studentSex);
                cmd.Parameters.Add("@ClassDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.firstClassDate);
                cmd.Parameters.Add("@GuaranteeDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.guaranteeDate);
                cmd.Parameters.Add("@CompletedDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.endReasonDate);
                cmd.Parameters.Add("@CompletedType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.endReasonType);
                cmd.Parameters.Add("@CompletedReason", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.endReason);
                cmd.Parameters.Add("@ShortEndDateSince", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.sendDateSince);
                cmd.Parameters.Add("@ShortEndDateUntil", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.sendDateUntil);
                cmd.Parameters.Add("@NomembershipType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.nomembershipType);
                cmd.Parameters.Add("@NomembershipReason", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.nomembershipReason);
                cmd.Parameters.Add("@StudentAvatar", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentPhoto);
                cmd.Parameters.Add("@CaregiversDaytime", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.wCare);
                cmd.Parameters.Add("@CaregiversDaytimeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.wCareName);
                cmd.Parameters.Add("@CaregiversNight", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.bCare);
                cmd.Parameters.Add("@CaregiversNightText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.bCareName);
                cmd.Parameters.Add("@ContactRelation1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPRelation1);
                cmd.Parameters.Add("@ContactName1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPName1);
                cmd.Parameters.Add("@ContactTel_company1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPTel1);
                cmd.Parameters.Add("@ContactPhone1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPPhone1);
                cmd.Parameters.Add("@ContactTel_home1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPHPhone1);
                cmd.Parameters.Add("@ContactFax1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPFax1);
                cmd.Parameters.Add("@ContactRelation2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPRelation2);
                cmd.Parameters.Add("@ContactName2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPName2);
                cmd.Parameters.Add("@ContactTel_company2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPTel2);
                cmd.Parameters.Add("@ContactPhone2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPPhone2);
                cmd.Parameters.Add("@ContactTel_home2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPHPhone2);
                cmd.Parameters.Add("@ContactFax2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPFax2);
                cmd.Parameters.Add("@ContactRelation3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPRelation3);
                cmd.Parameters.Add("@ContactName3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPName3);
                cmd.Parameters.Add("@ContactTel_company3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPTel3);
                cmd.Parameters.Add("@ContactPhone3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPPhone3);
                cmd.Parameters.Add("@ContactTel_home3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPHPhone3);
                cmd.Parameters.Add("@ContactFax3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPFax3);
                cmd.Parameters.Add("@ContactRelation4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPRelation4);
                cmd.Parameters.Add("@ContactName4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPName4);
                cmd.Parameters.Add("@ContactTel_company4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPTel4);
                cmd.Parameters.Add("@ContactPhone4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPPhone4);
                cmd.Parameters.Add("@ContactTel_home4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPHPhone4);
                cmd.Parameters.Add("@ContactFax4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fPFax4);
                cmd.Parameters.Add("@StudentEmail", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.email);
                cmd.Parameters.Add("@ReferralSourceType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.sourceType);
                cmd.Parameters.Add("@ReferralSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.sourceName);
                cmd.Parameters.Add("@PhysicalAndMentalDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.manualWhether);
                cmd.Parameters.Add("@DisabilityCategory1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.manualCategory1);
                cmd.Parameters.Add("@DisabilityGrade1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.manualGrade1);
                cmd.Parameters.Add("@DisabilityCategory2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.manualCategory2);
                cmd.Parameters.Add("@DisabilityGrade2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.manualGrade2);
                cmd.Parameters.Add("@DisabilityCategory3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.manualCategory3);
                cmd.Parameters.Add("@DisabilityGrade3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.manualGrade3);
                cmd.Parameters.Add("@NoDisabilityHandbook", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.manualNo);
                cmd.Parameters.Add("@ApplyDisabilityHandbook", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.manualUnit);
                cmd.Parameters.Add("@DisabilityProve", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentManualImg);
                cmd.Parameters.Add("@Notify", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.notificationWhether);
                cmd.Parameters.Add("@Notify_Unit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.notificationUnit);
                cmd.Parameters.Add("@Notify_Member", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.notificationManage);
                cmd.Parameters.Add("@Notify_Tel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.notificationTel);
                cmd.Parameters.Add("@Notify_Date", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.notificationDate);
                cmd.Parameters.Add("@Notify_City", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.notificationCity);
                cmd.Parameters.Add("@EconomyState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.economy);
                cmd.Parameters.Add("@EconomyLow", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.economyLow);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setStudentDataBase2(StudentData2 StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                //Unit=@Unit, 
                string sql = "UPDATE StudentDatabase SET FamilyAppellation1=@FamilyAppellation1, FamilyName1=@FamilyName1, FamilyBirthday1=@FamilyBirthday1, " +
                    "FamilyEducation1=@FamilyEducation1, FamilyProfession1=@FamilyProfession1, FamilyLive1=@FamilyLive1, FamilyHearing1=@FamilyHearing1, FamilyHealth1=@FamilyHealth1, " +
                    "FamilyHealthText1=@FamilyHealthText1, FamilyAppellation2=@FamilyAppellation2, FamilyName2=@FamilyName2, FamilyBirthday2=@FamilyBirthday2, " +
                    "FamilyEducation2=@FamilyEducation2, FamilyProfession2=@FamilyProfession2, FamilyLive2=@FamilyLive2, FamilyHearing2=@FamilyHearing2, FamilyHealth2=@FamilyHealth2, " +
                    "FamilyHealthText2=@FamilyHealthText2, FamilyAppellation3=@FamilyAppellation3, FamilyName3=@FamilyName3, FamilyBirthday3=@FamilyBirthday3, " +
                    "FamilyEducation3=@FamilyEducation3, FamilyProfession3=@FamilyProfession3, FamilyLive3=@FamilyLive3, FamilyHearing3=@FamilyHearing3, FamilyHealth3=@FamilyHealth3, " +
                    "FamilyHealthText3=@FamilyHealthText3, FamilyAppellation4=@FamilyAppellation4, FamilyName4=@FamilyName4, FamilyBirthday4=@FamilyBirthday4, " +
                    "FamilyEducation4=@FamilyEducation4, FamilyProfession4=@FamilyProfession4, FamilyLive4=@FamilyLive4, FamilyHearing4=@FamilyHearing4, FamilyHealth4=@FamilyHealth4, " +
                    "FamilyHealthText4=@FamilyHealthText4, FamilyAppellation5=@FamilyAppellation5, FamilyName5=@FamilyName5, FamilyBirthday5=@FamilyBirthday5, " +

                    "FamilyEducation5=@FamilyEducation5, FamilyProfession5=@FamilyProfession5, FamilyLive5=@FamilyLive5, FamilyHearing5=@FamilyHearing5, FamilyHealth5=@FamilyHealth5, " +
                    "FamilyHealthText5=@FamilyHealthText5, FamilyAppellation6=@FamilyAppellation6, FamilyName6=@FamilyName6, FamilyBirthday6=@FamilyBirthday6, " +
                    "FamilyEducation6=@FamilyEducation6, FamilyProfession6=@FamilyProfession6, FamilyLive6=@FamilyLive6, FamilyHearing6=@FamilyHearing6, FamilyHealth6=@FamilyHealth6, " +
                    "FamilyHealthText6=@FamilyHealthText6, FamilyAppellation7=@FamilyAppellation7, FamilyName7=@FamilyName7, FamilyBirthday7=@FamilyBirthday7, " +

                    "FamilyEducation7=@FamilyEducation7, FamilyProfession7=@FamilyProfession7, FamilyLive7=@FamilyLive7, FamilyHearing7=@FamilyHearing7, FamilyHealth7=@FamilyHealth7, " +
                    "FamilyHealthText7=@FamilyHealthText7, FamilyAppellation8=@FamilyAppellation8, FamilyName8=@FamilyName8, FamilyBirthday8=@FamilyBirthday8, " +
                    "FamilyEducation8=@FamilyEducation8, FamilyProfession8=@FamilyProfession8, FamilyLive8=@FamilyLive8, FamilyHearing8=@FamilyHearing8, FamilyHealth8=@FamilyHealth8, " +
                    "FamilyHealthText8=@FamilyHealthText8, FamilyLanguage=@FamilyLanguage, FamilyLanguageText=@FamilyLanguageText, FamilyConnectLanguage=@FamilyConnectLanguage, " +
                    "FamilyConnectLanguageText=@FamilyConnectLanguageText " +
                    "WHERE StudentID=@StudentID AND ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentID);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentData.ID;
                cmd.Parameters.Add("@FamilyAppellation1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation1);
                cmd.Parameters.Add("@FamilyName1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName1);
                cmd.Parameters.Add("@FamilyBirthday1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday1);
                cmd.Parameters.Add("@FamilyEducation1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU1);
                cmd.Parameters.Add("@FamilyProfession1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession1);
                cmd.Parameters.Add("@FamilyLive1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive1);
                cmd.Parameters.Add("@FamilyHearing1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing1);
                cmd.Parameters.Add("@FamilyHealth1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy1);
                cmd.Parameters.Add("@FamilyHealthText1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText01);
                cmd.Parameters.Add("@FamilyAppellation2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation2);
                cmd.Parameters.Add("@FamilyName2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName2);
                cmd.Parameters.Add("@FamilyBirthday2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday2);
                cmd.Parameters.Add("@FamilyEducation2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU2);
                cmd.Parameters.Add("@FamilyProfession2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession2);
                cmd.Parameters.Add("@FamilyLive2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive2);
                cmd.Parameters.Add("@FamilyHearing2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing2);
                cmd.Parameters.Add("@FamilyHealth2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy2);
                cmd.Parameters.Add("@FamilyHealthText2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText02);
                cmd.Parameters.Add("@FamilyAppellation3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation3);
                cmd.Parameters.Add("@FamilyName3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName3);
                cmd.Parameters.Add("@FamilyBirthday3", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday3);
                cmd.Parameters.Add("@FamilyEducation3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU3);
                cmd.Parameters.Add("@FamilyProfession3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession3);
                cmd.Parameters.Add("@FamilyLive3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive3);
                cmd.Parameters.Add("@FamilyHearing3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing3);
                cmd.Parameters.Add("@FamilyHealth3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy3);
                cmd.Parameters.Add("@FamilyHealthText3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText03);
                cmd.Parameters.Add("@FamilyAppellation4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation4);
                cmd.Parameters.Add("@FamilyName4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName4);
                cmd.Parameters.Add("@FamilyBirthday4", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday4);
                cmd.Parameters.Add("@FamilyEducation4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU4);
                cmd.Parameters.Add("@FamilyProfession4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession4);
                cmd.Parameters.Add("@FamilyLive4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive4);
                cmd.Parameters.Add("@FamilyHearing4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing4);
                cmd.Parameters.Add("@FamilyHealth4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy4);
                cmd.Parameters.Add("@FamilyHealthText4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText04);
                cmd.Parameters.Add("@FamilyAppellation5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation5);
                cmd.Parameters.Add("@FamilyName5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName5);
                cmd.Parameters.Add("@FamilyBirthday5", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday5);
                cmd.Parameters.Add("@FamilyEducation5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU5);
                cmd.Parameters.Add("@FamilyProfession5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession5);
                cmd.Parameters.Add("@FamilyLive5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive5);
                cmd.Parameters.Add("@FamilyHearing5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing5);
                cmd.Parameters.Add("@FamilyHealth5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy5);
                cmd.Parameters.Add("@FamilyHealthText5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText05);
                cmd.Parameters.Add("@FamilyAppellation6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation6);
                cmd.Parameters.Add("@FamilyName6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName6);
                cmd.Parameters.Add("@FamilyBirthday6", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday6);
                cmd.Parameters.Add("@FamilyEducation6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU6);
                cmd.Parameters.Add("@FamilyProfession6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession6);
                cmd.Parameters.Add("@FamilyLive6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive6);
                cmd.Parameters.Add("@FamilyHearing6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing6);
                cmd.Parameters.Add("@FamilyHealth6", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy6);
                cmd.Parameters.Add("@FamilyHealthText6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText06);
                cmd.Parameters.Add("@FamilyAppellation7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation7);
                cmd.Parameters.Add("@FamilyName7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName7);
                cmd.Parameters.Add("@FamilyBirthday7", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday7);
                cmd.Parameters.Add("@FamilyEducation7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU7);
                cmd.Parameters.Add("@FamilyProfession7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession7);
                cmd.Parameters.Add("@FamilyLive7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive7);
                cmd.Parameters.Add("@FamilyHearing7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing7);
                cmd.Parameters.Add("@FamilyHealth7", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy7);
                cmd.Parameters.Add("@FamilyHealthText7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText07);
                cmd.Parameters.Add("@FamilyAppellation8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fAppellation8);
                cmd.Parameters.Add("@FamilyName8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fName8);
                cmd.Parameters.Add("@FamilyBirthday8", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.fBirthday8);
                cmd.Parameters.Add("@FamilyEducation8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fEDU8);
                cmd.Parameters.Add("@FamilyProfession8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fProfession8);
                cmd.Parameters.Add("@FamilyLive8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fLive8);
                cmd.Parameters.Add("@FamilyHearing8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHearing8);
                cmd.Parameters.Add("@FamilyHealth8", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fHealthy8);
                cmd.Parameters.Add("@FamilyHealthText8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyText08);
                cmd.Parameters.Add("@FamilyLanguage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.lang1);
                cmd.Parameters.Add("@FamilyLanguageText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.lang1t01);
                cmd.Parameters.Add("@FamilyConnectLanguage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.lang2);
                cmd.Parameters.Add("@FamilyConnectLanguageText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.lang2t01);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setStudentDataBase4(StudentData4 StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        string upPhoto = "";
        if (StudentData.familyEcological != null)
        {
            upPhoto += " FamilyProfile1=@FamilyProfile1, ";
        }
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                //Unit=@Unit, 
                string sql = "UPDATE StudentDatabase SET FamilyProfile2=@FamilyProfile2, FamilyProfile3=@FamilyProfile3, " +
                    "FamilyProfile4=@FamilyProfile4, FamilyProfile5=@FamilyProfile5, FamilyProfileWriteName=@FamilyProfileWriteName, " +upPhoto+
                    "FamilyProfileWriteDate=@FamilyProfileWriteDate " +
                    "WHERE StudentID=@StudentID AND ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentID);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentData.ID;
                cmd.Parameters.Add("@FamilyProfile1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyEcological);
                cmd.Parameters.Add("@FamilyProfile2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.famailySituation);
                cmd.Parameters.Add("@FamilyProfile3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.famailyMedical);
                cmd.Parameters.Add("@FamilyProfile4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.famailyActionSituation);
                cmd.Parameters.Add("@FamilyProfile5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fswAssess);
                cmd.Parameters.Add("@FamilyProfileWriteName", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.socialID);
                cmd.Parameters.Add("@FamilyProfileWriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.socialDate);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setStudentDataBase5(StudentHearingInformation StudentHearing)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();

                string sql = "UPDATE StudentDatabase SET StudentDevelop=@StudentDevelop, StudentDevelopText21=@StudentDevelopText21, StudentDevelopText22=@StudentDevelopText22, " +
                  "StudentDevelopText31=@StudentDevelopText31, StudentDevelopText61=@StudentDevelopText61, StudentDevelopText62=@StudentDevelopText62, " +
                  "StudentDevelopText81=@StudentDevelopText81, StudentDevelopText10=@StudentDevelopText10, StudentDevelopText11=@StudentDevelopText11, " +
                  "StudentDevelopText12=@StudentDevelopText12 " +
                  "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentHearing.ID;
                cmd.Parameters.Add("@StudentDevelop", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history);
                cmd.Parameters.Add("@StudentDevelopText21", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history02t01);
                cmd.Parameters.Add("@StudentDevelopText22", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history02t02);
                cmd.Parameters.Add("@StudentDevelopText31", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history03t01);
                cmd.Parameters.Add("@StudentDevelopText61", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history06t01);
                cmd.Parameters.Add("@StudentDevelopText62", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history06t02);
                cmd.Parameters.Add("@StudentDevelopText81", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history08t01);
                cmd.Parameters.Add("@StudentDevelopText10", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history10t01);
                cmd.Parameters.Add("@StudentDevelopText11", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history11t01);
                cmd.Parameters.Add("@StudentDevelopText12", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.history12t01);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public string[] setStudentDataBase6(StudentHearingInformation StudentHearing)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        StudentHearingInformation oldData = this.getStudentHearingInfo(StudentHearing.ID.ToString());
        if ((oldData.accessory == "0" || oldData.accessory != StudentHearing.accessory) && StudentHearing.assistmanage == "3" && StudentHearing.accessory != "0")
        {
            StudentDataBasic DateItem = new StudentDataBasic();
            DateItem.studentID = StudentHearing.studentID;
            DateItem.assistmanageR = StudentHearing.assistmanageR;
            DateItem.brandR = StudentHearing.brandR;
            DateItem.modelR = StudentHearing.modelR;
            DateItem.buyingPlaceR = StudentHearing.buyingPlaceR;
            DateItem.buyingtimeR = StudentHearing.buyingtimeR;
            DateItem.insertHospitalR = StudentHearing.insertHospitalR;
            DateItem.openHzDateR = StudentHearing.openHzDateR;
            DateItem.assistmanageL = StudentHearing.assistmanageL;
            DateItem.brandL = StudentHearing.brandL;
            DateItem.modelL = StudentHearing.modelL;
            DateItem.buyingtimeL = StudentHearing.buyingtimeL;
            DateItem.buyingPlaceL = StudentHearing.buyingPlaceL;
            DateItem.insertHospitalL = StudentHearing.insertHospitalL;
            DateItem.openHzDateL = StudentHearing.openHzDateL;
            Audiometry aDB = new Audiometry();
            aDB.ComparisonAidsData(DateItem);
        }
        
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();

                string sql = "UPDATE StudentDatabase SET HearingProblemAge=@HearingProblemAge, HearingProblemMonth=@HearingProblemMonth, " +
                  "HearingProblem=@HearingProblem, HearingProblemText=@HearingProblemText, DiagnoseAge=@DiagnoseAge, DiagnoseMonth=@DiagnoseMonth, " +
                  "DiagnoseAgency=@DiagnoseAgency, DiagnoseR=@DiagnoseR, DiagnoseL=@DiagnoseL, NewbornHearing=@NewbornHearing, " +
                  "NewbornHearingInspection=@NewbornHearingInspection, NewbornHearingInspectionAgency=@NewbornHearingInspectionAgency, " +
                  "NewbornHearingInspectionDiagnoseR=@NewbornHearingInspectionDiagnoseR, NewbornHearingInspectionDiagnoseL=@NewbornHearingInspectionDiagnoseL, " +
                  "AuditoryElectrophysiology1=@AuditoryElectrophysiology1, AuditoryElectrophysiology_Date1=@AuditoryElectrophysiology_Date1, " +
                  "AuditoryElectrophysiology_Agency1=@AuditoryElectrophysiology_Agency1, AuditoryElectrophysiology_Item1=@AuditoryElectrophysiology_Item1, " +
                  "AuditoryElectrophysiology_Diagnose1R=@AuditoryElectrophysiology_Diagnose1R, AuditoryElectrophysiology_Diagnose1L=@AuditoryElectrophysiology_Diagnose1L, " +
                  "AuditoryElectrophysiology_Date2=@AuditoryElectrophysiology_Date2, AuditoryElectrophysiology_Agency2=@AuditoryElectrophysiology_Agency2, " +
                  "AuditoryElectrophysiology_Item2=@AuditoryElectrophysiology_Item2, AuditoryElectrophysiology_Diagnose2R=@AuditoryElectrophysiology_Diagnose2R, " +
                  "AuditoryElectrophysiology_Diagnose2L=@AuditoryElectrophysiology_Diagnose2L, AuditoryElectrophysiology_Date3=@AuditoryElectrophysiology_Date3, " +
                  "AuditoryElectrophysiology_Agency3=@AuditoryElectrophysiology_Agency3, AuditoryElectrophysiology_Item3=@AuditoryElectrophysiology_Item3, " +
                  "AuditoryElectrophysiology_Diagnose3R=@AuditoryElectrophysiology_Diagnose3R, AuditoryElectrophysiology_Diagnose3L=@AuditoryElectrophysiology_Diagnose3L, " +
                  "AuditoryElectrophysiology_Date4=@AuditoryElectrophysiology_Date4, AuditoryElectrophysiology_Agency4=@AuditoryElectrophysiology_Agency4, " +
                  "AuditoryElectrophysiology_Item4=@AuditoryElectrophysiology_Item4, AuditoryElectrophysiology_Diagnose4R=@AuditoryElectrophysiology_Diagnose4R, " +
                  "AuditoryElectrophysiology_Diagnose4L=@AuditoryElectrophysiology_Diagnose4L, AuditoryElectrophysiology_Date5=@AuditoryElectrophysiology_Date5, " +
                  "AuditoryElectrophysiology_Agency5=@AuditoryElectrophysiology_Agency5, AuditoryElectrophysiology_Item5=@AuditoryElectrophysiology_Item5, " +
                  "AuditoryElectrophysiology_Diagnose5R=@AuditoryElectrophysiology_Diagnose5R, AuditoryElectrophysiology_Diagnose5L=@AuditoryElectrophysiology_Diagnose5L, " +
                  "CTorMRI=@CTorMRI, CTorMRI_Date1=@CTorMRI_Date1, " +
                  "CTorMRI_Agency1=@CTorMRI_Agency1, CTorMRI_DiagnoseR=@CTorMRI_DiagnoseR, CTorMRI_DiagnoseL=@CTorMRI_DiagnoseL, " +
                  "GeneScreening=@GeneScreening, GeneScreening_Date=@GeneScreening_Date, GeneScreening_Agency=@GeneScreening_Agency, " +
                  "GeneScreening_Item=@GeneScreening_Item, " +
                  "FamilyHearingProblemHistory=@FamilyHearingProblemHistory, FamilyHearingProblemHistoryText=@FamilyHearingProblemHistoryText, " +
                  "HearingChangeHistory=@HearingChangeHistory, HearingChangeHistoryText=@HearingChangeHistoryText, AidsManagement=@AidsManagement, " +
                  "AidsManagementTextAge=@AidsManagementTextAge, HearingAids_R=@HearingAids_R, AidsBrand_R=@AidsBrand_R, AidsModel_R=@AidsModel_R, AidsOptionalTime_R=@AidsOptionalTime_R, " +
                  "AidsOptionalLocation_R=@AidsOptionalLocation_R, EEarHospital_R=@EEarHospital_R,EEarOpen_R=@EEarOpen_R, " +
                  "HearingAids_L=@HearingAids_L, AidsBrand_L=@AidsBrand_L, AidsModel_L=@AidsModel_L, AidsOptionalTime_L=@AidsOptionalTime_L, AidsOptionalLocation_L=@AidsOptionalLocation_L, " +
                  "EEarHospital_L=@EEarHospital_L, EEarOpen_L=@EEarOpen_L, AllDayAids=@AllDayAids, AllDayAidsText=@AllDayAidsText, " +
                  "ActiveReaction=@ActiveReaction, ActiveReactionText=@ActiveReactionText, BasicCare=@BasicCare, BasicCareText=@BasicCareText, UseFMsystem=@UseFMsystem, " +
                  "UseFMsystemBrand=@UseFMsystemBrand, HearingAssessmentSummary=@HearingAssessmentSummary, EarEndoscopy=@EarEndoscopy, " +
                  "EarEndoscopyAbnormalText=@EarEndoscopyAbnormalText, PureToneText=@PureToneText, Tympanogram=@Tympanogram, SATorSDT=@SATorSDT, " +
                  "SpeechRecognition=@SpeechRecognition, AidsSystem=@AidsSystem, HearingInspection=@HearingInspection, HearingOther=@HearingOther, " +
                  "Audiologist=@Audiologist, SurveyingDate=@SurveyingDate " +
                  "WHERE ID=@ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentHearing.ID;
                cmd.Parameters.Add("@HearingProblemAge", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.problems01t01);
                cmd.Parameters.Add("@HearingProblemMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.problems01t02);
                cmd.Parameters.Add("@HearingProblem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.hearingQ);
                cmd.Parameters.Add("@HearingProblemText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.hearingQText);
                cmd.Parameters.Add("@DiagnoseAge", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.problems02t01);
                cmd.Parameters.Add("@DiagnoseMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.problems02t02);
                cmd.Parameters.Add("@DiagnoseAgency", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.problems02t03);
                cmd.Parameters.Add("@DiagnoseR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.problems02t04);
                cmd.Parameters.Add("@DiagnoseL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.problems02t05);
                cmd.Parameters.Add("@NewbornHearing", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.hearingcheck);
                cmd.Parameters.Add("@NewbornHearingInspection", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.hearingYescheck);
                cmd.Parameters.Add("@NewbornHearingInspectionAgency", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.hearingYesPlace);
                cmd.Parameters.Add("@NewbornHearingInspectionDiagnoseR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.hearingYesResultR);
                cmd.Parameters.Add("@NewbornHearingInspectionDiagnoseL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.hearingYesResultL);
                cmd.Parameters.Add("@AuditoryElectrophysiology1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.sleepcheck);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Date1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.sleepcheckTime1);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Agency1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckPlace1);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Item1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.sleepcheckCheckItem1);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose1R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultL1);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose1L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultR1);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Date2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.sleepcheckTime2);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Agency2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckPlace2);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Item2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.sleepcheckCheckItem2);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose2R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultL2);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose2L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultR2);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Date3", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.sleepcheckTime3);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Agency3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckPlace3);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Item3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.sleepcheckCheckItem3);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose3R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultL3);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose3L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultR3);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Date4", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.sleepcheckTime4);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Agency4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckPlace4);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Item4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.sleepcheckCheckItem4);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose4R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultL4);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose4L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultR4);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Date5", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.sleepcheckTime5);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Agency5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckPlace5);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Item5", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.sleepcheckCheckItem5);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose5R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultL5);
                cmd.Parameters.Add("@AuditoryElectrophysiology_Diagnose5L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.sleepcheckResultR5);
                cmd.Parameters.Add("@CTorMRI", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.ctmri);
                cmd.Parameters.Add("@CTorMRI_Date1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.ctmriTime);
                cmd.Parameters.Add("@CTorMRI_Agency1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.ctmriPlace);
                cmd.Parameters.Add("@CTorMRI_DiagnoseR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.ctmriResultL);
                cmd.Parameters.Add("@CTorMRI_DiagnoseL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.ctmriResultR);
                cmd.Parameters.Add("@GeneScreening", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.gene);
                cmd.Parameters.Add("@GeneScreening_Date", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.geneTime);
                cmd.Parameters.Add("@GeneScreening_Agency", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.genePlace);
                cmd.Parameters.Add("@GeneScreening_Item", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.geneResult);
                cmd.Parameters.Add("@FamilyHearingProblemHistory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.familyhistory);
                cmd.Parameters.Add("@FamilyHearingProblemHistoryText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.familyhistoryText);
                cmd.Parameters.Add("@HearingChangeHistory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.changehistory);
                cmd.Parameters.Add("@HearingChangeHistoryText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.changehistoryText);
                cmd.Parameters.Add("@AidsManagement", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assistmanage);
                cmd.Parameters.Add("@AidsManagementTextAge", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.accessory);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.openHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.openHzDateL);
                cmd.Parameters.Add("@AllDayAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.allassis);
                cmd.Parameters.Add("@AllDayAidsText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.allassisNoText);
                cmd.Parameters.Add("@ActiveReaction", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assis1);
                cmd.Parameters.Add("@ActiveReactionText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.assis1NoText);
                cmd.Parameters.Add("@BasicCare", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assis2);
                cmd.Parameters.Add("@BasicCareText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.assis2NoText);
                cmd.Parameters.Add("@UseFMsystem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assisFM);
                cmd.Parameters.Add("@UseFMsystemBrand", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assisFMBrand);
                cmd.Parameters.Add("@HearingAssessmentSummary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.assessnotes);
                cmd.Parameters.Add("@EarEndoscopy", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.assessnotes1);
                cmd.Parameters.Add("@EarEndoscopyAbnormalText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.assessnotes102Text);
                cmd.Parameters.Add("@PureToneText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.problems11t02);
                cmd.Parameters.Add("@Tympanogram", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.assessnotes2);
                cmd.Parameters.Add("@SATorSDT", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.problems11t03);
                cmd.Parameters.Add("@SpeechRecognition", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.problems11t04);
                cmd.Parameters.Add("@AidsSystem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.problems11t05);
                cmd.Parameters.Add("@HearingInspection", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.problems11t06);
                cmd.Parameters.Add("@HearingOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentHearing.problems11t07);
                cmd.Parameters.Add("@Audiologist", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentHearing.inspectorID);
                cmd.Parameters.Add("@SurveyingDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentHearing.inspectorDate);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setStudentDataBase7(StudentTeachingInformation StudentTeaching)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE StudentDatabase SET CaseFamilyCooperative=@CaseFamilyCooperative, EntrustOthers=@EntrustOthers, " +
                    "CooperativeDifficulty=@CooperativeDifficulty, RearingAttitude=@RearingAttitude, RearingAttitude1=@RearingAttitude1, " +
                    "RearingAttitude1Text=@RearingAttitude1Text, RearingAttitude2=@RearingAttitude2, RearingAttitude2Text=@RearingAttitude2Text, " +
                    "RearingAttitude3=@RearingAttitude3, RearingAttitude3Text=@RearingAttitude3Text, RearingAttitude4=@RearingAttitude4, " +
                    "RearingAttitude4Text=@RearingAttitude4Text, RearingAttitude5=@RearingAttitude5, RearingAttitude5Text=@RearingAttitude5Text, " +
                    "RearingAttitude6=@RearingAttitude6, RearingAttitude6Text=@RearingAttitude6Text, Dispositional=@Dispositional, Dispositional1=@Dispositional1, " +
                    "Dispositional1Text=@Dispositional1Text, Dispositional2=@Dispositional2, Dispositional2Text=@Dispositional2Text, Dispositional3=@Dispositional3, " +
                    "Dispositional3Text=@Dispositional3Text, Dispositional4=@Dispositional4, Dispositional4Text=@Dispositional4Text, Attendance=@Attendance, " +
                    "AttendanceText=@AttendanceText, TeachEscorts=@TeachEscorts, TeachEscortsText=@TeachEscortsText, AfterTeach=@AfterTeach, AfterTeachText=@AfterTeachText, " +
                    "MajorProblem=@MajorProblem, MajorProblemText=@MajorProblemText, RemarkOther=@RemarkOther, LearningAttitude=@LearningAttitude, Temperament=@Temperament, " +
                    "Activity=@Activity, Focus=@Focus, Persistence=@Persistence, CommunicationBehavior=@CommunicationBehavior, AuditorySkill=@AuditorySkill, " +
                    "Acknowledge=@Acknowledge, Language=@Language, LanguageText=@LanguageText, Action=@Action, WearAids=@WearAids, SpiritAndCoordinate=@SpiritAndCoordinate, " +
                    "SpiritAndCoordinateOther=@SpiritAndCoordinateOther, Communication=@Communication, LearningDesire=@LearningDesire, AttachmentData=@AttachmentData, " +
                    "AttachmentDataText=@AttachmentDataText, RemarkOther2=@RemarkOther2, ExistingResources=@ExistingResources, InterventionAgencies=@InterventionAgencies, " +
                    "HospitalIntervention=@HospitalIntervention, RelatedIntervention=@RelatedIntervention, RelatedInterventionText=@RelatedInterventionText, " +
                    "InNurserySchool=@InNurserySchool, HearingRehabilitation=@HearingRehabilitation, CaseNeed=@CaseNeed, CaseFamilyEnthusiasm=@CaseFamilyEnthusiasm, " +
                    "Rehousing=@Rehousing, RehousingText=@RehousingText, CaseFamilyCourseProposal=@CaseFamilyCourseProposal, " +
                    "CaseFamilyCourseProposalText=@CaseFamilyCourseProposalText, CaseCourseProposal=@CaseCourseProposal, CaseCourseProposalText=@CaseCourseProposalText, " +
                    "Teacher=@Teacher, TeacherDate=@TeacherDate " +
                    "WHERE ID=@ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentTeaching.ID;
                cmd.Parameters.Add("@CaseFamilyCooperative", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case1);
                cmd.Parameters.Add("@EntrustOthers", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case1t04);
                cmd.Parameters.Add("@CooperativeDifficulty", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case1t05);
                cmd.Parameters.Add("@RearingAttitude", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case2);
                cmd.Parameters.Add("@RearingAttitude1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case21);
                cmd.Parameters.Add("@RearingAttitude1Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case21t01);
                cmd.Parameters.Add("@RearingAttitude2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case22);
                cmd.Parameters.Add("@RearingAttitude2Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case22t01);
                cmd.Parameters.Add("@RearingAttitude3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case23);
                cmd.Parameters.Add("@RearingAttitude3Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case23t01);
                cmd.Parameters.Add("@RearingAttitude4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case24);
                cmd.Parameters.Add("@RearingAttitude4Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case24t01);
                cmd.Parameters.Add("@RearingAttitude5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case25);
                cmd.Parameters.Add("@RearingAttitude5Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case25t01);
                cmd.Parameters.Add("@RearingAttitude6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case26);
                cmd.Parameters.Add("@RearingAttitude6Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case26t01);
                cmd.Parameters.Add("@Dispositional", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case3);
                cmd.Parameters.Add("@Dispositional1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case31);
                cmd.Parameters.Add("@Dispositional1Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case31t01);
                cmd.Parameters.Add("@Dispositional2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case32);
                cmd.Parameters.Add("@Dispositional2Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case32t01);
                cmd.Parameters.Add("@Dispositional3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case33);
                cmd.Parameters.Add("@Dispositional3Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case33t01);
                cmd.Parameters.Add("@Dispositional4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case34);
                cmd.Parameters.Add("@Dispositional4Text", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case34t01);
                cmd.Parameters.Add("@Attendance", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.attend);
                cmd.Parameters.Add("@AttendanceText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.attend01t01);
                cmd.Parameters.Add("@TeachEscorts", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.accompany);
                cmd.Parameters.Add("@TeachEscortsText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.accompany01t01);
                cmd.Parameters.Add("@AfterTeach", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.teach);
                cmd.Parameters.Add("@AfterTeachText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.teach01t01);
                cmd.Parameters.Add("@MajorProblem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.caseQ);
                cmd.Parameters.Add("@MajorProblemText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.caseQ01t01);
                cmd.Parameters.Add("@RemarkOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.OtherRemark1);
                cmd.Parameters.Add("@LearningAttitude", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case4);
                cmd.Parameters.Add("@Temperament", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case5);
                cmd.Parameters.Add("@Activity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case6);
                cmd.Parameters.Add("@Focus", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case7);
                cmd.Parameters.Add("@Persistence", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case8);
                cmd.Parameters.Add("@CommunicationBehavior", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case9);
                cmd.Parameters.Add("@AuditorySkill", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case10);
                cmd.Parameters.Add("@Acknowledge", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case11);
                cmd.Parameters.Add("@Language", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case12);
                cmd.Parameters.Add("@LanguageText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case12t01);
                cmd.Parameters.Add("@Action", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case13);
                cmd.Parameters.Add("@WearAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.wear);
                cmd.Parameters.Add("@SpiritAndCoordinate", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.mind);
                cmd.Parameters.Add("@SpiritAndCoordinateOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.mind01t01);
                cmd.Parameters.Add("@Communication", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.connectwish);
                cmd.Parameters.Add("@LearningDesire", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.studywish);
                cmd.Parameters.Add("@AttachmentData", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.related);
                cmd.Parameters.Add("@AttachmentDataText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.related01t01);
                cmd.Parameters.Add("@RemarkOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.OtherRemark2);
                cmd.Parameters.Add("@ExistingResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case14);
                cmd.Parameters.Add("@InterventionAgencies", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.trusteeship);
                cmd.Parameters.Add("@HospitalIntervention", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case14t01);
                cmd.Parameters.Add("@RelatedIntervention", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.proceed);
                cmd.Parameters.Add("@RelatedInterventionText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.proceedt01);
                cmd.Parameters.Add("@InNurserySchool", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.preschools);
                cmd.Parameters.Add("@HearingRehabilitation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case15);
                cmd.Parameters.Add("@CaseNeed", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.Rehabilitation1);
                cmd.Parameters.Add("@CaseFamilyEnthusiasm", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.Rehabilitation2);
                cmd.Parameters.Add("@Rehousing", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTeaching.Rehabilitation3);
                cmd.Parameters.Add("@RehousingText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.Rehabilitation3Text);
                cmd.Parameters.Add("@CaseFamilyCourseProposal", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case16);
                cmd.Parameters.Add("@CaseFamilyCourseProposalText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.OtherRemark3);
                cmd.Parameters.Add("@CaseCourseProposal", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.case17);
                cmd.Parameters.Add("@CaseCourseProposalText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTeaching.OtherRemark4);
                cmd.Parameters.Add("@Teacher", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTeaching.teacherID);
                cmd.Parameters.Add("@TeacherDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTeaching.teacherDate);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public string[] setStudentDataBase8(StudentBodyInformation StudentBody)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE StudentHeightWeightRecord SET hwDate=@hwDate, StudentHeight=@StudentHeight, StudentWeight=@StudentWeight, hwRemark=@hwRemark " +
                              "WHERE hwID=@hwID AND StudentID=@StudentID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@hwID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentBody.RecordID);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentBody.studentID);
                cmd.Parameters.Add("@hwDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentBody.RecordDate);
                cmd.Parameters.Add("@StudentHeight", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StudentBody.RecordHeight);
                cmd.Parameters.Add("@StudentWeight", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StudentBody.RecordWeight);
                cmd.Parameters.Add("@hwRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentBody.RecordRemark);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public string[] createStudentDataBase8(StudentBodyInformation StudentBody) //身高體重
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO StudentHeightWeightRecord (StudentID, hwDate, StudentHeight, StudentWeight, hwRemark) " +
                               "VALUES (@StudentID, @hwDate, @StudentHeight, @StudentWeight, @hwRemark)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentBody.studentID);
                cmd.Parameters.Add("@hwDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentBody.RecordDate);
                cmd.Parameters.Add("@StudentHeight", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StudentBody.RecordHeight);
                cmd.Parameters.Add("@StudentWeight", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StudentBody.RecordWeight);
                cmd.Parameters.Add("@hwRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentBody.RecordRemark);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public string[] createStudentDataBase3(StudentWelfareResource StudentWelfare) //學生基本資料(分頁)->福利資源使用狀況-新增
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO StudentWelfareResource (StudentID, ORGDate, ORGID, CreateFileBy, UpFileBy, UpFileDate) " +
                               "VALUES (@StudentID, @ORGDate, @ORGID, @CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentWelfare.studentID);
                cmd.Parameters.Add("@ORGDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentWelfare.resourceDate);
                cmd.Parameters.Add("@ORGID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentWelfare.resourceID);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue; 
    }

    public string[] delStudentDataBase8(string bID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE StudentHeightWeightRecord SET isDeleted=1 WHERE hwID=@hwID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@hwID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(bID);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public CreateVolunteer getVolunteerData(string vID)
    {
        CreateVolunteer returnValue = new CreateVolunteer();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM  VolunteerData WHERE isDeleted=0 AND ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(vID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.sUnit = dr["Unit"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.volunteerId =dr["VOLID"].ToString();
                    returnValue.volunteerName = dr["VOLName"].ToString();
                    returnValue.vEmail = dr["Email"].ToString();
                    returnValue.volunteerPhone = dr["Phone"].ToString();
                    returnValue.vSex = dr["sex"].ToString();
                    returnValue.servicecontent = dr["Servicetime"].ToString();
                    returnValue.nowJob = dr["CurrentWorkOrSchoolName"].ToString();
                    returnValue.telDaytime = dr["Tel_Daytime"].ToString();
                    returnValue.telNight = dr["Tel_Night"].ToString();
                    returnValue.volunteerFax = dr["Fax"].ToString();
                    returnValue.addressZip = dr["AddressZip"].ToString();
                    returnValue.addressCity = dr["AddressCity"].ToString();
                    returnValue.address = dr["AddressOther"].ToString();
                    returnValue.Expertise = dr["PersonalExpertise"].ToString();
                    returnValue.Experience = dr["ServiceExperience"].ToString();
                    returnValue.VOLExpect = dr["VOLExpect"].ToString();
                    returnValue.servicedate = dr["ServiceTime"].ToString();
                    returnValue.servicecontent = dr["ServiceItem"].ToString();
                    returnValue.otherService = dr["ServiceOther"].ToString();
                    returnValue.vSource = dr["IntroductionSource"].ToString();
                    returnValue.vBirthday = DateTime.Parse(dr["Birthday"].ToString()).ToString("yyyy-MM-dd");

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message;
            }

        }
        return returnValue;
    }

    private string SearchVolunteerConditionReturn(SearchVolunteer SearchStructure)
    {
        string ConditionReturn = "";
        if (SearchStructure.txtvID != null)
        {
            ConditionReturn += " AND VOLID=@vID ";
        }
        if (SearchStructure.txtvName != null)
        {
            ConditionReturn += " AND VOLName like @vName ";
        }
        if (SearchStructure.txtvSex != null && SearchStructure.txtvSex != "0")
        {
            ConditionReturn += " AND Sex=(@vSex)";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] searchVolunteerDataCount(SearchVolunteer SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchVolunteerConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM  VolunteerData WHERE isDeleted =0 " + ConditionReturn  ;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@vID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtvID);
                cmd.Parameters.Add("@vName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtvName) + "%";
                cmd.Parameters.Add("@vSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtvSex);
                returnValue[0] = cmd.ExecuteScalar().ToString();
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

    public List<CreateVolunteer> searchVolunteerData(int indexpage, SearchVolunteer SearchStructure)
    {
        List<CreateVolunteer> returnValue = new List<CreateVolunteer>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchVolunteerConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY VOLID DESC) " +
                             "AS RowNum, * FROM VolunteerData WHERE isDeleted = 0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)   ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@vID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtvID);
                cmd.Parameters.Add("@vName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtvName) + "%";
                cmd.Parameters.Add("@vSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtvSex);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateVolunteer addValue = new CreateVolunteer();
                    addValue.ID = dr["ID"].ToString();
                    addValue.volunteerId = dr["VOLID"].ToString();
                    addValue.volunteerName = dr["VOLName"].ToString();
                    addValue.volunteerPhone = dr["Phone"].ToString();
                    addValue.vEmail = dr["Email"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateVolunteer addValue = new CreateVolunteer();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }

    public string[] creatVolunteerDataBase(CreateVolunteer SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = "INSERT INTO VolunteerData(Unit, WriteDate, VOLID, VOLName, Sex, Birthday, CurrentWorkOrSchoolName, Tel_Daytime, Tel_Night, Phone, Fax, "+
                    "AddressZip, AddressCity, AddressOther, Email, PersonalExpertise, ServiceExperience, VOLExpect, ServiceTime, ServiceItem, ServiceOther, IntroductionSource, "+
                    "CreateFileBy, UpFileBy, UpFileDate ) " +
                    "VALUES (@Unit, @WriteDate, @VOLID, @VOLName, @Sex, @Birthday, @CurrentWorkOrSchoolName, @Tel_Daytime, @Tel_Night, @Phone, @Fax, @AddressZip, "+
                    "@AddressCity, @AddressOther, @Email, @PersonalExpertise, @ServiceExperience, @VOLExpect, @ServiceTime, @ServiceItem, @ServiceOther, @IntroductionSource, "+
                    "@CreateFileBy, @UpFileBy, (getDate()) )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fillInDate);
                cmd.Parameters.Add("@VOLID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.volunteerId);
                cmd.Parameters.Add("@VOLName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.volunteerName);
                cmd.Parameters.Add("@Sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.vSex);
                cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.vBirthday);
                cmd.Parameters.Add("@CurrentWorkOrSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.nowJob);
                cmd.Parameters.Add("@Tel_Daytime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.telDaytime);
                cmd.Parameters.Add("@Tel_Night", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.telNight);
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.volunteerPhone);
                cmd.Parameters.Add("@Fax", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.volunteerFax);
                cmd.Parameters.Add("@AddressZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.addressZip);
                cmd.Parameters.Add("@AddressCity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.addressCity);
                cmd.Parameters.Add("@AddressOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.address);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.vEmail);
                cmd.Parameters.Add("@PersonalExpertise", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.Expertise);
                cmd.Parameters.Add("@ServiceExperience", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.Experience);
                cmd.Parameters.Add("@VOLExpect", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.VOLExpect);
                cmd.Parameters.Add("@ServiceTime", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.servicedate);
                cmd.Parameters.Add("@ServiceItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.servicecontent);
                cmd.Parameters.Add("@ServiceOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.otherService);
                cmd.Parameters.Add("@IntroductionSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.vSource);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    string FieldName = "Volunteer_" + CreateFileName[2];
                    Int64 Column = 0;
                    sql = "UPDATE AutomaticNumberTable SET " + FieldName + "=" + FieldName + "+1 WHERE ID=1 ";
                    cmd = new SqlCommand(sql, Sqlconn);
                    
                    
                    returnValue[0] = cmd.ExecuteNonQuery().ToString();
                    returnValue[1] = Column.ToString();
                    
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setVolunteerDataBase(CreateVolunteer SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = "UPDATE VolunteerData SET VOLName=@VOLName, Sex=@Sex, Birthday=@Birthday, CurrentWorkOrSchoolName=@CurrentWorkOrSchoolName, "+
                    "Tel_Daytime=@Tel_Daytime, Tel_Night=@Tel_Night, Phone=@Phone, Fax=@Fax, AddressZip=@AddressZip, AddressCity=@AddressCity, AddressOther=@AddressOther, "+
                    "Email=@Email, PersonalExpertise=@PersonalExpertise, ServiceExperience=@ServiceExperience, VOLExpect=@VOLExpect, ServiceTime=@ServiceTime, "+
                    "ServiceItem=@ServiceItem, ServiceOther=@ServiceOther, IntroductionSource=@IntroductionSource " +
                    " WHERE ID = @ID ";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.ID);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.fillInDate);
                cmd.Parameters.Add("@VOLID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.volunteerId);
                cmd.Parameters.Add("@VOLName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.volunteerName);
                cmd.Parameters.Add("@Sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.vSex);
                cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.vBirthday);
                cmd.Parameters.Add("@CurrentWorkOrSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.nowJob);
                cmd.Parameters.Add("@Tel_Daytime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.telDaytime);
                cmd.Parameters.Add("@Tel_Night", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.telNight);
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.volunteerPhone);
                cmd.Parameters.Add("@Fax", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.volunteerFax);
                cmd.Parameters.Add("@AddressZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.addressZip);
                cmd.Parameters.Add("@AddressCity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.addressCity);
                cmd.Parameters.Add("@AddressOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.address);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.vEmail);
                cmd.Parameters.Add("@PersonalExpertise", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.Expertise);
                cmd.Parameters.Add("@ServiceExperience", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.Experience);
                cmd.Parameters.Add("@VOLExpect", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.VOLExpect);
                cmd.Parameters.Add("@ServiceTime", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.servicedate);
                cmd.Parameters.Add("@ServiceItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.servicecontent);
                cmd.Parameters.Add("@ServiceOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.otherService);
                cmd.Parameters.Add("@IntroductionSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.vSource);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }



    public string[] createVolunteerServiceDataBase(CreateVolunteerService VolunteerData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO VolunteerService(VolunteerID,ServiceDate,ServiceStartTime,ServiceEndTime,ServiceHours,Remark)" +
                             " VALUES (@VolunteerID,@vServiceDate,@vServiceBeginTime,@vServiceEndTime,@vServiceHour,@vOtherContent)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);

                cmd.Parameters.Add("@VolunteerID", SqlDbType.Int).Value = Chk.CheckStringFunction(VolunteerData.vID);
                cmd.Parameters.Add("@vServiceDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(VolunteerData.vsDate);
                cmd.Parameters.Add("@vServiceBeginTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsStartTime);
                cmd.Parameters.Add("@vServiceEndTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsEndTime);
                cmd.Parameters.Add("@vServiceHour", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsHours);
                cmd.Parameters.Add("@vOtherContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsContent);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setVolunteerServiceDataBase(CreateVolunteerService VolunteerData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE VolunteerService SET ServiceStartTime=@vServiceBeginTime, ServiceEndTime=@vServiceEndTime, ServiceHours =@vServiceHour, Remark=@vOtherContent " +
                             "WHERE ID=@ServicedataID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ServicedataID", SqlDbType.Int).Value = Chk.CheckStringFunction(VolunteerData.ID);
                cmd.Parameters.Add("@vServiceBeginTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsStartTime);
                cmd.Parameters.Add("@vServiceEndTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsEndTime);
                cmd.Parameters.Add("@vServiceHour", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsHours);
                cmd.Parameters.Add("@vOtherContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(VolunteerData.vsContent);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public string[] delVolunteerServiceData(string VOLID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE VolunteerService SET isDeleted=1 WHERE ID=@ServicedataID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ServicedataID", SqlDbType.Int).Value = VOLID;
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] getVolunteerServiceDataCount(string vID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM  VolunteerService WHERE isDeleted=0 AND VolunteerID = @vID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@vID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(vID);
                returnValue[0] = cmd.ExecuteScalar().ToString();
                returnValue[1] = vID;
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }

        }
        return returnValue;
    }
    public CreateVolunteer getVolunteerServiceData(int indexpage,string vID)
    {
        CreateVolunteer returnValue = this.getVolunteerData(vID);
        returnValue.Service = new List<CreateVolunteerService>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY ID DESC) " +
                             "AS RowNum, * FROM  VolunteerService WHERE isDeleted = 0 AND VolunteerID = @vID ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@vID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(vID);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateVolunteerService addValue = new CreateVolunteerService();
                    addValue.vID = dr["ID"].ToString();
                    addValue.vsDate = DateTime.Parse(dr["ServiceDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.vsStartTime = dr["ServiceStartTime"].ToString();
                    addValue.vsEndTime = dr["ServiceEndTime"].ToString();
                    addValue.vsHours = dr["ServiceHours"].ToString();
                    addValue.vsContent = dr["Remark"].ToString();
                    returnValue.Service.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateVolunteerService addValue = new CreateVolunteerService();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Service.Add(addValue);
            }

        }
        return returnValue;
    }



    private string SearchStudentServiceConditionReturn(SearchStudentService SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND CaseServiceRecord.StudentID=(@StudentID) ";
        }
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentDatabase.StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtstudentSex != null && SearchStructure.txtstudentSex != "0")
        {
            ConditionReturn += " AND StudentDatabase.StudentSex=(@StudentSex) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentDatabase.StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }

        if (SearchStructure.txtcaseStatu != null && SearchStructure.txtcaseStatu != "0")
        {
            ConditionReturn += " AND StudentDatabase.CaseStatu =(@CaseStatu) ";
        }
        if (SearchStructure.txtendviewstart != null && SearchStructure.txtendviewend != null && SearchStructure.txtendviewstart != DateBase && SearchStructure.txtendviewend != DateBase)
        {
            ConditionReturn += " AND CaseServiceRecord.ServiceDate BETWEEN (@sViewDateStart) AND (@sViewDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
         if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND CaseServiceRecord.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStudentServiceCount(SearchStudentService SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentServiceConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM CaseServiceRecord INNER JOIN StudentDatabase ON CaseServiceRecord.StudentID=StudentDatabase.StudentID " +
                    "WHERE CaseServiceRecord.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtcaseStatu);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sViewDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendviewstart);
                cmd.Parameters.Add("@sViewDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendviewend);
                returnValue[0] = cmd.ExecuteScalar().ToString();
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
    public List<SearchStudentServiceResult> SearchStudentService(int indexpage, SearchStudentService SearchStructure)
    {
        List<SearchStudentServiceResult> returnValue = new List<SearchStudentServiceResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentServiceConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StudentDatabase.ID DESC,ServiceDate DESC) " +
                 "AS RowNum, StudentDatabase.StudentName ,StudentDatabase.CaseStatu, CaseServiceRecord.* " +
                 "FROM CaseServiceRecord INNER JOIN StudentDatabase ON CaseServiceRecord.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                 "WHERE CaseServiceRecord.isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@CaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtcaseStatu);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sViewDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendviewstart);
                cmd.Parameters.Add("@sViewDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendviewend);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchStudentServiceResult addValue = new SearchStudentServiceResult();
                    addValue.ID = Int64.Parse(dr["ID"].ToString());
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtstudentStatu = dr["CaseStatu"].ToString();
                    addValue.txtviewData = DateTime.Parse(dr["ServiceDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtviewPeople = dr["Objects"].ToString();
                    addValue.txtviewStyle = dr["Method"].ToString();
                    addValue.txtviewTitle = dr["Theme"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchStudentServiceResult addValue = new SearchStudentServiceResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }


    public string[] createStudentService(CreateStudentService StudentService)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        CaseDataBase CData = new CaseDataBase();
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        int viewStaff1 = 0;
        int viewStaff2 = 0;
        if (StudentService.viewStaff.Count < 2)
        {
            viewStaff1 = Chk.CheckStringtoIntFunction(StudentService.viewStaff[0]);
        }
        else if (StudentService.viewStaff.Count >= 2)
        {
            viewStaff1 = Chk.CheckStringtoIntFunction(StudentService.viewStaff[0]);
            viewStaff2 = Chk.CheckStringtoIntFunction(StudentService.viewStaff[1]);
        }
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO CaseServiceRecord(Unit, StudentID, ServiceDate, ServiceTime, Method, Theme, Objects, Place, Personnel1, Personnel2, Item, CreateFileBy, UpFileBy, UpFileDate ) " +
                    "VALUES (@Unit, @StudentID, @ServiceDate, @ServiceTime, @Method, @Theme, @Objects, @Place, @Personnel1, @Personnel2, @Item, @CreateFileBy, @UpFileBy, (getDate()) )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.studentID);
                cmd.Parameters.Add("@ServiceDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentService.viewData);
                cmd.Parameters.Add("@ServiceTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewTime);
                cmd.Parameters.Add("@Method", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentService.viewStyle);
                cmd.Parameters.Add("@Theme", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewTitle);
                cmd.Parameters.Add("@Objects", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewPeople);
                cmd.Parameters.Add("@Place", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewPlace);
                cmd.Parameters.Add("@Personnel1", SqlDbType.Int).Value = viewStaff1;
                cmd.Parameters.Add("@Personnel2", SqlDbType.Int).Value = viewStaff2;
                cmd.Parameters.Add("@Item", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewContent);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('CaseServiceRecord') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();
                }

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public CreateStudentService getStudentService(Int64 ID)
    {
        CreateStudentService returnValue = new CreateStudentService();
        returnValue.viewStaff = new List<string>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT CaseServiceRecord.*, StudentDatabase.StudentName, StudentDatabase.StudentBirthday, StudentDatabase.StudentSex " +
                            "FROM CaseServiceRecord INNER JOIN StudentDatabase ON CaseServiceRecord.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                            "WHERE CaseServiceRecord.isDeleted=0 AND CaseServiceRecord.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = ID;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentSex = dr["StudentSex"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sUnit = dr["Unit"].ToString();
                    returnValue.viewData = DateTime.Parse(dr["ServiceDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.viewTime = dr["ServiceTime"].ToString();
                    returnValue.viewStyle = dr["Method"].ToString();
                    returnValue.viewTitle = dr["Theme"].ToString();
                    returnValue.viewPeople = dr["Objects"].ToString();
                    returnValue.viewPlace = dr["Place"].ToString();
                    returnValue.viewStaff.Add(dr["Personnel1"].ToString());
                    returnValue.viewStaff.Add(dr["Personnel2"].ToString());
                    returnValue.viewContent = dr["Item"].ToString();
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message;
            }

        }
        return returnValue;
    }

    public string[] setStudentService(CreateStudentService StudentService)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        CaseDataBase CData = new CaseDataBase();
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        int viewStaff1 = 0;
        int viewStaff2 = 0;
        if (StudentService.viewStaff.Count < 2 && StudentService.viewStaff.Count != 0)
        {
            viewStaff1 = Chk.CheckStringtoIntFunction(StudentService.viewStaff[0]);
        }
        else if (StudentService.viewStaff.Count >= 2)
        {
            viewStaff1 = Chk.CheckStringtoIntFunction(StudentService.viewStaff[0]);
            viewStaff2 = Chk.CheckStringtoIntFunction(StudentService.viewStaff[1]);
        }
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE CaseServiceRecord SET ServiceDate=@ServiceDate, ServiceTime=@ServiceTime, Method=@Method, Theme=@Theme, Objects=@Objects, " +
                    "Place=@Place, Personnel1=@Personnel1, Personnel2=@Personnel2, Item=@Item, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(StudentService.ID);
                cmd.Parameters.Add("@ServiceDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentService.viewData);
                cmd.Parameters.Add("@ServiceTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewTime);
                cmd.Parameters.Add("@Method", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentService.viewStyle);
                cmd.Parameters.Add("@Theme", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewTitle);
                cmd.Parameters.Add("@Objects", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewPeople);
                cmd.Parameters.Add("@Place", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewPlace);
                cmd.Parameters.Add("@Personnel1", SqlDbType.Int).Value = viewStaff1;
                cmd.Parameters.Add("@Personnel2", SqlDbType.Int).Value = viewStaff2;
                cmd.Parameters.Add("@Item", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentService.viewContent);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    private string SearchStudentTrackedConditionReturn(SearchStudent SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND StudentTrackedOffData.StudentID=(@StudentID) ";
        }
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtstudentSex != null && SearchStructure.txtstudentSex != "0")
        {
            ConditionReturn += " AND StudentSex=(@StudentSex) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStructure.txtendReasonDatestart != null && SearchStructure.txtendReasonDateend != null && SearchStructure.txtendReasonDatestart != DateBase && SearchStructure.txtendReasonDateend != DateBase)
        {
            ConditionReturn += " AND CompletedDate BETWEEN (@sCompletedDateStart) AND (@sCompletedDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND StudentTrackedOffData.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStudentTrackedCount(SearchStudent SearchStructure)
    {

        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentTrackedConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM StudentTrackedOffData INNER JOIN StudentDatabase ON StudentTrackedOffData.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                    "WHERE StudentTrackedOffData.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sCompletedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDatestart);
                cmd.Parameters.Add("@sCompletedDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDateend);

                returnValue[0] = cmd.ExecuteScalar().ToString();
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
    public List<SearchStudentTrackResult> SearchStudentTracked(int indexpage, SearchStudent SearchStructure)
    {
        List<SearchStudentTrackResult> returnValue = new List<SearchStudentTrackResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentTrackedConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StudentTrackedOffData.ID DESC) " +
                 "AS RowNum, StudentDatabase.StudentName ,StudentDatabase.CompletedType, StudentDatabase.CompletedDate, StudentTrackedOffData.* " +
                 "FROM StudentTrackedOffData INNER JOIN StudentDatabase ON StudentTrackedOffData.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                 "WHERE StudentTrackedOffData.isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sCompletedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDatestart);
                cmd.Parameters.Add("@sCompletedDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDateend);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchStudentTrackResult addValue = new SearchStudentTrackResult();
                    addValue.ID = Int64.Parse(dr["ID"].ToString());
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtendReasonDateType = dr["CompletedType"].ToString();
                    addValue.txtendReasonDate = DateTime.Parse(dr["CompletedDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtemail = dr["StudentEmail"].ToString();
                    addValue.txtTel = dr["Phone"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchStudentTrackResult addValue = new SearchStudentTrackResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] createStudentTrackedDataBase(CreateStudentTracked StudentTracked)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO StudentTrackedOffData(Unit, StudentID, StudentEmail, AddressZip2, AddressCity2, " +
                    "AddressOther2, Phone, DisabilityCategory1, DisabilityGrade1, DisabilityCategory2, DisabilityGrade2, DisabilityCategory3, DisabilityGrade3, " +
                    "HearingAids_R, AidsBrand_R, AidsModel_R, AidsOptionalTime_R, AidsOptionalLocation_R, EEarHospital_R, EEarOpen_R, HearingAids_L, AidsBrand_L, "+
                    "AidsModel_L, AidsOptionalTime_L, AidsOptionalLocation_L, EEarHospital_L, EEarOpen_L, ElementarySchool, ElementarySchoolName, JuniorHighSchool, " +
                    "JuniorHighSchoolName, HighSchool, HighSchoolName, University, UniversityName, JobName, OtherConditions, OtherConditionsName, CreateFileBy,"+
                    " UpFileBy, UpFileDate,ElementarySY,ElementarySM,ElementaryEY,ElementaryEM ,JuniorHighSY,JuniorHighSM,JuniorHighEY,JuniorHighEM,HighSY "+
      ",HighSM,HighEY,HighEM ,UniversitySY,UniversitySM,UniversityEY,UniversityEM,JobSY,JobSM,JobEY,JobEM,OtherSY,OtherSM,OtherEY,OtherEM,remark ) " +
                    "VALUES (@Unit, @StudentID, @StudentEmail, @AddressZip2, @AddressCity2, @AddressOther2, @Phone, " +
                    "@DisabilityCategory1, @DisabilityGrade1, @DisabilityCategory2, @DisabilityGrade2, @DisabilityCategory3, @DisabilityGrade3, @HearingAids_R, "+
                    "@AidsBrand_R, @AidsModel_R, @AidsOptionalTime_R, @AidsOptionalLocation_R, @EEarHospital_R, @EEarOpen_R, @HearingAids_L, @AidsBrand_L, "+
                    "@AidsModel_L, @AidsOptionalTime_L, @AidsOptionalLocation_L, @EEarHospital_L, @EEarOpen_L, @ElementarySchool, @ElementarySchoolName, "+
                    "@JuniorHighSchool, @JuniorHighSchoolName, @HighSchool, @HighSchoolName, @University, @UniversityName, @JobName, @OtherConditions, "+
                    "@OtherConditionsName, @CreateFileBy, @UpFileBy, (getDate()),@ElementarySY,@ElementarySM,@ElementaryEY,@ElementaryEM ,@JuniorHighSY,@JuniorHighSM,@JuniorHighEY,@JuniorHighEM,@HighSY " +
      ",@HighSM,@HighEY,@HighEM ,@UniversitySY,@UniversitySM,@UniversityEY,@UniversityEM,@JobSY,@JobSM,@JobEY,@JobEM,@OtherSY,@OtherSM,@OtherEY,@OtherEM,@remark )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.studentID);
                cmd.Parameters.Add("@StudentEmail", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.email);
                cmd.Parameters.Add("@AddressZip2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.addressZip);
                cmd.Parameters.Add("@AddressCity2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.addressCity);
                cmd.Parameters.Add("@AddressOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.address);
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.Tel);
                cmd.Parameters.Add("@DisabilityCategory1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory1);
                cmd.Parameters.Add("@DisabilityGrade1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade1);
                cmd.Parameters.Add("@DisabilityCategory2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory2);
                cmd.Parameters.Add("@DisabilityGrade2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade2);
                cmd.Parameters.Add("@DisabilityCategory3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory3);
                cmd.Parameters.Add("@DisabilityGrade3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade3);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.openHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.openHzDateL);
                cmd.Parameters.Add("@ElementarySchool", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.esType);
                cmd.Parameters.Add("@ElementarySchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.esName);
                cmd.Parameters.Add("@JuniorHighSchool", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.jsType);
                cmd.Parameters.Add("@JuniorHighSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.jsName);
                cmd.Parameters.Add("@HighSchool", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.hsType);
                cmd.Parameters.Add("@HighSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.hsName);
                cmd.Parameters.Add("@University", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.uType);
                cmd.Parameters.Add("@UniversityName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.uName);
                cmd.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.jobUnit);
                cmd.Parameters.Add("@OtherConditions", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.otherType);
                cmd.Parameters.Add("@OtherConditionsName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.otherName);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);

                cmd.Parameters.Add("@ElementarySY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementarySY);
                cmd.Parameters.Add("@ElementarySM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementarySM);
                cmd.Parameters.Add("@ElementaryEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementaryEY);
                cmd.Parameters.Add("@ElementaryEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementaryEM);

                cmd.Parameters.Add("@JuniorHighSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighSY);
                cmd.Parameters.Add("@JuniorHighSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighSM);
                cmd.Parameters.Add("@JuniorHighEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighEY);
                cmd.Parameters.Add("@JuniorHighEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighEM);

                cmd.Parameters.Add("@HighSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighSY);
                cmd.Parameters.Add("@HighSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighSM);
                cmd.Parameters.Add("@HighEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighEY);
                cmd.Parameters.Add("@HighEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighEM);

                cmd.Parameters.Add("@UniversitySY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversitySY);
                cmd.Parameters.Add("@UniversitySM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversitySM);
                cmd.Parameters.Add("@UniversityEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversityEY);
                cmd.Parameters.Add("@UniversityEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversityEM);

                cmd.Parameters.Add("@JobSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobSY);
                cmd.Parameters.Add("@JobSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobSM);
                cmd.Parameters.Add("@JobEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobEY);
                cmd.Parameters.Add("@JobEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobEM);

                cmd.Parameters.Add("@OtherSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherSY);
                cmd.Parameters.Add("@OtherSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherSM);
                cmd.Parameters.Add("@OtherEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherEY);
                cmd.Parameters.Add("@OtherEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherEM);


                cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.remark);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('StudentTrackedOffData') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();
                }

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setStudentTrackedDataBase(CreateStudentTracked StudentTracked)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE StudentTrackedOffData SET StudentEmail=@StudentEmail, " +
                    "Phone=@Phone, DisabilityCategory1=@DisabilityCategory1, DisabilityGrade1=@DisabilityGrade1, " +
                    "DisabilityCategory2=@DisabilityCategory2, DisabilityGrade2=@DisabilityGrade2, DisabilityCategory3=@DisabilityCategory3, " +
                    "DisabilityGrade3=@DisabilityGrade3, ElementarySchool=@ElementarySchool,"+
                    " ElementarySchoolName=@ElementarySchoolName, JuniorHighSchool=@JuniorHighSchool, " +
                    "JuniorHighSchoolName=@JuniorHighSchoolName, HighSchool=@HighSchool, HighSchoolName=@HighSchoolName, University=@University, " +
                    "UniversityName=@UniversityName, JobName=@JobName, OtherConditions=@OtherConditions, OtherConditionsName=@OtherConditionsName, " +
                    "ElementarySY=@ElementarySY ,ElementarySM=@ElementarySM ,ElementaryEY=@ElementaryEY ,ElementaryEM=@ElementaryEM , " +
                "JuniorHighSY=@JuniorHighSY ,JuniorHighSM=@JuniorHighSM ,JuniorHighEY=@JuniorHighEY ,JuniorHighEM=@JuniorHighEM , " +
                "HighSY=@HighSY ,HighSM=@HighSM ,HighEY=@HighEY ,HighEM=@HighEM , " +
                "UniversitySY=@UniversitySY ,UniversitySM=@UniversitySM ,UniversityEY=@UniversityEY ,UniversityEM=@UniversityEM , " +
                "JobSY=@JobSY ,JobSM=@JobSM ,JobEY=@JobEY ,JobEM=@JobEM , " +
                "OtherSY=@OtherSY ,OtherSM=@OtherSM ,OtherEY=@OtherEY ,OtherEM=@OtherEM , remark=@remark, " +
                    "UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentTracked.ID);
                cmd.Parameters.Add("@StudentEmail", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.email);
                
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.Tel);
                cmd.Parameters.Add("@DisabilityCategory1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory1);
                cmd.Parameters.Add("@DisabilityGrade1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade1);
                cmd.Parameters.Add("@DisabilityCategory2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory2);
                cmd.Parameters.Add("@DisabilityGrade2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade2);
                cmd.Parameters.Add("@DisabilityCategory3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory3);
                cmd.Parameters.Add("@DisabilityGrade3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade3);
                
                cmd.Parameters.Add("@ElementarySchool", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.esType);
                cmd.Parameters.Add("@ElementarySchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.esName);
                cmd.Parameters.Add("@JuniorHighSchool", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.jsType);
                cmd.Parameters.Add("@JuniorHighSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.jsName);
                cmd.Parameters.Add("@HighSchool", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.hsType);
                cmd.Parameters.Add("@HighSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.hsName);
                cmd.Parameters.Add("@University", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.uType);
                cmd.Parameters.Add("@UniversityName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.uName);
                cmd.Parameters.Add("@JobName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.jobUnit);
                cmd.Parameters.Add("@OtherConditions", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.otherType);
                cmd.Parameters.Add("@OtherConditionsName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.otherName);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                
                cmd.Parameters.Add("@ElementarySY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementarySY);
                cmd.Parameters.Add("@ElementarySM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementarySM);
                cmd.Parameters.Add("@ElementaryEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementaryEY);
                cmd.Parameters.Add("@ElementaryEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ElementaryEM);
                
                cmd.Parameters.Add("@JuniorHighSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighSY);
                cmd.Parameters.Add("@JuniorHighSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighSM);
                cmd.Parameters.Add("@JuniorHighEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighEY);
                cmd.Parameters.Add("@JuniorHighEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JuniorHighEM);
                
                cmd.Parameters.Add("@HighSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighSY);
                cmd.Parameters.Add("@HighSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighSM);
                cmd.Parameters.Add("@HighEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighEY);
                cmd.Parameters.Add("@HighEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.HighEM);
                
                cmd.Parameters.Add("@UniversitySY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversitySY);
                cmd.Parameters.Add("@UniversitySM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversitySM);
                cmd.Parameters.Add("@UniversityEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversityEY);
                cmd.Parameters.Add("@UniversityEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.UniversityEM);
                
                cmd.Parameters.Add("@JobSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobSY);
                cmd.Parameters.Add("@JobSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobSM);
                cmd.Parameters.Add("@JobEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobEY);
                cmd.Parameters.Add("@JobEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.JobEM);
                
                cmd.Parameters.Add("@OtherSY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherSY);
                cmd.Parameters.Add("@OtherSM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherSM);
                cmd.Parameters.Add("@OtherEY", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherEY);
                cmd.Parameters.Add("@OtherEM", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.OtherEM);
                

                cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.remark);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public CreateStudentTracked getStudentTrackedDataBase(Int64 tID)
    {
        CreateStudentTracked returnValue = new CreateStudentTracked();
        returnValue.Teack = new List<TeackedData>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StudentTrackedOffData.*, StudentDatabase.* " +
                            "FROM StudentTrackedOffData INNER JOIN StudentDatabase ON StudentTrackedOffData.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                            "WHERE StudentTrackedOffData.isDeleted=0 AND StudentTrackedOffData.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = tID;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.sUnit = dr["Unit"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentSex = dr["StudentSex"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.endReasonDate = DateTime.Parse(dr["CompletedDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.firstClassDate = DateTime.Parse(dr["ClassDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.email = dr["StudentEmail"].ToString();
                    returnValue.censusAddressZip = dr["AddressZip1"].ToString();
                    returnValue.censusAddressCity = dr["AddressCity1"].ToString();
                    returnValue.censusAddress = dr["AddressOther1"].ToString();

                    returnValue.addressZip = dr["AddressZip2"].ToString();
                    returnValue.addressCity = dr["AddressCity2"].ToString();
                    returnValue.address = dr["AddressOther2"].ToString();
                    returnValue.Tel = dr["Phone"].ToString();
                    
                    returnValue.manualCategory1 = dr["DisabilityCategory1"].ToString();
                    returnValue.manualGrade1 = dr["DisabilityGrade1"].ToString();
                    returnValue.manualCategory2 = dr["DisabilityCategory2"].ToString();
                    returnValue.manualGrade2 = dr["DisabilityGrade2"].ToString();
                    returnValue.manualCategory3 = dr["DisabilityCategory3"].ToString();
                    returnValue.manualGrade3 = dr["DisabilityGrade3"].ToString();
                    returnValue.assistmanageR = dr["HearingAids_R"].ToString();
                    returnValue.brandR = dr["AidsBrand_R"].ToString();
                    returnValue.modelR = dr["AidsModel_R"].ToString();
                    returnValue.buyingPlaceR = dr["AidsOptionalLocation_R"].ToString();
                    returnValue.buyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.insertHospitalR = dr["EEarHospital_R"].ToString();
                    returnValue.openHzDateR = DateTime.Parse(dr["EEarOpen_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.assistmanageL = dr["HearingAids_L"].ToString();
                    returnValue.brandL = dr["AidsBrand_L"].ToString();
                    returnValue.modelL = dr["AidsModel_L"].ToString();
                    returnValue.buyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.buyingPlaceL = dr["AidsOptionalLocation_L"].ToString();
                    returnValue.insertHospitalL = dr["EEarHospital_L"].ToString();
                    returnValue.openHzDateL = DateTime.Parse(dr["EEarOpen_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.esType = dr["ElementarySchool"].ToString();
                    returnValue.esName = dr["ElementarySchoolName"].ToString();
                    returnValue.jsType = dr["JuniorHighSchool"].ToString();
                    returnValue.jsName = dr["JuniorHighSchoolName"].ToString();
                    returnValue.hsType = dr["HighSchool"].ToString();
                    returnValue.hsName = dr["HighSchoolName"].ToString();
                    returnValue.uType = dr["University"].ToString();
                    returnValue.uName = dr["UniversityName"].ToString();
                    returnValue.jobUnit = dr["JobName"].ToString();
                    returnValue.otherType = dr["OtherConditions"].ToString();
                    returnValue.otherName = dr["OtherConditionsName"].ToString();


                    returnValue.fPRelation1 = dr["ContactRelation1"].ToString();
                    returnValue.fPName1 = dr["ContactName1"].ToString();
                    returnValue.fPTel1 = dr["ContactTel_company1"].ToString();
                    returnValue.fPPhone1 = dr["ContactPhone1"].ToString();
                    returnValue.fPHPhone1 = dr["ContactTel_home1"].ToString();
                    returnValue.fPFax1 = dr["ContactFax1"].ToString();
                    returnValue.fPRelation2 = dr["ContactRelation2"].ToString();
                    returnValue.fPName2 = dr["ContactName2"].ToString();
                    returnValue.fPTel2 = dr["ContactTel_company2"].ToString();
                    returnValue.fPPhone2 = dr["ContactPhone2"].ToString();
                    returnValue.fPHPhone2 = dr["ContactTel_home2"].ToString();
                    returnValue.fPFax2 = dr["ContactFax2"].ToString();
                    returnValue.fPRelation3 = dr["ContactRelation3"].ToString();
                    returnValue.fPName3 = dr["ContactName3"].ToString();
                    returnValue.fPTel3 = dr["ContactTel_company3"].ToString();
                    returnValue.fPPhone3 = dr["ContactPhone3"].ToString();
                    returnValue.fPHPhone3 = dr["ContactTel_home3"].ToString();
                    returnValue.fPFax3 = dr["ContactFax3"].ToString();
                    returnValue.fPRelation4 = dr["ContactRelation4"].ToString();
                    returnValue.fPName4 = dr["ContactName4"].ToString();
                    returnValue.fPTel4 = dr["ContactTel_company4"].ToString();
                    returnValue.fPPhone4 = dr["ContactPhone4"].ToString();
                    returnValue.fPHPhone4 = dr["ContactTel_home4"].ToString();
                    returnValue.fPFax4 = dr["ContactFax4"].ToString();

                    returnValue.ElementarySY = dr["ElementarySY"].ToString();
                    returnValue.ElementarySM = dr["ElementarySM"].ToString();
                    returnValue.ElementaryEY = dr["ElementaryEY"].ToString();
                    returnValue.ElementaryEM = dr["ElementaryEM"].ToString();

                    returnValue.JuniorHighSY = dr["JuniorHighSY"].ToString();
                    returnValue.JuniorHighSM = dr["JuniorHighSM"].ToString();
                    returnValue.JuniorHighEY = dr["JuniorHighEY"].ToString();
                    returnValue.JuniorHighEM = dr["JuniorHighEM"].ToString();

                    returnValue.HighSY = dr["HighSY"].ToString();
                    returnValue.HighSM = dr["HighSM"].ToString();
                    returnValue.HighEY = dr["HighEY"].ToString();
                    returnValue.HighEM = dr["HighEM"].ToString();

                    returnValue.UniversitySY = dr["UniversitySY"].ToString();
                    returnValue.UniversitySM = dr["UniversitySM"].ToString();
                    returnValue.UniversityEY = dr["UniversityEY"].ToString();
                    returnValue.UniversityEM = dr["UniversityEM"].ToString();

                    returnValue.JobSY = dr["JobSY"].ToString();
                    returnValue.JobSM = dr["JobSM"].ToString();
                    returnValue.JobEY = dr["JobEY"].ToString();
                    returnValue.JobEM = dr["JobEM"].ToString();

                    returnValue.OtherSY = dr["OtherSY"].ToString();
                    returnValue.OtherSM = dr["OtherSM"].ToString();
                    returnValue.OtherEY = dr["OtherEY"].ToString();
                    returnValue.OtherEM = dr["OtherEM"].ToString();
                    returnValue.remark = dr["remark"].ToString();

                    returnValue.Teack = new List<TeackedData>();

                    

                }
                dr.Close();

                sql = "SELECT StudentTracked.*,StaffDatabase.StaffName FROM StudentTracked INNER JOIN StaffDatabase ON StudentTracked.Personnel=StaffDatabase.StaffID "+
                    "WHERE StudentTracked.isDeleted=0 AND TrackedID=@TrackedID order by StudentTracked.TrackDate desc";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@TrackedID", SqlDbType.BigInt).Value = returnValue.ID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TeackedData addValue = new TeackedData();
                    addValue.tID = dr["ID"].ToString();
                    addValue.tDate = DateTime.Parse(dr["TrackDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.tStaffName = dr["StaffName"].ToString();
                    addValue.tStaff = dr["Personnel"].ToString();
                    addValue.tContent = dr["TrackItem"].ToString();
                    returnValue.Teack.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message.ToString();
            }

        }
        return returnValue;
    }

    public string[] createStudentTrackedRecord(TeackedData StudentTracked)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO StudentTracked(TrackedID, TrackDate, Personnel, TrackItem, CreateFileBy, UpFileBy, UpFileDate ) " +
                             "VALUES (@TrackedID, @TrackDate, @Personnel, @TrackItem, @CreateFileBy, @UpFileBy, (getDate()) )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@TrackedID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentTracked.offID);
                cmd.Parameters.Add("@TrackDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.tDate);
                cmd.Parameters.Add("@TrackItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.tContent);
                cmd.Parameters.Add("@Personnel", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setStudentTrackedRecord(TeackedData StudentTracked)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE StudentTracked SET TrackItem=@TrackItem, UpFileBy=@UpFileBy, UpFileDate=(getDate())  " +
                             "WHERE ID=@ID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentTracked.tID);
                cmd.Parameters.Add("@TrackItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.tContent);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] delStudentTrackedRecord(string tID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE StudentTracked SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate())  " +
                             "WHERE ID=@ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(tID);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] createStudentAid(CreateStudentAid StudentTracked)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO FinancialAidApplications(WriteDate, Unit, StudentID, AidCategory, AidCategory_other, DataPayment, DisabilityCategory, " +
                             "DisabilityGrade, OtherNotes, Grants, GrantsTimeSince, GrantsTimeUntil , ContactName, ContactTel_home, ContactPhone, AddressZip, " +
                             "AddressCity, AddressOther, CreateFileBy, UpFileBy, UpFileDate ) " +
                             "VALUES (@WriteDate, @Unit, @StudentID, @AidCategory, @AidCategory_other, @DataPayment, @DisabilityCategory, @DisabilityGrade, " +
                             "@OtherNotes, @Grants, @GrantsTimeSince, @GrantsTimeUntil, @ContactName, @ContactTel_home, @ContactPhone, @AddressZip, @AddressCity, " +
                             "@AddressOther, @CreateFileBy, @UpFileBy, (getDate()) )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.fillInDate);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.studentID);
                cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ParentName);
                cmd.Parameters.Add("@ContactTel_home", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ParentTel);
                cmd.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ParentPhone);
                cmd.Parameters.Add("@AddressZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.addressZip);
                cmd.Parameters.Add("@AddressCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.addressCity);
                cmd.Parameters.Add("@AddressOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.address);
                cmd.Parameters.Add("@AidCategory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.subsidyitem);
                cmd.Parameters.Add("@AidCategory_other", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.subsidytext);
                cmd.Parameters.Add("@DataPayment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.payitem);
                cmd.Parameters.Add("@DisabilityCategory", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory);
                cmd.Parameters.Add("@DisabilityGrade", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade);
                cmd.Parameters.Add("@OtherNotes", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.othertext);
                cmd.Parameters.Add("@Grants", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.subsidymoney);
                cmd.Parameters.Add("@GrantsTimeSince", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.subsidydate1);
                cmd.Parameters.Add("@GrantsTimeUntil", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.subsidydate2);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('FinancialAidApplications') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public string[] setStudentAid(CreateStudentAid StudentTracked)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE FinancialAidApplications SET ContactName=@ContactName, ContactTel_home=@ContactTel_home, ContactPhone=@ContactPhone, " +
                            "AddressZip=@AddressZip, AddressCity=@AddressCity, AddressOther=@AddressOther, AidCategory=@AidCategory, AidCategory_other=@AidCategory_other, " +
                            "DataPayment=@DataPayment, DisabilityCategory=@DisabilityCategory, DisabilityGrade=@DisabilityGrade, OtherNotes=@OtherNotes, Grants=@Grants, " +
                            "GrantsTimeSince=@GrantsTimeSince, GrantsTimeUntil=@GrantsTimeUntil, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                            "WHERE ID=@ID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentTracked.ID);
                cmd.Parameters.Add("@ContactName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ParentName);
                cmd.Parameters.Add("@ContactTel_home", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ParentTel);
                cmd.Parameters.Add("@ContactPhone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.ParentPhone);
                cmd.Parameters.Add("@AddressZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.addressZip);
                cmd.Parameters.Add("@AddressCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.addressCity);
                cmd.Parameters.Add("@AddressOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.address);
                cmd.Parameters.Add("@AidCategory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentTracked.subsidyitem);
                cmd.Parameters.Add("@AidCategory_other", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.subsidytext);
                cmd.Parameters.Add("@DataPayment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.payitem);
                cmd.Parameters.Add("@DisabilityCategory", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualCategory);
                cmd.Parameters.Add("@DisabilityGrade", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.manualGrade);
                cmd.Parameters.Add("@OtherNotes", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentTracked.othertext);
                cmd.Parameters.Add("@Grants", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentTracked.subsidymoney);
                cmd.Parameters.Add("@GrantsTimeSince", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.subsidydate1);
                cmd.Parameters.Add("@GrantsTimeUntil", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentTracked.subsidydate2);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public CreateStudentAid getStudentAidData(string aID)
    {
        CreateStudentAid returnValue = new CreateStudentAid();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT FinancialAidApplications.*, StudentDatabase.StudentName, StudentDatabase.StudentBirthday, StudentDatabase.StudentIdentity " +
                            "FROM FinancialAidApplications INNER JOIN StudentDatabase ON FinancialAidApplications.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                            "WHERE FinancialAidApplications.isDeleted=0 AND FinancialAidApplications.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(aID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.addressZip = dr["AddressZip"].ToString();
                    returnValue.addressCity = dr["AddressCity"].ToString();
                    returnValue.address = dr["AddressOther"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sUnit = dr["Unit"].ToString();
                    returnValue.subsidyitem = dr["AidCategory"].ToString();
                    returnValue.subsidytext = dr["AidCategory_other"].ToString();
                    returnValue.payitem = dr["DataPayment"].ToString();
                    returnValue.manualCategory = dr["DisabilityCategory"].ToString();
                    returnValue.manualGrade = dr["DisabilityGrade"].ToString();
                    returnValue.othertext = dr["OtherNotes"].ToString();
                    returnValue.subsidymoney = dr["Grants"].ToString();
                    returnValue.subsidydate1 = DateTime.Parse(dr["GrantsTimeSince"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.subsidydate2 = DateTime.Parse(dr["GrantsTimeUntil"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.ParentName = dr["ContactName"].ToString();
                    returnValue.ParentTel = dr["ContactTel_home"].ToString();
                    returnValue.ParentPhone = dr["ContactPhone"].ToString();
                    returnValue.studentTWID = dr["StudentIdentity"].ToString();
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message;
            }

        }
        return returnValue;
    }

    private string SearchStudentAidConditionReturn(SearchStudentAid SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND FinancialAidApplications.StudentID=(@StudentID) ";
        }
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtstudentSex != null && SearchStructure.txtstudentSex != "0")
        {
            ConditionReturn += " AND StudentSex=(@StudentSex) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStructure.txtsubsidyitem != null && SearchStructure.txtsubsidyitem != "0")
        {
            ConditionReturn += " AND AidCategory =(@AidCategory) ";
        }
        if (SearchStructure.txtfillInDatestart != null && SearchStructure.txtfillInDateend != null && SearchStructure.txtfillInDatestart != DateBase && SearchStructure.txtfillInDateend != DateBase)
        {
            ConditionReturn += " AND FinancialAidApplications.WriteDate BETWEEN (@sfillInDateStart) AND (@sfillInDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND FinancialAidApplications.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] searcStudentAidCount(SearchStudentAid SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentAidConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM FinancialAidApplications INNER JOIN StudentDatabase ON FinancialAidApplications.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                    "WHERE FinancialAidApplications.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sfillInDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtfillInDatestart);
                cmd.Parameters.Add("@sfillIndDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtfillInDateend);
                cmd.Parameters.Add("@AidCategory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtsubsidyitem);
                returnValue[0] = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public List<SearchStudentAidResult> searcStudentAidt(int indexpage, SearchStudentAid SearchStructure)
    {
        List<SearchStudentAidResult> returnValue = new List<SearchStudentAidResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentAidConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY FinancialAidApplications.ID DESC) " +
                             "AS RowNum, FinancialAidApplications.*,StudentDatabase.StudentName, StudentDatabase.StudentIdentity " +
                             "FROM FinancialAidApplications INNER JOIN StudentDatabase ON FinancialAidApplications.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                             "WHERE FinancialAidApplications.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sfillInDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtfillInDatestart);
                cmd.Parameters.Add("@sfillIndDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtfillInDateend);
                cmd.Parameters.Add("@AidCategory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtsubsidyitem);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchStudentAidResult addValue = new SearchStudentAidResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtfillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtsubsidyitem = dr["AidCategory"].ToString();
                    addValue.txtsubsidymoney = dr["Grants"].ToString();
                    addValue.txtsubsidydate1 = DateTime.Parse(dr["GrantsTimeSince"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtsubsidydate2 = DateTime.Parse(dr["GrantsTimeUntil"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtstudentTWID = dr["StudentIdentity"].ToString();
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchStudentAidResult addValue = new SearchStudentAidResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }


    public string[] createResourcedData(CreateResourceCard createData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO ResourceCard(Unit, WriteDate, ORGName, ORGItem, Category, AddressZip, AddressCity, AddressOther, ContactName1, " +
                    "ContacTel1, ContacFax1, ContactEmail1, ContactName2, ContacTel2, ContacFax2, ContactEmail2, ContactName3, ContacTel3, ContacFax3, ContactEmail3, " +
                    "ReferralUnit, ServiceObject, ServiceTime, ServiceItem, Charge, ApplicationProcedure, ResourceItem, ResourceLinks, CreateFileBy, UpFileBy, UpFileDate ) " +
                    "VALUES (@Unit, @WriteDate, @ORGName, @ORGItem, @Category, @AddressZip, @AddressCity, @AddressOther, @ContactName1, @ContacTel1, @ContacFax1, " +
                    "@ContactEmail1, @ContactName2, @ContacTel2, @ContacFax2, @ContactEmail2, @ContactName3, @ContacTel3, @ContacFax3, @ContactEmail3, @ReferralUnit, " +
                    "@ServiceObject, @ServiceTime, @ServiceItem, @Charge, @ApplicationProcedure, @ResourceItem, @ResourceLinks , @CreateFileBy, @UpFileBy, (getDate()) )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(createData.fillInDate);
                cmd.Parameters.Add("@ORGName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.resourceName);
                cmd.Parameters.Add("@ORGItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.resourceItem);
                cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(createData.resourceType);
                cmd.Parameters.Add("@AddressZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.addressZip);
                cmd.Parameters.Add("@AddressCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(createData.addressCity);
                cmd.Parameters.Add("@AddressOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.address);
                cmd.Parameters.Add("@ContactName1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contact_1);
                cmd.Parameters.Add("@ContacTel1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactPhone_1);
                cmd.Parameters.Add("@ContacFax1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactFax_1);
                cmd.Parameters.Add("@ContactEmail1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactEmail_1);
                cmd.Parameters.Add("@ContactName2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contact_2);
                cmd.Parameters.Add("@ContacTel2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactPhone_2);
                cmd.Parameters.Add("@ContacFax2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactFax_2);
                cmd.Parameters.Add("@ContactEmail2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactEmail_2);
                cmd.Parameters.Add("@ContactName3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contact_3);
                cmd.Parameters.Add("@ContacTel3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactPhone_3);
                cmd.Parameters.Add("@ContacFax3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactFax_3);
                cmd.Parameters.Add("@ContactEmail3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactEmail_3);
                cmd.Parameters.Add("@ReferralUnit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(createData.referral);
                cmd.Parameters.Add("@ServiceObject", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sObject);
                cmd.Parameters.Add("@ServiceTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sTime);
                cmd.Parameters.Add("@ServiceItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sItem);
                cmd.Parameters.Add("@Charge", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sExpense);
                cmd.Parameters.Add("@ApplicationProcedure", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sProgram);
                cmd.Parameters.Add("@ResourceItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sInformation);
                cmd.Parameters.Add("@ResourceLinks", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sLink);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('ResourceCard') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public CreateResourceCard getResourcedData(string ID)
    {
        CreateResourceCard returnValue = new CreateResourceCard();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT ResourceCard.*,a.StaffName AS upBy,b.StaffName AS fillInBy FROM ResourceCard  " +
                            "LEFT JOIN StaffDatabase a ON ResourceCard.UpFileBy=a.StaffID " +
                            "LEFT JOIN StaffDatabase b ON ResourceCard.CreateFileBy=b.StaffID " +
                            "WHERE ResourceCard.isDeleted=0 AND ResourceCard.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.sUnit = dr["Unit"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.resourceName = dr["ORGName"].ToString();
                    returnValue.resourceItem = dr["ORGItem"].ToString();
                    returnValue.resourceType = dr["Category"].ToString();
                    returnValue.addressZip = dr["AddressZip"].ToString();
                    returnValue.addressCity = dr["AddressCity"].ToString();
                    returnValue.address = dr["AddressOther"].ToString();
                    returnValue.contact_1 = dr["ContactName1"].ToString();
                    returnValue.contactPhone_1 = dr["ContacTel1"].ToString();
                    returnValue.contactFax_1 = dr["ContacFax1"].ToString();
                    returnValue.contactEmail_1 = dr["ContactEmail1"].ToString();
                    returnValue.contact_2 = dr["ContactName2"].ToString();
                    returnValue.contactPhone_2 = dr["ContacTel2"].ToString();
                    returnValue.contactFax_2 = dr["ContacFax2"].ToString();
                    returnValue.contactEmail_2 = dr["ContactEmail2"].ToString();
                    returnValue.contact_3 = dr["ContactName3"].ToString();
                    returnValue.contactPhone_3 = dr["ContacTel3"].ToString();
                    returnValue.contactFax_3 = dr["ContacFax3"].ToString();
                    returnValue.contactEmail_3 = dr["ContactEmail3"].ToString();
                    returnValue.referral = dr["ReferralUnit"].ToString();
                    returnValue.sObject = dr["ServiceObject"].ToString();
                    returnValue.sTime = dr["ServiceTime"].ToString();
                    returnValue.sItem = dr["ServiceItem"].ToString();
                    returnValue.sExpense = dr["Charge"].ToString();
                    returnValue.sProgram = dr["ApplicationProcedure"].ToString();
                    returnValue.sInformation = dr["ResourceItem"].ToString();
                    returnValue.sLink = dr["ResourceLinks"].ToString();
                    returnValue.fillInBy = dr["fillInBy"].ToString();
                    returnValue.upBy = dr["upBy"].ToString();
                    returnValue.upDate = DateTime.Parse(dr["UpFileDate"].ToString()).ToString("yyyy-MM-dd");
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message;
            }

        }
        return returnValue;
    }
    public string[] setResourceData(CreateResourceCard createData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE ResourceCard SET ORGName=@ORGName, ORGItem=@ORGItem, Category=@Category, AddressZip=@AddressZip, AddressCity=@AddressCity, "+
                    "AddressOther=@AddressOther, ContactName1=@ContactName1, ContacTel1=@ContacTel1, ContacFax1=@ContacFax1, ContactEmail1=@ContactEmail1, "+
                    "ContactName2=@ContactName2, ContacTel2=@ContacTel2, ContacFax2=@ContacFax2, ContactEmail2=@ContactEmail2, ContactName3=@ContactName3, "+
                    "ContacTel3=@ContacTel3, ContacFax3=@ContacFax3, ContactEmail3=@ContactEmail3, ReferralUnit=@ReferralUnit, ServiceObject=@ServiceObject, "+
                    "ServiceTime=@ServiceTime, ServiceItem=@ServiceItem, Charge=@Charge, ApplicationProcedure=@ApplicationProcedure, ResourceItem=@ResourceItem, "+
                    "ResourceLinks=@ResourceLinks, UpFileBy=@UpFileBy, UpFileDate=(getDate()) "+
                    "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(createData.ID);
                cmd.Parameters.Add("@ORGName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.resourceName);
                cmd.Parameters.Add("@ORGItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.resourceItem);
                cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(createData.resourceType);
                cmd.Parameters.Add("@AddressZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.addressZip);
                cmd.Parameters.Add("@AddressCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(createData.addressCity);
                cmd.Parameters.Add("@AddressOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.address);
                cmd.Parameters.Add("@ContactName1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contact_1);
                cmd.Parameters.Add("@ContacTel1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactPhone_1);
                cmd.Parameters.Add("@ContacFax1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactFax_1);
                cmd.Parameters.Add("@ContactEmail1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactEmail_1);
                cmd.Parameters.Add("@ContactName2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contact_2);
                cmd.Parameters.Add("@ContacTel2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactPhone_2);
                cmd.Parameters.Add("@ContacFax2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactFax_2);
                cmd.Parameters.Add("@ContactEmail2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactEmail_2);
                cmd.Parameters.Add("@ContactName3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contact_3);
                cmd.Parameters.Add("@ContacTel3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactPhone_3);
                cmd.Parameters.Add("@ContacFax3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactFax_3);
                cmd.Parameters.Add("@ContactEmail3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.contactEmail_3);
                cmd.Parameters.Add("@ReferralUnit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(createData.referral);
                cmd.Parameters.Add("@ServiceObject", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sObject);
                cmd.Parameters.Add("@ServiceTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sTime);
                cmd.Parameters.Add("@ServiceItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sItem);
                cmd.Parameters.Add("@Charge", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sExpense);
                cmd.Parameters.Add("@ApplicationProcedure", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sProgram);
                cmd.Parameters.Add("@ResourceItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sInformation);
                cmd.Parameters.Add("@ResourceLinks", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(createData.sLink);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }


    private string SearchResourceConditionReturn(SearchResourceCard SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtresourceName != null)
        {
            ConditionReturn += " AND ORGName like (@ORGName) ";
        }
        if (SearchStructure.txtresourceItem != null)
        {
            ConditionReturn += " AND ORGItem like (@ORGItem) ";
        }
        if (SearchStructure.txtaddressCity != null && SearchStructure.txtaddressCity != "0")
        {
            ConditionReturn += " AND AddressCity=(@AddressCity) ";
        }
        if (SearchStructure.txtresourceType != null && SearchStructure.txtresourceType != "0")
        {
            ConditionReturn += " AND Category=(@Category) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchResourceDataCount(SearchResourceCard SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchResourceConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM ResourceCard WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ORGName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtresourceName) + "%";
                cmd.Parameters.Add("@ORGItem", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtresourceItem) + "%";
                cmd.Parameters.Add("@AddressCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtaddressCity);
                cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtresourceType);
                returnValue[0] = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }
    public List<SearchResourceCardResult> searchResourceDataBase(int indexpage, SearchResourceCard SearchStructure)
    {
        List<SearchResourceCardResult> returnValue = new List<SearchResourceCardResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchResourceConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY ID DESC) " +
                             "AS RowNum, * FROM ResourceCard  " +
                             "WHERE isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@ORGName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtresourceName) + "%";
                cmd.Parameters.Add("@ORGItem", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtresourceItem) + "%";
                cmd.Parameters.Add("@AddressCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtaddressCity);
                cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtresourceType);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchResourceCardResult addValue = new SearchResourceCardResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtresourceName = dr["ORGName"].ToString();
                    addValue.txtaddressCity = dr["AddressCity"].ToString();
                    addValue.txtresourceType = dr["Category"].ToString();
                    addValue.txtresourceItem = dr["ORGItem"].ToString();
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchResourceCardResult addValue = new SearchResourceCardResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }


    private string SearchStudentVisitRecordConditionReturn(SearchVisitRecord SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND CaseVisitRecord.StudentID=(@StudentID) ";
        }
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentDatabase.StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtstudentSex != null && SearchStructure.txtstudentSex != "0")
        {
            ConditionReturn += " AND StudentDatabase.StudentSex=(@StudentSex) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentDatabase.StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStructure.txtvisitType != null && SearchStructure.txtvisitType != "0")
        {
            ConditionReturn += " AND CaseVisitRecord.VisitType =(@VisitType) ";
        }
        if (SearchStructure.txtvisitDatestart != null && SearchStructure.txtvisitDateend != null && SearchStructure.txtvisitDatestart != DateBase && SearchStructure.txtvisitDateend != DateBase)
        {
            ConditionReturn += " AND CaseVisitRecord.VisitDateTime BETWEEN (@sVisitDateStart) AND (@sVisitDateEnd) ";
        }
        if (SearchStructure.txtvisitSocial != null )
        {
            ConditionReturn += " AND StaffDatabase.StaffName like @StaffName ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND CaseVisitRecord.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStudentVisitRecordCount(SearchVisitRecord SearchStructure)
    {

        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentVisitRecordConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM CaseVisitRecord INNER JOIN StudentDatabase ON CaseVisitRecord.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                    "INNER JOIN StaffDatabase ON CaseVisitRecord.CreateFileBy=StaffDatabase.StaffID " +
                    "WHERE CaseVisitRecord.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sVisitDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtvisitDatestart);
                cmd.Parameters.Add("@sVisitDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtvisitDateend);
                cmd.Parameters.Add("@VisitType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtvisitType);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = SearchStructure.txtvisitSocial + "%";
                
                returnValue[0] = cmd.ExecuteScalar().ToString();
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
    public List<SearchVisitRecordResult> SearchStudentVisitRecord(int indexpage, SearchVisitRecord SearchStructure)
    {
        List<SearchVisitRecordResult> returnValue = new List<SearchVisitRecordResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentVisitRecordConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY CaseVisitRecord.ID DESC) " +
                 "AS RowNum, StudentDatabase.StudentName, CaseVisitRecord.* , StaffDatabase.StaffName " +
                 "FROM CaseVisitRecord INNER JOIN StudentDatabase ON CaseVisitRecord.StudentID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0" +
                 "INNER JOIN StaffDatabase ON CaseVisitRecord.CreateFileBy=StaffDatabase.StaffID " +
                 "WHERE CaseVisitRecord.isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sVisitDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtvisitDatestart);
                cmd.Parameters.Add("@sVisitDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtvisitDateend);
                cmd.Parameters.Add("@VisitType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtvisitType);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = SearchStructure.txtvisitSocial + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchVisitRecordResult addValue = new SearchVisitRecordResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtvisitType = dr["VisitType"].ToString();
                    addValue.txtvisitDate = DateTime.Parse(dr["VisitDateTime"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtvisitSocial = dr["StaffName"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchVisitRecordResult addValue = new SearchVisitRecordResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] createStudentVisitData(CreateStudentView StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO CaseVisitRecord(Unit, StudentID, VisitType, Purpose, VisitStaff, VisitDateTime, VisitTimeSince, VisitTimeUntil, VisitUnit, " +
                    "VisitUnitTel, VisitUnitZip, VisitUnitCity, VisitUnitOther, Participants, ContentRecord, CaseRecord, Remark, VisitPeople, VisitPlace, FamilyProfile1, " +
                    "FamilyProfile2, TreatmentUnit, TreatmentDate, TreatmentType1, TreatmentType2, TreatmentUnit1, Frequency1, FrequencyTime1, TreatmentType3, TreatmentUnit2, " +
                    "Frequency2, FrequencyTime2, NurserySchool, AttendedDate1, AttendedDate2, AttendedType, FamilyType, FamilyTypeText, PrimaryCaregiver, PrimaryCaregiverText, " +
                    "PrimaryCareTime, HomeOwnership, HomeEnvironment, HomeEnvironmentText, HomeType, HomeTypeText, TransportType, LeisurePlace, LeisurePlaceText, Remark2, " +
                    "HomeSpace1, HomeSpace2, Furniture, Electric, DangerPlace, Hydro, GarbagePlace, KitchenHealth, VentilateRoom, AdequateLight, SpaceSpaciou, SoundGood, " +
                    "Remark3, EconomicResource, PolicyDecision, Teaching, CareResources, EmotionalSupport, Problem, ResourceUse, ResourceUseText, Interactions, InteractionsText, " +
                    "Assistance, AssistanceText, ProblemSolve, Remark4, MedicalResources, InterventionResources, EconomicResources, fCareresources, fEmotionalSupport, " +
                    "EducationalResources, ReligiousResources, CulturalResources, fOther, fAssistance, fAssistanceText, Workaround, Remark5, FamilyEconomicResources, " +
                    "FamilyMedicalResources, FamilyInterventionResources, FamilyReligiousResources, FamilySources, FamilyEmotionalSupport, VisitSituation, FamilySuperiority, " +
                    "FamilyLimit, CaseSuperiority, CaseLimit, PrimaryCareSuperiority, PrimaryCareLimit, HouseholdDemand, SocialAssessment, TrafficRoute, Remark6, VisitPhoto1, " +
                    "VisitPhotoText1, VisitPhoto2, VisitPhotoText2, VisitPhoto3, VisitPhotoText3, VisitPhoto4, VisitPhotoText4, VisitPhoto5, VisitPhotoText5, VisitPhoto6, VisitPhotoText6, VisitPhoto7, VisitPhotoText7, VisitPhoto8, VisitPhotoText8, TeachingEnvironment, FamilySupport, FamilyInteraction, TeachingDemonstration, Interviews, TeachingAnalysis, CreateFileBy, UpFileBy, UpFileDate ) " +
                    "VALUES (@Unit, @StudentID, @VisitType, @Purpose, @VisitStaff, @VisitDateTime, @VisitTimeSince, @VisitTimeUntil, @VisitUnit, @VisitUnitTel, " +
                    "@VisitUnitZip, @VisitUnitCity, @VisitUnitOther, @Participants, @ContentRecord, @CaseRecord, @Remark, @VisitPeople, @VisitPlace, @FamilyProfile1, " +
                    "@FamilyProfile2, @TreatmentUnit, @TreatmentDate, @TreatmentType1, @TreatmentType2, @TreatmentUnit1, @Frequency1, @FrequencyTime1, @TreatmentType3, " +
                    "@TreatmentUnit2, @Frequency2, @FrequencyTime2, @NurserySchool, @AttendedDate1, @AttendedDate2, @AttendedType, @FamilyType, @FamilyTypeText, " +
                    "@PrimaryCaregiver, @PrimaryCaregiverText, @PrimaryCareTime, @HomeOwnership, @HomeEnvironment, @HomeEnvironmentText, @HomeType, @HomeTypeText, " +
                    "@TransportType, @LeisurePlace, @LeisurePlaceText, @Remark2, @HomeSpace1, @HomeSpace2, @Furniture, @Electric, @DangerPlace, @Hydro, @GarbagePlace, " +
                    "@KitchenHealth, @VentilateRoom, @AdequateLight, @SpaceSpaciou, @SoundGood, @Remark3, @EconomicResource, @PolicyDecision, @Teaching, @CareResources, " +
                    "@EmotionalSupport, @Problem, @ResourceUse, @ResourceUseText, @Interactions, @InteractionsText, @Assistance, @AssistanceText, @ProblemSolve, @Remark4, " +
                    "@MedicalResources, @InterventionResources, @EconomicResources, @fCareresources, @fEmotionalSupport, @EducationalResources, @ReligiousResources, " +
                    "@CulturalResources, @fOther, @fAssistance, @fAssistanceText, @Workaround, @Remark5, @FamilyEconomicResources, @FamilyMedicalResources, " +
                    "@FamilyInterventionResources, @FamilyReligiousResources, @FamilySources, @FamilyEmotionalSupport, @VisitSituation, @FamilySuperiority, " +
                    "@FamilyLimit, @CaseSuperiority, @CaseLimit, @PrimaryCareSuperiority, @PrimaryCareLimit, @HouseholdDemand, @SocialAssessment, @TrafficRoute, " +
                    "@Remark6, @VisitPhoto1, @VisitPhotoText1, @VisitPhoto2, @VisitPhotoText2, @VisitPhoto3, @VisitPhotoText3, @VisitPhoto4, @VisitPhotoText4, @VisitPhoto5, " +
                    "@VisitPhotoText5, @VisitPhoto6, @VisitPhotoText6, @VisitPhoto7, @VisitPhotoText7, @VisitPhoto8, @VisitPhotoText8, @TeachingEnvironment, @FamilySupport, " +
                    "@FamilyInteraction, @TeachingDemonstration, @Interviews, @TeachingAnalysis, @CreateFileBy, @UpFileBy, (getDate()) )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentID);
                cmd.Parameters.Add("@VisitType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.visitType);
                cmd.Parameters.Add("@Purpose", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.target);
                cmd.Parameters.Add("@VisitStaff", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.viewSocialWork);
                cmd.Parameters.Add("@VisitDateTime", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.viewDate);
                cmd.Parameters.Add("@VisitTimeSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewTime1);
                cmd.Parameters.Add("@VisitTimeUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewTime2);
                cmd.Parameters.Add("@VisitUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewUnit);
                cmd.Parameters.Add("@VisitUnitTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewTel);
                cmd.Parameters.Add("@VisitUnitZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.addressZip);
                cmd.Parameters.Add("@VisitUnitCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.addressCity);
                cmd.Parameters.Add("@VisitUnitOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.address);
                cmd.Parameters.Add("@Participants", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPeople1);
                cmd.Parameters.Add("@ContentRecord", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewContent1);
                cmd.Parameters.Add("@CaseRecord", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewContent2);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewRemark1);
                cmd.Parameters.Add("@VisitPeople", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.viewPeople2);
                cmd.Parameters.Add("@VisitPlace", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPlace);
                cmd.Parameters.Add("@FamilyProfile1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.pedigree);
                cmd.Parameters.Add("@FamilyProfile2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.ecological);
                cmd.Parameters.Add("@TreatmentUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.cureUnit);
                cmd.Parameters.Add("@TreatmentDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.cureDate);
                cmd.Parameters.Add("@TreatmentType1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureType);
                cmd.Parameters.Add("@TreatmentType2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureType1);
                cmd.Parameters.Add("@TreatmentUnit1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.cureUnit1);
                cmd.Parameters.Add("@Frequency1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumber1);
                cmd.Parameters.Add("@FrequencyTime1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumberTime1);
                cmd.Parameters.Add("@TreatmentType3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureType2);
                cmd.Parameters.Add("@TreatmentUnit2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.cureUnit2);
                cmd.Parameters.Add("@Frequency2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumber2);
                cmd.Parameters.Add("@FrequencyTime2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumberTime2);
                cmd.Parameters.Add("@NurserySchool", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.preSchool);
                cmd.Parameters.Add("@AttendedDate1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studyDate1);
                cmd.Parameters.Add("@AttendedDate2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studyDate2);
                cmd.Parameters.Add("@AttendedType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studyType);
                cmd.Parameters.Add("@FamilyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.familyType);
                cmd.Parameters.Add("@FamilyTypeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyTypeText);
                cmd.Parameters.Add("@PrimaryCaregiver", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.careProple);
                cmd.Parameters.Add("@PrimaryCaregiverText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.carePropleText);
                cmd.Parameters.Add("@PrimaryCareTime", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.careTime);
                cmd.Parameters.Add("@HomeOwnership", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.homeOwnership);
                cmd.Parameters.Add("@HomeEnvironment", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.homeAround);
                cmd.Parameters.Add("@HomeEnvironmentText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeAroundText);
                cmd.Parameters.Add("@HomeType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.homeType);
                cmd.Parameters.Add("@HomeTypeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeTypeText);
                cmd.Parameters.Add("@TransportType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transportType);
                cmd.Parameters.Add("@LeisurePlace", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.leisureType);
                cmd.Parameters.Add("@LeisurePlaceText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.leisureTypeText);
                cmd.Parameters.Add("@Remark2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation2);
                cmd.Parameters.Add("@HomeSpace1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeSpace1);
                cmd.Parameters.Add("@HomeSpace2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeSpace2);
                cmd.Parameters.Add("@Furniture", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.furniture);
                cmd.Parameters.Add("@Electric", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.electric);
                cmd.Parameters.Add("@DangerPlace", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.place);
                cmd.Parameters.Add("@Hydro", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.hydro);
                cmd.Parameters.Add("@GarbagePlace", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.dining1);
                cmd.Parameters.Add("@KitchenHealth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.dining2);
                cmd.Parameters.Add("@VentilateRoom", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor1);
                cmd.Parameters.Add("@AdequateLight", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor2);
                cmd.Parameters.Add("@SpaceSpaciou", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor3);
                cmd.Parameters.Add("@SoundGood", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor4);
                cmd.Parameters.Add("@Remark3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation3);
                cmd.Parameters.Add("@EconomicResource", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal1);
                cmd.Parameters.Add("@PolicyDecision", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal2);
                cmd.Parameters.Add("@Teaching", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal3);
                cmd.Parameters.Add("@CareResources", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal4);
                cmd.Parameters.Add("@EmotionalSupport", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal5);
                cmd.Parameters.Add("@Problem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal6);
                cmd.Parameters.Add("@ResourceUse", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal7);
                cmd.Parameters.Add("@ResourceUseText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.informal7Text);
                cmd.Parameters.Add("@Interactions", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal8);
                cmd.Parameters.Add("@InteractionsText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.informal8Text);
                cmd.Parameters.Add("@Assistance", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal9);
                cmd.Parameters.Add("@AssistanceText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.informal9Text);
                cmd.Parameters.Add("@ProblemSolve", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal10);
                cmd.Parameters.Add("@Remark4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation4);
                cmd.Parameters.Add("@MedicalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText1);
                cmd.Parameters.Add("@InterventionResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText2);
                cmd.Parameters.Add("@EconomicResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText3);
                cmd.Parameters.Add("@fCareresources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText4);
                cmd.Parameters.Add("@fEmotionalSupport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText5);
                cmd.Parameters.Add("@EducationalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText6);
                cmd.Parameters.Add("@ReligiousResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText7);
                cmd.Parameters.Add("@CulturalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText8);
                cmd.Parameters.Add("@fOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText9);
                cmd.Parameters.Add("@fAssistance", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.formal1);
                cmd.Parameters.Add("@fAssistanceText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formal2);
                cmd.Parameters.Add("@Workaround", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.formal3);
                cmd.Parameters.Add("@Remark5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation5);
                cmd.Parameters.Add("@FamilyEconomicResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction1);
                cmd.Parameters.Add("@FamilyMedicalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction2);
                cmd.Parameters.Add("@FamilyInterventionResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction3);
                cmd.Parameters.Add("@FamilyReligiousResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction4);
                cmd.Parameters.Add("@FamilySources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction5);
                cmd.Parameters.Add("@FamilyEmotionalSupport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction6);
                cmd.Parameters.Add("@VisitSituation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewSituation);
                cmd.Parameters.Add("@FamilySuperiority", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyStrengths);
                cmd.Parameters.Add("@FamilyLimit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyLimit);
                cmd.Parameters.Add("@CaseSuperiority", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.caseStrengths);
                cmd.Parameters.Add("@CaseLimit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.caseLimit);
                cmd.Parameters.Add("@PrimaryCareSuperiority", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.primaryCaseStrengths);
                cmd.Parameters.Add("@PrimaryCareLimit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.primaryCaseLimit);
                cmd.Parameters.Add("@HouseholdDemand", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyDemand);
                cmd.Parameters.Add("@SocialAssessment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.assessment);
                cmd.Parameters.Add("@TrafficRoute", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.trafficRoute);
                cmd.Parameters.Add("@Remark6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation6);
                cmd.Parameters.Add("@VisitPhoto1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto1);
                cmd.Parameters.Add("@VisitPhotoText1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText1);
                cmd.Parameters.Add("@VisitPhoto2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto2);
                cmd.Parameters.Add("@VisitPhotoText2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText2);
                cmd.Parameters.Add("@VisitPhoto3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto3);
                cmd.Parameters.Add("@VisitPhotoText3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText3);
                cmd.Parameters.Add("@VisitPhoto4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto4);
                cmd.Parameters.Add("@VisitPhotoText4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText4);
                cmd.Parameters.Add("@VisitPhoto5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto5);
                cmd.Parameters.Add("@VisitPhotoText5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText5);
                cmd.Parameters.Add("@VisitPhoto6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto6);
                cmd.Parameters.Add("@VisitPhotoText6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText6);
                cmd.Parameters.Add("@VisitPhoto7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto7);
                cmd.Parameters.Add("@VisitPhotoText7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText7);
                cmd.Parameters.Add("@VisitPhoto8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto8);
                cmd.Parameters.Add("@VisitPhotoText8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText8);
                cmd.Parameters.Add("@TeachingEnvironment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.teachingSpace);
                cmd.Parameters.Add("@FamilySupport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familySupport);
                cmd.Parameters.Add("@FamilyInteraction", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyInteraction);
                cmd.Parameters.Add("@TeachingDemonstration", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.teachingDemonstration);
                cmd.Parameters.Add("@Interviews", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewConclusion);
                cmd.Parameters.Add("@TeachingAnalysis", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.teachingAnalysis);

                cmd.Parameters.Add("@Personnel", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('CaseVisitRecord') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }

    public CreateStudentView getStudentVisitData(string ID)
    {
        CreateStudentView returnValue = new CreateStudentView();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StudentDatabase.StudentName, CaseVisitRecord.* , StaffDatabase.StaffName " +
                 "FROM CaseVisitRecord INNER JOIN StudentDatabase ON CaseVisitRecord.StudentID=StudentDatabase.StudentID " +
                 "INNER JOIN StaffDatabase ON CaseVisitRecord.CreateFileBy=StaffDatabase.StaffID " +
                 "WHERE CaseVisitRecord.isDeleted=0 AND CaseVisitRecord.ID=@ID"; 

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.visitType = dr["VisitType"].ToString();
                    returnValue.target = dr["Purpose"].ToString();
                    returnValue.viewSocialWork = dr["StaffName"].ToString();
                    returnValue.viewDate = DateTime.Parse(dr["VisitDateTime"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.viewTime1 = dr["VisitTimeSince"].ToString();
                    returnValue.viewTime2 = dr["VisitTimeUntil"].ToString();
                    returnValue.viewUnit = dr["VisitUnit"].ToString();
                    returnValue.viewTel = dr["VisitUnitTel"].ToString();
                    returnValue.addressZip = dr["VisitUnitZip"].ToString();
                    returnValue.addressCity = dr["VisitUnitCity"].ToString();
                    returnValue.address = dr["VisitUnitOther"].ToString();
                    returnValue.viewPeople1 = dr["Participants"].ToString();
                    returnValue.viewContent1 = dr["ContentRecord"].ToString();
                    returnValue.viewContent2 = dr["CaseRecord"].ToString();
                    returnValue.viewRemark1 = dr["Remark"].ToString();
                    returnValue.viewPeople2 = dr["VisitPeople"].ToString();
                    returnValue.viewPlace = dr["VisitPlace"].ToString();
                    returnValue.pedigree = dr["FamilyProfile1"].ToString();
                    returnValue.ecological = dr["FamilyProfile2"].ToString();
                    returnValue.cureUnit = dr["TreatmentUnit"].ToString();
                    returnValue.cureDate = DateTime.Parse(dr["TreatmentDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.cureType = dr["TreatmentType1"].ToString();
                    returnValue.cureType1 = dr["TreatmentType2"].ToString();
                    returnValue.cureUnit1 = dr["TreatmentUnit1"].ToString();
                    returnValue.cureNumber1 = dr["Frequency1"].ToString();
                    returnValue.cureNumberTime1 = dr["FrequencyTime1"].ToString();
                    returnValue.cureType2 = dr["TreatmentType3"].ToString();
                    returnValue.cureUnit2 = dr["TreatmentUnit2"].ToString();
                    returnValue.cureNumber2 = dr["Frequency2"].ToString();
                    returnValue.cureNumberTime2 = dr["FrequencyTime2"].ToString();
                    returnValue.preSchool = dr["NurserySchool"].ToString();
                    returnValue.studyDate1 = dr["AttendedDate1"].ToString();
                    returnValue.studyDate2 = dr["AttendedDate2"].ToString();
                    returnValue.studyType = dr["AttendedType"].ToString();
                    returnValue.familyType = dr["FamilyType"].ToString();
                    returnValue.familyTypeText = dr["FamilyTypeText"].ToString();
                    returnValue.careProple = dr["PrimaryCaregiver"].ToString();
                    returnValue.carePropleText = dr["PrimaryCaregiverText"].ToString();
                    returnValue.careTime = dr["PrimaryCareTime"].ToString();
                    returnValue.homeOwnership = dr["HomeOwnership"].ToString();
                    returnValue.homeAround = dr["HomeEnvironment"].ToString();
                    returnValue.homeAroundText = dr["HomeEnvironmentText"].ToString();
                    returnValue.homeType = dr["HomeType"].ToString();
                    returnValue.homeTypeText = dr["HomeTypeText"].ToString();
                    returnValue.transportType = dr["TransportType"].ToString();
                    returnValue.leisureType = dr["LeisurePlace"].ToString();
                    returnValue.leisureTypeText = dr["LeisurePlaceText"].ToString();
                    returnValue.explanation2 = dr["Remark2"].ToString();
                    returnValue.homeSpace1 = dr["HomeSpace1"].ToString();
                    returnValue.homeSpace2 = dr["HomeSpace2"].ToString();
                    returnValue.furniture = dr["Furniture"].ToString();
                    returnValue.electric = dr["Electric"].ToString();
                    returnValue.place = dr["DangerPlace"].ToString();
                    returnValue.hydro = dr["Hydro"].ToString();
                    returnValue.dining1 = dr["GarbagePlace"].ToString();
                    returnValue.dining2 = dr["KitchenHealth"].ToString();
                    returnValue.indoor1 = dr["VentilateRoom"].ToString();
                    returnValue.indoor2 = dr["AdequateLight"].ToString();
                    returnValue.indoor3 = dr["SpaceSpaciou"].ToString();
                    returnValue.indoor4 = dr["SoundGood"].ToString();
                    returnValue.explanation3 = dr["Remark3"].ToString();
                    returnValue.informal1 = dr["EconomicResource"].ToString();
                    returnValue.informal2 = dr["PolicyDecision"].ToString();
                    returnValue.informal3 = dr["Teaching"].ToString();
                    returnValue.informal4 = dr["CareResources"].ToString();
                    returnValue.informal5 = dr["EmotionalSupport"].ToString();
                    returnValue.informal6 = dr["Problem"].ToString();
                    returnValue.informal7 = dr["ResourceUse"].ToString();
                    returnValue.informal7Text = dr["ResourceUseText"].ToString();
                    returnValue.informal8 = dr["Interactions"].ToString();
                    returnValue.informal8Text = dr["InteractionsText"].ToString();
                    returnValue.informal9 = dr["Assistance"].ToString();
                    returnValue.informal9Text = dr["AssistanceText"].ToString();
                    returnValue.informal10 = dr["ProblemSolve"].ToString();
                    returnValue.explanation4 = dr["Remark4"].ToString();
                    returnValue.formalText1 = dr["MedicalResources"].ToString();
                    returnValue.formalText2 = dr["InterventionResources"].ToString();
                    returnValue.formalText3 = dr["EconomicResources"].ToString();
                    returnValue.formalText4 = dr["fCareresources"].ToString();
                    returnValue.formalText5 = dr["fEmotionalSupport"].ToString();
                    returnValue.formalText6 = dr["EducationalResources"].ToString();
                    returnValue.formalText7 = dr["ReligiousResources"].ToString();
                    returnValue.formalText8 = dr["CulturalResources"].ToString();
                    returnValue.formalText9 = dr["fOther"].ToString();
                    returnValue.formal1 = dr["fAssistance"].ToString();
                    returnValue.formal2 = dr["fAssistanceText"].ToString();
                    returnValue.formal3 = dr["Workaround"].ToString();
                    returnValue.explanation5 = dr["Remark5"].ToString();
                    returnValue.familyFunction1 = dr["FamilyEconomicResources"].ToString();
                    returnValue.familyFunction2 = dr["FamilyMedicalResources"].ToString();
                    returnValue.familyFunction3 = dr["FamilyInterventionResources"].ToString();
                    returnValue.familyFunction4 = dr["FamilyReligiousResources"].ToString();
                    returnValue.familyFunction5 = dr["FamilySources"].ToString();
                    returnValue.familyFunction6 = dr["FamilyEmotionalSupport"].ToString();
                    returnValue.viewSituation = dr["VisitSituation"].ToString();
                    returnValue.familyStrengths = dr["FamilySuperiority"].ToString();
                    returnValue.familyLimit = dr["FamilyLimit"].ToString();
                    returnValue.caseStrengths = dr["CaseSuperiority"].ToString();
                    returnValue.caseLimit = dr["CaseLimit"].ToString();
                    returnValue.primaryCaseStrengths = dr["PrimaryCareSuperiority"].ToString();
                    returnValue.primaryCaseLimit = dr["PrimaryCareLimit"].ToString();
                    returnValue.familyDemand = dr["HouseholdDemand"].ToString();
                    returnValue.assessment = dr["SocialAssessment"].ToString();
                    returnValue.trafficRoute = dr["TrafficRoute"].ToString();
                    returnValue.explanation6 = dr["Remark6"].ToString();
                    returnValue.viewPhoto1 = dr["VisitPhoto1"].ToString();
                    returnValue.viewPhotoText1 = dr["VisitPhotoText1"].ToString();
                    returnValue.viewPhoto2 = dr["VisitPhoto2"].ToString();
                    returnValue.viewPhotoText2 = dr["VisitPhotoText2"].ToString();
                    returnValue.viewPhoto3 = dr["VisitPhoto3"].ToString();
                    returnValue.viewPhotoText3 = dr["VisitPhotoText3"].ToString();
                    returnValue.viewPhoto4 = dr["VisitPhoto4"].ToString();
                    returnValue.viewPhotoText4 = dr["VisitPhotoText4"].ToString();
                    returnValue.viewPhoto5 = dr["VisitPhoto5"].ToString();
                    returnValue.viewPhotoText5 = dr["VisitPhotoText5"].ToString();
                    returnValue.viewPhoto6 = dr["VisitPhoto6"].ToString();
                    returnValue.viewPhotoText6 = dr["VisitPhotoText6"].ToString();
                    returnValue.viewPhoto7 = dr["VisitPhoto7"].ToString();
                    returnValue.viewPhotoText7 = dr["VisitPhotoText7"].ToString();
                    returnValue.viewPhoto8 = dr["VisitPhoto8"].ToString();
                    returnValue.viewPhotoText8 = dr["VisitPhotoText8"].ToString();
                    returnValue.teachingSpace = dr["TeachingEnvironment"].ToString();
                    returnValue.familySupport = dr["FamilySupport"].ToString();
                    returnValue.familyInteraction = dr["FamilyInteraction"].ToString();
                    returnValue.teachingDemonstration = dr["TeachingDemonstration"].ToString();
                    returnValue.viewConclusion = dr["Interviews"].ToString();
                    returnValue.teachingAnalysis = dr["TeachingAnalysis"].ToString();

                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.ID = "-1";
            }
        }
        return returnValue;
    }


    public string[] setStudentVisitData(CreateStudentView StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        string PicUpdatestring = "";

        if (StudentData.pedigree != null) { 
            PicUpdatestring +=" FamilyProfile1=@FamilyProfile1, ";
        }
        if (StudentData.ecological != null) {
            PicUpdatestring += " FamilyProfile2=@FamilyProfile2, ";
        }
        if (StudentData.trafficRoute != null)
        {
            PicUpdatestring += "TrafficRoute=@TrafficRoute, ";
        }
        if (StudentData.viewPhoto1 != null) {
             PicUpdatestring += "VisitPhoto1=@VisitPhoto1, ";
        }
        if (StudentData.viewPhoto2 != null) {
            PicUpdatestring += "VisitPhoto2=@VisitPhoto2, ";
        }
        if (StudentData.viewPhoto3 != null) {
           PicUpdatestring += "VisitPhoto3=@VisitPhoto3, ";
        }
        if (StudentData.viewPhoto4 != null) {
           PicUpdatestring += "VisitPhoto4=@VisitPhoto4, ";
        }
        if (StudentData.viewPhoto5 != null) {
           PicUpdatestring += "VisitPhoto5=@VisitPhoto5, ";
        }
        if (StudentData.viewPhoto6 != null) {
            PicUpdatestring += "VisitPhoto6=@VisitPhoto6, ";
        }
        if (StudentData.viewPhoto7 != null) {
           PicUpdatestring += "VisitPhoto7=@VisitPhoto7, ";
        }
        if (StudentData.viewPhoto8 != null)
        {
            PicUpdatestring += "VisitPhoto8=@VisitPhoto8, ";
        }
        DataBase Base = new DataBase();
        SqlConnection Sqlconn = new SqlConnection(Base.GetConnString());
        using (Sqlconn)
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE CaseVisitRecord SET Purpose=@Purpose, VisitStaff=@VisitStaff, VisitDateTime=@VisitDateTime, VisitTimeSince=@VisitTimeSince, " +
                    "VisitTimeUntil=@VisitTimeUntil, VisitUnit=@VisitUnit, VisitUnitTel=@VisitUnitTel, VisitUnitZip=@VisitUnitZip, VisitUnitCity=@VisitUnitCity, " +
                    "VisitUnitOther=@VisitUnitOther, Participants=@Participants, ContentRecord=@ContentRecord, CaseRecord=@CaseRecord, Remark=@Remark, " +
                    "VisitPeople=@VisitPeople, VisitPlace=@VisitPlace, TreatmentUnit=@TreatmentUnit, " +
                    "TreatmentDate=@TreatmentDate, TreatmentType1=@TreatmentType1, TreatmentType2=@TreatmentType2, TreatmentUnit1=@TreatmentUnit1, Frequency1=@Frequency1, " +
                    "FrequencyTime1=@FrequencyTime1, TreatmentType3=@TreatmentType3, TreatmentUnit2=@TreatmentUnit2, Frequency2=@Frequency2, FrequencyTime2=@FrequencyTime2, " +
                    "NurserySchool=@NurserySchool, AttendedDate1=@AttendedDate1, AttendedDate2=@AttendedDate2, AttendedType=@AttendedType, FamilyType=@FamilyType, " +
                    "FamilyTypeText=@FamilyTypeText, PrimaryCaregiver=@PrimaryCaregiver, PrimaryCaregiverText=@PrimaryCaregiverText, PrimaryCareTime=@PrimaryCareTime, " +
                    "HomeOwnership=@HomeOwnership, HomeEnvironment=@HomeEnvironment, HomeEnvironmentText=@HomeEnvironmentText, HomeType=@HomeType, HomeTypeText=@HomeTypeText, " +
                    "TransportType=@TransportType, LeisurePlace=@LeisurePlace, LeisurePlaceText=@LeisurePlaceText, Remark2=@Remark2, HomeSpace1=@HomeSpace1, " +
                    "HomeSpace2=@HomeSpace2, Furniture=@Furniture, Electric=@Electric, DangerPlace=@DangerPlace, Hydro=@Hydro, GarbagePlace=@GarbagePlace, " +
                    "KitchenHealth=@KitchenHealth, VentilateRoom=@VentilateRoom, AdequateLight=@AdequateLight, SpaceSpaciou=@SpaceSpaciou, SoundGood=@SoundGood, " +
                    "Remark3=@Remark3, EconomicResource=@EconomicResource, PolicyDecision=@PolicyDecision, Teaching=@Teaching, CareResources=@CareResources, " +
                    "EmotionalSupport=@EmotionalSupport, Problem=@Problem, ResourceUse=@ResourceUse, ResourceUseText=@ResourceUseText, Interactions=@Interactions, " +
                    "InteractionsText=@InteractionsText, Assistance=@Assistance, AssistanceText=@AssistanceText, ProblemSolve=@ProblemSolve, Remark4=@Remark4, " +
                    "MedicalResources=@MedicalResources, InterventionResources=@InterventionResources, EconomicResources=@EconomicResources, fCareresources=@fCareresources, " +
                    "fEmotionalSupport=@fEmotionalSupport, EducationalResources=@EducationalResources, ReligiousResources=@ReligiousResources, " +
                    "CulturalResources=@CulturalResources, fOther=@fOther, fAssistance=@fAssistance, fAssistanceText=@fAssistanceText, Workaround=@Workaround, " +
                    "Remark5=@Remark5, FamilyEconomicResources=@FamilyEconomicResources, FamilyMedicalResources=@FamilyMedicalResources, " +
                    "FamilyInterventionResources=@FamilyInterventionResources, FamilyReligiousResources=@FamilyReligiousResources, FamilySources=@FamilySources, " +
                    "FamilyEmotionalSupport=@FamilyEmotionalSupport, VisitSituation=@VisitSituation, FamilySuperiority=@FamilySuperiority, FamilyLimit=@FamilyLimit, " +
                    "CaseSuperiority=@CaseSuperiority, CaseLimit=@CaseLimit, PrimaryCareSuperiority=@PrimaryCareSuperiority, PrimaryCareLimit=@PrimaryCareLimit, " +
                    "HouseholdDemand=@HouseholdDemand, SocialAssessment=@SocialAssessment, Remark6=@Remark6, " +
                    "VisitPhotoText1=@VisitPhotoText1, VisitPhotoText2=@VisitPhotoText2, VisitPhotoText3=@VisitPhotoText3, VisitPhotoText4=@VisitPhotoText4, " +
                    "VisitPhotoText5=@VisitPhotoText5, VisitPhotoText6=@VisitPhotoText6, VisitPhotoText7=@VisitPhotoText7, VisitPhotoText8=@VisitPhotoText8, " +
                    "TeachingEnvironment=@TeachingEnvironment, FamilySupport=@FamilySupport, FamilyInteraction=@FamilyInteraction, TeachingDemonstration=@TeachingDemonstration, " +
                    "Interviews=@Interviews, TeachingAnalysis=@TeachingAnalysis, "+ PicUpdatestring +" UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.ID);
                cmd.Parameters.Add("@VisitType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.visitType);
                cmd.Parameters.Add("@Purpose", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.target);
                cmd.Parameters.Add("@VisitStaff", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.viewSocialWork);
                cmd.Parameters.Add("@VisitDateTime", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.viewDate);
                cmd.Parameters.Add("@VisitTimeSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewTime1);
                cmd.Parameters.Add("@VisitTimeUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewTime2);
                cmd.Parameters.Add("@VisitUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewUnit);
                cmd.Parameters.Add("@VisitUnitTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewTel);
                cmd.Parameters.Add("@VisitUnitZip", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.addressZip);
                cmd.Parameters.Add("@VisitUnitCity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.addressCity);
                cmd.Parameters.Add("@VisitUnitOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.address);
                cmd.Parameters.Add("@Participants", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPeople1);
                cmd.Parameters.Add("@ContentRecord", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewContent1);
                cmd.Parameters.Add("@CaseRecord", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewContent2);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewRemark1);
                cmd.Parameters.Add("@VisitPeople", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.viewPeople2);
                cmd.Parameters.Add("@VisitPlace", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPlace);
                cmd.Parameters.Add("@FamilyProfile1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.pedigree);
                cmd.Parameters.Add("@FamilyProfile2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.ecological);
                cmd.Parameters.Add("@TreatmentUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.cureUnit);
                cmd.Parameters.Add("@TreatmentDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.cureDate);
                cmd.Parameters.Add("@TreatmentType1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureType);
                cmd.Parameters.Add("@TreatmentType2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureType1);
                cmd.Parameters.Add("@TreatmentUnit1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.cureUnit1);
                cmd.Parameters.Add("@Frequency1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumber1);
                cmd.Parameters.Add("@FrequencyTime1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumberTime1);
                cmd.Parameters.Add("@TreatmentType3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureType2);
                cmd.Parameters.Add("@TreatmentUnit2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.cureUnit2);
                cmd.Parameters.Add("@Frequency2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumber2);
                cmd.Parameters.Add("@FrequencyTime2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.cureNumberTime2);
                cmd.Parameters.Add("@NurserySchool", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.preSchool);
                cmd.Parameters.Add("@AttendedDate1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studyDate1);
                cmd.Parameters.Add("@AttendedDate2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studyDate2);
                cmd.Parameters.Add("@AttendedType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studyType);
                cmd.Parameters.Add("@FamilyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.familyType);
                cmd.Parameters.Add("@FamilyTypeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyTypeText);
                cmd.Parameters.Add("@PrimaryCaregiver", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.careProple);
                cmd.Parameters.Add("@PrimaryCaregiverText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.carePropleText);
                cmd.Parameters.Add("@PrimaryCareTime", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.careTime);
                cmd.Parameters.Add("@HomeOwnership", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.homeOwnership);
                cmd.Parameters.Add("@HomeEnvironment", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.homeAround);
                cmd.Parameters.Add("@HomeEnvironmentText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeAroundText);
                cmd.Parameters.Add("@HomeType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.homeType);
                cmd.Parameters.Add("@HomeTypeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeTypeText);
                cmd.Parameters.Add("@TransportType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transportType);
                cmd.Parameters.Add("@LeisurePlace", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.leisureType);
                cmd.Parameters.Add("@LeisurePlaceText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.leisureTypeText);
                cmd.Parameters.Add("@Remark2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation2);
                cmd.Parameters.Add("@HomeSpace1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeSpace1);
                cmd.Parameters.Add("@HomeSpace2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.homeSpace2);
                cmd.Parameters.Add("@Furniture", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.furniture);
                cmd.Parameters.Add("@Electric", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.electric);
                cmd.Parameters.Add("@DangerPlace", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.place);
                cmd.Parameters.Add("@Hydro", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.hydro);
                cmd.Parameters.Add("@GarbagePlace", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.dining1);
                cmd.Parameters.Add("@KitchenHealth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.dining2);
                cmd.Parameters.Add("@VentilateRoom", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor1);
                cmd.Parameters.Add("@AdequateLight", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor2);
                cmd.Parameters.Add("@SpaceSpaciou", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor3);
                cmd.Parameters.Add("@SoundGood", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.indoor4);
                cmd.Parameters.Add("@Remark3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation3);
                cmd.Parameters.Add("@EconomicResource", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal1);
                cmd.Parameters.Add("@PolicyDecision", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal2);
                cmd.Parameters.Add("@Teaching", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal3);
                cmd.Parameters.Add("@CareResources", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal4);
                cmd.Parameters.Add("@EmotionalSupport", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal5);
                cmd.Parameters.Add("@Problem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal6);
                cmd.Parameters.Add("@ResourceUse", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal7);
                cmd.Parameters.Add("@ResourceUseText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.informal7Text);
                cmd.Parameters.Add("@Interactions", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal8);
                cmd.Parameters.Add("@InteractionsText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.informal8Text);
                cmd.Parameters.Add("@Assistance", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal9);
                cmd.Parameters.Add("@AssistanceText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.informal9Text);
                cmd.Parameters.Add("@ProblemSolve", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.informal10);
                cmd.Parameters.Add("@Remark4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation4);
                cmd.Parameters.Add("@MedicalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText1);
                cmd.Parameters.Add("@InterventionResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText2);
                cmd.Parameters.Add("@EconomicResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText3);
                cmd.Parameters.Add("@fCareresources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText4);
                cmd.Parameters.Add("@fEmotionalSupport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText5);
                cmd.Parameters.Add("@EducationalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText6);
                cmd.Parameters.Add("@ReligiousResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText7);
                cmd.Parameters.Add("@CulturalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText8);
                cmd.Parameters.Add("@fOther", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formalText9);
                cmd.Parameters.Add("@fAssistance", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.formal1);
                cmd.Parameters.Add("@fAssistanceText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.formal2);
                cmd.Parameters.Add("@Workaround", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.formal3);
                cmd.Parameters.Add("@Remark5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation5);
                cmd.Parameters.Add("@FamilyEconomicResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction1);
                cmd.Parameters.Add("@FamilyMedicalResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction2);
                cmd.Parameters.Add("@FamilyInterventionResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction3);
                cmd.Parameters.Add("@FamilyReligiousResources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction4);
                cmd.Parameters.Add("@FamilySources", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction5);
                cmd.Parameters.Add("@FamilyEmotionalSupport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyFunction6);
                cmd.Parameters.Add("@VisitSituation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewSituation);
                cmd.Parameters.Add("@FamilySuperiority", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyStrengths);
                cmd.Parameters.Add("@FamilyLimit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyLimit);
                cmd.Parameters.Add("@CaseSuperiority", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.caseStrengths);
                cmd.Parameters.Add("@CaseLimit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.caseLimit);
                cmd.Parameters.Add("@PrimaryCareSuperiority", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.primaryCaseStrengths);
                cmd.Parameters.Add("@PrimaryCareLimit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.primaryCaseLimit);
                cmd.Parameters.Add("@HouseholdDemand", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyDemand);
                cmd.Parameters.Add("@SocialAssessment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.assessment);
                cmd.Parameters.Add("@TrafficRoute", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.trafficRoute);
                cmd.Parameters.Add("@Remark6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.explanation6);
                cmd.Parameters.Add("@VisitPhoto1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto1);
                cmd.Parameters.Add("@VisitPhotoText1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText1);
                cmd.Parameters.Add("@VisitPhoto2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto2);
                cmd.Parameters.Add("@VisitPhotoText2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText2);
                cmd.Parameters.Add("@VisitPhoto3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto3);
                cmd.Parameters.Add("@VisitPhotoText3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText3);
                cmd.Parameters.Add("@VisitPhoto4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto4);
                cmd.Parameters.Add("@VisitPhotoText4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText4);
                cmd.Parameters.Add("@VisitPhoto5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto5);
                cmd.Parameters.Add("@VisitPhotoText5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText5);
                cmd.Parameters.Add("@VisitPhoto6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto6);
                cmd.Parameters.Add("@VisitPhotoText6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText6);
                cmd.Parameters.Add("@VisitPhoto7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto7);
                cmd.Parameters.Add("@VisitPhotoText7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText7);
                cmd.Parameters.Add("@VisitPhoto8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhoto8);
                cmd.Parameters.Add("@VisitPhotoText8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewPhotoText8);
                cmd.Parameters.Add("@TeachingEnvironment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.teachingSpace);
                cmd.Parameters.Add("@FamilySupport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familySupport);
                cmd.Parameters.Add("@FamilyInteraction", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.familyInteraction);
                cmd.Parameters.Add("@TeachingDemonstration", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.teachingDemonstration);
                cmd.Parameters.Add("@Interviews", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.viewConclusion);
                cmd.Parameters.Add("@TeachingAnalysis", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.teachingAnalysis);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message;
            }
        }
        return returnValue;
    }


    private string SearchStudentActivityConditionReturn(SearchStudentActivity SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txteventName != null)
        {
            ConditionReturn += " AND actName like @actName ";
        }
        if (SearchStructure.txteventDatestart != null && SearchStructure.txteventDateend != null && SearchStructure.txteventDatestart != DateBase && SearchStructure.txteventDateend != DateBase)
        {
            ConditionReturn += " AND actDate BETWEEN (@sDateStart) AND (@sDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStudentActivityCount(SearchStudentActivity SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentActivityConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM ActivitiesRecords "+
                            "WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@actName", SqlDbType.NVarChar).Value = SearchStructure.txteventName + "%";
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txteventDatestart);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txteventDateend);
                returnValue[0] = cmd.ExecuteScalar().ToString();
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
    public List<SearchStudentActivityResult> SearchStudentActivity(int indexpage, SearchStudentActivity SearchStructure)
    {
        List<SearchStudentActivityResult> returnValue = new List<SearchStudentActivityResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStudentActivityConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY actDate DESC) " +
                 "AS RowNum, * , (SELECT COUNT(*) FROM ActivityParticipation WHERE actID=ActivitiesRecords.actID AND ActivityParticipation.isDeleted=0) AS pCOUNT " +
                 "FROM ActivitiesRecords "+
                 "WHERE ActivitiesRecords.isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@actName", SqlDbType.NVarChar).Value = SearchStructure.txteventName + "%";
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txteventDatestart);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txteventDateend);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchStudentActivityResult addValue = new SearchStudentActivityResult();
                    addValue.ID = dr["actID"].ToString();
                    addValue.txteventName = dr["actName"].ToString();
                    addValue.txteventDate = DateTime.Parse(dr["actDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txttotalNumber = dr["pCOUNT"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchStudentActivityResult addValue = new SearchStudentActivityResult();
                addValue.ID = "-1";
                addValue.txteventName = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] createStudentActivityData(createStudentActivity StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO ActivitiesRecords (Unit, actName, actDate, actresponsible1, actresponsible2, actresponsible3, CreateFileBy, UpFileBy, UpFileDate ) " +
                            "VALUES (@Unit, @actName, @actDate, @actresponsible1, @actresponsible2, @actresponsible3, @CreateFileBy, @UpFileBy, getDate())";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@actDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.eventDate);
                cmd.Parameters.Add("@actName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.eventName);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                for (int i = 1; i <= 3; i++)
                {
                    string staffID = "";
                    if (StudentData.eventStaffList.Count >= i)
                    {
                        staffID = StudentData.eventStaffList[i - 1];
                    }
                    cmd.Parameters.Add("@actresponsible" + i, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(staffID);
                }
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('ActivitiesRecords') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["cID"].ToString());
                    }
                    dr.Close();
                    List<int> itemValue = new List<int>();
                    for (int i = 0; i < StudentData.Participants.Count; i++)
                    {
                        sql = "INSERT INTO ActivityParticipation (actID, StudentID, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                                "VALUES (@actID, @StudentID, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@actID", SqlDbType.Int).Value = Column;
                        cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.Participants[i][1]);
                        cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                        cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                        itemValue.Add(cmd.ExecuteNonQuery());
                    }
                    returnValue[0] = Column.ToString();
                    returnValue[1] = Convert.ToInt32(!itemValue.Contains(0)).ToString();
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
    public string[] setStudentActivityData(createStudentActivity StudentData, List<string> DelParticipantsID, List<string> NewParticipantsValue)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE ActivitiesRecords SET actName=@actName, actDate=@actDate, actresponsible1=@actresponsible1, actresponsible2=@actresponsible2, "+
                            "actresponsible3=@actresponsible3, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                            "WHERE actID=@actID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@actID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.ID);
                cmd.Parameters.Add("@actDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.eventDate);
                cmd.Parameters.Add("@actName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.eventName);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                for (int i = 1; i <= 3; i++)
                {
                    string staffID = "";
                    if (StudentData.eventStaffList.Count >= i)
                    {
                        staffID = StudentData.eventStaffList[i - 1];
                    }
                    cmd.Parameters.Add("@actresponsible" + i, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(staffID);
                }
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (DelParticipantsID.Count > 0)
                {
                    for (int i = 0; i < DelParticipantsID.Count; i++)
                    {
                        sql = "UPDATE ActivityParticipation SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                                "WHERE ID=@ID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(DelParticipantsID[i]);
                        cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                        cmd.ExecuteNonQuery().ToString();
                    }
                }
                if (NewParticipantsValue.Count > 0)
                {
                    for (int i = 0; i < NewParticipantsValue.Count; i++)
                    {
                        string ParticipationID = "";
                        sql = "SELECT ID FROM ActivityParticipation WHERE isDeleted=1 AND actID=@actID AND StudentID=@StudentID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@actID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.ID);
                        cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewParticipantsValue[i]);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            ParticipationID = dr["ID"].ToString();
                        }
                        dr.Close();
                        if (ParticipationID.Length > 0)
                        {
                            sql = "UPDATE ActivityParticipation SET isDeleted=0, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                                  "WHERE ID=@ID";
                            cmd = new SqlCommand(sql, Sqlconn);
                            cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ParticipationID);
                            cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            cmd.ExecuteNonQuery().ToString();
                        }
                        else
                        {
                            sql = "INSERT INTO ActivityParticipation (actID, StudentID, CreateFileBy, UpFileBy, UpFileDate ) " +
                                    "VALUES (@actID, @StudentID, @CreateFileBy, @UpFileBy, getDate())";
                            cmd = new SqlCommand(sql, Sqlconn);
                            cmd.Parameters.Add("@actID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.ID);
                            cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewParticipantsValue[i]);
                            cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            cmd.ExecuteNonQuery().ToString();
                        }
                    }
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
    public createStudentActivity getStudentActivityData(string ID)
    {
        createStudentActivity returnValue = new createStudentActivity();
        returnValue.eventStaffList = new List<string>();
        returnValue.Participants = new List<string[]>();
        StaffDataBase sDB = new StaffDataBase();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM ActivitiesRecords WHERE isDeleted=0 AND actID=@actID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@actID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["actID"].ToString();
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.eventName = dr["actName"].ToString();
                    returnValue.eventDate = DateTime.Parse(dr["actDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.eventStaffList.Add(dr["actresponsible1"].ToString());
                    returnValue.eventStaffList.Add(dr["actresponsible2"].ToString());
                    returnValue.eventStaffList.Add(dr["actresponsible3"].ToString());
                    List<string> CreateFileName = sDB.getStaffDataName(dr["CreateFileBy"].ToString());
                    returnValue.creatFileName = CreateFileName[1];
                }
                dr.Close();

                sql = "SELECT a.ID,a.StudentID,b.StudentName FROM ActivityParticipation a left join StudentDatabase b on a.StudentID=b.StudentID " +
                    "WHERE a.isDeleted=0 AND actID=@actID";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@actID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] addValue = new string[3];
                    addValue[0] = dr["ID"].ToString();
                    addValue[1] = dr["StudentID"].ToString();
                    addValue[2] = dr["StudentName"].ToString();
                    returnValue.Participants.Add(addValue);

                }
                dr.Close();

                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message;
            }
        }
        return returnValue;
    }

     public string[] createStudentTrans(CreateStudentTrans StudentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO CaseTransitionService (Unit, StudentID, Age, AgeMonth, TurnYear, TurnStage, BeforeSchool, AfterSchool, PrimaryContact, "+
                    "PrimaryContactTel, Events, EventsNam1, EventsDate1, EventsContent1, EventsNam2, EventsDate2, EventsContent2, EventsNam3, EventsDate3, EventsContent3, " +
                    "EventsNam4, EventsDate4, EventsContent4, ResettlementMessage, SendDocument, Report, TurnReport, FamilyReport, ParticipateMeeting, " +
                    "ParticipateMeetingRecord, TrackCaseStatu, TrackCaseStatuRecord, SchoolVisit, CreateFileBy, UpFileBy, UpFileDate) " +
                    "VALUES(@Unit, @StudentID, @Age, @AgeMonth, @TurnYear, @TurnStage, @BeforeSchool, @AfterSchool, @PrimaryContact, @PrimaryContactTel, @Events, @EventsNam1, " +
                    "@EventsDate1, @EventsContent1, @EventsNam2, @EventsDate2, @EventsContent2, @EventsNam3, @EventsDate3, @EventsContent3, @EventsNam4, @EventsDate4, " +
                    "@EventsContent4, @ResettlementMessage, @SendDocument, @Report, @TurnReport, @FamilyReport, @ParticipateMeeting, @ParticipateMeetingRecord, " +
                    "@TrackCaseStatu, @TrackCaseStatuRecord, @SchoolVisit, @CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.studentID);
                cmd.Parameters.Add("@Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studentAge);
                cmd.Parameters.Add("@AgeMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studentMonth);
                cmd.Parameters.Add("@TurnYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transYear);
                cmd.Parameters.Add("@TurnStage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transStage);
                cmd.Parameters.Add("@BeforeSchool", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.bSchool);
                cmd.Parameters.Add("@AfterSchool", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.aSchool);
                cmd.Parameters.Add("@PrimaryContact", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.contact);
                cmd.Parameters.Add("@PrimaryContactTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.contactTel);
                cmd.Parameters.Add("@Events", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transActivity);
                cmd.Parameters.Add("@EventsNam1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName1);
                cmd.Parameters.Add("@EventsDate1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate1);
                cmd.Parameters.Add("@EventsContent1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent1);
                cmd.Parameters.Add("@EventsNam2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName2);
                cmd.Parameters.Add("@EventsDate2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate2);
                cmd.Parameters.Add("@EventsContent2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent2);
                cmd.Parameters.Add("@EventsNam3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName3);
                cmd.Parameters.Add("@EventsDate3", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate3);
                cmd.Parameters.Add("@EventsContent3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent3);
                cmd.Parameters.Add("@EventsNam4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName4);
                cmd.Parameters.Add("@EventsDate4", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate4);
                cmd.Parameters.Add("@EventsContent4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent4);
                cmd.Parameters.Add("@ResettlementMessage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.antonMessage);
                cmd.Parameters.Add("@SendDocument", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.sendDocument);
                cmd.Parameters.Add("@Report", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transReport);
                cmd.Parameters.Add("@TurnReport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transReportFile);
                cmd.Parameters.Add("@FamilyReport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transFamilyReportFile);
                cmd.Parameters.Add("@ParticipateMeeting", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.meeting);
                cmd.Parameters.Add("@ParticipateMeetingRecord", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.meetingVisitReport);
                cmd.Parameters.Add("@TrackCaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.adaptation);
                cmd.Parameters.Add("@TrackCaseStatuRecord", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.adaptationReport);
                cmd.Parameters.Add("@SchoolVisit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.schoolVisit);
                cmd.Parameters.Add("@SchoolVisitRecord", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.schoolVisitRecord);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('CaseTransitionService') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
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

     public string[] setStudentTrans(CreateStudentTrans StudentData) {
         string[] returnValue = new string[2];
         returnValue[0] = "0";
         returnValue[1] = "";
         string FileUpdatestring = "";
         if (StudentData.transReportFile != null) {
             FileUpdatestring += " TurnReport=@TurnReport, ";
         }
         if (StudentData.transFamilyReportFile != null)
         {
             FileUpdatestring += " FamilyReport=@FamilyReport, ";
         }
         if (StudentData.schooladvocacyReport != null)
         {
             FileUpdatestring += " SchoolAdvocacyReport=@SchoolAdvocacyReport,  ";
         }
         DataBase Base = new DataBase();
         using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
         {
             try
             {
                 StaffDataBase sDB = new StaffDataBase();
                 List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                 Sqlconn.Open();
                 string sql = "UPDATE CaseTransitionService SET Age=@Age, AgeMonth=@AgeMonth, TurnYear=@TurnYear, TurnStage=@TurnStage, BeforeSchool=@BeforeSchool, " +
                     "AfterSchool=@AfterSchool, PrimaryContact=@PrimaryContact, PrimaryContactTel=@PrimaryContactTel, Events=@Events, EventsNam1=@EventsNam1, " +
                     "EventsDate1=@EventsDate1, EventsContent1=@EventsContent1, EventsNam2=@EventsNam2, EventsDate2=@EventsDate2, EventsContent2=@EventsContent2, " +
                     "EventsNam3=@EventsNam3, EventsDate3=@EventsDate3, EventsContent3=@EventsContent3, EventsNam4=@EventsNam4, EventsDate4=@EventsDate4, " +
                     "EventsContent4=@EventsContent4, ResettlementMessage=@ResettlementMessage, SendDocument=@SendDocument, Report=@Report, " +
                     "ParticipateMeeting=@ParticipateMeeting, ParticipateMeetingRecord=@ParticipateMeetingRecord, TrackCaseStatu=@TrackCaseStatu, " +
                     "TrackCaseStatuRecord=@TrackCaseStatuRecord, SchoolVisit=@SchoolVisit, SchoolVisitRecord=@SchoolVisitRecord, " +
                     FileUpdatestring + "UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                     "WHERE ID=@ID";
                 SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                 cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.ID);
                 cmd.Parameters.Add("@Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studentAge);
                 cmd.Parameters.Add("@AgeMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studentMonth);
                 cmd.Parameters.Add("@TurnYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transYear);
                 cmd.Parameters.Add("@TurnStage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transStage);
                 cmd.Parameters.Add("@BeforeSchool", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.bSchool);
                 cmd.Parameters.Add("@AfterSchool", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.aSchool);
                 cmd.Parameters.Add("@PrimaryContact", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.contact);
                 cmd.Parameters.Add("@PrimaryContactTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.contactTel);
                 cmd.Parameters.Add("@Events", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transActivity);
                 cmd.Parameters.Add("@EventsNam1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName1);
                 cmd.Parameters.Add("@EventsDate1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate1);
                 cmd.Parameters.Add("@EventsContent1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent1);
                 cmd.Parameters.Add("@EventsNam2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName2);
                 cmd.Parameters.Add("@EventsDate2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate2);
                 cmd.Parameters.Add("@EventsContent2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent2);
                 cmd.Parameters.Add("@EventsNam3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName3);
                 cmd.Parameters.Add("@EventsDate3", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate3);
                 cmd.Parameters.Add("@EventsContent3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent3);
                 cmd.Parameters.Add("@EventsNam4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityName4);
                 cmd.Parameters.Add("@EventsDate4", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.transActivityDate4);
                 cmd.Parameters.Add("@EventsContent4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transActivityContent4);
                 cmd.Parameters.Add("@ResettlementMessage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.antonMessage);
                 cmd.Parameters.Add("@SendDocument", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.sendDocument);
                 cmd.Parameters.Add("@Report", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.transReport);
                 cmd.Parameters.Add("@TurnReport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transReportFile);
                 cmd.Parameters.Add("@FamilyReport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.transFamilyReportFile);
                 cmd.Parameters.Add("@ParticipateMeeting", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.meeting);
                 cmd.Parameters.Add("@ParticipateMeetingRecord", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.meetingVisitReport);
                 cmd.Parameters.Add("@TrackCaseStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.adaptation);
                 cmd.Parameters.Add("@TrackCaseStatuRecord", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.adaptationReport);
                 cmd.Parameters.Add("@SchoolVisit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.schoolVisit);
                 cmd.Parameters.Add("@SchoolVisitRecord", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.schoolVisitRecord);
                 cmd.Parameters.Add("@SchoolAdvocacyReport", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.schooladvocacyReport);
                 cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                 cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                 returnValue[0] = cmd.ExecuteNonQuery().ToString();
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

     public CreateStudentTrans getStudentTrans(string ID)
     {
        CreateStudentTrans returnValue = new CreateStudentTrans();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StudentDatabase.StudentName, CaseTransitionService.* " +
                             "FROM CaseTransitionService INNER JOIN StudentDatabase ON CaseTransitionService.StudentID=StudentDatabase.StudentID " +
                             "WHERE CaseTransitionService.isDeleted=0 AND CaseTransitionService.ID=@ID"; 
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentAge = dr["Age"].ToString();
                    returnValue.studentMonth = dr["AgeMonth"].ToString();
                    returnValue.transYear = dr["TurnYear"].ToString();
                    returnValue.transStage = dr["TurnStage"].ToString();
                    returnValue.bSchool = dr["BeforeSchool"].ToString();
                    returnValue.aSchool = dr["AfterSchool"].ToString();
                    returnValue.contact = dr["PrimaryContact"].ToString();
                    returnValue.contactTel = dr["PrimaryContactTel"].ToString();
                    returnValue.transActivity = dr["Events"].ToString();
                    returnValue.transActivityName1 = dr["EventsNam1"].ToString();
                    returnValue.transActivityDate1 = DateTime.Parse(dr["EventsDate1"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.transActivityContent1 = dr["EventsContent1"].ToString();
                    returnValue.transActivityName2 = dr["EventsNam2"].ToString();
                    returnValue.transActivityDate2 = DateTime.Parse(dr["EventsDate2"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.transActivityContent2 = dr["EventsContent2"].ToString();
                    returnValue.transActivityName3 = dr["EventsNam3"].ToString();
                    returnValue.transActivityDate3 = DateTime.Parse(dr["EventsDate3"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.transActivityContent3 = dr["EventsContent3"].ToString();
                    returnValue.transActivityName4 = dr["EventsNam4"].ToString();
                    returnValue.transActivityDate4 = DateTime.Parse(dr["EventsDate4"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.transActivityContent4 = dr["EventsContent4"].ToString();
                    returnValue.antonMessage = dr["ResettlementMessage"].ToString();
                    returnValue.sendDocument = dr["SendDocument"].ToString();
                    returnValue.transReport = dr["Report"].ToString();
                    returnValue.transReportFile = dr["TurnReport"].ToString();
                    returnValue.transFamilyReportFile = dr["FamilyReport"].ToString();
                    returnValue.meeting = dr["ParticipateMeeting"].ToString();
                    returnValue.meetingVisitReport = dr["ParticipateMeetingRecord"].ToString();
                    returnValue.adaptation = dr["TrackCaseStatu"].ToString();
                    returnValue.adaptationReport = dr["TrackCaseStatuRecord"].ToString();
                    returnValue.schoolVisit = dr["SchoolVisit"].ToString();
                    returnValue.schooladvocacyReport = dr["SchoolAdvocacyReport"].ToString();
                    returnValue.schoolVisitRecord = dr["SchoolVisitRecord"].ToString();
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue.checkNo = "-1";
                returnValue.errorMsg = e.Message.ToString();
            }

        }
        return returnValue;
     }

     private string SearchStudentTransConditionReturn(SearchTransRecord SearchStructure)
     {
         string ConditionReturn = "";
         if (SearchStructure.txtstudentName != null)
         {
             ConditionReturn += " AND StudentDatabase.StudentName like (@StudentName) ";
         }
         if (SearchStructure.txttransYear != null)
         {
             ConditionReturn += " AND TurnYear=@TurnYear";
         }
         if (SearchStructure.txttransActivity != null)
         {
             ConditionReturn += " AND Events=@Events";
         }
         if (SearchStructure.txtmeeting != null)
         {
             ConditionReturn += " AND ParticipateMeeting=@ParticipateMeeting";
         }
         if (SearchStructure.txtschoolVisit != null)
         {
             ConditionReturn += " AND SchoolVisit=@SchoolVisit";
         }
         StaffDataBase sDB = new StaffDataBase();
         List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
         if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
         {
             ConditionReturn += " AND CaseTransitionService.Unit =" + UserFile[2] + " ";
         }
         return ConditionReturn;
     }
     public string[] SearchStudentTransCount(SearchTransRecord SearchStructure) {

         string[] returnValue = new string[2];
         returnValue[0] = "0";
         returnValue[1] = "0";
         DataBase Base = new DataBase();
         string ConditionReturn = this.SearchStudentTransConditionReturn(SearchStructure);
         using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
         {
             try
             {
                 Sqlconn.Open();
                 string sql = "SELECT COUNT(*) AS QCOUNT FROM CaseTransitionService " +
                            "INNER JOIN StudentDatabase ON CaseTransitionService.StudentID=StudentDatabase.StudentID " +
                             "WHERE CaseTransitionService.isDeleted=0 " + ConditionReturn;
                 SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                 cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                 cmd.Parameters.Add("@TurnYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txttransYear);
                 cmd.Parameters.Add("@Events", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txttransActivity);
                 cmd.Parameters.Add("@ParticipateMeeting", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtmeeting);
                 cmd.Parameters.Add("@SchoolVisit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtschoolVisit);
                 
                 returnValue[0] = cmd.ExecuteScalar().ToString();
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
     public List<SearchTransRecordResult> SearchStudentTrans(int indexpage, SearchTransRecord SearchStructure)
     {
         List<SearchTransRecordResult> returnValue = new List<SearchTransRecordResult>();
         DataBase Base = new DataBase();
         string ConditionReturn = this.SearchStudentTransConditionReturn(SearchStructure);
         using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
         {
             try
             {
                 Sqlconn.Open();
                 string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY CaseTransitionService.ID DESC) " +
                  "AS RowNum, .CaseTransitionService.* ,StudentDatabase.StudentName " +
                  "FROM CaseTransitionService " +
                  "INNER JOIN StudentDatabase ON CaseTransitionService.StudentID=StudentDatabase.StudentID " +
                  "WHERE CaseTransitionService.isDeleted=0 " + ConditionReturn + " ) " +
                  "AS NewTable " +
                  "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                 SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                 cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                 cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                 cmd.Parameters.Add("@TurnYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txttransYear);
                 cmd.Parameters.Add("@Events", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txttransActivity);
                 cmd.Parameters.Add("@ParticipateMeeting", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtmeeting);
                 cmd.Parameters.Add("@SchoolVisit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtschoolVisit);
                 SqlDataReader dr = cmd.ExecuteReader();
                 while (dr.Read())
                 {
                     SearchTransRecordResult addValue = new SearchTransRecordResult();
                     addValue.ID = dr["ID"].ToString();
                     addValue.txtstudentName = dr["StudentName"].ToString();
                     addValue.txtstudentAge = dr["Age"].ToString();
                     addValue.txtstudentMonth = dr["AgeMonth"].ToString();
                     addValue.txttransStage = dr["TurnStage"].ToString();
                     addValue.txttransYear = dr["TurnYear"].ToString();
                     returnValue.Add(addValue);
                 }
                 Sqlconn.Close();
             }
             catch (Exception e)
             {
                 SearchTransRecordResult addValue = new SearchTransRecordResult();
                 addValue.checkNo = "-1";
                 addValue.errorMsg = e.Message;
                 returnValue.Add(addValue);
             }
         }
         return returnValue;
     }

}
