<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_services.aspx.cs" Inherits="hearing_services" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>聽力管理 - 聽力服務一覽 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_services.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" href="./css/chosen/chosen.css" />
	<script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/pagination.css" />
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/hearing_services.js"></script>
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
			<div id="mainClass">聽力管理&gt; 聽力服務一覽</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">一覽</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			    <p id="Unit" align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input id="txtstudentName" type="text" value="" readonly="readonly" /><span id="txtstudentID" class="hideClassSpan" /></span></td>
			                <td width="260">性　　別 <select id="txtstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="txtstudentDateStart" class="date" type="text" size="10" value="" />～<input id="txtstudentDateEnd" class="date" type="text" size="10" value="" /></td>
			            </tr>
			            <tr>
			                <td>檢查日期 <input id="txtcheckDateStart" class="date" type="text" size="10" value="" />～<input id="txtcheckDateEnd" class="date" type="text" size="10" value="" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch2" type="button" onclick="showView(1)">顯 示</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="60">次數</th>
			                    <th width="100">日期(年/月/日)</th>
			                    <th width="200">內容</th>
			                    <th width="100">評估人員</th>
			                    <th width="200">備註</th>
			                    <th width="120">功能</th>
			                </tr>
			            </thead>
			            <tbody>
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
