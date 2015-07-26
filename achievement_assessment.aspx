<%@ Page Language="C#" AutoEventWireup="true" CodeFile="achievement_assessment.aspx.cs" Inherits="achievement_assessment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教學管理 - 成就評估 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/achievement_assessment.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/achievement_assessment.js"></script>
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
			<div id="mainClass">教學管理&gt; 成就評估</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./achievement_assessment.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
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
			                    <td>王小明</td>
			                    <td>98.01.01</td>
			                    <td>4歲9個月</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>2</td>
			                    <td>102</td>
			                    <td>陳曉雯</td>
			                    <td>99.01.01</td>
			                    <td>3歲9個月</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination" class="pagination"></div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p align="right" id="caseUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
<%--			    <p align="right"><input id="AcademicYear" type="text" size="5" /> 學年度</p>
--%>			    <p class="cP">一、基本資料</p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">學生姓名</th>
			            <td><input id="studentName" type="text" value="" readonly="readonly" /><span id="StudentID" class="hideClassSpan"></span><span class="startMark">*</span>&nbsp;&nbsp;&nbsp;<input id="AcademicYear" type="text" size="5" /> 學年度<span class="startMark">*</span></td>
			        </tr>
			      <%--   <tr>
			            <th>性　　別</th>
			            <td id="studentSex">&nbsp;</td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td id="studentbirthday">&nbsp;</td>
		            </tr>
		
		            <tr>
			            <th>障礙類別</th>
			            <td>身心障礙類別 <input type="text" id="manualCategory1" value="" size="15" /> 等級 <input type="text" id="manualGrade1" value="" size="15" /><br />
			                身心障礙類別 <input type="text" id="manualCategory2" value="" size="15" /> 等級 <input type="text" id="manualGrade2" value="" size="15" /><br />
			                身心障礙類別 <input type="text" id="manualCategory3" value="" size="15" /> 等級 <input type="text" id="manualGrade3" value="" size="15" />
			            </td>
			        </tr>
			        <tr>
		                <th>聽覺輔具</th>
		                <td>
			                右耳：<label><input type="radio" name="assistmanageR" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageR" value="2" /> 電子耳</label>　
			                廠牌/型號 <select id="BrandR" ><option value="0">廠牌/型號</option></select><br />
			                左耳：<label><input type="radio" name="assistmanageL" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageL" value="2" /> 電子耳</label>　
			                廠牌/型號 <select id="BrandL"><option value="0">廠牌/型號</option></select><br />
			            </td>
		            </tr>
                    <tr>
			            <th>配戴輔具後之聽力狀況</th>
			            <td>左耳 <input id="assistmanageHL" type="text" value="" size="5" /> dBHL，右耳 <input id="assistmanageHR" type="text" value="" size="5" /> dBHL
                        </td>
			        </tr>--%>
			     </table>
	             <div  id="SaveTable" >
			     <p class="cP">二、測驗結果摘述</p>
			     <p>評估時，個案的生理年齡為 <input id="StudentAge" type="text" value="" size="5"  readonly="readonly" /> 歲  <input id="StudentMonth" type="text" value="" size="5"  readonly="readonly" /> 個月</p>
	             <p>評估原因<label><input type="radio" name="AssessedTheReasons" value="1" /> 年度評估</label>　　
			                <label><input type="radio" name="AssessedTheReasons" value="2" /> 新生</label>　　
			                <label><input type="radio" name="AssessedTheReasons" value="3" /> 其他 <input id="AssessedTheReasonsText" type="text" value=""/></label>
			                </p>
			     <!--主要存檔區開始-->
			     <table class="tableContact" width="780" border="0">
			        <thead><tr>
			            <th width="150">測驗名稱</th>
			            <th width="100">日期／施測者</th>
			            <th>測驗結果</th>
			        </tr></thead>
			        <!--        類別：智力  托尼非語文智力測驗-->
			        <!--        Intelligence        -->
			        
                    <tr>
			            <td>類別：智力<br />托尼非語文智力測驗<br />
                        <label><input type="radio" name="Intelligence_Type" value="0" /> 甲式</label>　　
		                <label><input type="radio" name="Intelligence_Type" value="1" /> 乙式</label></td>
			            <td><input class="date" id="Intelligence_Date" type="text" size="10" />／<input id="Intelligence_Rater" type="text" value="" size="10"/></td>
			            <td>
                             <p><b>1. 測驗得分對照
                                     <select id="Intelligence_StudentAge">
                                         <option value="0">請選擇</option>
                                         <option value="1">3歲-3歲5個月</option>
                                         <option value="2">3歲6個月-3歲11個月</option>
                                         <option value="3">4歲-4歲5個月</option>
                                         <option value="4">4歲6個月-4歲11個月</option>
                                         <option value="5">5歲-5歲5個月</option>
                                         <option value="6">5歲6個月-5歲11個月</option>
                                         <option value="7">6歲-6歲5個月</option>
                                         <option value="8">6歲6個月-6歲11個月</option>
                                     </select>
                                     兒童常模的結果如下：</b><br />
                                 原始分數為
                                 <input id="Intelligence_RawScore" type="text" value="" size="5" />分，百分等級為
                                 <input id="Intelligence_ScorePer" type="text" value="" size="5" />，等級為
                                 <select id="Intelligence_Grade">
                                     <option value="0">請選擇</option>
                                     <option value="1">特殊優異</option>
                                     <option value="2">優秀</option>
                                     <option value="3">正常</option>
                                     <option value="4">中下</option>
                                     <option value="5">發展遲緩</option>
                                 </select>。</p>
                             <p>
                                 <b>2. 總結與建議</b><br />
                                 測驗結果顯示個案與同年齡常童相較下，其非語文智力為
                                 <select id="Intelligence_Result">
                                     <option value="0">請選擇</option>
                                     <option value="1">特殊優異</option>
                                     <option value="2">優秀</option>
                                     <option value="3">正常</option>
                                     <option value="4">中下</option>
                                     <option value="5">發展遲緩</option>
                                 </select>。</p>
                         </td>
                     </tr>
                    <!--        類別：聽覺技巧  聽覺技巧訓練能力測驗（管美玲）-->
                    <!--        AuditorySkills        -->
			        <tr>
			            <td>類別：聽覺技巧<br />聽覺技巧訓練能力測驗（管美玲）</td>
			            <td><input id="AuditorySkills_Date" class="date" type="text" size="10" />／<input id="AuditorySkills_Rater" type="text" value="" size="10" /></td>
			           <td>
                             <p>
                                 <b>1. 測驗結果如下</b><br />
                                 通過
                                 <select id="AuditorySkills_Text1" style="width: 450px;">
                                     <option value="0">請選擇</option>
                                     <option value="1">測驗1&lt;察覺語音></option>
                                     <option value="2">測驗2&lt;察覺五音&gt;</option>
                                     <option value="3">測驗3&lt;辨識有押韻，疊字的句子，童謠或兒歌&gt;</option>
                                     <option value="4">測驗4&lt;辨識熟悉的句子&gt;</option>
                                     <option value="5">測驗5&lt;辨識不同字數的詞彙&gt;</option>
                                     <option value="6">測驗6&lt;回想訊息中的2項關鍵詞></option>
                                     <option value="7">測驗6&lt;回想訊息中的3項關鍵詞&gt;</option>
                                     <option value="8">測驗6&lt;回想訊息中的4項關鍵詞></option>
                                     <option value="9">測驗7&lt;針對句子回答問題&gt;</option>
                                     <option value="10">測驗8-1&lt;告知主題並且在有線索的提示下，以正確的順序說出與主題相關之細節&gt;</option>
                                     <option value="11">測驗8-2&lt;不告知主題並且在沒有線索的提示下，以正確的順序說出與主題相關之細節&gt;</option>
                                 </select>及
                                 <select id="AuditorySkills_Text2">
                                     <option value="0">請選擇</option>
                                     <option value="1">測驗9-1&lt;辨識聲調></option>
                                     <option value="2">測驗9-2&lt;辨識相同聲調，但不同聲母與韻母的詞彙&gt;</option>
                                     <option value="3">測驗9-3&lt;辨識韻母&gt;</option>
                                     <option value="4">測驗9-4&lt;辨識聲母&gt;</option>
                                 </select>。</p>
                             <p>
                                 <b>2. 總結與建議</b><br />
                                 <textarea id="AuditorySkills_Summary" rows="2" cols="60"></textarea></p>
                         </td>
			        </tr>
			        <!--        類別：詞彙  畢保德圖畫詞彙測驗 -->
			        <!--        Vocabulary        -->
			        <tr>
			            <td>類別：詞彙<br />畢保德圖畫詞彙測驗<br />
                        <label><input type="radio" name="Vocabulary_Type" value="0" /> 甲式</label>　　
		                <label><input type="radio" name="Vocabulary_Type" value="1" /> 乙式</label></td>
			            <td><input id="Vocabulary_Date" class="date" type="text" size="10" />／<input id="Vocabulary_Rater" type="text" value="" size="10" /></td>
                        <td>
                            <p>
                                <b>1. 測驗得分對照
                                    <select id="Vocabulary_StudentAge">
                                        <option value="0">請選擇</option>
                                        <option value="1">3歲</option>
                                        <option value="2">3歲半</option>
                                        <option value="3">4歲</option>
                                        <option value="4">4歲半</option>
                                        <option value="5">5歲</option>
                                        <option value="6">5歲半</option>
                                        <option value="7">6歲</option>
                                        <option value="7">6歲半</option>
                                        <option value="8">7歲</option>
                                    </select>
                                    兒童常模的結果如下：</b><br />
                                原始分數為
                                <input id="Vocabulary_RawScore" type="text" value="" size="5" />分，百分等級為
                                <input id="Vocabulary_ScorePer" type="text" value="" size="5" />。
                            </p>
                            <p>
                                <b>2. 總結與建議</b><br />
                                <textarea id="Vocabulary_Result" rows="2" cols="60"></textarea></p>
                        </td>
			        </tr> 
			        <!--        類別：詞彙  3~6歲華語兒童理解與表達測驗（黃瑞珍) -->
			        <!--        Vocabulary1        -->
			        <tr>
			            <td>類別：詞彙<br />3~6歲華語兒童理解與表達測驗（黃瑞珍）</td>
                        <td><input id="Vocabulary1_Date" class="date" type="text" size="10" />／<input id="Vocabulary1_Rater" type="text" value="" size="10"/></td>
                        <td>
                            <p>
                                <b>1. 測驗得分對照
                                    <select id="Vocabulary1_StudentAge">
                                        <option value="0">請選擇</option>
                                        <option value="1">3歲-3歲5個月</option>
                                        <option value="2">3歲6個月-3歲11個月</option>
                                        <option value="3">4歲-4歲5個月</option>
                                        <option value="4">4歲6個月-4歲11個月</option>
                                        <option value="5">5歲-5歲5個月</option>
                                        <option value="6">5歲6個月-5歲11個月</option>
                                        <option value="7">6歲-6歲5個月</option>
                                        <option value="8">6歲6個月-6歲11個月</option>
                                    </select>兒童常模的結果如下：</b><br />
                                ※全測驗的原始分數為
                                <input id="Vocabulary1_RawScore1" type="text" value="" size="5" />分，標準分數為
                                <input id="Vocabulary1_Score1" type="text" value="" size="5" />分，百分等級為
                                <input id="Vocabulary1_ScorePer1" type="text" value="" size="5" />，<br />
                                屬於
                                <select id="Vocabulary1_Grade">
                                    <option value="0">請選擇</option>
                                    <option value="1">非常優秀</option>
                                    <option value="2">優秀</option>
                                    <option value="3">中上</option>
                                    <option value="4">中等普通</option>
                                    <option value="5">中下</option>
                                    <option value="6">低下</option>
                                    <option value="7">能力不足</option>
                                </select>
                                的程度。<br />
                                ※理解量表原始分數為
                                <input id="Vocabulary1_RawScore2" type="text" value="" size="5" />分，標準分數為
                                <input id="Vocabulary1_Score2" type="text" value="" size="5" />分，百分等級為
                                <input id="Vocabulary1_ScorePer2" type="text" value="" size="5" />，<br />
                                屬於
                                <select id="Vocabulary1_Grade2">
                                    <option value="0">請選擇</option>
                                    <option value="1">非常優秀</option>
                                    <option value="2">優秀</option>
                                    <option value="3">中上</option>
                                    <option value="4">中等普通</option>
                                    <option value="5">中下</option>
                                    <option value="6">低下</option>
                                    <option value="7">能力不足</option>
                                </select>
                                的程度。<br />
                                ※表達量表原始分數為
                                <input id="Vocabulary1_RawScore3" type="text" value="" size="5" />分，標準分數為
                                <input id="Vocabulary1_Score3" type="text" value="" size="5" />分，百分等級為
                                <input id="Vocabulary1_ScorePer3" type="text" value="" size="5" />，<br />
                                屬於
                                <select id="Vocabulary1_Grade3">
                                    <option value="0">請選擇</option>
                                    <option value="1">非常優秀</option>
                                    <option value="2">優秀</option>
                                    <option value="3">中上</option>
                                    <option value="4">中等普通</option>
                                    <option value="5">中下</option>
                                    <option value="6">低下</option>
                                    <option value="7">能力不足</option>
                                </select>
                                的程度。<br />
                                ※四個分測驗的表現：<br />
                                ●命名之原始分數為<input id="Vocabulary1_RawScore4" type="text" value="" size="5" />分，標準分數為<input
                                    id="Vocabulary1_Score4" type="text" value="" size="5" />分，百分等級為<input id="Vocabulary1_ScorePer4"
                                        type="text" value="" size="5" />；<br />
                                ●歸類之原始分數為<input id="Vocabulary1_RawScore5" type="text" value="" size="5" />分，標準分數為<input
                                    id="Vocabulary1_Score5" type="text" value="" size="5" />分，百分等級為<input id="Vocabulary1_ScorePer5"
                                        type="text" value="" size="5" />；<br />
                                ●定義之原始分數為<input id="Vocabulary1_RawScore6" type="text" value="" size="5" />分，標準分數為<input
                                    id="Vocabulary1_Score6" type="text" value="" size="5" />分，百分等級為<input id="Vocabulary1_ScorePer6"
                                        type="text" value="" size="5" />；<br />
                                ●推理之原始分數為<input id="Vocabulary1_RawScore7" type="text" value="" size="5" />分，標準分數為<input
                                    id="Vocabulary1_Score7" type="text" value="" size="5" />分，百分等級為<input id="Vocabulary1_ScorePer7"
                                        type="text" value="" size="5" />；<br />
                                四個分測驗中，<input id="Vocabulary1_Text" type="text" value="" />。
                            </p>
                            <p>
                                <b>2. 總結與建議</b><br />
                                <textarea id="Vocabulary1_Summary" rows="2" cols="60"></textarea></p>
                        </td>
			        </tr>
			        <!--        類別：語言 零歲至三歲華語嬰幼兒溝通及語言篩檢測驗（黃瑞珍） -->
			        <!--        Language1        -->
			        <tr>
			            <td>類別：語言<br />零歲至三歲華語嬰幼兒溝通及語言篩檢測驗（黃瑞珍）</td>
			            <td><input id="Language1_Date" class="date" type="text" size="10" />／<input id="Language1_Rater" type="text" value="" size="10"/></td>
                       <td>
                            <p>
                                <b>1. 測驗得分對照
                                    <select id="Language1_StudentMonth">
                                        <option value="0">請選擇</option>
                                        <option value="1">0-4個月</option>
                                        <option value="2">5-8個月</option>
                                        <option value="3">9-12個月</option>
                                        <option value="4">13-16個月</option>
                                        <option value="5">17-20個月</option>
                                        <option value="6">21-24個月</option>
                                        <option value="7">25-28個月</option>
                                        <option value="8">29-32個月</option>
                                        <option value="9">33-36個月</option>
                                    </select>
                                    個月嬰幼兒常模的結果如下：</b><br />
                                總得分為為
                                <input id="Language1_RawScore" type="text" value="" size="5" />分，百分位數為
                                <select id="Language1_ScorePer" >
                                    <option value="0">請選擇</option>
                                    <option value="1">5%以下</option>
                                    <option value="2">6%-25%</option>
                                    <option value="3">26%-74%</option>
                                    <option value="4">75%-89%</option>
                                    <option value="5">90%以上</option>
                                </select>
                                ，篩檢結果為
                                <select id="Language1_Result" >
                                    <option value="0">請選擇</option>
                                    <option value="1">疑似遲緩</option>
                                    <option value="2">可能落後</option>
                                    <option value="3">平均水準</option>
                                    <option value="4">稍微超前</option>
                                    <option value="5">超前</option>
                                </select>。
                            </p>
                            <p>
                                <b>2. 總結與建議</b><br />
                                測驗結果顯示個案
                                <select id="Language1_Summary" >
                                    <option value="0">請選擇</option>
                                    <option value="1">為發展遲緩之高危險群，需做進一步鑑定</option>
                                    <option value="2">的語言發展落後一般兒童，宜多注意</option>
                                    <option value="3">的發展與一般兒童一樣</option>
                                    <option value="4">的發展較一般兒童稍微超前</option>
                                    <option value="5">的發展較一般兒童超前</option>
                                </select>。</p>
                        </td>
			        </tr>
			        <!--        類別：語言 修定學前兒童語言障礙評量（3至6歲語言評量/林寶貴） -->
			        <!--        Language2        -->
			        <tr>
			            <td>類別：語言<br />修定學前兒童語言障礙評量（3至6歲語言評量/林寶貴）</td>
			            <td><input id="Language2_Date" class="date" type="text" size="10" />／<input id="Language2_Rater" type="text" value="" size="10"/></td>
                        <td>
                            <p>
                                <b>1. 測驗得分對照
                                    <select id="Language2_StudentAge" >
                                        <option value="0">請選擇</option>
                                        <option value="1">3歲-3歲11個月</option>
                                        <option value="2">4歲-4歲11個月</option>
                                        <option value="3">5歲-5歲11個月</option>
                                    </select>
                                    兒童常模的結果如下：</b><br />
                                ※全測驗的原始分數為
                                <input id="Language2_RawScore1"  type="text" value="" size="5" />分，百分等級為
                                <input id="Language2_ScorePer1"  type="text" value="" size="5" />，轉換T分數後，<br />
                                等級為
                                <select id="Language2_Grade1" >
                                    <option value="0">請選擇</option>
                                    <option value="1">極佳</option>
                                    <option value="2">佳</option>
                                    <option value="3">普通</option>
                                    <option value="4">差</option>
                                    <option value="5">極差</option>
                                </select>。<br />
                                ※語言理解量表原始分數為
                                <input id="Language2_RawScore2"  type="text" value="" size="5" />分，百分等級為
                                <input id="Language2_ScorePer2"  type="text" value="" size="5" />，轉換T分數後，<br />
                                等級為
                                <select id="Language2_Grade2" >
                                    <option value="0">請選擇</option>
                                    <option value="1">極佳</option>
                                    <option value="2">佳</option>
                                    <option value="3">普通</option>
                                    <option value="4">差</option>
                                    <option value="5">極差</option>
                                </select>。<br />
                                ※口語表達量表原始分數為
                                <input id="Language2_RawScore3"  type="text" value="" size="5" />分，百分等級為
                                <input id="Language2_ScorePer3"  type="text" value="" size="5" />，轉換T分數後，<br />
                                等級為
                                <select id="Language2_Grade3" >
                                    <option value="0">請選擇</option>
                                    <option value="1">極佳</option>
                                    <option value="2">佳</option>
                                    <option value="3">普通</option>
                                    <option value="4">差</option>
                                    <option value="5">極差</option>
                                </select>。
                            </p>
                            <p>
                                <b>2. 總結與建議</b><br />
                                <textarea  id="Language2_Summary" rows="2" cols="60"></textarea></p>
                        </td>
			        </tr>

			        <!--        類別：語言 學前幼兒與國小低年級兒童口語語法診斷測驗（楊坤堂） -->
			        <!--        Language3        -->
			        <tr>
			            <td>類別：語言<br />學前幼兒與國小低年級兒童口語語法診斷測驗（楊坤堂）</td>
			            <td><input id="Language3_Date" class="date" type="text" size="10" />／<input id="Language3_Rater" type="text" value="" size="10"/></td>
                        <td>
                            <p>
                                <b>1. 測驗得分對照
                                    <select  id="Language3_StudentAge1"  >
                                        <option value="0">請選擇</option>
                                        <option value="1">學前中班</option>
                                        <option value="2">學前大班</option>
                                        <option value="3">國小一年級</option>
                                        <option value="4">國小二年級</option>
                                    </select>
                                    兒童常模的結果如下：</b><br />
                                ※全測驗的原始分數為
                                <input id="Language3_RawScore1"  type="text" value="" size="5" />分，百分等級為
                                <input id="Language3_ScorePer1"  type="text" value="" size="5" />，轉換為口語語法商數後，<br />
                                其口語語法能力的程度為
                                <input id="Language3_Degree1"  type="text" value="" size="5" />
                                <select id="Language3_Grade1" >
                                    <option value="0">請選擇</option>
                                    <option value="1">極優</option>
                                    <option value="2">優良</option>
                                    <option value="3">中上</option>
                                    <option value="4">普通或正常</option>
                                    <option value="5">中下</option>
                                    <option value="6">口語語法遲緩</option>
                                    <option value="7">口語語法障礙</option>
                                </select>。<br />
                                ※接受性分測驗的原始分數為
                                <input id="Language3_RawScore2"  type="text" value="" size="5" />分，百分等級為
                                <input id="Language3_ScorePer2"  type="text" value="" size="5" />，轉換為口語語法<br />
                                商數後，其口語語法能力的程度為
                                <input id="Language3_Degree2"  type="text" value="" size="5" />
                                <select id="Language3_Grade2" >
                                    <option value="0">請選擇</option>
                                    <option value="1">極優</option>
                                    <option value="2">優良</option>
                                    <option value="3">中上</option>
                                    <option value="4">普通或正常</option>
                                    <option value="5">中下</option>
                                    <option value="6">口語語法遲緩</option>
                                    <option value="7">口語語法障礙</option>
                                </select>。<br />
                                ※表達性分測驗的原始分數為
                                <input id="Language3_RawScore3"  type="text" value="" size="5" />分，百分等級為
                                <input id="Language3_ScorePer3"  type="text" value="" size="5" />，轉換為口語語法<br />
                                商數後，其口語語法能力的程度為
                                <input id="Language3_Degree3"  type="text" value="" size="5" />
                                <select id="Language3_Grade3" >
                                    <option value="0">請選擇</option>
                                    <option value="1">極優</option>
                                    <option value="2">優良</option>
                                    <option value="3">中上</option>
                                    <option value="4">普通或正常</option>
                                    <option value="5">中下</option>
                                    <option value="6">口語語法遲緩</option>
                                    <option value="7">口語語法障礙</option>
                                </select>。
                            </p>
                            <p>
                                <b>2. 總結與建議</b><br />
                                <textarea id="Language3_Summary1"  rows="2" cols="60"></textarea><br />
                                依本測驗進一歩分析後，<input id="Language3_Text"  type="text" />。
                            </p>
                        </td>
			        </tr>
			        <!--        評估結果綜合建議-->
			        <tr>
			            <td>評估結果綜合建議</td>
			            <td colspan="2">綜合建議如下：<br />　<textarea id="Summary" rows="3" cols="80"></textarea></td>
			        </tr>
			    </table>
			    <!--主要存檔區結束-->
			         </div>
			    <p class="btnP">
		            <button class="btnSave" onclick="Save()" type="button">儲 存</button>
		            <button class="btnUpdate"  type="button">更 新</button>
		            <button class="btnSaveUdapteData" onclick="Update()" type="button">存 檔</button>
		            <button class="btnCancel" type="button">取 消</button>
		        </p>
			</div>
			</div>
			
			
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
