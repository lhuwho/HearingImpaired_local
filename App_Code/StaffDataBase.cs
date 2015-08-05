using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for StaffDataBase
/// </summary>
public class StaffDataBase
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
    public string[] _StaffhaveRoles = new string[5] { "0", "0", "0", "0", "0" };
	public StaffDataBase()
	{
		//
		// TODO: Add constructor logic here
		//

	}
    public void personnelFunction() {
        RolesStruct StaffAllRoles = this.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.personnel[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.personnel[1];//跨區與否
    }
    public void attendanceFunction()
    {
        RolesStruct StaffAllRoles = this.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.attendance[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.attendance[1];//跨區與否
    }
    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }
    /*
    public RolesStruct getStaffRoles(string sID)
    {
        RolesStruct returnValue = new RolesStruct();
        string LimitRoles = "";
        ManageDataBase msg=new ManageDataBase();
        string[] MembershipStaffRoles = msg.getMembershipStaffRoles(sID);
        for (int i = 0; i < MembershipStaffRoles.Length; i++) {
            if (MembershipStaffRoles[i] != "0") {
                if (i != 0)
                {
                    LimitRoles += " OR ";
                }
                LimitRoles += " cR.ID =" + MembershipStaffRoles[i];
            }
        }
        if (LimitRoles.Length > 0) {
            LimitRoles = " AND " + LimitRoles;
        }
            if (sID.Length > 0)
            {
                DataBase Base = new DataBase();
                using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
                {
                    try
                    {
                        Sqlconn.Open();
                        string sql = "SELECT MAX(newTable.CaseManagement) AS CaseManagement, MAX(newTable.ListeningManagement) AS ListeningManagement, " +
                        "MAX(newTable.TeachingManagement) AS TeachingManagement, MAX(newTable.Payroll) AS Payroll, MAX(newTable.Attendance) AS Attendance, " +
                        "MAX(newTable.PersonnelManagement) AS PersonnelManagement, MAX(newTable.PropertyApplyManagement) AS PropertyApplyManagement, " +
                        "MAX(newTable.PropertyManagement) AS PropertyManagement, MAX(newTable.LibraryManagement) AS LibraryManagement, " +
                        "MAX(newTable.ServiceFees) AS ServiceFees, MAX(newTable.CaseTemperature) AS CaseTemperature, MAX(newTable.TeachersTemperature) AS TeachersTemperature, " +
                        "MAX(newTable.StationeryManagement) AS StationeryManagement, MAX(newTable.RemindeSystem) AS RemindeSystem, MAX(newTable.CaseUnit) AS CaseUnit, " +
                        "MAX(newTable.hearingUnit) AS hearingUnit, MAX(newTable.teachUnit) AS teachUnit, MAX(newTable.payrollUnit) AS payrollUnit, MAX(newTable.aUnit) AS aUnit, " +
                        "MAX(newTable.personnelUnit) AS personnelUnit, MAX(newTable.paUnit) AS paUnit, MAX(newTable.propertyUnit) AS propertyUnit, MAX(newTable.LUnit) AS LUnit, " +
                        "MAX(newTable.sUnit) AS sUnit, MAX(newTable.cBTUnit) AS cBTUnit, MAX(newTable.tBTUnit) AS tBTUnit, MAX(newTable.stationeryUnit) AS stationeryUnit, " +
                        "MAX(newTable.remindeUnit) AS remindeUnit " +
                        "FROM ( " +
                            "SELECT cR.*,ISNULL(cRUnit.CaseManagement,0) as CaseUnit , ISNULL(cRUnit.ListeningManagement,0) as hearingUnit, " +
                            "ISNULL(cRUnit.TeachingManagement,0) as teachUnit, ISNULL(cRUnit.Payroll,0) as payrollUnit, ISNULL(cRUnit.Attendance,0) as aUnit, " +
                            "ISNULL(cRUnit.PersonnelManagement,0) as personnelUnit, ISNULL(cRUnit.PropertyApplyManagement,0) as paUnit, " +
                            "ISNULL(cRUnit.PropertyManagement,0) as propertyUnit, ISNULL(cRUnit.LibraryManagement,0) as LUnit, " +
                            "ISNULL(cRUnit.ServiceFees,0) as sUnit, ISNULL(cRUnit.CaseTemperature,0) as cBTUnit, ISNULL(cRUnit.TeachersTemperature,0) as tBTUnit, " +
                            "ISNULL(cRUnit.StationeryManagement,0) as stationeryUnit, ISNULL(cRUnit.RemindeSystem,0) as remindeUnit " +
                            "FROM Competence_Roles cR LEFT JOIN Competence_Roles_Unit cRUnit ON cR.ID=cRUnit.cRolesID AND cRUnit.isDeleted=0 " +
                            "WHERE cR.isDeleted=0 " +LimitRoles+
                            //"AND EXISTS (SELECT * FROM Staff_Competence_Roles scR WHERE scR.StaffID=@StaffID OR cR.ID=scR.Roles1 OR cR.ID=scR.Roles2 OR cR.ID=scR.Roles3 OR cR.ID=scR.Roles4 OR cR.ID=scR.Roles5) " +
                            ") AS newTable";
                        SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            returnValue.caseStu[0] = dr["CaseManagement"].ToString();
                            returnValue.caseStu[1] = dr["CaseUnit"].ToString();
                            returnValue.hearing[0] = dr["ListeningManagement"].ToString();
                            returnValue.hearing[1] = dr["hearingUnit"].ToString();
                            returnValue.teach[0] = dr["TeachingManagement"].ToString();
                            returnValue.teach[1] = dr["teachUnit"].ToString();
                            returnValue.salary[0] = dr["Payroll"].ToString();
                            returnValue.salary[1] = dr["payrollUnit"].ToString();
                            returnValue.attendance[0] = dr["Attendance"].ToString();
                            returnValue.attendance[1] = dr["aUnit"].ToString();
                            returnValue.personnel[0] = dr["PersonnelManagement"].ToString();
                            returnValue.personnel[1] = dr["personnelUnit"].ToString();
                            returnValue.apply[0] = dr["PropertyApplyManagement"].ToString();
                            returnValue.apply[1] = dr["paUnit"].ToString();
                            returnValue.property[0] = dr["PropertyManagement"].ToString();
                            returnValue.property[1] = dr["propertyUnit"].ToString();
                            returnValue.library[0] = dr["LibraryManagement"].ToString();
                            returnValue.library[1] = dr["LUnit"].ToString();
                            returnValue.serviceFees[0] = dr["ServiceFees"].ToString();
                            returnValue.serviceFees[1] = dr["sUnit"].ToString();
                            returnValue.caseBT[0] = dr["CaseTemperature"].ToString();
                            returnValue.caseBT[1] = dr["cBTUnit"].ToString();
                            returnValue.teachBT[0] = dr["TeachersTemperature"].ToString();
                            returnValue.teachBT[1] = dr["tBTUnit"].ToString();
                            returnValue.stationery[0] = dr["StationeryManagement"].ToString();
                            returnValue.stationery[1] = dr["stationeryUnit"].ToString();
                            returnValue.remind[0] = dr["RemindeSystem"].ToString();
                            returnValue.remind[1] = dr["remindeUnit"].ToString();

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
            }
        return returnValue;
    }*/
    public RolesStruct getStaffRoles()
    {
        StaffDataBase sDB = new StaffDataBase();
        string sID= HttpContext.Current.User.Identity.Name;

        RolesStruct returnValue = new RolesStruct();
        string LimitRoles = "";
        ManageDataBase msg = new ManageDataBase();
        string[] MembershipStaffRoles = msg.getMembershipStaffRoles(sID);
        for (int i = 1; i < MembershipStaffRoles.Length; i++)
        { //i=0=>Roles DB ID
            if (MembershipStaffRoles[i] != "0")
            {
                if (LimitRoles.Length > 0)
                {
                    LimitRoles += " OR ";
                }
                LimitRoles += " cR.ID=" + MembershipStaffRoles[i];
            }
        }
        if (LimitRoles.Length > 0)
        {
            LimitRoles = " AND " + LimitRoles;
        }
        if (sID.Length > 0 && LimitRoles.Length > 0)
        {
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    Sqlconn.Open();
                    string sql = "SELECT cR.*,ISNULL(cRUnit.CaseManagement,0) as CaseUnit , ISNULL(cRUnit.ListeningManagement,0) as hearingUnit, " +
                        "ISNULL(cRUnit.TeachingManagement,0) as teachUnit, ISNULL(cRUnit.Payroll,0) as payrollUnit, ISNULL(cRUnit.Attendance,0) as aUnit, " +
                        "ISNULL(cRUnit.PersonnelManagement,0) as personnelUnit, ISNULL(cRUnit.PropertyApplyManagement,0) as paUnit, " +
                        "ISNULL(cRUnit.PropertyManagement,0) as propertyUnit, ISNULL(cRUnit.LibraryManagement,0) as LUnit, " +
                        "ISNULL(cRUnit.ServiceFees,0) as sUnit, ISNULL(cRUnit.CaseTemperature,0) as cBTUnit, ISNULL(cRUnit.TeachersTemperature,0) as tBTUnit, " +
                        "ISNULL(cRUnit.StationeryManagement,0) as stationeryUnit, ISNULL(cRUnit.RemindeSystem,0) as remindeUnit " +
                        "FROM Competence_Roles cR LEFT JOIN Competence_Roles_Unit cRUnit ON cR.ID=cRUnit.cRolesID AND cRUnit.isDeleted=0 " +
                        "WHERE cR.isDeleted=0 " + LimitRoles;

                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string[] RolesCompareReturn = this.RolesCompare(returnValue.caseStu, dr["CaseManagement"].ToString(), dr["CaseUnit"].ToString());
                        returnValue.caseStu[0] = RolesCompareReturn[0];
                        returnValue.caseStu[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.hearing, dr["ListeningManagement"].ToString(), dr["hearingUnit"].ToString());
                        returnValue.hearing[0] = RolesCompareReturn[0];
                        returnValue.hearing[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.teach, dr["TeachingManagement"].ToString(), dr["teachUnit"].ToString());
                        returnValue.teach[0] = RolesCompareReturn[0];
                        returnValue.teach[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.salary, dr["Payroll"].ToString(), dr["payrollUnit"].ToString());
                        returnValue.salary[0] = RolesCompareReturn[0];
                        returnValue.salary[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.attendance, dr["Attendance"].ToString(), dr["aUnit"].ToString());
                        returnValue.attendance[0] = RolesCompareReturn[0];
                        returnValue.attendance[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.personnel, dr["PersonnelManagement"].ToString(), dr["personnelUnit"].ToString());
                        returnValue.personnel[0] = RolesCompareReturn[0];
                        returnValue.personnel[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.apply, dr["PropertyApplyManagement"].ToString(), dr["paUnit"].ToString());
                        returnValue.apply[0] = RolesCompareReturn[0];
                        returnValue.apply[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.property, dr["PropertyManagement"].ToString(), dr["propertyUnit"].ToString());
                        returnValue.property[0] = RolesCompareReturn[0];
                        returnValue.property[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.library, dr["LibraryManagement"].ToString(), dr["LUnit"].ToString());
                        returnValue.library[0] = RolesCompareReturn[0];
                        returnValue.library[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.serviceFees, dr["ServiceFees"].ToString(), dr["sUnit"].ToString());
                        returnValue.serviceFees[0] = RolesCompareReturn[0];
                        returnValue.serviceFees[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.caseBT, dr["CaseTemperature"].ToString(), dr["cBTUnit"].ToString());
                        returnValue.caseBT[0] = RolesCompareReturn[0];
                        returnValue.caseBT[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.teachBT, dr["TeachersTemperature"].ToString(), dr["tBTUnit"].ToString());
                        returnValue.teachBT[0] = RolesCompareReturn[0];
                        returnValue.teachBT[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.stationery, dr["StationeryManagement"].ToString(), dr["stationeryUnit"].ToString());
                        returnValue.stationery[0] = RolesCompareReturn[0];
                        returnValue.stationery[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.remind, dr["RemindeSystem"].ToString(), dr["remindeUnit"].ToString());
                        returnValue.remind[0] = RolesCompareReturn[0];
                        returnValue.remind[1] = RolesCompareReturn[1];

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
        }
        return returnValue;
    }

    public RolesStruct getStaffRoles(string sID)
    {
        RolesStruct returnValue = new RolesStruct();
        string LimitRoles = "";
        ManageDataBase msg=new ManageDataBase();
        string[] MembershipStaffRoles = msg.getMembershipStaffRoles(sID);
        for (int i = 1; i < MembershipStaffRoles.Length; i++) { //i=0=>Roles DB ID
            if (MembershipStaffRoles[i] != "0") {
                if (LimitRoles.Length > 0) 
                {
                    LimitRoles += " OR ";
                }
                LimitRoles += " cR.ID=" + MembershipStaffRoles[i];
            }
        }
        if (LimitRoles.Length > 0) {
            LimitRoles = " AND " + LimitRoles;
        }
        if (sID.Length > 0 && LimitRoles.Length > 0)
        {
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    Sqlconn.Open();
                    string sql ="SELECT cR.*,ISNULL(cRUnit.CaseManagement,0) as CaseUnit , ISNULL(cRUnit.ListeningManagement,0) as hearingUnit, " +
                        "ISNULL(cRUnit.TeachingManagement,0) as teachUnit, ISNULL(cRUnit.Payroll,0) as payrollUnit, ISNULL(cRUnit.Attendance,0) as aUnit, " +
                        "ISNULL(cRUnit.PersonnelManagement,0) as personnelUnit, ISNULL(cRUnit.PropertyApplyManagement,0) as paUnit, " +
                        "ISNULL(cRUnit.PropertyManagement,0) as propertyUnit, ISNULL(cRUnit.LibraryManagement,0) as LUnit, " +
                        "ISNULL(cRUnit.ServiceFees,0) as sUnit, ISNULL(cRUnit.CaseTemperature,0) as cBTUnit, ISNULL(cRUnit.TeachersTemperature,0) as tBTUnit, " +
                        "ISNULL(cRUnit.StationeryManagement,0) as stationeryUnit, ISNULL(cRUnit.RemindeSystem,0) as remindeUnit " +
                        "FROM Competence_Roles cR LEFT JOIN Competence_Roles_Unit cRUnit ON cR.ID=cRUnit.cRolesID AND cRUnit.isDeleted=0 " +
                        "WHERE cR.isDeleted=0 " + LimitRoles;

                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string[] RolesCompareReturn = this.RolesCompare(returnValue.caseStu, dr["CaseManagement"].ToString(), dr["CaseUnit"].ToString());
                        returnValue.caseStu[0] = RolesCompareReturn[0];
                        returnValue.caseStu[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.hearing, dr["ListeningManagement"].ToString(), dr["hearingUnit"].ToString());
                        returnValue.hearing[0] = RolesCompareReturn[0];
                        returnValue.hearing[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.teach, dr["TeachingManagement"].ToString(), dr["teachUnit"].ToString());
                        returnValue.teach[0] = RolesCompareReturn[0];
                        returnValue.teach[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.salary, dr["Payroll"].ToString(), dr["payrollUnit"].ToString());
                        returnValue.salary[0] = RolesCompareReturn[0];
                        returnValue.salary[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.attendance, dr["Attendance"].ToString(), dr["aUnit"].ToString());
                        returnValue.attendance[0] = RolesCompareReturn[0];
                        returnValue.attendance[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.personnel, dr["PersonnelManagement"].ToString(), dr["personnelUnit"].ToString());
                        returnValue.personnel[0] = RolesCompareReturn[0];
                        returnValue.personnel[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.apply, dr["PropertyApplyManagement"].ToString(), dr["paUnit"].ToString());
                        returnValue.apply[0] = RolesCompareReturn[0];
                        returnValue.apply[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.property, dr["PropertyManagement"].ToString(), dr["propertyUnit"].ToString());
                        returnValue.property[0] = RolesCompareReturn[0];
                        returnValue.property[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.library, dr["LibraryManagement"].ToString(), dr["LUnit"].ToString());
                        returnValue.library[0] = RolesCompareReturn[0];
                        returnValue.library[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.serviceFees, dr["ServiceFees"].ToString(), dr["sUnit"].ToString());
                        returnValue.serviceFees[0] = RolesCompareReturn[0];
                        returnValue.serviceFees[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.caseBT, dr["CaseTemperature"].ToString(), dr["cBTUnit"].ToString());
                        returnValue.caseBT[0] = RolesCompareReturn[0];
                        returnValue.caseBT[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.teachBT, dr["TeachersTemperature"].ToString(), dr["tBTUnit"].ToString());
                        returnValue.teachBT[0] = RolesCompareReturn[0];
                        returnValue.teachBT[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.stationery, dr["StationeryManagement"].ToString(), dr["stationeryUnit"].ToString());
                        returnValue.stationery[0] = RolesCompareReturn[0];
                        returnValue.stationery[1] = RolesCompareReturn[1];
                        RolesCompareReturn = this.RolesCompare(returnValue.remind, dr["RemindeSystem"].ToString(), dr["remindeUnit"].ToString());
                        returnValue.remind[0] = RolesCompareReturn[0];
                        returnValue.remind[1] = RolesCompareReturn[1];

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
        }
        return returnValue;
    }
    private string[] RolesCompare(string[] baseRoles, string DBRoles, string DBRolesUnit)
    {
        string[] returnValue = new string[2]{"0000","0"};
        int baseRoles0 = Convert.ToInt32(baseRoles[0],2);
        int DBRoles0 = Convert.ToInt32(DBRoles, 2);
        if (DBRoles0 > baseRoles0)
        {
            returnValue[0] = DBRoles;
        }
        else {
            returnValue[0] = baseRoles[0];
        }
        int baseRoles1 = Convert.ToInt32(baseRoles[1], 2);
        int DBRoles1 = Convert.ToInt32(DBRolesUnit, 2);
        if (DBRoles1 > baseRoles1)
        {
            returnValue[1] = DBRolesUnit;
        }
        else {
            returnValue[1] = baseRoles[1];
        }
        return returnValue;
    }
    public string getUnitName(string UnitID)
    {
        string Name = "";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT Name FROM System_Unit_List where ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(UnitID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    Name = dr["Name"].ToString();
                   
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                
            }

        }
        return Name;
    }
    public List<string> getStaffDataName(string sID)
    {
        List<string> returnValue = new List<string>();
        returnValue.Add(sID);//ID
        returnValue.Add("");//NAME
        returnValue.Add("0");//Unit
        returnValue.Add("");
        if (sID.Length > 0)
        {
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    Sqlconn.Open();
                    string sql = "SELECT StaffID,StaffName,Unit FROM StaffDatabase where StaffID=(@StaffID)";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["StaffName"].ToString();
                        returnValue[2] = dr["Unit"].ToString();
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
                }

            }
        }
        return returnValue;
    }

    public string[] getStaffDataName2(string sID)
    {
        string[] returnValue = new string[4];
        returnValue[0] = sID;
        returnValue[1] = "";
        returnValue[2] = "0";
        //returnValue[3] = "";
        if (sID.Length > 0)
        {
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    Sqlconn.Open();
                    string sql = "SELECT StaffID,StaffName,Unit FROM StaffDatabase where StaffID=(@StaffID)";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["StaffName"].ToString();
                        returnValue[2] = dr["Unit"].ToString();
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
                }

            }
        }
        return returnValue;
    }
    public string[] CreateUserMemberData(CreateStaff StaffData)
    {

            string[] returnValue = new string[3];
            returnValue[0] = "0";
            returnValue[1] = "0";
            returnValue[2] = "0";    
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                    Sqlconn.Open();
                    string sql = "INSERT INTO StaffDatabase (Unit, StaffID, StaffName, AppointmentDate, ResignationDate, WorkItem, JobCapacity, JobGrade, StaffIdentity, " +
                        "StaffAvatar, Hometown, Birthday, sex, Marriage, AddressZip1, AddressCity1, AddressOther1, AddressZip2, AddressCity2, AddressOther2, TelDaytime, "+
                        "TelNight, Phone, Email, " +
                        "UrgentContactName, UrgentContactAddress, UrgentContactTel, DoctorSchoolName, DoctorDepartment, DoctorSince, DoctorUntil, DoctorState, MasterSchoolName, " +
                        "MasterDepartment, MasterSince, MasterUntil, MasterState, UniversitySchoolName, UniversityDepartment, UniversitySince, UniversityUntil, UniversityState, " +
                        "VocationalSchoolName, VocationalDepartment, VocationalSince, VocationalUntil, VocationalState, Experience1Since, Experience1Until, Experience1Company, " +
                        "Experience1Post, Experience1Salary, Experience1Prove, Experience1Director, Experience1DirectorName, Experience2Since, Experience2Until, " +
                        "Experience2Company, Experience2Post, Experience2Salary, Experience2Prove, Experience2Director, Experience2DirectorName, Experience3Since, " +
                        "Experience3Until, Experience3Company, Experience3Post, Experience3Salary, Experience3Prove, Experience3Director, Experience3DirectorName, " +
                        "Experience4Since, Experience4Until, Experience4Company, Experience4Post, Experience4Salary, Experience4Prove, Experience4Director, " +
                        "Experience4DirectorName, Family1Title, Family1Name, Family1Age, Family1Profession, Family2Title, Family2Name, Family2Age, Family2Profession, " +
                        "Family3Title, Family3Name, Family3Age, Family3Profession, Family4Title, Family4Name, Family4Age, Family4Profession, Family5Title, " +
                        "Family5Name, Family5Age, Family5Profession, Family6Title, Family6Name, Family6Age, Family6Profession, GuarantorName, GuarantorUnit, " +
                        "GuarantorPost, GuarantorRelationship, GuarantorContact, GuarantorContactTime, CandidatesMessage, MessageExplain, Language1Name, Language1Listen, " +
                        "Language1Say, Language1Read, Language1Write, Language2Name, Language2Listen, Language2Say, Language2Read, Language2Write, Language3Name, " +
                        "Language3Listen, Language3Say, Language3Read, Language3Write, ExpertiseSkill1, ExpertiseSkill1License, ExpertiseSkill1Progression, " +
                        "ExpertiseSkill1GetUnit, ExpertiseSkill1GetDate, ExpertiseSkill1ValidDate, ExpertiseSkill2, ExpertiseSkill2License, ExpertiseSkill2Progression, " +
                        "ExpertiseSkill2GetUnit, ExpertiseSkill2GetDate, ExpertiseSkill2ValidDate, ExpertiseSkill3, ExpertiseSkill3License, ExpertiseSkill3Progression, " +
                        "ExpertiseSkill3GetUnit, ExpertiseSkill3GetDate, ExpertiseSkill3ValidDate, ExpertiseSkill4, ExpertiseSkill4License, ExpertiseSkill4Progression, " +
                        "ExpertiseSkill4GetUnit, ExpertiseSkill4GetDate, ExpertiseSkill4ValidDate, Disease, DiseaseExplain, FileDate, CreateFileBy, UpFileBy, UpFileDate)" +

                        "VALUES (@Unit, @StaffID, @StaffName, @AppointmentDate, @ResignationDate, @WorkItem, @JobCapacity, @JobGrade, @StaffIdentity, " +
                        "@StaffAvatar, @Hometown, @Birthday, @sex, @Marriage, @AddressZip1, @AddressCity1, " +
                        "@AddressOther1, @AddressZip2, @AddressCity2, @AddressOther2, @TelDaytime, @TelNight, @Phone, @Email, " +
                        "@UrgentContactName, @UrgentContactAddress, @UrgentContactTel, @DoctorSchoolName, @DoctorDepartment, @DoctorSince, @DoctorUntil, @DoctorState, @MasterSchoolName, " +
                        "@MasterDepartment, @MasterSince, @MasterUntil, @MasterState, @UniversitySchoolName, @UniversityDepartment, @UniversitySince, @UniversityUntil, @UniversityState, " +
                        "@VocationalSchoolName, @VocationalDepartment, @VocationalSince, @VocationalUntil, @VocationalState, @Experience1Since, @Experience1Until, @Experience1Company, " +
                        "@Experience1Post, @Experience1Salary, @Experience1Prove, @Experience1Director, @Experience1DirectorName, @Experience2Since, @Experience2Until, " +
                        "@Experience2Company, @Experience2Post, @Experience2Salary, @Experience2Prove, @Experience2Director, @Experience2DirectorName, @Experience3Since, " +
                        "@Experience3Until, @Experience3Company, @Experience3Post, @Experience3Salary, @Experience3Prove, @Experience3Director, @Experience3DirectorName, " +
                        "@Experience4Since, @Experience4Until, @Experience4Company, @Experience4Post, @Experience4Salary, @Experience4Prove, @Experience4Director, " +
                        "@Experience4DirectorName, @Family1Title, @Family1Name, @Family1Age, @Family1Profession, @Family2Title, @Family2Name, @Family2Age, @Family2Profession, " +
                        "@Family3Title, @Family3Name, @Family3Age, @Family3Profession, @Family4Title, @Family4Name, @Family4Age, @Family4Profession, @Family5Title, " +
                        "@Family5Name, @Family5Age, @Family5Profession, @Family6Title, @Family6Name, @Family6Age, @Family6Profession, @GuarantorName, @GuarantorUnit, " +
                        "@GuarantorPost, @GuarantorRelationship, @GuarantorContact, @GuarantorContactTime, @CandidatesMessage, @MessageExplain, @Language1Name, @Language1Listen, " +
                        "@Language1Say, @Language1Read, @Language1Write, @Language2Name, @Language2Listen, @Language2Say, @Language2Read, @Language2Write, @Language3Name, " +
                        "@Language3Listen, @Language3Say, @Language3Read, @Language3Write, @ExpertiseSkill1, @ExpertiseSkill1License, @ExpertiseSkill1Progression, " +
                        "@ExpertiseSkill1GetUnit, @ExpertiseSkill1GetDate, @ExpertiseSkill1ValidDate, @ExpertiseSkill2, @ExpertiseSkill2License, @ExpertiseSkill2Progression, " +
                        "@ExpertiseSkill2GetUnit, @ExpertiseSkill2GetDate, @ExpertiseSkill2ValidDate, @ExpertiseSkill3, @ExpertiseSkill3License, @ExpertiseSkill3Progression, " +
                        "@ExpertiseSkill3GetUnit, @ExpertiseSkill3GetDate, @ExpertiseSkill3ValidDate, @ExpertiseSkill4, @ExpertiseSkill4License, @ExpertiseSkill4Progression, " +
                        "@ExpertiseSkill4GetUnit, @ExpertiseSkill4GetDate, @ExpertiseSkill4ValidDate, @Disease, @DiseaseExplain, @FileDate,@CreateFileBy, @UpFileBy, (getDate())) ";

                    SqlCommand cmd = new SqlCommand(sql, Sqlconn); 
                    cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.unit);
                    cmd.Parameters.Add("@StaffID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffID);
                    cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffName);
                    cmd.Parameters.Add("@AppoIntmentDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.officeDate);
                    cmd.Parameters.Add("@ResignationDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.resignDate);
                    cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.applyJob);
                    cmd.Parameters.Add("@JobCapacity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.jobTitle);
                    cmd.Parameters.Add("@JobGrade", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.jobLevel);
                    cmd.Parameters.Add("@StaffIdentity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffTWID);
                    cmd.Parameters.Add("@StaffAvatar", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffPhoto);
                    cmd.Parameters.Add("@Hometown", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.comeCity);
                    cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.staffbirthday);
                    cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.staffsex);
                    cmd.Parameters.Add("@Marriage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.marriage);
                    cmd.Parameters.Add("@AddressZip1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddressZip);
                    cmd.Parameters.Add("@AddressCity1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.censusCity);
                    cmd.Parameters.Add("@AddressOther1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddress);
                    cmd.Parameters.Add("@AddressZip2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.addressZip);
                    cmd.Parameters.Add("@AddressCity2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.addressCity);
                    cmd.Parameters.Add("@AddressOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.address);
                    cmd.Parameters.Add("@TelDaytime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.TDaytime);
                    cmd.Parameters.Add("@TelNight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.TNight);
                    cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Phone);
                    cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffemail);
                    cmd.Parameters.Add("@UrgentContactName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.EmergencyName);
                    cmd.Parameters.Add("@UrgentContactAddress", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.EmergencyAddress);
                    cmd.Parameters.Add("@UrgentContactTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.EmergencyPhone);
                    cmd.Parameters.Add("@DoctorSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DSchoolName);
                    cmd.Parameters.Add("@DoctorDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DDepartment);
                    cmd.Parameters.Add("@DoctorSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DSince);
                    cmd.Parameters.Add("@DoctorUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DUntil);
                    cmd.Parameters.Add("@DoctorState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study1);
                    cmd.Parameters.Add("@MasterSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MSchoolName);
                    cmd.Parameters.Add("@MasterDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MDepartment);
                    cmd.Parameters.Add("@MasterSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MSince);
                    cmd.Parameters.Add("@MasterUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MUntil);
                    cmd.Parameters.Add("@MasterState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study2);
                    cmd.Parameters.Add("@UniversitySchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.USchoolName);
                    cmd.Parameters.Add("@UniversityDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.UDepartment);
                    cmd.Parameters.Add("@UniversitySince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.USince);
                    cmd.Parameters.Add("@UniversityUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.UUntil);
                    cmd.Parameters.Add("@UniversityState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study3);
                    cmd.Parameters.Add("@VocationalSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VSchoolName);
                    cmd.Parameters.Add("@VocationalDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VDepartment);
                    cmd.Parameters.Add("@VocationalSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VSince);
                    cmd.Parameters.Add("@VocationalUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VUntil);
                    cmd.Parameters.Add("@VocationalState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study4);
                    cmd.Parameters.Add("@Experience1Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince1);
                    cmd.Parameters.Add("@Experience1Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil1);
                    cmd.Parameters.Add("@Experience1Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname1);
                    cmd.Parameters.Add("@Experience1Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition1);
                    cmd.Parameters.Add("@Experience1Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary1);
                    cmd.Parameters.Add("@Experience1Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove1);
                    cmd.Parameters.Add("@Experience1Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle1);
                    cmd.Parameters.Add("@Experience1DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName1);
                    cmd.Parameters.Add("@Experience2Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince2);
                    cmd.Parameters.Add("@Experience2Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil2);
                    cmd.Parameters.Add("@Experience2Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname2);
                    cmd.Parameters.Add("@Experience2Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition2);
                    cmd.Parameters.Add("@Experience2Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary2);
                    cmd.Parameters.Add("@Experience2Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove2);
                    cmd.Parameters.Add("@Experience2Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle2);
                    cmd.Parameters.Add("@Experience2DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName2);
                    cmd.Parameters.Add("@Experience3Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince3);
                    cmd.Parameters.Add("@Experience3Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil3);
                    cmd.Parameters.Add("@Experience3Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname3);
                    cmd.Parameters.Add("@Experience3Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition3);
                    cmd.Parameters.Add("@Experience3Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary3);
                    cmd.Parameters.Add("@Experience3Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove3);
                    cmd.Parameters.Add("@Experience3Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle3);
                    cmd.Parameters.Add("@Experience3DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName3);
                    cmd.Parameters.Add("@Experience4Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince4);
                    cmd.Parameters.Add("@Experience4Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil4);
                    cmd.Parameters.Add("@Experience4Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname4);
                    cmd.Parameters.Add("@Experience4Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition4);
                    cmd.Parameters.Add("@Experience4Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary4);
                    cmd.Parameters.Add("@Experience4Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove4);
                    cmd.Parameters.Add("@Experience4Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle4);
                    cmd.Parameters.Add("@Experience4DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName4);
                    cmd.Parameters.Add("@Family1Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[0][0]);
                    cmd.Parameters.Add("@Family1Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[0][1]);
                    cmd.Parameters.Add("@Family1Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[0][2]);
                    cmd.Parameters.Add("@Family1Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[0][3]);
                    cmd.Parameters.Add("@Family2Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[1][0]);
                    cmd.Parameters.Add("@Family2Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[1][1]);
                    cmd.Parameters.Add("@Family2Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[1][2]);
                    cmd.Parameters.Add("@Family2Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[1][3]);
                    cmd.Parameters.Add("@Family3Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[2][0]);
                    cmd.Parameters.Add("@Family3Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[2][1]);
                    cmd.Parameters.Add("@Family3Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[2][2]);
                    cmd.Parameters.Add("@Family3Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[2][3]);
                    cmd.Parameters.Add("@Family4Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[3][0]);
                    cmd.Parameters.Add("@Family4Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[3][1]);
                    cmd.Parameters.Add("@Family4Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[3][2]);
                    cmd.Parameters.Add("@Family4Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[3][3]);
                    cmd.Parameters.Add("@Family5Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[4][0]);
                    cmd.Parameters.Add("@Family5Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[4][1]);
                    cmd.Parameters.Add("@Family5Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[4][2]);
                    cmd.Parameters.Add("@Family5Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[4][3]);
                    cmd.Parameters.Add("@Family6Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[5][0]);
                    cmd.Parameters.Add("@Family6Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[5][1]);
                    cmd.Parameters.Add("@Family6Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[5][2]);
                    cmd.Parameters.Add("@Family6Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[5][3]);
                    cmd.Parameters.Add("@GuarantorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailName);
                    cmd.Parameters.Add("@GuarantorUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailUnit);
                    cmd.Parameters.Add("@GuarantorPost", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailJob);
                    cmd.Parameters.Add("@GuarantorRelationship", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailRelationship);
                    cmd.Parameters.Add("@GuarantorContact", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailContact);
                    cmd.Parameters.Add("@GuarantorContactTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailContactTime);
                    cmd.Parameters.Add("@CandidatesMessage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.recruited);
                    cmd.Parameters.Add("@MessageExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.recruitedText);
                    cmd.Parameters.Add("@Language1Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.langAbility1);
                    cmd.Parameters.Add("@Language1Listen", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langL1);
                    cmd.Parameters.Add("@Language1Say", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langS1);
                    cmd.Parameters.Add("@Language1Read", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langR1);
                    cmd.Parameters.Add("@Language1Write", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langW1);
                    cmd.Parameters.Add("@Language2Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.langAbility2);
                    cmd.Parameters.Add("@Language2Listen", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langL2);
                    cmd.Parameters.Add("@Language2Say", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langS2);
                    cmd.Parameters.Add("@Language2Read", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langR2);
                    cmd.Parameters.Add("@Language2Write", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langW2);
                    cmd.Parameters.Add("@Language3Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.langAbility3);
                    cmd.Parameters.Add("@Language3Listen", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langL3);
                    cmd.Parameters.Add("@Language3Say", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langS3);
                    cmd.Parameters.Add("@Language3Read", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langR3);
                    cmd.Parameters.Add("@Language3Write", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langW3);

                    for (int i = 0; i < StaffData.SpecialtySkill.Count; i++)
                    {
                        cmd.Parameters.Add("@ExpertiseSkill" + (i + 1), SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][0]);
                        cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "License", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][1]);
                        cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "Progression", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][2]);
                        cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "GetUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][3]);
                        cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "GetDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.SpecialtySkill[i][4]);
                        cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "ValidDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.SpecialtySkill[i][5]);

                    }
                    cmd.Parameters.Add("@FileDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.fillInDate);
                    cmd.Parameters.Add("@Disease", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.disease);
                    cmd.Parameters.Add("@DiseaseExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.diseaseText);
                    cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                    cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                    returnValue[0] = cmd.ExecuteNonQuery().ToString();


                    if (returnValue[0] != "0")
                    {
                        string FieldName = "StaffDB_" + StaffData.unit;
                        Int64 Column = 0;
                        sql = "select IDENT_CURRENT('StaffDatabase') AS cID "+
                              "UPDATE AutomaticNumberTable SET " + FieldName + "=" + FieldName + "+1 WHERE ID=1 ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Column = Int64.Parse(dr["cID"].ToString());
                        }
                        dr.Close();

                        /*string stuIDName = "";
                        sql = "SELECT Count(*) AS QCOUNT FROM StaffDatabase WHERE Unit=(@Unit) ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffData.unit);
                        string stuNumber = cmd.ExecuteScalar().ToString();*/
                        CaseDataBase SDB = new CaseDataBase();
                        string stuNumber = SDB.getUnitAutoNumber(FieldName);
                        string stuIDName = Chk.CheckStringtoIntFunction(StaffData.unit) + stuNumber.PadLeft(3, '0');

                        sql = "UPDATE StaffDatabase SET StaffID=(@StaffID) WHERE ID=(@TID) ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
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

    public int UpOneStaffDataBase(CreateStaff StaffData)
    {

        int returnValue = 0;
        string upPhoto = "";
        if (StaffData.staffPhoto != null)
        {
            upPhoto += " StaffAvatar=@StaffAvatar, ";
        }
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE StaffDatabase SET Unit=@Unit, StaffName=@StaffName, AppointmentDate=@AppointmentDate, ResignationDate=@ResignationDate, "+
                    "WorkItem=@WorkItem, JobCapacity=@JobCapacity, JobGrade=@JobGrade, StaffIdentity=@StaffIdentity,  Hometown=@Hometown, " +upPhoto+
                    "Birthday=@Birthday, sex=@sex, Marriage=@Marriage, AddressZip1=@AddressZip1, " +
                    "AddressCity1=@AddressCity1, AddressOther1=@AddressOther1, AddressZip2=@AddressZip2, AddressCity2=@AddressCity2, AddressOther2=@AddressOther2, " +
                    "TelDaytime=@TelDaytime, TelNight=@TelNight, Phone=@Phone, Email=@Email, UrgentContactName=@UrgentContactName, " +
                    "UrgentContactAddress=@UrgentContactAddress, UrgentContactTel=@UrgentContactTel, DoctorSchoolName=@DoctorSchoolName, DoctorDepartment=@DoctorDepartment, " +
                    "DoctorSince=@DoctorSince, DoctorUntil=@DoctorUntil, DoctorState=@DoctorState, MasterSchoolName=@MasterSchoolName, MasterDepartment=@MasterDepartment, " +
                    "MasterSince=@MasterSince, MasterUntil=@MasterUntil, MasterState=@MasterState, UniversitySchoolName=@UniversitySchoolName, " +
                    "UniversityDepartment=@UniversityDepartment, UniversitySince=@UniversitySince, UniversityUntil=@UniversityUntil, UniversityState=@UniversityState, " +
                    "VocationalSchoolName=@VocationalSchoolName, VocationalDepartment=@VocationalDepartment, VocationalSince=@VocationalSince, VocationalUntil=@VocationalUntil, " +
                    "VocationalState=@VocationalState, Experience1Since=@Experience1Since, Experience1Until=@Experience1Until, Experience1Company=@Experience1Company, " +
                    "Experience1Post=@Experience1Post, Experience1Salary=@Experience1Salary, Experience1Prove=@Experience1Prove, Experience1Director=@Experience1Director, " +
                    "Experience1DirectorName=@Experience1DirectorName, Experience2Since=@Experience2Since, Experience2Until=@Experience2Until, " +
                    "Experience2Company=@Experience2Company, Experience2Post=@Experience2Post, Experience2Salary=@Experience2Salary, Experience2Prove=@Experience2Prove, " +
                    "Experience2Director=@Experience2Director, Experience2DirectorName=@Experience2DirectorName, Experience3Since=@Experience3Since, " +
                    "Experience3Until=@Experience3Until, Experience3Company=@Experience3Company, Experience3Post=@Experience3Post, Experience3Salary=@Experience3Salary, " +
                    "Experience3Prove=@Experience3Prove, Experience3Director=@Experience3Director, Experience3DirectorName=@Experience3DirectorName, " +
                    "Experience4Since=@Experience4Since, Experience4Until=@Experience4Until, Experience4Company=@Experience4Company, Experience4Post=@Experience4Post, " +
                    "Experience4Salary=@Experience4Salary, Experience4Prove=@Experience4Prove, Experience4Director=@Experience4Director, " +
                    "Experience4DirectorName=@Experience4DirectorName, Family1Title=@Family1Title, Family1Name=@Family1Name, Family1Age=@Family1Age, " +
                    "Family1Profession=@Family1Profession, Family2Title=@Family2Title, Family2Name=@Family2Name, Family2Age=@Family2Age, Family2Profession=@Family2Profession, " +
                    "Family3Title=@Family3Title, Family3Name=@Family3Name, Family3Age=@Family3Age, Family3Profession=@Family3Profession, Family4Title=@Family4Title, " +
                    "Family4Name=@Family4Name, Family4Age=@Family4Age, Family4Profession=@Family4Profession, Family5Title=@Family5Title, Family5Name=@Family5Name, " +
                    "Family5Age=@Family5Age, Family5Profession=@Family5Profession, Family6Title=@Family6Title, Family6Name=@Family6Name, Family6Age=@Family6Age, " +
                    "Family6Profession=@Family6Profession, GuarantorName=@GuarantorName, GuarantorUnit=@GuarantorUnit, GuarantorPost=@GuarantorPost, " +
                    "GuarantorRelationship=@GuarantorRelationship, GuarantorContact=@GuarantorContact, GuarantorContactTime=@GuarantorContactTime, " +
                    "CandidatesMessage=@CandidatesMessage, MessageExplain=@MessageExplain, Language1Name=@Language1Name, Language1Listen=@Language1Listen, " +
                    "Language1Say=@Language1Say, Language1Read=@Language1Read, Language1Write=@Language1Write, Language2Name=@Language2Name, Language2Listen=@Language2Listen, " +
                    "Language2Say=@Language2Say, Language2Read=@Language2Read, Language2Write=@Language2Write, Language3Name=@Language3Name, Language3Listen=@Language3Listen, " +
                    "Language3Say=@Language3Say, Language3Read=@Language3Read, Language3Write=@Language3Write, ExpertiseSkill1=@ExpertiseSkill1, " +
                    "ExpertiseSkill1License=@ExpertiseSkill1License, ExpertiseSkill1Progression=@ExpertiseSkill1Progression, ExpertiseSkill1GetUnit=@ExpertiseSkill1GetUnit, " +
                    "ExpertiseSkill1GetDate=@ExpertiseSkill1GetDate, ExpertiseSkill1ValidDate=@ExpertiseSkill1ValidDate, ExpertiseSkill2=@ExpertiseSkill2, " +
                    "ExpertiseSkill2License=@ExpertiseSkill2License, ExpertiseSkill2Progression=@ExpertiseSkill2Progression, ExpertiseSkill2GetUnit=@ExpertiseSkill2GetUnit, " +
                    "ExpertiseSkill2GetDate=@ExpertiseSkill2GetDate, ExpertiseSkill2ValidDate=@ExpertiseSkill2ValidDate, ExpertiseSkill3=@ExpertiseSkill3, " +
                    "ExpertiseSkill3License=@ExpertiseSkill3License, ExpertiseSkill3Progression=@ExpertiseSkill3Progression, ExpertiseSkill3GetUnit=@ExpertiseSkill3GetUnit, " +
                    "ExpertiseSkill3GetDate=@ExpertiseSkill3GetDate, ExpertiseSkill3ValidDate=@ExpertiseSkill3ValidDate, ExpertiseSkill4=@ExpertiseSkill4, " +
                    "ExpertiseSkill4License=@ExpertiseSkill4License, ExpertiseSkill4Progression=@ExpertiseSkill4Progression, ExpertiseSkill4GetUnit=@ExpertiseSkill4GetUnit, " +
                    "ExpertiseSkill4GetDate=@ExpertiseSkill4GetDate, ExpertiseSkill4ValidDate=@ExpertiseSkill4ValidDate, Disease=@Disease, DiseaseExplain=@DiseaseExplain, " +
                    "UpFileBy=(@UpFileBy), UpFileDate=(getDate())" +
                    "WHERE ID=(@ID)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringFunction(StaffData.ID);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.unit);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffName);
                cmd.Parameters.Add("@AppoIntmentDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.officeDate);
                cmd.Parameters.Add("@ResignationDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.resignDate);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.applyJob);
                cmd.Parameters.Add("@JobCapacity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.jobTitle);
                cmd.Parameters.Add("@JobGrade", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.jobLevel);
                cmd.Parameters.Add("@StaffIdentity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffTWID);
                cmd.Parameters.Add("@StaffAvatar", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffPhoto);
                cmd.Parameters.Add("@Hometown", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.comeCity);
                cmd.Parameters.Add("@Birthday", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.staffbirthday);
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.staffsex);
                cmd.Parameters.Add("@Marriage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.marriage);
                cmd.Parameters.Add("@AddressZip1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddressZip);
                cmd.Parameters.Add("@AddressCity1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.censusCity);
                cmd.Parameters.Add("@AddressOther1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddress);
                cmd.Parameters.Add("@AddressZip2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.addressZip);
                cmd.Parameters.Add("@AddressCity2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.addressCity);
                cmd.Parameters.Add("@AddressOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.address);
                cmd.Parameters.Add("@TelDaytime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.TDaytime);
                cmd.Parameters.Add("@TelNight", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.TNight);
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Phone);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffemail);
                cmd.Parameters.Add("@UrgentContactName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.EmergencyName);
                cmd.Parameters.Add("@UrgentContactAddress", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.EmergencyAddress);
                cmd.Parameters.Add("@UrgentContactTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.EmergencyPhone);
                cmd.Parameters.Add("@DoctorSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DSchoolName);
                cmd.Parameters.Add("@DoctorDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DDepartment);
                cmd.Parameters.Add("@DoctorSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DSince);
                cmd.Parameters.Add("@DoctorUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.DUntil);
                cmd.Parameters.Add("@DoctorState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study1);
                cmd.Parameters.Add("@MasterSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MSchoolName);
                cmd.Parameters.Add("@MasterDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MDepartment);
                cmd.Parameters.Add("@MasterSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MSince);
                cmd.Parameters.Add("@MasterUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.MUntil);
                cmd.Parameters.Add("@MasterState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study2);
                cmd.Parameters.Add("@UniversitySchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.USchoolName);
                cmd.Parameters.Add("@UniversityDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.UDepartment);
                cmd.Parameters.Add("@UniversitySince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.USince);
                cmd.Parameters.Add("@UniversityUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.UUntil);
                cmd.Parameters.Add("@UniversityState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study3);
                cmd.Parameters.Add("@VocationalSchoolName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VSchoolName);
                cmd.Parameters.Add("@VocationalDepartment", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VDepartment);
                cmd.Parameters.Add("@VocationalSince", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VSince);
                cmd.Parameters.Add("@VocationalUntil", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.VUntil);
                cmd.Parameters.Add("@VocationalState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.study4);
                cmd.Parameters.Add("@Experience1Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince1);
                cmd.Parameters.Add("@Experience1Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil1);
                cmd.Parameters.Add("@Experience1Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname1);
                cmd.Parameters.Add("@Experience1Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition1);
                cmd.Parameters.Add("@Experience1Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary1);
                cmd.Parameters.Add("@Experience1Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove1);
                cmd.Parameters.Add("@Experience1Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle1);
                cmd.Parameters.Add("@Experience1DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName1);
                cmd.Parameters.Add("@Experience2Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince2);
                cmd.Parameters.Add("@Experience2Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil2);
                cmd.Parameters.Add("@Experience2Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname2);
                cmd.Parameters.Add("@Experience2Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition2);
                cmd.Parameters.Add("@Experience2Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary2);
                cmd.Parameters.Add("@Experience2Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove2);
                cmd.Parameters.Add("@Experience2Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle2);
                cmd.Parameters.Add("@Experience2DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName2);
                cmd.Parameters.Add("@Experience3Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince3);
                cmd.Parameters.Add("@Experience3Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil3);
                cmd.Parameters.Add("@Experience3Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname3);
                cmd.Parameters.Add("@Experience3Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition3);
                cmd.Parameters.Add("@Experience3Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary3);
                cmd.Parameters.Add("@Experience3Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove3);
                cmd.Parameters.Add("@Experience3Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle3);
                cmd.Parameters.Add("@Experience3DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName3);
                cmd.Parameters.Add("@Experience4Since", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateSince4);
                cmd.Parameters.Add("@Experience4Until", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JDateUntil4);
                cmd.Parameters.Add("@Experience4Company", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JCname4);
                cmd.Parameters.Add("@Experience4Post", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jposition4);
                cmd.Parameters.Add("@Experience4Salary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Jsalary4);
                cmd.Parameters.Add("@Experience4Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.prove4);
                cmd.Parameters.Add("@Experience4Director", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitle4);
                cmd.Parameters.Add("@Experience4DirectorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.JTitleName4);
                cmd.Parameters.Add("@Family1Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[0][0]);
                cmd.Parameters.Add("@Family1Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[0][1]);
                cmd.Parameters.Add("@Family1Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[0][2]);
                cmd.Parameters.Add("@Family1Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[0][3]);
                cmd.Parameters.Add("@Family2Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[1][0]);
                cmd.Parameters.Add("@Family2Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[1][1]);
                cmd.Parameters.Add("@Family2Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[1][2]);
                cmd.Parameters.Add("@Family2Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[1][3]);
                cmd.Parameters.Add("@Family3Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[2][0]);
                cmd.Parameters.Add("@Family3Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[2][1]);
                cmd.Parameters.Add("@Family3Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[2][2]);
                cmd.Parameters.Add("@Family3Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[2][3]);
                cmd.Parameters.Add("@Family4Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[3][0]);
                cmd.Parameters.Add("@Family4Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[3][1]);
                cmd.Parameters.Add("@Family4Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[3][2]);
                cmd.Parameters.Add("@Family4Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[3][3]);
                cmd.Parameters.Add("@Family5Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[4][0]);
                cmd.Parameters.Add("@Family5Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[4][1]);
                cmd.Parameters.Add("@Family5Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[4][2]);
                cmd.Parameters.Add("@Family5Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[4][3]);
                cmd.Parameters.Add("@Family6Title", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[5][0]);
                cmd.Parameters.Add("@Family6Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[5][1]);
                cmd.Parameters.Add("@Family6Age", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.FamilyStatu[5][2]);
                cmd.Parameters.Add("@Family6Profession", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.FamilyStatu[5][3]);
                cmd.Parameters.Add("@GuarantorName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailName);
                cmd.Parameters.Add("@GuarantorUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailUnit);
                cmd.Parameters.Add("@GuarantorPost", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailJob);
                cmd.Parameters.Add("@GuarantorRelationship", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailRelationship);
                cmd.Parameters.Add("@GuarantorContact", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailContact);
                cmd.Parameters.Add("@GuarantorContactTime", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.bailContactTime);
                cmd.Parameters.Add("@CandidatesMessage", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.recruited);
                cmd.Parameters.Add("@MessageExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.recruitedText);
                cmd.Parameters.Add("@Language1Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.langAbility1);
                cmd.Parameters.Add("@Language1Listen", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langL1);
                cmd.Parameters.Add("@Language1Say", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langS1);
                cmd.Parameters.Add("@Language1Read", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langR1);
                cmd.Parameters.Add("@Language1Write", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langW1);
                cmd.Parameters.Add("@Language2Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.langAbility2);
                cmd.Parameters.Add("@Language2Listen", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langL2);
                cmd.Parameters.Add("@Language2Say", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langS2);
                cmd.Parameters.Add("@Language2Read", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langR2);
                cmd.Parameters.Add("@Language2Write", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langW2);
                cmd.Parameters.Add("@Language3Name", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.langAbility3);
                cmd.Parameters.Add("@Language3Listen", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langL3);
                cmd.Parameters.Add("@Language3Say", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langS3);
                cmd.Parameters.Add("@Language3Read", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langR3);
                cmd.Parameters.Add("@Language3Write", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.langW3);

                for (int i = 0; i < StaffData.SpecialtySkill.Count; i++)
                {
                    cmd.Parameters.Add("@ExpertiseSkill" + (i + 1), SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][0]);
                    cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "License", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][1]);
                    cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "Progression", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][2]);
                    cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "GetUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.SpecialtySkill[i][3]);
                    cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "GetDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.SpecialtySkill[i][4]);
                    cmd.Parameters.Add("@ExpertiseSkill" + (i + 1) + "ValidDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffData.SpecialtySkill[i][5]);

                }
                cmd.Parameters.Add("@Disease", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.disease);
                cmd.Parameters.Add("@DiseaseExplain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.diseaseText);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
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
    public string[] setCreateUserMemberPhotoData(CreateStaff StaffData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        string upPhoto = "";
        if (StaffData.staffPhoto != null)
        {
            upPhoto += " StaffAvatar=@StaffAvatar, ";
        }
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE StaffDatabase SET " + upPhoto + "UpFileBy=(@UpFileBy), UpFileDate=(getDate()) " +
                    "WHERE ID=(@ID)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringFunction(StaffData.ID);
                cmd.Parameters.Add("@StaffAvatar", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffPhoto);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                returnValue[1] = StaffData.ID;
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
    private string SearchStaffConditionReturn(SearchStaff SearchStaffConditionData)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStaffConditionData.txtstaffID != null)
        {
            ConditionReturn += " AND StaffID=(@StaffID) ";
        }
        if (SearchStaffConditionData.txtstaffName != null)
        {
            ConditionReturn += " AND StaffName like (@StaffName) ";
        }
        if (SearchStaffConditionData.txtstaffSex != null)
        {
            ConditionReturn += " AND sex=(@sex) ";
        }
        if (SearchStaffConditionData.txtstaffBirthdayStart != null && SearchStaffConditionData.txtstaffBirthdayEnd != null && SearchStaffConditionData.txtstaffBirthdayStart != DateBase && SearchStaffConditionData.txtstaffBirthdayEnd != DateBase)
        {
            ConditionReturn += " AND Birthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStaffConditionData.txtstaffJob != null)
        {
            ConditionReturn += " AND WorkItem =(@WorkItem) ";
        }
        if (SearchStaffConditionData.txtstaffUnit != null)
        {
            ConditionReturn += " AND Unit =(@Unit) ";
        }
        List<string> UserFile = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        this.personnelFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStaffDataBaseCount(SearchStaff SearchStaffConditionData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffConditionReturn(SearchStaffConditionData);
        
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM StaffDatabase WHERE isDeleted=0 " + SearchStaffCondition;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffJob);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffUnit);
                returnValue[0] = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0]= "-1";
                returnValue[1] = e.Message.ToString();
            }

        }
        return returnValue;
    }
    public List<StaffDataList> SearchStaffDataBase(int indexpage, SearchStaff SearchStaffConditionData)
    {
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffConditionReturn(SearchStaffConditionData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StaffDatabase.ID DESC) " +
                             "AS RowNum, StaffDatabase.* "+
                             "FROM StaffDatabase WHERE isDeleted=0 " +SearchStaffCondition + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffJob);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffUnit);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.ID = dr["ID"].ToString();
                    addValue.sID = dr["StaffID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sEmail = dr["Email"].ToString();
                    addValue.sSex = dr["sex"].ToString();
                    addValue.sUnit = dr["Unit"].ToString();
                    addValue.sJob = dr["WorkItem"].ToString();
                    addValue.FileDate = DateTime.Parse(dr["FileDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.Phone = dr["Phone"].ToString();
                    addValue.officeDate = DateTime.Parse(dr["AppointmentDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.resignDate = DateTime.Parse(dr["ResignationDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffDataList addValue = new StaffDataList();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }


    public string[] SearchStaffDataBaseCountCase(SearchStaff SearchStaffConditionData, string getid)
    {
        string[] returnValue = new string[3];
        returnValue[0] = "0";
        returnValue[1] = "0";
        returnValue[2] = getid.ToString();
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffConditionReturn(SearchStaffConditionData);

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM StaffDatabase WHERE isDeleted=0 " + SearchStaffCondition;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffJob);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffUnit);
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
    public List<StaffDataList> SearchStaffDataBaseCase(int indexpage, SearchStaff SearchStaffConditionData, int getid)
    {
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffConditionReturn(SearchStaffConditionData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StaffDatabase.ID DESC) " +
                             "AS RowNum, StaffDatabase.* " +
                             "FROM StaffDatabase WHERE isDeleted=0 " + SearchStaffCondition + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffJob);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffUnit);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.ID = dr["ID"].ToString();
                    addValue.sID = dr["StaffID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sEmail = dr["Email"].ToString();
                    addValue.sSex = dr["sex"].ToString();
                    addValue.sUnit = dr["Unit"].ToString();
                    addValue.sJob = dr["WorkItem"].ToString();
                    addValue.FileDate = DateTime.Parse(dr["FileDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.Phone = dr["Phone"].ToString();
                    addValue.officeDate = DateTime.Parse(dr["AppointmentDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.resignDate = DateTime.Parse(dr["ResignationDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.pw = getid.ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffDataList addValue = new StaffDataList();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }


    public StaffResult getOneStaffDataBase(string ID)
    {
        StaffResult returnValue = new StaffResult();
        returnValue.StaffBaseData = new CreateStaff();
        returnValue.WorkData = new List<staffWorkData>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StaffDatabase WHERE isDeleted=0 AND ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Int64.Parse(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateStaff addValue1 = new CreateStaff();
                    addValue1.FamilyStatu = new List<string[]>();
                    addValue1.SpecialtySkill = new List<string[]>();

                    returnValue.ID = Int64.Parse(dr["ID"].ToString());
                    addValue1.unit = dr["Unit"].ToString();
                    addValue1.staffID = dr["StaffID"].ToString();
                    addValue1.staffName = dr["StaffName"].ToString();
                    addValue1.officeDate = DateTime.Parse(dr["AppointmentDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue1.resignDate = DateTime.Parse(dr["ResignationDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue1.applyJob = dr["WorkItem"].ToString();
                    addValue1.jobTitle = dr["JobCapacity"].ToString();
                    addValue1.jobLevel = dr["JobGrade"].ToString();
                    addValue1.staffTWID = dr["StaffIdentity"].ToString();
                    addValue1.staffPhoto = dr["StaffAvatar"].ToString();
                    addValue1.comeCity = dr["Hometown"].ToString();
                    addValue1.staffbirthday = DateTime.Parse(dr["Birthday"].ToString()).ToString("yyyy-MM-dd");
                    addValue1.staffsex = dr["sex"].ToString();
                    addValue1.marriage = dr["Marriage"].ToString();
                    addValue1.censusAddress = dr["AddressOther1"].ToString();
                    addValue1.censusAddressZip = dr["AddressZip1"].ToString();
                    addValue1.censusCity = dr["AddressCity1"].ToString();
                    addValue1.address = dr["AddressOther2"].ToString();
                    addValue1.addressCity = dr["AddressCity2"].ToString();
                    addValue1.addressZip = dr["AddressZip2"].ToString();
                    addValue1.TDaytime = dr["TelDaytime"].ToString();
                    addValue1.TNight = dr["TelNight"].ToString();
                    addValue1.Phone = dr["Phone"].ToString();
                    addValue1.staffemail = dr["Email"].ToString();
                    addValue1.EmergencyName = dr["UrgentContactName"].ToString();
                    addValue1.EmergencyAddress = dr["UrgentContactAddress"].ToString();
                    addValue1.EmergencyPhone = dr["UrgentContactTel"].ToString();
                    addValue1.DSchoolName = dr["DoctorSchoolName"].ToString();
                    addValue1.DDepartment = dr["DoctorDepartment"].ToString();
                    addValue1.DSince = dr["DoctorSince"].ToString();
                    addValue1.DUntil = dr["DoctorUntil"].ToString();
                    addValue1.study1 = dr["DoctorState"].ToString();
                    addValue1.MSchoolName = dr["MasterSchoolName"].ToString();
                    addValue1.MDepartment = dr["MasterDepartment"].ToString();
                    addValue1.MSince = dr["MasterSince"].ToString();
                    addValue1.MUntil = dr["MasterUntil"].ToString();
                    addValue1.study2 = dr["MasterState"].ToString();
                    addValue1.USchoolName = dr["UniversitySchoolName"].ToString();
                    addValue1.UDepartment = dr["UniversityDepartment"].ToString();
                    addValue1.USince = dr["UniversitySince"].ToString();
                    addValue1.UUntil = dr["UniversityUntil"].ToString();
                    addValue1.study3 = dr["UniversityState"].ToString();
                    addValue1.VSchoolName = dr["VocationalSchoolName"].ToString();
                    addValue1.VDepartment = dr["VocationalDepartment"].ToString();
                    addValue1.VSince = dr["VocationalSince"].ToString();
                    addValue1.VUntil = dr["VocationalUntil"].ToString();
                    addValue1.study4 = dr["VocationalState"].ToString();
                    addValue1.JDateSince1 = dr["Experience1Since"].ToString();
                    addValue1.JDateUntil1 = dr["Experience1Until"].ToString();
                    addValue1.JCname1 = dr["Experience1Company"].ToString();
                    addValue1.Jposition1 = dr["Experience1Post"].ToString();
                    addValue1.Jsalary1 = dr["Experience1Salary"].ToString();
                    addValue1.prove1 = dr["Experience1Prove"].ToString();
                    addValue1.JTitle1 = dr["Experience1Director"].ToString();
                    addValue1.JTitleName1 = dr["Experience1DirectorName"].ToString();
                    addValue1.JDateSince2 = dr["Experience2Since"].ToString();
                    addValue1.JDateUntil2 = dr["Experience2Until"].ToString();
                    addValue1.JCname2 = dr["Experience2Company"].ToString();
                    addValue1.Jposition2 = dr["Experience2Post"].ToString();
                    addValue1.Jsalary2 = dr["Experience2Salary"].ToString();
                    addValue1.prove2 = dr["Experience2Prove"].ToString();
                    addValue1.JTitle2 = dr["Experience2Director"].ToString();
                    addValue1.JTitleName2 = dr["Experience2DirectorName"].ToString();
                    addValue1.JDateSince3 = dr["Experience3Since"].ToString();
                    addValue1.JDateUntil3 = dr["Experience3Until"].ToString();
                    addValue1.JCname3 = dr["Experience3Company"].ToString();
                    addValue1.Jposition3 = dr["Experience3Post"].ToString();
                    addValue1.Jsalary3 = dr["Experience3Salary"].ToString();
                    addValue1.prove3 = dr["Experience3Prove"].ToString();
                    addValue1.JTitle3 = dr["Experience3Director"].ToString();
                    addValue1.JTitleName3 = dr["Experience3DirectorName"].ToString();
                    addValue1.JDateSince4 = dr["Experience4Since"].ToString();
                    addValue1.JDateUntil4 = dr["Experience4Until"].ToString();
                    addValue1.JCname4 = dr["Experience4Company"].ToString();
                    addValue1.Jposition4 = dr["Experience4Post"].ToString();
                    addValue1.Jsalary4 = dr["Experience4Salary"].ToString();
                    addValue1.prove4 = dr["Experience4Prove"].ToString();
                    addValue1.JTitle4 = dr["Experience4Director"].ToString();
                    addValue1.JTitleName4 = dr["Experience4DirectorName"].ToString();

                    for (int i = 1; i <= 6; i++)
                    {
                        string[] Familyitem = new string[4];
                        Familyitem[0] = dr["Family" + i + "Title"].ToString();
                        Familyitem[1] = dr["Family" + i + "Name"].ToString();
                        Familyitem[2] = dr["Family" + i + "Age"].ToString();
                        Familyitem[3] = dr["Family" + i + "Profession"].ToString();
                        addValue1.FamilyStatu.Add(Familyitem);
                    }
                    addValue1.bailName = dr["GuarantorName"].ToString();
                    addValue1.bailUnit = dr["GuarantorUnit"].ToString();
                    addValue1.bailJob = dr["GuarantorPost"].ToString();
                    addValue1.bailRelationship = dr["GuarantorRelationship"].ToString();
                    addValue1.bailContact = dr["GuarantorContact"].ToString();
                    addValue1.bailContactTime = dr["GuarantorContactTime"].ToString();
                    addValue1.recruited = dr["CandidatesMessage"].ToString();
                    addValue1.recruitedText = dr["MessageExplain"].ToString();
                    addValue1.langAbility1 = dr["Language1Name"].ToString();
                    addValue1.langL1 = dr["Language1Listen"].ToString();
                    addValue1.langS1 = dr["Language1Say"].ToString();
                    addValue1.langR1 = dr["Language1Read"].ToString();
                    addValue1.langW1 = dr["Language1Write"].ToString();
                    addValue1.langAbility2 = dr["Language2Name"].ToString();
                    addValue1.langL2 = dr["Language2Listen"].ToString();
                    addValue1.langS2 = dr["Language2Say"].ToString();
                    addValue1.langR2 = dr["Language2Read"].ToString();
                    addValue1.langW2 = dr["Language2Write"].ToString();
                    addValue1.langAbility3 = dr["Language3Name"].ToString();
                    addValue1.langL3 = dr["Language3Listen"].ToString();
                    addValue1.langS3 = dr["Language3Say"].ToString();
                    addValue1.langR3 = dr["Language3Read"].ToString();
                    addValue1.langW3 = dr["Language3Write"].ToString();

                    for (int i = 1; i <= 4; i++)
                    {
                        string[] SpecialtySkillitem = new string[6];
                        SpecialtySkillitem[0] = dr["ExpertiseSkill" + i].ToString();
                        SpecialtySkillitem[1] = dr["ExpertiseSkill" + i + "License"].ToString();
                        SpecialtySkillitem[2] = dr["ExpertiseSkill" + i + "Progression"].ToString();
                        SpecialtySkillitem[3] = dr["ExpertiseSkill" + i + "GetUnit"].ToString();
                        SpecialtySkillitem[4] = DateTime.Parse(dr["ExpertiseSkill" + i + "GetDate"].ToString()).ToString("yyyy-MM-dd");
                        SpecialtySkillitem[5] = DateTime.Parse(dr["ExpertiseSkill" + i + "ValidDate"].ToString()).ToString("yyyy-MM-dd");
                        addValue1.SpecialtySkill.Add(SpecialtySkillitem);
                    }


                    addValue1.disease = dr["Disease"].ToString();
                    addValue1.diseaseText = dr["DiseaseExplain"].ToString();
                    addValue1.fillInDate = DateTime.Parse(dr["FileDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.StaffBaseData = addValue1;

                    returnValue.WorkData = this.GetStaffWorkData(dr["StaffID"].ToString());
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
    private List<staffWorkData> GetStaffWorkData(string staffID)
    {
        List<staffWorkData> returnValue = new List<staffWorkData>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StaffWorkRecordData WHERE isDeleted=0 AND staffID=@staffID ORDER BY WorkDate DESC";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@staffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(staffID);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    staffWorkData addValue = new staffWorkData();
                    addValue.ID = dr["WorkID"].ToString();
                    addValue.Type = dr["WorkItem"].ToString();
                    addValue.RecordDate = DateTime.Parse(dr["WorkDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.Record = dr["WorkRecord"].ToString();
                    addValue.RecordRemark = dr["WorkRemark"].ToString();
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                staffWorkData addValue = new staffWorkData();
                addValue.ID = "-1";
                addValue.Record = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] createStaffWorkData(staffWorkData WorkData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO StaffWorkRecordData (StaffID, WorkItem, WorkDate, WorkRecord, WorkRemark, CreateFileBy, UpFileBy, UpFileDate) " +
                            "VALUES (@StaffID ,@WorkItem, @WorkDate, @WorkRecord, @WorkRemark, @CreateFileBy, @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(WorkData.staffID);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(WorkData.Type);
                cmd.Parameters.Add("@WorkDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(WorkData.RecordDate);
                cmd.Parameters.Add("@WorkRecord", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(WorkData.Record);
                cmd.Parameters.Add("@WorkRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(WorkData.RecordRemark);
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
    public string[] setStaffWorkData(staffWorkData WorkData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE StaffWorkRecordData SET WorkItem=@WorkItem, WorkDate=@WorkDate, WorkRecord=@WorkRecord, WorkRemark=@WorkRemark, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE WorkID=@WorkID AND StaffID=@StaffID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@WorkID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(WorkData.ID);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(WorkData.staffID);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(WorkData.Type);
                cmd.Parameters.Add("@WorkDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(WorkData.RecordDate);
                cmd.Parameters.Add("@WorkRecord", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(WorkData.Record);
                cmd.Parameters.Add("@WorkRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(WorkData.RecordRemark);
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

    public string[] delStaffWorkData(Int64 wID, string StaffID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        DataBase Base = new DataBase();
        List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "UPDATE StaffWorkRecordData SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate())  " +
                            "WHERE WorkID=@WorkID AND StaffID=@StaffID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@WorkID", SqlDbType.Int).Value = wID;
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffID);
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
    private string[] SearchStaffMeritConditionReturn(SearchStaff SearchStaffMeritConditionData)
    {
        string[] returnValue = new string[2];
        string ConditionReturn = "";
        string ConditionReturn1 = "";
        string DataBase = "1900-01-01";
        if (SearchStaffMeritConditionData.txtstaffID != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffID=(@StaffID) ";
        }
        if (SearchStaffMeritConditionData.txtstaffName != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like (@StaffName) ";
        }
        if (SearchStaffMeritConditionData.txtstaffSex != null)
        {
            ConditionReturn += " AND StaffDatabase.sex=(@sex) ";
        }
        if (SearchStaffMeritConditionData.txtstaffBirthdayStart != null && SearchStaffMeritConditionData.txtstaffBirthdayEnd != null && SearchStaffMeritConditionData.txtstaffBirthdayStart != DataBase && SearchStaffMeritConditionData.txtstaffBirthdayEnd != DataBase)
        {
            ConditionReturn += " AND StaffDatabase.Birthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStaffMeritConditionData.txtstaffJob != null)
        {
            ConditionReturn += " AND StaffDatabase.WorkItem =(@WorkItem) ";
        }
        if (SearchStaffMeritConditionData.txtstaffUnit != null)
        {
            ConditionReturn1 += " AND EmployeePerformance.AgencyType =(@AgencyType) ";
        }
        if (SearchStaffMeritConditionData.txtstaffYear != null)
        {
            ConditionReturn1 = " AND EmployeePerformance.AcademicYear =(@AcademicYear) ";
        }
        List<string> UserFile = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        this.personnelFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn1 += " AND EmployeePerformance.Unit =" + UserFile[2] + " ";
        }
        returnValue[0] = ConditionReturn;
        returnValue[1] = ConditionReturn1;
        return returnValue;
    }
    public string[] SearchStaffMeritDataBaseCount(SearchStaff SearchStaffMeritConditionData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string[] ConditionReturn = this.SearchStaffMeritConditionReturn(SearchStaffMeritConditionData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT Count(EmployeePerformance.ID) " +
                            "FROM EmployeePerformance INNER JOIN StaffDatabase ON EmployeePerformance.EmployeeID=StaffDatabase.StaffID " +
                             ConditionReturn[0] +
                             "WHERE EmployeePerformance.isDeleted=0 " + ConditionReturn[1];
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffMeritConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffMeritConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffMeritConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffJob);
                cmd.Parameters.Add("@AgencyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffUnit);
                cmd.Parameters.Add("@AcademicYear", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffYear);
                returnValue[0] = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] ="-1";
                returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
    }

    public List<StaffMeritDataList> SearchStaffMeritDataBase(int indexpage, SearchStaff SearchStaffMeritConditionData)
    {
        List<StaffMeritDataList> returnValue = new List<StaffMeritDataList>();
        DataBase Base = new DataBase();
        string[] ConditionReturn = this.SearchStaffMeritConditionReturn(SearchStaffMeritConditionData);
        
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY EmployeePerformance.ID DESC) "+
                            "AS RowNum, EmployeePerformance.*,StaffDatabase.StaffID,StaffDatabase.StaffName,StaffDatabase.sex,StaffDatabase.Unit " +
                            "FROM EmployeePerformance INNER JOIN StaffDatabase ON EmployeePerformance.EmployeeID=StaffDatabase.StaffID " +
                             ConditionReturn[0] +
                             "WHERE EmployeePerformance.isDeleted=0 " + ConditionReturn[1] + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffMeritConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffMeritConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffMeritConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@WorkItem", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffJob);
                cmd.Parameters.Add("@AgencyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffUnit);
                cmd.Parameters.Add("@AcademicYear", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffMeritConditionData.txtstaffYear);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffMeritDataList addValue = new StaffMeritDataList();
                    addValue.sData = new StaffDataList();
                    addValue.sData.sID = dr["StaffID"].ToString();
                    addValue.sData.sName = dr["StaffName"].ToString();
                    addValue.sData.sSex = dr["sex"].ToString();
                    addValue.sData.sUnit = dr["Unit"].ToString();

                    addValue.MID = dr["ID"].ToString();
                    addValue.sUnit = int.Parse(dr["AgencyType"].ToString());
                    addValue.AY = int.Parse(dr["AcademicYear"].ToString());
                    addValue.AScore = int.Parse(dr["AttendanceScore"].ToString());
                    addValue.PScore = int.Parse(dr["ProfessionalScore"].ToString());
                    addValue.WScore = int.Parse(dr["WorkAttitudeScore"].ToString());
                    addValue.AddScore = int.Parse(dr["AddScore"].ToString());
                    addValue.LowerScore = int.Parse(dr["LowerScore"].ToString());
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffMeritDataList addValue = new StaffMeritDataList();
                addValue.sData.sID = "-1";
                addValue.sData.sName = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }
    public string[] createStaffMeritDataBase(List<string> MeritData, int Grade, string sID, int sUnit)
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
                string sql = "INSERT INTO EmployeePerformance (AcademicYear, EmployeeID, AgencyType, AttendanceScore, ProfessionalScore, WorkAttitudeScore, AddScore, LowerScore, Grade, isDeleted) " +
                            "VALUES (@AcademicYear, @EmployeeID, @AgencyType, @AttendanceScore, @ProfessionalScore, @WorkAttitudeScore, @AddScore, @LowerScore, @Grade, 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@AcademicYear", SqlDbType.Int).Value = int.Parse(MeritData[0]);
                cmd.Parameters.Add("@EmployeeID", SqlDbType.NVarChar).Value = sID;
                cmd.Parameters.Add("@AgencyType", SqlDbType.TinyInt).Value = sUnit;
                cmd.Parameters.Add("@AttendanceScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[2]);
                cmd.Parameters.Add("@ProfessionalScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[3]);
                cmd.Parameters.Add("@WorkAttitudeScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[4]);
                cmd.Parameters.Add("@AddScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[5]);
                cmd.Parameters.Add("@LowerScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[6]);
                cmd.Parameters.Add("@Grade", SqlDbType.TinyInt).Value = Grade;
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

    public string[] setStaffMeritDataBase(Int64 MID, int Grade, List<string> MeritData)
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
                string sql = "UPDATE EmployeePerformance SET AttendanceScore=(@AttendanceScore), "+
                            "ProfessionalScore=(@ProfessionalScore), WorkAttitudeScore=(@WorkAttitudeScore), AddScore=(@AddScore), LowerScore=(@LowerScore), Grade=(@Grade) " +
                            "WHERE ID=(@MID) AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@MID", SqlDbType.Int).Value = MID;

                cmd.Parameters.Add("@AttendanceScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[0]);
                cmd.Parameters.Add("@ProfessionalScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[1]);
                cmd.Parameters.Add("@WorkAttitudeScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[2]);
                cmd.Parameters.Add("@AddScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[3]);
                cmd.Parameters.Add("@LowerScore", SqlDbType.TinyInt).Value = int.Parse(MeritData[4]);
                cmd.Parameters.Add("@Grade", SqlDbType.TinyInt).Value = Grade;
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
    public string[] delStaffMeritDataBase(Int64 MID)
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
                string sql = "UPDATE EmployeePerformance SET isDeleted=1  " +
                            "WHERE ID=(@MID) AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@MID", SqlDbType.Int).Value = MID;
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



    public List<StaffDataList> getAllStaffDataList(List<int> WorkItem)
    {
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string itemStr = "";
                if (WorkItem.Count > 0)
                {
                    foreach (int item in WorkItem)
                    {
                        if (item != 0)
                        {
                            if (itemStr.Length > 0)
                            {
                                itemStr += " OR ";
                            }
                            itemStr += " WorkItem= " + item;
                        }
                    }
                    if (itemStr.Length > 0) {
                        itemStr = " AND " + itemStr;
                    }
                }
                StaffDataBase sDB = new StaffDataBase();
                sDB.personnelFunction();
                List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                if (int.Parse(sDB._StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
                {
                    itemStr += " AND Unit =" + UserFile[2] + " ";
                }
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = "SELECT * FROM StaffDatabase WHERE isDeleted=0  and ResignationDate = '1900-01-01' " + itemStr + "order by StaffID ASC";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[2].ToString());
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.sID = dr["StaffID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sEmail = dr["Email"].ToString();
                    addValue.sUnit = dr["Unit"].ToString();
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffDataList addValue = new StaffDataList();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }
    public string[] createStaffCreditData(CreateStaffUpgrade StaffUpgrade)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO StaffUpgradeCredit (CreditDate, CreditTeacher, Topics, Hours, Prove, CreditNumber, Remark, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                            "VALUES (@CreditDate, @CreditTeacher, @Topics, @Hours, @Prove, @CreditNumber, @Remark, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CreditDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffUpgrade.courseDate);
                cmd.Parameters.Add("@CreditTeacher", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.courseLecturer);
                cmd.Parameters.Add("@Topics", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.courseName);
                cmd.Parameters.Add("@Hours", SqlDbType.Float).Value = Chk.CheckFloatFunction(StaffUpgrade.courseTime);
                cmd.Parameters.Add("@Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.courseProve);
                cmd.Parameters.Add("@CreditNumber", SqlDbType.Float).Value = Chk.CheckFloatFunction(StaffUpgrade.courseCredit);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.otherExplanation);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('StaffUpgradeCredit') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["cID"].ToString());
                    }
                    dr.Close();
                    List<int> itemValue = new List<int>(); 
                    for (int i = 0; i < StaffUpgrade.Participants.Count; i++)
                    {
                        List<string> StaffData = this.getStaffDataName(StaffUpgrade.Participants[i].sID);
                        if (StaffData[0] != "-1")
                        {
                            sql = "INSERT INTO StaffUpgradeCreditStaff (Unit, CreditID, StaffID, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                                    "VALUES (@Unit, @CreditID, @StaffID, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                            cmd = new SqlCommand(sql, Sqlconn);
                            cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData[2]);
                            cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = Column;
                            cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffData[0]);
                            cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            itemValue.Add(cmd.ExecuteNonQuery());
                        }
                    }

                    returnValue[1] = Convert.ToInt32(!itemValue.Contains(0)).ToString();
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1]= e.Message.ToString();
            }
        }
        return returnValue;
    }
    private string[] SearchStaffCreditConditionReturn(SearchStaffCredit SearchStructure)
    {
        string[] ConditionReturnValue = new string[2];
        string ConditionReturn1 = "";
        string ConditionReturn2 = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstaffName != null)
        {
            ConditionReturn1 += " AND b.StaffID IN (SELECT c.StaffID FROM StaffDatabase c WHERE c.StaffID=b.StaffID AND c.StaffName like (@txtstaffName) )";
        }
        if (SearchStructure.txtLecturer != null)
        {
            ConditionReturn2 += " AND a.CreditTeacher like (@txtLecturer) ";
        }
        if (SearchStructure.txtDateStart != null && SearchStructure.txtDateEnd != null && SearchStructure.txtDateStart != DateBase && SearchStructure.txtDateEnd != DateBase)
        {
            ConditionReturn2 += " AND a.CreditDate BETWEEN (@sCreditDateStart) AND (@sCreditDateEnd) ";
        }
        if (SearchStructure.txtProve != null && SearchStructure.txtProve != "0")
        {
            ConditionReturn2 += " AND a.Prove =@txtProve ";
        }
        List<string> UserFile = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        this.personnelFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn2 += " AND a.Unit =" + UserFile[2] + " ";
        }
        ConditionReturnValue[0] = ConditionReturn1;
        ConditionReturnValue[1] = ConditionReturn2;
        return ConditionReturnValue;
    }
    public string[] SearchStaffCreditDataBaseCount(SearchStaffCredit SearchStaffUpgrade)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                string[] ConditionReturn = this.SearchStaffCreditConditionReturn(SearchStaffUpgrade);
                Sqlconn.Open();
                string sql = "SELECT count(DISTINCT NewTable.CreditID) AS QCOUNT ,SUM(NewTable.CreditNumber) AS SumNumber FROM( "+
                            "SELECT DISTINCT a.* FROM StaffUpgradeCredit AS a Left JOIN StaffUpgradeCreditStaff AS b " +
                                "ON a.CreditID=b.CreditID AND b.isDeleted=0 " +
                                "WHERE a.isDeleted=0 " + ConditionReturn[0] + ConditionReturn[1] +
                                " ) AS NewTable";
                    /*"SELECT count(DISTINCT a.CreditID) FROM StaffUpgradeCredit AS a Left JOIN StaffUpgradeCreditStaff AS b " +
                                "ON a.CreditID=b.CreditID AND b.isDeleted=0 " +
                                "WHERE a.isDeleted=0 " + ConditionReturn[0] + ConditionReturn[1];*/
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtstaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffUpgrade.txtstaffName) + "%";
                cmd.Parameters.Add("@sCreditDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateStart);
                cmd.Parameters.Add("@sCreditDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateEnd);
                cmd.Parameters.Add("@txtLecturer", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffUpgrade.txtLecturer) + "%";
                cmd.Parameters.Add("@txtProve", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffUpgrade.txtProve);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue[0] = dr["QCOUNT"].ToString();
                    returnValue[1] = dr["SumNumber"].ToString();

                }
                dr.Close();
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

    public List<CreateStaffUpgrade> SearchStaffCreditDataBase(int indexpage, SearchStaffCredit SearchStaffUpgrade)
    {
        List<CreateStaffUpgrade> returnValue = new List<CreateStaffUpgrade>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                string[] ConditionReturn = this.SearchStaffCreditConditionReturn(SearchStaffUpgrade);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY MuliTable.CreditID DESC) " +
                             "AS RowNum,MuliTable.* FROM "+
                             "( SELECT DISTINCT a.* FROM StaffUpgradeCredit AS a Left JOIN StaffUpgradeCreditStaff AS b " +
                                "ON a.CreditID=b.CreditID AND b.isDeleted=0 " +
                                "WHERE a.isDeleted=0 " + ConditionReturn[0] + ConditionReturn[1] +" ) "+
                                "AS MuliTable )"+
                                "AS NewTable " +
                                "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                /*SELECT * FROM (
                    SELECT ROW_NUMBER() OVER (ORDER BY MuliTable.CreditID DESC) 
                    AS RowNum,MuliTable.* FROM (
                    SELECT DISTINCT a.* FROM StaffUpgradeCredit AS a Left JOIN StaffUpgradeCreditStaff AS b
                    ON a.CreditID=b.CreditID AND b.isDeleted=0 
                    WHERE a.isDeleted=0 AND b.StaffID
                    IN (SELECT  c.StaffID FROM StaffDatabase c WHERE c.StaffID=b.StaffID AND c.StaffName like '曾%'))
                    AS MuliTable
                    )
                    AS NewTable*/
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtstaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffUpgrade.txtstaffName) + "%";
                cmd.Parameters.Add("@sCreditDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateStart);
                cmd.Parameters.Add("@sCreditDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateEnd);
                cmd.Parameters.Add("@txtLecturer", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffUpgrade.txtLecturer) + "%";
                cmd.Parameters.Add("@txtProve", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffUpgrade.txtProve);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateStaffUpgrade addValue = new CreateStaffUpgrade();
                    addValue.ID = dr["CreditID"].ToString();
                    addValue.courseDate = DateTime.Parse(dr["CreditDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.courseLecturer = dr["CreditTeacher"].ToString();
                    addValue.courseName = dr["Topics"].ToString();
                    addValue.courseTime = dr["Hours"].ToString();
                    addValue.courseProve = dr["Prove"].ToString();
                    addValue.courseCredit = dr["CreditNumber"].ToString();
                    addValue.otherExplanation = dr["Remark"].ToString();
                    addValue.Participants = new List<StaffDataList>();
                    addValue.Participants = this.getCreditParticipants(addValue.ID);
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateStaffUpgrade addValue = new CreateStaffUpgrade();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    private List<StaffDataList> getCreditParticipants(string cID){
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StaffUpgradeCreditStaff.*,StaffDatabase.StaffName FROM StaffUpgradeCreditStaff "+
                    "INNER JOIN StaffDatabase ON StaffUpgradeCreditStaff.StaffID=StaffDatabase.StaffID "+
                    "WHERE CreditID=@CreditID AND StaffUpgradeCreditStaff.isDeleted=0 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = int.Parse(cID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.ID = dr["ID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sID = dr["StaffID"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffDataList addValue = new StaffDataList();
                addValue.ID = "-1";
                addValue.sName = e.Message.ToString(); ;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] setStaffCreditDataBase(CreateStaffUpgrade StaffUpgrade)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE StaffUpgradeCredit SET CreditDate=@CreditDate, CreditTeacher=@CreditTeacher, Topics=@Topics, Hours=@Hours, Prove=@Prove, "+
                    "CreditNumber=@CreditNumber, Remark=@Remark, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE CreditID=@CreditID AND isDeleted=0 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.ID);
                cmd.Parameters.Add("@CreditDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffUpgrade.courseDate);
                cmd.Parameters.Add("@CreditTeacher", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.courseLecturer);
                cmd.Parameters.Add("@Topics", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.courseName);
                cmd.Parameters.Add("@Hours", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.courseTime);
                cmd.Parameters.Add("@Prove", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.courseProve);
                cmd.Parameters.Add("@CreditNumber", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.courseCredit);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.otherExplanation);
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

    public string[] delStaffCreditDataBase(Int64 cID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE StaffUpgradeCredit SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE CreditID=@CreditID AND isDeleted=0 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = cID;
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
    public string[] setStaffCreditParticipantDataBase(string cID, List<string> DelParticipantsID, List<string> NewParticipantsValue)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "";
                SqlCommand cmd;
                List<int> delreturnValue = new List<int>();
                for (int i = 0; i < DelParticipantsID.Count; i++)
                {
                    sql = "UPDATE StaffUpgradeCreditStaff SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                        "WHERE ID=@ID AND CreditID=@CreditID AND isDeleted=0 ";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(DelParticipantsID[i]);
                    cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(cID);
                    cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                    delreturnValue.Add(cmd.ExecuteNonQuery());
                }
                List<int> addreturnValue = new List<int>();
                for (int i = 0; i < NewParticipantsValue.Count; i++)
                {
                    string StaffUpgradeCreditID = "";
                    sql = "SELECT ID FROM StaffUpgradeCreditStaff WHERE isDeleted=1 AND StaffID=@StaffID AND CreditID=@CreditID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(NewParticipantsValue[i]);
                    cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(cID);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        StaffUpgradeCreditID = dr["ID"].ToString();
                    }
                    dr.Close();

                    if (StaffUpgradeCreditID.Length == 0)
                    {
                        List<string> StaffData = this.getStaffDataName(NewParticipantsValue[i]);
                        sql = "INSERT INTO StaffUpgradeCreditStaff (Unit, CreditID, StaffID, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                                "VALUES (@Unit, @CreditID, @StaffID, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData[2]);
                        cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(cID);
                        cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffData[0]);
                        cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                        cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                        addreturnValue.Add(cmd.ExecuteNonQuery());
                    }
                    else {
                        sql = "UPDATE StaffUpgradeCreditStaff SET isDeleted=0, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                        "WHERE ID=@ID AND CreditID=@CreditID AND isDeleted=1 ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StaffUpgradeCreditID;
                        cmd.Parameters.Add("@CreditID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(cID);
                        cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                        addreturnValue.Add(cmd.ExecuteNonQuery());
                    }
                }

                returnValue[0] = Convert.ToInt32(!delreturnValue.Contains(0)).ToString(); ;
                returnValue[1] = Convert.ToInt32(!addreturnValue.Contains(0)).ToString();
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

    //***/

    private string[] SearchStaffBehaveConditionReturn(SearchStaffBehave SearchStructure)
    {
        string[] ConditionReturnValue = new string[2];
        string ConditionReturn1 = "";
        string ConditionReturn2 = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstaffName1 != null)
        {
            ConditionReturn1 += " AND StaffDatabase.StaffName like (@txtstaffName) ";
        }
        if (SearchStructure.txtType != null)
        {
            ConditionReturn2 += " AND StaffUpgradeBehave.Category = @txtType ";
        }
        if (SearchStructure.txtDateStart1 != null && SearchStructure.txtDateEnd1 != null && SearchStructure.txtDateStart1 != DateBase && SearchStructure.txtDateEnd1 != DateBase)
        {
            ConditionReturn2 += " AND StaffUpgradeBehave.PublicationDate BETWEEN (@sDateStart) AND (@sDateEnd) ";
        }
        List<string> UserFile = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        this.personnelFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn2 += " AND StaffUpgradeBehave.Unit =" + UserFile[2] + " ";
        }
        ConditionReturnValue[0] = ConditionReturn1;
        ConditionReturnValue[1] = ConditionReturn2;
        return ConditionReturnValue;
    }
    public string[] SearchStaffBehaveDataBaseCount(SearchStaffBehave SearchStaffUpgrade)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                string[] ConditionReturn = this.SearchStaffBehaveConditionReturn(SearchStaffUpgrade);
                Sqlconn.Open();
                string sql = "SELECT count(BehaveID) FROM StaffUpgradeBehave INNER JOIN StaffDatabase ON StaffUpgradeBehave.StaffID=StaffDatabase.StaffID " +ConditionReturn[0] +
                            "WHERE StaffUpgradeBehave.isDeleted=0 " + ConditionReturn[1];
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtstaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffUpgrade.txtstaffName1) + "%";
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateStart1);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateEnd1);
                cmd.Parameters.Add("@txtType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffUpgrade.txtType);
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

    public List<CreateStaffUpgradeSeries> SearchStaffBehaveDataBase(int indexpage, SearchStaffBehave SearchStaffUpgrade)
    {
        List<CreateStaffUpgradeSeries> returnValue = new List<CreateStaffUpgradeSeries>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                string[] ConditionReturn = this.SearchStaffBehaveConditionReturn(SearchStaffUpgrade);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StaffUpgradeBehave.BehaveID DESC) " +
                             "AS RowNum,StaffUpgradeBehave.*,StaffDatabase.StaffName  FROM StaffUpgradeBehave INNER JOIN StaffDatabase ON StaffUpgradeBehave.StaffID=StaffDatabase.StaffID " + ConditionReturn[0] +
                             "WHERE StaffUpgradeBehave.isDeleted=0 " + ConditionReturn[1] + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtstaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffUpgrade.txtstaffName1) + "%";
                cmd.Parameters.Add("@sDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateStart1);
                cmd.Parameters.Add("@sDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffUpgrade.txtDateEnd1);
                cmd.Parameters.Add("@txtType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffUpgrade.txtType);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateStaffUpgradeSeries addValue = new CreateStaffUpgradeSeries();
                    addValue.ID = dr["BehaveID"].ToString();
                    addValue.articleDate = DateTime.Parse(dr["PublicationDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.author = dr["StaffName"].ToString();
                    addValue.serialNumber = dr["Amount"].ToString();
                    addValue.seriesTitle = dr["PublicationName"].ToString();
                    addValue.volume = dr["Period"].ToString();
                    addValue.articleTitle = dr["ArticleTitle"].ToString();
                    addValue.articleType = dr["Category"].ToString();
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateStaffUpgradeSeries addValue = new CreateStaffUpgradeSeries();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] createSerialDataBase(CreateStaffUpgradeSeries StaffUpgrade)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                List<string> UserFileName = this.getStaffDataName(StaffUpgrade.author);
                Sqlconn.Open();
                string sql = "INSERT INTO StaffUpgradeBehave (Unit, StaffID, PublicationDate, Amount, PublicationName, Period, ArticleTitle, Category, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                            "VALUES (@Unit,@StaffID,@PublicationDate,@Amount,@PublicationName,@Period,@ArticleTitle,@Category, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(UserFileName[2]);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.author);
                cmd.Parameters.Add("@PublicationDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(StaffUpgrade.articleDate);
                cmd.Parameters.Add("@Amount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.serialNumber);
                cmd.Parameters.Add("@PublicationName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.seriesTitle);
                cmd.Parameters.Add("@Period", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.volume);
                cmd.Parameters.Add("@ArticleTitle", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffUpgrade.articleTitle);
                cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffUpgrade.articleType);
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

    public string[] createNewTeacherDataBase(CreateNewTeacher NewTeacherData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                List<string> StaffFileName = this.getStaffDataName(NewTeacherData.teacher);
                Sqlconn.Open();
                string sql = "INSERT INTO NewTeacherPerformanceRating (Unit,TeacherID, RatingDate, CaseReport, BasicTheory, TeachingPractice, Result, TeachingContentAdvantage, " +
                        "TeachingContentShortcoming, TeachingAidAdvantage, TeachingAidShortcoming, TeachingLangAdvantage, TeachingLangShortcoming, TeachingSkillAdvantage, " +
                        "TeachingSkillShortcoming, InteractionAdvantage, InteractionShortcoming, ParentAdvisoryAdvantage, ParentAdvisoryShortcoming, OverallPerformanceAdvantage, " +
                        "OverallPerformanceShortcoming, Director, Supervisor, Trainer, Collation, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                        "VALUES (@Unit, @TeacherID, @RatingDate, @CaseReport, @BasicTheory, @TeachingPractice, @Result, @TeachingContentAdvantage, @TeachingContentShortcoming, " +
                        "@TeachingAidAdvantage, @TeachingAidShortcoming, @TeachingLangAdvantage, @TeachingLangShortcoming, @TeachingSkillAdvantage, @TeachingSkillShortcoming, " +
                        "@InteractionAdvantage, @InteractionShortcoming, @ParentAdvisoryAdvantage, @ParentAdvisoryShortcoming, @OverallPerformanceAdvantage, " +
                        "@OverallPerformanceShortcoming, @Director, @Supervisor, @Trainer, @Collation, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffFileName[2]);
                cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(NewTeacherData.teacher);
                cmd.Parameters.Add("@RatingDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(NewTeacherData.evaluationDate);
                cmd.Parameters.Add("@CaseReport", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(NewTeacherData.reportPer);
                cmd.Parameters.Add("@BasicTheory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(NewTeacherData.basicPer);
                cmd.Parameters.Add("@TeachingPractice", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(NewTeacherData.teachPer);
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.rated);
                cmd.Parameters.Add("@TeachingContentAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.contentMerit);
                cmd.Parameters.Add("@TeachingContentShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.contentDefect);
                cmd.Parameters.Add("@TeachingAidAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.useMerit);
                cmd.Parameters.Add("@TeachingAidShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.useDefect);
                cmd.Parameters.Add("@TeachingLangAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.langMerit);
                cmd.Parameters.Add("@TeachingLangShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.langDefect);
                cmd.Parameters.Add("@TeachingSkillAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.skillMerit);
                cmd.Parameters.Add("@TeachingSkillShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.skillDefect);
                cmd.Parameters.Add("@InteractionAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.ExchangeMerit);
                cmd.Parameters.Add("@InteractionShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.ExchangeDefect);
                cmd.Parameters.Add("@ParentAdvisoryAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.AdvisoryMerit);
                cmd.Parameters.Add("@ParentAdvisoryShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.AdvisoryDefect);
                cmd.Parameters.Add("@OverallPerformanceAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.OverallMerit);
                cmd.Parameters.Add("@OverallPerformanceShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.OverallDefect);
                cmd.Parameters.Add("@Director", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(NewTeacherData.director);
                cmd.Parameters.Add("@Supervisor", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.prison);
                cmd.Parameters.Add("@Trainer", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.trainPerson);
                cmd.Parameters.Add("@Collation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.file);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('NewTeacherPerformanceRating') AS cID";
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
    public CreateNewTeacher getNewTeacherDataBase(string tID)
    {
        CreateNewTeacher returnValue = new CreateNewTeacher();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();

                string sql = "SELECT nT.*,a.StaffName AS teacherName, a.AppointmentDate, b.StaffName AS trainPersonName,c.StaffName AS fileName1 FROM NewTeacherPerformanceRating AS nT " +
                    "INNER JOIN StaffDatabase AS a ON nT.TeacherID=a.StaffID " +
                    "LEFT JOIN StaffDatabase AS b ON nT.Trainer=b.StaffID " +
                    "LEFT JOIN StaffDatabase AS c ON nT.Collation=c.StaffID " +
                    "WHERE nT.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(tID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.officeDate = DateTime.Parse(dr["AppointmentDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.teacher = dr["TeacherID"].ToString();
                    returnValue.teacherName = dr["teacherName"].ToString();
                    returnValue.evaluationDate = DateTime.Parse(dr["RatingDate"].ToString()).ToString("yyyy-MM-dd");;
                    returnValue.reportPer = dr["CaseReport"].ToString();
                    returnValue.basicPer = dr["BasicTheory"].ToString();
                    returnValue.teachPer = dr["TeachingPractice"].ToString();
                    returnValue.rated = dr["Result"].ToString();
                    returnValue.contentMerit = dr["TeachingContentAdvantage"].ToString();
                    returnValue.contentDefect = dr["TeachingContentShortcoming"].ToString();
                    returnValue.useMerit = dr["TeachingAidAdvantage"].ToString();
                    returnValue.useDefect = dr["TeachingAidShortcoming"].ToString();
                    returnValue.langMerit = dr["TeachingLangAdvantage"].ToString();
                    returnValue.langDefect = dr["TeachingLangShortcoming"].ToString();
                    returnValue.skillMerit = dr["TeachingSkillAdvantage"].ToString();
                    returnValue.skillDefect = dr["TeachingSkillShortcoming"].ToString();
                    returnValue.ExchangeMerit = dr["InteractionAdvantage"].ToString();
                    returnValue.ExchangeDefect = dr["InteractionShortcoming"].ToString();
                    returnValue.AdvisoryMerit = dr["ParentAdvisoryAdvantage"].ToString();
                    returnValue.AdvisoryDefect = dr["ParentAdvisoryShortcoming"].ToString();
                    returnValue.OverallMerit = dr["OverallPerformanceAdvantage"].ToString();
                    returnValue.OverallDefect = dr["OverallPerformanceShortcoming"].ToString();
                    returnValue.director = dr["Director"].ToString();
                    returnValue.prison = dr["Supervisor"].ToString();
                    returnValue.trainPerson = dr["Trainer"].ToString();
                    returnValue.trainPersonName = dr["trainPersonName"].ToString();
                    returnValue.file = dr["Collation"].ToString();
                    returnValue.fileName = dr["fileName1"].ToString();
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

    public string[] setNewTeacherDataBase(CreateNewTeacher NewTeacherData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                List<string> CreateFileName = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "UPDATE NewTeacherPerformanceRating SET RatingDate = @RatingDate, CaseReport = @CaseReport, BasicTheory = @BasicTheory, "+
                             "TeachingPractice = @TeachingPractice, Result = @Result, TeachingContentAdvantage = @TeachingContentAdvantage, " +
                             "TeachingContentShortcoming = @TeachingContentShortcoming, TeachingAidAdvantage = @TeachingAidAdvantage, " +
                             "TeachingAidShortcoming = @TeachingAidShortcoming, TeachingLangAdvantage = @TeachingLangAdvantage, " +
                             "TeachingLangShortcoming = @TeachingLangShortcoming, TeachingSkillAdvantage = @TeachingSkillAdvantage, " +
                             "TeachingSkillShortcoming = @TeachingSkillShortcoming, InteractionAdvantage = @InteractionAdvantage, " +
                             "InteractionShortcoming = @InteractionShortcoming, ParentAdvisoryAdvantage = @ParentAdvisoryAdvantage, " +
                             "ParentAdvisoryShortcoming = @ParentAdvisoryShortcoming, OverallPerformanceAdvantage = @OverallPerformanceAdvantage, " +
                             "OverallPerformanceShortcoming = @OverallPerformanceShortcoming, Director = @Director, Supervisor = @Supervisor, " +
                             "Trainer = @Trainer, Collation = @Collation, UpFileBy=@UpFileBy, UpFileDate=(getDate()) "+
                             "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(NewTeacherData.ID);
                cmd.Parameters.Add("@RatingDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(NewTeacherData.evaluationDate);
                cmd.Parameters.Add("@CaseReport", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(NewTeacherData.reportPer);
                cmd.Parameters.Add("@BasicTheory", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(NewTeacherData.basicPer);
                cmd.Parameters.Add("@TeachingPractice", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(NewTeacherData.teachPer);
                cmd.Parameters.Add("@Result", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.rated);
                cmd.Parameters.Add("@TeachingContentAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.contentMerit);
                cmd.Parameters.Add("@TeachingContentShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.contentDefect);
                cmd.Parameters.Add("@TeachingAidAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.useMerit);
                cmd.Parameters.Add("@TeachingAidShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.useDefect);
                cmd.Parameters.Add("@TeachingLangAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.langMerit);
                cmd.Parameters.Add("@TeachingLangShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.langDefect);
                cmd.Parameters.Add("@TeachingSkillAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.skillMerit);
                cmd.Parameters.Add("@TeachingSkillShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.skillDefect);
                cmd.Parameters.Add("@InteractionAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.ExchangeMerit);
                cmd.Parameters.Add("@InteractionShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.ExchangeDefect);
                cmd.Parameters.Add("@ParentAdvisoryAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.AdvisoryMerit);
                cmd.Parameters.Add("@ParentAdvisoryShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.AdvisoryDefect);
                cmd.Parameters.Add("@OverallPerformanceAdvantage", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.OverallMerit);
                cmd.Parameters.Add("@OverallPerformanceShortcoming", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.OverallDefect);
                cmd.Parameters.Add("@Director", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(NewTeacherData.director);
                cmd.Parameters.Add("@Supervisor", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.prison);
                cmd.Parameters.Add("@Trainer", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.trainPerson);
                cmd.Parameters.Add("@Collation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(NewTeacherData.file);
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

    private string[] SearchTeacherRstimateConditionReturn(SearchTeacherRstimate SearchConditionData)
    {
        string[] returnValue = new string[2];
        string ConditionReturn = "";
        string ConditionReturn1 = "";
        string DataBase = "1900-01-01";
        if (SearchConditionData.txtTeacher != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like (@txtTeacher) ";
        }
        if (SearchConditionData.txtTeacherSex != null)
        {
            ConditionReturn += " AND StaffDatabase.sex = (@txtTeacherSex) ";
        }
        if (SearchConditionData.txtTeacherUnit != null)
        {
            ConditionReturn1 += " AND NewTeacherPerformanceRating.Unit =(@txtTeacherUnit) ";
        }
        if (SearchConditionData.txtassessmentStart != null && SearchConditionData.txtassessmentEnd != null && SearchConditionData.txtassessmentStart != DataBase && SearchConditionData.txtassessmentEnd != DataBase)
        {
            ConditionReturn1 += " AND NewTeacherPerformanceRating.RatingDate BETWEEN (@sassessmentStart) AND (@sassessmentEnd) ";
        }
        List<string> UserFile = this.getStaffDataName(HttpContext.Current.User.Identity.Name);
        this.personnelFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn1 += " AND NewTeacherPerformanceRating.Unit =" + UserFile[2] + " ";
        }
        returnValue[0] = ConditionReturn;
        returnValue[1] = ConditionReturn1;
        return returnValue;
    }
    public string[] SearchTeacherRstimateDataBaseCount(SearchTeacherRstimate SearchTeacher)
    {
       string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string[] ConditionReturn = this.SearchTeacherRstimateConditionReturn(SearchTeacher);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT Count(NewTeacherPerformanceRating.ID) " +
                            "FROM NewTeacherPerformanceRating INNER JOIN StaffDatabase ON NewTeacherPerformanceRating.TeacherID=StaffDatabase.StaffID " +
                             ConditionReturn[0] +
                             "WHERE NewTeacherPerformanceRating.isDeleted=0 " + ConditionReturn[1];
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtTeacher", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchTeacher.txtTeacher) + "%";
                cmd.Parameters.Add("@txtTeacherSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchTeacher.txtTeacherSex);
                cmd.Parameters.Add("@sassessmentStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchTeacher.txtassessmentStart);
                cmd.Parameters.Add("@sassessmentEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchTeacher.txtassessmentEnd);
                cmd.Parameters.Add("@txtTeacherUnit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchTeacher.txtTeacherUnit);
                returnValue[0] = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue[0] ="-1";
                returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
    }

    public List<SearchTeacherRstimateResult> SearchTeacherRstimateDataBase(int indexpage, SearchTeacherRstimate SearchTeacher)
    {
        List<SearchTeacherRstimateResult> returnValue = new List<SearchTeacherRstimateResult>();
        DataBase Base = new DataBase();
        string[] ConditionReturn = this.SearchTeacherRstimateConditionReturn(SearchTeacher);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY NewTeacherPerformanceRating.ID DESC) " +
                            "AS RowNum, NewTeacherPerformanceRating.*,StaffDatabase.StaffName " +
                            "FROM NewTeacherPerformanceRating INNER JOIN StaffDatabase ON NewTeacherPerformanceRating.TeacherID=StaffDatabase.StaffID " +
                             ConditionReturn[0] +
                             "WHERE NewTeacherPerformanceRating.isDeleted=0 " + ConditionReturn[1] + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtTeacher", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchTeacher.txtTeacher) + "%";
                cmd.Parameters.Add("@txtTeacherSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchTeacher.txtTeacherSex);
                cmd.Parameters.Add("@sassessmentStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchTeacher.txtassessmentStart);
                cmd.Parameters.Add("@sassessmentEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchTeacher.txtassessmentEnd);
                cmd.Parameters.Add("@txtTeacherUnit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchTeacher.txtTeacherUnit);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchTeacherRstimateResult addValue = new SearchTeacherRstimateResult();
                    addValue.Number = dr["RowNum"].ToString();
                    addValue.ID = dr["ID"].ToString();
                    addValue.TeacherName = dr["StaffName"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.AssessmentDate = DateTime.Parse(dr["RatingDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchTeacherRstimateResult addValue = new SearchTeacherRstimateResult();
                addValue.ID = "-1";
                addValue.TeacherName = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }
    public string[] createWorkDateData(string StaffID) {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO WorkRecord (StaffID, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                        "VALUES (@StaffID, 0, getDate(), 0, getDate())";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffID);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message.ToString();
            }
        }
        return returnValue;
    }


    public List<string> SearchStaff(string SearchString)
    {
        List<string> returnValue = new List<string>();
        string atom = "";

        DataBase Base = new DataBase();


        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT top 10 staffid,staffName FROM StaffDatabase WHERE isDeleted=0 AND (staffid like  '%' + @StudentID + '%' or " +
                    "staffName like '%' + @StudentName + '%') and ResignationDate = '1900-01-01' ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = SearchString;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = SearchString;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    atom = dr["staffName"].ToString() + "(" + dr["staffid"].ToString() + ")";

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
}
