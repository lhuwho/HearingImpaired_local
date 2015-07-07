﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="voice_distance.aspx.cs" Inherits="voice_distance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教學管理 - 語音距離察覺圖 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/voice_distance.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/voice_distance.js"></script>
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
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
			<div id="mainClass">教學管理&gt; 語音距離察覺圖</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./voice_distance.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input type="text" value="" /></td>
			                <td width="260">性　　別 <select><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" value="" size="10" />～<input class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>年　　度 <input type="text" name="name" size="5" />～<input type="text" name="name" size="5" /></td>
			                <td>教師姓名 <input type="text" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="120">排序</th>
			                    <th width="120">年度</th>
			                    <th width="120">學期</th>
			                    <th width="120">學生姓名</th>
			                    <th width="120">出生日期</th>
			                    <th width="120">年齡</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>1</td>
			                    <td>102</td>
			                    <td>1</td>
			                    <td>王小明</td>
			                    <td>98.01.01</td>
			                    <td>4歲9個月</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>2</td>
			                    <td>102</td>
			                    <td>1</td>
			                    <td>陳曉雯</td>
			                    <td>99.01.01</td>
			                    <td>3歲9個月</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p align="right" style="background-color:#FFDF71;padding:0 10px;">台北至德</p>
			    <p align="right"><input type="text" name="name" size="5" /> 年度第 <input type="text" name="name" size="5" /> 學期</p>
			    <table class="tableText" width="780" border="0">
			       <tr>
			           <th width="150">學生姓名</th>
			           <td><input id="studentName" type="text" name="name" /></td>
			       </tr>
			       <tr>
			           <th>出生日期</th>
			           <td><select id="year_ddl1"><option value="-1">民國年</option></select>
                           <select id="month_ddl1"><option value="-1">月</option></select>
                           <select id="day_ddl1"><option value="-1">日</option></select>
		               </td>
		           </tr>
			    </table>
			    <div class="PerceiveDIV">
			        <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" name="name" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" name="name" /></td>
		                   </tr>
			            </table>   
			        </div>
			        <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" name="name" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" name="name" /></td>
		                   </tr>
			            </table>   
			        </div>
			        <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" name="name" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" name="name" /></td>
		                   </tr>
			            </table>   
			        </div>
			    </div>
			    <p class="btnP">
		            <button class="btnSave" type="button">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button">存 檔</button>
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