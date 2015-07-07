var MyBase = new Base();
var noEmptyItem = ["studentName"];
var noEmptyShow = ["學生姓名"];
var GradeList = new Array("", "優等", "甲等", "乙等", "丙等");
var _ReturnValue;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    AgencyUnitSelectFunction("mainSearchForm", "gosrhstaffUnit");
   

    $(".btnAdd").click(function() {
        $("#insertDataDiv input[type=text]").attr("disabled", false);
    });
    $("#mainSearchForm .btnSearch").click(function() {
        SearchStaffData();
    });

    $("#setMerit input").unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8)) {
            event.preventDefault();
        }
    }).unbind('keyup').keyup(function() {
        countScore("setMerit");
    });

    $("#setMerit .MeritName").unbind("click").click(function() {
    var inner = '<div id="inline"><br /><table border="0" width="360" id="searchTableinline">' +
                    '<tr><td width="80">員工姓名　<input type="text" id="gosrhstaffName" value="" /></td></tr>' +
                    '<tr><td>派任單位　<select id="gosrhstaffUnit"></select></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table class="tableList" border="0"  width="360">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="100">員工編號</th>' +
			                    '<th width="130">派任單位</th>' +
			                    '<th width="130">員工姓名</th>' +
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
                AgencyUnitSelectFunction("inline", "gosrhstaffUnit");
                $('button').css({ "background-color": "#F9AE56" });
                $("#inline .btnSearch").unbind("click").click(function() {
                    var obj = MyBase.getTextValueBase("searchTableinline");
                    AspAjax.SearchStaffDataBaseCount(obj);
                });
            }
        });
    });

});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    // alert(methodName);
    switch (methodName) {
        case "SearchStaffMeritDataBaseCount":
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
                        var obj = MyBase.getTextValueBase("searchTable");
                        AspAjax.SearchStaffMeritDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='12'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='12'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffMeritDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].sData.sID) != -1) {
                    _ReturnValue = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += ' <tr id="HS_' + result[i].MID + '" class="sStaffData" >' +
			                    '<td>' + result[i].AY + '</td>' +
			                    '<td>' + result[i].sData.sName + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td><input type="text" class="AScore" value="' + result[i].AScore + '" size="5"/></td>' +
			                    '<td><input type="text" class="PScore" value="' + result[i].PScore + '" size="5" /></td>' +
			                    '<td><input type="text" class="WScore" value="' + result[i].WScore + '" size="5" /></td>' +
			                    '<td class="PASRawScore"></td>' +
			                    '<td><input type="text" class="AddScore" value="' + result[i].AddScore + '" size="5" /></td>' +
			                    '<td><input type="text" class="LowerScore" value="' + result[i].LowerScore + '" size="5" /></td>' +
			                    '<td class="PASScore"></td>' +
			                    '<td><span class="Grade"></span><span class="GradeID" style="display:none;"></span>' +
			                    '</td>' +
			                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(\'' + result[i].MID + '\')">更 新</button> <button class="btnView" type="button" onclick="DelData(\'' + result[i].MID + '\')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(\'' + result[i].MID + '\')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(\'' + result[i].MID + '\',\'' + i + '\')">取 消</button></div>' +
			                    '</td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                    $("#mainSearchList input[type=text]").attr("disabled", true);
                    for (var i = 0; i < result.length; i++) {
                        countScore("HS_" + result[i].MID);
                    }
                    $(".sStaffData input").unbind('keydown').keydown(function(event) {
                        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8)) {
                            event.preventDefault();
                        }
                    }).unbind('keyup').keyup(function() {
                        var pIDname = $(this).parent().parent().attr("id");
                        countScore(pIDname);
                    });

                } else {
                alert("發生錯誤，錯誤訊息如下：" + result[0].sData.sName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='12'>查無資料</td></tr>");
            }
            break;
        case "SearchStaffDataBaseCount":
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
                        var obj = MyBase.getTextValueBase("searchTableinline");
                        AspAjax.SearchStaffDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].sID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="s_' + result[i].sID + '">' +
                                '<td>' + result[i].sID + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td style="display:none;">' + result[i].sUnit + '</td>' +
			                    '<td>' + result[i].sName + '</td>' +
			                '</tr>';
                    }
                    //
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("s_", "");
                        $("#setMerit .MeritID").html(id);
                        var Name = $(this).children("td:nth-child(4)").html();
                        $("#setMerit .MeritName").val(Name);
                        var Unit = $(this).children("td:nth-child(3)").html();
                        $("#setMerit .MeritUnitID").html(Unit);
                        $("#setMerit .MeritUnit").html($(this).children("td:nth-child(2)").html());
                        
                        $.fancybox.close();
                    });


                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "createStaffMeritDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "setStaffMeritDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("更新成功");
            }
            break;
        case "delStaffMeritDataBase":
            $.fancybox.close();
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("刪除成功");
                window.location.reload();
            }
            break;         
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);

}
function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function SaveData(TrID) {
    var MeritData = new Array();
    //var MeritYear

    var setMerit = true;
    $("#HS_" + TrID + " input[type=text]").each(function() {
        var item = $(this).val();
        if (item.length > 0) {
            MeritData.push(item);
        } else {
            alert("請填寫完整");
            $(this).select();
            setMerit = false;
            return false;
        }
    });
    if (setMerit) {
        var Grade = $("#HS_" + TrID +" .GradeID").html();
        if (Grade.length > 0) {
            $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
            $("#HS_" + TrID + " .UD").show();
            $("#HS_" + TrID + " .SC").hide();
            AspAjax.setStaffMeritDataBase(parseInt(TrID), parseInt(Grade), MeritData);
        } else {
            alert("請填寫完整");
        }
    }
}
function cancelUpData(TrID,vIndex) {
 	$("#HS_" + TrID + " .AScore").val(_ReturnValue[vIndex].AScore);
 	$("#HS_" + TrID + " .PScore").val(_ReturnValue[vIndex].PScore);
 	$("#HS_" + TrID + " .WScore").val(_ReturnValue[vIndex].WScore);
 	$("#HS_" + TrID + " .AddScore").val(_ReturnValue[vIndex].AddScore);
 	$("#HS_" + TrID + " .LowerScore").val(_ReturnValue[vIndex].LowerScore);
 	countScore("HS_" + TrID);
 	$("#HS_" + TrID + " input[type=text]").attr("disabled", true);
 	$("#HS_" + TrID + " .UD").show();
 	$("#HS_" + TrID + " .SC").hide();
  
}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delStaffMeritDataBase(parseInt(TrID));
    })
}
function InsertData() {
    $("#insertDataDiv").show();
    $("#setB").hide();
}
function cancelInsert() {
    $("#insertDataDiv").hide();
    $("#setB").show();
}
function SearchStaffData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var SearchBirthday = false;
    if ((obj.txtstaffBirthdayStart != null && obj.txtstaffBirthdayEnd != null) || (obj.txtstaffBirthdayStart == null && obj.txtstaffBirthdayEnd == null)) {
        SearchBirthday = true;
    }
    if (SearchBirthday) {
        AspAjax.SearchStaffMeritDataBaseCount(obj);
    } else {
        alert("請填寫完整出生日期區間");
    }
}
function countScore(mainID) {
    var AScore = $("#" + mainID + " .AScore").val();
    var PScore = $("#" + mainID + " .PScore").val();
    var WScore = $("#" + mainID + " .WScore").val();
    var AddScore = $("#" + mainID + " .AddScore").val();
    var LowerScore = $("#" + mainID + " .LowerScore").val();

    if (AScore.length > 0 && PScore.length > 0 && WScore.length > 0) {
        //if (parseInt(AScore) <= 20 && parseInt(AScore) >= 17 && parseInt(PScore) <= 34 && parseInt(PScore) >= 40 && parseInt(WScore) <= 34 && parseInt(WScore) >= 40) {
            var Score = parseInt(AScore) + parseInt(PScore) + parseInt(WScore);
            $("#" + mainID + " .PASRawScore").html(Score);
            if (AddScore.length > 0 && LowerScore.length > 0) {
                var Score2 = Score + parseInt(AddScore) - parseInt(LowerScore);
                if (Score2 > 100) {
                    Score2 = 100;
                }
                $("#" + mainID + " .PASScore").html(Score2);
                if (Score2 >= 90) {
                    $("#" + mainID + " .Grade").html(GradeList[1]);
                    $("#" + mainID + " .GradeID").html("1");
                } else if (Score2 >= 80) {
                    $("#" + mainID + " .Grade").html(GradeList[2]);
                    $("#" + mainID + " .GradeID").html("2");
                } else if (Score2 >= 70) {
                    $("#" + mainID + " .Grade").html(GradeList[3]);
                    $("#" + mainID + " .GradeID").html("3");
                } else if (Score2 < 70) {
                    $("#" + mainID + " .Grade").html(GradeList[4]);
                    $("#" + mainID + " .GradeID").html("4");
                }
            }
        //} else {
        //alert("數值超過");
        //}
    }

}
function setMeritData() {
    var AScore = $("#mainID .AScore").val();
    var PScore = $("#mainID .PScore").val();
    var WScore = $("#mainID .WScore").val();
    var AddScore = $("#mainID .AddScore").val();
    var LowerScore = $("#mainID .LowerScore").val();
    
    
    var MeritData = new Array();
    //var MeritYear

    var setMerit = true;
    $("#setMerit input[type=text]").each(function() {
        var item = $(this).val();
        if (item.length > 0) {
            MeritData.push(item);
        } else {
            alert("請填寫完整");
            $(this).select();
            setMerit = false;
            return false;
        }
    });
    if (setMerit) {
        var Grade = $("#setMerit .GradeID").html();
        var MeritUnit = $("#setMerit .MeritUnitID").html();
        if (Grade.length > 0) {
            AspAjax.createStaffMeritDataBase(MeritData, parseInt(Grade), $("#setMerit .MeritID").html(), parseInt(MeritUnit));
        } else {
            alert("請填寫完整");
        }
    }
}