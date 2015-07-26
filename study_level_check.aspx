<%@ Page Language="C#" AutoEventWireup="true" CodeFile="study_level_check.aspx.cs" Inherits="study_level_check" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教學管理 - 個案學習需求等級檢核 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/study_level_check.css" />
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
                <asp:ScriptReference Path="~/js/study_level_check.js" />
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
			<div id="mainClass">教學管理&gt; 個案學習需求等級檢核</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./study_level_check.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
	                    <tr>
			                <td width="260">學生姓名 <input type="text" id="gosrhstudentName" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>年　　度 <input  id="gosrhAcademicYearstart"   type="text" value="" size="5" />～ <input id="gosrhAcademicYearend" type="text" value="" size="5" /></td>
			               <%-- <td>教師姓名 <input id="gosrhteacherName" type="text" value="" /></td>--%>
			                <td>&nbsp;</td>
			                 <td>&nbsp;</td>
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
			                    <th width="160">服務使用者編號</th>
			                    <th width="160">學生姓名</th>
			                    <th width="160">填表日期</th>
			                    <th width="160">出生日期</th>
			                    <th width="120">年齡</th>
			                    <th width="100">性別</th>
			                    <th width="80">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>10123</td>
			                    <td>王小明</td>
			                    <td>98.1.1</td>
			                    <td>4歲9個月</td>
			                    <td>男</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>20123</td>
			                    <td>陳曉雯</td>
			                    <td>97.6.1</td>
			                    <td>5歲3個月</td>
			                    <td>女</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainMenuList2">
			    <div id="item1" class="menuTabs">檢核項目及內容（教師、家長）</div>
			    <div id="item2" class="menuTabs">檢核結果</div>
			    <div id="item3" class="menuTabs">學習需求等級檢核輔導會議</div>
			</div>
			  <div id="tableContact1">
			<div id="mainContent">
			
			    <div id="item1Content">
			  
			        <p align="right" id="sUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
                    <p>
                        學生姓名
                        <input id="studentName" type="text" value="" size="15" readonly="readonly" /><span
                            id="StudentID" class="hideClassSpan"></span><span class="startMark">*</span>
                        出生日期
                        <span id="studentbirthday">
                        </span>
                    </p>
                    <p>
                        填寫人
                        <input id="WriteNameName" type="text" value="" size="15" /><span id="WriteName" class="hideClassSpan"></span>
                        填表日期
                        <input id="WriteDate" class="date" type="text" value="" size="10" /></p>
			        <table class="tableContact" width="780" border="0">
			            <tr>
			                <th width="200">項目</th>
			                <th width="75">教師得分</th>
			                <th width="75">家長得分</th>
			                <th width="430">說明</th>
			            </tr>
                        <tr>
                            <td>
                                個別化教育服務計畫進展狀況
                            </td>
                            <td>
                                <select id="TeacherScore1" >
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select  id="ParentScore1" >
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：教育計畫大多能在目標設定日期內全數達成，甚至超前<br />
                                    3：教育計畫能在目標設定日期內至少達到7成，且其他未達到7成的目標，也至少能達到5成以上<br />
                                    1：教育計畫只能達到5成或5成以下，許多目標皆需修訂或延續執行
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                評估結果或進展（以本會評估工具為主）
                            </td>
                            <td>
                                <select id="TeacherScore2">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore2">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：整體評估接近或超過同年齡常模，整體學習進展相當明顯<br />
                                    3：整體評估與同年齡常模有落差（雖仍有常模可對照）或有進展但整體學習進展速度普通<br />
                                    1：整體評估無法與同齡常模對照或進展速度相當緩慢
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                主要教學者教學執行狀況與成效
                            </td>
                            <td>
                                <select id="TeacherScore3">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore3">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：投入時間極多且教學效能佳，對教師交代之作業能有效進行<br />
                                    3：對教師交代之作業尚能執行，唯能投入的時間仍相當有限或是執行效能有限<br />
                                    1：對教師交代之作業多半沒有時間或無法進行，致使教學執行效能無法彰顯
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                主要教學者的教學技巧
                            </td>
                            <td>
                                <select id="TeacherScore4">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore4">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：會利用情境、創造情境，運用輸入技巧將教學目標融合於活動中，且能和孩子有良好的互動<br />
                                    3：會把握時間隨機教學，不論孩子聽懂與否，努力輸入<br />
                                    1：很難控制孩子，不知怎麼教，也不知在情境下可以說什麼話
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                學習潛能
                            </td>
                            <td>
                                <select id="TeacherScore5">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore5">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：具良好的學習態度且有很高的溝通意願（智能表現在中上）<br />
                                    3：具學習及溝通的意願，無智能或其它學習的障礙及困難<br />
                                    1：明顯的學習困難（具其它疑似或明確的障礙）
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                教學資源（聽語教學單位，例如：語言治療等）
                            </td>
                            <td>
                                <select  id="TeacherScore6">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore6">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：除本會課程外尚有適切的療育單位或目前不需要其它的療育資源<br />
                                    3：除本會課程外尚有其它療育資源，但在功能及協助上有限<br />
                                    1：需要但未能安排到適切的療育資源
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                相關療育資源（非聽語教學單位，例如：物理、職能治療、發展中心等）
                            </td>
                            <td>
                                <select id="TeacherScore7">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore7">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：沒有需求或已在適切的療育單位接受適當的相關療育服務<br />
                                    3：已進行各項療育評估，並於相關療育單位接受療育服務，但尚未有明顯成效<br />
                                    1：疑因注意力、理解力等不足影響學習，但尚未進行醫療評估
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                情緒行為
                            </td>
                            <td>
                                <select id="TeacherScore8">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore8">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：情緒行為穩定、良好或情緒表現符合該年齡之一般行為<br />
                                    3：在特殊情境或事件發生時，情緒處理或行為控制等表現有時不易安撫或轉移，且強度高（發生頻率為偶發）<br />
                                    1：在熟悉的環境下，經常性的出現內向性行為（退怯、恐懼、焦慮、哀傷等）、外向性行為（反抗、攻擊、暴躁、哭鬧等）或偏差行為（自我傷害、說謊、咬指甲等）<br />
                                    備註：該項得分為3（不含）以下，需填寫情緒檢核表
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                輔具選擇
                            </td>
                            <td>
                                <select id="TeacherScore9">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore9">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：輔具選擇及調整符合需求，且效益顯著<br />
                                    3：輔具選擇及調整尚符合需求，效益普通<br />
                                    1：輔具選擇及調整不理想，效益不佳
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                輔具管理
                            </td>
                            <td>
                                <select id="TeacherScore10">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore10">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：能例行使用輔具檢核工具及進行簡易保養<br />
                                    3：偶爾使用輔具檢核工具及進行簡易保養<br />
                                    1：從未或不會使用輔具檢核工具及進行簡易保養
                                </p>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                聽力管理
                            </td>
                            <td>
                                <select  id="TeacherScore11">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <select id="ParentScore11">
                                    <option value="0">請選擇</option>
                                    <option value="1">1</option>
                                    <option value="2">2</option>
                                    <option value="3">3</option>
                                    <option value="4">4</option>
                                    <option value="5">5</option>
                                </select>
                            </td>
                            <td>
                                <p>
                                    5：聽力穩定，且定期接受聽力服務/諮詢<br />
                                    3：聽力偶有變化，但多能即時察覺並接受聽力及相關醫療服務<br />
                                    1：未接受定期聽力服務/諮詢
                                </p>
                            </td>
                        </tr>
			            <tr>
			                <td colspan="4"  align="center">教師－備註/其它狀況說明<br /><textarea id="TeacheRemark"  cols="80" rows="2"></textarea></td>
			            </tr>
			            <tr>
			                <td colspan="4"  align="center">家長－備註/其它狀況說明<br /><textarea id="ParentRemark" cols="80" rows="2"></textarea></td>
			            </tr>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(1)">下一頁</button>
			            <button class="btnUpdate"  type="button">更 新</button>
			            <button class="btnSaveUdapteData" onclick="Update()" type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item2Content">
			        <p align="right" style="background-color:#FFDF71;padding:0 10px;">台北至德</p>
			        <p align="right">填寫人  <input id="WriteName1Name" type="text" value="" size="15" /><span id="WriteName1" class="hideClassSpan"></span></p>
			      
			        <table  id="tableContact2" class="tableContact" width="780" border="0">
			            <tr>
			                <th width="130">評估項目</th>
			                <th width="50">總分</th>
			                <th>案家得分</th>
			                <th>教師得分</th>
			                <th>平均得分</th>
			                <th>服務需求</th>
			            </tr>
			            <tr>
			                <td>教學服務需求評估</td>
			                <td>30</td>
			                <td align="center"><input id="ParentSum1" type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="TeacherSum1" type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="Average1" type="text" value="" size="5" readonly="readonly" /></td>
			                <td><label>高度（01-12分）</label>　
			                <label>中度（13-22分）</label>　
			                <label> 低度（23-30分）</label></td>
			            </tr>
			            <tr>
			                <td>相關療育需求評估</td>
			                <td>05</td>
			                <td align="center"><input id="ParentSum2"  type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="TeacherSum2"  type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="Average2"  type="text" value="" size="5 readonly="readonly"" /></td>
			                <td><label>高度（01-02分）</label>　
			                <label> 低度（03-05分）</label></td>
			            </tr>
			            <tr>
			                <td>情緒輔導需求評估</td>
			                <td>05</td>
			                <td align="center"><input id="ParentSum3" type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="TeacherSum3" type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="Average3" type="text" value="" size="5" readonly="readonly" /></td>
			                <td><label> 高度（01-02分）</label>　
			                <label> 低度（03-05分）</label></td>
			            </tr>
			            <tr>
			                <td>聽力服務需求評估</td>
			                <td>15</td>
		                    <td align="center"><input id="ParentSum4" type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="TeacherSum4" type="text" value="" size="5" readonly="readonly" /></td>
			                <td align="center"><input id="Average4" type="text" value="" size="5" readonly="readonly" /></td>
			                <td><label>高度（01-12分）</label>　
			                <label> 中度（13-22分）</label>　
			                <label> 低度（23-30分）</label></td>
			            </tr>
			            <tr>
			                <td colspan="6"  align="center" >其他（簡述說明特殊狀況或是其它說明及建議）：<br /><textarea id="OtherRemark" cols="80" rows="2"></textarea><br /><br /></td>
			            </tr>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(2)">下一頁</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" onclick="Update()"  type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item3Content">
			        <p align="right" style="background-color:#FFDF71;padding:0 10px;">台北至德</p>
			        <div id="tableContact3" >
			        <p align="right">記錄者 <input id="RecordedByName"  type="text" value="" size="15" /><span id="RecordedBy" class="hideClassSpan"></span>　　記錄日期 <input id="RecordedDateTime" class="date" type="text" value="" size="15" /></p>
			        <table   class="tableContact2" width="780" border="0">
			            <tr>
			                <td colspan="6">（跨專業服務人員討論決議記錄）<br /><textarea id="Recorded" cols="100" rows="5"></textarea><br /><br /></td>
			            </tr>
			            <tr>
			                <th>職稱</th>
			                <th>姓名</th>
			                <th>職稱</th>
			                <th>姓名</th>
			                <th>職稱</th>
			                <th>姓名</th>
			            </tr>
			            <tr>
			                <td><input type="text" id="Title1" value="" size="15" /></td>
			                <td><input type="text" id="Participants1" value="" size="15" /></td>
			                <td><input type="text" id="Title2" value="" size="15" /></td>
			                <td><input type="text" id="Participants2" value="" size="15" /></td>
			                <td><input type="text" id="Title3" value="" size="15" /></td>
			                <td><input type="text" id="Participants3" value="" size="15" /></td>
			            </tr>
			            <tr>
			                <td><input type="text" id="Title4" value="" size="15" /></td>
			                <td><input type="text" id="Participants4" value="" size="15" /></td>
			                <td><input type="text" id="Title5" value="" size="15" /></td>
			                <td><input type="text" id="Participants5" value="" size="15" /></td>
			                <td><input type="text" id="Title6" value="" size="15" /></td>
			                <td><input type="text" id="Participants6" value="" size="15" /></td>
			            </tr>
			        </table>
			        </div>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="Save()" >儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" onclick="Update()"  type="button">存 檔</button>
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