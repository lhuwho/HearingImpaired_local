<%@ Page Language="C#" AutoEventWireup="true" CodeFile="colleagues_work_manage.aspx.cs" Inherits="colleagues_work_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>薪資管理 - 出勤紀錄管理 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/colleagues_work_manage.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>

    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/colleagues_work_manage.js"></script>
	
		<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <!--<asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
        </asp:ScriptManager>-->
    </form>
    
    <div id="container">
		<div id="header">
			<div id="logo"><a href="default.aspx"><img src="./images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">薪資管理&gt; 出勤紀錄管理</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">一覽</div>
			    <div id="btnSearch2" class="menuTabs2">新增</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table id="searchTable" width="780" border="0">
			            <tr>
			                <td width="260">出勤日期 <input class="date" type="text" id="gosrhstaffBirthdayStart" size="10" /></td>
			                <td width="260">員工編號 <input type="text" id="gosrhstaffID" value=""/></td>
			                <td width="260">員工姓名 <input type="text" id="gosrhstaffName" value="" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="Search()">顯 示</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <caption>員工打卡紀錄</caption>
			            <thead>
			                <tr>
			                    <th height="30" width="100">員工編號</th>
			                    <th width="100">員工姓名</th>
			                    <th>打卡時間</th>
			                    <th width="100">請假</th>
			                    <th width="100">詳細紀錄</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr id="HS_1">
			                    <td height="30">2033</td>
			                    <td>嚴大光</td>
			                    <td>8:20　17:30</td>
			                    <td><button type="button" class="btnView" onclick="viewRecord(1)">檢 視</button></td>
			                </tr>
			                <tr id="HS_2" class="recodUnusual">
			                    <td height="30">2034</td>
			                    <td>廖炎</td>
			                    <td>8:20　12:10　17:30</td>
			                    <td><button type="button" class="btnView" onclick="viewRecord(2)">檢 視</button></td>
			                </tr>
			                <tr id="HS_3" class="recodUnusual">
			                    <td height="30">2035</td>
			                    <td>李武</td>
			                    <td>8:45　17:50</td>
			                    <td><button type="button" class="btnView" onclick="viewRecord(3)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
                </div>
                <div id="mainPagination" class="pagination"></div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
    <div id="top"></div>
</body>
</html>



