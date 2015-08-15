var courseArray = new Array("認知語言溝通", "情境活動", "故事奶奶語言互動", "復活節彩蛋活動", "畢業典禮");
var eventID = 10;
var MyBase = new Base();
var noEmptyItem = ["studentID","studentName", "startPlanDate", "endPlanDate"];
var noEmptyShow = ["學生ID抓取錯誤，請重新選擇學生", "學生姓名", "服務計畫期程-起", "服務計畫期程-迄"];
var _ColumnID = 0;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    $("#item1").add("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#item1Content").fadeIn();

    $(".menuTabs").click(function() {
        $(".menuTabs").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("input").add("select").add("textarea").attr("disabled", true);
        if (id == null && act == 1) {
            $("input").add("select").add("textarea").attr("disabled", false);
        } else {
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
        }
        getStudentData(this.id);

        $(".way").dropdownchecklist({ width: 60 });
        $(".ui-dropdownchecklist").css({ "margin": "4px 0", "height": "auto" });
        $(".ui-dropdownchecklist-dropcontainer").css({ "height": "auto" });
        $(".ui-dropdownchecklist-text").css({ "font-size": "13px", "line-height": "20px" });
    });

    $(".showUploadImg").fancybox();
    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").add(".btnRevise").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").add(".btnRevise").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './teach_isp.aspx';
        }
    }

    $("#PlanWriterName").unbind("click").click(function() { callTeacherSearchfunction("1"); });
    $("#PlanReviseName").unbind("click").click(function() { callTeacherSearchfunction("2"); });

    $("#PlanWriter2Name").unbind("click").click(function() { callTeacherSearchfunction("10"); });
    $("#PlanRevise2Name").unbind("click").click(function() { callTeacherSearchfunction("11"); });
    $("#AudiometryAssessmentByName").unbind("click").click(function() { callTeacherSearchfunction("12"); });
    $("#HearingAssessmentBy1Name").unbind("click").click(function() { callTeacherSearchfunction("13"); });
    $("#AidsAssessmentByName").unbind("click").click(function() { callTeacherSearchfunction("14"); });
    
    $("#PlanWriter3Name").unbind("click").click(function() { callTeacherSearchfunction("3"); });
    $("#PlanRevise3Name").unbind("click").click(function() { callTeacherSearchfunction("4"); });
    $("#HearingAssessmentByName").unbind("click").click(function() { callTeacherSearchfunction("5"); });
    $("#VocabularyAssessmentByName").unbind("click").click(function() { callTeacherSearchfunction("6"); });
    $("#LanguageAssessmentByName").unbind("click").click(function() { callTeacherSearchfunction("7"); });
    $("#intelligenceAssessmentByName").unbind("click").click(function() { callTeacherSearchfunction("8"); });
    $("#OtherAssessmentByName").unbind("click").click(function() { callTeacherSearchfunction("9"); });

    
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
            //alert("event link click");
            getViewClick(event);
            return true;
        },
        onEventBlockClick: function(event) {
            //alert("block clicked");
            getViewClick(event);
            return true;
        },
        onEventBlockOver: function(event) {
            return true;
        },
        onEventBlockOut: function(event) {
            return true;
        },
        onDayLinkClick: function(date) {
            alert(date.toLocaleDateString());
            return true;
        },
        onDayCellClick: function(date) {
            //alert(date.toLocaleDateString());
            var msg = '<ul>' +
		        '<li>' +
				    '<b>日　　期</b>　' + (date.getFullYear() - 1911) + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日" +
			    '</li>' +
			    '<li>' +
				    '<b>班　　別</b>　<select><option value="0">請選擇班別</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>課別名稱</b>　<select><option value="0">請選擇課別名稱</option><option value="1">個別課</option><option value="2">認知語言溝通</option><option value="3">故事課</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>教師姓名</b>　<select><option value="0">請選擇</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>上課時間</b>　<select><option value="0">請選擇</option></select>～<select><option value="0">請選擇</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>教室名稱</b>　<select><option value="0">請選擇教室</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>狀　　態</b>　<select id="CourseState"><option value="0">正常</option><option value="1">調課</option></select>' +
				    '<div id="SwitchClass" style="display:none;">' +
				    '<font color="red"><b>調課上課日期</b></font>　<input class="date" type="text" value="" /><br />' +
				    '<font color="red"><b>調課上課時間</b></font>　<select><option value="0">請選擇</option></select>～<select><option value="0">請選擇</option></select><br />' +
				    '<font color="red"><b>調課教室名稱</b></font>　<select><option value="0">請選擇教室</option></select>' +
				    '</div>' +
			    '</li>' +
	        '</ul>';
            var buttonStr = '<button id="btnAdd" class="btnAdd2">新 增</button>　' +
                            '<button class="btnCancel" onclick="cancelEvent(0)">取 消</button>';
            $.fancybox({
                'content': '<div id="inline"><br /><table border="0" width="450"><tr><td width="90">&nbsp;</td><td>' + msg + '</td></tr><tr><td colspan="2" height="50" valign="bottom" align="center">' + buttonStr + '</td></tr></table><br /></div>',
                'autoDimensions': true,
                'centerOnScroll': true,
                'onComplete': function() {
                    $("#inline").find("button").fadeIn();
                    $("#btnAdd").one("click", function() {
                        //if ($("select[name='item'] :selected").val() != "-") {
                        var dateStr = (new Date(date)).Format("yyyy-MM-dd");
                        var courseInt = 1;
                        $.jMonthCalendar.AddEvents({ "EventID": eventID, "Date": dateStr, "Title": "09:00~10:00" + courseArray[courseInt], "URL": "#", "Description": "描述", "CssClass": "course" + courseInt });
                        eventID++;
                        $.fancybox.close();
                        /*} else {
                        alert("請選擇課程項目");
                        }*/
                    });
                    $('#inline input[type="text"]').css({ "margin": "0", "padding": "0" });
                    $("#CourseState").change(function() {
                        if ($(this).find(":selected").val() == "1") {
                            $("#SwitchClass").fadeIn();
                        } else {
                            $("#SwitchClass").fadeOut();
                        }
                    });
                }
            });
            return true;
        }
    };

    var today = new Date();
    var events = [{ "EventID": 0, "Date": "2013-09-05", "Title": "09:00~11:00認知語言溝通", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明@認知語言溝通@09:00~11:00@E01", "CssClass": "course1" },
				    { "EventID": 1, "Date": "2013-09-11", "Title": "09:00~10:00個別課", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 2, "Date": "2013-09-06", "Title": "13:30~15:30故事課", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明、王小明@故事課@13:30~15:30@E01", "CssClass": "course1" },
				    { "EventID": 3, "Date": "2013-09-19", "Title": "09:00~11:00認知語言溝通", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明@認知語言溝通@09:00~11:00@E01", "CssClass": "course1" },
				    { "EventID": 4, "Date": "2013-09-18", "Title": "09:00~10:00個別課", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 5, "Date": "2013-09-04", "Title": "09:00~10:00個別課", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 6, "Date": "2013-09-25", "Title": "09:00~10:00個別課", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" }
    ];
    $.jMonthCalendar.Initialize(options, events);
    $.jMonthCalendar.ChangeMonth(new Date(today.getFullYear(), today.getMonth(), today.getDate()));
    
    
//    var list = '<tr><td>1</td></tr>';
//    $("#Page2tableContact tbody tr:eq(4)").after(list);
//    增行 by who

    function resetForm($dialogContent) {
        $dialogContent.find("input").val("");
        $dialogContent.find("textarea").val("");
    }

    $("#studentName").unbind("click").click(function() {
        callStudentSearchfunction();
    });
});

function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "setStudentDataBase":
            if (result == 1) {
                alert("新增成功");
            } else {
                alert("發生錯誤");
            }
            break;
        case "SearchTeachISPDateCount":
            var obj = MyBase.getTextValueBase("searchTable");
            var pageCount = parseInt(result);
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
                    AspAjax.SearchTeachISPData(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("查無資料");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("發生錯誤");
            }
            break;
        case "SearchTeachISPData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].txtstudentID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        var AgeList = BirthdayFromDateFunction(result[i].txtstudentbirthday);
                        inner += '<tr>' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + TransformADFromDateFunction(result[i].txtstudentbirthday) + '</td>' +
			                    '<td>' + AgeList[0] + '歲' + AgeList[1] + '個月</td>' +
			                    '<td>' + result[i].txtLegalRepresentative + '</td>' +
			                    '<td>' + result[i].txtLegalRepresentativePhone + '</td>' +
			                    '<td>' + result[i].txtLegalRepresentativeTel + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].txtstudentName);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("查無資料");
            }
            break;
        case "SearchStudentDataBaseCount":
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
                        AspAjax.SearchStudentDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#searchStuinline .tableList").children("tbody").html("查無資料");
            } else {
                $("#searchStuinline .tableList").children("tbody").html("發生錯誤");
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
			                    '<td>' + _SexList[result[i].txtstudentSex] + '</td>' +
			                    '<td><span style="display:none;">' + result[i].ID + '</span>' + TransformADFromDateFunction(result[i].txtstudentbirthday) + '</td>' +
			                '</tr>';
                    }
                    $("#StuinlineReturn tbody").html(inner);

                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var id = $(this).find("span").html();
                        /*$("#studentID").html(id);
                        var Name = $(this).children("td:nth-child(2)").html();
                        $("#studentName").val(Name);*/
                        AspAjax.getStudentDataBase(parseInt(id));
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("查無資料");
            }
            break;
        case "getStudentDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.StudentData.studentID != -1) {
                    PushPageValue(result.StudentData);
                   // alert(result.StudentData.studentID);
                    $("#studentCID").html(result.Column);
                    $("#studentID").html(result.StudentData.studentID);
                    $("#LegalrepresentativeName").val(result.StudentData.fPName2);
                    $("#LegalrepresentativePhone").val(result.StudentData.fPTel2);
                    $("#LegalrepresentativePhoneHome").val(result.StudentData.fPHPhone2);
                    $("#LegalrepresentativePhoneMobile").val(result.StudentData.fPPhone2);
                    $("#LegalrepresentativePhoneFax").val(result.StudentData.fPFax2);
                    $("#caseUnit").html(_UnitList[result.StudentData.sUnit]);
                    PushPageValue(result.HearingData);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.StudentData.txtstudentName);
                }
            } else {
                alert("查無資料");
            }
            break;
        case "createCaseISPData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./teach_isp.aspx?id=" + result[1] + "&act=2";
                }
            }
            break;
        case "getTeachISPDate":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result.ISP1Data.studentID) > 0) {
                    PushPageValue(result.ISP1Data);
                    $("#PlanDate").add("#HE_ProjectDate").add("#EP_Plan_Date").unbind().removeClass();
                    AspAjax.getStudentDataBase(result.ISP1Data.studentID);

                } else {
                    if (result.ISP1Data.studentID == null) {
                        alert("查無資料");
                        window.location.href = "./teach_isp.aspx";
                    } else {
                        alert("發生錯誤，錯誤訊息如下：" + result.ISP1Data.studentName);
                    }
                }
            } else {
                alert("查無資料");
            }
            break;
        case "GetHomeService":
            if (!(result == null || result.length == 0 || result == undefined)) {
                PushPageValue(result[0]);
                for (var i = 0; i < result.length; i++) {
                    $("#" + result[i].MasterOrder + "_" + result[i].DetailOrder + "_Target").val(result[i].Target)
                    $("#" + result[i].MasterOrder + "_" + result[i].DetailOrder + "_StartDate").val(result[i].StartDate)
                    $("#" + result[i].MasterOrder + "_" + result[i].DetailOrder + "_EndDate").val(result[i].EndDate)
                    $("#" + result[i].MasterOrder + "_" + result[i].DetailOrder + "_Executor").val(result[i].Executor)
                    $("#" + result[i].MasterOrder + "_" + result[i].DetailOrder + "_TrackDate").val(result[i].TrackDate)
                    $("#" + result[i].MasterOrder + "_" + result[i].DetailOrder + "_Manner").val(result[i].Manner)
                    $("#" + result[i].MasterOrder + "_" + result[i].DetailOrder + "_Results").val(result[i].Results)
                    // alert(result[i].MasterOrder + "--" + result[i].DetailOrder);
                }

                //$("#PlanDate").add("#HE_ProjectDate").add("#EP_Plan_Date").unbind().removeClass()
            }
            break;
        case "GetTeachISPPage4Count":
            if (result[0] > 0 || result[1] > 0) {
                while ($("#table1>tbody>tr").length > 1) {
                    $("#table1>tbody>tr:last-child").detach();
                }
                while ($("#table2>tbody>tr").length > 1) {
                    $("#table2>tbody>tr:last-child").detach();
                }
                for (var i = 2; i <= result[0]; i++) {
                
                
                    getAdd(1);
                }
                for (var i = 2; i <= result[1]; i++) {
                    getAdd(2);
                }
            }
            var id = GetQueryString("id");
            AspAjax.GetTeachISPPage4(id);

            break;
        case "GetTeachISPPage3":
            if (!(result == null || result.length == 0 || result == undefined)) {
                PushPageValue(result);
                $("#HearingAssessmentBy1").html(result.HearingAssessmentBy);
                $("#HearingAssessmentBy1Name").val(result.HearingAssessmentByName);
                for (var i = 0; i < result.HearingManagerDetail.length; i++) {
                    $("#" + result.HearingManagerDetail[i].PlanOrder + "_" + result.HearingManagerDetail[i].DetailOrder + "HMAMode").val(result.HearingManagerDetail[i].HMAMode);
                    $("#" + result.HearingManagerDetail[i].PlanOrder + "_" + result.HearingManagerDetail[i].DetailOrder + "HMAResult").val(result.HearingManagerDetail[i].HMAResult);
                    $("#" + result.HearingManagerDetail[i].PlanOrder + "_" + result.HearingManagerDetail[i].DetailOrder + "HMTeachingDecision").val(result.HearingManagerDetail[i].HMTeachingDecision);
                    $("#" + result.HearingManagerDetail[i].PlanOrder + "_" + result.HearingManagerDetail[i].DetailOrder + "HMDateStart").val(TransformADFromStringFunction(result.HearingManagerDetail[i].HMDateStart));
                    $("#" + result.HearingManagerDetail[i].PlanOrder + "_" + result.HearingManagerDetail[i].DetailOrder + "HMDateEnd").val(TransformADFromStringFunction(result.HearingManagerDetail[i].HMDateEnd));
                    $("#" + result.HearingManagerDetail[i].PlanOrder + "_" + result.HearingManagerDetail[i].DetailOrder + "HMADate").val(TransformADFromStringFunction(result.HearingManagerDetail[i].HMADate));

                }
            }
            break;
        case "GetTeachISPPage4":
            if (!(result == null || result.length == 0 || result == undefined)) {
                PushPageValue(result);
                for (var i = 0; i < result.TeachingPlan.length; i++) {
                    $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "TargetLong").val(result.TeachingPlan[i].TargetLong);
                    for (var j = 0; j < result.TeachingPlan[i].TeachingPlanDetail.length; j++) {
                        //alert(result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "---" + i + "----"+j);
                        $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "_" + result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "Target").val(result.TeachingPlan[i].TeachingPlanDetail[j].TargetShort);
                        $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "_" + result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "DateStart").val(TransformADFromStringFunction(result.TeachingPlan[i].TeachingPlanDetail[j].DateStart));
                        $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "_" + result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "DateEnd").val(TransformADFromStringFunction(result.TeachingPlan[i].TeachingPlanDetail[j].DateEnd));
                        $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "_" + result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "EffectiveDate").val(TransformADFromStringFunction(result.TeachingPlan[i].TeachingPlanDetail[j].EffectiveDate));
                        $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "_" + result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "EffectiveMode").val(result.TeachingPlan[i].TeachingPlanDetail[j].EffectiveMode);
                        $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "_" + result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "EffectiveResult").val(result.TeachingPlan[i].TeachingPlanDetail[j].EffectiveResult);
                        $("#" + result.TeachingPlan[i].TeachOrder + "_" + result.TeachingPlan[i].MasterOrder + "_" + result.TeachingPlan[i].TeachingPlanDetail[j].DetailOrder + "Decide").val(result.TeachingPlan[i].TeachingPlanDetail[j].Decide);
                    }
                }
            }
            break;
        case "setTeachISPDate1":
            if (result <= 0) {
                alert("發生錯誤，請重新整理頁面");
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "SearchStaffDataBaseCountCase":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#smainPagination2").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    //ellipse_text: "/",
                    //link_to: ,
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTeacherline");
                        AspAjax.SearchStaffDataBaseCase(parseInt((index + 1) * _LimitPage, 10), obj, parseInt(result[2]));
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#searchTeacherline .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#searchTeacherline .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBaseCase":

            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].sID) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" >' +
                                '<td>' + result[i].sID + '<span class="staffID" style="display:none;">' + result[i].sID + '</span></td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td>' + result[i].sName + '<span class="staffName" style="display:none;">' + result[i].sName + '</span></td>' +
			                    '<td>' + _JobItemList[result[i].sJob] + '<span class="staffjob" style="display:none;">' + result[i].pw + '</span></td>' +
                        //			                    '<td>' + TransformADFromStringFunction(result[i].officeDate) + '</td>' +
                        //			                    '<td>' + TransformADFromStringFunction(result[i].resignDate) + '</td>' +
			                '</tr>';
                    }
                    //
                    $("#StuinlineReturn2 tbody").html(inner);

                    $("#StuinlineReturn2 .ImgJs").unbind('click').click(function() {
                        var id = $(this).find(".staffID").html();
                        var name = $(this).find(".staffName").html();
                        switch ($(this).find(".staffjob").html()) {
                            case "1":
                                $("#PlanWriter").html(id);
                                $("#PlanWriterName").val(name);
                                break;
                            case "2":
                                $("#PlanRevise").html(id);
                                $("#PlanReviseName").val(name);
                                break;
                            case "3":
                                $("#PlanWriter3").html(id);
                                $("#PlanWriter3Name").val(name);
                                break;
                            case "4":
                                $("#PlanRevise3").html(id);
                                $("#PlanRevise3Name").val(name);
                                break;
                            case "5":
                                $("#HearingAssessmentBy").html(id);
                                $("#HearingAssessmentByName").val(name);
                                break;
                            case "6":
                                $("#VocabularyAssessmentBy").html(id);
                                $("#VocabularyAssessmentByName").val(name);
                                break;
                            case "7":
                                $("#LanguageAssessmentBy").html(id);
                                $("#LanguageAssessmentByName").val(name);
                                break;
                            case "8":
                                $("#intelligenceAssessmentBy").html(id);
                                $("#intelligenceAssessmentByName").val(name);
                                break;
                            case "9":
                                $("#OtherAssessmentBy").html(id);
                                $("#OtherAssessmentByName").val(name);
                                break;
                            case "10":
                                $("#PlanWriter2").html(id);
                                $("#PlanWriter2Name").val(name);
                                break;
                            case "11":
                                $("#PlanRevise2").html(id);
                                $("#PlanRevise2Name").val(name);
                                break;
                            case "12":
                                $("#AudiometryAssessmentBy").html(id);
                                $("#AudiometryAssessmentByName").val(name);
                                break;
                            case "13":
                                $("#HearingAssessmentBy1").html(id);
                                $("#HearingAssessmentBy1Name").val(name);
                                break;
                            case "14":
                                $("#AidsAssessmentBy").html(id);
                                $("#AidsAssessmentByName").val(name);
                                break;


                        }
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#StuinlineReturn2 .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "GetWriteName":
            $("#PlanReviseName").val(result[1]);
            $("#PlanWriterName").val(result[1]);
            $("#PlanWriter").html(result[0]);
            $("#PlanRevise").html(result[0]);

            $("#PlanWriter2Name").val(result[1]);
            $("#PlanWriter2").html(result[0])

            $("#PlanRevise2Name").val(result[1]);
            $("#PlanRevise2").html(result[0])
            
            $("#PlanWriter3Name").val(result[1]);
            $("#PlanWriter3").html(result[0])

            $("#PlanRevise3Name").val(result[1]);
            $("#PlanRevise3").html(result[0])

            // alert(result[1]);
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function getView(id) {
    window.open("./teach_isp.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
    _ColumnID = id;
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.GetWriteName();
        AspAjax.getTeachISPDate(id);
        AspAjax.GetHomeService(id);
        AspAjax.GetTeachISPPage3(id);
        AspAjax.GetTeachISPPage4Count(id);
        //AspAjax.GetTeachISPPage4(id);

    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#item2").add("#item3").add("#item4").hide();
    }
}

function saveEvent(id) {
    $.fancybox.close();
}

function cancelEvent(id) {
    $.fancybox.close();
}

function getViewClick(event) {
    var date = new Date(event["Date"]);
    var inner = event["Description"];
    var innerArray = inner.split("@");
    var msg = '<ul>' +
		        '<li>' +
				    '<b>日　　期</b>　' + (date.getFullYear() - 1911) + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日" +
			    '</li>' +
			    '<li>' +
				    '<b>班　　別</b>　<select><option value="0">請選擇</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>課別名稱</b>　<select><option value="0">請選擇</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>教師姓名</b>　<select><option value="0">請選擇</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>上課時間</b>　<select><option value="0">請選擇</option></select>～<select><option value="0">請選擇</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>教室名稱</b>　<select><option value="0">請選擇</option></select>' +
			    '</li>' +
			    '<li>' +
				    '<b>狀　　態</b>　<select id="CourseState"><option value="0">正常</option><option value="1">調課</option></select>' +
				    '<div id="SwitchClass" style="display:none;">' +
				    '<font color="red"><b>調課上課日期</b></font>　<input class="date" type="text" value="" /><br />' +
				    '<font color="red"><b>調課上課時間</b></font>　<select><option value="0">請選擇</option></select>～<select><option value="0">請選擇</option></select><br />' +
				    '<font color="red"><b>調課教室名稱</b></font>　<select><option value="0">請選擇教室</option></select>' +
				    '</div>' +
			    '</li>' +
	        '</ul>';
    var buttonStr = '<button id="btnDel" class="btnDel">刪 除</button>　' +
	        '<button id="btnSave" class="btnSave" onclick="saveEvent(0)">儲 存</button>　' +
	        '<button class="btnCancel" onclick="cancelEvent(0)">取 消</button>';
    $.fancybox({
        'content': '<div id="inline"><br /><table border="0" width="450"><tr><td width="90">&nbsp;</td><td>' + msg + '</td></tr><tr><td colspan="2" height="50" valign="bottom" align="center">' + buttonStr + '</td></tr></table><br /></div>',
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $("#inline").find("button").fadeIn();
            $("#btnDel").one("click", function() {
                $.fancybox.close();
            });

            $('#inline input[type="text"]').css({ "margin": "0", "padding": "0" });
            $("#CourseState").change(function() {
                if ($(this).find(":selected").val() == "1") {
                    $("#SwitchClass").fadeIn();
                } else {
                    $("#SwitchClass").fadeOut();
                }
            });
        }
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

function getAdd(tid) {
   
    var List = '';
    List += ' <tr id="' +tid+'_' + ($("#table" + tid + ">tbody>tr").length + 1 ) + 'dataTR"><td colspan="7"><table class="tableContact3" width="974" border="0"><tbody><tr><td  colspan="7">' +
            '長期目標：<textarea id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'TargetLong" class="long" cols="100" rows="2" ></textarea></td></tr>  ';
    for (var i = 1; i < 6; i++) {
        List += '<tr><td width="383">短期目標：<textarea id ="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + '_' + i + 'Target" class="short" cols="100" rows="2"></textarea></td>' +
               '<td> <input id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + '_' + i + 'DateStart" class="date" type="text" value="" size="10" /></td>' +
              '<td> <input id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + '_' + i + 'DateEnd" class="date" type="text" value="" size="10" /></td>' +
              '<td> <input id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + '_' + i + 'EffectiveDate" class="date" type="text" value="" size="10" /></td>' +
              '<td> <select id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + '_' + i + 'EffectiveMode"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option</select></td>' +
              '<td> <select id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + '_' + i + 'EffectiveResult"><option value="0"></option><option value="3">完全達成</option><option value="2">部分達成</option><option value="1">未達成</option></select></td>' +
              '<td> <select id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + '_' + i + 'Decide"><option value="0"></option><option value="3">通過目標</option><option value="2">繼續目標</option><option value="1">修正目標</option></select></select></td>' +
               '</tr>';
    }
    List += '</tbody></table></td></tr>';
    $("#table" + tid + ">tbody>tr:last-child").after(List);


    //$("#table" + tid + ">tbody>tr:last-child").after($("#dataTR" + tid).clone().attr("id", "dataTR" + tid + ($("#table" + tid + ">tbody>tr").length + 1)));   
    //$("#dataTR" + tid + ($("#table" + tid + ">tbody>tr").length + 1)).val("");
    //var inner = '<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + 1 + '" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>';

    //$("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length).find(".wayTD").html(inner);
//    var inner = '';
//    inner = '" class="way" multiple="multiple"><option value="a">A</option><option value="b">B</option><option value="c">C</option><option value="d">D</option></select>';
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length + " tr:nth-child(2)").find(".wayTD").html('<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + 1 + inner);
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length + " tr:nth-child(3)").find(".wayTD").html('<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + 2 + inner);
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length + " tr:nth-child(4)").find(".wayTD").html('<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + 3 + inner);
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length + " tr:nth-child(5)").find(".wayTD").html('<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + 4 + inner);
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length + " tr:nth-child(6)").find(".wayTD").html('<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + 5 + inner);
//    
//    
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length).find(".way").dropdownchecklist({ width: 60 });
//    $(".ui-dropdownchecklist").css({ "margin": "4px 0", "height": "auto" });
//    $(".ui-dropdownchecklist-dropcontainer").css({ "height": "auto" });
//    $(".ui-dropdownchecklist-text").css({ "font-size": "13px", "line-height": "20px" });
}

function getSubtract(tid) {
    if ($("#table" + tid + ">tbody>tr").length > 1) {
        $("#table" + tid + ">tbody>tr:last-child").detach();
    }
}
function creact() {
    var obj = MyBase.getTextValueBase("item1Content");
    var aa = 0;
    //AspAjax.createTeachISPDate(obj);
}
function callStudentSearchfunction() {
    var inner = '<div id="inline"><br /><table id="searchStuinline" border="0" width="400">' +
                    '<tr><td width="80">服務使用者編號 <input type="text" name="name" id="gosrhstudentID"/></td></tr>' +
                    '<tr><td>學生姓名 <input type="text" name="name" id="gosrhstudentName"/></td></tr>' +
                    '<tr><td>出生日期 <input class="date" type="text" name="name" size="10" id="gosrhbirthdaystart"/>～' +
                            '<input class="date" type="text" name="name" size="10" id="gosrhbirthdayend"/></td></tr>' +
                    '<tr><td>入會日期 <input class="date" type="text" name="name" size="10" id="gosrhjoindaystart"/>～' +
			                '<input class="date" type="text" name="name" size="10" id="gosrhjoindayend" /></td><tr>' +

                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn" class="tableList" border="0"  width="400">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="100">服務使用者編號</th>' +
			                    '<th width="130">學生姓名</th>' +
			                    '<th width="70">性別</th>' +
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
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchStuinline");
                AspAjax.SearchStudentDataBaseCount(obj);
            });
        }
    });
}
function SearchstudentISP() {
    var obj = MyBase.getTextValueBase("searchTable");
    AspAjax.SearchTeachISPDateCount(obj);
}


function callTeacherSearchfunction(getid) {//老師姓名點取跳出

    var inner = '<div id="inline2"><br /><table id="searchTeacherline" border="0" width="400">' +
			            '<tr><td width="80">教師姓名 <input type="text" id="gosrhstaffName" value="" /></td></tr>' +
			            '<tr><td >性　　別 <select id="gosrhstaffSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td></tr>' +
                        '<tr><td >出生日期 <input class="date" type="text" id="gosrhstaffBirthdayStart" size="10" />～<input class="date" type="text" id="gosrhstaffBirthdayEnd" size="10" /></td></tr>' +
			            '<tr><td>教師編號 <input type="text" id="gosrhstaffID" value=""/>';
    // if (getid == 1 || getid == 2) { inner += '<select id="gosrhstaffJob" style="display:none;" ><option value="14">教師</option></select>'; }
    inner += '</td></tr><tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn2" class="tableList" border="0"  width="400">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="100">員工編號</th>' +
			                    '<th width="130">派任單位</th>' +
			                    '<th width="70">員工姓名</th>' +
			                    '<th width="100">職稱</th>' +
			                '</tr>' +
			            '</thead>' +
			            '<tbody>' +
			            '</tbody>' +
                    '</table>' +
                    '<div id="smainPagination2" class="pagination"></div>' +
                    '</div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline2 .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchTeacherline");

                AspAjax.SearchStaffDataBaseCountCase(obj, getid);
            });
        }
    });

}




function SaveCaseISP(Type) {

    var obj = MyBase.getTextValueBase("item1Content");
    var obj1 = getHideSpanValue("item1Content", "hideClassSpan");
    MergerObject(obj, obj1);

//    for (var i = 2; i <= 2; i++) {
//        var obj2 = MyBase.getTextValueBase("item" + i + "Content");    
//        var obj3 = getHideSpanValue("item" + i + "Content", "hideClassSpan");
////        alert(1);
//        MergerObject(obj, obj2);
//        MergerObject(obj, obj3);
//    }


    
    switch (Type) {
        case 0:
            var stuCID = parseInt($("#studentCID").html());
            var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
            if (checkString.length > 0) {
                alert(checkString);
            } else {
                AspAjax.createCaseISPData(stuCID,obj);
            }
            break;
        case 1:
            obj.Column = _ColumnID;
            AspAjax.setTeachISPDate1(obj);
           // AspAjax.setTeachISPDate2(obj);
//            AspAjax.setTeachISPDate3(obj);
//            AspAjax.setTeachISPDate4(obj);
            break;
        case 2:
            var cbxVehicle = new Array();
            var Q1myData = new Array();
//            $('input:checkbox:checked[name="EconmicNeedSituation"]').each(function(i) { cbxVehicle[i] = this.value; });
//            // var chks = $("input:checkbox:checked[name='EconmicNeedSituation']").vals();
            //alert($("input[name='EconmicNeedSituation']:checked").val());
            for (var i = 1; i <= 4; i++) {// 四個大項
                for (var j = 1; j <= 4; j++) //四個明細(要調整數量由此調整)
                {
                    var data = {};
                    data.ISPID = _ColumnID;
                    data.PlanWriter = $("#PlanWriter").html();
                    data.FrameDate = TransformRepublicReturnValue($("#FrameDate").val());
                    data.PlanExecutor = $("#PlanExecutor").val();
                    data.PlanRevise = $("#PlanRevise").html();
                    data.ReviseDate = TransformRepublicReturnValue($("#ReviseDate").val());
                    data.ReviseExecutor = $("#ReviseExecutor").val();
                    data.EconomicNeedResource = $("#EconomicNeedResource").val();
                    $('input:checkbox:checked[name="EconomicNeedSituation"]').each(function(i) { cbxVehicle[i] = this.value; });
                    data.EconomicNeedSituation = cbxVehicle.toString();
                    cbxVehicle.length = 0;
                    data.ServicesResource = $("#ServicesResource").val();
                    $('input:checkbox:checked[name="ServicesSituation"]').each(function(i) { cbxVehicle[i] = this.value; });
                    data.ServicesSituation = cbxVehicle.toString();
                    data.ServicesActiivity = $("#ServicesActiivity").val();
                    data.ServicesStatus = $("#ServicesStatus").val();
                    data.MedicalResource = $("#MedicalResource").val();
                    cbxVehicle.length = 0;
                    $("input:checkbox:checked[name='MedicalSituation']").each(function(i) { cbxVehicle[i] = this.value; });
                    data.MedicalSituation = cbxVehicle.toString();
                    data.MedicalReason = $("#MedicalReason").val();
                    data.MedicalOther = $("#MedicalOther").val();
                    data.EducationResource = $("#EducationResource").val();
                    cbxVehicle.length = 0;
                    $("input:checkbox:checked[name='EducationSituation']").each(function(i) { cbxVehicle[i] = this.value; });
                    data.EducationSituation = cbxVehicle.toString();
                    data.EducationOther = $("#EducationOther").val();

                    data.MasterOrder = i.toString();
                    data.DetailOrder = j.toString();
                    data.Target = $("#" + i + "_" + j + "_Target").val();
                    data.StartDate = $("#" + i + "_" + j + "_StartDate").val();
                    data.EndDate = $("#" + i + "_" + j + "_EndDate").val();
                    data.Executor = $("#" + i + "_" + j + "_Executor").val();
                    data.TrackDate = $("#" + i + "_" + j + "_TrackDate").val();
                    data.Manner = $("#" + i + "_" + j + "_Manner").val();
                    data.Results = $("#" + i + "_" + j + "_Results").val();
                    Q1myData[Q1myData.length] = data;


                }

            }
            AspAjax.setTeachISPDate2(Q1myData);
            break;
        case 3:
            ///待處理
            var obj = MyBase.getTextValueBase("item3Content");
            var obj1 = getHideSpanValue("item3Content", "hideClassSpan");
            MergerObject(obj, obj1);
            obj.ISPID = _ColumnID;
            obj.HearingAssessmentBy = $("#HearingAssessmentBy1").html();
            obj.HearingAssessmentBy1Name = $("#HearingAssessmentBy1Name").val();
           
            var HearingManagerDetail = new Array();
            var i = 1;
            $(".rowspans").each(function() {
                for (var j = 1; j <= $(this).attr("rowspan"); j++) {
                    var data = {};
                    data.PlanOrder = i;
                    data.DetailOrder = j;
                    data.HMDateStart = TransformRepublicReturnValue($("#" + i + "_" + j + "HMDateStart").val());
                    data.HMDateEnd = TransformRepublicReturnValue($("#" + i + "_" + j + "HMDateEnd").val());
                    data.HMADate = TransformRepublicReturnValue($("#" + i + "_" + j + "HMADate").val());
                    data.HMAMode = $("#" + i + "_" + j + "HMAMode").val();
                    data.HMAResult = $("#" + i + "_" + j + "HMAResult").val();
                    data.HMTeachingDecision = $("#" + i + "_" + j + "HMTeachingDecision").val();
                    HearingManagerDetail[HearingManagerDetail.length] = data;
                }
                i++;
            });
            obj.HearingManagerDetail = HearingManagerDetail;

            AspAjax.setTeachISPDate3(obj);

            break;
            
            
        case 4:

            var obj = MyBase.getTextValueBase("item4Content");
            obj.PlanWriter3 = $("#PlanWriter3").html();
            obj.PlanRevise3 = $("#PlanRevise3").html();
            obj.HearingAssessmentBy = $("#HearingAssessmentBy").html();
            obj.VocabularyAssessmentBy = $("#VocabularyAssessmentBy").html();
            obj.LanguageAssessmentBy = $("#LanguageAssessmentBy").html();
            obj.intelligenceAssessmentBy = $("#intelligenceAssessmentBy").html();
            obj.OtherAssessmentBy = $("#OtherAssessmentBy").html();
            var TeachingPlanarry = new Array();
            for (var i = 1; i <= 2; i++) {// 兩個領域
                for (var j = 1; j <= $("#table" + i + ">tbody>tr").length; j++) //主檔增加
                {
                    var TeachingPlanDetailArry = new Array();
                    var data = {};
                    data.TeachOrder = i;
                    data.MasterOrder = j;
                    data.TargetLong = $("#" + i + "_" + j + "TargetLong").val();

                    //    public List<TeachingPlanDetail> TeachingPlanDetail;
                    for (var k = 1; k <= 5; k++)//明細增加
                    {
                        var Detail = {};
                        Detail.DetailOrder = k;
                        Detail.TargetShort = $("#" + i + "_" + j + "_" + k + "Target").val();
                        Detail.DateStart = TransformRepublicReturnValue($("#" + i + "_" + j + "_" + k + "DateStart").val());
                        Detail.DateEnd = TransformRepublicReturnValue($("#" + i + "_" + j + "_" + k + "DateEnd").val());
                        Detail.EffectiveDate = TransformRepublicReturnValue($("#" + i + "_" + j + "_" + k + "EffectiveDate").val());
                        Detail.EffectiveMode = $("#" + i + "_" + j + "_" + k + "EffectiveMode").val();
                        Detail.EffectiveResult = $("#" + i + "_" + j + "_" + k + "EffectiveResult").val();
                        Detail.Decide = $("#" + i + "_" + j + "_" + k + "Decide").val();
                        TeachingPlanDetailArry[TeachingPlanDetailArry.length] = Detail;

                    }
                    data.TeachingPlanDetail = TeachingPlanDetailArry;
                    //TeachingPlanDetail.length = 0;
                    TeachingPlanarry[TeachingPlanarry.length] = data
                }

            }
            obj.TeachingPlan = TeachingPlanarry;
            obj.ISPID = _ColumnID;

            AspAjax.setTeachISPDate4(obj);
            break;

    }
}
