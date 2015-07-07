<%@ Page Language="C#" AutoEventWireup="true" CodeFile="hearing_isp.aspx.cs" Inherits="hearing_isp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>聽力管理 - 個案聽覺管理服務計畫(ISP) | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/All.css" />
	<link rel="stylesheet" type="text/css" href="./css/hearing_isp.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	
	<link rel="stylesheet" type="text/css" media="screen" href="./css/fancybox/jquery.fancybox-1.3.4.css" />
    <script type="text/javascript" src="./js/jquery.fancybox-1.3.4.pack.js"></script>
	<link rel="stylesheet" type="text/css" href="./css/jquery.datepick.css" />
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
    
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/hearing_isp.js"></script>
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
			<div id="mainClass">聽力管理&gt; 個案聽覺管理服務計畫(ISP)</div>
			
			<div id="mainMenuList">
			    <div id="btnSearch" class="menuTabs2">搜尋</div>
			    <a href="./hearing_isp.aspx?act=1" target="_blank"><div id="btnInsert" class="menuTabs2">新增</div></a>
			</div>
			<div id="mainContentSearch">
			    <div id="mainSearchForm">
			        <table width="780" border="0">
			            <tr>
			                <td width="260">學生姓名 <input type="text" name="name" /></td>
			                <td width="260">性　　別 <select name="name"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td>
                            <td width="260">出生日期 <input class="date" type="text" name="name" size="10" />～<input class="date" type="text" name="name" size="10" /></td>
			            </tr>
			            <tr>
			                <td>服務使用者編號 <input type="text" name="name" /></td>
			                <td>&nbsp;</td>
			                <td>&nbsp;</td>
			            </tr>
			            <tr>
			                <td colspan="3" align="center"><button class="btnSearch" type="button">查 詢</button></td>
			            </tr>
			        </table>
			    </div>
			    <div id="mainSearchList">
			        <table class="tableList" width="780" border="0">
			            <thead>
			                <tr>
			                    <th width="144">服務使用者編號</th>
			                    <th width="144">學生姓名</th>
			                    <th width="144">出生日期</th>
			                    <th width="144">擬定日期</th>
			                    <th width="144">修訂日期</th>
			                    <th width="60">功能</th>
			                </tr>
			            </thead>
			            <tbody>
			                <tr>
			                    <td>10123</td>
			                    <td>王小明</td>
			                    <td>100.01.01</td>
			                    <td>102.01.01</td>
			                    <td>102.03.11</td>
			                    <td><button class="btnView" type="button" onclick="getView(10123)">檢 視</button></td>
			                </tr>
			                <tr>
			                    <td>20123</td>
			                    <td>陳曉雯</td>
			                    <td>101.01.01</td>
			                    <td>102.03.01</td>
			                    <td>102.05.21</td>
			                    <td><button class="btnView" type="button" onclick="getView(20123)">檢 視</button></td>
			                </tr>
			            </tbody>
			        </table>
			    </div>
			</div>
			<div id="main">
			<div id="mainMenuList2">
			    <div id="item1" class="menuTabs">個案聽能評量狀況</div>
			    <div id="item2" class="menuTabs">個案輔具管理目標</div>
			</div>
			<div id="mainContent">
			    <div id="item1Content">
			        <p style="background-color:#FFDF71;padding:0 10px;">學生姓名 <input id="studentName" type="text" value="" /><span style="float:right;">台北至德</span></p>
			        <table class="tableContact" width="780" border="0">
			            <tr>
			                <td rowspan="2">計畫撰寫者 <input id="planName" type="text" value="" /></td>
			                <td>擬定日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>交接日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td rowspan="2">計畫修訂者 <input type="text" value="" /></td>
			                <td>修訂日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
			            </tr>
			            <tr>
			                <td>交接日期 <input class="date" type="text" value="" size="10" /></td>
			                <td>執行者 <input type="text" value="" /></td>
			            </tr>
			        </table>
			        
			        <table class="tableContact2" width="780" border="0">
			            <thead><tr>
			                <th>項目</th>
			                <th>評量結果/現況描述</th>
			                <th>評量者/日期</th>
			                <th>評量方式</th>
			            </tr></thead>
			            <tbody><tr>
			                <td align="center" width="80">聽力檢查</td>
			                <td width="540"><ul>
			                    <li>1. 中耳功能：左耳 <select><option value="0">未測</option>
			                        <option value="1">在正常範圍內</option>
			                        <option value="2">呈負壓</option>
			                        <option value="3">耳膜順應力低</option>
			                        <option value="4">耳膜破洞</option>
			                    </select>，右耳 <select><option value="0">未測</option>
			                        <option value="1">在正常範圍內</option>
			                        <option value="2">呈負壓</option>
			                        <option value="3">耳膜順應力低</option>
			                        <option value="4">耳膜破洞</option>
			                    </select></li>
			                    <li>2. 裸耳<br />
			                        <table class="tableContact" width="500" border="0">
			                            <thead><tr>
			                                <th>dB HL</th>
			                                <th>250</th>
			                                <th>500</th>
			                                <th>1000</th>
			                                <th>2000</th>
			                                <th>4000</th>
			                                <th>8000</th>
			                                <th>平均</th>				
			                            </tr></thead>
			                            <tbody><tr>
			                                <td>左</td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>右</td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>不分耳</td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                            </tr></tbody>
			                        </table>
			                    </li>
			                    <li>3. 助聽後<br />
			                        <table class="tableContact" width="500" border="0">
			                            <thead><tr>
			                                <th>dB HL</th>
			                                <th>250</th>
			                                <th>500</th>
			                                <th>1000</th>
			                                <th>2000</th>
			                                <th>4000</th>
			                                <th>8000</th>
			                                <th>平均</th>				
			                            </tr></thead>
			                            <tbody><tr>
			                                <td>左</td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>右</td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                            </tr>
			                            <tr>
			                                <td>不分耳</td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                                <td><input type="text" value="" size="5" /></td>
			                            </tr></tbody>
			                        </table>
			                    </li>
			                    <li>4. 其他(請說明)<br /><textarea cols="65" rows="1"></textarea></li>
			                </ul></td>
			                <td align="center" width="80"><textarea cols="5" rows="15"></textarea></td>
			                <td align="center" width="80"><textarea cols="5" rows="15"></textarea></td>
			            </tr>
			            <tr>
			                <td align="center">聽覺能力</td>
			                <td><ul>
			                    <li>1. <select><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input type="text" value="" size="50" />
			                    </li>
			                    <li>2. <select><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input type="text" value="" size="50" />
			                    </li>
			                    <li>3. <select><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input type="text" value="" size="50" />
			                    </li>
			                    <li>4. <select><option value="0">初階語音聽知覺</option><option value="1">字詞辨識率</option><option value="2">母音辨識率</option><option value="3">子音辨識率</option></select>：
			                        <input type="text" value="" size="50" />
			                    </li>
			                </ul></td>
			                <td align="center" width="80"><textarea cols="5" rows="5"></textarea></td>
			                <td align="center" width="80"><input type="text" value="" size="9" /><br /><input type="text" value="" size="9" /><br /><input type="text" value="" size="9" /><br /><input type="text" value="" size="9" /></td>
			            </tr><tr>
			                <td align="center">輔具管理</td>
			                <td><ul>
			                    <li>1. 配戴輔具的習慣與時間：
			                        <select>
			                            <option value="0">請選擇</option>
			                            <option value="1">完全不願意配戴輔具</option>
			                            <option value="2">時常拉扯輔具，且配戴時間不長</option>
			                            <option value="3">只在某些特定時間/場合配戴輔具，其餘時間不願配合</option>
			                            <option value="4">除了洗澡和睡覺時間外，能全天配戴輔具</option>
			                            <option value="5">會主動要求配戴輔具</option>
			                            <option value="6">會自行配戴及取下輔具</option>
			                        </select>
			                    </li>
			                    <li>2. 配戴及取下助聽輔具：
			                        <select>
			                            <option value="0">請選擇</option>
			                            <option value="1">尚未能自行配戴及取下輔具</option>
			                            <option value="2">會自行配戴及取下輔具，但方法不正確</option>
			                            <option value="3">會正確地配戴及取下輔具</option>
			                        </select>
			                    </li>
			                    <li>3. 反映助聽輔具的聲音輸出狀況：
			                        <select>
			                            <option value="0">請選擇</option>
			                            <option value="1">尚未能反應反映輔具聲音狀況</option>
			                            <option value="2">會反映聲音大小聲</option>
			                            <option value="3">會反映聲音清楚與否</option>
			                            <option value="4">會反映音質正常與否</option>
			                        </select>
			                    </li>
			                    <li>4. 助聽輔具的操作：
			                        <select>
			                            <option value="0">請選擇</option>
			                            <option value="1">不會自行操作輔具</option>
			                            <option value="2">會開、關助聽器(電池蓋、控制鈕)</option>
			                            <option value="3">會在適當的情境自行調整音量及或明敏度</option>
			                            <option value="4">會在適當的情境自行更換程式</option>
			                            <option value="5">會正確更換電池</option>
			                            <option value="6">會自行為輔具(電子耳、調頻系統)充電</option>
			                            <option value="7">會分辨左、右耳輔具</option>
			                            <option value="8">會主動在上課前將發射器交給老師</option>
			                        </select>
			                    </li>
			                    <li>5. 助聽輔具的簡易保養：
			                        <select>
			                            <option value="0">請選擇</option>
			                            <option value="1">尚不理解保養的概念，完全由家長負責</option>
			                            <option value="2">知道保養包配件的用途</option>
			                            <option value="3">知道水及汗水對輔具的影響</option>
			                            <option value="4">知道輔具的置放處</option>
			                            <option value="5">知道電池的置放處</option>
			                            <option value="6">知道廢棄電池的處理方式</option>
			                            <option value="7">會正確使用保養包配件清潔輔具</option>
			                            <option value="8">會把取下的助聽器輔具放在正確的置放處</option>
			                            <option value="9">會自行以正確方式將輔具放入除濕盒</option>
			                        </select>
			                    </li>
			                </ul></td>
			                <td align="center" width="80"><textarea cols="5" rows="5"></textarea></td>
			                <td align="center" width="80"><textarea cols="5" rows="5"></textarea></td>
			            </tr><tr>
			                <td align="center">綜合摘要</td>
			                <td colspan="3"><textarea cols="100" rows="5"></textarea></td>
			            </tr></tbody>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button">儲 存</button>
			            <button class="btnUpdate" type="button">更 新</button>
			            <button class="btnSaveUdapteData" type="button">存 檔</button>
			            <button class="btnCancel" type="button">取 消</button>
			        </p>
			    </div>
			    <div id="item2Content">
			        <table class="tableContact" width="780" border="0">
			        <thead><tr>
		                <th width="150" rowspan="2">長期目標</th>
		                <th width="180" rowspan="2">短期目標</th>
		                <th colspan="2">起迄日期</th>
		                <th colspan="3">成效評量</th>
		                <th width="50" rowspan="2">教學<br />決定</th>
			        </tr>
			        <tr>
			            <th>始</th>
			            <th>末</th>
			            <th>日期</th>
			            <th>方式</th>
			            <th>結果</th>
			        </tr>
			        </thead>
			        <tbody><tr>
		                <td rowspan="6"><textarea cols="15" rows="3">1. 建立全天配戴助聽輔具"</textarea></td>
		                <td><input type="text" name="name" value="1-1 能配戴輔具至少20分鐘" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" name="name" value="1-2 能在上課時間配戴輔具不拉扯(配戴1小時以上)" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="1-3 每天配戴輔具達4小時" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="1-4 每天配戴輔具達8小時" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="1-5除洗澡和睡覺時間外，能全天配戴輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="1-6 能主動要求配戴輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="5"><textarea cols="15" rows="3">2. 能正確配戴及取下輔具</textarea></td>
		                <td><input type="text" name="name" value="2-1 主要照顧者能正確替學童配戴及取下個人助聽輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" name="name" value="2-2 學童能配合並協助將助聽個人輔具配戴好" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="2-3學童會分辨左、右耳輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="2-4學童能自行正確配戴個人助聽輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="2-5 學童能自行正確取下個人助聽輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="12"><textarea cols="15" rows="3">3. 能反映輔具的聲音狀況</textarea></td>
		                <td><input type="text" name="name" value="3-1 主要照顧者會以測聽方式檢查輔具的輸出音量及/或音質是否正常" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" name="name" value="3-2 主要照顧者能每日以測聽方式檢查輔具的輸出音量及/或音質是否正常" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-3 主要照顧者能以觀察方式反應輔具的輸出音量及/或音質是否正常" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-4 學童能察覺雙側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-5學童能主動告知雙側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-6學童能察覺單側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-7學童能主動告知單側輔具沒有聲音" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-8 學童能察覺輔具音察覺輔具音量的大小" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-9 學童能主動反映輔具音量異常" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-10 學童能主動反映輔具有噪音" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-11 學童能主動反映音質異常" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="3-12 學童能清楚描述音量及音質的異常情況" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="11"><textarea cols="15" rows="3">4. 能操作助聽輔具</textarea></td>
		                <td><input type="text" name="name" value="4-1 主要照顧者能了解輔具的各個控制項，並能正確操作" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" name="name" value="4-2 學童會開、關輔具 (電池蓋、控制鈕…)" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-3 學童會正確更換電池" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-4 學童會正確連接耳模與輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-5 學童會主動在上課前將調頻系統發射器交給老師" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-6 學童會主動在上課前要求配戴個人調頻系統輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-7學童會正確連接個人輔具與調頻系統" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-8學童會正確開關個人調頻系統輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-9 學童會在適當情境更換程式/電流圖" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-10 學童會在適當情境調整音量、靈敏度" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="4-11 學童會自行為輔具充電" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
		                <td rowspan="9"><textarea cols="15" rows="3">5. 能保養輔具</textarea></td>
		                <td><input type="text" name="name" value="5-1主要照顧者能了解保養工具的用途，並能正確使用保養工具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
			        <tr>
			            <td><input type="text" name="name" value="5-2 主要照顧者能定期保養輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="5-3 學童能了解保養包工具的用途" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="5-4 學童能了解輔具保養的概念" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="5-5 學童能了解水及汗水對輔具的影響" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="5-6 學童能了解輔具及電池的置放處" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="5-7 學童能了解輔具及電池的置放方式" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="5-8 學童能了解廢棄電池的處理方法" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            <tr>
			            <td><input type="text" name="name" value="5-9 學童能正確使用保養包工具清潔輔具" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><input class="date" type="text" name="name" value="" size="10" /></td>
		                <td><select><option></option><option value="a">觀察</option><option  value="b">問答</option><option value="c">操作</option><option value="d">紙筆</option></select></td>
		                <td><select><option></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>
		                <td><select><option></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></td>
		            </tr>
		            </tbody>
                    </table>
			        
			        <table width="610" border="0" align="center">
			            <tr>
			                <td colspan="2">方式：觀察(A)　問答(B)　操作(C)　紙筆(D)　其他(E)</td>
			            </tr>
			            <tr>
			                <td width="410">結果：五次測試，達成4次或5次 (達成率80%以上)，完全達成(3)</td>
			                <td width="200" align="right">教學決定：通過目標(○)</td>
			            </tr>
			            <tr>
                        　　<td>　　　五次測試，達成2次或3次 (達成率40~60%)，部分達成(2)</td>
                        　　<td align="right">繼續目標(&empty;)</td>
                        </tr>
                        <tr>
                            <td>　　　五次測試，達成0次或1次 (達成率20%以下)，未達成(1)</td>
                            <td align="right">修正目標(&otimes;)</td>
                        </tr>
			        </table>
			        <p class="btnP">
			            <button class="btnSave" type="button">儲 存</button>
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
