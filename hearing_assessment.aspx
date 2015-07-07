<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_assessment.aspx.cs" Inherits="hearing_assessment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>聽力管理 - 聽力評估報告 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_assessment.css" />
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
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/hearing_assessment.js" />
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
			<div id="mainClass">聽力管理&gt; 聽力評估報告</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./hearing_assessment.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
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
			                <td>檢查日期 <input id="gosrhcheckDatestart" class="date" type="text" value="" size="10" />～<input id="gosrhcheckDateend" class="date" type="text" value="" size="10" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search();">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">學生姓名</th>
			                    <th width="100">出生日期</th>
			                    <th width="140">檢查日期</th>
			                    <th width="100">檢查年齡</th>
			                    <th width="100">聽力師</th>
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
			    <p align="right" id="caseUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
			    <p align="right">聽力師 <input id="audiologistName" type="text" value="" />　　　　檢查日期 <input id="checkDate" class="date" type="text" value="" size="10" /></p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">學生姓名</th>
			            <td><input id="studentName" type="text" value="" readonly="readonly"/><span id="studentID" class="hideClassSpan"></span><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td id="studentbirthday"></td>
		            </tr>
		            <tr>
			            <th>年　　齡</th>
			            <td><input id="studentAge" type="text" size="5" value=""/> 歲 <input id="studentMonth" type="text" size="5" value=""/> 月</td>
			        </tr>
			        <tr>
			            <th>使用輔具</th>
			            <td><select id="studentUseAids"><option value="0">請選擇</option><option value="1">雙側助聽器</option><option value="2">單側助聽器</option><option value="3">雙側人工電子耳</option>
			            <option value="4">單側人工電子耳</option><option value="5">人工電子耳加助聽器</option><option value="6">尚未配輔具</option><option value="7">無輔具</option></select></td>
			        </tr>
			        <tr>
			            <th>輔具型式與序號</th>
			            <td>右耳：<label><input type="radio" name="assistmanageR" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageR" value="2" /> 電子耳</label>　
			                廠牌 <select id="brandR" ><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelR" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeR" value="" /> 選配/植入地點 <input type="text"  id="buyingPlaceR" value="" /><br />　　　
			                植入醫院醫生 <input type="text"  id="insertHospitalR" size="15" value="" /> 開頻日 <input class="date" type="text" id="openHzDateR" value="" size="10" /><br />
			                左耳：<label><input type="radio" name="assistmanageL" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageL" value="2" /> 電子耳</label>　
			                廠牌<select id="brandL"><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelL" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeL" value="" /> 選配/植入地點 <input type="text" id="buyingPlaceL" value="" /><br />　　　
			                植入醫院醫生 <input type="text" id="insertHospitalL" value="" size="15" /> 開頻日 <input class="date" type="text" id="openHzDateL" value="" size="10" /><br />
			            </td>
			        </tr>
			        <tr>
			            <th>檢測目的</th>
			            <td>
			                <select id="detectionPurposes"><option value="0">請選擇</option><option value="1">例行追蹤</option>
			                <option value="2">入學評估</option><option value="3">聽力評估</option><option value="4">更換助聽器</option>
			                <option value="5">助聽器成效評量</option><option value="6">聽力疑似下降</option><option value="7">人工電子耳術前評估與諮商</option>
			                <option value="8">助聽器調整</option><option value="9">助聽器功能疑似異常</option><option value="10">聽知覺評量</option>
			                <option value="11">其他，說明：</option></select> <input id="detectionPurposesText" style="display:none;" type="text" value="" size="50" />
			            </td>
			        </tr>
			        <tr>
			            <th>背景</th>
			            <td><textarea id="explain" cols="80" rows="2"></textarea></td>
			        </tr>
			        <tr>
			            <th>檢測時聽覺行為觀察</th>
			            <td>
			                <select id="observation"><option value="0">請選擇</option><option value="1">可辦別聲源方向</option>
			                <option value="2">可主動對聲音表現出一致性的反應</option><option value="3">尚無法對聲音表現出一致性的反應，常有假反應</option>
			                <option value="4">無法配合接受檢查，因為</option></select> 
			                <input id="observationExplain" style="display:none;" type="text" value="" size="35" />
			            </td>
			        </tr>
			        <tr>
			            <th>聽力檢查結果</th>
			            <td>
			                中耳功能：右耳 <select id="checkR"><option value="0">請選擇</option><option value="1">在正常範圍內</option><option value="2">耳膜順應力低</option><option value="3">負壓</option>
			                <option value="4">耳膜破洞</option><option value="5">未測</option></select>，左耳 <select id="checkL"><option value="0">請選擇</option><option value="1">在正常範圍內</option><option value="2">耳膜順應力低</option><option value="3">負壓</option>
			                <option value="4">耳膜破洞</option><option value="5">未測</option></select><br />
                            右耳：平均聽損為 <input id="checkRecibelR" type="text" value="" /> 分貝，屬 <select id="checkLossR"><option value="0">請選擇</option><option value="1">輕度</option><option value="2">中度</option><option value="3">重度</option><option value="4">極重度</option></select> 
                            <select id="checkCategoryR"><option value="0">請選擇</option><option value="1">聽力損失</option><option value="2">感覺神經性聽力損失</option><option value="3">傳導性聽力損失</option>
                            <option value="5">混合型聽力損失</option><option value="6">聽覺處理異常</option><option value="7">聽神經病變</option></select><br />
                            左耳：平均聽損為 <input id="checkRecibelL" type="text" value="" /> 分貝，屬 <select id="checkLossL"><option value="0">請選擇</option><option value="1">輕度</option><option value="2">中度</option><option value="3">重度</option><option value="4">極重度</option></select> 
                            <select id="checkCategoryL"><option value="0">請選擇</option><option value="1">聽力損失</option><option value="2">感覺神經性聽力損失</option><option value="3">傳導性聽力損失</option>
                            <option value="4">混合型聽力損失</option><option value="5">聽覺處理異常</option><option value="6">聽神經病變</option></select>               
                        </td>
			        </tr>
			        <tr>
			            <th>聽知覺/語詞測驗結果</th>
			            <td>
			                <input id="checkResult" type="text" value="" size="50" /> 
			            </td>
			        </tr>
			        <tr>
			            <th>輔具評估結果/<br />配戴輔具後成效</th>
			            <td>
			                <ul>
			                    <li>一、輔具功能評估<br />
			                        右側 <select id="checkAidsResultR"><option value="0">助聽器</option><option value="1">人工電子耳</option><option value="2">調頻系統</option><option value="3">無助聽輔具</option></select> 
			                        <input id="checkAidsResultRText" type="text" value="" size="50" /><br />           
                                    左側  <select id="checkAidsResultL"><option value="0">助聽器</option><option value="1">人工電子耳</option><option value="2">調頻系統</option><option value="3">無助聽輔具</option></select> 
			                        <input id="checkAidsResultLText" type="text" value="" size="50" />               
			                    </li>
			                    <li>二、輔具增益量驗証<br />
                                    右側 <select id="effectR"><option value="0">助聽器</option><option value="1">人工電子耳</option><option value="2">調頻系統</option><option value="3">無助聽輔具</option></select> 
			                        <input id="effectRText" type="text" value="" size="50" /><br />           
                                    左側  <select id="effectL"><option value="0">助聽器</option><option value="1">人工電子耳</option><option value="2">調頻系統</option><option value="3">無助聽輔具</option></select> 
			                        <input id="effectLText" type="text" value="" size="50" />
			                    </li>
			                    <li>三、其他輔具評估 <select id="effectOther"><option value="0">請選擇</option><option value="1">調頻系統</option><option value="2">無</option></select> 
			                    <input id="effectOtherText" type="text" value="" size="50" /></li>
			                </ul>
			            </td>
			        </tr>
			        <tr>
			            <th>聽力學管理建議事項</th>
			            <td>
			               <select id="suggestion"><option value="0">其他</option><option value="1">每六個月定期追蹤聽力</option>
			               <option value="2">每三個月定期追蹤聽力</option><option value="3">延長使用輔具之時間，目前每日只使用</option>
			               <option value="4">每一個月定期回廠保養</option><option value="5">學童應學習檢測輔具</option>
			               <option value="6">學童應學習自行配戴輔具</option><option value="7">每日確實簡易檢測輔具</option>
			               <option value="8">學童應學習主動反應輔具異常</option></select>
			               <span id="suggestionHourspan" style="display:none;"><input id="suggestionHour" type="text" value="" size="5" />小時</span>
			            </td>
			        </tr>
			        <tr>
			            <th>聽覺技巧經營建議事項</th>
			            <td>
                            <input id="suggestion2" type="text" value="" size="50"/>
			            </td>
			        </tr>
			        <tr>
			            <th>其他建議</th>
			            <td>
			               <select id="suggestion3"><option value="0">請選擇</option><option value="1">安排適當的座位，說明：</option><option value="2">提供豐富的視覺線索，說明：</option><option value="3">確實使用調頻系統，說明：</option><option value="4">其他，說明：</option></select>
			                <input id="suggestion3Text" type="text" value="" size="50" />
			            </td>
			        </tr>
			        <tr>
			            <th>下次評估時間</th>
			            <td>
			               <input id="checkNextDate" class="date" type="text" value="" size="10" />
			            </td>
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

