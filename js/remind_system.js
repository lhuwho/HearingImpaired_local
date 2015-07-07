var MyBase = new Base();
var noEmptyItem = ["recipient", "recipientID", "executionContent", "executionDate"];
var noEmptyShow = ["執行者", "執行者","內容","提醒日期"];
var _ReturnValue;

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    //setTimeout('$("#Unit").html(_UnitList[_uUnit]);', 500);
    $(".btnSearch").click(function() {
    $("#mainSearchList input[type=text]").add("#mainSearchList textarea").attr("disabled", true);
    });
});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    // alert(methodName);
    switch (methodName) {
        case "SearchStaffDataBaseCount":
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
                        var obj = MyBase.getTextValueBase("searchTableinline");
                        AspAjax.SearchStaffDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].sID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="s_' + result[i].sID + '">' +
                                '<td>' + result[i].sID + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td style="display:none;">' + result[i].sUnit + '</td>' +
			                    '<td>' + result[i].sName + '</td>' +
			                '</tr>';
                    }
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("s_", "");
                        $("#recipientID").html(id);
                        var Name = $(this).children("td:nth-child(4)").html();
                        $("#recipient").val(Name);

                        $.fancybox.close();
                    });

                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "CreateRemindSystemData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.reload();
                }
            }
            break;
        case "SearchRemindSystemDataCount":
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
                        AspAjax.SearchRemindSystemData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchRemindSystemData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].fulfillmentDate == "1900-01-01") {
                            inner += ' <tr id="HS_' + result[i].rID + '" class="sStaffData" >' +
			                    '<td>' + result[i].Number + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].designeeDate) + '</td>' +
			                    '<td>' + result[i].designee + '</td>' +
			                    '<td>' + result[i].recipient + '</td>' +
			                    '<td><textarea class="SexecutionContent"  cols="26" rows="1">' + result[i].executionContent + '</textarea></td>' +
			                    '<td><input type="text" class="SexecutionDate date" value="' + TransformADFromStringFunction(result[i].executionDate) + '" size="10" /></td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].fulfillmentDate) + '</td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].rID + ')">更 新</button> <button class="btnView" type="button" onclick="DelData(' + result[i].rID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(' + result[i].rID + ',' + i + ')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(' + result[i].rID + ',' + i + ')">取 消</button></div>' +
			                    '</td>' +
			                '</tr>';
                        } else {
                        inner += ' <tr id="HS_' + result[i].rID + '" class="sStaffData" >' +
			                    '<td>' + result[i].Number + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].designeeDate) + '</td>' +
			                    '<td>' + result[i].designee + '</td>' +
			                    '<td>' + result[i].recipient + '</td>' +
			                    '<td>' + result[i].executionContent + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].executionDate) + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].fulfillmentDate) + '</td>' +
			                    '<td>&nbsp;</td>' +
			                '</tr>';
                        }
                    }

                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $('.date').datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("#mainSearchList input[type=text]").add("#mainSearchList textarea").attr("disabled", true);
                } else {
                    $("#inline .tableList").children("tbody").html("<tr><td colspan='8'>發生錯誤，錯誤訊息如下：" + result[0].errorMsg+"</td></tr>");
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
            }
            break;
        case "setRemindSystemData1":
            if (parseInt(result[0]) <= 0) {
                if (parseInt(result[0]) == -1) {
                    alert("發生錯誤，錯誤訊息如下:" + result[1]);
                } else if (parseInt(result[0]) == -2) {
                    alert(result[1]);
                    cancelUpData(result[2], result[3]);
                }
            } else {
                alert("更新成功");
                //window.location.reload();
                SearchData();
            }
            break;
        case "delRemindSystemData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                $.fancybox.close();
                SearchData();
            }
            break;   
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function SaveData(TrID, vIndex) {
    var obj = new Object();
    obj.rID = parseInt(TrID);
    var SexecutionDate = TransformRepublicReturnValue($("#HS_" + TrID + " .SexecutionDate").val());
    if (SexecutionDate.length > 0) {
        obj.executionDate = SexecutionDate;
    }
    var SexecutionContent = $("#HS_" + TrID + " .SexecutionContent").val();
    if (SexecutionContent.length > 0) {
        obj.executionContent = SexecutionContent;
    }
    if (obj.executionDate != null && obj.executionContent != null) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setRemindSystemData1(obj, vIndex);
    } else {
        alert("請填寫完整");
    }
    
}
function InsertData() {
    $("#insertDataDiv2").hide();
    $("#insertDataDiv").show();
    $("#setB").hide();
    $("#fileInData").html(TodayADDateFunction());
    $("#sender").html(_uName);
    $("#recipient").val(_uName);
    $("#recipientID").html(_uId);
    $("#executionContent").add("#executionDate").val("");
    
    
    $("#recipient").unbind("click").click(function() {
        var inner = '<div id="inline"><br /><table border="0" width="360" id="searchTableinline">' +
                    '<tr><td width="80">員工姓名　<input type="text" id="gosrhstaffName" value="" /></td></tr>' +
                    '<tr><td>派任單位　<select id="gosrhstaffUnit"></select></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table class="tableList" border="0"  width="360">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="100">員工編號</th>' +
			                    '<th width="130">派任單位</th>' +
			                    '<th width="130">員工姓名</th>' +
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
                AgencyUnitSelectFunction("inline", "gosrhstaffUnit");
                $('button').css({ "background-color": "#F9AE56" });
                $("#inline .btnSearch").unbind("click").click(function() {
                    var obj = MyBase.getTextValueBase("searchTableinline");
                    AspAjax.SearchStaffDataBaseCount(obj);
                });
            }
        });
    });

}

function cancelInsert() {
    $("#insertDataDiv").hide();
    $("#setB").show();
}

function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .SexecutionContent").val(_ReturnValue[vIndex].executionContent);
    $("#HS_" + TrID + " .SexecutionDate").val(TransformADFromStringFunction(_ReturnValue[vIndex].executionDate));

    $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();

}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delRemindSystemData(parseInt(TrID));
    });
}
function setRemindData() {
    var obj = MyBase.getTextValueBase("insertDataDiv");
    obj.recipientID = $("#recipientID").html();

    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.CreateRemindSystemData(obj);
    }
}
function SearchData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var Date1 = false;
    var Date2 = false;
    var Date3 = false;
    if ((obj.txtfulfillmentDatestart != null && obj.txtfulfillmentDateend != null) || (obj.txtfulfillmentDatestart == null && obj.txtfulfillmentDateend == null)) {
        Date1 = true;
    }
    if ((obj.txtexecutionDatestart != null && obj.txtexecutionDateend != null) || (obj.txtexecutionDatestart == null && obj.txtexecutionDateend == null)) {
        Date2 = true;
    }
    if ((obj.txtdesigneeDatestart != null && obj.txtdesigneeDateend != null) || (obj.txtdesigneeDatestart == null && obj.txtdesigneeDateend == null)) {
        Date3 = true;
    }
    if (Date1 && Date2 && Date3) {
        AspAjax.SearchRemindSystemDataCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }
}
function InsertData2() {
    cancelInsert();
    $("#insertDataDiv2").show();
    $("#fileInData2").html(TodayADDateFunction());
    $("#sender2").html(_uName);
    //$("#recipient2").val(_uName); //select
    $("#executionContent2").add("#executionDate").val("");
    ApplyJobSelectFunction("insertDataDiv2", "recipient2");
    
}