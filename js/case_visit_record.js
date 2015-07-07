var MyBase = new Base();
var noEmptyItem = ["studentID", "studentName", "target", "viewDate", "viewTime1", "viewTime2"];
var noEmptyShow = ["服務使用者編號抓取錯誤，請重新選擇學生", "學生姓名", "目的", "訪視日期", "訪視時間", "訪視時間"];
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
        $("#visitType").add("#viewSocialWorkName").add("#studentName").attr("disabled", true);
    });

    $(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

   

    $("#visitTable1").add("#visitTable2").hide();
    $("#visitType").change(function() {
        if ($(this).val() == 1) {
            $("#visitTable2").hide();
            $("#visitTable1").fadeIn();
        } else if ($(this).val() == 2) {
            $("#visitTable1").hide();
            $("#visitTable2").fadeIn();
        } else {
            $("#visitTable1").add("#visitTable2").hide();
        }
    });
    zoneCityFunction();
    visitTypeFunction();
    $("#studentName").unbind("click").click(function() {
        callStudentSearchfunction();
    });
});
function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStudentVisitDataBaseCount":
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
                    AspAjax.SearchStudentVisitDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentVisitDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
                                '<td>' + _visitType[result[i].txtvisitType] + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].txtvisitDate) + '</td>' +
			                    '<td>' + result[i].txtvisitSocial + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            }
            break;
        case "getStudentVisitDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#visitType").change();
                    $("#caseUnit").html(_UnitList[result.caseUnit]);
                    var path = "./uploads/student/" + result.studentID + "/print/";
                    if (result.pedigree.length > 0) {
                        $("#pedigree").attr("src", path + result.pedigree);
                    }
                    if (result.ecological.length > 0) {
                        $("#ecological").attr("src", path + result.ecological);
                    }
                    if (result.trafficRoute.length > 0) {
                        $("#trafficRoute").attr("src", path + result.trafficRoute);
                    }
                    if (result.viewPhoto1.length > 0) {
                        $("#viewPhoto1").attr("src", path + result.viewPhoto1);
                    }
                    if (result.viewPhoto2.length > 0) {
                        $("#viewPhoto2").attr("src", path + result.viewPhoto2);
                    }
                    if (result.viewPhoto3.length > 0) {
                        $("#viewPhoto3").attr("src", path + result.viewPhoto3);
                    }
                    if (result.viewPhoto4.length > 0) {
                        $("#viewPhoto4").attr("src", path + result.viewPhoto4);
                    }
                    if (result.viewPhoto5.length > 0) {
                        $("#viewPhoto5").attr("src", path + result.viewPhoto5);
                    }
                    if (result.viewPhoto6.length > 0) {
                        $("#viewPhoto6").attr("src", path + result.viewPhoto6);
                    }
                    if (result.viewPhoto7.length > 0) {
                        $("#viewPhoto7").attr("src", path + result.viewPhoto7);
                    }
                    if (result.viewPhoto8.length > 0) {
                        $("#viewPhoto8").attr("src", path + result.viewPhoto8);
                    }
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            } else {
                alert("查無資料。");
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
                        /*$("#studentSex").html($(this).children("td:nth-child(3)").html());
                        $("#studentbirthday").html($(this).children("td:nth-child(4)").html());*/
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "createStudentVisitDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./case_visit_record.aspx?id=" + result[1] + "&act=2";
                }
            }
            break;
        case "setStudentVisitDataBase":
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
    window.open("./case_visit_record.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getStudentVisitDataBase(id);
    } else if (id == null && act == 1) {
    $(".btnSave").fadeIn();
    $("#caseUnit").html(_UnitList[_uUnit]); $("#viewSocialWork").val(_uName);
     
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

function saveData(Type) {
    var visitType = $("#visitType :selected").val();
    if (parseInt(visitType) > 0) {

        var obj1 = MyBase.getTextValueBase("tableContent");
        var obj2 = MyBase.getTextValueBase("visitTable" + visitType);
        var obj = MergerObject(obj1, obj2);
        obj.studentID = $("#studentID").html();
        obj.visitType = visitType;
        
        var picSave = false;
        var PicNameArray;
        if (obj.studentID != null) {
            var options = {
                type: "POST",
                url: 'Files.ashx?n=student&type=VisitRecord&ID=' + obj.studentID,
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
                        AspAjax.createStudentVisitDataBase(obj);
                    }
                    break;
                case 1:
                    $(".btnSaveUdapteData").add(".btnCancel").hide();
                    $(".btnUpdate").fadeIn();
                    $("input").add("select").add("textarea").attr("disabled", true);
                    obj.ID = _ColumnID;
                    AspAjax.setStudentVisitDataBase(obj);
                    break;
            }
        }
    } else {
        alert("請選擇訪視類別");
    }
}
function visitTypeFunction() {
    $(".visitTypeList").append($("<option></option>").attr("value", 0).text("請選擇"));
    for (var i = 1; i < _visitType.length; i++) {
        $(".visitTypeList").append($("<option></option>").attr("value", parseInt(i)).text(_visitType[i]));
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
    if ((obj.txtvisitDatastart != null && obj.txtvisitDataend != null) || (obj.txtvisitDatastart == null && obj.txtvisitDataend == null)) {
        Date2 = true;
    }
    if (Date1 && Date2) {
        AspAjax.SearchStudentVisitDataBaseCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }
}