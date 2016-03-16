var MyBase = new Base();
var noEmptyItem = ["vsDate", "vsStartTime", "vsEndTime"];
var noEmptyShow = ["日期", "起訖時間-起", "起訖時間-訖"];
var _ColumnID = 0;
var _ReturnValue;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
   

    $('.serviceBegin, .serviceEnd').timePicker({
        startTime: new Date(0, 0, 0, 7, 0, 0),
        endTime: new Date(0, 0, 0, 21, 00, 0),
        //show24Hours: false,
        separator: ':',
        step: 30
    });

});



function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "createVolunteerServiceDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "setVolunteerServiceDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "delVolunteerServiceDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                window.location.reload();
            }
            break;

        case "searchVolunteerDataBaseCount":
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
                        AspAjax.searchVolunteerDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchVolunteerDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += ' <tr id="HS_' + result[i].volunteerId + '" class="sStaffData" >' +
			                    '<td>' + result[i].volunteerId + '</td>' +
			                    '<td>' + result[i].volunteerName + '</td>' +
	                            '<td><div class="UD"><button class="btnView" type="button" onclick="getView(' + result[i].ID + ',2)">檢 視</button> </div>' +
			                    '</td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);

                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            }
            break;
        case "getVolunteerServiceDataBaseCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#ServicePagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    //ellipse_text: "/",
                    //link_to: ,
                    callback: function(index, jq) {
                        AspAjax.getVolunteerServiceDataBase(parseInt((index + 1) * _LimitPage, 10), result[1]);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#ServiceSearchlist .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#ServiceSearchlist .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "getVolunteerServiceDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    _ReturnValue = result.Service;
                    $("#sUnit").html(_UnitList[result.sUnit]);
                    for (var i = 0; i < result.Service.length; i++) {
                        var Service = result.Service[i];
                        inner += '<tr id = "HS_' + Service.vID + '">' +
                        '<td class="ServiceDate">' + TransformADFromStringFunction(Service.vsDate) + '</td>' +
			            '<td ><input class="serviceBegin" value="' + Service.vsStartTime + '" type="text" size="10" onchange="chkChangeTime(\'HS_' + Service.vID + '\');"/> ～ ' +
			                  '<input class="serviceEnd" value="' + Service.vsEndTime + '" type="text" size="10" onchange="CountTime(\'HS_' + Service.vID + '\');"/></td>' +
			            '<td class="serviceHour">' + Service.vsHours + '</td>' +
			            '<td > <input class="vOtherContent" value ="' + Service.vsContent + '" type="text" name="name" size="35" /></td>' +
			            '<td >' +
			            '<div class="UD">' +
                            '<button class="btnView" onclick="UpData(' + Service.vID + ')" type="button" >更 新</button> ' +
                            '<button class="btnView"  type="button" onclick="DelData(\'' + Service.vID + '\')">刪 除</button>' +
                         '</div>' +
                         '<div class="SC" style="display:none">' +
                            '<button class="btnView" type="button" onclick="SaveData(' + Service.vID + ')">儲 存</button> ' +
                            '<button class="btnView" type="button" onclick="cancelUpData(\'' + Service.vID + '\',\'' + i + '\')" >取 消</button>' +
                         '</div>' +
			            '</td>' +
			         '</tr>';

                    }

                    $("#ServiceSearchlist .tableList").children("tbody").html(inner);
                    $('.date').datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("input").add("select").add("textarea").attr("disabled", true);
                    $(".serviceBegin, .serviceEnd").timePicker();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#ServiceSearchlist").children("tbody").html("查無資料");
            }
            break;
        case "getVolunteerDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    $("#volunteerId").val(result.volunteerId);
                    $("#volunteerName").val(result.volunteerName);
                    $("#sUnit").html(_UnitList[result.sUnit]);
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            } else {
                $("#inline .tableList").children("tbody").html("查無資料");
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}


function getView(id) {
    window.open("./volunteer_sign.aspx?id=" + id + "&act=2");
}


// 檢視志工服務時數
function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        _ColumnID = id;
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.getVolunteerServiceDataBaseCount(id);
        AspAjax.getVolunteerDataBase(id);
        $(".btnSave").fadeIn();
    } else if (id == null && act == 2) {


    }
}

function chkChangeTime(indexID) {
    var sEndTime = $("#" + indexID + " .serviceEnd").val();
    var sEndTime2 = $.timePicker("#" + indexID + " .serviceEnd").getTime();
    var sSratTime = $.timePicker("#" + indexID + " .serviceBegin").getTime();
    if (sEndTime.length > 0 ) { // Only update when second input has a value.
        // Calculate duration.
        if (sSratTime <= sEndTime2) {
            var duration = 1800000; //(sEndTime2 - oldTime);
            var time = $.timePicker("#" + indexID + " .serviceBegin").getTime();
            // Calculate and update the time in the second input.
            $.timePicker("#" + indexID + " .serviceEnd").setTime(new Date(new Date(time.getTime() + duration)));
            // oldTime = time;
        }
        CountTime(indexID);
    }
}


function CountTime(mainID) {
    var StartTimeAarray = new Array();
    var EndTimeAarray = new Array();
    var StartTime = $("#" + mainID + " .serviceBegin").val();
    var EndTime = $("#" + mainID + " .serviceEnd").val();
    StartTimeAarray = StartTime.split(":");
    EndTimeAarray = EndTime.split(":");

    var Servicehour = parseInt(EndTimeAarray[0]) - parseInt(StartTimeAarray[0]);
    var EndTime = parseInt(EndTimeAarray[0]) + parseInt(EndTimeAarray[1]) / 60;
    var StartTime = parseInt(StartTimeAarray[0]) + parseInt(StartTimeAarray[1]) / 60;
    
    var minus = 0;
    if (StartTime <= 12 && EndTime >= 13) {
        minus = 1;
    }
    if (Servicehour >= 0) {
        Servicehour = EndTime - StartTime - minus;
    } else {
        Servicehour = 0;
        alert("時間格式不正確");
    }

    $("#" + mainID + " .serviceHour").html(Servicehour);
}




// 新增服務時數
function SetVolunteerServiceTimeData() {
    var obj = new Object();
    obj.vID = _ColumnID;
    obj.vsDate = TransformRepublicReturnValue($("#inserTable .ServiceDate").val());
    obj.vsStartTime = $("#inserTable .serviceBegin").val();
    obj.vsEndTime = $("#inserTable .serviceEnd").val();
    obj.vsHours = $("#inserTable .serviceHour").html();
    obj.vsContent = $("#inserTable .vOtherContent").val();

    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createVolunteerServiceDataBase(obj);
    }
}



// 打開新增欄位
function InsertData() {
    $("#InsertServiceTime").show();
    $("#inserTable .date").datepick({
        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
    }).val(TodayADDateFunction());
    $("#inserTable input").attr("disabled", false)
}

//搜尋
function SearchVolunteerData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.searchVolunteerDataBaseCount(obj);
}
function UpData(TrID) {   
    $("#HS_" + TrID + " input[type=text]").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
    
}

function SaveData(TrID) {
    var obj = new Object();
    obj.ID = TrID;
    obj.vsDate = TransformRepublicReturnValue($("#HS_" + TrID + " .ServiceDate").html());
    obj.vsStartTime = $("#HS_" + TrID + " .serviceBegin").val();
    obj.vsEndTime = $("#HS_" + TrID + " .serviceEnd").val();
    obj.vsHours = $("#HS_" + TrID + " .serviceHour").html();
    obj.vsContent = $("#HS_" + TrID + " .vOtherContent").val();

    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setVolunteerServiceDataBase(obj);
    }

}
function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .serviceBegin").val(_ReturnValue[vIndex].vsStartTime);
    $("#HS_" + TrID + " .serviceEnd").val(_ReturnValue[vIndex].vsEndTime);
    $("#HS_" + TrID + " .serviceHour").html(_ReturnValue[vIndex].vsHours);
    $("#HS_" + TrID + " .vOtherContent").val(_ReturnValue[vIndex].vsContent);
    
    $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}


function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delVolunteerServiceDataBase(TrID);
    })
}