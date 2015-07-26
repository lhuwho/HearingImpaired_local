<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teach_service_inspect.aspx.cs" Inherits="teach_service_inspect" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教學管理 - 教學服務檢核 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/teach_service_inspect.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/teach_service_inspect.js"></script>
	
		
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
			<div id="mainClass">教學管理&gt; 教學服務檢核</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./teach_service_inspect.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
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
<%--			                <td>教師姓名 <input id="gosrhTeacherName"  type="text" value="" /></td>
--%>			                <td>檢核日期 <input id="gosrhendReasonDatestart" class="date" type="text" name="name" size="10" />～<input id="gosrhendReasonDateend" class="date" type="text" name="name" size="10" /></td>
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
			                    <th width="100">排序</th>
			                    <th width="120">年度</th>
			                    <th width="120">學生姓名/班級名稱</th>
			                    <th width="120">課別</th>
			                    <th width="120">教師姓名</th>
			                    <th width="120">檢核日期</th>
			                    <th width="80">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>1</td>
			                    <td>102</td>
			                    <td>西瓜班</td>
			                    <td>團體課</td>
			                    <td>王貞貞</td>
			                    <td>100.01.01</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>2</td>
			                    <td>102</td>
			                    <td>陳曉雯</td>
			                    <td>個別課</td>
			                    <td>陳品言</td>
			                    <td>101.01.01</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p style="background-color:#FFDF71;padding:0 10px;">班類型 <select id="ClassType" name="class"><option value="0">個別班</option><option value="1">團體班</option></select>
			    <span id="caseUnit" style="float:right;">台北至德</span></p>
			    <p align="right"><input id="AcademicYear" type="text" value="" size="5" /> 學年度</p>
			    <div id="page0">
			        <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="150">學生姓名</th>
			                <td><input id="studentName" type="text" value="" size="15" readonly="readonly" /><span id="PageList1_StudentID" class="hideClassSpan"></span></td>
			                <th width="150">教師姓名</th>
			                <td><input id="PageList1_teacherName" type="text" value="" readonly="readonly" /><span id="PageList1_TeacherID" class="hideClassSpan"></span></td>
			            </tr>
			            <tr>
			                <th>檢核日期</th>
			                <td><input id="PageList1_InspectDate" type="text" class="date" value="" /></td>
			                <th>課　　別</th>
			                <td><label><input type="radio" name="PageList1_CourseType" value="1" /> 個別課</label>　　
			                    <label><input type="radio" name="PageList1_CourseType" value="2" /> 聽能課</label>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4">&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="4">
			                    <p>壹、檢核項目<span style="float:right;text-align:right;">評分標準：欠佳＜---------＞優<br />不作答：忽略　　1　　3　　5　</span></p>
			                    <table class="tableContact" width="780" border="0">
			                        <thead>
			                            <tr>
			                                <th>檢核項目</th>
			                                <th width="110">評分概況</th>
			                            </tr>
			                        </thead>
			                        <tbody>
			                            <tr>
			                                <td colspan="2">一、適切的教學目標及活動</td>
			                            </tr>
			                            <tr>
			                                <td>1. ISP之聽覺技巧內容符合孩子學習需求</td>
			                                <td align="center"><select  id="PageList1_Score1" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. ISP之認知語言溝通內容符合孩子學習需求</td>
			                                <td align="center"><select  id="PageList1_Score2" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. ISP之人際互動內容符合孩子學習需求</td>
			                                <td align="center"><select  id="PageList1_Score3" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 當次聽覺技巧課程目標符合所設計的ISP內容</td>
			                                <td align="center"><select  id="PageList1_Score4" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 當次認知語言溝通課程目標符合所設計的ISP內容</td>
			                                <td align="center"><select  id="PageList1_Score5" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>6. 當次人際互動課程目標符合所設計的ISP內容</td>
			                                <td align="center"><select  id="PageList1_Score6" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>7. 活動呈現方式具引導與整合性（由已知的能力引導至新的學習）</td>
			                                <td align="center"><select  id="PageList1_Score7" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>8. 活動呈現方式能考量孩子特質、年齡及能力上的落差等</td>
			                                <td align="center"><select  id="PageList1_Score8" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>9. 活動的設計與呈現方式，能引發孩子學習的動機並適時提升孩子的能力</td>
			                                <td align="center"><select  id="PageList1_Score9" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>10. 活動進行流暢，教具準備充份，選擇適合團體班運用及操作</td>
			                                <td align="center"><select  id="PageList1_Score10" ><option value="0">0</option><option value="1">1</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>11.	能提供每位學生參與活動、操作教材教具或發言的機會</td>
			                                <td align="center"><select  id="PageList1_Score11" ><option value="0">0</option><option value="1">1</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">二、適當的語言及溝通技巧</td>
			                            </tr>
			                            <tr>
			                                <td>1. 能恰當地運用相關語言輸入技巧，至少下列兩項以上<br />
                                                （延伸、擴展、自言自語、平行談話、重述、仿說、示範、提供訊息等技巧）</td>
			                                <td align="center"><select  id="PageList1_Score12" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 語言恰當地運用輸入內容，至少下列兩項以上<br />
                                                （命名、描述、比較、說明、給指令）</td>
			                                <td align="center"><select  id="PageList1_Score13" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. 能運用技巧引發孩子進行表達</td>
			                                <td align="center"><select  id="PageList1_Score14" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 能用完整句輸入且語調富變化、自然、流暢（咬字清晰、斷句適當、說話速度及音量適中）</td>
			                                <td align="center"><select  id="PageList1_Score15" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 能適當的使用情境語言與孩子對話或互動</td>
			                                <td align="center"><select  id="PageList1_Score16" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>6. 能有良好的傾聽環境（避免噪音/玩具聲響及眾人說話等不良的傾聽干擾）</td>
			                                <td align="center"><select  id="PageList1_Score17" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>7. 語言輸入重覆性高且能強調關鍵詞</td>
			                                <td align="center"><select  id="PageList1_Score18" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">三、互動技巧</td>
			                            </tr>
			                            <tr>
			                                <td>1. 能提供機會讓孩子主動操作、探索或練習</td>
			                                <td align="center"><select  id="PageList1_Score19" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 能觀察孩子的反應並適時調整或修改活動方式，以維持孩子的學習動機</td>
			                                <td align="center"><select  id="PageList1_Score20" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. 能依孩子的能力表現，適時調整目標或活動的難易度</td>
			                                <td align="center"><select  id="PageList1_Score21" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 能依孩子的能力表現，對孩子有適當要求（仿說、回答或溝通的動作行為等）</td>
			                                <td align="center"><select  id="PageList1_Score22" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 能恰當地應用對話技巧，至少下列兩項以上<br />
                                                （能運用「注視」的技巧（眼神、表情及情緒互動）、「等待與傾聽」、「輪替」、「轉換話題」、「結束話題」的技巧）</td>
			                                <td align="center"><select  id="PageList1_Score23" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>6. 能運用不同方式提供機會讓家長配合教學活動學習教學技能</td>
			                                <td align="center"><select  id="PageList1_Score24" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>7. 對家長的教學技巧或教學能進行相關的指導或建議</td>
			                                <td align="center"><select  id="PageList1_Score25" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>8. 能以豐富的肢體、聲音等吸引孩子的注意力，營造良好的上課氣氛</td>
			                                <td align="center"><select  id="PageList1_Score26" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>9. 能給適當的時間等待、鼓勵孩子的回應或表現</td>
			                                <td align="center"><select  id="PageList1_Score27" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>10.	能建立良好的上課規範</td>
			                                <td align="center"><select  id="PageList1_Score28" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">四、家長諮詢</td>
			                            </tr>
			                            <tr>
			                                <td>1. 能以淺顯易懂的話語清楚解說教學目標，避免過多的專有名詞</td>
			                                <td align="center"><select  id="PageList1_Score29" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 能適時提供課後教學方法</td>
			                                <td align="center"><select  id="PageList1_Score30" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. 能以正面關懷的語氣和表情進行諮詢</td>
			                                <td align="center"><select  id="PageList1_Score31" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 能適時的鼓勵或激勵家長教學的動機</td>
			                                <td align="center"><select  id="PageList1_Score32" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 能清楚解答家長所提的問題</td>
			                                <td align="center"><select  id="PageList1_Score33" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>6. 能運用資源有效監督家長的參與（輔具檢查、在家教學、相關活動..）</td>
			                                <td align="center"><select  id="PageList1_Score34" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">五、個案資料夾檢核</td>
			                            </tr>
			                            <tr>
			                                <td>1. 個案資料完整且放置位置正確</td>
			                                <td align="center"><select  id="PageList1_Score35" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 個案資料適時更新</td>
			                                <td align="center"><select  id="PageList1_Score36" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                        </tbody>
			                    </table>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4"><br />貳、教學服務檢核表平均分數相關督導建議參考如下：<br />
			                    1. 檢核總得分為教師當次考核分數<br />
                                2. 考核中未評到的項目，直接給分<br />
                                3. 各項項得分比例若未達該項的65%，督導需針對該項目進行檢討與追蹤<br />
                                4. 總分成績<br />
                                75分以上：建議每學期例行教學督導觀摩課程1次、參與相關研討、必要時教師得提出需求<br />
			                    65-74分：督導人員向中心主管報告，增加督導看課次數或安排研討<br />
			                    64分以下：督導人員可協請中心主管、總幹事共同進行課程督導，增加督導看課次數或研討，同時提出具體改進的目標或方法，必要時得安排教學考核
			                    <br /><br />
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4">叁、得分統計<br />
			                    <table class="tableContact" width="780" border="0">
		                            <tr>
		                                <th width="50">項　目</th>
		                                <th width="172">一、適切的教學目標及活動</th>
		                                <th width="172">二、適當的語言及溝通技巧</th>
		                                <th width="120">三、互動技巧</th>
		                                <th width="120">四、家長諮詢</th>
		                                <th width="146">五、個案資料夾檢核</th>
		                            </tr>
		       <%--                     <tr>
		                                <th>得　分</th>
		                                <td><input type="text" value="" size="10" /> ／38</td>
		                                <td><input type="text" value="" size="10" /> ／19</td>
		                                <td><input type="text" value="" size="10" /> ／27</td>
		                                <td><input type="text" value="" size="10" /> ／12</td>
		                                <td><input type="text" value="" size="10" /> ／4</td>
		                            </tr>
		                            <tr>
		                                <th>百分比</th>
		                                <td><input type="text" value="" size="10" /> %</td>
		                                <td><input type="text" value="" size="10" /> %</td>
		                                <td><input type="text" value="" size="10" /> %</td>
		                                <td><input type="text" value="" size="10" /> %</td>
		                                <td><input type="text" value="" size="10" /> %</td>
		                            </tr>--%>
		                            <tr>
		                                <th>得　分</th>
		                                <td><input id="PageList1_SumScore1" type="text" value="" size="5" /> ／<span id="PageList1_Denominator1" >38</span></td>
		                                <td><input id="PageList1_SumScore2" type="text" value="" size="5" /> ／<span id="PageList1_Denominator2" >19</span></td>
		                                <td><input id="PageList1_SumScore3" type="text" value="" size="5" /> ／<span id="PageList1_Denominator3" >27</span></td>
		                                <td><input id="PageList1_SumScore4" type="text" value="" size="5" /> ／<span id="PageList1_Denominator4" >12</span></td>
		                                <td><input id="PageList1_SumScore5" type="text" value="" size="5" /> ／<span id="PageList1_Denominator5" >4</span></td>
		       
		                            </tr>
		                            <tr>
		                                <th>百分比</th>
		                                <td><input id="PageList1_PerScore1" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList1_PerScore2" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList1_PerScore3" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList1_PerScore4" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList1_PerScore5" type="text" value="" size="10" /> %</td>
		                            </tr>
		                            
		                            <tr>
		                                <th>總　分</th>
		                                <td colspan="6"><input id="PageList1_SumScore" type="text" value="" />　<input type="button" value="計算" onclick="calInspect(0)" /></td>
		                            </tr>
			                    </table><br />
			                </td>
			            </tr>
			            <tr>
			                <th>督導人員</th>
			                <td><input id="PageList1_SupervisorName" type="text" value="" /></td>
			                <th>授課教師</th>
			                <td><input  id="PageList1_teacherName1" type="text" value="" readonly="readonly" /></td>
			            </tr>
			            <tr>
			                <th>中心主任</th>
			                <td><input id="PageList1_DirectorName" type="text" value="" /><span id="PageList1_Director" class="hideClassSpan"></span></td>
			                <!--<input id="PageList2_DirectorName" type="text" value="" /><span id="PageList2_Director" class="hideClassSpan"></span>-->
			                <th>日　　期</th>
			                <td><input id="PageList1_Date" class="date" type="text" value="" /></td>
			            </tr>
			         </table>
			    </div>
			    
			    <div id="page1" style="display:none;">
			        <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="150">班級名稱</th>
			                <td><input id="PageList2_ClassName" type="text" value="" readonly="readonly" /><span id="PageList2_ClassNameID" class="hideClassSpan"></span></td>
			                <th width="150">教師姓名</th>
			                <td><input id="PageList2_teacherName" type="text" value="" readonly="readonly" /><span id="PageList2_TeacherID" class="hideClassSpan"></span></td>
			            </tr>
			            <tr>
			                <th>學生年齡</th>
			                <td><input id="PageList2_StudentAgeFrom" type="text" value="" size="5" /> 歲至 <input id="PageList2_StudentAgeTo" type="text" value="" size="5" /> 歲</td>
			                <th>檢核日期</th>
			                <td><input id="PageList2_InspectDate" type="text" class="date" value="" /></td>
			            </tr>
			            <tr>
			                <th>課　　別</th>
			                <td colspan="3"><label><input type="radio" name="PageList2_CourseType" value="1" /> 團體課</label>　　
			                    <label><input type="radio" name="PageList2_CourseType" value="2" /> 聽故事學語文課</label>　　
			                    <label><input type="radio" name="PageList2_CourseType" value="3" /> 其它</label> <input id="PageList2_CourseOther"  type="text" value="" />
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4">
			                    <p><br />壹、檢核項目<span style="float:right;text-align:right;">評分標準：欠佳＜---------＞優<br />不作答：忽略　　1　　3　　5　</span></p>
			                    <table class="tableContact" width="780" border="0">
			                        <thead>
			                            <tr>
			                                <th>檢核項目</th>
			                                <th width="110">評分概況</th>
			                            </tr>
			                        </thead>
			                        <tbody>
			                            <tr>
			                                <td colspan="2">一、適切的教學目標及活動</td>
			                            </tr>
			                            <tr>
			                                <td>1. ISP之聽覺技巧內容符合孩子學習需求</td>
			                                <td align="center"><select  id="PageList2_Score1" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. ISP之認知語言溝通內容符合孩子學習需求</td>
			                                <td align="center"><select  id="PageList2_Score2" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. ISP之人際互動內容符合孩子學習需求</td>
			                                <td align="center"><select  id="PageList2_Score3" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="5">5</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 當次聽覺技巧課程目標符合所設計的ISP內容</td>
			                                <td align="center"><select  id="PageList2_Score4" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 當次認知語言溝通課程目標符合所設計的ISP內容</td>
			                                <td align="center"><select  id="PageList2_Score5" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>6. 當次人際互動課程目標符合所設計的ISP內容</td>
			                                <td align="center"><select  id="PageList2_Score6" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>7. 活動呈現方式能由已知的能力引導至新的學習，具引導與整合性</td>
			                                <td align="center"><select  id="PageList2_Score7" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>8. 活動呈現方式能考量孩子特質及年齡等</td>
			                                <td align="center"><select  id="PageList2_Score8" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>9. 活動的設計與呈現方式能引發孩子學習的動機並適時提升孩子能力</td>
			                                <td align="center"><select  id="PageList2_Score9" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>10. 活動進行流暢，少有中斷的情形</td>
			                                <td align="center"><select  id="PageList2_Score10" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>11.	教具準備充份不需當場製作或尋找</td>
			                                <td align="center"><select  id="PageList2_Score11" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">二、適當的語言及溝通技巧</td>
			                            </tr>
			                            <tr>
			                                <td>1. 能恰當地運用相關語言輸入技巧，至少下列兩項以上<br />
                                                （延伸、擴展、自言自語、平行談話、重述、仿說、示範、提供訊息等技巧）</td>
			                                <td align="center"><select  id="PageList2_Score12" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 語言恰當地運用輸入內容，至少下列兩項以上<br />
                                                （命名、描述、比較、說明、給指令）</td>
			                                <td align="center"><select  id="PageList2_Score13" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="4">4</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. 能運用技巧引發孩子進行表達</td>
			                                <td align="center"><select  id="PageList2_Score14" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 能用完整句輸入且語調富變化、自然、流暢（咬字清晰、斷句適當、說話速度及音量適中）</td>
			                                <td align="center"><select  id="PageList2_Score15" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 能適當的使用情境語言與孩子對話或互動</td>
			                                <td align="center"><select  id="PageList2_Score16" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">三、互動技巧</td>
			                            </tr>
			                            <tr>
			                                <td>1. 能依班級的年齡或發展，運用不同的遊戲方式來引發孩子的學習動機或挑戰的意願</td>
			                                <td align="center"><select  id="PageList2_Score17" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 能觀察班級的能力表現，適時調整目標、活動的難易度或活動方式，以維持孩子的學習動機</td>
			                                <td align="center"><select  id="PageList2_Score18" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. 能依班級的能力表現，對孩子有適當要求（仿說、回答或溝通的動作行為等）</td>
			                                <td align="center"><select  id="PageList2_Score19" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 能恰當地應用對話技巧，至少下列兩項以上<br />
                                                （能運用「注視」的技巧（眼神、表情及情緒互動）、「等待與傾聽」、「輪替」、「轉換話題」、「結束話題」的技巧）</td>
			                                <td align="center"><select  id="PageList2_Score20" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="3">3</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 在活動中，能運用技巧建立孩子間互相傾聽的習慣及態度<br />
                                                （能運用「注視」的技巧（眼神、表情及情緒互動）、「等待與傾聽」、「輪替」、「轉換話題」、「結束話題」的技巧）</td>
			                                <td align="center"><select  id="PageList2_Score21" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>6. 能以豐富的肢體、聲音等吸引孩子的注意力與學習動機</td>
			                                <td align="center"><select  id="PageList2_Score22" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>7. 能在活動或對話中，鼓勵孩子的表達，給予適當的時間等待孩子的回應，不指責不符合期待時的表現</td>
			                                <td align="center"><select  id="PageList2_Score23" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>8. 能提供機會讓孩子主動操作、探索或練習</td>
			                                <td align="center"><select  id="PageList2_Score24" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">四、班級經營</td>
			                            </tr>
			                            <tr>
			                                <td>1. 能運用適當策略有效的建立上課規範，營造良好的上課氣氛</td>
			                                <td align="center"><select  id="PageList2_Score25" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 在活動中能運用技巧建立孩子發言的禮貌</td>
			                                <td align="center"><select  id="PageList2_Score26" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. 能有效處理孩子的負面行為或情緒</td>
			                                <td align="center"><select  id="PageList2_Score27" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 能引導孩子主動分享、關懷的情操</td>
			                                <td align="center"><select  id="PageList2_Score28" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">五、家長諮詢</td>
			                            </tr>
			                            <tr>
			                                <td>1. 能清楚解答家長所提的問題，說明每位學生的學習狀況與需加強的地方</td>
			                                <td align="center"><select  id="PageList2_Score29" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 能適時提供課後教學方法，讓家長能配合教學活動，發揮教學技能</td>
			                                <td align="center"><select  id="PageList2_Score30" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>3. 能以淺顯易懂的話語清楚解說教學目標，避免過多的專有名詞</td>
			                                <td align="center"><select  id="PageList2_Score31" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>4. 能以正面關懷的語氣和表情進行諮詢，適時的鼓勵或激勵家長教學的動機</td>
			                                <td align="center"><select  id="PageList2_Score32" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>5. 能運用資源有效的監督家長的參與（輔具檢查、在家教學、相關活動..）</td>
			                                <td align="center"><select  id="PageList2_Score33" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>6. 能有效經營家長間的關係，並增進家長互助互諒的態度</td>
			                                <td align="center"><select  id="PageList2_Score34" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td colspan="2">六、個案資料夾檢核</td>
			                            </tr>
			                            <tr>
			                                <td>1. 個案資料完整且放置位置正確</td>
			                                <td align="center"><select  id="PageList2_Score35" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                            <tr>
			                                <td>2. 個案資料適時更新</td>
			                                <td align="center"><select  id="PageList2_Score36" ><option value="0">0</option><option value="1">1</option><option value="2">2</option><option value="99">忽略</option></select></td>
			                            </tr>
			                        </tbody>
			                    </table>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4"><br />貳、教學服務檢核表平均分數相關督導建議參考如下：<br />
			                    1. 檢核總得分為教師當次考核分數<br />
                                2. 考核中未評到的項目，直接給分<br />
                                3. 各項項得分比例若未達該項的65%，督導需針對該項目進行檢討與追蹤<br />
                                4. 總分成績<br />
                                75分以上：建議每學期例行教學督導觀摩課程1次、參與相關研討、必要時教師得提出需求<br />
			                    65-74分：督導人員向中心主管報告，增加督導看課次數或安排研討<br />
			                    64分以下：督導人員可協請中心主管、總幹事共同進行課程督導，增加督導看課次數或研討，同時提出具體改進的目標或方法，必要時得安排教學考核
			                    <br /><br />
			                </td>
			            </tr>
			            <tr>
			                <td colspan="4">叁、得分統計<br />
			                    <table class="tableContact" width="780" border="0">
		                            <tr>
		                                <th width="50">項　目</th>
		                                <th width="140">一、適切的教學目標及活動</th>
		                                <th width="140">二、適當的語言及溝通技巧</th>
		                                <th width="110">三、互動技巧</th>
		                                <th width="110">四、班級經營</th>
		                                <th width="110">五、家長諮詢</th>
		                                <th width="120">六、個案資料夾檢核</th>
		                            </tr>
		                            <tr>
		                                <th>得　分</th>
		                                <td><input id="PageList2_SumScore1" type="text" value="" size="5" /> ／<span id="PageList2_Denominator1" >40</span></td>
		                                <td><input id="PageList2_SumScore2" type="text" value="" size="5" /> ／<span id="PageList2_Denominator2" >15</span></td>
		                                <td><input id="PageList2_SumScore3" type="text" value="" size="5" /> ／<span id="PageList2_Denominator3" >20</span></td>
		                                <td><input id="PageList2_SumScore4" type="text" value="" size="5" /> ／<span id="PageList2_Denominator4" >8</span></td>
		                                <td><input id="PageList2_SumScore5" type="text" value="" size="5" /> ／<span id="PageList2_Denominator5" >12</span></td>
		                                <td><input id="PageList2_SumScore6" type="text" value="" size="5" /> ／<span id="PageList2_Denominator6" >4</span></td>
		                            </tr>
		                            <tr>
		                                <th>百分比</th>
		                                <td><input id="PageList2_PerScore1" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList2_PerScore2" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList2_PerScore3" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList2_PerScore4" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList2_PerScore5" type="text" value="" size="10" /> %</td>
		                                <td><input id="PageList2_PerScore6" type="text" value="" size="10" /> %</td>
		                            </tr> 
		                            <tr>
		                                <th>總　分</th>
		                                <td colspan="6"><input id="PageList2_SumScore" type="text" value="" />　<input type="button" value="計算" onclick="calInspect(1)" /></td>
		                            </tr>
			                    </table><br />
			                </td>
			            </tr>
			            <tr>
			                <th>督導人員</th>
			                <td><input id="PageList2_SupervisorName" type="text" value="" /></td>
			                <th>授課教師</th>
			                <td><input  id="PageList2_teacherName1" type="text" value="" readonly="readonly" /></td>
			            </tr>
			            <tr>
			                <th>中心主任</th>
			                <td><input id="PageList2_DirectorName" type="text" value="" /><span id="PageList2_Director" class="hideClassSpan"></span></td>
			                <th>日　　期</th>
			                <td><input id="PageList2_Date" class="date" type="text" value="" /></td>
			            </tr>
			         </table>
			    </div>
			    
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
