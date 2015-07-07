using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for OtherDataBase
/// </summary>
public class OtherDataBase
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
    public string[] _StaffhaveRoles = new string[5] { "0", "0", "0", "0", "0" };
	public OtherDataBase()
	{
		//
		// TODO: Add constructor logic here
		//
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.stationery[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.stationery[1];//跨區與否
	}
    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }
    public string[] CreateRemindSystem(CreateRemind RemindSystemData)
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
                string sql = "INSERT INTO RemindList (ExecutorType, Executor, RemindContent, RemindDate, CompleteDate, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                    "VALUES(@ExecutorType, @Executor, @RemindContent, @RemindDate, @CompleteDate, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ExecutorType", SqlDbType.TinyInt).Value = 0;//Chk.CheckStringtoIntFunction(RemindSystemData.rType);
                cmd.Parameters.Add("@Executor", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RemindSystemData.recipientID);
                cmd.Parameters.Add("@RemindContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(RemindSystemData.executionContent);
                cmd.Parameters.Add("@RemindDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.executionDate);
                cmd.Parameters.Add("@CompleteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.fulfillmentDate);
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

    private string SearchRemindSystemConditionReturn(SearchRemindSystem RemindSystemData)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (RemindSystemData.txtrecipient != null) {
            ConditionReturn += " AND s2.StaffName like @ExecutorName ";
        }
        if (RemindSystemData.txtdesignee != null)
        {
            ConditionReturn += " AND s1.StaffName like @designeName ";
        }
        if (RemindSystemData.txtfulfillmentDatestart != null && RemindSystemData.txtfulfillmentDateend != null && RemindSystemData.txtfulfillmentDatestart != DateBase && RemindSystemData.txtfulfillmentDateend != DateBase) {
            ConditionReturn += " AND RemindList.CompleteDate BETWEEN @fulfillmentDatestart AND @fulfillmentDateend";
        }
        if (RemindSystemData.txtexecutionDatestart != null && RemindSystemData.txtexecutionDateend != null && RemindSystemData.txtexecutionDatestart != DateBase && RemindSystemData.txtexecutionDateend != DateBase)
        {
            ConditionReturn += " AND RemindList.RemindDate BETWEEN @executionDatestart AND @executionDateend";
        }
        if (RemindSystemData.txtdesigneeDatestart != null && RemindSystemData.txtdesigneeDateend != null && RemindSystemData.txtdesigneeDatestart != DateBase && RemindSystemData.txtdesigneeDateend != DateBase)
        {
            ConditionReturn += " AND (select CONVERT(varchar(12) ,RemindList.CreateFileDate,23)) BETWEEN @designeeDatestart AND @designeeDateend";
        }
        return ConditionReturn;
    
    }
    public string[] SearchRemindCount(SearchRemindSystem RemindSystemData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn=this.SearchRemindSystemConditionReturn(RemindSystemData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM ( " +
                                "SELECT * FROM RemindList " +
                                "WHERE Executor=@UserName OR CreateFileBy=@UserName AND isDeleted=0 ) AS NewTable " +
                                "LEFT JOIN StaffDatabase AS s1 ON NewTable.CreateFileBy=s1.StaffID " +
                                "LEFT JOIN StaffDatabase AS s2 ON NewTable.Executor=s2.StaffID "+
                                "WHERE NewTable.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                //cmd.Parameters.Add("@ExecutorType", SqlDbType.TinyInt).Value = 0;//Chk.CheckStringtoIntFunction(RemindSystemData.rType);
                cmd.Parameters.Add("@ExecutorName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(RemindSystemData.txtrecipient) + "%";
                cmd.Parameters.Add("@designeName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(RemindSystemData.txtdesignee) + "%";
                cmd.Parameters.Add("@fulfillmentDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtfulfillmentDatestart);
                cmd.Parameters.Add("@fulfillmentDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtfulfillmentDateend);
                cmd.Parameters.Add("@executionDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtexecutionDatestart);
                cmd.Parameters.Add("@executionDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtexecutionDateend);
                cmd.Parameters.Add("@designeeDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtdesigneeDatestart);
                cmd.Parameters.Add("@designeeDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtdesigneeDateend);
                cmd.Parameters.Add("@UserName", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);

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
    public List<SearchRemindSystemResult> SearchRemind(int indexpage, SearchRemindSystem RemindSystemData)
    {
        List<SearchRemindSystemResult> returnValue = new List<SearchRemindSystemResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchRemindSystemConditionReturn(RemindSystemData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY NewTable.rID DESC) " +
                             "AS RowNum, NewTable.*,s1.StaffName AS designee,s2.StaffName AS recipient FROM " +
                             "( SELECT * FROM RemindList " +
                             "WHERE Executor=@UserName OR CreateFileBy=@UserName AND isDeleted=0 ) AS NewTable "+
                             "LEFT JOIN StaffDatabase AS s1 ON NewTable.CreateFileBy=s1.StaffID " +
                             "LEFT JOIN StaffDatabase AS s2 ON NewTable.Executor=s2.StaffID " +//AND StaffDatabase.isDeleted=0 
                             "WHERE NewTable.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable2 " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                //cmd.Parameters.Add("@ExecutorType", SqlDbType.TinyInt).Value = 0;//Chk.CheckStringtoIntFunction(RemindSystemData.rType);
                cmd.Parameters.Add("@ExecutorName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(RemindSystemData.txtrecipient) + "%";
                cmd.Parameters.Add("@designeName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(RemindSystemData.txtdesignee) + "%";
                cmd.Parameters.Add("@fulfillmentDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtfulfillmentDatestart);
                cmd.Parameters.Add("@fulfillmentDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtfulfillmentDateend);
                cmd.Parameters.Add("@executionDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtexecutionDatestart);
                cmd.Parameters.Add("@executionDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtexecutionDateend);
                cmd.Parameters.Add("@designeeDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtdesigneeDatestart);
                cmd.Parameters.Add("@designeeDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.txtdesigneeDateend);
                cmd.Parameters.Add("@UserName", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchRemindSystemResult addValue = new SearchRemindSystemResult();
                    addValue.Number = dr["RowNum"].ToString();
                    addValue.rID = dr["rID"].ToString();
                    addValue.rType = dr["ExecutorType"].ToString();
                    addValue.recipient = dr["recipient"].ToString();
                    addValue.executionContent = dr["RemindContent"].ToString();
                    addValue.executionDate = DateTime.Parse(dr["RemindDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fulfillmentDate = DateTime.Parse(dr["CompleteDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.designee = dr["designee"].ToString();
                    addValue.designeeDate = DateTime.Parse(dr["CreateFileDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchRemindSystemResult addValue = new SearchRemindSystemResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }

    public SearchRemindSystemResult SearchRemindOne(string rID)
    {
        SearchRemindSystemResult returnValue = new SearchRemindSystemResult();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM RemindList WHERE rID=@rID AND isDeleted=0 ";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@rID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(rID); 
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.rID = dr["rID"].ToString();
                    returnValue.rType = dr["ExecutorType"].ToString();
                    returnValue.recipient = dr["Executor"].ToString();
                    returnValue.executionContent = dr["RemindContent"].ToString();
                    returnValue.executionDate = DateTime.Parse(dr["RemindDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fulfillmentDate = DateTime.Parse(dr["CompleteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.designee = dr["CreateFileBy"].ToString();
                    returnValue.designeeDate = DateTime.Parse(dr["CreateFileDate"].ToString()).ToString("yyyy-MM-dd");
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
    public string[] setRemindSystemData1(SearchRemindSystemResult RemindSystemData)
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
                string sql = "UPDATE RemindList SET RemindContent=@executionContent, RemindDate=@executionDate, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE rID=@rID AND isDeleted=0 AND CreateFileBy=@UpFileBy";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                //cmd.Parameters.Add("@ExecutorType", SqlDbType.TinyInt).Value = 0;//Chk.CheckStringtoIntFunction(RemindSystemData.rType);
                cmd.Parameters.Add("@rID", SqlDbType.BigInt).Value = RemindSystemData.rID;
                cmd.Parameters.Add("@executionContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(RemindSystemData.executionContent);
                cmd.Parameters.Add("@executionDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.executionDate);
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

    public string[] delRemindSystemData(Int64 rID)
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
                string sql = "UPDATE RemindList SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                            "WHERE rID=@rID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                //cmd.Parameters.Add("@ExecutorType", SqlDbType.TinyInt).Value = 0;//Chk.CheckStringtoIntFunction(RemindSystemData.rType);
                cmd.Parameters.Add("@rID", SqlDbType.BigInt).Value = rID;
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
    /**main*/
    public List<SearchRemindSystemResult> getMyselfRemindSystemData(string UserStaffName)
    {
        List<SearchRemindSystemResult> returnValue = new List<SearchRemindSystemResult>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                                
                string sql = "SELECT * FROM (SELECT  ROW_NUMBER() OVER (ORDER BY RemindList.rID DESC) " +
                             "AS RowNum,RemindList.*,s1.StaffName AS designee,s2.StaffName AS recipient FROM RemindList " +
                                "RIGHT JOIN StaffDatabase AS s1 ON RemindList.CreateFileBy=s1.StaffID " +
                                "RIGHT JOIN StaffDatabase AS s2 ON RemindList.Executor=s2.StaffID AND s2.StaffID=@StaffID " +//AND StaffDatabase.isDeleted=0 
                                "WHERE RemindList.isDeleted=0 AND CompleteDate = @CompleteDate )"+
                                "AS NewTable ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(UserStaffName);
                cmd.Parameters.Add("@CompleteDate", SqlDbType.Date).Value = new DateTime(1900,01,01);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchRemindSystemResult addValue = new SearchRemindSystemResult();
                    addValue.Number = dr["RowNum"].ToString();
                    addValue.rID = dr["rID"].ToString();
                    addValue.rType = dr["ExecutorType"].ToString();
                    addValue.recipient = dr["recipient"].ToString();
                    addValue.executionContent = dr["RemindContent"].ToString();
                    addValue.executionDate = DateTime.Parse(dr["RemindDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.fulfillmentDate = DateTime.Parse(dr["CompleteDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.designee = dr["designee"].ToString();
                    addValue.designeeDate = DateTime.Parse(dr["CreateFileDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchRemindSystemResult addValue = new SearchRemindSystemResult();
                addValue.recipient = "-1";
                addValue.designee = e.Message.ToString();
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }

    public string[] setRemindSystemData2(SearchRemindSystemResult RemindSystemData)
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
                string sql = "UPDATE RemindList SET CompleteDate=@fulfillmentDate, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE rID=@rID AND isDeleted=0 AND Executor=@recipientID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@rID", SqlDbType.BigInt).Value = RemindSystemData.rID;
                cmd.Parameters.Add("@recipientID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(HttpContext.Current.User.Identity.Name);
                cmd.Parameters.Add("@fulfillmentDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(RemindSystemData.fulfillmentDate);
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

    public string[] createstationeryDataBase(CreateStationery StationeryData)
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
                string sql = "INSERT INTO PropertyDatabase (Unit, PropertyID, PropertyName, ItemUnit, SafetyStock, Category, Remark, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                    "VALUES(@Unit, @PropertyID, @PropertyName, @ItemUnit, @SafetyStock, @Category, @Remark, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StationeryData.stationeryID);
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StationeryData.stationeryName);
                cmd.Parameters.Add("@ItemUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StationeryData.stationeryUnit);
                cmd.Parameters.Add("@SafetyStock", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StationeryData.safeQuantity);
                cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StationeryData.stationeryType);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(StationeryData.remark);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('PropertyDatabase') AS cID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["cID"].ToString());
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        sql = "SELECT Count(*) AS QCOUNT FROM PropertyDatabase WHERE Category=(@Category) ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(StationeryData.stationeryType);
                        string stuNumber = cmd.ExecuteScalar().ToString();
                        string stuIDName = Chk.CheckStringtoIntFunction(StationeryData.stationeryType) + stuNumber.PadLeft(3, '0');

                        sql = "UPDATE PropertyDatabase SET PropertyID=(@PropertyID) WHERE ID=(@TID) ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
                        returnValue[0] = cmd.ExecuteNonQuery().ToString();
                        //returnValue[1] = Column.ToString();
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

    private string SearchStationeryConditionReturn(SearchStationery stationerySystemData,string DBName)
    {
        string ConditionReturn = "";
        if (stationerySystemData.txtstationeryID != null)
        {
            ConditionReturn += " AND PropertyID=@txtstationeryID ";
        }
        if (stationerySystemData.txtstationeryName != null)
        {
            ConditionReturn += " AND PropertyName like @PropertyName ";
        }
        if (stationerySystemData.txtsafeQuantityStart != null && stationerySystemData.txtsafeQuantityEnd != null)
        {
            ConditionReturn += " AND SafetyStock BETWEEN @txtsafeQuantityStart AND @txtsafeQuantityEnd";
        }
        if (stationerySystemData.txtstationeryType != null)
        {
            ConditionReturn += " AND Category = @txtstationeryType";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND " + DBName + ".Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }
    public string[] SearchStationeryCount(SearchStationery stationeryData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStationeryConditionReturn(stationeryData, "PropertyDatabase");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM PropertyDatabase WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtstationeryID);
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(stationeryData.txtstationeryName) + "%";
                cmd.Parameters.Add("@txtsafeQuantityStart", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtsafeQuantityStart);
                cmd.Parameters.Add("@txtsafeQuantityEnd", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtsafeQuantityEnd);
                cmd.Parameters.Add("@txtstationeryType", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtstationeryType);
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
    public List<CreateStationery> SearchStationery(int indexpage, SearchStationery stationeryData)
    {
        List<CreateStationery> returnValue = new List<CreateStationery>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStationeryConditionReturn(stationeryData, "baseDB");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY baseDB.PropertyID DESC) " +
                             "AS RowNum, baseDB.*,ISNULL((SELECT TOP 1 Price FROM PropertyPurchase " +
                             "INNER JOIN PropertyDatabase ON PropertyPurchase.PropertyID=PropertyDatabase.PropertyID "+
                             "WHERE PropertyPurchase.isDeleted=0 AND PropertyPurchase.PropertyID=baseDB.PropertyID " +
                             "ORDER BY PropertyPurchase.InputDate DESC, PropertyPurchase.ID DESC),0) "+
                             "AS NEWPrice FROM PropertyDatabase AS baseDB " +
                             "WHERE baseDB.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " + 
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtstationeryID);
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(stationeryData.txtstationeryName) + "%";
                cmd.Parameters.Add("@txtsafeQuantityStart", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtsafeQuantityStart);
                cmd.Parameters.Add("@txtsafeQuantityEnd", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtsafeQuantityEnd);
                cmd.Parameters.Add("@txtstationeryType", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.txtstationeryType);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateStationery addValue = new CreateStationery();
                    addValue.sID = dr["ID"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.stationeryID = dr["PropertyID"].ToString();
                    addValue.stationeryName = dr["PropertyName"].ToString();
                    addValue.stationeryType = dr["Category"].ToString();
                    addValue.stationeryUnit = dr["ItemUnit"].ToString();
                    addValue.safeQuantity = dr["SafetyStock"].ToString();
                    addValue.remark = dr["Remark"].ToString();
                    addValue.inventory = this.pInventory(addValue.stationeryID);
                    addValue.recentPrice = dr["NEWPrice"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateStationery addValue = new CreateStationery();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    private string pInventory(string stationeryID) {
        string returnValue = "";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT SUM(QCOUNT) AS new FROM ( " +
                            "SELECT isNull(SUM(Quantity),0) AS QCOUNT FROM PropertyPurchase " +
                            "WHERE PropertyID=@PropertyID AND isDeleted=0 " +
                            "UNION ALL " +
                            "SELECT -isNull(SUM(Quantity),0) AS QCOUNT FROM PropertyUse " +
                            "WHERE PropertyID=@PropertyID AND isDeleted=0 " +
                            "UNION ALL " +
                            "SELECT -isNull(SUM(Quantity),0) AS QCOUNT FROM PropertyScrap " +
                            "WHERE PropertyID=@PropertyID AND isDeleted=0 " +
                            "UNION ALL " +
                            "SELECT isNull(SUM(Quantity),0) AS QCOUNT FROM PropertyReturn " +
                            "WHERE PropertyID=@PropertyID AND isDeleted=0 " +
                            ") AS NEWTABLE1";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryID);
                returnValue = cmd.ExecuteScalar().ToString();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                returnValue = "-1";
            }

        }
        return returnValue;
    }
    public string[] setStationeryData1(SearchStationeryResult stationeryData)
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
                string sql = "UPDATE PropertyDatabase SET PropertyName=@executionName, ItemUnit=@executionUnit, SafetyStock=@executionQuantity, " +
                            "Remark=@executionRemark, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@sID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@sID", SqlDbType.BigInt).Value = stationeryData.sID;
                cmd.Parameters.Add("@executionName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(stationeryData.executionName);
                cmd.Parameters.Add("@executionUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(stationeryData.executionUnit);
                cmd.Parameters.Add("@executionQuantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stationeryData.executionQuantity);
                cmd.Parameters.Add("@executionRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(stationeryData.executionRemark);
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

    public string[] delStationeryData(Int64 sID)
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
                string sql = "UPDATE PropertyDatabase SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@sID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@sID", SqlDbType.BigInt).Value = sID;
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

    public string[] SearchStationeryResultCount(SearchStationery stationeryData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStationeryConditionReturn(stationeryData, "PropertyDatabase");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM PropertyDatabase WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = "%" + stationeryData.txtstationeryName + "%";
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
    public List<CreateStationery> SearchStationeryResult(int indexpage, SearchStationery stationeryData)
    {
        List<CreateStationery> returnValue = new List<CreateStationery>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchStationeryConditionReturn(stationeryData, "PropertyDatabase");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                /*string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyDatabase.PropertyID DESC) " +
                 "AS RowNum, PropertyDatabase.* " +
                 "FROM PropertyDatabase WHERE isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";*/


                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyDatabase.ID DESC) " +
                             "AS RowNum, PropertyDatabase.*,(SELECT TOP 1 Price FROM PropertyPurchase " +
                             "INNER JOIN PropertyDatabase ON PropertyPurchase.PropertyID=PropertyDatabase.PropertyID WHERE PropertyPurchase.isDeleted=0 " +
                             "ORDER BY PropertyPurchase.InputDate DESC) AS NEWPrice FROM PropertyDatabase " +
                             "WHERE PropertyDatabase.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";


                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = "%" + stationeryData.txtstationeryName + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateStationery addValue = new CreateStationery();
                    addValue.stationeryID = dr["PropertyID"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.stationeryName = dr["PropertyName"].ToString();
                    addValue.stationeryUnit = dr["ItemUnit"].ToString();
                    addValue.stationeryType = dr["Category"].ToString();
                    addValue.inventory = this.pInventory(addValue.stationeryID);
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateStationery addValue = new CreateStationery();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] createPurchaseDataBase(CreatePurchase purchaseData)
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
                string sql = "INSERT INTO PropertyPurchase (Unit, InputID, InputDate, CompanyName, CompanyTel, PropertyID, Quantity, Price, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                    "VALUES(@Unit, @InputID, @InputDate, @CompanyName, @CompanyTel, @PropertyID, @Quantity, @Price, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@InputID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseData.purchaseID);
                cmd.Parameters.Add("@InputDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(purchaseData.purchaseDate);
                cmd.Parameters.Add("@CompanyName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(purchaseData.firmName);
                cmd.Parameters.Add("@CompanyTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(purchaseData.firmTel);
                cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseData.stationeryID);
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseData.stationeryQuantity);
                cmd.Parameters.Add("@Price", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(purchaseData.stationeryPrice);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('PropertyPurchase') AS pID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["pID"].ToString());
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        sql = "SELECT SUM(QCOUNT) FROM (" +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "LEFT JOIN PropertyPurchase ON PropertyDatabase.PropertyID=PropertyPurchase.PropertyID WHERE DATEDIFF(month,PropertyPurchase.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyUse ON PropertyDatabase.PropertyID=PropertyUse.PropertyID WHERE DATEDIFF(month,PropertyUse.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyScrap ON PropertyDatabase.PropertyID=PropertyScrap.PropertyID WHERE DATEDIFF(month,PropertyScrap.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyReturn ON PropertyDatabase.PropertyID=PropertyReturn.PropertyID WHERE DATEDIFF(month,PropertyReturn.CreateFileDate,getdate())=0 " +
                            ") AS NEWTABLE";
                        cmd = new SqlCommand(sql, Sqlconn);
                        string stuNumber = cmd.ExecuteScalar().ToString();
                        string tcYear = (DateTime.Now.Year - 1911).ToString();
                        string tcMonth = (DateTime.Now.Month).ToString();
                        string stuIDName = CreateFileName[2] + "3" + tcYear.Substring(1, tcYear.Length - 1) + tcMonth.PadLeft(2, '0') + stuNumber.PadLeft(3, '0');

                        sql = "UPDATE PropertyPurchase SET InputID=(@InputID) WHERE ID=(@TID) ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@InputID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
                        returnValue[0] = cmd.ExecuteNonQuery().ToString();
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

    public string[] SearchPurchaseCount(SearchPurchase purchaseData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchPurchaseConditionReturn(purchaseData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM PropertyPurchase "+
                            "INNER JOIN PropertyDatabase AS s1 ON PropertyPurchase.PropertyID=s1.PropertyID AND s1.isDeleted=0 " +
                            "WHERE PropertyPurchase.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtpurchaseID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseData.txtpurchaseID);
                cmd.Parameters.Add("@txtfirmName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(purchaseData.txtfirmName) + "%";
                cmd.Parameters.Add("@txtpurchaseDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(purchaseData.txtpurchaseDateStart);
                cmd.Parameters.Add("@txtpurchaseDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(purchaseData.txtpurchaseDateEnd);
                cmd.Parameters.Add("@txtstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseData.txtstationeryID);
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
    public List<CreatePurchase> SearchPurchase(int indexpage, SearchPurchase purchaseData)
    {
        List<CreatePurchase> returnValue = new List<CreatePurchase>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchPurchaseConditionReturn(purchaseData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyPurchase.InputDate DESC, PropertyPurchase.ID DESC) " +
                             "AS RowNum, PropertyPurchase.*,s1.PropertyName,s1.ItemUnit FROM PropertyPurchase " +
                             "INNER JOIN PropertyDatabase AS s1 ON PropertyPurchase.PropertyID=s1.PropertyID AND s1.isDeleted=0 " +
                             "WHERE PropertyPurchase.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtpurchaseID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseData.txtpurchaseID);
                cmd.Parameters.Add("@txtfirmName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(purchaseData.txtfirmName) + "%";
                cmd.Parameters.Add("@txtpurchaseDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(purchaseData.txtpurchaseDateStart);
                cmd.Parameters.Add("@txtpurchaseDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(purchaseData.txtpurchaseDateEnd);
                cmd.Parameters.Add("@txtstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseData.txtstationeryID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreatePurchase addValue = new CreatePurchase();
                    addValue.pID = dr["ID"].ToString();
                    addValue.purchaseID = dr["InputID"].ToString();
                    addValue.purchaseDate = DateTime.Parse(dr["InputDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.firmName = dr["CompanyName"].ToString();
                    addValue.firmTel = dr["CompanyTel"].ToString();
                    addValue.stationeryID = dr["PropertyID"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.stationeryQuantity = dr["Quantity"].ToString();
                    addValue.stationeryPrice = dr["Price"].ToString();
                    addValue.stationeryName = dr["PropertyName"].ToString();
                    addValue.stationeryUnit = dr["ItemUnit"].ToString();

                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreatePurchase addValue = new CreatePurchase();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchPurchaseConditionReturn(SearchPurchase purchaseData)
    {
        string ConditionReturn = "";
        if (purchaseData.txtpurchaseID != null)
        {
            ConditionReturn += " AND PropertyPurchase.InputID = @txtpurchaseID ";
        }
        if (purchaseData.txtfirmName != null)
        {
            ConditionReturn += " AND PropertyPurchase.CompanyName like @txtfirmName ";
        }
        if (purchaseData.txtpurchaseDateStart != null && purchaseData.txtpurchaseDateEnd != null)
        {
            ConditionReturn += " AND PropertyPurchase.InputDate BETWEEN @txtpurchaseDateStart AND @txtpurchaseDateEnd";
        }
        if (purchaseData.txtstationeryID != null)
        {
            ConditionReturn += " AND PropertyPurchase.PropertyID = @txtstationeryID";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND PropertyPurchase.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] setPurchaseData1(SearchPurchaseResult purchaseDataResult)
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
                string sql = "UPDATE PropertyPurchase SET InputDate=@executionPurchaseDate, CompanyName=@executionFirmName, CompanyTel=@executionFirmTel, " +
                            "Quantity=@executionQuantity,Price=@executionPrice, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@pID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@pID", SqlDbType.BigInt).Value = purchaseDataResult.pID;
                cmd.Parameters.Add("@executionFirmName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(purchaseDataResult.executionFirmName);
                cmd.Parameters.Add("@executionFirmTel", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(purchaseDataResult.executionFirmTel);
                cmd.Parameters.Add("@executionPurchaseDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(purchaseDataResult.executionPurchaseDate);
                cmd.Parameters.Add("@executionQuantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(purchaseDataResult.executionQuantity);
                cmd.Parameters.Add("@executionPrice", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(purchaseDataResult.executionPrice);
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

    public string[] delPurchaseData(Int64 pID)
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
                string sql = "UPDATE PropertyPurchase SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@pID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@pID", SqlDbType.BigInt).Value = pID;
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

    public string[] createReceiveDataBase(CreateReceive receiveData)
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
                string sql = "INSERT INTO PropertyUse (Unit, UseID, UseDate, UseWho, PropertyID, Quantity, Remark, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                    "VALUES(@Unit, @UseID, @UseDate, @UseWho, @PropertyID, @Quantity, @Remark, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@UseID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.receiveID);
                cmd.Parameters.Add("@UseDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(receiveData.receiveDate);
                cmd.Parameters.Add("@UseWho", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.receiveByID);
                cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.rstationeryID);
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.receiveQuantity);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(receiveData.receiveRemark);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('PropertyUse') AS rID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["rID"].ToString());
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        sql = "SELECT SUM(QCOUNT) FROM ("+
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase "+
                            "LEFT JOIN PropertyPurchase ON PropertyDatabase.PropertyID=PropertyPurchase.PropertyID WHERE DATEDIFF(month,PropertyPurchase.CreateFileDate,getdate())=0 "+
                            "UNION ALL "+
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase "+
                            "INNER JOIN PropertyUse ON PropertyDatabase.PropertyID=PropertyUse.PropertyID WHERE DATEDIFF(month,PropertyUse.CreateFileDate,getdate())=0 "+
                            "UNION ALL "+
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase "+
                            "INNER JOIN PropertyScrap ON PropertyDatabase.PropertyID=PropertyScrap.PropertyID WHERE DATEDIFF(month,PropertyScrap.CreateFileDate,getdate())=0 "+
                            "UNION ALL "+
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase "+
                            "INNER JOIN PropertyReturn ON PropertyDatabase.PropertyID=PropertyReturn.PropertyID WHERE DATEDIFF(month,PropertyReturn.CreateFileDate,getdate())=0 "+
                            ") AS NEWTABLE";
                        cmd = new SqlCommand(sql, Sqlconn);
                        string stuNumber = cmd.ExecuteScalar().ToString();
                        string tcYear = (DateTime.Now.Year - 1911).ToString();
                        string tcMonth = (DateTime.Now.Month).ToString();
                        string stuIDName = CreateFileName[2] + "3" + tcYear.Substring(1, tcYear.Length - 1) + tcMonth.PadLeft(2, '0') + stuNumber.PadLeft(3, '0');

                        sql = "UPDATE PropertyUse SET UseID=(@UseID) WHERE ID=(@TID)";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@UseID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
                        returnValue[0] = cmd.ExecuteNonQuery().ToString();
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

    public string[] SearchReceiveCount(SearchReceive receiveData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchReceiveConditionReturn(receiveData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM PropertyUse " +
                            "INNER JOIN PropertyDatabase ON PropertyUse.PropertyID=PropertyDatabase.PropertyID AND PropertyDatabase.isDeleted=0 " +
                            "INNER JOIN StaffDatabase ON PropertyUse.UseWho=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                            "WHERE PropertyUse.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtreceiveID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.txtreceiveID);
                cmd.Parameters.Add("@txtreceiveDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(receiveData.txtreceiveDateStart);
                cmd.Parameters.Add("@txtreceiveDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(receiveData.txtreceiveDateEnd);
                cmd.Parameters.Add("@txtreceiveBy", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(receiveData.txtreceiveBy) + "%";
                cmd.Parameters.Add("@txtrstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.txtrstationeryID);
                cmd.Parameters.Add("@txtrstationeryName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(receiveData.txtrstationeryName) + "%";
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
    public List<CreateReceive> SearchReceive(int indexpage, SearchReceive receiveData)
    {
        List<CreateReceive> returnValue = new List<CreateReceive>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchReceiveConditionReturn(receiveData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyUse.UseDate DESC, PropertyUse.ID DESC) " +
                             "AS RowNum, PropertyUse.*,PropertyDatabase.PropertyName,PropertyDatabase.ItemUnit,StaffDatabase.StaffName AS receiveByName FROM PropertyUse " +
                             "INNER JOIN PropertyDatabase ON PropertyUse.PropertyID=PropertyDatabase.PropertyID AND PropertyDatabase.isDeleted=0 " +
                            "INNER JOIN StaffDatabase ON PropertyUse.UseWho=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                             "WHERE PropertyUse.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtreceiveID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.txtreceiveID);
                cmd.Parameters.Add("@txtreceiveDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(receiveData.txtreceiveDateStart);
                cmd.Parameters.Add("@txtreceiveDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(receiveData.txtreceiveDateEnd);
                cmd.Parameters.Add("@txtreceiveBy", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(receiveData.txtreceiveBy) + "%";
                cmd.Parameters.Add("@txtrstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveData.txtrstationeryID);
                cmd.Parameters.Add("@txtrstationeryName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(receiveData.txtrstationeryName) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateReceive addValue = new CreateReceive();
                    addValue.rID = dr["ID"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.receiveID = dr["UseID"].ToString();
                    addValue.receiveDate = DateTime.Parse(dr["UseDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.receiveByID = dr["UseWho"].ToString();
                    addValue.receiveByName = dr["receiveByName"].ToString();
                    addValue.rstationeryID = dr["PropertyID"].ToString();
                    addValue.receiveQuantity = dr["Quantity"].ToString();
                    addValue.receiveRemark = dr["Remark"].ToString();
                    addValue.rstationeryName = dr["PropertyName"].ToString();
                    addValue.rstationeryUnit = dr["ItemUnit"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateReceive addValue = new CreateReceive();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchReceiveConditionReturn(SearchReceive receiveData)
    {
        string ConditionReturn = "";
        if (receiveData.txtreceiveID != null)
        {
            ConditionReturn += " AND PropertyUse.UseID = @txtreceiveID ";
        }
        if (receiveData.txtreceiveDateStart != null && receiveData.txtreceiveDateEnd != null)
        {
            ConditionReturn += " AND PropertyUse.UseDate BETWEEN @txtreceiveDateStart AND @txtreceiveDateEnd ";
        }
        if (receiveData.txtreceiveBy != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like @txtreceiveBy ";
        }
        if (receiveData.txtrstationeryID != null)
        {
            ConditionReturn += " AND PropertyUse.PropertyID = @txtrstationeryID ";
        }
        if (receiveData.txtrstationeryName != null)
        {
            ConditionReturn += " AND PropertyDatabase.PropertyName like @txtrstationeryName ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND PropertyUse.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] setReceiveData1(SearchReceiveResult receiveDataResult)
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
                string sql = "UPDATE PropertyUse SET UseDate=@executionreceiveDate, Quantity=@executionQuantity, Remark=@executionRemark, "+
                            "UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@rID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@rID", SqlDbType.BigInt).Value = receiveDataResult.rID;
                cmd.Parameters.Add("@executionreceiveDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(receiveDataResult.executionreceiveDate);
                cmd.Parameters.Add("@executionQuantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(receiveDataResult.executionQuantity);
                cmd.Parameters.Add("@executionRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(receiveDataResult.executionRemark);
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

    public string[] delReceiveData(Int64 rID)
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
                string sql = "UPDATE PropertyUse SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@rID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@rID", SqlDbType.BigInt).Value = rID;
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

    public string[] createScrapDataBase(CreateScrap scrapData)
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
                string sql = "INSERT INTO PropertyScrap (Unit, ScrapID, ScrapDate, Transactor, PropertyID, Quantity, Remark, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                    "VALUES(@Unit, @ScrapID, @ScrapDate, @Transactor, @PropertyID, @Quantity, @Remark, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@ScrapID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.scrappedID);
                cmd.Parameters.Add("@ScrapDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(scrapData.scrappedDate);
                cmd.Parameters.Add("@Transactor", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.scrappedByID);
                cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.sstationeryID);
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.scrappedQuantity);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(scrapData.scrappedRemark);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('PropertyScrap') AS sID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["sID"].ToString());
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        sql = "SELECT SUM(QCOUNT) FROM (" +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "LEFT JOIN PropertyPurchase ON PropertyDatabase.PropertyID=PropertyPurchase.PropertyID WHERE DATEDIFF(month,PropertyPurchase.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyUse ON PropertyDatabase.PropertyID=PropertyUse.PropertyID WHERE DATEDIFF(month,PropertyUse.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyScrap ON PropertyDatabase.PropertyID=PropertyScrap.PropertyID WHERE DATEDIFF(month,PropertyScrap.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyReturn ON PropertyDatabase.PropertyID=PropertyReturn.PropertyID WHERE DATEDIFF(month,PropertyReturn.CreateFileDate,getdate())=0 " +
                            ") AS NEWTABLE";
                        cmd = new SqlCommand(sql, Sqlconn);
                        string stuNumber = cmd.ExecuteScalar().ToString();
                        string tcYear = (DateTime.Now.Year - 1911).ToString();
                        string tcMonth = (DateTime.Now.Month).ToString();
                        string stuIDName = CreateFileName[2] + "3" + tcYear.Substring(1, tcYear.Length - 1) + tcMonth.PadLeft(2, '0') + stuNumber.PadLeft(3, '0');

                        sql = "UPDATE PropertyScrap SET ScrapID=(@ScrapID) WHERE ID=(@TID)";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@ScrapID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
                        returnValue[0] = cmd.ExecuteNonQuery().ToString();
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

    public string[] SearchScrapCount(SearchScrap scrapData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchScrapConditionReturn(scrapData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM PropertyScrap " +
                            "INNER JOIN PropertyDatabase ON PropertyScrap.PropertyID=PropertyDatabase.PropertyID AND PropertyDatabase.isDeleted=0 " +
                            "INNER JOIN StaffDatabase ON PropertyScrap.Transactor=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                            "WHERE PropertyScrap.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtscrappedID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.txtscrappedID);
                cmd.Parameters.Add("@txtscrappedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(scrapData.txtscrappedDateStart);
                cmd.Parameters.Add("@txtscrappedDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(scrapData.txtscrappedDateEnd);
                cmd.Parameters.Add("@txtscrappedBy", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(scrapData.txtscrappedBy) + "%";
                cmd.Parameters.Add("@txtsstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.txtsstationeryID);
                cmd.Parameters.Add("@txtsstationeryName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(scrapData.txtsstationeryName) + "%";
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
    public List<CreateScrap> SearchScrap(int indexpage, SearchScrap scrapData)
    {
        List<CreateScrap> returnValue = new List<CreateScrap>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchScrapConditionReturn(scrapData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyScrap.ScrapDate DESC,PropertyScrap.ID DESC) " +
                             "AS RowNum, PropertyScrap.*,PropertyDatabase.PropertyName,PropertyDatabase.ItemUnit,StaffDatabase.StaffName AS receiveByName FROM PropertyScrap " +
                             "INNER JOIN PropertyDatabase ON PropertyScrap.PropertyID=PropertyDatabase.PropertyID AND PropertyDatabase.isDeleted=0 " +
                            "INNER JOIN StaffDatabase ON PropertyScrap.Transactor=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                             "WHERE PropertyScrap.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtscrappedID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.txtscrappedID);
                cmd.Parameters.Add("@txtscrappedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(scrapData.txtscrappedDateStart);
                cmd.Parameters.Add("@txtscrappedDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(scrapData.txtscrappedDateEnd);
                cmd.Parameters.Add("@txtscrappedBy", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(scrapData.txtscrappedBy) + "%";
                cmd.Parameters.Add("@txtsstationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(scrapData.txtsstationeryID);
                cmd.Parameters.Add("@txtsstationeryName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(scrapData.txtsstationeryName) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateScrap addValue = new CreateScrap();
                    addValue.sID = dr["ID"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.scrappedID = dr["ScrapID"].ToString();
                    addValue.scrappedDate = DateTime.Parse(dr["ScrapDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.scrappedByID = dr["Transactor"].ToString();
                    addValue.scrappedByName = dr["receiveByName"].ToString();
                    addValue.sstationeryID = dr["PropertyID"].ToString();
                    addValue.scrappedQuantity = dr["Quantity"].ToString();
                    addValue.scrappedRemark = dr["Remark"].ToString();
                    addValue.sstationeryName = dr["PropertyName"].ToString();
                    addValue.sstationeryUnit = dr["ItemUnit"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateScrap addValue = new CreateScrap();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchScrapConditionReturn(SearchScrap scrapData)
    {
        string ConditionReturn = "";
        if (scrapData.txtscrappedID != null)
        {
            ConditionReturn += " AND PropertyScrap.ScrapID = @txtscrappedID ";
        }
        if (scrapData.txtscrappedDateStart != null && scrapData.txtscrappedDateEnd != null)
        {
            ConditionReturn += " AND PropertyScrap.ScrapDate BETWEEN @txtscrappedDateStart AND @txtscrappedDateEnd ";
        }
        if (scrapData.txtscrappedBy != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like @txtscrappedBy ";
        }
        if (scrapData.txtsstationeryID != null)
        {
            ConditionReturn += " AND PropertyScrap.PropertyID = @txtsstationeryID ";
        }
        if (scrapData.txtsstationeryName != null)
        {
            ConditionReturn += " AND PropertyDatabase.PropertyName like @txtsstationeryName ";
        }
        return ConditionReturn;
    }

    public string[] setScrapData1(SearchScrapResult ScrapDataResult)
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
                string sql = "UPDATE PropertyScrap SET ScrapDate=@executionscrapDate, Quantity=@executionQuantity, Remark=@executionRemark, " +
                            "UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@sID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@sID", SqlDbType.BigInt).Value = ScrapDataResult.sID;
                cmd.Parameters.Add("@executionscrapDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(ScrapDataResult.executionscrapDate);
                cmd.Parameters.Add("@executionQuantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ScrapDataResult.executionQuantity);
                cmd.Parameters.Add("@executionRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(ScrapDataResult.executionRemark);
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

    public string[] delScrapData(Int64 sID)
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
                string sql = "UPDATE PropertyScrap SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@sID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@sID", SqlDbType.BigInt).Value = sID;
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

    public string[] createReturnDataBase(CreateReturn returnData)
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
                string sql = "INSERT INTO PropertyReturn (Unit, ReturnID, ReturnDate, OutputDate, OutputTransactor, PropertyID, Quantity, Reason, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) " +
                    "VALUES(@Unit, @ReturnID, @ReturnDate, @OutputDate, @OutputTransactor, @PropertyID, @Quantity, @Reason, @CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@ReturnID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.returnedID);
                cmd.Parameters.Add("@ReturnDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(returnData.returnedDate);
                cmd.Parameters.Add("@OutputDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(returnData.getgoodsDate);
                cmd.Parameters.Add("@OutputTransactor", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.getgoodsByID);
                cmd.Parameters.Add("@PropertyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.restationeryID);
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.returnedQuantity);
                cmd.Parameters.Add("@Reason", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(returnData.returnedReason);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('PropertyReturn') AS rID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["rID"].ToString());
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        sql = "SELECT SUM(QCOUNT) FROM (" +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "LEFT JOIN PropertyPurchase ON PropertyDatabase.PropertyID=PropertyPurchase.PropertyID WHERE DATEDIFF(month,PropertyPurchase.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyUse ON PropertyDatabase.PropertyID=PropertyUse.PropertyID WHERE DATEDIFF(month,PropertyUse.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyScrap ON PropertyDatabase.PropertyID=PropertyScrap.PropertyID WHERE DATEDIFF(month,PropertyScrap.CreateFileDate,getdate())=0 " +
                            "UNION ALL " +
                            "SELECT Count(*) AS QCOUNT FROM PropertyDatabase " +
                            "INNER JOIN PropertyReturn ON PropertyDatabase.PropertyID=PropertyReturn.PropertyID WHERE DATEDIFF(month,PropertyReturn.CreateFileDate,getdate())=0 " +
                            ") AS NEWTABLE";
                        cmd = new SqlCommand(sql, Sqlconn);
                        string stuNumber = cmd.ExecuteScalar().ToString();
                        string tcYear = (DateTime.Now.Year - 1911).ToString();
                        string tcMonth = (DateTime.Now.Month).ToString();
                        string stuIDName = CreateFileName[2] + "3" + tcYear.Substring(1, tcYear.Length - 1) + tcMonth.PadLeft(2, '0') + stuNumber.PadLeft(3, '0');

                        sql = "UPDATE PropertyReturn SET ReturnID=(@ReturnID) WHERE ID=(@TID)";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@TID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@ReturnID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
                        returnValue[0] = cmd.ExecuteNonQuery().ToString();
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

    public string[] SearchReturnCount(SearchReturn returnData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchReturnConditionReturn(returnData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM PropertyReturn " +
                            "INNER JOIN PropertyDatabase ON PropertyReturn.PropertyID=PropertyDatabase.PropertyID AND PropertyDatabase.isDeleted=0 " +
                            "INNER JOIN StaffDatabase ON PropertyReturn.OutputTransactor=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                            "WHERE PropertyReturn.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtreturnedID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.txtreturnedID);
                cmd.Parameters.Add("@txtreturnedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(returnData.txtreturnedDateStart);
                cmd.Parameters.Add("@txtreturnedDateeEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(returnData.txtreturnedDateeEnd);
                cmd.Parameters.Add("@txtgetgoodsBy", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(returnData.txtgetgoodsBy) + "%";
                cmd.Parameters.Add("@txtrestationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.txtrestationeryID);
                cmd.Parameters.Add("@txtrestationeryName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(returnData.txtrestationeryName) + "%";
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
    public List<CreateReturn> SearchReturn(int indexpage, SearchReturn returnData)
    {
        List<CreateReturn> returnValue = new List<CreateReturn>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchReturnConditionReturn(returnData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyReturn.ReturnDate DESC, PropertyReturn.ID DESC) " +
                             "AS RowNum, PropertyReturn.*,PropertyDatabase.PropertyName,PropertyDatabase.ItemUnit,StaffDatabase.StaffName AS receiveByName FROM PropertyReturn " +
                             "INNER JOIN PropertyDatabase ON PropertyReturn.PropertyID=PropertyDatabase.PropertyID AND PropertyDatabase.isDeleted=0 " +
                             "INNER JOIN StaffDatabase ON PropertyReturn.OutputTransactor=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                             "WHERE PropertyReturn.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtreturnedID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.txtreturnedID);
                cmd.Parameters.Add("@txtreturnedDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(returnData.txtreturnedDateStart);
                cmd.Parameters.Add("@txtreturnedDateeEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(returnData.txtreturnedDateeEnd);
                cmd.Parameters.Add("@txtgetgoodsBy", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(returnData.txtgetgoodsBy) + "%";
                cmd.Parameters.Add("@txtrestationeryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(returnData.txtrestationeryID);
                cmd.Parameters.Add("@txtrestationeryName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(returnData.txtrestationeryName) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateReturn addValue = new CreateReturn();
                    addValue.rID = dr["ID"].ToString();
                    addValue.returnedID = dr["ReturnID"].ToString();
                    addValue.returnedDate = DateTime.Parse(dr["ReturnDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.getgoodsDate = DateTime.Parse(dr["OutputDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.getgoodsByID = dr["OutputTransactor"].ToString();
                    addValue.getgoodsByName = dr["receiveByName"].ToString();
                    addValue.restationeryID = dr["PropertyID"].ToString();
                    addValue.Unit = dr["Unit"].ToString();
                    addValue.returnedQuantity = dr["Quantity"].ToString();
                    addValue.returnedReason = dr["Reason"].ToString();
                    addValue.restationeryName = dr["PropertyName"].ToString();
                    addValue.restationeryUnit = dr["ItemUnit"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateReturn addValue = new CreateReturn();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchReturnConditionReturn(SearchReturn returnData)
    {
        string ConditionReturn = "";
        if (returnData.txtreturnedID != null)
        {
            ConditionReturn += " AND PropertyReturn.ReturnID = @txtreturnedID ";
        }
        if (returnData.txtreturnedDateStart != null && returnData.txtreturnedDateeEnd != null)
        {
            ConditionReturn += " AND PropertyReturn.ReturnDate BETWEEN @txtreturnedDateStart AND @txtreturnedDateeEnd ";
        }
        if (returnData.txtgetgoodsBy != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like @txtgetgoodsBy ";
        }
        if (returnData.txtrestationeryID != null)
        {
            ConditionReturn += " AND PropertyReturn.PropertyID = @txtrestationeryID ";
        }
        if (returnData.txtrestationeryName != null)
        {
            ConditionReturn += " AND PropertyDatabase.PropertyName like @txtrestationeryName ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND PropertyReturn.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] setReturnData1(SearchReturnResult ReturnDataResult)
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
                string sql = "UPDATE PropertyReturn SET ReturnDate=@executionreturnedDate, OutputDate=@executiongetgoodsDate, Quantity=@executionQuantity, "+
                            "Reason=@executionReason, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@rID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@rID", SqlDbType.BigInt).Value = ReturnDataResult.rID;
                cmd.Parameters.Add("@executionreturnedDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(ReturnDataResult.executionreturnedDate);
                cmd.Parameters.Add("@executiongetgoodsDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(ReturnDataResult.executiongetgoodsDate);
                cmd.Parameters.Add("@executionQuantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ReturnDataResult.executionQuantity);
                cmd.Parameters.Add("@executionReason", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(ReturnDataResult.executionReason);
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

    public string[] delReturnData(Int64 rID)
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
                string sql = "UPDATE PropertyReturn SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@rID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@rID", SqlDbType.BigInt).Value = rID;
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
