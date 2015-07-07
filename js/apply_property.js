var MyBase = new Base();
var noEmptyItem = ["applyType", "applyPay", "applyDate"];
var noEmptyShow = ["申請項目", "付款方式", "填表日期"];
var _money = new Array("零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖");
var _applyType = new Array("", "請購", "請修");
var _applyPay = new Array("", "現金", "支票");
var _applyStatus = new Array("未列印", "已列印", "審核中", "審核通過", "審核駁回", "作廢");
var _ColumnID = 0;
var _ReturnValue;
var _nowStatus = 0;

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    
    
    $(".btnUpdate").click(function() {
        $(".btnUpdate").add(".btnPrint").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input[name='applyPay']").add("#applyDate").attr("disabled", false);
        if (_nowStatus != 0) {
            $("#applyStatus").attr("disabled", false);
        }
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").add(".btnPrint").fadeIn();
        $("input[name='applyPay']").add("#applyDate").attr("disabled", true);
        if (_nowStatus != 0) {
            $("#applyStatus").attr("disabled", true);
        }
    });

    $(".propertyQuantity").add(".propertyPrice").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 9) && !(event.keyCode == 46)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function(event) {
        if (event.keyCode != 13) {
            countPrice($(this).parents("tr").attr("id"));
        }
    });

   
});

function DetailObject() {
    this.Name = '';
    this.Unit = '';
    this.Quantity = 0;
    this.Format = '';
    this.Price = 0;
    this.Explain = '';
    this.Bill = '';
}

function countPrice(mainID) {
    var propertyQuantity = $("#" + mainID + " .propertyQuantity").val();
    var propertyPrice = $("#" + mainID + " .propertyPrice").val();
    var propertySum = 0;
    if (propertyQuantity.length > 0 && propertyPrice.length > 0) {
        propertySum = propertyQuantity * propertyPrice;
    } else {
        propertySum = 0;
    }
    $("#" + mainID + " .propertySum").html(propertySum);
    var applySum = 0;
    if (propertySum != 0) {
        $(".propertySum").each(function() {
            applySum += parseInt($(this).html(), 10);
        });
        $("#applySum").html(applySum);
        DigitShow(applySum);
    }
}

function DigitShow(applySum) {
    /*var sumArray = ((applySum).toString()).split("");
    var digitArray = new Array(0, 0, 0, 0, 0);

    switch (((applySum).toString()).length) {
        case 1:
            digitArray[4] = sumArray[0];
            break;
        case 2:
            digitArray[4] = sumArray[1];
            digitArray[3] = sumArray[0];
            break;
        case 3:
            digitArray[4] = sumArray[2];
            digitArray[3] = sumArray[1];
            digitArray[2] = sumArray[0];
            break;
        case 4:
            digitArray[4] = sumArray[3];
            digitArray[3] = sumArray[2];
            digitArray[2] = sumArray[1];
            digitArray[1] = sumArray[0];
            break;
        case 5:
            digitArray = sumArray;
            break;
        default:
            
            break;
    }*/

    /*$("#sumOne").val(_money[parseInt(digitArray[4], 10)]);
    $("#sumTwo").val(_money[parseInt(digitArray[3], 10)]);
    $("#sumThree").val(_money[parseInt(digitArray[2], 10)]);
    $("#sumFour").val(_money[parseInt(digitArray[1], 10)]);
    $("#sumFive").val(_money[parseInt(digitArray[0], 10)]);*/
    pushsunValue(applySum);

}
function pushsunValue(applySum) {
    var sunDiv = new Array("sumOne", "sumTwo", "sumThree", "sumFour", "sumFive");
    for (var i = 0; i < sunDiv.length; i++) {
        var astart = applySum.length - (i + 1);
        if (astart != 0 && i == sunDiv.length-1) {
            astart = 0;
        }
        var sunNum = applySum.substring(astart, applySum.length - i);
        if (sunNum.length == 0) {
            sunNum = 0;
        }
        $("#" + sunDiv[i]).val(sunNum);
    }
}
function detailInsert() {
    var num = $("#mainContent .tableContact tbody tr").length + 1;
    if (num <= 4) {
        $("#mainContent .tableContact tbody").append($("#Detail1").clone().attr("id", "Detail" + num));
        $("#Detail" + num + " input").val("");
        $("#Detail" + num + " .propertySum").html(0);
    }

    $(".propertyQuantity").add(".propertyPrice").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 9) && !(event.keyCode == 46)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function(event) {
        if (event.keyCode != 13) {
            countPrice($(this).parents("tr").attr("id"));
        }
    });
}

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "createApplyPropertyDataBase":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = './apply_property.aspx?id=' + result[1] + '&act=2';
            }
            break;
        case "searchApplyPropertyDataBaseCount":
            var obj = MyBase.getTextValueBase("searchTable");
            var pageCount = parseInt(result[0], 10);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        AspAjax.searchApplyPropertyDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchApplyPropertyDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].checkNo, 10) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        var applyStr = '';
                        
                        if (parseInt(result[i].txtapplyID, 10) != 0) {
                            applyStr = result[i].txtapplyID;
                        }
                        inner += '<tr>' +
                                '<td>' + applyStr + '</td>' +
                                '<td>' + _applyType[result[i].txtapplyType] + '</td>' +
                                '<td>' + _applyPay[result[i].txtapplyPay] + '</td>' +
                                '<td>' + _applyStatus[result[i].txtapplyStatus] + '</td>' +
                                '<td>' + result[i].txtapplyBy + '</td>' +
                                '<td>' + result[i].txtapplySum + '</td>' +
                                '<td>' + TransformADFromStringFunction(result[i].txtapplyDate) + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
            }
            break;
        case "getApplyPropertyDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#sUnit").html(_UnitList[result.Unit]);
                    _nowStatus = result.applyStatus;
                    var inner = "";
                    _ReturnValue = result.DetailArray;
                    for (var i = 0; i < result.DetailArray.length; i++) {
                        var DetailData = result.DetailArray[i];
                        inner += '<tr id="HS_' + DetailData.pID + '">' +
                                    '<td height="40"><input class="propertyName" type="text" value="' + DetailData.Name + '" autocomplete="off" /></td>' +
			                        '<td><input class="propertyUnit" type="text" value="' + DetailData.Unit + '" autocomplete="off" size="3" /></td>' +
			                        '<td><input class="propertyQuantity" type="text" value="' + DetailData.Quantity + '" autocomplete="off" size="3" /></td>' +
			                        '<td><input class="propertyFormat" type="text" value="' + DetailData.Format + '" autocomplete="off" size="5" /></td>' +
			                        '<td><input class="propertyPrice" type="text" value="' + DetailData.Price + '" autocomplete="off" size="5" /></td>' +
			                        '<td class="propertySum">' + (DetailData.Quantity * DetailData.Price) + '</td>';
                        if (_nowStatus == 0) {
                            inner += '<td><input class="propertyExplain" type="text" value="' + DetailData.Explain + '" autocomplete="off" size="15" /></td>' +
			                        '<td><input class="propertyBill" type="text" value="' + DetailData.Bill + '" autocomplete="off" size="13" /></td><td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + DetailData.pID + ')">更 新</button> <button class="btnView" type="button" onclick="DelData(' + DetailData.pID + ')">刪 除</button></div>' +
			                        '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(' + DetailData.pID + ')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(' + DetailData.pID + ',' + i + ')">取 消</button></div></td>';
                        } else {
                            inner += '<td><input class="propertyExplain" type="text" value="' + DetailData.Explain + '" autocomplete="off" size="30" /></td>' +
			                        '<td><input class="propertyBill" type="text" value="' + DetailData.Bill + '" autocomplete="off" size="13" /></td>';
                        }
                        inner += '</tr>';
                    }
                    if (_nowStatus == 0) {
                        $("#statusP").hide();
                        $("#DetailTable thead tr").append('<th>功能</th>');
                    } else {
                    $("#statusP").show().append('　　申請單流水號 ' + result.applyID);
                        $(".btnUpdate").hide();
                    }
                    $("#DetailTable tbody").empty().html(inner);
                    $(".propertyQuantity").add(".propertyPrice").unbind('keydown').keydown(function(event) {
                        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 9) && !(event.keyCode == 46)) {
                            event.preventDefault();
                        }
                    }).unbind('keyup').keyup(function(event) {
                        if (event.keyCode != 13) {
                            countPrice($(this).parents("tr").attr("id"));
                        }
                    });
                    DigitShow(result.applySum);
                    $("#DetailTable tbody input[type='text']").attr("disabled", true);
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
        case "setApplyPropertyDataBase":
        case "setApplyPropertyDetail":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "delApplyPropertyDetail":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                $.fancybox.close();
                alert("刪除成功");
                window.location.reload();
            }
            break;
        case "printApplyPropertyDataBase":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                $.fancybox.close();
                wOpen();
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function getView(id) {
    window.open("./apply_property.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").add(".btnPrint").fadeIn();
        $(".btnAdd").hide();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getApplyPropertyDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#applyDate").val(TodayADDateFunction());
        $("#sUnit").html(_UnitList[_uUnit]); $("#applyBy").html(_uName);
        $("#statusP").hide();
    }
}

function search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    if ((obj.txtapplyDateStart != null && obj.txtapplyDateEnd != null) || (obj.txtapplyDateStart == null && obj.txtapplyDateEnd == null)) {
        AspAjax.searchApplyPropertyDataBaseCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }
}

function UpData(TrID) {
    $("#HS_" + TrID + " input[type='text']").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}

function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .propertyName").val(_ReturnValue[vIndex].Name);
    $("#HS_" + TrID + " .propertyUnit").val(_ReturnValue[vIndex].Unit);
    $("#HS_" + TrID + " .propertyQuantity").val(_ReturnValue[vIndex].Quantity);
    $("#HS_" + TrID + " .propertyFormat").val(_ReturnValue[vIndex].Format);
    $("#HS_" + TrID + " .propertyPrice").val(_ReturnValue[vIndex].Price);
    $("#HS_" + TrID + " .propertyExplain").val(_ReturnValue[vIndex].Explain);
    $("#HS_" + TrID + " .propertyBill").val(_ReturnValue[vIndex].Bill);
    $("#HS_" + TrID + " input[type='text']").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
    countPrice("HS_" + TrID);
}

function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delApplyPropertyDetail(TrID);
    });
}

function SaveData(TrID) {
    var obj = new Object();
    obj.pID = parseInt(TrID);
    var propertyName = $("#HS_" + TrID + " .propertyName").val();
    var propertyUnit = $("#HS_" + TrID + " .propertyUnit").val();
    var propertyQuantity = $("#HS_" + TrID + " .propertyQuantity").val();
    var propertyFormat = $("#HS_" + TrID + " .propertyFormat").val();
    var propertyPrice = $("#HS_" + TrID + " .propertyPrice").val();
    var propertyExplain = $("#HS_" + TrID + " .propertyExplain").val();
    var propertyBill = $("#HS_" + TrID + " .propertyBill").val();

    if (propertyName.length > 0 && propertyUnit.length > 0 && propertyQuantity.length > 0 && propertyPrice.length > 0) {//&& propertyFormat.length > 0
        obj.Name = propertyName;
        obj.Unit = propertyUnit;
        obj.Quantity = propertyQuantity;
        obj.Format = propertyFormat;
        obj.Price = propertyPrice;
        obj.Explain = propertyExplain;
        obj.Bill = propertyBill;
        obj.Sum = $("#applySum").html();
        obj.aID = _ColumnID;
        $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setApplyPropertyDetail(obj);
    } else {
        alert("請填寫完整");
    }
}

function saveOffData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");
    switch (Type) {
        case 0:
            var noEmptyStr = '';
            var DetailArray = new Array();
            for (i = 1; i <= $("#mainContent .tableContact tbody tr").length; i++) {
                var Detailitem = new DetailObject();
                Detailitem.Name = $("#Detail" + i + " .propertyName").val();
                Detailitem.Unit = $("#Detail" + i + " .propertyUnit").val();
                Detailitem.Quantity = $("#Detail" + i + " .propertyQuantity").val();
                Detailitem.Format = $("#Detail" + i + " .propertyFormat").val();
                Detailitem.Price = $("#Detail" + i + " .propertyPrice").val();
                Detailitem.Explain = $("#Detail" + i + " .propertyExplain").val();
                Detailitem.Bill = $("#Detail" + i + " .propertyBill").val();
                if (Detailitem.Name != "" && Detailitem.Unit != "" && Detailitem.Quantity != "" && Detailitem.Format != "" && Detailitem.Price != "") {
                    DetailArray.push(Detailitem);
                } else if (Detailitem.Name != "" || Detailitem.Unit != "" || Detailitem.Quantity != "" || Detailitem.Format != "" || Detailitem.Price != "") {
                    noEmptyStr += i + ". ";
                }
            }
            if (noEmptyStr.length > 0) {
                alert("第" + noEmptyStr + "筆未填寫完整");
            } else {
                obj.DetailArray = DetailArray;
                obj.applySum = $("#applySum").html();
                var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
                if (checkString.length > 0) {
                    alert(checkString);
                } else {
                    AspAjax.createApplyPropertyDataBase(obj);
                }
            }
            break;
        case 1:
            obj.ID = _ColumnID;
            obj.applySum = $("#applySum").html();
            var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                AspAjax.setApplyPropertyDataBase(obj);
            }
            break;
    }
}

function printPage() {
    if (_nowStatus == "0") {
        var x = confirm("列印後不能再進行任何修改。\n確定列印?");
        if (x) {
            AspAjax.printApplyPropertyDataBase(_ColumnID);
        }
    } else {
        wOpen();
    }
}
function wOpen() {
    var wOpen = window.open('./apply_property_print.aspx?id=' + _ColumnID, "_blank");
    if (wOpen == null) {
        alert("彈出視窗已封鎖，請自行開啟被封鎖的頁面。");
        getViewData(_ColumnID, "2");
    } else if (_nowStatus == "0") {
        window.location.reload();
    }
}

