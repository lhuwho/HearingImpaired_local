var MyBase = new Base();
var _ColumnID = 0;
var _StuID = 0;
var _staffIndex;
var _ReturnStu8Value;
var _ClassType = ["個別課", "團體課"]; 
var _PageList1 = ["0","5","5","5","4","4","4","3","3","3","1","1","4","4","3","2","2","2","2","3","3","3","3","3","3","3","2","2","2","2","2","2","2","2","2","2","2"];
var _PageList2 = ["0","5","5","5","4","4","4","3","3","3","2","2","4","4","4","2","2","3","3","3","3","2","2","2","2","2","2","2","2","2","2","2","2","2","2","2","2"];

$(document).ready(function() {

AspAjax.set_defaultSucceededCallback(SucceededCallback);

    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    initPage();
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


    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './teach_service_inspect.aspx';
        }
    }
});

function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {

        case "UpdateTeachServiceInspect":
            if (parseInt(result) > 0) {
                alert("更新成功");
                window.location = "./teach_service_inspect.aspx?id=" + parseInt(result) + "&act=2";
            }
            break;
        case "CreateTeachServiceInspect":
            if (parseInt(result) > 0) {
                alert("新增成功");
                window.location = "./teach_service_inspect.aspx?id=" + parseInt(result) + "&act=2";
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
            //alert(1);
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
                    $("#PageList1_studentCID").html(result.Column);
                    $("#PageList1_StudentID").html(result.StudentData.ID);
                    var stuAge = BirthdayStringDateFunction(result.StudentData.studentbirthday);
                    $("#PageList1_StudentAge").val(stuAge[0]);
                    $("#PageList1_StudentMonth").val(stuAge[1]);
                    
                    $("#LegalrepresentativeName").val(result.StudentData.fPName2);
                    $("#LegalrepresentativePhone").val(result.StudentData.fPTel2);
                    $("#LegalrepresentativePhoneHome").val(result.StudentData.fPHPhone2);
                    $("#LegalrepresentativePhoneMobile").val(result.StudentData.fPPhone2);
                    $("#LegalrepresentativePhoneFax").val(result.StudentData.fPFax2);
                     
                    $("#caseUnit").html(_UnitList[result.StudentData.Unit]);
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
                                $("#PageList1_TeacherID").html(id);
                                $("#PageList1_teacherName").val(name);
                                break;
                            case "2":
                                $("#PageList2_TeacherID").html(id);
                                $("#PageList2_teacherName").val(name);
                                break;
                            case "3":
                                $("#PageList1_Director").html(id);
                                $("#PageList1_DirectorName").val(name);
                                break;
                            case "4":
                                $("#PageList2_Director").html(id);
                                $("#PageList2_DirectorName").val(name);
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
        case "SearchClassNameCountCase":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#smainPagination3").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    //ellipse_text: "/",
                    //link_to: ,
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTeacherline");
                        AspAjax.SearchClassNameDataBaseCase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#searchTeacherline .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#searchTeacherline .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchClassNameDataBaseCase":

            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].sID) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" >' +
                                '<td>' + result[i].txtClassID + '<span class="ClassID" style="display:none;">' + result[i].txtClassID + '</span></td>' +

			                    '<td>' + result[i].txtClassName + '<span class="ClassName" style="display:none;">' + result[i].txtClassName + '</span></td>' +
			                    '<td>' + result[i].txtstaffName + '</td>' +
                        //			                    '<td>' + TransformADFromStringFunction(result[i].officeDate) + '</td>' +
                        //			                    '<td>' + TransformADFromStringFunction(result[i].resignDate) + '</td>' +
			                '</tr>';
                    }
                    //
                    $("#StuinlineReturn3 tbody").html(inner);

                    $("#StuinlineReturn3 .ImgJs").unbind('click').click(function() {
                        var id = $(this).find(".ClassID").html();
                        var name = $(this).find(".ClassName").html();
                        $("#PageList2_ClassName").val(name);
                        $("#PageList2_ClassNameID").html(id);
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#StuinlineReturn2 .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
            
        case "SearchTeachServiceInspectCount":
            var obj = MyBase.getTextValueBase("searchTable");
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //alert(pageCount);
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                    AspAjax.SearchTeachServiceInspect(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchTeachServiceInspect":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {

                    var inner = "";
                    var date = new Date();
                    for (var i = 0; i < result.length; i++) {
                        // alert(result[i].ClassDate);
                        // var stuAge = BirthdayStringDateFunction(result[i].txtstudentbirthday.toLocaleDateString("ja-JP").replace("/", "-"));
                        inner += '<tr>' +
                                '<td>' + result[i].RowNum + '</td>' +
                                '<td>' + result[i].AcademicYear + '</td>' +
                                '<td>' + result[i].StudentName + result[i].ClassIDName + '</td>' +
                                '<td>' + _ClassType[result[i].ClassType] + '</td>' +
                                '<td>' + result[i].TeacherName + '</td>' +
                                '<td>' + YearChange(result[i].InspectDate.replace("/", "-").replace("/", "-")) + '</td>';

                        // '<td>' + YearChange(result[i].txtstudentbirthday.toLocaleDateString("tw").replace("/", "-").replace("/", "-")) + '</td>';
                        //			                    '<td>' + result[i].StudentAge + '歲' + result[i].StudentMonth + '個月</td>';

                        inner += '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }

                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "ShowTeachServiceInspect":
            if (!(result == null || result.length == 0 || result == undefined)) {

                if (result[0].ThisValue == "0") {
                    $("#ClassType").attr("onchange", "selectedIndex=" + result[0].ThisValue);
                    for (var i = 0; i < result.length; i++) {

                        if (result[i].ThisValue != "001/1/1") {
                            $("#PageList1_" + result[i].IDname).val(result[i].ThisValue);
                            $("input[name=PageList1_" + result[i].IDname + "][value='" + result[i].ThisValue + "']").attr('checked', true);
                        }
                        if (result[i].IDname == "teacherName") {
                            $("#PageList1_" + result[i].IDname + "1").val(result[i].ThisValue);
                        }
                        if (result[i].IDname == "StudentID" || result[i].IDname == "TeacherID" || result[i].IDname == "Director" || result[i].IDname == "ClassNameID") {
                            $("#PageList1_" + result[i].IDname).html(result[i].ThisValue);
                        }
                        if (result[i].IDname == "InspectDate" || result[i].IDname == "Date") {
                           // alert(result[i].ThisValue);
                            $("#PageList1_" + result[i].IDname).val(YearChange(new Date(result[i].ThisValue).toLocaleDateString("tw")));
                            // 
                        }
                        $("#" + result[i].IDname).val(result[i].ThisValue);

                    }
                    calInspect(0);
                }
                else {

                    $("#" + result[0].IDname).val(result[0].ThisValue);
                    $("#ClassType").attr("onchange", "selectedIndex=" + result[0].ThisValue);
                    if ($("#ClassType").val() == 1) {
                        $("#page0").fadeOut();
                        $("#page1").fadeIn();
                    }

                    for (var i = 0; i < result.length; i++) {
                        if (result[i].ThisValue != "001/1/1") {
                            $("#PageList2_" + result[i].IDname).val(result[i].ThisValue);
                            $("input[name=PageList2_" + result[i].IDname + "][value='" + result[i].ThisValue + "']").attr('checked', true);
                        }
                        if (result[i].IDname == "teacherName") {
                            $("#PageList2_" + result[i].IDname + "1").val(result[i].ThisValue);
                        }
                        if (result[i].IDname == "StudentID" || result[i].IDname == "TeacherID" || result[i].IDname == "Director" || result[i].IDname == "ClassNameID") {
                            $("#PageList2_" + result[i].IDname).html(result[i].ThisValue);
                        }
                        if (result[i].IDname == "InspectDate" || result[i].IDname == "Date") {
                            //alert(result[i].ThisValue);
                             $("#PageList2_" + result[i].IDname).val(YearChange(new Date(result[i].ThisValue).toLocaleDateString("tw")));
                            // 
                        }
                        $("#" + result[i].IDname).val(result[i].ThisValue);

                    }
                    calInspect(1);

                }


            } else {
                alert("查無資料");
                window.close();
            }
            break;
    } 
}

function getView(id) {
    window.open("./teach_service_inspect.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);

        AspAjax.ShowTeachServiceInspect(id);
//        $("select[name='class']").val("1");
//        if ($("#ClassType").val() == 1) {
//                    $("#page0").fadeOut();
//                    $("#page1").fadeIn();
//        }
//        $("#page0").fadeOut();
//        $("#page1").fadeIn();
//        $("#className").val("西瓜班");
//        $("#teacherName").val("王貞貞");
    } else if (id == null && act == 1) {
         $("#studentName").unbind("click").click(function() { callStudentSearchfunction(); });
        $("#PageList1_teacherName").unbind("click").click(function() { callTeacherSearchfunction("1"); });
        $("#PageList2_teacherName").unbind("click").click(function() { callTeacherSearchfunction("2"); });
        $("#PageList1_DirectorName").unbind("click").click(function() { callTeacherSearchfunction("3"); });
        $("#PageList2_DirectorName").unbind("click").click(function() { callTeacherSearchfunction("4"); });
        $("#PageList2_ClassName").unbind("click").click(function() { callClassSearchfunction(); });
        $("select[name='class']").change(function() {
            if ($(this).find(":selected").val() == 0) {
                $("#page0").fadeIn();
                $("#page1").fadeOut();
            } else {
                $("#page0").fadeOut();
                $("#page1").fadeIn();
            }
        });
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

function callClassSearchfunction() {//班級點取跳出

    var inner = '<div id="inline3"><br /><table id="searchClassline" border="0" width="400">' +
			            '<tr><td width="80">教師姓名 <input type="text" id="gosrhstaffName" value="" /></td></tr>' +
			            '<tr><td>班級名稱 <input type="text" id="gosrhClassName" value=""/>'+
                     '</td></tr><tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn3" class="tableList" border="0"  width="400">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="70">班級編號</th>' +
			                    '<th width="100">班級名稱</th>' +
			                    '<th width="70">導師姓名</th>' +
			                '</tr>' +
			            '</thead>' +
			            '<tbody>' +
			            '</tbody>' +
                    '</table>' +
                    '<div id="smainPagination3" class="pagination"></div>' +
                    '</div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $('button').css({ "background-color": "#F9AE56" });
            $("#inline3 .btnSearch").unbind("click").click(function() {
                var obj = MyBase.getTextValueBase("searchTeacherline");

                AspAjax.SearchClassNameCountCase(obj);
            });
        }
    });

}






function calInspect(page) {
    if (page == 0) {
        //$("#PageList1_SumScore").val($("#PageList1_SumScore").val() == "" ? "0" : $("#PageList1_SumScore").val());
        $("#PageList1_SumScore").val("0");
        for (var i = 1; i <= 6; i++) { $("#PageList1_SumScore" + i).val("0"); }
        $("#PageList1_Denominator1").html("38");
        $("#PageList1_Denominator2").html("19");
        $("#PageList1_Denominator3").html("27");
        $("#PageList1_Denominator4").html("12");
        $("#PageList1_Denominator5").html("4");
        for (var i = 1; i <= 36; i++) {
            if (i <= 11) {
                if ($("#PageList1_Score" + i).val() != 99) {
                    $("#PageList1_SumScore1").val(parseInt($("#PageList1_SumScore1").val()) + parseInt($("#PageList1_Score" + i).val()));
                }
                else {
                    $("#PageList1_Denominator1").html(parseInt($("#PageList1_Denominator1").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 18) {
                if ($("#PageList1_Score" + i).val() != 99) {
                    $("#PageList1_SumScore2").val(parseInt($("#PageList1_SumScore2").val()) + parseInt($("#PageList1_Score" + i).val()));
                }
                else {
                    $("#PageList1_Denominator2").html(parseInt($("#PageList1_Denominator2").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 28) {
                if ($("#PageList1_Score" + i).val() != 99) {
                    $("#PageList1_SumScore3").val(parseInt($("#PageList1_SumScore3").val()) + parseInt($("#PageList1_Score" + i).val()));
                }
                else {
                    $("#PageList1_Denominator3").html(parseInt($("#PageList1_Denominator3").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 34) {
                if ($("#PageList1_Score" + i).val() != 99) {
                    $("#PageList1_SumScore4").val(parseInt($("#PageList1_SumScore4").val()) + parseInt($("#PageList1_Score" + i).val()));
                }
                else {
                    $("#PageList1_Denominator4").html(parseInt($("#PageList1_Denominator4").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 36) {
                if ($("#PageList1_Score" + i).val() != 99) {
                    $("#PageList1_SumScore5").val(parseInt($("#PageList1_SumScore5").val()) + parseInt($("#PageList1_Score" + i).val()));
                }
                else {
                    $("#PageList1_Denominator5").html(parseInt($("#PageList1_Denominator5").html()) - parseInt(_PageList1[i]));
                }
            }
        }
        for (var i = 1; i <= 5; i++) {
            if ($("#PageList1_Denominator" + i).html() != 0) {
                var ans = (parseInt($("#PageList1_SumScore" + i).val()) / parseInt($("#PageList1_Denominator" + i).html())) * 100
                $("#PageList1_PerScore" + i).val(Math.round(ans * 100) / 100);
            } else { $("#PageList1_PerScore" + i).val(0); }
            $("#PageList1_SumScore").val(parseInt($("#PageList1_SumScore").val()) + parseInt($("#PageList1_SumScore" + i).val()));
        }

    }
    else {
        $("#PageList2_SumScore").val(0);
        //$("#PageList2_SumScore").val($("#PageList2_SumScore").val() == "" ? "0" : $("#PageList2_SumScore").val());
        for (var i = 1; i <= 6; i++) { $("#PageList2_SumScore" + i).val("0"); }
        $("#PageList2_Denominator1").html("40");
        $("#PageList2_Denominator2").html("15");
        $("#PageList2_Denominator3").html("20");
        $("#PageList2_Denominator4").html("8");
        $("#PageList2_Denominator5").html("12");
        $("#PageList2_Denominator6").html("4");
        for (var i = 1; i <= 36; i++) {
            if (i <= 11) {
                if ($("#PageList2_Score" + i).val() != 99) {
                    $("#PageList2_SumScore1").val(parseInt($("#PageList2_SumScore1").val()) + parseInt($("#PageList2_Score" + i).val()));
                }
                else {
                    $("#PageList2_Denominator1").html(parseInt($("#PageList2_Denominator1").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 16) {
                if ($("#PageList2_Score" + i).val() != 99) {
                    $("#PageList2_SumScore2").val(parseInt($("#PageList2_SumScore2").val()) + parseInt($("#PageList2_Score" + i).val()));
                }
                else {
                    $("#PageList2_Denominator2").html(parseInt($("#PageList2_Denominator2").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 24) {
                if ($("#PageList2_Score" + i).val() != 99) {
                    $("#PageList2_SumScore3").val(parseInt($("#PageList2_SumScore3").val()) + parseInt($("#PageList2_Score" + i).val()));
                }
                else {
                    $("#PageList2_Denominator3").html(parseInt($("#PageList2_Denominator3").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 28) {
                if ($("#PageList2_Score" + i).val() != 99) {
                    $("#PageList2_SumScore4").val(parseInt($("#PageList2_SumScore4").val()) + parseInt($("#PageList2_Score" + i).val()));
                }
                else {
                    $("#PageList2_Denominator4").html(parseInt($("#PageList2_Denominator4").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 34) {
                if ($("#PageList2_Score" + i).val() != 99) {
                    $("#PageList2_SumScore5").val(parseInt($("#PageList2_SumScore5").val()) + parseInt($("#PageList2_Score" + i).val()));
                }
                else {
                    $("#PageList2_Denominator5").html(parseInt($("#PageList2_Denominator5").html()) - parseInt(_PageList1[i]));
                }
            }
            else if (i <= 36) {
                if ($("#PageList2_Score" + i).val() != 99) {
                    $("#PageList2_SumScore6").val(parseInt($("#PageList2_SumScore6").val()) + parseInt($("#PageList2_Score" + i).val()));
                }
                else {
                    $("#PageList2_Denominator6").html(parseInt($("#PageList2_Denominator6").html()) - parseInt(_PageList1[i]));
                }
            }
        }
        for (var i = 1; i <= 6; i++) {
            if ($("#PageList2_Denominator" + i).html() != 0) {
                var ans = (parseInt($("#PageList2_SumScore" + i).val()) / parseInt($("#PageList2_Denominator" + i).html())) * 100
                $("#PageList2_PerScore" + i).val(Math.round(ans * 100) / 100);
            } else { $("#PageList2_PerScore" + i).val(0); }
            $("#PageList2_SumScore").val(parseInt($("#PageList2_SumScore").val()) + parseInt($("#PageList2_SumScore" + i).val()));
        }


    }
}


function Search() { //主頁面 顯示名單
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");

        AspAjax.SearchTeachServiceInspectCount(obj);

}


function Update() {//驗證待補  by Awho
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    //alert($("#ClassType").val());
    if (id != null ) {
        var obj = MyBase.getTextValueBase("page" + $("#ClassType").val());
        obj.StudentID = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_StudentID").html();
        obj.TeacherID = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_TeacherID").html();
        obj.ClassNameID = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_ClassNameID").html();
        obj.Director = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_Director").html();

        obj.AcademicYear = $("#AcademicYear").val();
        obj.ClassType = $("#ClassType").val();
        obj.ID = id;
        AspAjax.UpdateTeachServiceInspect(obj);
        //        if ($("#StudentID").html() != null && $("#StudentID").html() != "" && $("#TeacherID").html() != "" && $("#ClassDate").val() != "" && $("#AcademicYear").val() != "") {
        //            var obj = MyBase.getTextValueBase("tableContact1");

        ////            obj.StudentID = $("#StudentID").html();
        ////            obj.TeacherID = $("#TeacherID").html();
        ////            obj.ClassDate = $("#ClassDate").val();
        ////            obj.SupervisorName = $("#SupervisorName").val();
        ////            obj.AcademicYear = $("#AcademicYear").val();
        //            AspAjax.CreatTeachServiceSupervisor(obj);
        //        }
        //        else {
        //            alert("請補齊資料");
        //            $("#studentName").focus();
        //        }

    }
}




function Save() {//驗證待補  by Awho
    var id = GetQueryString("id");
    var act = GetQueryString("act");

    if (id == null) {
        var obj = MyBase.getTextValueBase("page" + $("#ClassType").val());
        obj.StudentID = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_StudentID").html();
        obj.TeacherID = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_TeacherID").html();
        obj.ClassNameID = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_ClassNameID").html();
        obj.Director = $("#PageList" + (parseInt($("#ClassType").val()) + 1) + "_Director").html();
        
        obj.AcademicYear = $("#AcademicYear").val();
        obj.ClassType = $("#ClassType").val();
        AspAjax.CreateTeachServiceInspect(obj);
//        if ($("#StudentID").html() != null && $("#StudentID").html() != "" && $("#TeacherID").html() != "" && $("#ClassDate").val() != "" && $("#AcademicYear").val() != "") {
//            var obj = MyBase.getTextValueBase("tableContact1");

////            obj.StudentID = $("#StudentID").html();
////            obj.TeacherID = $("#TeacherID").html();
////            obj.ClassDate = $("#ClassDate").val();
////            obj.SupervisorName = $("#SupervisorName").val();
////            obj.AcademicYear = $("#AcademicYear").val();
//            AspAjax.CreatTeachServiceSupervisor(obj);
//        }
//        else {
//            alert("請補齊資料");
//            $("#studentName").focus();
//        }

    }
}


