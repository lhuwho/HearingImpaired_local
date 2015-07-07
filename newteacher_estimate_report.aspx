<%@ Page Language="C#" AutoEventWireup="true" CodeFile="newteacher_estimate_report.aspx.cs" Inherits="newteacher_estimate_report" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人事管理 - 新進教師考評成績 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/newteacher_estimate_report.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/newteacher_estimate_report.js"></script>
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
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
			<div id="mainClass">人事管理&gt; 新進教師考評成績</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./newteacher_estimate_report.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">教師姓名 <input id="gosrhTeacher" type="text" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhTeacherSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
			                <td>機 構 別 <select id="gosrhTeacherUnit"></select></td>
			            </tr>
			            <tr>
			               <td width="260">受評日期 <input id="gosrhassessmentStart" class="date" type="text" value="" size="10" />～<input id="gosrhassessmentEnd" class="date" type="text" value="" size="10" /></td>    
			                <td colspan="2">&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="searchData();">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">排序</th>
			                    <th width="150">受評教師</th>
			                    <th width="150">受評日期</th>
			                    <th width="150">機構別</th>
			                    <th width="80">功能</th>
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
			    <p id="Unit" align="right" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">受評者</th>
			            <td colspan="3"><input id="teacherName" class="getstaffData" type="text" value=""/> 教師<span class="hideClassSpan" id="teacher"></span></td>
			        </tr>
			        <tr>
			            <th>入會時間</th>
			            <td colspan="3" id="officeDate"></td>
		            </tr>
		            <tr>
			            <th>受評時間</th>
			            <td colspan="3"><input id="evaluationDate" class="date" type="text" value="" size="10"/></td>
		            </tr>
		            <tr>
		                <td colspan="4">
		                    <p><br />一、考評成績</p>
		                    <table class="tableContact" width="780" border="0">
		                        <tr>
		                            <th>個案報告20%</th>
		                            <th>基礎理論40%</th>
		                            <th>實務教學40%</th>
		                            <th width="100px">總成績</th>
		                            <th width="250px">評鑑結果</th>
		                        </tr>
		                        <tr>
		                            <td><input id="reportPer" type="text" value="" size="10" /></td>
		                            <td><input id="basicPer" type="text" value="" size="10" /></td>
		                            <td><input id="teachPer" type="text" value="" size="10" /></td>
		                            <td id="sunPer"></td>
		                            <td><input id="rated" type="text" value="" size="30"/></td>
		                        </tr>
		                    </table>
		                </td>
		            </tr>
		            <tr>
		                <td colspan="4">
		                    <p><br />二、評鍵結果及建議(個別課)</p>
		                    <table class="tableContact" width="780" border="0">
		                        <tr>
		                            <td>&nbsp;</td>
		                            <th>優點</th>
		                            <th>建議</th>
		                        </tr>
		                        <tr>
		                            <th>教學內容設計</th>
		                            <td><textarea cols="40" rows="3" id="contentMerit"></textarea></td>
		                            <td><textarea cols="40" rows="3" id="contentDefect"></textarea></td>
		                        </tr>
		                        <tr>
		                            <th>教具選則及運用</th>
		                            <td><textarea cols="40" rows="3" id="useMerit"></textarea></td>
		                            <td><textarea cols="40" rows="3" id="useDefect"></textarea></td>
		                        </tr>
		                        <tr>
		                            <th>教學語言使用</th>
		                            <td><textarea cols="40" rows="3" id="langMerit"></textarea></td>
		                            <td><textarea cols="40" rows="3" id="langDefect"></textarea></td>
		                        </tr>
		                        <tr>
		                            <th>教學技巧</th>
		                            <td><textarea cols="40" rows="3" id="skillMerit"></textarea></td>
		                            <td><textarea cols="40" rows="3" id="skillDefect"></textarea></td>
		                        </tr>
		                        <tr>
		                            <th>師生互動關係</th>
		                            <td><textarea cols="40" rows="3" id="ExchangeMerit"></textarea></td>
		                            <td><textarea cols="40" rows="3" id="ExchangeDefect"></textarea></td>
		                        </tr>
		                        <tr>
		                            <th>家長諮詢</th>
		                            <td><textarea cols="40" rows="3" id="AdvisoryMerit"></textarea></td>
		                            <td><textarea cols="40" rows="3" id="AdvisoryDefect"></textarea></td>
		                        </tr>
		                        <tr>
		                            <th>整體表現</th>
		                            <td><textarea cols="40" rows="3" id="OverallMerit"></textarea></td>
		                            <td><textarea cols="40" rows="3" id="OverallDefect"></textarea></td>
		                        </tr>
		                    </table>
		                </td>
		            </tr>
		            <tr>
		                <th>總幹事</th>
		                <td><input type="text" value="" size="15"/></td>
		                <th>督導</th>
		                <td><input id="prison" type="text" value="" size="15"/></td>
		            </tr>
		            <tr>
		                <th>培訓負責人</th>
		                <td><input id="trainPersonName" class="getstaffData"  type="text" value="" size="15"/><span id="trainPerson" class="hideClassSpan" ></span></td>
		                <th>整理者</th>
		                <td><input id="fileName" class="getstaffData"  type="text" value="" size="15"/><span id="file" class="hideClassSpan" ></span></td>
		            </tr>
			    </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="saveData(1)">存 檔</button>
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

