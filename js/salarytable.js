var MyBase = new Base();
var noEmptyItem = ["staffName", "staffID", "fillInDate", "yearDate", "monthDate"];
var noEmptyShow = ["員工姓名", "員工編號抓取錯誤，請重新選擇員工", "填表日期", "薪資日期-民國年", "薪資日期-月"];
var _ColumnID = 0;
var _addItemLength = 1;
var _minusItemLength = 1;
var _RealWagesInt = 0;
var _oldAddItem = new Array();
var _oldMinusItem = new Array();
var _ThisSaly = {};
var _AddMoney = 0;
var _MinsMoney = 0;

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    setDate(1990);


    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#fillInDate").add("#staffName").add("#yearDate").add("#monthDate").add("#fillInDate").attr("disabled", true);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });


    $("#salaryDeductions").unbind('click').click(function() {
        $(this).select();
    }).unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        RealwagesFunction();
    }).unbind('blur').blur(function() {
        if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
            $(this).val("0");
        }
        else {
            $(this).val((parseInt($(this).val(), 10)));
        }
    });
});
function setDate(year_start) {
    var now = new Date();

    //年(year_start~今年)
    for (var i = (now.getFullYear() - 1911); i >= (year_start - 1911); i--) {
        $('#yearDate').append($("<option></option>").attr("value", i).text(i));
    }

    //月
    for (var i = 1; i <= 12; i++) {
        $('#monthDate').append($("<option></option>").attr("value", i).text(i));
    }
}

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    // alert(methodName);
    switch (methodName) {
        case "SearchStaffSalaryBaseCount":
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
                        AspAjax.SearchStaffSalaryDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffSalaryDataBase":
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
                    $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>發生錯誤，錯誤訊息如下："+ result[0].errorMsg+"</td></tr>");
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
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
                        $("#Unit").html(_UnitList[Unit]);
                        var ThisMonth = (parseInt($('#yearDate').val()) + 1911) + "-" + $('#monthDate').val() + "-28";
                        AspAjax.getStaffContractedSalaryLatestDataBase(id, ThisMonth, 0);
                        $("#explanationList tbody").html("");
                        $.fancybox.close();
                    });

                } else {
                    $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[0].errorMsg + "</td></tr>");
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "getStaffContractedSalaryLatestDataBase":
            if (!(result == null || result.length == 0 || result == undefined) && result.ID != null) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    _ThisSaly = result;
                    $("#laborInsurance").html(result.laborInsurance);
                    $("#healthInsurance").html(result.healthInsurance);
                    $("#pensionFunds").html(result.pensionFunds);
                    $("#pensionFundsPer").html("(" + result.pensionFundsPer + "%)");
                    $("#withholdingTax").html(result.withholdingTax);
                    $("#totalSalary").html(result.totalSalary);
                    RealwagesFunction();
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            } else {
                alert("此人無歷史資料紀錄");
            }
            break;
        case "createStaffSalaryDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = "./salarytable.aspx?id=" + result[1] + "&act=2";
            }
            break;
        case "getStaffSalaryDataBase":
            if (!(result == null || result.length == 0 || result == undefined) && result.ID != null) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#pensionFundsPer").html("(" + result.pensionFundsPer + "%)");
                    $("#Unit").html(_UnitList[result.Unit]);
                    $("input").add("select").add("textarea").attr("disabled", false);
                    _AddMoney = result.AddMoney;
                    _MinsMoney = result.MinsMoney;
                    console.log(result, _AddMoney, _MinsMoney);
                    for (var i = 0; i < result.addItem.length; i++) {
                        $("#addItem_0 .addSelect").children("option[value='" + result.addItem[i].project + "']").attr("selected", true);
                        getAddItem("add");
                        $("#addItem_" + (_addItemLength - 1) + " .price").val(result.addItem[i].projectMoney);
                        $("#addItem_" + (_addItemLength - 1) + " .explain").val(result.addItem[i].explain);
                        $("#addItem_" + (_addItemLength - 1) + " img").attr("onclick", "delAddItem(\'add\',\'" + (_addItemLength - 1) + "\')");
                        _oldAddItem.push(result.addItem[i].project);

                    }
                    for (var i = 0; i < result.minusItem.length; i++) {
                        $("#minusItem_0 .minusSelect").children("option[value='" + result.minusItem[i].project + "']").attr("selected", true);
                        getAddItem("minus");
                        $("#minusItem_" + (_minusItemLength - 1) + " .price").val(result.minusItem[i].projectMoney);
                        $("#minusItem_" + (_minusItemLength - 1) + " .explain").val(result.minusItem[i].explain);
                        $("#minusItem_" + (_addItemLength - 1) + " img").attr("onclick", "delAddItem(\'minus\',\'" + (_addItemLength - 1) + "\')");
                        _oldMinusItem.push(result.minusItem[i].project);
                    }
                    //RealwagesFunction();
                    _RealWagesInt = result.realWages;
                    $("#realWages").html(FormatNumber(result.realWages));
                    $("input").add("select").add("textarea").attr("disabled", true);
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            }
            break;
        case "setStaffSalaryDataBase":
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
    window.open("./salarytable.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    AgencyUnitSelectFunction("mainSearchForm", "gosrhstaffUnit");
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getStaffSalaryDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#staffName").unbind("click").click(function() {
            callStaffSearchfunction();
        });
    }
}

function setDate(year_start) {
    var now = new Date();

    //年(year_start~今年)
    for (var i = (now.getFullYear() - 1911); i >= (year_start - 1911); i--) {
        $('#yearDate').add('#yearDate2').add('#yearDate3').append($("<option></option>").attr("value", i).text(i));
    }

    //月
    for (var i = 1; i <= 12; i++) {
        $('#monthDate').add('#monthDate2').append($("<option></option>").attr("value", i).text(i));
    }
}

function getAddItem(divName) {

    /*var itemLength = $("#" + divName + "ItemDiv").find("." + divName + "ItemList").length;
    if (itemLength > 0) {
    
    alert($("#addItemDiv").find(".addItemList:last-child").attr("id"));
    var itemLength = ($("#addItemDiv").find(".addItemList:last-child").attr("id")).replace("addItem_", "");
    itemLength = parseInt(itemLength) + 1;
    } else {
    itemLength = 1;
    }*/
    var itemSelect = $("#" + divName + "Item_0 ." + divName + "Select :selected").val();
    if (itemSelect != "0" && parseInt(itemSelect) != 0) {
        var itemLength = 0;
        if (divName == "add") {
            itemLength = _addItemLength;
            _addItemLength++;
        } else if (divName == "minus") {
            itemLength = _minusItemLength;
            _minusItemLength++;
        }

        var inner = '<ul id="' + divName + "Item_" + itemLength + '" class="' + divName + 'ItemList">' +
		            '<li class="thSystem"><span class="hideClassSpan">' + itemSelect + '</span></li>' +
		            '<li class="thItem">' + itemSelect + '</li>' +
		            '<li class="thItem"><input type="text" value="0" class="price"></li>' +
		            '<li class="thItem2"><input type="text" value="" class="explain"></li>' +
		            '<li class="thSystem"><img alt="" src="./images/x.png" class="ImgJs" onclick="delAddItem(\'' + divName + '\',\'' + itemLength + '\')"></li>' +
		        '</ul>';
        $("#" + divName + "Item_0").before(inner);
        $("#" + divName + "Item_0 ." + divName + "Select :selected").attr("disabled", true);
        $("#" + divName + "Item_0 ." + divName + "Select [value='0']").attr("selected", "selected");


        $("." + divName + "ItemList .price").unbind('click').click(function() {
            $(this).select();
        }).unbind('keydown').keydown(function(event) {
            if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8)) {
                event.preventDefault();
            }
        }).unbind('keyup').keyup(function() {
            RealwagesFunction();
        }).unbind('blur').blur(function() {
            if (parseInt($(this).val(), 10) <= 0 || $(this).val() == "") {
                $(this).val("0");
            }
            else {
                $(this).val((parseInt($(this).val(), 10)));
            }
        });
    } else {
        alert("請選擇項目");
    }
}

function delAddItem(divName, itemLength) {
    var itemText=$("#" + divName + "Item_" + itemLength).children(".thItem:first").html();
    fancyConfirm("刪除 " + itemText + " 項目?", function() {
        $("#" + divName + "Item_0 ." + divName + "Select [value=" + $("#" + divName + "Item_" + itemLength + " .hideClassSpan:first").html() + "]").attr("disabled", false);
        $("#" + divName + "Item_" + itemLength).remove();
        $.fancybox.close();
    });

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
    var obj = new Object();
    obj = MyBase.getTextValueBase("mainContent");
   // MergerObject(obj, _ThisSaly);
    obj.staffID = $("#staffID").html();
    obj.realWages = _RealWagesInt;
    obj.addItem = new Object();
    obj.addItem = ItemObjFunction("add");
    obj.minusItem = new Object();
    obj.minusItem = ItemObjFunction("minus");
    obj.laborInsurance = ItemisNumber($("#laborInsurance").html());
    obj.healthInsurance = ItemisNumber($("#healthInsurance").html());
    obj.pensionFunds = ItemisNumber($("#pensionFunds").html());
    obj.withholdingTax = ItemisNumber($("#withholdingTax").html());
    obj.salaryDeductions = ItemisNumber($("#salaryDeductions").html());
    obj.totalSalary = ItemisNumber($("#totalSalary").html());
    obj.pensionFundsPer = ItemisNumber($("#pensionFundsPer").html().replace("(", "").replace("%)", ""));
    obj.AddMoney = _AddMoney;
    obj.MinsMoney = _MinsMoney;
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0 && _RealWagesInt >0) {
        alert(checkString);
    } else {
        switch (Type) {
            case 0:
                console.log(obj);
                AspAjax.createStaffSalaryDataBase(obj);
                break;
            case 1:
                $(".btnSaveUdapteData").add(".btnCancel").hide();
                $(".btnUpdate").fadeIn();
                $("input").add("select").add("textarea").attr("disabled", true);
                obj.ID = _ColumnID;

                var DelAddItem = new Array();
                var AddItemList = new Array();
                //var NewAddItem = new Array();
                $(".addItemList").each(function() {
                    AddItemList.push($(this).find(".thItem:first").html());
                });
                DelAddItem = array_diff(_oldAddItem, AddItemList);  //被取消的
                //NewAddItem = array_diff(AddItemList, _oldAddItem);  //新增加的

                var DelMinusItem = new Array();
                var MinusItemList = new Array();
                //var NewMinusItem = new Array();
                $(".minusItemList").each(function() {
                    MinusItemList.push($(this).find(".thItem:first").html());
                });
                DelMinusItem = array_diff(_oldMinusItem, MinusItemList);  //被取消的
                //NewMinusItem = array_diff(MinusItemList, _oldMinusItem);  //新增加的

                AspAjax.setStaffSalaryDataBase(obj, DelAddItem, DelMinusItem);
                break;
        }
    }
}
function itemStruct() {
    this.ID = "";
    this.salaryID = "";
    this.project = "";
    this.projectMoney = "";
    this.explain = "";
    this.checkNo = "";
    this.errorMsg = "";
}
function ItemObjFunction(divName) {
    var ObjList = new Array();
    $("." + divName + "ItemList").each(function() {
        var obj = new itemStruct();
        obj.project = $(this).find(".thItem:first").html();
        obj.projectMoney = $(this).find(".price").val();
        obj.explain = $(this).find(".explain").val();
        obj.salaryID = _ColumnID;
        ObjList.push(obj);
    });
    return ObjList;
}

function RealwagesFunction() {
    var realWages = 0;
    var laborInsurance = ItemisNumber($("#laborInsurance").html());
    var healthInsurance = ItemisNumber($("#healthInsurance").html());
    var pensionFunds = ItemisNumber($("#pensionFunds").html());
    var withholdingTax = ItemisNumber($("#withholdingTax").html());
    var salaryDeductions = ItemisNumber($("#salaryDeductions").val());
    var baseSalary = ItemisNumber($("#totalSalary").html());
    var addSalary=0;
    $(".addItemList .price").each(function() {
        addSalary += ItemisNumber($(this).val());
    });
    var minusSalary = 0;
    $(".minusItemList .price").each(function() {
        minusSalary += ItemisNumber($(this).val());
    });
    _AddMoney = baseSalary + addSalary;
    _MinsMoney = laborInsurance + healthInsurance + pensionFunds + withholdingTax + salaryDeductions + minusSalary;
    realWages = baseSalary - laborInsurance - healthInsurance - pensionFunds - withholdingTax - salaryDeductions + addSalary - minusSalary;
    _RealWagesInt = realWages;
    $("#realWages").html(FormatNumber(realWages.toString()));
}

function SearchData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var SearchBirthday = false;
    if ((obj.txtstaffBirthdayStart != null && obj.txtstaffBirthdayEnd != null) || (obj.txtstaffBirthdayStart == null && obj.txtstaffBirthdayEnd == null)) {
        SearchBirthday = true;
    }
    if (SearchBirthday) {
        AspAjax.SearchStaffSalaryBaseCount(obj);
    } else {
        alert("請填寫完整出生日期區間");
    }
}