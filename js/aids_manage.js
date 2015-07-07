var MyBase = new Base();
var noEmptyItem = ["aStatu", "assistmanage", "brand"];
var noEmptyShow = ["狀態", "輔具類別", "廠牌"];
var noEmptyItem2 = ["lendDate", "lendPeopleID", "lendPeople", "lendDate", "returnDate"];
var noEmptyShow2 = ["出借日期", "請選擇借用人", "借用人", "出借日期", "預定歸還日"];
var noEmptyItem3 = ["serviceDate", "serviceItem", "serviceFirm"];
var noEmptyShow3 = ["維修/保養日期", "原因", "維修廠商"];
var _ColumnID = 0;
var _ReturnValue;
var _ReturnValue2;
var _aStatuList = new Array("", "借出中", "維修/保養中", "館內保存");
var _assistmanageList = new Array("", "助聽器", "電子耳及配件", "調頻系統");
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();

    $(".btnSearch").click(function() {
        $("#mainSearchList input[type=text]").add("#mainSearchList select").attr("disabled", true);
    });

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

    
    $("#setLending").click(function() {
        $("#insertLendingDiv").fadeIn();
        $("#setLending").hide();
        $("#insertLendingDiv input[type=text]").attr("disabled", false);
        $("#insertLendingDiv .date").datepick({
            yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
        });
        $("#lendDate").val(TodayADDateFunction());
        $("#lendPeople").unbind("click").click(function() {
            callStudentSearchfunction();
        });
    });

    $("#setService").click(function() {
        $("#insertServiceDiv").show();
        $("#setService").hide();
        $("#insertServiceDiv input[type=text]").attr("disabled", false);
        $("#insertServiceDiv .date").datepick({
            yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
        });
        $("#serviceDate").val(TodayADDateFunction());

    });
    assistmanagebrandFunction2();
});
function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchAidsManageDataBaseCount":
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
                        AspAjax.SearchAidsManageDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchAidsManageDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    var brandList = new Object();
                    for (var i = 0; i < result.length; i++) {
                        if (result[i].txtassistmanage == "1") {
                            brandList = _HearingList;
                        } else if (result[i].txtassistmanage == "2") {
                            brandList = _eEarList;
                        } else if (result[i].txtassistmanage == "3") {
                            brandList = _FMList;
                        }
                        var brandIndexitem = new Array();
                        var brandIndex = 0;
                        for (var j = 0; j < brandList.length; j++) {
                            brandIndexitem.push(brandList[j].ID);
                        }
                        brandIndex = brandIndexitem.indexOf(result[i].txtbrand);
                        inner += '<tr>' +
                                '<td>' + result[i].txtaID + '</td>' +
			                    '<td>' + _assistmanageList[result[i].txtassistmanage] + '</td>' +
			                    '<td>' + brandList[brandIndex].brandName + '</td>' +
			                    '<td>' + result[i].txtmodel + '</td>' +
			                    '<td>' + result[i].txtaNo + '</td>' +
			                    '<td>' + result[i].txtaSource + '</td>' +
			                    '<td>' + _aStatuList[result[i].txtaStatu] + '</td>' +
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
        case "createAidsManageDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./aids_manage.aspx?id=" + result[1] + "&act=2";
                }
            }
            break;
        case "getAidsManageDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#Unit").html(_UnitList[result.Unit]);
                    $("#assistmanage").change();
                    $("#brand").children("option[value=" + result.brand + "]").attr("selected", true);
                    var inner = "";
                    _ReturnValue = result.LoanList;
                    for (var i = 0; i < result.LoanList.length; i++) {
                        var LoanData = result.LoanList[i];
                        inner += '<tr id="HL_' + LoanData.ID + '">' +
			                        '<td height="30">' + TransformADFromStringFunction(LoanData.lendDate) + '</td>' +
			                        '<td>' + LoanData.lendPeople + '</td>' +
			                        '<td>' + TransformADFromStringFunction(LoanData.returnDate) + '</td>' +
			                        '<td><input class="date LreturnDate2" type="text" value="' + TransformADFromStringFunction(LoanData.returnDate2) + '" size="10" /></td>' +
			                        '<td><input class="Lremark" type="text" value="' + LoanData.remark + '" /></td>' +
			                        '<td><div class="UD"><button class="btnView" type="button" onclick="UpLendingData(\'' + LoanData.ID + '\')">更 新</button> <button class="btnDelete" type="button" onclick="DelLendingData(\'' + LoanData.ID + '\')">刪 除</button></div>' +
			                            '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveLendingData(\'' + LoanData.ID + '\')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpLendingData(\'' + LoanData.ID + '\',\'' + i + '\')">取 消</button></div>' +
			                        '</td>' +
			                    '</tr>';
                    }
                    $("#LendingTable").children('tbody').html(inner);
                    $("#LendingTable input[type=text]").add("#LendingTable select").attr("disabled", true);


                    _ReturnValue = result.ServiceList;
                    inner = "";
                    for (var i = 0; i < result.ServiceList.length; i++) {
                        var ServiceList = result.ServiceList[i];
                        inner += '<tr id="HS_' + ServiceList.ID + '">' +
			                        '<td height="30">' + TransformADFromStringFunction(ServiceList.serviceDate) + '</td>' +
			                        '<td>' + ServiceList.serviceItem + '</td>' +
			                        '<td>' + ServiceList.serviceFirm + '</td>' +
			                        '<td><input class="date SserviceFirmDate" type="text" value="' + TransformADFromStringFunction(ServiceList.serviceFirmDate) + '" size="10" /></td>' +
			                        '<td><input class="SsRemark" type="text" value="' + ServiceList.sRemark + '" /></td>' +
			                        '<td><div class="UD"><button class="btnView" type="button" onclick="UpServiceData(\'' + ServiceList.ID + '\')">更 新</button> <button class="btnDelete" type="button" onclick="DelServiceData(\'' + ServiceList.ID + '\')">刪 除</button></div>' +
			                            '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveServiceData(\'' + ServiceList.ID + '\')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpServiceData(\'' + ServiceList.ID + '\',\'' + i + '\')">取 消</button></div>' +
			                        '</td>' +
			                    '</tr>';
                    }
                    $("#ServiceTable").children('tbody').html(inner);
                    $("#ServiceTable input[type=text]").add("#LendingTable select").attr("disabled", true);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
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
                        $("#lendPeopleID").html($(this).children("td:nth-child(1)").html());
                        $("#lendPeople").val($(this).children("td:nth-child(2)").html());
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
            case "setAidsManageDataBase":
        case "createLendingDataBase":
        case "setLendingDataBase":
        case "delLendingDataBase":
        case "createServiceDataBase":
        case "setServiceDataBase":
        case "delServiceDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    var alertValue = "新增";
                    if (methodName == "setLendingDataBase" || methodName == "setServiceDataBase" || methodName == "setAidsManageDataBase") {
                        alertValue = "更新";
                    } else if (methodName == "delLendingDataBase" || methodName == "delServiceDataBase") {
                        alertValue = "刪除";
                    }
                    alert(alertValue + "成功");
                    window.location.reload();
                }
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function getView(id) {
    window.open("./aids_manage.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getAidsManageDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#otherTable").hide();
        $("#Unit").html(_UnitList[_uUnit]);
    }
    $(".btnDelete").show();
}

function SaveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");
    switch (Type) {
        case 0:
            var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                AspAjax.createAidsManageDataBase(obj);
            }
            break;
        case 1:
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
            $("input").add("select").add("textarea").attr("disabled", true);
            obj.ID = _ColumnID;
            AspAjax.setAidsManageDataBase(obj);
            break;
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
function InsertLending() {
    var obj = MyBase.getTextValueBase("insertLendingDiv");
    obj.mID = _ColumnID;
    obj.lendPeopleID = $("#lendPeopleID").html();
    var checkString = MyBase.noEmptyCheck(noEmptyItem2, obj, null, noEmptyShow2);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        if (obj.lendDate <= obj.returnDate) {
            AspAjax.createLendingDataBase(obj);
        } else {
            alert("預定歸還日不可大於出借日期");
        }
    }
}
function cancelInsertLending() {
    $("#insertLendingDiv").fadeOut(function() {
        $("#setLending").show();
    });
}
function UpLendingData(TrID) {
    $("#HL_" + TrID + " input[type=text]").attr("disabled", false);
    $("#HL_" + TrID + " .UD").hide();
    $("#HL_" + TrID + " .SC").show();

    $("#HL_" + TrID + " .LreturnDate2").datepick({
        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
    });
}
function SaveLendingData(TrID) {
    $("#HL_" + TrID + " input[type=text]").attr("disabled", true);
    $("#HL_" + TrID + " .UD").show();
    $("#HL_" + TrID + " .SC").hide();
    var obj = new Object;
    obj.ID = TrID;
    obj.returnDate2 = TransformRepublicReturnValue($("#HL_" + TrID + " .LreturnDate2").val());
    obj.remark = $("#HL_" + TrID + " .Lremark").val();
    AspAjax.setLendingDataBase(obj);
}
function cancelUpLendingData(TrID, index) {
    $("#HL_" + TrID + " .LreturnDate2").val(TransformADFromStringFunction(_ReturnValue[index].returnDate2));
    $("#HL_" + TrID + " .Lremark").val(_ReturnValue[index].remark);
    $("#HL_" + TrID + " input[type=text]").add("#HL_" + TrID + " select").attr("disabled", true);
    $("#HL_" + TrID + " .UD").show();
    $("#HL_" + TrID + " .SC").hide();
}
function DelLendingData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delLendingDataBase(parseInt(TrID));
    });
}
function UpServiceData(TrID) {
    $("#HS_" + TrID + " input[type=text]").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
    $("#HS_" + TrID + " .SserviceFirmDate").datepick({
        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
    });
}
function SaveServiceData(TrID) {
    $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
    var obj = new Object;
    obj.ID = TrID;
    obj.serviceFirmDate = TransformRepublicReturnValue($("#HS_" + TrID + " .SserviceFirmDate").val());
    obj.sRemark = $("#HS_" + TrID + " .SsRemark").val();
    AspAjax.setServiceDataBase(obj);
}

function cancelUpServiceData(TrID, index) {
    $("#HS_" + TrID + " .SserviceFirmDate").val(TransformADFromStringFunction(_ReturnValue[index].serviceFirmDate));
    $("#HS_" + TrID + " .SsRemark").val(_ReturnValue[index].sRemark);
    $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}
function DelServiceData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delServiceDataBase(parseInt(TrID));
    });
}
function InsertService() {
    var obj = MyBase.getTextValueBase("insertServiceDiv");
    obj.mID = _ColumnID;
    var checkString = MyBase.noEmptyCheck(noEmptyItem3, obj, null, noEmptyShow3);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createServiceDataBase(obj);
    }
}
function cancelInsertService() {
    $("#insertServiceDiv").fadeOut(function() {
        $("#setService").show();
    });

}
function search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.SearchAidsManageDataBaseCount(obj);
}