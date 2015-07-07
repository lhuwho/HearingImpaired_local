var MyBase = new Base();
var noEmptyItem = ["teacherName", "teacher"];
var noEmptyShow = ["受評者", "請重新選取受評者"];
var getstaffDataID = "";
var _ColumnID = 0;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    AgencyUnitSelectFunction("mainSearchForm", "gosrhTeacherUnit");
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

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

    
    $(".getstaffData").unbind("click").click(function() {
        getstaffDataID = ($(this).attr("id")).replace("Name", "");
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

    $("#reportPer").add("#basicPer").add("#teachPer").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        countSum();
    });

});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchTeacherRstimateDataBaseCount":
            var pageCount = parseInt(result);
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
                        AspAjax.SearchTeacherRstimateDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchTeacherRstimateDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].ID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].Number + '</td>' +
			                    '<td>' + result[i].TeacherName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].AssessmentDate) + '</td>' +
			                    '<td>' + _UnitList[result[i].Unit] + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].TeacherName);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            }
            break;
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
                        inner += '<tr class="ImgJs" id="s_' + i + '">' +
                                '<td>' + result[i].sID + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td style="display:none;">' + result[i].sUnit + '</td>' +
			                    '<td>' + result[i].sName + '</td>' +
			                '</tr>';
                    }
                    //
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var index = ($(this).attr("id")).replace("s_", "");
                        $("#" + getstaffDataID).html(result[index].sID);
                        $("#" + getstaffDataID+"Name").val(result[index].sName);
                        if (getstaffDataID == "teacher") {
                            $("#officeDate").html(TransformADFromStringFunction(result[index].officeDate));
                        }
                        $.fancybox.close();
                    });


                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "createNewTeacherDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = "./newteacher_estimate_report.aspx?id=" + result[1] + "&act=2";
            }
            break;
        case "getNewTeacherDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#Unit").html(_UnitList[result.Unit]);
                    _ColumnID = result.ID;
                    countSum();
                    $("#officeDate").html(TransformADFromStringFunction(result.officeDate));
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            } else {
                alert("查無資料");
            }
            break;
        case "setNewTeacherDataBase":
            if (result <= 0) {
                alert("發生錯誤，請重新整理頁面");
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
    window.open("./newteacher_estimate_report.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.getNewTeacherDataBase(id);
    } else if (id == null && act == 1) {
    $(".btnSave").fadeIn();
    $("#Unit").html(_UnitList[_uUnit]);
    }
}
function countSum() {
    var reportPer = $("#reportPer").val();
    var basicPer = $("#basicPer").val();
    var teachPer = $("#teachPer").val();
    if (reportPer.length > 0 && basicPer.length > 0 && teachPer.length > 0) {
        var sunPer = parseInt(reportPer) + parseInt(basicPer) + parseInt(teachPer);
        $("#sunPer").html(sunPer);
    }
}
function saveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");
    var obj1 = getHideSpanValue("mainContent", "hideClassSpan");
    MergerObject(obj, obj1);
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        if (Type == 0) {
            AspAjax.createNewTeacherDataBase(obj);
        } else if (Type == 1) {
            obj.ID = _ColumnID;
            AspAjax.setNewTeacherDataBase(obj);
        }
    }
}
function searchData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var Searchassessment = false;
    if ((obj.txtassessmentStart != null && obj.txtassessmentEnd != null) || (obj.txtassessmentStart == null && obj.txtassessmentEnd == null)) {
        Searchassessment = true;
    }
    if (Searchassessment) {
        AspAjax.SearchTeacherRstimateDataBaseCount(obj);
    } else {
        alert("請填寫完整期區間");
    }
}