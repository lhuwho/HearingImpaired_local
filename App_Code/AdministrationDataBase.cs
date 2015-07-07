using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


/// <summary>
/// Summary description for AdministrationDataBase
/// </summary>
public class AdministrationDataBase
{
    CheckDataTypeClass Chk = new CheckDataTypeClass();
    public string[] _StaffhaveRoles = new string[5] { "0", "0", "0", "0", "0" };
    public AdministrationDataBase()
	{
		//
		// TODO: Add constructor logic here
		//

	}
    public void libraryFunction()
    {
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.library[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.library[1];//跨區與否
    }
    public void caseBTFunction()
    {
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.caseBT[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.caseBT[1];//跨區與否
    }
    public void teachBTFunction()
    {
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.teachBT[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.teachBT[1];//跨區與否
    }
    public void serviceFeesFunction()//服務費
    {
        StaffDataBase sDB = new StaffDataBase();
        RolesStruct StaffAllRoles = sDB.getStaffRoles(HttpContext.Current.User.Identity.Name);
        //_StaffhaveRoles[0] = StaffAllRoles.caseStu[0];//權限

        char[] haveRolesitem = new char[4] { '0', '0', '0', '0' };
        haveRolesitem = StaffAllRoles.serviceFees[0].ToCharArray();
        _StaffhaveRoles[0] = haveRolesitem[0].ToString();//刪除
        _StaffhaveRoles[1] = haveRolesitem[1].ToString();//更新
        _StaffhaveRoles[2] = haveRolesitem[2].ToString();//新增
        _StaffhaveRoles[3] = haveRolesitem[3].ToString();//查詢
        _StaffhaveRoles[4] = StaffAllRoles.serviceFees[1];//跨區與否
    }
    public int PageMinNumFunction()
    {
        AspAjax aspAjax = new AspAjax();
        return aspAjax.PageMinNumFunction();
    }

    public List<string[]> getClassification()
    {
        List<string[]> returnValue = new List<string[]>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT * FROM BookCategory WHERE isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    string[] addValue = new string[3];
                    addValue[0]=dr["ID"].ToString();
                    addValue[1]=dr["CategoryCode"].ToString();
                    addValue[2]=dr["CategoryName"].ToString();
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string[] addValue = new string[3];
                addValue[0] = "-1";
                addValue[1] = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] createBookDataBase(CreateBook bookData)
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
                string sql = "INSERT INTO BookDatabase (Unit, BookCodeID, BookName, CategoryID, Author, Press, PressDate, Remark, ArchivingDate, Status, " +
                    "CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES "+
                    "(@Unit, @BookCodeID, @BookName, @CategoryID, @Author, @Press, @PressDate, @Remark, @ArchivingDate, 1, " +
                    "@CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                cmd.Parameters.Add("@BookCodeID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.bookNumber);
                cmd.Parameters.Add("@BookName", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.bookTitle);
                cmd.Parameters.Add("@CategoryID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(bookData.bookClassification);
                cmd.Parameters.Add("@Author", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.bookAuthor);
                cmd.Parameters.Add("@Press", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.bookPress);
                cmd.Parameters.Add("@PressDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(bookData.bookPressDate);
                cmd.Parameters.Add("@Remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.bookRemark);
                cmd.Parameters.Add("@ArchivingDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(bookData.bookFilingDate);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                returnValue[0] = cmd.ExecuteNonQuery().ToString();
                if (returnValue[0] != "0")
                {
                    Int64 Column = 0;
                    sql = "select IDENT_CURRENT('BookDatabase') AS bID";
                    cmd = new SqlCommand(sql, Sqlconn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Column = Int64.Parse(dr["bID"].ToString());
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        sql = "SELECT Count(*) AS QCOUNT FROM BookDatabase WHERE CategoryID=(@CategoryID) ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@CategoryID", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(bookData.bookClassification);
                        string stuNumber = cmd.ExecuteScalar().ToString();
                        string stuIDName = CreateFileName[2] + Chk.CheckStringFunction(bookData.bookClassificationCode) + stuNumber.PadLeft(3, '0');

                        sql = "UPDATE BookDatabase SET BookCodeID=(@BookCodeID) WHERE BookID=(@BID) ";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@BID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@BookCodeID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(stuIDName);
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

    public string[] searchBookCount(SearchBook bookData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookConditionReturn(bookData, "BookDatabase");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM BookDatabase WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtbookNumber", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.txtbookNumber);
                cmd.Parameters.Add("@txtbookTitle", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookTitle) + "%";
                cmd.Parameters.Add("@txtbookClassification", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(bookData.txtbookClassification);
                cmd.Parameters.Add("@txtbookAuthor", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookAuthor) + "%";
                cmd.Parameters.Add("@txtbookPress", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookPress) + "%";
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

    public List<CreateBook> searchBook(int indexpage, SearchBook bookData)
    {
        List<CreateBook> returnValue = new List<CreateBook>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookConditionReturn(bookData, "BookDatabase");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY BookDatabase.BookID DESC) " +
                             "AS RowNum, BookDatabase.*, BookCategory.CategoryCode, BookCategory.CategoryName FROM BookDatabase " +
                             "RIGHT JOIN BookCategory ON BookDatabase.CategoryID=BookCategory.ID " +
                             "WHERE BookDatabase.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtbookNumber", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.txtbookNumber);
                cmd.Parameters.Add("@txtbookTitle", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookTitle) + "%";
                cmd.Parameters.Add("@txtbookClassification", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(bookData.txtbookClassification);
                cmd.Parameters.Add("@txtbookAuthor", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookAuthor) + "%";
                cmd.Parameters.Add("@txtbookPress", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookPress) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateBook addValue = new CreateBook();
                    addValue.bID = dr["BookID"].ToString();
                    addValue.bookNumber = dr["BookCodeID"].ToString();
                    addValue.bookTitle = dr["BookName"].ToString();
                    addValue.bookClassification = dr["CategoryID"].ToString();
                    addValue.bookClassificationCode = dr["CategoryCode"].ToString();
                    addValue.bookClassificationName = dr["CategoryName"].ToString();
                    addValue.bookAuthor = dr["Author"].ToString();
                    addValue.bookPress = dr["Press"].ToString();
                    addValue.bookPressDate = DateTime.Parse(dr["PressDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.bookRemark = dr["Remark"].ToString();
                    addValue.bookFilingDate = DateTime.Parse(dr["ArchivingDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.bookStatus = dr["Status"].ToString();
                    addValue.checkNo = "1";
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateBook addValue = new CreateBook();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchBookConditionReturn(SearchBook bookData, string bookDBName)
    {
        string ConditionReturn = "";
        if (bookData.txtbookNumber != null)
        {
            ConditionReturn += " AND BookCodeID=@txtbookNumber ";
        }
        if (bookData.txtbookTitle != null)
        {
            ConditionReturn += " AND BookName like @txtbookTitle ";
        }
        if (bookData.txtbookClassification != null)
        {
            ConditionReturn += " AND CategoryID=@txtbookClassification ";
        }
        if (bookData.txtbookAuthor != null)
        {
            ConditionReturn += " AND Author like @txtbookAuthor ";
        }
        if (bookData.txtbookPress != null)
        {
            ConditionReturn += " AND Press like @txtbookPress ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND " + bookDBName + ".Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] setBookData1(SearchBookResult bookDataResult)
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
                string sql = "UPDATE BookDatabase SET BookName=@executionTitle, Author=@executionAuthor, Press=@executionPress, PressDate=@executionPressDate, " +
                            "Remark=@executionRemark, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE BookID=@bID AND isDeleted=0";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@bID", SqlDbType.BigInt).Value = bookDataResult.bID;
                cmd.Parameters.Add("@executionTitle", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookDataResult.executionTitle);
                cmd.Parameters.Add("@executionAuthor", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookDataResult.executionAuthor);
                cmd.Parameters.Add("@executionPress", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookDataResult.executionPress);
                cmd.Parameters.Add("@executionPressDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(bookDataResult.executionPressDate);
                cmd.Parameters.Add("@executionRemark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookDataResult.executionRemark);
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

    public string[] delBookData(Int64 bID)
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
                string sql = "UPDATE BookDatabase SET isDeleted=1, UpFileBy=@UpFileBy, UpFileDate=(getDate()) WHERE BookID=@bID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@bID", SqlDbType.BigInt).Value = bID;
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


    public string[] searchUserData(SearchUser userData)
    {
        string[] returnValue = new string[4];
        returnValue[0] = "0";
        returnValue[1] = "";
        returnValue[2] = "0"; //
        returnValue[3] = "";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                CaseDataBase cDB = new CaseDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(userData.txtpeopleID);
                if (CreateFileName[1].Length == 0)
                {
                    CreateFileName = cDB.getStudentDataName(userData.txtpeopleID);
                    if (CreateFileName[1].Length != 0)
                    {
                        CreateFileName[2] = "2";
                    }
                    else {
                        CreateFileName[0] = "0";
                    }
                }
                else
                {
                    CreateFileName[2] = "1";
                }
                returnValue[0] = CreateFileName[0];
                returnValue[1] = CreateFileName[1];
                returnValue[2] = CreateFileName[2];
                returnValue[3] = CreateFileName[3];
            }
            catch (Exception e)
            {
                returnValue[0] = "-1";
                returnValue[1] = e.Message.ToString();
                returnValue[2] = "0";
            }
        }
        return returnValue;
    }

    public List<CreateBookSystem> createBookSystemDataBase(CreateBookSystem bookSystemData)
    {
        List<CreateBookSystem> returnValue = new List<CreateBookSystem>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                string[] checkBookData = getBookDataName(bookSystemData.bookCode);
                if (int.Parse(checkBookData[0]) > 0 && int.Parse(checkBookData[1]) == 1)
                {
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                    Sqlconn.Open();
                    string sql = "INSERT INTO BookManage (Unit, Status, ClassID, BorrowerIdentity, BorrowerID, BookID, BorrowDate, MaturityDate, " +
                        "CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES " +
                        "(@Unit, @Status, @ClassID, @BorrowerIdentity, @BorrowerID, @BookID, (getDate()), DATEADD(day,7,getDate()), " +
                        "@CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@Unit", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(CreateFileName[2]);
                    cmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = 1;
                    //cmd.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(bookSystemData.borrowerClassID);
                    cmd.Parameters.Add("@ClassID", SqlDbType.BigInt).Value = 0;
                    cmd.Parameters.Add("@BorrowerIdentity", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(bookSystemData.borrowerStatus);
                    cmd.Parameters.Add("@BorrowerID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(bookSystemData.borrowerID);
                    cmd.Parameters.Add("@BookID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(checkBookData[2]);
                    cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                    cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                    string numberStr = cmd.ExecuteNonQuery().ToString();
                    if (numberStr != "0")
                    {
                        Int64 Column = 0;
                        sql = "select IDENT_CURRENT('BookManage') AS bID";
                        cmd = new SqlCommand(sql, Sqlconn);
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            Column = Int64.Parse(dr["bID"].ToString());
                        }
                        dr.Close();

                        sql = "UPDATE BookDatabase SET Status=@Status WHERE BookID=@bookID AND isDeleted=0";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = 2;
                        cmd.Parameters.Add("@bookID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(checkBookData[2]);
                        string numberStr2 = cmd.ExecuteNonQuery().ToString();

                        if (Column != 0 && numberStr2 != "0")
                        {
                            if (bookSystemData.borrowerStatus == "1")
                            {
                                sql = "SELECT BookManage.*, BookDatabase.BookCodeID, BookDatabase.BookName, StaffDatabase.StaffName AS BorrowerName FROM BookManage " +
                                "RIGHT JOIN StaffDatabase ON BookManage.BorrowerID=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                                "RIGHT JOIN BookDatabase ON BookManage.BookID=BookDatabase.BookID AND BookDatabase.isDeleted=0 " +
                                "WHERE BookManage.isDeleted=0 AND BookManage.ID=@BID";
                            }
                            else if (bookSystemData.borrowerStatus == "2")
                            {
                                sql = "SELECT BookManage.*, BookDatabase.BookCodeID, BookDatabase.BookName, StudentDatabase.StudentName AS BorrowerName FROM BookManage " +
                                "RIGHT JOIN StudentDatabase ON BookManage.BorrowerID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                                "RIGHT JOIN BookDatabase ON BookManage.BookID=BookDatabase.BookID AND BookDatabase.isDeleted=0 " +
                                "WHERE BookManage.isDeleted=0 AND BookManage.ID=@BID";
                            }
                            cmd = new SqlCommand(sql, Sqlconn);
                            cmd.Parameters.Add("@BID", SqlDbType.BigInt).Value = Column;
                            dr = cmd.ExecuteReader();
                            while (dr.Read())
                            {
                                CreateBookSystem addValue = new CreateBookSystem();
                                addValue.bID = Column.ToString();
                                addValue.borrowStatus = dr["Status"].ToString();
                                addValue.borrowerClassID = dr["ClassID"].ToString();
                                addValue.borrowerName = dr["BorrowerName"].ToString();
                                addValue.borrowerID = dr["BorrowerID"].ToString();
                                addValue.borrowerStatus = dr["BorrowerIdentity"].ToString();
                                addValue.bookCode = dr["BookCodeID"].ToString();
                                addValue.bookName = dr["BookName"].ToString();
                                addValue.borrowDate = DateTime.Parse(dr["BorrowDate"].ToString()).ToString("yyyy-MM-dd");
                                addValue.expireDate = DateTime.Parse(dr["MaturityDate"].ToString()).ToString("yyyy-MM-dd");
                                addValue.checkNo = "1";
                                returnValue.Add(addValue);
                            }
                            dr.Close();
                            Sqlconn.Close();
                        }
                    }
                    Sqlconn.Close();
                }
                else if (int.Parse(checkBookData[1]) == 2)
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.checkNo = "0";
                    addValue.errorMsg = "此本圖書已借出";
                    returnValue.Add(addValue);
                }
                else if (int.Parse(checkBookData[0]) == 0)
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.checkNo = checkBookData[0];
                    addValue.errorMsg = "查無此本圖書";
                    returnValue.Add(addValue);
                }
                else
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.checkNo = checkBookData[0];
                    addValue.errorMsg = checkBookData[1];
                    returnValue.Add(addValue);
                }
            }
            catch (Exception e)
            {
                CreateBookSystem addValue = new CreateBookSystem();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }


    public string[] getBookDataName(string bookCode)
    {
        string[] returnValue = new string[3];
        returnValue[0] = "0";
        returnValue[1] = "0";
        returnValue[2] = "0";
        if (bookCode.Length > 0)
        {
            DataBase Base = new DataBase();
            using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
            {
                try
                {
                    Sqlconn.Open();
                    string sql = "SELECT BookID,Status FROM BookDatabase WHERE BookCodeID=(@BookCodeID) AND isDeleted=0";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@BookCodeID", SqlDbType.NVarChar).Value = bookCode;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        returnValue[0] = "1";
                        returnValue[1] = dr["Status"].ToString();
                        returnValue[2] = dr["BookID"].ToString();
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
        }
        return returnValue;
    }

    public List<CreateBookSystem> setBookReturnDataBase(CreateBookSystem bookSystemData)
    {
        List<CreateBookSystem> returnValue = new List<CreateBookSystem>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                string[] checkBookData = getBookDataName(bookSystemData.bookReturnCode);
                if (int.Parse(checkBookData[0]) > 0 && int.Parse(checkBookData[1]) == 2)
                {
                    StaffDataBase sDB = new StaffDataBase();
                    List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                    Sqlconn.Open();
                    string sql = "SELECT ID,BorrowerIdentity FROM BookManage WHERE BookID=@bookID AND Status=1 AND isDeleted=0";
                    SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                    cmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = 2;
                    cmd.Parameters.Add("@bookID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(checkBookData[2]);
                    SqlDataReader dr = cmd.ExecuteReader();
                    Int64 Column = 0;
                    string borrowerStatus = "0";
                    if (dr.Read())
                    {
                        Column = Int64.Parse(dr["ID"].ToString());
                        borrowerStatus = dr["BorrowerIdentity"].ToString();
                    }
                    dr.Close();
                    if (Column != 0)
                    {
                        sql = "UPDATE BookManage SET Status=@Status,ReturnDate=(getDate()) WHERE ID=@ID AND Status=1 AND isDeleted=0";
                        cmd = new SqlCommand(sql, Sqlconn);
                        cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = Column;
                        cmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = 2;
                        string numberStr = cmd.ExecuteNonQuery().ToString();
                        if (numberStr != "0")
                        {
                            sql = "UPDATE BookDatabase SET Status=@Status WHERE BookID=@bookID AND isDeleted=0";
                            cmd = new SqlCommand(sql, Sqlconn);
                            cmd.Parameters.Add("@Status", SqlDbType.TinyInt).Value = 1;
                            cmd.Parameters.Add("@bookID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(checkBookData[2]);
                            string numberStr2 = cmd.ExecuteNonQuery().ToString();

                            if (numberStr2 != "0")
                            {
                                if (borrowerStatus == "1")
                                {
                                    sql = "SELECT BookManage.*, BookDatabase.BookCodeID, BookDatabase.BookName, StaffDatabase.StaffName AS BorrowerName FROM BookManage " +
                                    "RIGHT JOIN StaffDatabase ON BookManage.BorrowerID=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                                    "RIGHT JOIN BookDatabase ON BookManage.BookID=BookDatabase.BookID AND BookDatabase.isDeleted=0 " +
                                    "WHERE BookManage.isDeleted=0 AND BookManage.ID=@BID";
                                }
                                else if (borrowerStatus == "2")
                                {
                                    sql = "SELECT BookManage.*, BookDatabase.BookCodeID, BookDatabase.BookName, StudentDatabase.StudentName AS BorrowerName FROM BookManage " +
                                    "RIGHT JOIN StudentDatabase ON BookManage.BorrowerID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                                    "RIGHT JOIN BookDatabase ON BookManage.BookID=BookDatabase.BookID AND BookDatabase.isDeleted=0 " +
                                    "WHERE BookManage.isDeleted=0 AND BookManage.ID=@BID";
                                }
                                cmd = new SqlCommand(sql, Sqlconn);
                                cmd.Parameters.Add("@BID", SqlDbType.BigInt).Value = Column;
                                dr = cmd.ExecuteReader();
                                while (dr.Read())
                                {
                                    CreateBookSystem addValue = new CreateBookSystem();
                                    addValue.bID = Column.ToString();
                                    addValue.borrowStatus = dr["Status"].ToString();
                                    addValue.borrowerClassID = dr["ClassID"].ToString();
                                    addValue.borrowerName = dr["BorrowerName"].ToString();
                                    addValue.borrowerID = dr["BorrowerID"].ToString();
                                    addValue.borrowerStatus = dr["BorrowerIdentity"].ToString();
                                    addValue.bookCode = dr["BookCodeID"].ToString();
                                    addValue.bookName = dr["BookName"].ToString();
                                    addValue.borrowDate = DateTime.Parse(dr["BorrowDate"].ToString()).ToString("yyyy-MM-dd");
                                    addValue.expireDate = DateTime.Parse(dr["MaturityDate"].ToString()).ToString("yyyy-MM-dd");
                                    addValue.restoreDate = DateTime.Parse(dr["ReturnDate"].ToString()).ToString("yyyy-MM-dd");
                                    addValue.checkNo = "1";
                                    returnValue.Add(addValue);
                                }
                                dr.Close();
                                Sqlconn.Close();
                            }
                        }
                    }
                    Sqlconn.Close();
                }
                else if (int.Parse(checkBookData[1]) == 1)
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.checkNo = "0";
                    addValue.errorMsg = "此本圖書已歸還";
                    returnValue.Add(addValue);
                }
                else if (int.Parse(checkBookData[0]) == 0)
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.checkNo = checkBookData[0];
                    addValue.errorMsg = "查無此本圖書";
                    returnValue.Add(addValue);
                }
                else
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.checkNo = checkBookData[0];
                    addValue.errorMsg = checkBookData[1];
                    returnValue.Add(addValue);
                }
            }
            catch (Exception e)
            {
                CreateBookSystem addValue = new CreateBookSystem();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] SearchBookDayCount(SearchBookStatistics BookStatisticsData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookStatisticsConditionReturn(BookStatisticsData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM BookManage WHERE isDeleted=0 AND Status=1 AND BorrowerIdentity=@txtbookDayType " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtbookDayType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtbookDayType);
                cmd.Parameters.Add("@txtbookStartDay", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtbookStartDay);
                cmd.Parameters.Add("@txtbookEndDay", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtbookEndDay);
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

    public List<CreateBookSystem> SearchBookDay(int indexpage, SearchBookStatistics BookStatisticsData)
    {
        List<CreateBookSystem> returnValue = new List<CreateBookSystem>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookStatisticsConditionReturn(BookStatisticsData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "";
                if (BookStatisticsData.txtbookDayType == "1")
                {
                    sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY BookManage.BookID DESC) " +
                         "AS RowNum, BookManage.*, BookDatabase.BookCodeID, BookDatabase.BookName, StaffDatabase.StaffName AS BorrowerName FROM BookManage " +
                         "RIGHT JOIN StaffDatabase ON BookManage.BorrowerID=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                         "RIGHT JOIN BookDatabase ON BookManage.BookID=BookDatabase.BookID " +
                         "WHERE BookManage.isDeleted=0 AND BookManage.Status=1 AND BookManage.BorrowerIdentity=@txtbookDayType " + ConditionReturn + " ) " +
                         "AS NewTable " +
                         "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                }
                else if (BookStatisticsData.txtbookDayType == "2")
                {
                    sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY BookManage.BookID DESC) " +
                         "AS RowNum, BookManage.*, BookDatabase.BookCodeID, BookDatabase.BookName, StudentDatabase.StudentName AS BorrowerName FROM BookManage " +
                         "RIGHT JOIN StudentDatabase ON BookManage.BorrowerID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                         "RIGHT JOIN BookDatabase ON BookManage.BookID=BookDatabase.BookID " +
                         "WHERE BookManage.isDeleted=0 AND BookManage.Status=1 AND BookManage.BorrowerIdentity=@txtbookDayType " + ConditionReturn + " ) " +
                         "AS NewTable " +
                         "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                }
                
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtbookDayType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtbookDayType);
                cmd.Parameters.Add("@txtbookStartDay", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtbookStartDay);
                cmd.Parameters.Add("@txtbookEndDay", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtbookEndDay);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.borrowStatus = dr["Status"].ToString();
                    addValue.borrowerClassID = dr["ClassID"].ToString();
                    addValue.borrowerName = dr["BorrowerName"].ToString();
                    addValue.borrowerID = dr["BorrowerID"].ToString();
                    addValue.borrowerStatus = dr["BorrowerIdentity"].ToString();
                    addValue.bookCode = dr["BookCodeID"].ToString();
                    addValue.bookName = dr["BookName"].ToString();
                    addValue.borrowDate = DateTime.Parse(dr["BorrowDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.expireDate = DateTime.Parse(dr["MaturityDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.checkNo = "1";
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateBookSystem addValue = new CreateBookSystem();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    private string SearchBookStatisticsConditionReturn(SearchBookStatistics BookStatisticsData)
    {
        string ConditionReturn = "";
        if (BookStatisticsData.txtbookStartDay != null && BookStatisticsData.txtbookEndDay != null)
        {
            ConditionReturn += " AND (SELECT CONVERT(varchar, BookManage.BorrowDate, 23)) BETWEEN CONVERT(varchar, GETDATE() - @txtbookEndDay, 23) AND CONVERT(varchar, GETDATE() - @txtbookStartDay, 23) ";
        }

        if (BookStatisticsData.txtbookDateStartDate != null && BookStatisticsData.txtbookDateEndDate != null)
        {
            ConditionReturn += " AND (SELECT CONVERT(varchar, BookManage.BorrowDate, 23)) BETWEEN @txtbookDateStartDate AND @txtbookDateEndDate ";
        }

        if (BookStatisticsData.txtrecordBookID != null)
        {
            ConditionReturn += " AND BookManage.BookID=@txtrecordBookID ";
        }
        if (BookStatisticsData.txtrecordBookStartDate != null && BookStatisticsData.txtrecordBookEndDate != null)
        {
            ConditionReturn += " AND (SELECT CONVERT(varchar, BookManage.BorrowDate, 23)) BETWEEN @txtrecordBookStartDate AND @txtrecordBookEndDate ";
        }


        if (BookStatisticsData.txtrecordBorrowerType != null)
        {
            ConditionReturn += " AND BorrowerIdentity=@txtrecordBorrowerType ";
        }
        if (BookStatisticsData.txtrecordBorrowerType == "1" && BookStatisticsData.txtrecordBorrowerName != null)
        {
            ConditionReturn += " AND StaffDatabase.StaffName like @txtrecordBorrowerName ";
        }
        else if (BookStatisticsData.txtrecordBorrowerType == "2" && BookStatisticsData.txtrecordBorrowerName != null)
        {
            ConditionReturn += " AND StudentDatabase.StudentName like @txtrecordBorrowerName ";
        }
        if (BookStatisticsData.txtrecordBorrowerStartDate != null && BookStatisticsData.txtrecordBorrowerEndDate != null)
        {
            ConditionReturn += " AND (SELECT CONVERT(varchar, BookManage.BorrowDate, 23)) BETWEEN @txtrecordBorrowerStartDate AND @txtrecordBorrowerEndDate ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND BookManage.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public List<CreateBookBorrow> SearchBookDate(SearchBookStatistics BookStatisticsData)
    {
        List<CreateBookBorrow> returnValue = new List<CreateBookBorrow>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookStatisticsConditionReturn(BookStatisticsData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM BookManage WHERE isDeleted=0 AND BorrowerIdentity=1 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtbookDateStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateStartDate);
                cmd.Parameters.Add("@txtbookDateEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateEndDate);
                CreateBookBorrow addValue = new CreateBookBorrow();
                addValue.staffBorrowBookSum = cmd.ExecuteScalar().ToString();

                sql = "SELECT COUNT(DISTINCT BorrowerID) FROM BookManage WHERE isDeleted=0 AND BorrowerIdentity=1 " + ConditionReturn;
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtbookDateStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateStartDate);
                cmd.Parameters.Add("@txtbookDateEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateEndDate);
                addValue.staffBorrowerSum = cmd.ExecuteScalar().ToString();

                sql = "SELECT COUNT(*) FROM BookManage WHERE isDeleted=0 AND BorrowerIdentity=2 " + ConditionReturn;
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtbookDateStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateStartDate);
                cmd.Parameters.Add("@txtbookDateEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateEndDate);
                addValue.studentBorrowBookSum = cmd.ExecuteScalar().ToString();

                sql = "SELECT COUNT(DISTINCT BorrowerID) FROM BookManage WHERE isDeleted=0 AND BorrowerIdentity=2 " + ConditionReturn;
                cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtbookDateStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateStartDate);
                cmd.Parameters.Add("@txtbookDateEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtbookDateEndDate);
                addValue.studentBorrowerSum = cmd.ExecuteScalar().ToString();

                addValue.bookBorrowStartDate = DateTime.Parse(BookStatisticsData.txtbookDateStartDate).ToString("yyyy-MM-dd");
                addValue.bookBorrowEndDate = DateTime.Parse(BookStatisticsData.txtbookDateEndDate).ToString("yyyy-MM-dd");
                addValue.checkNo = "1";
                returnValue.Add(addValue);
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateBookBorrow addValue = new CreateBookBorrow();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] SearchBookRecordCount(SearchBookStatistics BookStatisticsData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookStatisticsConditionReturn(BookStatisticsData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM BookManage WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtrecordBookID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtrecordBookID);
                cmd.Parameters.Add("@txtrecordBookStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBookStartDate);
                cmd.Parameters.Add("@txtrecordBookEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBookEndDate);
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

    public List<CreateBookSystem> SearchBookRecord(int indexpage, SearchBookStatistics BookStatisticsData)
    {
        List<CreateBookSystem> returnValue = new List<CreateBookSystem>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookStatisticsConditionReturn(BookStatisticsData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "";
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY BookManage.BookID DESC) " +
                     "AS RowNum, BookManage.*, BookDatabase.BookCodeID, BookDatabase.BookName, StaffDatabase.StaffName AS BorrowerName1, StudentDatabase.StudentName AS BorrowerName2 FROM BookManage " +
                     "LEFT JOIN StaffDatabase ON BookManage.BorrowerID=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                     "LEFT JOIN StudentDatabase ON BookManage.BorrowerID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                     "RIGHT JOIN BookDatabase ON BookManage.BookID=BookDatabase.BookID " +
                     "WHERE BookManage.isDeleted=0 " + ConditionReturn + " ) " +
                     "AS NewTable " +
                     "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtrecordBookID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtrecordBookID);
                cmd.Parameters.Add("@txtrecordBookStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBookStartDate);
                cmd.Parameters.Add("@txtrecordBookEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBookEndDate);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateBookSystem addValue = new CreateBookSystem();
                    addValue.borrowerClassID = dr["ClassID"].ToString();
                    if (dr["BorrowerName1"].ToString() == null || (dr["BorrowerName1"].ToString()).Length ==0)
                    {
                        addValue.borrowerName = dr["BorrowerName2"].ToString();
                    }
                    else
                    {
                        addValue.borrowerName = dr["BorrowerName1"].ToString();
                    }
                    addValue.borrowerID = dr["BorrowerID"].ToString();
                    addValue.borrowerStatus = dr["BorrowerIdentity"].ToString();
                    addValue.bookCode = dr["BookCodeID"].ToString();
                    addValue.bookName = dr["BookName"].ToString();
                    addValue.borrowDate = DateTime.Parse(dr["BorrowDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.restoreDate = DateTime.Parse(dr["ReturnDate"].ToString()).ToString("yyyy-MM-dd");
                    addValue.checkNo = "1";
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateBookSystem addValue = new CreateBookSystem();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] SearchBookRecordBorrowerCount(SearchBookStatistics BookStatisticsData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookStatisticsConditionReturn(BookStatisticsData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM ( " +
                            "SELECT COUNT(BookManage.BorrowerID) AS QCOUNT,BorrowerID FROM BookManage " +
                            "LEFT JOIN StaffDatabase ON BookManage.BorrowerID=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                            "LEFT JOIN StudentDatabase ON BookManage.BorrowerID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                            "WHERE BookManage.isDeleted=0 " + ConditionReturn +
                            "GROUP BY BorrowerID,BorrowerIdentity " +
                            "HAVING COUNT(BorrowerID)>=1 " +
                            ") AS NewTable";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtrecordBorrowerType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtrecordBorrowerType);
                cmd.Parameters.Add("@txtrecordBorrowerStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBorrowerStartDate);
                cmd.Parameters.Add("@txtrecordBorrowerEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBorrowerEndDate);
                cmd.Parameters.Add("@txtrecordBorrowerClassID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtrecordBorrowerClassID);
                cmd.Parameters.Add("@txtrecordBorrowerName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(BookStatisticsData.txtrecordBorrowerName) + "%";
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

    public List<CreateBookRecordBorrower> SearchBookRecordBorrower(int indexpage, SearchBookStatistics BookStatisticsData)
    {
        List<CreateBookRecordBorrower> returnValue = new List<CreateBookRecordBorrower>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookStatisticsConditionReturn(BookStatisticsData);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "";
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY BookManage.BorrowerID DESC) " +
                    "AS RowNum, COUNT(BookManage.BorrowerID) AS QCOUNT,BookManage.BorrowerID,BookManage.BorrowerIdentity," +
                    "StaffDatabase.StaffName AS BorrowerName1,StudentDatabase.StudentName AS BorrowerName2,BookManage.ClassID FROM BookManage " +
                    "LEFT JOIN StaffDatabase ON BookManage.BorrowerID=StaffDatabase.StaffID AND StaffDatabase.isDeleted=0 " +
                    "LEFT JOIN StudentDatabase ON BookManage.BorrowerID=StudentDatabase.StudentID AND StudentDatabase.isDeleted=0 " +
                    "WHERE BookManage.isDeleted=0 " + ConditionReturn +
                    "GROUP BY BookManage.BorrowerID,BookManage.BorrowerIdentity,StaffDatabase.StaffName,StudentDatabase.StudentName,BookManage.ClassID " +
                    "HAVING COUNT(BookManage.BorrowerID)>=1 ) " +
                    "AS NewTable " +
                    "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtrecordBorrowerType", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtrecordBorrowerType);
                cmd.Parameters.Add("@txtrecordBorrowerStartDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBorrowerStartDate);
                cmd.Parameters.Add("@txtrecordBorrowerEndDate", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(BookStatisticsData.txtrecordBorrowerEndDate);
                cmd.Parameters.Add("@txtrecordBorrowerClassID", SqlDbType.BigInt).Value = Chk.CheckStringtoIntFunction(BookStatisticsData.txtrecordBorrowerClassID);
                cmd.Parameters.Add("@txtrecordBorrowerName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(BookStatisticsData.txtrecordBorrowerName) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateBookRecordBorrower addValue = new CreateBookRecordBorrower();
                    addValue.borrowerClassID = dr["ClassID"].ToString();
                    if (dr["BorrowerName1"].ToString().Length ==0)
                    {
                        addValue.borrowerName = dr["BorrowerName2"].ToString();
                    }
                    else
                    {
                        addValue.borrowerName = dr["BorrowerName1"].ToString();
                    }
                    addValue.borrowerID = dr["BorrowerID"].ToString();
                    addValue.borrowerStatus = dr["BorrowerIdentity"].ToString();
                    addValue.borrowQuantity = dr["QCOUNT"].ToString();
                    addValue.checkNo = "1";
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateBookRecordBorrower addValue = new CreateBookRecordBorrower();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] searchBookResultCount(SearchBook bookData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookConditionReturn(bookData, "BookDatabase");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) FROM BookDatabase WHERE isDeleted=0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtbookNumber", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.txtbookNumber);
                cmd.Parameters.Add("@txtbookTitle", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookTitle) + "%";
                cmd.Parameters.Add("@txtbookClassification", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(bookData.txtbookClassification);
                cmd.Parameters.Add("@txtbookAuthor", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookAuthor) + "%";
                cmd.Parameters.Add("@txtbookPress", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookPress) + "%";
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

    public List<CreateBookResult> searchBookResult(int indexpage, SearchBook bookData)
    {
        List<CreateBookResult> returnValue = new List<CreateBookResult>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchBookConditionReturn(bookData, "BookDatabase");
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (ORDER BY BookDatabase.BookID DESC) " +
                             "AS RowNum, BookDatabase.*, BookCategory.CategoryCode, BookCategory.CategoryName FROM BookDatabase " +
                             "RIGHT JOIN BookCategory ON BookDatabase.CategoryID=BookCategory.ID " +
                             "WHERE BookDatabase.isDeleted=0 " + ConditionReturn + " ) " +
                             "AS NewTable " +
                             "WHERE RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtbookNumber", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(bookData.txtbookNumber);
                cmd.Parameters.Add("@txtbookTitle", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookTitle) + "%";
                cmd.Parameters.Add("@txtbookClassification", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(bookData.txtbookClassification);
                cmd.Parameters.Add("@txtbookAuthor", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookAuthor) + "%";
                cmd.Parameters.Add("@txtbookPress", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(bookData.txtbookPress) + "%";
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    CreateBookResult addValue = new CreateBookResult();
                    addValue.bID = dr["BookID"].ToString();
                    addValue.bookNumber = dr["BookCodeID"].ToString();
                    addValue.bookTitle = dr["BookName"].ToString();
                    addValue.bookClassificationCode = dr["CategoryCode"].ToString();
                    addValue.bookClassificationName = dr["CategoryName"].ToString();
                    addValue.bookAuthor = dr["Author"].ToString();
                    addValue.bookPress = dr["Press"].ToString();
                    addValue.checkNo = "1";
                    returnValue.Add(addValue);
                }
                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                CreateBookResult addValue = new CreateBookResult();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }




    public List<CreateTemperatureSystem> getStudentTemperatureData(string studentID, string Year, string Month)
    {
        List<CreateTemperatureSystem> returnValue = new List<CreateTemperatureSystem>();
        CreateTemperatureSystem addValue = new CreateTemperatureSystem();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                
                Sqlconn.Open();
                string sql = "select a.*,b.StudentName from StudentTemperature a left join StudentDatabase b on a.StudentID=b.StudentID where( a.StudentID=@StudentID and " +
                    "Year=@Year and Month=@Month)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
                cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = int.Parse(Year)+1911;
                cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Month;
                /*
                string DateStart = Month + "/1/" + (int.Parse(Year) + 1911);
                string DateEnd = "";
                cmd.Parameters.Add("@CreateFileDatestart", SqlDbType.DateTime).Value = DateStart;
                if (Month=="12")
                    DateEnd = "1/1/" + (int.Parse(Year) + 1 + 1911);
                else
                    DateEnd = (int.Parse(Month) + 1) + "/1/" + (int.Parse(Year) + 1911); 
                cmd.Parameters.Add("@CreateFileDateend", SqlDbType.DateTime).Value =DateEnd;
 */
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                  
                    addValue.tID = dr["ID"].ToString();
                    addValue.txtpeopleID = dr["StudentID"].ToString();
                    addValue.StudentName = dr["StudentName"].ToString();
                    addValue.peopleTemp = dr["StudentTemperature"].ToString();
                    addValue.parentsTemp = dr["ParentTemperature"].ToString();
                    addValue.Year = dr["Year"].ToString();
                    addValue.Month = dr["Month"].ToString();
                    addValue.Day = dr["Day"].ToString();
                    addValue.leaveItem = dr["LeaveItem"].ToString();
                    addValue.leaveStatus = dr["LeaveState"].ToString();

                  
                    returnValue.Add(addValue);
                }
                
                
                dr.Close();
                Sqlconn.Close();
                
            }
            catch (Exception e)
            {
                //Temperature addValue = new Temperature();
                //addValue.checkNo = "-1";
                //addValue.errorMsg = e.Message.ToString();
                //returnValue.Add(addValue);
            }
        }
        if (returnValue.Count == 0)
        {
            using (SqlConnection Sqlconn2 = new SqlConnection(Base.GetConnString()))
            {
                Sqlconn2.Open();
                string sql = "select StudentName from StudentDatabase where StudentID=@StudentID";
                SqlCommand cmd2 = new SqlCommand(sql, Sqlconn2);
                cmd2.Parameters.Add("@StudentID", SqlDbType.Int).Value = studentID;
                SqlDataReader dr2 = cmd2.ExecuteReader();
                string StudentName = "";
                while (dr2.Read())
                {
                    StudentName = dr2["StudentName"].ToString();
                }
                addValue.StudentName = StudentName;
                addValue.txtpeopleID = studentID;
                returnValue.Add(addValue);
                dr2.Close();
            }

        }
                
        return returnValue;
    }
    public string[] updateStudentTemperatureData(List<CreateTemperatureSystem> sTemperatureData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        foreach (CreateTemperatureSystem atom in sTemperatureData)
        {
            deleteStudentTemperatureData(atom.txtpeopleID, atom.Year, atom.Month, atom.Day);
        }
        foreach (CreateTemperatureSystem atom in sTemperatureData)
        {
            returnValue = createStudentTemperatureData(atom);
        }
        return returnValue;

    }
    public void deleteStudentTemperatureData(string StudentID,string Year, string Month , string Day)
    {
        
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "DELETE FROM StudentTemperature where (StudentID=@StudentID and Year = @Year and Month=@Month and Day=@Day)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = StudentID;
                cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = Year;
                cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Month;
                cmd.Parameters.Add("@Day", SqlDbType.NVarChar).Value = Day;
                cmd.ExecuteNonQuery();
                

            }
            catch (Exception e)
            {
 
            }
        }
    }
    public string[] createStudentTemperatureData(CreateTemperatureSystem temperatureDataSystem)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DateTime now = DateTime.Now;
        deleteStudentTemperatureData(temperatureDataSystem.txtpeopleID,now.Year.ToString(), now.Month.ToString(), now.Day.ToString());
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO StudentTemperature (StudentID, StudentTemperature, ParentTemperature, Year,Month,Day ,LeaveItem,LeaveState," +
                    "CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES " +
                    "(@StudentID, @StudentTemperature, @ParentTemperature, @Year,@Month,@Day ,@LeaveItem,@LeaveState, " +
                    "@CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(temperatureDataSystem.txtpeopleID);
                cmd.Parameters.Add("@StudentTemperature", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(temperatureDataSystem.peopleTemp);
                cmd.Parameters.Add("@ParentTemperature", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(temperatureDataSystem.parentsTemp);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@LeaveItem", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(temperatureDataSystem.leaveItem);
                cmd.Parameters.Add("@LeaveState", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.leaveStatus);
                if(string.IsNullOrEmpty(temperatureDataSystem.Year))
                    cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(now.Year.ToString());
                else
                    cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.Year);
                if (string.IsNullOrEmpty(temperatureDataSystem.Month))
                    cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(now.Month.ToString());
                else
                    cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.Month);
                if (string.IsNullOrEmpty(temperatureDataSystem.Day))
                    cmd.Parameters.Add("@Day", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(now.Day.ToString());
                else
                    cmd.Parameters.Add("@Day", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.Day);
               
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


    public List<CreateTeacherSystem> getTeacherTemperatureData(string StaffID, string Year, string Month)
    {
        List<CreateTeacherSystem> returnValue = new List<CreateTeacherSystem>();
        CreateTeacherSystem addValue = new CreateTeacherSystem();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();

                Sqlconn.Open();
                string sql = "select a.*,DAY(a.CreateFileDate) as Day ,MONTH(a.CreateFileDate) as Month,YEAR(a.CreateFileDate) as Year ,b.staffname from teacherTemperature a left join StaffDatabase b on a.TeacherID=b.StaffID " +
                   // " where( 1=1 and " +
                     " where( a.teacherid = @StaffID and " +
                    " YEAR(a.CreateFileDate)=@Year and MONTH(a.CreateFileDate)=@Month )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = StaffID ;
                cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = int.Parse(Year) + 1911;
                cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Month;
                /*
                string DateStart = Month + "/1/" + (int.Parse(Year) + 1911);
                string DateEnd = "";
                cmd.Parameters.Add("@CreateFileDatestart", SqlDbType.DateTime).Value = DateStart;
                if (Month=="12")
                    DateEnd = "1/1/" + (int.Parse(Year) + 1 + 1911);
                else
                    DateEnd = (int.Parse(Month) + 1) + "/1/" + (int.Parse(Year) + 1911); 
                cmd.Parameters.Add("@CreateFileDateend", SqlDbType.DateTime).Value =DateEnd;
 */
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    addValue.tID = dr["ID"].ToString();
                    addValue.txtpeopleID = dr["TeacherID"].ToString();
                    addValue.TeacherName = dr["staffname"].ToString();
                    addValue.TeacherTemp = dr["TeacherTemperature"].ToString();
                    addValue.CheckContent = dr["CheckContent"].ToString();
                    addValue.Year = dr["Year"].ToString();
                    addValue.Day = dr["Day"].ToString();
                    addValue.Month = dr["Month"].ToString();
 

                    returnValue.Add(addValue);
                }


                dr.Close();
                Sqlconn.Close();

            }
            catch (Exception e)
            {
              
                //Temperature addValue = new Temperature();
                //addValue.checkNo = "-1";
                //addValue.errorMsg = e.Message.ToString();
                //returnValue.Add(addValue);
            }
        }
        if (returnValue.Count == 0)
        {
            using (SqlConnection Sqlconn2 = new SqlConnection(Base.GetConnString()))
            {
                Sqlconn2.Open();
                string sql = "select StaffName from StaffDatabase where StaffID=@StaffID";
                SqlCommand cmd2 = new SqlCommand(sql, Sqlconn2);
                cmd2.Parameters.Add("@StaffID", SqlDbType.Int).Value = StaffID;
                SqlDataReader dr2 = cmd2.ExecuteReader();
                string StaffName = "";
                while (dr2.Read())
                {
                    StaffName = dr2["StaffName"].ToString();
                }
                addValue.TeacherName = StaffName;
                addValue.txtpeopleID = StaffID;
                returnValue.Add(addValue);
                dr2.Close();
            }

        }

        return returnValue;
    }

    public string[] updateTeacherTemperatureData(List<CreateTeacherSystem> sTemperatureData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        foreach (CreateTeacherSystem atom in sTemperatureData)
        {
            deleteTeacherTemperatureData(atom.txtpeopleID, atom.Year, atom.Month, atom.Day);
        }
        foreach (CreateTeacherSystem atom in sTemperatureData)
        {
            returnValue = createTeacherTemperatureData(atom);
        }
        return returnValue;

    }
    public void deleteTeacherTemperatureData(string TeacherID, string Year, string Month, string Day)
    {

        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "DELETE FROM TeacherTemperature where (TeacherID=@TeacherID and  YEAR(CreateFileDate)=@Year and MONTH(CreateFileDate)=@Month  and Day(CreateFileDate)=@Day )";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = TeacherID;
                cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = Year;
                cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Month;
                cmd.Parameters.Add("@Day", SqlDbType.NVarChar).Value = Day;
                cmd.ExecuteNonQuery();


            }
            catch (Exception e)
            {

            }
        }
    }
    public string[] createTeacherTemperatureData(CreateTeacherSystem temperatureDataSystem)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DateTime now = DateTime.Now;
        deleteStudentTemperatureData(temperatureDataSystem.txtpeopleID, now.Year.ToString(), now.Month.ToString(), now.Day.ToString());
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = "INSERT INTO TeacherTemperature ([TeacherID],[TeacherTemperature],[CheckContent],[CreateFileBy],[CreateFileDate],[UpFileBy],[UpFileDate] " +
                    " ) VALUES " +
                    "(@TeacherID, @TeacherTemperature, @CheckContent, @CreateFileBy, @CreateFileDate , @UpFileBy, (getDate()))";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(temperatureDataSystem.txtpeopleID);
                cmd.Parameters.Add("@TeacherTemperature", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(temperatureDataSystem.TeacherTemp);
                cmd.Parameters.Add("@CheckContent", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.CheckContent);
                cmd.Parameters.Add("@CreateFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@UpFileBy", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                cmd.Parameters.Add("@CreateFileDate", SqlDbType.Date).Value = (string.IsNullOrEmpty(temperatureDataSystem.Year) ? Chk.CheckStringFunction(now.Year.ToString()) : Chk.CheckStringFunction(temperatureDataSystem.Year)) + "/" + (string.IsNullOrEmpty(temperatureDataSystem.Month) ? Chk.CheckStringFunction(now.Month.ToString()) : Chk.CheckStringFunction(temperatureDataSystem.Month)) + "/" + (string.IsNullOrEmpty(temperatureDataSystem.Day) ? Chk.CheckStringFunction(now.Day.ToString()) : Chk.CheckStringFunction(temperatureDataSystem.Day));
                //if (string.IsNullOrEmpty(temperatureDataSystem.Year))
                //    cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(now.Year.ToString());
                //else
                //    cmd.Parameters.Add("@Year", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.Year);
                //if (string.IsNullOrEmpty(temperatureDataSystem.Month))
                //    cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(now.Month.ToString());
                //else
                //    cmd.Parameters.Add("@Month", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.Month);
                //if (string.IsNullOrEmpty(temperatureDataSystem.Day))
                //    cmd.Parameters.Add("@Day", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(now.Day.ToString());
                //else
                //    cmd.Parameters.Add("@Day", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.Day);

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



    public string[] createTeacherTemperatureData(CreateTemperatureSystem temperatureDataSystem)
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
                string sql = "INSERT INTO TeacherTemperature (TeacherID, TeacherTemperature, " +
                    "CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES " +
                    "(@TeacherID, @TeacherTemperature, " +
                    "@CreateFileBy, (getDate()), @UpFileBy, (getDate()), 0)";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@TeacherID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(temperatureDataSystem.txtpeopleID);
                cmd.Parameters.Add("@TeacherTemperature", SqlDbType.Decimal).Value = Chk.CheckStringtoDecimalFunction(temperatureDataSystem.peopleTemp);
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


}
