var MyBase = new Base();
var checkedus = [false, true];

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
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
            window.location.href = './student_tracked.aspx';
        }
    }
});
function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "CreatVoiceDistanceDatabase":
            if (parseInt(result) > 0) {
                alert("新增完成");
                window.location = "./voice_distance.aspx?id=" + parseInt(result) + "&act=2";
            }
            break;
        case "UpdateVoiceDistance":
            if (parseInt(result) > 0) {
                alert("更新完成");
                window.location = "./voice_distance.aspx?id=" + parseInt(result) + "&act=2";
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
        case "SearchVoiceDistanceCount":
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
                        AspAjax.SearchVoiceDistance(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchVoiceDistance":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    var date = new Date();
                    for (var i = 0; i < result.length; i++) {
                        var stuAge = BirthdayStringDateFunction(result[i].txtstudentbirthday.toLocaleDateString("ja-JP").replace("/", "-"));
                        inner += '<tr>' +
                                '<td>' + result[i].RowNum + '</td>' +
                                '<td>' + result[i].AcademicYear + '</td>' +
                                '<td>' + result[i].AcademicTerm + '</td>' +
			                    '<td>' + result[i].StudentName + '</td>' +
			                    '<td>' + result[i].txtstudentbirthday.toLocaleDateString("tw") + '</td>' +
			                    '<td>' + stuAge[0] + '歲' + stuAge[1] + '個月</td>';

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
        case "GetVoiceDistanceCount":
            var pageCount = parseInt(result[0]);
            $("#PerceiveDIV").children("div").empty();
            var addvalue = "";
            for (var i = pageCount + 1; i >= 1; i--) {
                addvalue += '<div class="Perceive">' +
                            '<table id="tableContact' + i + '" class="tableContact" width="250" border="0"><caption>語音距離察覺圖</caption>' +
                            '<tr><th width="50">&nbsp</th>';
                for (var j = 1; j <= 5; j++) {
                    addvalue += '<th width="40"><input id="p' + i + '_up' + j + '" style="width:27px;" class="name" type="text"/></th>';
                }
                addvalue += '</tr>';
                for (var list = 1; list <= 5; list++) {
                    addvalue += '<tr><th><input id="p' + i + '_Q' + list + '" style="width:27px;" class="perceiveRight" type="text" /><input id="p' + i + '_hidID' + list + '" type="text"  class="hideClassSpan"/></th>';
                    for (var j = 1; j <= 5; j++) {
                        addvalue += '<td><input type="checkbox"  id="p' + i + '_Q' + list + '_A' + j + '" ></td>';
                    }
                    addvalue += '</tr>';
                }
                addvalue += '<tr><th>日期</th><td colspan="5"><input id="p' + i + '_date" class="date" type="text" name="name" size="10" /></td></tr>';
                addvalue += '<tr><th>備註</th><td colspan="5"><input id="p' + i + '_remark" type="text" name="name" /></td></tr>';
                addvalue += '</table></div>';
            }
            //document.write(addvalue);
            $("#PerceiveDIV").html(addvalue);
            setInitData();
            $(".btnUpdate").fadeIn();
            $("input").add("select").add("textarea").attr("disabled", true);
            var id = GetQueryString("id");
            AspAjax.showVoiceDistance(id);
            break;
        case "showVoiceDistance":
            if (!(result == null || result.length == 0 || result == undefined)) {
                $("#studentName").val(result[0].StudentName);
                $("#StudentID").html(result[0].StudentID);
                $("#AcademicYear").val(result[0].AcademicYear);
                $("#AcademicTerm").val(result[0].AcademicTerm);
                $("#studentbirthday").html(result[0].txtstudentbirthday.toLocaleDateString("tw"));
                for (var i = 0; i < result.length; i++) {
                    var ups = result[i].up.split('|');
                    var ans = result[i].Anser.split('|');
                    for (var j = 1; j <= 5; j++) {
                        $("#p" + result[i].ListOrder + "_up" + j).val(ups[j - 1]);
                        $("#p" + result[i].ListOrder + "_Q" + result[i].RowNum + "_A" + j).attr("checked", checkedus[ans[j - 1]]);
                    }
                    $("#p" + result[i].ListOrder + "_hidID" + result[i].RowNum).val(result[i].HidID);
                    $("#p" + result[i].ListOrder + "_Q" + result[i].RowNum).val(result[i].Question);
                    $("#p" + result[i].ListOrder + "_date").val(result[i].Date.toLocaleDateString("tw"));
                    $("#p" + result[i].ListOrder + "_remark").val(result[i].remark);
                }
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function getView(id) {
    window.open("./voice_distance.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        AspAjax.GetVoiceDistanceCount(id);
       
        
//        $("#studentName").val("王小明");
    } else if (id == null && act == 1) {
    $("#studentName").unbind("click").click(function() { callStudentSearchfunction(); });
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

function Search() { //主頁面 顯示名單
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");

    AspAjax.SearchVoiceDistanceCount(obj);

}


function Update() {
    alert(1);
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    var Q1Data = new Array();
    var obj = new Array();
    var num = 0;
    if (id != null) {

        if ($("#StudentID").html() != null && $("#StudentID").html() != "" && $("#AcademicYear").val() != "" && $("#AcademicTerm").val() != "") {
            $(".tableContact").each(function() {//取得題目編號參數

                var data = {};
                data.id = $(this).attr("ID").replace("tableContact", "");

                Q1Data[Q1Data.length] = data;
            });

            for (var page = 0; page < Q1Data.length; page++) {
                num = parseInt(num) > parseInt(Q1Data[page].id) ? parseInt(num) : parseInt(Q1Data[page].id);
            }

            for (var i = 1; i <= 5; i++) {
                var data = {};
                data.ID = id;
                data.date = $("#p" + num + "_date").val();
                data.remark = $("#p" + num + "_remark").val();
                data.Question = $("#p" + num + "_Q" + i).val();
                data.ListOrder = num;
                var ans = "";
                var up = "";
                for (var j = 1; j <= 5; j++) {

                    ans += ($("#p" + num + "_Q" + i + "_A" + j).attr("checked") == "checked" ? "1" : "0") + "|";
                    up += $("#p" + num + "_up" + j).val() + "|";
                }
                //alert(ans);
                data.Anser = ans;
                data.up = up;
                obj[obj.length] = data;
            }
            //alert(obj[0].ans);
            AspAjax.InsertVoiceDistance(obj);

            obj.length = 0;
            for (var page = 1; page < num; page++) {
                for (var i = 1; i <= 5; i++) {
                    var data = {};
                    data.ID = id;
                    data.AcademicYear = $("#AcademicYear").val();
                    data.AcademicTerm = $("#AcademicTerm").val();
                    data.date = $("#p" + page + "_date").val();
                    data.remark = $("#p" + page + "_remark").val();
                    data.Question = $("#p" + page + "_Q" + i).val();
                    data.ListOrder = page;
                    data.HidID = $("#p" + page + "_hidID" + i).val();
                    var ans = "";
                    var up = "";
                    for (var j = 1; j <= 5; j++) {

                        ans += ($("#p" + page + "_Q" + i + "_A" + j).attr("checked") == "checked" ? "1" : "0") + "|";
                        up += $("#p" + page + "_up" + j).val() + "|";
                    }
                    //alert(ans);
                    data.Anser = ans;
                    data.up = up;
                    obj[obj.length] = data;
                }
            }
            AspAjax.UpdateVoiceDistance(obj);

        }
        else {
            alert(1);
            $("#studentName").focus();
        }

    }



//    var Q1Data = new Array();
//    var Q1myData = new Array();
//    for (var page = 1; page <= 4; page++) {
//        $(".P" + page + "Question").each(function() {//取得題目編號參數
//            var data = {};
//            data.id = $(this).attr("ID").replace("P" + page + "Question", "");
//            Q1Data[Q1Data.length] = data;
//        });
//        for (var i = 1; i < 9; i++) {//記錄 日期 助聽器 答案
//            var data = {};
//            if ($("#tableContact" + page).find("#P" + page + "checkdate" + i).val() != "") {
//                data.StudentID = $("#studentName").val();
//                data.Date = $("#tableContact" + page).find("#P" + page + "checkdate" + i).val();
//                data.Tool = $("#tableContact" + page).find("#P" + page + "tool" + i).val();
//                data.page = page;
//                data.anser = "";
//                for (var j = 0; j < Q1Data.length; j++) {
//                    if ($("#tableContact" + page).find("#P" + page + "Q" + Q1Data[j].id + "_" + i).val() != "") {//有勾選才記錄-減少資源浪費
//                        if (data.anser.length != 0) {
//                            data.anser += "|";
//                        }
//                        data.anser += "P" + page + "Q" + Q1Data[j].id + "_" + i + "#" + $("#tableContact" + page).find("#P" + page + "Q" + Q1Data[j].id + "_" + i).val();
//                    }
//                }
//                Q1myData[Q1myData.length] = data;
//            }
//        }
//        Q1Data.length = 0;
//    }
    //AspAjax.updateHearLossDataBase(Q1myData, _StuID);

}




function Save() { //暫時先寫 之後再修正
    var id = GetQueryString("id");
    var act = GetQueryString("act");
    var obj = new Array();
    if (id == null) {
        if ($("#StudentID").html() != null && $("#StudentID").html() != "" && $("#AcademicYear").val() != "" && $("#AcademicTerm").val() != "") {
            //var obj = MyBase.getTextValueBase("tableContact1");
            var Q1myData = new Array();
            for (var i = 1; i <= 5; i++) {
                var data = {};
                data.StudentID = $("#StudentID").html();
                data.AcademicYear = $("#AcademicYear").val();
                data.AcademicTerm = $("#AcademicTerm").val();
                data.date = $("#p1_date").val();
                data.remark = $("#p1_remark").val();
                data.Question = $("#p1_Q" + i).val();
                data.ListOrder = 1;
                var ans = "";
                var up = "";
                for (var j = 1; j <= 5; j++) {

                    ans += ($("#p1_Q" + i + "_A" + j).attr("checked") == "checked" ? "1" : "0") + "|";
                    up += $("#p1_up" + j).val() + "|";
                }
                //alert(ans);
                data.Anser = ans;
                data.up = up;
                obj[obj.length] = data;
            }
            AspAjax.CreatVoiceDistanceDatabase(obj);
            //            AspAjax.CreatCaseStudy(obj);
        }
        else {
            alert(1);
            $("#studentName").focus();
        }

    }
}

