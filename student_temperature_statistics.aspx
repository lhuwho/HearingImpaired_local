<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_temperature_statistics.aspx.cs" Inherits="student_temperature_statistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>行政管理 - 個案出勤統計 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
     <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/pagination.css" />	<link rel="stylesheet" type="text/css" href="./css/chosen/chosen.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	   	<script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
	<script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
 
	
	
    
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/student_temperature_statistics.js"></script>
	
	<style type="text/css">
		.tableList input[type='text'] {
			text-align: center;
		}
		.tableList th {
			background-color: #F9AE56;
			border: 1px solid #FFFFFF;
		}
		.tableList tbody td {
			border: 1px solid #F9AE56;
		}
	</style>
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <<asp:ScriptManager ID="ScriptManager" runat="server">
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
			<div id="mainClass">行政管理&gt; 個案出勤統計</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">一覽</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			    <p id="Unit" align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生名稱 <input type="text" name="name" value="" id="studentID"  class="ui-autocomplete-input" autocomplete="off"></td>
			            
			                <td>測量月份 
			                    <select id="yearDate"><option value="-1">民國年</option></select>
                                <select id="monthDate"><option value="-1">月</option></select>
                            </td>
			               
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">顯 示</button></td>
			            </tr>
			            <tr>
			                <td colspan="3" height="10"></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList"></div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
