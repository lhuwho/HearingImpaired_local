using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Web.Security;

/// <summary>
/// Summary description for ManageDataBase
/// </summary>
public class ManageDataBase
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
	public ManageDataBase()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public List<StaffDataList> getNoMembershipStaffDataList()
    {
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StaffDatabase a WHERE NOT EXISTS (select UserName FROM aspnet_Users b WHERE b.UserName=a.StaffID) AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.sID = dr["StaffID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sEmail = dr["Email"].ToString();
                    returnValue.Add(addValue);

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffDataList addValue = new StaffDataList();
                addValue.sID = "-1";
                addValue.sName = e.Message;
                returnValue.Add(addValue);
            }

        }
        return returnValue;
    }
    public List<StaffDataList> getMembershipStaffDataList()
    {
        List<StaffDataList> returnValue = new List<StaffDataList>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM StaffDatabase a WHERE EXISTS (select UserName FROM aspnet_Users b WHERE b.UserName=a.StaffID) AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    StaffDataList addValue = new StaffDataList();
                    addValue.sID = dr["StaffID"].ToString();
                    addValue.sName = dr["StaffName"].ToString();
                    addValue.sEmail = dr["Email"].ToString();
                    MembershipUser UserName = Membership.GetUser(addValue.sID, true);
                    addValue.pw = UserName.GetPassword();
                    addValue.Roles = new string[5];
                    addValue.Roles = getMembershipStaffRoles(addValue.sID);
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffDataList addValue = new StaffDataList();
                addValue.sID = "-1";
                addValue.sName = e.Message;
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] getMembershipStaffRoles(string StaffID)
    {
        string[] returnValue = new string[6];
        returnValue[0] = "0";
        returnValue[1] = "0";
        returnValue[2] = "0";
        returnValue[3] = "0";
        returnValue[4] = "0";
        returnValue[5] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM Staff_Competence_Roles WHERE StaffID=@StaffID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(StaffID);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue[0] = dr["ID"].ToString();
                    returnValue[1] = dr["Roles1"].ToString();
                    returnValue[2] = dr["Roles2"].ToString();
                    returnValue[3] = dr["Roles3"].ToString();
                    returnValue[4] = dr["Roles4"].ToString();
                    returnValue[5] = dr["Roles5"].ToString();

                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                StaffDataList addValue = new StaffDataList();
                addValue.sID = "-1";
                addValue.sName = e.Message;
                //returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] getSalaryValue()
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
                string sql = "SELECT TOP 1 * FROM PointValue ORDER BY ID DESC";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    returnValue[0] = dr["PointValue"].ToString();
                }
                dr.Close();
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
    public string[] createSalaryValue(string Value) {
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
                string sql = "INSERT INTO PointValue (PointValue, CreateFileBy, CreateFileDate) " +
                    "VALUES(@PointValue, @CreateFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@PointValue", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(Value);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
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

    public string[] createAidsBrandList(CreateAidsBrand structValue)
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
                string sql = "INSERT INTO AidsBrandTable (Category, Brand, CreateFileBy, CreateFileDate, UpFileBy , UpFileDate) " +
                    "VALUES(@Category, @Brand, @CreateFileBy, getDate(), @UpFileBy, getDate() )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Category", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(structValue.brandType);
                cmd.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structValue.brandName);
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
    public string[] setAidsBrandList(CreateAidsBrand structValue)
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
                string sql = "UPDATE AidsBrandTable SET  Brand=@Brand, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                             "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(structValue.ID);
                cmd.Parameters.Add("@Brand", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(structValue.brandName);
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
    public string[] delAidsBrandList(string sID)
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
                string sql = "UPDATE AidsBrandTable SET  isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=getDate() " +
                             "WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
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
    public AidsTypestruct getAidsBrandList()
    {
        AidsTypestruct returnValue = new AidsTypestruct();
        returnValue.HearingList = new List<CreateAidsBrand>();
        returnValue.FMList = new List<CreateAidsBrand>();
        returnValue.eEarList = new List<CreateAidsBrand>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM AidsBrandTable ORDER BY ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateAidsBrand addValue = new CreateAidsBrand();
                    addValue.ID = dr["ID"].ToString();
                    addValue.brandName = dr["Brand"].ToString();
                    addValue.brandType = dr["Category"].ToString();
                    if (addValue.brandType == "1")
                    {
                        returnValue.HearingList.Add(addValue);
                    }
                    else if (addValue.brandType == "2")
                    {
                        returnValue.eEarList.Add(addValue);
                    }
                    else if (addValue.brandType == "3")
                    {
                        returnValue.FMList.Add(addValue);
                    }
                    
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
    public List<string[]> getRolesDataList() {
        List<string[]> returnValue = new List<string[]>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM Competence_Roles WHERE isDeleted=0 ORDER BY ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] addValue = new string[2];
                    addValue[0] = dr["ID"].ToString();
                    addValue[1] = dr["RolesName"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
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
    public string[] setStaffRolesData(List<string> RolesList, string sID)
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
                string sql = "";
                SqlCommand cmd ;
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                if (RolesList[0] == "0")
                {
                    sql = "INSERT INTO Staff_Competence_Roles (StaffID, Roles1, Roles2, Roles3, Roles4, Roles5, CreateFileBy, UpFileBy, UpFileDate) "+
                        "VALUES(@StaffID, @Roles1, @Roles2, @Roles3, @Roles4, @Roles5, @CreateFileBy, @UpFileBy, (getDate()))";
                }
                else
                {
                    sql = "UPDATE Staff_Competence_Roles SET Roles1=@Roles1, Roles2=@Roles2, Roles3=@Roles3, Roles4=@Roles4, "+
                            "Roles5=@Roles5, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE StaffID=@StaffID AND ID=@ID";
                }
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RolesList[0]);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sID);
                cmd.Parameters.Add("@Roles1", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RolesList[1]);
                cmd.Parameters.Add("@Roles2", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RolesList[2]);
                cmd.Parameters.Add("@Roles3", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RolesList[3]);
                cmd.Parameters.Add("@Roles4", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RolesList[4]);
                cmd.Parameters.Add("@Roles5", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(RolesList[5]);
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
}
