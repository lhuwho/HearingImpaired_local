var MyBase = new Base();
var noEmptyItem = ["studentID", "studentName"];
var noEmptyShow = ["服務使用者編號抓取錯誤，請重新選擇學生", "學生姓名"];
var _ColumnID = 0;
var _endReason = new Array("", "家長主動要求結案", "進入小學就讀", "進入一般幼兒園就讀", "暫無聽語訓練需求", "家庭意外變故", "其它原因");
var _ReturnValue;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
 

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    });

    $(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

   
    zoneCityFunction();
    $("#studentName").unbind("click").click(function() {
        callStudentSearchfunction();
    });
    assistmanagebrandFunction();
});
function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStudentTrackedDataBaseCount":
            var obj = MyBase.getTextValueBase("searchTable");
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
                    AspAjax.SearchStudentTrackedDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentTrackedDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].txtendReasonDate) + '</td>' +
			                    '<td>' + _endReason[result[i].txtendReasonDateType] + '</td>' +
			                    '<td>' + result[i].txtTel + '</td>' +
			                    '<td>' + result[i].txtemail + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
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
                        /*$("#studentName").val($(this).children("td:nth-child(2)").html());
                        $("#studentSex").html($(this).children("td:nth-child(3)").html());
                        $("#studentbirthday").html($(this).children("td:nth-child(4)").html());*/
                        AspAjax.getStudentDataBaseForTrack($(this).children("td:nth-child(1)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "getStudentDataBaseForTrack":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#studentSex").html(_SexList[result.studentSex]);
                    $("#studentbirthday").html(TransformADFromStringFunction(result.studentbirthday));
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
        case "createStudentTrackedDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./student_tracked.aspx?id=" + result[1] + "&act=2";
                }
            }
            break;
        case "getStudentTrackedDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#censusAddressCity").html(getCity(result.censusAddressCity));
                    $("#addressCity").html(getCity(result.addressCity));
                    $("input[name=assistmanageR]").change();
                    $("input[name=assistmanageL]").change();
                    $("#brandR").children("option[value=" + result.brandR + "]").attr("selected", true);
                    $("#brandL").children("option[value=" + result.brandL + "]").attr("selected", true);

                    $("#studentSex").html(_SexList[result.studentSex]);
                    $("#studentbirthday").html(TransformADFromStringFunction(result.studentbirthday));
                    $("#endReasonDate").html(TransformADFromStringFunction(result.endReasonDate));
                    $("#firstClassDate").html(TransformADFromStringFunction(result.firstClassDate));

                    $("#buyingtimeR").html(TransformADFromStringFunction(result.buyingtimeR));
                    $("#openHzDateR").html(TransformADFromStringFunction(result.openHzDateR));
                    $("#buyingtimeL").html(TransformADFromStringFunction(result.buyingtimeL));
                    $("#openHzDateL").html(TransformADFromStringFunction(result.openHzDateL));
                    //TransformADFromStringFunction
                    $("#assistmanageR").html(assistmanage(result.assistmanageR));
                    $("#assistmanageL").html(assistmanage(result.assistmanageL));
                    
                    $("#caseUnit").html(_UnitList[result.sUnit]);
                    var inner = "";
                    _ReturnValue = result.Teack;
                    for (var i = 0; i < result.Teack.length; i++) {
                        var TeackData = result.Teack[i];
                        inner += ' <tr id="HS_' + TeackData.tID + '" class="sStaffData" >' +
			                    '<td>' + TransformADFromStringFunction(TeackData.tDate) + '</td>' +
			                    '<td>' + TeackData.tStaffName + '</td>' +
			                    '<td><textarea class="StContent" >' + TeackData.tContent + '</textarea></td>' +
			                    '<td><div class="UD"><button class="btnUpdateSmall" type="button" onclick="UpData(' + TeackData.tID + ')">更 新</button> <button class="btnDelete" type="button" onclick="DelData(' + TeackData.tID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnSaveSmall" type="button" onclick="SaveData(' + TeackData.tID + ')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(' + TeackData.tID + ',' + i + ')">取 消</button></div>' +
			                    '</td>' +
			                '</tr>';
                    }
                    $("#TeackTable tbody").html(inner);
                    $("#TeackTable tbody textarea").attr("disabled", true);
                    haveRolesWaitFunction();
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
        case "createStudentTrackedRecord":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "setStudentTrackedDataBase":
        case "setStudentTrackedRecord":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "delStudentTrackedRecord":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                $.fancybox.close();
                window.location.reload();
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function getView(id) {
    window.open("./student_tracked.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        _ColumnID = id;
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.getStudentTrackedDataBase(parseInt(id));
    } else if (id == null && act == 1) {
    $(".btnSave").fadeIn();
    $(".btnAdd").hide();
    $("#caseUnit").html(_UnitList[_uUnit]);
   
    }
}
function saveOffData(Type) {
    var obj1 = MyBase.getTextValueBase("item1Content");
    obj1.studentID = $("#studentID").html()
    var obj2 = MyBase.getTextValueBase("item2Content");
    var obj = MergerObject(obj1, obj2);

    switch (Type) {
        case 0:
            var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                AspAjax.createStudentTrackedDataBase(obj);
            }
            break;
        case 1:
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
            $("input").add("select").add("textarea").attr("disabled", true);
            obj.ID = _ColumnID;
            AspAjax.setStudentTrackedDataBase(obj);
            break;
    }
}
function InsertData() {
    $("#insertDataDiv").show();
    $("#tStaffName").val(_uName);
    $("#tStaff").html(_uId);
    $("#insertDataDiv1 .date").datepick({
        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
    });
    $("#tDate").val(TodayADDateFunction());
    $("#tDate").add("#tStaffName").add("#tContent").attr("disabled", false);
}
function cancelInsert() {
    $("#insertDataDiv").hide();
}
function InsertTrackData() {
    var obj = MyBase.getTextValueBase("insertDataDiv");
    obj.offID = _ColumnID;

    if (obj.offID != null && obj.offID != 0) {
        AspAjax.createStudentTrackedRecord(obj);

    } else {
        alert("發生錯誤，請刷新頁面後再嘗試");
    }
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
function search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var Date1 = false;
    var Date2 = false;
    if ((obj.txtbirthdaystart != null && obj.txtbirthdayend != null) || (obj.txtbirthdaystart == null && obj.txtbirthdayend == null)) {
        Date1 = true;
    }
    if ((obj.txtendReasonDatestart != null && obj.txtendReasonDateend != null) || (obj.txtendReasonDatestart == null && obj.txtendReasonDateend == null)) {
        Date2 = true;
    }
    if (Date1 && Date2) {
        AspAjax.SearchStudentTrackedDataBaseCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }
}
function UpData(TrID) {
    $("#HS_" + TrID + " textarea").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .StContent").val(_ReturnValue[vIndex].tContent);
    $("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delStudentTrackedRecord(TrID);
    });
}
function SaveData(TrID) {
    var obj = new Object();
    obj.tID = parseInt(TrID);
    var Content = $("#HS_" + TrID + " .StContent").val();
    if (Content.length > 0) {
        obj.tContent = Content;
    }
    if (obj.tContent != null) {
        $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setStudentTrackedRecord(obj);
    } else {
        alert("請填寫完整");
    }

}