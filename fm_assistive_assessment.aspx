<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_assistive_assessment.aspx.cs" Inherits="fm_assistive_assessment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>聽力管理 - 調頻輔具評估記錄 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/fm_assistive_assessment.css" />
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
                <asp:ScriptReference Path="~/js/fm_assistive_assessment.js" />
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
			<div id="mainClass">聽力管理&gt; 調頻輔具評估記錄</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./fm_assistive_assessment.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input type="text" id="gosrhstudentName" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>評估日期 <input id="gosrhAssessDatestart" class="date" type="text" value="" size="10" />～<input id="gosrhAssessDateend" class="date" type="text" value="" size="10" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button" onclick="search()">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="150">學生姓名</th>
			                    <th width="100">出生日期</th>
			                    <th width="140">評估日期</th>
			                    <th width="150">評估人員</th>
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
                <p align="right" id="Unit" style="background-color:#FFDF71;padding:0 10px;">&nbsp;</p>
                <p align="right">評估人員 <input id="audiologist" type="text" value=""  readonly="readonly"/>　　　　評估日期 <input id="assessDate" class="date" type="text" value="" size="10" /></p>
                <p class="cP">一、基本資料</p>
                <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">學生姓名</th>
			            <td><input id="studentName" type="text" value="" readonly="readonly"/><span id="studentID" class="hideClassSpan"></span><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td id="studentbirthday">&nbsp;</td>
		            </tr>
			    </table>
			    
                <p class="cP">二、調頻輔具基本資料</p>
                <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">輔具來源</th>
			            <td colspan="2"><label><input type="radio" name="aSource" value="1" /> 自購</label>　　
			            <label><input type="radio" name="aSource" value="2" /> 中心借用</label>　　
			            <label><input type="radio" name="aSource" value="3" /> 學校申請</label>　　
			            <label><input type="radio" name="aSource" value="4" /> 其他</label> <input id="aSourceText" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>發射器型號</th>
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
			    </table>
			    
		        <p class="cP">三、調頻輔具聲電評估結果</p>
                <table class="tableContact" width="780" border="0">
                    <thead><tr>
                        <th width="150" rowspan="2">&nbsp</th>
		                <th width="100" rowspan="2">刺激音</th>
		                <th colspan="4">左耳</th>
		                <th colspan="4">右耳</th>
                    </tr>
                    <tr>
		                <th>750 Hz</th>
		                <th>1K Hz</th>
		                <th>2K Hz</th>
		                <th>平均</th>
		                <th>750 Hz</th>
		                <th>1K Hz</th>
		                <th>2K Hz</th>
		                <th>平均</th>
                    </tr></thead>
                    <tr>
		                <th>HA Only</th>
		                <th>65 dB SPL</th>
		                <td><input id="fm1L_1" type="text" value="" size="5"/></td>
		                <td><input id="fm1L_2" type="text" value="" size="5"/></td>
		                <td><input id="fm1L_3" type="text" value="" size="5"/></td>
		                <td><input id="fm1L_4" type="text" value="" size="5"/></td>
		                <td><input id="fm1R_1" type="text" value="" size="5"/></td>
		                <td><input id="fm1R_2" type="text" value="" size="5"/></td>
		                <td><input id="fm1R_3" type="text" value="" size="5"/></td>
		                <td><input id="fm1R_4" type="text" value="" size="5"/></td>
                    </tr>
		            <tr>
		                <th>HA+FM/Tx mute</th>
		                <th>65 dB SPL</th>
		                <td><input id="fm2L_1" type="text" value="" size="5"/></td>
		                <td><input id="fm2L_2" type="text" value="" size="5"/></td>
		                <td><input id="fm2L_3" type="text" value="" size="5"/></td>
		                <td><input id="fm2L_4" type="text" value="" size="5"/></td>
		                <td><input id="fm2R_1" type="text" value="" size="5"/></td>
		                <td><input id="fm2R_2" type="text" value="" size="5"/></td>
		                <td><input id="fm2R_3" type="text" value="" size="5"/></td>
		                <td><input id="fm2R_4" type="text" value="" size="5"/></td>
		            </tr>
		            <tr>
		                <th>HA+FM/Txmic-omni</th>
		                <th>65 dB SPL</th>
		                <td><input id="fm3L_1" type="text" value="" size="5"/></td>
		                <td><input id="fm3L_2" type="text" value="" size="5"/></td>
		                <td><input id="fm3L_3" type="text" value="" size="5"/></td>
		                <td><input id="fm3L_4" type="text" value="" size="5"/></td>
		                <td><input id="fm3R_1" type="text" value="" size="5"/></td>
		                <td><input id="fm3R_2" type="text" value="" size="5"/></td>
		                <td><input id="fm3R_3" type="text" value="" size="5"/></td>
		                <td><input id="fm3R_4" type="text" value="" size="5"/></td>
		            </tr>
		            <tr>
		                <th>HA+FM/Txmic-omni</th>
		                <th>80 dB SPL</th>
		                <td><input id="fm4L_1" type="text" value="" size="5"/></td>
		                <td><input id="fm4L_2" type="text" value="" size="5"/></td>
		                <td><input id="fm4L_3" type="text" value="" size="5"/></td>
		                <td><input id="fm4L_4" type="text" value="" size="5"/></td>
		                <td><input id="fm4R_1" type="text" value="" size="5"/></td>
		                <td><input id="fm4R_2" type="text" value="" size="5"/></td>
		                <td><input id="fm4R_3" type="text" value="" size="5"/></td>
		                <td><input id="fm4R_4" type="text" value="" size="5"/></td>
		            </tr>
		            <tr>
		                <th>HA+FM/Tx mute</th>
		                <th>90 dB SPL</th>
		                <td><input id="fm5L_1" type="text" value="" size="5"/></td>
		                <td><input id="fm5L_2" type="text" value="" size="5"/></td>
		                <td><input id="fm5L_3" type="text" value="" size="5"/></td>
		                <td><input id="fm5L_4" type="text" value="" size="5"/></td>
		                <td><input id="fm5R_1" type="text" value="" size="5"/></td>
		                <td><input id="fm5R_2" type="text" value="" size="5"/></td>
		                <td><input id="fm5R_3" type="text" value="" size="5"/></td>
		                <td><input id="fm5R_4" type="text" value="" size="5"/></td>
		            </tr>
		            <tr>
		                <th>HA+FM/Txmic-omni</th>
		                <th>90 dB SPL</th>
		                <td><input id="fm6L_1" type="text" value="" size="5"/></td>
		                <td><input id="fm6L_2" type="text" value="" size="5"/></td>
		                <td><input id="fm6L_3" type="text" value="" size="5"/></td>
		                <td><input id="fm6L_4" type="text" value="" size="5"/></td>
		                <td><input id="fm6R_1" type="text" value="" size="5"/></td>
		                <td><input id="fm6R_2" type="text" value="" size="5"/></td>
		                <td><input id="fm6R_3" type="text" value="" size="5"/></td>
		                <td><input id="fm6R_4" type="text" value="" size="5"/></td>
                    </tr>
                    <tr>
                        <td colspan="10">評估結果/說明<br /><textarea id="result" cols="50" rows="5"></textarea></td>
                    </tr>
                </table>
                
		        <p class="cP">四、語音辨識結果</p>
                <table class="tableContact" width="780" border="0">
                    <thead><tr>
                        <th width="150">測試耳</th>
		                <th colspan="3">左耳</th>
		                <th colspan="3">右耳</th>
		                <th colspan="3">雙耳</th>
		            </tr>
		            <tr>
		                <th>情境(S/N dB HL)</th>
		                <th>50/0</th>
		                <th>50/50<br />(FM off)</th>
		                <th>50/50<br />(FM on)</th>
		                <th>50/0</th>
		                <th>50/50<br />(FM off)</th>
		                <th>50/50<br />(FM on)</th>
		                <th>50/0</th>
		                <th>50/50<br />(FM off)</th>
		                <th>50/50<br />(FM on)</th>
		           </tr></thead>
		           <tr>
		                <th>測試材料1</th>
		                <td colspan="9"><label><input type="radio" name="testmaterial" value="1" /> 陳小娟子母音聽辨測驗</label>　　
                        <label><input type="radio" name="testmaterial" value="2" /> WIPI </label>　　
			            <label><input type="radio" name="testmaterial" value="3" /> M-LNT</label>　　
			            <label><input type="radio" name="testmaterial" value="4" /> 中國語音均衡字彙辨識測驗</label></td>
		           </tr>
		           <tr>
		                <th>辨識正確率(%)</th>
		                <td><input id="true1L_1" type="text" value="" size="5"/></td>
		                <td><input id="true1L_2" type="text" value="" size="5"/></td>
		                <td><input id="true1L_3" type="text" value="" size="5"/></td>
		                <td><input id="true1R_1" type="text" value="" size="5"/></td>
		                <td><input id="true1R_2" type="text" value="" size="5"/></td>
		                <td><input id="true1R_3" type="text" value="" size="5"/></td>
		                <td><input id="true1D_1" type="text" value="" size="5"/></td>
		                <td><input id="true1D_2" type="text" value="" size="5"/></td>
		                <td><input id="true1D_3" type="text" value="" size="5"/></td>
		           </tr>
		           <tr>
		                <th>測試材料2</th>
		                <td colspan="9"><label><input type="radio" name="testmaterial2" value="1" /> 陳小娟子母音聽辨測驗</label>　　
			            <label><input type="radio" name="testmaterial2" value="2" /> WIPI </label>　　
			            <label><input type="radio" name="testmaterial2" value="3" /> M-LNT</label>　　
			            <label><input type="radio" name="testmaterial2" value="4" /> 中國語音均衡字彙辨識測驗</label></td>
		           </tr>
		           <tr>
		                <th>辨識正確率(%)</th>
		                <td><input id="true2L_1" type="text" value="" size="5"/></td>
		                <td><input id="true2L_2" type="text" value="" size="5"/></td>
		                <td><input id="true2L_3" type="text" value="" size="5"/></td>
		                <td><input id="true2R_1" type="text" value="" size="5"/></td>
		                <td><input id="true2R_2" type="text" value="" size="5"/></td>
		                <td><input id="true2R_3" type="text" value="" size="5"/></td>
		                <td><input id="true2D_1" type="text" value="" size="5"/></td>
		                <td><input id="true2D_2" type="text" value="" size="5"/></td>
		                <td><input id="true2D_3" type="text" value="" size="5"/></td>
		           </tr>
		           <tr>
                        <td colspan="10">評估結果/說明<br /><textarea id="result2" cols="50" rows="5"></textarea></td>
                    </tr>
                </table>
		        
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="SaveData(0)">儲 存</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button" onclick="SaveData(1)">存 檔</button>
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

