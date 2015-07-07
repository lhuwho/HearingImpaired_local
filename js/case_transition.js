var MyBase = new Base();
var noEmptyItem = ["studentID", "studentName", "transYear"];
var noEmptyShow = ["服務使用者編號抓取錯誤，請重新選擇學生", "學生姓名", "轉銜年度"];
var _ColumnID = 0;
//var _transStage -> All 
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#studentName").attr("disabled", true);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
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
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
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
			                    '<td><span style="display:none;">' + i + '</span>' + TransformADFromDateFunction(result[i].txtstudentbirthday) + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        $("#studentID").html($(this).children("td:nth-child(1)").html());
                        $("#studentName").val($(this).children("td:nth-child(2)").html());
                        /*$("#studentSex").html($(this).children("td:nth-child(3)").html());*/
                        var index = $(this).find('span').html();
                        var studentbirthday = result[index].txtstudentbirthday;

                        var stuAge = BirthdayFromDateFunction(studentbirthday);
                        $("#studentAge").val(stuAge[0]);
                        $("#studentMonth").val(stuAge[1]);
                        $.fancybox.close();
                    });
                } else {
                alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;

        case "SearchStudentVisitDataBaseCount":
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
                        AspAjax.SearchStudentVisitDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentVisitDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="C_' + result[i].ID + '">' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
                                '<td>' + _visitType[result[i].txtvisitType] + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].txtvisitDate) + '</td>' +
			                '</tr>';
                    }
                    //
                    $("#StuinlineReturn").children("tbody").html(inner);
                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        /*$("#studentID").html($(this).children("td:nth-child(1)").html());
                        $("#studentName").val($(this).children("td:nth-child(2)").html());*/
                        var indexID = ($(this).attr("id")).replace("C_", "");
                        var ViewType = $("#inline #ViewType").html();
                        var htmlID = "";
                        if (ViewType == "1") {
                            htmlID = "meetingVisitReport";
                        } else if (ViewType == "2") {
                            htmlID = "schoolVisitRecord";
                        }

                        $("#" + htmlID).html(indexID);
                        $("#" + htmlID + "Url").attr("href", "./case_visit_record.aspx?id=" + indexID + "&act=2");
                        $("#" + htmlID + "Url").show();

                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;

        case "SearchStudentServiceDataBaseCount":
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
                        AspAjax.SearchStudentServiceDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentServiceDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].txtstudentID) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="C_' + result[i].ID + '">' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].txtviewData) + '</td>' +
			                    '<td>' + result[i].txtviewTitle + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn").children("tbody").html(inner);
                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        /*$("#studentID").html($(this).children("td:nth-child(1)").html());
                        $("#studentName").val($(this).children("td:nth-child(2)").html());*/
                        var indexID = ($(this).attr("id")).replace("C_", "");
                        $("#adaptationReport").html(indexID);
                        $("#adaptationReportUrl").attr("href", "./case_service_record.aspx?id=" + indexID + "&act=2");
                        $("#adaptationReportUrl").show();

                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].txtstudentName);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "createStudentTransData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./case_transition.aspx?id=" + result[1] + "&act=2";
                }
            }
            break;
        case "getStudentTransDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#caseUnit").html(_UnitList[result.caseUnit]);
                    if (result.transReportFile.length > 0) {
                        $("#transReportFile").html('<a href="./uploads/student/' + result.studentID + '/' + result.transReportFile + '" target="_blank" >轉銜報告書</a>');
                    }
                    if (result.transFamilyReportFile.length > 0) {
                        $("#transFamilyReportFile").html('<a href="./uploads/student/' + result.studentID + '/' + result.transFamilyReportFile + '" target="_blank" >轉銜家庭報告</a>');
                    }
                    if (result.schooladvocacyReport.length > 0) {
                        $("#schooladvocacyReportUrl").html('<a href="./uploads/student/' + result.studentID + '/' + result.schooladvocacyReport + '" target="_blank" >校園宣導報告</a>');
                    }
                    if (result.meetingVisitReport.length > 0 && parseInt(result.meetingVisitReport) > 0) {
                        $("#meetingVisitReport").html(result.meetingVisitReport);
                        $("#meetingVisitReportUrl").attr("href", "./case_visit_record.aspx?id=" + result.meetingVisitReport + "&act=2");
                        $("#meetingVisitReportUrl").show();
                    }
                    if (result.schoolVisitRecord.length > 0 && parseInt(result.schoolVisitRecord) > 0) {
                        $("#schoolVisitRecord").html(result.schoolVisitRecord);
                        $("#schoolVisitRecordUrl").attr("href", "./case_visit_record.aspx?id=" + result.schoolVisitRecord + "&act=2");
                        $("#schoolVisitRecordUrl").show();
                    }
                    if (result.adaptationReport.length > 0 && parseInt(result.adaptationReport) > 0) {
                        $("#adaptationReport").html(result.adaptationReport);
                        $("#adaptationReportUrl").attr("href", "./case_service_record.aspx?id=" + result.adaptationReport + "&act=2");
                        $("#adaptationReportUrl").show();
                    }
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            }
            break;
        case "setStudentTransData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "SearchStudentTransDataBaseCount":
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
                    AspAjax.SearchStudentTransDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentTransDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].txttransYear + '</td>' +
                                '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + result[i].txtstudentAge + "歲" + result[i].txtstudentMonth + '月</td>' +
			                    '<td>' + _transStage[result[i].txttransStage] + '</td>' +
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
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function getView(id) {
    window.open("./case_transition.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getStudentTransDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#caseUnit").html(_UnitList[_uUnit]);
        $("#studentName").unbind("click").click(function() {
            callStudentSearchfunction();
        });
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
        $('.date').datepick();
            AgencyStatuSelectFunction("inline", "gosrhcaseStatu");
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchStuinline");
                AspAjax.SearchStudentDataBaseCount(obj);
            });
        }
    });
}
function callStudentViewSearchfunction(Type) {
    var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
	                '<tr><td width="80">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td></tr>' +
                    '<tr><td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td></tr>' +
	                '<tr><td>訪視類別 <select id="gosrhvisitType" class="visitTypeList"></select></td></tr>' +
	                '<tr><td>訪視日期 <input id="gosrhvisitDatestart" class="date" type="text"  value="" size="10" />～<input id="gosrhvisitDateend" class="date" type="text"  value="" size="10" /></td></tr>' +
            '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
            '</table><br />' +
            '<table id="StuinlineReturn" class="tableList" border="0"  width="400">' +
            '<thead>' +
	                '<tr>' +
	                    '<th width="100">服務使用者編號</th>' +
	                    '<th width="100">學生姓名</th>' +
	                    '<th width="100">訪視類別</th>' +
	                    '<th width="100">訪視日期</th>' +
	                '</tr>' +
	            '</thead>' +
	            '<tbody>' +
	            '</tbody>' +
            '</table>' +
            '<div id="smainPagination" class="pagination"></div><span id="ViewType" class="hideClassSpan">' + Type + '</span>' +
            '</div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $(".visitTypeList").append($("<option></option>").attr("value", 0).text("請選擇"));
            for (var i = 1; i < _visitType.length; i++) {
                $(".visitTypeList").append($("<option></option>").attr("value", parseInt(i)).text(_visitType[i]));
            }
            $('.date').datepick();
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchStuinline");
                var Date1 = false;
                var Date2 = false;
                if ((obj.txtbirthdaystart != null && obj.txtbirthdayend != null) || (obj.txtbirthdaystart == null && obj.txtbirthdayend == null)) {
                    Date1 = true;
                }
                if ((obj.txtvisitDatastart != null && obj.txtvisitDataend != null) || (obj.txtvisitDatastart == null && obj.txtvisitDataend == null)) {
                    Date2 = true;
                }
                if (Date1 && Date2) {
                    AspAjax.SearchStudentVisitDataBaseCount(obj);
                } else {
                    alert("請填寫完整日期區間");

                }
            });
        }
    });
}
function callRemoveStudentViewSearchfunction(ViewType) {
    var htmlID = "";
    if (ViewType == "1") {
        htmlID = "meetingVisitReport";
    } else if (ViewType == "2") {
        htmlID = "schoolVisitRecord";
    }

    $("#" + htmlID).html("");
    $("#" + htmlID + "Url").attr("href", "#");
    $("#" + htmlID + "Url").hide();
}
function callStudentCaseSearchfunction() {
    var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
	                '<tr><td width="80">學生姓名 <input id="gosrhstudentName" type="text" value="" /></td></tr>' +
                    '<tr><td width="260">出生日期 <input id="gosrhbirthdaystart" class="date" type="text" value="" size="10" />～<input id="gosrhbirthdayend" class="date" type="text" value="" size="10" /></td></tr>' +
	                '<tr><td>個案狀態 <select id="gosrhcaseStatu"></select></td></tr>' +
	                '<tr><td>訪談日期 <input id="gosrhviewstart" class="date" type="text" size="10" value="" />～<input id="gosrhviewend" class="date" type="text" value="" size="10" /></td></tr>' +
            '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
            '</table><br />' +
            '<table id="StuinlineReturn" class="tableList" border="0"  width="400">' +
            '<thead>' +
	                '<tr>' +
	                    '<th width="100">服務使用者編號</th>' +
	                    '<th width="100">學生姓名</th>' +
	                    '<th width="100">訪談日期</th>' +
	                    '<th width="100">訪談主題</th>' +
	                '</tr>' +
	            '</thead>' +
	            '<tbody>' +
	            '</tbody>' +
            '</table>' +
            '<div id="smainPagination" class="pagination"></div><span id="ViewType" class="hideClassSpan">' + Type + '</span>' +
            '</div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $(".visitTypeList").append($("<option></option>").attr("value", 0).text("請選擇"));
            for (var i = 1; i < _visitType.length; i++) {
                $(".visitTypeList").append($("<option></option>").attr("value", parseInt(i)).text(_visitType[i]));
            }
            $('.date').datepick();
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchStuinline");
                var Date1 = false;
                if ((obj.txtviewstart != null && obj.txtviewend != null) || (obj.txtviewstart == null && obj.txtviewend == null)) {
                    Date1 = true;
                }
                if (Date1) {
                    AspAjax.SearchStudentServiceDataBaseCount(obj);
                } else {
                    alert("請填寫完整日期區間");

                }
            });
        }
    });
}
function callRemoveStudentCaseSearchfunction() {
    $("#adaptationReport").html("");
    $("#adaptationReportUrl").attr("href", "#");
    $("#adaptationReportUrl").hide();
}
function saveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent .tableText");
    var obj1 = getHideSpanValue("mainContent", "hideClassSpan");
    MergerObject(obj, obj1);

    var picSave = false;
    var PicNameArray;
    if (obj.studentID != null) {
        var options = {
            type: "POST",
            url: 'Files.ashx?n=student&type=TransRecord&ID=' + obj.studentID,
            async: false,
            success: function(value) {
                var myObject = eval('(' + value + ')');
                if (myObject["error"] == undefined) {
                    PicNameArray = myObject["Msg"];
                    for (var item in PicNameArray) {
                        obj[item] = PicNameArray[item];
                    }
                    picSave = true;
                } else if (myObject["error"] == "NO FILE") {
                    picSave = true;
                }
                else {
                    alert(myObject["error"]);
                }
            }
        };
        // 將options傳給ajaxForm
        $('#GmyForm').ajaxSubmit(options);
    } else {
        alert("服務使用者編號抓取錯誤，請重新選擇學生");
    }
    if (picSave) {
        var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
        if (checkString.length > 0) {
            alert(checkString);
        } else {
            switch (Type) {
                case 0:
                    AspAjax.createStudentTransData(obj);
                    break;
                case 1:
                    $(".btnSaveUdapteData").add(".btnCancel").hide();
                    $(".btnUpdate").fadeIn();
                    $("input").add("select").add("textarea").attr("disabled", true);
                    obj.ID = _ColumnID;
                    AspAjax.setStudentTransData(obj);
                    break;
            }
        }
    }
}
function search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.SearchStudentTransDataBaseCount(obj);

}
