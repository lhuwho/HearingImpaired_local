<%@ Page Language="C#" AutoEventWireup="true" CodeFile="activity_statistics.aspx.cs" Inherits="activity_statistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 活動統計 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/activity_statistics.css" />
	<link rel="stylesheet" type="text/css" href="./css/chosen/chosen.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="./js/source/jquery.fancybox.css" />
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<link rel="stylesheet" type="text/css" href="./css/pagination.css" />
	<script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
	<script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
	
	
    <script type="text/javascript" src="./js/source/jquery.fancybox.pack.js"></script>
	
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	
	
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
    
    <script type="text/javascript" src="./js/activity_statistics.js"></script>
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
			<div id="mainClass">個案管理&gt; 活動統計</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./activity_statistics.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">活動名稱 <input id="gosrheventName" type="text" value="" /></td>
			                <td width="260">活動日期 <input id="gosrheventDatestart" class="date" type="text" value="" size="10" />～<input id="gosrheventDateend" class="date" type="text" value="" size="10" /></td>
			                <td width="260">&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="480">活動名稱</th>
			                    <th width="100">活動日期</th>
			                    <th width="100">參與人數</th>
			                    <th width="100">功能</th>
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
			    <p align="right" id="caseUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
			    <p align="right">填寫者 <input id="creatFileName" type="text" size="10" /></p>
			    
                <table class="tableContact" width="780" border="0">
                    <tr>
		                <th>活動名稱</th>
		            </tr>
		            <tr>
		                <td height="30" align="center"><input id="eventName" type="text" value="" size="80" /></td>
		            </tr>
		            <tr>
		                <th>活動日期</th>
		            </tr>
		            <tr>
		                <td height="30" align="center"><input id="eventDate" class="date" type="text" value="" /></td>
		            </tr>
		            <tr>
		                <th>活動負責人員(最多選三個)</th>
		            </tr>
		            <tr>
		                <td height="30" align="center"> 
		                    <select id="eventStaffList" data-placeholder="選擇人員..." class="chosen-select" multiple="multiple"></select>
		                </td>
		            </tr>
		            <tr>
		                <th>參與活動名單</th>
		            </tr>
		            <tr>
		                <td id="listDiv" align="center"> 
		                    <input type="text" id="Participant"/><input class="" type="button"  onclick="addParticipantone();" value="確定"></input>
		                    <div class="chosen-container chosen-container-multi" ><ul id="ParticipantsList" class="chosen-choices"></ul></div>
		                </td>
		            </tr>
		            <tr>
		                <td class="hitP"><font color="Red">※  使用說明：（暫定此流程，可變更）<br />1. 先刷入學生ID後會帶出學生姓名，或輸入學生ID後選取。<br />
		                2. 確認後再按ENTER，會再參與名單內加入此學生的姓名。<br />
		                3. 如要刪除此學生，請按紅色X圖示。</font></td>
		            </tr>
                </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveData();">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button"  onclick="setData();">存 檔</button>
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