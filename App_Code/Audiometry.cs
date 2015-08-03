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

public struct AADataList
{
    public Int64 aID;
    public int aRowNum;
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
    public string aAssessName1;
    public string aAssessName2;
    public int aUnit;
}


/// <summary>
/// Summary description for Audiometry
/// </summary>
public class Audiometry
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
    public string[] _StaffhaveRoles = new string[5] { "0", "0", "0", "0", "0" };
	public Audiometry()
	{
		//
		// TODO: Add constructor logic here
		//
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.hearing[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.hearing[1];//跨區與否
	}

    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }
    public string[] ComparisonAidsData(StudentDataBasic DateItem)
    {
        string[] returnValue = new string[2];

        AspAjax asp = new AspAjax();
        try
        {
            StudentDataBasic BasicData = asp.getStudentAidsDataBaseBasic(DateItem.studentID);
            if (DateItem.assistmanageR != BasicData.assistmanageR || DateItem.brandR != BasicData.brandR || DateItem.modelR != BasicData.modelR || DateItem.buyingPlaceR != BasicData.buyingPlaceR || DateItem.buyingtimeR != BasicData.buyingtimeR || DateItem.insertHospitalR != BasicData.insertHospitalR || DateItem.openHzDateR != BasicData.openHzDateR || DateItem.assistmanageL != BasicData.assistmanageL || DateItem.brandL != BasicData.brandL || DateItem.modelL != BasicData.modelL || DateItem.buyingtimeL != BasicData.buyingtimeL || DateItem.buyingPlaceL != BasicData.buyingPlaceL || DateItem.insertHospitalL != BasicData.insertHospitalL || DateItem.openHzDateL != BasicData.openHzDateL)
            {
                CreateStudentAidsUse AidsUseData = new CreateStudentAidsUse();
                AidsUseData.studentID = DateItem.studentID;
                AidsUseData.assistmanageR = DateItem.assistmanageR;
                AidsUseData.brandR = DateItem.brandR;
                AidsUseData.modelR = DateItem.modelR;
                AidsUseData.buyingPlaceR = DateItem.buyingPlaceR;
                AidsUseData.buyingtimeR = DateItem.buyingtimeR;
                AidsUseData.insertHospitalR = DateItem.insertHospitalR;
                AidsUseData.openHzDateR = DateItem.openHzDateR;
                AidsUseData.assistmanageL = DateItem.assistmanageL;
                AidsUseData.brandL = DateItem.brandL;
                AidsUseData.modelL = DateItem.modelL;
                AidsUseData.buyingtimeL = DateItem.buyingtimeL;
                AidsUseData.buyingPlaceL = DateItem.buyingPlaceL;
                AidsUseData.insertHospitalL = DateItem.insertHospitalL;
                AidsUseData.openHzDateL = DateItem.openHzDateL;

                
                string[] item=this.createStudentAidsUse(AidsUseData);
                DateItem.ID = BasicData.ID;
                returnValue = this.setStudentDataHearingInformation(DateItem);
            }
        }
        catch (Exception e)
        {
            returnValue[0] = "-1";
            returnValue[1] = e.Message.ToString();
        }
        return returnValue;
    }
    private string[] setStudentDataHearingInformation(StudentDataBasic StudentHearing)
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
                string sql = "UPDATE StudentDatabase SET HearingAids_R=@HearingAids_R, AidsBrand_R=@AidsBrand_R, AidsModel_R=@AidsModel_R, AidsOptionalTime_R=@AidsOptionalTime_R, " +
                  "AidsOptionalLocation_R=@AidsOptionalLocation_R, EEarHospital_R=@EEarHospital_R,EEarOpen_R=@EEarOpen_R, " +
                  "HearingAids_L=@HearingAids_L, AidsBrand_L=@AidsBrand_L, AidsModel_L=@AidsModel_L, AidsOptionalTime_L=@AidsOptionalTime_L, AidsOptionalLocation_L=@AidsOptionalLocation_L, " +
                  "EEarHospital_L=@EEarHospital_L, EEarOpen_L=@EEarOpen_L " +
                  "WHERE ID=@ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.ID);
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
    public string[] createAudiometryAppointment(CreateAudiometryAppointment StructData)
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
                string sql = "INSERT INTO AudiometryAppointment (ReserveDate, StartTime, EndTime, ReserveID, StudentID, CheckItem, ItemOtherExplain, CheckContent, State, AssessWho1, " +
                    "AssessWho2, Unit)" +
                    "VALUES (@ReserveDate, @StartTime, @EndTime, @ReserveID, @StudentID, @CheckItem, @ItemOtherExplain, @CheckContent, @State, @AssessWho1, @AssessWho2, " +
                    "@Unit)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ReserveDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.appDate);
                cmd.Parameters.Add("@StartTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.startTime);
                cmd.Parameters.Add("@EndTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.endTime);
                cmd.Parameters.Add("@ReserveID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.authorID);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.studentID);
                cmd.Parameters.Add("@CheckItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.item);
                cmd.Parameters.Add("@ItemOtherExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.itemExplain);
                cmd.Parameters.Add("@CheckContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.sContent);
                cmd.Parameters.Add("@State", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.State);
                cmd.Parameters.Add("@AssessWho1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.AssessWho1);
                cmd.Parameters.Add("@AssessWho2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.AssessWho2);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.Unit);
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

    public string[] setAudiometryAppointment(CreateAudiometryAppointment StructData)
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
                /*string sql = "INSERT INTO AudiometryAppointment (ReserveDate, StartTime, EndTime, ReserveID, StudentID, CheckItem, ItemOtherExplain, CheckContent, State, AssessWho1, " +
                    "AssessWho2, Unit, CreateDateTime, isDeleted)" +
                    "VALUES (@ReserveDate, @StartTime, @EndTime, @ReserveID, @StudentID, @CheckItem, @ItemOtherExplain, @CheckContent, @State, @AssessWho1, @AssessWho2, " +
                    "@Unit, @CreateDateTime, 0)";
                */
                //sql = "UPDATE CoursePlanTemplateLong SET TargetContent=@TargetContent WHERE CPTLID=@CPTLID AND CPTID=@CPTID AND isDeleted=0";

                string sql = "UPDATE AudiometryAppointment SET ReserveDate=@ReserveDate, StartTime=@StartTime, EndTime=@EndTime, "+
                    "StudentID=@StudentID, CheckItem=@CheckItem, ItemOtherExplain=@ItemOtherExplain, CheckContent=@CheckContent, State=@State, "+
                    "AssessWho1=@AssessWho1, AssessWho2=@AssessWho2, Unit=@Unit WHERE ID=@ID AND isDeleted=0";
                    //"AssessWho1=@AssessWho1, AssessWho2=@AssessWho2, Unit=@Unit WHERE ID=@ID AND ReserveID=@ReserveID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StructData.ID);
                cmd.Parameters.Add("@ReserveDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.appDate);
                cmd.Parameters.Add("@StartTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.startTime);
                cmd.Parameters.Add("@EndTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.endTime);
                cmd.Parameters.Add("@ReserveID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.authorID);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.studentID);
                cmd.Parameters.Add("@CheckItem", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.item);
                cmd.Parameters.Add("@ItemOtherExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.itemExplain);
                cmd.Parameters.Add("@CheckContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.sContent);
                cmd.Parameters.Add("@State", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.State);
                cmd.Parameters.Add("@AssessWho1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.AssessWho1);
                cmd.Parameters.Add("@AssessWho2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.AssessWho2);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.Unit);
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

    public string[] delAudiometryAppointment(Int64 eventID)
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
                string sql = "UPDATE AudiometryAppointment SET isDeleted=1 WHERE ID=@ID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = eventID;
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

    public string[] SearchAudiometryAppointmentDataBaseCount(SearchAudiometryAppointment SearchAudiometryAppointmentData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAudiometryAppointmentConditionReturn(SearchAudiometryAppointmentData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM AudiometryAppointment " +
                             "RIGHT JOIN StudentDatabase AS s1 ON AudiometryAppointment.StudentID=s1.StudentID AND s1.isDeleted=0 " +
                             "RIGHT JOIN StaffDatabase AS s2 ON AudiometryAppointment.ReserveID=s2.StaffID AND s2.isDeleted=0 " +
                             "WHERE AudiometryAppointment.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtstudentName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchAudiometryAppointmentData.txtstudentName) + "%";
                cmd.Parameters.Add("@txtstudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchAudiometryAppointmentData.txtstudentID);
                cmd.Parameters.Add("@txtstudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchAudiometryAppointmentData.txtstudentSex);
                cmd.Parameters.Add("@txtstudentDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtstudentDateStart);
                cmd.Parameters.Add("@txtstudentDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtstudentDateEnd);
                cmd.Parameters.Add("@txtcheckDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtcheckDateStart);
                cmd.Parameters.Add("@txtcheckDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtcheckDateEnd);
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
    public List<AADataList> SearchAudiometryAppointmentDataBase(int indexpage, SearchAudiometryAppointment SearchAudiometryAppointmentData)
    {
        List<AADataList> returnValue = new List<AADataList>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAudiometryAppointmentConditionReturn(SearchAudiometryAppointmentData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY AudiometryAppointment.ID DESC) " +
                             "AS RowNum, AudiometryAppointment.*,s1.StudentName,s1.CaseStatu,s2.StaffName AS ReserveName,"+
                             "s3.StaffName AS AssessName1,s4.StaffName AS AssessName2 FROM AudiometryAppointment " +
                             "RIGHT JOIN StudentDatabase AS s1 ON AudiometryAppointment.StudentID=s1.StudentID AND s1.isDeleted=0 " +
                             "RIGHT JOIN StaffDatabase AS s2 ON AudiometryAppointment.ReserveID=s2.StaffID AND s2.isDeleted=0 " +
                             "LEFT JOIN StaffDatabase AS s3 ON AudiometryAppointment.AssessWho1=s3.StaffID AND s3.isDeleted=0 " +
                             "LEFT JOIN StaffDatabase AS s4 ON AudiometryAppointment.AssessWho2=s4.StaffID AND s4.isDeleted=0 " +
                             "WHERE AudiometryAppointment.isDeleted=0 AND AudiometryAppointment.State=1 " + ConditionReturn + " ) AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtstudentName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchAudiometryAppointmentData.txtstudentName) + "%";
                cmd.Parameters.Add("@txtstudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchAudiometryAppointmentData.txtstudentID);
                cmd.Parameters.Add("@txtstudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchAudiometryAppointmentData.txtstudentSex);
                cmd.Parameters.Add("@txtstudentDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtstudentDateStart);
                cmd.Parameters.Add("@txtstudentDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtstudentDateEnd);
                cmd.Parameters.Add("@txtcheckDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtcheckDateStart);
                cmd.Parameters.Add("@txtcheckDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchAudiometryAppointmentData.txtcheckDateEnd);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AADataList addValue = new AADataList();
                    addValue.aRowNum = int.Parse(dr["RowNum"].ToString());
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
                    addValue.aAssessName1 = dr["AssessName1"].ToString();
                    addValue.aAssessName2 = dr["AssessName2"].ToString();
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

    private string SearchAudiometryAppointmentConditionReturn(SearchAudiometryAppointment SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND s1.StudentID = (@txtstudentID) ";
        }
        if (SearchStructure.txtstudentSex != null)
        {
            ConditionReturn += " AND s1.StudentSex = (@txtstudentSex) ";
        }
        if (SearchStructure.txtstudentDateStart != null && SearchStructure.txtstudentDateEnd != null && SearchStructure.txtstudentDateStart != DateBase && SearchStructure.txtstudentDateEnd != DateBase)
        {
            ConditionReturn += " AND s1.StudentBirthday BETWEEN (@txtstudentDateStart) AND (@txtstudentDateEnd) ";
        }
        if (SearchStructure.txtcheckDateStart != null && SearchStructure.txtcheckDateEnd != null && SearchStructure.txtcheckDateStart != DateBase && SearchStructure.txtcheckDateEnd != DateBase)
        {
            ConditionReturn += " AND AudiometryAppointment.ReserveDate BETWEEN (@txtcheckDateStart) AND (@txtcheckDateEnd) ";
        }
        return ConditionReturn;
    }

    public string[] setAudiometryAppointmentContent(SaveAudiometryAppointmentContent AaSystemData)
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
                string sql = "UPDATE AudiometryAppointment SET CheckContent=@aContent WHERE ID=@aID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@aID", SqlDbType.BigInt).Value = AaSystemData.aID;
                cmd.Parameters.Add("@aContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(AaSystemData.aContent);
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

    public string[] createHearingAssessmentData(CreateHearingAssessment StructData) {
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
                string sql = "INSERT INTO HearingAssessment (Unit, CheckDate, StudentID, Age, AgeMonth, UseAids, HearingAids_R, AidsBrand_R, AidsModel_R, AidsOptionalTime_R, "+
                    "AidsOptionalLocation_R, EEarHospital_R, EEarOpen_R, HearingAids_L, AidsBrand_L, AidsModel_L, AidsOptionalTime_L, AidsOptionalLocation_L, EEarHospital_L, "+
                    "EEarOpen_L,  Purpose, PurposeText, PurposeExplain, Observation, ObservationExplain, RightEar, LeftEar, RightDecibel, RightDegree, " +
                    "RightExplain, LeftDecibel, LeftDegree, LeftExplain, TestResult, AssessRAids, AssessRExplain, AssessLAids, AssessLExplain, VerificationRAids, " +
                    "VerificationRExplain, VerificationLAids, VerificationLExplain, OtherAssess, OtherAssessExplain, ManageSuggestion, ManageSuggestionHour, OperateSuggestion, " +
                    "suggestion, suggestionExplain, NextDate, CreateFileBy, UpFileBy, UpFileDate) " +
                    "VALUES(@Unit, @CheckDate, @StudentID, @Age, @AgeMonth, @UseAids, @HearingAids_R, @AidsBrand_R, @AidsModel_R, @AidsOptionalTime_R, @AidsOptionalLocation_R, "+
                    "@EEarHospital_R, @EEarOpen_R, @HearingAids_L, @AidsBrand_L, @AidsModel_L, @AidsOptionalTime_L, @AidsOptionalLocation_L, @EEarHospital_L, @EEarOpen_L, "+
                    "@Purpose, @PurposeText, @PurposeExplain, @Observation, @ObservationExplain, @RightEar, @LeftEar, @RightDecibel, @RightDegree, @RightExplain, " +
                    "@LeftDecibel, @LeftDegree, @LeftExplain, @TestResult, @AssessRAids, @AssessRExplain, @AssessLAids, @AssessLExplain, @VerificationRAids, " +
                    "@VerificationRExplain, @VerificationLAids, @VerificationLExplain, @OtherAssess, @OtherAssessExplain, @ManageSuggestion, @ManageSuggestionHour, " +
                    "@OperateSuggestion, @suggestion, @suggestionExplain, @NextDate, @CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@CheckDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.checkDate);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.studentID);
                cmd.Parameters.Add("@Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.studentAge);
                cmd.Parameters.Add("@AgeMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.studentMonth);
                cmd.Parameters.Add("@UseAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.studentUseAids);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateL);
                cmd.Parameters.Add("@Purpose", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.detectionPurposes);
                cmd.Parameters.Add("@PurposeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.detectionPurposesText);
                cmd.Parameters.Add("@PurposeExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.explain);
                cmd.Parameters.Add("@Observation", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.observation);
                cmd.Parameters.Add("@ObservationExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.observationExplain);
                cmd.Parameters.Add("@RightEar", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkR);
                cmd.Parameters.Add("@LeftEar", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkL);
                cmd.Parameters.Add("@RightDecibel", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.checkRecibelR);
                cmd.Parameters.Add("@RightDegree", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkLossR);
                cmd.Parameters.Add("@RightExplain", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkCategoryR);
                cmd.Parameters.Add("@LeftDecibel", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.checkRecibelL);
                cmd.Parameters.Add("@LeftDegree", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkLossL);
                cmd.Parameters.Add("@LeftExplain", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkCategoryL);
                cmd.Parameters.Add("@TestResult", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkResult);
                cmd.Parameters.Add("@AssessRAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkAidsResultR);
                cmd.Parameters.Add("@AssessRExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkAidsResultRText);
                cmd.Parameters.Add("@AssessLAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkAidsResultL);
                cmd.Parameters.Add("@AssessLExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkAidsResultLText);
                cmd.Parameters.Add("@VerificationRAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.effectR);
                cmd.Parameters.Add("@VerificationRExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.effectRText);
                cmd.Parameters.Add("@VerificationLAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.effectL);
                cmd.Parameters.Add("@VerificationLExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.effectLText);
                cmd.Parameters.Add("@OtherAssess", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.effectOther);
                cmd.Parameters.Add("@OtherAssessExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.effectOtherText);
                cmd.Parameters.Add("@ManageSuggestion", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.suggestion);
                cmd.Parameters.Add("@ManageSuggestionHour", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.suggestionHour);
                cmd.Parameters.Add("@OperateSuggestion", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.suggestion2);
                cmd.Parameters.Add("@suggestion", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.suggestion3);
                cmd.Parameters.Add("@suggestionExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.suggestion3Text);
                cmd.Parameters.Add("@NextDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.checkNextDate);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('HearingAssessment') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();

                    StudentDataBasic DateItem = new StudentDataBasic();
                    DateItem.studentID = StructData.studentID;
                    DateItem.assistmanageR = StructData.assistmanageR;
                    DateItem.brandR = StructData.brandR;
                    DateItem.modelR = StructData.modelR;
                    DateItem.buyingPlaceR = StructData.buyingPlaceR;
                    DateItem.buyingtimeR = StructData.buyingtimeR;
                    DateItem.insertHospitalR = StructData.insertHospitalR;
                    DateItem.openHzDateR = StructData.openHzDateR;
                    DateItem.assistmanageL = StructData.assistmanageL;
                    DateItem.brandL = StructData.brandL;
                    DateItem.modelL = StructData.modelL;
                    DateItem.buyingtimeL = StructData.buyingtimeL;
                    DateItem.buyingPlaceL = StructData.buyingPlaceL;
                    DateItem.insertHospitalL = StructData.insertHospitalL;
                    DateItem.openHzDateL = StructData.openHzDateL;
                    this.ComparisonAidsData(DateItem);
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
    
    public CreateHearingAssessment getHearingAssessmentData(string sID)
    {
        CreateHearingAssessment returnValue = new CreateHearingAssessment();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT HearingAssessment.*, StudentDatabase.StudentName, StudentDatabase.StudentBirthday " +
                            "FROM HearingAssessment INNER JOIN StudentDatabase ON HearingAssessment.StudentID=StudentDatabase.StudentID " +
                            "WHERE HearingAssessment.isDeleted=0 and StudentDatabase.isDeleted=0 AND HearingAssessment.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.checkDate = DateTime.Parse(dr["CheckDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.studentAge = dr["Age"].ToString();
                    returnValue.studentMonth = dr["AgeMonth"].ToString();
                    returnValue.studentUseAids = dr["UseAids"].ToString();
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
                    returnValue.detectionPurposes = dr["Purpose"].ToString();
                    returnValue.detectionPurposesText = dr["PurposeText"].ToString();
                    returnValue.explain = dr["PurposeExplain"].ToString();
                    returnValue.observation = dr["Observation"].ToString();
                    returnValue.observationExplain = dr["ObservationExplain"].ToString();
                    returnValue.checkR = dr["RightEar"].ToString();
                    returnValue.checkL = dr["LeftEar"].ToString();
                    returnValue.checkRecibelR = dr["RightDecibel"].ToString();
                    returnValue.checkLossR = dr["RightDegree"].ToString();
                    returnValue.checkCategoryR = dr["RightExplain"].ToString();
                    returnValue.checkRecibelL = dr["LeftDecibel"].ToString();
                    returnValue.checkLossL = dr["LeftDegree"].ToString();
                    returnValue.checkCategoryL = dr["LeftExplain"].ToString();
                    returnValue.checkResult = dr["TestResult"].ToString();
                    returnValue.checkAidsResultR = dr["AssessRAids"].ToString();
                    returnValue.checkAidsResultRText = dr["AssessRExplain"].ToString();
                    returnValue.checkAidsResultL = dr["AssessLAids"].ToString();
                    returnValue.checkAidsResultLText = dr["AssessLExplain"].ToString();
                    returnValue.effectR = dr["VerificationRAids"].ToString();
                    returnValue.effectRText = dr["VerificationRExplain"].ToString();
                    returnValue.effectL = dr["VerificationLAids"].ToString();
                    returnValue.effectLText = dr["VerificationLExplain"].ToString();
                    returnValue.effectOther = dr["OtherAssess"].ToString();
                    returnValue.effectOtherText = dr["OtherAssessExplain"].ToString();
                    returnValue.suggestion = dr["ManageSuggestion"].ToString();
                    returnValue.suggestionHour = dr["ManageSuggestionHour"].ToString();
                    returnValue.suggestion2 = dr["OperateSuggestion"].ToString();
                    returnValue.suggestion3 = dr["suggestion"].ToString();
                    returnValue.suggestion3Text = dr["suggestionExplain"].ToString();
                    returnValue.checkNextDate = DateTime.Parse(dr["NextDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.audiologist = dr["CreateFileBy"].ToString();
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> CreateFileName = sDB.getStaffDataName(returnValue.audiologist);
                    returnValue.audiologistName = CreateFileName[1].ToString();
                }
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
    public string[] setHearingAssessmentData(CreateHearingAssessment StructData)
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
                string sql = "UPDATE HearingAssessment SET CheckDate=@CheckDate, Age=@Age, AgeMonth=@AgeMonth, UseAids=@UseAids, HearingAids_R=@HearingAids_R, "+
                    "AidsBrand_R=@AidsBrand_R, AidsModel_R=@AidsModel_R, AidsOptionalTime_R=@AidsOptionalTime_R, AidsOptionalLocation_R=@AidsOptionalLocation_R, "+
                    "EEarHospital_R=@EEarHospital_R,EEarOpen_R=@EEarOpen_R, HearingAids_L=@HearingAids_L, AidsBrand_L=@AidsBrand_L, AidsModel_L=@AidsModel_L, "+
                    "AidsOptionalTime_L=@AidsOptionalTime_L, AidsOptionalLocation_L=@AidsOptionalLocation_L, " +
                    "EEarHospital_L=@EEarHospital_L, EEarOpen_L=@EEarOpen_L, Purpose=@Purpose, PurposeText=@PurposeText, "+
                    "PurposeExplain=@PurposeExplain, Observation=@Observation, ObservationExplain=@ObservationExplain, RightEar=@RightEar, LeftEar=@LeftEar, "+
                    "RightDecibel=@RightDecibel, RightDegree=@RightDegree, RightExplain=@RightExplain, LeftDecibel=@LeftDecibel, LeftDegree=@LeftDegree, "+
                    "LeftExplain=@LeftExplain, TestResult=@TestResult, AssessRAids=@AssessRAids, AssessRExplain=@AssessRExplain, AssessLAids=@AssessLAids, "+
                    "AssessLExplain=@AssessLExplain, VerificationRAids=@VerificationRAids, VerificationRExplain=@VerificationRExplain, VerificationLAids=@VerificationLAids, "+
                    "VerificationLExplain=@VerificationLExplain, OtherAssess=@OtherAssess, OtherAssessExplain=@OtherAssessExplain, ManageSuggestion=@ManageSuggestion, "+
                    "ManageSuggestionHour=@ManageSuggestionHour, OperateSuggestion=@OperateSuggestion, suggestion=@suggestion, suggestionExplain=@suggestionExplain, "+
                    "NextDate=@NextDate, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.ID);
                cmd.Parameters.Add("@CheckDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.checkDate);
                cmd.Parameters.Add("@Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.studentAge);
                cmd.Parameters.Add("@AgeMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.studentMonth);
                cmd.Parameters.Add("@UseAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.studentUseAids);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateL);
                cmd.Parameters.Add("@Purpose", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.detectionPurposes);
                cmd.Parameters.Add("@PurposeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.detectionPurposesText);
                cmd.Parameters.Add("@PurposeExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.explain);
                cmd.Parameters.Add("@Observation", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.observation);
                cmd.Parameters.Add("@ObservationExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.observationExplain);
                cmd.Parameters.Add("@RightEar", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkR);
                cmd.Parameters.Add("@LeftEar", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkL);
                cmd.Parameters.Add("@RightDecibel", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.checkRecibelR);
                cmd.Parameters.Add("@RightDegree", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkLossR);
                cmd.Parameters.Add("@RightExplain", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkCategoryR);
                cmd.Parameters.Add("@LeftDecibel", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.checkRecibelL);
                cmd.Parameters.Add("@LeftDegree", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkLossL);
                cmd.Parameters.Add("@LeftExplain", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkCategoryL);
                cmd.Parameters.Add("@TestResult", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkResult);
                cmd.Parameters.Add("@AssessRAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkAidsResultR);
                cmd.Parameters.Add("@AssessRExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkAidsResultRText);
                cmd.Parameters.Add("@AssessLAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.checkAidsResultL);
                cmd.Parameters.Add("@AssessLExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkAidsResultLText);
                cmd.Parameters.Add("@VerificationRAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.effectR);
                cmd.Parameters.Add("@VerificationRExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.effectRText);
                cmd.Parameters.Add("@VerificationLAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.effectL);
                cmd.Parameters.Add("@VerificationLExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.effectLText);
                cmd.Parameters.Add("@OtherAssess", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.effectOther);
                cmd.Parameters.Add("@OtherAssessExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.effectOtherText);
                cmd.Parameters.Add("@ManageSuggestion", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.suggestion);
                cmd.Parameters.Add("@ManageSuggestionHour", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.suggestionHour);
                cmd.Parameters.Add("@OperateSuggestion", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.suggestion2);
                cmd.Parameters.Add("@suggestion", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.suggestion3);
                cmd.Parameters.Add("@suggestionExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.suggestion3Text);
                cmd.Parameters.Add("@NextDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.checkNextDate);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] == "1")
                {
                    StudentDataBasic DateItem = new StudentDataBasic();
                    DateItem.studentID = StructData.studentID;
                    DateItem.assistmanageR = StructData.assistmanageR;
                    DateItem.brandR = StructData.brandR;
                    DateItem.modelR = StructData.modelR;
                    DateItem.buyingPlaceR = StructData.buyingPlaceR;
                    DateItem.buyingtimeR = StructData.buyingtimeR;
                    DateItem.insertHospitalR = StructData.insertHospitalR;
                    DateItem.openHzDateR = StructData.openHzDateR;
                    DateItem.assistmanageL = StructData.assistmanageL;
                    DateItem.brandL = StructData.brandL;
                    DateItem.modelL = StructData.modelL;
                    DateItem.buyingtimeL = StructData.buyingtimeL;
                    DateItem.buyingPlaceL = StructData.buyingPlaceL;
                    DateItem.insertHospitalL = StructData.insertHospitalL;
                    DateItem.openHzDateL = StructData.openHzDateL;
                    this.ComparisonAidsData(DateItem);
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

    private string SearchHearingAssessmentConditionReturn(SearchHearingAssessment SearchStructure)
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

        if (SearchStructure.txtcheckDatestart != null && SearchStructure.txtcheckDateend != null && SearchStructure.txtcheckDatestart != DateBase && SearchStructure.txtcheckDateend != DateBase)
        {
            ConditionReturn += " AND HearingAssessment.CheckDate BETWEEN (@sDateStart) AND (@sDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND HearingAssessment.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchHearingAssessmentDataCount(SearchHearingAssessment SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchHearingAssessmentConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) " +
                            "FROM HearingAssessment INNER JOIN StudentDatabase ON HearingAssessment.StudentID=StudentDatabase.StudentID " +
                            "WHERE HearingAssessment.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDatestart);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDateend);
                returnValue[0] = cmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
    }
    public List<SearchHearingAssessmentResult> SearchHearingAssessmentData(int indexpage, SearchHearingAssessment SearchStructure)
    {
        List<SearchHearingAssessmentResult> returnValue = new List<SearchHearingAssessmentResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchHearingAssessmentConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY HearingAssessment.ID DESC) " +
                            "AS RowNum, HearingAssessment.*, StudentDatabase.StudentName, StudentDatabase.StudentBirthday " +
                            "FROM HearingAssessment INNER JOIN StudentDatabase ON HearingAssessment.StudentID=StudentDatabase.StudentID " +
                            "WHERE HearingAssessment.isDeleted=0 and StudentDatabase.isDeleted=0 " + ConditionReturn + " ) AS NewTable " +
                            "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDatestart);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDateend);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchHearingAssessmentResult addValue = new SearchHearingAssessmentResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtcheckAge = dr["Age"].ToString();
                    addValue.txtcheckAgeMonth = dr["AgeMonth"].ToString();
                    addValue.txtcheckDate = DateTime.Parse(dr["checkDate"].ToString()).ToString("yyyy-MM-dd");

                    StaffDataBase sDB = new StaffDataBase();
                    List<string> CreateFileName = sDB.getStaffDataName(dr["CreateFileBy"].ToString());
                    addValue.txtaudiologistName = CreateFileName[1].ToString();
                    returnValue.Add(addValue);
                }
            }
            catch (Exception e)
            {
                SearchHearingAssessmentResult addValue = new SearchHearingAssessmentResult();
                addValue.ID = "-1";
                addValue.txtstudentName = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] createStudentAidsUse(CreateStudentAidsUse StudentData)
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
                string sql = "INSERT INTO HearingCaseAidsUseRecord (Unit, StudentID, StudentAge, StudentMonth, HearingAids_R, AidsBrand_R, AidsModel_R, AidsOptionalTime_R, " +
                    "AidsOptionalLocation_R, EEarHospital_R, EEarOpen_R, AidsSource_R, AidsSourceText_R, HearingAids_L, AidsBrand_L, AidsModel_L, AidsOptionalTime_L, " +
                    "AidsOptionalLocation_L, EEarHospital_L, EEarOpen_L, AidsSource_L, AidsSourceText_L, " +
                    "AcquisitionDate, FMSourceAids, FMTypeAids, FMAidsBrand, FMAidsModel, FMAidsChannel, FMAidsModelR, FMAidsDPAIR, FMAidsProgramR, FMAudioProportionR, " +
                    "FMAidsUIR, FMAidsUITextR, FMAidsReceptorR, FMAidsReceptorVolumeR, FMAidsGainR, FMAidsModelL, FMAidsDPAIL, FMAidsProgramL, FMAudioProportionL, FMAidsUIL, " +
                    "FMAidsUITextL, FMAidsReceptorL, FMAidsReceptorVolumeL, FMAidsGainL, CreateFileBy, UpFileBy, UpFileDate) " +
                    "VALUES(@Unit, @StudentID, @StudentAge, @StudentMonth, @HearingAids_R, @AidsBrand_R, @AidsModel_R, @AidsOptionalTime_R, @AidsOptionalLocation_R, " +
                    "@EEarHospital_R, @EEarOpen_R, @AidsSource_R, @AidsSourceText_R, @HearingAids_L, @AidsBrand_L, @AidsModel_L, @AidsOptionalTime_L, " +
                    "@AidsOptionalLocation_L, @EEarHospital_L, @EEarOpen_L, @AidsSource_L, @AidsSourceText_L, " +
                    "@AcquisitionDate, @FMSourceAids, @FMTypeAids, @FMAidsBrand, @FMAidsModel, @FMAidsChannel, @FMAidsModelR, @FMAidsDPAIR, @FMAidsProgramR, " +
                    "@FMAudioProportionR, @FMAidsUIR, @FMAidsUITextR, @FMAidsReceptorR, @FMAidsReceptorVolumeR, @FMAidsGainR, @FMAidsModelL, @FMAidsDPAIL, " +
                    "@FMAidsProgramL, @FMAudioProportionL, @FMAidsUIL, @FMAidsUITextL, @FMAidsReceptorL, @FMAidsReceptorVolumeL, @FMAidsGainL, @CreateFileBy, " +
                    "@UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentData.studentID);
                cmd.Parameters.Add("@StudentAge", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studentAge);
                cmd.Parameters.Add("@StudentMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.studentMonth);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.openHzDateR);
                cmd.Parameters.Add("@AidsSource_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.sourceR);
                cmd.Parameters.Add("@AidsSourceText_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.sourceTextR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.openHzDateL);
                cmd.Parameters.Add("@AidsSource_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.sourceL);
                cmd.Parameters.Add("@AidsSourceText_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.sourceTextL);
                cmd.Parameters.Add("@AcquisitionDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.gainDate);
                cmd.Parameters.Add("@FMSourceAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmAidssource);
                cmd.Parameters.Add("@FMTypeAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmAidstype);
                cmd.Parameters.Add("@FMAidsBrand", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmBrand);
                cmd.Parameters.Add("@FMAidsModel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmModel);
                cmd.Parameters.Add("@FMAidsChannel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmChannel);
                cmd.Parameters.Add("@FMAidsModelR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAidstypeR);
                cmd.Parameters.Add("@FMAidsDPAIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.DPAIsettingR);
                cmd.Parameters.Add("@FMAidsProgramR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmProgramR);
                cmd.Parameters.Add("@FMAudioProportionR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAudioR);
                cmd.Parameters.Add("@FMAidsUIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmUIR);
                cmd.Parameters.Add("@FMAidsUITextR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmUITextR);
                cmd.Parameters.Add("@FMAidsReceptorR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmReceptorR);
                cmd.Parameters.Add("@FMAidsReceptorVolumeR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmVolumeR);
                cmd.Parameters.Add("@FMAidsGainR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmGainR);
                cmd.Parameters.Add("@FMAidsModelL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAidstypeL);
                cmd.Parameters.Add("@FMAidsDPAIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.DPAIsettingL);
                cmd.Parameters.Add("@FMAidsProgramL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmProgramL);
                cmd.Parameters.Add("@FMAudioProportionL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAudioL);
                cmd.Parameters.Add("@FMAidsUIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmUIL);
                cmd.Parameters.Add("@FMAidsUITextL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmUITextL);
                cmd.Parameters.Add("@FMAidsReceptorL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmReceptorL);
                cmd.Parameters.Add("@FMAidsReceptorVolumeL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmVolumeL);
                cmd.Parameters.Add("@FMAidsGainL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmGainL);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('HearingCaseAidsUseRecord') AS cID";
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
    public CreateStudentAidsUse getStudentAidsUse(string ID)
    {
        CreateStudentAidsUse returnValue = new CreateStudentAidsUse();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StudentDatabase.StudentName,StudentDatabase.StudentBirthday, HearingCaseAidsUseRecord.* " +
                             "FROM HearingCaseAidsUseRecord INNER JOIN StudentDatabase ON HearingCaseAidsUseRecord.StudentID=StudentDatabase.StudentID " +
                             "WHERE HearingCaseAidsUseRecord.isDeleted=0 and StudentDatabase.isDeleted=0 AND HearingCaseAidsUseRecord.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.studentAge = dr["StudentAge"].ToString();
                    returnValue.studentMonth = dr["StudentMonth"].ToString();
                    returnValue.assistmanageR = dr["HearingAids_R"].ToString();
                    returnValue.brandR = dr["AidsBrand_R"].ToString();
                    returnValue.modelR = dr["AidsModel_R"].ToString();
                    returnValue.buyingPlaceR = dr["AidsOptionalLocation_R"].ToString();
                    returnValue.buyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.insertHospitalR = dr["EEarHospital_R"].ToString();
                    returnValue.openHzDateR = DateTime.Parse(dr["EEarOpen_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sourceR = dr["AidsSource_R"].ToString();
                    returnValue.sourceTextR = dr["AidsSourceText_R"].ToString();
                    returnValue.assistmanageL = dr["HearingAids_L"].ToString();
                    returnValue.brandL = dr["AidsBrand_L"].ToString();
                    returnValue.modelL = dr["AidsModel_L"].ToString();
                    returnValue.buyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.buyingPlaceL = dr["AidsOptionalLocation_L"].ToString();
                    returnValue.insertHospitalL = dr["EEarHospital_L"].ToString();
                    returnValue.openHzDateL = DateTime.Parse(dr["EEarOpen_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sourceL = dr["AidsSource_L"].ToString();
                    returnValue.sourceTextL = dr["AidsSourceText_L"].ToString();
                    returnValue.gainDate = DateTime.Parse(dr["AcquisitionDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fmAidssource = dr["FMSourceAids"].ToString();
                    returnValue.fmAidstype = dr["FMTypeAids"].ToString();
                    returnValue.fmBrand = dr["FMAidsBrand"].ToString();
                    returnValue.fmModel = dr["FMAidsModel"].ToString();
                    returnValue.fmChannel = dr["FMAidsChannel"].ToString();
                    returnValue.fmAidstypeR = dr["FMAidsModelR"].ToString();
                    returnValue.DPAIsettingR = dr["FMAidsDPAIR"].ToString();
                    returnValue.fmProgramR = dr["FMAidsProgramR"].ToString();
                    returnValue.fmAudioR = dr["FMAudioProportionR"].ToString();
                    returnValue.fmUIR = dr["FMAidsUIR"].ToString();
                    returnValue.fmUITextR = dr["FMAidsUITextR"].ToString();
                    returnValue.fmReceptorR = dr["FMAidsReceptorR"].ToString();
                    returnValue.fmVolumeR = dr["FMAidsReceptorVolumeR"].ToString();
                    returnValue.fmGainR = dr["FMAidsGainR"].ToString();
                    returnValue.fmAidstypeL = dr["FMAidsModelL"].ToString();
                    returnValue.DPAIsettingL = dr["FMAidsDPAIL"].ToString();
                    returnValue.fmProgramL = dr["FMAidsProgramL"].ToString();
                    returnValue.fmAudioL = dr["FMAudioProportionL"].ToString();
                    returnValue.fmUIL = dr["FMAidsUIL"].ToString();
                    returnValue.fmUITextL = dr["FMAidsUITextL"].ToString();
                    returnValue.fmReceptorL = dr["FMAidsReceptorL"].ToString();
                    returnValue.fmVolumeL = dr["FMAidsReceptorVolumeL"].ToString();
                    returnValue.fmGainL = dr["FMAidsGainL"].ToString();
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


    public string[] setStudentAidsUse(CreateStudentAidsUse StudentData)
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
                string sql = "UPDATE HearingCaseAidsUseRecord SET HearingAids_R=@HearingAids_R, AidsBrand_R=@AidsBrand_R, AidsModel_R=@AidsModel_R, " +
                    "AidsOptionalTime_R=@AidsOptionalTime_R, AidsOptionalLocation_R=@AidsOptionalLocation_R, EEarHospital_R=@EEarHospital_R, EEarOpen_R=@EEarOpen_R, " +
                    "AidsSource_R=@AidsSource_R, AidsSourceText_R=@AidsSourceText_R,HearingAids_L=@HearingAids_L, AidsBrand_L=@AidsBrand_L, AidsModel_L=@AidsModel_L, " +
                    "AidsOptionalTime_L=@AidsOptionalTime_L, AidsOptionalLocation_L=@AidsOptionalLocation_L, EEarHospital_L=@EEarHospital_L, EEarOpen_L=@EEarOpen_L, " +
                    "AidsSource_L=@AidsSource_L, AidsSourceText_L=@AidsSourceText_L, AcquisitionDate=@AcquisitionDate, " +
                    "FMSourceAids=@FMSourceAids, FMTypeAids=@FMTypeAids, FMAidsBrand=@FMAidsBrand, FMAidsModel=@FMAidsModel, FMAidsChannel=@FMAidsChannel, " +
                    "FMAidsModelR=@FMAidsModelR, FMAidsDPAIR=@FMAidsDPAIR, FMAidsProgramR=@FMAidsProgramR, FMAudioProportionR=@FMAudioProportionR, FMAidsUIR=@FMAidsUIR, " +
                    "FMAidsUITextR=@FMAidsUITextR, FMAidsReceptorR=@FMAidsReceptorR, FMAidsReceptorVolumeR=@FMAidsReceptorVolumeR, FMAidsGainR=@FMAidsGainR, " +
                    "FMAidsModelL=@FMAidsModelL, FMAidsDPAIL=@FMAidsDPAIL, FMAidsProgramL=@FMAidsProgramL, FMAudioProportionL=@FMAudioProportionL, FMAidsUIL=@FMAidsUIL, " +
                    "FMAidsUITextL=@FMAidsUITextL, FMAidsReceptorL=@FMAidsReceptorL, FMAidsReceptorVolumeL=@FMAidsReceptorVolumeL, FMAidsGainL=@FMAidsGainL, " +
                    "UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(StudentData.ID);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.openHzDateR);
                cmd.Parameters.Add("@AidsSource_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.sourceR);
                cmd.Parameters.Add("@AidsSourceText_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.sourceTextR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.openHzDateL);
                cmd.Parameters.Add("@AidsSource_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.sourceL);
                cmd.Parameters.Add("@AidsSourceText_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.sourceTextL);
                cmd.Parameters.Add("@AcquisitionDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StudentData.gainDate);
                cmd.Parameters.Add("@FMSourceAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmAidssource);
                cmd.Parameters.Add("@FMTypeAids", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmAidstype);
                cmd.Parameters.Add("@FMAidsBrand", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmBrand);
                cmd.Parameters.Add("@FMAidsModel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmModel);
                cmd.Parameters.Add("@FMAidsChannel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmChannel);
                cmd.Parameters.Add("@FMAidsModelR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAidstypeR);
                cmd.Parameters.Add("@FMAidsDPAIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.DPAIsettingR);
                cmd.Parameters.Add("@FMAidsProgramR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmProgramR);
                cmd.Parameters.Add("@FMAudioProportionR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAudioR);
                cmd.Parameters.Add("@FMAidsUIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmUIR);
                cmd.Parameters.Add("@FMAidsUITextR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmUITextR);
                cmd.Parameters.Add("@FMAidsReceptorR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmReceptorR);
                cmd.Parameters.Add("@FMAidsReceptorVolumeR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmVolumeR);
                cmd.Parameters.Add("@FMAidsGainR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmGainR);
                cmd.Parameters.Add("@FMAidsModelL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAidstypeL);
                cmd.Parameters.Add("@FMAidsDPAIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.DPAIsettingL);
                cmd.Parameters.Add("@FMAidsProgramL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmProgramL);
                cmd.Parameters.Add("@FMAudioProportionL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmAudioL);
                cmd.Parameters.Add("@FMAidsUIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StudentData.fmUIL);
                cmd.Parameters.Add("@FMAidsUITextL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmUITextL);
                cmd.Parameters.Add("@FMAidsReceptorL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmReceptorL);
                cmd.Parameters.Add("@FMAidsReceptorVolumeL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmVolumeL);
                cmd.Parameters.Add("@FMAidsGainL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StudentData.fmGainL);
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


    private string SearchAidsUseConditionReturn(SearchAidsUse SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentID != null)
        {
            ConditionReturn += " AND HearingCaseAidsUseRecord.StudentID=(@StudentID) ";
        }
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentDatabase.StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtbirthdaystart != null && SearchStructure.txtbirthdayend != null && SearchStructure.txtbirthdaystart != DateBase && SearchStructure.txtbirthdayend != DateBase)
        {
            ConditionReturn += " AND StudentDatabase.StudentBirthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStructure.txtaidstypeL != null && SearchStructure.txtaidstypeL != "0")
        {
            ConditionReturn += " AND HearingCaseAidsUseRecord.HearingAids_L =(@AidstypeL) ";
        }
        if (SearchStructure.txtaidstypeR != null && SearchStructure.txtaidstypeR != "0")
        {
            ConditionReturn += " AND HearingCaseAidsUseRecord.HearingAids_R =(@AidstypeR) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND HearingCaseAidsUseRecord.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStudentAidsUseCount(SearchAidsUse SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAidsUseConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM HearingCaseAidsUseRecord " +
                               "INNER JOIN StudentDatabase ON HearingCaseAidsUseRecord.StudentID=StudentDatabase.StudentID " +
                               "WHERE HearingCaseAidsUseRecord.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@AidstypeL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtaidstypeL);
                cmd.Parameters.Add("@AidstypeR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtaidstypeR);
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

    public List<SearchAidsUseResult> SearchStudentAidsUse(int indexpage, SearchAidsUse SearchStructure)
    {
        List<SearchAidsUseResult> returnValue = new List<SearchAidsUseResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAidsUseConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY HearingCaseAidsUseRecord.ID DESC) " +
                 "AS RowNum, HearingCaseAidsUseRecord.*,StudentDatabase.StudentName FROM HearingCaseAidsUseRecord " +
                 "LEFT JOIN StudentDatabase ON HearingCaseAidsUseRecord.StudentID=StudentDatabase.StudentID " +
                 "WHERE HearingCaseAidsUseRecord.isDeleted=0 and  StudentDatabase.isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@AidstypeL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtaidstypeL);
                cmd.Parameters.Add("@AidstypeR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtaidstypeR);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchAidsUseResult addValue = new SearchAidsUseResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtstudentID = dr["StudentID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtaidstypeL = dr["HearingAids_L"].ToString();
                    addValue.txtaidstypeR = dr["HearingAids_R"].ToString();
                    addValue.txtbuyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtbuyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtfmAidstypeL = dr["FMAidsModelL"].ToString();
                    addValue.txtfmAidstypeR = dr["FMAidsModelR"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchAidsUseResult addValue = new SearchAidsUseResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public CreateHearingInspection getStudentAidsUseNewData(string SID)
    {
        CreateHearingInspection returnValue = new CreateHearingInspection();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT TOP 1 HearingInspection.*, StudentDatabase.StudentName, StudentDatabase.StudentBirthday " +
                            "FROM HearingInspection INNER JOIN StudentDatabase ON HearingInspection.StudentID=StudentDatabase.StudentID " +
                            "WHERE HearingInspection.isDeleted=0 AND HearingInspection.StudentID=@StudentID ORDER BY ID DESC";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.checkDate = DateTime.Parse(dr["CheckDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.checkMode = dr["CheckType"].ToString();
                    returnValue.credibility = dr["Credibility"].ToString();
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
                    returnValue.checkInstrument = dr["ListenCheck"].ToString();
                    returnValue.headphone = dr["Earphones"].ToString();
                    returnValue.nudetonety = dr["NakedEar"].ToString();
                    returnValue.hearingtone = dr["HearingAfter"].ToString();
                    returnValue.hearingtoneImg = dr["PureToneImg"].ToString();
                    returnValue.toneR = dr["PrueRDecibel"].ToString();
                    returnValue.toneL = dr["PrueLDecibel"].ToString();
                    returnValue.hearingtoneR = dr["EarEndoscopyRight"].ToString();
                    returnValue.hearingtoneRText = dr["EarEndoscopyRExplain"].ToString();
                    returnValue.hearingtoneL = dr["EarEndoscopyLeft"].ToString();
                    returnValue.hearingtoneLText = dr["EarEndoscopyLExplain"].ToString();
                    returnValue.hearingInstrument = dr["TympanicCavity"].ToString();
                    returnValue.hearingImgR = dr["PatternRight"].ToString();
                    returnValue.hearingImgL = dr["PatternLeft"].ToString();
                    returnValue.hearingVolumeR = dr["CanalRight"].ToString();
                    returnValue.hearingVolumeL = dr["CanalLeft"].ToString();
                    returnValue.conformR = dr["EardrumRight"].ToString();
                    returnValue.conformL = dr["EardrumLeft"].ToString();
                    returnValue.pressureR = dr["PressureRight"].ToString();
                    returnValue.pressureL = dr["PressureLeft"].ToString();
                    returnValue.aidsR = dr["AidsDetectRight1"].ToString();
                    returnValue.aidsdetectR = dr["AidsDetectRight2"].ToString();
                    returnValue.aidsL = dr["AidsDetectLeft1"].ToString();
                    returnValue.aidsdetectL = dr["AidsDetectLeft2"].ToString();
                    returnValue.aidsOther = dr["AidsDetect"].ToString();
                    returnValue.material1 = dr["VoiceMaterial1"].ToString();
                    returnValue.voice1 = dr["VoiceWear1"].ToString();
                    returnValue.state1 = dr["VoiceState1"].ToString();
                    returnValue.volume1 = dr["VoiceVolume1"].ToString();
                    returnValue.result1 = dr["VoiceResult1"].ToString();
                    returnValue.remark1 = dr["VoiceRemark1"].ToString();
                    returnValue.material2 =int.Parse( dr["VoiceMaterial2"].ToString());
                    returnValue.voice2 = dr["VoiceWear2"].ToString();
                    returnValue.state2 = dr["VoiceState2"].ToString();
                    returnValue.volume2 = dr["VoiceVolume2"].ToString();
                    returnValue.result2 = dr["VoiceResult2"].ToString();
                    returnValue.remark2 = dr["VoiceRemark2"].ToString();
                    returnValue.project3 = dr["VoiceItem3"].ToString();
                    returnValue.material3 = dr["VoiceMaterial3"].ToString();
                    returnValue.voice3 = dr["VoiceWear3"].ToString();
                    returnValue.state3 = dr["VoiceState3"].ToString();
                    returnValue.volume3 = dr["VoiceVolume3"].ToString();
                    returnValue.result3 = dr["VoiceResult3"].ToString();
                    returnValue.remark3 = dr["VoiceRemark3"].ToString();
                    returnValue.project4 = dr["VoiceItem4"].ToString();
                    returnValue.material4 = dr["VoiceMaterial4"].ToString();
                    returnValue.voice4 = dr["VoiceWear4"].ToString();
                    returnValue.state4 = dr["VoiceState4"].ToString();
                    returnValue.volume4 = dr["VoiceVolume4"].ToString();
                    returnValue.result4 = dr["VoiceResult4"].ToString();
                    returnValue.remark4 = dr["VoiceRemark4"].ToString();
                    returnValue.checkPurpose =int.Parse( dr["VoicePurpose"].ToString());
                    returnValue.checkPurposeText = dr["VoicePurposeExplain"].ToString();
                    returnValue.explain = dr["VoiceExplain"].ToString();

                    returnValue.audiologist = dr["CreateFileBy"].ToString();
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> CreateFileName = sDB.getStaffDataName(returnValue.audiologist);
                    returnValue.audiologistName = CreateFileName[1].ToString();
                }
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
    public string[] createHearingInspectionData(CreateHearingInspection StructData)
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
                string sql = "INSERT INTO HearingInspection (Unit, CheckDate, StudentID, CheckType, Credibility,HearingAids_R, "+
                    "AidsBrand_R, AidsModel_R, AidsOptionalTime_R, "+
                    "AidsOptionalLocation_R, EEarHospital_R, EEarOpen_R, HearingAids_L, AidsBrand_L, AidsModel_L, AidsOptionalTime_L, "+
                    "AidsOptionalLocation_L, EEarHospital_L,"+
                    "EEarOpen_L,  ListenCheck, Earphones, NakedEar, HearingAfter, PureToneImg, PrueRDecibel, PrueLDecibel, EarEndoscopyRight, " +
                    "EarEndoscopyRExplain, EarEndoscopyLeft, EarEndoscopyLExplain, TympanicCavity, PatternRight, PatternLeft, CanalRight, CanalLeft, "+
                    "EardrumRight, " +
                    "EardrumLeft, PressureRight, PressureLeft, AidsDetectRight1, AidsDetectRight2, AidsDetectLeft1, AidsDetectLeft2, AidsDetect, "+
                    "VoiceMaterial1, VoiceWear1, " +
                    "VoiceState1, VoiceVolume1, VoiceResult1, VoiceRemark1, VoiceMaterial2, VoiceWear2, VoiceState2, VoiceVolume2, VoiceResult2, "+
                    "VoiceRemark2, VoiceItem3, " +
                    "VoiceMaterial3, VoiceWear3, VoiceState3, VoiceVolume3, VoiceResult3, VoiceRemark3, VoiceItem4, VoiceMaterial4, VoiceWear4, "+
                    "VoiceState4, VoiceVolume4, " +
                    "VoiceResult4, VoiceRemark4, VoicePurpose, VoicePurposeExplain, VoiceExplain, CreateFileBy, UpFileBy, UpFileDate,"+
                    "material1,SATVolumeBefore,SATREarBefore,SATLEarBefore,SATEarBefore,SATVolumeAfter,SATREarAfter,SATLEarAfter,SATEarAfter,"+
                    "WRSVolumeBefore,WRSREarBefore,WRSLEarBefore,WRSEarBefore,WRSVolumeAfter,WRSREarAfter,WRSLEarAfter,WRSEarAfter," +
                    "material2,material3,project3,project3VolumeBefore,project3REarBefore,project3LEarBefore,project3EarBefore,"+
                    "project3VolumeAfter,project3REarAfter,project3LEarAfter,project3EarAfter," +
                    "material4,project4,project4VolumeBefore,project4REarBefore,project4LEarBefore,project4EarBefore," +
                    "project4VolumeAfter,project4REarAfter,project4LEarAfter,project4EarAfter," +
                    "checkPurposeText,checkPurpose,explain" +
                  
                    ") " +
                    "VALUES(@Unit, @CheckDate, @StudentID, @CheckType, @Credibility, @HearingAids_R, @AidsBrand_R, @AidsModel_R, @AidsOptionalTime_R, "+
                    "@AidsOptionalLocation_R, "+
                    "@EEarHospital_R, @EEarOpen_R, @HearingAids_L, @AidsBrand_L, @AidsModel_L, @AidsOptionalTime_L, @AidsOptionalLocation_L, "+
                    "@EEarHospital_L, @EEarOpen_L, "+
                    "@ListenCheck, @Earphones, @NakedEar, @HearingAfter, @PureToneImg, @PrueRDecibel, @PrueLDecibel, @EarEndoscopyRight, "+
                    "@EarEndoscopyRExplain, " +
                    "@EarEndoscopyLeft, @EarEndoscopyLExplain, @TympanicCavity, @PatternRight, @PatternLeft, @CanalRight, @CanalLeft, "+
                    "@EardrumRight, @EardrumLeft, " +
                    "@PressureRight, @PressureLeft, @AidsDetectRight1, @AidsDetectRight2, @AidsDetectLeft1, @AidsDetectLeft2, @AidsDetect, "+
                    "@VoiceMaterial1, @VoiceWear1, " +
                    "@VoiceState1, @VoiceVolume1, @VoiceResult1, @VoiceRemark1, @VoiceMaterial2, @VoiceWear2, @VoiceState2, @VoiceVolume2, "+
                    "@VoiceResult2, @VoiceRemark2, " +
                    "@VoiceItem3, @VoiceMaterial3, @VoiceWear3, @VoiceState3, @VoiceVolume3, @VoiceResult3, @VoiceRemark3, @VoiceItem4, "+
                    "@VoiceMaterial4, @VoiceWear4, " +
                    "@VoiceState4, @VoiceVolume4, @VoiceResult4, @VoiceRemark4, @VoicePurpose, @VoicePurposeExplain, @VoiceExplain, "+
                    "@CreateFileBy, @UpFileBy, (getDate()),"+
                    "@WRSVolumeBefore,@WRSREarBefore,@WRSLEarBefore,@WRSEarBefore,@WRSVolumeAfter,@WRSREarAfter,@WRSLEarAfter,@WRSEarAfter," +
                    "@material2,@material3,@project3,@project3VolumeBefore,@project3REarBefore,@project3LEarBefore,@project3EarBefore," +
                    "@project3VolumeAfter,@project3REarAfter,@project3LEarAfter,@project3EarAfter," +
                    "@material4,@project4,@project4VolumeBefore,@project4REarBefore,@project4LEarBefore,@project4EarBefore," +
                    "@project4VolumeAfter,@project4REarAfter,@project4LEarAfter,@project4EarAfter," +
                    "@checkPurposeText,@checkPurpose,@explain" +
                  
                    ")";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@material1 ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material1);
                cmd.Parameters.Add("@SATVolumeBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATVolumeBefore);
                cmd.Parameters.Add("@SATREarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATREarBefore);
                cmd.Parameters.Add("@SATLEarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATLEarBefore);
                cmd.Parameters.Add("@SATEarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATEarBefore);

                cmd.Parameters.Add("@SATVolumeAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATVolumeAfter);
                cmd.Parameters.Add("@SATREarAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATREarAfter);
                cmd.Parameters.Add("@SATLEarAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATLEarAfter);
                cmd.Parameters.Add("@SATEarAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATEarAfter);

                cmd.Parameters.Add("@WRSVolumeBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSVolumeBefore);
                cmd.Parameters.Add("@WRSREarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSREarBefore);
                cmd.Parameters.Add("@WRSLEarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSLEarBefore);
                cmd.Parameters.Add("@WRSEarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSEarBefore);
                cmd.Parameters.Add("@WRSVolumeAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSVolumeAfter);
                cmd.Parameters.Add("@WRSREarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSREarAfter);
                cmd.Parameters.Add("@WRSLEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSLEarAfter);
                cmd.Parameters.Add("@WRSEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSEarAfter);

                cmd.Parameters.Add("@material3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material3);
                cmd.Parameters.Add("@project3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3);
                cmd.Parameters.Add("@material2", SqlDbType.TinyInt).Value = StructData.material2;
                cmd.Parameters.Add("@project3VolumeBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3VolumeBefore);
                cmd.Parameters.Add("@project3REarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3REarBefore);
                cmd.Parameters.Add("@project3LEarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3LEarBefore);
                cmd.Parameters.Add("@project3EarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3EarBefore);
                cmd.Parameters.Add("@project3VolumeAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3VolumeAfter);
                cmd.Parameters.Add("@project3REarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3REarAfter);
                cmd.Parameters.Add("@project3LEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3LEarAfter);
                cmd.Parameters.Add("@project3EarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3EarAfter);

                cmd.Parameters.Add("@project4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4);
                cmd.Parameters.Add("@material4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material4);
                cmd.Parameters.Add("@project4VolumeBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4VolumeBefore);
                cmd.Parameters.Add("@project4REarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4REarBefore);
                cmd.Parameters.Add("@project4LEarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4LEarBefore);
                cmd.Parameters.Add("@project4EarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4EarBefore);
                cmd.Parameters.Add("@project4VolumeAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4VolumeAfter);
                cmd.Parameters.Add("@project4REarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4REarAfter);
                cmd.Parameters.Add("@project4LEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4LEarAfter);
                cmd.Parameters.Add("@project4EarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4EarAfter);
                cmd.Parameters.Add("@checkPurposeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkPurposeText);
                cmd.Parameters.Add("@checkPurpose", SqlDbType.TinyInt).Value = StructData.checkPurpose;
                cmd.Parameters.Add("@explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.explain);    


                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@CheckDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.checkDate);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.studentID);
                cmd.Parameters.Add("@CheckType", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkMode);
                cmd.Parameters.Add("@Credibility", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.credibility);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateL);
                cmd.Parameters.Add("@ListenCheck", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkInstrument);
                cmd.Parameters.Add("@Earphones", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.headphone);
                cmd.Parameters.Add("@NakedEar", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.nudetonety);
                cmd.Parameters.Add("@HearingAfter", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.hearingtone);
                cmd.Parameters.Add("@PureToneImg", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingtoneImg);
                cmd.Parameters.Add("@PrueRDecibel", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.toneR);
                cmd.Parameters.Add("@PrueLDecibel", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.toneL);
                cmd.Parameters.Add("@EarEndoscopyRight", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.hearingtoneR);
                cmd.Parameters.Add("@EarEndoscopyRExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingtoneRText);
                cmd.Parameters.Add("@EarEndoscopyLeft", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.hearingtoneL);
                cmd.Parameters.Add("@EarEndoscopyLExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingtoneLText);
                cmd.Parameters.Add("@TympanicCavity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingInstrument);
                cmd.Parameters.Add("@PatternRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingImgR);
                cmd.Parameters.Add("@PatternLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingImgL);
                cmd.Parameters.Add("@CanalRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingVolumeR);
                cmd.Parameters.Add("@CanalLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingVolumeL);
                cmd.Parameters.Add("@EardrumRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.conformR);
                cmd.Parameters.Add("@EardrumLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.conformL);
                cmd.Parameters.Add("@PressureRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.pressureR);
                cmd.Parameters.Add("@PressureLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.pressureL);
                cmd.Parameters.Add("@AidsDetectRight1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aidsR);
                cmd.Parameters.Add("@AidsDetectRight2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.aidsdetectR);
                cmd.Parameters.Add("@AidsDetectLeft1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aidsL);
                cmd.Parameters.Add("@AidsDetectLeft2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.aidsdetectL);
                cmd.Parameters.Add("@AidsDetect", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aidsOther);
                cmd.Parameters.Add("@VoiceMaterial1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material1);
                cmd.Parameters.Add("@VoiceWear1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice1);
                cmd.Parameters.Add("@VoiceState1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state1);
                cmd.Parameters.Add("@VoiceVolume1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume1);
                cmd.Parameters.Add("@VoiceResult1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result1);
                cmd.Parameters.Add("@VoiceRemark1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark1);
                cmd.Parameters.Add("@VoiceMaterial2", SqlDbType.NVarChar).Value = StructData.material2.ToString();
                cmd.Parameters.Add("@VoiceWear2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice2);
                cmd.Parameters.Add("@VoiceState2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state2);
                cmd.Parameters.Add("@VoiceVolume2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume2);
                cmd.Parameters.Add("@VoiceResult2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result2);
                cmd.Parameters.Add("@VoiceRemark2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark2);
                cmd.Parameters.Add("@VoiceItem3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3);
                cmd.Parameters.Add("@VoiceMaterial3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material3);
                cmd.Parameters.Add("@VoiceWear3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice3);
                cmd.Parameters.Add("@VoiceState3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state3);
                cmd.Parameters.Add("@VoiceVolume3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume3);
                cmd.Parameters.Add("@VoiceResult3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result3);
                cmd.Parameters.Add("@VoiceRemark3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark3);
                cmd.Parameters.Add("@VoiceItem4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4);
                cmd.Parameters.Add("@VoiceMaterial4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material4);
                cmd.Parameters.Add("@VoiceWear4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice4);
                cmd.Parameters.Add("@VoiceState4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state4);
                cmd.Parameters.Add("@VoiceVolume4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume4);
                cmd.Parameters.Add("@VoiceResult4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result4);
                cmd.Parameters.Add("@VoiceRemark4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark4);
                cmd.Parameters.Add("@VoicePurpose", SqlDbType.TinyInt).Value = StructData.checkPurpose;
                cmd.Parameters.Add("@VoicePurposeExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkPurposeText);
                cmd.Parameters.Add("@VoiceExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.explain);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);

                

               
                

                 
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('HearingInspection') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();
                        StudentDataBasic DateItem = new StudentDataBasic();
                        DateItem.studentID = StructData.studentID;
                        DateItem.assistmanageR = StructData.assistmanageR;
                        DateItem.brandR = StructData.brandR;
                        DateItem.modelR = StructData.modelR;
                        DateItem.buyingPlaceR = StructData.buyingPlaceR;
                        DateItem.buyingtimeR = StructData.buyingtimeR;
                        DateItem.insertHospitalR = StructData.insertHospitalR;
                        DateItem.openHzDateR = StructData.openHzDateR;
                        DateItem.assistmanageL = StructData.assistmanageL;
                        DateItem.brandL = StructData.brandL;
                        DateItem.modelL = StructData.modelL;
                        DateItem.buyingtimeL = StructData.buyingtimeL;
                        DateItem.buyingPlaceL = StructData.buyingPlaceL;
                        DateItem.insertHospitalL = StructData.insertHospitalL;
                        DateItem.openHzDateL = StructData.openHzDateL;
                        this.ComparisonAidsData(DateItem);
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

    public CreateHearingInspection getHearingInspectionData(string sID) {
        CreateHearingInspection returnValue = new CreateHearingInspection();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {/*
             cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(StudentHearing.ID);
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
              * "HearingAids_R=@HearingAids_R, AidsBrand_R=@AidsBrand_R, AidsModel_R=@AidsModel_R, AidsOptionalTime_R=@AidsOptionalTime_R, " +
                    "AidsOptionalLocation_R=@AidsOptionalLocation_R, EEarHospital_R=@EEarHospital_R,EEarOpen_R=@EEarOpen_R, " +
                    "HearingAids_L=@HearingAids_L, AidsBrand_L=@AidsBrand_L, AidsModel_L=@AidsModel_L, AidsOptionalTime_L=@AidsOptionalTime_L, "+
                    "AidsOptionalLocation_L=@AidsOptionalLocation_L, EEarHospital_L=@EEarHospital_L, EEarOpen_L=@EEarOpen_L, ListenCheck=@ListenCheck, "+
                    "Earphones=@Earphones, NakedEar=@NakedEar, HearingAfter=@HearingAfter, PrueRDecibel=@PrueRDecibel, PrueLDecibel=@PrueLDecibel, "+
              */
                Sqlconn.Open();
                string sql = "SELECT StudentDatabase.*,HearingInspection.* " +
                            "FROM HearingInspection INNER JOIN StudentDatabase ON HearingInspection.StudentID=StudentDatabase.StudentID " +
                            "WHERE HearingInspection.isDeleted=0 AND HearingInspection.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.checkDate = DateTime.Parse(dr["CheckDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.checkMode = dr["CheckType"].ToString();
                    returnValue.credibility = dr["Credibility"].ToString();
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
                    returnValue.checkInstrument = dr["ListenCheck"].ToString();
                    returnValue.headphone = dr["Earphones"].ToString();
                    returnValue.nudetonety = dr["NakedEar"].ToString();
                    returnValue.hearingtone = dr["HearingAfter"].ToString();
                    returnValue.hearingtoneImg = dr["PureToneImg"].ToString();
                    returnValue.toneR = dr["PrueRDecibel"].ToString();
                    returnValue.toneL = dr["PrueLDecibel"].ToString();
                    returnValue.hearingtoneR = dr["EarEndoscopyRight"].ToString();
                    returnValue.hearingtoneRText = dr["EarEndoscopyRExplain"].ToString();
                    returnValue.hearingtoneL = dr["EarEndoscopyLeft"].ToString();
                    returnValue.hearingtoneLText = dr["EarEndoscopyLExplain"].ToString();
                    returnValue.hearingInstrument = dr["TympanicCavity"].ToString();
                    returnValue.hearingImgR = dr["PatternRight"].ToString();
                    returnValue.hearingImgL = dr["PatternLeft"].ToString();
                    returnValue.hearingVolumeR = dr["CanalRight"].ToString();
                    returnValue.hearingVolumeL = dr["CanalLeft"].ToString();
                    returnValue.conformR = dr["EardrumRight"].ToString();
                    returnValue.conformL = dr["EardrumLeft"].ToString();
                    returnValue.pressureR = dr["PressureRight"].ToString();
                    returnValue.pressureL = dr["PressureLeft"].ToString();
                    returnValue.aidsR = dr["AidsDetectRight1"].ToString();
                    returnValue.aidsdetectR = dr["AidsDetectRight2"].ToString();
                    returnValue.aidsL = dr["AidsDetectLeft1"].ToString();
                    returnValue.aidsdetectL = dr["AidsDetectLeft2"].ToString();
                    returnValue.aidsOther = dr["AidsDetect"].ToString();
                    returnValue.material1 = dr["VoiceMaterial1"].ToString();
                    returnValue.voice1 = dr["VoiceWear1"].ToString();
                    returnValue.state1 = dr["VoiceState1"].ToString();
                    returnValue.volume1 = dr["VoiceVolume1"].ToString();
                    returnValue.result1 = dr["VoiceResult1"].ToString();
                    returnValue.remark1 = dr["VoiceRemark1"].ToString();
                    returnValue.material2 =int.Parse( dr["VoiceMaterial2"].ToString());
                    returnValue.voice2 = dr["VoiceWear2"].ToString();
                    returnValue.state2 = dr["VoiceState2"].ToString();
                    returnValue.volume2 = dr["VoiceVolume2"].ToString();
                    returnValue.result2 = dr["VoiceResult2"].ToString();
                    returnValue.remark2 = dr["VoiceRemark2"].ToString();
                    //returnValue.project3 = dr["VoiceItem3"].ToString();
                    returnValue.material3 = dr["VoiceMaterial3"].ToString();
                    returnValue.voice3 = dr["VoiceWear3"].ToString();
                    returnValue.state3 = dr["VoiceState3"].ToString();
                    returnValue.volume3 = dr["VoiceVolume3"].ToString();
                    returnValue.result3 = dr["VoiceResult3"].ToString();
                    returnValue.remark3 = dr["VoiceRemark3"].ToString();
                    returnValue.project4 = dr["VoiceItem4"].ToString();
                    returnValue.material4 = dr["VoiceMaterial4"].ToString();
                    returnValue.voice4 = dr["VoiceWear4"].ToString();
                    returnValue.state4 = dr["VoiceState4"].ToString();
                    returnValue.volume4 = dr["VoiceVolume4"].ToString();
                    returnValue.result4 = dr["VoiceResult4"].ToString();
                    returnValue.remark4 = dr["VoiceRemark4"].ToString();
                    returnValue.checkPurpose =int.Parse( dr["VoicePurpose"].ToString());
                    returnValue.checkPurposeText = dr["VoicePurposeExplain"].ToString();
                    returnValue.explain = dr["VoiceExplain"].ToString();

                    returnValue.SATVolumeBefore = dr["SATVolumeBefore"].ToString();
                    returnValue.SATEarBefore = dr["SATEarBefore"].ToString();
                    returnValue.SATLEarBefore = dr["SATLEarBefore"].ToString();
                    returnValue.SATREarBefore = dr["SATREarBefore"].ToString();

                    returnValue.SATVolumeAfter = dr["SATVolumeAfter"].ToString();
                    returnValue.SATEarAfter = dr["SATEarAfter"].ToString();
                    returnValue.SATLEarAfter = dr["SATLEarAfter"].ToString();
                    returnValue.SATREarAfter = dr["SATREarAfter"].ToString();

                    returnValue.WRSVolumeBefore = dr["WRSVolumeBefore"].ToString();
                    returnValue.WRSEarBefore = dr["WRSEarBefore"].ToString();
                    returnValue.WRSLEarBefore = dr["WRSLEarBefore"].ToString();
                    returnValue.WRSREarBefore = dr["WRSREarBefore"].ToString();

                    returnValue.WRSVolumeAfter = dr["WRSVolumeAfter"].ToString();
                    returnValue.WRSEarAfter = dr["WRSEarAfter"].ToString();
                    returnValue.WRSLEarAfter = dr["WRSLEarAfter"].ToString();
                    returnValue.WRSREarAfter = dr["WRSREarAfter"].ToString();

                    returnValue.project3 = dr["project3"].ToString();
                    returnValue.project3VolumeBefore = dr["project3VolumeBefore"].ToString();
                    returnValue.project3EarBefore = dr["project3EarBefore"].ToString();
                    returnValue.project3LEarBefore = dr["project3LEarBefore"].ToString();
                    returnValue.project3REarBefore = dr["project3REarBefore"].ToString();

                    returnValue.project3VolumeAfter = dr["project3VolumeAfter"].ToString();
                    returnValue.project3EarAfter = dr["project3EarAfter"].ToString();
                    returnValue.project3LEarAfter = dr["project3LEarAfter"].ToString();
                    returnValue.project3REarAfter = dr["project3REarAfter"].ToString();

                    returnValue.project4 = dr["project4"].ToString();
                    returnValue.project4VolumeBefore = dr["project4VolumeBefore"].ToString();
                    returnValue.project4EarBefore = dr["project4EarBefore"].ToString();
                    returnValue.project4LEarBefore = dr["project4LEarBefore"].ToString();
                    returnValue.project4REarBefore = dr["project4REarBefore"].ToString();

                    returnValue.project4VolumeAfter = dr["project4VolumeAfter"].ToString();
                    returnValue.project4EarAfter = dr["project4EarAfter"].ToString();
                    returnValue.project4LEarAfter = dr["project4LEarAfter"].ToString();
                    returnValue.project4REarAfter = dr["project4REarAfter"].ToString();


                    returnValue.audiologist = dr["CreateFileBy"].ToString();
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> CreateFileName = sDB.getStaffDataName(returnValue.audiologist);
                    returnValue.audiologistName = CreateFileName[1].ToString();
                }
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


    public string[] setHearingInspectionData(CreateHearingInspection StructData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        string PicUpdatestring = "";
        if (StructData.hearingtoneImg != null) {
            PicUpdatestring = "PureToneImg=@PureToneImg,  ";
        }
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE HearingInspection SET CheckDate=@CheckDate, StudentID=@StudentID, CheckType=@CheckType, Credibility=@Credibility, "+PicUpdatestring+
                    "HearingAids_R=@HearingAids_R, AidsBrand_R=@AidsBrand_R, AidsModel_R=@AidsModel_R, AidsOptionalTime_R=@AidsOptionalTime_R, " +
                    "AidsOptionalLocation_R=@AidsOptionalLocation_R, EEarHospital_R=@EEarHospital_R,EEarOpen_R=@EEarOpen_R, " +
                    "HearingAids_L=@HearingAids_L, AidsBrand_L=@AidsBrand_L, AidsModel_L=@AidsModel_L, AidsOptionalTime_L=@AidsOptionalTime_L, "+
                    "AidsOptionalLocation_L=@AidsOptionalLocation_L, EEarHospital_L=@EEarHospital_L, EEarOpen_L=@EEarOpen_L, ListenCheck=@ListenCheck, "+
                    "Earphones=@Earphones, NakedEar=@NakedEar, HearingAfter=@HearingAfter, PrueRDecibel=@PrueRDecibel, PrueLDecibel=@PrueLDecibel, "+
                    "EarEndoscopyRight=@EarEndoscopyRight, EarEndoscopyRExplain=@EarEndoscopyRExplain, EarEndoscopyLeft=@EarEndoscopyLeft, "+
                    "EarEndoscopyLExplain=@EarEndoscopyLExplain, TympanicCavity=@TympanicCavity, PatternRight=@PatternRight, PatternLeft=@PatternLeft, "+
                    "CanalRight=@CanalRight, CanalLeft=@CanalLeft, EardrumRight=@EardrumRight, EardrumLeft=@EardrumLeft, PressureRight=@PressureRight, "+
                    "PressureLeft=@PressureLeft, AidsDetectRight1=@AidsDetectRight1, AidsDetectRight2=@AidsDetectRight2, AidsDetectLeft1=@AidsDetectLeft1, "+
                    "AidsDetectLeft2=@AidsDetectLeft2, AidsDetect=@AidsDetect, VoiceMaterial1=@VoiceMaterial1, VoiceWear1=@VoiceWear1, VoiceState1=@VoiceState1, "+
                    "VoiceVolume1=@VoiceVolume1, VoiceResult1=@VoiceResult1, VoiceRemark1=@VoiceRemark1, VoiceMaterial2=@VoiceMaterial2, VoiceWear2=@VoiceWear2, "+
                    "VoiceState2=@VoiceState2, VoiceVolume2=@VoiceVolume2, VoiceResult2=@VoiceResult2, VoiceRemark2=@VoiceRemark2, VoiceItem3=@VoiceItem3, "+
                    "VoiceMaterial3=@VoiceMaterial3, VoiceWear3=@VoiceWear3, VoiceState3=@VoiceState3, VoiceVolume3=@VoiceVolume3, VoiceResult3=@VoiceResult3, "+
                    "VoiceRemark3=@VoiceRemark3, VoiceItem4=@VoiceItem4, VoiceMaterial4=@VoiceMaterial4, VoiceWear4=@VoiceWear4, VoiceState4=@VoiceState4, "+
                    "VoiceVolume4=@VoiceVolume4, VoiceResult4=@VoiceResult4, VoiceRemark4=@VoiceRemark4, VoicePurpose=@VoicePurpose, VoicePurposeExplain=@VoicePurposeExplain, "+
                    "VoiceExplain=@VoiceExplain, UpFileBy= @UpFileBy,SATVolumeBefore=@SATVolumeBefore,material1=@material1,SATVolumeAfter=@SATVolumeAfter , "+
                    "SATREarAfter=@SATREarAfter, SATLEarAfter=@SATLEarAfter, SATEarAfter=@SATEarAfter,"+
                    "SATREarBefore=@SATREarBefore, SATLEarBefore=@SATLEarBefore, SATEarBefore=@SATEarBefore, WRSVolumeBefore=@WRSVolumeBefore, "+
                    "WRSREarBefore=@WRSREarBefore, WRSLEarBefore=@WRSLEarBefore, WRSEarBefore=@WRSEarBefore, WRSVolumeAfter=@WRSVolumeAfter,  "+
                    "WRSREarAfter=@WRSREarAfter, WRSLEarAfter=@WRSLEarAfter, WRSEarAfter=@WRSEarAfter, project3=@project3,material3=@material3 ,  "+
                    "material2=@material2, project3VolumeBefore=@project3VolumeBefore, project3REarBefore=@project3REarBefore, "+
                    "project3LEarBefore=@project3LEarBefore, project3EarBefore=@project3EarBefore,project3VolumeAfter=@project3VolumeAfter,  "+
                    "project3REarAfter=@project3REarAfter, project3LEarAfter=@project3LEarAfter, project3EarAfter=@project3EarAfter,  "+
                    "project4=@project4, material4=@material4, project4VolumeBefore=@project4VolumeBefore,  "+
                    "project4REarBefore=@project4REarBefore, project4LEarBefore=@project4LEarBefore, project4EarBefore=@project4EarBefore,  "+
                    "project4VolumeAfter=@project4VolumeAfter, project4REarAfter=@project4REarAfter, "+
                    "project4EarAfter=@project4EarAfter, project4LEarAfter=@project4LEarAfter,  "+
                    "checkPurpose=@checkPurpose, explain=@explain, UpFileDate=(getDate()) WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.ID);
                cmd.Parameters.Add("@CheckDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.checkDate);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.studentID);
                cmd.Parameters.Add("@CheckType", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkMode);
                cmd.Parameters.Add("@Credibility", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.credibility);
                cmd.Parameters.Add("@HearingAids_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageR);
                cmd.Parameters.Add("@AidsBrand_R", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandR);
                cmd.Parameters.Add("@AidsModel_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelR);
                cmd.Parameters.Add("@AidsOptionalTime_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.buyingtimeR);
                cmd.Parameters.Add("@AidsOptionalLocation_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceR);
                cmd.Parameters.Add("@EEarHospital_R", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalR);
                cmd.Parameters.Add("@EEarOpen_R", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateR);
                cmd.Parameters.Add("@HearingAids_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanageL);
                cmd.Parameters.Add("@AidsBrand_L", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brandL);
                cmd.Parameters.Add("@AidsModel_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.modelL);
                cmd.Parameters.Add("@AidsOptionalTime_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingtimeL);
                cmd.Parameters.Add("@AidsOptionalLocation_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.buyingPlaceL);
                cmd.Parameters.Add("@EEarHospital_L", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.insertHospitalL);
                cmd.Parameters.Add("@EEarOpen_L", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.openHzDateL);
                cmd.Parameters.Add("@ListenCheck", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkInstrument);
                cmd.Parameters.Add("@Earphones", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.headphone);
                cmd.Parameters.Add("@NakedEar", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.nudetonety);
                cmd.Parameters.Add("@HearingAfter", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.hearingtone);
                cmd.Parameters.Add("@PureToneImg", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingtoneImg);
                cmd.Parameters.Add("@PrueRDecibel", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.toneR);
                cmd.Parameters.Add("@PrueLDecibel", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.toneL);
                cmd.Parameters.Add("@EarEndoscopyRight", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.hearingtoneR);
                cmd.Parameters.Add("@EarEndoscopyRExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingtoneRText);
                cmd.Parameters.Add("@EarEndoscopyLeft", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.hearingtoneL);
                cmd.Parameters.Add("@EarEndoscopyLExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingtoneLText);
                cmd.Parameters.Add("@TympanicCavity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingInstrument);
                cmd.Parameters.Add("@PatternRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingImgR);
                cmd.Parameters.Add("@PatternLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingImgL);
                cmd.Parameters.Add("@CanalRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingVolumeR);
                cmd.Parameters.Add("@CanalLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.hearingVolumeL);
                cmd.Parameters.Add("@EardrumRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.conformR);
                cmd.Parameters.Add("@EardrumLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.conformL);
                cmd.Parameters.Add("@PressureRight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.pressureR);
                cmd.Parameters.Add("@PressureLeft", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.pressureL);
                cmd.Parameters.Add("@AidsDetectRight1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aidsR);
                cmd.Parameters.Add("@AidsDetectRight2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.aidsdetectR);
                cmd.Parameters.Add("@AidsDetectLeft1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aidsL);
                cmd.Parameters.Add("@AidsDetectLeft2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.aidsdetectL);
                cmd.Parameters.Add("@AidsDetect", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aidsOther);
                cmd.Parameters.Add("@VoiceMaterial1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material1);
                cmd.Parameters.Add("@VoiceWear1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice1);
                cmd.Parameters.Add("@VoiceState1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state1);
                cmd.Parameters.Add("@VoiceVolume1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume1);
                cmd.Parameters.Add("@VoiceResult1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result1);
                cmd.Parameters.Add("@VoiceRemark1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark1);
                cmd.Parameters.Add("@VoiceMaterial2", SqlDbType.NVarChar).Value = StructData.material2;
                cmd.Parameters.Add("@VoiceWear2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice2);
                cmd.Parameters.Add("@VoiceState2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state2);
                cmd.Parameters.Add("@VoiceVolume2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume2);
                cmd.Parameters.Add("@VoiceResult2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result2);
                cmd.Parameters.Add("@VoiceRemark2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark2);
                cmd.Parameters.Add("@project3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3);
                cmd.Parameters.Add("@VoiceMaterial3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material3);
                cmd.Parameters.Add("@VoiceWear3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice3);
                cmd.Parameters.Add("@VoiceState3", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state3);
                cmd.Parameters.Add("@VoiceVolume3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume3);
                cmd.Parameters.Add("@VoiceResult3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result3);
                cmd.Parameters.Add("@VoiceRemark3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark3);
                cmd.Parameters.Add("@VoiceItem3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.VoiceItem3);
                cmd.Parameters.Add("@VoiceItem4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.VoiceItem4);
                cmd.Parameters.Add("@VoiceMaterial4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material4);
                cmd.Parameters.Add("@VoiceWear4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice4);
                cmd.Parameters.Add("@VoiceState4", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state4);
                cmd.Parameters.Add("@VoiceVolume4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume4);
                cmd.Parameters.Add("@VoiceResult4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result4);
                cmd.Parameters.Add("@VoiceRemark4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark4);
                cmd.Parameters.Add("@VoicePurpose", SqlDbType.TinyInt).Value = StructData.checkPurpose;
                cmd.Parameters.Add("@VoicePurposeExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkPurposeText);
                cmd.Parameters.Add("@VoiceExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.explain);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
              
                cmd.Parameters.Add("@material1 ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material1);
                cmd.Parameters.Add("@SATVolumeBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATVolumeBefore);
                cmd.Parameters.Add("@SATREarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATREarBefore);
                cmd.Parameters.Add("@SATLEarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATLEarBefore);
                cmd.Parameters.Add("@SATEarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATEarBefore);

                cmd.Parameters.Add("@SATVolumeAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATVolumeAfter);
                cmd.Parameters.Add("@SATREarAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATREarAfter);
                cmd.Parameters.Add("@SATLEarAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATLEarAfter);
                cmd.Parameters.Add("@SATEarAfter ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.SATEarAfter);
                cmd.Parameters.Add("@WRSVolumeBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSVolumeBefore);
                cmd.Parameters.Add("@WRSREarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSREarBefore);
                cmd.Parameters.Add("@WRSLEarBefore ", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSLEarBefore);
                cmd.Parameters.Add("@WRSEarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSEarBefore);
                cmd.Parameters.Add("@WRSVolumeAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSVolumeAfter);
                cmd.Parameters.Add("@WRSREarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSREarAfter);
                cmd.Parameters.Add("@WRSLEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSLEarAfter);
                cmd.Parameters.Add("@WRSEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.WRSEarAfter);
                cmd.Parameters.Add("@material3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material3);
                cmd.Parameters.Add("@material2", SqlDbType.TinyInt).Value = StructData.material2;
                cmd.Parameters.Add("@project3VolumeBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3VolumeBefore);
                cmd.Parameters.Add("@project3REarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3REarBefore);
                cmd.Parameters.Add("@project3LEarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3LEarBefore);
                cmd.Parameters.Add("@project3EarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3EarBefore);
                cmd.Parameters.Add("@project3VolumeAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3VolumeAfter);
                cmd.Parameters.Add("@project3REarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3REarAfter);
                cmd.Parameters.Add("@project3LEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3LEarAfter);
                cmd.Parameters.Add("@project3EarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project3EarAfter);
                cmd.Parameters.Add("@project4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4);
                cmd.Parameters.Add("@material4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material4);
                cmd.Parameters.Add("@project4VolumeBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4VolumeBefore);
                cmd.Parameters.Add("@project4REarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4REarBefore);
                cmd.Parameters.Add("@project4LEarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4LEarBefore);
                cmd.Parameters.Add("@project4EarBefore", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4EarBefore);
                cmd.Parameters.Add("@project4VolumeAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4VolumeAfter);
                cmd.Parameters.Add("@project4REarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4REarAfter);
                cmd.Parameters.Add("@project4LEarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4LEarAfter);
                cmd.Parameters.Add("@project4EarAfter", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project4EarAfter);
                cmd.Parameters.Add("@checkPurposeText", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.checkPurposeText);
                cmd.Parameters.Add("@checkPurpose", SqlDbType.TinyInt).Value = StructData.checkPurpose;
                cmd.Parameters.Add("@explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.explain);     
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] == "1")
                {
                    StudentDataBasic DateItem = new StudentDataBasic();
                    DateItem.studentID = StructData.studentID;
                    DateItem.assistmanageR = StructData.assistmanageR;
                    DateItem.brandR = StructData.brandR;
                    DateItem.modelR = StructData.modelR;
                    DateItem.buyingPlaceR = StructData.buyingPlaceR;
                    DateItem.buyingtimeR = StructData.buyingtimeR;
                    DateItem.insertHospitalR = StructData.insertHospitalR;
                    DateItem.openHzDateR = StructData.openHzDateR;
                    DateItem.assistmanageL = StructData.assistmanageL;
                    DateItem.brandL = StructData.brandL;
                    DateItem.modelL = StructData.modelL;
                    DateItem.buyingtimeL = StructData.buyingtimeL;
                    DateItem.buyingPlaceL = StructData.buyingPlaceL;
                    DateItem.insertHospitalL = StructData.insertHospitalL;
                    DateItem.openHzDateL = StructData.openHzDateL;
                    this.ComparisonAidsData(DateItem);
                }
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
    }

    private string SearchHearingInspectionConditionReturn(SearchHearingAssessment SearchStructure)
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

        if (SearchStructure.txtcheckDatestart != null && SearchStructure.txtcheckDateend != null && SearchStructure.txtcheckDatestart != DateBase && SearchStructure.txtcheckDateend != DateBase)
        {
            ConditionReturn += " AND HearingInspection.CheckDate BETWEEN (@sDateStart) AND (@sDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND HearingInspection.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchHearingInspectionDataCount(SearchHearingAssessment SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchHearingInspectionConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) " +
                            "FROM HearingInspection INNER JOIN StudentDatabase ON HearingInspection.StudentID=StudentDatabase.StudentID " +
                            "WHERE HearingInspection.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDatestart);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDateend);
                returnValue[0] = cmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
    }
    public List<SearchHearingInspectionResult> SearchHearingInspectionData(int indexpage, SearchHearingAssessment SearchStructure)
    {
        List<SearchHearingInspectionResult> returnValue = new List<SearchHearingInspectionResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchHearingInspectionConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY HearingInspection.ID DESC) " +
                            "AS RowNum, HearingInspection.*, StudentDatabase.StudentName, StudentDatabase.StudentBirthday " +
                            "FROM HearingInspection INNER JOIN StudentDatabase ON HearingInspection.StudentID=StudentDatabase.StudentID " +
                            "WHERE HearingInspection.isDeleted=0 and StudentDatabase.isDeleted=0 " + ConditionReturn + " ) AS NewTable " +
                            "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDatestart);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtcheckDateend);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchHearingInspectionResult addValue = new SearchHearingInspectionResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtcheckMode = dr["CheckType"].ToString();
                    addValue.txtcheckDate = DateTime.Parse(dr["checkDate"].ToString()).ToString("yyyy-MM-dd");

                    StaffDataBase sDB = new StaffDataBase();
                    List<string> CreateFileName = sDB.getStaffDataName(dr["CreateFileBy"].ToString());
                    addValue.txtaudiologistName = CreateFileName[1].ToString();
                    returnValue.Add(addValue);
                }
            }
            catch (Exception e)
            {
                SearchHearingInspectionResult addValue = new SearchHearingInspectionResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public List<setHearingTests> getHearingTestData(string studentID)
    {
        List<setHearingTests> returnValue = new List<setHearingTests>();
        string item1 = "語音察覺閾 (SAT)";
        string item2 = "字詞辨識率 (WRS)";
        string ConditionReturn = "";
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM HearingInspection WHERE isDeleted=0 AND StudentID=@StudentID " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(studentID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    setHearingTests addValue = new setHearingTests();
                    for (int i = 1; i <= 4; i++) {
                        string item = "";
                        if (i ==1) {
                            item = item1;
                        }
                        else if (i == 2) {
                            item = item2;
                        }
                        else {
                            item = dr["VoiceItem" + i].ToString();
                        }
                        if (dr["VoiceWear" + i].ToString() != "0" || dr["VoiceState" + i].ToString() != "0" || dr["VoiceMaterial" + i].ToString().Length >0)
                        {
                            addValue = new setHearingTests();
                            addValue.ID = dr["ID"].ToString();
                            addValue.checkDate = DateTime.Parse(dr["CheckDate"].ToString()).ToString("yyyy-MM-dd");
                            addValue.voice = dr["VoiceWear" + i].ToString();
                            addValue.state = dr["VoiceState" + i].ToString();
                            addValue.project = item;
                            addValue.material = dr["VoiceMaterial" + i].ToString();
                            addValue.volume = dr["VoiceVolume" + i].ToString();
                            addValue.result = dr["VoiceResult" + i].ToString();
                            addValue.remark = dr["VoiceRemark" + i].ToString();
                            addValue.itemNumber = i.ToString();
                            returnValue.Add(addValue);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                setHearingTests addValue = new setHearingTests();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] setHearingTestData(setHearingTests StructData){
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        string LimitValue = "";
        int itemNumber = Chk.CheckStringtoIntFunction(StructData.itemNumber);
        if (itemNumber > 0)
        {
            if (itemNumber > 2)
            {
                LimitValue = "VoiceItem" + itemNumber + "=@project,";
            }
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {

                try
                {
                    Sqlconn.Open();
                    string sql = "UPDATE HearingInspection SET VoiceWear" + itemNumber + "=@voice, VoiceState" + itemNumber + "=@state, " + LimitValue +
                        "VoiceMaterial" + itemNumber + "=@material, VoiceVolume" + itemNumber + "=@volume, VoiceResult" + itemNumber + "=@result, VoiceRemark" + itemNumber + "=@remark " +
                        "WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.ID);
                    cmd.Parameters.Add("@voice", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.voice);
                    cmd.Parameters.Add("@state", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.state);
                    cmd.Parameters.Add("@project", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.project);
                    cmd.Parameters.Add("@material", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.material);
                    cmd.Parameters.Add("@volume", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.volume);
                    cmd.Parameters.Add("@result", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result);
                    cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark);
                    cmd.Parameters.Add("@number", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.itemNumber);
                    returnValue[0] = cmd.ExecuteNonQuery().ToString();
                    Sqlconn.Close();
                }
                catch (Exception e)
                {
                    returnValue[0] = "-1";
                    returnValue[1] = e.Message;
                }
            }
        }
        else
        {
            returnValue[0] = "-1";
            returnValue[1] = "系統錯誤";
        }
        return returnValue;
    }
    private string SearchAidsManageDataConditionReturn(SearchAidsManageResult SearchStructure)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtaID != null)
        {
            ConditionReturn += " AND HearingAidsID like (@HearingAidsID) ";
        }
        if (SearchStructure.txtassistmanage != null && SearchStructure.txtassistmanage != "0")
        {
            ConditionReturn += " AND AidsType=(@AidsType) ";
        }
        if (SearchStructure.txtbrand != null && SearchStructure.txtbrand != "0")
        {
            ConditionReturn += " AND AidsBrand =(@AidsBrand) ";
        }
        if (SearchStructure.txtmodel != null)
        {
            ConditionReturn += " AND AidsModel like (@AidsModel) ";
        }
        if (SearchStructure.txtaNo != null)
        {
            ConditionReturn += " AND HearingAidsNo like(@HearingAidsNo) ";
        }
        if (SearchStructure.txtaSource != null)
        {
            ConditionReturn += " AND AidsSource like (@AidsSource) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchAidsManageDataCount(SearchAidsManageResult SearchData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        string ConditionReturn = this.SearchAidsManageDataConditionReturn(SearchData);
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM HearingAidsManage WHERE isDeleted=0" + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@AidsStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchData.txtaStatu);
                cmd.Parameters.Add("@HearingAidsID", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtaID) + "%";
                cmd.Parameters.Add("@AidsType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchData.txtassistmanage);
                cmd.Parameters.Add("@AidsBrand", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchData.txtbrand);
                cmd.Parameters.Add("@AidsModel", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtmodel) + "%";
                cmd.Parameters.Add("@HearingAidsNo", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtaNo) + "%";
                cmd.Parameters.Add("@AidsSource", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtaSource) + "%";
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
    public List<SearchAidsManageResult> SearchAidsManageData(int indexpage, SearchAidsManageResult SearchData)
    {
        List<SearchAidsManageResult> returnValue = new List<SearchAidsManageResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchAidsManageDataConditionReturn(SearchData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY ID DESC) " +
                            "AS RowNum, * FROM HearingAidsManage WHERE isDeleted=0 " + ConditionReturn + ") AS NewTable " +
                            "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@AidsStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchData.txtaStatu);
                cmd.Parameters.Add("@HearingAidsID", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtaID) + "%";
                cmd.Parameters.Add("@AidsType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchData.txtassistmanage);
                cmd.Parameters.Add("@AidsBrand", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchData.txtbrand);
                cmd.Parameters.Add("@AidsModel", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtmodel) + "%";
                cmd.Parameters.Add("@HearingAidsNo", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtaNo) + "%";
                cmd.Parameters.Add("@AidsSource", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchData.txtaSource) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchAidsManageResult addValue = new SearchAidsManageResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtaStatu = dr["AidsStatu"].ToString();
                    addValue.txtaID = dr["HearingAidsID"].ToString();
                    addValue.txtassistmanage = dr["AidsType"].ToString();
                    addValue.txtbrand = dr["AidsBrand"].ToString();
                    addValue.txtmodel = dr["AidsModel"].ToString();
                    addValue.txtaNo = dr["HearingAidsNo"].ToString();
                    addValue.txtaSource = dr["AidsSource"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchAidsManageResult addValue = new SearchAidsManageResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] createAidsManageData (createAidsManage StructData)
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
                string sql = "INSERT INTO HearingAidsManage (Unit, AidsStatu, HearingAidsID, AidsType, AidsBrand, AidsModel, HearingAidsNo, " +
                    "AidsSource, WriteDate, Remark, CreateFileBy, UpFileBy, UpFileDate) " +
                    "VALUES(@Unit, @AidsStatu, @HearingAidsID, @AidsType, @AidsBrand, @AidsModel, @HearingAidsNo, @AidsSource, @WriteDate, "+
                    "@Remark, @CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@AidsStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.aStatu);
                cmd.Parameters.Add("@HearingAidsID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aID);
                cmd.Parameters.Add("@AidsType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanage);
                cmd.Parameters.Add("@AidsBrand", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brand);
                cmd.Parameters.Add("@AidsModel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.model);
                cmd.Parameters.Add("@HearingAidsNo", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aNo);
                cmd.Parameters.Add("@AidsSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aSource);
                cmd.Parameters.Add("@WriteDate",SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.fillInDate);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('HearingAidsManage') AS cID";
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
    public string[] setAidsManageData(createAidsManage StructData)
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
                string sql = "UPDATE HearingAidsManage SET AidsStatu =@AidsStatu, HearingAidsID =@HearingAidsID, AidsType =@AidsType, "+
                    "AidsBrand =@AidsBrand, AidsModel =@AidsModel, HearingAidsNo =@HearingAidsNo, AidsSource =@AidsSource, "+
                    "WriteDate =@WriteDate, Remark =@Remark, UpFileBy=@UpFileBy, UpFileDate=getDate()  " +
                    "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.ID);
                cmd.Parameters.Add("@AidsStatu", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.aStatu);
                cmd.Parameters.Add("@HearingAidsID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aID);
                cmd.Parameters.Add("@AidsType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.assistmanage);
                cmd.Parameters.Add("@AidsBrand", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.brand);
                cmd.Parameters.Add("@AidsModel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.model);
                cmd.Parameters.Add("@HearingAidsNo", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aNo);
                cmd.Parameters.Add("@AidsSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aSource);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.fillInDate);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark);
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
    public createAidsManage getAidsManageData(string sID)
    {
        createAidsManage returnValue = new createAidsManage();
        returnValue.LoanList = new List<createAidsManageLoan>();
        returnValue.ServiceList = new List<createAidsManageService>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "SELECT * FROM HearingAidsManage WHERE ID=@ID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.aStatu = dr["AidsStatu"].ToString();
                    returnValue.aID = dr["HearingAidsID"].ToString();
                    returnValue.assistmanage = dr["AidsType"].ToString();
                    returnValue.brand = dr["AidsBrand"].ToString();
                    returnValue.model = dr["AidsModel"].ToString();
                    returnValue.aNo = dr["HearingAidsNo"].ToString();
                    returnValue.aSource = dr["AidsSource"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.remark = dr["Remark"].ToString();
                }
                dr.Close();

                sql = "SELECT HearingAidsManageLoanRecord.*,StudentDatabase.StudentName FROM HearingAidsManageLoanRecord " +
                      "LEFT JOIN StudentDatabase ON HearingAidsManageLoanRecord.OutPeople=StudentDatabase.StudentID "+
                      "WHERE ManageID=@mID AND HearingAidsManageLoanRecord.isDeleted=0 ORDER BY OutDate DESC";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@mID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnValue.ID);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    createAidsManageLoan addValue = new createAidsManageLoan();
                    addValue.ID= dr["ID"].ToString();
                    addValue.mID = dr["ManageID"].ToString();
                    addValue.lendDate = DateTime.Parse(dr["OutDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.lendPeople = dr["StudentName"].ToString();
                    addValue.lendPeopleID = dr["OutPeople"].ToString();
                    addValue.returnDate = DateTime.Parse(dr["ScheduledReturnDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.returnDate2 = DateTime.Parse(dr["ReturnDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.remark = dr["Remark"].ToString();
                    returnValue.LoanList.Add(addValue);
                }
                dr.Close();
                sql = "SELECT * FROM HearingAidsManageMaintenanceRecord WHERE ManageID=@mID AND isDeleted=0 ORDER BY MaintenanceDate DESC";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@mID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnValue.ID);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    createAidsManageService addValue2 = new createAidsManageService();
                    addValue2.ID = dr["ID"].ToString();
                    addValue2.mID = dr["ManageID"].ToString();
                    addValue2.serviceDate = DateTime.Parse(dr["MaintenanceDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.serviceItem = dr["Reason"].ToString();
                    addValue2.serviceFirm = dr["MaintenanceVendor"].ToString();
                    addValue2.serviceFirmDate = DateTime.Parse(dr["ReturnDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue2.sRemark = dr["Remark"].ToString();
                    returnValue.ServiceList.Add(addValue2);
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
    public string[] createLendingData(createAidsManageLoan StructData)
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
                string sql = "INSERT INTO HearingAidsManageLoanRecord (ManageID, OutDate, OutPeople, ScheduledReturnDate, ReturnDate, Remark, CreateFileBy, UpFileBy, UpFileDate) " +
                    "VALUES(@ManageID, @OutDate, @OutPeople, @ScheduledReturnDate, @ReturnDate, @Remark,@CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ManageID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.mID);
                cmd.Parameters.Add("@OutDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.lendDate);
                cmd.Parameters.Add("@OutPeople", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.lendPeopleID);
                cmd.Parameters.Add("@ScheduledReturnDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.returnDate);
                cmd.Parameters.Add("@ReturnDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.returnDate2);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark);
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

    public string[] setLendingData(createAidsManageLoan StructData)
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
                string sql = "UPDATE HearingAidsManageLoanRecord SET ReturnDate=@ReturnDate, Remark=@Remark, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                             "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.ID);
                cmd.Parameters.Add("@ReturnDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.returnDate2);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.remark);
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

    public string[] delLendingData(string SID)
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
                string sql = "UPDATE HearingAidsManageLoanRecord SET isDeleted=1,UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                             "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SID);
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

    public string[] createServiceData(createAidsManageService StructData)
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
                string sql = "INSERT INTO HearingAidsManageMaintenanceRecord (ManageID, MaintenanceDate, Reason, MaintenanceVendor, ReturnDate, Remark, CreateFileBy, UpFileBy, UpFileDate) " +
                    "VALUES(@ManageID, @MaintenanceDate, @Reason, @MaintenanceVendor, @ReturnDate, @Remark, @CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ManageID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.mID);
                cmd.Parameters.Add("@MaintenanceDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.serviceDate);
                cmd.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.serviceItem);
                cmd.Parameters.Add("@MaintenanceVendor", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.serviceFirm);
                cmd.Parameters.Add("@ReturnDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.serviceFirmDate);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.sRemark);
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
    public string[] setServiceData(createAidsManageService StructData)
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
                string sql = "UPDATE HearingAidsManageMaintenanceRecord SET ReturnDate=@ReturnDate, Remark=@Remark, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                                "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.ID);
                cmd.Parameters.Add("@ReturnDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.serviceFirmDate);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.sRemark);
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
    public string[] delServiceData(string SID)
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
                string sql = "UPDATE HearingAidsManageMaintenanceRecord SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                                "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SID);
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
    public string[] createFmAssessmentData(CreateFMAidsAssess StructData)
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
                string sql = "INSERT INTO FMAidsAssess (Unit, StudentID, Audiologist, AssessDate, AidsSource, AidsSourceExplain, " +
                    "TransmitterType, Channel, FMAidsModelR, FMAidsDPAIR, FMAidsProgramR, FMAudioProportionR, FMAidsUIR, FMAidsUITextR, " +
                    "FMAidsReceptorR, FMAidsReceptorVolumeR, FMAidsModelL, FMAidsDPAIL, FMAidsProgramL, FMAudioProportionL, " +
                    "FMAidsUIL, FMAidsUITextL, FMAidsReceptorL, FMAidsReceptorVolumeL, FML1_750hz, FML1_1khz, FML1_2khz, FML1_Average, " +
                    "FMR1_750hz, FMR1_1khz, FMR1_2khz, FMR1_Average, FML2_750hz, FML2_1khz, FML2_2khz, FML2_Average, FMR2_750hz, FMR2_1khz, " +
                    "FMR2_2khz, FMR2_Average, FML3_750hz, FML3_1khz, FML3_2khz, FML3_Average, FMR3_750hz, FMR3_1khz, FMR3_2khz, FMR3_Average, " +
                    "FML4_750hz, FML4_1khz, FML4_2khz, FML4_Average, FMR4_750hz, FMR4_1khz, FMR4_2khz, FMR4_Average, FML5_750hz, FML5_1khz, " +
                    "FML5_2khz, FML5_Average, FMR5_750hz, FMR5_1khz, FMR5_2khz, FMR5_Average, FML6_750hz, FMR6_750hz, FML6_1khz, FMR6_1khz, " +
                    "FML6_2khz, FMR6_2khz, FML6_Average, FMR6_Average, FMExplain, VoiceValue1, VoiceItem1TrueL1, VoiceItem1TrueL2, " +
                    "VoiceItem1TrueL3, VoiceItem1TrueR1, VoiceItem1TrueR2, VoiceItem1TrueR3, VoiceItem1TrueD1, VoiceItem1TrueD2, " +
                    "VoiceItem1TrueD3, VoiceValue2, VoiceItem2TrueL1, VoiceItem2TrueL2, VoiceItem2TrueL3, VoiceItem2TrueR1, VoiceItem2TrueR2, " +
                    "VoiceItem2TrueR3, VoiceItem2TrueD1, VoiceItem2TrueD2, VoiceItem2TrueD3, VoiceExplain, CreateFileBy, UpFileBy, UpFileDate) " +
                    "VALUES(@Unit, @StudentID, @Audiologist, @AssessDate, @AidsSource, @AidsSourceExplain, @TransmitterType, @Channel, " +
                    "@FMAidsModelR, @FMAidsDPAIR, @FMAidsProgramR, @FMAudioProportionR, @FMAidsUIR, @FMAidsUITextR, @FMAidsReceptorR, " +
                    "@FMAidsReceptorVolumeR, @FMAidsModelL, @FMAidsDPAIL, @FMAidsProgramL, @FMAudioProportionL, @FMAidsUIL, @FMAidsUITextL, " +
                    "@FMAidsReceptorL, @FMAidsReceptorVolumeL, @FML1_750hz, @FML1_1khz, @FML1_2khz, @FML1_Average, @FMR1_750hz, " +
                    "@FMR1_1khz, @FMR1_2khz, @FMR1_Average, @FML2_750hz, @FML2_1khz, @FML2_2khz, @FML2_Average, @FMR2_750hz, @FMR2_1khz, " +
                    "@FMR2_2khz, @FMR2_Average, @FML3_750hz, @FML3_1khz, @FML3_2khz, @FML3_Average, @FMR3_750hz, @FMR3_1khz, @FMR3_2khz, " +
                    "@FMR3_Average, @FML4_750hz, @FML4_1khz, @FML4_2khz, @FML4_Average, @FMR4_750hz, @FMR4_1khz, @FMR4_2khz, @FMR4_Average, " +
                    "@FML5_750hz, @FML5_1khz, @FML5_2khz, @FML5_Average, @FMR5_750hz, @FMR5_1khz, @FMR5_2khz, @FMR5_Average, @FML6_750hz, " +
                    "@FMR6_750hz, @FML6_1khz, @FMR6_1khz, @FML6_2khz, @FMR6_2khz, @FML6_Average, @FMR6_Average, @FMExplain, @VoiceValue1, " +
                    "@VoiceItem1TrueL1, @VoiceItem1TrueL2, @VoiceItem1TrueL3, @VoiceItem1TrueR1, @VoiceItem1TrueR2, @VoiceItem1TrueR3, " +
                    "@VoiceItem1TrueD1, @VoiceItem1TrueD2, @VoiceItem1TrueD3, @VoiceValue2, @VoiceItem2TrueL1, @VoiceItem2TrueL2, " +
                    "@VoiceItem2TrueL3, @VoiceItem2TrueR1, @VoiceItem2TrueR2, @VoiceItem2TrueR3, @VoiceItem2TrueD1, @VoiceItem2TrueD2, " +
                    "@VoiceItem2TrueD3, @VoiceExplain, @CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.studentID);
                cmd.Parameters.Add("@Audiologist", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@AssessDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StructData.assessDate);
                cmd.Parameters.Add("@AidsSource", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.aSource);
                cmd.Parameters.Add("@AidsSourceExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aSourceText);
                cmd.Parameters.Add("@TransmitterType", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmModel);
                cmd.Parameters.Add("@Channel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmChannel);
                cmd.Parameters.Add("@FMAidsModelR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAidstypeR);
                cmd.Parameters.Add("@FMAidsDPAIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.DPAIsettingR);
                cmd.Parameters.Add("@FMAidsProgramR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmProgramR);
                cmd.Parameters.Add("@FMAudioProportionR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAudioR);
                cmd.Parameters.Add("@FMAidsUIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.fmUIR);
                cmd.Parameters.Add("@FMAidsUITextR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmUITextR);
                cmd.Parameters.Add("@FMAidsReceptorR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmReceptorR);
                cmd.Parameters.Add("@FMAidsReceptorVolumeR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmVolumeR);
                cmd.Parameters.Add("@FMAidsModelL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAidstypeL);
                cmd.Parameters.Add("@FMAidsDPAIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.DPAIsettingL);
                cmd.Parameters.Add("@FMAidsProgramL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmProgramL);
                cmd.Parameters.Add("@FMAudioProportionL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAudioL);
                cmd.Parameters.Add("@FMAidsUIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.fmUIL);
                cmd.Parameters.Add("@FMAidsUITextL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmUITextL);
                cmd.Parameters.Add("@FMAidsReceptorL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmReceptorL);
                cmd.Parameters.Add("@FMAidsReceptorVolumeL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmVolumeL);
                cmd.Parameters.Add("@FML1_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_1);
                cmd.Parameters.Add("@FML1_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_2);
                cmd.Parameters.Add("@FML1_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_3);
                cmd.Parameters.Add("@FML1_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_4);
                cmd.Parameters.Add("@FMR1_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_1);
                cmd.Parameters.Add("@FMR1_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_2);
                cmd.Parameters.Add("@FMR1_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_3);
                cmd.Parameters.Add("@FMR1_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_4);
                cmd.Parameters.Add("@FML2_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_1);
                cmd.Parameters.Add("@FML2_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_2);
                cmd.Parameters.Add("@FML2_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_3);
                cmd.Parameters.Add("@FML2_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_4);
                cmd.Parameters.Add("@FMR2_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_1);
                cmd.Parameters.Add("@FMR2_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_2);
                cmd.Parameters.Add("@FMR2_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_3);
                cmd.Parameters.Add("@FMR2_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_4);
                cmd.Parameters.Add("@FML3_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_1);
                cmd.Parameters.Add("@FML3_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_2);
                cmd.Parameters.Add("@FML3_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_3);
                cmd.Parameters.Add("@FML3_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_4);
                cmd.Parameters.Add("@FMR3_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_1);
                cmd.Parameters.Add("@FMR3_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_2);
                cmd.Parameters.Add("@FMR3_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_3);
                cmd.Parameters.Add("@FMR3_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_4);
                cmd.Parameters.Add("@FML4_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_1);
                cmd.Parameters.Add("@FML4_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_2);
                cmd.Parameters.Add("@FML4_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_3);
                cmd.Parameters.Add("@FML4_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_4);
                cmd.Parameters.Add("@FMR4_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_1);
                cmd.Parameters.Add("@FMR4_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_2);
                cmd.Parameters.Add("@FMR4_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_3);
                cmd.Parameters.Add("@FMR4_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_4);
                cmd.Parameters.Add("@FML5_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_1);
                cmd.Parameters.Add("@FML5_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_2);
                cmd.Parameters.Add("@FML5_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_3);
                cmd.Parameters.Add("@FML5_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_4);
                cmd.Parameters.Add("@FMR5_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_1);
                cmd.Parameters.Add("@FMR5_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_2);
                cmd.Parameters.Add("@FMR5_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_3);
                cmd.Parameters.Add("@FMR5_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_4);
                cmd.Parameters.Add("@FML6_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_1);
                cmd.Parameters.Add("@FMR6_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_2);
                cmd.Parameters.Add("@FML6_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_3);
                cmd.Parameters.Add("@FMR6_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_4);
                cmd.Parameters.Add("@FML6_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_1);
                cmd.Parameters.Add("@FMR6_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_2);
                cmd.Parameters.Add("@FML6_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_3);
                cmd.Parameters.Add("@FMR6_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_4);
                cmd.Parameters.Add("@FMExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result);
                cmd.Parameters.Add("@VoiceValue1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.testmaterial);
                cmd.Parameters.Add("@VoiceItem1TrueL1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1L_1);
                cmd.Parameters.Add("@VoiceItem1TrueL2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1L_2);
                cmd.Parameters.Add("@VoiceItem1TrueL3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1L_3);
                cmd.Parameters.Add("@VoiceItem1TrueR1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1R_1);
                cmd.Parameters.Add("@VoiceItem1TrueR2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1R_2);
                cmd.Parameters.Add("@VoiceItem1TrueR3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1R_3);
                cmd.Parameters.Add("@VoiceItem1TrueD1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1D_1);
                cmd.Parameters.Add("@VoiceItem1TrueD2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1D_2);
                cmd.Parameters.Add("@VoiceItem1TrueD3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1D_3);
                cmd.Parameters.Add("@VoiceValue2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.testmaterial2);
                cmd.Parameters.Add("@VoiceItem2TrueL1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2L_1);
                cmd.Parameters.Add("@VoiceItem2TrueL2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2L_2);
                cmd.Parameters.Add("@VoiceItem2TrueL3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2L_3);
                cmd.Parameters.Add("@VoiceItem2TrueR1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2R_1);
                cmd.Parameters.Add("@VoiceItem2TrueR2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2R_2);
                cmd.Parameters.Add("@VoiceItem2TrueR3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2R_3);
                cmd.Parameters.Add("@VoiceItem2TrueD1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2D_1);
                cmd.Parameters.Add("@VoiceItem2TrueD2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2D_2);
                cmd.Parameters.Add("@VoiceItem2TrueD3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2D_3);
                cmd.Parameters.Add("@VoiceExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result2);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('FMAidsAssess') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["cID"].ToString();
                    }
                    dr.Close();

                    this.ComparisonFMAidsData(StructData);
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
    public CreateFMAidsAssess getFmAssessmentData(string SID) {
        CreateFMAidsAssess returnValue = new CreateFMAidsAssess();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "SELECT FMAidsAssess.*,StudentDatabase.StudentName,StudentDatabase.StudentBirthday FROM FMAidsAssess " +
                    "INNER JOIN StudentDatabase ON FMAidsAssess.StudentID=StudentDatabase.StudentID " +
                    "WHERE FMAidsAssess.ID=@ID AND FMAidsAssess.isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.audiologistID = dr["Audiologist"].ToString();
                    List<string> CreateFileName = sDB.getStaffDataName(returnValue.audiologistID);
                    returnValue.audiologist = CreateFileName[1];
                    returnValue.assessDate = DateTime.Parse(dr["AssessDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.aSource = dr["AidsSource"].ToString();
                    returnValue.aSourceText = dr["AidsSourceExplain"].ToString();
                    returnValue.fmModel = dr["TransmitterType"].ToString();
                    returnValue.fmChannel = dr["Channel"].ToString();
                    returnValue.fmAidstypeR = dr["FMAidsModelR"].ToString();
                    returnValue.DPAIsettingR = dr["FMAidsDPAIR"].ToString();
                    returnValue.fmProgramR = dr["FMAidsProgramR"].ToString();
                    returnValue.fmAudioR = dr["FMAudioProportionR"].ToString();
                    returnValue.fmUIR = dr["FMAidsUIR"].ToString();
                    returnValue.fmUITextR = dr["FMAidsUITextR"].ToString();
                    returnValue.fmReceptorR = dr["FMAidsReceptorR"].ToString();
                    returnValue.fmVolumeR = dr["FMAidsReceptorVolumeR"].ToString();
                    returnValue.fmAidstypeL = dr["FMAidsModelL"].ToString();
                    returnValue.DPAIsettingL = dr["FMAidsDPAIL"].ToString();
                    returnValue.fmProgramL = dr["FMAidsProgramL"].ToString();
                    returnValue.fmAudioL = dr["FMAudioProportionL"].ToString();
                    returnValue.fmUIL = dr["FMAidsUIL"].ToString();
                    returnValue.fmUITextL = dr["FMAidsUITextL"].ToString();
                    returnValue.fmReceptorL = dr["FMAidsReceptorL"].ToString();
                    returnValue.fmVolumeL = dr["FMAidsReceptorVolumeL"].ToString();
                    returnValue.fm1L_1 = dr["FML1_750hz"].ToString();
                    returnValue.fm1L_2 = dr["FML1_1khz"].ToString();
                    returnValue.fm1L_3 = dr["FML1_2khz"].ToString();
                    returnValue.fm1L_4 = dr["FML1_Average"].ToString();
                    returnValue.fm1R_1 = dr["FMR1_750hz"].ToString();
                    returnValue.fm1R_2 = dr["FMR1_1khz"].ToString();
                    returnValue.fm1R_3 = dr["FMR1_2khz"].ToString();
                    returnValue.fm1R_4 = dr["FMR1_Average"].ToString();
                    returnValue.fm2L_1 = dr["FML2_750hz"].ToString();
                    returnValue.fm2L_2 = dr["FML2_1khz"].ToString();
                    returnValue.fm2L_3 = dr["FML2_2khz"].ToString();
                    returnValue.fm2L_4 = dr["FML2_Average"].ToString();
                    returnValue.fm2R_1 = dr["FMR2_750hz"].ToString();
                    returnValue.fm2R_2 = dr["FMR2_1khz"].ToString();
                    returnValue.fm2R_3 = dr["FMR2_2khz"].ToString();
                    returnValue.fm2R_4 = dr["FMR2_Average"].ToString();
                    returnValue.fm3L_1 = dr["FML3_750hz"].ToString();
                    returnValue.fm3L_2 = dr["FML3_1khz"].ToString();
                    returnValue.fm3L_3 = dr["FML3_2khz"].ToString();
                    returnValue.fm3L_4 = dr["FML3_Average"].ToString();
                    returnValue.fm3R_1 = dr["FMR3_750hz"].ToString();
                    returnValue.fm3R_2 = dr["FMR3_1khz"].ToString();
                    returnValue.fm3R_3 = dr["FMR3_2khz"].ToString();
                    returnValue.fm3R_4 = dr["FMR3_Average"].ToString();
                    returnValue.fm4L_1 = dr["FML4_750hz"].ToString();
                    returnValue.fm4L_2 = dr["FML4_1khz"].ToString();
                    returnValue.fm4L_3 = dr["FML4_2khz"].ToString();
                    returnValue.fm4L_4 = dr["FML4_Average"].ToString();
                    returnValue.fm4R_1 = dr["FMR4_750hz"].ToString();
                    returnValue.fm4R_2 = dr["FMR4_1khz"].ToString();
                    returnValue.fm4R_3 = dr["FMR4_2khz"].ToString();
                    returnValue.fm4R_4 = dr["FMR4_Average"].ToString();
                    returnValue.fm5L_1 = dr["FML5_750hz"].ToString();
                    returnValue.fm5L_2 = dr["FML5_1khz"].ToString();
                    returnValue.fm5L_3 = dr["FML5_2khz"].ToString();
                    returnValue.fm5L_4 = dr["FML5_Average"].ToString();
                    returnValue.fm5R_1 = dr["FMR5_750hz"].ToString();
                    returnValue.fm5R_2 = dr["FMR5_1khz"].ToString();
                    returnValue.fm5R_3 = dr["FMR5_2khz"].ToString();
                    returnValue.fm5R_4 = dr["FMR5_Average"].ToString();
                    returnValue.fm6L_1 = dr["FML6_750hz"].ToString();
                    returnValue.fm6L_2 = dr["FMR6_750hz"].ToString();
                    returnValue.fm6L_3 = dr["FML6_1khz"].ToString();
                    returnValue.fm6L_4 = dr["FMR6_1khz"].ToString();
                    returnValue.fm6R_1 = dr["FML6_2khz"].ToString();
                    returnValue.fm6R_2 = dr["FMR6_2khz"].ToString();
                    returnValue.fm6R_3 = dr["FML6_Average"].ToString();
                    returnValue.fm6R_4 = dr["FMR6_Average"].ToString();
                    returnValue.result = dr["FMExplain"].ToString();
                    returnValue.testmaterial = dr["VoiceValue1"].ToString();
                    returnValue.true1L_1 = dr["VoiceItem1TrueL1"].ToString();
                    returnValue.true1L_2 = dr["VoiceItem1TrueL2"].ToString();
                    returnValue.true1L_3 = dr["VoiceItem1TrueL3"].ToString();
                    returnValue.true1R_1 = dr["VoiceItem1TrueR1"].ToString();
                    returnValue.true1R_2 = dr["VoiceItem1TrueR2"].ToString();
                    returnValue.true1R_3 = dr["VoiceItem1TrueR3"].ToString();
                    returnValue.true1D_1 = dr["VoiceItem1TrueD1"].ToString();
                    returnValue.true1D_2 = dr["VoiceItem1TrueD2"].ToString();
                    returnValue.true1D_3 = dr["VoiceItem1TrueD3"].ToString();
                    returnValue.testmaterial2 = dr["VoiceValue2"].ToString();
                    returnValue.true2L_1 = dr["VoiceItem2TrueL1"].ToString();
                    returnValue.true2L_2 = dr["VoiceItem2TrueL2"].ToString();
                    returnValue.true2L_3 = dr["VoiceItem2TrueL3"].ToString();
                    returnValue.true2R_1 = dr["VoiceItem2TrueR1"].ToString();
                    returnValue.true2R_2 = dr["VoiceItem2TrueR2"].ToString();
                    returnValue.true2R_3 = dr["VoiceItem2TrueR3"].ToString();
                    returnValue.true2D_1 = dr["VoiceItem2TrueD1"].ToString();
                    returnValue.true2D_2 = dr["VoiceItem2TrueD2"].ToString();
                    returnValue.true2D_3 = dr["VoiceItem2TrueD3"].ToString();
                    returnValue.result2 = dr["VoiceExplain"].ToString();

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
    private CreateStudentAidsUse getStudentFMUseNew(string StudentID)
    {
        CreateStudentAidsUse returnValue = new CreateStudentAidsUse();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT TOP 1 * FROM HearingCaseAidsUseRecord  " +
                             "WHERE isDeleted=0 AND StudentID=@StudentID ORDER BY ID DESC";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StudentID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.caseUnit = dr["Unit"].ToString();
                    returnValue.studentID = dr["StudentID"].ToString();
                    /*returnValue.studentName = dr["StudentName"].ToString();
                    returnValue.studentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.studentAge = dr["StudentAge"].ToString();
                    returnValue.studentMonth = dr["StudentMonth"].ToString();
                    returnValue.assistmanageR = dr["HearingAids_R"].ToString();
                    returnValue.brandR = dr["AidsBrand_R"].ToString();
                    returnValue.modelR = dr["AidsModel_R"].ToString();
                    returnValue.buyingPlaceR = dr["AidsOptionalLocation_R"].ToString();
                    returnValue.buyingtimeR = DateTime.Parse(dr["AidsOptionalTime_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.insertHospitalR = dr["EEarHospital_R"].ToString();
                    returnValue.openHzDateR = DateTime.Parse(dr["EEarOpen_R"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sourceR = dr["AidsSource_R"].ToString();
                    returnValue.sourceTextR = dr["AidsSourceText_R"].ToString();
                    returnValue.assistmanageL = dr["HearingAids_L"].ToString();
                    returnValue.brandL = dr["AidsBrand_L"].ToString();
                    returnValue.modelL = dr["AidsModel_L"].ToString();
                    returnValue.buyingtimeL = DateTime.Parse(dr["AidsOptionalTime_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.buyingPlaceL = dr["AidsOptionalLocation_L"].ToString();
                    returnValue.insertHospitalL = dr["EEarHospital_L"].ToString();
                    returnValue.openHzDateL = DateTime.Parse(dr["EEarOpen_L"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.sourceL = dr["AidsSource_L"].ToString();
                    returnValue.sourceTextL = dr["AidsSourceText_L"].ToString();
                    returnValue.gainDate = DateTime.Parse(dr["AcquisitionDate"].ToString()).ToString("yyyy-MM-dd");*/
                    returnValue.fmAidssource = dr["FMSourceAids"].ToString();
                    returnValue.fmAidstype = dr["FMTypeAids"].ToString();
                    returnValue.fmBrand = dr["FMAidsBrand"].ToString();
                    returnValue.fmModel = dr["FMAidsModel"].ToString();
                    returnValue.fmChannel = dr["FMAidsChannel"].ToString();
                    returnValue.fmAidstypeR = dr["FMAidsModelR"].ToString();
                    returnValue.DPAIsettingR = dr["FMAidsDPAIR"].ToString();
                    returnValue.fmProgramR = dr["FMAidsProgramR"].ToString();
                    returnValue.fmAudioR = dr["FMAudioProportionR"].ToString();
                    returnValue.fmUIR = dr["FMAidsUIR"].ToString();
                    returnValue.fmUITextR = dr["FMAidsUITextR"].ToString();
                    returnValue.fmReceptorR = dr["FMAidsReceptorR"].ToString();
                    returnValue.fmVolumeR = dr["FMAidsReceptorVolumeR"].ToString();
                    returnValue.fmGainR = dr["FMAidsGainR"].ToString();
                    returnValue.fmAidstypeL = dr["FMAidsModelL"].ToString();
                    returnValue.DPAIsettingL = dr["FMAidsDPAIL"].ToString();
                    returnValue.fmProgramL = dr["FMAidsProgramL"].ToString();
                    returnValue.fmAudioL = dr["FMAudioProportionL"].ToString();
                    returnValue.fmUIL = dr["FMAidsUIL"].ToString();
                    returnValue.fmUITextL = dr["FMAidsUITextL"].ToString();
                    returnValue.fmReceptorL = dr["FMAidsReceptorL"].ToString();
                    returnValue.fmVolumeL = dr["FMAidsReceptorVolumeL"].ToString();
                    returnValue.fmGainL = dr["FMAidsGainL"].ToString();
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
    public string[] ComparisonFMAidsData(CreateFMAidsAssess DateItem)
    {
        string[] returnValue = new string[2];
        try
        {
            CreateStudentAidsUse FMNewData = this.getStudentFMUseNew(DateItem.studentID);
            if (DateItem.fmChannel != FMNewData.fmChannel || DateItem.fmAidstypeR != FMNewData.fmAidstypeR || DateItem.DPAIsettingR != FMNewData.DPAIsettingR || DateItem.fmProgramR != FMNewData.fmProgramR || DateItem.fmAudioR != FMNewData.fmAudioR || DateItem.fmUIR != FMNewData.fmUIR || DateItem.fmUITextR != FMNewData.fmUITextR || DateItem.fmReceptorR != FMNewData.fmReceptorR || DateItem.fmVolumeR != FMNewData.fmVolumeR || DateItem.fmAidstypeL != FMNewData.fmAidstypeL || DateItem.DPAIsettingL != FMNewData.DPAIsettingL || DateItem.fmProgramL != FMNewData.fmProgramL || DateItem.fmAudioL != FMNewData.fmAudioL || DateItem.fmUIL != FMNewData.fmUIL || DateItem.fmUITextL != FMNewData.fmUITextL || DateItem.fmReceptorL != FMNewData.fmReceptorL || DateItem.fmVolumeL != FMNewData.fmVolumeL)
            {
                CreateStudentAidsUse AidsUseData = new CreateStudentAidsUse();
                AidsUseData.studentID = DateItem.studentID;
                AidsUseData.fmChannel=DateItem.fmChannel;
                AidsUseData.fmAidstypeR=DateItem.fmAidstypeR;
                AidsUseData.DPAIsettingR=DateItem.DPAIsettingR;
                AidsUseData.fmProgramR=DateItem.fmProgramR;
                AidsUseData.fmAudioR=DateItem.fmAudioR;
                AidsUseData.fmUIR=DateItem.fmUIR;
                AidsUseData.fmUITextR=DateItem.fmUITextR;
                AidsUseData.fmReceptorR=DateItem.fmReceptorR;
                AidsUseData.fmVolumeR=DateItem.fmVolumeR;
                AidsUseData.fmAidstypeL=DateItem.fmAidstypeL;
                AidsUseData.DPAIsettingL=DateItem.DPAIsettingL;
                AidsUseData.fmProgramL=DateItem.fmProgramL;
                AidsUseData.fmAudioL=DateItem.fmAudioL;
                AidsUseData.fmUIL=DateItem.fmUIL;
                AidsUseData.fmUITextL=DateItem.fmUITextL;
                AidsUseData.fmReceptorL=DateItem.fmReceptorL;
                AidsUseData.fmVolumeL=DateItem.fmVolumeL;

                string[] item = this.createStudentAidsUse(AidsUseData);
            }
        }
        catch (Exception e)
        {
            returnValue[0] = "-1";
            returnValue[1] = e.Message.ToString();
        }
        return returnValue;
    }
    public string[] setFmAssessmentData(CreateFMAidsAssess StructData)
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
                string sql = "UPDATE FMAidsAssess SET AidsSource = @AidsSource, AidsSourceExplain = @AidsSourceExplain, "+
                    "TransmitterType = @TransmitterType, Channel = @Channel, FMAidsModelR = @FMAidsModelR, FMAidsDPAIR = @FMAidsDPAIR, " +
                    "FMAidsProgramR = @FMAidsProgramR, FMAudioProportionR = @FMAudioProportionR, FMAidsUIR = @FMAidsUIR, " +
                    "FMAidsUITextR = @FMAidsUITextR, FMAidsReceptorR = @FMAidsReceptorR, FMAidsReceptorVolumeR = @FMAidsReceptorVolumeR, " +
                    "FMAidsModelL = @FMAidsModelL, FMAidsDPAIL = @FMAidsDPAIL, FMAidsProgramL = @FMAidsProgramL, " +
                    "FMAudioProportionL = @FMAudioProportionL, FMAidsUIL = @FMAidsUIL, FMAidsUITextL = @FMAidsUITextL, " +
                    "FMAidsReceptorL = @FMAidsReceptorL, FMAidsReceptorVolumeL = @FMAidsReceptorVolumeL, FML1_750hz = @FML1_750hz, " +
                    "FML1_1khz = @FML1_1khz, FML1_2khz = @FML1_2khz, FML1_Average = @FML1_Average, FMR1_750hz = @FMR1_750hz, " +
                    "FMR1_1khz = @FMR1_1khz, FMR1_2khz = @FMR1_2khz, FMR1_Average = @FMR1_Average, FML2_750hz = @FML2_750hz, " +
                    "FML2_1khz = @FML2_1khz, FML2_2khz = @FML2_2khz, FML2_Average = @FML2_Average, FMR2_750hz = @FMR2_750hz, " +
                    "FMR2_1khz = @FMR2_1khz, FMR2_2khz = @FMR2_2khz, FMR2_Average = @FMR2_Average, FML3_750hz = @FML3_750hz, " +
                    "FML3_1khz = @FML3_1khz, FML3_2khz = @FML3_2khz, FML3_Average = @FML3_Average, FMR3_750hz = @FMR3_750hz, " +
                    "FMR3_1khz = @FMR3_1khz, FMR3_2khz = @FMR3_2khz, FMR3_Average = @FMR3_Average, FML4_750hz = @FML4_750hz, " +
                    "FML4_1khz = @FML4_1khz, FML4_2khz = @FML4_2khz, FML4_Average = @FML4_Average, FMR4_750hz = @FMR4_750hz, " +
                    "FMR4_1khz = @FMR4_1khz, FMR4_2khz = @FMR4_2khz, FMR4_Average = @FMR4_Average, FML5_750hz = @FML5_750hz, " +
                    "FML5_1khz = @FML5_1khz, FML5_2khz = @FML5_2khz, FML5_Average = @FML5_Average, FMR5_750hz = @FMR5_750hz, " +
                    "FMR5_1khz = @FMR5_1khz, FMR5_2khz = @FMR5_2khz, FMR5_Average = @FMR5_Average, FML6_750hz = @FML6_750hz, " +
                    "FMR6_750hz = @FMR6_750hz, FML6_1khz = @FML6_1khz, FMR6_1khz = @FMR6_1khz, FML6_2khz = @FML6_2khz, FMR6_2khz = @FMR6_2khz, " +
                    "FML6_Average = @FML6_Average, FMR6_Average = @FMR6_Average, FMExplain = @FMExplain, VoiceValue1 = @VoiceValue1, " +
                    "VoiceItem1TrueL1 = @VoiceItem1TrueL1, VoiceItem1TrueL2 = @VoiceItem1TrueL2, VoiceItem1TrueL3 = @VoiceItem1TrueL3, " +
                    "VoiceItem1TrueR1 = @VoiceItem1TrueR1, VoiceItem1TrueR2 = @VoiceItem1TrueR2, VoiceItem1TrueR3 = @VoiceItem1TrueR3, " +
                    "VoiceItem1TrueD1 = @VoiceItem1TrueD1, VoiceItem1TrueD2 = @VoiceItem1TrueD2, VoiceItem1TrueD3 = @VoiceItem1TrueD3, " +
                    "VoiceValue2 = @VoiceValue2, VoiceItem2TrueL1 = @VoiceItem2TrueL1, VoiceItem2TrueL2 = @VoiceItem2TrueL2, " +
                    "VoiceItem2TrueL3 = @VoiceItem2TrueL3, VoiceItem2TrueR1 = @VoiceItem2TrueR1, VoiceItem2TrueR2 = @VoiceItem2TrueR2, " +
                    "VoiceItem2TrueR3 = @VoiceItem2TrueR3, VoiceItem2TrueD1 = @VoiceItem2TrueD1, VoiceItem2TrueD2 = @VoiceItem2TrueD2, " +
                    "VoiceItem2TrueD3 = @VoiceItem2TrueD3, VoiceExplain = @VoiceExplain, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                    "WHERE ID=@ID" ;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.ID);
                cmd.Parameters.Add("@AidsSource", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.aSource);
                cmd.Parameters.Add("@AidsSourceExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.aSourceText);
                cmd.Parameters.Add("@TransmitterType", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmModel);
                cmd.Parameters.Add("@Channel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmChannel);
                cmd.Parameters.Add("@FMAidsModelR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAidstypeR);
                cmd.Parameters.Add("@FMAidsDPAIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.DPAIsettingR);
                cmd.Parameters.Add("@FMAidsProgramR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmProgramR);
                cmd.Parameters.Add("@FMAudioProportionR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAudioR);
                cmd.Parameters.Add("@FMAidsUIR", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.fmUIR);
                cmd.Parameters.Add("@FMAidsUITextR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmUITextR);
                cmd.Parameters.Add("@FMAidsReceptorR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmReceptorR);
                cmd.Parameters.Add("@FMAidsReceptorVolumeR", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmVolumeR);
                cmd.Parameters.Add("@FMAidsModelL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAidstypeL);
                cmd.Parameters.Add("@FMAidsDPAIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.DPAIsettingL);
                cmd.Parameters.Add("@FMAidsProgramL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmProgramL);
                cmd.Parameters.Add("@FMAudioProportionL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmAudioL);
                cmd.Parameters.Add("@FMAidsUIL", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.fmUIL);
                cmd.Parameters.Add("@FMAidsUITextL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmUITextL);
                cmd.Parameters.Add("@FMAidsReceptorL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmReceptorL);
                cmd.Parameters.Add("@FMAidsReceptorVolumeL", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.fmVolumeL);
                cmd.Parameters.Add("@FML1_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_1);
                cmd.Parameters.Add("@FML1_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_2);
                cmd.Parameters.Add("@FML1_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_3);
                cmd.Parameters.Add("@FML1_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1L_4);
                cmd.Parameters.Add("@FMR1_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_1);
                cmd.Parameters.Add("@FMR1_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_2);
                cmd.Parameters.Add("@FMR1_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_3);
                cmd.Parameters.Add("@FMR1_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm1R_4);
                cmd.Parameters.Add("@FML2_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_1);
                cmd.Parameters.Add("@FML2_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_2);
                cmd.Parameters.Add("@FML2_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_3);
                cmd.Parameters.Add("@FML2_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2L_4);
                cmd.Parameters.Add("@FMR2_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_1);
                cmd.Parameters.Add("@FMR2_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_2);
                cmd.Parameters.Add("@FMR2_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_3);
                cmd.Parameters.Add("@FMR2_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm2R_4);
                cmd.Parameters.Add("@FML3_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_1);
                cmd.Parameters.Add("@FML3_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_2);
                cmd.Parameters.Add("@FML3_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_3);
                cmd.Parameters.Add("@FML3_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3L_4);
                cmd.Parameters.Add("@FMR3_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_1);
                cmd.Parameters.Add("@FMR3_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_2);
                cmd.Parameters.Add("@FMR3_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_3);
                cmd.Parameters.Add("@FMR3_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm3R_4);
                cmd.Parameters.Add("@FML4_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_1);
                cmd.Parameters.Add("@FML4_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_2);
                cmd.Parameters.Add("@FML4_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_3);
                cmd.Parameters.Add("@FML4_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4L_4);
                cmd.Parameters.Add("@FMR4_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_1);
                cmd.Parameters.Add("@FMR4_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_2);
                cmd.Parameters.Add("@FMR4_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_3);
                cmd.Parameters.Add("@FMR4_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm4R_4);
                cmd.Parameters.Add("@FML5_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_1);
                cmd.Parameters.Add("@FML5_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_2);
                cmd.Parameters.Add("@FML5_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_3);
                cmd.Parameters.Add("@FML5_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5L_4);
                cmd.Parameters.Add("@FMR5_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_1);
                cmd.Parameters.Add("@FMR5_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_2);
                cmd.Parameters.Add("@FMR5_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_3);
                cmd.Parameters.Add("@FMR5_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm5R_4);
                cmd.Parameters.Add("@FML6_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_1);
                cmd.Parameters.Add("@FMR6_750hz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_2);
                cmd.Parameters.Add("@FML6_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_3);
                cmd.Parameters.Add("@FMR6_1khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6L_4);
                cmd.Parameters.Add("@FML6_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_1);
                cmd.Parameters.Add("@FMR6_2khz", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_2);
                cmd.Parameters.Add("@FML6_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_3);
                cmd.Parameters.Add("@FMR6_Average", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(StructData.fm6R_4);
                cmd.Parameters.Add("@FMExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result);
                cmd.Parameters.Add("@VoiceValue1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.testmaterial);
                cmd.Parameters.Add("@VoiceItem1TrueL1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1L_1);
                cmd.Parameters.Add("@VoiceItem1TrueL2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1L_2);
                cmd.Parameters.Add("@VoiceItem1TrueL3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1L_3);
                cmd.Parameters.Add("@VoiceItem1TrueR1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1R_1);
                cmd.Parameters.Add("@VoiceItem1TrueR2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1R_2);
                cmd.Parameters.Add("@VoiceItem1TrueR3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1R_3);
                cmd.Parameters.Add("@VoiceItem1TrueD1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1D_1);
                cmd.Parameters.Add("@VoiceItem1TrueD2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1D_2);
                cmd.Parameters.Add("@VoiceItem1TrueD3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true1D_3);
                cmd.Parameters.Add("@VoiceValue2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StructData.testmaterial2);
                cmd.Parameters.Add("@VoiceItem2TrueL1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2L_1);
                cmd.Parameters.Add("@VoiceItem2TrueL2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2L_2);
                cmd.Parameters.Add("@VoiceItem2TrueL3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2L_3);
                cmd.Parameters.Add("@VoiceItem2TrueR1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2R_1);
                cmd.Parameters.Add("@VoiceItem2TrueR2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2R_2);
                cmd.Parameters.Add("@VoiceItem2TrueR3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2R_3);
                cmd.Parameters.Add("@VoiceItem2TrueD1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2D_1);
                cmd.Parameters.Add("@VoiceItem2TrueD2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2D_2);
                cmd.Parameters.Add("@VoiceItem2TrueD3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StructData.true2D_3);
                cmd.Parameters.Add("@VoiceExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StructData.result2);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0]=="1")
                {
                    this.ComparisonFMAidsData(StructData);
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

    private string SearchFmAssessmentConditionReturn(SearchFMAidsAssess SearchStructure)
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
        if (SearchStructure.txtAssessDatestart != null && SearchStructure.txtAssessDateend != null && SearchStructure.txtAssessDatestart != DateBase && SearchStructure.txtAssessDateend != DateBase)
        {
            ConditionReturn += " AND AssessDate BETWEEN (@sAssessDateStart) AND (@sAssessDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND FMAidsAssess.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchFmAssessmentDataCount(SearchFMAidsAssess SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchFmAssessmentConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM FMAidsAssess " +
                            "INNER JOIN StudentDatabase ON FMAidsAssess.StudentID=StudentDatabase.StudentID " +
                            "WHERE FMAidsAssess.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sAssessDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAssessDatestart);
                cmd.Parameters.Add("@sAssessDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAssessDateend);
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
    public List<SearchFMAidsAssessResult> SearchFmAssessmentData(int indexpage, SearchFMAidsAssess SearchStructure)
    {
        List<SearchFMAidsAssessResult> returnValue = new List<SearchFMAidsAssessResult>();
        DataBase Base = new DataBase();
        StaffDataBase sDB = new StaffDataBase();
        string ConditionReturn = this.SearchFmAssessmentConditionReturn(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StudentDatabase.ID DESC) " +
                            "AS RowNum,  FMAidsAssess.*,StudentDatabase.StudentName,StudentDatabase.StudentBirthday FROM FMAidsAssess " +
                            "INNER JOIN StudentDatabase ON FMAidsAssess.StudentID=StudentDatabase.StudentID " +
                            "WHERE FMAidsAssess.isDeleted=0 " + ConditionReturn + " ) " +
                         "AS NewTable " +
                         "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + SearchStructure.txtstudentName + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);
                cmd.Parameters.Add("@sAssessDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAssessDatestart);
                cmd.Parameters.Add("@sAssessDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAssessDateend);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchFMAidsAssessResult addValue = new SearchFMAidsAssessResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.txtstudentName = dr["StudentName"].ToString();
                    addValue.txtAssessDate = DateTime.Parse(dr["AssessDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtbirthday = DateTime.Parse(dr["StudentBirthday"].ToString()).ToString("yyyy-MM-dd");
                    string Audiologist = dr["Audiologist"].ToString();
                    List<string> CreateFileName = sDB.getStaffDataName(Audiologist);
                    addValue.txtaudiologist = CreateFileName[1];
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchFMAidsAssessResult addValue = new SearchFMAidsAssessResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

}

