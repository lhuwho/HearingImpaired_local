var MyBase = new Base();
var noEmptyItem = ["courseDate", "courseLecturer", "courseName", "courseProve", "courseCredit","courseCreditTeacherType"];
var noEmptyShow = ["日期", "講師", "主題", "研習證明", "小時", "內部/外聘"];
var _ReturnStaffValue;
var _ReturnValue;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#mainContentIndex").hide();

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

    $(".menuTabs2").click(function() {
        $(".menuTabs2").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").add("#mainContentIndex").add(".mainSearchList").add(".insertDataDiv").hide();
        var DivName = '';
        switch (this.id) {
            case "btnSearch":
                DivName = "#mainContentSearch";
                break;
            case "btnIndex":
                DivName = "#mainContentIndex";
                break;
        }
        $(DivName).add(".btnAdd").fadeIn();
    });

   

    $("select[name='item']").chosen({
        disable_search_threshold: 10,
        display_selected_options: false,
        search_contains: true,
        //max_selected_options: 2, //最大選擇數
        no_results_text: "查無資料", //沒有結果匹配
        width: "125px" //寬度
    });
    $("#author").chosen({
        search_contains: true, //選項模糊查詢
        display_selected_options: false,
        no_results_text: "查無資料", //沒有結果匹配
        width: "130px" //寬度
    });
    $(".chosen-results").css("max-height", "100px");
    $(".chosen-choices").add(".chosen-drop").css("text-align", "left");
    
    
});
function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStaffCreditDataBaseCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                $("#SumCredit").val(result[1]);
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
                        var obj = MyBase.getTextValueBase("searchTable");
                        AspAjax.SearchStaffCreditDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='9'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffCreditDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        //                        inner += ' <tr id="HS_' + result[i].ID + '" class="sStaffData" >' +
                        //                        			                    '<td><input type="text" class="ScourseDate date" value="' + TransformADFromStringFunction(result[i].courseDate) + '" size="9"/></td>' +
                        //                        			                    '<td><input type="text" class="ScourseLecturer" value="' + result[i].courseLecturer + '" size="9"/></td>' +
                        //                        			                    '<td><input type="text" class="ScourseName" value="' + result[i].courseName + '"  /></td>' +
                        //                        			                    '<td><input type="text" class="ScourseTime" value="' + result[i].courseTime + '" size="5" /></td>' +
                        //                        			                    '<td><select class="ScourseProve"><option value="0">請選擇</option><option value="1">會內研習</option><option value="2">會外研習</option><option value="3">委派出席</option></select></td>' +
                        //                        			                    '<td><input type="text" class="ScourseCredit" value="' + result[i].courseCredit + '" size="5" /></td>' +
                        //                        			                    '<td><input type="text" class="SotherExplanation" value="' + result[i].otherExplanation + '" size="20" /></td>' +
                        //                        			                    '<td><button id="sV_' + i + '" class="btnView ScourseStaffView" type="button" onclick="">詳細</button></td>' +
                        //                        			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(\'' + result[i].ID + '\')">更 新</button> <button class="btnDelete" type="button" onclick="DelData(\'' + result[i].ID + '\')">刪 除</button></div>' +
                        //                        			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(\'' + result[i].ID + '\')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].ID + '\',\'' + i + '\')">取 消</button></div>' +
                        //                        			                    '</td>' +
                        //                              			                '</tr>';
                        inner += ' <tr id="HS_' + result[i].ID + '" class="sStaffData" >' +
                                    '<td colspan="7"> ' +

                                    '<table style="width: 100%; border:0;border-collapse: collapse;">' +
                                        '<thead style="line-height: 1px; opacity: 0;"><tr>' +
                                        '<th width="127" ></th>' +
                                        '<th width="160" ></th>' +
                                        '<th width="90" ></th>' +
                                        '<th width="80" ></th>' +
                                        '<th width="88" ></th>' +
                                        '<th width="41" ></th>' +
                                        '<th width="130" ></th>' +
                                        '</tr></thead>' +

                                    '<tbody> ' +
                                    '<tr> ' +
                                         '<td ><select class="ScourseProve"><option value="0">請選擇</option><option value="1">教學/個案研討</option><option value="2">聽力課程研討</option><option value="3">相關專業課程</option><option value="3">一般性研習課程</option><option value="3">行政工作/管理</option></select></td>' +
                                         '<td><input type="text" class="ScourseName" value="' + result[i].courseName + '"  /></td>' +
                                         '<td>' +
                                              '<select class="ScourseCreditTeacherType" disabled="disabled"><option value="1">內部</option><option value="2">外聘</option></select>'+
                                              '<input type="text" class="ScourseLecturer" value="' + result[i].courseLecturer + '" size="9"/>'+
                                         '</td>' +
                                         '<td><input type="text" class="ScourseDate date" value="' + TransformADFromStringFunction(result[i].courseDate) + '" size="9"/></td>' +
                                         '<td > <input type="text" class="courseStartH"  value="' + result[i].courseStartH + '"  size="3" />～<input type="text" class="courseEndH" value="' + result[i].courseEndH + '"  size="3" /></td>' +
                                         '<td ><input type="text" class="ScourseCredit" value="' + result[i].courseCredit + '" size="3" /></td>' +
                                         '<td><textarea class="SotherExplanation" rows="3" cols="15" disabled="disabled">' + result[i].otherExplanation + '</textarea></td>' +
                                    '</tr> ' +

                                    '<tr> ' +
                                        '<td colspan="1">滿意度調查： <br /><input type="radio" name="IsSatisfaction_' + result[i].ID + '" class="ScourseIsSatisfaction" value="1" disabled="disabled" > 有 <input name="IsSatisfaction_' + result[i].ID + '" type="radio" class="ScourseIsSatisfaction" value="0" disabled="disabled"> 無</td>' +
                                        '<td colspan="1" style="text-align: right;">  ' +
                                            '<div>講題滿意百分比：</div>' +
                                            '<div>講師表現滿意百分比：</div>' +
                                        '</td>' +
                                        '<td style="text-align: left;">' +
                                            '<div><input class="ScourseDataQuestion" style="width:40px;" type="text" placeholder="%" value="' + result[i].courseDataQuestion + '" disabled="disabled"></div>' +
                                            '<div><input class="ScourseDataLecturer" style="width:40px;" type="text" placeholder="%" value="' + result[i].courseDataLecturer + '" disabled="disabled"></div>' +
                                        '</td>' +
                                        '<td>出席人員： </td>' +
                                        '<td colspan="2"> ' +
                                        '<label><input type="checkbox" class="ScourseInTeacher" value="1" ' + result[i].courseInTeacher + ' disabled="disabled"> 教師  </label>' +
                                        '<label><input type="checkbox" class="ScourseInAudiologist" value="1" ' + result[i].courseInAudiologist + ' disabled="disabled"> 社工  </label>' +
                                        '<br>'+
                                        '<label><input type="checkbox" class="ScourseInSocialWorkers" value="1" ' + result[i].courseInSocialWorkers + ' disabled="disabled"> 行政 </label>' +
                                        '<label><input type="checkbox" class="ScourseInAdministrative" value="1" ' + result[i].courseInAdministrative + ' disabled="disabled"> 聽力師</label>' +
                                        '</td> ' +
                                        '<td ><button id="sV_' + i + '" class="btnView ScourseStaffView" type="button" onclick="">詳細</button></td>' +
                                    '</tr>' +

                                    '<tr>' +
                                        '<td colspan="5">檢附資料： <label><input type="checkbox" class="ScourseDataLecture" value="1" ' + result[i].courseDataLecture + ' disabled="disabled"> 講義  </label>' +
                                        '<label><input type="checkbox" class="ScourseDataPhoto" value="1" ' + result[i].courseDataPhoto + ' disabled="disabled"> 照片  </label>' +
                                        '<label><input type="checkbox" class="ScourseDataTeaching" value="1" ' + result[i].courseDataTeaching + ' disabled="disabled"> 教學/研討記錄 </label>' +
                                        '<label><input type="checkbox" class="ScourseDataIsp" value="1" ' + result[i].courseDataIsp + ' disabled="disabled"> ISP</label>' +
                                        '<label><input type="checkbox" class="ScourseDataOther" value="1" ' + result[i].courseDataOther + ' disabled="disabled"> 其它</label>'+
                                        '<input class="courseDataOtherText"  value="' + result[i].courseDataOtherText + '" type="text" />' +
                                        '</td>' +
                                    '</tr>' +

                                    
                                    
                        //'<td><input type="text" class="ScourseTime" value="' + result[i].courseTime + '" size="5" /></td>' +
                                    
                                   
                                    
                                   
                                    


                                        '</tbody></table>' +
                                   
                                   
                                    '</td>' +
                                    '<td>' +
                                        '<div class="UD"><button class="btnView" type="button" onclick="UpData(\'' + result[i].ID + '\')">更 新</button>' +
                                        '<button class="btnDelete" type="button" onclick="DelData(\'' + result[i].ID + '\')">刪 除</button></div>' +
                                        '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(\'' + result[i].ID + '\')">儲 存</button>' +
                                        '<button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].ID + '\',\'' + i + '\')">取 消</button></div></td>' +
                                    '</tr>';

                    }

                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList input[type=text]").add("#mainSearchList select").attr("disabled", true);


                    $('.date').datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    for (var i = 0; i < result.length; i++) {
                        $("#HS_" + result[i].ID + " .ScourseProve").children('option[value="' + result[i].courseProve + '"]').attr("selected", true);
                    }
                    
                    for (var i = 0; i < result.length; i++) {
                        $("#HS_" + result[i].ID + " .ScourseCreditTeacherType").children('option[value="' + result[i].courseCreditTeacherType + '"]').attr("selected", true);
                    }

                    for (var i = 0; i < result.length; i++) {
                        $("#HS_" + result[i].ID + " .ScourseIsSatisfaction[value='" + result[i].courseIsSatisfaction + "']").attr('checked', true);
                    }
                    
//                    $(".ScourseTime").add(".ScourseCredit").unbind('keydown').keydown(function(event) {
//                        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 46)) {
//                            event.preventDefault();
//                        }
//                    });
                    $(".ScourseStaffView").unbind("click").click(function() {
                        var index = ($(this).attr("id")).replace("sV_", "");
                        var inner2 = "";
                        for (var i = 0; i < _ReturnStaffValue.length; i++) {
                            inner2 += '<option value="' + _ReturnStaffValue[i].sID + '">' + _ReturnStaffValue[i].sName + '(' + _ReturnStaffValue[i].sID + ')</option>';
                        }

                        var inner = '<div id="inline" style="overflow:auto;height:200px;width:360px"><br />' +
                                    '<select id="ParticipantsSelectview" name="item" data-placeholder="選擇檢查項目..." class="chosen-select" multiple="multiple" style="display:none;">' +
                                    inner2 +
                                    '</select>' +
                                    '<br /><br />' +
                                    '<p align="center"><button class="btnView" type="button" onclick="">儲 存</button></p>' +
                                    '</div>';

                        $.fancybox({
                            'content': inner,
                            'autoDimensions': true,
                            'centerOnScroll': true,
                            'onComplete': function() {
                                $("#ParticipantsSelectview").show();
                                $("#ParticipantsSelectview").chosen({
                                    search_contains: true, //選項模糊查詢
                                    disable_search_threshold: 10,
                                    display_selected_options: false,
                                    no_results_text: "查無資料", //沒有結果匹配
                                    width: "350px" //寬度
                                });

                                $(".chosen-results").css("max-height", "100px");
                                $(".chosen-choices").add(".chosen-drop").css("text-align", "left");
                                var oldParticipants = new Array();
                                for (var i = 0; i < result[index].Participants.length; i++) {
                                    var itemValue = result[index].Participants[i];
                                    $("#ParticipantsSelectview").children('option[value="' + itemValue.sID + '"]').attr("selected", true);
                                    oldParticipants.push(itemValue.sID);
                                }
                                $("#ParticipantsSelectview").trigger('chosen:updated');

                                $("#inline .btnView").unbind("click").click(function() {
                                    var newParticipants = new Array();
                                    $("#ParticipantsSelectview").find(':selected').each(function() {
                                        newParticipants.push($(this).val());
                                    });
                                    //oldSize
                                    var DelParticipantsValue = new Array();
                                    var NewParticipantsValue = new Array();
                                    DelParticipantsValue = array_diff(oldParticipants, newParticipants);  //被取消的
                                    NewParticipantsValue = array_diff(newParticipants, oldParticipants);  //新增加的

                                    var DelParticipantsID = new Array();
                                    for (var i = 0; i < DelParticipantsValue.length; i++) {
                                        var itemindex = oldParticipants.indexOf(DelParticipantsValue[i]);
                                        if (index != -1 && itemindex != -1) {
                                            DelParticipantsID.push(result[index].Participants[itemindex].ID);
                                        }
                                    }
                                    if (DelParticipantsID.length != 0 || NewParticipantsValue.length != 0) {
                                        AspAjax.setStaffCreditParticipantDataBase(result[index].ID, DelParticipantsID, NewParticipantsValue);
                                    } else {
                                        alert("");
                                    }
                                });
                            }
                        });
                    });

                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='9'>查無資料</td></tr>");
            }
            break;
        case "getAllStaffDataList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnStaffValue = result;
                    for (var i = 0; i < result.length; i++) {
                        $("#ParticipantsSelect").append($('<option></option>').attr("value", result[i].sID).text(result[i].sName + "(" + result[i].sID + ")"));
                        $("#author").append($('<option></option>').attr("value", result[i].sID).text(result[i].sName + "(" + result[i].sID + ")"));
                    }
                    $("#ParticipantsSelect").add("#author").trigger('chosen:updated');
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#ParticipantsSelect").find("option").empty();
                $("#author").find("option").empty();
            }
            break;
        case "createStaffCreditDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                if (parseInt(result[1]) <= 0) {
                    alert("發生錯誤，請重新整理頁面");
                } else {
                    alert("新增成功");
                    //window.location.reload();
                }
            }
            break;        
        case "setStaffCreditParticipantDataBase":
            $.fancybox.close();
        case "setStaffCreditDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
            }
            break;
        case "delStaffCreditDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                window.location.reload();
            }
            break;
        case "createSerialDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("新增成功");
                //window.location.reload();
            }
            break; 
        /**/
        case "SearchStaffBehaveDataBaseCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $("#mainPagination1").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    //ellipse_text: "/",
                    //link_to: ,
                    callback: function(index, jq) {
                        var obj = MyBase.getTextValueBase("searchTable2");
                        AspAjax.SearchStaffBehaveDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
            } else {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='8'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffBehaveDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += ' <tr id="HS_' + result[i].ID + '" class="sStaffData" >' +
                                '<td>' + result[i].author + '</td>' +
			                    '<td><input type="text" class="SarticleDate date" value="' + TransformADFromStringFunction(result[i].articleDate) + '" size="10"/></td>' +
			                    '<td><input type="text" class="SserialNumber" value="' + result[i].serialNumber + '" size="5"/></td>' +
			                    '<td><input type="text" class="SseriesTitle" value="' + result[i].seriesTitle + '" /></td>' +
			                    '<td><input type="text" class="Svolume" value="' + result[i].volume + '" size="5"/></td>' +
			                    '<td><input type="text" class="SarticleTitle" value="' + result[i].articleTitle + '" /></td>' +
			                    '<td><select class="SarticleType"><option value="0">請選擇</option><option value="1">專業文章</option><option value="2">學術發表</option><option value="3">親子</option></select></td>' +
				                '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(\'' + result[i].ID + '\')">更 新</button> <button class="btnDelete" type="button" onclick="DelData(\'' + result[i].ID + '\')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(\'' + result[i].ID + '\')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].ID + '\',\'' + i + '\')">取 消</button></div>' +
			                    '</td>' +
			                '</tr>';
                    }
                    //
                    $("#mainIndexList .tableList").children("tbody").html(inner);
                    $("#mainIndexList input[type=text]").add("#mainIndexList select").attr("disabled", true);


                    $('.date').datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    for (var i = 0; i < result.length; i++) {
                        $("#HS_" + result[i].ID + " .SarticleType").children('option[value="' + result[i].articleType + '"]').attr("selected", true);
                    }
                    $(".SserialNumber").add(".volume").unbind('keydown').keydown(function(event) {
                        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8)) {
                            event.preventDefault();
                        }
                    });

                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainIndexList .tableList").children("tbody").html("<tr><td colspan='8'>查無資料</td></tr>");
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);

}
function getView(id) {
    window.open("./staff_upgrade_data.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    AspAjax.getAllStaffDataList([0]);
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        $("#studentName").val("王小明");
        $("#planName").val("張鶯英");
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    }
}

function UpData(TrID) {
   // $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", false);
    $("#HS_" + TrID + " input").add("#HS_" + TrID + " select").add("#HS_" + TrID + " textarea").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function SaveData(TrID) {
  //  $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", true);
    $("#HS_" + TrID + " input").add("#HS_" + TrID + " select").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
    var obj = new Object();
    obj.ID = TrID;
    obj.courseDate=TransformRepublicReturnValue($("#HS_" + TrID + " .ScourseDate").val());
    obj.courseLecturer = $("#HS_" + TrID + " .ScourseLecturer").val();
    obj.courseName = $("#HS_" + TrID + " .ScourseName").val();
    obj.courseTime = $("#HS_" + TrID + " .ScourseTime").val();
    obj.courseProve = $("#HS_" + TrID + " .ScourseProve :selected").val();
    obj.courseCredit = $("#HS_" + TrID + " .ScourseCredit").val();
    obj.otherExplanation = $("#HS_" + TrID + " .SotherExplanation").val();

    obj.courseCreditTeacherType = $("#HS_" + TrID + " .ScourseCreditTeacherType :selected").val();
    obj.courseInTeacher = (typeof ($("#HS_" + TrID + " .ScourseInTeacher:checked").val()) == "undefined") ? "0" : "1";
    obj.courseInAudiologist = (typeof ($("#HS_" + TrID + " .ScourseInAudiologist:checked").val()) == "undefined") ? "0" : "1";
    obj.courseInSocialWorkers = (typeof ($("#HS_" + TrID + " .ScourseInSocialWorkers:checked").val()) == "undefined") ? "0" : "1";
    obj.courseInAdministrative = (typeof ($("#HS_" + TrID + " .ScourseInAdministrative:checked").val()) == "undefined") ? "0" : "1";
    obj.courseIsSatisfaction = $("#HS_" + TrID + " .ScourseIsSatisfaction:checked").val();
    obj.courseDataQuestion = $("#HS_" + TrID + " .ScourseDataQuestion").val();
    obj.courseDataLecturer = $("#HS_" + TrID + " .ScourseDataLecturer").val();
    obj.courseDataLecture = (typeof ($("#HS_" + TrID + " .ScourseDataLecture:checked").val()) == "undefined") ? "0" : "1";
    obj.courseDataPhoto = (typeof ($("#HS_" + TrID + " .ScourseDataPhoto:checked").val()) == "undefined") ? "0" : "1";
    obj.courseDataTeaching = (typeof ($("#HS_" + TrID + " .ScourseDataTeaching:checked").val()) == "undefined") ? "0" : "1";
    obj.courseDataIsp = (typeof ($("#HS_" + TrID + " .ScourseDataIsp:checked").val()) == "undefined") ? "0" : "1";
    obj.courseDataOther = (typeof ($("#HS_" + TrID + " .ScourseDataOther:checked").val()) == "undefined") ? "0" : "1";

    obj.courseDataOtherText = $("#HS_" + TrID + " .courseDataOtherText").val();
    obj.courseStartH = $("#HS_" + TrID + " .courseStartH").val();
    obj.courseEndH = $("#HS_" + TrID + " .courseEndH").val();
    

    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.setStaffCreditDataBase(obj);
    }
    
}
function cancelInsert(viewID) {
    var DivName = '';
    switch (viewID) {
        case 1:
            DivName = "#insertDataDiv";
            break;
        case 2:
            DivName = "#insertDataDiv2";
            break;
    }
    $(DivName).hide();
    //$(".btnAdd").fadeIn();
}

function showView(viewID) {
    var DivName = '';
    switch (viewID) {
        case 1:
            DivName = "#mainSearchList";
            break;
        case 2:
            DivName = "#mainIndexList";
            break;
    }
    $(DivName).fadeIn();
    $(DivName + " input[type=text]").add(DivName + " select").attr("disabled", true);
    switch (viewID) {
        case 1:
            var obj = MyBase.getTextValueBase("searchTable");
            AspAjax.getAllStaffDataList([0]);
            AspAjax.SearchStaffCreditDataBaseCount(obj);
            break;
        case 2:
            var obj = MyBase.getTextValueBase("searchTable2");
            //AspAjax.getAllStaffDataList([0]);
            AspAjax.SearchStaffBehaveDataBaseCount(obj);
            break;
    }
    
}

function showInsert(insertID) {
    var DivName = '';
    switch (insertID) {
        case 1:
            DivName = "#insertDataDiv";
            break;
        case 2:
            DivName = "#insertDataDiv2";
            break;
    }
    $(DivName).fadeIn();
    $(DivName + " input[type=text]").add(DivName + " select").attr("disabled", false);
    AspAjax.getAllStaffDataList([0]);
    //$(".btnAdd").fadeOut();
    
}
function SaveStaffCreditData() {
    var obj = MyBase.getTextValueBase("setStaffCredit");
    var Participants = new Array();
    $("#ParticipantsSelect").find(':selected').each(function() {
        var objPeople = new Object();
        objPeople.sID = $(this).val();
        Participants.push(objPeople);
    });
    obj.Participants = Participants;

    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if ($("input[name='courseIsSatisfaction']:checked").val() == "1") {
        var checkOK = false;
        if (checkString.length == 0) {
            checkString = "未填寫：";
            checkOK = true;
        }
//        if ($('#courseDataQuestion').val().length == 0) {
//            if (checkOK) {
//                checkString += "講題滿意百分比、";
//            } else {
//                checkString += "、講題滿意百分比";
//            }
//            
//        }
//        if ($('#courseDataLecturer').val().length == 0) {
//            if (checkOK) {
//                checkString += "講師表現滿意百分比、";
//            } else {
//                checkString += "、講師表現滿意百分比";
//            }
//        }
        if (checkOK && checkString != "未填寫：") {
          checkString=  checkString.substr(0, checkString.length - 1);
        }
    }
    if (checkString.length > 0 && checkString != "未填寫：") {
        alert(checkString);
    } else {
        AspAjax.createStaffCreditDataBase(obj);
    }

}

function cancelUpData(TrID, vIndex) {
    /*$("#HS_" + TrID + " .SexecutionContent").val(_ReturnValue[vIndex].executionContent);
    $("#HS_" + TrID + " .SexecutionDate").val(TransformADFromStringFunction(_ReturnValue[vIndex].executionDate));*/

//    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", true);
    $("#HS_" + TrID + " input").add("#HS_" + TrID + " select").add("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();

}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delStaffCreditDataBase(parseInt(TrID));
    });
}
function SaveSerialData() {
    var obj = MyBase.getTextValueBase("setSerial");
    AspAjax.createSerialDataBase(obj);
}