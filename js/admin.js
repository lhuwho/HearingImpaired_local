var MyBase = new Base();
var noEmptyItem = ["brandType", "brandName"];
var noEmptyShow = ["項目", "廠牌"];
var _RolesData = new Array();
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    $('body').hide();
    AspAjax.ManagePageRoles(SucceededCallback);
    getMenu();

    $(window).scroll(function() {
        if ($(this).scrollTop() != 0) {
            $('#top').fadeIn();
        } else {
            $('#top').fadeOut();
        }
    });
    $('#top').click(function() {
        $('body,html').animate({ scrollTop: 0 }, 800);
    });
});

function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    switch (methodName) {
        case "ManagePageRoles":
            if (result.length == 0) {
                alert("您無權限做此動作");
                window.location.href = '../main.aspx';
            } else {
            $('body').show();
            }
            break;
        case "getNoMembershipStaffDataList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                var inner = '<table width="780" border="0" class="tableText"><tr><th width="150">員工編號</th><th width="120">員工姓名</th><th>密碼</th><th>確認密碼</th><th width="80">功能</th></tr>';

                for (var i = 0; i < result.length; i++) {
                    inner += '<tr id="staffCreate' + i + '"><td class="sID">' + result[i].sID + '</td><td>' + result[i].sName + '</td><td><span class="sEmail" style="display:none;">' + result[i].sEmail + '</span><input class="staffpw" type="password" value="" maxlength="20" /></td><td><input class="staffpwa" type="password" value="" maxlength="20" /></td><td><button class="btnAdd" type="button" onclick="staffCreate(' + i + ')">創建帳號</button></td></tr>';
                }
                inner += '</table>';
                $("#mainSearchForm").html(inner);
            } else {
                if (result.length > 0 && result[0].sID == -1) {
                    alert(result[0].sName);
                } else {
                $("#mainSearchForm").html("無資料");
                }
            }
            break;
        case "getMembershipStaffDataList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                var inner = '<table width="780" border="0" class="tableText"><tr><th width="150">員工編號</th><th width="120">員工姓名</th><th>舊密碼</th><th>新密碼</th><th width="160">功能</th></tr>';

                for (var i = 0; i < result.length; i++) {
                    inner += '<tr id="staffChange' + i + '"><td class="sID">' + result[i].sID + '</td><td>' + result[i].sName + '</td>' +
                    '<td id="coldstaffpw">' + result[i].pw + '</td><td><input id="cstaffpw" type="text" value="" maxlength="20" /></td>' +
                    '<td><button class="btnAdd" type="button" onclick="changePW(' + i + ')">變更密碼</button> <button class="btnAdd changeRole" type="button" >變更權限</button></td></tr>';
                }
                inner += '</table>';
                $("#mainSearchForm").html(inner);


                $(".changeRole").click(function() {
                    var sIndex = ($(this).parent().parent().attr("id")).replace("staffChange", "");
                    var inner = '<table border="0" width="500"><caption>變更權限</caption>' +
                    '<tr><td align="center">權限一<select id="r01"></select></td></tr>' +
                    '<tr><td align="center">權限二<select id="r02"></select></td></tr>' +
                    '<tr><td align="center">權限三<select id="r03"></select></td></tr>' +
                    '<tr><td align="center">權限四<select id="r04"></select></td></tr>' +
                    '<tr><td align="center">權限五<select id="r05"></select></td></tr>' +
                    '<tr><td align="center"><input id="changeRoleOK" type="button" value="確認" title="確認" />　　' +
                    '<input type="button" value="取消" title="取消" onclick="$.fancybox.close();" /></td></tr>' +
                    '</table>';
                    $.fancybox({
                        'content': '<div id="inline"><br />' + inner + '<br /></div>',
                        'autoDimensions': true,
                        'centerOnScroll': true,
                        'showCloseButton': false,
                        'hideOnOverlayClick': false,
                        'onComplete': function() {
                            $("#r01").add("#r02").add("#r03").add("#r04").add("#r05").append($('<option></option>').attr("value", "0").text("請選擇"));
                            for (var i = 0; i < _RolesData.length; i++) {
                                var Roles = _RolesData[i];
                                $("#r01").add("#r02").add("#r03").add("#r04").add("#r05").append($('<option></option>').attr("value", Roles[0]).text(Roles[1]));
                            }
                            for (var i = 1; i < result[sIndex].Roles.length; i++) {
                                $("#r0" + i).children('option[value="' + result[sIndex].Roles[i] + '"]').attr("selected", true);
                            }


                            $('#inline').bind('keydown', function(e) {
                                if (e.which == 13) {
                                    $('#changeRoleOK').click();
                                }
                            });
                            $('#changeRoleOK').click(function() {
                                var staffRoles = new Array();
                                staffRoles.push(result[sIndex].Roles[0]);
                                $("#inline select").each(function() {
                                    staffRoles.push($(this).find(':selected').val());
                                });
                                AspAjax.setStaffRolesData(staffRoles,result[sIndex].sID);
                                //alert("變更權限成功");
                            });
                        }
                    });
                });
            } else {
                if (result.length > 0 && result[0].sID == -1) {
                    alert(result[0].sName);
                } else {
                    $("#mainSearchForm").html("無資料");
                }
            }
            break;
        case "CreateStaffMember":
            if (result == "Success") {
                alert("創建成功");
                AspAjax.getNoMembershipStaffDataList();
            } else {
                alert(result);
            }
            break;
        case "changePWStaffMember":
            if (result <= 0) {
                if (result == -2) {
                    alert("查無此會員");
                } else {
                    alert("發生錯誤，請重新整理頁面");
                }
            } else {
                alert("更改密碼成功");
                AspAjax.getMembershipStaffDataList();
            }
            break;
        case "Logout":
            $("body").hide();
            alert("登出成功");
            window.location.href = './Default.aspx';
            break;
        case "getSalaryValue":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0]) != -1) {
                    $("#salaryManage").val(result[0]);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                }
            }
            break;
        case "createSalaryValue":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，請重新整理頁面");
            } else {
                alert("儲存成功");
                salaryManage();
            }
            break;
        case "createAidsBrandList":
        case "setAidsBrandList":
        case "delAidsBrandList":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，請重新整理頁面");
            } else {
                var itemValue = "新增";
                if (methodName == "setAidsBrandList") {
                    itemValue = "更新";
                } else if (methodName == "delAidsBrandList") {
                    itemValue = "刪除";
                }
                alert(itemValue + "成功");
                $.fancybox.close();
                HearingManage();
            }
            break;
        case "getAidsBrandList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    HearingManageFunction(result);
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                }
            }
            break;
        case "getRolesDataList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0][0]) != -1) {
                    _RolesData = result;
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0][1]);
                }
            }
            break;
        case "setStaffRolesData":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，請重新整理頁面");
            } else {
                alert("更新成功");
                $.fancybox.close();
                changeAccount()
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function loginsuccess(result) {
    isLogin = true;
    var today = new Date();
    var weekArray = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    $("#loginInfo").html("今天是" + (today.getFullYear() - 1911) + "年" + (today.getMonth() + 1) + "月" + today.getDate() + "日 " + weekArray[today.getDay()] + "　歡迎 " + result + " 使用本系統　　<a href='#' id='logout'>登出</a>");
    $("#logout").click(function() {
        AspAjax.Logout();
    });
}

function getMenu() {
    var inner = '<ul class="menu">' +
					'<li><a href="#" onclick="createAccount()">創建帳號</a></li>' +
					'<li><a href="#" onclick="changeAccount()">變更密碼權限</a></li>' +
					'<li><a href="#" onclick="HearingManage()">聽力管理</a></li>' +
					'<li><a href="#" onclick="salaryManage()">薪資管理</a></li>' +
				'</ul>';
    $("#menu").html(inner);
}

function createAccount() {
    $("#mainClass").html("創建帳號");
    AspAjax.getNoMembershipStaffDataList();

}

function staffCreate(sid) {
    var staffpw = $("#staffCreate" + sid).find(".staffpw").val();
    var staffpwa = $("#staffCreate" + sid).find(".staffpwa").val();
    if (staffpw.length > 0 && staffpwa.length > 0) {
        if (staffpw == staffpwa) {
            AspAjax.CreateStaffMember($("#staffCreate" + sid).find(".sID").html(), staffpw, $("#staffCreate" + sid).find(".sEmail").html());
        } else {
            alert("密碼與確認密碼不相同");
        }
    } else {
        alert("請輸入密碼");
    }
}

function changeAccount() {
    $("#mainClass").html("變更密碼權限");
    AspAjax.getMembershipStaffDataList();
    AspAjax.getRolesDataList();
}

function changePW(sindex) {
    var staffpw = $("#staffChange" + sindex).find("#cstaffpw").val();
    var oldstaffpw = $("#staffChange" + sindex).find("#coldstaffpw").html();
    var sID = $("#staffChange" + sindex).find(".sID").html();
    if (staffpw.length > 0 && staffpw.length >= 6 && staffpw.length <= 10) {
        if (staffpw != oldstaffpw) {
            AspAjax.changePWStaffMember(sID, staffpw);
        } else {
            alert("新密碼與舊密碼相同");
        }
    } else {
        if (staffpw.length == 0) {
            alert("請輸入密碼");
        } else if (staffpw.length < 6) {
            alert("密碼最短長度為六碼");
        } else if (staffpw.length > 10) {
            alert("密碼最長為十碼");
        }
    }
}

function salaryManage() {
    $("#mainSearchForm").empty();
    var inner = '點值 <input id="salaryManage" type="text" value="0" size="10"/> <input type="button" value="儲存" onclick="salaryManageSave()"/>';
    $("#mainSearchForm").html(inner);
    $("#salaryManage").unbind('click').click(function() {
        $(this).select();
    }).unbind('keydown').keydown(function(event) {
        if (!(event.keyCode >= 48 && event.keyCode <= 57) && !(event.keyCode >= 96 && event.keyCode <= 105) && !(event.keyCode == 8) && !(event.keyCode == 110)) {
            event.preventDefault();
        }
    });
    AspAjax.getSalaryValue();
}
function salaryManageSave() {
    var salaryManage = $("#salaryManage").val();
    if (salaryManage.length > 0) {
        AspAjax.createSalaryValue(salaryManage);
    }
}
function HearingManage() {
    $("#mainSearchForm").empty();
    var inner = '<table id="HearingTable" border="0" class="tableList" widtd="780">' +
                    '<thead>' +
                    '<tr>' +
                        '<th width="100">序號</th>' +
                        '<th width="560">助聽器廠牌</th>' +
                        '<th width="120">功能</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody>' +
                    '</tbody>' +
                '</table><br />' +
                '<table id="eEarTable" border="0" class="tableList" widtd="780">' +
                    '<thead>' +
                    '<tr>' +
                        '<th width="100">序號</th>' +
                        '<th width="560">電子耳廠牌</th>' +
                        '<th width="120">功能</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody>' +
                    '</tbody>' +
                '</table><br />' +
                '<table id="FMTable" border="0" class="tableList" widtd="780">' +
                    '<thead>' +
                    '<tr>' +
                        '<th width="100">序號</th>' +
                        '<th width="560">調頻系統廠牌</th>' +
                        '<th width="120">功能</th>' +
                    '</tr>' +
                    '</thead>' +
                    '<tbody>' +
                    '</tbody>' +
                '</table><br />' +
                '<p align="center"><br /><button class="btnAdd" type="button" onclick="InsertData()">新 增</button></p>' +
		        '<div id="insertDataDiv" style="display:none">' +
		            '<table class="tableList" width="780" border="0">' +
		                '<thead>' +
		                    '<tr>' +
		                        '<th width="100">項目</th>' +
		                        '<th width="560">廠牌</th>' +
		                        '<th width="120">功能</th>' +
		                    '</tr>' +
		                '</thead>' +
		                '<tbody>' +
		                    '<tr >' +
		                        '<td><select id="brandType"><option vlaue="0">請選擇</option><option value="1">助聽器</option><option value="2">電子耳</option><option value="3">調頻系統</option></select></td>' +
		                        '<td><input id="brandName" type="text" value="" size="80"></td>' +
		                        '<td><button class="btnView" type="button" onclick="HearingManageSave();">儲 存</button> <button class="btnView" type="button" onclick="cancelInsert()">取 消</button></td>' +
		                    '</tr>' +
		                 '</tbody>' +
		            '</table>' +
		        '</div>';
    $("#mainSearchForm").html(inner);
    $("#mainSearchForm .btnSave").show();
    AspAjax.getAidsBrandList();
}
function HearingManageFunction(result) {
    for (var i = 0; i < result.HearingList.length; i++) {
        $("#HearingTable").append(HearingManageDataFunction(i, result.HearingList[i]));
    }
    for (var i = 0; i < result.eEarList.length; i++) {
        $("#eEarTable").append(HearingManageDataFunction(i, result.eEarList[i]));
    }
    for (var i = 0; i < result.FMList.length; i++) {
        $("#FMTable").append(HearingManageDataFunction(i, result.FMList[i]));
    }

}
function HearingManageDataFunction(dNumber,resultData) {
    var inner = '<tr id="HS_' + resultData.ID + '">' +
                '<td>' + (parseInt(dNumber,10)+1) + '</td>' +
                '<td><input class="hbrandName" type="text" value="' + resultData.brandName + '" size="80" disabled="disabled"></td>' +
			    '<td><div class="UD"><button class="btnView" type="button" onclick="UpData(' + resultData.ID + ')">更 新</button> <button class="btnView" type="button" onclick="DelData(' + resultData.ID + ')">刪 除</button></div>' +
			         '<div class="SC" style="display:none"><button class="btnView" type="button" onclick="SaveData(' + resultData.ID + ')">儲 存</button> <button class="btnView" type="button" onclick="cancelUpData(' + resultData.ID + ',' + dNumber + ')">取 消</button></div>' +
                '</tr>';
    return inner;
}
function InsertData() {
    $("#insertDataDiv").show();
}
function cancelInsert() {
    $("#insertDataDiv").hide();
}

function UpData(TrID) {
    $("#HS_" + TrID + " .hbrandName").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function cancelUpData(TrID, vIndex) {
    $("#HS_" + TrID + " .hbrandName").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}

function DelData(TrID) {
    var inner = '<br /><p>確定刪除此筆資料?</p><br /></td></tr><tr><td height="40" align="center"><button id="fancyOK" class="button">確定</button>　<button class="button" onclick="$.fancybox.close();">取消</button>';
    $.fancybox({
        'content': '<div id="inline"><br /><table border="0" width="350"><tr><td align="center">' + inner + '</td></tr></table><br /></div>',
        'modal': true,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $(".button").show();
            $("#fancyOK").bind('click', function() {
                AspAjax.delAidsBrandList(TrID);
            });
        }
    });
}
function SaveData(TrID) {
    var obj = new Object();
    obj.ID = parseInt(TrID);
    var Content = $("#HS_" + TrID + " .hbrandName").val();
    if (Content.length > 0) {
        obj.brandName = Content;
    }
    if (obj.brandName != null) {
        $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
        $("#HS_" + TrID + " .UD").show();
        $("#HS_" + TrID + " .SC").hide();
        AspAjax.setAidsBrandList(obj);
    } else {
        alert("請填寫完整");
    }

}
function HearingManageSave() {
    var obj = MyBase.getTextValueBase("insertDataDiv");
    var checkString = MyBase.noEmptyCheck(noEmptyItem, obj, null, noEmptyShow);
    if (checkString.length > 0) {
        alert(checkString);
    } else {
        AspAjax.createAidsBrandList(obj);
    }
}