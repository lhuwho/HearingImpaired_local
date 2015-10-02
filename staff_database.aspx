<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_database.aspx.cs" Inherits="staff_database" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人事管理 - 員工資料維護 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/staff_database.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/jquery.form.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/staff_database.js"></script>
	
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
			<div id="mainClass">人事管理&gt; 員工資料維護</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./staff_database.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
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
			                    職　　稱 <select id="gosrhstaffJob"></select>
			                </td>
			                <td>
			                    派任單位 <select id="gosrhstaffUnit">
			                    </select>
			                </td>
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
			                    <th width="100">員工編號</th>
			                    <th width="130">派任單位</th>
			                    <th width="130">員工姓名</th>
			                    <th width="130">職稱</th>
			                    <th width="130">到職日</th>
			                    <th width="100">離職日</th>
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
			<div id="mainMenuList2">
			    <div id="item1" class="menuTabs">個人基本資料</div>
			    <div id="item2" class="menuTabs">人事紀錄</div>
			    <div id="item3" class="menuTabs">加退保記錄</div>
			</div>
			<div id="mainContent">
			    <div id="item1Content">
			        <p style="background-color:#FFDF71;padding:0 10px;">
			            <font size="3"><span class="startMark">*</span>派任單位：
			            <label><input type="radio" name="unit" value="1" /> 總會</label>　　<label><input type="radio" name="unit" value="2" /> 台北至德</label>　　
			            <label><input type="radio" name="unit" value="3" /> 台中至德</label>　　<label><input type="radio" name="unit" value="5" /> 高雄至德</label></font>
			        </p>
			        <p align="right"><span class="startMark">*</span>填表日期 <span><input id="fillInDate" class="date" type="text" name="name" size="10" /></span></p>
			        <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="150">員工編號</th>
			                <td><span id="staffID"></span></td>
			                <td width="50">&nbsp;</td>
			                <th rowspan="9" width="200"><p align="center">大頭照<br />
			                <form id="Form2" name="GmyForm1" action="" method="post" enctype="multipart/form-data">
			                <input id="staffPhotoUpload" name="staffPhoto" type="file" name="File1" size="1" />
			                </form><br />
			                <a class="staffPhotoUrl" href="./images/noAvatar2.jpg"><img id="staffPhoto" src="./images/noAvatar.jpg" alt="" border="0" /></a></p>
			                <p style="font-weight:normal;">點圖放大</p></th>
			            </tr>
			              <tr>
			                <th width="150">卡號</th>
			                <td><input id="CardNum" type="text" value="" /><span class="startMark">*</span></td>
			                <td width="50">&nbsp;</td>
			            </tr>
			            <tr>
			                <th width="150">員工姓名</th>
			                <td><input id="staffName" type="text" value="" /><span class="startMark">*</span></td>
			                <td width="50">&nbsp;</td>
			            </tr>
			            <tr>
			                <th>到職日</th>
			                <td>
			                    <input id="officeDate" class="date" type="text" value="" /><span class="startMark">*</span>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>離職日</th>
			                <td>
			                    <input id="resignDate" class="date" type="text" value=""/>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>試用期</th>
			                <td>
			                    <input id="TrialStart" class="date" type="text" value=""/>～<input id="TrialEnd" class="date" type="text" value=""/>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			             <tr>
			                <th>合約日期</th>
			                <td>
			                    <input id="DealStart" class="date" type="text" value=""/>～<input id="DealEnd" class="date" type="text" value=""/>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>職稱</th>
			                <td>
			                    <select id="applyJob"></select><span class="startMark">*</span>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>職務</th>
			                <td>
			                    <select id="jobTitle"><option value="0">請選擇</option>
			                    <option value="1">高專</option><option value="2">專員</option>
			                    <option value="3">組員</option><option value="4">助理</option>
			                    <option value="5">助佐</option><option value="6">講師</option>
			                    <option value="7">助理講師</option><option value="8">專業教師</option>
			                    <option value="9">助理教師</option><option value="10">教師助理</option>
			                    </select><span class="startMark">*</span>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>職等</th>
			                <td>
			                    <select id="jobLevel"><option value="-1">請選擇</option>
			                    <option value="0">0</option>
			                    <option value="1">1</option><option value="2">2</option>
			                    <option value="3">3</option><option value="4">4</option>
			                    <option value="5">5</option><option value="6">6</option>
			                    <option value="7">7</option><option value="8">8</option>
			                    <option value="9">9</option><option value="10">10</option>
			                    <option value="11">11</option><option value="12">12</option>
			                    </select><span class="startMark">*</span>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>出生地</th>
			                <td><input id="comeCity" type="text" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>出生日期</th>
			                <td>
			                    <input type="text" id="staffbirthday" class="date3" value=""/><span class="startMark">*</span>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>年　　齡</th>
			                <td><input id="staffAge" type="text" name="" size="5" value="0"/> 歲 <input id="staffMonth" type="text" name="" size="5" value="0"/> 月</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>性　　別</th>
			                <td><label><input type="radio" name="staffsex" value="1" /> 男</label>　　
			                <label><input type="radio" name="staffsex" value="2" /> 女</label><span class="startMark">*</span></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>身份證字號</th>
			                <td><input type="text" id="staffTWID" /><span class="startMark">*</span></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>婚姻狀況</th>
			                <td>
                                <label><input type="radio" name="marriage" value="1" /> 未婚</label>　　
			                    <label><input type="radio" name="marriage" value="2" /> 已婚</label>　　
			                    <label><input type="radio" name="marriage" value="3" /> 其他</label>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
		                <tr>
			                <th>戶籍地址</th>
			                <td colspan="3"><input id="censusAddressZip" type="text" maxlength="5" value="" size="5"/> 
			                <select id="censusCity" class="zoneCity"></select> 
			                <input id="censusAddress" type="text" value="" size="50"/>
			                </td>
			            </tr>
			            <tr>
			                <th>通訊地址　<label><input type="checkbox" name="AddressCopyaddress" value="1"/> 同上</label></th>
			                <td colspan="3"><input id="addressZip" type="text" maxlength="5" value=""  size="5"/> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" value="" size="50"/></td>
			            </tr>
			            <tr>
			                <th>連絡電話</th>
			                <td colspan="3">
			                    <ul>
			                        <li>(日)：<input id="TDaytime" type="text" value="" /></li>
			                        <li>(夜)：<input id="TNight" type="text" value="" /></li>
			                        <li>行動電話：<input id="Phone" type="text" value=""  /></li>
			                    </ul>
			                </td>
			            </tr>
			            <tr>
			                <th>E-mail</th>
			                <td colspan="3"><input type="text" value="" size="80" id="staffemail" /></td>
			            </tr>
			            <tr>
			                <th>緊急連絡人</th>
			                <td colspan="3">
			                    <ul>
			                        <li>姓名：<input type="text" value="" id="EmergencyName" /></li>
			                        <li>地址：<input type="text" value="" id="EmergencyAddress" /></li>
			                        <li>電話：<input type="text" value="" id="EmergencyPhone" /></li>
			                    </ul>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4">
			                    <table class="tableContact" width="780" border="0">
		                            <tr>
		                                <th>學歷</th>
		                                <th>校名</th>
		                                <th>科系╱系所</th>
		                                <th>自</th>
		                                <th>至</th>
		                                <th>修業狀況</th>
		                            </tr>
		                            <tr>
		                                <th>博士</th>
		                                <td><input type="text" value="" id="DSchoolName" /></td>
		                                <td><input type="text" value="" id="DDepartment" /></td>
		                                <td><input type="text" value="" size="3" id="Dyear1" />年 <input type="text" value="" size="3" id="Dmonth1" />月</td>
		                                <td><input type="text" value="" size="3" id="Dyear2" />年 <input type="text" value="" size="3" id="Dmonth2" />月</td>
		                                <td><label><input type="radio" name="study1" value="1" /> 畢</label>　<label><input type="radio" name="study1" value="2" /> 肄</label></td>
		                            </tr>
		                            <tr>
		                                <th>碩士</th>
		                                <td><input type="text" value="" id="MSchoolName" /></td>
		                                <td><input type="text" value="" id="MDepartment" /></td>
		                                <td><input type="text" value="" size="3" id="Myear1" />年 <input type="text" value="" size="3" id="Mmonth1" />月</td>
		                                <td><input type="text" value="" size="3" id="Myear2" />年 <input type="text" value="" size="3" id="Mmonth2" />月</td>
		                                <td><label><input type="radio" name="study2" value="1" /> 畢</label>　<label><input type="radio" name="study2" value="2" /> 肄</label></td>
		                            </tr>
		                            <tr>
		                                <th>大學</th>
		                                <td><input type="text" value="" id="USchoolName" /></td>
		                                <td><input type="text" value="" id="UDepartment" /></td>
		                                <td><input type="text" value="" size="3" id="Uyear1" />年 <input type="text" value="" size="3" id="Umonth1" />月</td>
		                                <td><input type="text" value="" size="3" id="Uyear2" />年 <input type="text" value="" size="3" id="Umonth2" />月</td>
		                                <td><label><input type="radio" name="study3" value="1" /> 畢</label>　<label><input type="radio" name="study3" value="2" /> 肄</label></td>
		                            </tr>
		                            <tr>
		                                <th>專科</th>
		                                <td><input type="text" value="" id="VSchoolName" /></td>
		                                <td><input type="text" value="" id="VDepartment" /></td>
		                                <td><input type="text" value="" size="3" id="Vyear1" />年 <input type="text" value="" size="3" id="Vmonth1" />月</td>
		                                <td><input type="text" value="" size="3" id="Vyear2" />年 <input type="text" value="" size="3" id="Vmonth2" />月</td>
		                                <td><label><input type="radio" name="study4" value="1" /> 畢</label>　<label><input type="radio" name="study4" value="2" /> 肄</label></td>
		                            </tr>
			                    </table>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4">工作經歷(請由最近的一份工作開始填起)：</td>
			            </tr>
			            <tr>
			                <td colspan="4">
			                    <table class="tableContact" width="780" border="0">
		                            <tr>
		                                <th colspan="2">自</th>
		                                <th colspan="2">至</th>
		                                <th rowspan="2">公司名稱</th>
		                                <th rowspan="2">職務及擔任工作</th>
		                                <th rowspan="2">薪資</th>
		                                <th rowspan="2">年資/離職證明</th>
		                                <th colspan="2">長官或主管人員</th>
		                            </tr>
		                            <tr>
		                                <th>年</th>
		                                <th>月</th>
		                                <th>年</th>
		                                <th>月</th>
		                                <th>職銜</th>
		                                <th>姓名</th>
		                            </tr>
		                            <tr>
		                                <td><input type="text" value="" size="3" id="Jyear1_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth1_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jyear1_2" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth1_2" /></td>
		                                <td><input type="text" value="" size="15" id="JCname1" /></td>
		                                <td><input type="text" value="" size="15" id="Jposition1" /></td>
		                                <td><input type="text" value="" size="10" id="Jsalary1" /></td>
		                                <td><label><input type="radio" name="prove1" value="1" /> 有</label>　<label><input type="radio" name="prove1" value="2" /> 無</label></td>
		                                <td><input type="text" value="" size="10" id="JTitle1" /></td>
		                                <td><input type="text" value="" size="10" id="JTitleName1" /></td>
		                            </tr>
		                            <tr>
		                                <td><input type="text" value="" size="3" id="Jyear2_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth2_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jyear2_2" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth2_2" /></td>
		                                <td><input type="text" value="" size="15" id="JCname2" /></td>
		                                <td><input type="text" value="" size="15" id="Jposition2" /></td>
		                                <td><input type="text" value="" size="10" id="Jsalary2" /></td>
		                                <td><label><input type="radio" name="prove2" value="1" /> 有</label>　<label><input type="radio" name="prove2" value="2" /> 無</label></td>
		                                <td><input type="text" value="" size="10" id="JTitle2" /></td>
		                                <td><input type="text" value="" size="10" id="JTitleName2" /></td>
		                            </tr>
		                            <tr>
		                                <td><input type="text" value="" size="3" id="Jyear3_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth3_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jyear3_2" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth3_2" /></td>
		                                <td><input type="text" value="" size="15" id="JCname3" /></td>
		                                <td><input type="text" value="" size="15" id="Jposition3" /></td>
		                                <td><input type="text" value="" size="10" id="Jsalary3" /></td>
		                                <td><label><input type="radio" name="prove3" value="1" /> 有</label>　<label><input type="radio" name="prove3" value="2" /> 無</label></td>
		                                <td><input type="text" value="" size="10" id="JTitle3" /></td>
		                                <td><input type="text" value="" size="10" id="JTitleName3" /></td>
		                            </tr>
		                            <tr>
		                                <td><input type="text" value="" size="3" id="Jyear4_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth4_1" /></td>
		                                <td><input type="text" value="" size="3" id="Jyear4_2" /></td>
		                                <td><input type="text" value="" size="3" id="Jmonth4_2" /></td>
		                                <td><input type="text" value="" size="15" id="JCname4" /></td>
		                                <td><input type="text" value="" size="15" id="Jposition4" /></td>
		                                <td><input type="text" value="" size="10" id="Jsalary4" /></td>
		                                <td><label><input type="radio" name="prove4" value="1" /> 有</label>　<label><input type="radio" name="prove4" value="2" /> 無</label></td>
		                                <td><input type="text" value="" size="10" id="JTitle4" /></td>
		                                <td><input type="text" value="" size="10" id="JTitleName4" /></td>
		                            </tr>
		                        </table>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4">
			                    <table class="tableContact" width="780" border="0" id="FamilyStatus">
		                            <tr>
		                                <th rowspan="4" width="25">家庭狀況</th>
		                                <th>稱謂</th>
		                                <th>姓名</th>
		                                <th>年齡</th>
		                                <th>職業</th>
		                                <th>稱謂</th>
		                                <th>姓名</th>
		                                <th>年齡</th>
		                                <th>職業</th>
		                            </tr>
		                            <tr>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                            </tr>
		                            <tr>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                            </tr>
		                            <tr>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                            </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
			                <td colspan="4">
			                    <table class="tableContact" width="780" border="0" >
		                            <tr>
		                                <th>保證人姓名</th>
		                                <th>服務單位</th>
		                                <th>職稱</th>
		                                <th>與應徵者關係</th>
		                                <th>聯絡方式<br />(telephone-mail)</th>
		                                <th>方便聯絡時間</th>
		                            </tr>
		                            <tr>
		                                <td><input type="text" id="bailName" value="" size="10" /></td>
		                                <td><input type="text" id="bailUnit" value="" size="10" /></td>
		                                <td><input type="text" id="bailJob" value="" size="10" /></td>
		                                <td><input type="text" id="bailRelationship" value="" size="10" /></td>
		                                <td><input type="text" id="bailContact" value="" size="10" /></td>
		                                <td><input type="text" id="bailContactTime" value="" size="10" /></td>
		                            </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <th>應徵訊息</th>
                            <td colspan="3">
                                <label><input type="radio" name="recruited" value="1" /> 本會網站</label>　
                                <label><input type="radio" name="recruited" value="2" /> 徵才網站</label>　
                                <label><input type="radio" name="recruited" value="3" /> 報紙廣告</label>　
                                <label><input type="radio" name="recruited" value="4" /> 親友介紹</label>　
                                <label><input type="radio" name="recruited" value="5" /> 其他</label> <input id="recruitedText" type="text" value="" />
                            </td>
                        </tr>
                        <tr>
			                <td colspan="4">
			                    <table class="tableContact" width="780" border="0">
		                            <tr>
		                                <th width="300">語文能力</th>
		                                <th>聽</th>
		                                <th>說</th>
		                                <th>讀</th>
		                                <th>寫</th>
		                            </tr>
		                            <tr>
		                                <td><input id="langAbility1" type="text" value="" size="40" /></td>
		                                <td><select id="langL1" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langS1" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langR1" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langW1" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                            </tr>
		                            <tr>
		                                <td><input id="langAbility2" type="text" value="" size="40" /></td>
		                                <td><select id="langL2" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langS2" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langR2" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langW2" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                            </tr>
		                            <tr>
		                                <td><input id="langAbility3" type="text" value="" size="40" /></td>
		                                <td><select id="langL3" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langS3" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langR3" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                                <td><select id="langW3" ><option value="0">請選擇</option><option value="1">優</option><option value="2">佳</option><option value="3">可</option></select></td>
		                            </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
			                <td colspan="4">
			                    <table class="tableContact" width="780" border="0" id="SpecialtySkillTable">
		                            <tr>
		                                <th colspan="2">專長及技能</th>
		                                <th rowspan="5" width="25">證照／專業訓練</th>
		                                <th>證照/訓練名稱</th>
		                                <th>級數/成績</th>
		                                <th>授證單位</th>
		                                <th>取得日期</th>
		                                <th>有效日期</th>
		                            </tr>
		                            <tr>
		                                <th>1</th>
		                                <td><input type="text" value="" /></td>
		                                <td><input type="text" value="" size="15" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                            </tr>
		                            <tr>
		                                <th>2</th>
		                                <td><input type="text" value="" /></td>
		                                <td><input type="text" value="" size="15" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                            </tr>
		                            <tr>
		                                <th>3</th>
		                                <td><input type="text" value="" /></td>
		                                <td><input type="text" value="" size="15" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                            </tr>
		                            <tr>
		                                <th>4</th>
		                                <td><input type="text" value="" /></td>
		                                <td><input type="text" value="" size="15" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                                <td><input class="date" type="text" value="" size="10" /></td>
		                            </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4">曾否罹患重大疾病或重大殘障及傷害？
                                <label><input type="radio" name="disease" value="1" /> 是，請說明：</label> <input id="diseaseText" type="text" value="" />　　
                                <label><input type="radio" name="disease" value="2" /> 否</label> 
                            </td>
                        </tr>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="SetStaffData(0)">存 檔</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="SetStaffData(1)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item2Content">
			    <p class="staffNameData"></p>
			  <%--  <p>單位異動記錄</p>
                    <table class="tableContact" width="780" border="0" id="WorkDataDiv1">
	                    <thead>
	                        <tr>
	                            <th width="100">日期</th>
	                            <th width="290">內容</th>
	                            <th width="290">備註</th>
	                            <th width="100">功能</th>
	                        </tr>
	                    </thead>
	                    <tbody>
  
			            </tbody>
			        </table>
			        <br />		        
			        <p align="center"><button class="btnAdd" type="button" onclick="insertDataDiv(1)">新增紀錄</button></p>
			        <div id="insertDataDiv1" style="display:none">
	                    <table class="tableContact" width="780" border="0">
	                        <tr>
	                            <th width="100">日期</th>
	                            <th width="290">內容</th>
	                            <th width="290">備註</th>
	                            <th width="100">功能</th>
	                        </tr>
	                        <tr>
	                            <td align="center" height="30"><input class="date RecordDate" type="text" value="" size="10" /></td>
	                            <td align="center"><textarea class="Record" rows="1" cols="35"></textarea></td>
	                            <td align="center"><textarea class="RecordRemark" rows="1" cols="35"></textarea></td>
	                            <td><button class="btnView insertStaffData" type="button">儲 存</button> <button class="btnView canceInsert" type="button" >取 消</button></td>
	                        </tr>
	                    </table>
	               </div>
	               <br />
	               <p>獎懲記錄</p>--%>
                    <table class="tableContact" width="780" border="0" id="WorkDataDiv2">
	                    <thead>
	                        <tr>
	                            <th width="100">日期</th>
	                            <th width="290">內容</th>
	                            <th width="290">備註</th>
	                            <th width="100">功能</th>
	                        </tr>
	                    </thead>
	                    <tbody>
			                
			            </tbody>
			        </table>
			        <br />		        
			        <p align="center"><button class="btnAdd" type="button" onclick="insertDataDiv(2)">新增紀錄</button></p>
			        <div id="insertDataDiv2" style="display:none">
	                    <table class="tableContact" width="780" border="0">
	                        <tr>
	                            <th width="100">日期</th>
	                            <th width="290">內容</th>
	                            <th width="290">備註</th>
	                            <th width="100">功能</th>
	                        </tr>
	                        <tr>
	                            <td align="center" height="30"><span class="RecordID" style="display:none;">0</span><input class="date RecordDate" type="text" value="" size="10" /></td>
	                            <td align="center"><textarea class="Record" rows="1" cols="35"></textarea></td>
	                            <td align="center"><textarea class="RecordRemark" rows="1" cols="35"></textarea></td>
	                            <td><button class="btnView insertStaffData" type="button">儲 存</button> <button class="btnView canceInsert" type="button" >取 消</button></td>
	                        </tr>
	                    </table>
	               </div>	                

			    </div>
			    <div id="item3Content">
			        <p class="staffNameData"></p>
                    <table class="tableContact" width="780" border="0" id="WorkDataDiv3">
                        <thead>
                            <tr>
                                <th width="100">日期</th>
                                <th width="290">內容</th>
                                <th width="290">備註</th>
                                <th width="100">功能</th>
                            </tr>
                        </thead>
                        <tbody>
                            
                        </tbody>
                    </table>
                    <br />		        
                    <p align="center"><button class="btnAdd" type="button" onclick="insertDataDiv(3)">新增紀錄</button></p>
                    <div id="insertDataDiv3" style="display:none">
                        <table class="tableContact" width="780" border="0">
                            <tr>
                                <th width="100">日期</th>
                                <th width="290">內容</th>
                                <th width="290">備註</th>
                                <th width="100">功能</th>
                            </tr>
                            <tr>
                                <td align="center" height="30"><span class="RecordID" style="display:none;">0</span><input class="date RecordDate" type="text" value="" size="10" /></td>
                                <td align="center"><textarea class="Record" rows="1" cols="35"></textarea></td>
                                <td align="center"><textarea class="RecordRemark" rows="1" cols="35"></textarea></td>
                                <td><button class="btnView insertStaffData" type="button">儲 存</button> <button class="btnView canceInsert" type="button" >取 消</button></td>
                            </tr>
                        </table>
                        <p class="btnP">
                            <button class="btnSaveUdapteData" type="button">存 檔</button>
                        </p>
                    </div>	 
			    </div>
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
