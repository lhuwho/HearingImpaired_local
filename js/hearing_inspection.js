﻿var MyBase = new Base();
var noEmptyItem = ["studentID", "studentName"];
var noEmptyShow = ["服務使用者編號抓取錯誤，請重新選擇學生", "學生姓名"];
var _checkMode = ["行為觀察", "視覺增強", "遊戲制約", "標準測試"];
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

    $(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

   
    assistmanagebrandFunction();
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
			                    '<td><span style="display:none;" class="sID">' + result[i].txtstudentID + '</span>' + TransformADFromDateFunction(result[i].txtstudentbirthday) + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        /*$("#studentID").html($(this).children("td:nth-child(1)").html());
                        $("#studentName").val($(this).children("td:nth-child(2)").html());
                        //$("#studentSex").html($(this).children("td:nth-child(3)").html());
                        $("#studentbirthday").html($(this).children("td:nth-child(4)").html());*/

                    AspAjax.getStudentAidsDataBaseBasic($(this).find(".sID").html());

                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "getStudentAidsDataBaseBasic":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);

                    $("#studentbirthday").html(TransformADFromStringFunction(result.studentbirthday));

                    $("input[name=assistmanageR]").change();
                    $("input[name=assistmanageL]").change();
                    $("#brandR").children("option[value=" + result.brandR + "]").attr("selected", true);
                    $("#brandL").children("option[value=" + result.brandL + "]").attr("selected", true);
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            }
            break;
        case "createHearingInspectionDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./hearing_inspection.aspx?id=" + result[1] + "&act=2";
                }
            }
            break;
        case "getHearingInspectionDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    //alert(result.modelR);
                    $("#caseUnit").html(_UnitList[result.caseUnit]);
                    $("#studentbirthday").html(TransformADFromStringFunction(result.studentbirthday));
                    var path = "./uploads/student/" + result.studentID + "/print/";
                    if (result.hearingtoneImg.length > 0) {
                        $("#hearingtoneImg").attr("src", path + result.hearingtoneImg);
                    }
                    $("input[name=assistmanageR]").change();
                    $("input[name=assistmanageL]").change();
                    $("#brandR").children("option[value=" + result.brandR + "]").attr("selected", true);
                    $("#brandL").children("option[value=" + result.brandL + "]").attr("selected", true);
                    $("input").add("select").add("textarea").attr("disabled", true);
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            }
            break;
        case "setHearingInspectionDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "SearchHearingInspectionDataBaseCount":
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
                    AspAjax.SearchHearingInspectionDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchHearingInspectionDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].txtstudentName + '</td>' +
                                '<td>' + TransformADFromStringFunction(result[i].txtbirthday) + '</td>' +
                                '<td>' + TransformADFromStringFunction(result[i].txtcheckDate) + '</td>' +
			                    '<td>' + _checkMode[(result[i].txtcheckMode != "" ? result[i].txtcheckMode : 0)] + '</td>' +
			                    '<td>' + result[i].txtaudiologistName + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function getView(id) {
    window.open("./hearing_inspection.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        //$("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getHearingInspectionDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#checkDate").val(TodayADDateFunction());
        setTimeout('$("#caseUnit").html(_UnitList[_uUnit]);$("#audiologistName").val(_uName);', 500);
        $("#studentName").unbind("click").click(function() {
            callStudentSearchfunction();
        });
        $(".hearingtoneImgUrl").hide();
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

function saveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent .tableText");
    var obj1 = MyBase.getTextValueBase("mainContent .tableText2");
    MergerObject(obj, obj1);
    var obj2 = getHideSpanValue("mainContent", "hideClassSpan");
    MergerObject(obj, obj2);
    var obj3 = MyBase.getTextValueBase("mainContent #CheckPurposeDIV");
    MergerObject(obj, obj3);
    obj.checkDate = TransformRepublicReturnValue($("#checkDate").val());

    var picSave = false;
    var PicNameArray;
    if (obj.studentID != null) {
        var options = {
            type: "POST",
            url: 'Files.ashx?n=student&type=HearingInspection&ID=' + obj.studentID,
            async: false,
            success: function(value) {
                var myObject = eval('(' + value + ')');
                if (myObject["error"] == undefined) {
                    PicNameArray = myObject["Msg"];
                    for (var item in PicNameArray) {
                        obj[item] = PicNameArray[item];
                    }

                    picSave = true;
                } else if (myObject["error"] == "NO PIC") {
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
        switch (Type) {
            case 0:
                var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
                if (checkString.length > 0) {
                    alert(checkString);
                } else {
                    AspAjax.createHearingInspectionDataBase(obj);
                }
                break;
            case 1:
                $(".btnSaveUdapteData").add(".btnCancel").hide();
                $(".btnUpdate").fadeIn();
                $("input").add("select").add("textarea").attr("disabled", true);
                obj.ID = _ColumnID;
                AspAjax.setHearingInspectionDataBase(obj);
                break;
        }
    }
}


function search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");

    var Date1 = false;
    var Date2 = false;
    if ((obj.txtbirthdaystart != null && obj.txtbirthdayend != null) || (obj.txtbirthdaystart == null && obj.txtbirthdayend == null)) {
        Date1 = true;
    }
    if ((obj.txtcheckDatestart != null && obj.txtcheckDateend != null) || (obj.txtcheckDatestart == null && obj.txtcheckDateend == null)) {
        Date2 = true;
    }
    if (Date1 && Date2) {
        AspAjax.SearchHearingInspectionDataBaseCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }
}
