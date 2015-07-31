<%@ Page Language="C#" AutoEventWireup="true" CodeFile="classrooms_schedule.aspx.cs" Inherits="classrooms_schedule" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>教學管理 - 教室使用時間表 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
    <link rel="stylesheet" type="text/css" href="./css/classrooms_schedule.css" />
    
    <link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/classrooms_schedule.js"></script>
	
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
			<div id="mainClass">教學管理&gt; 教室使用時間表</div>
			<div id="main">
			    <div id="mainContent">
			        <p align="right" style="background-color:#FFDF71;padding:0 10px;margin-bottom:10px;">台北至德</p>
			        <p align="center"><input style="float:left;" type="button" value="上一週" onclick="getLastWeek()" /><input type="button" value="本週" onclick="getThisWeek()" /><input style="float:right;" type="button" value="下一週" onclick="getNextWeek()" /><br /><br /></p>
			        <div id="weekTable">
			            <table class="tableContact" width="780" border="0">
			                <tr>
			                    <th width="80"><span style="float:right;">日期</span><br /><span style="float:left;">教室名稱</span></th>
			                    <th width="100"><span class="weekDate1"></span><br />星期一</th>
			                    <th width="100"><span class="weekDate2"></span><br />星期二</th>
			                    <th width="100"><span class="weekDate3"></span><br />星期三</th>
			                    <th width="100"><span class="weekDate4"></span><br />星期四</th>
			                    <th width="100"><span class="weekDate5"></span><br />星期五</th>
			                    <th width="100"><span class="weekDate6"></span><br />星期六</th>
			                    <th width="100"><span class="weekDate7"></span><br />星期日</th>
			                </tr>
			                <tr>
			                    <th>E01</th>
			                    <td>
			                        <div class="rooms">&nbsp;</div> <!-- 1 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 5 -->
			                        <div class="rooms">陳淑玲</div> <!-- 2 -->
			                        <div class="rooms">&nbsp;</div> <!-- 6 -->
			                        <div class="rooms"><span>張延齡</span></div> <!-- 3 -->
			                        <div class="rooms">李雲雲</div> <!-- 7 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 4 -->
			                        <div class="rooms">陳淑玲</div> <!-- 8 -->
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">黃信義</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張延齡</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">陳淑玲</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                </tr>
			                <tr>
			                    <th>E02</th>
			                    <td>
			                        <div class="rooms">&nbsp;</div> <!-- 1 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 5 -->
			                        <div class="rooms">陳淑玲</div> <!-- 2 -->
			                        <div class="rooms">&nbsp;</div> <!-- 6 -->
			                        <div class="rooms"><span>張延齡</span></div> <!-- 3 -->
			                        <div class="rooms">李雲雲</div> <!-- 7 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 4 -->
			                        <div class="rooms">陳淑玲</div> <!-- 8 -->
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">黃信義</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張延齡</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">陳淑玲</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                </tr>
			                <tr>
			                    <th>E03</th>
			                    <td>
			                        <div class="rooms">&nbsp;</div> <!-- 1 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 5 -->
			                        <div class="rooms">陳淑玲</div> <!-- 2 -->
			                        <div class="rooms">&nbsp;</div> <!-- 6 -->
			                        <div class="rooms"><span>張延齡</span></div> <!-- 3 -->
			                        <div class="rooms">李雲雲</div> <!-- 7 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 4 -->
			                        <div class="rooms">陳淑玲</div> <!-- 8 -->
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">黃信義</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張延齡</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">陳淑玲</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                </tr>
			                <tr>
			                    <th>E04</th>
			                    <td>
			                        <div class="rooms">&nbsp;</div> <!-- 1 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 5 -->
			                        <div class="rooms">陳淑玲</div> <!-- 2 -->
			                        <div class="rooms">&nbsp;</div> <!-- 6 -->
			                        <div class="rooms"><span>張延齡</span></div> <!-- 3 -->
			                        <div class="rooms">李雲雲</div> <!-- 7 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 4 -->
			                        <div class="rooms">陳淑玲</div> <!-- 8 -->
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">黃信義</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張延齡</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">陳淑玲</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                </tr>
			                <tr>
			                    <th>E05</th>
			                    <td>
			                        <div class="rooms">&nbsp;</div> <!-- 1 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 5 -->
			                        <div class="rooms">陳淑玲</div> <!-- 2 -->
			                        <div class="rooms">&nbsp;</div> <!-- 6 -->
			                        <div class="rooms"><span>張延齡</span></div> <!-- 3 -->
			                        <div class="rooms">李雲雲</div> <!-- 7 -->
			                        <div class="rooms"><span>黃信義</span></div> <!-- 4 -->
			                        <div class="rooms">陳淑玲</div> <!-- 8 -->
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">黃信義</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張延齡</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms">李雲雲</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>張延齡</span></div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">陳淑玲</div>
			                        <div class="rooms">張清清</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>李雲雲</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms"><span>陳淑玲</span></div>
			                        <div class="rooms"><span>黃信義</span></div>
			                        <div class="rooms">張延齡</div>
			                        <div class="rooms">陳淑玲</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                    <td>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                        <div class="rooms">&nbsp;</div>
			                    </td>
			                </tr>
			            </table>
			        </div>
		        </div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>