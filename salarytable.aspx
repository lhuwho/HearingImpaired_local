<%@ Page Language="C#" AutoEventWireup="true" CodeFile="salarytable.aspx.cs" Inherits="salarytable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>薪資管理 - 薪資明細 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/salarytable.css" />
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
                <asp:ScriptReference Path="~/js/salarytable.js" />
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
			<div id="mainClass">薪資管理&gt; 薪資明細</div>
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./salarytable.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">員工姓名 <input type="text" id="gosrhstaffName" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstaffSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" id="gosrhstaffBirthdayStart" size="10" />～<input class="date" type="text" id="gosrhstaffBirthdayEnd" size="10" /></td>
			            </tr>
			            <tr>
			                <td>員工編號 <input type="text" id="gosrhstaffID" value=""/></td>
			                <td>
			                    派任單位 <select id="gosrhstaffUnit"></select>
			                </td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="SearchData()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="150">員工編號</th>
			                    <th width="140">派任單位</th>
			                    <th width="140">員工姓名</th>
			                    <th width="110">性別</th>
			                    <th width="140">填表日期</th>
			                    <th width="100">功能</th>
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
			    <p align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;" id="Unit">&nbsp;</p>
			    <span class="startMark">*</span><select id="yearDate"><option value="0">民國年</option></select> <select id="monthDate"><option value="0">月</option></select>月 薪資明細
		        <br />
		        <p>員工姓名 <input id="staffName" type="text" value="" size="10" readonly="readonly"/><span id="staffID" class="hideClassSpan"></span><span class="startMark">*</span>
		        <span style="float:right;"><span class="startMark">*</span>填表日期 <input id="fillInDate" class="date" type="text" value="" size="10" /></span></p>
		        <div id="addItemDiv">
		            <ul class="tableTH">
			            <li class="thSystem">加項目</li><li class="thItem">項目名</li><li class="thItem">金額</li><li class="thItem2">備註</li><li class="thSystem">功能</li>
			        </ul>
			        <ul>
			            <li class="thSystem"></li><li class="thItem">本薪</li><li class="thItem" id="totalSalary"></li><li class="thItem2"><input id="salaryExplain5" type="text" value=""/></li><li class="thSystem"></li>
			        </ul>
			        <ul id="addItem_0">
			            <li class="thSystem"><button type="button" class="btnAdd" onclick="getAddItem('add')">＋</button></li><li class="thItem"><select class="addSelect">
			                                <option value="0">增加項目</option>
			                                <option value="補發薪資">補發薪資</option>
			                                <option value="伙食津貼">伙食津貼</option>
		                                    <option value="全勤獎金">全勤獎金</option>
		                                    <option value="全勤獎勵金">全勤獎勵金</option>
		                                    <option value="年終獎金">年終獎金</option>
		                                    <option value="考績獎金">考績獎金</option>
			                                <option value="退還勞保保費">退還勞保保費</option>
			                                <option value="退還健保保費">退還健保保費</option>
		                                    <option value="退還自提退休準備金">退還自提退休準備金</option>
		                                    <option value="特殊加給">特殊加給</option>
		                                    <option value="專業加給">專業加給</option>
		                                    <option value="證照加給">證照加給</option>
		                                    <option value="其他－發生再陳列">其他－發生再陳列</option>
		                              </select>
		                </li><li class="thItem"></li><li class="thItem2"></li><li class="thSystem"></li>
			        </ul>
		        </div>
		        <div id="minusItemDiv">
		            <ul class="tableTH">
			            <li class="thSystem">減項目</li><li class="thItem">項目名</li><li class="thItem">金額</li><li class="thItem2">備註</li><li class="thSystem">功能</li>
			        </ul>
			        <ul>
			            <li class="thSystem"></li><li class="thItem">勞保費</li><li class="thItem" id="laborInsurance"></li><li class="thItem2"><input id="salaryExplain1" type="text" value=""/></li><li class="thSystem">&nbsp;</li>
			        </ul>
			        <ul>
			            <li class="thSystem"></li><li class="thItem">健保費</li><li class="thItem" id="healthInsurance"></li><li class="thItem2"><input id="salaryExplain2" type="text" value=""/></li><li class="thSystem"></li>
			        </ul>
			        <ul>
			            <li class="thSystem"></li><li class="thItem">所得稅</li><li class="thItem"  id="withholdingTax"></li><li class="thItem2"><input id="salaryExplain4" type="text" value=""/></li><li class="thSystem"></li>
			        </ul>
			        <ul>
			            <li class="thSystem"></li><li class="thItem">自提退休準備金</li><li class="thItem"><span id="pensionFunds"></span><span id="pensionFundsPer"></span></li><li class="thItem2"><input id="salaryExplain3" type="text" value=""/></li><li class="thSystem"></li>
			        </ul>
			        <ul>
			            <li class="thSystem"></li><li class="thItem">請假扣款</li><li class="thItem"><input id="salaryDeductions" type="text" value="0"/></li><li class="thItem2"><input id="salaryExplain6" type="text" value=""/></li><li class="thSystem"></li>
			        </ul>
			        <ul id="minusItem_0">
			            <li class="thSystem"><button type="button" class="btnAdd" onclick="getAddItem('minus')">＋</button></li><li class="thItem"> <select class="minusSelect">
			                                <option value="0">減少項目</option>
			                                <option value="追溯勞保費">追溯勞保費</option>
			                                <option value="追溯健保費">追溯健保費</option>
		                                    <option value="追溯自提退休準備">追溯自提退休準備金</option>
		                                    <option value="其他－發生再陳列">其他－發生再陳列</option>
		                              </select></li><li class="thItem"></li><li class="thItem2"></li><li class="thSystem">&nbsp;</li>
			        </ul>
		        </div>
		        <br />
		        <ul  class="tableTH">
			        <li class="thSystem"></li><li class="thItem"></li><li class="thItem"></li><li class="thItem2"></li><li class="thSystem">實領工資</li>
			        <li class="thSystem"></li><li class="thItem"></li><li class="thItem"></li><li class="thItem2"></li><li class="thSystem" id="realWages"></li>
			    </ul>
			    <br />
			    <ul class="tableTH">
			       <li class="thItem">備註</li>
			    </ul>
			      <ul><li class="thItem"><textarea id="AddTitle" cols="100" ></textarea></li> 
			  <%--  <ul class="tableTH"><li class="thItem">說明問題請洽</li></ul>
			      <ul><li class="thItem"><textarea id="ErMessage" cols="100"  ></textarea></li></ul>--%>
			   

			    

			    <br />
		        <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="saveData(1)">存 檔</button>
		            <button class="btnCancel" type="button">取 消</button>
		        </p>
			</div>
			</div>
			</div>
			
		</div>
		<div id="footer"></div>
	</div>
</body>
</html>



