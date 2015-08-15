<%@ Page Language="C#" AutoEventWireup="true" CodeFile="student_database.aspx.cs" Inherits="student_database" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 學生基本資料 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/student_database.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
		<script type="text/javascript" src="./js/jquery.timePicker.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/jquery.timePicker.css" />
    <script type="text/javascript" src="./js/base.js"></script>
    <script type="text/javascript" src="./js/jquery.form.js"></script>
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
                <asp:ScriptReference Path="~/js/student_database.js" />
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
			<div id="mainClass">個案管理&gt; 學生基本資料</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./student_database.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input type="text" id="gosrhstudentName" value=""/></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex">
			                <option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 
                            <input class="date" type="text" size="10" id="gosrhbirthdaystart" value=""/>～
                            <input class="date" type="text" size="10" id="gosrhbirthdayend" value=""/></td>
			            </tr>
			            <tr>
			                <td>服務使用者編號 <input type="text" id="gosrhstudentID" value=""/></td>
			                <td>
			                    個案狀態 <select id="gosrhcaseStatu"><option value="0">請選擇</option>
			                    <option value="1">未入會</option><option value="2">已入會</option>
			                    <option value="3">短結</option><option value="4">結案</option>
			                    </select>
			                </td>
			                <td>入會日期 <input class="date" type="text" size="10" id="gosrhjoindaystart" value="" />～
			                <input class="date" type="text" size="10" id="gosrhjoindayend" value="" /></td>
			            </tr>
			            <tr>
			                <td>結案日期 <input class="date" type="text" size="10" id="gosrhendReasonDatestart" value="" />～
			                <input class="date" type="text" size="10" id="gosrhendReasonDateend" value="" /></td>
			                <td>
			                    結案原因 <select id="gosrhendReasonType"><option value="0">請選擇</option>
			                    <option value="1">家長主動要求結案</option><option value="2">進入小學就讀</option>
			                    <option value="3">進入一般幼兒園就讀</option><option value="4">暫無聽語訓練需求</option>
			                    <option value="5">家庭意外變故</option><option value="6">其它原因</option></select>
			                </td>
			                <td>
			                    未入會原因 <select id="gosrhnomembershipType"><option value="0">請選擇</option>
			                    <option value="1">尚無聽語訓練需求</option><option value="2">家庭因素</option>
			                    <option value="3">已安排至其他單位療育</option><option value="4">距離太遠</option></select>
			                </td>
			            </tr>
			            
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="Search()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">服務使用者編號</th>
			                    <th width="100">個案狀態</th>
			                    <th width="100">學生姓名</th>
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
			    <div id="item1" class="menuTabs">個人基本資料</div>
			    <div id="item2" class="menuTabs">家庭背景資料</div>
			    <div id="item3" class="menuTabs">福利資源使用狀況</div>
			    <div id="item4" class="menuTabs">家庭概況</div>
			    <div id="item5" class="menuTabs">生產、發展及醫療史</div>
			    <div id="item6" class="menuTabs">聽力相關資料</div>
			    <div id="item7" class="menuTabs">教學諮詢紀錄</div>
			    <div id="item8" class="menuTabs">身高體重紀錄</div>
			</div>
			<div id="mainContent">
			    <div id="item1Content">
			        <p style="background-color:#FFDF71;padding:0 10px;">
			            <font size="3" id="caseStatu">個案狀態：未入會</font>
			            <span style="float:right;margin-top: 3px;"><select id="Unit"></select></span>
			        </p>
			        <p align="right">接案諮詢日期 <span><input id="consultationDate" class="date" type="text" size="10" value="" /></span>　　
			        排定評估日期 <span><input class="date" type="text" size="10" id="assessDate" value="" /></span>　　
			        更新日期 <span><input id="upDate" class="date" type="text" size="10" value="" readonly="readonly"/></span>　　
			        填寫者 <input id="fillInName" type="text" size="10" value="" readonly="readonly"/></p>
			        <form id="Form2" name="GmyForm1" action="" method="post" enctype="multipart/form-data">
			        <table width="780" border="0" id="">
			            <tr>
			                <th width="150">服務使用者編號</th>
			                <td><span id="studentID" ></span></td>
			                <td width="50">&nbsp;</td>
			                <th rowspan="9" width="200">
			                <!-- <div class='uploadcontrol'>
			                    <div style="text-align:center;">大頭照<br />
			                    <iframe src="upload.aspx?id=studentPhoto" id="ifUpload" frameborder="no" 
			                    allowtransparency="true" scrolling="no" style="width:300px; height:80px;"></iframe>
                                <div class="uploading" id="uploading1" style="display:none;" ></div>
                                <a class="showUploadImg" href="./images/noAvatar.jpg">
			                    <img id="studentPhoto" src="./images/noAvatar.jpg" alt="" border="0" /></a></div>
                            </div> -->
                            <p align="center">大頭照</p>
			                 <input id="studentPhotoUpload" type="file" name="studentPhoto" size="1" autocomplete="OFF"><br>
			                 <a id="studentPhotoUrl" class="showUploadImg" href="./images/noAvatar.jpg"><img id="studentPhoto" src="./images/noAvatar.jpg" alt="" border="0"></a>
			                
			                <p style="font-weight:normal;">點圖放大</p></th>
			            </tr>
			            <tr>
			                <th>學生姓名</th>
			                <td><input id="studentName" type="text" /><span class="startMark">*</span></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>出生日期</th>
			                <td>
			                    <input type="text" id="studentbirthday" class="date3" value=""/>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>年　　齡</th>
			                <td><input type="text" id="studentAge" value="0" size="5" /> 歲 <input type="text" id="studentMonth" value="0" size="5" /> 月</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>性　　別</th>
			                <td><label><input type="radio" name="studentSex" value="1" /> 男</label>　　
			                <label><input type="radio" name="studentSex" value="2" /> 女</label></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>身份證字號</th>
			                <td><input id="studentTWID" type="text" maxlength="10" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>課程保留金繳交日期</th>
			                <td><input id="guaranteeDate" class="date" type="text" size="10" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>首次上課日期</th>
			                <td><input id="firstClassDate" class="date" type="text" size="10" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>結案日期</th>
			                <td><input id="endReasonDate" class="date" type="text" size="10" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>結案原因</th>
			                <td colspan="3"><select id="endReasonType"><option value="0">請選擇</option><option value="1">家長主動要求結案</option><option value="2">進入小學就讀</option><option value="3">進入一般幼兒園就讀</option><option value="4">暫無聽語訓練需求</option><option value="5">家庭意外變故</option><option value="6">其它原因</option></select><br /><textarea id="endReason"></textarea></td>
			            </tr>
			            <tr>
			                <th>短結日期</th>
			                <td><input id="sendDateSince" class="date" type="text" value="" size="10" />～<input id="sendDateUntil" class="date" type="text" value="" size="10" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <th>未入會原因</th>
			                <td colspan="3"><select id="nomembershipType"><option value="0">請選擇</option><option value="1">尚無聽語訓練需求</option><option value="2">家庭因素</option><option value="3">已安排至其他單位療育</option><option value="4">距離太遠</option></select><br /><textarea id="nomembershipReason"></textarea></td>
			            </tr>
			            <tr>
			                <th>戶籍地址</th>
			                <td colspan="3"><input id="censusAddressZip" type="text" maxlength="5" value="" size="5"/> <select id="censusAddressCity" class="zoneCity"></select> <input id="censusAddress" type="text" value="" size="50"/></td>
			            </tr>
			            <tr>
			                <th>通訊地址　<label><input type="checkbox" name="AddressCopyaddress" value="1"/> 同上</label></th>
			                <td colspan="3"><input id="addressZip" type="text" maxlength="5" value=""  size="5"/> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" value="" size="50"/></td>
			            </tr>
			            <tr>
			                <th>主要照顧者</th>
			                <td colspan="3">
			                    <ul>
			                        <li>白天：<label><input type="radio" name="wCare" value="1" /> 父親</label>　　
			                        <label><input type="radio" name="wCare" value="2" /> 母親</label>　　
			                        <label><input type="radio" name="wCare" value="3" /> 其他</label> <input id="wCareName" type="text" value=""/></li>
			                        <li>夜間：<label><input type="radio" name="bCare" value="1" /> 父親</label>　　
			                        <label><input type="radio" name="bCare" value="2" /> 母親</label>　　
			                        <label><input type="radio" name="bCare" value="3" /> 其他</label> <input id="bCareName" type="text" value=""/></li>
			                    </ul>
			                </td>
			            </tr>
			            <tr>
			                <th>聯 絡 人</th>
			                <td colspan="3">
			                    <table class="tableContact" width="600" border="0">
			                        <tr>
			                            <th>姓名</th>
			                            <th>聯絡方式</th>
			                        </tr>
			                        <tr id="PrimaryContact1">
			                            <td>主要聯絡人<br />關係 <input id="fPRelation1" type="text" value="" size="5" /><br /><input id="fPName1" type="text" value="" /></td>
			                            <td>
			                                (公) <input id="fPTel1" type="text" value="" />　
			                                (手機) <input id="fPPhone1" type="text" value="" /><br />
			                                (家) <input id="fPHPhone1" type="text" value="" />　
			                                (傳真) <input id="fPFax1" type="text" value="" />
			                            </td>
			                        </tr>
			                        <tr id="PrimaryContact2">
			                            <td>法定代理人<br />關係 <input id="fPRelation2" type="text" size="5" value="" /><br /><input id="fPName2" type="text" value="" /></td>
			                            <td>
			                                (公) <input id="fPTel2" type="text"  value="" />　
			                                (手機) <input id="fPPhone2" type="text" value="" /><br />
			                                (家) <input id="fPHPhone2" type="text" value="" />　
			                                (傳真) <input id="fPFax2" type="text" value="" />
			                            </td>
			                        </tr>
			                        <tr id="PrimaryContact3">
			                            <td>關係 <input id="fPRelation3" type="text" size="5" value="" /><br /><input id="fPName3" type="text" value="" /></td>
			                            <td>
			                                (公) <input id="fPTel3" type="text" value="" />　
			                                (手機) <input id="fPPhone3" type="text" value="" /><br />
			                                (家) <input id="fPHPhone3" type="text" value="" />　
			                                (傳真) <input id="fPFax3" type="text" value="" />
			                            </td>
			                        </tr>
			                        <tr >
			                            <td>關係 <input id="fPRelation4" type="text" size="5" value="" /><br /><input id="fPName4" type="text" value="" /></td>
			                            <td>
			                                (公) <input id="fPTel4" type="text" value="" />　
			                                (手機) <input id="fPPhone4" type="text" value="" /><br />
			                                (家) <input id="fPHPhone4" type="text" value="" />　
			                                (傳真) <input id="fPFax4" type="text" value="" />
			                            </td>
			                        </tr>
			                    </table>
			                </td>
			            </tr>
			            <tr>
			                <th>E-mail</th>
			                <td colspan="3"><input id="email" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <th>轉介來源</th>
			                <td colspan="3">
			                    <select id="sourceType" >
			                    <option value="0">來源類別</option>
			                    <option value="1">醫院</option>
			                    <option value="2">學校</option>
			                    <option value="3">家長</option>
			                    <option value="4">助聽器公司</option>
			                    <option value="5">早療單位</option>
			                    <option value="6">網路</option>
			                    <option value="7">其他</option>
			                    </select> 
			                    <input type="text" id="sourceName" />
			                    <!--
			                    <select id="sourceName">
			                    <option value="0">機構名稱</option>
			                    </select>
			                    -->
			                    
                            </td>
			            </tr>
			            <tr>
			                <th>領有身心障礙證明</th>
			                <td colspan="3">
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
			            </tr>
			            <tr>
			                <th>身心障礙證明影本</th>
			                <td colspan="3">
    		                    <!--<iframe src="upload.aspx?id=studentManualImg" id="ifUpload2" frameborder="no" 
			                    allowtransparency="true" scrolling="no" style="width:300px; height:80px;"></iframe>
                                <div class="uploading" id="Div1" style="display:none;" ></div>
                                <a class="showUploadImg" href="./images/noAvatar.jpg"><img id="studentManualImg" src="./images/noPhoto2.jpg" alt="" border="0" /></a>
                                -->    
                                <input id="manualUpload" type="file" name="studentManualImg" size="1" autocomplete="OFF" />
                               <br />
			                    <a id="studentManualImgUrl" class="showUploadImg" href="./images/noPhoto.jpg"><img id="studentManualImg" src="./images/noPhoto2.jpg" alt="" border="0"></a>               		               
			                    <span style="font-weight:normal;">點圖放大</span>
			                </td>
			            </tr>
			            <tr>
			                <th>早療通報</th>
			                <td colspan="3">
			                    <div id="notificationDiv1"><label><input type="radio" name="notification" value="1" /> 已由其他單位通報</label>（通報單位 <input class="notificationUnit" type="text" value="" />／個管員 <input class="notificationManage"  type="text" size="15" value="" />　電話 <input class="notificationTel" type="text" size="15" value="" />）</div><br />
			                    <div id="notificationDiv2"><label><input type="radio" name="notification" value="2" /> 由本中心通報</label>（通報日期 <input class="date notificationDate" type="text" value="" />／通報縣市 <select class="zoneCity notificationCity"></select> ／單位 <input class="notificationUnit"  type="text" value="" />／個管員 <input class="notificationManage" type="text" size="15" value="" />　電話 <input class="notificationTel" type="text" size="15" value="" />）</div>
			                </td>
			            </tr>
			            <tr>
			                <th>經濟狀況</th>
			                <td colspan="3">
			                    <label><input type="radio" name="economy" value="1" /> 低收入戶</label> <select id="economyLow"><option value="0">請選擇</option><option value="1">第0類</option><option value="2">第1類</option><option value="3">第2類</option><option value="4">第3類</option><option value="5">第4類</option></select>　　
			                    <label><input type="radio" name="economy" value="2" /> 中低收入戶</label>　　
			                    <label><input type="radio" name="economy" value="3" /> 一般戶</label>
			                </td>
			            </tr>
			        </table>
			        </form>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(1)">下一頁</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="saveStudentData(1)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    
			    <div id="item2Content">
			        <table width="780" border="0"><caption>家庭成員狀況</caption>
			            <thead><tr>
			                <th>稱謂</th>
			                <th>姓名</th>
			                <th>出生日期</th>
			                <th>目前年齡</th>
			                <th>學歷</th>
			                <th>職業</th>
			                <th>居住狀況</th>
			                <th>聽力狀況</th>
			                <th>健康狀況</th>
			            </tr></thead>
			            <tbody><tr>
			                <td align="center"><input id="fAppellation1" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName1" type="text" value="" size="15"/></td>
			                <td align="center"><input id="fBirthday1" class="fBirthday date3" type="text" value="" size="10" /></td>
			                <td align="center"><input id="fAge1" type="text" value="" size="5" /></td>
			                <td align="center"><select id="fEDU1"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession1" type="text" value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive1" value="1" /> 同住</label><br /><label><input type="radio" name="fLive1" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing1" value="1" /> 正常</label><br /><label><input type="radio" name="fHearing1" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy1" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy1" value="2" /></label> <input id="familyText01" type="text" size="10" value="" /></td>
			            </tr><tr>
			                <td align="center"><input id="fAppellation2" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName2" type="text" value="" size="15"/></td>
			                <td align="center"><input id="fBirthday2" class="fBirthday date3" type="text" value=""  size="10" /></td>
			                <td align="center"><input id="fAge2" type="text" value="" size="5" /></td>
			                <td align="center"><select id="fEDU1"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession2" type="text" value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive2" value="1" /> 同住</label><br /><label><input type="radio" name="fLive2" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing2" value="1" /> 正常</label><br /><label><input type="radio" name="hearing2" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy2" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy2" value="2" /></label> <input id="familyText02" type="text" value="" size="10" /></td>
			            </tr><tr>
			                <td align="center"><input id="fAppellation3" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName3" type="text" value="" size="15"/></td>
			                <td align="center"><input id="fBirthday3" class="fBirthday date3" type="text" value="" size="10" /></td>
			                <td align="center"><input id="fAge3" type="text" value="" size="5" /></td>
			                <td align="center"><select id="fEDU3"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession3" type="text" value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive3" value="1" /> 同住</label><br /><label><input type="radio" name="fLive3" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing3" value="1" /> 正常</label><br /><label><input type="radio" name="fHearing3" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy3" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy3" value="2" /></label> <input id="familyText03" type="text" value="" size="10" /></td>
			            </tr><tr>
			                <td align="center"><input id="fAppellation4" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName4" type="text" value="" size="15"/></td>
			                <td align="center"><input id="fBirthday4" class="fBirthday date3" type="text" value="" size="10" /></td>
			                <td align="center"><input id="fAge4" type="text" value="" size="5" /></td>
			                <td align="center"><select id="fEDU4"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession4" type="text" value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive4" value="1" /> 同住</label><br /><label><input type="radio" name="fLive4" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing4" value="1" /> 正常</label><br /><label><input type="radio" name="fHearing4" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy4" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy4" value="2" /></label> <input id="familyText04" type="text" value="" size="10" /></td>
			            </tr><tr>
			                <td align="center"><input id="fAppellation5" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName5" type="text" name=""  value="" size="15"/></td>
			                <td align="center"><input id="fBirthday5" class="fBirthday date3" type="text" name=""  value="" size="10" /></td>
			                <td align="center"><input id="fAge5" type="text" name=""  value="" size="5" /></td>
			                <td align="center"><select id="fEDU5"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession5" type="text" name=""  value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive5" value="1" /> 同住</label><br /><label><input type="radio" name="fLive5" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing5" value="1" /> 正常</label><br /><label><input type="radio" name="fHearing5" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy5" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy5" value="2" /></label> <input id="familyText05" type="text" value="" size="10" /></td>
			            </tr><tr>
			                <td align="center"><input id="fAppellation6" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName6" type="text" name="" value="" size="15"/></td>
			                <td align="center"><input id="fBirthday6" class="fBirthday date3" type="text" name="" value=""  size="10" /></td>
			                <td align="center"><input id="fAge6" type="text" name=""  value="" size="5" /></td>
			                <td align="center"><select id="fEDU6"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession6" type="text" name=""  value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive6" value="1" /> 同住</label><br /><label><input type="radio" name="fLive6" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing6" value="1" /> 正常</label><br /><label><input type="radio" name="fHearing6" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy6" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy6" value="2" /></label> <input id="familyText06" type="text" value="" size="10" /></td>
			            </tr><tr>
			                <td align="center"><input id="fAppellation7" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName7" type="text" value="" size="15"/></td>
			                <td align="center"><input id="fBirthday7" class="fBirthday date3" type="text" value="" size="10" /></td>
			                <td align="center"><input id="fAge7" type="text" value="" size="5" /></td>
			                <td align="center"><select id="fEDU7"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession7" type="text" value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive7" value="1" /> 同住</label><br /><label><input type="radio" name="fLive7" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing7" value="1" /> 正常</label><br /><label><input type="radio" name="fHearing7" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy7" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy7" value="2" /></label> <input id="familyText07" type="text" value="" size="10" /></td>
			            </tr><tr>
			                <td align="center"><input id="fAppellation8" type="text" value="" size="5" /></td>
			                <td align="center"><input id="fName8" type="text" value="" size="15"/></td>
			                <td align="center"><input id="fBirthday8" class="fBirthday date3" type="text" value="" size="10" /></td>
			                <td align="center"><input id="fAge8" type="text"  value="" size="5" /></td>
			                <td align="center"><select id="fEDU8"><option value="0">請選擇</option><option value="1">國小以下</option><option value="2">國小</option><option value="3">國中</option><option value="4">高中職</option><option value="5">專科</option><option value="6">大學</option><option value="7">研究所</option></select></td>
			                <td align="center"><input id="fProfession8" type="text"  value="" size="15" /></td>
			                <td><label><input type="radio" name="fLive8" value="1" /> 同住</label><br /><label><input type="radio" name="fLive8" value="2" /> 不同住</label></td>
			                <td><label><input type="radio" name="fHearing8" value="1" /> 正常</label><br /><label><input type="radio" name="fHearing8" value="2" /> 聽障</label></td>
			                <td><label><input type="radio" name="fHealthy8" value="1" /> 佳</label><br /><label><input type="radio" name="fHealthy8" value="2" /></label> <input id="familyText08" type="text" value="" size="10" /></td>
			            </tr></tbody>
			            <tfoot><tr>
			                <th colspan="2">家族使用的語言</th>
			                <td colspan="7">
			                    <label><input type="checkbox" name="lang1" value="1" /> 國語</label>　
			                    <label><input type="checkbox" name="lang1" value="2" /> 台語</label>　
			                    <label><input type="checkbox" name="lang1" value="3" /> 客語</label>　
			                    <label><input type="checkbox" name="lang1" value="4" /> 手語</label>　
			                    <label><input type="checkbox" name="lang1" value="5" /> 其它</label> <input type="text" id="lang1t01"  value="" />
			                </td>
			            </tr><tr>
			                <th colspan="2">家人與學員溝通之語言</th>
			                <td colspan="7">
			                    <label><input type="checkbox" name="lang2" value="1" /> 國語</label>　
			                    <label><input type="checkbox" name="lang2" value="2" /> 台語</label>　
			                    <label><input type="checkbox" name="lang2" value="3" /> 客語</label>　
			                    <label><input type="checkbox" name="lang2" value="4" /> 手語</label>　
			                    <label><input type="checkbox" name="lang2" value="5" /> 其它</label> <input type="text" id="lang2t01"  value="" />
			                </td>
			            </tr></tfoot>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(3)">下一頁</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="saveStudentData(2)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item3Content">
			        <table width="780" border="0" class="table2Contact">
			        <thead>
			            <tr>
			                <th width="100">日期</th>
			                <th width="100">資源卡編號</th>
			                <th width="190">機構名稱</th>
			                <th width="190">資源項目</th>
			                <th width="100">資源類別</th>
			                <th width="70">功能</th>
			            </tr>
			            </thead>
			            <tbody>    
			            </tbody>
			        </table>
			        <p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData()">新 增</button></p>
		            <div id="insertDataDiv1" style="display:none">
		                <table class="tableList" width="780" border="0">
		                    <thead>
		                        <tr>
			                        <th width="100">日期</th>
			                        <th width="100">資源卡編號</th>
			                        <th>機構名稱</th>
			                        <th>資源項目</th>
			                        <th>資源類別</th>
			                        <th width="110">功能</th>
		                        </tr>
		                    </thead>
		                    <tbody>
		                        <tr >
		                            <td><input id="resourceDate" class="date" type="text" value="" size="10" /></td>
		                            <td><input id="resourceID" type="text" value="" size="10" readonly="readonly" /></td>
		                            <td id="resourceName"></td>
		                            <td id="resourceItem"></td>
		                            <td id="resourceType"></td>
		                            <td><button class="btnView" type="button" onclick="setResourceData();">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert(1)">取 消</button></td>
		                        </tr>
		                     </tbody>
		                </table>
		            </div>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(3)">下一頁</button>
			            <!--<button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>-->
			        </p>
			    </div>
			    <div id="item4Content">
			        <table width="780" border="0">
			            <tr>
			                <th align="left">一、家庭生態圖：(需列點說明) <br />　　
			                <img id="Img1" src="./images/student_chart.jpg" alt="" border="0" />
			                <form id="Form3" name="GmyForm4" action="" method="post" enctype="multipart/form-data">
			                    <input id="File2" type="file" name="familyEcological" size="1" autocomplete="OFF"/>
			                </form>
			                <a id="familyEcologicalUrl" class="showUploadImg" href="#"><img id="familyEcological" src="#" alt="" border="0"></a><br>
			                <br /><br /></th>
			            </tr>
			            <tr>
			                <th align="left">二、家庭概況(需含案父母工作情況、家庭經濟狀況)<br /><textarea id="famailySituation" class="item4Textarea"></textarea><br /><br /></th>
			            </tr>
			            <tr>
			                <th align="left">三、生產、發展及醫療史<br /><textarea id="famailyMedical" class="item4Textarea"></textarea><br /><br /></th>
			            </tr>
			            <tr>
			                <th align="left">四、案家互動情況<br /><textarea id="famailyActionSituation" class="item4Textarea"></textarea><br /><br /></th>
			            </tr>
			            <tr>
			                <th align="left">五、社工評估：(含案家整體功能、後續需提供的服務內容)<br /><textarea id="fswAssess" class="item4Textarea"></textarea><br /><br /></th>
			            </tr>
			        </table>
			        <p align="right">社工員 <span><input type="text" id="socialName" class="searchStaff" readonly="readonly"/><span id="socialID" class="hideClassSpan"></span></span>　　填表日期 <span><input id="socialDate" class="date" type="text" size="10" /></span></p>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="saveStudentData(0)">存 檔</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button"  onclick="saveStudentData(4)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item5Content">
			        <label><input type="checkbox" name="history" value="1" /> 母親懷孕時無異狀</label><br />
			        <label><input type="checkbox" name="history" value="2" /> 生產過程正常</label>（<input type="text" id="history02t01"  value="" size="5" maxlength="2" />週，<input type="text" id="history02t02"  value="" size="5" maxlength="4" />公克）<br />
			        <label><input type="checkbox" name="history" value="3" /> 母親懷孕時感染(巨細胞病毒、德國痲疹、梅毒等)，</label>其他異狀 <input type="text" id="history03t01"  value="" size="40" /><br />
			        <label><input type="checkbox" name="history" value="4" /> 病理性黃疸</label><br />
			        <label><input type="checkbox" name="history" value="5" /> 出生時缺氧</label><br />
			        <label><input type="checkbox" name="history" value="6" /> 早產</label>（<input type="text" id="history06t01" value="" size="5" maxlength="2" />週，<input type="text" id="history06t02"  value="" size="5" maxlength="4" />公克）<br />
			        <label><input type="checkbox" name="history" value="7" /> 新生兒加護病房</label><br />
			        <label><input type="checkbox" name="history" value="8" /> 重大手術或疾病</label> <input type="text" id="history08t01" value="" /><br />
			        <label><input type="checkbox" name="history" value="9" /> 腦膜炎</label><br />
			        <label><input type="checkbox" name="history" value="10" /> 耳朵經常性積水或發炎</label>（最近一次 <input type="text" id="history10t01"  value="" />）<br />
			        <label><input type="checkbox" name="history" value="11" /> 殊發展史(動作、認知、情緒等)</label> <input type="text" id="history11t01" value="" /><br />
			        <label><input type="checkbox" name="history" value="12" /> 其他</label> <input type="text" id="history12t01" value="" size="100" />
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(5)">下一頁</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="saveHearinghistory(1)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item6Content">
			        <ul>
			            <li>一、如何發現聽力問題（發現年齡 <input type="text" id="problems01t01" value="" size="5" /> 歲 <input type="text" id="problems01t02" value="" size="5" /> 月）：<br />
			                <label><input type="checkbox" name="hearingQ" value="1" /> 比較晚開始說話</label>　
			                <label><input type="checkbox" name="hearingQ" value="2" /> 對聲音反應不好</label>　
			                <label><input type="checkbox" name="hearingQ" value="3" /> 新生兒聽力篩檢</label>　
			                <label><input type="checkbox" name="hearingQ" value="4" /> 學校老師</label>　
			                <label><input type="checkbox" name="hearingQ" value="5" /> 其他</label> <input type="text" id="hearingQText" value="" />
			            </li>
			            <li>二、確診聽損年齡：<input type="text" id="problems02t01" value="" size="5" /> 歲 <input type="text" id="problems02t02" value="" size="5" /> 月，<br />
			            於<input type="text" id="problems02t03" value="" />診斷，結果：左耳 <select id="problems02t04"><option value="0">請選擇</option><option value="1">極重度</option><option value="2">重度</option><option value="3">中度</option><option value="4">中重度</option><option value="5">輕度</option><option value="6">單側聽損</option></select>
			            ／右耳 <select id="problems02t05"><option value="0">請選擇</option><option value="1">極重度</option><option value="2">重度</option><option value="3">中度</option><option value="4">中重度</option><option value="5">輕度</option><option value="6">單側聽損</option></select></li>
			            <li>三、曾做過新生兒聽力篩檢：<br />
			                <label><input type="radio" name="hearingcheck" value="1" /> 否</label>　
			                <label><input type="radio" name="hearingcheck" value="2" /> 是，檢查：</label>
			                <label><input type="checkbox" name="hearingYescheck" value="1" /> AABR</label>　
			                <label><input type="checkbox" name="hearingYescheck" value="2" /> OAE</label>　
			                <label><input type="checkbox" name="hearingYescheck" value="3" /> 不知道，地點</label> <input type="text" id="hearingYesPlace" value="" />，<br />結果：左耳 <input type="text" id="hearingYesResultL" value="" size="10" />／右耳 <input type="text" id="hearingYesResultL" value="" size="10" />
			            </li>
			            <li>四、聽覺電生理檢查：<label><input type="radio" name="sleepcheck" value="1" /> 否</label>　　
			                <label><input type="radio" name="sleepcheck" value="2" /> 不清楚</label>　　
			                <label><input type="radio" name="sleepcheck" value="3" /> 是</label><br />
			                時間 <input class="date" type="text" id="sleepcheckTime1" value="" size="10" /> 地點 <input type="text" id="sleepcheckPlace1" value="" size="10" />，檢查項目 <select id="sleepcheckCheckItem1"><option value="0">請選擇</option><option value="1">TEOAE</option><option value="2">DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select>
			                ，結果：左耳 <input type="text" id="sleepcheckResultL1" value="" size="10" />／右耳 <input type="text" id="sleepcheckResultR1" value="" size="10" /><br />
			                時間 <input class="date" type="text" id="sleepcheckTime2" value="" size="10" /> 地點 <input type="text" id="sleepcheckPlace2" value="" size="10" />，檢查項目 <select id="sleepcheckCheckItem2"><option value="0">請選擇</option><option value="1">TEOAE</option><option value="2">DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select>
			                ，結果：左耳 <input type="text" id="sleepcheckResultL2" value="" size="10" />／右耳 <input type="text" id="sleepcheckResultR2" value="" size="10" /><br />
			                時間 <input class="date" type="text" id="sleepcheckTime3" value="" size="10" /> 地點 <input type="text" id="sleepcheckPlace3" value="" size="10" />，檢查項目 <select id="sleepcheckCheckItem3"><option value="0">請選擇</option><option value="1">TEOAE</option><option value="2">DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select>
			                ，結果：左耳 <input type="text" id="sleepcheckResultL3" value="" size="10" />／右耳 <input type="text" id="sleepcheckResultR3" value="" size="10" /><br />
			                時間 <input class="date" type="text" id="sleepcheckTime4" value="" size="10" /> 地點 <input type="text" id="sleepcheckPlace4" value="" size="10" />，檢查項目 <select id="sleepcheckCheckItem4"><option value="0">請選擇</option><option value="1">TEOAE</option><option value="2">DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select>
			                ，結果：左耳 <input type="text" id="sleepcheckResultL4" value="" size="10" />／右耳 <input type="text" id="sleepcheckResultR4" value="" size="10" /><br />
			                時間 <input class="date" type="text" id="sleepcheckTime5" value="" size="10" /> 地點 <input type="text" id="sleepcheckPlace5" value="" size="10" />，檢查項目 <select id="sleepcheckCheckItem5"><option value="0">請選擇</option><option value="1">TEOAE</option><option value="2">DPOAE</option><option value="3">ABR-click</option><option value="4">ABR-toneburst</option><option value="5">ASSR</option></select>
			                ，結果：左耳 <input type="text" id="sleepcheckResultL5" value="" size="10" />／右耳 <input type="text" id="sleepcheckResultR5" value="" size="10" />
			            </li>
			            <li>五、做過耳部斷層掃瞄(CT)或核磁共振(MRI)：<br />
			                <label><input type="radio" name="ctmri" value="1" /> 否</label>　
			                <label><input type="radio" name="ctmri" value="2" /> 是</label>，時間 <input type="text" class="date" id="ctmriTime"  value="" size="10" /> 地點 <input type="text" id="ctmriPlace"  value="" />，結果：左耳 <input type="text" id="ctmriResultL" value="" size="10" />／右耳 <input type="text" id="ctmriResultR" value="" size="10" />
			            </li>
			            <li>六、曾做過聽損基因篩檢：<br />
			                <label><input type="radio" name="gene" value="1" /> 否</label>　
			                <label><input type="radio" name="gene" value="2" /> 是</label>，時間 <input type="text" class="date" id="geneTime"  value="" size="10" /> 地點 <input type="text" id="genePlace"  value="" />，結果 <input type="text" id="geneResult"   value="" />
			            </li>
			            <li>七、家族聽損史：
			                <label><input type="radio" name="familyhistory" value="1" /> 否</label>　
			                <label><input type="radio" name="familyhistory" value="2" /> 是</label> <input type="text" id="familyhistoryText" value="" />
			            </li>
			            <li>八、聽力變化史：
			                <label><input type="radio" name="changehistory" value="1" /> 否</label>　
			                <label><input type="radio" name="changehistory" value="2" /> 是</label> <input type="text" id="changehistoryText" value="" />
			            </li>
			            <li>九、聽覺輔具管理：<br />
			                <label><input type="radio" name="assistmanage" value="1" /> 無輔具(以下免填)</label>　
			                <label><input type="radio" name="assistmanage" value="2" /> 耳模製作中(以下免填)</label>　
			                <label><input type="radio" name="assistmanage" value="3" /> 已選配輔具</label>（初配年齡<input type="text" id="accessory1" value="" size="5" maxlength="3" />歲 <input type="text" id="accessory2" value="" size="5" /> 月）　
			                <label><input type="radio" name="assistmanage" value="4" /> 已植入人工電子耳</label><br />
			                右耳：<label><input type="radio" name="assistmanageR" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageR" value="2" /> 電子耳</label>　
			                廠牌 <select id="brandR" ><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelR" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeR" value="" /> 選配/植入地點 <input type="text"  id="buyingPlaceR" value="" /><br />　　　
			                植入醫院醫生 <input type="text"  id="insertHospitalR" size="15" value="" /> 開頻日 <input class="date" type="text" id="openHzDateR" value="" size="10" /><br />
			                左耳：<label><input type="radio" name="assistmanageL" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageL" value="2" /> 電子耳</label>　
			                廠牌<select id="brandL"><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelL" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeL" value="" /> 選配/植入地點 <input type="text" id="buyingPlaceL" value="" /><br />　　　
			                植入醫院醫生 <input type="text" id="insertHospitalL" value="" size="15" /> 開頻日 <input class="date" type="text" id="openHzDateL" value="" size="10" /><br />
			                全日配戴輔具：
                            <label><input type="radio" name="allassis" value="1" /> 是</label>　
			                <label><input type="radio" name="allassis" value="2" /> 否，原因</label> <input type="text" id="allassisNoText" value="" size="" /><br />
			                能主動反應輔具聲音有無：
                            <label><input type="radio" name="assis1" value="1" /> 是</label>　
			                <label><input type="radio" name="assis1" value="2" /> 否，原因</label> <input type="text" id="assis1NoText" value="" size="" /><br />
			                輔具每日基礎保養及檢測：
                            <label><input type="radio" name="assis2" value="1" /> 是</label>　
			                <label><input type="radio" name="assis2" value="2" /> 否，原因</label> <input type="text" id="assis2NoText" value="" size="" />
			            </li>
			            <li>十、使用調頻系統：
			                <label><input type="radio" name="assisFM" value="1" /> 否</label>　
			                <label><input type="radio" name="assisFM" value="2" /> 是</label>（廠牌/型號<select id="assisFMBrand" ><option value="0">廠牌/型號</option></select>）
			            </li>
			            <li>十一、入學聽力評估摘要：</li>
			        </ul>
			                <table class="table2Contact" width="780" border="0">
			                    <tr>
			                        <td width="200"><label><input type="checkbox" name="assessnotes" value="1" /> 耳視鏡檢查</label></td>
			                        <td><label><input type="radio" name="assessnotes1" value="1" /> 無異常</label>　<label><input type="radio" name="assessnotes1" value="2" /> 異常</label> <input type="text" id="assessnotes102Text" size="55" /></td>
			                    </tr>
			                    <tr>
			                        <td width="200"><label><input type="checkbox" name="assessnotes" value="2" /> 純音聽力檢查</label></td>
			                        <td><input type="text" id="problems11t02" size="70" /></td>
			                    </tr>
			                    <tr>
			                        <td width="200"><label><input type="checkbox" name="assessnotes" value="3" /> 中耳鼓室圖</label></td>
			                        <td><label><input type="radio" name="assessnotes2" value="1" /> 雙耳在正常範圍內</label>　<label><input type="radio" name="assessnotes2" value="2" /> 需轉介</label></td>
			                    </tr>
			                    <tr>
			                        <td width="200"><label><input type="checkbox" name="assessnotes" value="4" /> 語音察覺/辨識閾</label></td>
			                        <td><input type="text" id="problems11t03" size="70" /></td>
			                    </tr>
			                    <tr>
			                        <td width="200"><label><input type="checkbox" name="assessnotes" value="5" /> 語音辨識率</label></td>
			                        <td><input type="text" id="problems11t04" size="70" /></td>
			                    </tr>
			                    <tr>
			                        <td width="200"><label><input type="checkbox" name="assessnotes" value="6" /> 輔具功能檢測</label></td>
			                        <td><input type="text" id="problems11t05" size="70" /></td>
			                    </tr>
			                    <tr>
			                        <td><label><input type="checkbox" name="assessnotes" value="7" /> 附聽力評估報告</label></td>
			                        <td><input type="text" id="problems11t06" value="" size="70" /></td>
			                    </tr>
			                    <tr>
			                        <td><label><input type="checkbox" name="" value="8" /> 其他</label></td>
			                        <td><input type="text" id="problems11t07" value="" size="70" /></td>
			                    </tr>
			                </table>
			        <p align="right">聽力師 <span><input type="text" id="inspectorName" value="" class="searchStaff" readonly="readonly"/><span id="inspectorID" class="hideClassSpan"></span></span>　　施測日期 <span><input id="inspectorDate" class="date" type="text" size="10" /></span></p>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(6)">下一頁</button>
			            <button class="btnUpdate" type="button">更 新</button>
		                <button class="btnSaveUdapteData" type="button" onclick="saveHearinghistory(2)">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item7Content">
			        <p>一、案父/案母（請勾選表示個案目前的狀況）</p>
	                <table class="table2Contact" width="780" border="0">
	                    <tr>
	                        <th width="260">案家配合度</th>
	                        <th width="260">教養態度</th>
	                        <th width="260">氣質性</th>
	                    </tr>
	                    <tr>
	                        <td>
	                            <label><input type="checkbox" name="case1" value="1" /> 主動積極</label><br />
	                            <label><input type="checkbox" name="case1" value="2" /> 提醒後表示能配合</label><br />
	                            <label><input type="checkbox" name="case1" value="3" /> 需隨時督促</label><br />
	                            <label><input type="checkbox" name="case1" value="4" /> 委託他人/</label> <input type="text" id="case1t04" size="15" value="" /><br />
	                            <label><input type="checkbox" name="case1" value="5" /> 配合上有困難，</label><br />　原因 <input type="text" id="case1t05" size="15" value="" />
	                        </td>
	                        <td>
	                            <label><input type="checkbox" name="case2" value="1" /> 規範</label>　　
	                            (<label><input type="checkbox" name="case21" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case21" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case21" value="3" /> </label><input type="text" id="case21t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case2" value="2" /> 嚴格約束</label>
	                            (<label><input type="checkbox" name="case22" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case22" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case22" value="3" /> </label><input type="text" id="case22t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case2" value="3" /> 寵溺</label>　　
	                            (<label><input type="checkbox" name="case23" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case23" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case23" value="3" /> </label><input type="text" id="case23t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case2" value="4" /> 放縱</label>　　
	                            (<label><input type="checkbox" name="case24" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case24" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case24" value="3" /> </label><input type="text" id="case24t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case2" value="5" /> 較無方法</label>
	                            (<label><input type="checkbox" name="case25" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case25" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case25" value="3" /> </label><input type="text" id="case25t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case2" value="6" /> 無所適從</label>
	                            (<label><input type="checkbox" name="case26" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case26" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case26" value="3" /> </label><input type="text" id="case26t01" value="" size="8" />)
	                        </td>
	                        <td>
	                            <label><input type="checkbox" name="case3" value="1" /> 個性較急</label>
	                            (<label><input type="checkbox" name="case31" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case31" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case31" value="3" /> </label><input type="text" id="case31t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case3" value="2" /> 個性溫合</label>
	                            (<label><input type="checkbox" name="case32" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case32" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case32" value="3" /> </label><input type="text" id="case32t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case3" value="3" /> 懶散</label>　　
	                            (<label><input type="checkbox" name="case33" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case33" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case33" value="3" /> </label><input type="text" id="case33t01" value="" size="8" />)<br />
	                            <label><input type="checkbox" name="case3" value="4" /> 自主性強</label>
	                            (<label><input type="checkbox" name="case34" value="1" /> 父</label>
	                            <label><input type="checkbox" name="case34" value="2" /> 母</label>
	                            <label><input type="checkbox" name="case34" value="3" /> </label><input type="text" id="case34t01" value="" size="8" />)<br />
	                        </td>
	                    </tr>
	                    <tr>
	                        <td colspan="3">備註說明：<br />
	                            <ul>
	                                <li>　◎ 諮詢當天案家出席人員：
	                                    <label><input type="checkbox" name="attend" value="1" /> 父</label>　
	                                    <label><input type="checkbox" name="attend" value="2" /> 母</label>　
	                                    <label><input type="checkbox" name="attend" value="3" /> 外婆/外公</label>　
	                                    <label><input type="checkbox" name="attend" value="4" /> 奶奶/爺爺</label>　
	                                    <label><input type="checkbox" name="attend" value="5" /> </label><input type="text" id="attend01t01" value="" />
	                                </li>
	                                <li>　◎ 授課時的主要陪同者：　
	                                    <label><input type="checkbox" name="accompany" value="1" /> 父</label>　
	                                    <label><input type="checkbox" name="accompany" value="2" /> 母</label>　
	                                    <label><input type="checkbox" name="accompany" value="3" /> 外婆/外公</label>　
	                                    <label><input type="checkbox" name="accompany" value="4" /> 奶奶/爺爺</label>　
	                                    <label><input type="checkbox" name="accompany" value="5" /> </label><input type="text" id="accompany01t01" value="" />
	                                </li>
	                                <li>　◎ 授課後的主要教導者：　
	                                    <label><input type="checkbox" name="teach" value="1" /> 父</label>　
	                                    <label><input type="checkbox" name="teach" value="2" /> 母</label>　
	                                    <label><input type="checkbox" name="teach" value="3" /> 外婆/外公</label>　
	                                    <label><input type="checkbox" name="teach" value="4" /> 奶奶/爺爺</label>　
	                                    <label><input type="checkbox" name="teach" value="5" /> </label><input type="text" id="teach01t01" value="" />
	                                </li>
	                                <li>　◎ 案家主訴問題：
	                                    <label><input type="radio" name="caseQ" value="1" /> 無</label>　
	                                    <label><input type="radio" name="caseQ" value="2" /> 有 </label><input type="text" id="caseQ01t01" value="" />
	                                </li>
	                                <li>　◎ 其他：<br /><textarea class="item7Textarea" id="OtherRemark1" ></textarea></li>
	                            </ul>
	                        </td>
	                    </tr>
	                </table>
			        <p>二、個案（請勾選表示個案目前的狀況）</p>
	                <table class="table2Contact" width="780" border="0">
	                    <tr>
	                        <th width="156">學習態度</th>
	                        <th width="156">情緒氣質</th>
	                        <th width="156">活動量</th>
	                        <th width="156">専注力</th>
	                        <th width="156">堅持度</th>
	                    </tr>
	                    <tr>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case4" value="1" /> 主動參與</label><br />
	                            <label><input type="checkbox" name="case4" value="2" /> 引導後可遵從</label><br />
	                            <label><input type="checkbox" name="case4" value="3" /> 要求後可遵從</label><br />
	                            <label><input type="checkbox" name="case4" value="4" /> 遵從性差</label><br />
	                            <label><input type="checkbox" name="case4" value="5" /> 無法觀察</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case5" value="1" /> 情緒穩定</label><br />
	                            <label><input type="checkbox" name="case5" value="2" /> 情緒易怒</label><br />
	                            <label><input type="checkbox" name="case5" value="3" /> 情緒易哭鬧</label><br />
	                            <label><input type="checkbox" name="case5" value="4" /> 內向害羞</label><br />
	                            <label><input type="checkbox" name="case5" value="5" /> 外向活潑</label><br />
	                            <label><input type="checkbox" name="case5" value="6" /> 無法觀察</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case6" value="1" /> 好動(需動態遊戲)</label><br />
	                            <label><input type="checkbox" name="case6" value="2" /> 懶散(對遊戲無興趣)</label><br />
	                            <label><input type="checkbox" name="case6" value="3" /> 適中</label><br />
	                            <label><input type="checkbox" name="case6" value="4" /> 無法觀察</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case7" value="1" /> 主動、專注力佳</label><br />
	                            <label><input type="checkbox" name="case7" value="2" /> 引導後可專注</label><br />
	                            <label><input type="checkbox" name="case7" value="3" /> 選擇性專注</label><br />
	                            <label><input type="checkbox" name="case7" value="4" /> 易分心</label><br />
	                            <label><input type="checkbox" name="case7" value="5" /> 無法專注</label><br />
	                            <label><input type="checkbox" name="case7" value="6" /> 無法觀察</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case8" value="1" /> 想法不易改變</label><br />
	                            <label><input type="checkbox" name="case8" value="2" /> 可被轉移或說服</label><br />
	                            <label><input type="checkbox" name="case8" value="3" /> 無法觀察</label>
	                        </td>
	                    </tr>
	                    <tr>
	                        <th>溝通行為</th>
	                        <th>聽覺技巧</th>
	                        <th>認知</th>
	                        <th>語言表達</th>
	                        <th>動作</th>
	                    </tr>
	                    <tr>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case9" value="1" /> 無法使用任何語言、手勢、聲音、表情與他人溝通</label><br />
	                            <label><input type="checkbox" name="case9" value="2" /> 會使用一些手勢、動作、聲音、表情但沒有語言</label><br />
	                            <label><input type="checkbox" name="case9" value="3" /> 使用有限的詞彙</label><br />
	                            <label><input type="checkbox" name="case9" value="4" /> 使用片語</label><br />
	                            <label><input type="checkbox" name="case9" value="5" /> 使用不完整的句子</label><br />
	                            <label><input type="checkbox" name="case9" value="6" /> 可使用句子</label><br />
	                            <label><input type="checkbox" name="case9" value="7" /> 可說故事</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case10" value="1" /> 尚無明確的察覺行為及反應</label><br />
	                            <label><input type="checkbox" name="case10" value="2" /> 可察覺鼓聲、玩具聲響等環境聲音</label><br />
	                            <label><input type="checkbox" name="case10" value="3" /> 可察覺語音</label><br />
	                            <label><input type="checkbox" name="case10" value="4" /> 可辨識熟悉的句子</label><br />
	                            <label><input type="checkbox" name="case10" value="5" /> 可辨識不同字數的詞彙</label><br />
	                            <label><input type="checkbox" name="case10" value="6" /> 可回想訊息中的關鍵詞</label><br />
	                            <label><input type="checkbox" name="case10" value="7" /> 可針對句子回答問題</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case11" value="1" /> 能轉動眼睛尋找物件或注視平面的圖片</label><br />
	                            <label><input type="checkbox" name="case11" value="2" /> 可分辨顏色及形狀。可用動作來表示有與無</label><br />
	                            <label><input type="checkbox" name="case11" value="3" /> 具有簡單的比較序列概念</label><br />
	                            <label><input type="checkbox" name="case11" value="4" /> 能有簡單形狀或顏色的聯想</label><br />
	                            <label><input type="checkbox" name="case11" value="5" /> 能正確排列順序圖卡</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case12" value="1" /> 僅能模仿一些口腔動作，並無什麼聲音</label><br />
	                            <label><input type="checkbox" name="case12" value="2" /> 有一些自發性的呀呀聲或能模仿一些聲音</label><br />
	                            <label><input type="checkbox" name="case12" value="3" /> 能模仿發出聲音：</label><br />　<input type="text" id="case12t01" size="15" /><br />
	                            <label><input type="checkbox" name="case12" value="4" /> 能仿說詞彙</label><br />
	                            <label><input type="checkbox" name="case12" value="5" /> 能仿說片語或句子</label><br />
	                            <label><input type="checkbox" name="case12" value="6" /> 能主動說詞彙</label><br />
	                            <label><input type="checkbox" name="case12" value="7" /> 能主動說句子</label>
	                        </td>
	                        <td valign="top">
	                            <label><input type="checkbox" name="case13" value="1" /> 可坐著或趴著玩玩具</label><br />
	                            <label><input type="checkbox" name="case13" value="2" /> 可站至少5分鐘或扶著走</label><br />
	                            <label><input type="checkbox" name="case13" value="3" /> 可獨自行走</label><br />
	                            <label><input type="checkbox" name="case13" value="4" /> 會雙腳跳</label><br />
	                            <label><input type="checkbox" name="case13" value="5" /> 會跑歩</label><br />
	                            <label><input type="checkbox" name="case13" value="6" /> 會抓握東西且可簡單把玩</label><br />
	                            <label><input type="checkbox" name="case13" value="7" /> 可翻厚頁書或疊積木、開盒蓋</label><br />
	                            <label><input type="checkbox" name="case13" value="8" /> 能操弄各式玩具</label><br />
	                            <label><input type="checkbox" name="case13" value="9" /> 會使用安全剪刀或疊高1個積木</label><br />
	                            <label><input type="checkbox" name="case13" value="10" /> 會握筆畫圖或寫字</label>
	                        </td>
	                    </tr>
	                    <tr>
	                        <td colspan="5">備註說明：<br />
	                            <ul>
	                                <li>　◎ 個案當天的狀況：<br />
	                                    助聽輔具的配戴：<label><input type="radio" name="wear" value="" /> 有配戴左/右耳</label>　
	                                    <label><input type="radio" name="wear" value="" /> 無配戴助聽輔具</label><br />
	                                    個案精神及配合狀況：
	                                    <label><input type="radio" name="mind" value="1" /> 良好</label>　
	                                    <label><input type="radio" name="mind" value="2" /> 普通</label>　
	                                    <label><input type="radio" name="mind" value="3" /> 情緒不佳</label>　
	                                    <label><input type="radio" name="mind" value="4" /> 精神不佳</label>　
	                                    <label><input type="radio" name="mind" value="5" /> 其它</label> <input type="text" id="mind01t01" value="" />
	                                </li>
	                                <li>　◎ 個案溝通意願及企圖：
	                                    <label><input type="radio" name="connectwish" value="1" /> 極高</label>　
	                                    <label><input type="radio" name="connectwish" value="2" /> 普通</label>　
	                                    <label><input type="radio" name="connectwish" value="3" /> 被動</label>　
	                                    <label><input type="radio" name="connectwish" value="4" /> 抗拒</label>
	                                </li>
	                                <li>　◎ 個案學習意願及企圖：
	                                    <label><input type="radio" name="studywish" value="1" /> 極高</label>　
	                                    <label><input type="radio" name="studywish" value="2" /> 普通</label>　
	                                    <label><input type="radio" name="studywish" value="3" /> 被動</label>　
	                                    <label><input type="radio" name="studywish" value="4" /> 抗拒</label>
	                                </li>
	                                <li>　◎ 檢附相關評估/教學資料：
	                                    <label><input type="radio" name="related" value="1" /> 無其它資料</label>　
	                                    <label><input type="radio" name="related" value="2" /> 有</label> <input type="text" id="related01t01" value="" />
	                                </li>
	                                <li>　◎ 其他：<br /><textarea class="item7Textarea" id="OtherRemark2" ></textarea></li>
	                            </ul>
	                        </td>
	                    </tr>
	                </table>
			        <p>三、安置（請勾選表示個案目前的狀況）</p>
	                <table class="table2Contact" width="780" border="0">
	                    <tr>
	                        <th width="390">目前已有的療育資源</th>
	                        <th width="390">聽語復健療育資源</th>
	                    </tr>
	                    <tr>
	                        <td>
	                            <label><input type="checkbox" name="case14" value="1" /> 目前都沒有任何的療育資源</label><br />
	                            <label><input type="checkbox" name="case14" value="2" /> 有在啟聰、聲暉等相關療育機構</label><br />　　
	                            <label><input type="radio" name="trusteeship" value="1" /> 時段</label>　
	                            <label><input type="radio" name="trusteeship" value="2" /> 半日托</label>　
	                            <label><input type="radio" name="trusteeship" value="3" /> 全日</label><br />
	                            <label><input type="checkbox" name="case14" value="3" /> 有在<input type="text" id="case14t01" size="10" value="" />醫院進行相關療育</label><br />　　
	                            <label><input type="checkbox" name="proceed" value="1" /> 職能</label>　
	                            <label><input type="checkbox" name="proceed" value="2" /> 物理</label>　
	                            <label><input type="checkbox" name="proceed" value="3" /> 語言</label>　
	                            <label><input type="checkbox" name="proceed" value="4" /> 其它</label> <input type="text" id="proceedt01" size="10" value="" /><br />
	                            <label><input type="checkbox" name="case14" value="4" /> 有在幼托園所：</label>
	                            <label><input type="radio" name="preschools" value="1" /> 全日</label>　
	                            <label><input type="radio" name="preschools" value="2" /> 半日</label>
	                        </td>
	                        <td>
	                            <label><input type="checkbox" name="case15" value="1" /> 個案需求性：</label>
	                            <label><input type="radio" name="Rehabilitation1" value="1" /> 高</label>　
	                            <label><input type="radio" name="Rehabilitation1" value="2" /> 中</label>　
	                            <label><input type="radio" name="Rehabilitation1" value="3" /> 低</label><br />
	                            <label><input type="checkbox" name="case15" value="2" /> 案家的積極性：</label>
	                            <label><input type="radio" name="Rehabilitation2" value="1" /> 高</label>　
	                            <label><input type="radio" name="Rehabilitation2" value="2" /> 中</label>　
	                            <label><input type="radio" name="Rehabilitation2" value="3" /> 低</label><br />
	                            <label><input type="checkbox" name="case15" value="3" /> 本會教學資源的安置建議</label><br />　　
	                            <label><input type="radio" name="Rehabilitation3" value="1" /> 安排排課：希望時段</label> <input type="text" id="Rehabilitation3t01" value="" /><br />　　
	                            <label><input type="radio" name="Rehabilitation3" value="2" /> 暫不排課：原因</label> <input type="text" id="Rehabilitation3t02" value="" /><br />　　
	                            <label><input type="radio" name="Rehabilitation3" value="3" /> 須考量的因素有</label> <input type="text" id="Rehabilitation3t03" value="" /><br />
	                        </td>
	                    </tr>
	                    <tr>
	                        <td colspan="2">排課後的課程建議：<br />
	                            <ul>
	                                <li>1. 案家：<br />
	                                    <label><input type="checkbox" name="case16" value="1" /> 指導家長語言輸入技巧，並養成隨時隨地輸入的習慣。</label><br />
	                                    <label><input type="checkbox" name="case16" value="2" /> 鼓勵案家參加相關講座課程等。</label><br />
	                                    <label><input type="checkbox" name="case16" value="3" /> 持續關切案家對個案的接受度，並適時鼓勵家長。</label><br />
	                                    <label><input type="checkbox" name="case16" value="4" /> 多給予案父母心理的支持。</label><br />
	                                    <label><input type="checkbox" name="case16" value="5" /> 持續輔導案家對兒童發展或聽損教育知能等等的認知。</label><br />
	                                    <label><input type="checkbox" name="case16" value="6" /> 其它：</label><br />
	                                    <textarea class="item7Textarea" id="OtherRemark3" ></textarea>
	                                </li>
	                                <li>2. 個案的學習：<br />
	                                    <label><input type="checkbox" name="case17" value="1" /> 要求並輔導個案助聽輔具的配戴。</label><br />
	                                    <label><input type="checkbox" name="case17" value="2" /> 持續注意及追蹤個案的整體發展。</label><br />
	                                    <label><input type="checkbox" name="case17" value="3" /> 本會第一堂課應安排客觀性的評估。</label><br />
	                                    <label><input type="checkbox" name="case17" value="4" /> 其它：</label><br />
	                                    <textarea class="item7Textarea" id="OtherRemark4" ></textarea>
	                                </li>
	                            </ul>
	                        </td>
	                    </tr>
	                </table>
			        <p align="right">諮詢教師 <span><input type="text" id="teacherName" value="" class="searchStaff" readonly="readonly"/><span id="teacherID" class="hideClassSpan"></span></span>　　填表日期 <span><input id="TeacherDate" class="date" type="text" size="10" /></span></p>
			        <p class="btnP">
			            <button class="btnSave" type="button" onclick="goNext(7)">下一頁</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="saveTeachhistory()">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item8Content">
	                <table class="table2Contact" width="780" border="0">
	                <thead>
	                    <tr>
	                        <th width="100">紀錄日期</th>
	                        <th width="80">身高</th>
	                        <th width="80">體重</th>
	                        <th width="370">備註</th>
	                        <th width="110">功能</th>
	                    </tr>
	                    </thead>
	                    <tbody>
		                </tbody>
	                </table>
	                <p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData2()">新 增</button></p>
		            <div id="insertDataDiv2" style="display:none">
		                <table class="tableList" width="780" border="0">
		                    <thead>
		                        <tr>
			                        <th width="100">紀錄日期</th>
	                                <th width="80">身高</th>
	                                <th width="80">體重</th>
	                                <th width="370">備註</th>
			                        <th width="110">功能</th>
		                        </tr>
		                    </thead>
		                    <tbody>
		                        <tr >
		                            <td align="center" height="30"><input id="RecordDate" class="date" type="text" value="" size="10" /></td>
	                                <td align="center"><input id="RecordHeight" type="text" value="" size="10"/></td>
	                                <td align="center"><input id="RecordWeight" type="text" value="" size="10"/></td>
	                                <td align="center"><input id="RecordRemark" type="text" value="" size="50" /></td>
		                            <td><button class="btnView" type="button" onclick="setHWResourceData();">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert(2)">取 消</button></td>
		                        </tr>
		                     </tbody>
		                </table>
		            </div>
			        <!-- <p class="btnP">
			            <button class="btnSave" type="button" onclick="">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button" onclick="saveBodyRecord()">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button> -->
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
