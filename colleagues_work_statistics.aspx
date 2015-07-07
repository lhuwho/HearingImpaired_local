<%@ Page Language="C#" AutoEventWireup="true" CodeFile="colleagues_work_statistics.aspx.cs" Inherits="colleagues_work_statistics" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>薪資管理 - 出勤統計 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/colleagues_work_statistics.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/colleagues_work_statistics.js"></script>
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
			<div id="mainClass">薪資管理&gt; 出勤統計</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">每月全會每個人出勤表</div>
			    <div id="btnIndex" class="menuTabs2">年度教職員個人出勤統計表</div>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <p>出勤月份（年月） <select id="yearDate2"><option value="-1">民國年</option></select>
                        <select id="monthDate2"><option value="-1">月</option></select>
                        <button class="btnSearch" type="button" onclick="showView(1)">顯 示</button>
                    </p>
			    </div>
			    <div id="mainSearchList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <caption>102年六月全會每個人出勤表</caption>
			            <thead>
			                <tr>
                                <th width="80" height="30">單位</th>
                                <th width="60">同仁<br />編號</th>
                                <th width="80">姓名</th>
                                <th width="40">遲到</th>
                                <th width="40">事假</th>
			                    <th width="40">病假</th>
			                    <th width="40">特別<br />休假</th>
			                    <th width="40">婚假</th>
			                    <th width="40">產假</th>
                                <th width="40">喪假</th>
                                <th width="40">公假</th>
                                <th width="40">公傷</th>
			                    <th width="40">未打卡</th>
			                    <th width="40">工作<br />異動</th>
			                    <th width="120">備註</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>基金會</td>
			                    <td>1</td>
			                    <td>蔡曉明</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>0.5</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                </tr>
			                <tr>
			                    <td>台北</td>
			                    <td>2</td>
			                    <td>＊＊＊</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>3</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>0.875</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>10/3參加口足畫藝活動*0.5；10/23參加婦聯總會蔣夫人追思會*0.375</td>
			                </tr>
			                <tr>
			                    <td>台北</td>
			                    <td>3</td>
			                    <td>＊＊＊</td>
			                    <td>3</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>1.375</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                </tr>
			                <tr>
			                    <td>台北</td>
			                    <td>4</td>
			                    <td>＊＊＊</td>
			                    <td>1</td>
			                    <td>2.25</td>
			                    <td>&nbsp;</td>
			                    <td>1.375</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>0.375</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>10/5(林森國小)班級宣導*0.375</td>
			                </tr>
			                <tr>
			                    <td>台北</td>
			                    <td>5</td>
			                    <td>＊＊＊</td>
			                    <td>&nbsp;</td>
			                    <td>0.875</td>
			                    <td>&nbsp;</td>
			                    <td>1.375</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>10/8支援婦聯總會活動*0.5；10/30參加婦聯會薩爾瓦多共和國第一夫人演講會*0.375</td>
			                </tr>
			            </tbody>
			        </table>
                </div>
			</div>
			
			<div id="mainContentIndex">
			    <div id="mainIndexForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">同仁編號 <input type="text" name="name" value="" /></td>
			                <td width="260">同仁姓名 <input type="text" name="name" value="" /></td>
			                <td width="260">出勤年份 <select id="yearDate3"><option value="-1">民國年</option></select></td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="showView(2)">顯 示</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainIndexList" class="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="260" height="30">姓名</th>
			                    <th width="260">到職日</th>
			                    <th width="260">特休天數</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>嚴大光</td>
			                    <td>102.03.01</td>
			                    <td>12</td>
			                </tr>
			            </tbody>
			        </table>
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100" height="30">起訖日期/時間</th>
                                <th width="40">事假</th>
                                <th width="40">累計</th>
			                    <th width="40">病假</th>
			                    <th width="40">累計</th>
			                    <th width="40">特休</th>
			                    <th width="40">補休</th>
			                    <th width="40">剩餘</th>
			                    <th width="40">婚假</th>
			                    <th width="40">產假</th>
                                <th width="40">喪假</th>
                                <th width="40">剩餘</th>
                                <th width="40">公假</th>
                                <th width="40">累計</th>
                                <th width="40">公傷</th>
                                <th width="40">累計</th>
			                    <th width="40">事由</th>
			                    <th width="40">備註</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>1月10日/08:30<br />至<br />1月10日/13:00</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>0.5</td>
			                    <td>2.125</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>出席防災說明會</td>
			                    <td>&nbsp;</td>
			                </tr>
			                <tr>
			                    <td>1月11日/15:30<br />至<br />1月11日/17:30</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>0.25</td>
			                    <td>&nbsp;</td>
			                    <td>19.75</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>2.125</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
			                    <td>&nbsp;</td>
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



