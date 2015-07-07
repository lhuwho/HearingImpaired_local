<%@ Page Language="C#" AutoEventWireup="true" CodeFile="stationery_manage.aspx.cs" Inherits="stationery_manage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>其他管理 - 文具管理 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/stationery_manage.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/stationery_manage.js?v=1"></script>
	
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
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
			<div id="mainClass">其他管理&gt; 文具管理</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">文具基本資料</div>
			    <div id="btnIndex" class="menuTabs2">進貨管理</div>
			    <div id="btnIndex2" class="menuTabs2">領用管理</div>
			    <div id="btnIndex3" class="menuTabs2">報廢管理</div>
			    <div id="btnIndex4" class="menuTabs2">退貨管理</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">產品編號 <input id="gosrhstationeryID" type="text" value="" /></td>
			                <td width="260">品　　名 <input id="gosrhstationeryName" type="text" value="" /></td>
			                <td width="260">類　　別 <select id="gosrhstationeryType" ><option value="0">請選擇</option><option value="1">消耗品</option><option value="2">非消耗品</option><option value="3">管制品</option></select></td>
                        </tr>
			            <tr>
			                <td>安全數量 <input id="gosrhsafeQuantityStart" type="text" value="" size="5" /> ～ <input id="gosrhsafeQuantityEnd" type="text" value="" size="5" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(1)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="70">產品<br />編號</th>
			                    <th width="40">部別<br />名稱</th>
		                        <th width="130">品名</th>
		                        <th width="55">最小<br />單位</th>
		                        <th width="60">庫存量</th>
		                        <th width="55">安全<br />數量</th>
		                        <th width="60">最新<br />單價</th>
		                        <th width="60">類別</th>
		                        <th width="200">備註</th>
		                        <th width="50">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
                </div>
                <div id="mainPagination" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="showInsert(1)">新 增</button></p>
		        <div id="insertDataDiv" class="insertDataDiv">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
	                            <th width="170">品名</th>
	                            <th width="70">最小<br />單位</th>
	                            <th width="70">安全<br />數量</th>
	                            <th width="110">類別</th>
	                            <th width="300">備註</th>
	                            <th width="60">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
		                        <td><input id="stationeryName" type="text" value=""/></td>
		                        <td><input id="stationeryUnit" type="text" value="" size="5" /></td>
		                        <td><input id="safeQuantity" type="text" value="" size="5" /></td>
		                        <td><select id="stationeryType"><option value="0">請選擇</option><option value="1">消耗品</option><option value="2">非消耗品</option><option value="3">管制品</option></select></td>
		                        <td><textarea id="remark" rows="1" cols="35" ></textarea></td>
		                        <td><div class="UD"><button class="btnView" type="button" onclick="saveInsert()">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                        </td>
		                    </tr>
                        </tbody>
		            </table>
		        </div>
			</div>
			
			<div id="mainContentIndex">
			    <div id="mainIndexForm">
			        <table width="780" border="0" id="searchTable2">
			            <tr>
			                <td width="260">進貨單號 <input id="gosrhpurchaseID" type="text" value="" /></td>
			                <td width="260">廠商名稱 <input id="gosrhfirmName" type="text" value="" /></td>
			                <td width="260">進貨日期 <input id="gosrhpurchaseDateStart" class="date" type="text" value="" size="10" /> ～ <input id="gosrhpurchaseDateEnd" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>產品編號 <input id="gosrhstationeryID" type="text" value="" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(2)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="80">進貨<br />單號</th>
			                    <th width="100">廠商<br />名稱</th>
			                    <th width="90">廠商<br />電話</th>
			                    <th width="60">進貨<br />日期</th>
			                    <th width="65">產品<br />編號</th>
			                    <th width="40">部別<br />名稱</th>
		                        <th width="100">品名</th>
		                        <th width="40">最小<br />單位</th>
		                        <th width="45">數量</th>
		                        <th width="55">單價</th>
		                        <th width="50">總價</th>
		                        <th width="55">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
                </div>
                <div id="mainPaginationIndex" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="showInsert(2)">新 增</button></p>
		        <div id="insertDataDiv2" class="insertDataDiv">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
			                    <th width="110">廠商<br />名稱</th>
			                    <th width="110">廠商<br />電話</th>
			                    <th width="70">進貨<br />日期</th>
			                    <th width="80">產品<br />編號</th>
		                        <th width="110">品名</th>
		                        <th width="50">最小<br />單位</th>
		                        <th width="60">數量</th>
		                        <th width="65">單價</th>
		                        <th width="65">總價</th>
		                        <th width="60">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
			                    <td><input id="firmName" type="text" value="" size="12" /></td>
			                    <td><input id="firmTel" type="text" value="" size="12" /></td>
			                    <td><input id="purchaseDate" class="date" type="text" value="" size="10" /></td>
			                    <td><input id="stationeryID" type="text" value="" size="8" readonly="readonly" /></td>
			                    <td id="stationeryName2"></td>
			                    <td id="stationeryUnit2"></td>
			                    <td><input id="stationeryQuantity" type="text" value="" size="5" /></td>
			                    <td><input id="stationeryPrice" type="text" value="" size="6" /></td>
			                    <td id="stationeryTotal"></td>
		                        <td><div class="UD"><button class="btnView" type="button" onclick="saveInsert2()">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                        </td>
		                    </tr>
                        </tbody>
		            </table>
		        </div>
			</div>
			
			<div id="mainContentIndex2">
			    <div id="mainIndexForm2">
			        <table width="780" border="0" id="searchTable3">
			            <tr>
			                <td width="260">領用單號 <input id="gosrhreceiveID" type="text" value="" /></td>
			                <td width="260">領用日期 <input id="gosrhreceiveDateStart" class="date" type="text" value="" size="10" /> ～ <input id="gosrhreceiveDateEnd" class="date" type="text" value="" size="10" /></td>
			                <td width="260">領 用 人 <input id="gosrhreceiveBy" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>產品編號 <input id="gosrhrstationeryID" type="text" value="" /></td>
			                <td>品　　名 <input id="gosrhrstationeryName" type="text" value="" /></td>
			                <td>&nbsp;</td>
                        </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(3)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList2" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">領用<br />單號</th>
			                    <th width="70">領用<br />日期</th>
			                    <th width="80">領用人</th>
			                    <th width="60">產品<br />編號</th>
			                    <th width="40">部別<br />名稱</th>
		                        <th width="100">品名</th>
		                        <th width="50">最小<br />單位</th>
		                        <th width="50">數量</th>
		                        <th width="180">備註</th>
		                        <th width="50">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
                </div>
                <div id="mainPaginationIndex2" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="showInsert(3)">新 增</button></p>
		        <div id="insertDataDiv3" class="insertDataDiv">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
			                    <th width="90">領用<br />日期</th>
			                    <th width="100">領用人</th>
			                    <th width="100">產品<br />編號</th>
		                        <th width="100">品名</th>
		                        <th width="60">最小<br />單位</th>
		                        <th width="60">庫存量</th>
		                        <th width="60">數量</th>
		                        <th width="150">備註</th>
		                        <th width="60">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
			                    <td><input id="receiveDate" class="date" type="text" value="" size="10" /></td>
			                    <td><input id="receiveBy" type="text" value="" size="12" readonly="readonly" /><span id="receiveByID" class="hideClassSpan"></span></td>
			                    <td><input id="rstationeryID" type="text" value="" size="12" readonly="readonly" /></td>
			                    <td id="rstationeryName"></td>
			                    <td id="rstationeryUnit"></td>
			                    <td id="rStorage"></td>
			                    <td><input id="receiveQuantity" type="text" value="" size="5" /></td>
			                    <td><textarea id="receiveRemark" rows="1" cols="15"></textarea></td>
		                        <td><div class="UD"><button class="btnView" type="button" onclick="saveInsert3()">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                        </td>
		                    </tr>
                        </tbody>
		            </table>
		        </div>
			</div>
			
			<div id="mainContentIndex3">
			    <div id="mainIndexForm3">
			        <table width="780" border="0" id="searchTable4">
			            <tr>
			                <td width="260">報廢單號 <input id="gosrhscrappedID" type="text" value="" /></td>
			                <td width="260">報廢日期 <input id="gosrhscrappedDateStart" class="date" type="text" value="" size="10" /> ～ <input id="gosrhscrappedDateEnd" class="date" type="text" value="" size="10" /></td>
			                <td width="260">經 辦 人 <input id="gosrhscrappedBy" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>產品編號 <input id="gosrhsstationeryID" type="text" value="" /></td>
			                <td>品　　名 <input id="gosrhsstationeryName" type="text" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(4)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList3" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">報廢<br />單號</th>
			                    <th width="70">報廢<br />日期</th>
			                    <th width="85">經辦人</th>
			                    <th width="60">產品<br />編號</th>
			                    <th width="40">部別<br />名稱</th>
		                        <th width="100">品名</th>
		                        <th width="40">最小<br />單位</th>
		                        <th width="45">數量</th>
		                        <th width="190">備註</th>
		                        <th width="50">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
                </div>
                <div id="mainPaginationIndex3" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="showInsert(4)">新 增</button></p>
		        <div id="insertDataDiv4" class="insertDataDiv">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
		                        <th width="100">報廢<br />日期</th>
		                        <th width="100">經辦人</th>
		                        <th width="100">產品<br />編號</th>
		                        <th width="100">品名</th>
		                        <th width="80">最小<br />單位</th>
		                        <th width="80">數量</th>
		                        <th width="150">備註</th>
		                        <th width="70">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
			                    <td><input id="scrappedDate" class="date" type="text" value="" size="10" /></td>
			                    <td><input id="scrappedBy" type="text" value="" size="12" readonly="readonly" /><span id="scrappedByID" class="hideClassSpan"></span></td>
			                    <td><input id="sstationeryID" type="text" value="" size="12" readonly="readonly" /></td>
			                    <td id="sstationeryName"></td>
			                    <td id="sstationeryUnit"></td>
			                    <td><input id="scrappedQuantity" type="text" value="" size="5" /></td>
			                    <td><textarea id="scrappedRemark" rows="1" cols="15"></textarea></td>
		                        <td><div class="UD"><button class="btnView" type="button" onclick="saveInsert4()">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                        </td>
		                    </tr>
                        </tbody>
		            </table>
		        </div>
			</div>
			
			<div id="mainContentIndex4">
			    <div id="mainIndexForm4">
			        <table width="780" border="0" id="searchTable5">
			            <tr>
			                <td width="260">退貨單號 <input id="gosrhreturnedID" type="text" value="" /></td>
			                <td width="260">退貨日期 <input id="gosrhreturnedDateStart" class="date" type="text" value="" size="10" /> ～ <input id="gosrhreturnedDateEnd" class="date" type="text" value="" size="10" /></td>
			                <td width="260">領用對象 <input id="gosrhgetgoodsBy" type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>產品編號 <input id="gosrhrestationeryID" type="text" value="" /></td>
			                <td>品　　名 <input id="gosrhrestationeryName" type="text" value="" /></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(5)">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList4" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
                                <th width="90">退貨<br />單號</th>
                                <th width="85">退貨<br />日期</th>
                                <th width="85">領用<br />日期</th>
                                <th width="90">領用<br />對象</th>
			                    <th width="80">產品<br />編號</th>
			                    <th width="40">部別<br />名稱</th>
		                        <th width="100">品名</th>
		                        <th width="50">最小<br />單位</th>
		                        <th width="50">數量</th>
		                        <th width="100">退貨原因</th>
		                        <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr id="HS_1">
                                <td><input type="text" value="410207001" size="12" /></td>
                                <td><input class="date" type="text" value="102.07.03" size="10" /></td>
                                <td><input class="date" type="text" value="102.07.03" size="10" /></td>
                                <td><input type="text" value="嚴小光" size="10" /></td>
			                    <td><input type="text" value="001" size="5" /></td>
			                    <td><input type="text" value="白板筆-紅" size="10" /></td>
			                    <td><input type="text" value="支" size="5" /></td>
			                    <td><input type="text" value="1" size="4" /></td>
			                    <td><textarea rows="2" cols="15" >要黑筆不是紅筆</textarea></td>
			                    <td><div class="UD"><button class="btnView" type="button" onclick="UpData(1)">更 新</button><br /><button class="btnView" type="button" onclick="">刪 除</button></div>
			                    <div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(1)">儲 存</button><br /><button class="btnView" type="button" onclick="SaveData(1)">取 消</button></div>
			                    </td>
			                </tr>
			            </tbody>
			        </table>
                </div>
                <div id="mainPaginationIndex4" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="showInsert(5)">新 增</button></p>
		        <div id="insertDataDiv5" class="insertDataDiv">
		            <table class="tableList" width="780" border="0">
		                <thead>
		                    <tr>
		                        <th width="90">退貨<br />日期</th>
                                <th width="90">領用<br />日期</th>
                                <th width="100">領用<br />對象</th>
			                    <th width="100">產品<br />編號</th>
		                        <th width="100">品名</th>
		                        <th width="60">最小<br />單位</th>
		                        <th width="60">數量</th>
		                        <th width="120">退貨原因</th>
		                        <th width="60">功能</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr>
			                    <td><input id="returnedDate" class="date" type="text" value="" size="10" /></td>
			                    <td><input id="getgoodsDate" class="date" type="text" value="" size="10" /></td>
			                    <td><input id="getgoodsBy" type="text" value="" size="12" readonly="readonly" /><span id="getgoodsByID" class="hideClassSpan"></span></td>
			                    <td><input id="restationeryID" type="text" value="" size="12" readonly="readonly" /></td>
			                    <td id="restationeryName"></td>
			                    <td id="restationeryUnit"></td>
			                    <td><input id="returnedQuantity" type="text" value="" size="5" /></td>
			                    <td><textarea id="returnedReason" rows="1" cols="15"></textarea></td>
		                        <td><div class="UD"><button class="btnView" type="button" onclick="saveInsert5()">儲 存</button><br /><button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                        </td>
		                    </tr>
                        </tbody>
		            </table>
		        </div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
    <div id="top"></div>
</body>
</html>
