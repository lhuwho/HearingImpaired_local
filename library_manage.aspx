<%@ Page Language="C#" AutoEventWireup="true" CodeFile="library_manage.aspx.cs" Inherits="library_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>行政管理 - 圖書管理 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/library_manage.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/library_manage.js"></script>
	
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/pagination.css" />
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
			<div id="mainClass">行政管理&gt; 圖書管理</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">圖書明細</div>
			    <div id="btnInsert" class="menuTabs2">圖書新增</div>
			    <a href="./library_system.aspx" target="_blank"><div id="btnReturn" class="menuTabs2">借還管理</div></a>
			    <div id="btnIndex" class="menuTabs2">圖書借出天數查詢</div>
			    <div id="btnIndex2" class="menuTabs2">圖書借閱日期統計</div>
			    <div id="btnIndex3" class="menuTabs2">圖書借閱書目統計</div>
			    <div id="btnIndex4" class="menuTabs2">圖書借閱統計</div>
			</div>
		    <div id="mainContentSearch">
		        <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">書　　碼 <input id="gosrhbookNumber" type="text" value="" autocomplete="off" /></td>
			                <td width="260">書　　名 <input id="gosrhbookTitle" type="text" value="" autocomplete="off" /></td>
			                <td width="260">分　　類 <select id="gosrhbookClassification" name="Category" style="width:180px;" autocomplete="off"><option value="0">請選擇分類</option></select></td>
			            </tr>
			            <tr>
			                <td>作　　者 <input id="gosrhbookAuthor" type="text" value="" autocomplete="off" /></td>
			                <td>出 版 社 <input id="gosrhbookPress" type="text" value="" autocomplete="off" /></td>
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
			                    <th width="70">書碼</th>
			                  <%--  <th width="50">用途</th>--%>
			                    <th width="170">分類</th>
			                    <th width="120">書名</th>
			                    <th width="90">作者</th>
			                    <th  width="90">出版社</th>
			                    <th  width="80">出版日期</th>
			                    <th width="140">備註</th>
			                   <%-- <th width="40">轉出<br />報廢</th>--%>
			                    <th width="50">狀態</th> 
			                    <th  width="50">功能</th>
			                </tr>
			                <%--<tr>
			                    <th width="70">轉出/報廢</th>
			                    <th width="170">用途</th>
			                    <th width="120">來源</th>
			                    <th width="90">捐贈者</th>
			                </tr>--%>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination" class="pagination"></div>
		    </div>
		    <div id="mainContentInsert" style="display:none;">
		        <div>
		            <p id="Unit" style="background-color:#FFDF71;padding:0 10px;text-align:right;">&nbsp;</p>
		            <p align="right">建檔日期 <input id="bookFilingDate" class="date" type="text" value="" size="10"  /></p>
		            <table class="tableText" width="780" border="0" id="insertDataDiv">
		                <tr>
		                    <th>用　　途</th>
		                    <td><select id="bookUseTo" autocomplete="off"><option value="0">請選擇</option><option value="1">外借</option><option value="2">內用</option></select></td>
		                </tr>
		                <tr>
		                    <th width="150">分　　類</th>
		                    <td><select id="bookClassification" name="Category" autocomplete="off"><option value="0">請選擇分類</option></select><span class="startMark">*</span></td>
		                </tr>
		                <tr>
		                    <th>書　　名</th>
		                    <td><input id="bookTitle" type="text" value="" size="60" autocomplete="off" /><span class="startMark">*</span></td>
		                </tr>
		                <tr>
		                    <th>作　　者</th>
		                    <td><input id="bookAuthor" type="text" value="" size="30" autocomplete="off" /></td>
		                </tr>
		                <tr>
		                    <th>出 版 社</th>
		                    <td><input id="bookPress" type="text" value="" size="30" autocomplete="off" /></td>
		                </tr>
		                <tr>
		                    <th>出版日期</th>
		                    <td><input id="bookPressDate" type="text" value="" class="date" size="10" autocomplete="off" /></td>
		                </tr>
		                <tr>
		                    <th>圖書來源</th>
		                    <td><input id="bookComefrom" type="text" value="" size="10" autocomplete="off" /></td>
		                </tr>
		                <tr>
		                    <th>捐 贈 者</th>
		                    <td><input id="bookGeter" type="text" value="" size="30" autocomplete="off" /></td>
		                </tr>
		                 <tr>
		                    <th>轉出/報廢</th>
		                    <td><select  id="bookScrapstatus" autocomplete="off"><option value="0">請選擇</option><option value="1">轉出</option><option value="2">報廢</option></select></td>
		                </tr>
		                 <tr>
		                    <th>備　　註</th>
		                    <td><textarea id="bookRemark" autocomplete="off"></textarea></td>
		                </tr>
		            </table>
		            <p class="btnP">
	                    <button class="btnCancel" type="button" onclick="clearInsert()">重 填</button>　　
	                    <button class="btnSave" type="button" onclick="saveInsert()">新 增</button>
	                </p>
	            </div>
		    </div>
		    <div id="mainContentIndex" style="display:none;">
		        <div id="mainIndexForm">
			        <table width="780" border="0" id="searchTable2">
			            <tr>
			                <td width="260">借閱類別 <select id="gosrhbookDayType" autocomplete="off"><option value="0">請選擇</option><option value="1">員工</option><option value="2">學生</option></select></td>
			                <td width="260">借出天數 <input id="gosrhbookStartDay" type="text" value="" size="5" autocomplete="off" maxlength="2" />～<input id="gosrhbookEndDay" type="text" value="" size="5" autocomplete="off" maxlength="2" /></td>
			                <td width="260">&nbsp</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnIndex" type="button" onclick="showView(2)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">借閱人姓名</th>
			                    <th width="110">書碼</th>
			                    <th width="170">書名</th>
			                    <th width="90">借閱日期</th>
			                    <th width="90">到期日</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPaginationIndex" class="pagination"></div>
		    </div>
		    <div id="mainContentIndex2" style="display:none;">
		        <div id="mainIndex2Form">
			        <table width="780" border="0" id="searchTable3">
			            <tr>
			                <td width="260">統計期間 <input id="gosrhbookDateStartDate" class="date" type="text" value="" size="10" autocomplete="off" />～<input id="gosrhbookDateEndDate" class="date" type="text" value="" size="10" autocomplete="off" /></td>
			                <td width="260">&nbsp</td>
			                <td width="260">&nbsp</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnIndex2" type="button" onclick="showView(3)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList2" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="195">學生圖書借閱數量</th>
			                    <th width="195">學生借閱人數</th>
			                    <th width="195">員工圖書借閱數量</th>
			                    <th width="195">員工借閱人數</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
		    </div>
		    <div id="mainContentIndex3" style="display:none;">
		        <div id="mainIndex3Form">
			        <table width="780" border="0" id="searchTable4">
			            <tr>
			                <td width="260">統計期間 <input id="gosrhrecordBookStartDate" class="date" type="text" value="" size="10" autocomplete="off" />～<input id="gosrhrecordBookEndDate" class="date" type="text" value="" size="10" autocomplete="off" /></td>
			                <td width="260">書　　碼 <input id="gosrhrecordBookCode" type="text" value="" readonly="readonly" autocomplete="off" /><span id="txtrecordBookID" class="hideClassSpan"></span></td>
			                <td width="260">書　　名 <input id="recordBookName" type="text" value="" readonly="readonly" disabled="disabled" autocomplete="off" /></span></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnIndex3" type="button" onclick="showView(4)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList3" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="80">身分別</th>
			                    <th width="70">借閱人ID</th>
			                    <th width="100">借閱人姓名</th>
			                    <th width="100">書碼</th>
			                    <th width="230">書目</th>
			                    <th width="100">借出日期</th>
			                    <th width="100">還書日期</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPaginationIndex3" class="pagination"></div>
		    </div>
		    <div id="mainContentIndex4" style="display:none;">
		        <div id="mainIndex4Form">
			        <table width="780" border="0" id="searchTable5">
			            <tr>
			                <td width="260">統計期間 <input id="gosrhrecordBorrowerStartDate" class="date" type="text" value="" size="10" autocomplete="off" />～<input id="gosrhrecordBorrowerEndDate" class="date" type="text" value="" size="10" autocomplete="off" /></td>
			                <td width="260">&nbsp</td>
			                <td width="260">&nbsp</td>
			            </tr>
			            <tr>
			                <td>借閱類別 <select id="gosrhrecordBorrowerType" autocomplete="off"><option value="0">請選擇</option><option value="1">員工</option><option value="2">學生</option></select></td>
			                <td>借閱人姓名 <input id="gosrhrecordBorrowerName" type="text" value="" autocomplete="off" /></td>
			                <td><div style="display:none;">班　　別 <input id="gosrhrecordBorrowerClassName" type="text" value="" autocomplete="off" /></div></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnIndex4" type="button" onclick="showView(5)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList4" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="160">身分別</th>
			                    <th width="160" id="BorrowerClassTitle" style="display: none;">班別</th>
			                    <th width="160">借閱人ID</th>
			                    <th width="160">借閱人姓名</th>
			                    <th>借書數量</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPaginationIndex4" class="pagination"></div>
		    </div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
