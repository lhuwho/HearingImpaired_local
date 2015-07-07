<%@ Page Language="C#" AutoEventWireup="true" CodeFile="aids_manage.aspx.cs" Inherits="aids_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>聽力管理 - 輔具管理 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/aids_manage.css" />
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
                <asp:ScriptReference Path="~/js/aids_manage.js" />
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
			<div id="mainClass">聽力管理&gt; 輔具管理</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./aids_manage.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">輔具編號 <input id="gosrhaID" type="text" value="" /></td>
			                <td width="260">輔具類別 <select id="gosrhassistmanage"><option value="0">請選擇</option><option value="1">助聽器</option><option value="2">電子耳及配件</option><option value="3">調頻系統</option></select></td>
			                <td width="260">輔具廠牌 <select id="gosrhbrand"><option value="0">請選擇輔具類別</option></select></td>
			            </tr>
			            <tr>
			            	<td>輔具型號 <input id="gosrhmodel" type="text" value="" /></td>
			                <td>輔具序號 <input id="gosrhaNo" type="text" value="" /></td>
			                <td>輔具來源 <input id="gosrhaSource" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search();">顯 示</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="110">輔具編號</th>
			                    <th width="110">輔具類別</th>
			                    <th width="102">輔具廠牌</th>
			                    <th width="102">輔具型號</th>
			                    <th width="80">輔具序號</th>
			                    <th width="120">輔具來源</th>
			                    <th width="85">狀態</th>
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
                   <p align="right" id="Unit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
                   <table class="tableText" width="780" border="0">
                   		<tr>
			                <th width="150">狀態</th>
			                <td><select id="aStatu"><option value="0">請選擇</option><option value="1">借出中</option><option value="2">維修/保養中</option><option value="3">館內保存</option></select><span class="startMark">*</span></td>
			            </tr>
			            <tr>
			                <th>輔具編號</th>
			                <td><input id="aID" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <th>輔具類別</th>
			                <td><select id="assistmanage"><option value="0">請選擇</option><option value="1">助聽器</option><option value="2">電子耳及配件</option><option value="3">調頻系統</option></select><span class="startMark">*</span></td>
			            </tr>
			            <tr>
			            	<th>廠牌</th>
			                <td><select id="brand"><option value="0">請選擇輔具類別</option></select><span class="startMark">*</span></td>
			            </tr>
			            <tr>
			            	<th>型號</th>
			                <td><input id="model" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <th>輔具序號</th>
			                <td><input id="aNo" type="text" value="" /></td>
                        </tr>
			            <tr>			                
			                <th>輔具來源</th>
			                <td><input id="aSource" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <th>建立日期</th>
			                <td><input id="fillInDate" class="date" type="text" value="" /></td>
			            </tr>
                   </table>
                   <div id="otherTable">
                   <p class="cP">一、出借記錄</p>
                   <table id="LendingTable" class="tableContact" width="780" border="0">
			        <thead>
			            <tr>
			                <th width="85">出借日期</th>
			                <th width="200">借用人</th>
			                <th width="87">預定歸還日</th>
			                <th width="87">歸還日期</th>
			                <th width="200">備註</th>
			                <th width="110">功能</th>
			            </tr>
			          </thead>
			          <tbody>
			          </tbody>  
			       </table>
                   <p align="center" id="setLending"><button class="btnAdd" type="button" >新 增</button></p>
			       <div id="insertLendingDiv" style="display:none">
                       <table class="tableContact" width="780" border="0">
                       <caption>新增出借記錄</caption>
		                  <tr>
			                <th width="85"><span class="startMark">*</span>出借日期</th>
			                <th width="200"><span class="startMark">*</span>借用人</th>
			                <th width="87"><span class="startMark">*</span>預定歸還日</th>
			                <th width="87">歸還日期</th>
			                <th width="200">備註</th>
			                <th width="110">功能</th>
			              </tr>
			              <tr>
			                <td height="30"><input id="lendDate" class="date" type="text" value="" size="10" /></td>
			                <td><input id="lendPeople" type="text" value="" /><span id="lendPeopleID" class="hideClassSpan"></span></td>
			                <td><input id="returnDate" type="text" class="date" value="" size="10" /></td>
			                <td><input id="returnDate2" type="text" class="date" value="" size="10" /></td>
			                <td><input id="remark" type="text" value="" /></td>
			                <td><button class="btnView" type="button" onclick="InsertLending()">儲 存</button> <button class="btnView" type="button" onclick="cancelInsertLending()">取 消</button></td>
			             </tr>
			           </table>
			        </div> 
                   <p class="cP">二、維修/保養記錄</p>  
                   <table id="ServiceTable" class="tableContact" width="780" border="0">
		               <thead>
		                    <tr>
		                        <th width="85">日期</th>
		                        <th width="175">原因</th>
		                        <th width="175">維修廠商</th>
		                        <th width="85">送回日期</th>
		                        <th width="150">備註</th>
		                        <th width="110">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                </tbody>
			       </table>
			       
                   <p align="center" id="setLending2"><button id="setService" class="btnAdd" type="button" >新 增</button></p>
			       <div id="insertServiceDiv" style="display:none">
                       <table class="tableContact" width="780" border="0">
                       <caption>新增維修/保養記錄</caption>
                         <thead>
		                    <tr>
		                        <th width="85">日期</th>
		                        <th width="175">原因</th>
		                        <th width="180">維修廠商</th>
		                        <th width="85">送回日期</th>
		                        <th width="160">備註</th>
		                        <th width="110">功能</th>
		                    </tr>
		                </thead>
			             <tr>
			               <td height="30"><input class="date" id="serviceDate" type="text" value="" size="10" /></td>
			               <td><input id="serviceItem" type="text" value="" /></td>
			               <td><input id="serviceFirm" type="text" value="" /></td>
			               <td><input id="serviceFirmDate" class="date" type="text" value="" size="10" /></td>
			               <td><input id="sRemark" type="text" value="" /></td>
			               <td><button class="btnView" type="button" onclick="InsertService()">儲 存</button> <button class="btnView" type="button" onclick="cancelInsertService()">取 消</button></td>
			             </tr> 
			            </table>
			        </div> 
			       </div>
                   <p class="cP">備註：</p> 
                   <textarea cols="112" rows="3"></textarea>                           
                   <p class="btnP">
                       <button class="btnSave" type="button" onclick="SaveData(0)">儲 存</button>
                       <button class="btnUpdate" type="button">更 新</button>
                       <button class="btnSaveUdapteData" type="button" onclick="SaveData(1)">存 檔</button>
                       <button class="btnCancel" type="button">取 消</button>
                   </p></div>
                </div>
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>