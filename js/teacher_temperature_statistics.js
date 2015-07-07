var obj;
var editYear;
var editMonth;
var editStudentID;
var editTeacherName;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);

    setDate(1990);
    initPage();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

    $(".btnSearch").click(function() {
        if ($('#yearDate').val() != -1 && $('#monthDate').val() != -1) {
            //alert($('#name').val());
            AspAjax.getTeacherTemperatureData($('#name').val(), $('#yearDate').val(), $('#monthDate').val());
            // calendar($('#yearDate').val(), $('#monthDate').val());
        } else if ($('#yearDate').val() == -1 || $('#monthDate').val() == -1) {
            alert("請選擇測量年月份");
        }
    });
});


function SucceededCallback(result, userContext, methodName) {
    
    switch (methodName) {
        case "getTeacherTemperatureData":
            editTeacherName = result[0].TeacherName;
            if (editTeacherName.length > 0) {
                calendar($('#yearDate').val(), $('#monthDate').val());
                // $("#SName").html(result[0].StudentName);
                for (var i = 0; i < result.length; i++) {
                    editStudentID = result[i].txtpeopleID;

                    editYear = result[i].Year;
                    editMonth = result[i].Month;
                    var day = result[i].Day;
                    $("#a_" + day).val(result[i].TeacherTemp);
                    $("#b_" + day).val(result[i].CheckContent);
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
    inner += "<tr><th width=260>" + getYear + " 年/ " + monthArray[month] + "</th>";
    inner += "<th width=260>體溫</th><th width=260>備註</th></tr>";
    column = 0;
    for (var j = 0; j < startDay; j++) {
        column++;
    }
    for (var i = 1; i <= nDays; i++) {
        inner += "<tr id='tr_" + i + "' class='data' ><td align='center' class='cDay' >";
        inner += i + "</td><td align='center'>";
        if (column == 0 || column == 6) {
            inner += "-----";
        } else {
        inner += "<input type='text'  id='a_" + i + "'  class='TeacherTemp'  value='' size='10' />";
        }
        inner += "</td><td><input type='text'   id='b_" + i + "' class='CheckContent' value='' size='15' /></td></tr>";
        column++;
        if (column == 7) {
            column = 0;
        }
    }
    inner += "</table>";
    inner += '<p class="btnP">' +
                '<button class="btnSave" type="button">儲 存</button> '+
                '<button class="btnUpdate" type="button">更 新</button> '+
                '<button class="btnSaveUdapteData" type="button" onclick="save()" >存 檔</button> ' +
                '<button class="btnCancel" type="button">取 消</button>' +
            '</p>';
    $("#mainSearchList").html(inner);

    $(".btnUpdate").fadeIn();
    $("#mainSearchList input[type='text']").attr("disabled", true);

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("#mainSearchList input[type='text']").attr("disabled", false);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("#mainSearchList input[type='text']").attr("disabled", true);
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

        data.TeacherTemp = $(this).find(".TeacherTemp").val();
        data.CheckContent = $(this).find(".CheckContent").val();
        data.Day = $(this).find(".cDay").html();
        if ($('#yearDate').val() != -1 && $('#monthDate').val() != -1) {
            data.Month = $('#monthDate').val();
            data.Year = parseInt($('#yearDate').val()) + 1911;
        }
        data.txtpeopleID = editStudentID;
        myData[myData.length] = data;
    });
    AspAjax.updateTeacherTemperatureDataBase(myData);
}