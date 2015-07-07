<%@ Page Language="C#" AutoEventWireup="true" CodeFile="financial_aid.aspx.cs" Inherits="financial_aid" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 財務補助申請 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/financial_aid.css" />
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
                <asp:ScriptReference Path="~/js/financial_aid.js" />
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
			<div id="mainClass">個案管理&gt; 財務補助申請</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./financial_aid.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table id="searchTable" width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>補助項目 <select id="gosrhsubsidyitem"><option value="0">請選擇</option>
			                    <option value="1">助聽器補助</option><option value="2">人工電子耳補助</option>
			                    <option value="3">學費補助</option><option value="4">其他</option>
			                    </select>
			                </td>
			                <td>填表日期 <input id="gosrhfillInDatestart" class="date" type="text" value="" size="10" />～<input id="gosrhfillInDateend" class="date" type="text" value="" size="10" /></td>
			                <td>&nbsp;</td>
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
			                    <th width="120">填表日期</th>
			                    <th width="120">學生姓名</th>
			                    <th width="120">身份證字號</th>
			                    <th width="120">補助項目</th>
			                    <th width="80">補助金額</th>
			                    <th width="160">補助期程</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>102.01.03</td>
			                    <td>王小明</td>
			                    <td>A123456789</td>
			                    <td>助聽器補助</td>
			                    <td>1100</td>
			                    <td>102.01.03-102.04.03</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>101.11.16</td>
			                    <td>陳曉雯</td>
			                    <td>F223456789</td>
			                    <td>學費補助</td>
			                    <td>1100</td>
			                    <td>102.01.03-102.04.03</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p id="sUnit" align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			    <p align="right"><span class="startMark">*</span>填表日期 <input id="fillInDate" class="date" type="text" value="" size="10" /></p>
			    <table class="tableText" width="780" border="0">
			     <tr>
			            <th width="150">服務使用者編號</th>
			            <td><input id="studentID" type="text" value="" readonly="readonly"/><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>學生姓名</th>
			            <td id="studentName"></td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td id="studentbirthday"></td>
		            </tr>
		            <tr>
			            <th>身份證字號(學生)</th>
			            <td id="studentTWID"></td>
			        </tr>
		            <tr>
		                <th>家長姓名</th>
		                <td><input id="ParentName" type="text" value=""/></td>
			        </tr>
			        <tr>
			            <th>電　　話</th>
			            <td><input id="ParentTel" type="text" value=""/></td>
			        </tr>
			        <tr>
			            <th>手　　機</th>
			            <td><input id="ParentPhone" type="text" value=""/></td>
			        </tr>
			        <tr>
			            <th>通訊地址</th>
			            <td><input id="addressZip" type="text" maxlength="5" value=""  size="5"/> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" value="" size="50"/></td>
			        </tr>
			        <tr>
			            <th>補助項目</th>
			            <td>
			                <label><input type="radio" name="subsidyitem" value="1" /> 助聽器補助</label>　
			                <label><input type="radio" name="subsidyitem" value="2" /> 人工電子耳補助</label>　
			                <label><input type="radio" name="subsidyitem" value="3" /> 學費補助</label>　
			                <label><input type="radio" name="subsidyitem" value="4" /> 其他</label> <input id="subsidytext" type="text" value="" />
			            </td>
			        </tr>
			        <tr>
			            <th>資料繳交</th>
			            <td>
			                <label><input type="checkbox" name="payitem" value="1" /> 身心障礙手冊影本</label>（障礙類別 <input id="manualCategory" type="text" value="" size="15" /> 等級 <input id="manualGrade" type="text" value="" size="15" />）<br />
			                <label><input type="checkbox" name="payitem" value="2" /> 學生證明</label><br />
			                <label><input type="checkbox" name="payitem" value="3" /> 中低收入戶證明</label><br />
			                <label><input type="checkbox" name="payitem" value="4" /> 聽力圖（最近一次檢查）</label><br />
			                <label><input type="checkbox" name="payitem" value="5" /> 低收入戶卡影本（雙面）</label><br />
			                <label><input type="checkbox" name="payitem" value="6" /> 政府人工電子耳補助證明</label><br />
			                <label><input type="checkbox" name="payitem" value="7" /> 最近三個月戶籍謄本</label><br />
			                <label><input type="checkbox" name="payitem" value="8" /> 存摺影本</label><br />
			                <label><input type="checkbox" name="payitem" value="9" /> 全戶所得及不動產清冊</label><br />
			                <label><input type="checkbox" name="payitem" value="10" /> 家長會幹部名冊</label>
			            </td>
			        </tr>
			        <tr>
			            <th>其他說明</th>
			            <td><input id="othertext" type="text" value="" size="80" /></td>
			        </tr>
			        <tr>
			            <th>補助金額</th>
			            <td><input type="text" id="subsidymoney" value="" /></td>
			        </tr>
			        <tr>
			            <th>補助期程</th>
			            <td>自 <input type="text" id="subsidydate1" class="date" value="" size="10" /> 至 <input type="text" id="subsidydate2" class="date" value="" size="10" /> 止</td>
			        </tr>
			    </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="SaveData(0);">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="SaveData(1);">存 檔</button>
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
