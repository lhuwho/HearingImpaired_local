<%@ Page Language="C#" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<script type="text/javascript" src="./js/default.js"></script>
	<style type="text/css">
	    body {
	        background: #EFA320;
        }
        #cerceve {
            width: 400px;
            height: 300px;
            margin-left: -200px;
            margin-top: -150px;
            position: absolute;
            top: 50%;
            left: 50%;
        }
        .header {
            background: url(./images/header_bg.png) no-repeat top;
            height: 63px;
            border-top-left-radius: 1px;
            border-top-right-radius: 1px;
            line-height: 63px;
            text-shadow: 1px 1px rgba(53,16,56,0.5);
            color: #fff;
            font-weight: bold;
            text-align: center;
        }
        .formbody {
            width: 400px;
            height: 240px;
            background: url(./images/formbody.png) no-repeat top;
        }
        #username {
	        background: url(./images/username.png) no-repeat;
        }
        #password {
	        background: url(./images/password.png) no-repeat;
        }

        #cerceve input[type="text"], #cerceve input[type="password"] {
            width: 298px;
            height: 52px;
            margin: 20px 0 0 22px;
            outline: none;
            padding: 0 40px 0 20px;
            border: 0;
        }
        #loginok {
            width: 100px;
            height: 40px;
            margin: 20px 20px 0 22px;
            background: url(./images/login.png) no-repeat;
            font-weight: bold;
            text-shadow: 1px 1px rgba(53,16,56,0.5);
            color: #fff;
            cursor: pointer;
            border: 0;
            cursor: pointer;
            filter: alpha(opacity=80);
            opacity: 0.8;
        }
        #loginok:hover, #sign:hover, #inline input[type="button"]:hover {
            filter: alpha(opacity=100);
            opacity: 1.0;
        }

        #forgot {
            text-decoration: none;
            color: #a9a9bc;
            font-size: 13px;
            text-shadow: 1px 1px rgba(255,255,255,0.75);
            filter: alpha(opacity=80);
            opacity: 0.8;
        }
	</style>
</head>
<body >
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
        </asp:ScriptManager>
    </form>
    
    <div id="cerceve">
	<div class="header">歡迎登入本系統</div>
	<div class="formbody">
        <input id="username" class="validate[required] text-input" type="text" name="username" placeholder="帳號" title="帳號" maxlength="6" autocomplete="off" />
        <input id="password" class="validate[required] text-input" type="password" name="password" placeholder="••••••••••••" title="密碼" maxlength="20" autocomplete="off" />
        <input id="loginok" type="button" value="登入" title="登入" /><a href="#" id="forgot" title="忘記密碼">忘記密碼</a>
		<div id="validationDiv"></div>
	</div>
</div>
</body>
</html>
