<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_tracked.aspx.cs" Inherits="student_tracked" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 離會學生追蹤 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/student_tracked.css" />
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
                <asp:ScriptReference Path="~/js/student_tracked.js" />
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
			<div id="mainClass">個案管理&gt; 離會學生追蹤</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <!-- <a href="./student_tracked.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a> -->
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input type="text" id="gosrhstudentName" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>結案日期 <input id="gosrhendReasonDatestart" class="date" type="text" value="" size="10" />～<input id="gosrhendReasonDateend" class="date" type="text" value="" size="10" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search();">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">服務使用者編號</th>
			                    <th width="100">學生姓名</th>
			                    <th width="100">結案日期</th>
			                    <th width="140">結案原因</th>
			                    <th width="100">電話</th>
			                    <th width="200">E-mail</th>
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
			    <p align="right" id="caseUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
			    
			    <p class="cP">一、基本資料</p>
			    <table id="item1Content" class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">學生姓名</th>
			            <td><input id="studentName" type="text" value="" readonly="readonly"/><span id="studentID" class="hideClassSpan"></span><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td id="studentbirthday">&nbsp;</td>
		            </tr>
			        <tr>
			            <th>性　　別</th>
			            <td id="studentSex">&nbsp;</td>
			        </tr>
			        <tr>
			            <th>結案日期</th>
			            <td id="endReasonDate">&nbsp;</td>
			        </tr>
		            <tr>
			            <th>通訊地址</th>
			            <td colspan="3"><input id="addressZip" type="text" maxlength="5" value=""  size="5"/> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" value="" size="50"/></td>
			        </tr>
			        <tr>
			            <th>連絡電話</th>
			            <td><input id="Tel" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>E-mail</th>
			            <td><input id="email" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>障礙類型</th>
			            <td>身心障礙類別 <input type="text" id="manualCategory1" value="" size="15" /> 等級 <input type="text" id="manualGrade1" value="" size="15" /><br />
			                身心障礙類別 <input type="text" id="manualCategory2" value="" size="15" /> 等級 <input type="text" id="manualGrade2" value="" size="15" /><br />
			                身心障礙類別 <input type="text" id="manualCategory3" value="" size="15" /> 等級 <input type="text" id="manualGrade3" value="" size="15" />
			            </td>
			        </tr>
			        <tr>
			            <th>輔具型式與序號</th>
			            <td>
			                右耳：<label><input type="radio" name="assistmanageR" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageR" value="2" /> 電子耳</label>　
			                廠牌 <select id="brandR" ><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelR" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeR" value="" /> 選配/植入地點 <input type="text"  id="buyingPlaceR" value="" /><br />　　　
			                植入醫院醫生 <input type="text"  id="insertHospitalR" size="15" value="" /> 開頻日 <input class="date" type="text" id="openHzDateR" value="" size="10" /><br />
			                左耳：<label><input type="radio" name="assistmanageL" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageL" value="2" /> 電子耳</label>　
			                廠牌<select id="brandL"><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelL" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeL" value="" /> 選配/植入地點 <input type="text" id="buyingPlaceL" value="" /><br />　　　
			                植入醫院醫生 <input type="text" id="insertHospitalL" value="" size="15" /> 開頻日 <input class="date" type="text" id="openHzDateL" value="" size="10" /><br />
			           </td>
			        </tr>
			    </table>
			    <p class="cP">二、就學/就業狀況</p>
			    <table id="item2Content" class="tableText" width="780" border="0">
			        <tr style="background-color:#F9AE56;">
			            <th width="170">就學階段/就業</th>
			            <th width="80">就學狀況</th>
			            <th width="530">就讀學校/職稱</th>
			        </tr>
			        <tr>
			            <th>小　　學</th>
			            <td><select id="esType"><option value="0">請選擇</option><option value="1">就學中</option><option value="2">畢業</option><option value="3">肄業</option></select></td>
		                <td><input type="text" id="esName" value="" /></td>
		            </tr>
			        <tr>
			            <th>國　　中</th>
			            <td><select id="jsType"><option value="0">請選擇</option><option value="1">就學中</option><option value="2">畢業</option><option value="3">肄業</option></select></td>
			            <td><input type="text" id="jsName" value="" /></td>
			        </tr>
			        <tr>
			            <th>高　　中</th>
			            <td><select id="hsType"><option value="0">請選擇</option><option value="1">就學中</option><option value="2">畢業</option><option value="3">肄業</option></select></td>
			            <td><input type="text" id="hsName" value="" /></td>
			        </tr>
			        <tr>
			            <th>大　　學</th>
			            <td><select id="uType"><option value="0">請選擇</option><option value="1">就學中</option><option value="2">畢業</option><option value="3">肄業</option></select></td>
			            <td><input type="text" id="uName" value="" /></td>
			        </tr>
			        <tr>
			            <th>就業單位</th>
			            <td>&nbsp;</td>
			            <td><input type="text" id="jobUnit" value="" /></td>
			        </tr>
			        <tr>
			            <th>其　　他</th>
			            <td><select id="otherType"><option value="0">請選擇</option><option value="1">就學中</option><option value="2">畢業</option><option value="3">肄業</option></select></td>
			            <td><input type="text" id="otherName" value="" /></td>
			        </tr>
			    </table>
			    <p class="cP">三、追蹤紀錄</p>
			    <table class="tableText2" width="780" border="0"  id="TeackTable">
			        <thead>
			            <tr style="background-color:#F9AE56;">
			                <th width="100">追蹤日期</th>
			                <th width="100">追蹤人員</th>
			                <th width="470">追蹤內容</th>
			                <th width="110">功能</th>
			            </tr>
			        </thead>
			        <tbody>
			        </tbody>
			    </table>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData()">新 增</button></p>
		        <div id="insertDataDiv" style="display:none">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
		                        <th width="95">追蹤日期</th>
		                        <th width="95">追蹤人員</th>
		                        <th width="95">追蹤內容</th>
		                        <th width="110">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr >
		                        <td><input id="tDate" class="date" type="text" value="" size="10" /></td>
		                        <td><input id="tStaffName" type="text" value="" size="10" readonly="readonly" ><span id="tStaff" class="hideClassSpan"></span></td>
		                        <td><textarea id="tContent" ></textarea></td>
		                        <td><button class="btnView" type="button" onclick="InsertTrackData();">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert()">取 消</button></td>
		                    </tr>
		                 </tbody>
		            </table>
		        </div>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveOffData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="saveOffData(1)">存 檔</button>
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
