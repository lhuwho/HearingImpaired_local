<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teach_service_check.aspx.cs" Inherits="teach_service_check" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教學管理 - 教學服務督導 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/teach_service_check.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
		    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/teach_service_check.js"></script>
	
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
			<div id="mainClass">教學管理&gt; 教學服務督導</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./teach_service_check.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
                     <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input type="text" value="" id="gosrhstudentName" /></td>
			                <td width="260">性　　別 <select name="name" id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" id="gosrhbirthdaystart"  type="text" value="" size="10" />～<input class="date" id="gosrhbirthdayend" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>年　　度 <input  id="gosrhAcademicYearstart"   type="text" value="" size="5" />～ <input id="gosrhAcademicYearend" type="text" value="" size="5" /></td>
			                <td>教師姓名 <input id="gosrhTeacherName"  type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" onclick="Search()" type="button">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="120">排序</th>
			                    <th width="120">年度</th>
			                    <th width="120">授課日期</th>
			                    <th width="120">學生姓名</th>
			                    <th width="120">出生日期</th>
			                    <th width="120">年齡</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>1</td>
			                    <td>102</td>
			                    <td>103.01.01</td>
			                    <td>王小明</td>
			                    <td>98.01.01</td>
			                    <td>4歲9個月</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>2</td>
			                    <td>102</td>
			                    <td>103.01.01</td>
			                    <td>陳曉雯</td>
			                    <td>99.01.01</td>
			                    <td>3歲9個月</td>
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
			    <p align="right"><input id="AcademicYear" type="text" name="name" size="5" /> 學年度</p>
			    <p>授課日期 <input id="ClassDate" class="date" type="text" name="name" size="10" />　　　　任課教師   <input id="teacherName" type="text" value="" readonly="readonly" /><span id="TeacherID" class="hideClassSpan"></span>　　　　督導姓名 <input id="SupervisorName" type="text" name="name" size="15" /></p>
                    <table id="tableContact1" class="tableText" width="780" border="0">
                        <tr>
                            <th width="150">
                                學生姓名
                            </th>
                            <td>
                                <input id="studentName" type="text" value="" size="15" readonly="readonly" /><span id="StudentID" class="hideClassSpan"></span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                出生日期
                            </th>
                            <td><input id="StudentAge" type="text" value="" size="5"  readonly="readonly" /> 歲  <input id="StudentMonth" type="text" value="" size="5"  readonly="readonly" /> 個月</td>

                        </tr>
<%--                        <tr>
                            <th>
                                性 別
                            </th>
                            <td>
                                <label>
                                    <input type="radio" name="sex" value="male" />
                                    男</label>
                                <label>
                                    <input type="radio" name="sex" value="female" />
                                    女</label>
                            </td>
                        </tr>
       
                        <tr>
                            <th>
                                聽力相關
                            </th>
                            <td>
                                <label>
                                    <input type="radio" name="hearingQ" value="1" />
                                    HA</label>
                                <label>
                                    <input type="radio" name="hearingQ" value="2" />
                                    CI／</label><input type="text" name="name" value="" size="5" />耳</label><br />
                                聽損程度
                                <input type="text" name="name" value="" />
                            </td>
                        </tr>--%>
                        <tr>
                            <th>
                                檢核目的
                            </th>
                            <td>
                                <label>
                                    <input type="radio" name="ChecklistPurpose" value="1" />
                                    檢核目的</label>
                                <label>
                                    <input type="radio" name="ChecklistPurpose" value="2" />
                                    追蹤</label>
                                <label>
                                    <input type="radio" name="ChecklistPurpose" value="3" />
                                    其他</label>
                                <input id="ChecklistPurposeOther" type="text" name="name" value="" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                課 別
                            </th>
                            <td>
                                <label>
                                    <input type="radio" name="CourseType" value="1" />
                                    個別課</label>
                                <label>
                                    <input type="radio" name="CourseType" value="2" />
                                    團體課</label>
                                <label>
                                    <input type="radio" name="CourseType" value="3" />
                                    聽能課</label>
                                <label>
                                    <input type="radio" name="CourseType" value="4" />
                                    聽故事學語文</label>
                                <label>
                                    <input type="radio" name="CourseType" value="5" />
                                    其他</label>
                                <input id="CourseTypeOther" type="text" name="name" value="" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                個案選擇方式
                            </th>
                            <td>
                                <label>
                                    <input type="radio" name="CaseChoose" value="1" />
                                    督導自選/隨機</label>
                                <label>
                                    <input type="radio" name="CaseChoose" value="2" />
                                    教師提出，原因</label>
                                <input id="CaseChooseReason" type="text" name="name" value="" size="15" />
                                <label>
                                    <input type="radio" name="CaseChoose" value="3" />
                                    其它原因</label>
                                <input id="CaseChooseOther" type="text" name="name" value="" size="15" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                課程督導方式
                            </th>
                            <td>
                                <label>
                                    <input type="radio" name="SupervisorType" value="1" />
                                    督導示範教學（不須檢附檢核表）</label><br />
                                <label>
                                    <input type="radio" name="SupervisorType" value="2" />
                                    督導觀摩課程（須檢附檢核表）</label><br />
                                <label>
                                    <input type="radio" name="SupervisorType" value="3" />
                                    錄影帶觀摩課程（須檢附檢核表、報告資料等相關資料）</label><br />
                                <label>
                                    <input type="radio" name="SupervisorType" value="4" />
                                    個案討論、教學研討（依研討方式檢附表單)</label><br />
                                <label>
                                    <input type="radio" name="SupervisorType" value="5" />
                                    其他</label>
                                <input id="SupervisorTypeOther" type="text" name="name" value="" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                督導建議討論記要（包含：優點、建議、討論記要及時間）
                            </th>
                            <td>
                                討論時間（日期與時間）<br />
                                <textarea id="Remark" rows="2" cols="80"></textarea>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                督導結果
                            </th>
                            <td>
                                <label>
                                    <input type="checkbox" name="Result" value="1" />
                                    教學檢核表總分</label>
                                <input id="ResultScore" type="text" name="name" value="" size="10" />
                                分<br />
                                <label>
                                    <input type="checkbox" name="Result" value="2" />
                                    單項成績百分比為65%(含)以下項目為</label>
                                <input id="Following" type="text" name="name" value="" /><br />
                                <label>
                                    <input type="checkbox" name="Result" value="3" />
                                    督導決議（可參考教學檢核表平均分數相關督導建議）<br />
                                    <textarea id="Resolution" rows="2" cols="80"></textarea></label>
                            </td>
                        </tr>
                    </table>
			        
			    <p class="btnP">
		            <button class="btnSave" onclick="Save()" type="button">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" onclick="Update()" type="button">存 檔</button>
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
