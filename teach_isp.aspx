<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teach_isp.aspx.cs" Inherits="teach_isp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教學管理 - 個別化服務計畫書(ISP) | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/teach_isp.css" />
	<script type='text/javascript' src='http://code.jquery.com/jquery-1.7.2.min.js'></script>
	    
    <link rel="stylesheet" type="text/css" href="./css/jquery-ui-1.8.13.custom.css" />
    <link rel="stylesheet" type="text/css" href="./css/ui.dropdownchecklist.themeroller.css" />
    <script type="text/javascript" src="./js/jquery-ui-1.8.13.custom.min.js"></script>
    <script type="text/javascript" src="./js/ui.dropdownchecklist-1.4-min.js"></script>
	<script type="text/javascript" src="./js/jMonthCalendar.js"></script>
    
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/teach_isp.js"></script>
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
			<div id="mainClass">教學管理&gt; 個別化服務計畫書(ISP)</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./teach_isp.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input id ="gosrhstudentName" type="text" value=""  /></td>
			                <td width="260">性　　別 <select><option id="gosrhstudentSex" value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>服務使用者編號 <input id="gosrhstudentID"  type="text" value="" /></td>
			                <td>教師姓名 <input id="gosrhteachername" type="text" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="SearchstudentISP()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="80">服務使用者<br />編號</th>
			                    <th width="70">學生姓名</th>
			                    <th width="80">出生日期</th>
			                    <th width="70">年齡</th>
			                    <th width="130">法定代理人姓名</th>
			                    <th width="160">法定代理人聯絡電話(手機)</th>
			                    <th width="150">法定代理人聯絡電話(家)</th>
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
			    <div id="item1" class="menuTabs">個案基本資料</div>
			    <div id="item2" class="menuTabs">家庭服務計畫</div>
			    <div id="item3" class="menuTabs">聽力學管理</div>
			    <div id="item4" class="menuTabs">教學計畫</div>
			</div>
			<div id="mainContent">
			    <div id="item1Content">
			        <p align="right" style="background-color:#FFDF71;padding:0 10px;" id="caseUnit">台北至德</p>
			        <p class="cP">一、基本資料</p>
			        <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="150">學生姓名</th>
			                <td colspan="3"><input id="studentName" type="text" value="" /><span id="studentID" class="hideClassSpan"></span><span id="studentCID" style="display:none;">0</span></td>
			                </tr>
			            <tr>
			                <th>出生日期</th>
			                <td colspan="2">
			                <input type="text" id="studentbirthday" class="date" value=""/>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>性　　別</th>
			                <td colspan="2"><label><input type="radio" name="studentSex" value="1" /> 男</label>　　
			                <label><input type="radio" name="studentSex" value="2" /> 女</label></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>法定代理人</th>
			                <td colspan="2"><input id="LegalrepresentativeName" type="text" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>法定代理人連絡電話</th>
			                <td colspan="2">(公) <input id="LegalrepresentativePhone" type="text" value="" />　
			                    (手機) <input id="LegalrepresentativePhoneMobile" type="text" value="" /><br />
			                    (家) <input id="LegalrepresentativePhoneHome" type="text" value="" />　
			                    (傳真) <input id="LegalrepresentativePhoneFax" type="text" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>領有身心障礙手冊</th>
			                <td>
			                    <p><label><input type="radio" name="manualWhether" value="1" /> 是</label>
			                    （身心障礙類別 <input type="text" id="manualCategory1" value="" size="15" /> 等級 <input type="text" id="manualGrade1" value="" size="15" />）<br />
			                    　 　（身心障礙類別 <input type="text" id="manualCategory2" value="" size="15" /> 等級 <input type="text" id="manualGrade2" value="" size="15" />）<br />
			                    　 　（身心障礙類別 <input type="text" id="manualCategory3" value="" size="15" /> 等級 <input type="text" id="manualGrade3" value="" size="15" />）</p>
			                    <p><label><input type="radio" name="manualWhether" value="2" /> 否</label>
			                    （<label><input type="radio" name="manualNo" value="1" /> 未達申請標準</label>　
			                    <label><input type="radio" name="manualNo" value="2" /> 診斷證明</label>　
			                    <label><input type="radio" name="manualNo" value="3" /> 綜合評估報告書</label>）</p>
			                    <p><label><input type="radio" name="manualWhether" value="3" /> 申請中</label>（協助送件單位 <input id="manualUnit" type="text" value=""/>）</p>
			                </td>
			                <td>&nbsp;</td>
			                <th height="210"><p align="center">身心障礙手冊影本<br />
			                <a class="showUploadImg" href="./images/noPhoto.jpg"><img id="studentManualImg" src="./images/noPhoto.jpg" alt="" border="0" /></a></p></th>
			            </tr>
			            <tr>
		                <th>聽覺輔具</th>
		                <td colspan="3">
		                    <label><input type="radio" name="assistmanage" value="1" /> 無輔具</label>　　
		                    <label><input type="radio" name="assistmanage" value="2" /> 耳模製作中</label>　　
		                    <label><input type="radio" name="assistmanage" value="3" /> 已選配輔具（初配年齡<input  id="Accessory" type="text" value="" size="5" maxlength="3" />）</label>　　
		                    <label><input type="radio" name="assistmanage" value="4" /> 已植入人工電子耳</label><br />
		                    
		                    右耳：<label><input  type="radio" name="assistmanageR" value="1" /> 助聽器</label>，廠牌/型號
		                    <select id="BrandR1"> <option value="0">廠牌/型號</option></select>
		                    選配時間 <input id="BuyingtimeR" class="date" type="text" value="" size="10"/>，選配地點 <input id="BuyingPlaceR" type="text" value="" size="10" />
		                    <br />　　　
		                    <label><input type="radio" name="assistmanageR" value="2" /> 電子耳</label>，廠牌/型號
		                    <select id="BrandR2"><option value="">廠牌/型號</option></select>
		                    植入醫院 <input id="InsertHospitalR" type="text" value="" size="15" />，植入日 <input id="InsertDateR" class="date" type="text" value="" size="10" />
		                    <br />　　　　　　　　　　　　　　　　　　　　開頻日 <input id="OpenHzDateR" class="date" type="text" size="10" /><br />
		                    左耳：<label><input  type="radio" name="assistmanageL" value="1" /> 助聽器</label>，廠牌/型號
		                    <select id="BrandL1"><option value="0">廠牌/型號</option></select>
		                    選配時間 <input id="BuyingtimeL" class="date" type="text" value="" size="10"/>，選配地點 <input id="BuyingPlaceL" type="text" value="" size="10"/>
		                    <br />　　　<label><input type="radio" name="assistmanageL" value="2" /> 電子耳</label>，廠牌/型號
		                    <select id="BrandL2"><option value="0">廠牌/型號</option></select>
		                    植入醫院 <input id="InsertHospitalL" type="text" value="" size="15" />，植入日 <input id ="InsertDateL" class="date" type="text" size="10" />
		                    <br />　　　　　　　　　　　　　　　　　　　　開頻日 <input id="OpenHzDateL" class="date" type="text" value="" size="10" />
		                </td>
		            </tr>
			        </table>
			        <p class="cP">二、醫療與教育狀況</p>
			        <table class="tableText" width="780" border="0">
			            <tr>
			                <td colspan="2"><label><input type="radio" name="edu" value="1" /> 僅在本會接受課程(以下不填)</label>　
			                <label><input type="radio" name="edu" value="2" /> 尚在其他機構接受課程</label>
			                </td>
			            </tr>
			            <tr>
			                <td width="150"><label><input type="checkbox" name="edu1" value="1" /> 語言治療</label></td>
			                <td>地點/頻率：<input id="PandF1" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td><label><input type="checkbox" name="edu1" value="2" /> 物理治療</label></td>
			                <td>地點/頻率：<input id="PandF2" type="text" value="" size="50" /></td>
			            </tr>
			            <tr>
			                <td><label><input type="checkbox" name="edu1" value="" /> 職能治療</label></td>
			                <td>地點/頻率：<input id="PandF3" type="text" value="" size="50" /></td>
			            </tr>
			            <tr>
			                <td><label><input type="checkbox" name="edu1" value="4" /> 學前特教機構</label></td>
			                <td>地點/頻率：<input id="PandF4" type="text" value="" size="50" /></td>
			            </tr>
			            <tr>
			                <td><label><input type="checkbox" name="edu1" value="5" /> 一般幼托園所</label></td>
			                <td>地點/頻率：<input id="PandF5" type="text" value="" size="50" /></td>
			            </tr>
			            <tr>
			                <td><label><input type="checkbox" name="edu1" value="6" /> 一般小學</label></td>
			                <td>地點/頻率：<input id="PandF6" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td><label><input type="checkbox" name="edu1" value="7" /> 其他</label></td>
			                <td>地點/頻率：<input id="PandF7" type="text" value="" size="50" /></td>
			            </tr>
			        </table>
			        <p class="cP">三、服務計畫期程　<span>自 <input id="startPlanDate" class="date" type="text" value="" size="10" />
			                至 <input id="endPlanDate" class="date" type="text" value="" size="10" /></span></p>
			        <table class="tableContact" width="780" border="0"><caption>服務計畫會議</caption>
			           <thead><tr>
			                <th>日期</th>
			                <th>家長</th>
			                <th>教師</th>
			                <th>社工</th>
			                <th>聽力師</th>
			                <th>主管</th>
			                <th>相關專業人員</th>
			            </tr></thead>
			             <tbody>
			                 <tr>
			                    <td align="center"><input id="ServiceDate1" class="date" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="Parent1" class="hideClassSpan">0</span><input id="ParentName1" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="Teacher1" class="hideClassSpan">0</span><input id="TeacherName1" type="text" value="" size="10"/></td>
			                    <td align="center"><span id="Sociality1" class="hideClassSpan">0</span><input id="SocialityName1" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="ListenTutor1" class="hideClassSpan">0</span><input id="ListenTutorName1" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="Manager1" class="hideClassSpan">0</span><input id="ManagerName1" type="text" value="" size="10"/></td>
			                    <td align="center"><span id="RelationalPeople1" class="hideClassSpan">0</span><input id="RelationalPeopleName1" type="text" value="" size="10" /></td>
			                  </tr>
			                  <tr>
			                    <td align="center"><input id="ServiceDate2" class="date" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="Parent2" class="hideClassSpan">0</span><input id="ParentName2" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="Teacher2" class="hideClassSpan">0</span><input id="Teacher2" type="text" value="" size="10"/></td>
			                    <td align="center"><span id="Sociality2" class="hideClassSpan">0</span><input id="Sociality2" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="ListenTutor2" class="hideClassSpan">0</span><input id="ListenTutorName2" type="text" value="" size="10" /></td>
			                    <td align="center"><span id="Manager2" class="hideClassSpan">0</span><input id="Manager2" type="text" value="" size="10"/></td>
			                    <td align="center"><span id="RelationalPeople2" class="hideClassSpan">0</span><input id="RelationalPeopleName2" type="text" value="" size="10" /></td>
			                  </tr>
			            </tbody>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="SaveCaseISP(0);">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="SaveCaseISP(1);">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			            <button class="btnRevise" type="button">修 訂</button>
			        </p>
			    </div>
			    <div id="item2Content">
			        <p style="background-color:#FFDF71;padding:0 10px;text-align:right;">台北至德</p>
			        <table class="tableContact" width="780" border="0">
			            <tr>
			                <td>計畫撰寫者 <input id="planWriter" type="text" value="" readonly="readonly" /></td>
			                <td>擬定日期 <input id="PlanDate" class="date" type="text" value="" size="10" readonly="readonly" /></td>
			                <td>執行者 <input id="Excuter1" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>計畫修訂者 <input id="planEditor" type="text" value="" /></td>
			                <td>修訂日期 <input id="EditDate" class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input id="Excuter2" type="text" value="" /></td>
			            </tr>
			        </table>
			        
			        <table class="tableContact2" width="780" border="0">
			            <thead><tr>
			                <th rowspan="2" width="10">家<br />庭<br />需<br />求</th>
			                <th rowspan="2" width="170">服務目標</th>
			                <th colspan="3">執行內容</th>
			                <th colspan="3">追蹤紀錄</th>
			            </tr><tr>
			                <th>資源與方式</th>
			                <th>執行期間</th>
			                <th>執行者</th>
			                <th>追蹤日期</th>
			                <th>執行成效</th>
			                <th>服務執行情形</th>
			            </tr></thead>
			            <tbody><tr>
			                <td align="center">一、經濟需求</td>
			                <td>
			                    <input name="EconomicDemand" type="checkbox" value="1" /> <input id="EconomicDemandContent1" class = "EconomicDemandContent" type="text" value="協助申請早療補助" /><br />
			                    <input name="EconomicDemand" type="checkbox" value="2" /> <input id="EconomicDemandContent2" class = "EconomicDemandContent" type="text" value="協助申請生活補助" /><br />
			                    <input name="EconomicDemand" type="checkbox" value="3" /> <input id="EconomicDemandContent3" class = "EconomicDemandContent" type="text" value="" /><br />
			                    <input name="EconomicDemand" type="checkbox" value="4" /> <input id="EconomicDemandContent4" class = "EconomicDemandContent" type="text" value="" />
			                </td>
			                <td>
			                    資源:<br /><input id="ED_Resource" type="text" value="" size="15" /><br /><br />
			                    方式:
			                    <input id="ED_ResourceWay1"  class = "ED_ResourceWay" type="text" value="協助案家申請早療補助" /><br />
			                    <input id="ED_ResourceWay2" class = "ED_ResourceWay"  type="text" value="協助案家申請中低收入生活補助" /><br />
			                    <input id="ED_ResourceWay3" class = "ED_ResourceWay"  type="text" value="" /><br />
			                    <input id="ED_ResourceWay4" class = "ED_ResourceWay"  type="text" value="" />
			                </td>
			                <td align="center">
			                    <input id="ED_ExcuteDateStart1" class="date" type="text" value="" size="10" /><br />～<br /><input id="ED_ExcuteDateEnd1" class="date" type="text" value="" size="10" /><br /><br />
			                    <input id="ED_ExcuteDateStart2" class="date" type="text" value="" size="10" /><br />～<br /><input id="ED_ExcuteDateEnd2" class="date" type="text" value="" size="10" /><br /><br />
			                    <input id="ED_ExcuteDateStart3" class="date" type="text" value="" size="10" /><br />～<br /><input id="ED_ExcuteDateEnd3" class="date" type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input id="ED_Excuter1" class = "ED_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="ED_Excuter2" class = "ED_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="ED_Excuter3" class = "ED_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="ED_Excuter4" class = "ED_Excuter" type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea id="ED_Follow_date1"  class="date"></textarea><br /><br />
			                    <textarea id="ED_Follow_date2" class="date"></textarea><br /><br />
			                    <textarea id="ED_Follow_date3" class="date"></textarea><br /><br />
			                    <textarea id="ED_Follow_date4" class="date"></textarea>
			                </td>
			                <td>
			                    <select id ="ED_Excute_Performance1"  class = "ED_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="ED_Excute_Performance2" class = "ED_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="ED_Excute_Performance3" class = "ED_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="ED_Excute_Performance4" class = "ED_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select>
			                </td>
			                <td>
			                    <label><input class = "ED_ServiceExcuteSituation" type="checkbox" value="1" />提供台北市療育申請表單。<br />(因台北市療育費申請不需核章，故個案無須協助核章)</label><br />
                                <label><input class = "ED_ServiceExcuteSituation" type="checkbox" value="2" />已提供案母生活補助申請資格及所需文件。</label><br />
                                <label><input class = "ED_ServiceExcuteSituation" type="checkbox" value="3" />案母提出申請且符合資格。</label><br />
                                <label><input class = "ED_ServiceExcuteSituation" type="checkbox" value="4" />案母提出申請未符合資格。</label>
			                </td>
			            </tr><tr>
			                <td align="center">二、支持性服務</td>
			                <td>
			                    <input id="SupportService1" class = "SupportService" type="checkbox" value="0" /> <input id="SupportServiceContent1" class = "SupportServicContent" type="text" value="提供相關資訊及關懷案家情況" /><br />
			                    <input id="SupportService2" class = "SupportService" type="checkbox" value="1" /> <input id="SupportServiceContent2" class = "SupportServicContent" type="text" value="提供親職教育活動訊息" /><br />
			                    <input id="SupportService3" class = "SupportService" type="checkbox" value="2" /> <input id="SupportServiceContent3" class = "SupportServicContent" type="text" value="" /><br />
			                    <input id="SupportService4" class = "SupportService" type="checkbox" value="3" /> <input id="SupportServiceContent4" class = "SupportServicContent" type="text" value="" />
			                </td>
			                <td>
			                    資源<br /><input id="SS_Resource" type="text" value="" size="15" /><br /><br />
			                    <input id="SS_ResourceWay1"  class = "SS_ResourceWay" type="text" value="主動提供案家適切的社會福利資訊" /><br />
			                    <input id="SS_ResourceWay2"  class = "SS_ResourceWay" type="text" value="關心案母身心狀況" /><br />
			                    <input id="SS_ResourceWay3"  class = "SS_ResourceWay" type="text" value="主動提供案家適切的相關活動訊息，並邀請案家參與本中心活動" /><br />
			                    <input id="SS_ResourceWay4"  class = "SS_ResourceWay" type="text" value="" />
			                </td>
			                <td align="center">
			                    <input id="SS_ExcuteDateStart1" class="date" type="text" value="" size="10" /><br />～<br /><input id="SS_ExcuteDateEnd1" class="date" type="text" value="" size="10" /><br /><br />
			                    <input id="SS_ExcuteDateStart2" class="date" type="text" value="" size="10" /><br />～<br /><input id="SS_ExcuteDateEnd2" class="date"  type="text" value="" size="10" /><br /><br />
			                    <input id="SS_ExcuteDateStart3" class="date" type="text" value="" size="10" /><br />～<br /><input id="SS_ExcuteDateEnd3" class="date"  type="text" value="" size="10" /><br /><br />
			                    <input id="SS_ExcuteDateStart4" class="date" type="text" value="" size="10" /><br />～<br /><input id="SS_ExcuteDateEnd4" class="date"  type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input id="SS_Excuter1" class = "SS_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="SS_Excuter2" class = "SS_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="SS_Excuter3" class = "SS_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="SS_Excuter4" class = "SS_Excuter" type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea id="SS_Follow_date1" class="date"></textarea><br /><br />
			                    <textarea id="SS_Follow_date2" class="date"></textarea><br /><br />
			                    <textarea id="SS_Follow_date3" class="date"></textarea><br /><br />
			                    <textarea id="SS_Follow_date4" class="date"></textarea>
			                </td>
			                <td>
			                    <select id ="SS_Excute_Performance1"  class = "SS_Excute_Performance" ><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="SS_Excute_Performance2"  class = "SS_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="SS_Excute_Performance3"  class = "SS_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="SS_Excute_Performance4"  class = "SS_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select>
			                </td>
			                <td>
			                    <label><input class = "SS_ServiceExcuteSituation" type="checkbox" value="0" />案家能順利使用或取得相關的社福資源。</label><br />
                                <label><input class = "SS_ServiceExcuteSituation" type="checkbox" value="1" />因個案學習成效不彰，以致案母對於個案狀況(可能須植入CI)感到相當不安，故將多介入關懷，降低案母心理壓力。</label><br />
                                       <input class = "SS_ServiceExcuteSituation" type="checkbox" value="2" />
                                       <input id="SS_ServiceExcuteSituation1" type="text" value="" size="5" />年提供
                                       <input id="SS_ServiceExcuteSituation2" type="text" value="" size="5" />場活動訊息予案家，案家的參與狀況：
                                       <input id="SS_ServiceExcuteSituation3" type="text" value="" />
			                </td>
			            </tr><tr>
			                <td align="center">三、復健與醫療</td>
			                <td>
			                    <input id="RecoverandMedical1" class = "RecoverandMedical" type="checkbox" value="0" /> <input id="RecoverandMedicalContent1" class = "RecoverandMedicalContent" type="text" value="追蹤個案聽力檢查情況符合至少半年追蹤一次" /><br />
			                    <input id="RecoverandMedical2" class = "RecoverandMedical" type="checkbox" value="1" /> <input id="RecoverandMedicalContent2" class = "RecoverandMedicalContent" type="text" value="追蹤個案聽覺技巧訓練課程之需求性" /><br />
			                    <input id="RecoverandMedical3" class = "RecoverandMedical" type="checkbox" value="2" /> <input id="RecoverandMedicalContent3" class = "RecoverandMedicalContent" type="text" value="" /><br />
			                    <input id="RecoverandMedical4" class = "RecoverandMedical" type="checkbox" value="3" /> <input id="RecoverandMedicalContent4" class = "RecoverandMedicalContent" type="text" value="" />
			                </td>
			                <td>
			                    資源<br /><input id="RM_Resource" type="text" value="" size="15" /><br /><br />
			                    <input id="RM_ResourceWay1"  class = "RM_ResourceWay" type="text" value="於計劃執行完畢時確認聽檢紀錄" /><br />
			                    <input id="RM_ResourceWay2"  class = "RM_ResourceWay" type="text" value="於計劃執行完畢前一個月追蹤個案的學習情況" /><br />
			                    <input id="RM_ResourceWay3"  class = "RM_ResourceWay" type="text" value="" /><br />
			                    <input id="RM_ResourceWay4"  class = "RM_ResourceWay" type="text" value="" />
			                </td>
			                <td align="center">
			                    <input id="RM_ExcuteDateStart1" class="date" type="text" value="" size="10" /><br />～<br /><input id="RM_ExcuteDateEnd1" class="date" type="text" value="" size="10" /><br /><br />
			                    <input id="RM_ExcuteDateStart2" class="date" type="text" value="" size="10" /><br />～<br /><input id="RM_ExcuteDateEnd2" class="date" type="text" value="" size="10" /><br /><br />
			                    <input id="RM_ExcuteDateStart3" class="date" type="text" value="" size="10" /><br />～<br /><input id="RM_ExcuteDateEnd3" class="date" type="text" value="" size="10" /><br /><br />
			                    <input id="RM_ExcuteDateStart4" class="date" type="text" value="" size="10" /><br />～<br /><input id="RM_ExcuteDateEnd4" class="date" type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input id="RM_Excuter1" class = "RM_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="RM_Excuter2" class = "RM_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="RM_Excuter3" class = "RM_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="RM_Excuter4" class = "RM_Excuter" type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea id="RM_Follow_date1" class="date"></textarea><br /><br />
			                    <textarea id="RM_Follow_date2" class="date"></textarea><br /><br />
			                    <textarea id="RM_Follow_date3" class="date"></textarea><br /><br />
			                    <textarea id="RM_Follow_date4" class="date"></textarea>
			                </td>
			                <td>
			                    <select id ="RM_Excute_Performance1"  class = "RM_Excute_Performance" ><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="RM_Excute_Performance2"  class = "RM_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="RM_Excute_Performance3"  class = "RM_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="RM_Excute_Performance4"  class = "RM_Excute_Performance"><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select>
			                </td>
			                <td>
			                    <label><input class = "RM_ServiceExcuteSituation" type="checkbox" value="0" />個案聽力檢查皆有符合至少半年追蹤一次。</label><br />
                                <label><input  class = "RM_ServiceExcuteSituation" type="checkbox" value="1" />個案聽力檢查未符合至少半年追蹤一次，原因：</label><input type="text" value="" /><br />
                                <label><input class = "RM_ServiceExcuteSituation" type="checkbox" value="2" />個案的學習狀況穩定，經教學組年度評估仍有訓練需求，需持續學習。</label><br />
                                <label><input  class = "RM_ServiceExcuteSituation" type="checkbox" value="2" />個案的學習狀況穩定，經教學組年度評估已符合常童水準，可辦理結案。</label><br />
                                <label><input class = "RM_ServiceExcuteSituation" type="checkbox" value="3" />其他：</label><input id="RM_ServiceExcuteSituation1" type="text" value="" />
			                </td>
			            </tr><tr>
			                <td align="center">四、教育資源連結</td>
			                <td>
			                    <input id="EducatereSource1" class = "EducatereSource" type="checkbox" value="0" /> <input id="EducatereSourceContent1" class = "EducatereSourceContent" type="text" value="協助案母暸解幼兒園所相關資訊" /><br />
			                    <input id="EducatereSource2" class = "EducatereSource" type="checkbox" value="1" /> <input id="EducatereSourceContent2" class = "EducatereSourceContent" type="text" value="" /><br />
			                    <input id="EducatereSource3" class = "EducatereSource" type="checkbox" value="2" /> <input id="EducatereSourceContent3" class = "EducatereSourceContent" type="text" value="" /><br />
			                    <input id="EducatereSource4" class = "EducatereSource" type="checkbox" value="3" /> <input id="EducatereSourceContent4" class = "EducatereSourceContent" type="text" value="" />
			                </td>
			                <td>
			                    資源<br /><input id="ES_Resource" type="text" value="" size="15" /><br /><br />
			                    <input id="ES_ResourceWay1"  class = "ES_ResourceWay" type="text" value="協助提供入學資訊並追蹤個案回歸幼兒園所的學習狀況" /><br />
			                    <input id="ES_ResourceWay2"  class = "ES_ResourceWay" type="text" value="" /><br />
			                    <input id="ES_ResourceWay3"  class = "ES_ResourceWay" type="text" value="" /><br />
			                    <input id="ES_ResourceWay4"  class = "ES_ResourceWay" type="text" value="" />
			                </td>
			                <td align="center">
			                    <input id="ES_ExcuteDateStart1" class="date" type="text" value="" size="10" /><br />～<br /><input id="ES_ExcuteDateEnd1"  class="date" type="text" value="" size="10" /><br /><br />
			                    <input id="ES_ExcuteDateStart2" class="date" type="text" value="" size="10" /><br />～<br /><input id="ES_ExcuteDateEnd2" class="date" type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input id="ES_Excuter1" class = "ES_Excuter" type="text" value="" size="10" /><br /><br />
			                    <input id="ES_Excuter2" class = "ES_Excuter" type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea id="ES_Follow_date1" class="date"></textarea><br /><br />
			                    <textarea id="ES_Follow_date2" class="date"></textarea>
			                </td>
			                <td>
			                    <select id ="ES_Excute_Performance1" ><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select><br /><br />
			                    <select id ="ES_Excute_Performance2" ><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option></select>
			                </td>
			                <td>
			                    <label><input class= "ES_ServiceExcuteSituation"  type="checkbox" value="1" />已提供台北啟聰幼兒園入園資訊予案家，並鼓勵案家參與啟聰學校辦理之親師體驗營。</label><br />
                                <label><input class= "ES_ServiceExcuteSituation" type="checkbox" value="2" />個案學習狀況穩定(包含學業、人際互動等)暫無需其他資源介入。</label><br />
                                <label><input class= "ES_ServiceExcuteSituation" type="checkbox" value="3" />個案仍需要其他資源介入：</label><input id="ES_ServiceExcuteSituation1" type="text" value="" />
			                </td>
			            </tr></tbody>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			            <button class="btnRevise" type="button">修 訂</button>
			        </p>
			    </div>
			    <div id="item3Content">
			        <p style="background-color:#FFDF71;padding:0 10px;text-align:right;">台北至德</p>
			        <table class="tableContact" width="780" border="0">
			            <tr>
			                <td>計畫撰寫者 <input id="HE_ProjectWriter" type="text" value="" readonly="readonly"/></td>
			                <td>擬定日期 <input id="HE_ProjectDate" class="date" type="text" value="" size="10" readonly="readonly"/></td>
			                <td>執行者 <input id="HE_ProjectExcuter" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>計畫修訂者 <input id="HE_ProjectEditor" type="text" value="" /></td>
			                <td>修訂日期 <input id="HE_EditDate" class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input id="HE_ProjectExcuter2" type="text" value="" /></td>
			            </tr>
			        </table>
			        
			        <table class="tableContact2" width="780" border="0">
			            <thead><tr>
			                <th>項目</th>
			                <th>評量結果/現況描述</th>
			                <th>評量者/日期</th>
			                <th>評量方式</th>
			            </tr></thead>
			            <tbody><tr>
			                <td align="center" width="80">聽力檢查</td>
			                <td width="540"><ul>
			                    <li>1. 中耳功能：左耳 
			                    <select id="EarFuntionL">
			                        <option value="0">未測</option>
			                        <option value="1">在正常範圍內</option>
			                        <option value="2">呈負壓</option>
			                        <option value="3">耳膜順應力低</option>
			                        <option value="4">耳膜破洞</option>
			                    </select>，右耳 
			                    <select id="EarFuntionR">
			                        <option value="0">未測</option>
			                        <option value="1">在正常範圍內</option>
			                        <option value="2">呈負壓</option>
			                        <option value="3">耳膜順應力低</option>
			                        <option value="4">耳膜破洞</option>
			                    </select></li>
			                    <li>2. 裸耳<br />
			                        <table class="tableContact" width="500" border="0">
			                            <thead><tr>
			                                <th>dB HL</th>
			                                <th>250</th>
			                                <th>500</th>
			                                <th>1000</th>
			                                <th>2000</th>
			                                <th>4000</th>
			                                <th>8000</th>
			                                <th>平均</th>				
			                            </tr></thead>
			                            <tbody><tr>
			                                <td>左</td>
			                                <td><input id="Naked_L_250" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_L_500" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_L_1000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_L_2000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_L_4000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_L_8000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_L_Average" type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>右</td>
			                                <td><input id="Naked_R_250" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_R_500" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_R_1000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_R_2000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_R_4000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_R_8000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_R_Average" type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>不分耳</td>
			                                <td><input id="Naked_B_250" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_B_500" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_B_1000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_B_2000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_B_4000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_B_8000" type="text" value="" size="5" /></td>
			                                <td><input id="Naked_B_Average" type="text" value="" size="5" /></td>
			                            </tr></tbody>
			                        </table>
			                    </li>
			                    <li>3. 助聽後<br />
			                        <table class="tableContact" width="500" border="0">
			                            <thead><tr>
			                                <th>dB HL</th>
			                                <th>250</th>
			                                <th>500</th>
			                                <th>1000</th>
			                                <th>2000</th>
			                                <th>4000</th>
			                                <th>8000</th>
			                                <th>平均</th>				
			                            </tr></thead>
			                            <tbody><tr>
			                                <td>左</td>
			                                <td><input id="AferFix_L_250" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_L_500" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_L_1000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_L_2000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_L_4000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_L_8000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_L_Average" type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>右</td>
			                                <td><input id="AferFix_R_250" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_R_500" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_R_1000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_R_2000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_R_4000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_R_8000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_R_Average" type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>不分耳</td>
			                                <td><input id="AferFix_B_250" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_B_500" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_B_1000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_B_2000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_B_4000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_B_8000" type="text" value="" size="5" /></td>
			                                <td><input id="AferFix_B_Average" type="text" value="" size="5" /></td>
			                            </tr></tbody>
			                        </table>
			                    </li>
			                    <li>4. 其他(請說明)<br /><textarea id="Others"  cols="65" rows="1"></textarea></li>
			                </ul></td>
			                <td align="center" width="80">評量者<br /><input id="HI_inspector_Way" type="text" value="" size="9" /><br />日期<br /><input id="HI_inspector_Date" class="date" type="text" value="" size="9" /></td>
			                <td align="center" width="80"><textarea id="HI_inspectorWay1" cols="5" rows="15"></textarea></td>
			            </tr>
			            <tr>
			                <td align="center">聽覺能力</td>
			                <td><ul>
			                    <li>1. <select id="HP_1" ><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input id="HP_Explain_1" type="text" value="" size="50" />
			                    </li>
			                    <li>2. <select id="HP_2"><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input id="HP_Explain_2" type="text" value="" size="50" />
			                    </li>
			                    <li>3. <select id="HP_3"><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input id="HP_Explain_3" type="text" value="" size="50" />
			                    </li>
			                    <li>4. <select id="HP_4"><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input id="HP_Explain_4" type="text" value="" size="50" />
			                    </li>
			                </ul></td>
			                <td  align="center" width="80">評量者<br /><input id="HP_inspectorWay" type="text" value="" size="9" /><br />日期<input id="HP_inspector_Date" class="date" type="text" value="" size="9" /></td>			                
			                <td  align="center" width="80">
			                    <input id="HP_InspectWay1" type="text" value="" size="9" /><br />
			                    <input id="HP_InspectWay2" type="text" value="" size="9" /><br />
			                    <input id="HP_InspectWay3" type="text" value="" size="9" /><br />
			                    <input id="HP_InspectWay4" type="text" value="" size="9" /></td>
			            </tr><tr>
			                <td align="center">輔具管理</td>
			                <td><ul>
			                    <li>1. 配戴輔具的習慣與時間：
			                        <select id="Helping_Manager1">
			                            <option value="0">請選擇</option>
			                            <option value="1">完全不願意配戴輔具</option>
			                            <option value="2">時常拉扯輔具，且配戴時間不長</option>
			                            <option value="3">只在某些特定時間/場合配戴輔具，其餘時間不願配合</option>
			                            <option value="4">除了洗澡和睡覺時間外，能全天配戴輔具</option>
			                            <option value="5">會主動要求配戴輔具</option>
			                            <option value="6">會自行配戴及取下輔具</option>
			                        </select>
			                    </li>
			                    <li>2. 配戴及取下助聽輔具：
			                        <select id="Helping_Manager2">
			                            <option value="0">請選擇</option>
			                            <option value="1">尚未能自行配戴及取下輔具</option>
			                            <option value="2">會自行配戴及取下輔具，但方法不正確</option>
			                            <option value="3">會正確地配戴及取下輔具</option>
			                        </select>
			                    </li>
			                    <li>3. 反映助聽輔具的聲音輸出狀況：
			                        <select id="Helping_Manager3">
			                            <option value="0">請選擇</option>
			                            <option value="1">尚未能反應反映輔具聲音狀況</option>
			                            <option value="2">會反映聲音大小聲</option>
			                            <option value="3">會反映聲音清楚與否</option>
			                            <option value="4">會反映音質正常與否</option>
			                        </select>
			                    </li>
			                    <li>4. 助聽輔具的操作：
			                        <select id="Helping_Manager4">
			                            <option value="0">請選擇</option>
			                            <option value="1">不會自行操作輔具</option>
			                            <option value="2">會開、關助聽器(電池蓋、控制鈕)</option>
			                            <option value="3">會在適當的情境自行調整音量及或明敏度</option>
			                            <option value="4">會在適當的情境自行更換程式</option>
			                            <option value="5">會正確更換電池</option>
			                            <option value="6">會自行為輔具(電子耳、調頻系統)充電</option>
			                            <option value="7">會分辨左、右耳輔具</option>
			                            <option value="8">會主動在上課前將發射器交給老師</option>
			                        </select>
			                    </li>
			                    <li>5. 助聽輔具的簡易保養：
			                        <select id="Helping_Manager5">
			                            <option value="0">請選擇</option>
			                            <option value="1">尚不理解保養的概念，完全由家長負責</option>
			                            <option value="2">知道保養包配件的用途</option>
			                            <option value="3">知道水及汗水對輔具的影響</option>
			                            <option value="4">知道輔具的置放處</option>
			                            <option value="5">知道電池的置放處</option>
			                            <option value="6">知道廢棄電池的處理方式</option>
			                            <option value="7">會正確使用保養包配件清潔輔具</option>
			                            <option value="8">會把取下的助聽器輔具放在正確的置放處</option>
			                            <option value="9">會自行以正確方式將輔具放入除濕盒</option>
			                        </select>
			                    </li>
			                </ul></td>
			                <td align="center" width="80">評量者<br /><input id="HM_inspector" type="text" value="" size="9" /><br />日期<br /><input id="HM_inspectorDate" class="date" type="text" value="" size="9" /></td>		                <td align="center" width="80">       
			                <td align="center" width="80"><textarea id="HM_inspectorWay" cols="5" rows="5"></textarea></td>
			            </tr><tr>
			                <td align="center">綜合摘要</td>
			                <td colspan="3"><textarea id="SummaryPoint" cols="100" rows="5"></textarea></td>
			            </tr></tbody>
			        </table>
			        <p>聽力覺管理目標</p>
			        <table class="tableContact" width="780" border="0">
			        <thead><tr>
		                <th width="150" rowspan="2">長期目標</th>
		                <th width="180" rowspan="2">短期目標</th>
		                <th colspan="2">起迄日期</th>
		                <th colspan="3">成效評量</th>
		                <th width="50" rowspan="2">教學<br />決定</th>
			        </tr>
			        <tr>
			            <th>始</th>
			            <th>末</th>
			            <th>日期</th>
			            <th>方式</th>
			            <th>結果</th>
			        </tr>
			        </thead>
			        <tbody><tr>
		                <td rowspan="6"><textarea cols="15" rows="3">1. 建立全天配戴助聽輔具"</textarea></td>
		                <td><input type="text" value="1-1 能配戴輔具至少20分鐘" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" value="1-2 能在上課時間配戴輔具不拉扯(配戴1小時以上)" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="1-3 每天配戴輔具達4小時" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="1-4 每天配戴輔具達8小時" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="1-5除洗澡和睡覺時間外，能全天配戴輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="1-6 能主動要求配戴輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="5"><textarea cols="15" rows="3">2. 能正確配戴及取下輔具</textarea></td>
		                <td><input type="text" value="2-1 主要照顧者能正確替學童配戴及取下個人助聽輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" value="2-2 學童能配合並協助將助聽個人輔具配戴好" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="2-3學童會分辨左、右耳輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="2-4學童能自行正確配戴個人助聽輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="2-5 學童能自行正確取下個人助聽輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="12"><textarea cols="15" rows="3">3. 能反映輔具的聲音狀況</textarea></td>
		                <td><input type="text" value="3-1 主要照顧者會以測聽方式檢查輔具的輸出音量及/或音質是否正常" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" value="3-2 主要照顧者能每日以測聽方式檢查輔具的輸出音量及/或音質是否正常" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-3 主要照顧者能以觀察方式反應輔具的輸出音量及/或音質是否正常" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-4 學童能察覺雙側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-5學童能主動告知雙側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-6學童能察覺單側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-7學童能主動告知單側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-8 學童能察覺輔具音察覺輔具音量的大小" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-9 學童能主動反映輔具音量異常" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-10 學童能主動反映輔具有噪音" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-11 學童能主動反映音質異常" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="3-12 學童能清楚描述音量及音質的異常情況" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="11"><textarea cols="15" rows="3">4. 能操作助聽輔具</textarea></td>
		                <td><input type="text" value="4-1 主要照顧者能了解輔具的各個控制項，並能正確操作" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" value="4-2 學童會開、關輔具 (電池蓋、控制鈕…)" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-3 學童會正確更換電池" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-4 學童會正確連接耳模與輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-5 學童會主動在上課前將調頻系統發射器交給老師" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-6 學童會主動在上課前要求配戴個人調頻系統輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-7學童會正確連接個人輔具與調頻系統" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-8學童會正確開關個人調頻系統輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-9 學童會在適當情境更換程式/電流圖" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-10 學童會在適當情境調整音量、靈敏度" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="4-11 學童會自行為輔具充電" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="9"><textarea cols="15" rows="3">5. 能保養輔具</textarea></td>
		                <td><input type="text" value="5-1主要照顧者能了解保養工具的用途，並能正確使用保養工具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" value="5-2 主要照顧者能定期保養輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="5-3 學童能了解保養包工具的用途" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="5-4 學童能了解輔具保養的概念" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="5-5 學童能了解水及汗水對輔具的影響" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="5-6 學童能了解輔具及電池的置放處" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="5-7 學童能了解輔具及電池的置放方式" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="5-8 學童能了解廢棄電池的處理方法" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" value="5-9 學童能正確使用保養包工具清潔輔具" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><input class="date" type="text" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            </tbody>
                    </table>
                    <table width="780" border="0" align="center">
	                    <tr>
	                        <td colspan="4" align="center">方式：觀察(A)　問答(B)　操作(C)　紙筆(D)　其他(E)</td>
	                    </tr>
	                    <tr>
	                        <td width="80" rowspan="3">&nbsp;</td>
	                        <td width="420" align="right">結果：五次測試，達成4次或5次 (達成率80%以上)，完全達成(3)</td>
	                        <td width="200" align="right">教學決定：通過目標(○)</td>
	                        <td width="80" rowspan="3">&nbsp;</td>
	                    </tr>
	                    <tr>
	                        <td align="right">五次測試，達成2次或3次 (達成率40~60%)，部分達成(2)</td>
	                        <td align="right">繼續目標(&empty;)</td>
	                    </tr>
	                    <tr>
	                        <td align="right">五次測試，達成0次或1次 (達成率20%以下)，未達成(1)</td>
	                        <td align="right">修正目標(&otimes;)</td>
	                    </tr>
	                </table>
			        <p class="btnP">
			            <button class="btnSave" type="button">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			            <button class="btnRevise" type="button">修 訂</button>
			        </p>
			    </div>
			    <div id="item4Content">
			        <p style="background-color:#FFDF71;padding:0 10px;text-align:right;">台北至德</p>
			        <table class="tableContact" width="780" border="0">
			            <tr>
			                <td width="300">計畫撰寫者 <input id="EP_Writer" type="text" readonly="readonly" value=""/></td>
			                <td>擬定日期 <input id="EP_Plan_Date" class="date" type="text" size="10" readonly="readonly"/></td>
			                <td>執行者 <input id="EP_Excuter1" type="text" /></td>
			            </tr>
			             <tr>
			                <td>計畫修訂者 <input id="EP_Editor" type="text" /></td>
			                <td>修訂日期 <input id="EP_Edit_Date" class="date" type="text" size="10" /></td>
			                <td>執行者 <input id="EP_Excuter2" type="text" /></td>
			            </tr>
			        </table>
			         <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp;</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" /></td>
		                   </tr>
			            </table>   
			        </div>
			         <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp;</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" /></td>
		                   </tr>
			            </table>   
			        </div>
			         <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp;</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" /></td>
		                   </tr>
			            </table>   
			        </div>
			         <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp;</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" /></td>
		                   </tr>
			            </table>   
			        </div>
			         <div class="Perceive">
			            <table class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>
			               <tr>
			                   <th width="50">&nbsp;</th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			                   <th width="40"><select class="perceiveUp"><option></option></select></th>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
			               <tr>
                               <th><select class="perceiveRight"><option></option></select></th>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			                   <td><select class="perceiveContent"><option></option></select></td>
			               </tr>
		                   <tr>
		                       <th>日期</th>
			                   <td colspan="5"><input class="date" type="text" size="10" /></td>
		                   </tr>
		                   <tr>
		                       <th>備註</th>
			                   <td colspan="5"><input type="text" /></td>
		                   </tr>
			            </table>   
			        </div>
			        <div class="Perceive">
			        </div>
			        <p class="cP">學生服務時間表</p>
			        <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="150">課程名稱</th>
			                <td><select><option value="0">請選擇</option><option value="1">個別課</option><option value="2">故事課</option><option value="3">認知語言課</option></select></td>
			                </tr>
			            <tr>
			                <th>服務期程</th>
			                <td><input classe="date" type="text" value="" size="10" />～<input class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <th>上課時間</th>
			                <td>
			                    <select><option value="0">請選擇</option><option value="1">每週</option><option value="2">隔單週</option><option value="3">隔雙週</option><option value="4">每月</option></select>；
			                    <select><option value="0">請選擇星期</option><option value="1">星期一</option><option value="2">星期二</option><option value="3">星期三</option><option value="4">星期四</option><option value="5">星期五</option><option value="6">星期六</option><option value="7">星期日</option></select>
			                    <select><option value="0">請選擇時間</option><option value="1">09:00</option><option value="2">09:30</option></select>～<select><option value="0">請選擇時間</option><option value="1">09:00</option><option value="2">09:30</option></select>
			                </td>
			            </tr>
			            <tr>
			                <th>教師姓名</th>
			                <td><input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <th>教室名稱</th>
			                <td><select><option value="0">請選擇</option><option value="1">E01</option><option value="2">E02</option><option value="3">E03</option><option value="4">E04</option></select></td>
			            </tr>
			            <tr>
			                <td align="center" colspan="2"><input type="button" value="課程確認" /></td>
			            </tr>
                    </table>
                    <p><br /></p>
			        <div id="jMonthCalendar"></div>
			        <p><br /></p>
			        <p class="cP">一、個案評量記錄</p>
			        <table class="tableContact2" width="780" border="0">
			            <thead><tr>
			                <th width="100">項目</th>
			                <th>評量結果摘要</th>
			                <th width="130">評量者</th>
			                <th width="100">日期</th>
			                <th width="100">評量工具</th>
			            </tr></thead>
			            <tr>
			                <td align="center">聽覺技巧</td>
			                <td><textarea id="ResultSumary1" rows="2" cols="40"></textarea></td>
			                <td><input id="inspector1" type="text" name="" value="" size="15" /></td>
			                <td><input id="CaseDate1" class="date" type="text" name="" value="" size="10"/></td>
			                <td><input id="inspectorTool1" type="text" name="" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td align="center">詞　　彙</td>
			                <td><textarea id="ResultSumary2"  rows="2" cols="40"></textarea></td>
			                <td><input id="inspector2" type="text" name="" value="" size="15" /></td>
			                <td><input id="CaseDate2" class="date" type="text" name="" value="" size="10"/></td>
			                <td><input id="inspectorTool2" type="text" name="" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td align="center">語言能力</td>
			                <td><textarea id="ResultSumary3"  rows="2" cols="40"></textarea></td>
			                <td><input id="inspector3" type="text" name="" value="" size="15" /></td>
			                <td><input id="CaseDate3" class="date" type="text" name="" value="" size="10"/></td>
			                <td><input id="inspectorTool3" type="text" name="" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td align="center">智　　力</td>
			                <td><textarea id="ResultSumary4"  rows="2" cols="40"></textarea></td>
			                <td><input id="inspector4" type="text" name="" value="" size="15" /></td>
			                <td><input id="CaseDate4" class="date" type="text" name="" value="" size="10"/></td>
			                <td><input id="inspectorTool4" type="text" name="" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td align="center">其　　它</td>
			                <td><textarea id="ResultSumary5"  rows="2" cols="40"></textarea></td>
			                <td><input id="inspector5" type="text" name="" value="" size="15" /></td>
			                <td><input id="CaseDate5" class="date" type="text" name="" value="" size="10"/></td>
			                <td><input id="inspectorTool5" type="text" name="" value="" size="10" /></td>
			            </tr>
			        </table>
			        <p class="cP">二、個案能力現況（依標準化測驗以及檢核手冊之觀察評量）</p>
			        <table class="tableContact2" width="780" border="0">
			            <thead><tr>
			                <th width="150" align="center">領域</th>
			                <th>能力現況描述</th>
			            </tr></thead>
			            <tr>
			                <td   align="center">聽覺能力</td>
			                <td><input id="CaseStatus_Ability1" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td  align="center">認知能力</td>
			                <td><input id="CaseStatus_Ability2" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td align="center">溝通能力</td>
			                <td><input id="CaseStatus_Ability3" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td  align="center">行動能力</td>
			                <td><input id="CaseStatus_Ability4" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td  align="center">人際關係</td>
			                <td><input id="CaseStatus_Ability5" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td   align="center">情緒管理</td>
			                <td><input id="CaseStatus_Ability6" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td  align="center">感官功能</td>
			                <td><input id="CaseStatus_Ability7" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td align="center">健康狀況</td>
			                <td><input id="CaseStatus_Ability8" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td align="center">生活自理</td>
			                <td><input id="CaseStatus_Ability9" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td align="center">學習特質</td>
			                <td><input id="CaseStatus_Ability10" type="text" name="" value="" size="90"/></td>
			            </tr>
			            <tr>
			                <td align="center">綜合摘要</td>
			                <td>綜合上述可分析個案的優弱勢能力如下：<br />
			                優勢能力<br />
			                <textarea id="CaseStatus_Ability11" cols="50" rows="5"></textarea><br />
			                弱勢能力<br />
			                <textarea id="CaseStatus_Ability12" cols="50" rows="5"></textarea>
			                </td>
			            </tr>
			        </table>
			        
			        <p class="cP">三、個案教學目標</p>
			        <p class="cP1">領域(一) 聽覺</p>
			        <table id="table1" class="tableContact2" width="780" border="0">
			        <thead><tr>
		                <td width="305" rowspan="2" align="center"><span class="cP1">聽覺輔具</span>　<button type="button" class="btnAdd" onclick="getAdd(1)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(1)">－</button></td>
		                <th colspan="2">起迄日期</th>
		                <th colspan="3">成效評量</th>
		                <th width="70" rowspan="2">教學<br />決定</th>
			        </tr>
			        <tr>
			            <th width="90">始</th>
			            <th width="90">末</th>
			            <th width="90">日期</th>
			            <th width="65">方式</th>
			            <th width="70">結果</th>
			        </tr>
			        </thead>
			        <tbody>
			        <tr id="dataTR1">
			            <td colspan="7">
			                <table class="tableContact3" width="774" border="0">
			                    <tr>
			                        <td colspan="7"><textarea id="" class="long" cols="50" rows="2"></textarea></td>
			                    </tr>
			                    <tr>
			                        <td width="300">
			                            <textarea id ="Target1" class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input id="TargetStart1_Date_Start1" class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input id="TargetStart1_Date_End1" class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input id="TargetDate" class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way1" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select id="TargetStart1_Conclusion"><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select id="TargetStart1_Decision"><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way112" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way113" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way114" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way115" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
		                    </table>
		                </td>
		            </tr>
		            </tbody>
                    </table>
                    
                    <table id="table2" class="tableContact2" width="780" border="0">
			        <thead><tr>
		                <td width="305" rowspan="2" align="center"><span class="cP1">聽覺技巧</span>　<button type="button" class="btnAdd" onclick="getAdd(2)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(2)">－</button></td>
		                <th colspan="2">起迄日期</th>
		                <th colspan="3">成效評量</th>
		                <th width="70" rowspan="2">教學<br />決定</th>
			        </tr>
			        <tr>
			            <th width="90">始</th>
			            <th width="90">末</th>
			            <th width="90">日期</th>
			            <th width="65">方式</th>
			            <th width="70">結果</th>
			        </tr>
			        </thead>
			        <tbody>
			        <tr id="dataTR2">
			            <td colspan="7">
			                <table class="tableContact3" width="774" border="0">
			                    <tr>
			                        <td colspan="7"><textarea class="long" cols="50" rows="2"></textarea></td>
			                    </tr>
			                    <tr>
			                        <td width="300">
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way211" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way212" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way213" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way214" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way215" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
		                    </table>
		                </td>
		            </tr>
		            </tbody>
                    </table>
			        
			        <p class="cP1">領域(二) 認知語言及溝通技巧</p>
			        <table id="table3" class="tableContact2" width="780" border="0">
			        <thead><tr>
		                <td width="305" rowspan="2" align="center"><button type="button" class="btnAdd" onclick="getAdd(3)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(3)">－</button></td>
		                <th colspan="2">起迄日期</th>
		                <th colspan="3">成效評量</th>
		                <th width="70" rowspan="2">教學<br />決定</th>
			        </tr>
			        <tr>
			            <th width="90">始</th>
			            <th width="90">末</th>
			            <th width="90">日期</th>
			            <th width="65">方式</th>
			            <th width="70">結果</th>
			        </tr>
			        </thead>
			        <tbody>
			        <tr id="dataTR3">
			            <td colspan="7">
			                <table class="tableContact3" width="774" border="0">
			                    <tr>
			                        <td colspan="7"><textarea class="long" cols="50" rows="2"></textarea></td>
			                    </tr>
			                    <tr>
			                        <td width="300">
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way311" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way312" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way313" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way314" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way315" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
		                    </table>
		                </td>
		            </tr>
		            </tbody>
                    </table>
			        
			        <p class="cP1">領域（三）其它特殊需求（針對baby、多障、情障等特殊個案特殊需求）</p>
			        <table id="table4" class="tableContact2" width="780" border="0">
			        <thead><tr>
		                <td width="305" rowspan="2" align="center"><button type="button" class="btnAdd" onclick="getAdd(4)">＋</button>　<button type="button" class="btnAdd" onclick="getSubtract(4)">－</button></td>
		                <th colspan="2">起迄日期</th>
		                <th colspan="3">成效評量</th>
		                <th width="70" rowspan="2">教學<br />決定</th>
			        </tr>
			        <tr>
			            <th width="90">始</th>
			            <th width="90">末</th>
			            <th width="90">日期</th>
			            <th width="65">方式</th>
			            <th width="70">結果</th>
			        </tr>
			        </thead>
			        <tbody>
			        <tr id="dataTR4">
			            <td colspan="7">
			                <table class="tableContact3" width="774" border="0">
			                    <tr>
			                        <td colspan="7"><textarea class="long" cols="50" rows="2"></textarea></td>
			                    </tr>
			                    <tr>
			                        <td width="300">
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way411" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way412" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way413" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way414" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
			                    <tr>
			                        <td>
			                            <textarea class="short" cols="50" rows="2"></textarea>
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td>
			                            <input class="date" type="text" value="" size="10" />
			                        </td>
			                        <td class="wayTD">
			                            <select id="way415" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select>
			                        </td>
			                        <td>
			                            <select><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select>
			                        </td>
			                    </tr>
		                    </table>
		                </td>
		            </tr>
		            </tbody>
                    </table>
			        
			        <p class="btnP">
			            <button class="btnSave" type="button">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			            <button class="btnRevise" type="button">修 訂</button>
			        </p>
			    </div>
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
