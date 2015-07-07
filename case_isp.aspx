<%@ Page Language="C#" AutoEventWireup="true" CodeFile="case_isp.aspx.cs" Inherits="case_isp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 個案家庭服務計畫(ISP) | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/case_isp.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/case_isp.js"></script>
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
			<div id="logo"><a href="./default.aspx"><img src="./images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">個案管理&gt; 個案家庭服務計畫(ISP)</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./case_isp.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input type="text" name="name" /></td>
			                <td width="260">性　　別 <select name="name"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" name="name" size="10" />～<input class="date" type="text" name="name" size="10" /></td>
			            </tr>
			            <tr>
			                <td>服務使用者編號 <input type="text" name="name" /></td>
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
			                    <th width="144">服務使用者編號</th>
			                    <th width="144">學生姓名</th>
			                    <th width="144">出生日期</th>
			                    <th width="144">擬定日期</th>
			                    <th width="144">修訂日期</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>10123</td>
			                    <td>王小明</td>
			                    <td>100.01.01</td>
			                    <td>102.01.01</td>
			                    <td>102.03.11</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>20123</td>
			                    <td>陳曉雯</td>
			                    <td>101.01.01</td>
			                    <td>102.03.01</td>
			                    <td>102.05.21</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div>
			        <p style="background-color:#FFDF71;padding:0 10px;">學生姓名 <input id="studentName" type="text" value="" /><span style="float:right;">台北至德</span></p>
			        <table class="tableContact" width="780" border="0">
			            <tr>
			                <td rowspan="2">計畫撰寫者 <input id="planName" type="text" value="" /></td>
			                <td>擬定日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>交接日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td rowspan="2">計畫修訂者 <input type="text" value="" /></td>
			                <td>修訂日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>交接日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
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
			                    <input type="checkbox" value="0" /> <input type="text" value="協助申請早療補助" /><br />
			                    <input type="checkbox" value="1" /> <input type="text" value="協助申請生活補助" /><br />
			                    <input type="checkbox" value="2" /> <input type="text" value="" /><br />
			                    <input type="checkbox" value="3" /> <input type="text" value="" />
			                </td>
			                <td>
			                    資源<br /><input type="text" value="" size="15" /><br /><br />
			                    <input type="text" value="協助案家申請早療補助" /><br />
			                    <input type="text" value="協助案家申請中低收入生活補助" /><br />
			                    <input type="text" value="" /><br />
			                    <input type="text" value="" />
			                </td>
			                <td align="center">
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea>
			                </td>
			                <td>
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select>
			                </td>
			                <td>
			                    <label><input type="checkbox" value="0" />提供台北市療育申請表單。<br />(因台北市療育費申請不需核章，故個案無須協助核章)</label><br />
                                <label><input type="checkbox" value="1" />已提供案母生活補助申請資格及所需文件。</label><br />
                                <label><input type="checkbox" value="2" />案母提出申請且符合資格。</label><br />
                                <label><input type="checkbox" value="3" />案母提出申請未符合資格。</label>
			                </td>
			            </tr><tr>
			                <td align="center">二、支持性服務</td>
			                <td>
			                    <input type="checkbox" value="0" /> <input type="text" value="提供相關資訊及關懷案家情況" /><br />
			                    <input type="checkbox" value="1" /> <input type="text" value="提供親職教育活動訊息" /><br />
			                    <input type="checkbox" value="2" /> <input type="text" value="" /><br />
			                    <input type="checkbox" value="3" /> <input type="text" value="" />
			                </td>
			                <td>
			                    資源<br /><input type="text" value="" size="15" /><br /><br />
			                    <input type="text" value="主動提供案家適切的社會福利資訊" /><br />
			                    <input type="text" value="關心案母身心狀況" /><br />
			                    <input type="text" value="主動提供案家適切的相關活動訊息，並邀請案家參與本中心活動" /><br />
			                    <input type="text" value="" />
			                </td>
			                <td align="center">
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea>
			                </td>
			                <td>
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select>
			                </td>
			                <td>
			                    <label><input type="checkbox" value="0" />案家能順利使用或取得相關的社福資源。</label><br />
                                <label><input type="checkbox" value="1" />因個案學習成效不彰，以致案母對於個案狀況(可能須植入CI)感到相當不安，故將多介入關懷，降低案母心理壓力。</label><br />
                                <input type="checkbox" value="2" /><input type="text" value="" size="5" />年提供<input type="text" value="" size="5" />場活動訊息予案家，案家的參與狀況：<input type="text" value="" />
			                </td>
			            </tr><tr>
			                <td align="center">三、復健與醫療</td>
			                <td>
			                    <input type="checkbox" value="0" /> <input type="text" value="追蹤個案聽力檢查情況符合至少半年追蹤一次" /><br />
			                    <input type="checkbox" value="1" /> <input type="text" value="追蹤個案聽覺技巧訓練課程之需求性" /><br />
			                    <input type="checkbox" value="2" /> <input type="text" value="" /><br />
			                    <input type="checkbox" value="3" /> <input type="text" value="" />
			                </td>
			                <td>
			                    資源<br /><input type="text" value="" size="15" /><br /><br />
			                    <input type="text" value="於計劃執行完畢時確認聽檢紀錄" /><br />
			                    <input type="text" value="於計劃執行完畢前一個月追蹤個案的學習情況" /><br />
			                    <input type="text" value="" /><br />
			                    <input type="text" value="" />
			                </td>
			                <td align="center">
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea>
			                </td>
			                <td>
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select>
			                </td>
			                <td>
			                    <label><input type="checkbox" value="0" />個案聽力檢查皆有符合至少半年追蹤一次。</label><br />
                                <label><input type="checkbox" value="1" />個案聽力檢查未符合至少半年追蹤一次，原因：</label><input type="text" value="" /><br />
                                <label><input type="checkbox" value="2" />個案的學習狀況穩定，經教學組年度評估仍有訓練需求，需持續學習。</label><br />
                                <label><input type="checkbox" value="2" />個案的學習狀況穩定，經教學組年度評估已符合常童水準，可辦理結案。</label><br />
                                <label><input type="checkbox" value="3" />其他：</label><input type="text" value="" />
			                </td>
			            </tr><tr>
			                <td align="center">四、教育資源連結</td>
			                <td>
			                    <input type="checkbox" value="0" /> <input type="text" value="協助案母暸解幼兒園所相關資訊" /><br />
			                    <input type="checkbox" value="1" /> <input type="text" value="" /><br />
			                    <input type="checkbox" value="2" /> <input type="text" value="" /><br />
			                    <input type="checkbox" value="3" /> <input type="text" value="" />
			                </td>
			                <td>
			                    資源<br /><input type="text" value="" size="15" /><br /><br />
			                    <input type="text" value="協助提供入學資訊並追蹤個案回歸幼兒園所的學習狀況" /><br />
			                    <input type="text" value="" /><br />
			                    <input type="text" value="" /><br />
			                    <input type="text" value="" />
			                </td>
			                <td align="center">
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" /><br /><br />
			                    <input class="date" type="text" value="" size="10" /><br />～<br /><input class="date" type="text" value="" size="10" />
			                </td>
			                <td>
			                    <input type="text" value="" size="10" /><br /><br />
			                    <input type="text" value="" size="10" />
			                </td>
			                <td align="center">
			                    <textarea class="date"></textarea><br /><br />
			                    <textarea class="date"></textarea>
			                </td>
			                <td>
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select><br /><br />
			                    <select><option value="0">請選擇</option><option value="1">完全未執行</option><option value="2">進行中</option><option value="3">已達成</option><select>
			                </td>
			                <td>
			                    <label><input type="checkbox" value="0" />已提供台北啟聰幼兒園入園資訊予案家，並鼓勵案家參與啟聰學校辦理之親師體驗營。</label><br />
                                <label><input type="checkbox" value="1" />個案學習狀況穩定(包含學業、人際互動等)暫無需其他資源介入。</label><br />
                                <label><input type="checkbox" value="3" />個案仍需要其他資源介入：</label><input type="text" value="" />
			                </td>
			            </tr></tbody>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
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
