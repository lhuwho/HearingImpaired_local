<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_salary.aspx.cs" Inherits="staff_salary" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>薪資管理 - 薪資表 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/staff_salary.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/staff_salary.js"></script>
	<%--<script src="http://ajax.aspnetcdn.com/ajax/jQuery/jquery-1.9.1.js "></script>--%>
    <script src="http://ajax.aspnetcdn.com/ajax/knockout/knockout-3.0.0.js "></script>
    <script src="http://html2canvas.hertzen.com/build/html2canvas.js"></script>
	
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
			<div id="mainClass">薪資管理&gt; 薪資表</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./staff_salary.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">員工姓名 <input type="text" id="sName" value="" /></td>
			                <td width="260">性　　別 <select id="sSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" id="sBirthdayStart" size="10" />～<input class="date" type="text" id="sBirthdayEnd" size="10" /></td>
			            </tr>
			            <tr>
			                <td>員工編號 <input type="text" id="sID" value=""/></td>
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
			<div id="mainMenuList2">
			    <div id="item1" class="menuTabs">薪資表</div>
			</div>

			<div id="mainContent" style="background:white;" >
			    <table width="780" border="1" style="border-style:groove">
			    <tr><td rowspan="2" ><img width="45%" src="./images/print_logo.jpg" /></td><td colspan="2" >財團法人中華民國婦聯聽障文教基金會</td><td rowspan="2" ><p id="Unit"></p></td></tr>
			    <tr><td colspan="2" ><span id="yearDate"></span> 年 <span id="monthDate"></span> 月份薪津給與明細表</td></tr>
			    <tr><td colspan="4">&nbsp;</td></tr>
			    <tr><td >姓名</td><td colspan="3"><span id="staffName"></span></td></tr>
			    <tr><td >職稱</td><td colspan="3"><span id="WorkItem"></span></td></tr>
			    <tr><td >職等</td><td colspan="3"><span id="JobCapacity"></span></td></tr>
			    <tr><td colspan="4">&nbsp;</td></tr>
			    <tr><td colspan="2" ><p align="center">應發項目</p></td><td colspan="2"><p align="center">應扣項目</p></td></tr>
			    <tr>
			        <td  colspan="2"  style="vertical-align:text-top;" >		            
			            <div id="addItemDiv">
		                    <ul class="tableTH">
			                   <li class="thItem">項目名</li><li class="thItem">金額</li>
			                </ul>
			                <ul>
			                   <li class="thItem">本薪</li><li class="thItem">$<span id="totalSalary"></span></li>
			                </ul>
		                </div>
		            </td>
		            <td  colspan="2"  style="vertical-align:text-top;" >		            
			              <div id="minusItemDiv">
		                <ul class="tableTH">
			                <li class="thItem">項目名</li><li class="thItem">金額</li>
			            </ul>
			            <ul>
			                <li class="thItem">勞保費</li><li class="thItem" >$<span id="laborInsurance"></span></li>
			            </ul>
			            <ul>
			                </li><li class="thItem">健保費</li><li class="thItem" >$<span id="healthInsurance"></span></li>
			            </ul>
			            <ul>
			                </li><li class="thItem">所得稅</li><li class="thItem" >$<span id="withholdingTax"></span></li>
			            </ul>
			            <ul>
			                <li class="thItem">自提退休準備金</li><li class="thItem">$<span id="pensionFunds"></span><span id="pensionFundsPer"></span></li>
			            </ul>
			            <ul>
			                <li class="thItem">請假扣款</li><li class="thItem" >$<span id="salaryDeductions"></span></li>
			            </ul>
			            
		                </div>
		            </td>
		        </tr>
		        <tr><td colspan="4">&nbsp;</td></tr>
		       <tr><td width="25%" >應發金額</td><td width="25%">$<span id="AddMoney"></span></td><td  width="25%">應扣金額</td><td width="25%">$<span id="MinsMoney"></span></td></tr>
		        <tr><td colspan="4">&nbsp;</td></tr>
		        <tr><td >實發金額</td><td colspan="3">$<span id="realWages"></span></td></tr>
		        <tr><td colspan="4">&nbsp;</td></tr>
		       <%-- <tr><td colspan="4"><textarea  id="AddTitle" disabled="disabled" rows="3" style="width:100%;border:none;" ></textarea></td></tr>--%>
			     <tr><td colspan="4"><span  id="AddTitle"  ></span></td></tr>
			    </table>
			    <%--<div id="item1Content">
			        <p align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;" id="Unit">&nbsp;</p>
			        <p align="center">民國 <span id="yearDate"></span> 年 <span id="monthDate"></span> 月 薪資明細</p>
		            <p>員工姓名 <span id="staffName"></span>
		            <span style="float:right;">填表日期 <span id="fillInDate"></span></span></p>
		            <div id="addItemDiv">
		                <ul class="tableTH">
			                <li class="thSystem">加項目</li><li class="thItem">項目名</li><li class="thItem">金額</li><li class="thItem2">備註</li>
			            </ul>
			            <ul>
			                <li class="thSystem"></li><li class="thItem">本薪</li><li class="thItem" id="totalSalary"></li><li class="thItem2" id="salaryExplain5"></li>
			            </ul>
		            </div>
		            <div id="minusItemDiv">
		                <ul class="tableTH">
			                <li class="thSystem">減項目</li><li class="thItem">項目名</li><li class="thItem">金額</li><li class="thItem2">備註</li>
			            </ul>
			            <ul>
			                <li class="thSystem"></li><li class="thItem">勞保費</li><li class="thItem" id="laborInsurance"></li><li class="thItem2" id="salaryExplain1"></li>
			            </ul>
			            <ul>
			                <li class="thSystem"></li><li class="thItem">健保費</li><li class="thItem" id="healthInsurance"></li><li class="thItem2" id="salaryExplain2"></li>
			            </ul>
			            <ul>
			                <li class="thSystem"></li><li class="thItem">所得稅</li><li class="thItem"  id="withholdingTax"></li><li class="thItem2" id="salaryExplain4"></li>
			            </ul>
			            <ul>
			                <li class="thSystem"></li><li class="thItem">自提退休準備金</li><li class="thItem"><span id="pensionFunds"></span><span id="pensionFundsPer"></span></li><li class="thItem2" id="salaryExplain3" ></li>
			            </ul>
			            <ul>
			                <li class="thSystem"></li><li class="thItem">請假扣款</li><li class="thItem"  id="salaryDeductions"></li><li class="thItem2" id="salaryExplain6"></li>
			            </ul>
			            
		            </div>
		            <br />
		            <ul  class="tableTH">
			            <li class="thSystem"></li><li class="thItem"></li><li class="thItem"></li><li class="thItem2"></li><li class="thSystem">實領工資</li>
			            <li class="thSystem"></li><li class="thItem"></li><li class="thItem"></li><li class="thItem2"></li><li class="thSystem" id="realWages"></li>
			        </ul>
		            <p class="btnP">
		                <button class="button" type="button" onclick="">寄 出</button>
		            </p>
			    </div>--%>
			</div>
			<p class="btnP">
			    <button class="btnUpdate"  type="button" onclick="GetImg()"  >列印</button>
			 </p>
			<div id="test" ></div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>