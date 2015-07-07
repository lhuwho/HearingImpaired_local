<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_inspection.aspx.cs" Inherits="hearing_inspection" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>聽力管理 - 聽力檢查記錄 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_inspection.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/jquery.form.js"></script>
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
                <asp:ScriptReference Path="~/js/hearing_inspection.js" />
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
			<div id="mainClass">聽力管理&gt; 聽力檢查記錄</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./hearing_inspection.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td>
			                <td width="260">性　　別 <select id="gosrhstudentSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td>
			            </tr>
			            <tr>
			                <td>檢查日期 <input id="gosrhcheckDatestart" class="date" type="text" value="" size="10" />～<input id="gosrhcheckDateend" class="date" type="text" value="" size="10" /></td>
			                <td>&nbsp;</td>
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
			                    <th width="100">學生姓名</th>
			                    <th width="100">出生日期</th>
			                    <th width="140">檢查日期</th>
			                    <th width="100">檢查方式</th>
			                    <th width="100">聽力師</th>
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
			    <p align="right">聽力師 <input id="audiologistName" type="text" value="" />　　　　檢查日期 <input id="checkDate" class="date" type="text" value="" size="10" /></p>
			    <p class="cP">一、基本資料</p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">學生姓名</th>
			            <td><input id="studentName" type="text" value="" readonly="readonly"/><span id="studentID" class="hideClassSpan"></span><span class="startMark">*</span></td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td id="studentbirthday"></td>
		            </tr>
			        <tr>
			            <th>檢查方式</th>
			            <td>
			                <select id="checkMode">
			                    <option value="0">行為觀察</option>
			                    <option value="1">視覺增強</option>
			                    <option value="2">遊戲制約</option>
			                    <option value="3">標準測試</option>
			                </select>
			                
			            </td>
			        </tr>
		            <tr>
			            <th>可信度</th>
			            <td><label><input type="radio" name="credibility" value="1" /> 佳</label>　　
			            <label><input type="radio" name="credibility" value="2" /> 普通</label>　　
			            <label><input type="radio" name="credibility" value="3" /> 差</label> </td>
			        </tr>
			        <tr>
			            <th>輔具型式與序號</th>
			            <td>右耳：<label><input type="radio" name="assistmanageR" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageR" value="2" /> 電子耳</label>　
			                廠牌 <select id="brandR" ><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelR" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeR" value="" /> 選配/植入地點 <input type="text"  id="buyingPlaceR" value="" /><br />　　　
			                植入醫院醫生 <input type="text"  id="insertHospitalR" size="15" value="" /> 開頻日 <input class="date" type="text" id="openHzDateR" value="" size="10" /><br />
			                左耳：<label><input type="radio" name="assistmanageL" value="1" /> 助聽器</label>　<label><input type="radio" name="assistmanageL" value="2" /> 電子耳</label>　
			                廠牌<select id="brandL"><option value="0">請選擇輔具類型</option></select>　型號<input type="text" id="modelL" value="" /><br />　　　
			                選配/植入時間 <input type="text" class="date" id="buyingtimeL" value="" /> 選配/植入地點 <input type="text" id="buyingPlaceL" value="" /><br />　　　
			                植入醫院醫生 <input type="text" id="insertHospitalL" value="" size="15" /> 開頻日 <input class="date" type="text" id="openHzDateL" value="" size="10" /><br />
			                </td>
			        </tr>
			     </table>
			        
			     <p class="cP">二、純音聽力檢查</p>
			     <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">聽檢儀</th>
			            <td>　　
			                <label><input type="radio" name="headphone" value="1" /> 頭戴式耳機</label>　　
			                <label><input type="radio" name="headphone" value="2" /> 插入式耳機</label>
			                <label><input type="radio" name="headphone" value="3" /> 插入式耳機+耳膜</label>
			            </td>
			        </tr>
			        <tr>
			            <th>裸耳</th>
			            <td>
			                <label><input type="radio" name="nudetonety" value="0" /> 請選擇</label>　　
			                <label><input type="radio" name="nudetonety" value="1" /> 純音</label>　　
			                <label><input type="radio" name="nudetone" value="2" /> 顫音</label>　　
			                <label><input type="radio" name="nudetone" value="3" /> 窄頻音</label>
			            </td>
			        </tr>
			        <tr>
			            <th>助聽後</th>
			            <td>
			                <label><input type="radio" name="hearingtone" value="0" /> 請選擇</label>　　
			                <label><input type="radio" name="hearingtone" value="1" /> 純音</label>　　
			                <label><input type="radio" name="hearingtone" value="2" /> 顫音</label>　　
			                <label><input type="radio" name="hearingtone" value="3" /> 窄頻音</label>
			            </td>
			        </tr>
			         <tr>
			            <th>純音聽力圖</th>
			            <td>
			                <form id="GmyForm" action="" method="post" enctype="multipart/form-data">
			                    <input type="file" name="hearingtoneImg" size="1" autocomplete="OFF"/><br /> 
			                </form>
			                <a class="hearingtoneImgUrl" href="./images/noPhoto.jpg"><img id="hearingtoneImg" src="./images/noPhoto.jpg" alt="" border="0" /></a>
			                <p>純音平均閾值(500, 1000, 2000 Hz之平均值)：</p>
                            右耳 <input id="toneR" type="text" value="" size="10" />分貝，左耳 <input id="toneL" type="text" value="" size="10" />分貝
                            
                            <table class="tableContact" width="300" border="0" style="line-height:20px;">
                                <caption>聽力圖符號解釋</caption>
                                <tr>
                                    <th width="90">&nbsp;</th>
                                    <th width="70">左</th>
                                    <th width="70">右</th>
                                    <th width="70">不分耳</th>
                                </tr>
                                <tr>
                                    <th>氣導</th>
                                    <td>╳</td>
                                    <td>○</td>
                                    <td>S</td>
                                </tr>
                                <tr>
                                    <th>氣導遮蔽</th>
                                    <td><font size="5" style="position:relative;top:-4px;">□</font></td>
                                    <td><font size="6">△</font></td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <th>骨導</th>
                                    <td><font size="4">＞</font></td>
                                    <td><font size="4">＜</font></td>
                                    <td><font size="4">^</font></td>
                                </tr>
                                <tr>
                                    <th>骨導遮蔽</th>
                                    <td>]</td>
                                    <td>[</td>
                                    <td>&nbsp;</td>
                                </tr>
                                <tr>
                                    <th>助聽器</th>
                                    <td>A<font size="1">L</font></td>
                                    <td>A<font size="1">R</font></td>
                                    <td>A</td>
                                </tr>
                                <tr>
                                    <th>電子耳</th>
                                    <td>C<font size="1">L</font></td>
                                    <td>C<font size="1">R</font></td>
                                    <td>C</td>
                                </tr>
                                <tr>
                                    <th>無反應</th>
                                    <td>↘</td>
                                    <td>↙</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
			            </td>
			        </tr>
			        <tr>
			            <th>耳視鏡檢視</th>
			            <td>
			                左：
			                <label><input type="radio" name="hearingtoneR" value="0" /> 請選擇</label>
			                <label><input type="radio" name="hearingtoneR" value="1" /> 正常</label>／
			                <label><input type="radio" name="hearingtoneR" value="2" /> 異常</label>　　
			                <input id="hearingtoneRText" type="text" value="" />
			                <br />
                            右：
                            <label><input type="radio" name="hearingtoneL" value="0" /> 請選擇</label>
                            <label><input type="radio" name="hearingtoneL" value="1" /> 正常</label>／
			                <label><input type="radio" name="hearingtoneL" value="2" /> 異常</label>　　
			                <input id="hearingtoneLText" type="text" value="" /></td>
			        </tr>
			        <tr>
			            <th>中耳鼓室圖</th>
			            <td>
			                <table class="tableContact" width="600" border="0">
			                <tr>
			                    <th width="150">&nbsp</th>
			                    <th>左</th>
			                    <th>右</th>
			                </tr>
			                <tr>
			                    <th>圖型</th>
			                    <td><input id="hearingImgL" type="text" vlaue="" /></td>
			                    <td><input id="hearingImgR" type="text" vlaue="" /></td>
			                </tr>
			                <tr>
			                    <th>耳道容積 (c.c.)</th>
			                    <td><input id="hearingVolumeL" type="text" vlaue="" /></td>
			                    <td><input id="hearingVolumeR" type="text" vlaue="" /></td>
			                </tr>
			                <tr>
			                    <th>耳膜順應 (c.c.)</th>
			                    <td><input id="conformL" type="text" vlaue="" /></td>
			                    <td><input id="conformR" type="text" vlaue="" /></td>
			                </tr>
			                <tr>
			                    <th>尖峰壓力 (daPa)</th>
			                    <td><input id="pressureL" type="text" vlaue="" /></td>
			                    <td><input id="pressureR" type="text" vlaue="" /></td>
			                </tr>
			                </table>
			            </td>
			        </tr>
			        <tr>
			            <th>輔具檢測</th>
			            <td>
			                左：
			                
			                <label><input type="checkbox" name="aidsL" value="1" /> 功能</label>／
			                <label><input type="checkbox" name="aidsL" value="2" /> 增益</label>　
			                　
			                <label><input type="radio" name="aidsdetectL" value="1" /> 正常</label>／
			                <label><input type="radio" name="aidsdetectL" value="2" /> 異常</label><br />
                            右：
                            
                            <label><input type="checkbox" name="aidsR" value="1" /> 功能</label>／
			                <label><input type="checkbox" name="aidsR" value="2" /> 增益</label>　
			                　
			                <label><input type="radio" name="aidsdetectR" value="1" /> 正常</label>／
			                <label><input type="radio" name="aidsdetectR" value="2" /> 異常</label><br />
			                其他 <input id="aidsOther" type="text" value="" />
			            </td>
			        </tr>
			    </table>
			        
			     <p class="cP">三、語音聽力檢查</p>
			     <table class="tableText2" width="780" border="0">
			        <tr style="background-color:#F9AE56;">
			            <th width="150">測驗項目</th>
			            <th>材料</th>
			            <th width="130">裸耳/助聽後</th>
			            <th width="80px;">音量/噪音</th>
			            <th width="280px;">結果</th>
			        </tr>
			        <tr>
			            <td colspan="1">語音察覺閾 (SAT)</td>
			            <td colspan="1"><input id="material1" type="text" value="" /></td>
			            <td>
			                <label>裸耳</label> 
			            </td>
			            <td>
			                <input id="SATVolumeBefore" type="text" value="" class="middleInput" />  
			            </td>
			            <td>
			                右耳:<input id="SATREarBefore" type="text" value="" class="shortInput" />
			                左耳:<input id="SATLEarBefore" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="SATEarBefore" type="text" value="" class="shortInput" />
			            </td>
			        </tr>
			        <tr>
			            <td></td>
			            <td></td>
			            <td>
			                <label>助聽後</label>
			            </td>
			            <td>
			                <input id="SATVolumeAfter" type="text" value=""  class="middleInput"/>
			            </td>
			            <td>
			                右耳:<input id="SATREarAfter" type="text" value="" class="shortInput" />
			                左耳:<input id="SATLEarAfter" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="SATEarAfter" type="text" value="" class="shortInput"/>
			            </td>
			        </tr>
			        <tr>
			            <td>字詞辨識率 (WRS)</td>
			            <td>
			                <select id="material2">
			                    <option value="0">M-LNT</option>
			                    <option value="1">M-WIPI</option>
			                    <option value="2">ESP</option>
			                    <option value="3">中國語音均衡字彙聽辨測驗</option>
			                    <option value="4">陳小娟子母音聽辨測驗</option>
			                    <option value="5">華語單字音語音聽辨測驗(LIST A)</option>
			                    <option value="6">華語單字音語音聽辨測驗(LIST B)</option>
			                </select>
			               
			            </td>
			            <td>
			                <label>裸耳</label> 
			            </td>
			            <td>
			                <input id="WRSVolumeBefore" type="text" value="" class="middleInput" />  
			            </td>
			            <td>
			                右耳:<input id="WRSREarBefore" type="text" value="" class="shortInput" />
			                左耳:<input id="WRSLEarBefore" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="WRSEarBefore" type="text" value="" class="shortInput" />
			            </td>
			        </tr>
			        <tr>
                        <td></td>
			            <td></td>
			            <td>
			                <label>助聽後</label>
			            </td>
			            <td>
			                <input id="WRSVolumeAfter" type="text" value=""  class="middleInput"/>
			            </td>
			            <td>
			                右耳:<input id="WRSREarAfter" type="text" value="" class="shortInput" />
			                左耳:<input id="WRSLEarAfter" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="WRSEarAfter" type="text" value="" class="shortInput"/>
			            </td>
                    </tr>
			        <tr>
                        <td><input id="project3" type="text" value="" /></td>
                        <td><input id="material3" type="text" value="" /></td>
			            <td>
			                <label>裸耳</label> 
			            </td>
			            <td>
			                <input id="project3VolumeBefore" type="text" value="" class="middleInput" />  
			            </td>
			            <td>
			                右耳:<input id="project3REarBefore" type="text" value="" class="shortInput" />
			                左耳:<input id="project3LEarBefore" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="project3EarBefore" type="text" value="" class="shortInput" />
			            </td>
                    </tr>
                    <tr>
                        <td></td>
			            <td></td>
			            <td>
			                <label>助聽後</label>
			            </td>
			            <td>
			                <input id="project3VolumeAfter" type="text" value=""  class="middleInput"/>
			            </td>
			            <td>
			                右耳:<input id="project3REarAfter" type="text" value="" class="shortInput" />
			                左耳:<input id="project3LEarAfter" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="project3EarAfter" type="text" value="" class="shortInput"/>
			            </td>
                    </tr>
                    <tr>
			            <td><input id="project4" type="text" value="" /></td>
			            <td><input id="material4" type="text" value="" /></td>
			            <td>
			                <label> 裸耳</label> 
			               
			            </td>
			            <td><input id="project4VolumeBefore" type="text" value=""  class="middleInput"/></td>
			            <td>
			                右耳:<input id="project4REarBefore" type="text" value="" class="shortInput" />
			                左耳:<input id="project4LEarBefore" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="project4EarBefore" type="text" value="" class="shortInput"/>
			            </td>
			        </tr>
			        <tr>
                        <td></td>
			            <td></td>
			            <td>
			                <label>助聽後</label>
			            </td>
			            <td>
			                <input id="project4VolumeAfter" type="text" value=""  class="middleInput"/>
			            </td>
			            <td>
			                右耳:<input id="project4REarAfter" type="text" value="" class="shortInput" />
			                左耳:<input id="project4LEarAfter" type="text" value="" class="shortInput" /> 
			                不分耳:<input id="project4EarAfter" type="text" value="" class="shortInput"/>
			            </td>
                    </tr>
			     </table>
			     <table id="CheckPurposeDIV">
			        <tr>
			            <th width="150">檢測目的</th>
			            <td colspan="4">
			                <p align="left"><select id="checkPurpose">
			                <option value="0">請選擇</option><option value="1">新生入學</option>
			                <option value="2">例行聽檢</option><option value="3">輔具驗證</option>
			                <option value="4">其他</option></select>
			                <input id="checkPurposeText" type="text" value="" /></p>
			            </td>
			        </tr>
			        <tr>
			            <th width="150">說明及建議</th>
			            <td colspan="4"><textarea id="explain" rows="3" cols="50"></textarea></td>
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

