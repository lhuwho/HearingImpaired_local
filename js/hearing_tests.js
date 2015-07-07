var MyBase = new Base();
var noEmptyItem = ["studentID"];
var noEmptyShow = ["服務使用者編號抓取錯誤，請重新選擇學生"];
var _ColumnID = 0;
var _ReturnValue;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();

    $(".btnSearch").click(function() {
        var stuID = $("#studentID").html();
        if (stuID.length > 0) {
            AspAjax.getHearingTestDataBase(stuID);
            //$("#mainSearchList input[type=text]").attr("disabled", true);
        } else {
            alert("請選擇學生");
        }
    });

    $(".btnAdd").click(function() {
        $("#insertDataDiv input[type=text]").attr("disabled", false);
    });

    $("#studentName").unbind("click").click(function() {
        callStudentSearchfunction();
    });
   
});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStudentDataBaseCount":
            var obj = MyBase.getTextValueBase("searchStuinline");
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#smainPagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    //ellipse_text: "/",
                    //link_to: ,
                    callback: function(index, jq) {
                        AspAjax.SearchStudentDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>發生錯誤</td></tr>");
            }
            break;
        case "SearchStudentDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs">' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + _SexList[result[i].txtstudentSex] + '</td>' +
			                    '<td><span style="display:none;">' + result[i].ID + '</span>' + TransformADFromDateFunction(result[i].txtstudentbirthday) + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        $("#studentID").html($(this).children("td:nth-child(1)").html());
                        $("#studentName").val($(this).children("td:nth-child(2)").html());
                        $("#studentSex").html($(this).children("td:nth-child(3)").html());
                        $("#studentbirthday").html($(this).children("td:nth-child(4)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "getHearingTestDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = '';
                    var item1 = "語音察覺閾 (SAT)";
                    var item2 = "字詞辨識率 (WRS)";
                    _ReturnValue = result;
                    for (var i = 0; i < result.length; i++) {

                        var item1readonly = '';
                        if (result[i].project == item1 || result[i].project == item2) {
                            item1readonly = 'readonly="readonly"';
                        }
                        inner += '<tr id="HS_' + result[i].ID + 'HS_' + result[i].itemNumber + '">' +
			                    '<td>' + TransformADFromStringFunction(result[i].checkDate) + '</td>' +
			                    '<td><select class="voice"><option value="0">請選擇</option><option value="1">裸耳</option><option value="2">助聽器</option></select></td>' +
			                    '<td><select class="state"><option value="0">請選擇</option><option value="1">左耳</option><option value="2">右耳</option><option value="3">不分耳</option></select></td>' +
			                    '<td><input class="project" type="text" value="' + result[i].project + '" size="12" ' + item1readonly + '/></td>' +
			                    '<td><input class="material" type="text" value="' + result[i].material + '" size="9" /></td>' +
			                    '<td><input class="volume" type="text" value="' + result[i].volume + '" size="10"/></td>' +
			                    '<td><input class="result" type="text" value="' + result[i].result + '" size="12"/></td>' +
			                    '<td><input class="remark" type="text" value="' + result[i].remark + '" size="12"/></td>' +
			                    '<td><div class="UD"><button class="btnUpdateSmall" type="button" onclick="UpData(\'' + result[i].ID + '\',\'' + result[i].itemNumber + '\')">更 新</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnSaveSmall" type="button" onclick="SaveData(\'' + result[i].ID + '\',\'' + result[i].itemNumber + '\')">儲 存</button><br /><button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].ID + '\',\'' + result[i].itemNumber + '\',\'' + i + '\')">取 消</button></div>' +
			                    '</td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList input[type=text]").add("#mainSearchList select").attr("disabled", true);

                    for (var i = 0; i < result.length; i++) {
                        $("#HS_" + result[i].ID + "HS_" + result[i].itemNumber + " .voice").children('option[value="' + result[i].voice + '"]').attr("selected", true);
                        $("#HS_" + result[i].ID + "HS_" + result[i].itemNumber + " .state").children('option[value="' + result[i].state + '"]').attr("selected", true);
                    }
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        case "setHearingTestDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                $(".btnSearch").click();
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function callStudentSearchfunction() {
    var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                    '<tr><td width="80">服務使用者編號 <input type="text" name="name" id="gosrhstudentID"/></td></tr>' +
                    '<tr><td>學生姓名 <input type="text" name="name" id="gosrhstudentName"/></td></tr>' +
                    '<tr><td>出生日期 <input class="date" type="text" name="name" size="10" id="gosrhbirthdaystart"/>～' +
                            '<input class="date" type="text" name="name" size="10" id="gosrhbirthdayend"/></td></tr>' +
                    '<tr><td>個案狀態 <select id="gosrhcaseStatu"></select></td><tr>' +

                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn" class="tableList" border="0"  width="400">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="100">服務使用者編號</th>' +
			                    '<th width="130">學生姓名</th>' +
			                    '<th width="70">性別</th>' +
			                    '<th width="100">出生日期</th>' +
			                '</tr>' +
			            '</thead>' +
			            '<tbody>' +
			            '</tbody>' +
                    '</table>' +
                    '<div id="smainPagination" class="pagination"></div>' +
                    '</div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            AgencyStatuSelectFunction("inline", "gosrhcaseStatu");
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchStuinline");
                AspAjax.SearchStudentDataBaseCount(obj);
            });
        }
    });
}
function UpData(TrID,number) {
    $("#HS_" + TrID + "HS_" + number + " input[type=text]").add("#HS_" + TrID + "HS_" + number + " select").attr("disabled", false);
    $("#HS_" + TrID + "HS_" + number + " .UD").hide();
    $("#HS_" + TrID + "HS_" + number + " .SC").show();
    
}
function SaveData(TrID, number) {
    $("#HS_" + TrID + "HS_" + number + " input[type=text]").add("#HS_" + TrID + "HS_" + number + " select").attr("disabled", true);
    $("#HS_" + TrID + "HS_" + number + " .UD").show();
    $("#HS_" + TrID + "HS_" + number + " .SC").hide();

    var html = "#HS_" + TrID + "HS_" + number;
    var Obj = new Object();
    Obj.ID = TrID;
    Obj.voice = $(html + " .voice :selected").val();
    Obj.state = $(html + " .state :selected").val();
    Obj.project = $(html + " .project").val();
    Obj.material = $(html + " .material").val();
    Obj.volume = $(html + " .volume").val();
    Obj.result = $(html + " .result").val();
    Obj.remark = $(html + " .remark").val();
    Obj.itemNumber = number;
    AspAjax.setHearingTestDataBase(Obj);
}


function cancelUpData(TrID, number, vIndex) {
    $("#HS_" + TrID + "HS_" + number + " .project").val(_ReturnValue[vIndex].project);
    $("#HS_" + TrID + "HS_" + number + " .material").val(_ReturnValue[vIndex].material);
    $("#HS_" + TrID + "HS_" + number + " .volume").val(_ReturnValue[vIndex].volume);
    $("#HS_" + TrID + "HS_" + number + " .result").val(_ReturnValue[vIndex].result);
    $("#HS_" + TrID + "HS_" + number + " .remark").val(_ReturnValue[vIndex].remark);
    $("#HS_" + TrID + "HS_" + number + " .voice").children('option[value="' + _ReturnValue[vIndex].voice + '"]').attr("selected", true);
    $("#HS_" + TrID + "HS_" + number + " .state").children('option[value="' + _ReturnValue[vIndex].state + '"]').attr("selected", true);

    $("#HS_" + TrID + "HS_" + number + " input[type=text]").add("#HS_" + TrID + "HS_" + number + " select").attr("disabled", true);
    $("#HS_" + TrID + "HS_" + number + " .UD").show();
    $("#HS_" + TrID + "HS_" + number + " .SC").hide();

}