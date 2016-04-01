<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apply_property.aspx.cs" Inherits="apply_property" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>財產管理 - 請購、請修單 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/apply_property.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/pagination.css" />
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/apply_property.js" />
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
			<div id="mainClass">財產管理&gt; 請購單</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./apply_property.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">申請類別 <select id="gosrhapplyType" autocomplete="off"><option value="0">請選擇</option><option value="1">請購</option><option value="2">請修</option></select></td>
			                <td width="260">申請狀態 <select id="gosrhapplyStatus" autocomplete="off"><option value="0">請選擇</option><option value="1">已列印</option><option value="2">審核中</option><option value="3">審核通過</option><option value="4">審核駁回</option><option value="5">作廢</option></select></td>
			                <td width="260">申請單流水號 <input id="gosrhapplyID" type="text" value="" autocomplete="off" /></td>
                        </tr>
			            <tr>
			                <td>填寫日期 <input id="gosrhapplyDateStart" class="date" type="text" value="" size="10" autocomplete="off" />～<input id="gosrhapplyDateEnd" class="date" type="text" value="" size="10" autocomplete="off" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">申請單流水號</th>
			                    <th width="80">申請類別</th>
			                    <th width="80">付款方式</th>
			                    <th width="80">申請狀態</th>
			                    <th width="100">申請人</th>
			                    <th width="80">合計</th>
			                    <th width="80">申請日期</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination" class="pagination"></div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p style="background-color:#FFDF71;padding:0 10px;"><font size="3"><select id="applyType" autocomplete="off"><option value="0">請選擇</option><option value="1">請購</option><option value="2">請修</option></select>　申請暨核銷單</font><span id="sUnit" style="float:right;"></span></p>
			    <p><span id="statusP">狀態 <select id="applyStatus" autocomplete="off"><option value="0">請選擇</option><option value="1">已列印</option><option value="2">審核中</option><option value="3">審核通過</option><option value="4">審核駁回</option><option value="5">作廢</option></select></span> <span style="float:right">填表日期 <input id="applyDate" class="date" type="text" value="" size="10" autocomplete="off" /></span></p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">付款方式</th>
			            <td><label><input type="radio" name="applyPay" value="1" autocomplete="off" /> 現金</label>　　<label><input type="radio" name="applyPay" value="2" autocomplete="off" /> 支票</label></td>
			        </tr>
			        <tr>
			            <td colspan="2" align="right"><button class="btnAdd" onclick="detailInsert()" type="button">新 增</button></td>
			        </tr>
			        <tr>
			            <td colspan="2">
			                <table class="tableContact" width="780" border="0" id="DetailTable">
			                    <thead><tr>
			                        <th width="150">品名</th>
			                        <th width="45">單位</th>
			                        <th width="45">數量</th>
			                        <th width="55">規格</th>
			                        <th width="55">估計<br />單價</th>
			                        <th width="60">總價</th>
			                        <th>說明</th>
			                        <th width="120">發票號碼</th>
			                    </tr></thead>
			                    <tbody><tr id="Detail1">
			                        <td height="40"><input class="propertyName" type="text" value="" autocomplete="off" /></td>
			                        <td><input class="propertyUnit" type="text" value="" autocomplete="off" size="3" /></td>
			                        <td><input class="propertyQuantity" type="text" value="" autocomplete="off" size="3" /></td>
			                        <td><input class="propertyFormat" type="text" value="" autocomplete="off" size="5" /></td>
			                        <td><input class="propertyPrice" type="text" value="" autocomplete="off" size="5" /></td>
			                        <td class="propertySum">0</td>
			                        <td><input class="propertyExplain" type="text" value="" autocomplete="off" size="30" /></td>
			                        <td><input class="propertyBill" type="text" value="" autocomplete="off" size="13" /></td>
			                    </tr></tbody>
			                    <tfoot><tr>
			                        <td colspan="4">&nbsp;</td>
			                        <th>合計</th>
			                        <th id="applySum">0</th>
			                        <th>領款人</th>
			                        <th id="applyBy"></th>
			                    </tr>
			                    <tr>
			                        <th>合計新台幣</th>
			                        <td colspan="7"><input id="sumFive" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 萬
			                        <input id="sumFour" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 仟 <input id="sumThree" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 佰
			                        <input id="sumTwo" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 拾 <input id="sumOne" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 元　整</td>
			                    </tr></tfoot>
			                </table>
			            </td>
			        </tr>
			    </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveOffData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button"  onclick="saveOffData(1)">存 檔</button>
		            <button class="btnCancel" type="button">取 消</button>
		            <button class="btnPrint" type="button"  onclick="printPage()">列 印</button>
		        </p></div>
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>