var MyBase = new Base();
var noEmptyItem = ["staffName", "officeDate", "unit", "fillInDate", "applyJob", "jobTitle",  "staffbirthday", "staffsex", "staffTWID"];
var noEmptyShow = ["員工姓名", "到職日", "派任單位", "填表日期", "職稱", "職務",  "出生日期", "性別", "身份證字號"];
var actNumber = 2;
//"jobLevel","職等",
var _viewstaffID = 0;
var _ReturnValue;



$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    ApplyJobSelectFunction("searchTable", "gosrhstaffJob");
    ApplyJobSelectFunction("", "applyJob");
    initPage();
    _id = GetQueryString("id");
    _act = GetQueryString("act");
    if (_id != null || _act != null) {
        if (_act != null) {
            getViewData(_id, _act);
        } else {
            window.location.href = './staff_database.aspx';
        }
    }
    AgencyUnitSelectFunction("mainSearchForm", "gosrhstaffUnit");
    $("#item1").add("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#item1Content").fadeIn();

    $(".menuTabs").click(function() {
        $(".menuTabs").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("input").add("select").add("textarea").attr("disabled", true);
        if (_id == null && (_act == 1)) {
            $("input").add("select").add("textarea").attr("disabled", false);
        } else {
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
        }
        getStudentData(this.id);
        $("#insertDataDiv1").add("#insertDataDiv2").add("#insertDataDiv3").hide();
        getViewData(_id, _act);
    });

    $(".showUploadImg").fancybox();

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#fillInDate").attr("readonly", "readonly");
    });

    $(".btnAdd").click(function() {
        $("#item8Content>table tr:last-child").after($("#whTable").clone().attr("id", ""));
    });



    $("#mainSearchForm .btnSearch").click(function() {
        SearchStaffData();
    });
    $("#year_ddl1").add("#month_ddl1").add("#day_ddl1").change(function() {
        countAge();
    });

    $("#CAddressCopyaddress").bind("click", function() {
        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
            $("#Azip").val($("#Censuszip").val());
            $("#AAddress").val($("#CensusAddress").val());
        }

    });

    $('#staffbirthday').datepick({
        yearRange: new Date().getFullYear() - 101 + ":" + (new Date().getFullYear() + 5),
        onSelect: function(dates) {
            var stuAge = BirthdayFromDateFunction(dates[0]);
            $("#staffAge").val(stuAge[0]);
            $("#staffMonth").val(stuAge[1]);
        }
    });
    $('#staffbirthday').change(function() {
        var staffBirehday = TransformRepublic($(this).val());
        var stuAge = BirthdayStringDateFunction(staffBirehday);
        $("#staffAge").val(stuAge[0]);
        $("#staffMonth").val(stuAge[1]);
    });
    zoneCityFunction();
    $("input[name=AddressCopyaddress]").bind("click", function() {
        if ($(this).attr("checked") == true || $(this).attr("checked") == "checked") {
            $("#addressZip").val($("#censusAddressZip").val());
            $("#addressCity").children("option[value=" + $("#censusCity :selected").val() + "]").attr("selected", true);
            $("#address").val($("#censusAddress").val());
        }
    });
});

function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    SucceededCallbackAll(result, userContext, methodName);
    switch (methodName) {
        case "getUnitAutoNumber":
            $("#staffID").html(result);
            break;
        case "CreateUserMemberData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，發生錯誤如下：" + result[1]);
            } else {
                /*alert("新增成功");
                window.location.href = "./staff_database.aspx?id=" + result[1] + "&act=2";*/
                var picSave = upStaffPhoto(result[2]);
                var obj = new Object;
                if (picSave[0] && picSave[1] != null) {
                    obj.ID = result[1];
                    obj.staffID = result[2];
                    MergerObject(obj, picSave[1])
                    AspAjax.setCreateUserMemberPhotoData(obj);
                }
            }
            break;
        case "SearchStaffDataBaseCount":
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
                        AspAjax.SearchStaffDataBase(parseInt((index + 1) * _LimitPage, 10), obj);
                        return false;
                    }
                });
            } else if (pageCount == 0) {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>發生錯誤，錯誤訊息如下：" + result[1] + "</td></tr>");
            }
            break;
        case "SearchStaffDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].sID) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                '<td>' + result[i].sID + '</td>' +
			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
			                    '<td>' + result[i].sName + '</td>' +
			                    '<td>' + _JobItemList[result[i].sJob] + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].officeDate) + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].resignDate) + '</td>' +
			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                    //
                    $("#mainSearchList .tableList").children("tbody").html(inner);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                }
            } else {
                $("#mainSearchList .tableList").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
        case "getOneStaffDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    getOneStaffData(result);
                    $("#fillInDate").unbind().removeClass();
                    $(".staffNameData").html("員工姓名：" + result.StaffBaseData.staffName);
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下:" + result.errorMsg);
                }
            } else {
                alert("查無資料");
            }
            break;
        case "UpOneStaffDataBase":
        case "setStaffWorkData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                //window.location.reload();
                AspAjax.getOneStaffDataBase(_id);
            }
            break;
        case "createStaffWorkData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("新增成功");
                //window.location.reload();
                AspAjax.getOneStaffDataBase(_id);
            }
            break;
        case "delStaffWorkData":
            $.fancybox.close();
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("刪除成功");
                //window.location.reload();
                AspAjax.getOneStaffDataBase(_id);
            }
            break;
        case "setCreateUserMemberPhotoData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("新增成功");
                window.location.href = "./staff_database.aspx?id=" + result[1] + "&act=2";
            }
            break;
        case "AddYearVacation":
            _id = GetQueryString("id");
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下：" + result[1]);
            } else {
                alert("新增年假成功");
                window.location.href = "./staff_database.aspx?id=" + _id + "&act=2";
            }
            break;
        case "GetYearVacation":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0].ID) != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += '<tr>' +
                                //'<td>' + result[i].sID + '</td>' +
			                    '<td>' + result[i].Year + '</td>' +
			                    '<td>' + result[i].YearVacation + '</td>' +
			                    '<td>' + result[i].WorkAdd + '</td>' +
			                    '<td>' + result[i].WorkMinus + '</td>' +
			                    //'<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
			                '</tr>';
                    }
                     $("#WorkDataDiv4").children("tbody").html(inner);
                }
                //                if (parseInt(result[0].sID) != -1) {
                //                    var inner = "";
                //                    for (var i = 0; i < result.length; i++) {
                //                        inner += '<tr>' +
                //                                '<td>' + result[i].sID + '</td>' +
                //			                    '<td>' + _UnitList[result[i].sUnit] + '</td>' +
                //			                    '<td>' + result[i].sName + '</td>' +
                //			                    '<td>' + _JobItemList[result[i].sJob] + '</td>' +
                //			                    '<td>' + TransformADFromStringFunction(result[i].officeDate) + '</td>' +
                //			                    '<td>' + TransformADFromStringFunction(result[i].resignDate) + '</td>' +
                //			                    '<td><button class="btnView" type="button" onclick="getView(' + result[i].ID + ')">檢 視</button></td>' +
                //			                '</tr>';
                //                    }
                //                    //
                //                    $("#mainSearchList .tableList").children("tbody").html(inner);
                //                } else {
                //                    alert("發生錯誤，錯誤訊息如下：" + result[0].sName);
                //                }
            } else {
                 $("#WorkDataDiv4").children("tbody").html("<tr><td colspan='7'>查無資料</td></tr>");
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);

}
function getView(id) {
    window.open("./staff_database.aspx?id=" + id + "&act=" + actNumber);
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    /*if (act == 3) {
        actNumber = 31;
        $("#mainMenuList").add("#mainContentSearch").fadeIn();
        $("#main").hide();
        $("#mainMenuList").find("a").attr("href", "./staff_database.aspx?act=31");
        $("#mainClass").html("人事管理&gt; 外聘教師資料維護");
        document.title = "人事管理 - 外聘教師資料維護 | 財團法人中華民國婦聯聽障文教基金會管理後臺";
    } else if (id == null && act == 31) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#mainClass").html("人事管理&gt; 外聘教師資料維護");
        document.title = "人事管理 - 外聘教師資料維護 | 財團法人中華民國婦聯聽障文教基金會管理後臺";
    } else if (id != null && act == 31) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        $("#staffName").val("王小明");
        $("#comeCity").val("台灣台北");
        $("#mainClass").html("人事管理&gt; 外聘教師資料維護");
        document.title = "人事管理 - 外聘教師資料維護 | 財團法人中華民國婦聯聽障文教基金會管理後臺";
    } else */
    
    if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("#item2").add("#item3").add("#item4").hide();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("input[name=unit]").change(function() {
        AspAjax.getUnitAutoNumber("StaffDB_", this.value);
        });
        AspAjax.getUnitAutoNumber("StaffDB_", "");
    } else if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        _ColumnID = id;
        AspAjax.getOneStaffDataBase(id);
    }

}
function SetStaffData(Type) {
    var obj = new Object();
    obj = MyBase.getTextValueBase("item1Content");
    obj.DSince = $("#Dyear1").val() + "@@" + $("#Dmonth1").val();
    obj.DUntil = $("#Dyear2").val() + "@@" + $("#Dmonth2").val();
    obj.MSince = $("#Myear1").val() + "@@" + $("#Mmonth1").val();
    obj.MUntil = $("#Myear2").val() + "@@" + $("#Mmonth2").val();
    obj.USince = $("#Uyear1").val() + "@@" + $("#Umonth1").val();
    obj.UUntil = $("#Uyear2").val() + "@@" + $("#Umonth2").val();
    obj.VSince = $("#Vyear1").val() + "@@" + $("#Vmonth1").val();
    obj.VUntil = $("#Vyear2").val() + "@@" + $("#Vmonth2").val();
    obj.JDateSince1 = $("#Jyear1_1").val() + "@@" + $("#Jmonth1_1").val();
    obj.JDateUntil1 = $("#Jyear1_2").val() + "@@" + $("#Jmonth1_2").val();
    obj.JDateSince2 = $("#Jyear2_1").val() + "@@" + $("#Jmonth2_1").val();
    obj.JDateUntil2 = $("#Jyear2_2").val() + "@@" + $("#Jmonth2_2").val();
    obj.JDateSince3 = $("#Jyear3_1").val() + "@@" + $("#Jmonth3_1").val();
    obj.JDateUntil3 = $("#Jyear3_2").val() + "@@" + $("#Jmonth3_2").val();
    obj.JDateSince4 = $("#Jyear4_1").val() + "@@" + $("#Jmonth4_1").val();
    obj.JDateUntil4 = $("#Jyear4_2").val() + "@@" + $("#Jmonth4_2").val();
    var FamilyStatus = new Array();
    var FamilyStatusItem = new Array();
    $("#FamilyStatus input[type=text]").each(function() {
        if (FamilyStatusItem.length == 4) {
            FamilyStatus.push(FamilyStatusItem);
            FamilyStatusItem = new Array();
        }
        FamilyStatusItem.push($(this).val());

    });
    FamilyStatus.push(FamilyStatusItem);
    obj.FamilyStatu = FamilyStatus;
    var SpecialtySkill = new Array();
    var SpecialtySkillItem = new Array();
    $("#SpecialtySkillTable input[type=text]").each(function() {
        if (SpecialtySkillItem.length == 6) {
            SpecialtySkill.push(SpecialtySkillItem);
            SpecialtySkillItem = new Array();
        }
        var thisCalss = $(this).attr("class");
        var ItemValue = $(this).val();
        if (thisCalss != undefined && thisCalss.indexOf("date") != -1) {
            var ItemValue = TransformRepublicReturnValue(ItemValue);
        }
        SpecialtySkillItem.push(ItemValue);
    });
    SpecialtySkill.push(SpecialtySkillItem);
    obj.SpecialtySkill = SpecialtySkill;

    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0 || obj.jobLevel == "-1") {
        var item = "";
        if (checkString.length > 0) {
            item = checkString;
        }
        if (obj.jobLevel == "-1") {
            if (item.length > 0) {
                item += "、職等";
            } else {
                item += "未填寫：職等";
            }
        }
        alert(item);
    } else {
        if (Type == 0) {
            AspAjax.CreateUserMemberData(obj);
        } else if (Type == 1) {
        obj.ID = _ColumnID;
        obj.staffID = $("#staffID").html();
            var picSave = upStaffPhoto(obj.staffID);
            MergerObject(obj, picSave[1])
            AspAjax.UpOneStaffDataBase(obj);
        }
    }
}
function upStaffPhoto(staffID) {
    var picSave = false;
    var obj = new Object;
    var options = {
        type: "POST",
        url: 'Files.ashx?n=staff&type=staffPhoto&ID=' + staffID,
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
    $("form[name=GmyForm1]").ajaxSubmit(options);
    return [picSave, obj];
}
function SearchStaffData() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    var SearchBirthday = false;
    if ((obj.txtstaffBirthdayStart != null && obj.txtstaffBirthdayEnd != null) || (obj.txtstaffBirthdayStart == null && obj.txtstaffBirthdayEnd == null)) {
        SearchBirthday = true;
    }
    if (SearchBirthday) {
        AspAjax.SearchStaffDataBaseCount(obj);
    } else {
        alert("請填寫完整出生日期區間");
    }
}
function getOneStaffData(result) {
    PushPageValue(result.StaffBaseData);
    _viewstaffID = result.StaffBaseData.staffID;
    var stuAge = BirthdayStringDateFunction(result.StaffBaseData.staffbirthday);
    $("#staffAge").val(stuAge[0]);
    $("#staffMonth").val(stuAge[1]);
    if (result.StaffBaseData.censusAddressZip.length > 0 && result.StaffBaseData.censusAddressZip == result.StaffBaseData.addressZip && result.StaffBaseData.censusCity == result.StaffBaseData.addressCity && result.StaffBaseData.censusAddress == result.StaffBaseData.address) {
        $("input[name=AddressCopyaddress]").attr("checked", true);
    } else {
        $("input[name=AddressCopyaddress]").attr("checked", false);
    }

    var path = "./uploads/staff/" + result.StaffBaseData.staffID + "/print/";
    var path2 = "./uploads/staff/" + result.StaffBaseData.staffID + "/org/";
    if (result.StaffBaseData.staffPhoto.length > 0) {
        $("#staffPhoto").attr("src", path + result.StaffBaseData.staffPhoto);
        $("#staffPhotoUrl").attr("href", path2 + result.StaffBaseData.staffPhoto);
    }
    
    var sDateTime = "@@";
    sDateTime = (result.StaffBaseData.DSince).split("@@");
    $("#Dyear1").val(sDateTime[0]);
    $("#Dmonth1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.DUntil).split("@@");
    $("#Dyear2").val(sDateTime[0]);
    $("#Dmonth2").val(sDateTime[1]);

    sDateTime = (result.StaffBaseData.MSince).split("@@");
    $("#Myear1").val(sDateTime[0]);
    $("#Mmonth1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.MUntil).split("@@");
    $("#Myear2").val(sDateTime[0]);
    $("#Mmonth2").val(sDateTime[1]);

    sDateTime = (result.StaffBaseData.USince).split("@@");
    $("#Uyear1").val(sDateTime[0]);
    $("#Umonth1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.UUntil).split("@@");
    $("#Uyear2").val(sDateTime[0]);
    $("#Umonth2").val(sDateTime[1]);

    sDateTime = (result.StaffBaseData.VSince).split("@@");
    $("#Vyear1").val(sDateTime[0]);
    $("#Vmonth1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.VUntil).split("@@");
    $("#Vyear2").val(sDateTime[0]);
    $("#Vmonth2").val(sDateTime[1]);
/**/
    sDateTime = (result.StaffBaseData.JDateSince1).split("@@");
    $("#Jyear1_1").val(sDateTime[0]);
    $("#Jmonth1_1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.JDateUntil1).split("@@");
    $("#Jyear1_2").val(sDateTime[0]);
    $("#Jmonth1_2").val(sDateTime[1]);

    sDateTime = (result.StaffBaseData.JDateSince2).split("@@");
    $("#Jyear2_1").val(sDateTime[0]);
    $("#Jmonth2_1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.JDateUntil2).split("@@");
    $("#Jyear2_2").val(sDateTime[0]);
    $("#Jmonth2_2").val(sDateTime[1]);

    sDateTime = (result.StaffBaseData.JDateSince3).split("@@");
    $("#Jyear3_1").val(sDateTime[0]);
    $("#Jmonth3_1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.JDateUntil3).split("@@");
    $("#Jyear3_2").val(sDateTime[0]);
    $("#Jmonth3_2").val(sDateTime[1]);

    sDateTime = (result.StaffBaseData.JDateSince3).split("@@");
    $("#Jyear3_1").val(sDateTime[0]);
    $("#Jmonth3_1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.JDateUntil3).split("@@");
    $("#Jyear3_2").val(sDateTime[0]);
    $("#Jmonth3_2").val(sDateTime[1]);

    sDateTime = (result.StaffBaseData.JDateSince4).split("@@");
    $("#Jyear4_1").val(sDateTime[0]);
    $("#Jmonth4_1").val(sDateTime[1]);
    sDateTime = (result.StaffBaseData.JDateUntil4).split("@@");
    $("#Jyear4_2").val(sDateTime[0]);
    $("#Jmonth4_2").val(sDateTime[1]);

    var item = result.StaffBaseData.FamilyStatu[0];
    i = 0;
    var j = 1;
    $("#FamilyStatus input[type=text]").each(function() {
        if (i == 4) {
            item = result.StaffBaseData.FamilyStatu[j];
            i = 0;
            j++;
        }
        if (item[i] != "0") {
            $(this).val(item[i]);
        }
        i++;
    });

    var item = result.StaffBaseData.SpecialtySkill[0];
    i = 0;
    var j = 1;
    $("#SpecialtySkillTable input[type=text]").each(function() {
        if (i == 6) {
            item = result.StaffBaseData.SpecialtySkill[j];
            i = 0;
            j++;
        }
        var thisCalss = $(this).attr("class");
        var ItemValue = item[i];
        var aa = $(this).attr("id");
        if (thisCalss != undefined && thisCalss.indexOf("date") != -1) {
            var ItemValue = TransformADFromStringFunction(item[i]);
        }
        $(this).val(ItemValue);
        i++;
    });
    $("#WorkDataDiv1").children('tbody').empty();
    $("#WorkDataDiv2").children('tbody').empty();
    $("#WorkDataDiv3").children('tbody').empty();
    for (var i = 0; i < result.WorkData.length; i++) {
        var WorkData = result.WorkData[i];
        var itemValue = '<tr id="HS_' + WorkData.ID + '" class="sStaffData">' +
	                    '<td align="center" height="30"><span class="RecordType hideClassSpan">' + WorkData.Type + '</span><input class="date RecordDate" type="text" value="' + TransformADFromStringFunction(WorkData.RecordDate) + '" size="10" /></td>' +
	                    '<td align="center"><textarea class="Record" rows="1" cols="35">' + WorkData.Record + '</textarea></td>' +
	                    '<td align="center"><textarea class="RecordRemark" rows="1" cols="35">' + WorkData.RecordRemark + '</textarea></td>' +
	                    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(\'' + WorkData.ID + '\')">更 新</button> <button class="btnView" type="button" onclick="DelData(\'' + WorkData.ID + '\')">刪 除</button></div>' +
			                    '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(\'' + WorkData.ID + '\')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(\'' + WorkData.ID + '\',\'' + i + '\')">取 消</button></div>' +
			            '</td>' +
			           '</tr>';
        $("#WorkDataDiv" + WorkData.Type).children('tbody').append(itemValue);
    }
    _ReturnValue = result.WorkData;
    $("#WorkDataDiv1 input").add("#WorkDataDiv1 textarea").add("#WorkDataDiv2 input").add("#WorkDataDiv2 textarea").add("#WorkDataDiv3 input").add("#WorkDataDiv3 textarea").attr("disabled", true);
    AspAjax.GetYearVacation(_viewstaffID); 
}

function insertDataDiv(Type) {
    $("#insertDataDiv" + Type + " input").add("#insertDataDiv" + Type + " textarea").val("");
    $("#insertDataDiv" + Type).show();
    $("#insertDataDiv" + Type + " input").add("#insertDataDiv" + Type + " textarea").attr("disabled", false);
    $("#insertDataDiv" + Type + " .canceInsert").unbind().click(function() {
        $("#insertDataDiv" + Type).fadeOut(function() {
            $("#insertDataDiv" + Type + " input").add("#insertDataDiv" + Type + " textarea").val("");
        });
    });

    $("#insertDataDiv" + Type + " .insertStaffData").unbind().click(function() {
        var RecordDate = TransformRepublicReturnValue($("#insertDataDiv" + Type + " .RecordDate").val());
        var Record = $("#insertDataDiv" + Type + " .Record").val();
        if (RecordDate.length > 0 && Record.length > 0) {
            var obj = new Object;
            obj.staffID = _viewstaffID;
            obj.Type = Type;
            obj.RecordDate = RecordDate;
            obj.Record = Record;
            obj.RecordRemark = $("#insertDataDiv" + Type + " .RecordRemark").val();
            AspAjax.createStaffWorkData(obj);
        } else {
            alert("新增失敗，資料填寫不完整");
        }
    });

}

function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function SaveData(TrID) {

    var RecordDate = TransformRepublicReturnValue($("#HS_" + TrID + " .RecordDate").val());
    var Record = $("#HS_" + TrID + " .Record").val();
    if (RecordDate.length > 0 && Record.length > 0) {
        $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        $(".btnSaveUdapteData").add(".btnCancel").click(function() {
            $(".btnSaveUdapteData").add(".btnCancel").hide();
            $(".btnUpdate").fadeIn();
            $("input").add("select").add("textarea").attr("disabled", true);
        });
        var obj = new Object;
        obj.ID = TrID;
        obj.staffID = _viewstaffID;
        obj.Type = $("#HS_" + TrID + " .RecordType").html();
        obj.RecordDate = RecordDate;
        obj.Record = Record;
        obj.RecordRemark = $("#HS_" + TrID + " .RecordRemark").val();
        AspAjax.setStaffWorkData(obj);
    } else {
        alert("更新失敗，資料填寫不完整");
    }
}
function cancelUpData(TrID, vIndex) {
    vIndex = parseInt(vIndex);
    var RecordDate = TransformADFromStringFunction(_ReturnValue[vIndex].RecordDate);
    $("#HS_" + TrID + " .RecordDate").val(RecordDate);
    $("#HS_" + TrID + " .Record").val(_ReturnValue[vIndex].Record);
    $("#HS_" + TrID + " .RecordRemark").val(_ReturnValue[vIndex].RecordRemark);
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " textarea").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();

}
function DelData(TrID) {
    fancyConfirm("確定刪除此筆資料?", function() {
    AspAjax.delStaffWorkData(parseInt(TrID), _viewstaffID);
    })
}

function AddYearVacation() {
    var obj = new Object;
    obj.StaffID = _viewstaffID;
    obj.Year = $("#VaYear").val();
    obj.YearVacation = $("#VaYearVa").val();
    obj.WorkAdd = $("#VaWorkAdd").val();
    obj.WorkMinus = $("#VaWorkMinus").val();
    AspAjax.AddYearVacation(obj); //會在修正寫法
}