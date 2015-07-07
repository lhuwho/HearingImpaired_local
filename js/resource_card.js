var MyBase = new Base();
var noEmptyItem = ["fillInDate", "resourceName"];
var noEmptyShow = ["填寫日期", "機構名稱"];
var _ColumnID = 0;

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#upDate").val(TodayADDateFunction());
        $("#upBy").val(_uName);
    });
   
    
    zoneCityFunction();
    
});
function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "searchResourceDataBaseCount":
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
                    AspAjax.searchResourceDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchResourceDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
			                    '<td>' + result[i].txtresourceName + '</td>' +
			                    '<td>' + _zoneCity[result[i].txtaddressCity] + '</td>' +
			                    '<td>' + _resourceType[result[i].txtresourceType] + '</td>' +
			                    '<td>' + result[i].txtresourceItem + '</td>' +
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
        case "createResourcedDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，請重新整理頁面，錯誤訊息如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = './resource_card.aspx?id=' + result[1] + '&act=2';
            }
            break;
        case "getResourcedDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#sUnit").html(_UnitList[result.sUnit]);
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，請重新整理頁面，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
        case "setResourceDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
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
    window.open("./resource_card.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        _ColumnID = id;
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.getResourcedDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#fillInDate").add("#upDate").val(TodayADDateFunction());
        $("#sUnit").html(_UnitList[_uUnit]); $("#upBy").val(_uName); $("#fillInBy").val(_uName);
    }
}

function SaveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");
    switch (Type) {
        case 0:
            var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
            AspAjax.createResourcedDataBase(obj);
            }
            break;
        case 1:
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
            $("input").add("select").add("textarea").attr("disabled", true);
            obj.ID = _ColumnID;
            AspAjax.setResourceDataBase(obj);
            break;
    }
}
function Search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.searchResourceDataBaseCount(obj);
}