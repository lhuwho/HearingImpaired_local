<%@ Page Language="C#" AutoEventWireup="true" CodeFile="supplement_inventory.aspx.cs" Inherits="supplement_inventory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 個案補助清冊 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/supplement_inventory.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/supplement_inventory.js"></script>
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
			<div id="mainClass">個案管理&gt; 個案補助清冊</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./supplement_inventory.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">個案姓名 <input type="text" name="name" /></td>
			                <td width="260">戶 籍 地 <input type="text" name="name" /></td>
			                <td width="260">個案性別 <select name="name"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
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
			                    <th width="240">服務使用者編號</th>
			                    <th width="240">個案姓名</th>
			                    <th width="240">戶 籍 地</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>10123</td>
			                    <td>王小明</td>
			                    <td>台北市</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>20123</td>
			                    <td>陳曉雯</td>
			                    <td>新北市</td>
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
			    <p>個案姓名 <input id="studentName" type="text" name="name" />　　戶籍地 <input id="studentCity" type="text" name="name" /></p>
			    <table class="tableText" width="780" border="0">
			        <thead><tr>
			            <th width="290">補助項目</th>
			            <th width="100">補助年度</th>
			            <th width="195">補助年額</th>
			            <th width="195">備　　註</th>
			        </tr></thead>
			        <tr>
			            <th>身心障礙者生活補助</th>
			            <td><select class="year_ddl"><option value="-1">民國年</option></select></td>
			            <td><input type="text" name="name" /></td>
			            <td><input type="text" name="name" /></td>
		            </tr>
		            <tr>
			            <th>發展遲緩兒童療育補助</th>
			            <td><select class="year_ddl"><option value="-1">民國年</option></select></td>
			            <td><input type="text" name="name" /></td>
			            <td><input type="text" name="name" /></td>
		            </tr>
		            <tr>
			            <th>身心障礙者生活輔助器具補助助聽器</th>
			            <td><select class="year_ddl"><option value="-1">民國年</option></select></td>
			            <td><input type="text" name="name" /></td>
			            <td><input type="text" name="name" /></td>
		            </tr>
		            <tr>
			            <th>低收入戶生活補助</th>
			            <td><select class="year_ddl"><option value="-1">民國年</option></select></td>
			            <td><input type="text" name="name" /></td>
			            <td><input type="text" name="name" /></td>
		            </tr>
		            <tr>
			            <th>身心障礙者托育養護費</th>
			            <td><select class="year_ddl"><option value="-1">民國年</option></select></td>
			            <td><input type="text" name="name" /></td>
			            <td><input type="text" name="name" /></td>
		            </tr>
		            <tr>
			            <th>其他</th>
			            <td><select class="year_ddl"><option value="-1">民國年</option></select></td>
			            <td><input type="text" name="name" /></td>
			            <td><input type="text" name="name" /></td>
		            </tr>
			    </table>
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
