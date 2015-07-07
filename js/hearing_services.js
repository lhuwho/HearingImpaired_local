var MyBase = new Base();
var itemNumber = new Array("A", "B", "C", "D", "E", "F", "G", "H", "I", "J");
var itemArray = new Array("(A)定期追蹤", "(B)助聽器調整", "(C)聽力異常", "(D)新生諮詢", "(E)電子耳調圖", "(F)語音聽知覺", "(G)調頻系統評估", "(H)ISP會議", "(I)輔具功能異常原", "(J)其他");
var _StudentStatu = new Array("會外人士", "會內生", "會內生", "離會生");
//var _ReturnStaffValue;
var _ReturnValue;

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    //AspAjax.getAllStaffDataList([16]);
    //setTimeout('$("#Unit").html(_UnitList[_uUnit]);', 500);
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

    $("#txtstudentName").unbind("click").click(function() {
        var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                '<tr><td width="80">學生姓名 <input type="text" id="gosrhstudentName" /></td></tr>' +
                '<tr><td>學生出生日期 <input class="date" type="text" size="10" id="gosrhbirthdaystart" />' +
                '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                '</table>' +
                '<table id="StuinlineReturn" class="tableList" border="0"  width="400">' +
                    '<thead>' +
	                    '<tr>' +
	                        '<th width="110">服務使用者編號	</th>' +
	                        '<th width="100">學生姓名</th>' +
	                        '<th width="70">身分別</th>' +
	                        '<th width="50">性別</th>' +
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
                $('.date').datepick({
                    yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                });
                $('button').css({ "background-color": "#F9AE56" });
                $("#inline .btnSearch").unbind("click").click(function() {
                    var obj = MyBase.getTextValueBase("searchStuinline");
                    obj.txtbirthdayend = obj.txtbirthdaystart;
                    AspAjax.SearchStudentDataBaseCount(obj);
                });
            }
        });
    });
});

function UpData(TrID) {
    $("#HS_" + TrID + " textarea").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function SaveData(TrID) {
    var obj = new Object();
    obj.aID = parseInt(TrID);
    var SexecutionContent = $("#HS_" + TrID + " .ScourseContent").val();
    if (SexecutionContent.length > 0) {
        obj.aContent = SexecutionContent;
    }
    if (obj.aContent != null) {
        $("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setAudiometryAppointmentContent(obj);
    }
    
    $("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}

function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .ScourseContent").val(_ReturnValue[vIndex].aContent);
    $("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}

function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delAudiometryAppointment(parseInt(TrID));
    });
}

function showView(viewID) {
    if (($("#txtstudentName").val()).length > 0) {
        $("#mainSearchList").fadeIn();
        $("#mainSearchList input[type=text]").add("#mainSearchList select").attr("disabled", true);
        var obj = MyBase.getTextValueBase("searchTable");
        var obj1 = getHideSpanValue("searchTable", "hideClassSpan");
        MergerObject(obj, obj1);
        AspAjax.SearchAudiometryAppointmentDataBaseCount(obj);
    } else {
        alert("請選擇學生姓名");
    }
}

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
			                    '<td>' + _StudentStatu[result[i].txtstudentStatu] + '</td>' +
			                    '<td>' + _SexList[result[i].txtstudentSex] + '</td>' +
			                    '<td>' + TransformADFromDateFunction(result[i].txtstudentbirthday) + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        $("#txtstudentName").val($(this).children("td:nth-child(2)").html());
                        $("#txtstudentID").html($(this).children("td:nth-child(1)").html())
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "setAudiometryAppointmentContent":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
                showView(1);
            }
            break;
        case "delAudiometryAppointment":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                $.fancybox.close();
                showView(1);
            }
            break;

        case "SearchAudiometryAppointmentDataBaseCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                $("#SumCredit").val(result[1]);
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
                        var obj1 = getHideSpanValue("searchTable", "hideClassSpan");
                        MergerObject(obj, obj1);
                        AspAjax.SearchAudiometryAppointmentDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchAudiometryAppointmentDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].ID != -1) {
                    _ReturnValue = result;
                    var inner = '';
                    for (var i = 0; i < result.length; i++) {

                        var itemStr = '';
                        var itemArr = (result[i].aItem).split("＠");
                        for (var j = 0; j < itemArr.length; j++) {
                            itemStr += itemArray[(itemNumber.indexOf(itemArr[j]))];
                            if (j < (itemArr.length - 1)) {
                                itemStr += ", ";
                            }
                        }
                        if (itemArr.indexOf("J") > -1) {
                            itemStr += "_" + result[i].aItemExplain;
                        }

                        var assessStr = '';
                        if (result[i].aAssessName1 != '' && result[i].aAssessName2 != '') {
                            assessStr = result[i].aAssessName1 + ', ' + result[i].aAssessName2;
                        } else {
                            if (result[i].aAssessName1 != '') {
                                assessStr = result[i].aAssessName1;
                            } else if (result[i].aAssessName2 != '') {
                                assessStr = result[i].aAssessName2;
                            }
                        }

                        inner += ' <tr id="HS_' + result[i].aID + '" class="sStaffData" >' +
                                '<td height="45">' + result[i].aRowNum + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].aDate) + '</td>' +
			                    '<td>' + itemStr + '</td>' +
					            '<td>' + assessStr + '</td>' +
			                    '<td><textarea class="ScourseContent" rows="1">' + result[i].aContent + '</textarea></td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(\'' + result[i].aID + '\')">更 新</button> <button class="btnView" type="button" onclick="DelData(\'' + result[i].aID + '\')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(\'' + result[i].aID + '\')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].aID + '\',\'' + i + '\')">取 消</button></div>' +
			                    '</td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList textarea").attr("disabled", true);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].courseName);
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

