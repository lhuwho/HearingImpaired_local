<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_upgrade_data.aspx.cs" Inherits="staff_upgrade_data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人事管理 - 員工升等資料查詢 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/staff_upgrade_data.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/chosen/chosen.css" />
	<script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/pagination.css" />
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/staff_upgrade_data.js"></script>
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
			<div id="mainClass">人事管理&gt; 員工升等資料查詢</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">教師升等在職進修學分</div>
			    <div id="btnIndex" class="menuTabs2">教師升等專業表現</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">教師姓名 <input id="gosrhstaffName" type="text" value=""/></td>
			                <td width="260">研習證明 <select id="gosrhProve"><option value="0">請選擇</option><option value="1">會內研習</option><option value="2">會外研習</option><option value="3">委派出席</option></select></td>
                            <td width="260">講師姓名 <input id="gosrhLecturer" type="text" value=""/></td>
			            </tr>
			            <tr>
			                <td>日　　期 <input id="gosrhDateStart" class="date" type="text" value="" size="10" />～<input id="gosrhDateEnd" class="date" type="text" value="" size="10" /></td>
			                <td>累計學分數 <input id="SumCredit" type="text" value="" size="10" /></td>
                            <td>&nbsp</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(1)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="85">日期</th>
			                    <th width="85">講師</th>
			                    <th width="120">課程主題</th>
			                    <th width="60">時數</th>
			                    <th width="100">研習證明</th>
			                    <th width="60">本會<br />學分數</th>
			                    <th width="140">備註</th>
			                    <th width="60">參與<br />同仁</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="showInsert(1)">新 增</button></p>
			    <div id="insertDataDiv" class="insertDataDiv">
			        <table class="tableList" width="780" border="0" id="setStaffCredit">
			            <thead>
			                <tr>
			                    <th width="85">日期</th>
			                    <th width="85">講師</th>
			                    <th width="100">課程主題</th>
			                    <th width="40">時數</th>
			                    <th width="80">研習證明</th>
			                    <th width="60">本會<br />學分數</th>
			                    <th width="170">參加同仁</th>
			                    <th width="80">備註</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td><input id="courseDate" class="date" type="text" value="" size="9" /></td>
			                    <td><input id="courseLecturer" type="text" value="" size="9" /></td>
			                    <td><input id="courseName" type="text" value="" size="" /></td>
			                    <td><input id="courseTime" type="text" value="" size="3" /></td>
			                    <td><select id="courseProve" ><option value="0">請選擇</option><option value="1">會內研習</option><option value="2">會外研習</option><option value="3">委派出席</option></select></td>
			                    <td><input id="courseCredit" type="text" value="" size="5" /></td>
			                    <td>
			                        <select id="ParticipantsSelect" name="item" data-placeholder="選擇教師..." class="chosen-select" multiple="multiple">
					                </select>
			                    </td>
			                    <td><textarea id="otherExplanation" rows="1" cols="7"></textarea></td>
			                    <td><div class="UD"><button class="btnView" type="button" onclick="SaveStaffCreditData();">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert(1)">取 消</button></div>
			                    </td>
			                </tr>
                        </tbody>
                    </table>
                </div>
			</div>
			<div id="mainContentIndex" style="display:none;">
		        <div id="mainIndexForm">
			        <table width="780" border="0" id="searchTable2">
			            <tr>
			                <td width="260">教師姓名 <input id="gosrhstaffName1" type="text" value=""/></td>
			                <td width="260">日　　期 <input id="gosrhDateStart1" class="date" type="text" value="" size="10" />～<input id="gosrhDateEnd1" class="date" type="text" value="" size="10" /></td>
			                <td width="260">類　　別 <select id="gosrhType"><option value="0">請選擇</option><option value="1">專業文章</option><option value="2">學術發表</option><option value="3">親子</option></select></td>
                        </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(2)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="90">教師姓名</th>
			                    <th width="100">日期</th>
			                    <th width="70">數量</th>
			                    <th width="150">刊物名稱</th>
			                    <th width="70">期數/月份</th>
			                    <th width="140">文章題目</th>
			                    <th width="100">類別</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination1" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="showInsert(2)">新 增</button></p>
			    <div id="insertDataDiv2" class="insertDataDiv">
			        <table class="tableList" width="780" border="0" id="setSerial">
			            <thead>
			                <tr>
			                    <th width="135">教師姓名</th>
			                    <th width="95">日期</th>
			                    <th width="60">數量</th>
			                    <th width="155">刊物名稱</th>
			                    <th width="60">期數/月份</th>
			                    <th width="125">文章題目</th>
			                    <th width="90">類別</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td><select id="author" class="chosen-select" data-placeholder="選擇教師..." ><option value=""></option></select></td>
			                    <td><input id="articleDate" class="date" type="text" value="" size="10" /></td>
			                    <td><input id="serialNumber" type="text" value="" size="5" /></td>
			                    <td><input id="seriesTitle" type="text" value="" /></td>
			                    <td><input id="volume" type="text" value="" size="5" /></td>
			                    <td><input id="articleTitle" type="text" value="" size="15" /></td>
			                    <td><select id="articleType"><option value="0">請選擇</option><option value="1">專業文章</option><option value="2">學術發表</option><option value="3">親子</option></select></td>
			                    <td><div class="UD"><button class="btnView" type="button" onclick="SaveSerialData()">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert(2)">取 消</button></div>
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