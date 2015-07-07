<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_contracted_salary.aspx.cs" Inherits="staff_contracted_salary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>薪資管理 - 敘薪表 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/staff_contracted_salary.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/staff_contracted_salary.js"></script>
	
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
			<div id="mainClass">薪資管理&gt; 敘薪表</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./staff_contracted_salary.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">員工姓名 <input type="text" id="gosrhstaffName" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstaffSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" id="gosrhstaffBirthdayStart" size="10" />～<input class="date" type="text" id="gosrhstaffBirthdayEnd" size="10" /></td>
			            </tr>
			            <tr>
			                <td>員工編號 <input type="text" id="gosrhstaffID" value=""/></td>
			                <td>
			                    派任單位 <select id="gosrhstaffUnit"></select>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="SearchData()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="150">員工編號</th>
			                    <th width="140">派任單位</th>
			                    <th width="140">員工姓名</th>
			                    <th width="110">性別</th>
			                    <th width="140">填表日期</th>
			                    <th width="100">功能</th>
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
			    <div id="item1Content">
			        <p style="background-color:#FFDF71;padding:0 10px;">
			            <font size="3" id="sUnit">派任單位：</font>
			        </p>
			        <p>員工姓名 <input id="staffName" type="text" value="" size="10" readonly="readonly"/><span id="staffID" class="hideClassSpan"></span><span class="startMark">*</span>
			        <span style="float:right;"><span class="startMark">*</span>填表日期 <input id="fillInDate" class="date" type="text" value="" size="10" /></span></p>
			         <table class="tableContact" width="780" border="0">
			            <tr>
			                <th width="195">勞保費</th>
                            <th width="195">健保費</th>
                            <th width="195">退休金自提</th>
                            <th width="195">所得稅</th>
                        </tr>
                        <tr>
                            <td><input id="laborInsurance" type="text" value="" /></td>
                            <td><input id="healthInsurance" type="text" value="" /></td>
                            <td><select id="pensionFundsPer">
                            <option value="0">0%</option>
                            <option value="1">1%</option>
                            <option value="2">2%</option>
                            <option value="3">3%</option>
                            <option value="4">4%</option>
                            <option value="5">5%</option>
                            <option value="6">6%</option>
                            </select> <input id="pensionFunds" type="text" value="" size="16"/></td>
                            <td><input id="withholdingTax" type="text" value="" /></td>
                        </tr>
                    </table>
			        <table class="tableContact" width="780" border="0">
			            <tr>
			                <th rowspan="3" width="25">敘薪表</th>
			                <th width="25">每點</th>
			                <th colspan="2">學歷</th>
			                <th colspan="2">資歷</th>
			                <th colspan="3">職務加給</th>
			                <th colspan="2">主管加給</th>
			                <th colspan="2">特硃加給</th>
			                <th colspan="2">薪資</th>
			            </tr>
			            <tr>
			               <th rowspan="2">元計算</th>
			                <th width="100">學歷</th>
			                <th width="40">點數</th>
			                <th width="40">年資</th>
			                <th width="40">點數</th>
			                <th>職稱</th>
			                <th>職等</th>
			                <th width="40">點數</th>
			                <th>職稱</th>
			                <th width="40">點數</th>
			                <th width="40">職稱</th>
			                <th width="40">點數</th>
			                <th>總點數</th>
			                <th>總金額</th>
			            </tr>
			            <tr>
			                <td><select id="education"><option value="0">請選擇</option>
			                <option value="210">高中(職)</option>
			                <option value="230">二專、五專</option>
			                <option value="240">三專</option>
			                <option value="265">大學</option>
			                <option value="280">碩士</option>
			                <option value="300">博士</option>
			                </select></td>
			                <td id="count1">0</td>
			                <td><input id="years" type="text" value="0" size="3" /></td>
			                <td id="count2">0</td>
			                <td><select id="applyJob"><option value="0">請選擇</option>
			                <option value="70">高專</option>
			                <option value="45">專員</option>
			                <option value="25">組員</option>
			                <option value="10">助理</option>
			                <option value="0">助佐</option>
			                <option value="90">講師</option>
			                <option value="65">助理講師</option>
			                <option value="45">專業教師</option>
			                <option value="30">助理教師</option>
			                <option value="20">教師助理</option>
			                </select></td>
			                <td><select id="jobLevel"><option value="0">請選擇</option><option value="0">0</option>
			                    <option value="1">1</option><option value="2">2</option>
			                    <option value="3">3</option><option value="4">4</option>
			                    <option value="5">5</option><option value="6">6</option>
			                    <option value="7">7</option><option value="8">8</option>
			                    <option value="9">9</option><option value="10">10</option>
			                    <option value="11">11</option><option value="12">12</option>
			                    </select></td>
			                <td id="count3">0</td>
			                <td><select id="director"><option value="0">請選擇</option>
			                <option value="100">總幹事</option>
			                <option value="70">督導</option>
			                <option value="50">主任</option>
			                <option value="25">組長</option>
			                </select></td>
			                <td id="count4">0</td>
			                <td><input id="special" type="text" value="" size="3" /></td>
			                <td><input id="count5" type="text" value="0" size="3" /></td>
			                <td id="total">0</td>
			                <td id="totalSalary">0</td>
			            </tr>
			            <tr>
			                <th colspan="2">敘薪<br />說明</th>
			                <td colspan="13"><input id="explanation" type="text" value="" size="100" /></td>
			            </tr>
			        </table>
			        <p>歷史敘薪說明</p>
                    <table id="explanationList" class="tableText" width="780" border="0">
                        <thead>
                            <tr style="background-color:#F9AE56;">
                                <th width="100">日期</th>
                                <th width="680">敘薪說明</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="saveData(0)">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="saveData(1)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
