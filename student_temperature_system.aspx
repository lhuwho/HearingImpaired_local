<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_temperature_system.aspx.cs" Inherits="student_temperature_system" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案體溫系統 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
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
	    .tableText td {
	        line-height: 35px;
	        font-size: 15px;
	        color: #683812;
        }
        
        .tableText input[type="text"] {
        	text-align: center;
        }
        .tableText td > div {
        	display: inline-block;
	        *display: inline;
	        *zoom: 1;
	        width: 200px;
        	text-align: center;
        }
        .inputDiv {
        	text-align: left;
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
                <asp:ScriptReference Path="~/js/student_temperature_system.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    
    <div id="container">
		<div id="header">
			<div id="logo"><a href="./default.aspx"><img src="images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
		</div>
		<div id="content">
			<div id="mainClass">個案體溫系統</div>
			<div id="mainContent">
			    <table class="tableText" width="780" border="0">
		            <tr>
		                <th width="350" height="300"><img id="studentAvatar" src="./images/noAvatar2.jpg" alt="" /></th>
		                <td>
		                    <div>學生ID</div><div class="inputDiv"><input id="gosrhpeopleID" type="text" value="" autocomplete="off" /></div>
		                    <div>學生姓名</div><div class="inputDiv"><span id="studentName"></span></div>
		                    <div>學生體溫</div><div class="inputDiv"><input id="peopleTemp" type="text" value="" autocomplete="off" maxlength="5" size="10" /> 度</div>
		                    <div>陪同人體溫</div><div class="inputDiv"><input id="parentsTemp" type="text" value="" autocomplete="off" maxlength="5" size="10" /> 度</div>
		                </td>
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