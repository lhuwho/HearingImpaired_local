<%@ Page Language="C#" AutoEventWireup="true" CodeFile="library_system.aspx.cs" Inherits="library_system" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>圖書借還管理 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	
	<style type="text/css">
	    .tableText {
	        line-height: 35px;
	        font-size: 15px;
	        color: #683812;
	        margin: 20px auto;
	        width: 400px;
        }
        
        .tableText input[type="text"] {
        	text-align: center;
        }
        .tableText > div {
        	display: inline-block;
	        *display: inline;
	        *zoom: 1;
	        width: 200px;
        	text-align: center;
        }
        
        #msgDiv {
        	color: Red;
        	font-size: 16px;
        	font-weight: bold;
        	text-align: center;
        	margin: 20px 0;
        	display: none;
        }
        
        #RecordList {
        	border-top: 1px solid #CCC;
	        line-height: 35px;
	        font-size: 15px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/library_system.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    
    <div id="container">
		<div id="header">
			<div id="logo"><a href="./default.aspx"><img src="images/empty.gif" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0" width="460" height="70" /></a></div>
			<div id="loginInfo"></div>
		</div>
		<div id="content">
			<div id="mainClass">圖書借還管理</div>
			<div id="mainContent">
			    <p align="center">選擇狀態　　<label><input type="radio" name="book" value="1" autocomplete="off" /> 借書</label>　　
			                <label><input type="radio" name="book" value="2" autocomplete="off" /> 還書</label></p>
			    
			    <div id="userData" class="tableText">
			        <div>借閱人ID</div><div><input id="gosrhpeopleID" type="text" value="" autocomplete="off" /></div>
			        <div>借閱人身分</div><div><span id="borrowerStatusName"></span><span id="borrowerStatus" class="hideClassSpan"></span></div>
			        <div>借閱人姓名</div><div><span id="borrowerName"></span><span id="borrowerID" class="hideClassSpan"></span></div>
			        <div>借閱圖書書號</div><div><input id="bookCode" type="text" value="" autocomplete="off" /></div>
			    </div>
			    <div id="bookData" class="tableText">
			        <div>歸還圖書書號</div><div><input id="bookReturnCode" type="text" value="" autocomplete="off" /></div>
			    </div>
			    <p id="msgDiv"></p>
			    <div id="RecordList">
			        <table width="780" border="0" class="tableList"><caption>借還記錄</caption>
                        <thead><tr>
                            <th width="90">借閱日期</th>
                            <th width="70">借還狀態</th>
                            <th width="70">借閱人身分</th>
                            <th width="70">借閱人ID</th>
                            <th width="80">借閱姓名</th>
                            <th width="80">圖書書號</th>
                            <th width="140">圖書書名</th>
                            <th width="90">到期日</th>
                            <th width="90">歸還日期</th>
                        </tr></thead>
                        <tbody>
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