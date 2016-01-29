var MyBase = new Base();
var noEmptyItem = ["staffName", "staffID", "fillInDate"];
var noEmptyShow = ["員工姓名", "員工編號抓取錯誤，請重新選擇員工", "填表日期"];
var _director = ["0", "180", "120", "90", "90", "60", "60", "30"];
var _ColumnID = 0;
var _SalaryValue = 0; //點值
var _SalaryLatestData;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);

    initPage();
    
    $("#item1").add("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#item1Content").fadeIn();
    
    $(".menuTabs").click(function() {
        $(".menuTabs").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("input").add("select").add("textarea").attr("disabled", true);
        if (id == null && (act == 1 || act == 31)) {
            $("input").add("select").add("textarea").attr("disabled", false);
        } else {
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
        }
        getStudentData(this.id);
    });

    $(".showUploadImg").fancybox();

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#fillInDate").add("#staffName").add("#Unit").attr("disabled", true);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

    $(".btnAdd").click(function() {
        $("#item8Content>table tr:last-child").after($("#whTable").clone().attr("id", ""));
    });

    
    $("#education").change(function() {
        $("#count1").html($("#education :selected").val());
        TotalCountFunction();
    });
    $("#years").unbind('click').click(function() {
        $(this).select();
    }).unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 110)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        var thisValue = $(this).val();
        if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
            thisValue = 0;
        } else {
            thisValue = parseInt(thisValue);
        }
        $("#count2").html(parseFloat(thisValue) * 2);
        TotalCountFunction();
    }).unbind('blur').blur(function() {
        if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
            $(this).val("0");
            $("#count2").html("0")
            TotalCountFunction();
        }
        else {
            $(this).val((parseInt($(this).val(), 10)));
        }
    });
    $("#applyJob").add("#jobLevel").change(function() {
        var applyJob = $("#applyJob :selected").val();
        var jobLevel = $("#jobLevel :selected").val();
        var oldapplyJob = 0;
        var oldjobLevel = 0;

        var count3 = parseInt(applyJob, 10) + (parseInt(jobLevel, 10) * 2);
        if (_SalaryLatestData != undefined) {
            oldapplyJob = _SalaryLatestData.applyJob != null ? _SalaryLatestData.applyJob : 0;
            oldjobLevel = _SalaryLatestData.jobLevel != null ? _SalaryLatestData.jobLevel : 0;
        }
        if (parseInt(applyJob, 10) > parseInt(oldapplyJob, 10)) {
            count3 += parseInt(oldapplyJob, 10) + (parseInt(oldjobLevel, 10) * 2);
        }
        $("#count3").html(count3);
        TotalCountFunction();
    });
    $("#director").change(function() {

        $("#count4").val(_director[$("#director :selected").val()]);
        //$("#count4").html($("#director :selected").val());
        TotalCountFunction();
    });
    $("#count5").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 110)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        if (parseInt($(this).val(), 10) >= 0 && $(this).val() != "" && ($(this).val()).length > 0) {
            TotalCountFunction();
        }

    }).unbind('blur').blur(function() {
        if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
            $(this).val("0");
            TotalCountFunction();
        }
        else {
            $(this).val((parseInt($(this).val(), 10)));
        }
    });

    $("#special").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 110)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        if (parseInt($(this).val(), 10) >= 0 && $(this).val() != "" && ($(this).val()).length > 0) {
            TotalCountFunction();
        }

    }).unbind('blur').blur(function() {
        if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
            $(this).val("0");
            TotalCountFunction();
        }
        else {
            $(this).val((parseInt($(this).val(), 10)));
        }
    });

    $("#EndPoint").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 110)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        if (parseInt($(this).val(), 10) >= 0 && $(this).val() != "" && ($(this).val()).length > 0) {
            TotalCountFunction();
        }

    }).unbind('blur').blur(function() {
        if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
            $(this).val("0");
            TotalCountFunction();
        }
        else {
            $(this).val((parseInt($(this).val(), 10)));
        }
    });
    
    /*$("#total").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 110)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        if (parseInt($(this).val(), 10) >= 0 && $(this).val() != "" && ($(this).val()).length > 0) {
            $("#totalSalary").html(Math.round(parseInt($(this).val(), 10) * parseFloat(_SalaryValue)));
        }

    }).unbind('blur').blur(function() {
        if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
            $(this).val("0");
            TotalCountFunction();
        }
        else {
            $(this).val((parseInt($(this).val(), 10)));
        }
    });*/

});
function TotalCountFunction() {
    var count1 = $("#count1").html();
    var count2 = $("#count2").html();
    var count3 = $("#count3").html();
    var count4 = $("#count4").val();
    var count5 = $("#count5").val();
    var count6 = $("#special").val();
    var count7 = $("#EndPoint").val();
    var total = 0;
    total = parseInt(count1, 10) + parseInt(count2, 10) + parseInt(count3, 10) + parseInt(count4, 10) + parseInt(count5, 10) + parseInt(count6, 10) + parseInt(count7, 10);
    $("#total").html(total);
    $("#totalSalary").html(Math.round(total * parseFloat(_SalaryValue)));
}
function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    // alert(methodName);
    switch (methodName) {
        case "getSalaryValue":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) != -1) {
                    _SalaryValue = result[0];
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                }
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
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="s_' + result[i].sID + '">' +
                                '<td>' + result[i].sID + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td style="display:none;">' + result[i].sUnit + '</td>' +
			                    '<td>' + result[i].sName + '</td>' +
			                '</tr>';
                    }
                    //
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("s_", "");
                        $("#staffID").html(id);
                        var Name = $(this).children("td:nth-child(4)").html();
                        $("#staffName").val(Name);
                        var Unit = $(this).children("td:nth-child(3)").html();
                        $("input[name=Unit][value=" + Unit + "]").attr("checked", true);
                        $("input[name=Unit]").attr("disabled", true);
                        //AspAjax.getStaffSalaryexplanationDataBase(id);
                        AspAjax.getStaffContractedSalaryLatestDataBase(id,"","");
                        $("#explanationList tbody").html("");
                        $.fancybox.close();
                    });


                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "createStaffContractedSalaryDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = "./staff_contracted_salary.aspx?id=" + result[1] + "&act=2";
            }
            break;
        case "getStaffContractedSalaryLatestDataBase":
        case "getStaffContractedSalaryDataBase":
            console.log(result);
            if (!(result == null || result.length == 0 || result == undefined) && result.ID != null) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    AspAjax.getStaffSalaryexplanationDataBase(result.staffID);
                    if (methodName == "getStaffContractedSalaryLatestDataBase") {
                        _SalaryLatestData = result;
                    } else {
                        AspAjax.getStaffContractedSalaryLatestDataBase1(result.staffID, result.fillInDate, result.ID);
                    }
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            } else if (parseInt(result.checkNo) == -2) {
                $("body").empty();
                alert(result.errorMsg);
                window.location.href = "./main.aspx";
            } else {
                alert("此人無歷史資料紀錄");
            }
            break;
        case "getStaffContractedSalaryLatestDataBase1":
            if (!(result == null || result.length == 0 || result == undefined) && result.ID != null) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    _SalaryLatestData = result;
                }
            }
            break;
        case "getStaffSalaryexplanationDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) != -1) {
                    var inner = '';
                    for (var i = 0; i < result.length; i++) {
                        var edata = result[i];
                        inner += '<tr>' +
                                    '<td>' + TransformADFromStringFunction(edata[0]) + '</td>' +
                                    '<td>' + edata[1] + '</td>' +
                                '</tr>';
                    }
                    $("#explanationList tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result[1]);
                }
            }
            break;
        case "setStaffContractedSalaryDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.href = "./staff_contracted_salary.aspx?id=" + result[0] + "&act=2";
                //window.location.reload();
            }
            break;
        case "SearchStaffContractedSalaryBaseCount":
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
                        AspAjax.SearchStaffContractedSalaryDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffContractedSalaryDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].sID + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td>' + result[i].sName + '</td>' +
			                    '<td>' + _SexList[result[i].sSex] + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].FileDate) + '</td>' +
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

    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);

}
function getView(id) {
    window.open("./staff_contracted_salary.aspx?id=" + id + "&act=2" );
}

function getViewData(id, act) {
    AspAjax.getSalaryValue();
    AgencyUnitSelectFunction("mainSearchForm", "gosrhstaffUnit");
    AgencyUnitRadioFunction("item1Content", "sUnit", "Unit");
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getStaffContractedSalaryDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);

        $("#staffName").unbind("click").click(function() {
            callStaffSearchfunction();
        });
    }
}

function SearchStaffData() {
    var sName = $("#sName").val();
    var sBirthdayStart = $("#sBirthdayStart").val();
    var sBirthdayEnd = $("#sBirthdayEnd").val();
    var sID = $("#sID").val();
    var SearchBirthday = false;
    if (sBirthdayStart.length > 0 && sBirthdayEnd.length > 0) {
        SearchBirthday = true;
    } else {
        if (sBirthdayStart.length == 0 && sBirthdayEnd.length == 0) {
            SearchBirthday = true;
        } else {
            alert("請填寫完整出生日期區間");
        }
    }
    if (SearchBirthday) {
        //AspAjax.SearchStaffDataBaseCount(sID, sName, parseInt($("#sSex :selected").val()), sBirthdayStart, sBirthdayEnd, parseInt($("#sJob :selected").val()), parseInt($("#sUnit :selected").val()));
    }
}
function callStaffSearchfunction() {
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

}
function saveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");
    var obj1 = getHideSpanValue("mainContent", "hideClassSpan");
    MergerObject(obj, obj1);
    obj.count1 = $("#count1").html();
    obj.count2 = $("#count2").html();
    obj.count3 = $("#count3").html();
    obj.count4 = $("#count4").val();
    obj.total = $("#total").html();
    obj.totalSalary = $("#totalSalary").html();
    console.log(obj);
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        switch (Type) {
            case 0:
                AspAjax.createStaffContractedSalaryDataBase(obj);
                break;
            case 1:
                $(".btnSaveUdapteData").add(".btnCancel").hide();
                $(".btnUpdate").fadeIn();
                $("input").add("select").add("textarea").attr("disabled", true);
                obj.ID = _ColumnID;
                AspAjax.setStaffContractedSalaryDataBase(obj);
                break;
        }
    }
}
function SearchData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var SearchBirthday = false;
    if ((obj.txtstaffBirthdayStart != null && obj.txtstaffBirthdayEnd != null) || (obj.txtstaffBirthdayStart == null && obj.txtstaffBirthdayEnd == null)) {
        SearchBirthday = true;
    }
    if (SearchBirthday) {
        AspAjax.SearchStaffContractedSalaryBaseCount(obj);
    } else {
        alert("請填寫完整出生日期區間");
    }
}