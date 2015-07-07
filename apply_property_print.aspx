<%@ Page Language="C#" AutoEventWireup="true" CodeFile="apply_property_print.aspx.cs" Inherits="apply_property_print" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>財產管理 - 請購、請修單 | 財團法人中華民國婦聯聽障文教基金會管理後臺</title>
	<link rel="stylesheet" type="text/css" href="./css/reset.css" />
	<link rel="stylesheet" type="text/css" href="./css/apply_property_print.css" />
	<script type="text/javascript" src="./js/jquery-1.8.2.min.js"></script>
	<script type="text/javascript" src="./js/All.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick.js"></script>
	<script type="text/javascript" src="./js/jquery.datepick-zh-TW.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" runat="server">
            <Services>
                <asp:ServiceReference Path="AspAjax.asmx" />
            </Services>
            <Scripts>
                <asp:ScriptReference Path="~/js/apply_property_print.js" />
            </Scripts>
        </asp:ScriptManager>
    </form>
    <div id="container">
		<div id="header">
			<div id="logo"><img src="./images/print_logo.jpg" alt="財團法人中華民國婦聯聽障文教基金會" title="財團法人中華民國婦聯聽障文教基金會" border="0"  /></div>
			<div id="Info">財團法人中華民國婦聯聽障文教基金會
			                    <div id="applyTypeMenu"><label><input name="applyType" type="radio" value="1"/><img src="./images/choose.jpg" border="0" alt=""/>請購</label>　
                                <label><input name="applyType" type="radio" value="2"/><img src="./images/choose2.jpg" border="0" alt="" />請修</label>　　申請暨核銷單</div></div>
			<div id="UnitInfo"><label><input name="Unit" type="radio" value="1"/><img src="./images/choose.jpg" border="0" alt=""/ >總會　　</label>
                               <label><input name="Unit" type="radio" value="2"/><img src="./images/choose2.jpg" border="0" alt="" />台北至德</label>
                               <label><input name="Unit" type="radio" value="3"/><img src="./images/choose2.jpg" border="0" alt="" />台中至德</label>
                               <label><input name="Unit" type="radio" value="5"/><img src="./images/choose2.jpg" border="0" alt="" />高雄至德</label></div>
		</div>
		<div id="content">
			<div id="applyDate"><span>中華民國2014年10月13日</span><span>付款方式: <label><input name="applyPay" type="radio" value="5"/><img src="./images/choose.jpg" border="0" alt=""/>現金</label> <label><input name="applyPay" type="radio" value="5"/><img src="./images/choose2.jpg" border="0" alt=""/>支票</label></span></div>
			<div id="main">
                <table class="tableContact" width="1024" border="0" id="DetailTable">
                    <thead><tr>
                        <td width="180" height="50">品名</td>
                        <td width="60">單位</td>
                        <td width="60">數量</td>
                        <td width="150">規格</td>
                        <td width="100">估計單價</td>
                        <td width="80">總價</td>
                        <td>說明</td>
                        <td width="100">發票號碼</td>
                    </tr></thead>
                    <tbody><tr id="Detail1">
                        <td height="80" align="center">筆記本電腦用外接光碟機</td>
                        <td align="center">式</td>
                        <td align="right">1</td>
                        <td></td>
                        <td align="right">4199</td>
                        <td align="right">4199</td>
                        <td>原光碟機故障，修理費過高，不合經濟效益。</td>
                        <td></td>
                    </tr></tbody>
                    <tfoot><tr>
                        <td colspan="4">&nbsp;</td>
                        <td align="center">合計</td>
                        <td align="right" id="applySum">0</td>
                        <td align="left" colspan="2"><span>領款人簽章:</span></td>
                    </tr>
                    <tr>
                        <td align="center">合計新台幣</td>
                        <td colspan="7"><input id="sumFive" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 萬
                        <input id="sumFour" type="text" class="digit" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 仟 <input id="sumThree" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 佰
                        <input id="sumTwo" type="text" class="digit" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 拾 <input id="sumOne" class="digit" type="text" value="" size="5" readonly="readonly" disabled="disabled" autocomplete="off" /> 元　整</td>
                    </tr></tfoot>
                </table>
                <table class="tableContact2" width="1024" border="0">
                    <tr>
                        <td height="80" width="80" rowspan="2" align="center">會計<br />科目</td>
                        <td width="150" rowspan="2"></td>
                        <td width="80" rowspan="2" align="center">預算<br />金額</td>
                        <td width="150" rowspan="2"></td>
                        <td width="80" rowspan="2" align="center">備註</td>
                        <td width="200" rowspan="2"></td>
                        <td width="100" align="center">財產編號</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td width="100" align="center">管理者</td>
                        <td></td>
                    </tr>
                    <tfoot>
                    <tr>
                        <td colspan="8">1.購物$200元以內由組長簽可，$201元至$5,000元由總幹事核批，$5,001元至$20,000元由財務委員核批，<span>超過$20,000元以上請以簽呈，呈請董事長核批</span>。</td>
                    </tr>
                    <tr><td colspan="8">2.購物請務必索取統一發票或有效收據（有統一編號、店章、私章）。</td></tr>
                    <tr><td colspan="8">3.申請預支金額請於活動結束後3日內完成核銷。<span>傳真號碼2820-1826</span></td></tr>
                    </tfoot>
                </table>
                <div id="applypople"><span>申請人：</span><span>出納：</span><span>單位主管：</span><span>會計組：</span><span>總幹事：</span><span>財務委員：</span></div>
		        <br />
		        <div id="applyDate2">製表：2013/9/10<div id="applyNo"><span id="applyID"></span>　編號：會-024</div></div>
		        <div id="applyDate3"></div>
		    </div>
		 </div>
	</div>
</body>
</html>
