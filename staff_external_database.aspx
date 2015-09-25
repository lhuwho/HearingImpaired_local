<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_external_database.aspx.cs" Inherits="staff_external_database" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人事管理 - 外聘教師資料維護 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/staff_external_database.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/staff_external_database.js"></script>
	
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>
    <div id="container">
		<div id="header">
			<div id="logo"><a href="default.aspx"><img src="./images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">人事管理&gt; 外聘教師資料維護</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./staff_external_database.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">教師姓名 <input type="text" id="gosrhstaffName" value="" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">教師姓名</th>
			                    <th>聯絡電話1</th>
			                    <th>聯絡電話2</th>
			                    <th width="200">E-mail</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination" class="pagination"></div>
			</div>
			<div id="main">
			<div id="mainContent">
			<br />
			    <div id="item1Content">
			        <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="150">教師姓名</th>
			                <td><input id="staffName" type="text" value="" /><span class="startMark">*</span></td>
			            </tr>
			            <tr>
			                <th>身份證字號</th>
			                <td><input type="text" id="staffTWID" /><span class="startMark">*</span></td>
			                <td>&nbsp;</td>
			            </tr>
		                <tr>
			                <th>戶籍地址</th>
			                <td colspan="3"><input id="censusAddressZip" type="text" maxlength="5" value="" size="5"/> <select id="censusCity" class="zoneCity"></select> <input id="censusAddress" type="text" value="" size="50"/></td>
			            </tr>
			            <tr>
			                <th>通訊地址　<label><input type="checkbox" name="AddressCopyaddress" value="1"/> 同上</label></th>
			                <td colspan="3"><input id="addressZip" type="text" maxlength="5" value=""  size="5"/> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" value="" size="50"/></td>
			            </tr>
			            <tr>
			                <th>連絡電話</th>
			                <td colspan="3">
			                    <ul>
			                        <li><input id="Phone" type="text" value="" /><span class="startMark">*</span></li>
			                        <li><input id="Phone2" type="text" value="" /></li>
			                    </ul>
			                </td>
			            </tr>
			            <tr>
			                <th>E-mail</th>
			                <td colspan="3"><input type="text" value="" size="80" id="staffemail" /></td>
			            </tr>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="SetStaffData(0)">存 檔</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="SetStaffData(1)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			        <div id="WorkDiv">
			            <table class="tableText" width="600" border="0" id="WorkingTime">
			                <thead>
			                    <tr>
			                        <th width="200">日期期間</th>
			                        <th>課程名稱</th>
			                        <th>費用</th>
		                            <th width="100">功能</th>
			                    </tr>
                            </thead>
                            <tbody>
                            </tbody>
			            </table>
			            <p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData()">新 增</button></p>
			            <div id="insertDataDiv" style="display:none">
		                    <table class="tableText" width="600" border="0">
		                        <thead>
		                            <tr>
                                        <th width="200">日期期間</th>
			                            <th>課程名稱</th>
			                            <th>費用</th>
		                                <th width="100">功能</th>
		                            </tr>
		                        </thead>
		                        <tbody>
		                            <tr>
		                                <td><input id="CourseDate1" class="date" type="text" value="" size="10" /> ~ <input id="CourseDate2" class="date" type="text" value="" size="10" /></td>
		                                <td><input id="Course" type="text" value="" /></td>
		                                <td><input id="CoursePrice" type="text" value="" size="10"/></td>
		                                <td><div class="UD"><button class="btnView" type="button" onclick="setWorkData()">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                                </td>
		                            </tr>
                                </tbody>
                            </table>
		                </div>
			        </div>
			    
			    </div>
			    
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
