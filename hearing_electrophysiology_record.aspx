<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_electrophysiology_record.aspx.cs" Inherits="hearing_electrophysiology_record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>聽力管理 - 聽覺電生理檢查記錄 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_electrophysiology_record.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/hearing_electrophysiology_record.js"></script>
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
			<div id="logo"><a href="./default.aspx"><img src="./images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">聽力管理&gt; 聽覺電生理檢查記錄</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">一覽</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			    <p align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">台北至德</p>
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input type="text" name="name" value="王小明"/></td>
			                <td width="260">性　　別 <select name="name"><option value="0">請選擇</option><option value="1" selected="selected">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" name="name" size="10" value="100.01.01"/></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">顯 示</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="60">#</th>
			                    <th width="120">檢查日期<br />(年/月/日)</th>
			                    <th width="120">檢查地點</th>
			                    <th width="120">檢查項目</th>
			                    <th width="150">檢查結果-左耳</th>
			                    <th width="150">查結果-右耳</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr id="HS_1">
			                    <td><input type="text" value="1" size="3"/></td>
			                    <td><input class="date" type="text" value="101.12.10" size="10"/></td>
			                    <td><input type="text" value="基金會" size="10"/></td>
			                    <td><select><option value="0">請選擇</option><option value="1" selected>TEOAE</option><option value="2">DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select></td>
			                    <td><input type="text" value="" size="15"/></td>
			                    <td><input type="text" value="" size="15"/></td>
			                    <td><div class="UD"><button class="btnView" type="button" onclick="UpData(1)">更 新</button><br /><button class="btnView" type="button" onclick="">刪 除</button></div>
			                    <div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(1)">儲 存</button><br /><button class="btnView" type="button" onclick="SaveData(1)">取 消</button></div>
			                    </td>
			                </tr>
			                <tr id="HS_2">
			                    <td><input type="text" value="2" size="3"/></td>
			                    <td><input class="date" type="text" value="101.12.10" size="10"/></td>
			                    <td><input type="text" value="基金會" size="10"/></td>
			                    <td><select><option value="0">請選擇</option><option value="1">TEOAE</option><option value="2" selected>DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select></td>
			                    <td><input type="text" value="" size="15"/></td>
			                    <td><input type="text" value="" size="15"/></td>
			                    <td><div class="UD"><button class="btnView" type="button" onclick="UpData(2)">更 新</button><br /><button class="btnView" type="button" onclick="">刪 除</button></div>
			                    <div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(2)">儲 存</button><br /><button class="btnView" type="button" onclick="SaveData(2)">取 消</button></div>
			                    </td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			    <p align="center"><br /><button class="btnAdd" type="button">新 增</button></p>
			    <div id="insertDataDiv" style="display:none">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
		                        <th width="120">檢查日期<br />(年/月/日)</th>
		                        <th width="120">檢查地點</th>
		                        <th width="120">檢查項目</th>
		                        <th width="150">檢查結果-左耳</th>
		                        <th width="150">查結果-右耳</th>
		                        <th width="90">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
		                        <td><input class="date" type="text" value="" size="10"/></td>
		                        <td><input type="text" value="" size="10"/></td>
		                        <td><select><option value="0">請選擇</option><option value="1">TEOAE</option><option value="2">DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select></td>
		                        <td><input type="text" value="" size="15"/></td>
		                        <td><input type="text" value="" size="15"/></td>
		                        <td><div class="UD"><button class="btnView" type="button" onclick="">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                        </td>
		                    </tr>
		                 </tbody>
                    </table>
		        </div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>