var MyBase = new Base();
var noEmptyItem = ["studentID", "studentName"];
var noEmptyShow = ["服務使用者編號抓取錯誤，請重新選擇學生", "學生姓名"];
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
    });

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

    $("#appurtenance img").fancybox();
    $("#WriteNameName").unbind("click").click(function() { callTeacherSearchfunction("1"); });
    $("#WriteName1Name").unbind("click").click(function() { callTeacherSearchfunction("2"); });
    $("#RecordedByName").unbind("click").click(function() { callTeacherSearchfunction("3"); });
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './study_level_check.aspx';
        }
    }
});
function SucceededCallback(result, userContext, methodName) {

    switch (methodName) {
        case "UpdateCaseStudy":
            if (result[0].ID != null) {
                alert("更新成功");
                window.location = "./study_level_check.aspx?id=" + result[0].ID + "&act=2";
            }
            break;
        case "CreatCaseStudy":
            if (result[0].ID != null) {
                alert("新增完成");
                window.location = "./study_level_check.aspx?id=" + result[0].ID + "&act=2";
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
            //alert(result.StudentData.studentID);
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.StudentData.studentID != -1) {
                    PushPageValue(result.StudentData);
                    $("#studentCID").html(result.Column);
                    $("#StudentID").html(result.StudentData.ID);
                    var stuAge = BirthdayStringDateFunction(result.StudentData.studentbirthday);
                    $("#StudentAge").val(stuAge[0]);
                    $("#StudentMonth").val(stuAge[1]);
                    $("#studentbirthday").html(YearChange(result.StudentData.studentbirthday));

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
                                $("#WriteName").html(id);
                                $("#WriteNameName").val(name);
                                break;
                            case "2":
                                $("#WriteName1").html(id);
                                $("#WriteName1Name").val(name);
                                break;
                            case "2":
                                $("#RecordedBy").html(id);
                                $("#RecordedByName").val(name);
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
            $("#WriteNameName").val(result[1]);
            $("#WriteName1Name").val(result[1]);
            $("#RecordedByName").val(result[1]);
            $("#RecordedBy").html(result[0]);
            $("#WriteName").html(result[0]);
            $("#WriteName1").html(result[0]);
            
           // alert(result[1]);
            break;
        case "SearchCaseStudyCount":
            var obj = MyBase.getTextValueBase("searchTable");
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
                        AspAjax.SearchCaseStudy(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchCaseStudy":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    var date = new Date();
                    for (var i = 0; i < result.length; i++) {
                        var stuAge = BirthdayStringDateFunction2(result[i].txtstudentbirthday.toLocaleDateString("ja-JP").replace("/", "-"), result[i].WriteDate.toLocaleDateString("tw").replace("/", "-"));
                        inner += '<tr>' +
                                '<td>' + result[i].RowNum + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + YearChange(result[i].WriteDate.toLocaleDateString("tw")) + '</td>' +
			                    '<td>' + YearChange(result[i].txtstudentbirthday.toLocaleDateString("tw")) + '</td>' +
			                    '<td>' + stuAge[0] + '歲' + stuAge[1] + '個月</td>';
//                                if (result[i].txtstudentSex == 1) {
//                                    inner += '<td>男</td>';
//                                } else if (result[i].txtstudentSex == 2) {
//                                    inner += '<td>女</td>';
//                                } else { inner += '<td></td>'; }

                        inner += '<td>' + _SexList[result[i].txtstudentSex] + '</td>'; 
                        inner += '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "ShowCaseStudy":
            if (!(result == null || result.length == 0 || result == undefined)) {
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ThisValue != "001/1/1") {
                        $("#" + result[i].IDname).val(result[i].ThisValue);
                        $("input[name=" + result[i].IDname + "][value='" + result[i].ThisValue + "']").attr('checked', true);
                    }
                    if (result[i].IDname == "StudentID" || result[i].IDname == "WriteName" || result[i].IDname == "WriteName1" || result[i].IDname == "RecordedBy") {
                        $("#" + result[i].IDname).html(result[i].ThisValue);
                    }
                     if ( result[i].IDname == "studentbirthday") {
                        $("#" + result[i].IDname).html( YearChange(result[i].ThisValue));
                    }
                    

                }
                for (var i = 1; i <= 4; i++) {
                    $("#ParentSum"+i).val(0);
                    $("#TeacherSum"+i).val(0);
                    $("#Average"+i).val(0);
                }
                for (var i = 1; i <= 11; i++) {

                    if (i <= 6) {
                        $("#ParentSum1").val(parseInt($("#ParentSum1").val()) + parseInt($("#ParentScore" + i).val()));
                        $("#TeacherSum1").val(parseInt($("#TeacherSum1").val()) + parseInt($("#TeacherScore" + i).val()));
                        $("#Average1").val((parseInt($("#ParentSum1").val()) + parseInt($("#TeacherSum1").val())) / 2);

                    }
                    else if (i == 7) {
                        $("#ParentSum2").val( $("#ParentScore" + i).val());
                        $("#TeacherSum2").val($("#TeacherScore" + i).val());
                        $("#Average2").val((parseInt($("#ParentSum2").val()) + parseInt($("#TeacherSum2").val())) / 2);
                    }
                    else if (i == 8) {
                        $("#ParentSum3").val($("#ParentScore" + i).val());
                        $("#TeacherSum3").val($("#TeacherScore" + i).val());
                        $("#Average3").val((parseInt($("#ParentSum3").val()) + parseInt($("#TeacherSum3").val())) / 2);
                    }
                    else if (i >= 9) {
                        $("#ParentSum4").val(parseInt($("#ParentSum4").val()) + parseInt($("#ParentScore" + i).val()));
                        $("#TeacherSum4").val(parseInt($("#TeacherSum4").val()) + parseInt($("#TeacherScore" + i).val()));
                        $("#Average4").val((parseInt($("#ParentSum4").val()) + parseInt($("#TeacherSum4").val())) / 2);
                    }
                }
            } else {
                //alert("查無資料");
                window.close();
            }
            break;
    }

}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function getView(id) {
    window.open("./study_level_check.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.ShowCaseStudy(id);
        //$("#studentName").val("王小明");
    } else if (id == null && act == 1) {
    $("#studentName").unbind("click").click(function() { callStudentSearchfunction(); });
    AspAjax.GetWriteName();
        $(".btnSave").fadeIn();
    }
}

function callStudentSearchfunction() {//學生姓名點取跳出
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


function callTeacherSearchfunction(getid) {//老師姓名點取跳出

    var inner = '<div id="inline2"><br /><table id="searchTeacherline" border="0" width="400">' +
			            '<tr><td width="80">教師姓名 <input type="text" id="gosrhstaffName" value="" /></td></tr>' +
			            '<tr><td >性　　別 <select id="gosrhstaffSex"><option value="0">請選擇</option><option value="1">男</option><option value="2">女</option></select></td></tr>' +
                        '<tr><td >出生日期 <input class="date" type="text" id="gosrhstaffBirthdayStart" size="10" />～<input class="date" type="text" id="gosrhstaffBirthdayEnd" size="10" /></td></tr>' +
			            '<tr><td>教師編號 <input type="text" id="gosrhstaffID" value=""/>';
    if (getid == 1 || getid == 2) { inner += '<select id="gosrhstaffJob" style="display:none;" ><option value="14">教師</option></select>'; }
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

function Search() { //主頁面 顯示名單
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");

    AspAjax.SearchCaseStudyCount(obj);

}
function Update() {
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null) {
        if ($("#StudentID").html() != null && $("#StudentID").html() != "" ) {
            var obj = MyBase.getTextValueBase("tableContact1");
            obj.StudentID = $("#StudentID").html();
            obj.WriteName = $("#WriteName").html();
            obj.WriteName1 = $("#WriteName1").html();
            obj.RecordedBy = $("#RecordedBy").html();
            obj.ID = id;
           
            AspAjax.UpdateCaseStudy(obj);
        }
        else {
           
            goNext(0);
            $("#studentName").focus();
        }

    }
}

function Save() { //暫時先寫 之後再修正
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id == null) {
        if ($("#StudentID").html() != null && $("#StudentID").html() != "" && $("#WriteDate").val() != "") {
            var obj = MyBase.getTextValueBase("tableContact1");
            
            //  alert(obj.TeacherScore1 + "- "+ obj.Participants6);
            obj.StudentID = $("#StudentID").html();
            obj.WriteName = $("#WriteName").html();
            obj.WriteName1 = $("#WriteName1").html();
            obj.RecordedBy = $("#RecordedBy").html();
            ///RecordedBy
            AspAjax.CreatCaseStudy(obj);
        }
        else {
            goNext(0);
            $("#studentName").focus();
        }

    }
}





/////AWHO 改版
function BirthdayStringDateFunction2(BirthdayDate,StratDate) { //String
    var item = BirthdayDate.split("-");
    var item2 = StratDate.split("-");
    return callculateAge2(item[0], item[1] , item2[0], item2[1]);
}
function callculateAge2(year_ddl1, month_ddl1, year_ddl2 , month_ddl2) {
    var Age = 0;
    var staffMonth = 0;
    if (year_ddl1 != 1900 && year_ddl2 != 1900 ) {
        var myDate = new Date();
        var gYear = year_ddl2;
        var gMonth = parseInt(month_ddl2) + 1;
        var gDate = myDate.getDate();
        var totalManth = (parseInt(gYear) * 12 + parseInt(gMonth)) - (parseInt(year_ddl1) * 12 + parseInt(month_ddl1));
        return [parseInt(totalManth / 12), totalManth % 12];
        //        var myDate = new Date();
        //        var gYear = myDate.getFullYear();
        //        var gMonth = (myDate.getMonth()) + 1;
        //        var gDate = myDate.getDate();
        //        Age = parseInt(gYear - year_ddl1);
        //        alert(Age);
        //        var Monthitem = parseInt(gMonth - month_ddl1);

        //        if (Monthitem < 0 && Age > 0) {
        //            staffMonth = 12 - month_ddl1 + gMonth;
        //        } else if (Monthitem > 0) {
        //            staffMonth = gMonth - month_ddl1;
        //        } else if (Monthitem == 0) {
        //            staffMonth = 0;
        //        }
        //        if (parseInt(year_ddl1) == parseInt(gYear)) {
        //            Age = 0;
        //        } else if (Monthitem <= 0) {
        //            if (gDate - day_ddl1 > 0) {
        //                Age = Age - 1;
        //            }
        //        }
    }
    return [Age, staffMonth];
}


