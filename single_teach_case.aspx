<%@ Page Language="C#" AutoEventWireup="true" CodeFile="single_teach_case.aspx.cs" Inherits="single_teach_case" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教學管理 - 短期目標課程計畫（教案） | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/single_teach_case.css" />
	<script type='text/javascript' src='http://code.jquery.com/jquery-1.7.2.min.js'></script>
	    
    <link rel="stylesheet" type="text/css" href="./css/jquery-ui-1.8.13.custom.css" />
    <link rel="stylesheet" type="text/css" href="./css/ui.dropdownchecklist.themeroller.css" />
    <script type="text/javascript" src="./js/jquery-ui-1.8.13.custom.min.js"></script>
    <script type="text/javascript" src="./js/ui.dropdownchecklist-1.4-min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/single_teach_case.js"></script>
	
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
			<div id="mainClass">教學管理&gt; 短期目標課程計畫（教案）</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./single_teach_case.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input type="text" name="name" /></td>
			                <td width="260">教師姓名 <input type="text" name="name" /></td>
			                <td width="260">計畫期程 <input class="date" type="text" name="name" size="10" />～<input class="date" type="text" name="name" size="10" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="120">編號</th>
			                    <th width="150">學生姓名</th>
			                    <th width="150">教師姓名</th>
			                    <th width="260">計畫期程</th>
			                    <th width="100">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>10123</td>
			                    <td>王小明</td>
			                    <td>連淑貞</td>
			                    <td>98.1.1~98.6.30</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>20123</td>
			                    <td>陳曉雯</td>
			                    <td>連淑貞</td>
			                    <td>97.6.1~97.12.31</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p align="right" style="background-color:#FFDF71;padding:0 10px;">台北至德</p>
			    <table class="tableText" width="780" border="0">
                    <tr>
		                <th width="200">學生姓名</th>
		                <td><input id="studentName" type="text" value="" /><span id="studentID" class="hideClassSpan"></span></td>
		            </tr>
		            <tr>
		                <th>教師姓名</th>
		                <td><input id="teacherName" type="text" value="" readonly="readonly" /><span id="teacherID" class="hideClassSpan"></span></td>
		            </tr>
		            <tr>
		                <th>計畫期程</th>
		                <td><input id="PlanDateStart" class="date" type="text" value="" size="10" />～<input id="PlanDateEnd" class="date" type="text" value="" size="10" />
		                <button id="BtnDateOk" class="btnAdd" onclick="TakeTPD()" >確認</button>
		                </td>
		            </tr>
			    </table>
			    
			  
			    <p class="cP">領域(一) 聽覺</p>
			  <%--  
                <table id="table1" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td>聽覺輔具　<button type="button" class="btnAdd" onclick="getAdd(1)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(1)">－</button></td>
                            <th width="86">執行日期</th>
                            <th width="82">評量方式</th>
                            <th width="82">學習表現</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="dataTR1">
                            <td colspan="4">
                                <table class="tableContact2" width="774" border="0">
                                    <tr>
                                        <th width="50">目標</th>
                                        <td width="480"><textarea class="short" cols="50" rows="3"></textarea></td>
                                        <td rowspan="2" width="84">
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" />
                                        </td>
                                        <td rowspan="2" width="80" class="wayTD">
                                            <select id="way111" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way112" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way113" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way114" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way115" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>
                                        </td>
                                        <td rowspan="2" width="80">
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>學習<br />進度</th>
                                        <td><textarea class="short" cols="50" rows="10"></textarea></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>--%>
                
                <table id="table1" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td>聽覺技巧　<button type="button" class="btnAdd" onclick="getAdd(1)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(1)">－</button></td>
                            <th width="86">執行日期</th>
                            <th width="82">評量方式</th>
                            <th width="82">學習表現</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="1_1dataTR">
                            <td colspan="4">
                                <table class="tableContact2" width="774" border="0">
                                    <tr>
                                        <th width="50">目標<button id="1_1remove" class="btnAdd" onclick="del(this);" >刪除</button><span id="1_1TPDID" class="hideClassSpan"></span></th>
                                        <td width="480"><textarea id="1_1TargetShort" class="short" cols="50" rows="3"></textarea></td>
                                        <td rowspan="2" width="84">
                                            <input id="1_1PlanExecutionDate1" class="date" type="text" value="" size="10" /><br />
                                            <input id="1_1PlanExecutionDate2" class="date" type="text" value="" size="10" /><br />
                                            <input id="1_1PlanExecutionDate3" class="date" type="text" value="" size="10" /><br />
                                            <input id="1_1PlanExecutionDate4" class="date" type="text" value="" size="10" /><br />
                                            <input id="1_1PlanExecutionDate5" class="date" type="text" value="" size="10" />
                                        </td>
                                        <td rowspan="2" width="80" >
                                            <select id="1_1Assessment1"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="1_1Assessment2"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="1_1Assessment3" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="1_1Assessment4" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="1_1Assessment5" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>
                                        </td>
                                        <td rowspan="2" width="80">
                                            <select id="1_1Performance1"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="1_1Performance2"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="1_1Performance3"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="1_1Performance4"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="1_1Performance5"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>學習<br />進度</th>
                                        <td><textarea id="1_1TargetContent" class="short" cols="50" rows="10"></textarea></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
                <p class="cP">領域(二) 認知語言及溝通技巧</p>
                
                <table id="table2" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td><button type="button" class="btnAdd" onclick="getAdd(2)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(2)">－</button></td>
                            <th width="86">執行日期</th>
                            <th width="82">評量方式</th>
                            <th width="82">學習表現</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="2_1dataTR">
                            <td colspan="4">
                                <table class="tableContact2" width="774" border="0">
                                    <tr>
                                        <th width="50">目標<button id="2_1remove" class="btnAdd" onclick="del(this);" >刪除</button><span id="2_1TPDID" class="hideClassSpan"></span></th>
                                        <td width="480"><textarea id="2_1TargetShort" class="short" cols="50" rows="3"></textarea></td>
                                        <td rowspan="2" width="84">
                                            <input id="2_1PlanExecutionDate1" class="date" type="text" value="" size="10" /><br />
                                            <input id="2_1PlanExecutionDate2" class="date" type="text" value="" size="10" /><br />
                                            <input id="2_1PlanExecutionDate3" class="date" type="text" value="" size="10" /><br />
                                            <input id="2_1PlanExecutionDate4" class="date" type="text" value="" size="10" /><br />
                                            <input id="2_1PlanExecutionDate5" class="date" type="text" value="" size="10" />
                                        </td>
                                        <td rowspan="2" width="80" >
                                            <select id="2_1Assessment1"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="2_1Assessment2"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="2_1Assessment3" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="2_1Assessment4" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="2_1Assessment5" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>
                                        </td>
                                        <td rowspan="2" width="80">
                                            <select id="2_1Performance1"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="2_1Performance2"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="2_1Performance3"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="2_1Performance4"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select id="2_1Performance5"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>學習<br />進度</th>
                                        <td><textarea id="2_1TargetContent" class="short" cols="50" rows="10"></textarea></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
<%--                <p class="cP">領域（三）其它特殊需求</p>
                
                <table id="table3" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td><button type="button" class="btnAdd" onclick="getAdd(3)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(3)">－</button></td>
                            <th width="86">執行日期</th>
                            <th width="82">評量方式</th>
                            <th width="82">學習表現</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="dataTR4">
                            <td colspan="4">
                                <table class="tableContact2" width="774" border="0">
                                    <tr>
                                        <th width="50">目標</th>
                                        <td width="480"><textarea class="short" cols="50" rows="3"></textarea></td>
                                        <td rowspan="2" width="84">
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" /><br />
                                            <input class="date" type="text" value="" size="10" />
                                        </td>
                                        <td rowspan="2" width="80" class="wayTD">
                                            <select id="way411" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way412" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way413" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way414" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
                                            <select id="way415" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>
                                        </td>
                                        <td rowspan="2" width="80">
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
                                            <select><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th>學習<br />進度</th>
                                        <td><textarea class="short" cols="50" rows="10"></textarea></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>--%>

                <table class="tableText" width="780" border="0">
                    <tr>
		                <td align="right">評量方式：觀察(A)　問答(B)　操作(C)　紙筆(D)　其他(E)，例如作業等<br />學習表現：完全達成（○）、協助下達成（＋）、協助下部份達成（＋－）、大多無法達成（－）<br /><br /></td>
		            </tr>
		            <tr>
		                <th>整體學習說明及其它備註說明：</th>
		            </tr>
		            <tr>
		                <td align="center"><textarea cols="100" rows="10"></textarea></td>
		            </tr>
			    </table>
                
			    <p class="btnP">
		            <button class="btnSave" onclick="Save(0)" type="button">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button">存 檔</button>
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