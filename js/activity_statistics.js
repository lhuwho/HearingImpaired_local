var MyBase = new Base();
var noEmptyItem = ["eventName", "eventDate"];
var noEmptyShow = ["活動名稱", "活動日期"];
var _ColumnID = 0;
var isEdit=0;
var _oldParticipants = new Array();
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    initPage();
    initEventsandPageSetting();
    //AspAjax.getAllStudentDataList(0); 
    $("#eventStaffList").chosen({
        search_contains: true, //選項模糊查詢
        disable_search_threshold: 10,
        display_selected_options: false,
        max_selected_options: 3, //最大選擇數
        no_results_text: "查無資料", //沒有結果匹配
        width: "380px" //寬度
    });
    /*
    $("#ParticipantsList").ajaxChosen({
    method: 'POST',
   
    url: "AspAjax.asmx/SearchStudent",
    url: '/ajax-chosen/data.php',
    dataType: 'json'
    }, function(data) {
    var terms = {};

        $.each(data, function(i, val) {
    terms[i] = val;
    });

        return terms;
    });
    
    $("#ParticipantsList").chosen({
    search_contains: true, //選項模糊查詢
    disable_search_threshold: 10,
    display_selected_options: false,
    no_results_text: "查無資料", //沒有結果匹配
    width: "500px" //寬度
    });
    
    */
    $("#Participant").autocomplete({
        source: function (request, response) {
            var data = {
                term: request.term
            };
            $.ajax({
                type: "POST",
                url: "AspAjax.asmx/SearchStudent",
                dataType: 'json',
                contentType: "application/json; charset=utf-8",
                data: "{ 'SearchString' : '" + request.term + "'}",
               
            }).success(function (data) {
                response(data.d);
               //("#ParticipantsList").destroy().init() ;
               /*
               $("#ParticipantsList").trigger('chosen:updated');
               if (!(data == null || data.length == 0 || data == undefined)) {
                
                    for (var i = 0; i < data.length; i++) {
                        $("#ParticipantsList_chosen .chosen-drop .chosen-results").append($('<li></li>').attr("value", 'aaa').text('bbb'));
                    }
                    $("#ParticipantsList").trigger('chosen:updated');
                
            } else {
            $("#ParticipantsList").find("option").empty();
            }
               */
            }).fail(function () {
              //  alert("failed");
            });
        }
    });
    
});
function initEventsandPageSetting(){
    if(GetQueryString("act")==1)
        isEdit=1;
    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $(".chosen-select").trigger('chosen:updated');
        isEdit=1;
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        $(".chosen-select").trigger('chosen:updated');
        isEdit=0;
    });

    $("#appurtenance img").fancybox();

}
function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStudentActivityDataBaseCount":
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
                        AspAjax.SearchStudentActivityDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='4'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStudentActivityDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].txteventName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].txteventDate) + '</td>' +
			                    '<td>' + result[i].txttotalNumber + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "getAllStaffDataList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo == null && parseInt(result[0].checkNo) != -1) {
                    for (var i = 0; i < result.length; i++) {
                        $("#eventStaffList").append($('<option></option>').attr("value", result[i].sID).text(result[i].sName + "(" + result[i].sID + ")"));
                    }
                    $("#eventStaffList").trigger('chosen:updated');
                    AspAjax.getStudentActivityData(_MainID);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            } else {
                $("#eventStaffList").find("option").empty();
            }
            break;
        case "getAllStudentDataList":
            
            break;
        case "createStudentActivityData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) <= 0) {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                } else {
                    alert("新增成功");
                    window.location.href = "./activity_statistics.aspx?id=" + result[0] + "&act=2";
                }
            }
            break;
        case "getStudentActivityData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    PushPageValue(result);
                    $("#caseUnit").html(_UnitList[result.caseUnit]);
                    _oldParticipants = result.Participants;
                    for (var i = 0; i < result.eventStaffList.length; i++) {
                        $("#eventStaffList").children('option[value="' + result.eventStaffList[i] + '"]').attr("selected", true);
                    }
                    $("#eventStaffList").trigger('chosen:updated');
                    for (var i = 0; i < result.Participants.length; i++) {
                        addParticipant(result.Participants[i][1],result.Participants[i][2]);
                       // var itemValue = result.Participants[i];
                       // $("#ParticipantsList").children('option[value="' + itemValue[1] + '"]').attr("selected", true);
                    }
                    $(".search-choice-close").attr('disabled', true);
                    //$("#ParticipantsList").trigger('chosen:updated');

                   // 
                    $(".chosen-select").trigger('chosen:updated');
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
        case "setStudentActivityData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function getView(id) {
    window.open("./activity_statistics.aspx?id=" + id + "&act=2");
}
function search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var Date1 = false;
    if ((obj.txteventDatestart != null && obj.txteventDateend != null) || (obj.txteventDatestart == null && obj.txteventDateend == null)) {
        Date1 = true;
    }
    if (Date1) {
        AspAjax.SearchStudentActivityDataBaseCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }
}
function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        _ColumnID = id;
        $(".btnUpdate").fadeIn();
        
        AspAjax.getAllStaffDataList([17,18]);
        $("input").add("select").add("textarea").attr("disabled", true);
    } else if (id == null && act == 1) {
        AspAjax.getAllStaffDataList([17,18]);
        $(".btnSave").fadeIn();
        $("#caseUnit").html(_UnitList[_uUnit]);$("#creatFileName").val(_uName);
        $("input").add("select").add("textarea").attr("disabled", false);
       
    }
}
function saveData() {
    var obj = MyBase.getTextValueBase("mainContent .tableContact");
    var Participants = new Array();
    $("#participant").each(function() {
        var objPeople = ["", $(this).attr("id").replace("participant_","")];
        Participants.push(objPeople);
    });
    obj.Participants = Participants;

    var eventStaffList = new Array();
    $("#eventStaffList").find(':selected').each(function() {
        eventStaffList.push($(this).val());
    });
    obj.eventStaffList = eventStaffList;
    
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createStudentActivityData(obj);
    }
}
function setData() {
    var obj = MyBase.getTextValueBase("mainContent .tableContact");
    obj.ID = _ColumnID;
    var eventStaffList = new Array();
    $("#eventStaffList").find(':selected').each(function() {
        eventStaffList.push($(this).val());
    });
    obj.eventStaffList = eventStaffList;
    var newParticipants = new Array();
   // $("#ParticipantsList").find(':selected').each(function() {
   //     newParticipants.push($(this).val());
   // });
    $(".participant").each(function() {
        var id=$(this).attr("id");
        var objPeople = id.replace("participant_","");
        newParticipants.push(objPeople);
    });
    
    //oldSize
    var DelParticipantsValue = new Array();
    var NewParticipantsValue = new Array();
    var oldParticipantsValue = new Array();
    for (var i = 0; i < _oldParticipants.length; i++) {
        oldParticipantsValue.push(_oldParticipants[i][1]);
    }

    DelParticipantsValue = array_diff(oldParticipantsValue, newParticipants);   //被取消的
    NewParticipantsValue = array_diff(newParticipants, oldParticipantsValue);  //新增加的

    var DelParticipantsID = new Array();
    for (var i = 0; i < DelParticipantsValue.length; i++) {
        var itemindex = oldParticipantsValue.indexOf(DelParticipantsValue[i]);
        if (itemindex != -1) {
            DelParticipantsID.push(_oldParticipants[itemindex][0]);
        }
    }
     var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
     if (checkString.length > 0) {
         alert(checkString);
     } else {
         if (DelParticipantsID.length != 0 || NewParticipantsValue.length != 0) {
             AspAjax.setStudentActivityData(obj, DelParticipantsID, NewParticipantsValue);
         }
     }
}
function addParticipantone(){
    var val=$("#Participant").val();
    var id=val.substring(val.indexOf("(")+1,val.indexOf(")"));
    var inner='<li class="search-choice participant" id="participant_'+id+'">'+
    '<span>'+val+'</span>'+
    '<a class="search-choice-close" onclick="deleteMyself('+id+')"></a>'+
    '</li>';
    $("#ParticipantsList").append(inner);
}
function addParticipant(id,name){
    
    var inner='<li class="search-choice participant" id="participant_'+id+'">'+
    '<span>'+name+'('+id+')'+'</span>'+
    '<a class="search-choice-close" href="javascript:deleteMyself('+id+')"></a>'+
    '</li>';
    $("#ParticipantsList").append(inner);
}
function deleteMyself(id)
{
    if(isEdit==1)
        $("#participant_"+id).remove();
}
