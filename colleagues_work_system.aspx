<%@ Page Language="C#" AutoEventWireup="true" CodeFile="colleagues_work_system.aspx.cs" Inherits="colleagues_work_system" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>出勤系統 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	
	<style type="text/css">
	    .tableText {
	    	margin: 0 auto;
	    }
	    .tableText td {
	        line-height: 35px;
	        font-size: 15px;
	        color: #683812;
	        text-align: center;
        }
        
        .tableText input[type="text"] {
        	text-align: center;
        }
        
        #msgDiv {
        	color: Red;
        	font-size: 16px;
        	font-weight: bold;
        	text-align: center;
        	margin: 20px 0 0;
        	display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/colleagues_work_system.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    
    <div id="container">
		<div id="header">
			<div id="logo"><a href="./default.aspx"><img src="images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
		</div>
		<div id="content">
			<div id="mainClass">出勤系統</div>
			<div id="mainContent">
			    <table class="tableText" width="450" border="0">
		            <tr>
		                <td colspan="2">&nbsp;</td>
		            </tr>
		            <tr>
		                <td width="200">員工ID</td>
		                <td><input id="staffID" style="color:White;"  type="text" value="" autocomplete="off" /></td>
		            </tr>
		            <tr>
		                <td width="200">員工姓名</td>
		                <td><span id="staffName"></span></td>
		            </tr>
		            <tr>
		                <td colspan="2">&nbsp;</td>
		            </tr>
			    </table>
			    <p id="msgDiv"></p>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
