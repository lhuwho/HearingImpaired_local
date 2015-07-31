var MyBase = new Base();
var noEmptyItem = ["volunteerName", "servicedate"];
var noEmptyShow = ["志工姓名", "服務時間"];
var _ColumnID = 0;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
  

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });



    //  搜尋頁面- 查詢 按鈕
    $(".btnSearch").click(function() {
        SearchVolunteerData();
    });
    
    zoneCityFunction();
    
});
function getView(id) { 
    window.open("./volunteer_data.aspx?id=" + id + "&act=2");
}
function getViewData( vID ,  act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    var id = vID;
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getVolunteerDataBase(vID);
    }
    else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#fillInDate").val(TodayADDateFunction());
        $("#sUnit").html(_UnitList[_uUnit]);
        AspAjax.getUnitAutoNumber("Volunteer_", "");
        
    }
}
//搜尋
function SearchVolunteerData() {

    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.searchVolunteerDataBaseCount(obj);
}





// 搜尋後接 cs 回傳回來的值  -固定語法
function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "getUnitAutoNumber":
            $("#volunteerId").html(result);
            break;
            
        case "creatVolunteerDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = "./volunteer_data.aspx?id=" + result[1] + "&act=2";
            }
            break;
        case "setVolunteerDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "getVolunteerDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    //AspAjax.getUnitAutoNumber("Volunteer_", "");
                    $("#sUnit").html(_UnitList[result.sUnit]);
                } else {
                alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            } else if (parseInt(result.checkNo) == -2) {
                $("body").empty();
                alert(result.errorMsg);
                window.location.href = "./main.aspx";
            } else {
                $("#inline .tableList").children("tbody").html("查無資料");
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
			                    '<td>' + result[i].volunteerPhone + '</td>' +
			                    '<td>' + result[i].vEmail + '</td>' +
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
    } 
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}



// 新增
function SetVolunteerData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");

    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
    if (Type == 0) {
        obj.volunteerId = $("#volunteerId").html();
            AspAjax.creatVolunteerDataBase(obj);
        } else if (Type == 1) {
            obj.ID = _ColumnID;
            AspAjax.setVolunteerDataBase(obj);
        }
    }

}
   