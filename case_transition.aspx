<%@ Page Language="C#" AutoEventWireup="true" CodeFile="case_transition.aspx.cs" Inherits="case_transition" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 轉銜服務 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/case_transition.css" />
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
    <script type="text/javascript" src="./js/jquery.form.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/case_transition.js" />
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
			<div id="mainClass">個案管理&gt; 轉銜服務</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./case_transition.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td>
			                <td width="260">轉銜年度 <input id="gosrhtransYear" type="text" value="" /></td>
			                <td width="260"><label><input name="gosrhtransActivity" type="checkbox" value="1" /> 會內舉辦之轉銜活動</label></td>
			            </tr>
			            <tr>
			                <td><label><input name="gosrhmeeting" type="checkbox" value="1" /> 參與IEP會議或輔導會議</label></td>
			                <td><label><input name="gosrhschoolVisit" type="checkbox" value="1" /> 校園宣導及到校訪視</label></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="2" align="center"><button class="btnSearch" type="button" onclick="search();">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">轉銜年度</th>
			                    <th width="150">學生姓名</th>
			                    <th width="100">轉銜年齡</th>
			                    <th width="140">轉銜階段</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p id="caseUnit" align="right" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
			    <form id="GmyForm" action="" method="post" enctype="multipart/form-data">
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="300">學生姓名</th>
			            <td colspan="2"><input id="studentName" type="text" value="" readonly="readonly"/><span id="studentID" class="hideClassSpan"></span><span class="startMark">*</span></td>
			        </tr>
		            <tr>
			            <th>年　　齡</th>
			            <td colspan="2"><input id="studentAge" type="text" size="5"/> 歲 <input id="studentMonth" type="text" size="5"/> 月</td>
			        </tr>
			        <tr>
			            <th>轉銜年度</th>
			            <td colspan="2"><input id="transYear" type="text" value="" size="10" /><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>轉銜階段</th>
			            <td colspan="2"><label><input type="radio" name="transStage" value="1" /> 銜接幼兒園</label>　　<label><input type="radio" name="transStage" value="2" /> 銜接國小</label>　　<label><input type="radio" name="transStage" value="3" /> 銜接特教學校</label></td>
			        </tr>
			        <tr>
			            <th>轉銜前就讀單位</th>
			            <td colspan="2"><input id="bSchool" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>轉銜後就讀單位</th>
			            <td colspan="2"><input id="aSchool" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>轉銜後教育單位主要聯絡人</th>
			            <td colspan="2"><input id="contact" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>轉銜後教育單位主要聯絡電話</th>
			            <td colspan="2"><input id="contactTel" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>會內舉辦之轉銜活動</th>
			            <td width="100"><select id="transActivity"><option value="0">請選擇</option><option value="1">是</option><option value="2">否</option></select></td>
			            <td>
			                <table class="tableContact" width="380" border="0">
			                    <tr>
			                        <th>活動名稱</th>
			                        <th>活動日期</th>
			                        <th>活動內容</th>
			                    </tr>
			                    <tr>
			                        <td><input id="transActivityName1" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityDate1" class="date" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityContent1" type="text" value="" size="15" /></td>
			                    </tr>
			                    <tr>
			                        <td><input id="transActivityName2" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityDate2" class="date" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityContent2" type="text" value="" size="15" /></td>
			                    </tr>
			                    <tr>
			                        <td><input id="transActivityName3" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityDate3" class="date" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityContent3" type="text" value="" size="15" /></td>
			                    </tr>
			                    <tr>
			                        <td><input id="transActivityName4" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityDate4" class="date" type="text" value="" size="15" /></td>
			                        <td><input id="transActivityContent4" type="text" value="" size="15" /></td>
			                    </tr>
			                </table>
			            </td>
			        </tr>
			        <tr>
			            <th>提供教育單位及鑑定安置訊息</th>
			            <td><select id="antonMessage"><option value="0">請選擇</option><option value="1">是</option><option value="2">否</option></select></td>
			            <td>&nbsp;</td>
			        </tr>
			        <tr>
			            <th>確定就讀之教育單位後，<br />發公文至該單位參與個案IEP或輔導會議</th>
			            <td><select id="sendDocument"><option value="0">請選擇</option><option value="1">是</option><option value="2">否</option></select></td>
			            <td>&nbsp;</td>
			        </tr>
			        <tr>
			            <th>轉銜報告書</th>
			            <td><select id="transReport"><option value="0">請選擇</option><option value="1">是</option><option value="2">否</option></select></td>
			            <td><span id="transReportFile">轉銜報告書</span> <input type="file" name="transReportFile"  /><br />
			            <span id="transFamilyReportFile" >轉銜家庭報告</span> <input type="file" name="transFamilyReportFile" /></td>
			        </tr>
			        <tr>
			            <th>參與IEP會議或輔導會議</th>
			            <td><select id="meeting"><option value="0">請選擇</option><option value="1">是</option><option value="2">否</option></select></td>
			            <td><a id="meetingVisitReportUrl" href="#" target="_blank" style="display:none;">訪視紀錄</a><input type="button" value="選擇訪視紀錄" onclick="callStudentViewSearchfunction(1);"/> <input type="button" value="清除" onclick="callRemoveStudentViewSearchfunction(1);"/><span id="meetingVisitReport" class="hideClassSpan"></span></td>
			        </tr>
			        <tr>
			            <th>追蹤案主學校學習與適應狀況</th>
			            <td><select id="adaptation"><option value="0">請選擇</option><option value="1">是</option><option value="2">否</option></select></td>
			            <td><a id="adaptationReportUrl" href="#" target="_blank" style="display:none;">個案服務紀錄</a><input type="button" value="選擇個案服務紀錄" onclick="callStudentCaseSearchfunction();"/> <input type="button" value="清除" onclick="callRemoveStudentCaseSearchfunction();"/><span id="adaptationReport" class="hideClassSpan"></span></td>
			        </tr>
			        <tr>
			            <th>校園宣導及到校訪視</th>
			            <td><select id="schoolVisit"><option value="0">請選擇</option><option value="1">是</option><option value="2">否</option></select></td>
			            <td><span id="schooladvocacyReportUrl" >校園宣導報告</span> <input type="file" name="schooladvocacyReport"  /><br />
			                <a id="schoolVisitRecordUrl" href="#" target="_blank" style="display:none;">訪視紀錄</a><input type="button" value="選擇訪視紀錄" onclick="callStudentViewSearchfunction(2);"/> <input type="button" value="清除" onclick="callRemoveStudentViewSearchfunction(2);"/><span id="schoolVisitRecord" class="hideClassSpan"></span></td>
			        </tr>
			    </table>
			    </form>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveData(0);">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="saveData(1);">存 檔</button>
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
