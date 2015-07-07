<%@ Page Language="C#" AutoEventWireup="true" CodeFile="isp_meet_list.aspx.cs" Inherits="isp_meet_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教學管理 - 個案化服務計畫(ISP)會議一覽 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/isp_meet_list.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/isp_meet_list.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <!--<asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
        </asp:ScriptManager>-->
    </form>
    
    <div id="container">
		<div id="header">
			<div id="logo"><a href="default.aspx"><img src="./images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">教學管理&gt; 個案化服務計畫(ISP)會議一覽</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./isp_meet_list.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input type="text" value="" /></td>
			                <td width="260">會議日期 <input class="date" type="text" value="" size="10" />～<input class="date" type="text" value="" size="10" /></td>
			                <td width="260">教師姓名 <input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="80">編號</th>
			                    <th width="100">學生姓名</th>
			                    <th width="80">會議日期</th>
			                    <th width="150">會議名稱</th>
			                    <th width="310">會議參與人員<br />家長、教師、社工、聽力師、主管、其他</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>10123</td>
			                    <td>王小明</td>
			                    <td>98.1.1</td>
			                    <td>ISP會議</td>
			                    <td>王大名、連淑貞、杜佩穎、邱文貞、葉靜雯</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>20123</td>
			                    <td>陳曉雯</td>
			                    <td>97.6.1</td>
			                    <td>ISP會議</td>
			                    <td>陳文文、連淑貞、杜佩穎、邱文貞、葉靜雯</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainContent">
			    <div style="display:block;">
			    <p align="right" style="background-color:#FFDF71;padding:0 10px;">台北至德</p>
			    <p align="right">填寫者 <input type="text" size="10" /></p>
			    
                <table class="tableContact" width="780" border="0">
                    <tr>
		                <th colspan="2">學生名稱</th>
		                <td colspan="6"><input id="studentName" type="text" value="" /></td>
		            </tr>
		            <tr>
		                <th colspan="2">教師名稱</th>
		                <td colspan="6"><input id="teacherName" type="text" value="" /></td>
		            </tr>
		            <tr>
		                <th colspan="2">會議名稱</th>
		                <td colspan="6"><input type="text" value="" size="50" /></td>
		            </tr>
		            <tr>
		                <th colspan="2">會議日期／時間</th>
		                <td colspan="6"><input class="date" type="text" value="" size="10" />／<input type="text" value="" size="10" />～<input type="text" value="" size="10" /></td>
		            </tr>
		            <tr>
		                <th colspan="2">會議地點</th>
		                <td colspan="6"><input type="text" value="" size="30" /></td>
		            </tr>
		            <tr>
		                <th colspan="2">ISP執行期限</th>
		                <td colspan="6"><input class="date" type="text" value="" size="10" />～<input class="date" type="text" value="" size="10" /></td>
		            </tr>
		            <tr>
		                <th colspan="2">會議內容</th>
		                <td colspan="6"><textarea cols="80" rows="10"></textarea></td>
		            </tr>
		            <tr>
		                <th rowspan="2" width="80">出席人員</th>
		                <th width="40">職稱</th>
		                <th width="110">家長</th>
		                <th width="110">教師</th>
		                <th width="110">聽力師</th>
		                <th width="110">社工</th>
		                <th width="110">中心主管</th>
		                <th width="110">其他專業人員</th>
		            </tr>
		            <tr>
		                <th>姓名</th>
		                <td align="center"><input type="text" value="" size="10" /></td>
		                <td align="center"><input type="text" value="" size="10" /></td>
		                <td align="center"><input type="text" value="" size="10" /></td>
		                <td align="center"><input type="text" value="" size="10" /></td>
		                <td align="center"><input type="text" value="" size="10" /></td>
		                <td align="center"><input type="text" value="" size="10" /></td>
		            </tr>
		            <tr>
		                <th colspan="2">備註</th>
		                <td colspan="6"><textarea cols="80" rows="2"></textarea></td>
		            </tr>
                </table>
			    <p class="btnP">
		            <button class="btnSave" type="button">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button">存 檔</button>
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