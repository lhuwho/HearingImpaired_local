var MyBase = new Base();
var _ColumnID = 0;
$(document).ready(function() {
AspAjax.set_defaultSucceededCallback(SucceededCallback);
AspAjax.set_defaultFailedCallback(FailedCallback);
initPage();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

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
    
    $("#ParticipantTeacheName").unbind("click").click(function() { callTeacherSearchfunction(2); });
    $("#ParticipantSocialWorkerName").unbind("click").click(function() { callTeacherSearchfunction(3); });
    $("#ParticipantAudiologistName").unbind("click").click(function() { callTeacherSearchfunction(4); });
    $("#ParticipantHeadName").unbind("click").click(function() { callTeacherSearchfunction(5); });
    
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './isp_meet_list.aspx';
        }
    }
});
function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "UpdateCaseISPRecord":
            if (result[0].ID != null) {
                alert("更新成功");
                window.location = "./isp_meet_list.aspx?id=" + result[0].ID + "&act=2";
            }
            break;
        case "CreatCaseISPRecord":
            if (result[0].ID != null) {
                alert("新增成功");
                window.location = "./isp_meet_list.aspx?id=" + result[0].ID + "&act=2";
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
                    $("#studentCID").html(result.Column);
                    $("#StudentID").html(result.StudentData.ID);
                    var stuAge = BirthdayStringDateFunction(result.StudentData.studentbirthday);
                    $("#StudentAge").val(stuAge[0]);
                    $("#StudentMonth").val(stuAge[1]);
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
                                $("#TeacherID").html(id);
                                $("#teacherName").val(name);
                                break;
                            case "2":
                                $("#ParticipantTeache").html(id);
                                $("#ParticipantTeacheName").val(name);
                                break;
                            case "3":
                                $("#ParticipantSocialWorker").html(id);
                                $("#ParticipantSocialWorkerName").val(name);
                                break;
                            case "4":
                                $("#ParticipantAudiologist").html(id);
                                $("#ParticipantAudiologistName").val(name);
                                break;
                            case "5":
                                $("#ParticipantHead").html(id);
                                $("#ParticipantHeadName").val(name);
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
        case "SearchCaseISPRecordCount":
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
                        AspAjax.SearchCaseISPRecord(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchCaseISPRecord":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    var date = new Date();
                    for (var i = 0; i < result.length; i++) {
                        var col4 = "";
                        result[i].ParticipantParent.length > 0 && (col4 += (col4.length > 0 ? (",") : ("")) + result[i].ParticipantParent);
                        result[i].ParticipantTeacheName.length > 0 && (col4 += (col4.length > 0 ?( ","): ("") ) + result[i].ParticipantTeacheName);
                        result[i].ParticipantSocialWorkerName.length > 0 && (col4 += (col4.length > 0 ? (",") : ("")) + result[i].ParticipantSocialWorkerName);
                        result[i].ParticipantAudiologistName.length > 0 && (col4 += (col4.length > 0 ? (",") : ("")) + result[i].ParticipantAudiologistName);
                        result[i].ParticipantHeadName.length > 0 && (col4 += (col4.length > 0 ? (",") : ("")) + result[i].ParticipantHeadName);
                        result[i].ParticipantProfessionals.length > 0 && (col4 += (col4.length > 0 ? (",") : ("")) + result[i].ParticipantProfessionals);
                        inner += '<tr>' +
                                '<td>' + result[i].RowNum + '</td>' +
			                    '<td>' + result[i].StudentName + '</td>' +
			                    '<td>' + (result[i].ConventionDate == "001/1/1" ? ('') : (result[i].ConventionDate) )+ '</td>' +
			                     '<td>' + result[i].ConventionName + '</td>' +
                                 '<td>' + col4 + '</td>'
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
        case "ShowCaseIspRecord":
            if (!(result == null || result.length == 0 || result == undefined)) {
                for (var i = 0; i < result.length; i++) {
                    if (result[i].ThisValue != "001/1/1") {
                        $("#" + result[i].IDname).val(result[i].ThisValue);
                        $("input[name=" + result[i].IDname + "][value='" + result[i].ThisValue + "']").attr('checked', true);
                    }
                    if (result[i].IDname == "StudentID" || result[i].IDname == "TeacherID" || result[i].IDname == "ParticipantTeache" || result[i].IDname == "ParticipantSocialWorker" || result[i].IDname == "ParticipantAudiologist" || result[i].IDname == "ParticipantHead") {
                        $("#" + result[i].IDname).html(result[i].ThisValue);
                    }
                }

            } else {
                //alert("查無資料");
               // window.close();
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}


function getView(id) {
    window.open("./isp_meet_list.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainClass").html("教學管理&gt; 個別化服務計畫(ISP)會議記錄");
    document.title = "教學管理 - 個別化服務計畫(ISP)會議記錄";
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.ShowCaseIspRecord(id);
//        $("#studentName").val("王小明");
//        $("#teacherName").val("連淑貞");
    } else if (id == null && act == 1) {
    $("#studentName").unbind("click").click(function() { callStudentSearchfunction(); });
    $("#teacherName").unbind("click").click(function() { callTeacherSearchfunction("1"); });
    
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
			        if(getid == 1){ inner += '<select id="gosrhstaffJob" style="display:none;" ><option value="14">教師</option></select>';}			           
//			            '<tr><td>職　　稱 <select id="gosrhstaffJob"></select></td></tr>' +
//			            '<tr><td>派任單位 <select id="gosrhstaffUnit"></select></td></tr>' +
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

    AspAjax.SearchCaseISPRecordCount(obj);

}

function Update() {
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null) {
        if ($("#StudentID").html() != null && $("#StudentID").html() != "") {
            var obj = MyBase.getTextValueBase("tableContact1");
            obj.StudentID = $("#StudentID").html();
            obj.TeacherID = $("#TeacherID").html();
            obj.ParticipantTeache = $("#ParticipantTeache").html();
            obj.ParticipantSocialWorker = $("#ParticipantSocialWorker").html();
            obj.ParticipantAudiologist = $("#ParticipantAudiologist").html();
            obj.ParticipantHead = $("#ParticipantHead").html();
            
            
            obj.ID = id;

            AspAjax.UpdateCaseISPRecord(obj);
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
        if ($("#StudentID").html() != null && $("#StudentID").html() != "") {
            var obj = MyBase.getTextValueBase("tableContact1");
            obj.StudentID = $("#StudentID").html();
            obj.TeacherID = $("#TeacherID").html();
            obj.ParticipantTeache = $("#ParticipantTeache").html();
            obj.ParticipantSocialWorker = $("#ParticipantSocialWorker").html();
            obj.ParticipantAudiologist = $("#ParticipantAudiologist").html();
            obj.ParticipantHead = $("#ParticipantHead").html();
            AspAjax.CreatCaseISPRecord(obj);
        }
        else {
            $("#studentName").focus();
        }

    }
}
