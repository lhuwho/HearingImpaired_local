<%@ Page Language="C#" AutoEventWireup="true" CodeFile="resource_card.aspx.cs" Inherits="resource_card" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>個案管理 - 資源卡 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/resource_card.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/resource_card.js"></script>
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
			<div id="mainClass">個案管理&gt; 資源卡</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./resource_card.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">機構名稱 <input id="gosrhresourceName" type="text" value="" /></td>
			                <td width="260">縣　　市 <select id="gosrhaddressCity" class="zoneCity" ></select></td>
			                <td width="260">類　　別 <select id="gosrhresourceType"><option value="0">請選擇類別</option><option value="1">復健與醫療</option><option value="2">經濟</option><option value="3">福利服務</option><option value="4">教育</option><option value="5">其他</option></select></td>
                        </tr>
                        <tr>
			                <td>資源項目 <input id="gosrhresourceItem" type="text" value="" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
                        </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="Search();">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="240">機構名稱</th>
			                    <th width="150">縣市</th>
			                    <th width="150">類別</th>
			                    <th width="180">資源項目</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>ABC醫院</td>
			                    <td>台北市</td>
			                    <td>醫院</td>
			                    <td>早療</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>1123診療所</td>
			                    <td>新北市</td>
			                    <td>診療所</td>
			                    <td>早療</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p id="sUnit" align="right" style="background-color:#FFDF71;padding:0 10px;">&nbsp</p>
			    <table width="780" border="0">
			        <tr>
			            <td height="50" width="260">填表日期 <input id="fillInDate" class="date" type="text" value="" size="10" /><span class="startMark">*</span></td>
			            <td width="260">更新日期 <input id="upDate" class="date" type="text" value="" size="10" readonly="readonly"/></td>
			            <td width="260">最後更新人 <input id="upBy" type="text" value="" readonly="readonly"/></td>
			        </tr>
			    </table>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">機構名稱</th>
			            <td><input id="resourceName" type="text" value="" /><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>資源項目</th>
			            <td><input id="resourceItem" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>資源類別</th>
			            <td><select id="resourceType"><option value="0">請選擇類別</option><option value="1">復健與醫療</option><option value="2">經濟</option><option value="3">福利服務</option><option value="4">教育</option><option value="5">其他</option></select></td>
			        </tr>
			        <tr>
			            <th>機構地址</th>
			            <td><input id="addressZip" type="text" maxlength="5" value=""  size="5"/> <select id="addressCity" class="zoneCity"></select> <input id="address" type="text" value="" size="50"/></td>
			        </tr>
			        <tr>
			            <th>機構聯絡人</th>
			            <td>
			                <table class="tableContact" width="610" border="0">
		                        <tr>
		                            <th height="40">姓名</th>
		                            <th>電話</th>
		                            <th>傳真</th>
		                            <th>E-mail</th>
		                        </tr>
		                        <tr>
		                            <td height="35"><input id="contact_1" type="text" value="" size="10" /></td>
		                            <td><input id="contactPhone_1" type="text" value="" size="15" /></td>
		                            <td><input id="contactFax_1" type="text" value="" size="15" /></td>
		                            <td><input id="contactEmail_1" type="text" value="" /></td>
		                        </tr>
		                        <tr>
		                            <td height="35"><input id="contact_2"  type="text" value="" size="10" /></td>
		                            <td><input id="contactPhone_2" type="text" value="" size="15" /></td>
		                            <td><input id="contactFax_2" type="text" value="" size="15" /></td>
		                            <td><input id="contactEmail_2" type="text" value="" /></td>
		                        </tr>
		                        <tr>
		                            <td height="35"><input id="contact_3" type="text" value="" size="10" /></td>
		                            <td><input id="contactPhone_3" type="text" value="" size="15" /></td>
		                            <td><input id="contactFax_3" type="text" value="" size="15" /></td>
		                            <td><input id="contactEmail_3" type="text" value="" /></td>
		                        </tr>
		                    </table>
			            </td>
			        </tr>
			        
			        <tr>
			            <th>是否為轉介單位</th>
			            <td><label><input type="radio" name="referral" value="1" /> 是</label>　　
			                <label><input type="radio" name="referral" value="2" /> 否</label></td>
			        </tr>
		            <tr>
		                <th colspan="2" class="tctitle">服務內容</th>
		            </tr>
		            <tr>
		                <th>服務對象</th>
		                <td><input id="sObject" type="text" value="" /></td>
		            </tr>
		            <tr>
		                <th>服務時間</th>
		                <td><input id="sTime" type="text" value="" /></td>
		            </tr>
		            <tr>
		                <th>服務項目</th>
		                <td><textarea id="sItem"></textarea></td>
		            </tr>
		            <tr>
		                <th>收費標準</th>
		                <td><textarea id="sExpense"></textarea></td>
		            </tr>
		            <tr>
		                <th>申請程序(需備齊資料)</th>
		                <td><textarea id="sProgram"></textarea></td>
		            </tr>
		            <tr>
		                <th>資源優勢或限制</th>
		                <td><textarea id="sInformation"></textarea></td>
		            </tr>
		            <tr>
		                <th>資源連結日期及對象</th>
		                <td><textarea id="sLink"></textarea></td>
		            </tr>
		            <tr>
		                <td colspan="2" align="right">填表人 <input id="fillInBy" type="text" value="" readonly="readonly"/></td>
		            </tr>
			    </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="SaveData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="SaveData(1)">存 檔</button>
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
