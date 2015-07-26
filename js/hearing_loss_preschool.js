var MyBase = new Base();
var noEmptyItem = ["studentName"];
var noEmptyShow = ["學生姓名"];
var noEmptyItem3 = ["resourceDate", "resourceID"];
var noEmptyShow3 = ["日期", "資源卡編號"];
var noEmptyItem8 = ["RecordDate", "RecordHeight", "RecordWeight"];
var noEmptyShow8 = ["紀錄日期", "身高", "體重"];
var _ColumnID = 0;
var _StuID = 0;
var _staffIndex;
var _ReturnStu8Value;

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    //AspAjax.set_defaultFailedCallback(FailedCallback);
    $("#item1").add("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#item1Content").fadeIn();
    initPage();
    
    for (var i = 1; i < 5; i++)
    { 
        AspAjax.GetLossToolQuestion(i);
    }
    AspAjax.GetLossSkillQuestion();
    
    
    setEvents();


});
function setEvents() {
    $("#first1").click(function() {
            
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
                for (var page = 1; page <= 4; page++) {
                   
                    AspAjax.searchHearLossDataBase(_StuID, page);
                }
                $("#item1").click();
    });
    $(".menuTabs").click(function() {
        $(".menuTabs").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        //$("input").add("select").add("textarea").attr("disabled", true);
        if (_MainID == null && act == 1) {
            //$("input").add("select").add("textarea").attr("disabled", true);
        } else {
//            $(".btnSaveUdapteData").add(".btnCancel").hide();
//            $(".btnUpdate").fadeIn();
            //$("input").add("select").add("textarea").attr("disabled", true);
            var index = ($(this).attr("id")).replace("item", "");
            var intIndex = parseInt(index);
//            if (!isNaN(intIndex)) {
//                for (var page = 1; page <= 4; page++) { 
//                AspAjax.searchHearLossDataBase(_StuID,page);
//                }
//            }
        }
        //getStudentData(this.id);
        //haveRolesWaitFunction();
    });
    $(".showUploadImg").fancybox();
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

    $("#btnIndex").click(function() {
        $("#btnSearch").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").hide();
        $("#mainContentIndex").show();
    });

    $("#btnSearch").click(function() {
        $("#btnIndex").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").show();
        $("#mainContentIndex").hide();
    });
}

function addanser1(result) {///助聽器輔助管理
    var rowspan = 0;
    var inner = '';
    var SummeryID = '';
    var rowspanadd = '';
    inner += '<thead><tr><th width="20" rowspan="3">綱要</th><th rowspan="3" width="220">學習目標</th><th colspan="8">評量日期 / 配戴輔具</th></tr><tr>';
    for (var i = 1; i < 9; i++) {
        inner += '<td><textarea id="P1checkdate' + i + '" class="date checkdate" rows="3" cols="8"></textarea></td>';
    }
    inner += '</tr><tr>';
    for (var i = 1; i < 9; i++) {
        inner += '<td><select id="P1tool' + i + '"><option></option><option value="1">HA</option><option value="2">CI</option><option value="3">H+C</option></select></td>';
    }
    inner += '</tr></thead>';
    var rowname = '';
    for (var i = 0; i < result.length; i++) {
        if (rowname != result[i].SummeryDescription) {
            inner += '<tr><th id="P1rowspan' + result[i].SummeryID + '" rowspan="1" >' + result[i].SummeryDescription + '</th>';
            rowspan = 1;
            rowname = result[i].SummeryDescription;
            inner += '<td id="P1Question' + result[i].QuestionID + '" name="P1Question' + result[i].QuestionID + '" class="P1Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P1Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option><option value="4">/</option></select></td>';
            }
            inner += '</tr>';
        }

        else {
            rowspan++;
            inner += '<tr><td id="P1Question' + result[i].QuestionID + '" name="P1Question' + result[i].QuestionID + '" class="P1Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P1Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option><option value="4">/</option></select></td>';
            }
            inner += '</tr>';
        }
        SummeryID += result[i].SummeryID + ',';
        rowspanadd += rowspan + ',';
    }

    $("#tableContact1").html(inner);
    for (var i = 0; i < rowspanadd.split(",").length - 1; i++) {
        $("#P1rowspan" + SummeryID.split(",")[i]).attr("rowspan", rowspanadd.split(",")[i]);
    }
    setInitData();

}

function addanser2(result) {///聽覺技巧
    var inner = '';
    inner += '<thead><tr><th width="150" rowspan="2" colspan="2">測驗項目</th><th colspan="8">評量日期 / 結果</th></tr><tr>';
    for (var i = 1; i < 9; i++) {
        inner += '<td><textarea id="P2checkdate' + i + '" class="date checkdate" rows="3" cols="8"></textarea></td>';
    }
    inner += '</tr></thead>';
    for (var i = 0; i < result.length; i++) {
        inner += '<td width="50" id="P2Question' + result[i].SkillID + '" class="P2Question" >' + result[i].Title + '</td><td>' + result[i].SkillDescription + '</td>';
        for (var j = 1; j < 9; j++) {
            inner += '<td><select id="P2Q' + result[i].SkillID + '_' + j + '"><option></option><option value="1">V</option><option value="2">X</option></select></td>';
        }
        inner += '</tr>';

    }
    $("#tableContact2").html(inner);
    setInitData();
}
function addanser3(result) {///認知(上)
    var rowspan = 0;
    var inner = '';
    var SummeryID = '';
    var rowspanadd = '';
    inner += '<thead><tr><th width="20" rowspan="3">綱要</th><th rowspan="3" width="150">學習目標</th><th colspan="8">能力對照圖（參考年齡）</th></tr><tr>';
    for (var i = 1; i < 9; i++) {
        inner += '<td><textarea id="P3checkdate' + i + '" class="date checkdate" rows="3" cols="8" style="width:95%;"></textarea></td>';
    }
    inner += '</tr><tr>';
//    for (var i = 1; i < 9; i++) {
//        inner += '<td><select id="P1tool' + i + '"><option></option><option value="1">HA</option><option value="2">CI</option><option value="3">H+C</option></select></td>';
//    }
    inner += '</tr></thead>';
    inner += '<tr><td colspan="10">副領域：注意力</td></tr>';
    var rowname = '';
    for (var i = 0; i < result.length; i++) {
        if (rowname != result[i].SummeryDescription) {
            inner += '<tr><th id="P3rowspan' + result[i].SummeryID + '" rowspan="1" >' + result[i].SummeryDescription + '</th>';
            rowspan = 1;
            rowname = result[i].SummeryDescription;
            inner += '<td id="P3Question' + result[i].QuestionID + '" name="P3Question' + result[i].QuestionID + '" class="P3Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P3Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option></select></td>';
            }
            inner += '</tr>';
        }

        else {
            rowspan++;
            inner += '<tr><td id="P3Question' + result[i].QuestionID + '" name="P3Question' + result[i].QuestionID + '" class="P3Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P3Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option></select></td>';
            }
            inner += '</tr>';
        }
        SummeryID += result[i].SummeryID + ',';
        rowspanadd += rowspan + ',';
    }
    

    $("#tableContact3").html(inner);
    for (var i = 0; i < rowspanadd.split(",").length - 1; i++) {
        $("#P3rowspan" + SummeryID.split(",")[i]).attr("rowspan", rowspanadd.split(",")[i]);
    }
    setInitData();

}
function addanser4(result) {///認知(下)
    var rowspan = 0;
    var inner = '';
    var SummeryID = '';
    var rowspanadd = '';
    inner += '<tr><td colspan="10">副領域：基本概念</td></tr>';
    var rowname = '';
    for (var i = 0; i < result.length; i++) {
        if (rowname != result[i].SummeryDescription) {
            inner += '<tr><th id="P3rowspan' + result[i].SummeryID + '" rowspan="1" >' + result[i].SummeryDescription + '</th>';
            rowspan = 1;
            rowname = result[i].SummeryDescription;
            inner += '<td id="P3Question' + result[i].QuestionID + '" name="P3Question' + result[i].QuestionID + '" class="P3Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P3Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option></select></td>';
            }
            inner += '</tr>';
        }

        else {
            rowspan++;
            inner += '<tr><td id="P3Question' + result[i].QuestionID + '" name="P3Question' + result[i].QuestionID + '" class="P3Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P3Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option></select></td>';
            }
            inner += '</tr>';
        }
        SummeryID += result[i].SummeryID + ',';
        rowspanadd += rowspan + ',';
    }


    $("#tableContact3").append(inner);
    for (var i = 0; i < rowspanadd.split(",").length - 1; i++) {
        $("#P3rowspan" + SummeryID.split(",")[i]).attr("rowspan", rowspanadd.split(",")[i]);
    }
    setInitData();

}
function addanser5(result) {///表達性語言
    var rowspan = 0;
    var inner = '';
    var SummeryID = '';
    var rowspanadd = '';
    inner += '<thead><tr><th width="20" rowspan="3">綱要</th><th rowspan="3" width="150">學習目標</th><th colspan="8">能力對照圖（參考年齡）</th></tr><tr>';
    for (var i = 1; i < 9; i++) {
        inner += '<td><textarea id="P4checkdate' + i + '" class="date checkdate" rows="3" cols="8"  style="width:95%;" ></textarea></td>';
    }
    inner += '</tr><tr>';
    //    for (var i = 1; i < 9; i++) {
    //        inner += '<td><select id="P1tool' + i + '"><option></option><option value="1">HA</option><option value="2">CI</option><option value="3">H+C</option></select></td>';
    //    }
    inner += '</tr></thead>';
    inner += '<tr><td colspan="10">副領域：言語表達</td></tr>';
    var rowname = '';
    for (var i = 0; i < result.length; i++) {
        if (rowname != result[i].SummeryDescription) {
            inner += '<tr><th id="P4rowspan' + result[i].SummeryID + '" rowspan="1" >' + result[i].SummeryDescription + '</th>';
            rowspan = 1;
            rowname = result[i].SummeryDescription;
            inner += '<td id="P4Question' + result[i].QuestionID + '" name="P4Question' + result[i].QuestionID + '" class="P4Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P4Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option></td>';
            }
            inner += '</tr>';
        }

        else {
            rowspan++;
            inner += '<tr><td id="P4Question' + result[i].QuestionID + '" name="P4Question' + result[i].QuestionID + '" class="P4Question" >' + result[i].QuestionDescription + '</td>';
            for (var j = 1; j < 9; j++) {
                inner += '<td><select id="P4Q' + result[i].QuestionID + '_' + j + '"><option></option><option value="1">v</option><option value="2">Δ</option><option value="3">X</option></td>';
            }
            inner += '</tr>';
        }
        SummeryID += result[i].SummeryID + ',';
        rowspanadd += rowspan + ',';
    }

    $("#tableContact4").html(inner);
    for (var i = 0; i < rowspanadd.split(",").length - 1; i++) {
        $("#P4rowspan" + SummeryID.split(",")[i]).attr("rowspan", rowspanadd.split(",")[i]);
    }
    setInitData();

}



function SucceededCallback(result, userContext, methodName) {

    switch (methodName) {
        case "ShowStudent": //added by AWho
            //alert(1);
             $("#studentName").val(result[0].txtstudentName);
           // alert(result[0].txtstudentName);
            break;
        case "searchHearLossDataBase": //added by aaron
                setValue(result);
            break;
        case "GetLossToolQuestion":
            if (result[0].Category.length > 0) {
                switch (result[0].Category) {
                    case "1":
                        addanser1(result);
                        break;
                    case "2":
                        addanser3(result);
                        break;
                    case "3":
                        addanser4(result);
                        break;
                    case "4":
                        addanser5(result);
                        break;
                }

            }

            break;
        case "GetLossSkillQuestion":
            if (result[0].SkillID.length > 0) {
                addanser2(result);
            }

            break;
        case "SearchHearingLossPreschoolCount":
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
                        AspAjax.SearchStudentDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    var date = new Date();
                    for (var i = 0; i < result.length; i++) {
                        
                        var stuAge = BirthdayStringDateFunction(result[i].txtstudentbirthday.toLocaleDateString("ja-JP").replace("/", "-"));
                        inner += '<tr>' +
                                '<td>' + result[i].ID + '</td>' +
//			                    '<td>' + _CaseStatu[result[i].txtstudentStatu] + '</td>' +
//			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    '<td>' + YearChange(result[i].txtstudentbirthday.toLocaleDateString("tw"))+ '</td>' +
			                    '<td>' + stuAge[0] +'歲'+ stuAge[1] + '個月</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
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
        case "updateStudentTemperatureDataBase":
            break;
    }
}



function getView(id) {
    window.open("./hearing_loss_preschool.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        _StuID = id;
        $(".btnUpdate").fadeIn();
        //$("input").add("select").add("textarea").attr("disabled", true);
        $("#first1").click();
       
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.ShowStudent(id);
    } 
    else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    }
}
function Search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    
    AspAjax.SearchHearingLossPreschoolCount(obj);

}

function saveToolAns() {
    var Q1Data = new Array();
    var Q1myData = new Array();
    for (var page = 1; page <= 4; page++) {
        $(".P" + page + "Question").each(function() {//取得題目編號參數
            var data = {};
            data.id = $(this).attr("ID").replace("P" + page + "Question", "");
            Q1Data[Q1Data.length] = data;
        });
        for (var i = 1; i < 9; i++) {//記錄 日期 助聽器 答案
            var data = {};
            if ($("#tableContact"+page).find("#P"+page+"checkdate" + i).val() != "") {
                data.StudentID = $("#studentName").val();
                data.Date = $("#tableContact" + page).find("#P" + page + "checkdate" + i).val();
                data.Tool = $("#tableContact" + page).find("#P" + page + "tool" + i).val();
                data.page = page;
                data.anser = "";
                for (var j = 0; j < Q1Data.length; j++) {
                    if ($("#tableContact" + page).find("#P" + page + "Q" + Q1Data[j].id + "_" + i).val() != "") {//有勾選才記錄-減少資源浪費
                        if (data.anser.length != 0) {
                            data.anser += "|";
                        }
                        data.anser += "P"+page+"Q" + Q1Data[j].id + "_" + i + "#" + $("#tableContact"+page).find("#P"+page+"Q" + Q1Data[j].id + "_" + i).val();
                    }
                }
                Q1myData[Q1myData.length] = data;
            }
        }
         Q1Data.length = 0;
     }
    /////單一存檔(可考慮繼續使用)
//    $(".P1Question").each(function() {//取得題目編號參數
//        var data = {};
//        data.id = $(this).attr("ID").replace("P1Question", "");
//        Q1Data[Q1Data.length] = data;
//    });
//    for (var i = 1; i < 9; i++) {//記錄 日期 助聽器 答案
//        var data = {};
//        if ($("#tableContact1").find("#P1checkdate" + i).val() != "") {
//            data.StudentID = $("#studentName").val();
//            data.Date = $("#tableContact1").find("#P1checkdate" + i).val();
//            data.Tool = $("#tableContact1").find("#P1tool" + i).val();
//            data.anser = "";    
//            for (var j = 0; j < Q1Data.length; j++) {
//                if ($("#tableContact1").find("#P1Q" + Q1Data[j].id + "_" + i).val() != "") {//有勾選才記錄-減少資源浪費
//                    if (data.anser.length != 0) {
//                        data.anser += "|";
//                    }
//                    data.anser += "P1Q" + Q1Data[j].id + "_" + i + "#" + $("#tableContact1").find("#P1Q" + Q1Data[j].id + "_" + i).val();
//                }
//            }
//            Q1myData[Q1myData.length] = data;
//        }
//    }
    AspAjax.updateHearLossDataBase(Q1myData, _StuID);
    
}
function saveSkillAns() {
    
    var Q2Data = new Array();
    var Q2myData = new Array();
    
    $(".P2Question").each(function() {//取得題目編號參數
        var data = {};
        data.id = $(this).attr("ID").replace("P2Question", "");
        Q2Data[Q2Data.length] = data;
    });
    for (var i = 1; i < 9; i++) {//記錄 日期 助聽器 答案
        var data = {};
        if ($("#tableContact2").find("#P2checkdate" + i).val() != "") {
            data.StudentID = $("#studentName").val();
            data.Date = $("#tableContact2").find("#P2checkdate" + i).val();
            data.anser = "";
            for (var j = 0; j < Q2Data.length; j++) {
                if ($("#tableContact2").find("#P2Q" + Q2Data[j].id + "_" + i).val() != "") {//有勾選才記錄-減少資源浪費
                    if (data.anser.length != 0) {
                        data.anser += "|";
                    }
                    data.anser += "P2Q" + Q2Data[j].id + "_" + $("#tableContact2").find("#P2Q" + Q2Data[j].id + "_" + i).val();
                }
            }
            Q2myData[Q2myData.length] = data;
        }
 
    }
   
    AspAjax.updateHearLossDataBase(Q2myData);

}

function setValue(result) {
    //for (var page = 1; page <= 4; page++) {
        for (var i = 1; i <= result.length; i++) {
            $("#P" + result[i - 1].page + "checkdate" + i).val(result[i - 1].Date);
            $("#P" + result[i - 1].page + "tool" + i).val(result[i - 1].tool);
            var answers = result[i - 1].anser.split('|');
            for (var j = 0; j < answers.length; j++) {
                var ID = answers[j];

                ID = ID.substring(0, ID.indexOf("#"));
               // alert(ID);
                var val = answers[j].replace(ID + "#", "");
                //alert(val);
                console.log(ID + "  " + val);
                $("#" + ID).val(val);
            }

        }
    //}
}