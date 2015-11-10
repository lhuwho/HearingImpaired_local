var MyBase = new Base();
var Timer = '<option value="0">00</option><option value="1">01</option><option value="2">02</option><option value="3">03</option><option value="4">04</option><option value="5">05</option><option value="6">06</option><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option><option value="22">22</option><option value="23">23</option><option value="24">24</option><option value="25">25</option><option value="26">26</option><option value="27">27</option><option value="28">28</option><option value="29">29</option><option value="30">30</option><option value="31">31</option><option value="32">32</option><option value="33">33</option><option value="34">34</option><option value="35">35</option><option value="36">36</option><option value="37">37</option><option value="38">38</option><option value="39">39</option><option value="40">40</option><option value="41">41</option><option value="42">42</option><option value="43">43</option><option value="44">44</option><option value="45">45</option><option value="46">46</option><option value="47">47</option><option value="48">48</option><option value="49">49</option><option value="50">50</option><option value="51">51</option><option value="52">52</option><option value="53">53</option><option value="54">54</option><option value="55">55</option><option value="56">56</option><option value="57">57</option><option value="58">58</option><option value="59">59</option>';
$(document).ready(function() {
AspAjax.set_defaultSucceededCallback(SucceededCallback);
//AspAjax.set_defaultFailedCallback(FailedCallback);
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $('.date').datepick();
    initPage();
});
function Search() { //主頁面 顯示名單
    if ($("#gosrhstaffBirthdayStart").val() != "") {
        $("#mainSearchList .tableList").children("tbody").empty();
        var obj = MyBase.getTextValueBase("searchTable");
        AspAjax.SearchStaffDataBaseWorkCount(obj);
    }
    else { $("#mainSearchList .tableList").children("tbody").empty();alert("請選擇日期");}

}

function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "SearchStaffDataBaseWorkCount":
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
                        AspAjax.SearchStaffDataBaseWork(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBaseWork":
            if (!(result == null || result.length == 0 || result == undefined)) {
                //  alert(result.length);
                //  if (parseInt(result[0].sID) != -1) {
                var inner = "";
                for (var i = 0; i < result.length; i++) {
                    var TimeOK = true;
                    var cardTime = '';
                    for (var j = 0; j < result[i].WorkRecord.length; j++) {
                        cardTime += result[i].WorkRecord[j].CreateFileDate + "<br>";
                        if (j == 0) {
                            var ScheduleDate = "2015-8-30 " + result[i].WorkRecord[j].CreateFileDate + ":00";
                            var CurrentDate = "2015-8-30 08:50:00";
                            if ((Date.parse(ScheduleDate)).valueOf() >= (Date.parse(CurrentDate)).valueOf()) {
                                TimeOK = false;
                            }
                        }
                        if (j == result[i].WorkRecord.length - 1) {
                            var ScheduleDate = "2015-8-30 " + result[i].WorkRecord[j].CreateFileDate + ":00";
                            var CurrentDate = "2015-8-30 18:00:00";
                            if ((Date.parse(ScheduleDate)).valueOf() <= (Date.parse(CurrentDate)).valueOf()) {
                                TimeOK = false;
                            }
                        }


                    }


                    inner += '<tr>' +
                        '<td>' + result[i].StaffID + '</td>' +
                        '<td>' + result[i].StaffName + '</td>';
                    if (TimeOK) {
                        inner += '<td>' + cardTime + '</td>';
                    } else {
                        inner += '<td><span class="startMark">' + cardTime + '</span></td>';
                    }
                    inner += '<td>';
                    if (result[i].WorkRecordManage.length > 0) { inner += '有請假記錄'; }
                    inner += '</td>';
                    inner += '<td><button class="btnView" type="button" onclick="viewRecord(' + result[i].StaffID + ')">檢 視</button></td>' +
			                '</tr>';
                }
                //
                $("#mainSearchList .tableList").children("tbody").html(inner);
                //                } else {
                //                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                //                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "GetWorkRecordManage":
            if (!(result == null || result.length == 0 || result == undefined)) {
                for (var i = 0; i < result.length; i++) {
                    $("#startTime" + (i + 1)).val(result[i].StartTime);
                    $("#endTime" + (i + 1)).val(result[i].EndTime);
                    $("#startMin" + (i + 1)).val(result[i].StartMin);
                    $("#EndtMin" + (i + 1)).val(result[i].EndMin);
                    $("#vacationStyle" + (i + 1)).val(result[i].VacationType);
                    $("#VacationMark" + (i + 1)).val(result[i].VacationMark);
                    $("#RealStart" + (i + 1)).html(result[i].RealStart);
                    $("#RealEnd" + (i + 1)).html(result[i].RealEnd);
                }
            }

            break;

    }
}

//function UpData(TrID) {
//    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", false);
//    $("#HS_" + TrID + " .UD").hide();
//    $("#HS_" + TrID + " .SC").show();
//}
//function SaveData(TrID) {
//    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", true);
//    $("#HS_" + TrID + " .UD").show();
//    $("#HS_" + TrID + " .SC").hide();
//}
//function cancelInsert() {
//    $(".insertDataDiv").hide();
//    $(".btnAdd").fadeIn();
//}

//function showView(viewID) {
//    var DivName = '';
//    switch (viewID) {
//        case 1:
//            DivName = "#mainSearchList";
//            break;
//        case 2:
//            DivName = "#mainIndexList";
//            break;
//        case 3:
//            DivName = "#mainIndexList2";
//            break;
//        case 4:
//            DivName = "#mainIndexList3";
//            break;
//        case 5:
//            DivName = "#mainIndexList4";
//            break;
//    }
//    $(DivName).fadeIn();
//    $(DivName + " input[type=text]").add(DivName + " select").attr("disabled", true);
//}

function viewRecord(sid ) {
    var inner = '<div id="inline"><span id="StaffID" class="hideClassSpan">' + sid + '</span>' +
                    '<table class="tableList" border="0" width="500">' +
                    '<caption>出勤紀錄</caption>' +
                    '<thead>' +
		                '<tr>' +
		                    '<th width="300">時間</th>' +
		                    '<th width="200">狀態</th>' +
		                '</tr>' +
		            '</thead>' + 
		                '<tr><td width="250">' +
		                '<span id="RealStart1" class="hideClassSpan"></span><span id="RealEnd1" class="hideClassSpan"></span>' +
		                '<select id="startTime1" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="startMin1">' + Timer + '</select> ~ ' +
		                '<select id="endTime1" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="EndtMin1">' + Timer + '</select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle1" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">減-工作異動</option><option value="13">加-工作異動</option></select><input type="text" id="VacationMark1"  maxlength="20" value=""></td>' +
		                '</tr>' +
		                 '<tr><td width="250">' +
		                '<span id="RealStart2" class="hideClassSpan"></span><span id="RealEnd2" class="hideClassSpan"></span>' +
		               '<select id="startTime2" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="startMin2">' + Timer + '</select> ~ ' +
		                '<select id="endTime2" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="EndtMin2">' + Timer + '</select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle2" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">減-工作異動</option><option value="13">加-工作異動</option></select><input type="text" id="VacationMark2"  maxlength="20" value=""></td>' +
		                '</tr>' +
		                 '<tr><td width="250">' +
		                '<span id="RealStart3" class="hideClassSpan"></span><span id="RealEnd3" class="hideClassSpan"></span>' +
                        '<select id="startTime3" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="startMin3">' + Timer + '</select> ~ ' +
		                '<select id="endTime3" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="EndtMin3">' + Timer + '</select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle3" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">減-工作異動</option><option value="13">加-工作異動</option></select><input type="text" id="VacationMark3"  maxlength="20" value=""></td>' +
		                '</tr>' +
		                 '<tr><td width="250">' +
		                '<span id="RealStart4" class="hideClassSpan"></span><span id="RealEnd4" class="hideClassSpan"></span>' +
		                '<select id="startTime4" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="startMin4">' + Timer + '</select> ~ ' +
		                '<select id="endTime4" ><option value="7">07</option><option value="8">08</option><option value="9">09</option><option value="10">10</option><option value="11">11</option><option value="12">12</option><option value="13">13</option><option value="14">14</option><option value="15">15</option><option value="16">16</option><option value="17">17</option><option value="18">18</option><option value="19">19</option><option value="20">20</option><option value="21">21</option></select>' +
		                '<select id="EndtMin4">' + Timer + '</select>' +
		                '</td>' +
		                '<td align="center"><select  id="vacationStyle4" ><option value="0">請選擇假別</option><option value="1">正常</option><option value="2">事假</option>' +
	                        '<option value="3">病假</option><option value="4">遲到</option><option value="5">特休</option>' +
	                        '<option value="6">公假</option><option value="7">婚假</option><option value="8">產假</option>' +
	                        '<option value="9">喪假</option><option value="10">公傷</option><option value="11">未打卡</option>' +
	                        '<option value="12">減-工作異動</option><option value="13">加-工作異動</option></select><input type="text" id="VacationMark4"   maxlength="20" value=""></td>' +
		                '</tr>' +
                    '</table>' +
                    '<div id="inline2">'+
                    '<button class="btnSearch" type="button">存 檔</button>' +
                    '</div></div>';
    $.fancybox({
        'content': inner,
        'autoDimensions': true,
        'centerOnScroll': true,
        'hideOnOverlayClick': false,
        'onComplete': function() {

            $('button').css({ "background-color": "#F9AE56" });
            $("#inline2 .btnSearch").unbind("click").click(function() {
                var obj = new Array();
                for (var i = 1; i <= 4; i++) {
                    var data = {};
                    data.StaffID = $("#StaffID").html();
                    data.Date = TransformRepublicReturnValue($("#gosrhstaffBirthdayStart").val());
                    data.RealStart = $("#RealStart" + i).html();
                    
                    data.RealEnd = $("#RealEnd" + i).html();
                    data.StartTime = $("#startTime" + i).val();
                    data.StartMin = $("#startMin" + i).val();
                    data.EndTime = $("#endTime" + i).val();
                    data.EndMin = $("#EndtMin" + i).val();
                    data.VacationType = $("#vacationStyle" + i).val();
                    data.VacationMark = $("#VacationMark" + i).val();
                    if (data.VacationType != 0) {
                        obj[obj.length] = data;
                    }
                }
                if (obj.length > 0) {
                    AspAjax.SetWorkRecordManage(obj);
                    //alert(obj[0].StaffID + "---" + obj[0].Date + "---" + obj[0].StartTime);
                }

            });
        }
    });
    var getWork = {};
    getWork.StaffID = $("#StaffID").html();
    getWork.Date = TransformRepublicReturnValue($("#gosrhstaffBirthdayStart").val());
    AspAjax.GetWorkRecordManage(getWork);
}

