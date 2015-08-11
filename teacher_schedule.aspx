<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teacher_schedule.aspx.cs" Inherits="teacher_schedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教學管理 - 教師服務時間表 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
       <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<link rel="stylesheet" type="text/css" href="./css/chosen/chosen.css" />
    <link rel="stylesheet" type="text/css" href="./css/jquery-ui-1.8.13.custom.css" />
    <link rel='stylesheet' type='text/css' href='./css/jquery.weekcalendar.css' />
    <link rel="stylesheet" type="text/css" href="./css/pagination.css" />
    <link rel="stylesheet" type="text/css" href="./css/teacher_schedule.css" />
         	<script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
	<script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
    
    <script type='text/javascript' src='http://code.jquery.com/jquery-1.7.2.min.js'></script>
    <script type="text/javascript" src="./js/jquery-ui-1.8.13.custom.min.js"></script>
    <script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
    <script type='text/javascript' src='./js/jquery.weekcalendar.js'></script>
    
    

    
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	
	
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
   
    
    <script type="text/javascript" src="./js/base.js"></script>

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
			    <div id="mainContentSearch">
			        <p align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">台北至德</p>
			        <p>教師姓名 <input id="teacherName" type="text" name="name" class="ui-autocomplete-input" autocomplete="off" value="" /> <input type="button" value="確定" onclick="Search()" /><br /><br /></p>
			        <div id="calendar"></div>
		        </div>
			</div>
			<div id="event_edit_container" style="display:none;">
		            <div>
			           
			            <ul>
				            <li>
					            <label>日　　期：</label><span class="date_holder"></span> 
				            </li>
				            <li>
					            <label for="start">開始時間：</label><select name="start"><option value="">請選擇開始時間</option></select>
				            </li>
				            <li>
					            <label for="end">結束時間：</label><select name="end"><option value="">請選擇結束時間</option></select>
				            </li>
				           
				            <li>
				            <button type="button" onclick="getStudentNameUI()" class="ui-button ui-widget ui-state-default ui-corner-all ui-button-text-only" role="button" style="background-color: rgb(249, 174, 86);">
				                <span class="ui-button-text">學生姓名</span></button>
				               
					            <div class="chosen-container chosen-container-multi" >
					                <ul id="studentName" class="chosen-choices"></ul></div>
				            </li>
				            <li>
				            <select id="ClassNameID" >
				            <option value="0">請選擇教室</option>
					             <option value="1">E01</option>
					             <option value="2">E02</option>
					             <option value="3">E03</option>
					             <option value="4">E04</option>
					             <option value="5">E05</option>
					             <option value="6">E06</option>
					             </select>
					             </li>
			            </ul>
			           
		            </div>
	            </div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>