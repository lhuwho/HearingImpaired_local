<%@ Page Language="C#" AutoEventWireup="true" CodeFile="volunteer_sign.aspx.cs" Inherits="volunteer_sign" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 服務時數 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/volunteer_sign.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	
	<script type="text/javascript" src="./js/jquery.timePicker.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/jquery.timePicker.css" />
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/volunteer_sign.js"></script>
	
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
    <style type="text/css">
        pre {
            background:#fff;
            border:1px solid #ddd;
            padding:4px;
          }
          .error {
            border:1px solid red;
          }
    </style>
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
			<div id="mainClass">個案管理&gt; 服務時數</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">志工編號 <input  id = "gosrhvID" type="text" name="name" /></td>
			                <td width="260">志工姓名 <input  id = "gosrhvName" type="text" name="name" /></td>
			                <td width="260">志工性別 <select id = "gosrhvSex" name="name"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                        </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="SearchVolunteerData()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th>志工編號</th>
			                    <th>志工姓名</th>
			                    <th>功能</th>
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
			    <div style="display:block;">
			    <p align="right" id ="sUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
			    <p>
			        志工編號 <input id="volunteerId" type="text" name="name" size="10" />　　
			        志工姓名 <input id="volunteerName" type="text" name="name" size="10" />
			    </p>
			    
			  <div id="ServiceSearchlist" style="display: block;">
	              <table class="tableList" width="780" border="0">
        	        <thead>
			            <tr>
			                <th width="100">日期</th>
			                <th>時間起訖</th>
			                <th>總服務時數</th>
			                <th>備註</th>
			                <th>功能</th>
			            </tr> 
			        </thead>
			        <tbody>           
			        </tbody>
	            
	              </table>
	            
	          </div>
	           <div id="ServicePagination" class="pagination"></div>
			   <center> <button onclick="InsertData()" type="button" class="btnAdd">新 增</button> </center>
			<div id="InsertServiceTime" style="display:none">
			    <table class="tableText" width="780" border="0">
			        <thead>
			            <tr>
			                <th>日期</th>
			                <th>時間起訖</th>
			                <th>總服務時數</th>
			                <th>備註</th>
			                <th>功能</th>
			            </tr>
			        </thead>
			        <tbody>
			            <tr id="inserTable">
			                <td width="100"><input class="date ServiceDate" type="text" value="" size="10" /></td>
			                <td width="250"><input class="serviceBegin" type="text" value="07:00" size="10" autocomplete="off" onchange="chkChangeTime('inserTable')" /> ～ <input class="serviceEnd" type="text" value="07:30" size="10" autocomplete="off" onchange="CountTime('inserTable')"/></td>
			                <td width="100" class="serviceHour">0.5</td>
			                <td width="280"><input class="vOtherContent" type="text" value="" size="35" /></td>
			                <td width="100"><button class="btnSave" id="btnSave" type="button" onclick="SetVolunteerServiceTimeData()">儲 存</button></td>
			          </tr>
			           
			        </tbody>
			    </table>
			 </div>   
		        
			</div>
			</div>
		</div>
		
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
