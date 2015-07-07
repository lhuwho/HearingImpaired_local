<%@ Page Language="C#" AutoEventWireup="true" CodeFile="group_teach_case_item.aspx.cs" Inherits="group_teach_case_item" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教學管理 - 團體班目標課程計畫（模板） | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/group_teach_case_item.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
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
                <asp:ScriptReference Path="~/js/group_teach_case_item.js" />
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
			<div id="mainClass">教學管理&gt; 團體班目標課程計畫（模板）</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./group_teach_case_item.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">班　　別 <input id="gosrhClassName" type="text" value="" /></td>
			                <td width="260">教師姓名 <input id="gosrhTeacherName" type="text" value="" /></td>
			                <td width="260">課別名稱 <input id="gosrhCourseName" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>計畫期程 <input id="gosrhSPeriod" class="date" type="text" value="" size="10" />～<input id="gosrhEPeriod" class="date" type="text" value="" size="10" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
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
			                    <th width="60">編號</th>
			                    <th width="100">班別</th>
			                    <th width="130">教師姓名</th>
			                    <th width="120">課程名稱</th>
			                    <th width="200">計畫期程</th>
			                    <th width="90">部別單位</th>
			                    <th width="100">功能</th>
			                </tr>
			            </thead>
			            <tbody></tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p id="unitName" align="right" style="background-color:#FFDF71;padding:0 10px;">台北至德</p>
			    <table class="tableText" width="780" border="0">
                    <tr>
		                <th width="200">班　　別</th>
		                <td><input id="className" type="text" value="" /><span id="classNameID" style="display:none;"></span></td>
		            </tr>
		            <tr>
		                <th>教師姓名</th>
		                <td><input id="teacherName" type="text" value="" /><span id="teacherNameID" style="display:none;"></span></td>
		            </tr>
		            <tr>
		                <th>課程名稱</th>
		                <td><input id="courseName" type="text" value="" /><span id="courseNameID" style="display:none;"></span></td>
		            </tr>
		            <tr>
		                <th>計畫期程</th>
		                <td><input id="startPeriod" class="date" type="text" value="" size="10" />～<input id="endPeriod" class="date" type="text" value="" size="10" /></td>
		            </tr>
			    </table>
			    
			    <p class="cP">領域(一) 聽覺</p>
			    
                <table id="table1" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td colspan="2">聽覺輔具　<button type="button" class="btnAdd" onclick="getAdd(1)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(1)">－</button></td>
                        </tr>
                        <tr>
                            <th width="300">長期</th>
                            <th width="480">短期</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="dataTR10">
                            <td colspan="2">
                                <table class="tableContact2" width="760" border="0">
                                    <tr>
                                        <td rowspan="5" width="290"><textarea class="long" cols="40" rows="5"></textarea><span class="longID"></span></td>
                                        <td width="470"><textarea class="short0" cols="50" rows="3"></textarea><span class="short0ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short1" cols="50" rows="3"></textarea><span class="short1ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short2" cols="50" rows="3"></textarea><span class="short2ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short3" cols="50" rows="3"></textarea><span class="short3ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short4" cols="50" rows="3"></textarea><span class="short4ID"></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
                <table id="table2" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td colspan="2">聽覺技巧　<button type="button" class="btnAdd" onclick="getAdd(2)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(2)">－</button></td>
                        </tr>
                        <tr>
                            <th width="300">長期</th>
                            <th width="480">短期</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="dataTR20">
                            <td colspan="2">
                                <table class="tableContact2" width="760" border="0">
                                    <tr>
                                        <td rowspan="5" width="290"><textarea class="long" cols="40" rows="5"></textarea><span class="longID"></span></td>
                                        <td width="470"><textarea class="short0" cols="50" rows="3"></textarea><span class="short0ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short1" cols="50" rows="3"></textarea><span class="short1ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short2" cols="50" rows="3"></textarea><span class="short2ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short3" cols="50" rows="3"></textarea><span class="short3ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short4" cols="50" rows="3"></textarea><span class="short4ID"></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
                <p class="cP">領域(二) 認知語言及溝通技巧</p>
                
                <table id="table3" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td colspan="2"><button type="button" class="btnAdd" onclick="getAdd(3)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(3)">－</button></td>
                        </tr>
                        <tr>
                            <th width="300">長期</th>
                            <th width="480">短期</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="dataTR30">
                            <td colspan="2">
                                <table class="tableContact2" width="760" border="0">
                                    <tr>
                                        <td rowspan="5" width="290"><textarea class="long" cols="40" rows="5"></textarea><span class="longID"></span></td>
                                        <td width="470"><textarea class="short0" cols="50" rows="3"></textarea><span class="short0ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short1" cols="50" rows="3"></textarea><span class="short1ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short2" cols="50" rows="3"></textarea><span class="short2ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short3" cols="50" rows="3"></textarea><span class="short3ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short4" cols="50" rows="3"></textarea><span class="short4ID"></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
                <p class="cP">領域(三) 其它特殊需求</p>
                
                <table id="table4" class="tableContact" width="780" border="0">
                    <thead>
                        <tr>
                            <td colspan="2"><button type="button" class="btnAdd" onclick="getAdd(4)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(4)">－</button></td>
                        </tr>
                        <tr>
                            <th width="300">長期</th>
                            <th width="480">短期</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr id="dataTR40">
                            <td colspan="2">
                                <table class="tableContact2" width="760" border="0">
                                    <tr>
                                        <td rowspan="5" width="290"><textarea class="long" cols="40" rows="5"></textarea><span class="longID"></span></td>
                                        <td width="470"><textarea class="short0" cols="50" rows="3"></textarea><span class="short0ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short1" cols="50" rows="3"></textarea><span class="short1ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short2" cols="50" rows="3"></textarea><span class="short2ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short3" cols="50" rows="3"></textarea><span class="short3ID"></span></td>
                                    </tr>
                                    <tr>
                                        <td><textarea class="short4" cols="50" rows="3"></textarea><span class="short4ID"></span></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                
			    <p class="btnP">
		            <button class="btnSave" type="button">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button"  onclick="setaCoursePlanTemplate()">存 檔</button>
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
