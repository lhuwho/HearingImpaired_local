<%@ Page Language="C#" AutoEventWireup="true" CodeFile="remind_system.aspx.cs" Inherits="remind_system" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>其他管理 - 提醒系統 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/remind_system.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/remind_system.js"></script>
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
			<div id="mainClass">其他管理&gt; 提醒系統</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">一覽</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			    <p id="Unit" align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">指派者 <input id="gosrhdesignee" type="text" value=""/></td>
			                <td width="260">執行者 <input id="gosrhrecipient" type="text" value=""/></td>
			                <td width="260">建立日期 <input id="gosrhdesigneeDatestart" class="date" type="text" size="10" value=""/>～<input id="gosrhdesigneeDateend" class="date" type="text" size="10" value=""/></td>
			            </tr>
			            <tr>
			                <td>提醒日期 <input id="gosrhexecutionDatestart" class="date" type="text" size="10" value=""/>～<input id="gosrhexecutionDateend" class="date" type="text" size="10" value=""/></td>
			                <td>完成日期 <input id="gosrhfulfillmentDatestart" class="date" type="text" size="10" value=""/>～<input id="gosrhfulfillmentDateend" class="date" type="text" size="10" value=""/></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="SearchData()" >顯 示</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="60">流水號</th>
			                    <th width="85">建立日期</th>
			                    <th width="85">指派者</th>
			                    <th width="85">執行者</th>
			                    <th width="185">內容</th>
			                    <th width="85">提醒日期</th>
			                    <th width="85">完成日期</th>
			                    <th width="110">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination" class="pagination"></div>
		        <p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData()">新 增(個人)</button>　<button class="btnAdd" type="button" onclick="InsertData2()">新 增(群組)</button></p>
		        <div id="insertDataDiv" style="display:none">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
		                        <th width="95">建立日期</th>
		                        <th width="95">指派者</th>
		                        <th width="95">執行者</th>
		                        <th width="290">內容</th>
		                        <th width="95">提醒日期</th>
		                        <th width="110">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
		                        <td  height="50"><span id="fileInData"></span></td>
		                        <td><span id="sender"></span></td>
		                        <td><input id="recipient" type="text" value="" size="10" readonly="readonly" /><span id="recipientID" class="hideClassSpan"></span></td>
		                        <td><textarea id="executionContent" cols="42" rows="1"></textarea></td>
		                        <td><input id="executionDate" class="date" type="text" value="" size="10" /></td>
		                        <td><button class="btnView" type="button" onclick="setRemindData();">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert()">取 消</button></td>
		                    </tr>
		                 </tbody>
		            </table>
		        </div>
		        <div id="insertDataDiv2" style="display:none">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
		                        <th width="95">建立日期</th>
		                        <th width="95">指派者</th>
		                        <th width="95">執行者</th>
		                        <th width="290">內容</th>
		                        <th width="95">提醒日期</th>
		                        <th width="110">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
		                        <td  height="50"><span id="fileInData2"></span></td>
		                        <td><span id="sender2"></span></td>
		                        <td><select id="recipient2" style="width:80px"></select></td>
		                        <td><textarea id="executionContent2" cols="42" rows="1"></textarea></td>
		                        <td><input id="executionDate2" class="date" type="text" value="" size="10" /></td>
		                        <td><button class="btnView" type="button" onclick="setRemindData2();">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert2()">取 消</button></td>
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