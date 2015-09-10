
var MyBase = new Base();
var _StudentStatu = new Array("會外人士", "會外人士", "會內生", "會內生", "離會生");

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    AspAjax.getAllStaffDataList([11, 15, 16,21]);
    initPage();

    var $calendar = $('#calendar');
    var id = 10;

    $("#event_edit_container").hide();

    $calendar.weekCalendar({
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
        eventNew: function(calEvent, $event) {
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
                modal: true,
                title: "預約聽檢",
                close: function() {
                    $dialogContent.dialog("destroy");
                    $dialogContent.hide();
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
                            obj.appDate = dateField;
                            obj.startTime = startField.val();
                            obj.endTime = endField.val();
                            obj.authorID = _uId;
                            obj.studentID = titleIDField;
                            obj.item = ItemStr;
                            obj.itemExplain = otherField.val();
                            obj.sContent = contentField.val();
                            obj.State = stateField.val();
                            obj.AssessWho1 = AssessArray[0];
                            obj.AssessWho2 = AssessArray[1];
                            obj.Unit = _uUnit;

                            AspAjax.createAudiometryAppointment(obj);
                            $calendar.weekCalendar("removeUnsavedEvents");
                            $calendar.weekCalendar("updateEvent", calEvent);
                            $dialogContent.dialog("close");
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
                //                if ($(this).find("option:selected").length > 0) {
                //                    if ($(this).find("option[value='H']").attr("selected") != "selected") {
                //                        $(this).children("option[value='H']").attr('disabled', true).trigger("chosen:updated");
                //                    }
                //                } else {
                //                    $(this).children("option[value='H']").attr('disabled', false).trigger("chosen:updated");
                //                }

                if ($(this).find("option[value='J']").attr("selected") == "selected") {
                    $("#itemOther").fadeIn();
                } else {
                    $("#itemOther").fadeOut();
                }
                var maxOptions = 5;
                if ($(this).find("option[value='H']").attr("selected") == "selected") {
                    //$("#studentName").val("開會");
                    //$("#studentStatu").html("-1").hide();
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
            });
            $dialogContent.find(".date_holder").text($calendar.weekCalendar("formatDate", calEvent.start));
            setupStartAndEndTimeFields(startField, endField, calEvent, $calendar.weekCalendar("getTimeslotTimes", calEvent.start));

        },
        eventDrop: function(calEvent, $event) {
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
        },
        eventResize: function(calEvent, $event) {
        },
        eventClick: function(calEvent, $event) {
            /*if (calEvent.readOnly) {
            return;
            }*/
            var $dialogContent = $("#event_edit_container");
            $("#studentName").attr("class", calEvent.studentid);
            $dialogContent.find("select[name='state']").find("option[value='" + calEvent.state + "']").attr('selected', true);
            //resetForm($dialogContent);
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

            $dialogContent.dialog({
                modal: true,
                title: "編輯 - " + calEvent.title,
                close: function() {
                    $dialogContent.dialog("destroy");
                    $dialogContent.hide();
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
                            calEvent.item = itemField.val();
                            calEvent.other = otherField.val();
                            calEvent.content = contentField.val();
                            calEvent.assess = assessField.val();
                            calEvent.state = stateField.val();
                            var titleIDField = $("#studentName").attr("class");

                            var obj = new Object;
                            obj.ID = calEvent.id;
                            obj.appDate = dateField;
                            obj.startTime = startField.val();
                            obj.endTime = endField.val();
                            obj.authorID = _uId;
                            obj.studentID = titleIDField;
                            obj.item = ItemStr;
                            obj.itemExplain = otherField.val();
                            obj.sContent = contentField.val();
                            obj.State = stateField.val();
                            obj.AssessWho1 = AssessArray[0];
                            obj.AssessWho2 = AssessArray[1];
                            obj.Unit = _uUnit;


                            AspAjax.setAudiometryAppointment(obj);
                            $calendar.weekCalendar("updateEvent", calEvent);
                            $dialogContent.dialog("close");
                        }
                    },
                    "delete": function() {
                        AspAjax.delAudiometryAppointment(calEvent.id);
                        $calendar.weekCalendar("removeEvent", calEvent.id);
                        $dialogContent.dialog("close");
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

            $(".chosen-results").css("max-height", "100px");

            $("select[name='item']").change(function(evt, params) {
                if ($("select[name='item']").find("option[value='J']").attr("selected") == "selected") {
                    $("#itemOther").fadeIn();
                } else {
                    $("#itemOther").fadeOut();
                    $("input[type='other']").val("")
                }
            });

            var itemArray = (calEvent.item).split(",");
            for (var i = 0; i < itemArray.length; i++) {
                $("select[name='item']").find("option[value='" + itemArray[i] + "']").attr("selected", true);
                if (itemArray[i] == "J") {
                    $("#itemOther").show();
                    $("select[name='item']").prop('disabled', false);
                }
                if (itemArray[i] == "H") {
                    $("select[name='item']").prop('disabled', true);
                    $("select[name='item']").children("option[value='H']").attr('disabled', true);
                }
            }

            var assessArray = (calEvent.assess).split(",");
            for (var i = 0; i < assessArray.length; i++) {
                $("select[name='assess']").find("option[value='" + assessArray[i] + "']").attr("selected", true);
            }
            $("select[name='item']").add("select[name='assess']").trigger('chosen:updated');

            var startField = $dialogContent.find("select[name='start']").val(calEvent.start);
            var endField = $dialogContent.find("select[name='end']").val(calEvent.end);
            $dialogContent.find(".date_holder").text($calendar.weekCalendar("formatDate", calEvent.start));
            setupStartAndEndTimeFields(startField, endField, calEvent, $calendar.weekCalendar("getTimeslotTimes", calEvent.start));
            $(window).resize().resize(); //fixes a bug in modal overlay size ??

        },
        eventMouseover: function(calEvent, $event) {
        },
        eventMouseout: function(calEvent, $event) {
        },
        noEvents: function() {
        },
        data: function(start, end, callback) {
            $.ajax({
                type: 'POST',
                url: './AudiometryAppointment.ashx',
                data: { "sDate": TransformDBStringFunction(start), "eDate": TransformDBStringFunction(end), "sUnit": _uUnit, "type": "getEventData" },
                dataType: 'json',
                async: false,
                contentType: 'application/x-www-form-urlencoded;charset=UTF-8',
                success: function(txt) {
                    if (txt.length > 0) {
                        var innerArray = new Array();
                        for (var i = 0; i < txt.length; i++) {
                            var item = (txt[i]["aItem"]).replace(/\＠/g, ",");
                            var titleStr = txt[i]["aStudentName"];
                            var readonly = false;
                            if (titleStr == 0) {
                                if (txt[i]["aContent"] != "" ) {
                                    titleStr = txt[i]["aContent"];
                                } else {
                                    titleStr = "開會";
                                }
                                //titleStr = "開會";
                                readonly = true;
                            }
                            var sstate = txt[i]["aStudentState"];
                            if (sstate == "") {
                                sstate = -1;
                            }
                            innerArray.push({ "id": txt[i]["aID"], "start": txt[i]["aStartTime"], "end": txt[i]["aEndTime"], "author": txt[i]["aPeopleName"], "title": titleStr, "studentid": txt[i]["aStudentID"], "sstate": parseInt(sstate, 10), "item": item, "other": txt[i]["aItemExplain"], "content": txt[i]["aContent"], "assess": txt[i]["aAssess1"] + "," + txt[i]["aAssess2"], "state": parseInt(txt[i]["aState"], 10), readOnly: readonly });
                        }
                        var innerJson = { events: innerArray };
                        callback(innerJson);
                    }
                }, error: function(txt) {
                    alert("發生錯誤，請重新整理頁面");
                }
            });
        }
    });
    
    $(".wc-day-column-header").css("line-height", "16px");
    $(".wc-toolbar").css("height", "20px");
    $(".wc-title").css("line-height", "20px");
    $(".wc-time").css("font-size", "12px");
    $(".wc-scrollbar-shim").hide();

    function resetForm($dialogContent) {
        $dialogContent.find("input").val("");
        $dialogContent.find("textarea").val("");
    }

    /*
    * Sets up the start and end time fields in the calendar event
    * form for editing based on the calendar event being edited
    */
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
            $startTimeField.append("<option value=\"" + startTime + "\" " + startSelected + ">" + timeslotTimes[i].startFormatted + "</option>");
            $endTimeField.append("<option value=\"" + endTime + "\" " + endSelected + ">" + timeslotTimes[i].endFormatted + "</option>");

            $timestampsOfOptions.start[timeslotTimes[i].startFormatted] = startTime.getTime();
            $timestampsOfOptions.end[timeslotTimes[i].endFormatted] = endTime.getTime();

        }
        $endTimeOptions = $endTimeField.find("option");
        $startTimeField.trigger("change");
    }

    var $endTimeField = $("select[name='end']");
    var $endTimeOptions = $endTimeField.find("option");
    var $timestampsOfOptions = { start: [], end: [] };

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
  
});

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
                        $("#studentName").val($(this).children("td:nth-child(2)").html()).attr("class", $(this).children("td:nth-child(1)").html());
                        $("#studentStatu").html($(this).children("td:nth-child(3)").html()).show();
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            }
            break;
        case "createAudiometryAppointment":
        case "delAudiometryAppointment":
        case "setAudiometryAppointment":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                //alert("新增成功");
                window.location.reload();
            }
            break;
       
        case "getAllStaffDataList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    for (var i = 0; i < result.length; i++) {
                        $("select[name='assess']").append($('<option></option>').attr("value", result[i].sID).text(result[i].sName + "(" + result[i].sID + ")"));
                    }
                    $("select[name='assess']").trigger('chosen:updated');
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            }
            $('#calendar').weekCalendar('today');
            break;
    }
}

function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

