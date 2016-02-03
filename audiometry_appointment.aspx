<%@ Page Language="C#" AutoEventWireup="true" CodeFile="audiometry_appointment.aspx.cs" Inherits="audiometry_appointment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>聽力管理 - 聽檢預約 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	
	
	<link rel="stylesheet" type="text/css" href="./css/chosen/chosen.css" />
    <link rel="stylesheet" type="text/css" href="./css/jquery-ui-1.8.13.custom.css" />
    
    <link rel='stylesheet' type='text/css' href='./css/jquery.weekcalendar.css' />
    
    <link rel="stylesheet" type="text/css" href="./css/audiometry_appointment.css" />
    <link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
    
    <script type='text/javascript' src='http://code.jquery.com/jquery-1.7.2.min.js'></script>
    <script type="text/javascript" src="./js/chosen.jquery.min.js"></script>
    <script type="text/javascript" src="./js/jquery-ui-1.8.13.custom.min.js"></script>
    <script type='text/javascript' src='./js/jquery.weekcalendar.js'></script>
    
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/audiometry_appointment.js"></script>
	<script type="text/javascript" src="./js/jquery.pagination.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/pagination.css" />
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
			<div id="mainClass">聽力管理&gt; 聽檢預約</div>
			
			<div id="mainContentSearch">
			    <p id="Unit" align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">&nbsp;</p>
			    <div id="calendar"></div>
			    <p align="center" class="hitP"><font color="Red">※ 每天12:00<span>PM</span>~1:00<span>PM</span>為午休時間，每週三9:00<span>AM</span>~12:00<span>AM</span>為固定開會時間，每天檢查時間只到5:30<span>PM</span>。</font></p>
	            <p align="center"><font color="Red">※ 同一時間內最多只能排兩個預約。</font></p>
	            <div id="event_edit_container" style="display:none;">
		            <form>
			            <input type="hidden" />
			            <ul>
				            <li>
					            <label>日　　期：</label><span class="date_holder"></span> 
				            </li>
				            <li>
					            <label for="start">開始時間：</label><select name="start"><option value="">請選擇開始時間</option></select>
				            </li>
				            <li>
					            <label for="end">結束時間：</label><select name="end"><option value="">請選擇結束時間</option></select>
				            </li>
				            <li>
					            <label for="author">預約人員：</label><span id="author"></span>
				            </li>
				            <li>
					            <label for="title">學生姓名：</label><input id="studentName" style="width:117px;" type="text" name="title" class="" value="" readonly="readonly" /> 
					            <span id="studentStatu" style="display:none;width:60px;"></span>
				            </li>
				            
				            <li id="item">
					            <label for="item">檢查項目：</label><select name="item" data-placeholder="選擇檢查項目..." multiple="multiple">
					            <option value="L">(L)新生諮詢</option>
					             <option value="M">(M)聽力評估</option>
					             <option value="N">(N)語音聽知覺</option>
					             <option value="P">(P)HA/CI評估</option>
					             <option value="Q">(Q)HA調整/檢修</option>
					             <option value="R">(R)HA驗證 (#25表格)</option>
					             <option value="S">(S)CI調圖/檢修</option>
					             <option value="T">(T)FM評估</option>
					             <option value="U">(U)FM調整/檢修</option>
					             <option value="V">(V)助聽輔具借用</option>
					             <option value="W">(W)ISP會議</option>
					             <option value="X">(X)其他</option>
					             <option value="-" disabled="disabled" >-----------</option>
					            <option value="A">(A)聽力評估</option>
					            <option value="B">(B)助聽器調整</option>
					            <option value="C">(C)聽力異常</option>
					            <option value="D">(D)新生諮詢</option>
					            <option value="E">(E)電子耳調圖</option>
					            <option value="F">(F)語音聽知覺</option>
					            <option value="G">(G)調頻系統檢測/評估</option>
					            <option value="H">(H)ISP會議</option>
					            <option value="I">(I)輔具功能異常</option>
					            <option value="J">(J)其他(請說明)</option>
					             <option value="K">(K)助聽輔具借用</option>
					             



					            </select>
					        </li>
					        <li id="itemOther">
					            <label for="other">其他說明：</label><input type="text" name="other" value="" />
				            </li>
				            <li>
					            <label for="content">備　　註：</label><textarea name="content"></textarea>
				            </li>
				            <li id="assess">
				                <label for="assess">評估人員：</label><select name="assess" data-placeholder="選擇評估人員..." multiple="multiple">
					            <option value=""></option></select>
				            </li>
				            <li>
					            <label for="state">狀　　態：</label><select name="state">
					            <option value="0">未完成</option>
					            <option value="1">已完成</option>
					            <option value="2">未出現</option></select>
				            </li>
			            </ul>
			            <p><br /></p>
		            </form>
	            </div>
			</div>
			<div id="main">
			    <div id="mainContent">
    			    
			    </div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
