<%@ Page Language="C#" AutoEventWireup="true" CodeFile="volunteer_data.aspx.cs" Inherits="volunteer_data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 志工資料 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/volunteer_data.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/volunteer_data.js"></script>
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
			<div id="mainClass">個案管理&gt; 志工資料</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./volunteer_data.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">志工編號 <input id ="gosrhvID" type="text" value="" /></td>
			                <td width="260">志工姓名 <input id ="gosrhvName" type="text" value="" /></td>
			                <td width="260">志工性別 <select id="gosrhvSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                        </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" >查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="120">志工編號</th>
			                    <th width="120">志工姓名</th>
			                    <th width="120">手機</th>
			                    <th width="360">E-mail</th>
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
			    <p align="right" id ="sUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp</p>
			    <p align="right">填表日期 <input id="fillInDate" class="date" type="text" name="name" size="10" /></p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">志工編號</th>
			            <td id="volunteerId"></td>
			        </tr>
			        <tr>
			            <th>姓　　名</th>
			            <td><input id="volunteerName" type="text" value="" /><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>性　　別</th>
			            <td><label><input type="radio" name="vSex" value="1" /> 男</label>　　
			                <label><input type="radio" name="vSex" value="2" /> 女</label>
			            </td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td>
			                <input id="vBirthday" class = "date" type="text" value="" />
                        </td>
		            </tr>
		            <tr>
			            <th>現任工作/學校</th>
			            <td><input id="nowJob" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>電　　話</th>
			            <td>
			                <table width="600" border="0">
			                    <tr>
			                        <th>日</th>
			                        <td><input id="telDaytime" type="text" value="" /></td>
			                        <th>手機</th>
			                        <td><input id="volunteerPhone" type="text" value="" /></td>
			                    </tr>
			                    <tr>
			                        <th>夜</th>
			                        <td><input id="telNight" type="text" value="" /></td>
			                        <th>傳真</th>
			                        <td><input id="volunteerFax" type="text" value="" /></td>
			                    </tr>
			                </table>
			            </td>
			        </tr>
			        <tr>
		                <th>通訊地址</th>
		                <td><input id="addressZip" type="text" maxlength="5" size="5" value="" /> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" size="50" value="" /></td>
			        </tr>
			        <tr>
		                <th>E-mail</th>
		                <td><input id="vEmail" type="text" value="" size="50" /></td>
			        </tr>
			        <tr>
		                <th>個人專長</th>
		                <td><textarea id="Expertise"></textarea></td>
			        </tr>
			        <tr>
		                <th>服務經歷</th>
		                <td><textarea id="Experience"></textarea></td>
			        </tr>
			        <tr>
		                <th>對參與志願服務的期待</th>
		                <td><textarea id="VOLExpect"></textarea></td>
			        </tr>
			        <tr>
		                <th>請勾選服務項目</th>
		                <td>
		                    <ul>
		                        <li><I><U>服務時間</U></I><br />
		                            <label><input type="radio" name="servicedate" value="1" /> 固定時間之服務</label>　　
		                            <label><input type="radio" name="servicedate" value="2" /> 不定時之服務</label>
		                        </li>
		                        <li><I><U>服務內容</U></I><br />
		                            <label><input type="checkbox" name="servicecontent" value="1" /> 圖書館管理</label>　　　　
		                            <label><input type="checkbox" name="servicecontent" value="2" /> 說故事服務</label>　　　
		                            <label><input type="checkbox" name="servicecontent" value="3" /> 手語翻譯</label><br />
		                            <label><input type="checkbox" name="servicecontent" value="4" /> 行政支援</label>　　　　　
		                            <label><input type="checkbox" name="servicecontent" value="5" /> 教材整理製作</label>　　
		                            <label><input type="checkbox" name="servicecontent" value="6" /> 活動支援</label><br />
		                            <label><input type="checkbox" name="servicecontent" value="7" /> 活動褓母志工</label>　　　
		                            <label><input type="checkbox" name="servicecontent" value="8" /> 簡易修繕</label>　　　　
		                            <label><input type="checkbox" name="servicecontent" value="9" /> 攝影拍照</label><br />
		                            <label><input type="checkbox" name="servicecontent" value="10" /> 行政支援</label>　　　　　
		                            <label><input type="checkbox" name="servicecontent" value="11" /> 教材整理製作</label>　　
		                            <label><input type="checkbox" name="servicecontent" value="12" /> 團康設計</label><br />
		                            <label><input type="checkbox" name="servicecontent" value="13" /> 翻譯（中、英）</label>　　
		                            <label><input type="checkbox" name="servicecontent" value="14" /> 小孩陪讀</label>　　　　
		                            <label><input type="checkbox" name="servicecontent" value="15" /> 其他</label> <input id="otherService" type="text" value="" />
		                        </li>
		                    </ul>
		                </td>
			        </tr>
			        <tr>
		                <th>介紹來源</th>
		                <td><input id="vSource" type="text" value="" size="70" /></td>
			        </tr>
			    </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="SetVolunteerData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="SetVolunteerData(1)">存 檔</button>
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
