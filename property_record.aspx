<%@ Page Language="C#" AutoEventWireup="true" CodeFile="property_record.aspx.cs" Inherits="property_record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>財產管理 - 財產記錄單 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/property_record.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/jquery.pagination.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/pagination.css" />
    <script type="text/javascript" src="./js/base.js"></script>
    <script type="text/javascript" src="./js/jquery.form.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/property_record.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    
    <div id="container">
		<div id="header">
			<div id="logo"><a href="default.aspx">
			<img src="./images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo" runat="server"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">財產管理&gt; 財產記錄單</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./property_record.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">財產條碼 <input id="gosrhpropertyCode" type="text" value="" autocomplete="off" /></td>
			                <td width="260">財產編號 <input id="gosrhpropertyID" type="text" value="" autocomplete="off" /></td>
			                <td width="260">財產名稱 <input id="gosrhpropertyName" type="text" value="" autocomplete="off" /></td>
			            </tr>
			            <tr>
			                <td>地區 <input id="gosrhapplyID" type="text" value="" autocomplete="off" /></td>
			                <td>放置地點 <select id="gosrhLocation" name="propertyLocation" autocomplete="off"><option>請選擇</option></select></td>
			                <td>保管人員 <select id="gosrhCustody" name="propertyCustody" autocomplete="off"><option>請選擇</option></select></td>
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
			                    <th width="60">財產條碼</th>
			                    <th width="70">地區</th>
			                    <th width="120">財產編號</th>
			                    <th width="110">財產名稱</th>
			                    <th width="100">放置地點</th>
			                    <th width="90">保管人員</th>
			                    <th width="80">購置日期</th>
			                    <th width="50">單價</th>
			                    <th width="50">狀態</th>
			                    <th width="50">功能</th>
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
			    <p id="sUnit" align="right" style="background-color:#FFDF71;padding:0 10px;" runat="server">&nbsp;</p>
			    <p>財產狀態 <select id="propertyStatus" autocomplete="off"><option value="1">使用中</option><option value="2">報廢</option><option value="3">停用</option><option value="4">贈出</option><option value="5">轉出</option></select>
			    <span style="float:right;">填寫日期 <input id="fillInDate" type="text" class="date" size="10" autocomplete="off" /></span></p>
			    <p align="center" id="changeAdd"><label><input type="radio" name="propertyChange" value="1" autocomplete="off" /> 財產增加</label><span class="startMark">*</span>：
			              <label><input type="radio" name="changeStatus" value="1" autocomplete="off" /> 購買</label>　
			              <label><input type="radio" name="changeStatus" value="2" autocomplete="off" /> 捐贈</label></p>
			    <p align="center" id="changeSubtract" style="display:none;"><label><input type="radio" name="propertyChange" value="2" autocomplete="off" /> 財產減損</label><span class="startMark">*</span>：
			              <label><input type="radio" name="changeStatus" value="3" autocomplete="off" /> 報廢</label>　
			              <label><input type="radio" name="changeStatus" value="4" autocomplete="off" /> 遺失</label>　
			              <label><input type="radio" name="changeStatus" value="5" autocomplete="off" /> 贈出</label></p>
			    <table class="tableText" width="780" border="0">
			        <tr>
		                <th width="150">財產編號</th>
		                <td><input id="propertyID" type="text" value="" autocomplete="off" size="30" /><span class="startMark">*</span></td>
		            </tr>
		            <tr>
		                <th>條　　碼</th>
		                <td><input id="propertyCode" type="text" value="" autocomplete="off" readonly="readonly" disabled="disabled" /></td>
		            </tr>
		            <tr>
		                <th>流水編號</th>
		                <td><input id="applyID" type="text" value="" readonly="readonly" autocomplete="off" /></td>
		            </tr>
		            <tr>
		                <th>類　　別</th>
		                <td><select id="propertyCategory" name="propertyCategory" autocomplete="off"><option value="0">請選擇</option></select><span class="startMark">*</span></td>
		            </tr>
		            <tr>
		                <th>財產名稱</th>
			            <td><input id="propertyName" type="text" value="" autocomplete="off" /></td>
		            </tr>
		            <tr>
		                <th>廠牌/型式</th>
			            <td><input id="propertyLabel" type="text" value="" autocomplete="off" /></td>
		            </tr>
		            <tr>
		                <th>單　　位</th>
			            <td><input id="propertyUnit" type="text" value="個" autocomplete="off" /></td>
		            </tr>
		            <tr>
		                <th>數　　量</th>
			            <td><input id="propertyQuantity" type="text" value="1" autocomplete="off" /></td>
		            </tr>
		            <tr>
		                <th>是否有配件？</th>
			            <td><label><input type="radio" name="propertyFitting" value="1" autocomplete="off" /> 是</label>　　
			                <label><input type="radio" name="propertyFitting" value="2" autocomplete="off" /> 否</label><span class="startMark">*</span></td>
		            </tr>
		            <tr>
		                <th class="BdgColor">放置地點</th>
			            <td><select id="propertyLocation" name="propertyLocation" autocomplete="off" runat="server"></select><span class="startMark">*</span></td>
		            </tr>
		            <tr>
		                <th class="BdgColor">保管人員</th>
			            <td><select id="propertyCustody" name="propertyCustody" autocomplete="off" runat="server"></select><span class="startMark">*</span></td>
		            </tr>
		         </table>
		         
		         <table class="tableContact" width="780" border="0">
			       <tr>
			           <th width="150">購置日期</th>
			           <td><input id="buyDate" type="text" class="date" value="" size="10" autocomplete="off" /></td>
			           <th>殘　　值</th>
			           <td><input id="Remnants" type="text" value="" autocomplete="off" /></td>
			       </tr>
			       <tr>
			            <th>購置來源</th>
			            <td><input id="buySource" type="text" value="" autocomplete="off" /></td>
			            <th>使用年限</th>
			            <td><input id="userYear" type="text" value="" autocomplete="off" /></td>
			        </tr>
			        <tr>
			            <th>單　　價</th>
			            <td><input id="propertyPrice" type="text" value="" autocomplete="off" /></td>
			            <th>折舊方法</th>
			            <td><input id="Depreciation" type="text" value="" autocomplete="off" /></td>
			            
			        </tr> 
			        <tr>
			            <th>自 籌 款</th>
			            <td><input id="selfFunds" type="text" value="" autocomplete="off" /></td>
			            <th>經辦單位（採購人）</th>
			            <td><input id="PurchaserName" type="text" value="" autocomplete="off" readonly="readonly" /><input id="Purchaser" class="hideClassSpan" type="text" value="" autocomplete="off" /></td>
			        </tr> 
			        <tr>
			            <th>補 助 款</th>
			            <td><input id="Grant" type="text" value="" autocomplete="off" /></td>
			            <td colspan="2">&nbsp;</td>
			        </tr>
			        <tr>
			            <th>備　　註</th>
			            <td colspan="3"><textarea id="Remark" rows="2" cols="80" autocomplete="off"></textarea></td>
			        </tr> 
			    </table>
		         
		         <table class="tableText" width="780" border="0">
		            <tr>
			            <th>傳票號碼</th>
			            <td><input id="propertySummons" type="text" value="" autocomplete="off" /></td>
			        </tr>
			        <tr>
			            <th>收據號碼</th>
			            <td><input id="propertyReceipt" type="text" value="" autocomplete="off" /></td>
			        </tr>
			        <tr>
			            <th>會計科目</th>
			            <td><input id="propertyAccounting" type="text" value="" autocomplete="off" /></td>
		            </tr>
			        <tr>
			            <th>入帳日期</th>
			            <td><input id="inputDate" type="text" class="date" size="10" autocomplete="off" /></td>
			        </tr>
		            <tr>
			            <th>入出日期</th>
			            <td><input id="outputDate" type="text" class="date" size="10" autocomplete="off" /></td>
			        </tr>
		            <tr>
			            <th>經費來源</th>
			            <td><label><input type="radio" name="fundSource" value="1" autocomplete="off" /> 會務經費 </label>　　
			                <label><input type="radio" name="fundSource" value="2" autocomplete="off" /> </label><input id="fundAssist" type="text" value="" autocomplete="off" /> 輔助　　
			                <label><input type="radio" name="fundSource" value="3" autocomplete="off" /> 捐贈</label> <input id="fundDonate" type="text" value="" autocomplete="off" /></td>
                    </tr>
                    <tr>
		                <th class="BdgColor">報廢/遺失日期</th>
			            <td><input id="stopDate" type="text" class="date" size="10" autocomplete="off" /></td>
		            </tr>
                </table>
			    
			    <p class="cP">財產附件</p>
			    <form id="GmyForm" action="" method="post" enctype="multipart/form-data">
			    <table id="appurtenance" class="tableContact" width="780" border="0">
                    <tr>
			            <td width="195" height="195"><input type="file" name="attachment1" size="1" autocomplete="off" /><br />
			            <a id="attachment1Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment1" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			            <td width="195"><input type="file" name="attachment2" size="1" autocomplete="off" /><br />
			            <a id="attachment2Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment2" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			            <td width="195"><input type="file" name="attachment3" size="1" autocomplete="off" /><br />
			            <a id="attachment3Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment3" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			            <td width="195"><input type="file" name="attachment4" size="1" autocomplete="off" /><br />
			            <a id="attachment4Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment4" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			        </tr>
			        <tr>
			            <td width="195" height="195"><input type="file" name="attachment5" size="1" autocomplete="off" /><br />
			            <a id="attachment5Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment5" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			            <td width="195"><input type="file" name="attachment6" size="1" autocomplete="off" /><br />
			            <a id="attachment6Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment6" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			            <td width="195"><input type="file" name="attachment7" size="1" autocomplete="off" /><br />
			            <a id="attachment7Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment7" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			            <td width="195"><input type="file" name="attachment8" size="1" autocomplete="off" /><br />
			            <a id="attachment8Url" class="showUploadImg" href="./images/noPhoto.jpg"><i></i><img id="attachment8" src="./images/noPhoto.jpg" alt="" border="0" /></a></td>
			        </tr>
			    </table>
			    </form>
			    
			    <div id="ChangesExplainDiv" class="BdgColor" style="display:none;">
			        <p class="cP">異動/報廢說明</p>
			        <table class="tableContact" width="780" border="0">
			            <thead>
			            <tr>
			                 <th width="100">異動日期</th>
			                 <th>摘要</th>
			                 <th width="140">相關人員</th>
			                 <th width="110">功能</th>
			            </tr>
			            </thead>
			            <tbody>
			            </tbody>
			        </table>
			        <p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData()">新 增</button></p>
		            <div id="insertDataDiv" style="display:none">
		                <table class="tableList" width="780" border="0">
		                    <thead>
		                        <tr>
		                            <th width="100">異動日期</th>
			                        <th>摘要</th>
			                        <th width="140">相關人員</th>
			                        <th width="110">功能</th>
		                        </tr>
		                    </thead>
		                    <tbody>
		                        <tr>
		                            <td><input id="moveDate" class="date" type="text" value="" size="10" autocomplete="off" /></td>
		                            <td><textarea id="moveAbstract" cols="50" rows="1" autocomplete="off"></textarea></td>
		                            <td><input id="relatedBy" type="text" value="" readonly="readonly" autocomplete="off" size="15" /><span id="relatedByID" class="hideClassSpan"></span></td>
		                            <td><button class="btnView" type="button" onclick="InsertTrackData();">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert()">取 消</button></td>
		                        </tr>
		                     </tbody>
		                </table>
		            </div>
		            <p><br /></p>
		        </div>
		        
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnOutdate" type="button" onclick="outData()">移 出</button>
		            <button class="btnSaveUdapteData" type="button" onclick="saveData(1)">存 檔</button>
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