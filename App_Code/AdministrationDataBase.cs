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
                    addValue[0] = dr["ID"].ToString();
                    addValue[1] = dr["CategoryCode"].ToString();
                    addValue[2] = dr["CategoryName"].ToString();
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
                    "CreateFileBy, CreateFileDate, UpFileBy, UpFileDate, isDeleted) VALUES " +
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
                    else
                    {
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
                    if (dr["BorrowerName1"].ToString() == null || (dr["BorrowerName1"].ToString()).Length == 0)
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
                    if (dr["BorrowerName1"].ToString().Length == 0)
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
    public void deleteStudentTemperatureData(string StudentID, string Year, string Month, string Day)
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
        deleteStudentTemperatureData(temperatureDataSystem.txtpeopleID, now.Year.ToString(), now.Month.ToString(), now.Day.ToString());
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
                if (string.IsNullOrEmpty(temperatureDataSystem.Year))
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
                cmd.Parameters.Add("@StaffID", SqlDbType.Int).Value = StaffID;
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
    /**  教學管理 - 開始 **/

    public List<CreaHearing_Loss_Tool> GetLossToolQuestion(int Category)
    {
        List<CreaHearing_Loss_Tool> returnValue = new List<CreaHearing_Loss_Tool>();
        CreaHearing_Loss_Tool addValue = new CreaHearing_Loss_Tool();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "select a.SummeryID , b.Description as SummeryDescription, a.ID as QuestionID, a.Description as QuestionDescription  ,b.Category " +
                    " from Hearing_Loss_Tool_Question a  right join Hearing_Loss_Tool_Summery b on a.SummeryID = b.ID  where a.isOpen = 'true' and b.Category = @Category ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@Category", SqlDbType.Int).Value = Category;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    addValue.QuestionID = dr["QuestionID"].ToString();
                    addValue.SummeryID = dr["SummeryID"].ToString();
                    addValue.QuestionDescription = dr["QuestionDescription"].ToString();
                    addValue.SummeryDescription = dr["SummeryDescription"].ToString();
                    addValue.Category = dr["Category"].ToString();
                    returnValue.Add(addValue);
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

    public List<CreaHearing_Loss_Skill> GetLossSkillQuestion()
    {
        List<CreaHearing_Loss_Skill> returnValue = new List<CreaHearing_Loss_Skill>();
        CreaHearing_Loss_Skill addValue = new CreaHearing_Loss_Skill();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "select * from Hearing_Loss_Listen_Skill  ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    addValue.SkillID = dr["ID"].ToString();
                    addValue.Title = dr["Title"].ToString();
                    addValue.SkillDescription = dr["Description"].ToString();

                    returnValue.Add(addValue);
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

    public string[] UpdateHearLoss(List<UpdateHearLoss> sTemperatureData, int SID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        deleteHearLoss(SID);
        foreach (UpdateHearLoss atom in sTemperatureData)
        {
            returnValue = createHearLoss(atom, SID);
        }
        return returnValue;

    }

    public string[] createHearLoss(UpdateHearLoss temperatureDataSystem, int SID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DateTime now = DateTime.Now;
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = " DECLARE @MasterID int " +
                " insert into Hearing_Loss_AnsMaster ( StudentID , LossDate , Tool,page) values ( @StudentID ,@LossDate, @Tool,@page) " +
                "  select @MasterID = (select @@identity) ";
                for (int i = 0; i < Chk.CheckStringFunction(temperatureDataSystem.anser).Split('|').Length; i++)
                {
                    sql += " insert into Hearing_Loss_AnsDetail (MasterID , Anser) values (@MasterID , @Anser" + i.ToString() + ") ";
                }

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = SID;
                string date = temperatureDataSystem.Date;
                cmd.Parameters.Add("@LossDate", SqlDbType.NVarChar).Value = date;
                cmd.Parameters.Add("@Tool", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(temperatureDataSystem.tool);
                cmd.Parameters.Add("@page", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(temperatureDataSystem.page);
                for (int i = 0; i < Chk.CheckStringFunction(temperatureDataSystem.anser).Split('|').Length; i++)
                {
                    cmd.Parameters.Add("@Anser" + i.ToString(), SqlDbType.NVarChar).Value = Chk.CheckStringFunction(temperatureDataSystem.anser.Split('|').GetValue(i).ToString());
                }


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

    public string[] deleteHearLoss(int SID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DateTime now = DateTime.Now;
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();

                Sqlconn.Open();
                string sql = "delete Hearing_Loss_AnsDetail where MasterID in ( select ID from Hearing_Loss_AnsMaster where StudentID = @StudentID)";
                sql += " delete Hearing_Loss_AnsMaster where StudentID = @StudentID ";//SQL重新整理 提高流暢度 **有題目刪減遇上額外問題(待處理)** BY AWho 

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = SID;
                cmd.ExecuteNonQuery();
                //SqlDataReader dr = cmd.ExecuteReader();
                //while (dr.Read())
                //{
                //    using (SqlConnection Sqlconn2 = new SqlConnection(Base.GetConnString()))
                //    {
                //        Sqlconn2.Open();
                //        string sql2 = "DELETE FROM Hearing_Loss_AnsDetail where (MasterID=@MasterID)";
                //        SqlCommand cmd2 = new SqlCommand(sql2, Sqlconn2);
                //        cmd2.Parameters.Add("@MasterID", SqlDbType.Int).Value = int.Parse(dr["ID"].ToString());
                //        cmd2.ExecuteNonQuery();
                //    }
                //}

                //dr.Close();

                //string sql3 = "DELETE FROM Hearing_Loss_AnsMaster where (StudentID=@StudentID)";
                //SqlCommand cmd3 = new SqlCommand(sql3, Sqlconn);
                //cmd3.Parameters.Add("@StudentID", SqlDbType.Int).Value = SID;
                //cmd3.ExecuteNonQuery();



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
    public List<UpdateHearLoss> searchHearLossDataBase(int SID, int page)
    {
        List<UpdateHearLoss> returnvalue = new List<UpdateHearLoss>();
        UpdateHearLoss temp = new UpdateHearLoss();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();

                Sqlconn.Open();
                string sql = "select * from Hearing_Loss_AnsMaster where StudentID=@StudentID and page = @page ";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = SID;
                cmd.Parameters.Add("@page", SqlDbType.Int).Value = page;
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    temp.Date = dr["LossDate"].ToString();
                    temp.tool = dr["Tool"].ToString();
                    temp.page = dr["page"].ToString();
                    using (SqlConnection Sqlconn2 = new SqlConnection(Base.GetConnString()))
                    {
                        Sqlconn2.Open();
                        string sql2 = "select Anser from Hearing_Loss_AnsDetail where MasterID=@MasterID";
                        SqlCommand cmd2 = new SqlCommand(sql2, Sqlconn2);
                        cmd2.Parameters.Add("@MasterID", SqlDbType.Int).Value = int.Parse(dr["ID"].ToString());
                        SqlDataReader dr2 = cmd2.ExecuteReader();
                        int counter = 0;
                        temp.anser = "";// 不清空會造成讀取錯誤
                        while (dr2.Read())
                        {
                            if (counter > 0)
                            {

                                temp.anser += "|";
                            }
                            counter++;
                            temp.anser += dr2["Anser"].ToString();
                        }
                        dr2.Close();
                        Sqlconn2.Close();
                    }
                    returnvalue.Add(temp);

                }

                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {

            }
        }

        return returnvalue;
    }



    public List<SearchStudentResult> ShowStudent(int SID)//顯示學生姓名(有點多餘)
    {
        List<SearchStudentResult> returnvalue = new List<SearchStudentResult>();
        SearchStudentResult temp = new SearchStudentResult();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "select StudentName from StudentDatabase where ID=@StudentID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = SID;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    //temp.txtstudentSex = int.Parse(dr["StudentSex"].ToString());
                    temp.txtstudentName = dr["StudentName"].ToString();
                    //temp.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    returnvalue.Add(temp);
                }

                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {

            }
        }

        return returnvalue;
    }

    public List<AchievementAssessment> CreatAchievementAssessment(AchievementAssessment sTemperatureData)
    {
        List<AchievementAssessment> returnValue = new List<AchievementAssessment>();
        AchievementAssessment temp = new AchievementAssessment();
        DataBase Base = new DataBase();
        string strSql = "";
        string strSqlPara = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {

            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                    // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                    break;
                case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    //cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Date).Value = DateTime.Now.ToLongDateString();
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = " insert into  AchievementAssessment ( " + strSql + ")values(" + strSqlPara + ") ";
                sql += " select @@identity as id ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        case "ID":
                        case "Unit":
                            // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                            break;
                        case "CreateFileBy":
                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name,  System.DBNull.Value);
                            }
                            break;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    temp.ID = dr["id"].ToString();
                }

            }
            catch (Exception e)
            {
                temp.ID = e.Message.ToString();
            }
            Sqlconn.Close();
        }

        //temp.ID = strSql;
        returnValue.Add(temp);
        return returnValue;
    }

    public List<AchievementAssessmentLoad> ShowAchievementAssessment(int ID)
    {
        string strSql = "";
        List<AchievementAssessmentLoad> returnvalue = new List<AchievementAssessmentLoad>();
        AchievementAssessmentLoad temp = new AchievementAssessmentLoad();
        AchievementAssessment sTemperatureData = new AchievementAssessment();

        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {

            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                    break;
                case "Intelligence_Date":
                case "AuditorySkills_Date":
                case "Vocabulary_Date":
                case "Vocabulary1_Date":
                case "Language1_Date":
                case "Language2_Date":
                case "Language3_Date":

                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "convert(varchar, isnull(convert(date, a." + fldInfo.Name + ",1),'1912'), 111) as " + fldInfo.Name;
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "a." + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "select  b.studentname , " + strSql + " from AchievementAssessment a left join studentDatabase b on a.studentid = b.id  where a.ID=@ID and  isnull(a.isDeleted,0) = 0 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    temp.IDname = "studentName";
                    temp.ThisValue = dr["studentName"].ToString();
                    returnvalue.Add(temp);

                    foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                    {

                        switch (fldInfo.Name)
                        {
                            case "ID":
                            case "Unit":
                                break;
                            case "Intelligence_Date":
                            case "AuditorySkills_Date":
                            case "Vocabulary_Date":
                            case "Vocabulary1_Date":
                            case "Language1_Date":
                            case "Language2_Date":
                            case "Language3_Date":
                                temp.IDname = fldInfo.Name;
                                temp.ThisValue = Convert.ToDateTime(dr[fldInfo.Name].ToString()).AddYears(-1911).ToShortDateString().Remove(0, 1);
                                returnvalue.Add(temp);
                                break;
                            default:
                                string namee = fldInfo.Name;
                                string namee2 = dr[fldInfo.Name].ToString();
                                temp.IDname = fldInfo.Name;
                                temp.ThisValue = dr[fldInfo.Name].ToString();
                                returnvalue.Add(temp);
                                break;
                        }
                    }

                }

                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

        return returnvalue;
    }

    public List<AchievementAssessment> UpdateAchievementAssessment(AchievementAssessment sTemperatureData)
    {
        List<AchievementAssessment> returnValue = new List<AchievementAssessment>();
        AchievementAssessment temp = new AchievementAssessment();
        DataBase Base = new DataBase();
        string strSql = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {

            switch (fldInfo.Name)
            {
                case "CreateFileBy":
                case "CreateFileDate":
                case "ID":
                case "Unit":
                    // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                    break;
                //case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + "=getdate()";
                    // strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    //cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Date).Value = DateTime.Now.ToLongDateString();
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + "=@" + fldInfo.Name;
                    //strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = " update   AchievementAssessment set  " + strSql + " where id=@ID ";
                //sql += " select @@identity as id ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        //case "ID":
                        case "CreateFileDate":
                        case "Unit":
                            // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                            break;

                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                cmd.ExecuteNonQuery();
                temp.ID = sTemperatureData.ID;

                //SqlDataReader dr = cmd.ExecuteReader();
                //if (dr.Read())
                //{
                //    temp.ID = dr["id"].ToString();
                //}

            }
            catch (Exception e)
            {
                //temp.ID = e.Message.ToString();
            }
            Sqlconn.Close();
        }

        //temp.ID = strSql;
        returnValue.Add(temp);
        return returnValue;
    }

    public List<CaseStudy> CreatCaseStudy(CaseStudy sTemperatureData)
    {
        List<CaseStudy> returnValue = new List<CaseStudy>();

        CaseStudy temp = new CaseStudy();
        DataBase Base = new DataBase();
        string strSql = "";
        string strSqlPara = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                case "WriteNameName":
                case "WriteName1Name":
                case "RecordedByName":
                    break;
                case "isDeleted":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "0 ";
                    break;
                case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = " insert into  CaseStudy ( " + strSql + ")values(" + strSqlPara + ") ";
                sql += " select @@identity as id ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        case "ID":
                        case "Unit":
                        case "WriteNameName":
                        case "WriteName1Name":
                        case "RecordedByName":
                            // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                            break;
                        case "CreateFileBy":
                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));

                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    temp.ID = dr["id"].ToString();
                }

            }
            catch (Exception e)
            {
                string ex = e.Message.ToString();
            }
            Sqlconn.Close();
        }
        returnValue.Add(temp);
        return returnValue;
    }

    public List<AchievementAssessmentLoad> ShowCaseStudy(int ID)
    {
        string strSql = "";
        List<AchievementAssessmentLoad> returnvalue = new List<AchievementAssessmentLoad>();
        AchievementAssessmentLoad temp = new AchievementAssessmentLoad();
        CaseStudy sTemperatureData = new CaseStudy();

        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {

            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                case "WriteNameName":
                case "WriteName1Name":
                case "RecordedByName":
                    break;
                case "WriteDate":
                case "RecordedDateTime":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "convert(varchar, isnull(convert(date, a." + fldInfo.Name + ",1),'1912'), 111) as " + fldInfo.Name;
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "a." + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                string sql = "select  b.studentname ,convert(varchar, isnull(convert(date, b.studentbirthday ,1),'1912'), 23) as studentbirthday , c.WriteNameName , d.WriteName1Name,e.RecordedByName , " + strSql + " from CaseStudy a  ";
                sql += " left join ( select staffid as cid , StaffName as WriteNameName from staffDatabase ) c on a.WriteName = c.cid ";
                sql += " left join ( select staffid as did , StaffName as WriteName1Name from staffDatabase ) d on a.WriteName1 = d.did ";
                sql += " left join ( select staffid as eid , StaffName as RecordedByName from staffDatabase ) e on a.RecordedBy = e.eid ";
                sql += " left join studentDatabase b on a.studentid = b.id  where a.ID=@ID and  isnull(a.isDeleted,0) = 0 ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    temp.IDname = "studentName";
                    temp.ThisValue = dr["studentName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "WriteNameName";
                    temp.ThisValue = dr["WriteNameName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "WriteName1Name";
                    temp.ThisValue = dr["WriteName1Name"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "RecordedByName";
                    temp.ThisValue = dr["RecordedByName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "studentbirthday";
                    temp.ThisValue = dr["studentbirthday"].ToString();
                    returnvalue.Add(temp);

                    foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                    {

                        switch (fldInfo.Name)
                        {
                            case "ID":
                            case "Unit":
                            case "WriteNameName":
                            case "WriteName1Name":
                            case "RecordedByName":
                                break;
                            case "WriteDate":
                            case "RecordedDateTime":
                                temp.IDname = fldInfo.Name;
                                temp.ThisValue = Convert.ToDateTime(dr[fldInfo.Name].ToString()).AddYears(-1911).ToShortDateString().Remove(0, 1);
                                returnvalue.Add(temp);
                                break;
                            default:
                                string namee = fldInfo.Name;
                                string namee2 = dr[fldInfo.Name].ToString();
                                temp.IDname = fldInfo.Name;
                                temp.ThisValue = dr[fldInfo.Name].ToString();
                                returnvalue.Add(temp);
                                break;
                        }
                    }

                }

                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

        return returnvalue;
    }

    public List<CaseStudy> UpdateCaseStudy(CaseStudy sTemperatureData)
    {
        List<CaseStudy> returnValue = new List<CaseStudy>();
        CaseStudy temp = new CaseStudy();
        DataBase Base = new DataBase();
        string strSql = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {

            switch (fldInfo.Name)
            {
                case "CreateFileBy":
                case "CreateFileDate":
                case "ID":
                case "Unit":
                case "WriteNameName":
                case "WriteName1Name":
                case "RecordedByName":
                    // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                    break;
                //case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + "=getdate()";
                    // strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    //cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Date).Value = DateTime.Now.ToLongDateString();
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + "=@" + fldInfo.Name;
                    //strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = " update   CaseStudy set  " + strSql + " where id=@ID ";
                //sql += " select @@identity as id ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        //case "ID":
                        case "CreateFileDate":
                        case "Unit":
                            // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                            break;

                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                cmd.ExecuteNonQuery();
                temp.ID = sTemperatureData.ID;
            }
            catch (Exception e)
            {
                string a = e.Message.ToString();
            }
            Sqlconn.Close();
        }
        returnValue.Add(temp);
        return returnValue;
    }

    public List<CaseISPRecord> CreatCaseISPRecord(CaseISPRecord sTemperatureData)
    {
        List<CaseISPRecord> returnValue = new List<CaseISPRecord>();

        CaseISPRecord temp = new CaseISPRecord();
        DataBase Base = new DataBase();
        string strSql = "";
        string strSqlPara = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                    break;
                case "isDeleted":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "0 ";
                    break;
                case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = " insert into  CaseISPRecord ( " + strSql + ")values(" + strSqlPara + ") ";
                sql += " select @@identity as id ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        case "ID":
                        case "Unit":
                            // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                            break;
                        case "CreateFileBy":
                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    temp.ID = dr["id"].ToString();
                }

            }
            catch (Exception e)
            {
                string ex = e.Message.ToString();
            }
            Sqlconn.Close();
        }
        returnValue.Add(temp);
        return returnValue;
    }

    private string SearchCaseISPRecordCondition(SearchCaseISPRecord SearchStructure, int type)
    {
        string ConditionReturn = "";
        string DateBase = "1900-01-01";
        if (SearchStructure.txtstudentName != null)
        {
            ConditionReturn += " AND StudentName like (@StudentName) ";
        }
        if (SearchStructure.txtteacherName != null)
        {
            ConditionReturn += " AND TeacherName like (@TeacherName) ";
        }
        if (SearchStructure.txtConventionDatestart != null && SearchStructure.txtConventionDateend != null && SearchStructure.txtConventionDatestart != DateBase && SearchStructure.txtConventionDateend != DateBase)
        {
            ConditionReturn += " AND ConventionDate BETWEEN (@ConventionDatestart) AND (@ConventionDaterend) ";
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        caseBTFunction();
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

    public string[] SearchCaseISPRecordCount(SearchCaseISPRecord SearchStructure, int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchCaseISPRecordCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM CaseISPRecord a left join studentDatabase b on a.studentid = b.id left join ( select staffid as cid , StaffName as TeacherName from staffDatabase ) c on a.teacherid = c.cid  WHERE isnull(a.isDeleted,0) = 0 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@TeacherName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtteacherName) + "%";
                cmd.Parameters.Add("@ConventionDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtConventionDatestart);
                cmd.Parameters.Add("@ConventionDaterend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtConventionDateend);

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

    public List<ShowCaseISPRecord> SearchCaseISPRecord(int indexpage, SearchCaseISPRecord SearchStructure, int type)
    {

        List<ShowCaseISPRecord> returnValue = new List<ShowCaseISPRecord>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchCaseISPRecordCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT * from (select ROW_NUMBER() OVER (ORDER BY isnull( a.ConventionDate,'') DESC) as RowNum , a.ID ,a.ConventionName, convert(varchar, isnull(convert(date,a.ConventionDate,1),'1912'), 111) as  ConventionDate, b.StudentName ";
                sql += "     , TeacherName,  ParticipantTeacheName,ParticipantSocialWorkerName,ParticipantAudiologistName,ParticipantHeadName   ";
                sql += "     , a.ParticipantParent,  a.ParticipantProfessionals  ";
                sql += " FROM CaseISPRecord a left join studentDatabase b on a.studentid = b.id  ";
                sql += " left join ( select staffid as cid , StaffName as TeacherName from staffDatabase ) c on a.teacherid = c.cid ";
                sql += " left join (select staffid as did , StaffName as ParticipantTeacheName from staffDatabase) d on a.ParticipantTeache = d.did ";
                sql += " left join (select staffid as eid , StaffName as ParticipantSocialWorkerName from staffDatabase) e on a.ParticipantSocialWorker = e.eid ";
                sql += " left join (select staffid as fid , StaffName as ParticipantAudiologistName from staffDatabase) f on a.ParticipantAudiologist = f.fid ";
                sql += " left join (select staffid as gid , StaffName as ParticipantHeadName from staffDatabase) g on a. ParticipantHead = g.gid ";
                sql += " WHERE isnull(a.isDeleted,0) = 0 " + ConditionReturn + ") AS NewTable ";

                sql += " where  RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";



                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@TeacherName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtteacherName) + "%";
                cmd.Parameters.Add("@ConventionDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtConventionDatestart);
                cmd.Parameters.Add("@ConventionDaterend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtConventionDateend);


                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    ShowCaseISPRecord addValue = new ShowCaseISPRecord();
                    addValue.RowNum = dr["rownum"].ToString();
                    
                    addValue.ID = dr["ID"].ToString();
                    addValue.ConventionName = dr["ConventionName"].ToString();
                    addValue.ConventionDate = Convert.ToDateTime(dr["ConventionDate"].ToString()).AddYears(-1911).ToShortDateString().Remove(0, 1);
                    addValue.StudentName = dr["StudentName"].ToString();
                    addValue.TeacherName = dr["TeacherName"].ToString();
                    addValue.ParticipantTeacheName = dr["ParticipantTeacheName"].ToString();
                    addValue.ParticipantSocialWorkerName = dr["ParticipantSocialWorkerName"].ToString();
                    addValue.ParticipantAudiologistName = dr["ParticipantAudiologistName"].ToString();
                    addValue.ParticipantHeadName = dr["ParticipantHeadName"].ToString();
                    addValue.ParticipantParent = dr["ParticipantParent"].ToString();
                    addValue.ParticipantProfessionals = dr["ParticipantProfessionals"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                ShowCaseISPRecord addValue = new ShowCaseISPRecord();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public List<AchievementAssessmentLoad> ShowCaseIspRecord(int ID)
    {
        string strSql = "";
        List<AchievementAssessmentLoad> returnvalue = new List<AchievementAssessmentLoad>();
        AchievementAssessmentLoad temp = new AchievementAssessmentLoad();
        CaseISPRecord sTemperatureData = new CaseISPRecord();

        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {

            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                    break;
                case "ConventionDate":
                case "PlanExecutionFrameDate":
                case "PlanExecutionOverDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "convert(varchar, isnull(convert(date, a." + fldInfo.Name + ",1),'1912'), 111) as " + fldInfo.Name;
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "a." + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                Sqlconn.Open();
                //string sql = "select  b.studentname ,convert(varchar, isnull(convert(date, b.studentbirthday ,1),'1912'), 23) as studentbirthday , " + strSql + " from CaseStudy a left join studentDatabase b on a.studentid = b.id  where a.ID=@ID and  isnull(a.isDeleted,0) = 0 ";
                string sql = "select   b.StudentName , c.TeacherName,  ParticipantTeacheName,ParticipantSocialWorkerName,ParticipantAudiologistName,ParticipantHeadName ,   ";
                sql += strSql;
                sql += " FROM CaseISPRecord a left join  (select id, studentName from studentDatabase) b on a.studentid = b.id  ";
                sql += " left join ( select staffid as cid , StaffName as TeacherName from staffDatabase ) c on a.teacherid = c.cid ";
                sql += " left join (select staffid as did , StaffName as ParticipantTeacheName from staffDatabase) d on a.ParticipantTeache = d.did ";
                sql += " left join (select staffid as eid , StaffName as ParticipantSocialWorkerName from staffDatabase) e on a.ParticipantSocialWorker = e.eid ";
                sql += " left join (select staffid as fid , StaffName as ParticipantAudiologistName from staffDatabase) f on a.ParticipantAudiologist = f.fid ";
                sql += " left join (select staffid as gid , StaffName as ParticipantHeadName from staffDatabase) g on a. ParticipantHead = g.gid ";
                sql += " WHERE isnull(a.isDeleted,0) = 0 and a.ID = @ID ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = ID;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    temp.IDname = "studentName";
                    temp.ThisValue = dr["studentName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "ParticipantTeacheName";
                    temp.ThisValue = dr["ParticipantTeacheName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "teacherName";
                    temp.ThisValue = dr["TeacherName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "ParticipantSocialWorkerName";
                    temp.ThisValue = dr["ParticipantSocialWorkerName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "ParticipantAudiologistName";
                    temp.ThisValue = dr["ParticipantAudiologistName"].ToString();
                    returnvalue.Add(temp);
                    temp.IDname = "ParticipantHeadName";
                    temp.ThisValue = dr["ParticipantHeadName"].ToString();
                    returnvalue.Add(temp);



                    foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                    {

                        switch (fldInfo.Name)
                        {
                            case "ID":
                            case "Unit":
                                break;
                            case "ConventionDate":
                            case "PlanExecutionFrameDate":
                            case "PlanExecutionOverDate":
                                temp.IDname = fldInfo.Name;
                                temp.ThisValue = Convert.ToDateTime(dr[fldInfo.Name].ToString()).AddYears(-1911).ToShortDateString().Remove(0, 1);
                                returnvalue.Add(temp);
                                break;
                            default:
                                string namee = fldInfo.Name;
                                string namee2 = dr[fldInfo.Name].ToString();
                                temp.IDname = fldInfo.Name;
                                temp.ThisValue = dr[fldInfo.Name].ToString();
                                returnvalue.Add(temp);
                                break;
                        }
                    }

                }

                dr.Close();
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string error = e.Message;
            }
        }

        return returnvalue;
    }

    public List<CaseISPRecord> UpdateCaseISPRecord(CaseISPRecord sTemperatureData)
    {
        List<CaseISPRecord> returnValue = new List<CaseISPRecord>();
        CaseISPRecord temp = new CaseISPRecord();
        DataBase Base = new DataBase();
        string strSql = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {

            switch (fldInfo.Name)
            {
                case "CreateFileBy":
                case "CreateFileDate":
                case "ID":
                case "Unit":
                    // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                    break;
                //case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + "=getdate()";
                    // strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    //cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Date).Value = DateTime.Now.ToLongDateString();
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + "=@" + fldInfo.Name;
                    //strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }

        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                Sqlconn.Open();
                string sql = " update   CaseISPRecord set  " + strSql + " where id=@ID ";
                //sql += " select @@identity as id ";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        //case "ID":
                        case "CreateFileDate":
                        case "Unit":
                            // strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = " + fldInfo.GetValue(sTemperatureData);
                            break;

                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                cmd.ExecuteNonQuery();
                temp.ID = sTemperatureData.ID;
            }
            catch (Exception e)
            {
                string a = e.Message.ToString();
            }
            Sqlconn.Close();
        }
        returnValue.Add(temp);
        return returnValue;
    }

    public string[] CreatVoiceDistanceDatabase(List<VoiceDistance> sTemperatureData)
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
                string sql = "  DECLARE @ID int;  DECLARE @MasterID int; insert into VoiceDistance (studentID,AcademicYear,AcademicTerm,studentAge,studentMonth) values( @studentID , @AcademicYear,@AcademicTerm,@StudentAge,@StudentMonth) ";
                sql += "  select @ID = (select @@identity) ";
                sql += " insert into VoiceDistanceMaster ( VDid,date,remark ,ListOrder,up1,up2 ,up3 ,up4,up5 ) values( @ID,@date,@remark ,@ListOrder,@up1,@up2 ,@up3 ,@up4,@up5) ";
                sql += "  select @MasterID = (select @@identity) ";
                for (int i = 0; i < sTemperatureData.Count; i++) {
                    sql += "insert into VoiceDistanceDetail (VDMid,Question,A1,A2,A3,A4,A5,Rows)values(@MasterID,@Question" + i.ToString() + ",@A1" + i.ToString() + ",@A2" + i.ToString() + ",@A3" + i.ToString() + ",@A4" + i.ToString() + ",@A5" + i.ToString() + ","+(i+1).ToString()+")";
                }
                sql += " select @ID as ID ";
                Sqlconn.Open();
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData[0].StudentID.ToString());
                cmd.Parameters.Add("@AcademicYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[0].AcademicYear.ToString());
                cmd.Parameters.Add("@AcademicTerm", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[0].AcademicTerm.ToString());
                cmd.Parameters.Add("@StudentAge", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData[0].StudentAge.ToString());
                cmd.Parameters.Add("@StudentMonth", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData[0].StudentMonth.ToString());
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction( sTemperatureData[0].Date.AddYears(1911).AddDays(-1).ToShortDateString());
                cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[0].remark.ToString());
                cmd.Parameters.Add("@ListOrder", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(sTemperatureData[0].ListOrder.ToString());
                for (int i = 0; i < sTemperatureData[0].up.Split('|').Length; i++)
                {
                    cmd.Parameters.Add("@up" + (i + 1).ToString(), SqlDbType.NVarChar).Value = Chk.CheckStringFunction( sTemperatureData[0].up.Split('|').GetValue(i).ToString());
                }
                for (int i = 0; i < sTemperatureData.Count; i++)
                {
                    cmd.Parameters.Add("@Question" + i.ToString(), SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[i].Question.ToString());
                    for (int j = 0; j < sTemperatureData[0].Anser.Split('|').Length; j++)
                    {
                        cmd.Parameters.Add("@A" + (j + 1).ToString() + i.ToString(), SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(sTemperatureData[i].Anser.Split('|').GetValue(j).ToString());
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                   returnValue[0] = dr["ID"].ToString();
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


    private string SearchVoiceDistanceCondition(SearchStudent SearchStructure, int type)
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
        if (SearchStructure.txtAcademicYearstart != null && SearchStructure.txtAcademicYearend != null && SearchStructure.txtAcademicYearstart != DateBase && SearchStructure.txtAcademicYearend != DateBase)
        {
            ConditionReturn += " AND AcademicYear BETWEEN (@AcademicYearstart) AND (@AcademicYearend) "; // 教學管理使用 不知是否會與其他衝突
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        caseBTFunction();
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

    public string[] SearchVoiceDistanceCount(SearchStudent SearchStructure, int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchVoiceDistanceCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM VoiceDistance a left join studentDatabase b on a.studentid = b.id   WHERE 1=1 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
             
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);

                cmd.Parameters.Add("@AcademicYearstart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearstart);
                cmd.Parameters.Add("@AcademicYearend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearend);

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

    public List<VoiceDistance> SearchVoiceDistance(int indexpage, SearchStudent SearchStructure, int type)
    {

        List<VoiceDistance> returnValue = new List<VoiceDistance>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchVoiceDistanceCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT * from (select ROW_NUMBER() OVER (ORDER BY isnull( a.AcademicYear,'') DESC) as RowNum ";
                sql += " ,a.id, a.AcademicYear,a.AcademicTerm ,a.StudentAge,a.StudentMonth , b.StudentName , b.StudentBirthday ";
                sql += " FROM VoiceDistance a left join studentDatabase b on a.studentid = b.id  ";
                sql += " WHERE 1=1 " + ConditionReturn + ") AS NewTable ";

                sql += " where  RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";



                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);

                cmd.Parameters.Add("@AcademicYearstart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearstart);
                cmd.Parameters.Add("@AcademicYearend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearend);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    VoiceDistance addValue = new VoiceDistance();
                    addValue.RowNum = dr["rownum"].ToString();
                    addValue.ID = dr["ID"].ToString();
                    addValue.StudentName = dr["StudentName"].ToString();
                    addValue.AcademicYear = dr["AcademicYear"].ToString();
                    addValue.AcademicTerm = dr["AcademicTerm"].ToString();
                    addValue.StudentAge = dr["StudentAge"].ToString();
                    addValue.StudentMonth = dr["StudentMonth"].ToString();
                    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                VoiceDistance addValue = new VoiceDistance();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] GetVoiceDistanceCount( int ID)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM VoiceDistanceMaster   WHERE 1=1  and VDid = @ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ID.ToString());
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

    public List<VoiceDistance> showVoiceDistance(int ID)
    {
        List<VoiceDistance> returnValue = new List<VoiceDistance>();
        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT a.id, a.studentid,a.AcademicYear,a.AcademicTerm,a.StudentAge,a.StudentMonth ,b.date,b.remark,b.listOrder , b.up1,b.up2,b.up3,b.up4,b.up5  ";
                sql += " ,c.id as HidID, c.question, c.a1 ,c.a2,c.a3,c.a4,c.a5,c.rows as RowNum , d.studentname ,d.studentbirthday ";
                sql += " from VoiceDistance a  ";
                sql += " left join VoiceDistanceMaster b on a.id = b.VDid ";
                sql += " left join VoiceDistanceDetail c on b.id = c.VDMid ";
                sql += " left join (select Studentname,studentbirthday,ID as did from StudentDatabase) d on a.studentid = d.did ";
                sql += " WHERE 1=1 and a.id=@ID ";
                sql += " order by c.id , c.rows ";




                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(ID.ToString());

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    VoiceDistance addValue = new VoiceDistance();
                    addValue.RowNum = dr["RowNum"].ToString();
                    addValue.ID = dr["ID"].ToString();
                    addValue.StudentName = dr["StudentName"].ToString();
                    addValue.StudentID = dr["StudentID"].ToString();
                    addValue.AcademicYear = dr["AcademicYear"].ToString();
                    addValue.AcademicTerm = dr["AcademicTerm"].ToString();
                    addValue.StudentAge = dr["StudentAge"].ToString();
                    addValue.StudentMonth = dr["StudentMonth"].ToString();
                    addValue.ListOrder = dr["listorder"].ToString();
                    addValue.Date = Convert.ToDateTime(dr["date"].ToString()).AddYears(-1911); ;
                    addValue.remark = dr["remark"].ToString();
                    addValue.up = dr["up1"].ToString() + "|" + dr["up2"].ToString() + "|" + dr["up3"].ToString() + "|" + dr["up4"].ToString() + "|" + dr["up5"].ToString();
                    addValue.HidID = dr["hidID"].ToString();
                    addValue.Question = dr["question"].ToString();
                    addValue.Anser = dr["a1"].ToString() + "|" + dr["a2"].ToString() + "|" + dr["a3"].ToString() + "|" + dr["a4"].ToString() + "|" + dr["a5"].ToString();
                    addValue.txtstudentbirthday = Convert.ToDateTime(dr["studentbirthday"].ToString());
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                VoiceDistance addValue = new VoiceDistance();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }

        return returnValue;
    }

    public string[] InsertVoiceDistance(List<VoiceDistance> sTemperatureData)
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
                string sql = " DECLARE @MasterID int; insert into VoiceDistanceMaster ( VDid,date,remark ,ListOrder,up1,up2 ,up3 ,up4,up5 ) values( @VDID,@date,@remark ,@ListOrder,@up1,@up2 ,@up3 ,@up4,@up5) ";
                sql += "  select @MasterID = (select @@identity) ";
                for (int i = 0; i < sTemperatureData.Count; i++)
                {
                    sql += "insert into VoiceDistanceDetail (VDMid,Question,A1,A2,A3,A4,A5,Rows)values(@MasterID,@Question" + i.ToString() + ",@A1" + i.ToString() + ",@A2" + i.ToString() + ",@A3" + i.ToString() + ",@A4" + i.ToString() + ",@A5" + i.ToString() + "," + (i + 1).ToString() + ")";
                }
               // sql += " select @ID as ID ";
                Sqlconn.Open();
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@date", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(sTemperatureData[0].Date.AddYears(1911).ToShortDateString());
                cmd.Parameters.Add("@remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[0].remark.ToString());
                cmd.Parameters.Add("@ListOrder", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(sTemperatureData[0].ListOrder.ToString());
                cmd.Parameters.Add("@VDID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData[0].ID.ToString());
                for (int i = 0; i < sTemperatureData[0].up.Split('|').Length; i++)
                {
                    cmd.Parameters.Add("@up" + (i + 1).ToString(), SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[0].up.Split('|').GetValue(i).ToString());
                }
                for (int i = 0; i < sTemperatureData.Count; i++)
                {
                    cmd.Parameters.Add("@Question" + i.ToString(), SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[i].Question.ToString());
                    for (int j = 0; j < sTemperatureData[0].Anser.Split('|').Length; j++)
                    {
                        cmd.Parameters.Add("@A" + (j + 1).ToString() + i.ToString(), SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(sTemperatureData[i].Anser.Split('|').GetValue(j).ToString());
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    returnValue[0] = dr["ID"].ToString();
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

    public string[] UpdateVoiceDistance(List<VoiceDistance> sTemperatureData)
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
                string sql = " Update VoiceDistance set AcademicYear = @AcademicYear ,AcademicTerm = @AcademicTerm where id = @VDID ";
                for (int i = 0; i < sTemperatureData.Count; i++)
                {
                    sql += " Update VoiceDistanceMaster set date = @" + i.ToString() + "date , remark =@" + i.ToString() + "remark ,up1 = @" + i.ToString() + "up1,up2=@" + i.ToString() + "up2,up3=@" + i.ToString() + "up3 , up4 =@" + i.ToString() + "up4 , up5 =@" + i.ToString() + "up5 where VDid = @VDID and ListOrder =@" + i.ToString() + "ListOrder ";
                    sql += " Update VoiceDistanceDetail set Question = @" + i.ToString() + "Question , A1 = @" + i.ToString() + "A1,A2=@" + i.ToString() + "A2,A3=@" + i.ToString() + "A3,A4=@" + i.ToString() + "A4,A5 = @" + i.ToString() + "A5 where ID = @" + i.ToString() + "HidID";
                }

                Sqlconn.Open();
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@AcademicYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[0].AcademicYear.ToString());
                cmd.Parameters.Add("@AcademicTerm", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[0].AcademicTerm.ToString());
                cmd.Parameters.Add("@VDID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData[0].ID.ToString());
                for (int i = 0; i < sTemperatureData.Count; i++)
                {
                    cmd.Parameters.Add("@" + i.ToString() + "date", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(sTemperatureData[i].Date.AddYears(1911).AddDays(-1).ToShortDateString());
                    cmd.Parameters.Add("@" + i.ToString() + "remark", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[i].remark.ToString());
                    cmd.Parameters.Add("@" + i.ToString() + "ListOrder", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(sTemperatureData[i].ListOrder.ToString());
                    cmd.Parameters.Add("@" + i.ToString() + "Question", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[i].Question.ToString());
                    cmd.Parameters.Add("@" + i.ToString() + "HidID", SqlDbType.Int).Value = Chk.CheckStringtoInt64Function(sTemperatureData[i].HidID.ToString());
                    for (int up = 0; up < sTemperatureData[i].up.Split('|').Length; up++)
                    {
                        cmd.Parameters.Add("@" + i.ToString() + "up" + (up + 1).ToString(), SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData[i].up.Split('|').GetValue(up).ToString());
                    }
                    for (int j = 0; j < sTemperatureData[i].Anser.Split('|').Length; j++)
                    {
                        cmd.Parameters.Add("@" + i.ToString() + "A" + (j + 1).ToString(), SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(sTemperatureData[i].Anser.Split('|').GetValue(j).ToString());
                    }
                }
                int dr = cmd.ExecuteNonQuery();
                if (dr > 0)
                {
                    returnValue[0] = sTemperatureData[0].ID.ToString();
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


    public string[] CreatTeachServiceSupervisor(TeachServiceSupervisor sTemperatureData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        string strSql = "";
        string strSqlPara = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                case "RowNum":
                case "TeacherName":
                case "StudentName":
                case "txtstudentbirthday":
                case "checkNo":
                case "errorMsg":
                    break;
                case "isDeleted":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "0 ";
                    break;
                case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = " insert into  TeachServiceSupervisor ( " + strSql + " )values( " + strSqlPara + ")  select @@identity as ID ";
       

                Sqlconn.Open();
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                //cmd.Parameters.Add("@AcademicYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData.AcademicYear.ToString());
                //cmd.Parameters.Add("@VDID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData.ID.ToString());
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        case "ID":
                        case "Unit":
                        case "RowNum":
                        case "TeacherName":
                        case "StudentName":
                        case "txtstudentbirthday":
                        case "checkNo":
                        case "errorMsg":
                            break;
                        case "CreateFileBy":
                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        case "ClassDate":
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, Convert.ToDateTime( fldInfo.GetValue(sTemperatureData).ToString()).AddYears(1911).ToShortDateString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    returnValue[0] = dr["ID"].ToString();
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



    private string SearchTeachServiceSupervisorCondition(SearchStudent SearchStructure, int type)
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
        if (SearchStructure.txtAcademicYearstart != null && SearchStructure.txtAcademicYearend != null && SearchStructure.txtAcademicYearstart != DateBase && SearchStructure.txtAcademicYearend != DateBase)
        {
            ConditionReturn += " AND AcademicYear BETWEEN (@AcademicYearstart) AND (@AcademicYearend) "; // 教學管理使用 不知是否會與其他衝突
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        caseBTFunction();
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

    public string[] SearchTeachServiceSupervisorCount(SearchStudent SearchStructure, int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchTeachServiceSupervisorCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM TeachServiceSupervisor a left join studentDatabase b on a.studentid = b.id   WHERE 1=1 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);

                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);

                cmd.Parameters.Add("@AcademicYearstart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearstart);
                cmd.Parameters.Add("@AcademicYearend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearend);

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

    public List<TeachServiceSupervisor> SearchTeachServiceSupervisor(int indexpage, SearchStudent SearchStructure, int type)
    {

        List<TeachServiceSupervisor> returnValue = new List<TeachServiceSupervisor>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchVoiceDistanceCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT * from (select ROW_NUMBER() OVER (ORDER BY isnull( a.AcademicYear,'') DESC) as RowNum ";
                sql += " ,a.id, a.AcademicYear,  convert(varchar, isnull(convert(date,  a.ClassDate,1),'1912'), 111) as   ClassDate,a.StudentAge,a.StudentMonth , b.StudentName , b.StudentBirthday ";
                sql += " FROM TeachServiceSupervisor a left join studentDatabase b on a.studentid = b.id  ";
                sql += " WHERE 1=1 " + ConditionReturn + ") AS NewTable ";

                sql += " where  RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";



                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);

                cmd.Parameters.Add("@AcademicYearstart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearstart);
                cmd.Parameters.Add("@AcademicYearend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtAcademicYearend);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TeachServiceSupervisor addValue = new TeachServiceSupervisor();
                    addValue.RowNum = dr["rownum"].ToString();
                    addValue.ID = dr["ID"].ToString();
                    addValue.ClassDate = dr["classDate"].ToString();
                    addValue.StudentName = dr["StudentName"].ToString();
                    addValue.AcademicYear = dr["AcademicYear"].ToString();
                    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    addValue.StudentAge = dr["studentAge"].ToString();
                    addValue.StudentMonth = dr["studentMonth"].ToString();
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                TeachServiceSupervisor addValue = new TeachServiceSupervisor();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public List<AchievementAssessmentLoad> ShowTeachServiceSupervisor(int id)
    {
        List<AchievementAssessmentLoad> returnValue = new List<AchievementAssessmentLoad>();
        string strSql = "";
        TeachServiceSupervisor sTemperatureData = new TeachServiceSupervisor();
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {

                case "ID":
                case "Unit":
                case "RowNum":
                case "TeacherName":
                case "StudentName":
                case "txtstudentbirthday":
                case "checkNo":
                case "errorMsg":
                case "isDeleted":
                case "CreateFileDate":
                case "CreateFileBy":
                case "UpFileBy":
                case "UpFileDate":
                    break;
                case "ClassDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "convert(varchar, isnull(convert(date, a." + fldInfo.Name + ",1),'1912'), 111) as " + fldInfo.Name;
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "a." + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT  " + strSql + " , b.StudentName ,c.TeacherName ";
                sql += " from TeachServiceSupervisor a ";
                sql += " left join  (select id, studentName from studentDatabase) b on a.studentid = b.id  ";
                sql += " left join ( select staffid as cid , StaffName as TeacherName from staffDatabase ) c on a.teacherid = c.cid ";
                sql += " where 1=1 and a.ID = @ID ";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AchievementAssessmentLoad addValue = new AchievementAssessmentLoad();

                    addValue.IDname = "studentName";
                    addValue.ThisValue = dr["studentName"].ToString();
                    returnValue.Add(addValue);
                    addValue.IDname = "teacherName";
                    addValue.ThisValue = dr["TeacherName"].ToString();
                    returnValue.Add(addValue);
  


                    foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                    {


                        switch (fldInfo.Name)
                        {
                            case "ID":
                            case "Unit":
                            case "RowNum":
                            case "TeacherName":
                            case "StudentName":
                            case "txtstudentbirthday":
                            case "checkNo":
                            case "errorMsg":
                            case "isDeleted":
                            case "CreateFileDate":
                            case "CreateFileBy":
                            case "UpFileBy":
                            case "UpFileDate":
                                break;
                            case "ClassDate":
                                addValue.IDname = fldInfo.Name;
                                addValue.ThisValue = Convert.ToDateTime(dr[fldInfo.Name].ToString()).AddYears(-1911).ToShortDateString().Remove(0, 1);
                                returnValue.Add(addValue);
                                break;
                            default:
                                addValue.IDname = fldInfo.Name;
                                addValue.ThisValue = dr[fldInfo.Name].ToString();
                                returnValue.Add(addValue);
                                break;
                        }
                    }
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                //AchievementAssessmentLoad addValue = new AchievementAssessmentLoad();
                ////addValue.checkNo = "-1";
                ////addValue.errorMsg = e.Message.ToString();
                string ee = e.Message.ToString();
                //returnValue.Add(addValue);
            }
        }
        return returnValue;
    }
    public string[] UpdateTeachServiceSupervisor(TeachServiceSupervisor sTemperatureData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        string strSql = "";
        //string strSqlPara = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                case "RowNum":
                case "TeacherName":
                case "StudentName":
                case "txtstudentbirthday":
                case "checkNo":
                case "errorMsg":
                case "isDeleted":
                case "CreateFileDate":
                case "CreateFileBy":
                case "StudentAge":
                case "StudentMonth":
                case "DirectorName":    
                    break;
                case "UpFileDate":
                    //strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = getdate() " ;
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + "=@" + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = " update  TeachServiceSupervisor set " + strSql  + " where ID = @ID ";
                sql += " select id from TeachServiceSupervisor where ID = @ID ";
       

                Sqlconn.Open();
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData.ID.ToString());
                //cmd.Parameters.Add("@VDID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData.ID.ToString());
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        case "ID":
                        case "Unit":
                        case "RowNum":
                        case "TeacherName":
                        case "StudentName":
                        case "txtstudentbirthday":
                        case "checkNo":
                        case "errorMsg":
                        case "isDeleted":
                        case "CreateFileDate":
                        case "CreateFileBy":
                        case "StudentAge":
                        case "StudentMonth":
                        case "DirectorName":    
                            break;
                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        case "ClassDate":
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, Convert.ToDateTime( fldInfo.GetValue(sTemperatureData).ToString()).AddYears(1911).ToShortDateString());
                            }
                            else
                            {
                                //cmd.Parameters.Add("@" + fldInfo.Name, System.DBNull..Value);
                                cmd.Parameters.Add("@" + fldInfo.Name, "");

                            }
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                //cmd.Parameters.Add("@" + fldInfo.Name, System.DBNull..Value);
                                cmd.Parameters.Add("@" + fldInfo.Name, "");

                            }
                            break;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    returnValue[0] = dr["ID"].ToString();
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


    public string[] CreateTeachServiceInspect(TeachServiceInspect sTemperatureData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        string strSql = "";
        string strSqlPara = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                case "RowNum":
                case "TeacherName":
                case "StudentName":
                case "txtstudentbirthday":
                case "checkNo":
                case "errorMsg":
                case "ClassIDName":
                case "DirectorName":
                    break;
                case "isDeleted":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "0 ";
                    break;
                case "CreateFileDate":
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name;
                    strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = " insert into  TeachServiceInspect ( " + strSql + " )values( " + strSqlPara + ")  select @@identity as ID ";


                Sqlconn.Open();
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                //cmd.Parameters.Add("@AcademicYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData.AcademicYear.ToString());
                //cmd.Parameters.Add("@VDID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData.ID.ToString());
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        case "ID":
                        case "Unit":
                        case "RowNum":
                        case "TeacherName":
                        case "StudentName":
                        case "txtstudentbirthday":
                        case "checkNo":
                        case "errorMsg":
                        case "ClassIDName":
                        case "DirectorName":
                            break;
                        case "CreateFileBy":
                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        case "ClassDate":
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, Convert.ToDateTime(fldInfo.GetValue(sTemperatureData).ToString()).AddYears(1911).ToShortDateString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    returnValue[0] = dr["ID"].ToString();
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


    private string SearchTeachServiceInspectCondition(SearchStudent SearchStructure, int type)
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

        if (SearchStructure.txtendReasonDatestart != null && SearchStructure.txtendReasonDateend != null && SearchStructure.txtendReasonDatestart != DateBase && SearchStructure.txtendReasonDateend != DateBase)
        {
            ConditionReturn += " AND InspectDate BETWEEN (@InspectDatestart) AND (@InspectDateend) "; 
        }

        if (SearchStructure.txtAcademicYearstart != null && SearchStructure.txtAcademicYearend != null )
        {
            ConditionReturn += " AND AcademicYear BETWEEN (@AcademicYearstart) AND (@AcademicYearend) "; 
        }
        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        caseBTFunction();
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

    public string[] SearchTeachServiceInspectCount(SearchStudent SearchStructure, int type)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchTeachServiceInspectCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM TeachServiceInspect a left join studentDatabase b on a.studentid = b.id   WHERE 1=1 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);

                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);

                cmd.Parameters.Add("@InspectDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDatestart);
                cmd.Parameters.Add("@InspectDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDateend);

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

    public List<TeachServiceInspect> SearchTeachServiceInspect(int indexpage, SearchStudent SearchStructure, int type)
    {

        List<TeachServiceInspect> returnValue = new List<TeachServiceInspect>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchTeachServiceInspectCondition(SearchStructure, type);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT * from (select ROW_NUMBER() OVER (ORDER BY isnull( a.AcademicYear,'') DESC) as RowNum ";
                sql += " ,a.id, a.AcademicYear,  convert(varchar, isnull(convert(date,  a.InspectDate,1),'1912'), 111) as   InspectDate,a.ClassType , b.StudentName , b.StudentBirthday , c.TeacherName ,d.ClassIDName ";
                sql += " FROM TeachServiceInspect a left join studentDatabase b on a.studentid = b.id  ";
                sql += " left join (select staffid as cid , staffname as TeacherName from staffdatabase ) c on a.teacherid = c.cid ";
                sql += " left join (select ClassIDName , ClassID as did from ClassName ) d on a.ClassNameID = d.did "; 
                sql += " WHERE 1=1 " + ConditionReturn + ") AS NewTable ";

                sql += " where  RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";



                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@StudentID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstudentID);
                cmd.Parameters.Add("@StudentName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstudentName) + "%";
                cmd.Parameters.Add("@StudentSex", SqlDbType.TinyInt).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtstudentSex);
                cmd.Parameters.Add("@sBirthdayStart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdaystart);
                cmd.Parameters.Add("@sBirthdayEnd", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtbirthdayend);

                cmd.Parameters.Add("@InspectDatestart", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDatestart);
                cmd.Parameters.Add("@InspectDateend", SqlDbType.Date).Value = Chk.CheckStringtoDateFunction(SearchStructure.txtendReasonDateend);

                cmd.Parameters.Add("@AcademicYearstart", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtAcademicYearstart);
                cmd.Parameters.Add("@AcademicYearend", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(SearchStructure.txtAcademicYearend);

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    TeachServiceInspect addValue = new TeachServiceInspect();
                    addValue.RowNum = dr["rownum"].ToString();
                    addValue.ID = dr["ID"].ToString();
                    addValue.ClassType = dr["ClassType"].ToString();
                    addValue.InspectDate = dr["InspectDate"].ToString();
                    addValue.StudentName = dr["StudentName"].ToString();
                    addValue.TeacherName = dr["TeacherName"].ToString();
                    addValue.AcademicYear = dr["AcademicYear"].ToString();
                    addValue.ClassIDName = dr["ClassIDName"].ToString();
                    //if (dr["ClassType"].ToString() == 1)
                    //{
                    //    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    //}
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                TeachServiceInspect addValue = new TeachServiceInspect();
                addValue.checkNo = "-1";
                addValue.errorMsg = e.Message.ToString();
                returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public List<AchievementAssessmentLoad> ShowTeachServiceInspect(int id)
    {
        List<AchievementAssessmentLoad> returnValue = new List<AchievementAssessmentLoad>();
        string strSql = "";
        TeachServiceInspect sTemperatureData = new TeachServiceInspect();
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {

                case "ID":
                case "Unit":
                case "RowNum":
                case "TeacherName":
                case "StudentName":
                case "txtstudentbirthday":
                case "checkNo":
                case "errorMsg":
                case "isDeleted":
                case "CreateFileDate":
                case "CreateFileBy":
                case "UpFileBy":
                case "UpFileDate":
                case "ClassIDName":
                case "DirectorName":    
                    break;
                case "ClassDate":
                case "InspectDate":
                case "Date":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "convert(varchar, isnull(convert(date, a." + fldInfo.Name + ",1),'1912'), 111) as " + fldInfo.Name;
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + "a." + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT  " + strSql + " , b.StudentName ,c.TeacherName  ,d.ClassIDName ,e.DirectorName ";
                sql += " from TeachServiceInspect a ";
                sql += " left join  (select id, studentName from studentDatabase) b on a.studentid = b.id  ";
                sql += " left join ( select staffid as cid , StaffName as TeacherName from staffDatabase ) c on a.teacherid = c.cid ";
                sql += " left join (select ClassIDName , ClassID as did from ClassName ) d on a.ClassNameID = d.did ";
                sql += " left join ( select   staffid as eid , StaffName as DirectorName from staffDatabase ) e on a.Director = e.eid "; //DirectorName
                sql += " where 1=1 and a.ID = @ID ";

                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", id);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    AchievementAssessmentLoad addValue = new AchievementAssessmentLoad();
                    addValue.IDname = "ClassType";
                    addValue.ThisValue = dr["ClassType"].ToString();
                    returnValue.Add(addValue);
                    addValue.IDname = "studentName";
                    addValue.ThisValue = dr["studentName"].ToString();
                    returnValue.Add(addValue);
                    addValue.IDname = "teacherName";
                    addValue.ThisValue = dr["TeacherName"].ToString();
                    returnValue.Add(addValue);
                    addValue.IDname = "DirectorName";
                    addValue.ThisValue = dr["DirectorName"].ToString();
                    returnValue.Add(addValue);
                    addValue.IDname = "ClassName";
                    addValue.ThisValue = dr["ClassIDName"].ToString();
                    returnValue.Add(addValue);


                    foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                    {


                        switch (fldInfo.Name)
                        {
                            case "ID":
                            case "Unit":
                            case "RowNum":
                            case "TeacherName":
                            case "StudentName":
                            case "txtstudentbirthday":
                            case "checkNo":
                            case "errorMsg":
                            case "isDeleted":
                            case "CreateFileDate":
                            case "CreateFileBy":
                            case "UpFileBy":
                            case "UpFileDate":
                            case "ClassType":
                            case "ClassIDName":
                            case "DirectorName":    
                                break;
                            case "InspectDate":
                            case "Date":
                            case "ClassDate":
                                addValue.IDname = fldInfo.Name;
                                addValue.ThisValue = Convert.ToDateTime(dr[fldInfo.Name].ToString()).AddYears(-1911).AddDays(-1).ToShortDateString().Remove(0, 1);
                                returnValue.Add(addValue);
                                break;
                            default:
                                addValue.IDname = fldInfo.Name;
                                addValue.ThisValue = dr[fldInfo.Name].ToString();
                                returnValue.Add(addValue);
                                break;
                        }
                    }
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                //AchievementAssessmentLoad addValue = new AchievementAssessmentLoad();
                ////addValue.checkNo = "-1";
                ////addValue.errorMsg = e.Message.ToString();
                string ee = e.Message.ToString();
                //returnValue.Add(addValue);
            }
        }
        return returnValue;
    }

    public string[] UpdateTeachServiceInspect(TeachServiceInspect sTemperatureData)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        string strSql = "";
       // string strSqlPara = "";
        foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
        {
            switch (fldInfo.Name)
            {
                case "ID":
                case "Unit":
                case "RowNum":
                case "TeacherName":
                case "StudentName":
                case "txtstudentbirthday":
                case "checkNo":
                case "errorMsg":
                case "ClassIDName":
                case "isDeleted":
                case "CreateFileDate":
                case "CreateFileBy":
                case "DirectorName":    
                    break;
                case "UpFileDate":
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " = getdate() "; ;
                   // strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "" : ",") + "getdate() ";
                    break;
                default:
                    strSql += (string.IsNullOrEmpty(strSql) ? "" : ",") + fldInfo.Name + " =@" + fldInfo.Name;
                 //   strSqlPara += (string.IsNullOrEmpty(strSqlPara) ? "@" : ",@") + fldInfo.Name;
                    break;
            }
        }


        DataBase Base = new DataBase();
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                StaffDataBase sDB = new StaffDataBase();
                List<string> CreateFileName = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
                string sql = " update TeachServiceInspect set  " + strSql + "  where ID = @ID ";
                sql += " select id from TeachServiceInspect where id = @ID ";


                Sqlconn.Open();
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                //cmd.Parameters.Add("@AcademicYear", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(sTemperatureData.AcademicYear.ToString());
                //cmd.Parameters.Add("@VDID", SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(sTemperatureData.ID.ToString());
                foreach (System.Reflection.FieldInfo fldInfo in sTemperatureData.GetType().GetFields())
                {

                    switch (fldInfo.Name)
                    {
                        //case "ID":
                        case "Unit":
                        case "RowNum":
                        case "TeacherName":
                        case "StudentName":
                        case "txtstudentbirthday":
                        case "checkNo":
                        case "errorMsg":
                        case "ClassIDName":
                        case "isDeleted":
                        case "CreateFileDate":
                        case "CreateFileBy":
                        case "DirectorName":    
                            break;
                        case "UpFileBy":
                            //cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData));
                            cmd.Parameters.Add("@" + fldInfo.Name, SqlDbType.Int).Value = Chk.CheckStringtoIntFunction(CreateFileName[0]);
                            break;
                        case "ClassDate":
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, Convert.ToDateTime(fldInfo.GetValue(sTemperatureData).ToString()).AddYears(1911).ToShortDateString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                        default:
                            if (fldInfo.GetValue(sTemperatureData) != null)
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, fldInfo.GetValue(sTemperatureData).ToString());
                            }
                            else
                            {
                                cmd.Parameters.Add("@" + fldInfo.Name, "");
                            }
                            break;
                    }
                }
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    returnValue[0] = dr["ID"].ToString();
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




    private string SearchClassNameCondition(SearchClassName SearchStructure)
    {
        string ConditionReturn = "";
        if (SearchStructure.txtstaffID != null)
        {
            ConditionReturn += " AND TeacherID=(@txtstaffID) ";
        }
        if (SearchStructure.txtstaffName != null)
        {
            ConditionReturn += " AND TeacherName like (@txtstaffName) ";
        }
        if (SearchStructure.txtClassID != null)
        {
            ConditionReturn += " AND ClassID=(@txtClassID) ";
        }
        if (SearchStructure.txtClassName != null)
        {
            ConditionReturn += " AND ClassName like (@txtClassName) ";
        }


        StaffDataBase sDB = new StaffDataBase();
        List<string> UserFile = sDB.getStaffDataName(HttpContext.Current.User.Identity.Name);
        caseBTFunction();
        if (int.Parse(_StaffhaveRoles[4]) == 0 && UserFile[1].Length > 0)
        {
            ConditionReturn += " AND a.Unit =" + UserFile[2] + " ";
        }
        return ConditionReturn;
    }

    public string[] SearchClassNameCountCase(SearchClassName SearchStructure)
    {
        string[] returnValue = new string[2];
        returnValue[0] = "0";
        returnValue[1] = "0";
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchClassNameCondition(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = "SELECT COUNT(*) AS QCOUNT FROM ClassName a left join (select staffid as bid , StaffName as TeacherName from staffDatabase )  b on a.Teacherid = b.bid   WHERE 1=1 " + ConditionReturn;
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@txtstaffID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstaffID);
                cmd.Parameters.Add("@txtstaffName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstaffName) + "%";

                cmd.Parameters.Add("@txtClassID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtClassID);
                cmd.Parameters.Add("@txtClassName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtClassName) + "%";
         

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

    public List<SearchClassName> SearchClassNameDataBaseCase(int indexpage, SearchClassName SearchStructure)
    {

        List<SearchClassName> returnValue = new List<SearchClassName>();
        DataBase Base = new DataBase();
        string ConditionReturn = this.SearchClassNameCondition(SearchStructure);
        using (SqlConnection Sqlconn = new SqlConnection(Base.GetConnString()))
        {
            try
            {
                Sqlconn.Open();
                string sql = " SELECT * from   (select ROW_NUMBER() OVER (ORDER BY isnull( a.ClassID,'') DESC) as RowNum , * ";
                sql += " FROM classname a  ";
                sql += " left join (select staffid as bid , StaffName as TeacherName from staffDatabase )  b on a.Teacherid = b.bid ";
                sql += " WHERE 1=1 " + ConditionReturn + ") AS NewTable ";

                sql += " where  RowNum >= (@indexpage-" + PageMinNumFunction() + ") AND RowNum <= (@indexpage) ";



                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@indexpage", SqlDbType.Int).Value = indexpage;
                cmd.Parameters.Add("@txtstaffID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtstaffID);
                cmd.Parameters.Add("@txtstaffName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtstaffName) + "%";

                cmd.Parameters.Add("@txtClassID", SqlDbType.NVarChar).Value = Chk.CheckStringFunction(SearchStructure.txtClassID);
                cmd.Parameters.Add("@txtClassName", SqlDbType.NVarChar).Value = "%" + Chk.CheckStringFunction(SearchStructure.txtClassName) + "%";
         

                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    SearchClassName addValue = new SearchClassName();
                    addValue.txtClassID = dr["ClassID"].ToString();
                    addValue.txtClassName = dr["ClassIDName"].ToString();
                    addValue.txtstaffID = dr["Teacherid"].ToString();
                    addValue.txtstaffName = dr["teachername"].ToString();
                    //addValue.RowNum = dr["rownum"].ToString();
                    //addValue.ID = dr["ID"].ToString();
                    //addValue.ClassType = dr["ClassType"].ToString();
                    //addValue.InspectDate = dr["InspectDate"].ToString();
                    //addValue.StudentName = dr["StudentName"].ToString();
                    //addValue.TeacherName = dr["TeacherName"].ToString();
                    //addValue.AcademicYear = dr["AcademicYear"].ToString();
                    //addValue.ClassIDName = dr["ClassIDName"].ToString();
                    //if (dr["ClassType"].ToString() == 1)
                    //{
                    //    addValue.txtstudentbirthday = DateTime.Parse(dr["StudentBirthday"].ToString());
                    //}
                    returnValue.Add(addValue);
                }
                Sqlconn.Close();
            }
            catch (Exception e)
            {
                string a = e.Message.ToString();
                //SearchClassName addValue = new SearchClassName();
                //addValue.checkNo = "-1";
                //addValue.errorMsg = e.Message.ToString();
                //returnValue.Add(addValue);
            }
        }
        return returnValue;
    }






}
