var MyBase = new Base();
var $calendar;
var _StudentStatu = new Array("會外人士", "會外人士", "會內生", "會內生", "離會生");
var $endTimeField ;
var $endTimeOptions;
var $timestampsOfOptions;
var _classRoomName = ["","E01","E02","E03","E04","E05","E06"];
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    $("#main").fadeIn();
    initPage();
     $("#teacherName").autocomplete({
        source: function (request, response) {
            var data = {
                term: request.term
            };
            $.ajax({
                type: "POST",
                url: "AspAjax.asmx/SearchStaff",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{ 'SearchString' : '" + request.term + "'}",
               
            }).success(function (data) {
                response(data.d);

            }).fail(function () {
              //  alert("failed");
            });
        }
    });
  //  WeekCalendarSetting();
});

function Search()
{
if($("#ClassName").val() != 0){
if($('#calendar').html() != ""){
$('#calendar').weekCalendar('today');}
else{
  WeekCalendarSetting();
}
}
else
{
alert("須選擇教室");
}
}
function WeekCalendarSetting() {
//    var $calendar;
//    $('#calendar').html("");
    $calendar = $('#calendar');
    var id = 10;
    $calendar.weekCalendar({
    readonly: "readonly",
        dateFormat: "Y. M. d",
        newEventText: "新建預約",
        timeslotHeight: 35,
        defaultEventLength: 1,
        timeslotsPerHour: 2,
        allowCalEventOverlap: true,
        overlapEventsSeparate: true,
        firstDayOfWeek: 1,
        businessHours: { start: 9, end: 18, limitDisplay: true },
        daysToShow: 5,
        buttonText: {
            today: '今天',
            lastWeek: '上週',
            nextWeek: '下週'
        },
        shortMonths: ['1', '2', '3', '4', '5', '6', '7', '8', '9', '10', '11', '12'],
        longMonths: ['一月', '二月', '三月', '四月', '五月', '六月', '七月', '八月', '九月', '十月', '十一月', '十二月'],
        shortDays: ['日', '一', '二', '三', '四', '五', '六'],
        longDays: ['星期日', '星期一', '星期二', '星期三', '星期四', '星期五', '星期六'],
        switchDisplay: { '1 day': 1, '3 next days': 3, 'work week': 5, 'full week': 7 },
        title: function(daysToShow) {
            return daysToShow == 1 ? '%date%' : '%start% - %end%';
        },
        height: function($calendar) {
            return $(window).height() - $("h1").outerHeight() - 1;
        },
        eventRender: function(calEvent, $event) {
            if (calEvent.end.getTime() < new Date().getTime()) {
                $event.css("backgroundColor", "#aaa");
                $event.find(".wc-time").css({
                    "backgroundColor": "#999",
                    "border": "1px solid #888"
                });
            }
        },
        draggable: function(calEvent, $event) {
            return calEvent.readOnly != true;
        },
        resizable: function(calEvent, $event) {
            return calEvent.readOnly != true;
        },
//        eventNew: function(calEvent, $event) {
//            eventNew(calEvent, $event);
//        },
        eventDrop: function(calEvent, $event) {
            eventDrop(calEvent, $event);
        },
        eventResize: function(calEvent, $event) {
        },
        eventClick: function(calEvent, $event) {
            eventClick(calEvent, $event);

        },
        eventMouseover: function(calEvent, $event) {
        },
        eventMouseout: function(calEvent, $event) {
        },
        noEvents: function() {
        },
        data: function(start, end, callback) {
           // alert(callback);
            getData(start, end, callback);

        }
    });
    ReadyInit();
}
function getData(start, end, callback) {
    $.ajax({
        type: 'POST',
        url: './teacher_schedule.ashx',
        data: { "sDate": TransformDBStringFunction(start), "eDate": TransformDBStringFunction(end), "sUnit": _uUnit, "type": "getEventData","ClassNameID": $('#ClassName').val() },
        dataType: 'json',
        async: false,
        contentType: 'application/x-www-form-urlencoded;charset=UTF-8',
        success: function(txt) {
            if (txt.length > 0) {
                var innerArray = new Array();
                for (var i = 0; i < txt.length; i++) {
                    // alert(txt[i]["aItem"]);
                    var item = (txt[i]["aItem"]); //.replace(/\＠/g, ",");

                    var titleStr = txt[i].TeacherName + " - " +  txt[i].Students.length +"人";
//                    if(txt[i].Students.length ==0 && txt[i].ClassID != 0)
//                    {
//                        titleStr +=  " - " + _classRoomName[ txt[i].ClassID] ;
//                    }
//                    else
//                    {
//                        titleStr += 
//                    }
                    //
                    var studentList = "";
                    var studentIDList = "";
                    for (var j = 0; j < txt[i].Students.length; j++) {
                        if (j != 0)
                        { studentList += ","; studentIDList += ",";}
                        studentList += "(" + txt[i].Students[j].ID +")"+ txt[i].Students[j].Name;
                        studentIDList += txt[i].Students[j].ID;
                    }
                    var readonly = false;
                    if (titleStr == 0) {
                        titleStr = "開會";
                        readonly = true;
                    }
                    var sstate = txt[i]["aStudentState"];
                    if (sstate == "") {
                        sstate = -1;
                    }
                    innerArray.push({ "id": txt[i].ID, "start": txt[i].StartTime, "end": txt[i].EndTime,
                    "author": txt[i]["aPeopleName"], "title": titleStr, readOnly: readonly, "sstate": parseInt(1, 10), "item": txt[i].TeacherName , "TeacherID": txt[i].TeacherID
                    , "Studentlength": txt[i].Students.length, "studentList": studentList, "studentIDList": studentIDList, "ID": txt[i].ID ,"ClassID":txt[i].ClassID
                    });
                }
                var innerJson = { events: innerArray };
                callback(innerJson);
            }
        }, error: function(txt) {
            alert("發生錯誤，請重新整理頁面");
        }
    });
}
function eventClick(calEvent, $event) {
    /*if (calEvent.readOnly) {
    return;
    }*/
    var $dialogContent = $("#event_edit_container");
    $("#studentName").attr("class", calEvent.studentid);
    $dialogContent.find("select[name='state']").find("option[value='" + calEvent.state + "']").attr('selected', true);
    //resetForm($dialogContent);
    //alert( calEvent.start.getDate());
    var dateField = calEvent.start.getFullYear() + "-" + (calEvent.start.getMonth() + 1) + "-" + calEvent.start.getDate();
    var startField = $dialogContent.find("select[name='start']").val(calEvent.start);
    var endField = $dialogContent.find("select[name='end']").val(calEvent.end);
    var authorField = $dialogContent.find("#author").html(calEvent.author);
    var studentField = $dialogContent.find("input[name='title']").val(calEvent.title).attr("readonly", "readonly");
    if (calEvent.sstate != -1) {
        var sstateField = $dialogContent.find("#studentStatu").html(_StudentStatu[calEvent.sstate]).show();
    } else {
        var sstateField = $dialogContent.find("#studentStatu").html(calEvent.sstate).hide();
    }
    var itemField = $dialogContent.find("select[name='item']").val(calEvent.item);
    var otherField = $dialogContent.find("input[name='other']").val(calEvent.other);
    var contentField = $dialogContent.find("textarea[name='content']").val(calEvent.content);
    var assessField = $dialogContent.find("select[name='assess']").val(calEvent.assess);
    var stateField = $dialogContent.find("select[name='state']");
    var studentList = calEvent.studentList.split(",");
    var studentIDList = calEvent.studentIDList.split(",");
    for (var i = 0; i < calEvent.Studentlength; i++) {
        var inner = '<li class="search-choice participant" id="participant_' + studentIDList[i] + '">' +
                            '<span>' + studentList[i] + '</span>' +
                        '</li>';
        $("#studentName").append(inner);
    
    }
    $("#ClassNameID").val(calEvent.ClassID);
   
    $dialogContent.dialog({
        modal: true,
        title: "編輯 - " + calEvent.title,
        close: function() {
            $dialogContent.dialog("destroy");
            $dialogContent.hide();
            $("#studentName").html("");
            
            $('#calendar').weekCalendar("removeUnsavedEvents");
        },

    }).show();

$dialogContent.find(".date_holder").text($calendar.weekCalendar("formatDate", calEvent.start));
    setupStartAndEndTimeFields(startField, endField, calEvent, $calendar.weekCalendar("getTimeslotTimes", calEvent.start));
}
function eventDrop(calEvent, $event) {
    var dateField = calEvent.start.getFullYear() + "-" + (calEvent.start.getMonth() + 1) + "-" + calEvent.start.getDate();
    var startHours = calEvent.start.getHours();
    var startMinutes = calEvent.start.getMinutes();
    var endHours = calEvent.end.getHours();
    var endMinutes = calEvent.end.getMinutes();
    var startTime = calEvent.start.getHours() + ":" + calEvent.start.getMinutes();
    var endTime = calEvent.end.getHours() + ":" + calEvent.end.getMinutes();
    var AssessArray = (calEvent.assess).split(",");
    if (startTime == "12:0" || startTime == "12:30" || endTime == "12:30" || endTime == "13:0") {
        alert("請選擇正確的時段");
    } else {
        //AspAjax.setAudiometryAppointment(calEvent.id, dateField, (calEvent.start).toString(), (calEvent.end).toString(), parseInt(_uId, 10), parseInt(calEvent.studentid, 10), calEvent.item, calEvent.other, calEvent.content, parseInt(calEvent.state, 10), parseInt(AssessArray[0], 10), parseInt(AssessArray[1], 10), _uUnit);
    }
}
function eventNew(calEvent, $event) {
    var $dialogContent = $("#event_edit_container");
    $dialogContent.find("#author").html(_uName);
    //$dialogContent.find("#studentStatu").html("");
    resetForm($dialogContent);
    var dateField = calEvent.start.getFullYear() + "-" + (calEvent.start.getMonth() + 1) + "-" + calEvent.start.getDate();
    var startField = $dialogContent.find("select[name='start']").val(calEvent.start);
    var endField = $dialogContent.find("select[name='end']").val(calEvent.end);
    var authorField = $dialogContent.find("#author").html();
    var studentField = $dialogContent.find("input[name='title']");
    var sstateField = _StudentStatu.indexOf($("#studentStatu").html(""));
    var itemField = $dialogContent.find("select[name='item']");
    var otherField = $dialogContent.find("input[name='other']");
    var contentField = $dialogContent.find("textarea[name='content']");
    var assessField = $dialogContent.find("select[name='assess']");
    var stateField = $dialogContent.find("select[name='state']");
    $("select[name='item']").find('option').prop('selected', false);
    $("select[name='assess']").find('option').prop('selected', false);
    $("select[name='item']").add("select[name='assess']").trigger('chosen:updated');
    $("#itemOther").hide();

    $dialogContent.dialog({
        modal: false,
        title: "新增課表",
        close: function() {
            $dialogContent.dialog("destroy");
            $dialogContent.hide();
            $("#studentName").html("");

            $('#calendar').weekCalendar("removeUnsavedEvents");
        },
        buttons: {
            save: function() {
                var ItemStr = "";
                var ItemArray = new Array();
                var AssessArray = new Array(0, 0);
                $("select[name='item']").find("option:selected").each(function() {
                    ItemArray.push(this.value);
                });
                $("select[name='assess']").find("option:selected").each(function(i) {
                    AssessArray[i] = this.value;
                });
                ItemStr = ItemArray.toString();
                ItemStr = ItemStr.replace(/\,/g, "＠");

                var startHours = calEvent.start.getHours();
                var startMinutes = calEvent.start.getMinutes();
                var endHours = calEvent.end.getHours();
                var endMinutes = calEvent.end.getMinutes();
                var startTime = calEvent.start.getHours() + ":" + calEvent.start.getMinutes();
                var endTime = calEvent.end.getHours() + ":" + calEvent.end.getMinutes();

                if (startTime == "12:0" || startTime == "12:30" || endTime == "12:30" || endTime == "13:0") {
                    alert("請選擇正確的時段");
                } else if (authorField == "") {
                    alert("預約人員");
                } else {
                    if (ItemStr.indexOf("H") < 0 && sstateField != -1) {
                        calEvent.sstate = _StudentStatu[sstateField];
                        calEvent.title = studentField.val();
                    } else if (ItemStr.indexOf("H") >= 0) {
                        calEvent.title = $("#studentName").val();
                    }
                    calEvent.start = new Date(startField.val());
                    calEvent.end = new Date(endField.val());
                    calEvent.author = authorField;
                    var titleIDField = 0;
                    if (($("#studentName").attr("class")).length > 0) {
                        titleIDField = $("#studentName").attr("class");
                    }
                    calEvent.item = itemField.val();
                    calEvent.other = otherField.val();
                    calEvent.content = contentField.val();
                    calEvent.assess = assessField.val();
                    calEvent.state = stateField.val();
                    var obj = new Object;
                    obj.TeacherID = $('#teacherName').val().substring($('#teacherName').val().indexOf("(")+1,$('#teacherName').val().indexOf(")"));
                    obj.ClassID = $("#ClassNameID").val();
                    obj.appDate = dateField;
                    obj.Date = dateField;
                    obj.startTime = startField.val();
                    obj.endTime = endField.val();
                    var Participants = new Array();
                    $(".participant").each(function() {
                        var data = {};
                        data.StudentID = $(this).attr("id").replace("participant_", "");
                        // var objPeople = ["", $(this).attr("id").replace("participant_", "")];
                        Participants[Participants.length] = data;
                    });
                    obj.TeacherSchuduleStudent = Participants;
                    //obj.Unit = _uUnit;
                    //alert(obj.appDate)
                    // alert(obj.startTime + "---WHO---" + obj.endTime);
                    var id = $('#teacherName').val().substring($('#teacherName').val().indexOf("(")+1,$('#teacherName').val().indexOf(")"));
                    if(id != "")
                    {
                    AspAjax.createTeacherSchudule(obj);
                     $calendar.weekCalendar("removeUnsavedEvents");
                    $calendar.weekCalendar("updateEvent", calEvent);
                    $dialogContent.dialog("close");}
                    else{ alert("請先選好老師");
                }
                   
                }
            },
            cancel: function() {
                $dialogContent.dialog("close");
            }
        }
    }).show();

    $("select[name='item']").chosen({
        display_selected_options: false, //已選項目隱藏
        max_selected_options: 5, //最大選擇數
        no_results_text: "查無資料", //沒有結果匹配
        width: "208px" //寬度
    });
    $("select[name='assess']").chosen({
        display_selected_options: false, //已選項目隱藏
        max_selected_options: 2, //最大選擇數
        no_results_text: "查無資料", //沒有結果匹配
        width: "208px" //寬度
    });
    $("select[name='item']").prop('disabled', false).trigger("chosen:updated");

    $(".chosen-results").css("max-height", "100px");

    $("select[name='item']").change(function(evt, params) {
        if ($(this).find("option:selected").length > 0) {
            if ($(this).find("option[value='H']").attr("selected") != "selected") {
                $(this).children("option[value='H']").attr('disabled', true).trigger("chosen:updated");
            }
        } else {
            $(this).children("option[value='H']").attr('disabled', false).trigger("chosen:updated");
        }

        if ($(this).find("option[value='J']").attr("selected") == "selected") {
            $("#itemOther").fadeIn();
        } else {
            $("#itemOther").fadeOut();
        }
        var maxOptions = 5;
        if ($(this).find("option[value='H']").attr("selected") == "selected") {
            $("#studentName").val("開會");
            $("#studentStatu").html("-1").hide();
            maxOptions = 1;
        } else {
            if ($("#studentName").val() == "開會") {
                $("#studentName").val("");
                $("#studentStatu").html("").show();
            }
        }
        $(this).chosen('destroy').chosen({
            display_selected_options: false, //已選項目隱藏
            max_selected_options: maxOptions, //最大選擇數
            no_results_text: "查無資料", //沒有結果匹配
            width: "208px" //寬度
        });
        $(".chosen-results").css("max-height", "100px");
    });

    $("#studentName").unbind("click").click(function() {
        
    });
    $dialogContent.find(".date_holder").text($calendar.weekCalendar("formatDate", calEvent.start));
    setupStartAndEndTimeFields(startField, endField, calEvent, $calendar.weekCalendar("getTimeslotTimes", calEvent.start));
}
function getStudentNameUI() {
    var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                        '<tr><td width="80">學生姓名 <input type="text" name="name" id="gosrhstudentName" /></td></tr>' +
                        '<tr><td>出生日期 <input class="date" type="text" size="5" id="gosrhbirthdaystart" style="width:100px;" />～' +
                            '<input class="date" type="text" size="5" id="gosrhbirthdayend" style="width:100px;" /></td></tr>' +
                        '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                        '</table><br />' +
                        '<table id="StuinlineReturn" class="tableList" border="0"  width="400">' +
                            '<thead>' +
			                    '<tr>' +
			                        '<th width="110">服務使用者編號	</th>' +
			                        '<th width="100">學生姓名</th>' +
			                        '<th width="80">身分別</th>' +
			                        '<th width="50">性別</th>' +
			                        '<th width="90">出生日期</th>' +
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
                //obj.txtbirthdayend = obj.txtbirthdaystart;
                AspAjax.SearchStudentDataBaseCount1(obj);
            });
        }
    });
}
function ReadyInit() {
    $(".wc-day-column-header").css("line-height", "16px");
    $(".wc-toolbar").css("height", "20px");
    $(".wc-title").css("line-height", "20px");
    $(".wc-time").css("font-size", "12px");
    $(".wc-scrollbar-shim").hide();
    $endTimeField = $("select[name='end']");
    $endTimeOptions = $endTimeField.find("option");
    $timestampsOfOptions = { start: [], end: [] };

    //reduces the end time options to be only after the start time options.
    $("select[name='start']").change(function() {
        var startTime = $timestampsOfOptions.start[$(this).find(":selected").text()];
        var currentEndTime = $endTimeField.find("option:selected").val();
        $endTimeField.html(
            $endTimeOptions.filter(function() {
                return startTime < $timestampsOfOptions.end[$(this).text()];
            })
        );

        var endTimeSelected = false;
        $endTimeField.find("option").each(function() {
            if ($(this).val() === currentEndTime) {
                $(this).attr("selected", "selected");
                endTimeSelected = true;
                return false;
            }
        });

        if (!endTimeSelected) {
            //automatically select an end date 2 slots away.
            $endTimeField.find("option:eq(1)").attr("selected", "selected");
        }

    });
}
function setupStartAndEndTimeFields($startTimeField, $endTimeField, calEvent, timeslotTimes) {

    $startTimeField.empty();
    $endTimeField.empty();

    for (var i = 0; i < timeslotTimes.length; i++) {
        var startTime = timeslotTimes[i].start;
        var endTime = timeslotTimes[i].end;
        var startSelected = "";
        if (startTime.getTime() === calEvent.start.getTime()) {
            startSelected = "selected=\"selected\"";
        }
        var endSelected = "";
        if (endTime.getTime() === calEvent.end.getTime()) {
            endSelected = "selected=\"selected\"";

        }
       // alert(calEvent.end + "---" + endTime);
//        $startTimeField.append("<option value=\"" + startTime.toTimeString().substring(0, 5) + "\" " + startSelected + ">" + timeslotTimes[i].startFormatted + "</option>");
//        $endTimeField.append("<option value=\"" + endTime.toTimeString().substring(0, 5) + "\" " + endSelected + ">" + timeslotTimes[i].endFormatted + "</option>");
        $startTimeField.append("<option value=\"" + startTime + "\" " + startSelected + ">" + timeslotTimes[i].startFormatted + "</option>");
        $endTimeField.append("<option value=\"" + endTime + "\" " + endSelected + ">" + timeslotTimes[i].endFormatted + "</option>");

        $timestampsOfOptions.start[timeslotTimes[i].startFormatted] = startTime.getTime();
        $timestampsOfOptions.end[timeslotTimes[i].endFormatted] = endTime.getTime();

    }
    $endTimeOptions = $endTimeField.find("option");
    $startTimeField.trigger("change");
}
function resetForm($dialogContent) {
    $dialogContent.find("input").val("");
    $dialogContent.find("textarea").val("");
}
function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStudentDataBaseCount1":
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
                        AspAjax.SearchStudentDataBase1(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            } else {
                $("#StuinlineReturn").children("tbody").html("發生錯誤");
            }
            break;
        case "SearchStudentDataBase1":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" >' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + _StudentStatu[result[i].txtstudentStatu] + '</td>' +
			                    '<td>' + _SexList[result[i].txtstudentSex] + '</td>' +
			                    '<td>' + TransformADFromDateFunction(result[i].txtstudentbirthday) + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var id = $(this).children("td:nth-child(1)").html();
                        var val = "(" + id + ")" + $(this).children("td:nth-child(2)").html();
                        var inner = 
                        '<li class="search-choice participant" id="participant_' + id + '">' +
                            '<span>' + val + '</span>' +
                            '<a class="search-choice-close" onclick="deleteMyself(' + id + ')"></a>' +
                        '</li>';
                        $("#studentName").append(inner);
                        //$("#studentStatu").html($(this).children("td:nth-child(3)").html()).show();
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            }
            break;
       case "UpdateTeacherSchudule":
            if (result > 1) {
                alert("修改成功");
                $('#calendar').weekCalendar('today');

               // window.location = 'Teacher_schedule.aspx?id=' + result + '&act=2';
            } else {
                alert("發生錯誤");
            }
            break;
            
        case "createTeacherSchudule":
            if (result > 1) {
                alert("新增成功");
                 $('#calendar').weekCalendar('today');

                //window.location = 'Teacher_schedule.aspx?id=' + result + '&act=2';
            } else {
                alert("發生錯誤");
            }
            break;
        case "delTeacherSchudule":
            if (result >= 0) {
                alert("刪除成功");
                 $('#calendar').weekCalendar('today');

                //window.location = 'Teacher_schedule.aspx?id=' + result + '&act=2';
            } else {
                alert("發生錯誤");
            }
            break;
            
            
    }
}
function addParticipantone() {
    var val = $("#Participant").val();
    var id = val.substring(val.indexOf("(") + 1, val.indexOf(")"));
    if (id != "" && val != "") {

        var inner = '<li class="search-choice participant" id="participant_' + id + '">' +
    '<span>' + val + '</span>' +
    '<a class="search-choice-close" onclick="deleteMyself(' + id + ')"></a>' +
    '</li>';
        $("#ParticipantsList").append(inner);
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function deleteMyself(id) {
   // if (isEdit == 1)
        $("#participant_" + id).remove();
}
/*
var courseArray = new Array("認知語言溝通", "情境活動", "故事奶奶語言互動", "復活節彩蛋活動", "畢業典禮");

$(document).ready(function() {
    $("#main").fadeIn();
    initPage();
    var options = {
        height: 650,
        width: 780,
        navHeight: 25,
        labelHeight: 25,
        firstDayOfWeek: 1,
        navLinks: {
            enableToday: true,
            enableNextYear: false,
            enablePrevYear: false,
            p: '＜上個月',
            n: '下個月＞',
            t: '今天'
        },
        locale: {
            days: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日"],
            daysShort: ["日", "一", "二", "三", "四", "五", "六", "日"],
            months: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"]
        },
        onMonthChanging: function(dateIn) {
            //按上下個月的觸發事件
            return true;
        },
        onEventLinkClick: function(event) {
            alert("event link click");
            getViewClick(event);
            return true;
        },
        onEventBlockClick: function(event) {
            alert("block clicked");
            getViewClick(event);
            return true;
        },
        onEventBlockOver: function(event) {
        alert("onEventBlockOver clicked");
            return true;
        },
        onEventBlockOut: function(event) {
        alert("onEventBlockOut clicked");
            return true;
        },
        onDayLinkClick: function(date) {
        alert("onDayLinkClick clicked");
            alert(date.toLocaleDateString());
            return true;
        },
        onDayCellClick: function(date) {
        getViewClick(date);
        //alert("onDayCellClick clicked");
            //alert(date.toLocaleDateString());
            return true;
        }
    };

    var today = new Date();
    var events = [{ "EventID": 0, "Date": "2013-09-05", "Title": "09:00~11:00認知語言溝通", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明@認知語言溝通@09:00~11:00@E01", "CssClass": "course1" },
				    { "EventID": 1, "Date": "2013-09-11", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 2, "Date": "2013-09-06", "Title": "13:30~15:30故事課", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明、王小明@故事課@13:30~15:30@E01", "CssClass": "course1" },
				    { "EventID": 3, "Date": "2013-09-19", "Title": "09:00~11:00認知語言溝通", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明@認知語言溝通@09:00~11:00@E01", "CssClass": "course1" },
				    { "EventID": 4, "Date": "2013-09-18", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 5, "Date": "2013-09-04", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 6, "Date": "2013-09-25", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" }
    ];
    $.jMonthCalendar.Initialize(options, events);
    $.jMonthCalendar.ChangeMonth(new Date(today.getFullYear(), today.getMonth(), today.getDate()));

    function resetForm($dialogContent) {
        $dialogContent.find("input").val("");
        $dialogContent.find("textarea").val("");
    }
});

function getViewClick(event) {
    var date = new Date(event["Date"]);
    var inner = event["Description"];
    var innerArray = inner.split("@");
    var msg = '<ul>' +
		        '<li>' +
				    '<b>日　　期</b>　' + (date.getFullYear() - 1911) + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日" +
			    '</li>' +
			    '<li>' +
				    '<b>班　　別</b>　' + innerArray[0] +
			    '</li>' +
			    '<li>' +
				    '<b>學生姓名</b>　' + innerArray[1] +
			    '</li>' +
			    '<li>' +
				    '<b>課別名稱</b>　' + innerArray[2] +
			    '</li>' +
			    '<li>' +
				    '<b>上課時間</b>　' + innerArray[3] +
			    '</li>' +
			    '<li>' +
				    '<b>教室名稱</b>　' + innerArray[4] +
			    '</li>' +
	        '</ul>';
    $.fancybox({
        'content': '<div id="inline"><br /><table border="0" width="450"><tr><td width="90">&nbsp;</td><td>' + msg + '</td></tr></table><br /></div>',
        'autoDimensions': true,
        'centerOnScroll': true
    });
}

Date.prototype.Format = function(fmt) { //author: meizz
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小時
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

*/