var MyBase = new Base();
var cCheck = new Check();
var noEmptyItem = ["staffName", "staffTWID", "Phone"];
var noEmptyShow = ["員工姓名", "身份證字號", "聯絡電話"];
var noEmptyItem2 = ["CourseDate1", "CourseDate2", "Course", "CoursePrice"];
var noEmptyShow2 = ["日期期間", "日期期間", "課程名稱", "費用"];
var actNumber = 2;
var _viewstaffID = 0;
var _ReturnValue;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    
   
    
    $("#item1Content").fadeIn();


    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#fillInDate").attr("readonly", "readonly");
    });

    $(".btnAdd").click(function() {
        $("#item8Content>table tr:last-child").after($("#whTable").clone().attr("id", ""));
    });



    $("#mainSearchForm .btnSearch").click(function() {
        SearchStaffData();
    });

    $("#CAddressCopyaddress").bind("click", function() {
        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
            $("#Azip").val($("#Censuszip").val());
            $("#AAddress").val($("#CensusAddress").val());
        }

    });
    zoneCityFunction();
    $("input[name=AddressCopyaddress]").bind("click", function() {
        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
            $("#addressZip").val($("#censusAddressZip").val());
            $("#addressCity").children("option[value=" + $("#censusCity :selected").val() + "]").attr("selected", true);
            $("#address").val($("#censusAddress").val());
        }
    });
});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    // alert(methodName);
    switch (methodName) {
        case "createExternalTeacherData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = "./staff_external_database.aspx?id=" + result[1] + "&act=2";
            }
            break;
        case "SearchExternalTeacherDataBaseCount":
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
                        AspAjax.SearchExternalTeacherDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchExternalTeacherDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
			                    '<td>' + result[i].sName + '</td>' +
			                    '<td>' + result[i].Phone + '</td>' +
			                    '<td>' + result[i].Phone2 + '</td>' +
			                    '<td>' + result[i].sEmail + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
            $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            }
            break;
        case "getExternalTeacherDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    _viewstaffID = result.ID;
                    if (result.censusAddressZip.length > 0 && result.censusAddressZip == result.addressZip && result.censusCity == result.addressCity && result.censusAddress == result.address) {
                        $("input[name=AddressCopyaddress]").attr("checked", true);
                    } else {
                        $("input[name=AddressCopyaddress]").attr("checked", false);
                    }
                    var innrt = "";
                    for (var i = 0; i < result.WorkData.length; i++) {
                        var WorkData = result.WorkData[i];
                        inner += '<tr>' +
                                 '<td>' + TransformADFromStringFunction(WorkData.CourseDate1) + ' ~ ' + TransformADFromStringFunction(WorkData.CourseDate1) + '</td>' +
                                 '<td>' + WorkData.Course + '</td>' +
                                 '<td>' + WorkData.CoursePrice + '</td>' +
                                 '<td><button class="btnView" type="button" onclick="DelData(\'' + WorkData.ID + '\')">刪 除</button></td>' +
                                 '</tr>';
                    }
                    $("#WorkingTime").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            } else {
                alert("查無資料");
            }
            break;
        case "setExternalTeacherDataBase":
        case "setStaffWorkData":
            if (result <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "createExternalTeacherWorkDataBase":
        case "createStaffWorkData":
            if (result <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "delExternalTeacherDataBase":
            $.fancybox.close();
            if (result <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
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
    window.open("./staff_external_database.aspx?id=" + id + "&act=" + actNumber);
}

function getViewData(id, act) {
    AgencyUnitSelectFunction("mainSearchForm", "gosrhstaffUnit");
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();

    if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#item2").add("#item3").hide();
        $("input").add("select").add("textarea").attr("disabled", false);
    } else if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getExternalTeacherDataBase(id);
    }

}
function SetStaffData(Type) {
    var obj = new Object();
    obj = MyBase.getTextValueBase("item1Content");
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        var checjkNumber=cCheck.checkIdentityNumber(obj.staffTWID);
        if (Type == 0) {
            if (checjkNumber) {
                AspAjax.createExternalTeacherData(obj);
            } else {
                alert("身份證字號格式不正確");
            }
        } else if (Type == 1) {
            obj.ID = _ColumnID;
            AspAjax.setExternalTeacherDataBase(obj);
        }
    }
}
function SearchStaffData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.SearchExternalTeacherDataBaseCount(obj);
}

function InsertData() {
    $("#insertDataDiv").show();
    $("#insertDataDiv input").attr("disabled", false);
}
function cancelInsert() {
    $("#insertDataDiv").hide();
    $("#insertDataDiv input").attr("disabled", true);
}
function setWorkData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("insertDataDiv");
    obj.ExternalID = _viewstaffID;
     var checkString = MyBase.noEmptyCheck(noEmptyItem2, obj, null, noEmptyShow2);
     if (checkString.length > 0) {
         alert(checkString);
     } else {
         AspAjax.createExternalTeacherWorkDataBase(obj);
     }
}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delExternalTeacherDataBase(parseInt(TrID));
    })
}