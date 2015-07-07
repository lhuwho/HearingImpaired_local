<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_loss_preschool.aspx.cs" Inherits="hearing_loss_preschool" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>教學管理 - 學前聽損幼兒教育課程檢核 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_loss_preschool.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    <script type="text/javascript" src="./js/base.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/hearing_loss_preschool.js"></script>
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
			<div id="mainClass">教學管理&gt; 學前聽損幼兒教育課程檢核</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./hearing_loss_preschool.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0" id="searchTable">
			            <tr>
			                <td width="260">學生姓名 <input type="text" id="gosrhstudentName" /></td>
			                <td width="260">性　　別 
			                    <select name="name" id="gosrhstudentSex">
			                        <option value="0">請選擇</option>
			                        <option value="1">男</option>
			                        <option value="2">女</option>
			                    </select>
			                </td>
                            <td width="260">出生日期 
                            <input class="date" type="text" size="10" id="gosrhbirthdaystart" value=""/>～
                            <input class="date" type="text" size="10" id="gosrhbirthdayend" value=""/></td>
			            </tr>
			            <tr>
			                <td>教師姓名 <input type="text" id="gosrhTeacherName" /></td>
			                <td>&nbsp</td>
                            <td>&nbsp</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center">
			                    <button class="btnSearch" type="button" onclick="Search()">查 詢</button>
			                </td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="100">排序</th>
			                    <th width="100">年度</th>
			                    <th width="120">授課日期</th>
			                    <th width="120">學生姓名</th>
			                    <th width="120">出生日期</th>
			                    <th width="120">年齡</th>
			                    <th width="100">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>1</td>
			                    <td>102</td>
			                    <td>103.01.01</td>
			                    <td>王小明</td>
			                    <td>98.01.01</td>
			                    <td>4歲9個月</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>2</td>
			                    <td>102</td>
			                    <td>103.01.01</td>
			                    <td>陳曉雯</td>
			                    <td>99.01.01</td>
			                    <td>3歲9個月</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
		    <div id="mainContentIndex" style="display:none;"></div>
			<div id="main">
			<div id="mainMenuList2">
			    <div id="item1" class="menuTabs">助聽輔具管理</div>
			    <div id="item2" class="menuTabs">聽覺技巧</div>
			    <div id="item3" class="menuTabs">認知</div>
			    <div id="item4" class="menuTabs">表達性語言</div>
			</div>
			<div id="mainContent">
			    <div id="item1Content">
			    <p align="right" style="background-color:#FFDF71;padding:0 10px;">台北至德</p>
			    <table class="tableText" width="780" border="0">
			        <tr>
			            <th width="150">學生姓名</th>
			            <td><input id="studentName" type="text" name="name" /></td>
			        </tr>
			         <tr>
			            <th>性　　別</th>
			            <td><label><input type="radio" name="sex" value="male" /> 男</label>　　
			                <label><input type="radio" name="sex" value="female" /> 女</label>
			            </td>
			        </tr>
			        <tr>
			            <th>出生日期</th>
			            <td><select id="year_ddl1"><option value="-1">民國年</option></select>
                            <select id="month_ddl1"><option value="-1">月</option></select>
                            <select id="day_ddl1"><option value="-1">日</option></select>
		                </td>
		            </tr>
			    </table>
			    <table class="tableContact" width="780" border="0">
			        <thead><tr>
			            <th width="20" rowspan="3">綱要</th>
			            <th rowspan="3" width="220">學習目標</th>
			            <th colspan="8">評量日期 / 配戴輔具</th>
			        </tr> 
			        <tr>
			            <td><textarea id="checkdate1" class="date checkdate" rows="3" cols="1"></textarea></td>
		                <td><textarea id="checkdate2" class="date checkdate" rows="3" cols="1"></textarea></td>
		                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
		                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
		                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
		                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
		                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
		                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			        </tr>
			        <tr>
			            <td><select id="tool1"><option></option><option value="1">HA</option></select></td>
			            <td><select id="tool2"><option></option><option value="1">HA</option></select></td>
			            <td><select><option></option><option value="1">HA</option></select></td>
			            <td><select><option></option><option value="1">HA</option></select></td>
			            <td><select><option></option><option value="1">HA</option></select></td>
			            <td><select><option></option><option value="1">HA</option></select></td>
			            <td><select><option></option><option value="1">HA</option></select></td>
			            <td><select><option></option><option value="1">HA</option></select></td>
			        </tr></thead>
			        <tr>
			            <th rowspan="2">配戴助聽輔具時間</th>
			            <td>能全天配戴助聽輔具</td>
			            <td><select id="q111"><option></option><option value="1">v</option></select></td>
			            <td><select id="q112"><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			        </tr>
			        <tr>
			            <td>能配戴輔具至少20分鐘</td>
			            <td><select id="q121"><option></option><option value="1">v</option></select></td>
			            <td><select id="q122"><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			            <td><select><option></option><option value="1">v</option></select></td>
			        </tr>
			     </table>    
			     <table class="tableText" width="780" border="0">
			        <tfoot><tr>
			            <th width="156">配戴輔具</th>
			            <td width="156">HA 助聽器</td>
			            <td width="156">CI 電子耳</td>
			            <td width="156">H+C 助聽器與電子耳</td>
			            <td width="156">&nbsp</td>
			        </tr>
			        <tr>
			            <th>評量結果</th>
			            <td>V 完全會</td>
			            <td>&Delta; 需協助或不穩定</td>
			            <td>X 完全不會</td>
			            <td>／ 本項不適用</td>
			        </tr></tfoot>
			     </table> 
			    <p class="btnP">
		            <button class="btnSave" type="button" onclick="goNext(1)">下一頁</button>
		            <button class="btnUpdate" type="button">更 新</button>
		            <button class="btnSaveUdapteData" type="button">存 檔</button>
		            <button class="btnCancel" type="button">取 消</button>
		        </p></div>
		        
		        <div id="item2Content">
			        <table class="tableContact" width="780" border="0">
			            <thead><tr>
			                <th width="150" rowspan="2" colspan="2">測驗項目</th>
			                <th colspan="8">評量日期 / 結果</th>
			            </tr> 
			            <tr>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			            </tr></thead>
			            <tr>
			                <td width="50">測驗1</td>
			                <td>察覺語音</td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			            </tr>
			            <tr>
			                <td>測驗2</td>
			                <td>察覺五音</td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			            </tr>
			         </table>    
			         <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="156">評量結果</th>
			                <td width="156">V 通過</td>
			                <td width="156">X 不通過</td>
			                <td width="156">&nbsp;</td>
			                <td width="156">&nbsp;</td>
			            </tr>
			         </table> 
			        <p class="btnP">
		                <button class="btnSave" type="button" onclick="goNext(2)">下一頁</button>
		                <button class="btnUpdate" type="button">更 新</button>
		                <button class="btnSaveUdapteData" type="button">存 檔</button>
		                <button class="btnCancel" type="button">取 消</button>
		            </p>
		        </div>
		        <div id="item3Content">
                    <table class="tableContact" width="780" border="0">
                        <thead><tr>
                            <th width="20">綱要</th>
			                <th width="200">學習目標</th>
			                <th colspan="15">能力對照圖（參考年齡）</th>
			            </tr></thead>
			            <tr>
			                <th colspan="2">副領域：注意力</th>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			            </tr>
			            <tr>
			                <td rowspan="2">注意力</td>
			                <td >能專心注意人、物及活動</td>
			                <td>0</td>
			                <td>&nbsp</td>
			                <td>1</td>
			                <td>&nbsp</td>
			                <td>2</td>
			                <td>&nbsp</td>
			                <td>3</td>
			                <td>&nbsp</td>
			                <td>4</td>
			                <td>&nbsp</td>
			                <td>5</td>
			                <td>&nbsp</td>
			                <td>6</td>
			                <td>&nbsp</td>
			                <td>7</td>
			            </tr> 
			            <tr>
			                <td>能短暫注意眼前的人、物及活動</td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			            </tr>
			         </table>    
			         <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="156">評量結果</th>
			                <td width="156">V 完全會</td>
			                <td width="156">&Delta; 需協助或不穩定</td>
			                <td width="156">X 完全不會</td>
			                <td width="156">&nbsp;</td>
			            </tr>
			         </table> 
			        <p class="btnP">
		                <button class="btnSave" type="button" onclick="goNext(3)">下一頁</button>
		                <button class="btnUpdate" type="button">更 新</button>
		                <button class="btnSaveUdapteData" type="button">存 檔</button>
		                <button class="btnCancel" type="button">取 消</button>
		            </p>
		        </div>
		        <div id="item4Content">
		            <table class="tableContact" width="780" border="0">
			            <thead><tr>
			                <th width="20" >綱要</th>
			                <th width="200">學習目標</th>
			                <th colspan="15">能力對照圖（參考年齡）</th>
			            </tr></thead>
			            <tr>
			                <th colspan="2">副領域：言語表達</th>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			                <td><textarea class="date checkdate" rows="3" cols="1"></textarea></td>
			            </tr>
			            <tr>
			                <td rowspan="2">口腔動作</td>
			                <td >能模仿或主動做出口腔的動作</td>
			                <td>0</td>
			                <td>&nbsp</td>
			                <td>1</td>
			                <td>&nbsp</td>
			                <td>2</td>
			                <td>&nbsp</td>
			                <td>3</td>
			                <td>&nbsp</td>
			                <td>4</td>
			                <td>&nbsp</td>
			                <td>5</td>
			                <td>&nbsp</td>
			                <td>6</td>
			                <td>&nbsp</td>
			                <td>7</td>
			            </tr> 
			            <tr>
			                <td>能張合嘴巴（如：張口、閉口、連續張合嘴巴）</td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			                <td><select><option></option><option>v</option></select></td>
			            </tr>
			         </table>    
			         <table class="tableText" width="780" border="0">
			            <tr>
			                <th width="156">評量結果</th>
			                <td width="156">V 完全會</td>
			                <td width="156">&Delta; 需協助或不穩定</td>
			                <td width="156">X 完全不會</td>
			                <td width="156">&nbsp;</td>
			            </tr>
			         </table> 
			        <p class="btnP">
		                <button class="btnSave" type="button" onclick="">儲 存</button>
		                <button class="btnUpdate" type="button">更 新</button>
		                <button class="btnSaveUdapteData" type="button">存 檔</button>
		                <button class="btnCancel" type="button">取 消</button>
		            </p>
		        </div>
			</div>
			</div>
		</div>
		<div id="footer"></div>
	</div>
	<div id="top"></div>
</body>
</html>
