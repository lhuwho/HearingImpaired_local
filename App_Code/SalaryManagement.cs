using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

/// <summary>
/// Summary description for SalaryManagement
/// </summary>
public class SalaryManagement
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
    public string[] _StaffhaveRoles = new string[5] { "0", "0", "0", "0", "0" };
	public SalaryManagement()
	{
		//
		// TODO: Add constructor logic here
		//
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.salary[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.salary[1];//跨區與否
	}
    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }
    public string[] createStaffContractedSalaryData(CreateStaffContractedSalary structData)
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
                List<string> staffFileName = sDB.getStaffDataName(structData.staffID);
                Sqlconn.Open();
                string sql = "INSERT INTO StaffNarrativeSalary(Unit, StaffID, WriteDate, LaborInsurance, HealthInsurance, PensionFunds, PensionFundsPer, WithholdingTax, " +
                    "EducationalBackground, EducationCount, Years, YearsCount, WorkItem, JobGrade, PostCount, Director, DirectorCount, Special, SpecialCount, TotalCount, " +
                    "BaseSalary, Explain, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                    "VALUES(@Unit, @StaffID, @WriteDate, @LaborInsurance, @HealthInsurance, @PensionFunds, @PensionFundsPer, @WithholdingTax, @EducationalBackground, " +
                    "@EducationCount, @Years, @YearsCount, @WorkItem, @JobGrade, @PostCount, @Director, @DirectorCount, @Special, @SpecialCount, @TotalCount, " +
                    "@BaseSalary, @Explain, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(staffFileName[2]);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.staffID);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(structData.fillInDate);
                cmd.Parameters.Add("@LaborInsurance", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.laborInsurance);
                cmd.Parameters.Add("@HealthInsurance", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.healthInsurance);
                cmd.Parameters.Add("@PensionFunds", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.pensionFunds);
                cmd.Parameters.Add("@PensionFundsPer", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(structData.pensionFundsPer);
                cmd.Parameters.Add("@WithholdingTax", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.withholdingTax);
                cmd.Parameters.Add("@EducationalBackground", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.education);
                cmd.Parameters.Add("@educationCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count1);
                cmd.Parameters.Add("@Years", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.years);
                cmd.Parameters.Add("@YearsCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count2);
                cmd.Parameters.Add("@WorkItem", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.applyJob);
                cmd.Parameters.Add("@JobGrade", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.jobLevel);
                cmd.Parameters.Add("@PostCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count3);
                cmd.Parameters.Add("@Director", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.director);
                cmd.Parameters.Add("@DirectorCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count4);
                cmd.Parameters.Add("@Special", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.special);
                cmd.Parameters.Add("@SpecialCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count5);
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.total);
                cmd.Parameters.Add("@BaseSalary", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.totalSalary);
                cmd.Parameters.Add("@Explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.explanation);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('StaffNarrativeSalary') AS cID";
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

    public CreateStaffContractedSalary getStaffContractedSalaryData(string cID)
    {
        CreateStaffContractedSalary returnValue = new CreateStaffContractedSalary();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();

                string sql = "SELECT StaffNarrativeSalary.*,StaffDatabase.StaffName FROM StaffNarrativeSalary " +
                    "INNER JOIN StaffDatabase ON StaffNarrativeSalary.StaffID=StaffDatabase.StaffID "+
                    "WHERE StaffNarrativeSalary.isDeleted=0 AND StaffNarrativeSalary.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(cID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.staffID = dr["StaffID"].ToString();
                    returnValue.staffName = dr["StaffName"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.laborInsurance = dr["LaborInsurance"].ToString();
                    returnValue.healthInsurance = dr["HealthInsurance"].ToString();
                    returnValue.pensionFunds = dr["PensionFunds"].ToString();
                    returnValue.pensionFundsPer = dr["PensionFundsPer"].ToString();
                    returnValue.withholdingTax = dr["WithholdingTax"].ToString();
                    returnValue.education = dr["EducationalBackground"].ToString();
                    returnValue.count1 = dr["EducationCount"].ToString();
                    returnValue.years = dr["Years"].ToString();
                    returnValue.count2 = dr["YearsCount"].ToString();
                    returnValue.applyJob = dr["WorkItem"].ToString();
                    returnValue.jobLevel = dr["JobGrade"].ToString();
                    returnValue.count3 = dr["PostCount"].ToString();
                    returnValue.director = dr["Director"].ToString();
                    returnValue.count4 = dr["DirectorCount"].ToString();
                    returnValue.special = dr["Special"].ToString();
                    returnValue.count5 = dr["SpecialCount"].ToString();
                    returnValue.total = dr["TotalCount"].ToString();
                    returnValue.totalSalary = dr["BaseSalary"].ToString();
                    returnValue.explanation = dr["Explain"].ToString();
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

    public string[] setStaffContractedSalaryData(CreateStaffContractedSalary structData)
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
                List<string> staffFileName = sDB.getStaffDataName(structData.staffID);
                Sqlconn.Open();
                string sql = "UPDATE StaffNarrativeSalary SET LaborInsurance=@LaborInsurance, HealthInsurance=@HealthInsurance, PensionFunds=@PensionFunds, PensionFundsPer=@PensionFundsPer, " +
                    "WithholdingTax=@WithholdingTax, EducationalBackground=@EducationalBackground, EducationCount=@EducationCount, Years=@Years, YearsCount=@YearsCount, "+
                    "WorkItem=@WorkItem, JobGrade=@JobGrade, PostCount=@PostCount, Director=@Director, DirectorCount=@DirectorCount, Special=@Special, "+
                    "SpecialCount=@SpecialCount, TotalCount=@TotalCount, BaseSalary=@BaseSalary, Explain=@Explain, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.ID);
                cmd.Parameters.Add("@LaborInsurance", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.laborInsurance);
                cmd.Parameters.Add("@HealthInsurance", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.healthInsurance);
                cmd.Parameters.Add("@PensionFunds", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.pensionFunds);
                cmd.Parameters.Add("@PensionFundsPer", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(structData.pensionFundsPer);
                cmd.Parameters.Add("@WithholdingTax", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.withholdingTax);
                cmd.Parameters.Add("@EducationalBackground", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.education);
                cmd.Parameters.Add("@educationCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count1);
                cmd.Parameters.Add("@Years", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.years);
                cmd.Parameters.Add("@YearsCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count2);
                cmd.Parameters.Add("@WorkItem", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.applyJob);
                cmd.Parameters.Add("@JobGrade", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.jobLevel);
                cmd.Parameters.Add("@PostCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count3);
                cmd.Parameters.Add("@Director", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.director);
                cmd.Parameters.Add("@DirectorCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count4);
                cmd.Parameters.Add("@Special", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.special);
                cmd.Parameters.Add("@SpecialCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.count5);
                cmd.Parameters.Add("@TotalCount", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.total);
                cmd.Parameters.Add("@BaseSalary", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.totalSalary);
                cmd.Parameters.Add("@Explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.explanation);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
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


    public List<string[]> getStaffSalaryexplanationData(string staffID)
    {
        List<string[]> returnValue = new List<string[]>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();

                string sql = "SELECT * FROM StaffNarrativeSalary WHERE isDeleted=0 AND StaffID=@StaffID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(staffID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] addValue = new string[2];
                    addValue[0] = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue[1] = dr["Explain"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string[] addValue = new string[2];
                addValue[0] = "-1";
                addValue[1] = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public CreateStaffContractedSalary getStaffContractedSalaryLatestData(string staff, string LimitDate, string nowID)
    {
        CreateStaffContractedSalary returnValue = new CreateStaffContractedSalary();
        DataBase Base = new DataBase();
        string LimitValueItem = "";
        if (LimitDate.Length > 0) {
            LimitValueItem = " AND WriteDate < @WriteDate AND StaffNarrativeSalary.ID != @ID ";
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();

                string sql = "SELECT Top 1 StaffNarrativeSalary.*,StaffDatabase.StaffName FROM StaffNarrativeSalary " +
                    "INNER JOIN StaffDatabase ON StaffNarrativeSalary.StaffID=StaffDatabase.StaffID " +
                    "WHERE StaffNarrativeSalary.isDeleted=0 AND StaffNarrativeSalary.StaffID=@StaffID " + LimitValueItem + "ORDER BY WriteDate DESC";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(staff);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(LimitDate);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(nowID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.staffID = dr["StaffID"].ToString();
                    returnValue.staffName = dr["StaffName"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.laborInsurance = dr["LaborInsurance"].ToString();
                    returnValue.healthInsurance = dr["HealthInsurance"].ToString();
                    returnValue.pensionFunds = dr["PensionFunds"].ToString();
                    returnValue.pensionFundsPer = dr["PensionFundsPer"].ToString();
                    returnValue.withholdingTax = dr["WithholdingTax"].ToString();
                    returnValue.education = dr["EducationalBackground"].ToString();
                    returnValue.count1 = dr["EducationCount"].ToString();
                    returnValue.years = dr["Years"].ToString();
                    returnValue.count2 = dr["YearsCount"].ToString();
                    returnValue.applyJob = dr["WorkItem"].ToString();
                    returnValue.jobLevel = dr["JobGrade"].ToString();
                    returnValue.count3 = dr["PostCount"].ToString();
                    returnValue.director = dr["Director"].ToString();
                    returnValue.count4 = dr["DirectorCount"].ToString();
                    returnValue.special = dr["Special"].ToString();
                    returnValue.count5 = dr["SpecialCount"].ToString();
                    returnValue.total = dr["TotalCount"].ToString();
                    returnValue.totalSalary = dr["BaseSalary"].ToString();
                    returnValue.explanation = dr["Explain"].ToString();
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

    private string SearchStaffContractedSalaryConditionReturn(SearchStaff SearchStaffConditionData)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStaffConditionData.txtstaffID != null)
        {
            ConditionReturn += " AND StaffNarrativeSalary.StaffID=(@StaffID) ";
        }
        if (SearchStaffConditionData.txtstaffName != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like (@StaffName) ";
        }
        if (SearchStaffConditionData.txtstaffSex != null)
        {
            ConditionReturn += " AND StaffDatabase.sex=(@sex) ";
        }
        if (SearchStaffConditionData.txtstaffBirthdayStart != null && SearchStaffConditionData.txtstaffBirthdayEnd != null && SearchStaffConditionData.txtstaffBirthdayStart != DateBase && SearchStaffConditionData.txtstaffBirthdayEnd != DateBase)
        {
            ConditionReturn += " AND StaffDatabase.Birthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStaffConditionData.txtstaffUnit != null)
        {
            ConditionReturn += " AND StaffNarrativeSalary.Unit =(@Unit) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND StaffNarrativeSalary.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStaffContractedSalaryBaseCount(SearchStaff SearchStaffConditionData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffContractedSalaryConditionReturn(SearchStaffConditionData);

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM StaffNarrativeSalary "+
                    "INNER JOIN StaffDatabase ON StaffNarrativeSalary.StaffID=StaffDatabase.StaffID "+
                    "WHERE StaffNarrativeSalary.isDeleted=0 " + SearchStaffCondition;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
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
    public List<StaffDataList> SearchStaffContractedSalaryData(int indexpage, SearchStaff SearchStaffConditionData)
    {
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffContractedSalaryConditionReturn(SearchStaffConditionData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StaffNarrativeSalary.WriteDate DESC) " +
                             "AS RowNum, StaffNarrativeSalary.*,StaffDatabase.StaffName, StaffDatabase.sex  " +
                             "FROM StaffNarrativeSalary " +
                            "INNER JOIN StaffDatabase ON StaffNarrativeSalary.StaffID=StaffDatabase.StaffID WHERE StaffNarrativeSalary.isDeleted=0 " + SearchStaffCondition + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffUnit);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.ID = dr["ID"].ToString();
                    addValue.sID = dr["StaffID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sSex = dr["sex"].ToString();
                    addValue.sUnit = dr["Unit"].ToString();
                    addValue.FileDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
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

    /*private bool StaffSalaryRealWagesFunction(CreateStaffSalary structData)//再次計算總點數是否正確 (可刪除
    {
        bool returnValue = false;

        CreateStaffContractedSalary StaffContractedSalary = this.getStaffContractedSalaryLatestData(structData.staffID);
        int laborInsurance = Chk.CheckStringtoIntFunction(StaffContractedSalary.laborInsurance);
        int healthInsurance = Chk.CheckStringtoIntFunction(StaffContractedSalary.healthInsurance);
        int pensionFunds = Chk.CheckStringtoIntFunction(StaffContractedSalary.pensionFunds);
        int withholdingTax = Chk.CheckStringtoIntFunction(StaffContractedSalary.withholdingTax);
        int baseSalary = Chk.CheckStringtoIntFunction(StaffContractedSalary.totalSalary);
        int salaryDeductions = Chk.CheckStringtoIntFunction(structData.salaryDeductions);
        int addSalary = 0;
        for (int i = 0; i < structData.addItem.Count; i++)
        {
            addSalary += Chk.CheckStringtoIntFunction(structData.addItem[i].projectMoney);
        }
        int minusSalary = 0;
        for (int i = 0; i < structData.minusItem.Count; i++)
        {
            minusSalary += Chk.CheckStringtoIntFunction(structData.minusItem[i].projectMoney);
        }
        int realWages = baseSalary - laborInsurance - healthInsurance - pensionFunds - withholdingTax - salaryDeductions + addSalary - minusSalary;
        if (realWages == Chk.CheckStringtoIntFunction(structData.realWages))
        {
            returnValue = true;
        }
        return returnValue;
    }*/
    public string[] createStaffSalaryData(CreateStaffSalary structData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";
        /*CreateStaffContractedSalary StaffContractedSalary = this.getStaffContractedSalaryLatestData(structData.staffID);
        if (this.StaffSalaryRealWagesFunction(structData))
        {*/
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> staffFileName = sDB.getStaffDataName(structData.staffID);

                    Sqlconn.Open();
                    string sql = "INSERT INTO StaffSalary(Unit, SalaryYear, SalaryMonth, StaffID, WriteDate, LaborInsurance, Explain1, HealthInsurance, Explain2, PensionFunds, " +
                        "PensionFundsPer, Explain3, WithholdingTax, Explain4, BaseSalary, Explain5, PayrollDeductions, Explain6, NetTotal,CreateFileBy, CreateFileDate, UpFileBy, " +
                        "UpFileDate, isDeleted) " +
                        "VALUES(@Unit, @SalaryYear, @SalaryMonth, @StaffID, @WriteDate, @LaborInsurance, @Explain1, @HealthInsurance, @Explain2, @PensionFunds, " +
                        "@PensionFundsPer, @Explain3, @WithholdingTax, @Explain4, @BaseSalary, @Explain5, @PayrollDeductions, @Explain6, @NetTotal,@CreateFileBy, (getDate()), " +
                        "@UpFileBy, (getDate()), 0)";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(staffFileName[2]);
                    cmd.Parameters.Add("@SalaryYear", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.yearDate);
                    cmd.Parameters.Add("@SalaryMonth", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(structData.monthDate);
                    cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.staffID);
                    cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(structData.fillInDate);
                    cmd.Parameters.Add("@LaborInsurance", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.laborInsurance);
                    cmd.Parameters.Add("@Explain1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain1);
                    cmd.Parameters.Add("@HealthInsurance", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.healthInsurance);
                    cmd.Parameters.Add("@Explain2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain2);
                    cmd.Parameters.Add("@PensionFunds", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.pensionFunds);
                    cmd.Parameters.Add("@PensionFundsPer", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.pensionFundsPer);
                    cmd.Parameters.Add("@Explain3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain3);
                    cmd.Parameters.Add("@WithholdingTax", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.withholdingTax);
                    cmd.Parameters.Add("@Explain4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain4);
                    cmd.Parameters.Add("@BaseSalary", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.totalSalary);
                    cmd.Parameters.Add("@Explain5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain5);
                    cmd.Parameters.Add("@PayrollDeductions", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.salaryDeductions);
                    cmd.Parameters.Add("@Explain6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain6);
                    cmd.Parameters.Add("@NetTotal", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.realWages);
                    cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                    cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                    returnValue[0] = cmd.ExecuteNonQuery().ToString();
                    if (returnValue[0] != "0")
                    {
                        sql = "select IDENT_CURRENT('StaffSalary') AS cID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            returnValue[1] = dr["cID"].ToString();
                        }
                        dr.Close();
                    }
                    Sqlconn.Close();
                    if (structData.addItem.Count > 0)
                    {
                        this.createStaffSalaryItemData(structData.addItem, returnValue[1], 2);
                    }
                    if (structData.minusItem.Count > 0)
                    {
                        this.createStaffSalaryItemData(structData.minusItem, returnValue[1], 1);
                    }

                }
                catch (Exception e)
                {
                    returnValue[0] = "-1";
                    returnValue[1] = e.Message;
                }
            }
        /*}
        else
        {
            returnValue[0] = "-2";
            returnValue[1] = "薪資計算錯誤";
        }*/
        return returnValue;
    }
    private void createStaffSalaryItemData(List<StaffSalaryList> structDataList, string SalaryID, int Category)
    { 
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "";
                SqlCommand cmd;
                for (int i = 0; i < structDataList.Count; i++)
                {
                    StaffSalaryList structData = structDataList[i];
                    sql = "INSERT INTO StaffSalaryTable(SalaryID, Category, Item, Money, Explain, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                          "VALUES(@SalaryID, @Category, @Item, @Money, @Explain, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@SalaryID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(SalaryID);
                    cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Category;
                    cmd.Parameters.Add("@Item", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.project);
                    cmd.Parameters.Add("@Money", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.projectMoney);
                    cmd.Parameters.Add("@Explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.explain);
                    cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                    cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                    cmd.ExecuteNonQuery();
                }
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                string aa = e.Message;
            }
        }
    }
    public CreateStaffSalary getStaffSalaryData(string sID) {
        CreateStaffSalary returnValue = new CreateStaffSalary();
        returnValue.addItem = new List<StaffSalaryList>();
        returnValue.minusItem = new List<StaffSalaryList>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT StaffSalary.*,StaffDatabase.StaffName FROM StaffSalary " +
                            "INNER JOIN StaffDatabase ON StaffSalary.StaffID=StaffDatabase.StaffID " +
                            "WHERE StaffSalary.isDeleted=0 AND StaffSalary.ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(sID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.yearDate = dr["SalaryYear"].ToString();
                    returnValue.monthDate = dr["SalaryMonth"].ToString();
                    returnValue.staffID = dr["StaffID"].ToString();
                    returnValue.staffName = dr["StaffName"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.laborInsurance = dr["LaborInsurance"].ToString();
                    returnValue.salaryExplain1 = dr["Explain1"].ToString();
                    returnValue.healthInsurance = dr["HealthInsurance"].ToString();
                    returnValue.salaryExplain2 = dr["Explain2"].ToString();
                    returnValue.pensionFunds = dr["PensionFunds"].ToString();
                    returnValue.pensionFundsPer = dr["PensionFundsPer"].ToString();
                    returnValue.salaryExplain3 = dr["Explain3"].ToString();
                    returnValue.withholdingTax = dr["WithholdingTax"].ToString();
                    returnValue.salaryExplain4 = dr["Explain4"].ToString();
                    returnValue.totalSalary = dr["BaseSalary"].ToString();
                    returnValue.salaryExplain5 = dr["Explain5"].ToString();
                    returnValue.salaryDeductions = dr["PayrollDeductions"].ToString();
                    returnValue.salaryExplain6 = dr["Explain6"].ToString();
                    returnValue.realWages = dr["NetTotal"].ToString();
                }
                dr.Close();

                sql = "SELECT * FROM StaffSalaryTable WHERE isDeleted=0 AND SalaryID=@SalaryID";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@SalaryID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(sID);
                SqlDataReader dr2 = cmd.ExecuteReader();
                while (dr2.Read())
                {
                    StaffSalaryList addValue = new StaffSalaryList();
                    addValue.ID = dr2["ID"].ToString();
                    addValue.salaryID = dr2["SalaryID"].ToString();
                    addValue.project = dr2["Item"].ToString();
                    addValue.projectMoney = dr2["Money"].ToString();
                    addValue.explain = dr2["explain"].ToString();
                    string Category = dr2["Category"].ToString();
                    if (Category == "1") {
                        returnValue.minusItem.Add(addValue);
                    }
                    else if (Category == "2") {
                        returnValue.addItem.Add(addValue);
                    }
                }
                dr2.Close();

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
    public string[] setStaffSalaryData(CreateStaffSalary structData, List<string> DelAddItem, List<string> DelMinusItem)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "";

        //if (this.StaffSalaryRealWagesFunction(structData))
        //{
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> staffFileName = sDB.getStaffDataName(structData.staffID);

                    Sqlconn.Open();
                    string sql = "UPDATE StaffSalary SET Explain1=@Explain1, Explain2=@Explain2, Explain3=@Explain3, Explain4=@Explain4, Explain5=@Explain5, "+
                                "PayrollDeductions=@PayrollDeductions, Explain6=@Explain6, NetTotal=@NetTotal, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                                "WHERE ID=@ID";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(structData.ID);
                    cmd.Parameters.Add("@Explain1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain1);
                    cmd.Parameters.Add("@Explain2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain2);
                    cmd.Parameters.Add("@Explain3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain3);
                    cmd.Parameters.Add("@Explain4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain4);
                    cmd.Parameters.Add("@Explain5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain5);
                    cmd.Parameters.Add("@PayrollDeductions", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.salaryDeductions);
                    cmd.Parameters.Add("@Explain6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.salaryExplain6);
                    cmd.Parameters.Add("@NetTotal", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.realWages);
                    cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                    returnValue[0] = cmd.ExecuteNonQuery().ToString();
                    Sqlconn.Close();
                    if (structData.addItem.Count > 0)
                    {
                        this.setStaffSalaryItemData(structData.addItem, structData.ID, 2);
                    }
                    if (structData.minusItem.Count > 0)
                    {
                        this.setStaffSalaryItemData(structData.minusItem, structData.ID, 1);
                    }
                    if(DelAddItem.Count > 0){
                        this.delStaffSalaryItemData(DelAddItem, structData.ID, 2);
                    }
                    if (DelMinusItem.Count > 0)
                    {
                        this.delStaffSalaryItemData(DelMinusItem, structData.ID, 1);
                    }
                }
                catch (Exception e)
                {
                    returnValue[0] = "-1";
                    returnValue[1] = e.Message;
                }
            }
        /*}
        else
        {
            returnValue[0] = "-2";
            returnValue[1] = "薪資計算錯誤";
        }*/
        return returnValue;
    }

    private void setStaffSalaryItemData(List<StaffSalaryList> structDataList, string SalaryID, int Category)
    {
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "";
                SqlCommand cmd;
                for (int i = 0; i < structDataList.Count; i++)
                {
                    StaffSalaryList structData = structDataList[i];
                    Int64 CID = 0;
                    sql = "SELECT ID FROM StaffSalaryTable WHERE SalaryID=@SalaryID AND Category=@Category AND Item=@Item";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@SalaryID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(SalaryID);
                    cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Category;
                    cmd.Parameters.Add("@Item", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.project);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CID = Int64.Parse(dr["ID"].ToString());
                    }
                    dr.Close();
                    if (CID == 0)
                    {
                        sql = "INSERT INTO StaffSalaryTable(SalaryID, Category, Item, Money, Explain, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                              "VALUES(@SalaryID, @Category, @Item, @Money, @Explain, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@SalaryID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(SalaryID);
                        cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Category;
                        cmd.Parameters.Add("@Item", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.project);
                        cmd.Parameters.Add("@Money", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.projectMoney);
                        cmd.Parameters.Add("@Explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.explain);
                        cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                        cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                        cmd.ExecuteNonQuery();
                    }
                    else {
                        sql = "UPDATE StaffSalaryTable SET Money=@Money, Explain=@Explain, isDeleted=0, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                              "WHERE ID=@ID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = CID;
                        cmd.Parameters.Add("@Money", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structData.projectMoney);
                        cmd.Parameters.Add("@Explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structData.explain);
                        cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                        cmd.ExecuteNonQuery();
                    }
                    cmd.Clone();
                }
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                string aa = e.Message;
            }
        }
    }

    private void delStaffSalaryItemData(List<string> structDataList, string SalaryID, int Category)
    {
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "";
                SqlCommand cmd;
                for (int i = 0; i < structDataList.Count; i++)
                {
                    Int64 CID = 0;
                    sql = "SELECT ID FROM StaffSalaryTable WHERE SalaryID=@SalaryID AND Category=@Category AND Item=@Item";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@SalaryID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(SalaryID);
                    cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Category;
                    cmd.Parameters.Add("@Item", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structDataList[i]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        CID = Int64.Parse(dr["ID"].ToString());
                    }
                    dr.Close();
                    if (CID != 0)
                    {
                        sql = "UPDATE StaffSalaryTable SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                              "WHERE ID=@ID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = CID;
                        cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                        cmd.ExecuteNonQuery().ToString();
                    }
                    cmd.Clone();
                }
                Sqlconn.Close();

            }
            catch (Exception e)
            {
                string aa = e.Message;
            }
        }
    }


    private string SearchStaffSalaryConditionReturn(SearchStaff SearchStaffConditionData)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStaffConditionData.txtstaffID != null)
        {
            ConditionReturn += " AND StaffSalary.StaffID=(@StaffID) ";
        }
        if (SearchStaffConditionData.txtstaffName != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like (@StaffName) ";
        }
        if (SearchStaffConditionData.txtstaffSex != null)
        {
            ConditionReturn += " AND StaffDatabase.sex=(@sex) ";
        }
        if (SearchStaffConditionData.txtstaffBirthdayStart != null && SearchStaffConditionData.txtstaffBirthdayEnd != null && SearchStaffConditionData.txtstaffBirthdayStart != DateBase && SearchStaffConditionData.txtstaffBirthdayEnd != DateBase)
        {
            ConditionReturn += " AND StaffDatabase.Birthday BETWEEN (@sBirthdayStart) AND (@sBirthdayEnd) ";
        }
        if (SearchStaffConditionData.txtstaffUnit != null)
        {
            ConditionReturn += " AND StaffSalary.Unit =(@Unit) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND StaffSalary.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStaffSalaryBaseCount(SearchStaff SearchStaffConditionData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffSalaryConditionReturn(SearchStaffConditionData);

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM StaffSalary " +
                    "INNER JOIN StaffDatabase ON StaffSalary.StaffID=StaffDatabase.StaffID " +
                    "WHERE StaffSalary.isDeleted=0 " + SearchStaffCondition;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
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
    public List<StaffDataList> SearchStaffSalaryData(int indexpage, SearchStaff SearchStaffConditionData)
    {
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchStaffSalaryConditionReturn(SearchStaffConditionData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY StaffSalary.WriteDate DESC) " +
                             "AS RowNum, StaffSalary.*,StaffDatabase.StaffName, StaffDatabase.sex  " +
                             "FROM StaffSalary " +
                            "INNER JOIN StaffDatabase ON StaffSalary.StaffID=StaffDatabase.StaffID WHERE StaffSalary.isDeleted=0 " + SearchStaffCondition + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffID);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                cmd.Parameters.Add("@sex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayStart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStaffConditionData.txtstaffBirthdayEnd);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStaffConditionData.txtstaffUnit);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.ID = dr["ID"].ToString();
                    addValue.sID = dr["StaffID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sSex = dr["sex"].ToString();
                    addValue.sUnit = dr["Unit"].ToString();
                    addValue.FileDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
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

    private string SearchExternalTeacherDataBaseConditionReturn(SearchExternal SearchStaffConditionData)
    {
        string ConditionReturn = "";
        if (SearchStaffConditionData.txtstaffName != null)
        {
            ConditionReturn = " AND TeachName like (@StaffName)";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] SearchExternalTeacherDataBaseCount(SearchExternal SearchStaffConditionData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchExternalTeacherDataBaseConditionReturn(SearchStaffConditionData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM ExternalTeacherDatabase WHERE isDeleted=0 " + SearchStaffCondition;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
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
    public List<SearchExternalResult> SearchExternalTeacherDataBase(int indexpage, SearchExternal SearchStaffConditionData)
    {
        List<SearchExternalResult> returnValue = new List<SearchExternalResult>();
        DataBase Base = new DataBase();
        string SearchStaffCondition = this.SearchExternalTeacherDataBaseConditionReturn(SearchStaffConditionData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY ID DESC) " +
                             "AS RowNum, * " +
                             "FROM ExternalTeacherDatabase WHERE isDeleted=0 " + SearchStaffCondition + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StaffName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStaffConditionData.txtstaffName) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchExternalResult addValue = new SearchExternalResult();
                    addValue.ID = dr["ID"].ToString();
                    addValue.sName = dr["TeachName"].ToString();
                    addValue.sEmail = dr["Email"].ToString();
                    addValue.Phone = dr["Phone"].ToString();
                    addValue.Phone2 = dr["Phone2"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchExternalResult addValue = new SearchExternalResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }

    public string[] createExternalTeacherData(CreateExternal StaffData)
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
                string sql = "INSERT INTO ExternalTeacherDatabase (Unit, TeachName, TeachIdentity, AddressZip1, AddressCity1, AddressOther1, AddressZip2, AddressCity2, AddressOther2, Phone, Phone2, Email,  CreateFileBy, CreateFileDate, UpFileBy, UpFileDate ) " +
                            "VALUES (@Unit, @TeachName, @TeachIdentity, @AddressZip1, @AddressCity1, @AddressOther1, @AddressZip2, @AddressCity2, @AddressOther2, @Phone, @Phone2, @Email, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@TeachName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffName);
                cmd.Parameters.Add("@TeachIdentity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffTWID);
                cmd.Parameters.Add("@AddressZip1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddressZip);
                cmd.Parameters.Add("@AddressCity1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.censusCity);
                cmd.Parameters.Add("@AddressOther1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddress);
                cmd.Parameters.Add("@AddressZip2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.addressZip);
                cmd.Parameters.Add("@AddressCity2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.addressCity);
                cmd.Parameters.Add("@AddressOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.address);
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Phone);
                cmd.Parameters.Add("@Phone2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Phone2);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffemail);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    sql = "select IDENT_CURRENT('ExternalTeacherDatabase') AS cID";
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

    public CreateExternal getExternalTeacherDataBase(string ID)
    {
        CreateExternal returnValue = new CreateExternal();
        returnValue.WorkData = new List<ExternalWorkData>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM ExternalTeacherDatabase WHERE isDeleted=0 AND ID=(@ID)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Int64.Parse(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["ID"].ToString();
                    returnValue.staffName = dr["TeachName"].ToString();
                    returnValue.staffTWID = dr["TeachIdentity"].ToString();
                    returnValue.censusAddress = dr["AddressOther1"].ToString();
                    returnValue.censusAddressZip = dr["AddressZip1"].ToString();
                    returnValue.censusCity = dr["AddressCity1"].ToString();
                    returnValue.address = dr["AddressOther2"].ToString();
                    returnValue.addressCity = dr["AddressCity2"].ToString();
                    returnValue.addressZip = dr["AddressZip2"].ToString();
                    returnValue.Phone = dr["Phone"].ToString();
                    returnValue.Phone2 = dr["Phone2"].ToString();
                    returnValue.staffemail = dr["Email"].ToString();

                    returnValue.WorkData = this.GetExternalTeacherWorkData(returnValue.ID);

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

    private List<ExternalWorkData> GetExternalTeacherWorkData(string staffID)
    {
        List<ExternalWorkData> returnValue = new List<ExternalWorkData>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM ExternalTeacherWorkRecordData WHERE isDeleted=0 AND ExternalID=@ExternalID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ExternalID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(staffID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ExternalWorkData addValue = new ExternalWorkData();
                    addValue.ID = dr["WorkID"].ToString();
                    addValue.CourseDate1 = DateTime.Parse(dr["WorkDate1"].ToString()).ToString("yyyy-MM-dd");
                    addValue.CourseDate2 = DateTime.Parse(dr["WorkDate2"].ToString()).ToString("yyyy-MM-dd");
                    addValue.Course = dr["CourseName"].ToString();
                    addValue.CoursePrice = dr["WorkReward"].ToString();
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                ExternalWorkData addValue = new ExternalWorkData();
                addValue.ID = "-1";
                addValue.Course = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] setExternalTeacherDataBase(CreateExternal StaffData)
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

                string sql = "UPDATE ExternalTeacherDatabase SET TeachName = @TeachName, TeachIdentity = @TeachIdentity, AddressZip1 = @AddressZip1, " +
                                "AddressCity1 = @AddressCity1, AddressOther1 = @AddressOther1, AddressZip2 = @AddressZip2, AddressCity2 = @AddressCity2, " +
                                "AddressOther2 = @AddressOther2, Phone = @Phone, Phone2 = @Phone2, Email = @Email, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                                "WHERE ID=@ID AND isDeleted=0 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(StaffData.ID);
                cmd.Parameters.Add("@TeachName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffName);
                cmd.Parameters.Add("@TeachIdentity", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffTWID);
                cmd.Parameters.Add("@AddressZip1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddressZip);
                cmd.Parameters.Add("@AddressCity1", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.censusCity);
                cmd.Parameters.Add("@AddressOther1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.censusAddress);
                cmd.Parameters.Add("@AddressZip2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.addressZip);
                cmd.Parameters.Add("@AddressCity2", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StaffData.addressCity);
                cmd.Parameters.Add("@AddressOther2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.address);
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Phone);
                cmd.Parameters.Add("@Phone2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.Phone2);
                cmd.Parameters.Add("@Email", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StaffData.staffemail);
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

    public string[] createExternalTeacherWorkDataBase(ExternalWorkData SearchTeacher)
    {
        string[] returnValue = new string[2]; ;
        DataBase Base = new DataBase();
        StaffDataBase sDB = new StaffDataBase();
        List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "INSERT INTO ExternalTeacherWorkRecordData (ExternalID, WorkDate1, WorkDate2, CourseName, WorkReward, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate) " +
                            "VALUES (@ExternalID, @WorkDate1, @WorkDate2, @CourseName, @WorkReward, @CreateFileBy, getDate(), @UpFileBy, getDate())";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ExternalID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(SearchTeacher.ExternalID);
                cmd.Parameters.Add("@WorkDate1", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchTeacher.CourseDate1);
                cmd.Parameters.Add("@WorkDate2", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchTeacher.CourseDate2);
                cmd.Parameters.Add("@CourseName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchTeacher.Course);
                cmd.Parameters.Add("@WorkReward", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchTeacher.CoursePrice);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string item = e.Message.ToString();
                returnValue[0] = "-1";
                returnValue[1] = item;
            }
        }
        return returnValue;
    }

    public string[] delExternalTeacherDataBase(string cID)
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
                string sql = "UPDATE ExternalTeacherWorkRecordData SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE WorkID=@WorkID AND isDeleted=0 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@WorkID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(cID);
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
}

