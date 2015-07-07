<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teacher_schedule.aspx.cs" Inherits="teacher_schedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教學管理 - 教師服務時間表 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
    <link rel="stylesheet" type="text/css" href="./css/teacher_schedule.css" />
    <script type="text/javascript" src="./js/jMonthCalendar.js"></script>
    
    <link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/teacher_schedule.js"></script>
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
			<div id="mainClass">教學管理&gt; 教師服務時間表</div>
			<div id="main">
			    <div id="mainContent">
			        <p align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">台北至德</p>
			        <p>教師姓名 <input id="teacherName" type="text" name="name" value="" /> <input type="button" value="確定" /><br /><br /></p>
			        <div id="jMonthCalendar"></div>
		        </div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>