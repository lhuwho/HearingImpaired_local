var MyBase = new Base();
var noEmptyItem = ["bookTitle", "bookClassification"];
var noEmptyShow = ["書名", "分類"];
var noEmptyItem2 = ["txtbookDayType", "txtbookStartDay", "txtbookEndDay"];
var noEmptyShow2 = ["借閱類別", "借出天數(起始)", "借出天數(終止)"];
var noEmptyItem3 = ["txtbookDateStartDate", "txtbookDateEndDate"];
var noEmptyShow3 = ["統計期間(起始)", "統計期間(終止)"];
var noEmptyItem4 = ["txtrecordBorrowerType"];
var noEmptyShow4 = ["借閱類別"];
var _ReturnValue;
var _bookStatus = new Array("", "在架", "借出");
var _borrowerStatus = new Array("", "員工", "學生");
//var _bookScrapstatus = new Array("", "轉出", "報廢");
var _bookScrapstatus = new Array('<option value="0">請選擇</option><option value="1">轉出</option><option value="2">報廢</option>', '<option value="0">請選擇</option><option value="1"  selected="selected" >轉出</option><option value="2">報廢</option>', '<option value="0">請選擇</option><option value="1">轉出</option><option value="2"  selected="selected" >報廢</option>');

var _bookUseTo = new Array('<option value="0">請選擇</option><option value="1">外借</option><option value="2">內用</option>', '<option value="0">請選擇</option><option value="1"  selected="selected" >外借</option><option value="2">內用</option>', '<option value="0">請選擇</option><option value="1">外借</option><option value="2"  selected="selected" >內用</option>');


$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    AspAjax.getClassificationData();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#mainContentIndex").add("#mainContentIndex2").add("#mainContentIndex3").add("#mainContentIndex4").hide();
    $(".mainSearchList").show();

    $(".menuTabs2").click(function() {
        if (this.id != "btnReturn") {
            $(".menuTabs2").css("background-image", "url(./images/bg_menutab1.jpg)");
            $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
            $("#mainContentSearch").add("#mainContentInsert").add("#mainContentIndex").add("#mainContentIndex2").add("#mainContentIndex3").add("#mainContentIndex4").add(".pagination").hide();
            var DivName = '';
            switch (this.id) {
                case "btnSearch":
                    DivName = "#mainContentSearch";
                    break;
                case "btnInsert":
                    DivName = "#mainContentInsert";
                    $(DivName).find("button").show();
                    setTimeout('$("#Unit").html(_UnitList[_uUnit]);', 500);
                    break;
                case "btnIndex":
                    DivName = "#mainContentIndex";
                    break;
                case "btnIndex2":
                    DivName = "#mainContentIndex2";
                    break;
                case "btnIndex3":
                    DivName = "#mainContentIndex3";
                    $("#gosrhrecordBookCode").unbind("click").click(function() {
                        var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="500">' +
                    '<tr><td width="250">書　碼 <input type="text" value="" id="txtstationeryName" autocomplete="off" /></td>' +
                    '<td>分　類 <select id="bookClassification2" name="Category" autocomplete="off" style="width:170px;"></select></td></tr>' +
                    '<tr><td>書　名 <input type="text" value="" id="txtstationeryName" autocomplete="off" /></td>' +
                    '<td>作　者 <input type="text" value="" id="txtstationeryName" autocomplete="off" /></td></tr>' +
                    '<tr><td>出版社 <input type="text" value="" id="txtstationeryName" autocomplete="off" /></td><td>&nbsp;</td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table>' +
                    '<table id="StuinlineReturn" class="tableList" border="0" width="500">' +
                        '<thead>' +
		                    '<tr>' +
		                        '<th width="80">書碼</th>' +
		                        '<th width="100">分類</th>' +
		                        '<th width="180">書名</th>' +
		                        '<th width="70">作者</th>' +
		                        '<th width="70">出版社</th>' +
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
                                var options = $("#gosrhbookClassification> option").clone();
                                $("#bookClassification2").append(options);
                                $("#inline .btnSearch").unbind("click").click(function() {
                                    var obj = MyBase.getTextValueBase("searchStuinline");
                                    AspAjax.searchBookDataResultCount(obj);
                                });
                            }
                        });
                    });
                    break;
                case "btnIndex4":
                    DivName = "#mainContentIndex4";
                    break;
            }
            $(DivName).add(".btnAdd").fadeIn();
            $(DivName + " input[type='text']").val("");
            $(DivName + " .hideClassSpan").html("");
            $(DivName + " select").val(0);
            $(".tableList tbody").empty();
            $("#bookFilingDate").val(TodayADDateFunction());
        }
    });

    $("#searchTable5 select").change(function() {
        if ($(this).val() == 2) {
            $("#searchTable5").find("div").fadeIn();
            //$("#BorrowerClassTitle").show();
        } else {
            $("#searchTable5").find("div").fadeOut();
            $("#BorrowerClassTitle").hide();
        }
    });

    $("#bookClassification").change(function() {
        var obj = new Object();

        if ($(this).val() != "0") {
            if ($(this).val().length > 0) {
                obj.bookClassification = $(this).val();
            }
            if ($(this).find("option:selected").text().length > 0) {
                obj.bookClassificationCode = $(this).find("option:selected").text().substr(0, 4);
            }
            if (obj.bookClassification != null && obj.bookClassificationCode != null) {
                AspAjax.GetBookIDData(obj);
            }
        } else {
        $("#bookID").text("");
        }



    });
});


function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "GetBookIDData":
            if (result[0] != null) {
                $("#bookID").text(result[0]);
            } else {
            alert("發生錯誤，發生錯誤如下：" + result[1]);
            }
            break;
        case "getClassificationData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                var inner = '';
                for (var i = 0; i < result.length; i++) {
                    inner += '<option value="' + result[i][0] + '">' + result[i][1] + ' ' + result[i][2] + '</option>';
                }
                $("select[name='Category']").append(inner);
            }
            break;
        case "createBookDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                alert("新增成功");
                clearInsert();
            }
            break;
        case "searchBookDataCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTable");
                        AspAjax.searchBookData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchBookData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result[i].bID + '">' +
                        '<td colspan="8" ><table><tr>' +
			                    '<td height="45">' + result[i].bookNumber + '</td>' +

			                    '<td width="110" >' + result[i].bookClassificationCode + ' ' + result[i].bookClassificationName + '</td>' +
			                    '<td><input class="bookTitle" type="text" value="' + result[i].bookTitle + '" size="15"/></td>' +
			                    '<td><input class="bookAuthor" type="text" value="' + result[i].bookAuthor + '" size="10" /></td>' +
			                    '<td><input class="bookPress" type="text" value="' + result[i].bookPress + '" size="10" /></td>' +
			                    '<td><input class="bookPressDate" type="text" class="date" value="' + TransformADFromStringFunction(result[i].bookPressDate) + '" size="10" /></td>' +
			                    '<td><textarea rows="1" cols="15" class="bookRemark">' + result[i].bookRemark + '</textarea></td>' +
			                    '<td>' + _bookStatus[result[i].bookStatus] + '</td>' +
			                    '</tr><tr>' +
			                    '<td>轉出/報廢：</td>' +
			                    '<td><select class="HsbookScrapstatus" >' + _bookScrapstatus[result[i].bookScrapstatus] + '</select></td>' +
			                    '<td>用途：<select class="HsbookUseTo" >' + _bookUseTo[result[i].bookUseTo] + '</select></td>' +
			                    '<td>來源：</td>' +
			                    '<td><input class="HsbookComefrom" type="text" value="' + result[i].bookComefrom + '" size="10" /></td>' +
			                    '<td>捐贈者：</td>' +
			                    '<td><input class="HsbookGeter" type="text" value="' + result[i].bookGeter + '" size="10" /></td>' +
			                    '</tr></table></td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].bID + ')">更 新</button>' +
			                    '<br /><button class="btnView" type="button" onclick="DelData(' + result[i].bID + ')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(' + result[i].bID + ')">儲 存</button>' +
			                    '<br /><button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].bID + '\',\'' + i + '\')">取 消</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList .bookPressDate").datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("#mainSearchList input[type=text]").add("#mainSearchList select").add("#mainSearchList textarea").attr("disabled", true);
                    $("#mainSearchList").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        case "setBookData1":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                showView(1);
            }
            break;
        case "delBookData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                $.fancybox.close();
                showView(1);
            }
            break;
        case "searchBookDayDataCount":
            var pageCount = parseInt(result);
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
                        AspAjax.searchBookDayData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchBookDayData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td height="35">' + result[i].borrowerName + '</td>' +
			                    '<td>' + result[i].bookCode + '</td>' +
			                    '<td>' + result[i].bookName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].borrowDate) + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].expireDate) + '</td>' +
			                '</tr>';
                    }
                    $("#mainIndexList .tableList").children("tbody").html(inner);
                    $("#mainIndexList").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            }
            break;
        case "searchBookDateData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo != -1) {
                    var inner = "";
                    var bookDate = TransformADFromStringFunction(result[0].bookBorrowStartDate) + "~" + TransformADFromStringFunction(result[0].bookBorrowEndDate);
                    inner += '<tr>' +
			                    '<td height="40">' + result[0].studentBorrowBookSum + '</td>' +
			                    '<td>' + result[0].studentBorrowerSum + '</td>' +
			                    '<td>' + result[0].staffBorrowBookSum + '</td>' +
			                    '<td>' + result[0].staffBorrowerSum + '</td>' +
			                '</tr>';
                    $("#mainIndexList2 .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList2 .tableList").children("tbody").html("<tr><td colspan='3'>查無資料</td></tr>");
            }
            break;
        case "searchBookDataResultCount":
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
                        AspAjax.searchBookDataResult(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            } else {
                $("#StuinlineReturn").children("tbody").html("發生錯誤");
            }
            break;
        case "searchBookDataResult":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="' + result[i].bID + '">' +
                                '<td>' + result[i].bookNumber + '</td>' +
			                    '<td>' + result[i].bookClassificationCode + ' ' + result[i].bookClassificationName + '</td>' +
			                    '<td>' + result[i].bookTitle + '</td>' +
			                    '<td>' + result[i].bookAuthor + '</td>' +
			                    '<td>' + result[i].bookPress + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("s_", "");
                        $("#txtrecordBookID").html(id);
                        $("#gosrhrecordBookCode").val($(this).find("td:nth-child(1)").html());
                        $("#recordBookName").val($(this).find("td:nth-child(3)").html());
                        $.fancybox.close();
                    });
                } else {
                alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            }
            break;
        case "searchBookRecordDataCount":
            var pageCount = parseInt(result);
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
                        AspAjax.searchBookRecordData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList3 .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainIndexList3 .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchBookRecordData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td height="35">' + _borrowerStatus[result[i].borrowerStatus] + '</td>' +
                                '<td>' + result[i].borrowerID + '</td>' +
                                '<td>' + result[i].borrowerName + '</td>' +
			                    '<td>' + result[i].bookCode + '</td>' +
			                    '<td>' + result[i].bookName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].borrowDate) + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].restoreDate) + '</td>' +
			                '</tr>';
                    }
                    $("#mainIndexList3 .tableList").children("tbody").html(inner);
                    $("#mainIndexList3").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList3 .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "searchBookRecordBorrowerDataCount":
            var pageCount = parseInt(result);
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
                        AspAjax.searchBookRecordBorrowerData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList4 .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            } else {
                $("#mainIndexList4 .tableList").children("tbody").html("<tr><td colspan='5'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchBookRecordBorrowerData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td height="35">' + _borrowerStatus[result[i].borrowerStatus] + '</td>' +
                                //'<td>' + result[i].borrowerClassID + '</td>' +
                                '<td>' + result[i].borrowerID + '</td>' +
                                '<td>' + result[i].borrowerName + '</td>' +
                                '<td>' + result[i].borrowQuantity + '</td>' +
			                '</tr>';
                    }
                    $("#mainIndexList4 .tableList").children("tbody").html(inner);
                    $("#mainIndexList4").next(".pagination").show();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList4 .tableList").children("tbody").html("<tr><td colspan='5'>查無資料</td></tr>");
            }
            break;
    }
}

function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function showView(viewID) {
    var DivName = "";
    switch (viewID) {
        case 1:
            DivName = "#mainSearchList";
            var obj = MyBase.getTextValueBase("searchTable");
            AspAjax.searchBookDataCount(obj);
            break;
        case 2:
            DivName = "#mainIndexList";
            var obj = MyBase.getTextValueBase("searchTable2");
            var checkString = MyBase.noEmptyCheck(noEmptyItem2, obj, null, noEmptyShow2);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                AspAjax.searchBookDayDataCount(obj);
            }
            break;
        case 3:
            DivName = "#mainIndexList2";
            var obj = MyBase.getTextValueBase("searchTable3");
            var checkString = MyBase.noEmptyCheck(noEmptyItem3, obj, null, noEmptyShow3);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                AspAjax.searchBookDateData(obj);
            }
            break;
        case 4:
            DivName = "#mainIndexList3";
            var obj = MyBase.getTextValueBase("searchTable4");
            var obj1 = getHideSpanValue("searchTable4", "hideClassSpan");
            MergerObject(obj, obj1);
            if ((obj.txtrecordBookStartDate != null && obj.txtrecordBookEndDate != null) || (obj.txtrecordBookStartDate == null && obj.txtrecordBookEndDate == null)) {
                AspAjax.searchBookRecordDataCount(obj);
            } else {
                alert("請填寫完整日期區間");
            }
            break;
        case 5:
            DivName = "#mainIndexList4";
            var obj = MyBase.getTextValueBase("searchTable5");
            var checkString = MyBase.noEmptyCheck(noEmptyItem4, obj, null, noEmptyShow4);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                if ((obj.gosrhrecordBorrowerStartDate != null && obj.gosrhrecordBorrowerEndDate != null) || (obj.gosrhrecordBorrowerStartDate == null && obj.gosrhrecordBorrowerEndDate == null)) {
                    AspAjax.searchBookRecordBorrowerDataCount(obj);
                } else {
                    alert("請填寫完整日期區間");
                }
            }
            break;
    }
    $(DivName).fadeIn();
    $(DivName + " input[type=text]").add(DivName + " select").add(DivName + " textarea").attr("disabled", true);
}

function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").add("#HS_" + TrID + " select").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();    
}

function SaveData(TrID) {
    var obj = new Object();
    obj.bID = parseInt(TrID);
    var BookTitle = $("#HS_" + TrID + " .bookTitle").val();
    if (BookTitle.length > 0) {
        obj.executionTitle = BookTitle;
    }
    var BookAuthor = $("#HS_" + TrID + " .bookAuthor").val();
    if (BookAuthor.length > 0) {
        obj.executionAuthor = BookAuthor;
    }
    var BookPress = $("#HS_" + TrID + " .bookPress").val();
    if (BookPress.length > 0) {
        obj.executionPress = BookPress;
    }
    var BookPressDate = TransformRepublicReturnValue($("#HS_" + TrID + " .bookPressDate").val());
    if (BookPressDate.length > 0) {
        obj.executionPressDate = BookPressDate;
    }
    var BookRemark = $("#HS_" + TrID + " .bookRemark").val();
    if (BookRemark.length > 0) {
        obj.executionRemark = BookRemark;
    }
    var BookScrapstatus = $("#HS_" + TrID + " .HsbookScrapstatus").val();
    if (BookScrapstatus.length > 0) {
        obj.executionbookScrapstatus = BookScrapstatus;
    }
    var BookUseTo = $("#HS_" + TrID + " .HsbookUseTo").val();
    if (BookUseTo.length > 0) {
        obj.executionbookUseTo = BookUseTo;
    }
    var BookComefrom = $("#HS_" + TrID + " .HsbookComefrom").val();
    if (BookComefrom.length > 0) {
        obj.executionbookComefrom = BookComefrom;
    }
    var BookGeter = $("#HS_" + TrID + " .HsbookGeter").val();
    if (BookGeter.length > 0) {
        obj.executionbookGeter = BookGeter;
    }

    if (obj.executionTitle != null) {

   // if (obj.executionTitle != null && obj.executionAuthor != null && obj.executionPress != null && obj.executionPressDate != null && obj.executionRemark != null && obj.executionbookScrapstatus != null && obj.executionbookUseTo != null && obj.executionbookComefrom != null && obj.executionbookGeter!=null) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();       
        AspAjax.setBookData1(obj);
    } else {
        alert("請填寫完整");
    }
}

function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delBookData(parseInt(TrID));
    });
}

function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .bookTitle").val(_ReturnValue[vIndex].bookTitle);
    $("#HS_" + TrID + " .bookAuthor").val(_ReturnValue[vIndex].bookAuthor);
    $("#HS_" + TrID + " .bookPress").val(_ReturnValue[vIndex].bookPress);
    $("#HS_" + TrID + " .bookPressDate").val(TransformADFromStringFunction(_ReturnValue[vIndex].bookPressDate));
    $("#HS_" + TrID + " .bookRemark").val(_ReturnValue[vIndex].bookRemark);

    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}



function saveInsert() {
    var obj = MyBase.getTextValueBase("insertDataDiv");
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    var categoryName = $("#insertDataDiv select[name='Category']").find("option:selected").text()
    obj.bookClassificationCode = categoryName.substr(0, 4);
    obj.bookUseTo = $("#bookUseTo").val();
    obj.bookScrapstatus = $("#bookScrapstatus").val();
    obj.bookFilingDate = TransformRepublicReturnValue($("#bookFilingDate").val());
    
    //bookUseTo, bookComefrom, bookGeter, bookScrapstatus
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createBookDataBase(obj);
    }
}


function clearInsert() {
    $("#mainContentInsert").find("input[type='text']").val("");
    $("#mainContentInsert").find("textarea").val("");
    $("#mainContentInsert select").val(0);
}

