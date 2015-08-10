var MyBase = new Base();
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
            window.location.href = './single_teach_case.aspx';
        }
    }

//  //  $(".way").dropdownchecklist({ width: 60 });
//    $(".ui-dropdownchecklist").css({ "margin": "4px 0", "height": "auto" });
//    $(".ui-dropdownchecklist-dropcontainer").css({ "height": "auto" });
//    $(".ui-dropdownchecklist-text").css({"font-size": "13px", "line-height": "20px"});
});


function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
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
                                $("#teacherID").html(id);
                                $("#teacherName").val(name);
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
        case "GetSingleTeachCaseCount":
            while ($("#table1>tbody>tr").length > 0) {
                $("#table1>tbody>tr:last-child").detach();
            }
            while ($("#table2>tbody>tr").length > 0) {
                $("#table2>tbody>tr:last-child").detach();
            }
            getAdd(1); getAdd(2);
            if (result[0] > 0 || result[1] > 0) {
               
                for (var i = 2; i <= result[0]; i++) {

                    getAdd(1);
                }
                for (var i = 2; i <= result[1]; i++) {
                    getAdd(2);
                }
            }
            setInitData();
            if ($("#PlanDateStart").val() != "" && $("#PlanDateEnd").val() != "" && $("#studentID").html() != "") {
                // alert(1);
                AspAjax.GetSingleTeachCase($("#studentID").html(), TransformRepublicReturnValue($("#PlanDateStart").val()), TransformRepublicReturnValue($("#PlanDateEnd").val()));

            }
          //  setInitData();
//            var id = GetQueryString("id");
//            AspAjax.GetSingleTeachCase(id);
            break;
            
        case "GetSingleTeachCase":
            var case1 = 1; var case2 = 1; //宣告是第幾個用
           // alert(result[0].TargetMain);
            if (!(result == null || result.length == 0 || result == undefined)) {
                for (var i = 0; i < result.length; i++) {
                    if (result[i].PlanOrder == 1) {
                        $("#1_" + case1 + "TargetShort").val(result[i].TargetMain);
                        $("#1_" + case1 + "TPDID").html(result[i].ID);
                        case1++;
                    } else if (result[i].PlanOrder == 2) {
                        $("#2_" + case2 + "TargetShort").val(result[i].TargetMain);
                        $("#2_" + case2 + "TPDID").html(result[i].ID);
                         case2++;
                    }

                }

            }
            break;
        case "UpdateSingleTeachCase":
            if (result > 1) {
                alert("修改成功");
                window.location = 'single_teach_case.aspx?id=' + result + '&act=2';
            } else {
                alert("發生錯誤");
            }
            break;
            
        case "CreateSingleTeachCase":
            if (result > 1) {
                alert("新增成功");
                window.location = 'single_teach_case.aspx?id=' + result + '&act=2';
            } else {
                alert("發生錯誤");
            }
            break;
        case "GetSingleTeachShortTerm":
            if (!(result == null || result.length == 0 || result == undefined)) {
                
                var case1 = 1; var case2 = 1; //宣告是第幾個用
                while ($("#table1>tbody>tr").length > 0) {
                    $("#table1>tbody>tr:last-child").detach();
                }
                while ($("#table2>tbody>tr").length > 0) {
                    $("#table2>tbody>tr:last-child").detach();
                }
                for (var i = 0; i < result.SingleClassShortTermTarget.length; i++) {
                    if (result.SingleClassShortTermTarget[i].PlanOrder == 1) {
                        getAdd(1);
                    } else if (result.SingleClassShortTermTarget[i].PlanOrder == 2) {
                        getAdd(2);
                    }

                }
                PushPageValue(result);
                //setInitData();
                for (var i = 0; i < result.SingleClassShortTermTarget.length; i++) {
                    if (result.SingleClassShortTermTarget[i].PlanOrder == 1) {
                        $("#1_" + case1 + "TargetShort").val(result.SingleClassShortTermTarget[i].TargetMain);
                        $("#1_" + case1 + "TargetContent").val(result.SingleClassShortTermTarget[i].TargetContent);
                        $("#1_" + case1 + "TPDID").html(result.SingleClassShortTermTarget[i].TPDID);
                        $("#1_" + case1 + "Assessment1").val(result.SingleClassShortTermTarget[i].Assessment1);
                        $("#1_" + case1 + "Assessment2").val(result.SingleClassShortTermTarget[i].Assessment2);
                        $("#1_" + case1 + "Assessment3").val(result.SingleClassShortTermTarget[i].Assessment3);
                        $("#1_" + case1 + "Assessment4").val(result.SingleClassShortTermTarget[i].Assessment4);
                        $("#1_" + case1 + "Assessment5").val(result.SingleClassShortTermTarget[i].Assessment5);
                        $("#1_" + case1 + "Performance1").val(result.SingleClassShortTermTarget[i].Performance1);
                        $("#1_" + case1 + "Performance2").val(result.SingleClassShortTermTarget[i].Performance2);
                        $("#1_" + case1 + "Performance3").val(result.SingleClassShortTermTarget[i].Performance3);
                        $("#1_" + case1 + "Performance4").val(result.SingleClassShortTermTarget[i].Performance4);
                        $("#1_" + case1 + "Performance5").val(result.SingleClassShortTermTarget[i].Performance5);
                        $("#1_" + case1 + "PlanExecutionDate1").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate1));
                        $("#1_" + case1 + "PlanExecutionDate2").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate2));
                        $("#1_" + case1 + "PlanExecutionDate3").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate3));
                        $("#1_" + case1 + "PlanExecutionDate4").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate4));
                        $("#1_" + case1 + "PlanExecutionDate5").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate5));

                        case1++;
                    } else if (result.SingleClassShortTermTarget[i].PlanOrder == 2) {
                        $("#2_" + case2 + "TargetShort").val(result.SingleClassShortTermTarget[i].TargetMain);
                        $("#2_" + case2 + "TargetContent").val(result.SingleClassShortTermTarget[i].TargetContent);
                        $("#2_" + case2 + "TPDID").html(result.SingleClassShortTermTarget[i].TPDID);
                        $("#2_" + case2 + "Assessment1").val(result.SingleClassShortTermTarget[i].Assessment1);
                        $("#2_" + case2 + "Assessment2").val(result.SingleClassShortTermTarget[i].Assessment2);
                        $("#2_" + case2 + "Assessment3").val(result.SingleClassShortTermTarget[i].Assessment3);
                        $("#2_" + case2 + "Assessment4").val(result.SingleClassShortTermTarget[i].Assessment4);
                        $("#2_" + case2 + "Assessment5").val(result.SingleClassShortTermTarget[i].Assessment5);
                        $("#2_" + case2 + "Performance1").val(result.SingleClassShortTermTarget[i].Performance1);
                        $("#2_" + case2 + "Performance2").val(result.SingleClassShortTermTarget[i].Performance2);
                        $("#2_" + case2 + "Performance3").val(result.SingleClassShortTermTarget[i].Performance3);
                        $("#2_" + case2 + "Performance4").val(result.SingleClassShortTermTarget[i].Performance4);
                        $("#2_" + case2 + "Performance5").val(result.SingleClassShortTermTarget[i].Performance5);
                        $("#2_" + case2 + "PlanExecutionDate1").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate1));
                        $("#2_" + case2 + "PlanExecutionDate2").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate2));
                        $("#2_" + case2 + "PlanExecutionDate3").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate3));
                        $("#2_" + case2 + "PlanExecutionDate4").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate4));
                        $("#2_" + case2 + "PlanExecutionDate5").val(TransformADFromStringFunction(result.SingleClassShortTermTarget[i].PlanExecutionDate5));


                        case2++;
                    }

                }

            }
           // setInitData();
            $("input").add("select").add("textarea").attr("disabled", true);
            break;
        case "SearchSingleTeachCount":
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
                        AspAjax.SearchSingleTeach(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchSingleTeach":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    var date = new Date();
                    for (var i = 0; i < result.length; i++) {

                        inner += '<tr>' +
                                '<td>' + result[i].RowNum + '</td>' +
			                    '<td>' + result[i].studentName + '</td>' +
			                    '<td>' + result[i].teacherName + '</td>' +
			                     '<td>' + TransformADFromStringFunction(result[i].PlanDateStart) + '~' + TransformADFromStringFunction(result[i].PlanDateEnd) + '</td>';

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
    }
}



function getView(id) {
    window.open("./single_teach_case.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
//        $("#studentName").val("王小明");
        //        $("#teacherName").val("連淑貞");
          AspAjax.GetSingleTeachShortTerm(id);
    } else if (id == null && act == 1) {
    $(".btnSave").fadeIn();
    $("#teacherName").unbind("click").click(function() { callTeacherSearchfunction("1"); });

    $("#studentName").unbind("click").click(function() {callStudentSearchfunction();});
    }
}


function Search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");

    AspAjax.SearchSingleTeachCount(obj);

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

function getAdd(tid) {
    var act = GetQueryString("act");
    var inner = '';
    inner += ' <tr id="'+tid+'_'+($("#table" + tid + ">tbody>tr").length+1)+'dataTR">'+
                ' <td colspan="4">  <table class="tableContact2" width="774" border="0"> <tr><th width="50">目標<button id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'remove" class="btnAdd" onclick="del(this);" >刪除</button><span id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'TPDID" class="hideClassSpan"></span></th>' +
                '<td width="480"><textarea id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'TargetShort" class="short" cols="50" rows="3"></textarea></td>';
    var PlanExecutionDate = ''; var Assessment = ''; var Performance = '';
    for (var i = 1; i <= 5; i++) {
        PlanExecutionDate += (PlanExecutionDate.length > 0 ? '<br />' : '') + '<input id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'PlanExecutionDate' + i + '" class="date" type="text" value="" size="10" />';
        Assessment += (Assessment.length > 0 ? '<br />' : '') + '<select id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'Assessment'+i+'"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>';
        Performance += (Performance.length > 0 ? '<br />' : '') + '<select id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'Performance'+i+'"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select>';
    }
    inner += '<td rowspan="2" width="84">' + PlanExecutionDate + '</td><td rowspan="2" width="80" >' + Assessment + '</td><td rowspan="2" width="80">' + Performance + '</td></tr>' +
        '<tr><th>學習<br />進度</th><td><textarea id="' + tid + '_' + ($("#table" + tid + ">tbody>tr").length + 1) + 'TargetContent" class="short" cols="50" rows="10"></textarea></td></tr> </table>'+
        '</td></tr>';
    if ($("#table" + tid + ">tbody>tr").length > 0) {
        $("#table" + tid + ">tbody>tr:last-child").after(inner);
    }
    else {
        $("#table" + tid + ">tbody").html(inner);
    }
    if (act != 1) {
        $("input").add("select").add("textarea").attr("disabled", true);
    }
    setInitData();

}

function getSubtract(tid) {
    if ($("#table" + tid + ">tbody>tr").length > 1) {
        $("#table" + tid + ">tbody>tr:last-child").detach();
    }
}


/*删除*/
function del(obj) {
    //判斷哪一行TR
    var rowIndex = obj.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.rowIndex;
    //alert(rowIndex);
    var id = obj.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.parentNode.id;
    //alert(id);
    selectedTr = rowIndex;
    if (selectedTr != null) {
        if (confirm("是否刪除?")) {
//id.replace('table','')
            //var intId = document.getElementById(id).rows.length;
            document.getElementById(id).deleteRow(rowIndex);
            // ($("#table" + tid + ">tbody>tr").length + 1)
            //alert(($("#" + id + ">tbody>tr").length) + 1);
            var tid = id.replace('table', '');
            if (rowIndex != (($("#" + id + ">tbody>tr").length)+1)) {
                for (var i = rowIndex; i < (($("#" + id + ">tbody>tr").length) + 1); i++) {
                    $("#" + tid + "_" + (i + 1) + "dataTR").attr("id", tid + "_" + i + "dataTR");
                    $("#" + tid + "_" + (i + 1) + "remove").attr("id", tid + "_" + i + "remove");
                    $("#" + tid + "_" + (i + 1) + "TPDID").attr("id", tid + "_" + i + "TPDID");
                    $("#" + tid + "_" + (i + 1) + "TargetShort").attr("id", tid + "_" + i + "TargetShort");
                    $("#" + tid + "_" + (i + 1) + "PlanExecutionDate1").attr("id", tid + "_" + i + "PlanExecutionDate1");
                    $("#" + tid + "_" + (i + 1) + "PlanExecutionDate2").attr("id", tid + "_" + i + "PlanExecutionDate2");
                    $("#" + tid + "_" + (i + 1) + "PlanExecutionDate3").attr("id", tid + "_" + i + "PlanExecutionDate3");
                    $("#" + tid + "_" + (i + 1) + "PlanExecutionDate4").attr("id", tid + "_" + i + "PlanExecutionDate4");
                    $("#" + tid + "_" + (i + 1) + "PlanExecutionDate5").attr("id", tid + "_" + i + "PlanExecutionDate5");
                    $("#" + tid + "_" + (i + 1) + "Assessment1").attr("id", tid + "_" + i + "Assessment1");
                    $("#" + tid + "_" + (i + 1) + "Assessment2").attr("id", tid + "_" + i + "Assessment2");
                    $("#" + tid + "_" + (i + 1) + "Assessment3").attr("id", tid + "_" + i + "Assessment3");
                    $("#" + tid + "_" + (i + 1) + "Assessment4").attr("id", tid + "_" + i + "Assessment4");
                    $("#" + tid + "_" + (i + 1) + "Assessment5").attr("id", tid + "_" + i + "Assessment5");
                    $("#" + tid + "_" + (i + 1) + "Performance1").attr("id", tid + "_" + i + "Performance1");
                    $("#" + tid + "_" + (i + 1) + "Performance2").attr("id", tid + "_" + i + "Performance2");
                    $("#" + tid + "_" + (i + 1) + "Performance3").attr("id", tid + "_" + i + "Performance3");
                    $("#" + tid + "_" + (i + 1) + "Performance4").attr("id", tid + "_" + i + "Performance4");
                    $("#" + tid + "_" + (i + 1) + "Performance5").attr("id", tid + "_" + i + "Performance5");
                    $("#" + tid + "_" + (i + 1) + "TargetContent").attr("id", tid + "_" + i + "TargetContent");
                    
                }
            }
            
                

        }
    }
}

function TakeTPD() {
    if ($("#PlanDateStart").val() != "" && $("#PlanDateEnd").val() != "" && $("#studentID").html() != "") {
        // alert(1);
        AspAjax.GetSingleTeachCaseCount($("#studentID").html(), TransformRepublicReturnValue($("#PlanDateStart").val()), TransformRepublicReturnValue($("#PlanDateEnd").val()));

    }
    else {

        while ($("#table1>tbody>tr").length > 0) {
            $("#table1>tbody>tr:last-child").detach();
        }
        while ($("#table2>tbody>tr").length > 0) {
            $("#table2>tbody>tr:last-child").detach();
        }
        getAdd(1); getAdd(2);
     }
     setInitData();
}


function Save(Type) {
    switch (Type) {
        case 0:
            var obj = MyBase.getTextValueBase("mainContent");
            var obj1 = getHideSpanValue("mainContent", "hideClassSpan");
            MergerObject(obj, obj1);
            var TeachingPlanarry = new Array();
            for (var i = 1; i <= 2; i++) {
                for (var j = 1; j <= $("#table" + i + ">tbody>tr").length; j++) {
                    var data = {};
                    data.PlanOrder = i;
                    data.DetailOrder = j;
                    data.TPDID = $("#" + i + "_" + j + "TPDID").html();
                    data.TargetMain = $("#" + i + "_" + j + "TargetShort").val();
                    data.TargetContent = $("#" + i + "_" + j + "TargetContent").val();
                    data.PlanExecutionDate1 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate1").val());
                    data.PlanExecutionDate2 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate2").val());
                    data.PlanExecutionDate3 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate3").val());
                    data.PlanExecutionDate4 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate4").val());
                    data.PlanExecutionDate5 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate5").val());
                    data.Assessment1 = $("#" + i + "_" + j + "Assessment1").val();
                    data.Assessment2 = $("#" + i + "_" + j + "Assessment2").val();
                    data.Assessment3 = $("#" + i + "_" + j + "Assessment3").val();
                    data.Assessment4 = $("#" + i + "_" + j + "Assessment4").val();
                    data.Assessment5 = $("#" + i + "_" + j + "Assessment5").val();
                    data.Performance1 = $("#" + i + "_" + j + "Performance1").val();
                    data.Performance2 = $("#" + i + "_" + j + "Performance2").val();
                    data.Performance3 = $("#" + i + "_" + j + "Performance3").val();
                    data.Performance4 = $("#" + i + "_" + j + "Performance4").val();
                    data.Performance5 = $("#" + i + "_" + j + "Performance5").val();
                    TeachingPlanarry[TeachingPlanarry.length] = data;

                }
            }
            obj.SingleClassShortTermTarget = TeachingPlanarry;
            AspAjax.CreateSingleTeachCase(obj);
            break;
        case 1:
            var obj = MyBase.getTextValueBase("mainContent");
            var obj1 = getHideSpanValue("mainContent", "hideClassSpan");
            MergerObject(obj, obj1);
            var TeachingPlanarry = new Array();
            for (var i = 1; i <= 2; i++) {
                for (var j = 1; j <= $("#table" + i + ">tbody>tr").length; j++) {
                    var data = {};
                    data.PlanOrder = i;
                    data.DetailOrder = j;
                    data.TPDID = $("#" + i + "_" + j + "TPDID").html();
                    data.TargetMain = $("#" + i + "_" + j + "TargetShort").val();
                    data.TargetContent = $("#" + i + "_" + j + "TargetContent").val();
                    data.PlanExecutionDate1 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate1").val());
                    data.PlanExecutionDate2 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate2").val());
                    data.PlanExecutionDate3 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate3").val());
                    data.PlanExecutionDate4 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate4").val());
                    data.PlanExecutionDate5 = TransformRepublicReturnValue($("#" + i + "_" + j + "PlanExecutionDate5").val());
                    data.Assessment1 = $("#" + i + "_" + j + "Assessment1").val();
                    data.Assessment2 = $("#" + i + "_" + j + "Assessment2").val();
                    data.Assessment3 = $("#" + i + "_" + j + "Assessment3").val();
                    data.Assessment4 = $("#" + i + "_" + j + "Assessment4").val();
                    data.Assessment5 = $("#" + i + "_" + j + "Assessment5").val();
                    data.Performance1 = $("#" + i + "_" + j + "Performance1").val();
                    data.Performance2 = $("#" + i + "_" + j + "Performance2").val();
                    data.Performance3 = $("#" + i + "_" + j + "Performance3").val();
                    data.Performance4 = $("#" + i + "_" + j + "Performance4").val();
                    data.Performance5 = $("#" + i + "_" + j + "Performance5").val();
                    TeachingPlanarry[TeachingPlanarry.length] = data;

                }
            }
            obj.ID = GetQueryString("id"); ;
            obj.SingleClassShortTermTarget = TeachingPlanarry;
           // alert(obj.ID);
            AspAjax.UpdateSingleTeachCase(obj);
            break;
    }


}
