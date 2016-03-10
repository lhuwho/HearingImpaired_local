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
/*var _HearingList = new Array();
var _eEarList = new Array();
var _FMList = new Array();--All.js*/
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    $(".date").datepick({
        onSelect: function() {
            $(this).change();
        }
    });
    initPage();
    
    setEvents();
    zoneCityFunction();


   
    
    //haveRolesWaitFunction();
});
function setEvents() {
    $("#item1Content").fadeIn();

    $(".menuTabs").click(function() {
        $(".menuTabs").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        //$("input").add("select").add("textarea").attr("disabled", true);
        if (_MainID == null && act == 1) {
            $("input").add("select").add("textarea").attr("disabled", false);
        } else {
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
            $("input").add("select").add("textarea").attr("disabled", false);
            var index = ($(this).attr("id")).replace("item", "");
            var intIndex = parseInt(index);
            if (!isNaN(intIndex)) {
                getStuDataBase(intIndex);
            }
        }
        getStudentData(this.id);
        //haveRolesWaitFunction();
    });

    $(".showUploadImg").fancybox();

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $('.date').datepick();
        $("input").add("select").add("textarea").attr("disabled", false);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });
    $("input[name=AddressCopyaddress]").bind("click", function() {
        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
            $("#addressZip").val($("#censusAddressZip").val());
            $("#addressCity").children("option[value=" + $("#censusAddressCity :selected").val() + "]").attr("selected", true);
            $("#address").val($("#censusAddress").val());
        }
    });
    $(".searchStaff").unbind("click").click(function() {
        _staffIndex = $(this).attr("id");
        var inner = '<div id="inline"><br /><table border="0" width="360" id="searchTableinline">' +
                    '<tr><td width="80">員工姓名　<input type="text" id="gosrhstaffName" value="" /></td></tr>' +
        //'<tr><td>派任單位　<select id="gosrhstaffUnit"></select></td></tr>' +
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
                    //obj.gosrhstaffUnit = _uUnit;
                    AspAjax.SearchStaffDataBaseCount(obj);
                });
            }
        });
    });


    $('#studentbirthday').datepick({
        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5),
        onSelect: function(dates) {

            var stuAge = BirthdayFromDateFunction(dates[0]);
            $("#studentAge").val(stuAge[0]);
            $("#studentMonth").val(stuAge[1]);
        }
    }).change(function() {
        var sBirthday = TransformRepublicReturnValue($(this).val());
        var stuAge = BirthdayStringDateFunction(sBirthday);
        $("#studentAge").val(stuAge[0]);
        $("#studentMonth").val(stuAge[1]);
    });

    $('.fBirthday').datepick({
        yearRange: new Date().getFullYear() - 101 + ":" + (new Date().getFullYear() + 5),
        onSelect: function(dates) {
            //var TableNameID = $(this).parent().parent().attr("id");
            var indexID = ($(this).attr("id")).replace("fBirthday", "");
            var stuAge = BirthdayFromDateFunction(dates[0]);
            $("#fAge" + indexID).val(stuAge[0]);
        }
    }).change(function() {
        var indexID = ($(this).attr("id")).replace("fBirthday", "");
        var fBirthday = TransformRepublicReturnValue($(this).val());
        var fAge = BirthdayStringDateFunction(fBirthday);
        $("#fAge" + indexID).val(fAge[0]);
    });
}
function getView(id) {
    window.open("./student_database.aspx?id=" + id + "&act=2");
}

function getViewData(id,act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    AgencyUnitSelectFunction("item1Content", "Unit");
    haveRolesAndHide();
    if (id != null) {//update
        _ColumnID = id;
        $(".btnUpdate").fadeIn();
        //$("input").add("select").add("textarea").attr("disabled", true);
        /*$(".menuTabs").click(function() {
            var index = ($(this).attr("id")).replace("item", "");
            var intIndex = parseInt(index);
            if (!isNaN(intIndex)) {
                getStuDataBase(intIndex);
            }
        });*/
        $("#item1").click();
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#item3").add("#item5").add("#item6").add("#item7").add("#item8").hide();

        $("#upDate").val(TodayADDateFunction());
        //$("#caseUnit").children("option[value=" + _uUnit + "]").attr("selected", true);
        $("#caseUnit").children("option[value=" + _uUnit + "]").attr("selected", true); $("#fillInName").val(_uName);
       // setTimeout('$("#caseUnit").children("option[value=" + _uUnit + "]").attr("selected", true);$("#fillInName").val(_uName);', 500);
        $("#studentManualImgUrl").nextAll('span').hide();
        $("#studentManualImgUrl").hide();
        $("#studentPhotoUrl").nextAll('p').hide();
        $("#studentPhotoUrl").hide();
        $("#familyEcologicalUrl").hide();

        $("#Unit").change(function() {
            AspAjax.getUnitAutoNumber("StudentDB_", this.value);
        });
        AspAjax.getUnitAutoNumber("StudentDB_","");
    }
}

function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "getUnitAutoNumber":
            $("#studentID").html(result);
            break;
        case "SearchStudentDataBaseCount":
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
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].txtstudentID + '</td>' +
			                    '<td>' + _CaseStatu[result[i].txtstudentStatu] + '</td>' +
			                    '<td>' + result[i].txtstudentName + '</td>' +
			                    
			                    '<td>' + result[i].txtLegalRepresentative + '</td>' +
			                    '<td>' + result[i].txtLegalRepresentativeRelation + '</td>' + 
			                    
			                    '<td>' + result[i].txtLegalRepresentativePhone + '</td>' +
			                    '<td>' + result[i].txtLegalRepresentativeTel + '</td>' +
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
                        var Name = $(this).children("td:nth-child(4)").html();
                        $("#" + _staffIndex).val(Name);
                        var id = ($(this).attr("id")).replace("s_", "");
                        var staffIDHtml = _staffIndex.replace("Name","ID");
                        $("#" + staffIDHtml).html(id);
                        $.fancybox.close();
                    });


                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#inline .tableList").children("tbody").html("<tr><td colspan='4'>查無資料</td></tr>");
            }
            break;
        case "createStudentDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                //alert("新增成功");
                //window.location.href = './student_database.aspx?id=' + result[1] + '&act=2';
                var picSave = upStuPhoto(result[2],1);
                var picSave1 = upStuPhoto(result[2], 4);
                var obj = new Object;
                if ((picSave[0] && picSave[1] != null) || (picSave1[0] && picSave1[1] != null)) {
                    obj.ID = result[1];
                    obj.studentID = result[2];
                    MergerObject(obj, picSave[1]);
                    MergerObject(obj, picSave1[1]);
                    AspAjax.setCreateStudentPhotoDataBase(obj);
                }
            }
            break;
        case "createStudentDataBase3":
        case "createStudentDataBase8":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.reload();
            }
            break;
        case "setCreateStudentPhotoDataBase":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = './student_database.aspx?id=' + result[1] + '&act=2';
            }
            break;
        case "getStudentDataBase1":
        case "getStudentDataBase2":
        case "getStudentDataBase4":
        case "getstudentdhearInfo":
        case "getStudentDataBase7":
            if (!(result == null || result.length == 0 || result == undefined) && result.ID != null) {
                 if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    getOneStudentDataBase(result, methodName);
                    $("input").add("select").add("textarea").attr("disabled", true);
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            } else {
                alert("查無資料。");
            }
            break;
        case "getStudentDataBase3":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].checkNo != -1) {
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + TransformADFromStringFunction(result[i].resourceDate) + '</td>' +
			                    '<td>' + result[i].resourceID + '</td>' +
			                    '<td >' + result[i].resourceName + '</td>' +
			                    '<td>' + result[i].resourceItem + '</td>' +
			                    '<td>' + _resourceType[result[i].resourceType] + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="delResourceData(' + result[i].wID + ')">刪 除</button></td>' +
			                '</tr>';
                    }
                    $("#item3Content .table2Contact").children("tbody").html(inner);
                    $("input").add("select").add("textarea").attr("disabled", true);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }
            }
            break;
        case "getStudentDataBase8":
            if (!(result == null || result.length == 0 || result == undefined)) {
                $("#item8Content .table2Contact").children("body").empty();
                if (parseInt(result[0].checkNo) != -1) {
                    _ReturnStu8Value = result;
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr id="HS_' + result[i].RecordID + '" >' +
                                '<td align="center"><input class="RecordDate date" type="text" value="' + TransformADFromStringFunction(result[i].RecordDate) + '" size="10"/></td>' +
                                '<td align="center"><input class="RecordHeight" type="text" value="' + result[i].RecordHeight + '" size="10"/></td>' +
                                '<td align="center"><input class="RecordWeight" type="text" value="' + result[i].RecordWeight + '" size="10"/></td>' +
                                '<td align="center"><input class="RecordRemark" type="text" value="' + result[i].RecordRemark + '" size="50"/></td>' +
                                '<td align="center"><div class="UD"><button class="btnView" type="button" onclick="UpData(' + result[i].RecordID + ')">更 新</button> <button class="btnView" type="button" onclick="DelData(' + result[i].RecordID + ')">刪 除</button></div>' +
                                '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(' + result[i].RecordID + ')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(' + result[i].RecordID + ',' + i + ')">取 消</button></div>' +
                                '</tr>';
                    }
                    $("#item8Content .table2Contact").children("tbody").html(inner);
                    $('.date').datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                    $("#item8Content input[type=text]").attr("disabled", true);
                    $("input").add("select").add("textarea").attr("disabled", true);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].errorMsg);
                }

            }
            break;
        case "delStudentDataBase8":
            if (parseInt(result[0]) > 0) {
                alert("刪除成功");
                $.fancybox.hideActivity();
                $.fancybox.close();
                getStuDataBase(8);
            } else {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            }
            break;   
        case "delStudentDataBase3":
            if (parseInt(result[0]) > 0) {
                alert("刪除成功");
                $.fancybox.hideActivity();
                $.fancybox.close();
                getStuDataBase(3);
            } else {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            }
            break;
        case "setStudentDataBase1":
        case "setStudentDataBase2":
        case "setStudentDataBase4":
        case "setStudentDataBase5":
        case "setStudentDataBase6":
        case "setStudentDataBase7":
        case "setStudentDataBase8":
            if (parseInt(result[0]) > 0) {
                alert("更新成功");
                window.location.reload();
            } else {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            }
            break;

        case "searchResourceDataBaseCount":
            var obj = MyBase.getTextValueBase("searchTableinline");
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
                        AspAjax.searchResourceDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
            $("#StuinlineReturn").children("tbody").html("<tr><td colspan='3'>查無資料</td></tr>");
            } else {
            $("#StuinlineReturn").children("tbody").html("<tr><td colspan='3'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "searchResourceDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].txtstudentID != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr class="ImgJs" id="C_' + result[i].ID + '">' +
			                    '<td>' + result[i].txtresourceName + '</td>' +
			                    '<td>' + result[i].txtresourceItem + '</td>' +
			                    '<td>' + _resourceType[result[i].txtresourceType] + '</td>' +
			                '</tr>';
                    }
                    //
                    $("#StuinlineReturn").children("tbody").html(inner);
                    $("#StuinlineReturn .ImgJs").unbind('click').click(function() {
                        var index = (this.id).replace("C_","");
                        $("#resourceID").val(index);
                        $("#resourceName").html($(this).children("td:nth-child(1)").html());
                        $("#resourceItem").html($(this).children("td:nth-child(2)").html());
                        $("#resourceType").html($(this).children("td:nth-child(3)").html());
                        $.fancybox.close();
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].txtresourceName);
                }
            } else {
                $("#StuinlineReturn").children("tbody").html("<tr><td colspan='3'>查無資料</td></tr>");
            }
            break;
    }
}
function upStuPhoto(stuID,pType) {
    var picSave = false;
    var obj = new Object;
    var options = {
        type: "POST",
        url: 'Files.ashx?n=student&type=studentPhoto&ID=' + stuID,
        async: false,
        success: function(value) {
            var myObject = eval('(' + value + ')');
            if (myObject["error"] == undefined) {
                PicNameArray = myObject["Msg"];
                for (var item in PicNameArray) {
                    obj[item] = PicNameArray[item];
                }
                picSave = true;
            } else if (myObject["error"] == "NO PIC") {
                picSave = true;
            }
            else {
                alert(myObject["error"]);
            }
        }
    };
    // 將options傳給ajaxForm
    $("form[name=GmyForm" + pType + "]").ajaxSubmit(options);
    return [picSave, obj];
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function getStuDataBase(TypePage) {
    switch (TypePage) {
        case 1:
            AspAjax.getStudentDataBase1(_ColumnID);
            break;
        case 2:
            AspAjax.getStudentDataBase2(_ColumnID);
            break;
        case 3:
            $("#item3Content .table2Contact").children("tbody").empty();
            AspAjax.getStudentDataBase3(_ColumnID);
            break;
        case 4:
            AspAjax.getStudentDataBase4(_ColumnID);
            break;
        case 5:
        case 6:
            AspAjax.getstudentdhearInfo(_ColumnID);
            break;
        case 7:
            AspAjax.getStudentDataBase7(_ColumnID);
            break;
        case 8:
            AspAjax.getStudentDataBase8(_ColumnID);
            break;
    }
}
function saveStudentData(Type) {
    var obj = new Object();
    switch (Type) {
        case 0:
            var obj1 = saveStudentData1();
            var obj2 = saveStudentData2();
            var obj4 = saveStudentData4();
            MergerObject(obj, obj1);
            MergerObject(obj, obj2);
            MergerObject(obj, obj4);
            obj["sUnit"] = _uUnit;
            obj["FileIDName"] = _uId;
            break;
        case 1:
            obj = saveStudentData1();
            break;
        case 2:
            obj = saveStudentData2();
            break;
        case 4:
            obj = saveStudentData4();
            break;
    }
    if (Type == 1 || Type == 4) {
        var picSave = upStuPhoto(obj.studentID, Type);
        MergerObject(obj, picSave[1]);
    }
    switch (Type) {
        case 0://新增個案資料
            var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
            if (checkString.length > 0) {
                alert(checkString);
            }
            else {
                AspAjax.createStudentDataBase(obj);
            }
            break;
        case 1:
            AspAjax.setStudentBaseData(obj);
            break;
        case 2:
            AspAjax.setStudentDataBase2(obj);
            break;
        case 4:
            AspAjax.setStudentDataBase4(obj);
            break;
    }
    
}

function saveStudentData1() {
    var obj = new Object;
    obj = MyBase.getTextValueBase("item1Content");
    obj.ID = _ColumnID;
    obj.studentID = _StuID;
    /*var PrimaryContact = new Array();
    var itemArray = new Array();
    for (var i = 1; i <= 4; i++) {
        $("#PrimaryContact" + i + " input[type=text]").each(function() {
            itemArray.push($(this).val());
        });
        PrimaryContact.push(itemArray);
        itemArray = new Array();
    }
    obj["PrimaryContact"] = PrimaryContact;*/
    obj["notificationWhether"] = $("input[name=notification]:checked").val() == undefined ? 0 : $("input[name=notification]:checked").val();
    var notification = obj["notificationWhether"];
    obj["notificationUnit"] = $("#notificationDiv" + notification + " .notificationUnit").length > 0 ? $("#notificationDiv" + notification + " .notificationUnit").val() : "";
    obj["notificationManage"] = $("#notificationDiv" + notification + " .notificationManage").length > 0 ? $("#notificationDiv" + notification + " .notificationManage").val() : "";
    obj["notificationTel"] = $("#notificationDiv" + notification + " .notificationTel").length > 0 ? $("#notificationDiv" + notification + " .notificationTel").val() : "";
    obj["notificationDate"] = $("#notificationDiv" + notification + " .notificationDate").length > 0 ? TransformRepublicReturnValue($("#notificationDiv" + notification + " .notificationDate").val()) : "";
    obj["notificationCity"] = $("#notificationDiv" + notification + " .notificationCity").length > 0 ? $("#notificationDiv" + notification + " .notificationCity :selected").val() : "";
    return obj;
}
function saveStudentData2() {
    var obj = MyBase.getTextValueBase("item2Content");
    obj.ID = _ColumnID;
    obj.studentID = _StuID;
    /*var FamilyArray = new Array();
    for (i = 1; i <= 8; i++) {
    var Familyitem = new FamilyObject();
    Familyitem.Appellation = $("#Family" + i + " .fAppellation").val();
    Familyitem.Name = $("#Family" + i + " .fName").val();
    Familyitem.Birthday = TransformRepublicReturnValue($("#Family" + i + " .fBirthday").val());
    Familyitem.Education = $("#Family" + i + " select :selected").val() == undefined ? 0 : parseInt($("#Family" + i + " select :selected").val());
    Familyitem.Profession = $("#Family" + i + " .fProfession").val();
    Familyitem.Live = $("#Family" + i + " input[name=live" + i + "]:checked").val() == undefined ? 0 : parseInt($("#Family" + i + " input[name=live" + i + "]:checked").val());
    Familyitem.Hearing = $("#Family" + i + " input[name=hearing" + i + "]:checked").val() == undefined ? 0 : parseInt($("#Family" + i + " input[name=hearing" + i + "]:checked").val());
    Familyitem.Health = $("#Family" + i + " input[name=healthy" + i + "]:checked").val() == undefined ? 0 : parseInt($("#Family" + i + " input[name=healthy" + i + "]:checked").val());

        if (Familyitem.Health == 2) {
    Familyitem.HealthText = $("#Familyt0" + i).val();
    }

        FamilyArray.push(Familyitem);
    }
    obj["FamilyArray"] = FamilyArray;*/
    return obj;
}
function saveStudentData4() {
    var obj = MyBase.getTextValueBase("item4Content");
    var obj1 = getHideSpanValue("item4Content", "hideClassSpan");
    MergerObject(obj, obj1);
    obj.ID = _ColumnID;
    obj.studentID = _StuID;
    return obj;
}
function saveHearinghistory(Type) {
    if (Type == 1) {
        var obj = MyBase.getTextValueBase("item5Content");
        obj.ID = _ColumnID;
        obj.studentID = _StuID;
        AspAjax.setStudentDataBase5(obj);
    } else {
        var obj = MyBase.getTextValueBase("item6Content");
        var obj1 = getHideSpanValue("item6Content", "hideClassSpan");
        MergerObject(obj, obj1);
        obj.ID = _ColumnID;
        obj.studentID = _StuID;
        obj.accessory1 = parseInt(obj.accessory1) + "" == "NaN" ? "" : parseInt(obj.accessory1) + "";
        obj.accessory2 = parseInt(obj.accessory2) + "" == "NaN" ? "" : parseInt(obj.accessory1) + "";
        AspAjax.setStudentDataBase6(obj);
    }
   
}
function saveTeachhistory(Type) {
    var obj = MyBase.getTextValueBase("item7Content");
   // obj.teacherDate = $("#TeacherDate").val();
    obj.teacherID = $("#teacherID").html();
    obj.Rehabilitation3Text = $("#Rehabilitation3t01").val() + "@@@" + $("#Rehabilitation3t02").val() + "@@@" +$("#Rehabilitation3t03").val() ; //Rehabilitation3t01
    obj.ID = _ColumnID;
    AspAjax.setStudentDataBase7(obj);
}
function getOneStudentDataBase(result, methodName) {
    PushPageValue(result);
    if (methodName == "getStudentDataBase1") {
        _StuID = result.studentID;
        $("#caseStatu").html("個案狀態：" + _CaseStatu[result.caseStatu]);
        $("#Unit").children("option[value=" + result.Unit + "]").attr("selected", true);
        $("#upDate").unbind().removeClass();
        //$("#creatFileName").val(result.FileName);
        var stuAge = BirthdayStringDateFunction(result.studentbirthday);
        $("#studentAge").val(stuAge[0]);
        $("#studentMonth").val(stuAge[1]);
        if (result.censusAddressZip.length > 0 && result.censusAddressZip == result.addressZip && result.censusCity == result.addressCity && result.censusAddress == result.address) {
            $("input[name=AddressCopyaddress]").attr("checked", true);
        } else {
            $("input[name=AddressCopyaddress]").attr("checked", false);
        }

        $("input[name=notification][value=" + result.notificationWhether + "]").attr("checked", true);
        var itemindex = result.notificationWhether;
        $("#notificationDiv" + itemindex + " .notificationUnit").val(result.notificationUnit);
        $("#notificationDiv" + itemindex + " .notificationManage").val(result.notificationManage);
        $("#notificationDiv" + itemindex + " .notificationTel").val(result.notificationTel);
        $("#notificationDiv" + itemindex + " .notificationDate").val(TransformADFromStringFunction(result.notificationDate));
        $("#notificationDiv" + itemindex + " .notificationCity").children("option[value=" + result.notificationCity + "]").attr("selected", true);

        var path = "./uploads/student/" + result.studentID + "/print/";
        var path2 = "./uploads/student/" + result.studentID + "/org/";
        if (result.studentPhoto.length > 0) {
            $("#studentPhoto").attr("src", path + result.studentPhoto);
            $("#studentPhotoUrl").attr("href", path2 + result.studentPhoto);
        }
        if (result.studentManualImg.length > 0) {
            $("#studentManualImg").attr("src", path + result.studentManualImg);
            $("#studentManualImgUrl").attr("href", path2 + result.studentManualImg);
        }
    } else if (methodName == "getStudentDataBase2") {
        for (var i = 1; i <= 8; i++) {
            if (result["fBirthday" + i] != "1900-01-01") {
                var AgeItem = BirthdayStringDateFunction(result["fBirthday" + i]);
                $("#fAge" + i).val(AgeItem[0]);
            }
        }
    } else if (methodName == "getStudentDataBase4") {
        var path = "./uploads/student/" + result.studentID + "/print/";
        var path2 = "./uploads/student/" + result.studentID + "/org/";
        if (result.familyEcological.length > 0) {
            $("#familyEcological").attr("src", path + result.familyEcological);
            $("#familyEcologicalUrl").attr("href", path2 + result.familyEcological);
        }
    } else if (methodName == "getStudentDataBase7") {
     var RehousingText = result.Rehabilitation3Text.split('@@@');
     if (RehousingText.length > 0) {
         for (var i = 0; i < RehousingText.length; i++) {
             $("#Rehabilitation3t0" + (i + 1)).val(RehousingText[i]);
         }
     }
    } else if (methodName == "getstudentdhearInfo") {
        assistmanagebrandFunction();//All.js
        $("input[name=assistmanageR]").change();
        $("input[name=assistmanageL]").change();
        $("#brandR").children("option[value=" + result.brandR + "]").attr("selected", true);
        $("#brandL").children("option[value=" + result.brandL + "]").attr("selected", true);
    }
}
function saveBodyRecord() {
    var nowLength = $("#item8Content>table>tbody>tr").length-1;
    var objList = new Array();
    for (var i = 0; i < nowLength; i++) {
        var obj = new Object;
        if (($("#whTable" + (i + 1) + " .RecordDate").val()).length > 0) {
            obj.RecordID = $("#whTable" + (i + 1) + " .RecordID").html();
            obj.studentID = _StuID;
            obj.RecordDate = TransformRepublicReturnValue($("#whTable" + (i + 1) + " .RecordDate").val());
            obj.RecordHeight = $("#whTable" + (i + 1) + " .RecordHeight").val();
            obj.RecordWeight = $("#whTable" + (i + 1) + " .RecordWeight").val();
            obj.RecordRemark = $("#whTable" + (i + 1) + " .RecordRemark").val();
            objList.push(obj);
        }
    }
    AspAjax.setStudentDataBase8(objList);
}
function setHWResourceData() {
    var obj = MyBase.getTextValueBase("insertDataDiv2");
    obj.studentID = _StuID;
    var checkString = MyBase.noEmptyCheck(noEmptyItem8, obj, null, noEmptyShow8);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createStudentDataBase8(obj);
    }
}
function Search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var Date1 = false;
    var Date2 = false;
    var Date3 = false;
    if ((obj.txtbirthdaystart != null && obj.txtbirthdayend != null) || (obj.txtbirthdaystart == null && obj.txtbirthdayend == null)) {
        Date1 = true;
    }
    if ((obj.txtjoindaystart != null && obj.txtjoindayend != null) || (obj.txtjoindaystart == null && obj.txtjoindayend == null)) {
        Date2 = true;
    }
    if ((obj.txtendReasonDatestart != null && obj.txtendReasonDateend != null) || (obj.txtendReasonDatestart == null && obj.txtendReasonDateend == null)) {
        Date3 = true;
    }
    if (Date1 && Date2 && Date3) {
        AspAjax.SearchStudentDataBaseCount(obj);
    } else {
        alert("請填寫完整日期區間");
    }

}
function InsertData(Type) {
    $("#insertDataDiv1").show();
    $("#resourceID").add("#resourceDate").empty();
    /*date鎖住時的順序**/
    $("#insertDataDiv1 .date").datepick({
        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
    });
    $("#resourceDate").val(TodayADDateFunction());
    $("#insertDataDiv1 input").attr("disabled", false); //解鎖放最後面
    /***/
    $("#resourceID").unbind("click").click(function() {
        var inner = '<div id="inline"><br /><table border="0" width="360" id="searchTableinline">' +
                    '<tr><td width="80">機構名稱 <input id="gosrhresourceName" type="text" value="" /></td></tr>' +
                    '<tr><td>類　　別 <select id="gosrhresourceType"><option value="0">請選擇類別</option><option value="1">復健與醫療</option><option value="2">經濟</option><option value="3">福利服務</option><option value="4">教育</option><option value="5">其他</option></select></td></tr>' +
                    '<tr><td>資源項目 <input id="gosrhresourceItem" type="text" value="" /></td></tr>' +
                    '<tr><td height="40" align="center" colspan="2"><button class="btnSearch" type="button">查 詢</button></td></tr>' +
                    '</table><br />' +
                    '<table id="StuinlineReturn" class="tableList" border="0"  width="360">' +
                    '<thead>' +
			                '<tr>' +
			                    '<th width="130">機構名稱</th>' +
			                    '<th width="130">類別</th>' +
			                    '<th width="100">資源項目</th>' +
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
                    var obj = MyBase.getTextValueBase("searchTableinline");
                    AspAjax.searchResourceDataBaseCount(obj);
                });
            }
        });
    });
}
function InsertData2() {
    $("#insertDataDiv2").show();
    /*date鎖住時的順序**/
    $("#insertDataDiv2 .date").datepick({
        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
    });
    $("#RecordDate").val(TodayADDateFunction());
    $("#insertDataDiv2 input").attr("disabled", false).empty(); //解鎖放最後面
}
function cancelInsert(Type) {
    $("#insertDataDiv" + Type).hide();
}
function setResourceData() {
    var obj = MyBase.getTextValueBase("insertDataDiv1");
    obj.studentID = _StuID;
    var checkString = MyBase.noEmptyCheck(noEmptyItem3, obj, null, noEmptyShow3);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createStudentDataBase3(obj);
    }
}
function delResourceData(wID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delStudentDataBase3(parseInt(wID));
    });
}

function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}

function SaveData(TrID) {
    var obj = new Object();
    obj.RecordID = parseInt(TrID);
    obj.studentID = _StuID;
    obj.RecordDate = TransformRepublicReturnValue($("#HS_" + TrID + " .RecordDate").val());
    obj.RecordHeight = $("#HS_" + TrID + " .RecordHeight").val();
    obj.RecordWeight = $("#HS_" + TrID + " .RecordWeight").val();
    obj.RecordRemark = $("#HS_" + TrID + " .RecordRemark").val();

    var checkString = MyBase.noEmptyCheck(noEmptyItem8, obj, null, noEmptyShow8);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setStudentDataBase8(obj);
    }
}
function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .RecordDate").val(TransformADFromStringFunction(_ReturnStu8Value[vIndex].RecordDate));
    $("#HS_" + TrID + " .RecordHeight").val(_ReturnStu8Value[vIndex].RecordHeight);
    $("#HS_" + TrID + " .RecordWeight").val(_ReturnStu8Value[vIndex].RecordWeight);
    $("#HS_" + TrID + " .RecordRemark").val(_ReturnStu8Value[vIndex].RecordRemark);
    
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
        AspAjax.delStudentDataBase8(TrID);
    });
}

function DateOrderCheck(Level) {
    var _DateList = ["guaranteeDate", "firstClassDate", "endReasonDate", "BackGuaranteeDate"];

    for (var i = Level; i >= 0; i--) {

        var StartDate = TransformRepublicReturnValue($("#" + _DateList[i]).val());
        var EndDate = TransformRepublicReturnValue($("#" + _DateList[i + 1]).val());

        if (StartDate > EndDate || StartDate == '' || EndDate == '') {
            
             
            alert("注意日期順序！");        
            $("#" + _DateList[i + 1]).val('');
            $("#" + _DateList[i + 1]).focus();
            
            break;
        }
    }

}


/*
var loadingUrl = "Image/loading.gif";

var uploading = function(itemid) {
    var el = $("#uploading" + itemid);
    $("#ifUpload" + itemid).fadeOut("fast");
    el.show();
    el.html("<img src='" + loadingUrl + "' align='absmiddle' />上傳中");
    return el;
};


var uploaderror = function(itemid) {
    alert(itemid);

};


var uploadsuccess = function(newpath, itemid, height, width) {
    if (newpath == "noSession") {
        alert("Session發生問題");
    }
    else {
        imgheight = height;
        imgwidth = width;
        $("#" + itemid).attr("src",newpath + '?ts=' + new Date().getTime());
        
    }

};*/
