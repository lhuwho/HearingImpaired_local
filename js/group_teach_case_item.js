var MyBase = new Base();
var _avtnumID = 0;
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);

    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $(".tableContact input").add("select").add("textarea").attr("disabled", false);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

    $("#mainSearchForm .btnSearch").click(function() {
        SearchCPTData();
    });

    $("#className").unbind("click").click(function() {
        var inner = '<div id="inline"><br /><table border="0" width="360">' +
                    '<tr><td width="80">班　　別　<input type="text" id="ssName" value="" /></td></tr>' +
                    '<tr><td>派任單位　<select id="ssUnit"></select></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table class="tableList" border="0"  width="360">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="60">編號</th>' +
			                    '<th width="100">單　　位</th>' +
			                    '<th width="100">班　　別</th>' +
			                    '<th width="100">教師姓名</th>' +
			                '</tr>' +
			            '</thead>' +
			            '<tbody>' +
			            '</tbody>' +
                    '</table>' +
                    '<div class="pagination"></div>' +
                    '</div>';
        $.fancybox({
            'content': inner,
            'autoDimensions': true,
            'centerOnScroll': true,
            'onComplete': function() {
                AgencyUnitSelectFunction("inline", "ssUnit");
                $('button').css({ "background-color": "#F9AE56" });
                $("#inline .btnSearch").unbind("click").click(function() {
                    AspAjax.SearchClassDataBaseCount(0, $("#ssName").val(), parseInt($("#ssUnit :selected").val()));
                });
            }
        });
    });

    $("#courseName").unbind("click").click(function() {
        var inner = '<div id="inline"><br /><table border="0" width="360">' +
                    '<tr><td width="80">課程名稱　<input type="text" id="ssName" value="" /></td></tr>' +
                    '<tr><td>單　　位　<select id="ssUnit"></select></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table class="tableList" border="0"  width="360">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="60">編號</th>' +
			                    '<th width="100">單　　位</th>' +
			                    '<th width="100">課　　別</th>' +
			                    '<th width="100">課程名稱</th>' +
			                '</tr>' +
			            '</thead>' +
			            '<tbody>' +
			            '</tbody>' +
                    '</table>' +
                    '<div class="pagination"></div>' +
                    '</div>';
        $.fancybox({
            'content': inner,
            'autoDimensions': true,
            'centerOnScroll': true,
            'onComplete': function() {
                AgencyUnitSelectFunction("inline", "ssUnit");
                $('button').css({ "background-color": "#F9AE56" });
                $("#inline .btnSearch").unbind("click").click(function() {
                    AspAjax.SearchCourseDataBaseCount(0, $("#ssName").val(), parseInt($("#ssUnit :selected").val()));
                });
            }
        });
    });

    $(".btnSave").click(function() {
        var unitNum = _uUnit;
        var classNameID = $("#classNameID").html();
        var teacherNameID = $("#teacherNameID").html();
        var courseNameID = $("#courseNameID").html();
        var startPeriod = $("#startPeriod").val();
        var endPeriod = $("#endPeriod").val();

        var targetDomain = new Array();
        var targetShort = '';

        for (var i = 1; i < 5; i++) {
            for (var j = 0; j < $("#table" + i + ">tbody>tr").length; j++) {
                /*if (j == 1) {
                for (var k = 0; k < 5; k++) {
                if (k == 4) {
                targetShort += $("#dataTR" + i).find(".short" + k).val();
                } else {
                targetShort += $("#dataTR" + i).find(".short" + k).val() + "＠";
                }
                }
                if (($("#dataTR" + i).find(".long").val()).length > 0) {
                targetDomain.push([i.toString(), $("#dataTR" + i).find(".long").val(), targetShort]);
                }
                } else {*/
                for (var k = 0; k < 5; k++) {
                    if (k == 4) {
                        targetShort += $("#dataTR" + i + j).find(".short" + k).val();
                    } else {
                        targetShort += $("#dataTR" + i + j).find(".short" + k).val() + "＠";
                    }
                }
                if (($("#dataTR" + i + j).find(".long").val()).length > 0) {
                    targetDomain.push([i.toString(), $("#dataTR" + i + j).find(".long").val(), targetShort]);
                }
                //}
                targetShort = "";
            }
        }
        if (classNameID.length < 1) {
            alert("請選擇班別");
        } else if (teacherNameID.length < 1) {
            alert("請選擇教師");
        } else if (courseNameID.length < 1) {
            alert("請選擇課程");
        } else if (startPeriod.length < 1 || endPeriod.length < 1) {
            alert("請選擇計畫期程");
        } else if (targetDomain.length < 1) {
            alert("請填寫目標計畫");
        } else {
            AspAjax.createCoursePlanTemplate(unitNum, classNameID, teacherNameID, courseNameID, TransformRepublic(startPeriod), TransformRepublic(endPeriod), targetDomain);
        }
    });

    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './group_teach_case_item.aspx';
        }
    }
    setTimeout('$("#unitName").html(_UnitList[_uUnit]);', 500);
});

function getView(id) {
    window.open("./group_teach_case_item.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        _avtnumID=parseInt(id);
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        AspAjax.getCoursePlanTemplate(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
    }
}

function getAdd(tid) {
    var item = parseInt($("#table" + tid + ">tbody>tr").length);
    $("#table" + tid + ">tbody>tr:last-child").after($("#dataTR" + tid + "0").clone().attr("id", "dataTR" + tid + item));
    $("#dataTR" + tid + item).find(".longID").html(0);
    $("#dataTR" + tid + item).find(".short0ID").html(0);
    $("#dataTR" + tid + item).find(".short1ID").html(0);
    $("#dataTR" + tid + item).find(".short2ID").html(0);
    $("#dataTR" + tid + item).find(".short3ID").html(0);
    $("#dataTR" + tid + item).find(".short4ID").html(0);
}

function getSubtract(tid) {
    if ($("#table" + tid + ">tbody>tr").length > 1) {
        fancyConfirm("確定刪除?<br />此操作確定後不可逆。", function() {
            var longID = $("#table" + tid + ">tbody>tr:last-child").find(".longID").html();
            if (longID.length > 0) {
                AspAjax.delCoursePlanTemplate(parseInt(longID));
            }
            $("#table" + tid + ">tbody>tr:last-child").detach();
            $.fancybox.close();
        });
    }
}

function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "createCoursePlanTemplate":
            if (result <= 0) {
                alert("發生錯誤，請重新整理頁面");
            } else {
                alert("新增成功");
                window.location.href = "./group_teach_case_item.aspx";
            }
            break;
        case "SearchClassDataBaseCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $(".pagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        AspAjax.SearchClassDataBase(parseInt((index + 1) * _LimitPage, 10), result[1], result[2], parseInt(result[3], 10), result[4], result[5], parseInt(result[6], 10), parseInt(result[7], 10));
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchClassDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].sID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="c1_' + result[i].cID + '">' +
                                '<td>' + result[i].cID + '</td>' +
			                    '<td>' + _UnitList[result[i].cUnit] + '</td>' +
			                    '<td style="display:none;">' + result[i].cUnit + '</td>' +
			                    '<td>' + result[i].cName + '</td>' +
			                    '<td>' + result[i].cTeacher + '</td>' +
			                    '<td style="display:none;">' + result[i].cTeacherID + '</td>' +
			                '</tr>';
                    }
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("c1_", "");
                        $("#className").val($(this).children("td:nth-child(4)").html());
                        $("#classNameID").html(id);
                        $("#teacherName").val($(this).children("td:nth-child(5)").html());
                        $("#teacherNameID").html($(this).children("td:nth-child(6)").html());

                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].cName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "SearchCourseDataBaseCount":
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $(".pagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                        AspAjax.SearchCourseDataBase(parseInt((index + 1) * _LimitPage, 10), result[1], result[2], parseInt(result[3], 10), result[4], result[5], parseInt(result[6], 10), parseInt(result[7], 10));
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchCourseDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].sID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="c2_' + result[i].cID + '">' +
                                '<td>' + result[i].cID + '</td>' +
			                    '<td>' + _UnitList[result[i].cUnit] + '</td>' +
			                    '<td style="display:none;">' + result[i].cUnit + '</td>' +
			                    '<td>' + _CategoryList[result[i].cCategory] + '</td>' +
			                    '<td>' + result[i].cName + '</td>' +
			                '</tr>';
                    }
                    $("#inline .tableList").children("tbody").html(inner);

                    $("#inline .ImgJs").unbind('click').click(function() {
                        var id = ($(this).attr("id")).replace("c2_", "");
                        $("#courseNameID").html(id);
                        $("#courseName").val($(this).children("td:nth-child(5)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].cName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "SearchCPTDataBaseCount":
            var obj = MyBase.getTextValueBase("searchTable");
            var pageCount = parseInt(result[0]);
            if (pageCount > 0) {
                //分頁，PageCount是總條目數，這是必選參數，其它參數都是可選
                $(".mainPagination").pagination(pageCount, {
                    prev_text: '<', //上一頁按鈕裡text
                    next_text: '>', //下一頁按鈕裡text
                    items_per_page: _LimitPage, //顯示條數
                    num_display_entries: 4, //連續分頁主體部分分頁條目數
                    num_edge_entries: 2, //兩側首尾分頁條目數
                    callback: function(index, jq) {
                    AspAjax.SearchCPTDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchCPTDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].sID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += ' <tr id="HS_' + result[i].sCPTID + '" class="sStaffData" >' +
			                    '<td>' + result[i].sID + '</td>' +
			                    '<td>' + result[i].sClassName + '</td>' +
			                    '<td>' + result[i].sTeacherName + '</td>' +
			                    '<td>' + result[i].sCourseName + '</td>' +
			                    '<td>' + TransformADFromDateFunction(result[i].sStartPeriod) + '~' + TransformADFromDateFunction(result[i].sEndPeriod) + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].sCPTID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "getCoursePlanTemplate":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.Column != -1) {
                    var inner = "";
                    $("#className").val(result.classIDName);
                    $("#classNameID").html(result.classID);
                    $("#teacherName").val(result.teacherIDName);
                    $("#teacherNameID").html(result.teacherNameID);
                    $("#courseName").val(result.courseIDName);
                    $("#courseNameID").html(result.courseNameID);
                    $("#startPeriod").val(TransformADFromDateFunction(result.startPlanDate));
                    $("#endPeriod").val(TransformADFromDateFunction(result.endPlanDate));

                    for (var i = 0; i < result.targetDomain.length; i++) {
                        var LongDomain = result.targetDomain[i];
                        var DomeinNun = LongDomain.Domain;
                        if (LongDomain.LongColumn != 0) {
                            var TableNum = $("#table" + DomeinNun + ">tbody>tr").length;
                            TableNum = TableNum - 1;
                            $("#dataTR" + DomeinNun + TableNum).find(".long").val(LongDomain.Content);
                            $("#dataTR" + DomeinNun + TableNum).find(".longID").html(LongDomain.LongColumn);
                            for (var j = 0; j < LongDomain.ShortTarget.length; j++) {
                                var item = LongDomain.ShortTarget[j].Content;
                                var shorID = LongDomain.ShortTarget[j].ShortColumn;
                                $("#dataTR" + DomeinNun + TableNum).find(".short" + j).val(item);
                                $("#dataTR" + DomeinNun + TableNum).find(".short" + j + "ID").html(shorID)
                            }
                            if (i != 0 && ($("#dataTR" + DomeinNun + TableNum).find(".long").val()).length > 0 && (i + 1) < result.targetDomain.length && result.targetDomain[i + 1].LongColumn != 0 && result.targetDomain[i + 1].Domain == DomeinNun) {
                                getAdd(DomeinNun);
                            }
                        }

                    }
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.courseIDName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "setCoursePlanTemplate":
            if (result <= 0) {
                alert("發生錯誤，請重新整理頁面");
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
        case "delCoursePlanTemplate":
            if (result <= 0) {
                alert("發生錯誤，請重新整理頁面");
            } else {
                alert("刪除成功");
                window.location.reload();
            }
            break;
    }
}

function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function SearchCPTData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var Date1 = false;
    if ((obj.txtStartPeriod != null && obj.txtEndPeriod != null) || (obj.txtStartPeriod == null && obj.txtEndPeriod == null)) {
        Date1 = true;
    }
    if (Date1) {
        AspAjax.SearchCPTDataBaseCount(obj);
    } else {
        alert("請填寫完整計畫期程區間");
    }

/*    var sClassName = $("#sClassName").val();
    var sTeacherName = $("#sTeacherName").val();
    var sCourseName = $("#sCourseName").val();
    var sStartPeriod = $("#sSPeriod").val();
    var sEndPeriod = $("#sEPeriod").val();
    var SearchPeriod = false;
    if (sStartPeriod.length > 0 && sEndPeriod.length > 0) {
        sStartPeriod = TransformRepublic(sStartPeriod);
        sEndPeriod = TransformRepublic(sEndPeriod);
        SearchPeriod = true;
    } else {
        if (sStartPeriod.length == 0 && sEndPeriod.length == 0) {
            sStartPeriod = TransformRepublicReturnValue(sStartPeriod);
            sEndPeriod = TransformRepublicReturnValue(sEndPeriod);
            SearchPeriod = true;
        } else {
            alert("請填寫完整計畫期程區間");
        }
    }
    if (SearchPeriod) {
        AspAjax.SearchCPTDataBaseCount(sClassName, sTeacherName, sCourseName, sStartPeriod, sEndPeriod, _uUnit);
    }*/
}
function setaCoursePlanTemplate() {
    var objList = {};
    objList.Column=_avtnumID;
    //objList.targetDomain=new Array();
    var targetDomain = new Object;
    var targetShort = new Object;
    var targetArray = new Array();
    for (var i = 1; i < 5; i++) {
        for (var j = 0; j < $("#table" + i + ">tbody>tr").length ; j++) {
            var targetShortArray = new Array();
            var longIDHtml = $("#dataTR" + i + j).find(".longID").html();
            var longID = longIDHtml.length > 0 ? parseInt(longIDHtml) : 0
            for (var k = 0; k < 5; k++) {
                targetShort = new Object;
                var shortIDHtml = $("#dataTR" + i + j).find(".short" + k + "ID").html();
                var shortID = shortIDHtml.length > 0 ? parseInt(shortIDHtml) : 0;
                targetShort.LongColumn = longID;
                targetShort.ShortColumn = shortID;
                targetShort.Content = $("#dataTR" + i + j).find(".short" + k).val();
                targetShortArray.push(targetShort);
            }
            var LingItem=$("#dataTR" + i + j).find(".long").val();
            if (LingItem.length > 0) {
                targetDomain.ISPColumn = parseInt(_avtnumID);
                targetDomain.LongColumn = longID;
                targetDomain.Content = LingItem;
                targetDomain.Domain = parseInt(i);
                targetDomain.ShortTarget = targetShortArray;
            }
            targetArray.push(targetDomain);
            targetShort = new Object;
            targetDomain = new Object;
        }
       
    }
    objList.targetDomain = targetArray;
    AspAjax.setCoursePlanTemplate(objList);

}

