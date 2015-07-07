<%@ Page Language="C#" AutoEventWireup="true" CodeFile="testpage.aspx.cs" Inherits="testpage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
            AspAjax.set_defaultSucceededCallback(SucceededCallback);
            AspAjax.set_defaultFailedCallback(FailedCallback);

        });
        function SucceededCallback(result, userContext, methodName) {
            switch (methodName) {
                case "setStudentDataBase":
                    break;
            }
        }
        function FailedCallback(error, userContext, methodName) {
            //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
        }
        function functionaction() {
            var obj = {};
            obj.Column = 4;
            obj.studentID = 200004;
            obj.test1 = 1;
            AspAjax.setTeachISPDate4(obj);
        }
        /*
                        string sql = "UPDATE StudentDatabase SET CaseFamilyCooperative=@CaseFamilyCooperative WHERE ID=@ID";
                SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                cmd.Parameters.Add("@ID", SqlDbType.BigInt).Value = StudentTeaching.ID;
                cmd.Parameters.Add("@CaseFamilyCooperative", SqlDbType.NChar).Value = StudentTeaching.case1;
                returnValue = cmd.ExecuteNonQuery();
                Sqlconn.Close();
        */

        /*
        
                 string sql = "INSERT INTO StudentHeightWeightRecord (StudentID, StudentHeight) " +
                                "VALUES (@StudentID,  @StudentHeight)";
                 SqlCommand cmd = new SqlCommand(sql, Sqlconn);
                 cmd.Parameters.Add("@StudentID", SqlDbType.BigInt).Value = StudentBody.StudentID;
                 cmd.Parameters.Add("@StudentHeight", SqlDbType.Decimal).Value = StudentBody.Height;
                 returnValue = cmd.ExecuteNonQuery();
                 Sqlconn.Close();
        */
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>
    <input type="text" id="test1" value="" />
    <br />
    <input type="button" value="按鈕" onclick="functionaction()"/>
</body>
</html>
