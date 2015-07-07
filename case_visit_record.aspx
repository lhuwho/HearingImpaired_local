<%@ Page Language="C#" AutoEventWireup="true" CodeFile="case_visit_record.aspx.cs" Inherits="case_visit_record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 訪視記錄 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/case_visit_record.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	<script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/jquery.form.js"></script>
    <script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
	<script type="text/javascript" src="./js/All.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
             <Scripts>
                <asp:ScriptReference Path="~/js/case_visit_record.js" />
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
			<div id="mainClass">個案管理&gt; 訪視記錄</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./case_visit_record.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>訪視類別 <select id="gosrhvisitType" class="visitTypeList"></select>
			                </td>
			                <td>訪視日期 <input id="gosrhvisitDatestart" class="date" type="text"  value="" size="10" />～<input id="gosrhvisitDateend" class="date" type="text"  value="" size="10" /></td>
			                <td>訪視者 <input id="gosrhvisitSocial" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="120">服務使用者編號</th>
			                    <th width="120">學生姓名</th>
			                    <th width="120">訪視類別</th>
			                    <th width="120">訪視日期</th>
			                    <th width="120">訪視者</th>
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
			        <p id="caseUnit" align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			        <p><select id="visitType" class="visitTypeList"></select>
			            <span style="float:right;">訪視者 <input id="viewSocialWork" type="text" value="" size="10" /></span>
			        </p>
			        <table class="tableText" width="780" border="0" id="tableContent">
			            <tr>
			                <th width="170">學生姓名</th>
			                <td><input id="studentName" type="text" value="" readonly="readonly"/><span id="studentID" class="hideClassSpan"></span><span class="startMark">*</span></td>
			            </tr>
			            <tr>
			                <th>目　　的</th>
			                <td><input id="target" type="text" value="" size="70" /><span class="startMark">*</span></td>
		                </tr>
		                <tr>
			                <th>訪視時間</th>
			                <td><input id="viewDate" class="date" type="text" value="" size="10" /> <input id="viewTime1" type="text" value="" size="5" />－<input id="viewTime2" type="text" value="" size="5" /><span class="startMark">*</span></td>
			            </tr>
			        </table>
			        <div id="visitTable1">
			            <table class="tableText" width="780" border="0" >
			                <tr style="visibility:hidden;">
			                    <td colspan="2">&nbsp;</td>
			                </tr>
			                <tr>
			                    <th width="170">單位名稱</th>
			                    <td><input id="viewUnit" type="text" value="" /></td>
			                </tr>
			                <tr>
			                    <th>聯絡電話</th>
			                    <td><input id="viewTel" type="text" value="" /></td>
			                </tr>
			                <tr>
			                    <th>聯絡地址</th>
			                    <td><input id="addressZip" type="text" maxlength="5" value=""  size="5"/> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" value="" size="50"/></td>
			                </tr>
			                <tr>
			                    <th>參與人員及職稱</th>
			                    <td><input id="viewPeople1" type="text" value="" /></td>
			                </tr>
			                <tr>
			                    <th height="80">內容記錄</th>
			                    <td><textarea id="viewContent1"></textarea></td>
			                </tr>
			                <tr>
			                    <th height="80">處遇記錄</th>
			                    <td><textarea id="viewContent2"></textarea></td>
			                </tr>
			                <tr>
			                    <th height="80">備　　註</th>
			                    <td><textarea id="viewRemark1"></textarea></td>
			                </tr>
			            </table>
			        </div>
			        
			        <form id="GmyForm" action="" method="post" enctype="multipart/form-data">
			        <div id="visitTable2">
			            <table class="tableText" width="780" border="0">
			                <tr style="visibility:hidden;">
			                        <td colspan="2">&nbsp;</td>
			                    </tr>
			                <tr>
			                    <th width="170">訪視對象</th>
			                    <td><select id="viewPeople2"><option value="1">案父</option><option value="2">案母</option><option value="3">案祖父</option><option value="4">案祖母</option><option value="5">案外祖父</option><option value="6">案外祖母</option></select></td>
			                </tr>
			                <tr>
			                    <th>訪視地點</th>
			                    <td><input id="viewPlace" type="text" value="" size="70" /></td>
			                </tr>
			                <tr>
			                    <th height="150">家 系 圖</th>
			                    <td><img id="pedigree" src="#" alt=""><input type="file" name="pedigree" /></td>
			                </tr>
			                <tr>
			                    <th height="150">生 態 圖</th>
			                    <td><img id="ecological" src="#" alt=""><input type="file" name="ecological" /></td>
			                </tr>
			             </table>
			             <p class="cP">療育狀況</p>
			             <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170">療育單位</th>
			                    <td><input id="cureUnit" type="text" value="" size="50" /></td>
			                </tr>
			                <tr>
			                    <th>開始療育日期</th>
			                    <td><input id="cureDate" class="date" type="text" value="" size="10"/></td>
			                </tr>
			                <tr>
			                    <th>服務方式</th>
			                    <td><label><input type="radio" name="cureType1" value="1" />全日托</label>　<label><input type="radio" name="cureType1" value="2" />半日托</label>　<label><input type="radio" name="cureType1" value="3" />部份時制</label></td>
			                </tr>
			                <tr>
			                    <th>&nbsp;</th>
			                    <td><select id="cureType2"><option value="0">請選擇</option><option value="1">物理</option><option value="2">職能</option><option value="3">感覺統合</option><option value="4">語言</option></select> 治療：<input id="cureUnit1" type="text" value="" />(單位)　
			                        治療頻率：每週 <select id="cureNumber1"><option value="1">一</option><option value="2">二</option><option value="3">三</option><option value="4">四</option><option value="5">五</option></select> 次 
			                        每次 <select id="cureNumberTime1"><option value="1">30</option><option value="2">60</option></select> 分</td>
			                </tr>
			                <tr>
			                    <th>&nbsp;</th>
			                    <td><select id="cureType3"><option value="0">請選擇</option><option value="1">物理</option><option value="2">職能</option><option value="3">感覺統合</option><option value="4">語言</option></select> 治療：<input id="cureUnit2" type="text" value="" />(單位)　
			                        治療頻率：每週 <select id="cureNumber2"><option value="1">一</option><option value="2">二</option><option value="3">三</option><option value="4">四</option><option value="5">五</option></select> 次 
			                        每次 <select id="cureNumberTime2"><option value="1">30</option><option value="2">60</option></select> 分</td>
			                </tr>
			                <tr>
			                    <th>幼托園所</th>
			                    <td><input id="preSchool" type="text" value="" size="50" /></td>
			                </tr>
			                <tr>
			                    <th>開始就讀日期</th>
			                    <td><input id="studyDate1" type="text" value="" size="5" /> 年 <input id="studyDate2" type="text" value="" size="5" /> 月</td>
			                </tr>
			                <tr>
			                    <th>就讀形式</th>
			                    <td><input type="radio" name="studyType" value="1" />全日　<input type="radio" name="studyType" value="2" />半日</td>
			                </tr>
                        </table>
                        
			            <p class="cP">案家基本概況</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170">家庭型態</th>
			                    <td><label><input type="radio" name="familyType" value="1" />核心家庭</label>　<label><input type="radio" name="familyType" value="1" />折衷家庭(三代同堂)</label>　<label><input type="radio" name="familyType" value="3" />單親家庭</label>　<label><input type="radio" name="familyType" value="4" />其他</label> <input id="familyTypeText" type="text" value="" size="15" /></td>
			                </tr>
			                <tr>
			                    <th>主要照顧者</th>
			                    <td><label><input type="radio" name="careProple" value="1" />父/母</label>　<label><input type="radio" name="careProple" value="2" />案祖父/案祖母</label>　<label><input type="radio" name="careProple" value="3" />案外祖父/案外祖母</label>　<label><input type="radio" name="careProple" value="4" />保母</label>　<label><input type="radio" name="familyType" value="4" />其他</label> <input id="carePropleText" type="text" value="" size="15" /></td>
			                </tr> 
			                <tr>
			                    <th>主要照顧時段</th>
			                    <td><label><input type="radio" name="careTime" value="1" />上午</label>　<label><input type="radio" name="careTime" value="2" />下午</label>　<label><input type="radio" name="careTime" value="3" />夜間</label>　<label><input type="radio" name="careTime" value="4" />全日</label></td>
			                </tr>
			                <tr>
			                    <th>房屋持有</th>
			                    <td><label><input type="radio" name="homeOwnership" value="1" />自宅</label>　<label><input type="radio" name="homeOwnership" value="2" />租屋</label>　<label><input type="radio" name="homeOwnership" value="3" />借住</label>　<label><input type="radio" name="homeOwnership" value="4" />其他</label> <input type="text" value="" size="15" /></td>
			                </tr>
			                <tr>
			                    <th>居家環境</th>
			                    <td><label><input type="radio" name="homeAround" value="1" />住宅區</label>　<label><input type="radio" name="homeAround" value="2" />商業區</label>　<label><input type="radio" name="homeAround" value="3" />工業區</label>　<label><input type="radio" name="homeAround" value="4" />住商混合</label>　<label><input type="radio" name="homeAround" value="5" />其他</label> <input id="homeAroundText" type="text" value="" size="15" /></td>
			                </tr>
			                <tr>
			                    <th>住家類型</th>
			                    <td><label><input type="radio" name="homeType" value="1" />公寓</label>　<label><input type="radio" name="homeType" value="2" />華廈</label>　<label><input type="radio" name="homeType" value="3" />社區大廈</label>　<label><input type="radio" name="homeType" value="4" />透天厝</label>　<label><input type="radio" name="homeType" value="5" />其他</label> <input id="homeTypeText" type="text" value="" size="15" /></td>
			                </tr>
			                <tr>
			                    <th>至機構的主要交通工具</th>
			                    <td><label><input type="radio" name="transportType" value="1" />步行</label>　<label><input type="radio" name="transportType" value="2" />開車</label>　<label><input type="radio" name="transportType" value="3" />機車/腳踏車</label>　<label><input type="radio" name="transportType" value="4" />公車/客運</label>　<label><input type="radio" name="transportType" value="5" />捷運</label>　<label><input type="radio" name="transportType" value="6" />火車/高鐵</label></td>
			                </tr>
			                <tr>
			                    <th>附近的休閒場所</th>
			                    <td><label><input type="radio" name="leisureType" value="1" />公園</label>　<label><input type="radio" name="leisureType" value="2" />運動場</label>　<label><input type="radio" name="leisureType" value="3" />圖書館</label>　<label><input type="radio" name="leisureType" value="4" />速食店</label>　<label><input type="radio" name="leisureType" value="5" />博物館</label>　<label><input type="radio" name="leisureType" value="6" />書店</label>　<label><input type="radio" name="leisureType" value="7" />百貨公司</label><br />
			                    <label><input type="radio" name="leisureType" value="8" />其他</label> <input id="leisureTypeText" type="text" value="" size="15" /></td>
			                </tr>
			                <tr>
			        	        <th>說　　明</th>
			                    <td><textarea id="explanation2"></textarea></td>
			                </tr>
			            </table>
			            
			            <p class="cP">家庭環境</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170">居家空間</th>
			                    <td><input id="homeSpace1" type="text" value="" size="5" /> 房 <input id="homeSpace2" type="text" value="" size="5" /> 廳</td>
			                </tr>
			                <tr>
			                    <th colspan="2">居家環境安全性</th>
			                </tr>
			                <tr>
			                    <th>室內家具的設置是否安全</th>
			                    <td><label><input type="radio" name="furniture" value="1" />是</label>　<label><input type="radio" name="furniture" value="2" />否</label>　<label><input type="radio" name="furniture" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th>電器（插座）的使用是否安全</th>
			                    <td><label><input type="radio" name="electric" value="1" />是</label>　<label><input type="radio" name="electric" value="2" />否</label>　<label><input type="radio" name="electric" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th>危險物品是否妥善擺放</th>
			                    <td><label><input type="radio" name="place" value="1" />是</label>　<label><input type="radio" name="place" value="2" />否</label>　<label><input type="radio" name="place" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th>水電設備是否安全</th>
			                    <td><label><input type="radio" name="hydro" value="1" />是</label>　<label><input type="radio" name="hydro" value="2" />否</label>　<label><input type="radio" name="hydro" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th colspan="2">用餐環境</th>
			                </tr>
			                <tr>
			                    <th>垃圾是否整齊放置</th>
			                    <td><label><input type="radio" name="dining1" value="1" />是</label>　<label><input type="radio" name="dining1" value="2" />否</label>　<label><input type="radio" name="dining1" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th>廚房是否衛生</th>
			                    <td><label><input type="radio" name="dining2" value="1" />是</label>　<label><input type="radio" name="dining2" value="2" />否</label>　<label><input type="radio" name="dining2" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th colspan="2">家庭室內環境</th>
			                </tr>
			                <tr>
			                    <th>室內是否保持通風</th>
			                    <td><label><input type="radio" name="indoor1" value="1" />是</label>　<label><input type="radio" name="indoor1" value="2" />否</label>　<label><input type="radio" name="indoor1" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th>光線是否充足</th>
			                    <td><label><input type="radio" name="indoor2" value="1" />是</label>　<label><input type="radio" name="indoor2" value="2" />否</label>　<label><input type="radio" name="indoor2" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th>活動空間是否寬敞</th>
			                    <td><label><input type="radio" name="indoor3" value="1" />是</label>　<label><input type="radio" name="indoor3" value="2" />否</label>　<label><input type="radio" name="indoor3" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			                    <th>隔音是否良好</th>
			                    <td><label><input type="radio" name="indoor4" value="1" />是</label>　<label><input type="radio" name="indoor4" value="2" />否</label>　<label><input type="radio" name="indoor4" value="3" />無法觀察</label></td>
			                </tr>
			                <tr>
			        	        <th>說　　明</th>
			                    <td><textarea id="explanation3"></textarea></td>
			                </tr>
			            </table>
			            
			            <p class="cP">非正式資源</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170">經濟資源</th>
			                    <td><label><input type="radio" name="informal1" value="1" />案父</label>　<label><input type="radio" name="informal1" value="2" />案母</label>　<label><input type="radio" name="informal1" value="3" />案祖父</label>　<label><input type="radio" name="informal1" value="4" />案祖母</label>　<label><input type="radio" name="informal1" value="5" />案外祖父</label>　<label><input type="radio" name="informal1" value="6" />案外祖母</label></td>
			                </tr>
			                <tr>
			                    <th>主要決策</th>
			                    <td><label><input type="radio" name="informal2" value="1" />案父</label>　<label><input type="radio" name="informal2" value="2" />案母</label>　<label><input type="radio" name="informal2" value="3" />案祖父</label>　<label><input type="radio" name="informal2" value="4" />案祖母</label>　<label><input type="radio" name="informal2" value="5" />案外祖父</label>　<label><input type="radio" name="informal2" value="6" />案外祖母</label></td>
			                </tr>
			                <tr>
			                    <th>主要教學</th>
			                    <td><label><input type="radio" name="informal3" value="1" />案父</label>　<label><input type="radio" name="informal3" value="2" />案母</label>　<label><input type="radio" name="informal3" value="3" />案祖父</label>　<label><input type="radio" name="informal3" value="4" />案祖母</label>　<label><input type="radio" name="informal3" value="5" />案外祖父</label>　<label><input type="radio" name="informal3" value="6" />案外祖母</label></td>
			                </tr>
			                <tr>
			                    <th>照顧資源</th>
			                    <td><label><input type="radio" name="informal4" value="1" />案父</label>　<label><input type="radio" name="informal4" value="2" />案母</label>　<label><input type="radio" name="informal4" value="3" />案祖父</label>　<label><input type="radio" name="informal4" value="4" />案祖母</label>　<label><input type="radio" name="informal4" value="5" />案外祖父</label>　<label><input type="radio" name="informal4" value="6" />案外祖母</label></td>
			                </tr>
			                <tr>
			                    <th>情感支持</th>
			                    <td><label><input type="radio" name="informal5" value="1" />案父</label>　<label><input type="radio" name="informal5" value="2" />案母</label>　<label><input type="radio" name="informal5" value="3" />案祖父</label>　<label><input type="radio" name="informal5" value="4" />案祖母</label>　<label><input type="radio" name="informal5" value="5" />案外祖父</label>　<label><input type="radio" name="informal5" value="6" />案外祖母</label></td>
			                </tr>
			                <tr>
			                    <th>照顧者面臨問題時</th>
			                    <td><label><input type="radio" name="informal6" value="1" />家人會共同面對討論問題的處理方法。</label><br />
			                    <label><input type="radio" name="informal6" value="2" />由某一成員面對及處理問題，亦會讓家人了解處理情形。</label><br />
			                    <label><input type="radio" name="informal6" value="3" />獨自處理問題，亦不會讓家人知道處理狀況。</label><br />
			                    <label><input type="radio" name="informal6" value="4" />家庭沒有固定處理問題的模式或不知如何處理，而選擇不處理。</label></td>
			                </tr>
			                <tr>
			                    <th>照顧者資源使用的能力</th>
			                    <td><label><input type="radio" name="informal7" value="1" />自行尋找適合資源</label><br />
			                    <label><input type="radio" name="informal7" value="2" />需要服務單位提供資源</label><br />
			                    <label><input type="radio" name="informal7" value="3" />其他</label> <input type="text" id="informal7Text" value="" /></td>
			                </tr>
			                <tr>
			                    <th>案家與社區人士互動情況</th>
			                    <td><label><input type="radio" name="informal8" value="1" />與鄰居有良好的互動</label><br />
			                    <label><input type="radio" name="informal8" value="2" />少與鄰居往來</label><br />
			                    <label><input type="radio" name="informal8" value="3" />時常參與社區舉辦的活動</label><br />
			                    <label><input type="radio" name="informal8" value="4" />視社區活動類型選擇性參與</label><br />
			                    <label><input type="radio" name="informal8" value="5" />未能觀察出與鄰居的互動關係</label><br />
			                    <label><input type="radio" name="informal8" value="6" />其他</label> <input type="text" id="informal8Text" value="" />
			                    </td>
			                </tr>
			                <tr>
			                    <th>非正式資源可提供的有效協助</th>
			                    <td><label><input type="radio" name="informal9" value="1" />經濟支持</label>　
			                    <label><input type="radio" name="informal9" value="2" />教養知能</label>　
			                    <label><input type="radio" name="informal9" value="3" />替代照顧/家務人力</label>　
			                    <label><input type="radio" name="informal9" value="4" />情緒支持</label>　
			                    <label><input type="radio" name="informal9" value="5" />其他</label> <input type="text" id="informal9Text" value="" />
			                    </td>
			                </tr>
			                <tr>
			                    <th>非正式資源對家庭問題解決的助益</th>
			                    <td><label><input type="radio" name="informal10" value="1" />幾乎解決家庭大部分的問題</label><br />
			                    <label><input type="radio" name="informal10" value="2" />可解決家庭部分問題</label><br />
			                    <label><input type="radio" name="informal10" value="3" />幾乎沒有幫助</label></td>
			                </tr>
			                <tr>
			        	        <th>說　　明</th>
			                    <td><textarea id="explanation4"></textarea></td>
			                </tr>
			            </table>
			            
			            <p class="cP">正式資源</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170">可用的正式資源</th>
			                    <td>醫療資源<input type="text" id="formalText1" value="" />　
			                        療育資源<input type="text" id="formalText2" value="" /><br />
			                        經濟資源<input type="text" id="formalText3" value="" />　
			                        照顧資源<input type="text" id="formalText4" value="" /><br />
			                        情感支持<input type="text" id="formalText5" value="" />　
			                        教育資源<input type="text" id="formalText6" value="" /><br />
			                        宗教資源<input type="text" id="formalText7" value="" />　
			                        文化資源<input type="text" id="formalText8" value="" /><br />
			                        其他<input type="text" id="formalText9" value="" />
			                    </td>
			                </tr>
			                <tr>
			                    <th>正式資源可提供的有效協助</th>
			                    <td><label><input type="radio" name="formal" value="1" />經濟支持</label>
			                    <label><input type="radio" name="formal" value="2" />教養知能</label>
			                    <label><input type="radio" name="formal" value="3" />替代照顧/家務人力</label>
			                    <label><input type="radio" name="formal" value="4" />情緒支持</label>
			                    <label><input type="radio" name="formal" value="5" />其他</label> <input type="text" id="Text16" value="" size="15" />
			                    </td>
			                </tr>
			                <tr>
			                    <th>正式資源對家庭問題解決的助益</th>
			                    <td><label><input type="radio" name="forma2" value="1" />幾乎解決家庭大部分的問題</label>　
			                    <label><input type="radio" name="forma2" value="2" />可解決家庭部分問題</label>　
			                    <label><input type="radio" name="forma2" value="3" />幾乎沒有幫助</label></td>
			                </tr>
			                <tr>
			                    <th>面臨問題解決管道</th>
			                    <td><label><input type="radio" name="forma3" value="1" />親友</label>　
			                    <label><input type="radio" name="forma3" value="2" />鄰居</label>　
			                    <label><input type="radio" name="forma3" value="3" />網路</label>　
			                    <label><input type="radio" name="forma3" value="4" />其他聽損兒家長</label>　
			                    <label><input type="radio" name="forma3" value="5" />學校</label>　
			                    <label><input type="radio" name="forma3" value="6" />醫療院所</label>　
			                    <label><input type="radio" name="forma3" value="7" />療育單位</label><br />
			                    <label><input type="radio" name="forma3" value="8" />相關社福團體</label>　
			                    <label><input type="radio" name="forma3" value="9" />宗教團體</label>　
			                    <label><input type="radio" name="forma3" value="10" />其他</label> <input type="text" id="Text26" value="" size=10" /></td>
			                </tr>
			                <tr>
			        	        <th>說　　明</th>
			                    <td><textarea id="explanation5"></textarea></td>
			                </tr>
			            </table>
			            
			            <p class="cP">家庭功能(非正式及正式資源整合)</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="150">內外部資源</th>
			                    <td>經濟資源－<input type="text" id="familyFunction1" value="" size="60" /><br />
			                    醫療資源－<input type="text" id="familyFunction2" value="" size="60" /><br />
			                    療育資源－<input type="text" id="familyFunction3" value="" size="60" /><br />
			                    宗教資源－<input type="text" id="familyFunction4" value="" size="60" /><br />
			                    資訊來源－<input type="text" id="familyFunction5" value="" size="60" /><br />
			                    情緒支持－<input type="text" id="familyFunction6" value="" size="60" /></td>
                            </tr>
			            </table>
			            
			            <p class="cP">個案狀況或訪視觀察概況</p>
			            <p>(輔具配戴情況、個案與家人的互動關係、角色功能、支持度、溝通方式、身心狀態、孩子的照顧情形等)<br /><textarea id="viewSituation"></textarea></p>
			            
			            <p class="cP">家庭及個案優勢及限制</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170">家庭優勢及限制</th>
			                    <td>優勢 <input type="text" id="familyStrengths" value="" size="60" /><br />
			                    限制 <input type="text" id="familyLimit" value="" size="60" /></td>
                            </tr>
                            <tr>
			                    <th>個案優勢及限制</th>
			                    <td>優勢 <input type="text" id="caseStrengths" value="" size="60" /><br />
			                    限制 <input type="text" id="caseLimit" value="" size="60" /></td>
                            </tr>
                            <tr>
			                    <th>主要照顧者優勢及限制</th>
			                    <td>優勢 <input type="text" id="primaryCaseStrengths" value="" size="60" /><br />
			                    限制 <input type="text" id="primaryCaseLimit" value="" size="60" /></td>
                            </tr>
			            </table>
			            <p class="cP">家庭需求</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170" height="100">家庭主觀需求</th>
			                    <td>(家人對於孩子的看法、關注重點、期待等)<br /><textarea id="familyDemand"></textarea></td>
                            </tr>
                            <tr>
			                    <th height="80">社工客觀評定家庭需求</th>
			                    <td><textarea></textarea id="assessment"></td>
                            </tr>
                            <tr>
			                    <th height="150">交通路線</th>
			                    <td><img id="trafficRoute" src="#" alt=""><input type="file" name="trafficRoute" value="" /></td>
                            </tr>
                            <tr>
			                    <th height="80">備　　註</th>
			                    <td><textarea id="explanation6"></textarea></td>
                            </tr>
                            <tr>
			                    <th height="200">家訪照片</th>
			                    <td><img id="viewPhoto1" src="#" alt=""><input type="file" name="viewPhoto1" value="" /><input type="text" id="viewPhotoText1" value="" /><br />
			                        <img id="viewPhoto2" src="#" alt=""><input type="file" name="viewPhoto2" value="" /><input type="text" id="viewPhotoText2" value="" /><br />
			                        <img id="viewPhoto3" src="#" alt=""><input type="file" name="viewPhoto3" value="" /><input type="text" id="viewPhotoText3" value="" /><br />
			                        <img id="viewPhoto4" src="#" alt=""><input type="file" name="viewPhoto4" value="" /><input type="text" id="viewPhotoText4" value="" /><br />
			                        <img id="viewPhoto5" src="#" alt=""><input type="file" name="viewPhoto5" value="" /><input type="text" id="viewPhotoText5" value="" /><br />
			                        <img id="viewPhoto6" src="#" alt=""><input type="file" name="viewPhoto6" value="" /><input type="text" id="viewPhotoText6" value="" /><br />
			                        <img id="viewPhoto7" src="#" alt=""><input type="file" name="viewPhoto7" value="" /><input type="text" id="viewPhotoText7" value="" /><br />
			                        <img id="viewPhoto8" src="#" alt=""><input type="file" name="viewPhoto8" value="" /><input type="text" id="viewPhotoText8" value="" /></td>
                            </tr>
			            </table>
			            
			            <p class="cP">教學訪視紀錄</p>
			            <table class="tableText" width="780" border="0">
			                <tr>
			                    <th width="170" height="80">教學環境評估</th>
			                    <td><textarea id="teachingSpace"></textarea></td>
                            </tr>
                            <tr>
			                    <th height="80">家人支持療育情形</th>
			                    <td><textarea id="familySupport"></textarea></td>
                            </tr>
                            <tr>
			                    <th height="80">家人與個案之互動情形</th>
			                    <td><textarea id="familyInteraction"></textarea></td>
                            </tr>
                            <tr>
			                    <th height="80">家長教學情形與教師教學示範</th>
			                    <td><textarea id="teachingDemonstration"></textarea></td>
                            </tr>
                            <tr>
			                    <th height="80">訪談發現/討論與建議</th>
			                    <td><textarea id="viewConclusion"></textarea></td>
                            </tr>
                            <tr>
			                    <th height="80">教學優勢及限制分析</th>
			                    <td><textarea id="teachingAnalysis"></textarea></td>
                            </tr>
			            </table>
			        </div>
			        </form>
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
