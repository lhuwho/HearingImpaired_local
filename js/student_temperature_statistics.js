var obj;
var editYear;
var editMonth;
var editStudentID;
var editStudentName;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
   
    setDate(1990);
    initPage();
    
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

    $(".btnSearch").click(function() {

        if ($('#yearDate').val() != -1 && $('#monthDate').val() != -1) {
            AspAjax.getStudentTemperatureData($('#studentID').val(), $('#yearDate').val(), $('#monthDate').val());
            
        } else if ($('#yearDate').val() == -1 ||$('#monthDate').val() == -1) {
            alert("請選擇測量年月份");
        }
    });
});
function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "getStudentTemperatureData":
            editStudentName = result[0].StudentName;
            if (editStudentName.length > 0) {
                calendar($('#yearDate').val(), $('#monthDate').val());
                $("#SName").html(result[0].StudentName);
                for (var i = 0; i < result.length; i++) {
                    editStudentID = result[i].txtpeopleID;

                    editYear = result[i].Year;
                    editMonth = result[i].Month;
                    var day = result[i].Day;
                    $("#c_" + day).val(result[i].peopleTemp);
                    $("#p_" + day).val(result[i].parentsTemp);
                    $("#i_" + day).val(result[i].leaveItem);
                    $("#r_" + day).val(result[i].leaveStatus);
                }
            }
            else {
                alert("無此ID");
            }
            break;
        case "updateStudentTemperatureDataBase":
            break;
    }
}
function calendar(getYear, getMonth) {
    var today, year, month, thisday, nDays;
    var monthArray = new Array("一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月");

    var inner = '';
    year = parseInt(getYear, 10) + 1911;
    month = getMonth - 1;
    today = new Date(year, month);

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
    firstDay = today;
    firstDay.setDate(1);
    startDay = firstDay.getDay();

    inner += "<table class=tableList width=780 border=0>";
    inner += "<caption>體溫紀錄表</caption>";
    inner += "<thead>";
   
    inner += "<tr><th>" + getYear + "<br />" + monthArray[month] + "</th>";
    inner += "<th id='SName'></th></tr></thead>";
    column = 1;
    for (var j = 1; j < startDay; j++) {
        column++;
    }
    for (var i = 1; i <= nDays; i++) {
        if (column < 6) {
            inner += "<tr id='tr_" + i + "' class='data'>";
            inner +="<td align='center' class='cDay'>"+ i + "</td>";
            inner += "<td>c: <input id='c_" + i + "' class='peopleTemp' type='text' value='' size='5' />　" +
            "p: <input id='p_" + i + "' class='parentsTemp' type='text' value='' size='5' />　" +
            "請假項目 <select id='i_" + i + "' class='leaveItem'><option value='0'>正常</option>" +
            "<option value='1'>事假</option><option value='2'>病假</option><select>　" +
            "狀況 <input id='r_" + i + "' class='leaveStatus' type='text' value='' /></td></tr>";
        }
        if (column == 7) {
            column = 1;
        } else {
            column++;
        }
    }
    inner += "</table>";
    inner += '<p class="btnP">' +
                '<button class="btnSave" type="button">儲 存</button> ' +
                '<button class="btnUpdate" type="button">更 新</button> ' +
                '<button class="btnSaveUdapteData" type="button" onclick="save()">存 檔</button> ' +
                '<button class="btnCancel" type="button">取 消</button>' +
            '</p>';
    $("#mainSearchList").html(inner);

    $(".btnUpdate").fadeIn();
    $("#mainSearchList input[type='text']").attr("disabled", true);
    $("#mainSearchList select").attr("disabled", true);
    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("#mainSearchList input[type='text']").attr("disabled", false);
        $("#mainSearchList select").attr("disabled", false);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("#mainSearchList input[type='text']").attr("disabled", true);
        $("#mainSearchList select").attr("disabled", true);
    });
}

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

function save() {
    var myData = new Array();

    $(".data").each(function() {
        var data = {};

        data.peopleTemp = $(this).find(".peopleTemp").val();
        data.parentsTemp = $(this).find(".parentsTemp").val();
        data.leaveItem = $(this).find(".leaveItem").val();
        data.leaveStatus = $(this).find(".leaveStatus").val();
        data.Day = $(this).find(".cDay").html();
        if ($('#yearDate').val() != -1 && $('#monthDate').val() != -1) {
            data.Month = $('#monthDate').val();
            data.Year = parseInt($('#yearDate').val()) + 1911;
        }
        else {
            data.Month = editMonth;
            data.Year = editYear;

        }
        data.txtpeopleID = editStudentID;
        myData[myData.length] = data;
    });
    AspAjax.updateStudentTemperatureDataBase(myData);
}