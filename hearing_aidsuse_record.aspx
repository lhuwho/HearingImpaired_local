<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_aidsuse_record.aspx.cs" Inherits="hearing_aidsuse_record" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>聽力管理 - 學生輔具使用記錄 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_aidsuse_record.css" />
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
                <asp:ScriptReference Path="~/js/hearing_aidsuse_record.js" />
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
			<div id="mainClass">聽力管理&gt; 學生輔具使用記錄</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./hearing_aidsuse_record.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td>
			                <td width="260">服務使用者編號 <input id="gosrhstudentID" type="text" value="" /></td>
			                <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>左輔具類型 <select id="gosrhaidstypeL"><option value="0">請選擇</option><option value="1">助聽器</option><option value="2">人工電子耳</option><option value="3">其他</option></select></td>
			                <td>右輔具類型 <select id="gosrhaidstypeR"><option value="0">請選擇</option><option value="1">助聽器</option><option value="2">人工電子耳</option><option value="3">其他</option></select></td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search();">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="90">服務使用者編號</th>
			                    <th width="70">學生姓名</th>
			                    <th width="80">左輔具類型</th>
			                    <th width="90">左選配/植入日期</th>
			                    <th width="80">右輔具類型</th>		                    
			                    <th width="90">右選配/植入日期</th>
			                    <th width="90">左FM輔具類型</th>
			                    <th width="90">右FM輔具類型</th>
			                    <th width="90">使用日期</th>
			                    <th width="60">功能</th>
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
			    <p align="right" id="caseUnit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">服務使用者編號</th>
			            <td width="240"><input id="studentID" type="text" value="" readonly="readonly"/><span class="startMark">*</span></td>
			            <th width="150">學生姓名</th>
			            <td width="240" id="studentName"></td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td id="studentbirthday"></td>
			            <th>年　　齡</th>
			            <td><input id="studentAge" type="text" size="5" readonly="readonly"/> 歲 <input id="studentMonth" type="text" size="5" readonly="readonly"/> 月</td>
			        </tr>
			        <tr>
			            
			            <th>使用日期</th>
			            <td><input id="assessDate" class="date" type="text" value="" size="10" /></td>
			        </tr>
			         
			    </table>
			    
			    <p><br />一、個人輔具資料</p>
			    <table class="tableContact" width="780" border="0">
			        <tr>
			            <th width="100">&nbsp;</th>
			            <th>左</th>
			            <th>右</th>
			        </tr>
			        <tr>
			            <th>輔具類型</th>
			            <td><label><input type="radio" name="assistmanageL" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageL" value="2" /> 電子耳</label>　
			            </td>
			            <td><label><input type="radio" name="assistmanageR" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageR" value="2" /> 電子耳</label>　
			            </td>
			        </tr>
			        <tr>
			            <th>選配/植入日期</th>
			            <td><input id="buyingtimeL" class="date" type="text" value="" size="10" /></td>
			            <td><input id="buyingtimeR" class="date" type="text" value="" size="10" /></td>
			        </tr>
			        <tr>
			            <th>選配/植入地點</th>
			            <td><input id="buyingPlaceL" type="text" value="" /></td>
			            <td><input id="buyingPlaceR" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>植入醫師</th>
			            <td><input id="insertHospitalL" type="text" value="" /></td>
			            <td><input id="insertHospitalR" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>開頻日期</th>
			            <td><input id="openHzDateL" class="date" type="text" value="" /></td>
			            <td><input id="openHzDateR" class="date" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>廠牌</th>
			            <td><select id="brandL"><option value="0">請選擇輔具類型</option></select></td>
			            <td><select id="brandR"><option value="0">請選擇輔具類型</option></select></td>
			        </tr>
			        <tr>
			            <th>型號</th>
			            <td><input id="modelL" type="text" value="" /></td>
			            <td><input id="modelR" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>輔具來源</th>
			            <td><label><input type="radio" name="sourceL" value="1" /> 自購</label>　
			                <label><input type="radio" name="sourceL" value="2" /> 中心租借</label>　
			                <label><input type="radio" name="sourceL" value="3" /> 其他</label> <input id="sourceTextL" type="text" value="" size="10" />
			            </td>
			            <td><label><input type="radio" name="sourceR" value="1" /> 自購</label>　
			                <label><input type="radio" name="sourceR" value="2" /> 中心租借</label>　
			                <label><input type="radio" name="sourceR" value="3" /> 其他</label> <input id="sourceTextR" type="text" value="" size="10" />
			            </td>
			        </tr>
			    </table>
			    
			    <p><br />二、調頻系統資料</p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="100">取得日期</th>
			            <td colspan="2"><input id="gainDate" class="date" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>輔具來源</th>
			            <td colspan="2"><label><input type="radio" name="fmAidssource" value="1" /> 自購</label>　　
			                <label><input type="radio" name="fmAidssource" value="2" /> 中心租借</label>　　
			                <label><input type="radio" name="aidssource3" value="3" /> 其他</label>
			            </td>
			        </tr>
			        <tr>
			            <th>輔具類型</th>
			            <td colspan="2"><label><input type="radio" name="fmAidstype" value="1" /> 發射器</label>　　
			                <label><input type="radio" name="fmAidstype" value="2" /> 左接收器</label>　　
			                <label><input type="radio" name="fmAidstype" value="3" /> 右接收器</label>
			            </td>
			        </tr>
			        <tr>
			            <th>廠牌</th>
			            <td colspan="2"><select id="fmBrand"></select></td>
			        </tr>
			        <tr>
			            <th>型號</th>
			            <td colspan="2"><input id="fmModel" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>頻道</th>
			            <td colspan="2"><input id="fmChannel" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>&nbsp</th>
			            <th>左耳</th>
			            <th>右耳</th>
			        </tr>
			        <tr>
			            <th>輔具型號</th>
			            <td align="center"><input id="fmAidstypeL" type="text" value="" /></td>
			            <td align="center"><input id="fmAidstypeR" type="text" value="" /></td>
			        </tr>
			         <tr>
			            <th>DPAI設定</th>
			            <td align="center"><label><input type="radio" name="DPAIsettingL" value="1" /> 是</label>　　
			            <label><input type="radio" name="DPAIsettingL" value="2" /> 否</label>　　
			            <label><input type="radio" name="DPAIsettingL" value="3" /> 不適用</label></td>
			            <td align="center"><label><input type="radio" name="DPAIsettingR" value="1" /> 是</label>　　
			            <label><input type="radio" name="DPAIsettingR" value="2" /> 否</label>　　
			            <label><input type="radio" name="DPAIsettingR" value="3" /> 不適用</label></td>
			        </tr>
			        <tr>
			            <th>程式設定</th>
			            <td align="center"><input id="fmProgramL" type="text" value="" /></td>
			            <td align="center"><input id="fmProgramR" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>音源比例</th>
			            <td align="center"><input id="fmAudioL" type="text" value="" /></td>
			            <td align="center"><input id="fmAudioR" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>音靴/介面</th>
			            <td align="center"><label><input type="radio" name="fmUIL" value="1" /> 有</label> <input id="fmUITextL" type="text" value="" size="10" />　　
			                <label><input type="radio" name="fmUIL" value="2" /> 無</label>　　
			                <label><input type="radio" name="fmUIL" value="3" /> 不適用</label></td>
			            <td align="center"><label><input type="radio" name="fmUIR" value="1" /> 有</label> <input id="fmUITextR" type="text" value="" size="10" />　　
			                <label><input type="radio" name="fmUIR" value="2" /> 無</label>　　
			                <label><input type="radio" name="fmUIR" value="3" /> 不適用</label></td>
			        </tr>
			        <tr>
			            <th>接收器型號</th>
			            <td align="center"><input id="fmReceptorL" type="text" value="" /></td>
			            <td align="center"><input id="fmReceptorR" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>接收器音量設定</th>
			            <td align="center"><input id="fmVolumeL" type="text" value="" /></td>
			            <td align="center"><input id="fmVolumeR" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>增益設定</th>
			            <td align="center"><input id="fmGainL" type="text" value="" /></td>
			            <td align="center"><input id="fmGainR" type="text" value="" /></td>
			        </tr>
			    </table>
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="saveData(0);">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="saveData(1);">存 檔</button>
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

