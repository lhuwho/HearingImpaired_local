var MyBase = new Base();
var noEmptyItem = ["stationeryName", "stationeryUnit", "safeQuantity", "stationeryType"];
var noEmptyShow = ["品名", "最小單位", "安全數量", "類別"];
var noEmptyItem2 = ["firmName", "firmTel", "purchaseDate", "stationeryID", "stationeryQuantity", "stationeryPrice"];
var noEmptyShow2 = ["廠商名稱", "廠商電話", "進貨日期", "產品編號", "數量", "單價"];
var noEmptyItem3 = ["receiveDate", "receiveBy", "rstationeryID", "receiveQuantity"];
var noEmptyShow3 = ["領用日期", "領用人", "產品編號", "數量"];
var noEmptyItem4 = ["scrappedDate", "scrappedBy", "sstationeryID", "scrappedQuantity"];
var noEmptyShow4 = ["報廢日期", "經辦人", "產品編號", "數量"];
var noEmptyItem5 = ["returnedDate", "getgoodsBy", "restationeryID", "returnedQuantity"];
var noEmptyShow5 = ["退貨日期", "領用對象", "產品編號", "數量"];

var _ReturnValue;
var _TypeList = new Array("", "消耗品", "非消耗品", "管制品");

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    
    $("#mainContentIndex").add("#mainContentIndex2").add("#mainContentIndex3").add("#mainContentIndex4").hide();
   
    $(".menuTabs2").click(function() {
        $(".menuTabs2").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").add("#mainContentIndex").add("#mainContentIndex2").add("#mainContentIndex3").add("#mainContentIndex4").add(".mainSearchList").add(".insertDataDiv").add(".pagination").hide();
        var DivName = '';
        switch (this.id) {
            case "btnSearch":
                DivName = "#mainContentSearch";
                break;
            case "btnIndex":
                DivName = "#mainContentIndex";
                break;
            case "btnIndex2":
                DivName = "#mainContentIndex2";
                break;
            case "btnIndex3":
                DivName = "#mainContentIndex3";
                break;
            case "btnIndex4":
                DivName = "#mainContentIndex4";
                break;
        }
        $(DivName).add(".btnAdd").fadeIn();
        $(DivName + " input[type='text']").val("");
        $(DivName + " select").val(0);
    });
    
});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "createstationeryDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "SearchStationeryDataCount":
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
                        AspAjax.SearchStationeryData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStationeryData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result[i].sID + '">' +
			                    '<td>' + result[i].stationeryID + '</td>' +
			                    '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td><input class="SstationeryName" type="text" value="' + result[i].stationeryName + '" size="15"/></td>' +
			                    '<td><input class="SstationeryUnit" type="text" value="' + result[i].stationeryUnit + '" size="5" /></td>' +
			                    '<td>' + result[i].inventory + '</td>' +
			                    '<td><input class="SsafeQuantity" type="text" value="' + result[i].safeQuantity + '" size="5" /></td>' +
			                    '<td>' + result[i].recentPrice + '</td>' +
			                    '<td>' + _TypeList[result[i].stationeryType] + '</td>' +
			                    '<td><textarea rows="1" cols="25" class="SstationeryRemark">' + result[i].remark + '</textarea></td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].sID + ')">更 新</button>' +
			                    '<br /><button class="btnView" type="button" onclick="DelData(' + result[i].sID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(' + result[i].sID + ')">儲 存</button>' +
			                    '<br /><button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].sID + '\',\'' + i + '\')">取 消</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList input[type=text]").add("#mainSearchList select").add("#mainSearchList textarea").attr("disabled", true);
                    $("#mainSearchList").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        case "setStationeryData1":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
                showView(1);
            }
            break;
        case "delStationeryData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                $.fancybox.close();
                showView(1);
            }
            break;
        case "SearchStationeryResultCount":
            var obj = MyBase.getTextValueBase("searchStuinline");
            var pageCount = parseInt(result);
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
                        AspAjax.SearchStationeryResult(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='9'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStationeryResult":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].txtstudentID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs">' +
                                '<td>' + result[i].stationeryID + '</td>' +
                                '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].stationeryName + '</td>' +
			                    '<td>' + result[i].stationeryUnit + '</td>' +
			                    '<td>' + _TypeList[result[i].stationeryType] + '</td>' +
			                    '<td>' + result[i].inventory + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var id = $(this).find("span").html();
                        $("#stationeryID").val($(this).find("td:nth-child(1)").html());
                        $("#stationeryName2").html($(this).find("td:nth-child(3)").html());
                        $("#stationeryUnit2").html($(this).find("td:nth-child(4)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].txtstudentName);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        /*case "getStationeryDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.StudentData.studentID != -1) {
                    $("#studentName").val(result.StudentData.studentName).attr("class", result.StudentData.studentID);
                    $("#studentStatu").html(_StudentStatu[result.StudentStatu]).show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].txtstudentName);
                }
            } else {
                alert("查無資料");
            }
            break;*/
        case "createPurchaseDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                //window.location.reload();
                $(".menuTabs2:nth-child(2)").click();
            }
            break;
        case "SearchPurchaseDataCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPaginationIndex").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTable2");
                        AspAjax.SearchPurchaseData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='11'>查無資料</td></tr>");
            } else {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='11'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchPurchaseData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result[i].pID + '">' +
			                    '<td>' + result[i].purchaseID + '</td>' +
			                    '<td><input class="firmName" type="text" value="' + result[i].firmName + '" size="12"/></td>' +
			                    '<td><input class="firmTel" type="text" value="' + result[i].firmTel + '" size="10" /></td>' +
			                    '<td><input class="purchaseDate" type="text" value="' + TransformADFromStringFunction(result[i].purchaseDate) + '" size="9" /></td>' +
			                    '<td>' + result[i].stationeryID + '</td>' +
			                    '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].stationeryName + '</td>' +
			                    '<td>' + result[i].stationeryUnit + '</td>' +
			                    '<td><input class="stationeryQuantity" type="text" value="' + result[i].stationeryQuantity + '" size="3" /></td>' +
			                    '<td><input class="stationeryPrice" type="text" value="' + result[i].stationeryPrice + '" size="5" /></td>' +
			                    '<td>' + (parseInt(result[i].stationeryQuantity, 10) * parseFloat(result[i].stationeryPrice, 10)) + '</td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].pID + ')">更 新</button>' +
			                    '<br /><button class="btnView" type="button" onclick="DelData2(' + result[i].pID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData2(' + result[i].pID + ')">儲 存</button>' +
			                    '<br /><button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].pID + '\',\'' + i + '\')">取 消</button></td>' +
			                '</tr>';
                    }
                    $("#mainIndexList .tableList").children("tbody").html(inner);
                    $("#mainIndexList .purchaseDate").datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("#mainIndexList input[type=text]").attr("disabled", true);
                    $("#mainIndexList").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='11'>查無資料</td></tr>");
            }
            break;
        case "setPurchaseData1":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
                showView(2);
            }
            break;
        case "delPurchaseData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                $.fancybox.close();
                showView(2);
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
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("s_", "");
                        $("#receiveByID").html(id);
                        var Name = $(this).children("td:nth-child(4)").html();
                        $("#receiveBy").val(Name);

                        $.fancybox.close();
                    });

                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "SearchStationeryResultCount2":
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
                        AspAjax.SearchStationeryResult2(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            } else {
                $("#StuinlineReturn").children("tbody").html("發生錯誤");
            }
            break;
        case "SearchStationeryResult2":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs">' +
                                '<td>' + result[i].stationeryID + '</td>' +
                                '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].stationeryName + '</td>' +
			                    '<td>' + result[i].stationeryUnit + '</td>' +
			                    '<td>' + _TypeList[result[i].stationeryType] + '</td>' +
			                    '<td>' + result[i].inventory + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var id = $(this).find("span").html();
                        $("#rstationeryID").val($(this).find("td:nth-child(1)").html());
                        $("#rstationeryName").html($(this).find("td:nth-child(3)").html());
                        $("#rstationeryUnit").html($(this).find("td:nth-child(4)").html());
                        $("#rStorage").html($(this).find("td:nth-child(6)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            }
            break;
        case "createReceiveDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "SearchReceiveDataCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPaginationIndex2").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTable3");
                        AspAjax.SearchReceiveData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList2 .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            } else {
                $("#mainIndexList2 .tableList").children("tbody").html("<tr><td colspan='9'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchReceiveData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result[i].rID + '">' +
			                    '<td>' + result[i].receiveID + '</td>' +
			                    '<td><input class="receiveDate" type="text" value="' + TransformADFromStringFunction(result[i].receiveDate) + '" size="10" /></td>' +
			                    '<td>' + result[i].receiveByName + '</td>' +
			                    '<td>' + result[i].rstationeryID + '</td>' +
			                    '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].rstationeryName + '</td>' +
			                    '<td>' + result[i].rstationeryUnit + '</td>' +
			                    '<td><input class="receiveQuantity" type="text" value="' + result[i].receiveQuantity + '" size="5" /></td>' +
			                    '<td><textarea rows="1" cols="20" class="receiveRemark">' + result[i].receiveRemark + '</textarea></td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].rID + ')">更 新</button>' +
			                    '<br /><button class="btnView" type="button" onclick="DelData3(' + result[i].rID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData3(' + result[i].rID + ')">儲 存</button>' +
			                    '<br /><button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].rID + '\',\'' + i + '\')">取 消</button></td>' +
			                '</tr>';
                    }
                    $("#mainIndexList2 .tableList").children("tbody").html(inner);
                    $("#mainIndexList2 .receiveDate").datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("#mainIndexList2 input[type=text]").add("#mainIndexList2 textarea").attr("disabled", true);
                    $("#mainIndexList2").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList2 .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        case "setReceiveData1":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
                showView(3);
            }
            break;
        case "delReceiveData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                $.fancybox.close();
                showView(3);
            }
            break;
        case "SearchStaffDataBaseCount2":
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
                        AspAjax.SearchStaffDataBase2(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBase2":
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
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("s_", "");
                        $("#scrappedByID").html(id);
                        var Name = $(this).children("td:nth-child(4)").html();
                        $("#scrappedBy").val(Name);

                        $.fancybox.close();
                    });

                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "SearchStationeryResultCount3":
            var obj = MyBase.getTextValueBase("searchStuinline");
            var pageCount = parseInt(result);
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
                        AspAjax.SearchStationeryResult3(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            } else {
                $("#StuinlineReturn").children("tbody").html("發生錯誤");
            }
            break;
        case "SearchStationeryResult3":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs">' +
                                '<td>' + result[i].stationeryID + '</td>' +
                                '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].stationeryName + '</td>' +
			                    '<td>' + result[i].stationeryUnit + '</td>' +
			                    '<td>' + _TypeList[result[i].stationeryType] + '</td>' +
			                    '<td>' + result[i].inventory + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var id = $(this).find("span").html();
                        $("#sstationeryID").val($(this).find("td:nth-child(1)").html());
                        $("#sstationeryName").html($(this).find("td:nth-child(3)").html());
                        $("#sstationeryUnit").html($(this).find("td:nth-child(4)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            }
            break;
        case "createScrapDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "SearchScrapDataCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPaginationIndex3").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTable4");
                        AspAjax.SearchScrapData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList3 .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            } else {
                $("#mainIndexList3 .tableList").children("tbody").html("<tr><td colspan='9'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchScrapData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result[i].sID + '">' +
			                    '<td>' + result[i].scrappedID + '</td>' +
			                    '<td><input class="scrappedDate" type="text" value="' + TransformADFromStringFunction(result[i].scrappedDate) + '" size="10" /></td>' +
			                    '<td>' + result[i].scrappedByName + '</td>' +
			                    '<td>' + result[i].sstationeryID + '</td>' +
			                    '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].sstationeryName + '</td>' +
			                    '<td>' + result[i].sstationeryUnit + '</td>' +
			                    '<td><input class="scrappedQuantity" type="text" value="' + result[i].scrappedQuantity + '" size="5" /></td>' +
			                    '<td><textarea rows="1" cols="20" class="scrappedRemark">' + result[i].scrappedRemark + '</textarea></td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].sID + ')">更 新</button>' +
			                    '<br /><button class="btnView" type="button" onclick="DelData4(' + result[i].sID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData4(' + result[i].sID + ')">儲 存</button>' +
			                    '<br /><button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].sID + '\',\'' + i + '\')">取 消</button></td>' +
			                '</tr>';
                    }
                    $("#mainIndexList3 .tableList").children("tbody").html(inner);
                    $("#mainIndexList3 .scrappedDate").datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("#mainIndexList3 input[type=text]").add("#mainIndexList3 textarea").attr("disabled", true);
                    $("#mainIndexList3").next(".pagination").show();
                } else {
                alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList3 .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        case "setScrapData1":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
                showView(4);
            }
            break;
        case "delScrapData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                $.fancybox.close();
                showView(4);
            }
            break;
        case "createReturnDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "SearchStaffDataBaseCount3":
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
                        AspAjax.SearchStaffDataBase3(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBase3":
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
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("s_", "");
                        $("#getgoodsByID").html(id);
                        var Name = $(this).children("td:nth-child(4)").html();
                        $("#getgoodsBy").val(Name);

                        $.fancybox.close();
                    });

                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "SearchStationeryResultCount4":
            var obj = MyBase.getTextValueBase("searchStuinline");
            var pageCount = parseInt(result);
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
                        AspAjax.SearchStationeryResult4(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            } else {
                $("#StuinlineReturn").children("tbody").html("發生錯誤");
            }
            break;
        case "SearchStationeryResult4":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs">' +
                                '<td>' + result[i].stationeryID + '</td>' +
                                '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].stationeryName + '</td>' +
			                    '<td>' + result[i].stationeryUnit + '</td>' +
			                    '<td>' + _TypeList[result[i].stationeryType] + '</td>' +
			                    '<td>' + result[i].inventory + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var id = $(this).find("span").html();
                        $("#restationeryID").val($(this).find("td:nth-child(1)").html());
                        $("#restationeryName").html($(this).find("td:nth-child(3)").html());
                        $("#restationeryUnit").html($(this).find("td:nth-child(4)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            }
            break;
        case "SearchReturnDataCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPaginationIndex4").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTable5");
                        AspAjax.SearchReturnData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList4 .tableList").children("tbody").html("<tr><td colspan='10'>查無資料</td></tr>");
            } else {
                $("#mainIndexList4 .tableList").children("tbody").html("<tr><td colspan='10'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchReturnData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result[i].rID + '">' +
			                    '<td>' + result[i].returnedID + '</td>' +
			                    '<td><input class="returnedDate" type="text" value="' + TransformADFromStringFunction(result[i].returnedDate) + '" size="10" /></td>' +
			                    '<td><input class="getgoodsDate" type="text" value="' + TransformADFromStringFunction(result[i].getgoodsDate) + '" size="10" /></td>' +
			                    '<td>' + result[i].getgoodsByName + '</td>' +
			                    '<td>' + result[i].restationeryID + '</td>' +
			                    '<td><span class="UnitName">' + _UnitList[result[i].Unit] + '</span></td>' +
			                    '<td>' + result[i].restationeryName + '</td>' +
			                    '<td>' + result[i].restationeryUnit + '</td>' +
			                    '<td><input class="returnedQuantity" type="text" value="' + result[i].returnedQuantity + '" size="5" /></td>' +
			                    '<td><textarea rows="1" cols="20" class="returnedReason">' + result[i].returnedReason + '</textarea></td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].rID + ')">更 新</button>' +
			                    '<br /><button class="btnView" type="button" onclick="DelData5(' + result[i].rID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData5(' + result[i].rID + ')">儲 存</button>' +
			                    '<br /><button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].rID + '\',\'' + i + '\')">取 消</button></td>' +
			                '</tr>';
                    }
                    $("#mainIndexList4 .tableList").children("tbody").html(inner);
                    $("#mainIndexList4 .returnedDate").add("#mainIndexList4 .getgoodsDate").datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("#mainIndexList4 input[type=text]").add("#mainIndexList4 textarea").attr("disabled", true);
                    $("#mainIndexList4").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList4 .tableList").children("tbody").html("<tr><td colspan='10'>查無資料</td></tr>");
            }
            break;
        case "setReturnData1":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
                showView(5);
            }
            break;
        case "delReturnData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                $.fancybox.close();
                showView(5);
            }
            break;
    }
}

function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}

function SaveData(TrID) {
    var obj = new Object();
    obj.sID = parseInt(TrID);
    var SstationeryName = $("#HS_" + TrID + " .SstationeryName").val();
    if (SstationeryName.length > 0) {
        obj.executionName = SstationeryName;
    }
    var SstationeryUnit = $("#HS_" + TrID + " .SstationeryUnit").val();
    if (SstationeryUnit.length > 0) {
        obj.executionUnit = SstationeryUnit;
    }
    var SsafeQuantity = $("#HS_" + TrID + " .SsafeQuantity").val();
    if (SsafeQuantity.length > 0) {
        obj.executionQuantity = SsafeQuantity;
    }
    var SstationeryRemark = $("#HS_" + TrID + " .SstationeryRemark").val();
    if (SstationeryRemark.length > 0) {
        obj.executionRemark = SstationeryRemark;
    }

    if (obj.executionName != null && obj.executionUnit != null && obj.executionQuantity != null ) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setStationeryData1(obj);
    } else {
        alert("請填寫完整");
    }
}

function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delStationeryData(parseInt(TrID));
    });
}

function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .SstationeryName").val(_ReturnValue[vIndex].stationeryName);
    $("#HS_" + TrID + " .SstationeryUnit").val(_ReturnValue[vIndex].stationeryUnit);
    $("#HS_" + TrID + " .SsafeQuantity").val(_ReturnValue[vIndex].safeQuantity);
    $("#HS_" + TrID + " .SstationeryRemark").val(_ReturnValue[vIndex].remark);

    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}

function saveInsert() {
    var obj = MyBase.getTextValueBase("insertDataDiv");
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createstationeryDataBase(obj);
    }
}
function cancelInsert() {
    $(".insertDataDiv").hide();
    $(".btnAdd").fadeIn();
}

function saveInsert2() {
    var obj = MyBase.getTextValueBase("insertDataDiv2");
    var checkString = MyBase.noEmptyCheck(noEmptyItem2, obj, null, noEmptyShow2);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createPurchaseDataBase(obj);
    }
}

function SaveData2(TrID) {
    var obj = new Object();
    obj.pID = parseInt(TrID);
    var FirmName = $("#HS_" + TrID + " .firmName").val();
    if (FirmName.length > 0) {
        obj.executionFirmName = FirmName;
    }
    var FirmTel = $("#HS_" + TrID + " .firmTel").val();
    if (FirmTel.length > 0) {
        obj.executionFirmTel = FirmTel;
    }
    var PurchaseDate = TransformRepublicReturnValue($("#HS_" + TrID + " .purchaseDate").val());
    if (PurchaseDate.length > 0) {
        obj.executionPurchaseDate = PurchaseDate;
    }
    var StationeryQuantity = $("#HS_" + TrID + " .stationeryQuantity").val();
    if (StationeryQuantity.length > 0) {
        obj.executionQuantity = StationeryQuantity;
    }
    var StationeryPrice = $("#HS_" + TrID + " .stationeryPrice").val();
    if (StationeryPrice.length > 0) {
        obj.executionPrice = StationeryPrice;
    }

    if (obj.executionFirmName != null && obj.executionFirmTel != null && obj.executionPurchaseDate != null && obj.executionQuantity != null && obj.executionPrice != null) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setPurchaseData1(obj);
    } else {
        alert("請填寫完整");
    }
}

function DelData2(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delPurchaseData(parseInt(TrID));
    });
}

function saveInsert3() {
    var obj = MyBase.getTextValueBase("insertDataDiv3");
    var obj1 = getHideSpanValue("insertDataDiv3", "hideClassSpan");
    MergerObject(obj, obj1);
    var checkString = MyBase.noEmptyCheck(noEmptyItem3, obj, null, noEmptyShow3);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createReceiveDataBase(obj);
    }
}

function SaveData3(TrID) {
    var obj = new Object();
    obj.rID = parseInt(TrID);
    var ReceiveDate = TransformRepublicReturnValue($("#HS_" + TrID + " .receiveDate").val());
    if (ReceiveDate.length > 0) {
        obj.executionreceiveDate = ReceiveDate;
    }
    var ReceiveQuantity = $("#HS_" + TrID + " .receiveQuantity").val();
    if (ReceiveQuantity.length > 0) {
        obj.executionQuantity = ReceiveQuantity;
    }
    var ReceiveRemark = $("#HS_" + TrID + " .receiveRemark").val();
    if (ReceiveRemark.length > 0) {
        obj.executionRemark = ReceiveRemark;
    }

    if (obj.executionreceiveDate != null && obj.executionQuantity != null && obj.executionRemark != null) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setReceiveData1(obj);
    } else {
        alert("請填寫完整");
    }
}

function DelData3(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delReceiveData(parseInt(TrID));
    });
}

function saveInsert4() {
    var obj = MyBase.getTextValueBase("insertDataDiv4");
    var obj1 = getHideSpanValue("insertDataDiv4", "hideClassSpan");
    MergerObject(obj, obj1);
    var checkString = MyBase.noEmptyCheck(noEmptyItem4, obj, null, noEmptyShow4);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createScrapDataBase(obj);
    }
}

function SaveData4(TrID) {
    var obj = new Object();
    obj.sID = parseInt(TrID);
    var ReceiveDate = TransformRepublicReturnValue($("#HS_" + TrID + " .scrappedDate").val());
    if (ReceiveDate.length > 0) {
        obj.executionscrapDate = ReceiveDate;
    }
    var ReceiveQuantity = $("#HS_" + TrID + " .scrappedQuantity").val();
    if (ReceiveQuantity.length > 0) {
        obj.executionQuantity = ReceiveQuantity;
    }
    var ReceiveRemark = $("#HS_" + TrID + " .scrappedRemark").val();
    if (ReceiveRemark.length > 0) {
        obj.executionRemark = ReceiveRemark;
    }

    if (obj.executionscrapDate != null && obj.executionQuantity != null && obj.executionRemark != null) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setScrapData1(obj);
    } else {
        alert("請填寫完整");
    }
}

function DelData4(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delScrapData(parseInt(TrID));
    });
}

function saveInsert5() {
    var obj = MyBase.getTextValueBase("insertDataDiv5");
    var obj1 = getHideSpanValue("insertDataDiv5", "hideClassSpan");
    MergerObject(obj, obj1);
    var checkString = MyBase.noEmptyCheck(noEmptyItem5, obj, null, noEmptyShow5);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createReturnDataBase(obj);
    }
}

function SaveData5(TrID) {
    var obj = new Object();
    obj.rID = parseInt(TrID);
    var ReturnedDate = TransformRepublicReturnValue($("#HS_" + TrID + " .returnedDate").val());
    if (ReturnedDate.length > 0) {
        obj.executionreturnedDate = ReturnedDate;
    }
    var getGoodsDate = TransformRepublicReturnValue($("#HS_" + TrID + " .getgoodsDate").val());
    if (getGoodsDate.length > 0) {
        obj.executiongetgoodsDate = getGoodsDate;
    }
    var ReturnedQuantity = $("#HS_" + TrID + " .returnedQuantity").val();
    if (ReturnedQuantity.length > 0) {
        obj.executionQuantity = ReturnedQuantity;
    }
    var ReturnedReason = $("#HS_" + TrID + " .returnedReason").val();
    if (ReturnedReason.length > 0) {
        obj.executionReason = ReturnedReason;
    }

    if (obj.executionreturnedDate != null && obj.executiongetgoodsDate != null && obj.executionQuantity != null && obj.executionReason != null) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setReturnData1(obj);
    } else {
        alert("請填寫完整");
    }
}

function DelData5(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delReturnData(parseInt(TrID));
    });
}


function showView(viewID) {
    var DivName = "";
    switch (viewID) {
        case 1:
            DivName = "#mainSearchList";
            var obj = MyBase.getTextValueBase("searchTable");
            AspAjax.SearchStationeryDataCount(obj);
            break;
        case 2:
            DivName = "#mainIndexList";
            var obj = MyBase.getTextValueBase("searchTable2");
            if ((obj.txtpurchaseDateStart != null && obj.txtpurchaseDateEnd != null) || (obj.txtpurchaseDateStart == null && obj.txtpurchaseDateEnd == null)) {
                AspAjax.SearchPurchaseDataCount(obj);
            } else {
                alert("請填寫完整日期區間");
            }
            break;
        case 3:
            DivName = "#mainIndexList2";
            var obj = MyBase.getTextValueBase("searchTable3");
            if ((obj.txtreceiveDateStart != null && obj.txtreceiveDateEnd != null) || (obj.txtreceiveDateStart == null && obj.txtreceiveDateEnd == null)) {
                AspAjax.SearchReceiveDataCount(obj);
            } else {
                alert("請填寫完整日期區間");
            }
            break;
        case 4:
            DivName = "#mainIndexList3";
            var obj = MyBase.getTextValueBase("searchTable4");
            if ((obj.txtscrappedDateStart != null && obj.txtscrappedDateEnd != null) || (obj.txtscrappedDateStart == null && obj.txtscrappedDateEnd == null)) {
                AspAjax.SearchScrapDataCount(obj);
            } else {
                alert("請填寫完整日期區間");
            }
            break;
        case 5:
            DivName = "#mainIndexList4";
            var obj = MyBase.getTextValueBase("searchTable5");
            if ((obj.txtreturnedDateStart != null && obj.txtreturnedDateEnd != null) || (obj.txtreturnedDateStart == null && obj.txtreturnedDateEnd == null)) {
                AspAjax.SearchReturnDataCount(obj);
            } else {
                alert("請填寫完整日期區間");
            }
            break;
    }
    $(DivName).fadeIn();
    $(DivName + " input[type=text]").add(DivName + " select").add(DivName + " textarea").attr("disabled", true);
}

function showInsert(insertID) {
    var DivName = '';
    switch (insertID) {
        case 1:
            DivName = "#insertDataDiv";
            break;
        case 2:
            DivName = "#insertDataDiv2";
            $("#stationeryID").unbind("click").click(function() {
                var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                    '<tr><td width="80">品名 <input type="text" value="" id="txtstationeryName" /></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn" class="tableList" border="0" width="400">' +
                        '<thead>' +
		                    '<tr>' +
		                        '<th width="80">產品編號</th>' +
		                        '<th width="40">部別名稱</th>' +
		                        '<th width="100">品名</th>' +
		                        '<th width="65">最小單位</th>' +
		                        '<th width="80">類別</th>' +
		                        '<th width="65">庫存量</th>' +
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
                            AspAjax.SearchStationeryResultCount(obj);
                        });
                    }
                });
            });
            break;
        case 3:
            DivName = "#insertDataDiv3";
            $("#receiveBy").unbind("click").click(function() {
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
            $("#rstationeryID").unbind("click").click(function() {
                var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                    '<tr><td width="80">品名 <input type="text" value="" id="txtstationeryName" /></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn" class="tableList" border="0" width="400">' +
                        '<thead>' +
		                    '<tr>' +
		                    	'<th width="80">產品編號</th>' +
		                        '<th width="40">部別名稱</th>' +
		                        '<th width="100">品名</th>' +
		                        '<th width="65">最小單位</th>' +
		                        '<th width="80">類別</th>' +
		                        '<th width="65">庫存量</th>' +
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
                            AspAjax.SearchStationeryResultCount2(obj);
                        });
                    }
                });
            });
            break;
        case 4:
            DivName = "#insertDataDiv4";
            $("#scrappedBy").unbind("click").click(function() {
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
                            AspAjax.SearchStaffDataBaseCount2(obj);
                        });
                    }
                });
            });
            $("#sstationeryID").unbind("click").click(function() {
                var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                    '<tr><td width="80">品名 <input type="text" value="" id="txtstationeryName" /></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn" class="tableList" border="0" width="400">' +
                        '<thead>' +
		                    '<tr>' +
		                        '<th width="80">產品編號</th>' +
		                        '<th width="40">部別名稱</th>' +
		                        '<th width="100">品名</th>' +
		                        '<th width="65">最小單位</th>' +
		                        '<th width="80">類別</th>' +
		                        '<th width="65">庫存量</th>' +
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
                            AspAjax.SearchStationeryResultCount3(obj);
                        });
                    }
                });
            });
            break;
        case 5:
            DivName = "#insertDataDiv5";
            $("#getgoodsBy").unbind("click").click(function() {
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
                            AspAjax.SearchStaffDataBaseCount3(obj);
                        });
                    }
                });
            });
            $("#restationeryID").unbind("click").click(function() {
                var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                    '<tr><td width="80">品名 <input type="text" value="" id="txtstationeryName" /></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn" class="tableList" border="0" width="400">' +
                        '<thead>' +
		                    '<tr>' +
		                    	'<th width="80">產品編號</th>' +
		                        '<th width="40">部別名稱</th>' +
		                        '<th width="100">品名</th>' +
		                        '<th width="65">最小單位</th>' +
		                        '<th width="80">類別</th>' +
		                        '<th width="65">庫存量</th>' +
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
                            AspAjax.SearchStationeryResultCount4(obj);
                        });
                    }
                });
            });
            break;
    }
    $(DivName).fadeIn();
    $(DivName + " input[type='text']").add(DivName + " textarea").val("");
    $(DivName + " select").val(0);
    $(".btnAdd").fadeOut();
}

