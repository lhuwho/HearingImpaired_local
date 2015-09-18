var MyBase = new Base();
//var noEmptyItem = [ "propertyID", "propertyCategory", "propertyFitting", "propertyLocation", "propertyCustody", "changeStatus", "fillInDate"];
//var noEmptyShow = [ "財產編號", "財產類別", "是否有配件", "放置地點", "保管人員", "財產增加/減損類型", "填寫日期"];
var noEmptyItem = [  "propertyFitting", "propertyLocation", "propertyCustody", "changeStatus", "fillInDate"];
var noEmptyShow = [  "是否有配件", "放置地點", "保管人員", "財產增加/減損類型", "填寫日期"];
var _propertyStatus = new Array("", "使用中", "報廢", "停用","贈出","轉出");
var _ColumnID = 0;
var _nowDetailID;
var _nowapplyID = 0;
var _rUnit = 0;
var _itemValue;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    //initPage();
    AspAjax.getStaffRolesnoVar();
    setInitData();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

    $(".showUploadImg").hide().fancybox();

    $(".btnUpdate").click(function() {
    $(".btnUpdate").add(".btnOutdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#propertyCode").add("#applyID").add("#ChangesExplainDiv textarea").attr("disabled", true);
    });

    $(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").add(".btnOutdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

    $("#changeAdd label:first").find("input[name='propertyChange']").attr("checked", true);
    $("#propertyStatus").change(function() {
        if ($(this).val() == 1) {
            $("#changeAdd").fadeIn();
            $("#changeSubtract").hide();
            $("#changeAdd label:first").find("input[name='propertyChange']").attr("checked", true);

        } else if ($(this).val() == 5) {
            $("#changeAdd").add("#changeSubtract").hide();
        } else {
            $("#changeAdd").hide();
            $("#changeSubtract").fadeIn();
            $("#changeSubtract label:first").find("input[name='propertyChange']").attr("checked", true);
        }
        $("input[name='changeStatus']").attr("checked", false);
    });

   
});
function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "getUnitAutoNumber":
            $("#propertyCode").val(result);
            break;
        case "searchApplyPropertyDataBaseCount2":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#smainPagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        var CompletedapplyID = $("#CompletedapplyID").val();
                        var obj = new Object();
                        if (CompletedapplyID.length > 0) {
                            obj.txtapplyID = CompletedapplyID;
                        }
                        AspAjax.searchApplyPropertyDataBase2(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchApplyPropertyDataBase2":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].checkNo, 10) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="apply_' + result[i].ID + '">' +
                                '<td height="30" class="applyID">' + result[i].txtapplyID + '</td>' +
                                '<td><span class="applyBy">' + result[i].txtapplyBy + '</span><span class="applyByID" style="display:none;">' + result[i].txtapplyByID + '</span></td>' +
                                '<td>' + TransformADFromStringFunction(result[i].txtapplyDate) + '</td>' +
                                '<td>' + result[i].txtapplySum + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getViewDetail(' + result[i].ID + ')">詳細</button></td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn").children("tbody").html(inner);
                    $("#smainPagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            }
            break;
        case "getPropertyDetailDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result.pID + '" class="ImgJs">' +
                                    '<td height="30" class="DetailName">' + result[i].Name + '</td>' +
			                        '<td class="DetailUnit">' + result[i].Unit + '</td>' +
			                        '<td class="DetailQuantity">' + result[i].Quantity + '</td>' +
			                        '<td class="DetailFormat">' + result[i].Format + '</td>' +
			                        '<td class="DetailPrice">' + result[i].Price + '</td>' +
			                        '<td class="DetailSum">' + (result[i].Quantity * result[i].Price) +
			                        '<span class="DetailBill" style="display:none;">' + result[i].Bill + '</span></td>' +
			                        '</tr>';
                    }
                    $("#DetailTable tbody").empty().html(inner);
                    var captionStr = '申請單流水號：' + _nowapplyID + '　　申請人：<span id="byName">' + $("#apply_" + _nowDetailID).find(".applyBy").html() + '</span>' +
                                    '<span id="byID" style="display:none;">' + $("#apply_" + _nowDetailID).find(".applyByID").html() + '</span>';
                    $("#DetailTable caption").html(captionStr);
                    $("#DetailTable").show();
                    $.fancybox.resize();

                    $("#DetailTable .ImgJs").unbind('click').click(function() {
                        $("#applyID").val(_nowapplyID);
                        $("#propertyName").val($(this).find(".DetailName").html());
                        $("#propertyUnit").val($(this).find(".DetailUnit").html());
                        $("#propertyQuantity").val($(this).find(".DetailQuantity").html());
                        $("#propertyLabel").val($(this).find(".DetailFormat").html());
                        $("#propertyPrice").val($(this).find(".DetailPrice").html());
                        $("#propertyReceipt").val($(this).find(".DetailBill").html());
                        $("#PurchaserName").val($("#byName").html());
                        $("#Purchaser").html($("#byID").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                alert("查無資料");
            }
            break;
        case "getPropertyCategoryData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                var inner = '';
                for (var i = 0; i < result.length; i++) {
                    inner += '<option value="' + result[i][0] + '">' + result[i][1] + '</option>';
                }
                $("select[name='propertyCategory']").append(inner);
            }
            break;
       
        case "getPropertyCustodyData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                var inner = '<option value="0">請選擇</option>';
                $("select[name='propertyCustody']").find("option").remove();
                for (var i = 0; i < result.length; i++) {
                    inner += '<option value="' + result[i][0] + '">' + result[i][1] + '</option>';
                }
                $("select[name='propertyCustody']").html(inner);
            }
            break;
        case "createPropertyRecordDataBase":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                var picSave = false;
                var PicNameArray;
                var obj = MyBase.getTextValueBase("mainContent");
                obj.repairID = parseInt(result[1], 10);
                var options = {
                    type: "POST",
                    url: 'Files2.ashx?type=PropertyAnnex&ID=' + obj.repairID,
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
                        } else {
                            alert(myObject["error"]);
                        }
                    }
                };
                // 將options傳給ajaxForm
                $('#GmyForm').ajaxSubmit(options);

                if (picSave) {
                    AspAjax.setPropertyAnnexDataBase(obj);
                }
            }
            break;
        case "setPropertyAnnexDataBase":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = './property_record.aspx?id=' + result[1] + '&act=2';
            }
            break;
        case "getPropertyRecordDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    //gatLocationAndCustody(result.Unit);
                    _itemValue = result;
                    PushPageValueFunction();
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
        case "createPropertyChangesRecord":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "setPropertyRecordDataBase":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                var picSave = false;
                var PicNameArray;
                var obj = MyBase.getTextValueBase("mainContent");
                obj.repairID = parseInt(result[1], 10);
                var options = {
                    type: "POST",
                    url: 'Files2.ashx?type=PropertyAnnex&ID=' + obj.repairID,
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
                        } else {
                            alert(myObject["error"]);
                        }
                    }
                };
                // 將options傳給ajaxForm
                $('#GmyForm').ajaxSubmit(options);

                if (picSave) {
                    AspAjax.setPropertyAnnexDataBase2(obj);
                }
            }
            break;
        case "setPropertyAnnexDataBase2":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                $(".btnSaveUdapteData").add(".btnCancel").hide();
                $(".btnUpdate").fadeIn();
                $("input").add("select").add("textarea").attr("disabled", true);
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "setPropertyChangesRecord":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "delPropertyChangesRecord":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                $.fancybox.close();
                alert("刪除成功");
                window.location.reload();
            }
            break;
        case "searchPropertyRecordDataBaseCount":
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
                        AspAjax.searchPropertyRecordDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchPropertyRecordDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].checkNo, 10) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        var applyStr = '';

                        if (parseInt(result[i].txtapplyID, 10) != 0) {
                            applyStr = result[i].txtapplyID;
                        }
                        //var _UnitList = new Array("", "總會", "台北至德", "台中至德", "", "高雄至德");
                        inner += '<tr>' +
                                '<td>' + result[i].txtcode + '</td>' +
                                '<td>' + _UnitList[result[i].txtUnit] + '</td>' +
                                '<td>' + result[i].txtpropertyID + '</td>' +
                                '<td>' + result[i].txtpropertyName + '</td>' +
                                '<td>' + result[i].txtlocation + '</td>' +
                                '<td>' + result[i].txtcustody + '</td>' +
                                '<td>' + TransformADFromStringFunction(result[i].txtbuyDate) + '</td>' +
                                '<td>' + result[i].txtpropertyPrice + '</td>' +
                                '<td>' + _propertyStatus[result[i].txtpropertyState] + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        case "OutPropertyRecordDataBase":
            if (parseInt(result[0], 10) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("轉移成功");
                window.location.reload();
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function getView(id) {
    window.open("./property_record.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    AspAjax.getPropertyCategoryData();
    gatLocationAndCustody("00");
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").add("#ChangesExplainDiv").add(".btnOutdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getPropertyRecordDataBase(id);
    } else if (id == null && act == 1) {
        $("#ChangesExplainDiv").hide();
        $(".btnSave").fadeIn();
        $("#fillInDate").val(TodayADDateFunction());
        //setTimeout('$("#sUnit").html(_UnitList[_uUnit]);', 500);
        AspAjax.getUnitAutoNumber("PropertDB_", "");
        gatLocationAndCustody("0");
        $("#applyID").unbind("click").click(function() {
            var inner = '<div id="inline">' +
                        '<p>申請單流水號<input type="text" id="CompletedapplyID" value="" /><button class="searchCompletedapplyID btnSearch" type="button">查 詢</button></p>' +
                        '<table id="StuinlineReturn" class="tableList" border="0" width="400">' +
                            '<thead>' +
		                        '<tr>' +
		                            '<th width="100">申請單流水號</th>' +
		                            '<th width="100">申請人</th>' +
		                            '<th width="80">申請日期</th>' +
		                            '<th width="70">合計</th>' +
		                            '<th width="50">功能</th>' +
		                        '</tr>' +
		                    '</thead>' +
		                    '<tbody>' +
		                    '</tbody>' +
                        '</table>' +
                        '<div id="smainPagination" class="pagination"></div>' +
                        '</div>' +
                        '<br /><table id="DetailTable" class="tableList" border="0" width="400" style="display:none;">' +
                            '<caption></caption>' +
                            '<thead>' +
		                        '<tr>' +
		                            '<th width="150">品名</th>' +
			                        '<th width="50">單位</th>' +
			                        '<th width="50">數量</th>' +
			                        '<th width="50">規格</th>' +
			                        '<th width="50">估計<br />單價</th>' +
			                        '<th width="50">總價</th>' +
		                        '</tr>' +
		                    '</thead>' +
		                    '<tbody>' +
		                    '</tbody>' +
                        '</table>';
            $.fancybox({
                'content': inner,
                'autoDimensions': true,
                'centerOnScroll': true,
                'onComplete': function() {

                    $(".searchCompletedapplyID").click(function() {
                        var CompletedapplyID = $("#CompletedapplyID").val();
                        var obj = new Object();
                        if (CompletedapplyID.length > 0) {
                            obj.txtapplyID = CompletedapplyID;
                        }
                        AspAjax.searchApplyPropertyDataBaseCount2(obj);
                    });
                }
            });
        });
        $("#propertyStatus [value=1]:selected").change();
    }
}

function search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.searchPropertyRecordDataBaseCount(obj);
}

function getViewDetail(DetailID) {
    _nowDetailID = DetailID;
    _nowapplyID = $("#apply_" + DetailID + " .applyID").html();
    AspAjax.getPropertyDetailDataBase(DetailID);
}

function saveData(Type) {
    var obj = MyBase.getTextValueBase("mainContent");
    switch (Type) {
        case 0:
            var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                obj.Purchaser = $("#Purchaser").html();
                AspAjax.createPropertyRecordDataBase(obj);
            }
            break;
        case 1:
            obj.repairID = _ColumnID;
            AspAjax.setPropertyRecordDataBase(obj);
            break;
    }
}

function InsertData() {
    $("#insertDataDiv").show();
    $("#moveDate").val(TodayADDateFunction());
    $("#relatedBy").val(_uName);
    $("#relatedByID").html(_uId);
    $("#moveDate").add("#relatedBy").add("#moveAbstract").attr("disabled", false);
}
function cancelInsert() {
    $("#insertDataDiv").hide();
}
function InsertTrackData() {
    var obj = MyBase.getTextValueBase("insertDataDiv");
    var obj1 = getHideSpanValue("insertDataDiv", "hideClassSpan");
    MergerObject(obj, obj1);
    obj.recordID = _ColumnID;

    if (obj.recordID != null && obj.recordID != 0) {
        AspAjax.createPropertyChangesRecord(obj);
    } else {
        alert("發生錯誤，請刷新頁面後再嘗試");
    }
}

function UpData(TrID) {
    $("#HS_" + TrID + " textarea").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .moveAbstract").val(_ReturnValue[vIndex].moveAbstract);
    $("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delPropertyChangesRecord(TrID);
    });
}
function SaveData(TrID) {
    var obj = new Object();
    obj.cID = parseInt(TrID);
    var Abstract = $("#HS_" + TrID + " .moveAbstract").val();
    if (Abstract.length > 0) {
        obj.moveAbstract = Abstract;
    }
    if (obj.moveAbstract != null) {
        $("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setPropertyChangesRecord(obj);
    } else {
        alert("請填寫完整");
    }

}
function outData() {

    var inner = '<div id="inline">';
    for (var item in _UnitList) {
        if ((_UnitList[item]).length > 0) {
            var lockitem = "";
            if (item == _rUnit) {
                lockitem = "disabled='disabled'";
            }
            inner += '<label><input type="radio" name="outStatus" value="' + item + '" autocomplete="off" ' + lockitem + '/> ' + _UnitList[item] + '</label>　';
        }
    }
    inner += '<p align="center"><input type="button" value="移出確認" /></p></div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $("#inline [type=button]").unbind('click').click(function() {
                var unit = $("input[name=outStatus]:checked").val();
                if (unit != undefined) {
                    AspAjax.OutPropertyRecordDataBase( _ColumnID, unit);
                }
            });
        }
    });
}


function PushPageValueFunction() {
    var result = _itemValue;
    PushPageValue(result);
    if (result.applyID == "0") {
        $("#applyID").val("");
    }
   
    _rUnit = result.Unit;
    var path = "./uploads/property/" + result.repairID + "/print/";
    var path2 = "./uploads/property/" + result.repairID + "/org/";
    if (result.attachment1.length > 0) {
        $("#attachment1").attr("src", path + result.attachment1);
        $("#attachment1Url").attr("href", path2 + result.attachment1).show();
    }
    if (result.attachment2.length > 0) {
        $("#attachment2").attr("src", path + result.attachment2);
        $("#attachment2Url").attr("href", path2 + result.attachment2).show();
    }
    if (result.attachment3.length > 0) {
        $("#attachment3").attr("src", path + result.attachment3);
        $("#attachment3Url").attr("href", path2 + result.attachment3).show();
    }
    if (result.attachment4.length > 0) {
        $("#attachment4").attr("src", path + result.attachment4);
        $("#attachment4Url").attr("href", path2 + result.attachment4).show();
    }
    if (result.attachment5.length > 0) {
        $("#attachment5").attr("src", path + result.attachment5);
        $("#attachment5Url").attr("href", path2 + result.attachment5).show();
    }
    if (result.attachment6.length > 0) {
        $("#attachment6").attr("src", path + result.attachment6);
        $("#attachment6Url").attr("href", path2 + result.attachment6).show();
    }
    if (result.attachment7.length > 0) {
        $("#attachment7").attr("src", path + result.attachment7);
        $("#attachment7Url").attr("href", path2 + result.attachment7).show();
    }
    if (result.attachment8.length > 0) {
        $("#attachment8").attr("src", path + result.attachment8);
        $("#attachment8Url").attr("href", path2 + result.attachment8).show();
    }
    //_nowStatus = result.applyStatus;
    var inner = "";
    _ReturnValue = result.ChangesArray;
    for (var i = 0; i < result.ChangesArray.length; i++) {
        var ChangesData = result.ChangesArray[i];
        inner += '<tr id="HS_' + ChangesData.cID + '">' +
			                        '<td height="40">' + TransformADFromStringFunction(ChangesData.moveDate) + '</td>' +
			                        '<td><textarea class="moveAbstract" cols="50" rows="1" autocomplete="off">' + ChangesData.moveAbstract + '</textarea></td>' +
			                        '<td>' + ChangesData.relatedBy + '</td>' +
			                        '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + ChangesData.cID + ')">更 新</button> ' +
			                        '<button class="btnView" type="button" onclick="DelData(' + ChangesData.cID + ')">刪 除</button></div>' +
			                        '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(' + ChangesData.cID + ')">儲 存</button> ' +
			                        '<button class="btnView" type="button" onclick="cancelUpData(' + ChangesData.cID + ',' + i + ')">取 消</button></div></td>' +
                                    '</tr>';
    }
    $("#ChangesExplainDiv .tableContact tbody").empty().html(inner);
    $("#ChangesExplainDiv .tableContact tbody textarea").attr("disabled", true);
}

function gatLocationAndCustody(item) {
   // $("select[name='propertyLocation']").find('option').remove();
    //$("select[name='propertyCustody']").find('option').remove();
    //AspAjax.getPropertyLocationData(item);
    //AspAjax.getPropertyCustodyData(item);
}