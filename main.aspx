<%@ Page Language="C#" AutoEventWireup="true" CodeFile="main.aspx.cs" Inherits="main" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>首頁 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/main.js"></script>
	<style type="text/css">
	    .tableText td {
	        border-bottom: 1px solid #CCC;
	        line-height: 35px;
	        font-size: 15px;
	        color: #683812;
	        text-align: center;
        }
    </style>
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
			<div id="logo"><a href="./default.aspx"><img src="images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
			<div id="menu"></div>
		</div>
		<div id="content">
			<div id="mainClass">系統提醒</div>
			<div id="mainContent">
				
			    <table class="tableText" width="780" border="0">
		             <thead>
			                <tr>
			                    <th width="60">流水號</th>
			                    <th width="95">建立日期</th>
			                    <th width="95">指派者</th>
			                    <th width="270">內容</th>
			                    <th width="95">提醒日期</th>
			                    <th width="95">完成日期</th>
			                    <th width="70">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                
			            </tbody>
			    </table>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
