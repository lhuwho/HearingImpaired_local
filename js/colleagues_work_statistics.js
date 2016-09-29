var MyBase = new Base();
$(document).ready(function() {
AspAjax.set_defaultSucceededCallback(SucceededCallback);

    setDate(1990);
    initPage();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#mainContentIndex").hide();

    $(".menuTabs2").click(function() {
        $(".menuTabs2").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").add("#mainContentIndex").add(".mainSearchList").add(".insertDataDiv").hide();
        var DivName = '';
        switch (this.id) {
            case "btnSearch":
                DivName = "#mainContentSearch";
                break;
            case "btnIndex":
                DivName = "#mainContentIndex";
                break;
        }
        $(DivName).add(".btnAdd").fadeIn();
    });
});

function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStaffDataBaseWorkDetail":
            alert(result);
            //alert("");
            break;
        case "SearchStaffDataBaseWorkAllCount":
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
                        //var obj = MyBase.getTextValueBase("searchTable");
                    AspAjax.SearchStaffDataBaseWorkAll((parseInt(parseInt($("#yearDate2").val())) + 1911), $("#monthDate2").val(), parseInt((index + 1) * _LimitPage, 10), $("#dayDate2").val());
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBaseWorkAll":
            if (!(result == null || result.length == 0 || result == undefined)) {
                console.log(result);
                var inner = "";
                for (var i = 0; i < result.length; i++) {

                    var YearCation = result[i].YearVaction == '' ? parseFloat('0.0') : parseFloat(result[i].YearVaction);
                    var WorkAdd = result[i].WorkAdd == '' ? parseFloat('0.0') : parseFloat(result[i].WorkAdd);
                    var WorkMinus = result[i].WorkMinus == '' ? parseFloat('0.0') : parseFloat(result[i].WorkMinus);
                    var yearvac = YearCation + WorkAdd - WorkMinus;
                    var V4 = result[i].V4 == '' ? parseFloat('0.0') : parseFloat(result[i].V4);
                    var V11 = result[i].V11 == '' ? parseFloat('0.0') : parseFloat(result[i].V11);
                    var YearUser = V4 + V11;
                    console.log(V4 + "-" + V11 + "-" + YearUser );
                    inner += '<tr>' +
                        '<td>' + result[i].StaffID + '</td>' +
                        '<td>' + result[i].StaffName + '</td>' +
                        '<td><span style="color:blue;" >' + (yearvac == 0 ? '' : yearvac) + '</span></td>' +
                    //                        '<td><span style="color:blue;" >' + (result[i].YearVaction == "0.000" || result[i].YearVaction == '' ? '' : parseFloat(result[i].YearVaction)) + '</span></td>' +
                    //                        '<td><span style="color:blue;" >' + (result[i].WorkAdd == "0.000" || result[i].WorkAdd == '' ? '' : parseFloat(result[i].WorkAdd)) + '</span></td>' + 
                    //                        '<td><span style="color:blue;" >' + (result[i].WorkMinus == "0.000" || result[i].WorkMinus == '' ? '' : parseFloat(result[i].WorkMinus)) + '</span></td>' +
                    //                        '<td>' + result[i].WorkMinus + '</td>' +
                    //                        '<td>' + result[i].WorkAdd + '</td>' +
                        '<td>' + result[i].V1 + '</td>' +
                        '<td>' + result[i].V2 + '</td>' +
                        '<td>' + result[i].V3 + '</td>' +
                        '<td>' + (YearUser == 0 ? '' : YearUser) + '</td>' +


                        '<td>' + result[i].V6 + '</td>' +
                        '<td>' + result[i].V7 + '</td>' +
                        '<td>' + result[i].V8 + '</td>' +

                        '<td>' + result[i].V5 + '</td>' +
                        '<td>' + result[i].V9 + '</td>' +
                        '<td>' + result[i].V10 + '</td>' +

                        '<td>' + result[i].V12 + '</td>' +
                        '<td>' + result[i].V14 + '</td>' +
                        //'<td>' + result[i].V11 + '</td>' +
                    // '<td><button class="btnView" type="button" onclick="viewRecord(' + result[i].StaffID + ')">檢 視</button></td>' +
			                '</tr>';
                }
                $("#mainSearchList .tableList").children("tbody").html(inner);

            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
    }
}
function viewRecord(StaffID) {
    var year = (parseInt($("#yearDate2").val()) + 1911);
    var month = $("#monthDate2").val();
    AspAjax.SearchStaffDataBaseWorkDetail(StaffID, year, month);
    //alert(AspAjax.SearchStaffDataBaseWorkDetail(StaffID,  year, month));
    //alert(AspAjax.SearchStaffDataBaseWorkDetail(StaffID, (parseInt(parseInt($("#yearDate2").val())) + 1911), $("#indexpage").val(), $("#monthDate2").val()));
    //alert(AspAjax.SearchStaffDataBaseWorkAll(( parseInt( parseInt( $("#yearDate2").val())) + 1911), $("#monthDate2").val(), parseInt((index + 1) * _LimitPage, 10)));
}


function Search(viewID) {
    switch (viewID) {
        case 1:
        //年
            if ($("#yearDate2").val() != -1 && $("#monthDate2").val() == -1 && $("#dayDate2").val() == -1) {
                $("#mainSearchList .tableList").children("tbody").empty();
                $("#mainSearchList .tableList").children("caption").html($("#yearDate2").val() + "年個人出勤表");
                AspAjax.SearchStaffDataBaseWorkAllCount((parseInt($("#yearDate2").val()) + 1911), $("#monthDate2").val(),$("#dayDate2").val());
            }
        //月
            else if ($("#monthDate2").val() != -1 && $("#yearDate2").val() != -1 && $("#dayDate2").val() == -1) { //&& $("#dayDate2").val() != -1
                $("#mainSearchList .tableList").children("tbody").empty();
                $("#mainSearchList .tableList").children("caption").html($("#yearDate2").val() + "年" + $("#monthDate2").val() + "月個人出勤表");
                AspAjax.SearchStaffDataBaseWorkAllCount((parseInt($("#yearDate2").val()) + 1911), $("#monthDate2").val(), $("#dayDate2").val());
                //                var obj = {};
                //                obj.monthDate = $("#monthDate2").val();
                //                obj.yearDate = $("#yearDate2").val();
                //alert(obj.monthDate);
                //AspAjax.SearchStaffDataBaseWorkCount(obj);
            }
            else if ($("#yearDate2").val() != -1 && $("#monthDate2").val() != -1 && $("#dayDate2").val() != -1) {
                $("#mainSearchList .tableList").children("tbody").empty();
                $("#mainSearchList .tableList").children("caption").html($("#yearDate2").val() + "年" + $("#monthDate2").val() + "月" + $("#dayDate2").val() + "日個人出勤表");
                AspAjax.SearchStaffDataBaseWorkAllCount((parseInt($("#yearDate2").val()) + 1911), $("#monthDate2").val(), $("#dayDate2").val());
            }
            //日
            else {
                $("#mainSearchList .tableList").children("tbody").empty(); alert("請選擇日期");
            }
            break;
        case 2:
            DivName = "#mainIndexList";
            $(DivName).fadeIn();
            break;
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
    //日(選定月份日期在做)
}

function setDay() {

    if ($('#monthDate2').val() > 0 && $('#yearDate2').val() > 0) {
        var year = parseInt($('#yearDate2').val()) + 1911;
        var month = parseInt($('#monthDate2').val()) - 1, nDays;

        if (month == 1) {
            if (((year % 4 == 0) && (year % 100 != 0)) || (year % 400 == 0)) {
                nDays = 29;
            } else {
                nDays = 28;
            }
        } else if ((month == 3) || (month == 5) || (month == 8) || (month == 10)) {
            nDays = 30;
        } else {
            nDays = 31;
        }
        $('#dayDate').add('#dayDate2').empty().append($("<option></option>").attr("value", '-1').text('日'));
        for (var i = 1; i <= nDays; i++) {
            $('#dayDate').add('#dayDate2').append($("<option></option>").attr("value", i).text(i));
        }
    }
    else if ($('#yearDate2').val()<0)
    {
        $('#monthDate').add('#monthDate2').val("-1");
        $('#dayDate').add('#dayDate2').empty().append($("<option></option>").attr("value", '-1').text('日')); 
    }
    else  {
        $('#dayDate').add('#dayDate2').empty().append($("<option></option>").attr("value", '-1').text('日')); 
    }
    
    
}