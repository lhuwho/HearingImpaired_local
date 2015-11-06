<%@ Page Language="C#" AutoEventWireup="true" CodeFile="case_service_record.aspx.cs" Inherits="case_service_record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 個案服務紀錄 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/case_service_record.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/chosen/chosen.css" />
	<script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
	<script type="text/javascript" src="./js/case_service_record.js"></script>
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
			<div id="mainClass">個案管理&gt; 個案服務紀錄</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./case_service_record.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>服務使用者編號 <input type="text" id="gosrhstudentID" value="" /></td>
			                <td>個案狀態 <select id="gosrhcaseStatu"></select></td>
			                <td>訪談日期 <input id="gosrhendviewstart" class="date" type="text" size="10" value="" />～<input id="gosrhendviewend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="Search()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="80">服務使用者<br />編號</th>
			                    <th width="100">個案狀態</th>
			                    <th width="100">學生姓名</th>
			                    <th width="80">訪談日期</th>
			                    <th width="160">訪談主題</th>
			                    <th width="80">訪談方式</th>
			                    <th width="120">訪談對象</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			               
			            </tbody>
			        </table>
			         <div id="mainPagination" class="pagination"></div>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p id="sUnit" align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			    <table width="780" border="0">
			        <tr>
			            <td>學生姓名 <input id="studentName" type="text" value="" /><span id="studentID" class="hideClassSpan"></span></td>
			            <td>出生日期 <span id="studentbirthday">&nbsp;</span></td>
		                <td>性別 <span id="studentSex">&nbsp;</span></td>
			        </tr>
			        <tr>
			            <td colspan="3">
			                <table width="780" class="tableText" border="0">
			                    <tr>
			                        <th width="210">年月日時</th>
			                        <th width="100">訪談方式</th>
			                        <th width="470">訪談主題</th>
			                    </tr>
			                    <tr>
			                        <td><input id="viewData" class="date" type="text" value="" size="15" /> <input id="viewTime" class="time" type="text" value="" size="5" /><span class="startMark">*</span></td>
			                        <td><select id="viewStyle"><option value="0">請選擇</option><option value="1">電話訪談</option><option value="2">親自洽談</option></select></td>
			                        <td><input id="viewTitle" type="text" value="" size="50" /><span class="startMark">*</span></td>
			                    </tr>
			                    <tr>
			                        <th>訪談對象</th>
			                        <th>訪談地點</th>
			                        <th>訪談人員</th>
			                    </tr>
			                    <tr>
			                        <td><input id="viewPeople" type="text" value="" /></td>
			                        <td><input id="viewPlace" type="text" value="" /></td>
			                        <td> <select id="viewStaffselect" name="item" data-placeholder="選擇社工..." class="chosen-select" multiple="multiple">
					                </select></td>
			                    </tr>
			                    <tr>
			                        <th colspan="3">紀錄內容</th>
			                    </tr>
			                    <tr>
			                        <td colspan="3" height="360" align="center"><textarea id="viewContent"></textarea></td>
			                    </tr>
			                </table>
			            </td>
			        </tr>
			    </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="SaveData(0);">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="SaveData(1);">存 檔</button>
		            <button class="btnCancel" type="button">取 消</button>
		        </p></div>
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
