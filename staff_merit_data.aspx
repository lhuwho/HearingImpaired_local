<%@ Page Language="C#" AutoEventWireup="true" CodeFile="staff_merit_data.aspx.cs" Inherits="staff_merit_data" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>人事管理 - 員工考績資料維護 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/staff_merit_data.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
	<script src="./js/jquery.pagination.js" type="text/javascript"></script>
    <link type="text/css" href="./css/pagination.css" rel="stylesheet" />
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/staff_merit_data.js"></script>
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
			<div id="mainClass">人事管理&gt; 員工考績資料維護</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">一覽</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			    <p align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">台北至德</p>
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">員工姓名 <input type="text" id="gosrhstaffName" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstaffSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" id="gosrhstaffBirthdayStart" size="10" />～<input class="date" type="text" id="gosrhstaffBirthdayEnd" size="10" /></td>
			            </tr>
			            <tr>
			                <td>員工編號 <input type="text" id="gosrhstaffID" value="" /></td>
			                <td>年　　度 <input type="text" id="gosrhstaffYear" value="" size="5" /></td>
                            <td>機 構 別 <select id="gosrhstaffUnit"></select>
			                </td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">顯 示</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="37" rowspan="2">年度</th>
			                    <th width="71" rowspan="2">受評者姓名</th>
			                    <th width="71" rowspan="2">機構別</th>
			                    <th width="60" rowspan="2">出勤表現20分<br />(基本分17分)</th>
			                    <th width="60" rowspan="2">專業績效40分<br />(基本分34分)</th>
			                    <th width="60" rowspan="2">工作態度40分<br />(基本分34分)</th>
			                    <th width="60" rowspan="2">考績原始分數</th>
			                    <th width="" colspan="2">獎懲加扣分</th>
			                    <th width="60">考績<br />總分</th>
			                    <th width="" rowspan="2">考績等次</th>
			                    <th width="100" rowspan="2">功能</th>
			                </tr>
			                <tr>
			                    <th width="60">加分/嘉獎、記功</th>
			                    <th width="60">減分/申誡、記過</th>
			                    <th width="">100</th>
			                </tr>
			            </thead>
			            <tbody>
			                
			            </tbody>
			        </table>
			    </div>
			    <div id="mainPagination" class="pagination"></div>
			    <p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData()">新 增</button></p>
		        <div id="insertDataDiv" style="display:none">
		            <table class="tableText" width="780" border="0">
		                <thead>
		                    <tr>
		                        <th width="" rowspan="2">年度</th>
		                        <th width="70" rowspan="2">受評者姓名</th>
		                        <th width="71" rowspan="2">機構別</th>
		                        <th width="" rowspan="2">出勤表現20分<br />(基本分17分)</th>
		                        <th width="" rowspan="2">專業績效40分<br />(基本分34分)</th>
		                        <th width="" rowspan="2">工作態度40分<br />(基本分34分)</th>
		                        <th width="" rowspan="2">考績原始分數</th>
		                        <th width="" colspan="2">獎懲加扣分</th>
		                        <th width="">考績<br />總分</th>
		                        <th width="" rowspan="2">考績等次</th>
		                        <th width="100" rowspan="2">功能</th>
		                    </tr>
		                    <tr>
		                        <th width="">加分/嘉獎、記功</th>
		                        <th width="">減分/申誡、記過</th>
		                        <th width="">100</th>
		                    </tr>
		                </thead>
		                <tbody>
		                    <tr id="setMerit">
		                        <td><input class="MeritYear" type="text" value="" size="3" /></td>
		                        <td><input class="MeritName" type="text" value="" size="8"/><span class="MeritID" style="display:none;"></span></td>
		                        <td ><span class="MeritUnit"></span><span class="MeritUnitID" style="display:none;"></span></td>
		                        <td><input class="AScore" type="text" value="" size="5"/></td>
		                        <td><input class="PScore" type="text" value="" size="5" /></td>
		                        <td><input class="WScore" type="text" value="" size="5" /></td>
		                        <td class="PASRawScore"></td>
		                        <td><input class="AddScore" type="text" value="0" size="5" /></td>
		                        <td><input class="LowerScore" type="text" value="0" size="5" /></td>
		                        <td class="PASScore"></td>
		                        <td ><span class="Grade"></span><span class="GradeID" style="display:none;"></span></td>
		                        <td><div class="UD"><button class="btnView" type="button" onclick="setMeritData()">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert()">取 消</button></div>
		                        </td>
		                    </tr>
                        </tbody>
                    </table>
		        </div>
		        <div class="remark">
		            (1) 考績總分100為滿分。<br />
                    (2) 總幹事考績由董事長考評。<br />
                    (3) 總會員工及各中心主管由總幹事考評，至德中心員工主管考評，總幹事覆核。<br />
                    (4) 依員工手冊獎懲處要點及考績評分作業細則執行。<br />
                    獎懲加扣考分數如下：大功加3分；小功加2分；嘉獎加1分；大過扣3分；小過扣2分；申誡扣1分。
		        </div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
