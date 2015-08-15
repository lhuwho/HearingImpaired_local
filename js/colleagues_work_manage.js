var MyBase = new Base();

$(document).ready(function() {
AspAjax.set_defaultSucceededCallback(SucceededCallback);
//AspAjax.set_defaultFailedCallback(FailedCallback);
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $('.date').datepick();
    initPage();
});
function Search() { //主頁面 顯示名單
    if ($("#gosrhstaffBirthdayStart").val() != "") {
        $("#mainSearchList .tableList").children("tbody").empty();
        var obj = MyBase.getTextValueBase("searchTable");
        AspAjax.SearchStaffDataBaseWorkCount(obj);
    }
    else { $("#mainSearchList .tableList").children("tbody").empty();alert("請選擇日期");}

}

function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStaffDataBaseWorkCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    //ellipse_text: "/",
                    //link_to: ,
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTable");
                        AspAjax.SearchStaffDataBaseWork(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBaseWork":
            if (!(result == null || result.length == 0 || result == undefined)) {
              //  alert(result.length);
                //  if (parseInt(result[0].sID) != -1) {
                var inner = "";
                for (var i = 0; i < result.length; i++) {
                    var cardTime = '';
                    for (var j = 0; j < result[i].WorkRecord.length; j++) {
                        cardTime += result[i].WorkRecord[j].CreateFileDate + "<br>";
                    }
                    inner += '<tr>' +
                        '<td>' + result[i].StaffID + '</td>' +
                        '<td>' + result[i].StaffName + '</td>' +
                        '<td>' + cardTime + '</td>' +
                        '<td><button class="btnView" type="button" onclick="viewRecord(' + result[i].StaffID + ')">檢 視</button></td>' +
			                '</tr>';
                }
                //
                $("#mainSearchList .tableList").children("tbody").html(inner);
                //                } else {
                //                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                //                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "GetWorkRecordManage":
            if (!(result == null || result.length == 0 || result == undefined)) {
                for (var i = 0; i < result.length; i++) {
                    $("#startTime" + (i + 1)).val(result[i].StartTime);
                    $("#endTime" + (i + 1)).val(result[i].EndTime);
                    $("#vacationStyle" + (i + 1)).val(result[i].VacationType);
                }

            }

            break;
    }
}

//function UpData(TrID) {
//    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", false);
//    $("#HS_" + TrID + " .UD").hide();
//    $("#HS_" + TrID + " .SC").show();
//}
//function SaveData(TrID) {
//    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", true);
//    $("#HS_" + TrID + " .UD").show();
//    $("#HS_" + TrID + " .SC").hide();
//}
//function cancelInsert() {
//    $(".insertDataDiv").hide();
//    $(".btnAdd").fadeIn();
//}

//function showView(viewID) {
//    var DivName = '';
//    switch (viewID) {
//        case 1:
//            DivName = "#mainSearchList";
//            break;
//        case 2:
//            DivName = "#mainIndexList";
//            break;
//        case 3:
//            DivName = "#mainIndexList2";
//            break;
//        case 4:
//            DivName = "#mainIndexList3";
//            break;
//        case 5:
//            DivName = "#mainIndexList4";
//            break;
//    }
//    $(DivName).fadeIn();
//    $(DivName + " input[type=text]").add(DivName + " select").attr("disabled", true);
//}

function viewRecord(sid ) {
    var inner = '<div id="inline"><span id="StaffID" class="hideClassSpan">' + sid + '</span>' +
                    '<table class="tableList" border="0" width="500">' +
                    '<caption>出勤紀錄</caption>' +
                    '<thead>' +
		                '<tr>' +
		                    '<th width="300">時間</th>' +
		                    '<th width="200">狀態</th>' +
		                '</tr>' +
		            '</thead>' +
		                '<tr><td width="250">' +
		                '<select id="startTime1" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '<select id="endTime1" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle1" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td width="250">' +
		                '<select id="startTime2" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '<select id="endTime2" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle2" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td width="250">' +
		                '<select id="startTime3" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '<select id="endTime3" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle3" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
		                '<tr><td width="250">' +
		                '<select id="startTime4" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '<select id="endTime4" ><option value="7">07:00</option><option value="7.5">07:30</option><option value="8">08:00</option><option value="8.5">08:30</option><option value="9">09:00</option><option value="9.5">09:30</option><option value="10">10:00</option><option value="10.5">10:30</option><option value="11">11:00</option><option value="11.5">11:30</option><option value="12">12:00</option><option value="12.5">12:30</option><option value="13">13:00</option><option value="13.5">13:30</option><option value="14">14:00</option><option value="14.5">14:30</option><option value="15">15:00</option><option value="15.5">15:30</option><option value="16">16:00</option><option value="16.5">16:30</option><option value="17">17:00</option><option value="17.5">17:30</option><option value="18">18:00</option><option value="18.5">18:30</option><option value="19">19:00</option><option value="19.5">19:30</option><option value="20">20:00</option><option value="20.5">20:30</option><option value="21">21:00</option></select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle4" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">工作異動</option></select></td>' +
		                '</tr>' +
                    '</table>' +
                    '<div id="inline2">'+
                    '<button class="btnSearch" type="button">存 檔</button>' +
                    '</div></div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'hideOnOverlayClick': false,
        'onComplete': function() {

            $('button').css({ "background-color": "#F9AE56" });
            $("#inline2 .btnSearch").unbind("click").click(function() {
                var obj = new Array();
                for (var i = 1; i <= 4; i++) {
                    var data = {};
                    data.StaffID = $("#StaffID").html();
                    data.Date = TransformRepublicReturnValue($("#gosrhstaffBirthdayStart").val());
                    data.StartTime = $("#startTime" + i).val();
                    data.EndTime = $("#endTime" + i).val();
                    data.VacationType = $("#vacationStyle" + i).val();
                    if (data.VacationType != 0) {
                        obj[obj.length] = data;
                    }
                }
                if (obj.length > 0) {
                    AspAjax.SetWorkRecordManage(obj);
                    //alert(obj[0].StaffID + "---" + obj[0].Date + "---" + obj[0].StartTime);
                }

            });
        }
    });
    var getWork = {};
    getWork.StaffID = $("#StaffID").html();
    getWork.Date = TransformRepublicReturnValue($("#gosrhstaffBirthdayStart").val());
    AspAjax.GetWorkRecordManage(getWork);
}

