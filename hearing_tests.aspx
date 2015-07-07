<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_tests.aspx.cs" Inherits="hearing_tests" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>聽力管理 - 聽知覺測驗結果記錄 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_tests.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/hearing_tests.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    
    <div id="container">
		<div id="header">
			<div id="logo"><a href="default.aspx"><img src="./images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">聽力管理&gt; 聽知覺測驗結果記錄</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">一覽</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <p align="right" id="Unit" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input id="studentName" type="text" value="" readonly="readonly"/><span id="studentID" class="hideClassSpan"></span></td>
			                <td width="260">性　　別 　<span id="studentSex"></span></td>
                            <td width="260">出生日期　<span id="studentbirthday"></span></td>
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
			                    <th width="80">測驗日期<br />(年/月/日)</th>
			                    <th width="75">輔具</th>
			                    <th width="75">受測耳</th>
			                    <th width="180" colspan="2">測驗工具</th>
			                    <th width="80">情境<br />(PL/SNR)</th>
			                    <th width="110">結果</th>
			                    <th width="110">備註</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <!--<tr id="HS_1">
			                    <td>102/01/01</td>
			                    <td><select><option value="0">請選擇</option><option value="1">裸耳</option><option value="2">助聽器</option></select></td>
			                    <td><select id="state"><option value="0" selected="selected">請選擇</option><option value="1">左耳</option><option value="2">右耳</option><option value="3">不分耳</option></select></td>
			                    <td><input type="text" name="name" value="" size="12"/></td>
			                    <td><input type="text" name="name" value="PL" size="9"/></td>
			                    <td><input type="text" name="name" value="PL" size="10"/></td>
			                    <td><input type="text" name="name" value="" size="12"/></td>
			                    <td><input type="text" name="name" value="無" size="12"/></td>
			                    <td><div class="UD"><button class="btnView" type="button" onclick="UpData(1)">更 新</button><br /><button class="btnView" type="button" onclick="">刪 除</button></div>
			                    <div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(1)">儲 存</button><br /><button class="btnView" type="button" onclick="SaveData(1)">取 消</button></div>
			                    </td>
			                </tr>-->
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