using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for PropertyDataBase
/// </summary>
public class PropertyDataBase
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
    public string[] _StaffhaveRoles = new string[5] { "0", "0", "0", "0", "0" };
    public PropertyDataBase()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public void applyFunction()
    {
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.apply[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.apply[1];//跨區與否
    }
    public void propertyFunction()
    {
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.property[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.property[1];//跨區與否
    }
    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }

    public string[] createApplyPropertyData(CreateApplyProperty applyPropertyData)
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
                string sql = "INSERT INTO PropertyBuyRepair (FileDate,Unit, ApplyType, Payment, Recipients, State, TotalPrice, " +
                    "CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES " +
                    "(@FileDate, @Unit, @ApplyType, @Payment, @Recipients, 0, @TotalPrice, " +
                    "@CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@FileDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(applyPropertyData.applyDate);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@ApplyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(applyPropertyData.applyType);
                cmd.Parameters.Add("@Payment", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(applyPropertyData.applyPay);
                cmd.Parameters.Add("@Recipients", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(applyPropertyData.applySum);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('PropertyBuyRepair') AS pID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["pID"].ToString());
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        for (int i = 0; i < (applyPropertyData.DetailArray).Count; i++)
                        {
                            sql = "INSERT INTO PropertyApplyDetail (BuyRepairID, PropertyName, ItemUnit, Quantity, Format, EstimatePrice, " +
                            "Explain, Bill, CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES " +
                            "(@BuyRepairID, @PropertyName, @ItemUnit, @Quantity, @Format, @EstimatePrice, @Explain, @Bill, " +
                            "@CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                            cmd = new SqlCommand(sql, Sqlconn);
                            cmd.Parameters.Add("@BuyRepairID", SqlDbType.BigInt).Value = Column;
                            cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(applyPropertyData.DetailArray[i].Name);
                            cmd.Parameters.Add("@ItemUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(applyPropertyData.DetailArray[i].Unit);
                            cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(applyPropertyData.DetailArray[i].Quantity);
                            cmd.Parameters.Add("@Format", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(applyPropertyData.DetailArray[i].Format);
                            cmd.Parameters.Add("@EstimatePrice", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(applyPropertyData.DetailArray[i].Price);
                            cmd.Parameters.Add("@Explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(applyPropertyData.DetailArray[i].Explain);
                            cmd.Parameters.Add("@Bill", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(applyPropertyData.DetailArray[i].Bill);
                            cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            returnValue[0] = cmd.ExecuteNonQuery().ToString();
                        }
                        returnValue[1] = Column.ToString();
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

    public string[] SearchApplyPropertyDataCount(SearchApplyProperty searchApplyData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchApplyPropertyConditionReturn(searchApplyData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM PropertyBuyRepair "+
                    "INNER JOIN StaffDatabase ON PropertyBuyRepair.Recipients=StaffDatabase.StaffID " +
                    "WHERE PropertyBuyRepair.isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ApplyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(searchApplyData.txtapplyType);
                cmd.Parameters.Add("@State", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(searchApplyData.txtapplyStatus);
                cmd.Parameters.Add("@ApplyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchApplyData.txtapplyID);
                cmd.Parameters.Add("@FileDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(searchApplyData.txtapplyDateStart);
                cmd.Parameters.Add("@FileDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(searchApplyData.txtapplyDateEnd);
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

    public List<SearchApplyPropertyResult> SearchApplyPropertyData(int indexpage, SearchApplyProperty searchApplyData)
    {
        List<SearchApplyPropertyResult> returnValue = new List<SearchApplyPropertyResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchApplyPropertyConditionReturn(searchApplyData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyBuyRepair.BuyRepairID DESC) " +
                 "AS RowNum, PropertyBuyRepair.* , StaffDatabase.StaffName FROM PropertyBuyRepair " +
                 "INNER JOIN StaffDatabase ON PropertyBuyRepair.Recipients=StaffDatabase.StaffID " +
                 "WHERE PropertyBuyRepair.isDeleted=0 " + ConditionReturn + " ) " +
                 "AS NewTable " +
                 "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@ApplyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(searchApplyData.txtapplyType);
                cmd.Parameters.Add("@State", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(searchApplyData.txtapplyStatus);
                cmd.Parameters.Add("@ApplyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchApplyData.txtapplyID);
                cmd.Parameters.Add("@FileDateStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(searchApplyData.txtapplyDateStart);
                cmd.Parameters.Add("@FileDateEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(searchApplyData.txtapplyDateEnd);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchApplyPropertyResult addValue = new SearchApplyPropertyResult();
                    addValue.ID = dr["BuyRepairID"].ToString();
                    addValue.txtapplyDate = DateTime.Parse(dr["FileDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtapplyID = dr["ApplyID"].ToString();
                    addValue.txtapplyType = dr["ApplyType"].ToString();
                    addValue.txtapplyPay = dr["Payment"].ToString();
                    addValue.txtapplyByID = dr["Recipients"].ToString();
                    addValue.txtapplyBy = dr["StaffName"].ToString();
                    addValue.txtapplyStatus = dr["State"].ToString();
                    addValue.txtapplySum = dr["TotalPrice"].ToString();
                    addValue.checkNo = "1";
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchApplyPropertyResult addValue = new SearchApplyPropertyResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchApplyPropertyConditionReturn(SearchApplyProperty searchApplyData)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (searchApplyData.txtapplyType != null)
        {
            ConditionReturn += " AND PropertyBuyRepair.ApplyType=(@ApplyType) ";
        }
        if (searchApplyData.txtapplyStatus != null)
        {
            ConditionReturn += " AND PropertyBuyRepair.State=(@State) ";
        }
        if (searchApplyData.txtapplyID != null)
        {
            ConditionReturn += " AND PropertyBuyRepair.ApplyID=(@ApplyID) ";
        }
        if (searchApplyData.txtapplyDateStart != null && searchApplyData.txtapplyDateEnd != null && searchApplyData.txtapplyDateStart != DateBase && searchApplyData.txtapplyDateEnd != DateBase)
        {
            ConditionReturn += " AND PropertyBuyRepair.FileDate BETWEEN (@FileDateStart) AND (@FileDateEnd) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        this.applyFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND PropertyBuyRepair.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] setApplyPropertyDataBase(CreateApplyProperty applyPropertyData)
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
                string sql = "UPDATE PropertyBuyRepair SET FileDate=@FileDate, ApplyType=@ApplyType, Payment=@Payment, "+
                    "TotalPrice=@TotalPrice, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE BuyRepairID=@BuyRepairID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@BuyRepairID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(applyPropertyData.ID);
                cmd.Parameters.Add("@FileDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(applyPropertyData.applyDate);
                cmd.Parameters.Add("@ApplyType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(applyPropertyData.applyType);
                cmd.Parameters.Add("@Payment", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(applyPropertyData.applyPay);
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(applyPropertyData.applySum);
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

    public CreateApplyProperty getApplyPropertyDataBase(string ID)
    {
        CreateApplyProperty returnValue = new CreateApplyProperty();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT PropertyBuyRepair.* , StaffDatabase.StaffName FROM PropertyBuyRepair " +
                 "INNER JOIN StaffDatabase ON PropertyBuyRepair.Recipients=StaffDatabase.StaffID " +
                 "WHERE PropertyBuyRepair.isDeleted=0 AND PropertyBuyRepair.BuyRepairID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.ID = dr["BuyRepairID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.applyDate = DateTime.Parse(dr["FileDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.applyID = dr["ApplyID"].ToString();
                    returnValue.applyType = dr["ApplyType"].ToString();
                    returnValue.applyPay = dr["Payment"].ToString();
                    returnValue.applyByID = dr["Recipients"].ToString();
                    returnValue.applyBy = dr["StaffName"].ToString();
                    returnValue.applyStatus = dr["State"].ToString();
                    returnValue.applySum = dr["TotalPrice"].ToString();
                    returnValue.DetailArray = new List<PropertyDetailData>();
                }
                dr.Close();
                sql = "SELECT * FROM PropertyApplyDetail WHERE isDeleted=0 AND BuyRepairID=@BuyRepairID";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@BuyRepairID", SqlDbType.BigInt).Value = returnValue.ID;
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PropertyDetailData addValue = new PropertyDetailData();
                    addValue.pID = dr["ID"].ToString();
                    addValue.Name = dr["PropertyName"].ToString();
                    addValue.Unit = dr["ItemUnit"].ToString();
                    addValue.Quantity = dr["Quantity"].ToString();
                    addValue.Format = dr["Format"].ToString();
                    addValue.Price = dr["EstimatePrice"].ToString();
                    addValue.Explain = dr["Explain"].ToString();
                    addValue.Bill = dr["Bill"].ToString();
                    returnValue.DetailArray.Add(addValue);
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

    public string[] setApplyPropertyDetail(PropertyDetailData PropertyDetail)
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
                string sql = "UPDATE PropertyApplyDetail SET PropertyName=@PropertyName, ItemUnit=@ItemUnit, Quantity=@Quantity, Format=@Format, "+
                            "EstimatePrice=@EstimatePrice, Explain=@Explain, Bill=@Bill, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                            "WHERE ID=@ID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(PropertyDetail.pID);
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(PropertyDetail.Name);
                cmd.Parameters.Add("@ItemUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(PropertyDetail.Unit);
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(PropertyDetail.Quantity);
                cmd.Parameters.Add("@Format", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(PropertyDetail.Format);
                cmd.Parameters.Add("@EstimatePrice", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(PropertyDetail.Price);
                cmd.Parameters.Add("@Explain", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(PropertyDetail.Explain);
                cmd.Parameters.Add("@Bill", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(PropertyDetail.Bill);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();

                sql = "UPDATE PropertyBuyRepair SET TotalPrice=@TotalPrice, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE BuyRepairID=@BuyRepairID "+
                    "AND isDeleted=0";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@BuyRepairID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(PropertyDetail.aID);
                cmd.Parameters.Add("@TotalPrice", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(PropertyDetail.Sum);
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

    public string[] delApplyPropertyDetail(string tID)
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
                string sql = "UPDATE PropertyApplyDetail SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@ID";
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

    public string[] printApplyPropertyDataBase(string tID)
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
                string sql = "SELECT Count(*) AS QCOUNT FROM PropertyBuyRepair WHERE State>=1 AND State<5 AND DATEDIFF(month,PropertyBuyRepair.UpFileDate,getdate())=0 AND isDeleted=0 AND ApplyID != ''";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                string stuNumber = (int.Parse(cmd.ExecuteScalar().ToString()) + 1).ToString();
                string tcYear = (DateTime.Now.Year - 1911).ToString();
                string tcMonth = (DateTime.Now.Month).ToString();
                string stuIDName = CreateFileName[2] + "2" + tcYear.Substring(1, tcYear.Length - 1) + tcMonth.PadLeft(2, '0') + stuNumber.PadLeft(3, '0');

                sql = "UPDATE PropertyBuyRepair SET State=1,ApplyID=(@ApplyID), UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE BuyRepairID=(@BuyRepairID) AND isDeleted=0";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@BuyRepairID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(tID);
                cmd.Parameters.Add("@ApplyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
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

    public List<PropertyDetailData> getPropertyDetailDataBase(string ID)
    {
        List<PropertyDetailData> returnValue = new List<PropertyDetailData>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM PropertyApplyDetail WHERE isDeleted=0 AND BuyRepairID=@BuyRepairID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@BuyRepairID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PropertyDetailData addValue = new PropertyDetailData();
                    addValue.pID = dr["ID"].ToString();
                    addValue.Name = dr["PropertyName"].ToString();
                    addValue.Unit = dr["ItemUnit"].ToString();
                    addValue.Quantity = dr["Quantity"].ToString();
                    addValue.Format = dr["Format"].ToString();
                    addValue.Price = dr["EstimatePrice"].ToString();
                    addValue.Explain = dr["Explain"].ToString();
                    addValue.Bill = dr["Bill"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                PropertyDetailData addValue = new PropertyDetailData();
                addValue.pID = "-1";
                addValue.Unit = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public List<string[]> getPropertyCategory()
    {
        List<string[]> returnValue = new List<string[]>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM PropertyCategory WHERE isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] addValue = new string[2];
                    addValue[0] = dr["ID"].ToString();
                    addValue[1] = dr["CategoryName"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string[] addValue = new string[2];
                addValue[0] = "-1";
                addValue[1] = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public List<string[]> getPropertyLocation(string unit)
    {
        List<string[]> returnValue = new List<string[]>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                /*StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sUnit = "";
                if (unit == "0")
                {
                    sUnit = CreateFileName[2];
                }
                else {
                    sUnit = unit;
                }*/
                string ConditionReturn = "";
                StaffDataBase sDB = new StaffDataBase();
                List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                this.propertyFunction();
                if (unit == "00")
                {
                    if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
                    {
                        ConditionReturn += " AND Unit =" + UserFile[2] + " ";
                    }
                }
                else if (unit == "0")
                {
                    ConditionReturn += " AND Unit =" + UserFile[2] + " ";
                }
                else
                {
                    ConditionReturn += " AND Unit = @Unit ";
                }


                Sqlconn.Open();
                string sql = "SELECT * FROM PropertyLocation WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(unit);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] addValue = new string[2];
                    addValue[0] = dr["ID"].ToString();
                    addValue[1] = dr["LocationName"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string[] addValue = new string[2];
                addValue[0] = "-1";
                addValue[1] = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public List<string[]> getPropertyCustody(string unit)
    {
        List<string[]> returnValue = new List<string[]>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                /*StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sUnit = "";
                if (unit == "0")
                {
                    sUnit = CreateFileName[2];
                }
                else
                {
                    sUnit = unit;
                }*/
                string ConditionReturn = "";
                StaffDataBase sDB = new StaffDataBase();
                List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                this.propertyFunction();
                if (unit == "00")
                {
                    if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
                    {
                        ConditionReturn += " AND Unit =" + UserFile[2] + " ";
                    }
                }
                else if (unit == "0")
                {
                    ConditionReturn += " AND Unit =" + UserFile[2] + " ";
                }
                else
                {
                    ConditionReturn += " AND Unit = @Unit ";
                }
                  

                Sqlconn.Open();
                string sql = "SELECT * FROM PropertyCustody WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(unit);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] addValue = new string[2];
                    addValue[0] = dr["ID"].ToString();
                    addValue[1] = dr["CustodyName"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string[] addValue = new string[2];
                addValue[0] = "-1";
                addValue[1] = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] createPropertyRecordData(CreatePropertyRecord propertyRecordData)
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
                string sql = "INSERT INTO PropertyRecord (WriteDate, Unit, PropertyState, PropertyAddImpairment, PropertyChangeState, PropertyID, " +
                    "Code, ApplyID, Category, PropertyName, Label, ItemUnit, Quantity, Fitting, Location, Custody, ScrapDate, " +
                    "Summons, Receipt, Accounting, RecordedDate, OutDate, FundSource, FundSourceAssist, FundSourceDonate, PurchaseDate, " +
                    "Residual, PurchaseSource, ExpirationDate, Price, Depreciation, SinceFundraising, Procurement, Grants, Remark, " +
                    "CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES " +
                    "(@WriteDate, @Unit, @PropertyState, @PropertyAddImpairment, @PropertyChangeState, @PropertyID, " +
                    "@Code, @ApplyID, @Category, @PropertyName, @Label, @ItemUnit, @Quantity, @Fitting, @Location, @Custody, @ScrapDate, " +
                    "@Summons, @Receipt, @Accounting, @RecordedDate, @OutDate, @FundSource, @FundSourceAssist, @FundSourceDonate, @PurchaseDate, " +
                    "@Residual, @PurchaseSource, @ExpirationDate, @Price, @Depreciation, @SinceFundraising, @Procurement, @Grants, @Remark, " +
                    "@CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.fillInDate);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@PropertyState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyStatus);
                cmd.Parameters.Add("@PropertyAddImpairment", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyChange);
                cmd.Parameters.Add("@PropertyChangeState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.changeStatus);
                cmd.Parameters.Add("@PropertyID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyID);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyCode);
                cmd.Parameters.Add("@ApplyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.applyID);
                cmd.Parameters.Add("@Category", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyCategory);
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyName);
                cmd.Parameters.Add("@Label", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyLabel);
                cmd.Parameters.Add("@ItemUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyUnit);
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyQuantity);
                cmd.Parameters.Add("@Fitting", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyFitting);
                cmd.Parameters.Add("@Location", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyLocation);
                cmd.Parameters.Add("@Custody", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyCustody);
                cmd.Parameters.Add("@ScrapDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.stopDate);
                cmd.Parameters.Add("@Summons", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertySummons);
                cmd.Parameters.Add("@Receipt", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyReceipt);
                cmd.Parameters.Add("@Accounting", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyAccounting);
                cmd.Parameters.Add("@RecordedDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.inputDate);
                cmd.Parameters.Add("@OutDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.outputDate);
                cmd.Parameters.Add("@FundSource", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.fundSource);
                cmd.Parameters.Add("@FundSourceAssist", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.fundAssist);
                cmd.Parameters.Add("@FundSourceDonate", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.fundDonate);
                cmd.Parameters.Add("@PurchaseDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.buyDate);
                cmd.Parameters.Add("@Residual", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.Remnants);
                cmd.Parameters.Add("@PurchaseSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.buySource);
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.userYear);
                cmd.Parameters.Add("@Price", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyPrice);
                cmd.Parameters.Add("@Depreciation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.Depreciation);
                cmd.Parameters.Add("@SinceFundraising", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.selfFunds);
                cmd.Parameters.Add("@Procurement", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.Purchaser);
                cmd.Parameters.Add("@Grants", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.Grant);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.Remark);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);

                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    string FieldName = "PropertDB_" + CreateFileName[2];
                    sql = "SELECT IDENT_CURRENT('PropertyRecord') AS pID " +
                          "UPDATE AutomaticNumberTable SET " + FieldName + "=" + FieldName + "+1 WHERE ID=1 ";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        returnValue[1] = dr["pID"].ToString();
                    }
                    dr.Close();

                    /*sql = "SELECT Count(*) AS QCOUNT FROM PropertyRecord WHERE isDeleted=0";
                    cmd = new SqlCommand(sql, Sqlconn);
                    string stuNumber = cmd.ExecuteScalar().ToString();
                    string stuIDName = CreateFileName[2] + stuNumber.PadLeft(4, '0');*/
                    CaseDataBase SDB = new CaseDataBase();
                    string stuNumber = SDB.getUnitAutoNumber(FieldName);
                    string stuIDName = CreateFileName[2] + stuNumber.PadLeft(4, '0');

                    sql = "UPDATE PropertyRecord SET Code=(@Code), UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE RecordID=(@RecordID) AND isDeleted=0";
                    cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@RecordID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(returnValue[1]);
                    cmd.Parameters.Add("@Code", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(stuIDName);
                    cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                    returnValue[0] = cmd.ExecuteNonQuery().ToString();
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

    public string[] setPropertyAnnexDataBase(CreatePropertyRecord propertyRecordData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        string PicUpdatestring = "";
        if (propertyRecordData.attachment1 != null)
        {
            PicUpdatestring += "Annex1=@Annex1, ";
        }
        if (propertyRecordData.attachment2 != null)
        {
            PicUpdatestring += "Annex2=@Annex2, ";
        }
        if (propertyRecordData.attachment3 != null)
        {
            PicUpdatestring += "Annex3=@Annex3, ";
        }
        if (propertyRecordData.attachment4 != null)
        {
            PicUpdatestring += "Annex4=@Annex4, ";
        }
        if (propertyRecordData.attachment5 != null)
        {
            PicUpdatestring += "Annex5=@Annex5, ";
        }
        if (propertyRecordData.attachment6 != null)
        {
            PicUpdatestring += "Annex6=@Annex6, ";
        }
        if (propertyRecordData.attachment7 != null)
        {
            PicUpdatestring += "Annex7=@Annex7, ";
        }
        if (propertyRecordData.attachment8 != null)
        {
            PicUpdatestring += "Annex8=@Annex8, ";
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
                string sql = "UPDATE PropertyRecord SET " + PicUpdatestring + "UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                            "WHERE RecordID=@RecordID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@RecordID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(propertyRecordData.repairID);
                cmd.Parameters.Add("@Annex1", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment1);
                cmd.Parameters.Add("@Annex2", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment2);
                cmd.Parameters.Add("@Annex3", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment3);
                cmd.Parameters.Add("@Annex4", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment4);
                cmd.Parameters.Add("@Annex5", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment5);
                cmd.Parameters.Add("@Annex6", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment6);
                cmd.Parameters.Add("@Annex7", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment7);
                cmd.Parameters.Add("@Annex8", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.attachment8);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                returnValue[1] = propertyRecordData.repairID;
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

    public string[] setPropertyRecordDataBase(CreatePropertyRecord propertyRecordData)
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
                string sql = "UPDATE PropertyRecord SET WriteDate=@WriteDate, PropertyState=@PropertyState, PropertyAddImpairment=@PropertyAddImpairment, " +
                    "PropertyChangeState=@PropertyChangeState, PropertyID=@PropertyID, Code=@Code, ApplyID=@ApplyID, Category=@Category, " +
                    "PropertyName=@PropertyName, Label=@Label, ItemUnit=@ItemUnit, Quantity=@Quantity, Fitting=@Fitting, Location=@Location, " +
                    "Custody=@Custody, ScrapDate=@ScrapDate, Summons=@Summons, Receipt=@Receipt, Accounting=@Accounting, RecordedDate=@RecordedDate, " +
                    "OutDate=@OutDate, FundSource=@FundSource, FundSourceAssist=@FundSourceAssist, FundSourceDonate=@FundSourceDonate, PurchaseDate=@PurchaseDate, " +
                    "Residual=@Residual, PurchaseSource=@PurchaseSource, ExpirationDate=@ExpirationDate, Price=@Price, Depreciation=@Depreciation, " +
                    "SinceFundraising=@SinceFundraising, Procurement=@Procurement, Grants=@Grants, Remark=@Remark, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE RecordID=@RecordID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@RecordID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.repairID);
                cmd.Parameters.Add("@WriteDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.fillInDate);
                cmd.Parameters.Add("@PropertyState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyStatus);
                cmd.Parameters.Add("@PropertyAddImpairment", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyChange);
                cmd.Parameters.Add("@PropertyChangeState", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.changeStatus);
                cmd.Parameters.Add("@PropertyID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyID);
                cmd.Parameters.Add("@Code", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyCode);
                cmd.Parameters.Add("@ApplyID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.applyID);
                cmd.Parameters.Add("@Category", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyCategory);
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyName);
                cmd.Parameters.Add("@Label", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyLabel);
                cmd.Parameters.Add("@ItemUnit", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyUnit);
                cmd.Parameters.Add("@Quantity", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyQuantity);
                cmd.Parameters.Add("@Fitting", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyFitting);
                cmd.Parameters.Add("@Location", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyLocation);
                cmd.Parameters.Add("@Custody", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyCustody);
                cmd.Parameters.Add("@ScrapDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.stopDate);
                cmd.Parameters.Add("@Summons", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertySummons);
                cmd.Parameters.Add("@Receipt", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyReceipt);
                cmd.Parameters.Add("@Accounting", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.propertyAccounting);
                cmd.Parameters.Add("@RecordedDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.inputDate);
                cmd.Parameters.Add("@OutDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.outputDate);
                cmd.Parameters.Add("@FundSource", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(propertyRecordData.fundSource);
                cmd.Parameters.Add("@FundSourceAssist", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.fundAssist);
                cmd.Parameters.Add("@FundSourceDonate", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.fundDonate);
                cmd.Parameters.Add("@PurchaseDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyRecordData.buyDate);
                cmd.Parameters.Add("@Residual", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.Remnants);
                cmd.Parameters.Add("@PurchaseSource", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.buySource);
                cmd.Parameters.Add("@ExpirationDate", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.userYear);
                cmd.Parameters.Add("@Price", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.propertyPrice);
                cmd.Parameters.Add("@Depreciation", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.Depreciation);
                cmd.Parameters.Add("@SinceFundraising", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.selfFunds);
                cmd.Parameters.Add("@Procurement", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.Purchaser);
                cmd.Parameters.Add("@Grants", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(propertyRecordData.Grant);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyRecordData.Remark);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                returnValue[1] = propertyRecordData.repairID;
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

    public CreatePropertyRecord getPropertyRecordDataBase(string ID)
    {
        CreatePropertyRecord returnValue = new CreatePropertyRecord();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT PropertyRecord.*, StaffDatabase.StaffName FROM PropertyRecord " +
                 "LEFT JOIN StaffDatabase ON PropertyRecord.Procurement=StaffDatabase.StaffID " +
                 "WHERE PropertyRecord.isDeleted=0 AND PropertyRecord.RecordID=@RecordID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@RecordID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue.repairID = dr["RecordID"].ToString();
                    returnValue.Unit = dr["Unit"].ToString();
                    returnValue.fillInDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.propertyStatus = dr["PropertyState"].ToString();
                    returnValue.propertyChange = dr["PropertyAddImpairment"].ToString();
                    returnValue.changeStatus = dr["PropertyChangeState"].ToString();
                    returnValue.propertyID = dr["PropertyID"].ToString();
                    returnValue.propertyCode = dr["Code"].ToString();
                    returnValue.applyID = dr["ApplyID"].ToString();
                    returnValue.propertyCategory = dr["Category"].ToString();
                    returnValue.propertyName = dr["PropertyName"].ToString();
                    returnValue.propertyLabel = dr["Label"].ToString();
                    returnValue.propertyUnit = dr["ItemUnit"].ToString();
                    returnValue.propertyQuantity = dr["Quantity"].ToString();
                    returnValue.propertyFitting = dr["Fitting"].ToString();
                    returnValue.propertyLocation = dr["Location"].ToString();
                    returnValue.propertyCustody = dr["Custody"].ToString();
                    returnValue.stopDate = DateTime.Parse(dr["ScrapDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.propertySummons = dr["Summons"].ToString();
                    returnValue.propertyReceipt = dr["Receipt"].ToString();
                    returnValue.propertyAccounting = dr["Accounting"].ToString();
                    returnValue.inputDate = DateTime.Parse(dr["RecordedDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.outputDate = DateTime.Parse(dr["OutDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.fundSource = dr["FundSource"].ToString();
                    returnValue.fundAssist = dr["FundSourceAssist"].ToString();
                    returnValue.fundDonate = dr["FundSourceDonate"].ToString();
                    returnValue.buyDate = DateTime.Parse(dr["PurchaseDate"].ToString()).ToString("yyyy-MM-dd");
                    returnValue.Remnants = dr["Residual"].ToString();
                    returnValue.buySource = dr["PurchaseSource"].ToString();
                    returnValue.userYear = dr["ExpirationDate"].ToString();
                    returnValue.propertyPrice = dr["Price"].ToString();
                    returnValue.Depreciation = dr["Depreciation"].ToString();
                    returnValue.selfFunds = dr["SinceFundraising"].ToString();
                    returnValue.Purchaser = dr["Procurement"].ToString();
                    returnValue.PurchaserName = dr["StaffName"].ToString();
                    returnValue.Grant = dr["Grants"].ToString();
                    returnValue.Remark = dr["Remark"].ToString();
                    returnValue.attachment1 = dr["Annex1"].ToString();
                    returnValue.attachment2 = dr["Annex2"].ToString();
                    returnValue.attachment3 = dr["Annex3"].ToString();
                    returnValue.attachment4 = dr["Annex4"].ToString();
                    returnValue.attachment5 = dr["Annex5"].ToString();
                    returnValue.attachment6 = dr["Annex6"].ToString();
                    returnValue.attachment7 = dr["Annex7"].ToString();
                    returnValue.attachment8 = dr["Annex8"].ToString();
                    returnValue.ChangesArray = new List<PropertyChangesExplainData>();
                    //returnValue.checkNo = "1";
                }
                dr.Close();
                sql = "SELECT PropertyChangesExplain.*, StaffDatabase.StaffName FROM PropertyChangesExplain " +
                "INNER JOIN StaffDatabase ON PropertyChangesExplain.RelatedPersonnel=StaffDatabase.StaffID " +
                "WHERE PropertyChangesExplain.isDeleted=0 AND PropertyChangesExplain.RecordID=@RecordID";
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@RecordID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(ID);
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    PropertyChangesExplainData addValue = new PropertyChangesExplainData();
                    addValue.cID = dr["ID"].ToString();
                    addValue.recordID = dr["RecordID"].ToString();
                    addValue.moveDate = DateTime.Parse(dr["ChangeDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.moveAbstract = dr["Summary"].ToString();
                    addValue.relatedByID = dr["RelatedPersonnel"].ToString();
                    addValue.relatedBy = dr["StaffName"].ToString();
                    returnValue.ChangesArray.Add(addValue);
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

    public string[] createPropertyChangesRecord(PropertyChangesExplainData propertyChangesData)
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
                string sql = "INSERT INTO PropertyChangesExplain(RecordID, ChangeDate, Summary, RelatedPersonnel, CreateFileBy, UpFileBy, UpFileDate ) " +
                             "VALUES (@RecordID, @ChangeDate, @Summary, @RelatedPersonnel, @CreateFileBy, @UpFileBy, (getDate()) )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@RecordID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(propertyChangesData.recordID);
                cmd.Parameters.Add("@ChangeDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(propertyChangesData.moveDate);
                cmd.Parameters.Add("@Summary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyChangesData.moveAbstract);
                cmd.Parameters.Add("@RelatedPersonnel", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
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

    public string[] setPropertyChangesRecord(PropertyChangesExplainData propertyChangesData)
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
                string sql = "UPDATE PropertyChangesExplain SET Summary=@Summary, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                            "WHERE ID=@ID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Chk.CheckStringtoInt64Function(propertyChangesData.cID);
                cmd.Parameters.Add("@Summary", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(propertyChangesData.moveAbstract);
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

    public string[] delPropertyChangesRecord(string tID)
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
                string sql = "UPDATE PropertyChangesExplain SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE ID=@ID";
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

    public string[] SearchPropertyRecordDataCount(SearchPropertyRecord searchRecordData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchPropertyRecordConditionReturn(searchRecordData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM PropertyRecord WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@PropertyID", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(searchRecordData.txtpropertyID) + "%";
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(searchRecordData.txtpropertyName) + "%";
                cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringtoIntFunction(searchRecordData.txtpropertyCode)+"%";
                cmd.Parameters.Add("@ApplyID", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringtoIntFunction(searchRecordData.txtapplyID) + "%";
                cmd.Parameters.Add("@Location", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchRecordData.txtlocation);
                cmd.Parameters.Add("@Custody", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchRecordData.txtcustody);
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

    public List<SearchPropertyRecordResult> SearchPropertyRecordData(int indexpage, SearchPropertyRecord searchRecordData)
    {
        List<SearchPropertyRecordResult> returnValue = new List<SearchPropertyRecordResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchPropertyRecordConditionReturn(searchRecordData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY PropertyRecord.Code DESC) " +
                            "AS RowNum, PropertyRecord.*, PropertyLocation.LocationName, PropertyCustody.CustodyName FROM PropertyRecord " +
                            "LEFT JOIN PropertyLocation ON PropertyRecord.Location=PropertyLocation.ID " +
                            "LEFT JOIN PropertyCustody ON PropertyRecord.Custody=PropertyCustody.ID " +
                            "WHERE PropertyRecord.isDeleted=0 " + ConditionReturn + " ) " +
                            "AS NewTable " +
                            "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@PropertyID", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(searchRecordData.txtpropertyID) + "%";
                cmd.Parameters.Add("@PropertyName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(searchRecordData.txtpropertyName) + "%";
                cmd.Parameters.Add("@Code", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringtoIntFunction(searchRecordData.txtpropertyCode) + "%";
                cmd.Parameters.Add("@ApplyID", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringtoIntFunction(searchRecordData.txtapplyID) + "%";
                cmd.Parameters.Add("@Location", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchRecordData.txtlocation);
                cmd.Parameters.Add("@Custody", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(searchRecordData.txtcustody);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchPropertyRecordResult addValue = new SearchPropertyRecordResult();
                    addValue.ID = dr["RecordID"].ToString();
                    addValue.txtwriteDate = DateTime.Parse(dr["WriteDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtbuyDate = DateTime.Parse(dr["PurchaseDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.txtpropertyPrice = dr["Price"].ToString();
                    addValue.txtpropertyID = dr["PropertyID"].ToString();
                    addValue.txtcode = dr["Code"].ToString();
                    addValue.txtapplyID = dr["ApplyID"].ToString();
                    addValue.txtpropertyName = dr["PropertyName"].ToString();
                    addValue.txtpropertyState = dr["PropertyState"].ToString();
                    addValue.txtlocation = dr["LocationName"].ToString();
                    addValue.txtcustody = dr["CustodyName"].ToString();
                    addValue.txtUnit = dr["Unit"].ToString();
                    addValue.checkNo = "1";
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                SearchPropertyRecordResult addValue = new SearchPropertyRecordResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchPropertyRecordConditionReturn(SearchPropertyRecord searchRecordData)
    {
        string ConditionReturn = "";
        if (searchRecordData.txtpropertyCode != null)
        {
            ConditionReturn += " AND PropertyRecord.Code LIKE @Code ";
        }
        if (searchRecordData.txtpropertyID != null)
        {
            ConditionReturn += " AND PropertyRecord.PropertyID LIKE @PropertyID ";
        }
        if (searchRecordData.txtapplyID != null)
        {
            ConditionReturn += " AND PropertyRecord.ApplyID LIKE @ApplyID ";
        }
        if (searchRecordData.txtpropertyName != null)
        {
            ConditionReturn += " AND PropertyRecord.PropertyName like @PropertyName ";
        }
        if (searchRecordData.txtlocation != null)
        {
            ConditionReturn += " AND PropertyRecord.Location=(@Location) ";
        }
        if (searchRecordData.txtcustody != null)
        {
            ConditionReturn += " AND PropertyRecord.Custody=(@Custody) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        this.propertyFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND PropertyRecord.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] OutPropertyRecordData(string rID, string Unit)
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
                string sql = "UPDATE PropertyRecord SET Unit=@Unit, Location=0, Custody=0, UpFileBy=@UpFileBy, UpFileDate=(getDate()) " +
                    "WHERE RecordID=@RecordID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@RecordID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(rID);
                cmd.Parameters.Add("@Unit", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(Unit);
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
}
