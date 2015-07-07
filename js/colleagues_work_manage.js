
$(document).ready(function() {

    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $('.date').datepick();
    initPage();
});

function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function SaveData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}
function cancelInsert() {
    $(".insertDataDiv").hide();
    $(".btnAdd").fadeIn();
}

function showView(viewID) {
    var DivName = '';
    switch (viewID) {
        case 1:
            DivName = "#mainSearchList";
            break;
        case 2:
            DivName = "#mainIndexList";
            break;
        case 3:
            DivName = "#mainIndexList2";
            break;
        case 4:
            DivName = "#mainIndexList3";
            break;
        case 5:
            DivName = "#mainIndexList4";
            break;
    }
    $(DivName).fadeIn();
    $(DivName + " input[type=text]").add(DivName + " select").attr("disabled", true);
}

function viewRecord(sid) {
    var inner = '<div id="inline">' +
                    '<table class="tableList" border="0" width="500">' +
                    '<caption>出勤紀錄</caption>' +
                    '<thead>' +
		                '<tr>' +
		                    '<th width="300">時間</th>' +
		                    '<th width="200">狀態</th>' +
		                '</tr>' +
		            '</thead>' +
		                '<tr><td height="30" align="center">8:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">9:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">9:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">10:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">10:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">11:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">11:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td align="center">12:00</td>' +
		                '<td align="center">午休</td>' +
		                '</tr>' +
		                '<tr><td align="center">12:30</td>' +
		                '<td align="center">午休</td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">13:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">13:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">14:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">14:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">15:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">15:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">16:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">16:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">17:00</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td height="30" align="center">17:30</td>' +
		                '<td align="center"><select><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
                    '</table>' +
                    '</div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'hideOnOverlayClick': false,
        'onComplete': function() {
            
        }
    });
}

