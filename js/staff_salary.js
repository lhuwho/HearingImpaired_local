var MyBase = new Base();
var _jobTitle =  new Array("","高專","專員","組員","助理","助佐","講師","助理講師","專業教師","助理教師","教師助理","6職等","7職等","8職等","9職等","10職等","11職等","12職等");



$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    
   
    $("#item1Content").fadeIn();

    $(".menuTabs").click(function() {
        $(".menuTabs").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("input").add("select").add("textarea").attr("disabled", true);
        if (id == null && (act == 1 || act == 31)) {
            $("input").add("select").add("textarea").attr("disabled", false);
        } else {
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
            
        }
        getStudentData(this.id);
    });

    $(".showUploadImg").fancybox();

   


});

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    // alert(methodName);
    switch (methodName) {
        case "SearchStaffSalaryBaseCount":
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
                        AspAjax.SearchStaffSalaryDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffSalaryDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].sID + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td>' + result[i].sName + '</td>' +
			                    '<td>' + _SexList[result[i].sSex] + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].FileDate) + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='6'>查無資料</td></tr>");
            }
            break;
        case "getStaffSalaryDataBase":
            if (!(result == null || result.length == 0 || result == undefined) && result.ID != null) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#pensionFundsPer").html("(" + result.pensionFundsPer + "%)");
                    $("#Unit").html(_UnitList[result.Unit]);
                    $("input").add("select").attr("disabled", false);
                    $("#WorkItem").html(_JobItemList[result.WorkItem]);
                    $("#JobCapacity").html(_JobItemList[result.WorkItem] + "-" + _jobTitle[result.JobCapacity] + "(" + result.JobGrade + "級)");
                    $("#AddTitle").html(result.AddTitle.replace(/\n/g, "<br>"));
                    var inner = '';
                    for (var i = 0; i < result.addItem.length; i++) {
                        inner += '<ul >' +
                        //'<li class="thSystem"></li>' +
		                            '<li class="thItem">' + result.addItem[i].project + '</li>' +
		                            '<li class="thItem">$' + result.addItem[i].projectMoney + '</li>' +
                        //'<li class="thItem2">' + result.addItem[i].explain + '</li>' +
		                        '</ul>';
                        $("#addItemDiv").append(inner);
                    }
                    var inner = '';
                    for (var i = 0; i < result.minusItem.length; i++) {

                        inner += '<ul >' +
                        //'<li class="thSystem"></li>' +
		                            '<li class="thItem">' + result.minusItem[i].project + '</li>' +
		                            '<li class="thItem">$' + result.minusItem[i].projectMoney + '</li>' +
                        // '<li class="thItem2">' + result.minusItem[i].explain + '</li>' +
		                        '</ul>';
                        $("#minusItemDiv").append(inner);
                    }

                    $("#realWages").html(FormatNumber(result.realWages));
                    $("#AddMoney").html(FormatNumber(result.AddMoney));
                    $("#MinsMoney").html(FormatNumber(result.MinsMoney));
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            }
            break;
       
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);

}
function getView(id) {
    window.open("./staff_salary.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    AgencyUnitSelectFunction("mainSearchForm", "gosrhstaffUnit");
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getStaffSalaryDataBase(id);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    }

}

function SearchData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var SearchBirthday = false;
    if ((obj.txtstaffBirthdayStart != null && obj.txtstaffBirthdayEnd != null) || (obj.txtstaffBirthdayStart == null && obj.txtstaffBirthdayEnd == null)) {
        SearchBirthday = true;
    }
    if (SearchBirthday) {
        AspAjax.SearchStaffSalaryBaseCount(obj);
    } else {
        alert("請填寫完整出生日期區間");
    }
}
function GetImg() {
    html2canvas($("#mainContent"), {
        onrendered: function(canvas) {
            var type = 'jpg';
            var imgData = canvas.toDataURL(type);

            var _fixType = function(type) {
                type = type.toLowerCase().replace(/jpg/i, 'jpeg');
                var r = type.match(/png|jpeg|bmp|gif/)[0];
                return 'image/' + r;
            };

            // 加工image data，替换mime type
            imgData = imgData.replace(_fixType(type), 'image/octet-stream');


            var saveFile = function(data, filename) {
                var save_link = document.createElementNS('http://www.w3.org/1999/xhtml', 'a');
                save_link.href = data;
                save_link.download = filename;

                var event = document.createEvent('MouseEvents');
                event.initMouseEvent('click', true, false, window, 0, 0, 0, 0, 0, false, false, false, false, 0, null);
                save_link.dispatchEvent(event);
            };
           
            var filename = '薪資單_' + (new Date()).getTime() + '.' + type;
            // download
            saveFile(imgData, filename);
            
           // var $div = $("#test");
            //$div.empty();
            //$("<img />", { src: canvas.toDataURL("image/png") }).appendTo($div);
        }
    });
    
}