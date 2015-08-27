var MyBase = new Base();
var noEmptyItem = ["studentID", "studentName", "viewData", "viewTitle"];
var noEmptyShow = ["學生ID抓取錯誤，請重新選擇學生", "學生姓名", "訪談日期", "訪談主題"];
var _ColumnID = 0;
var _vStyle = new Array("", "電話訪談", "親自洽談");
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    
    initPage();
    AspAjax.getAllStaffDataList([18,17]);
    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#viewStaffselect").trigger('chosen:updated');
        $("#studentName").attr("disabled", true);
    });

    $(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

    
    $("select[name=item]").chosen({
        disable_search_threshold: 10,
        display_selected_options: false,
        max_selected_options: 2, //最大選擇數
        no_results_text: "查無資料", //沒有結果匹配
        width: "300px" //寬度
    });
    $(".chosen-results").css("max-height", "100px");
    $(".chosen-choices").add(".chosen-drop").css("text-align", "left");
    $("#studentName").unbind("click").click(function() {
        callStudentSearchfunction();
    });
    AgencyStatuSelectFunction("searchTable", "gosrhcaseStatu");
    //haveRolesWaitFunction();
});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "getAllStaffDataList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    for (var i = 0; i < result.length; i++) {
                        $("#viewStaffselect").append($('<option></option>').attr("value", result[i].sID).text(result[i].sName + "(" + result[i].sID + ")"));
                    }
                    $("#viewStaffselect").trigger('chosen:updated');
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#viewStaffselect").find("option").empty();
            }
            break;
        case "SearchStudentServiceDataBaseCount":
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
                        AspAjax.SearchStudentServiceDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentServiceDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].txtstudentID + '</td>' +
                                '<td>' + _CaseStatu[result[i].txtstudentStatu] + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].txtviewData) + '</td>' +
			                    '<td>' + result[i].txtviewTitle + '</td>' +
			                    '<td>' + _vStyle[result[i].txtviewStyle] + '</td>' +
			                    '<td>' + result[i].txtviewPeople + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
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
        case "createStudentServiceData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./case_service_record.aspx?id=" + result[1] + "&act=2";
                }
            }
            break;
        case "getStudentServiceData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    //$("#sUnit").html(_UnitList[result.sUnit]);
                    for (var i = 0; i < result.viewStaff.length; i++) {
                        if (result.viewStaff[i] != "0") {
                            $("#viewStaffselect").children('option[value="' + result.viewStaff[i] + '"]').attr("selected", true);
                        }
                        $("#viewStaffselect").trigger('chosen:updated');
                    }
                    $("#studentbirthday").html(TransformADFromStringFunction(result.studentbirthday));
                    $("#studentSex").html(_SexList[result.studentSex]);
                    $("#sUnit").html(_UnitList[result.sUnit]);
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
        case "setStudentServiceData":
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
    window.open("./case_service_record.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    
    haveRolesAndHide();
    if (id != null) {
        _ColumnID = id;
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.getStudentServiceData(parseInt(id));
    } else if (id == null && act == 1) {
    $(".btnSave").fadeIn();
    $("#sUnit").html(_UnitList[_uUnit]);
       
    }
}

function Search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var Date1 = false;
    var Date2 = false;
    if ((obj.txtbirthdaystart != null && obj.txtbirthdayend != null) || (obj.txtbirthdaystart == null && obj.txtbirthdayend == null)) {
        Date1 = true;
    }
    if ((obj.txtviewstart != null && obj.txtviewend != null) || (obj.txtviewstart == null && obj.txtviewend == null)) {
        Date2 = true;
    }
    if (Date1 && Date2 ) {
        AspAjax.SearchStudentServiceDataBaseCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }

}
function SaveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");
    var obj1 = getHideSpanValue("mainContent", "hideClassSpan");
    MergerObject(obj, obj1);
    var viewStaff = new Array();
    $("#viewStaffselect").find(':selected').each(function() {
        viewStaff.push($(this).val());
    });
    obj.viewStaff = viewStaff;
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        switch (Type) {
            case 0:
                AspAjax.createStudentServiceData(obj);
                break;
            case 1:
                $(".btnSaveUdapteData").add(".btnCancel").hide();
                $(".btnUpdate").fadeIn();
                $("input").add("select").add("textarea").attr("disabled", true);
                obj.ID = _ColumnID;
                AspAjax.setStudentServiceData(obj);
                break;
        }
    }
}
function callStudentSearchfunction() {
    var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                    '<tr><td width="80">服務使用者編號 <input type="text" name="name" id="gosrhstudentID"/></td></tr>' +
                    '<tr><td>學生姓名 <input type="text" name="name" id="gosrhstudentName"/></td></tr>' +
                    '<tr><td>出生日期 <input class="date" type="text" name="name" size="10" id="gosrhbirthdaystart"/>～' +
                            '<input class="date" type="text" name="name" size="10" id="gosrhbirthdayend"/></td></tr>' +
                    '<tr><td>入會日期 <input class="date" type="text" name="name" size="10" id="gosrhjoindaystart"/>～' +
			                '<input class="date" type="text" name="name" size="10" id="gosrhjoindayend" /></td><tr>' +

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
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchStuinline");
                AspAjax.SearchStudentDataBaseCount(obj);
            });
        }
    });
}