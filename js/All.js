var _uId = 0;
var _MainID = 0;
var _uName = '';
var _uUnit = 0;
var _uRoles = new Array("0000","0","0");
var _UnitList = new Array("", "基金會", "台北至德", "台中至德", "", "高雄至德");
var _CaseStatu = new Array("","未入會", "已入會", "短結", "結案");
var _CategoryList = new Array("", "個別課", "團體課");
var _LimitPage = 10;
//縣市
var _zoneCity = ['請選擇縣市', '台北市', '基隆市', '新北市', '宜蘭縣', '新竹市', '新竹縣', '桃園縣', '苗栗縣', '台中市', '彰化縣', '南投縣', '嘉義市', '嘉義縣', '雲林縣', '台南市', '高雄市', '屏東縣', '台東縣', '花蓮縣', '澎湖縣', '金門縣', '連江縣'];
var _SexList = new Array("", "男", "女");
var _JobItemList = new Array("", "總幹事", "督導", "行政組長", "總會行政", "總會社工組長", "總會教研組長", "會計組長", "會計", "資訊組長", "出納", "主任", "副主任", "教學管理長", "教師", "聽力管理長", "聽力師", "社工管理長", "社工", "中心行政", "主任/教師", "主任/聽力師", "主任/社工");
var _resourceType = new Array("", "復健與醫療", "經濟", "福利服務", "教育", "其他");
var _visitType = new Array("", "學校醫院機構", "家庭暨教學");
var _Aidstype = new Array("", "助聽器", "人工電子耳", "其他");
var _transStage = new Array("", "銜接幼兒園", "銜接國小", "銜接特教學校");
var _ApplyJob = new Array("請選擇", "總幹事", "督導", "行政組長", "總會行政", "總會社工組長", "總會教研組長", "會計組長", "會計", "資訊組長", "出納", "主任", "副主任", "教學管理長", "教師", "聽力管理長", "聽力師", "社工管理長", "社工", "中心行政", "主任/教師", "主任/聽力師", "主任/社工");

var _HearingList = new Array();
var _eEarList = new Array();
var _FMList = new Array();


function initPage() {
    $("body").hide();
    AspAjax.IsAuthenticated(SucceededCallbackAll);
    
    AspAjax.getAidsBrandList(SucceededCallbackAll);
    
    //getMenuDEl();
    getFooter();
    setInitData();
}
//設定初始頁籤
function initDefaultPage(url) {
    _MainID = GetQueryString("id");
    var act = GetQueryString("act");
    if (act != null) {
        getViewData(_MainID, act);
    }
    else if (act == null && _MainID != null) {
        window.location.href = './main.aspx';
    }
}

function setInitData() {
    //1911為開始年份
    set_ddl_date(1912);

    $('.date').datepick({
        yearRange: new Date().getFullYear() - 54 + ":" + (new Date().getFullYear() + 5)
    });
    /*
    $('.date2').datepick({
    yearRange: new Date().getFullYear() - 101 + ":" + (new Date().getFullYear() + 5)
    });
    */
    $(".btnSearch").click(function() {
        $("#mainSearchList").fadeIn();
    });

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
}
function zoneCityFunction() {
    for (var i in _zoneCity) {
        $(".zoneCity").
            append($("<option></option>").
            attr("value", parseInt(i)).
            text(_zoneCity[i]));
    }
}
function getCity(Idx) {
    return _zoneCity[Idx];
}
function assistmanage(Idx) {
    var name = "";
    if (Idx == 1) {
        name = "助聽器";
    }
    else if (Idx == 2) {
        name = "電子耳";
    }
    return name;
}
function SucceededCallbackAll(result, userContext, methodName) {
    // alert(methodName);
    switch (methodName) {
        case "IsAuthenticated":
            SetAuthenticate(result);
            break;
        case "Logout":
			resultObj = new Object();
            $("body").hide();
            alert("登出成功");
            window.location.href = './Default.aspx';
            break;
        case "getAidsBrandList":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    _HearingList = result.HearingList;
                    _eEarList = result.eEarList;
                    _FMList = result.FMList;
                    assistmanagefmBrandFunction();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                }
            }
            break;
        case "getStaffRoles":
        case "getStaffRolesnoVar":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    getMenu(result);
                    haveRoles(result);
                    initDefaultPage();
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[1]);
                }
            }
            break;
        case "ManagePageRoles":
            $("#mainContent .tableText").before(result);
            break;
    }
}
function FailedCallbackAll(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
function SetAuthenticate(result) {
    if (result != "-1") {
        if (result.length > 0) {
            $("body").show();
            loginsuccess(result);
        } else {
            alert("請登入");
            window.location.href = './Default.aspx';
        }
    } else {
        alert("發生錯誤，請聯繫程式管理人員");
    }
}
function loginsuccess(result) {
    _uId = result[0];
    _uName = result[1];
    _uUnit = parseInt(result[2], 10);
    $("#Unit").html(_UnitList[_uUnit]);
    isLogin = true;
    var today = new Date();
    var weekArray = new Array("星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六");
    $("#loginInfo").html("今天是" + (today.getFullYear() - 1911) + "年" + (today.getMonth() + 1) + "月" + today.getDate() + "日 " + 
    weekArray[today.getDay()] + "　歡迎 " + _uName + " 使用本系統　　<a href='#' id='logout'>登出</a>");
    $("#logout").click(function() {
        AspAjax.Logout(SucceededCallbackAll);
    });
    
    AspAjax.getStaffRoles(result[0],SucceededCallbackAll);
}
function logout() {
    AspAjax.Logout(SucceededCallbackAll);
}
function getMenu(resultRoles) {
    var inner = '<ul class="menu">';
    if (parseInt(resultRoles.caseStu[0],2)>0){
        inner += '<li><a href="#">個案管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./student_database.aspx">學生基本資料</a></li>' +
					        //'<li><a href="#"><span class="fontR">個案轉介回覆</span></a></li>' +
					        //'<li><a href="./case_isp.aspx"><span class="fontR">X</span>個案家庭服務計畫(ISP)</a></li>' +
					        '<li><a href="./case_service_record.aspx">個案服務紀錄</a></li>' +
					        //'<li><a href="#"><span class="fontR">個案處遇紀錄</span></a></li>' +
					        '<li><a href="./case_visit_record.aspx">訪視記錄</a></li>' +
					        '<li><a href="./financial_aid.aspx">財務補助申請</a></li>' +
					        //'<li><a href="./supplement_inventory.aspx"><span class="fontR">X</span>個案補助清冊</a></li>' +
					        '<li><a href="./student_tracked.aspx">離會學生追蹤</a></li>' +
					        '<li><a href="./activity_statistics.aspx">活動統計</a></li>' +
					        '<li><a href="./volunteer_data.aspx">志工資料</a></li>' +
					        '<li><a href="./volunteer_sign.aspx">志工服務時數</a></li>' +
					        '<li><a href="./resource_card.aspx">資源卡</a></li>' +
                            '<li><a href="./case_transition.aspx">轉銜服務</a></li>' +
					    '</ul>' +
					'</li>';
    }
	if (parseInt(resultRoles.hearing[0],2)>0){
        inner += '<li><a href="#">聽力管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./audiometry_appointment.aspx">聽檢預約</a></li>' +
                            '<li><a href="./hearing_services.aspx">聽力服務一覽</a></li>' +
                            '<li><a href="./hearing_assessment.aspx">聽力評估報告</a></li>' +
                            '<li><a href="./hearing_inspection.aspx">聽力檢查紀錄</a></li>' +
                            '<li><a href="./hearing_tests.aspx">聽知覺測驗結果記錄</a></li>' +
                            //'<li><a href="./hearing_electrophysiology_record.aspx"><span class="fontR">X</span>聽覺電生理檢查記錄</a></li>' +
                            '<li><a href="./hearing_aidsuse_record.aspx">學生輔具使用記錄</a></li>' +
                             '<li><a href="./aids_manage.aspx">輔具管理</a></li>' +
                            '<li><a href="./fm_assistive_assessment.aspx">調頻輔具評估記錄</a></li>' +
                            //'<li><a href="./hearing_isp.aspx"><span class="fontR">X</span>個案聽覺管理服務計畫(ISP)</a></li>' +
					    '</ul>' +
					'</li>';
    }
	if (parseInt(resultRoles.teach[0],2)>0){
        inner += '<li><a href="#">教學管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./hearing_loss_preschool.aspx">學前聽損幼兒教育課程檢核</a></li>' +
					        '<li><a href="./achievement_assessment.aspx">成就評估</a></li>' +
					        '<li><a href="./study_level_check.aspx">個案學習需求等級檢核</a></li>' +
                            '<li><a href="./teach_isp.aspx">個別化服務計畫書(ISP)</a></li>' +
                            '<li><a href="./isp_meet_list.aspx">個別化服務計畫(ISP)會議一覽</a></li>' +
                            '<li><a href="./single_teach_case.aspx">短期目標課程計畫(教案)</a></li>' +
                            '<li><a href="./group_teach_case_item.aspx">團體班目標課程計畫(模板)</a></li>' +
                            '<li><a href="./voice_distance.aspx">語音距離察覺圖</a></li>' +
                            '<li><a href="./teach_service_check.aspx">教學服務督導</a></li>' +
                            '<li><a href="./teach_service_inspect.aspx">教學服務檢核</a></li>' +
                            '<li><a href="./teacher_schedule.aspx">教師服務時間表</a></li>' +
                            '<li><a href="./classrooms_schedule.aspx">教室使用時間表</a></li>' +
//                            '<li><a href="./foundation_schedule.aspx">全會課表</a></li>' +
					    '</ul>';
    }
    if (parseInt(resultRoles.personnel[0], 2) > 0) {
        inner += '</li>' +
					'<li><a href="#">人事管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./staff_database.aspx">員工資料維護</a></li>' +
                            //'<li><a href="./staff_merit_data.aspx">員工考績資料維護</a></li>' +
                            '<li><a href="./staff_upgrade_data.aspx">員工升等資料查詢</a></li>' +
                            //'<li><a href="./newteacher_estimate_report.aspx">新進教師考評成績</a></li>'+
                            '';

        if (parseInt(resultRoles.attendance[0], 2) > 0) {
            inner += '<li><a href="./colleagues_work_manage.aspx">出勤紀錄管理</a></li>' +
                     '<li><a href="./colleagues_work_statistics.aspx">出勤統計</a></li>' +
                     '<li><a href="./colleagues_work_system.aspx">出勤系統</a></li>';
        }
        inner += '</ul>' +
				'</li>';
    }
	if (parseInt(resultRoles.property[0],2)>0){
        inner += '<li><a href="#">財產管理</a>' +
					    '<ul class="sub">';
        if (parseInt(resultRoles.apply[0], 2) > 0) {
            inner += '<li><a href="./apply_property.aspx">請購單</a></li>';
        }
		inner +='<li><a href="./property_record.aspx">財產記錄單</a></li>' +
					    '</ul>' +
					'</li>';
    }
    if (parseInt(resultRoles.library[0], 2) > 0 || parseInt(resultRoles.serviceFees[0], 2) > 0 || parseInt(resultRoles.caseBT[0], 2) > 0 || parseInt(resultRoles.teachBT[0], 2) > 0) {
        inner += '<li><a href="#">行政管理</a>' +
					    '<ul class="sub">';
        if (parseInt(resultRoles.serviceFees[0], 2) > 0) {
            //inner += '<li><a href="#"><span class="fontR">服務費管理</span></a></li>';
        }
        if (parseInt(resultRoles.caseBT[0], 2) > 0) {
            inner += '<li><a href="./student_temperature_system.aspx">個案體溫系統</a></li>' +
                     '<li><a href="./student_temperature_statistics.aspx">個案出勤統計</a></li>';
        }
        if (parseInt(resultRoles.teachBT[0], 2) > 0) {
            inner += '<li><a href="./teacher_temperature_system.aspx">教師體溫系統</a></li>' +
					 '<li><a href="./teacher_temperature_statistics.aspx">教師體溫統計</a></li>';
        }
        if (parseInt(resultRoles.library[0], 2) > 0) {

            inner += '<li><a href="./library_manage.aspx">圖書管理</a></li>';
        }
        inner += '</ul>' +
		        '</li>';
    }
    if (parseInt(resultRoles.stationery[0], 2) > 0 || parseInt(resultRoles.remind[0], 2) > 0) {
        inner += '<li><a href="#">其他管理</a>' +
					    '<ul class="sub">';
        if (parseInt(resultRoles.stationery[0], 2) > 0) {
            inner += '<li><a href="./stationery_manage.aspx">文具管理</a></li>';
        }
        if (parseInt(resultRoles.remind[0], 2) > 0) {
            inner += '<li><a href="./remind_system.aspx">提醒系統</a></li>';
        }
        inner += '<li><a href="./colleagues_work_statistics.aspx">出勤統計</a></li>';
        inner += '<li><a href="./staff_upgrade_data.aspx">簽到表</a></li>';
        inner += '</ul>' +
				'</li>';

        if (parseInt(resultRoles.salary[0], 2) > 0 ) {
            inner += '<li><a href="#">薪資管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./staff_contracted_salary.aspx">敘薪表</a></li>' +
					        '<li><a href="./salarytable.aspx">薪資明細</a></li>' +
					        '<li><a href="./staff_salary.aspx">薪資表</a></li>' +
                            '<li><a href="./staff_external_database.aspx">外聘教師資料維護</a></li>' +
					    '</ul>' +
					'</li>';
        }
        if (resultRoles.HasPeopleRole) {
            inner += '<li><a href="#">出勤管理</a>' +
           '<ul class="sub">' +
           '<li><a href="./colleagues_work_manage.aspx">出勤紀錄管理</a></li>' +
            '<li><a href="./colleagues_work_statistics.aspx">出勤統計</a></li>' +
            '</ul></li>';
        }
        inner += '</ul>';
    }

    $("#menu").html(inner);
}

function getFooter() {
    var inner = '<div>'+
				'財團法人中華民國婦聯聽障文教基金會　版權所有<br />'+
				'電話：02-2820-1825　　傳真：02-2820-1826　　電子信箱：nwlhif@nwlhif.org.tw<br />'+
				'地址：112 台北市北投區振興街45號（振興醫院健康大樓三樓）<br />'+
				'戶名：財團法人中華民國婦聯聽障文教基金會　　劃撥帳號：18883319' +
			    '</div>';
    $("#footer").html("").hide();
}

function getStudentData(page) {
    $("#mainContent>div").hide();
    $("#" + page + "Content").fadeIn();
}

function goNext(page) {
    $(".menuTabs").css("background-image", "url(./images/bg_menutab1.jpg)");
    $("#item" + (page + 1)).css("background-image", "url(./images/bg_menutab2.jpg)");
    getStudentData("item" + (page + 1));
    $('body,html').animate({ scrollTop: 0 }, 800);
}

function set_ddl_date(year_start) {
    var now = new Date();

    //年(year_start~今年)
    for (var i = (now.getFullYear() - 1911); i >= (year_start - 1911); i--) {
        $('#year_ddl1').append($("<option></option>").attr("value", i).text(i));
    }

    //月
    for (var i = 1; i <= 12; i++) {
        $('#month_ddl1').append($("<option></option>").attr("value", i).text(i));
    }

    $('#year_ddl1').change(onChang_date);
    $('#month_ddl1').change(onChang_date);
}

//年、月選單改變時
function onChang_date() {
    if ($('#year_ddl1').val() != -1 && $('#month_ddl1').val() != -1) {

        var date_temp = new Date($('#year_ddl1').val(), $('#month_ddl1').val(), 0);

        //移除超過此月份的天數
        $("#day_ddl1 option").each(function() {
            if ($(this).val() != -1 && $(this).val() > date_temp.getDate()) $(this).remove();
        });

        //加入此月份的天數
        for (var i = 1; i <= date_temp.getDate(); i++) {
            if (!$("#day_ddl1 option[value='" + i + "']").length) {
                $('#day_ddl1').append($("<option></option>").attr("value", i).text(i));
            }
        }
    } else {
        $("#day_ddl1 option:selected").removeAttr("selected");
    }
}

function GetQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    } else {
        return null;
    }
}
function TransformRepublicReturnValue(oldDate) {
    if (oldDate.length > 0) {
        return TransformRepublic(oldDate);
    } else {
    //return "1900-01-01";
    return "";
    }
}
function TransformRepublic(oldDate) {
    var DateSplie = oldDate.split("/");
    var newDateYear = parseInt(DateSplie[0]) + 1911;
    return newDateYear + "-" + DateSplie[1] + "-" + DateSplie[2];
}
/**Transform AD**/
function TransformAD(DateYear, DateMonth, DateDate) {
    var retuenValue = "";
 if (parseInt(DateYear) != 1900 && parseInt(DateYear) > 1900) {
     var newDateYear = parseInt(DateYear) - 1911;
     if (parseInt(DateMonth) < 10 && DateMonth.toString().length ==1) {
         DateMonth = "0" + DateMonth;
     }
     if (parseInt(DateDate) < 10 && DateDate.toString().length == 1) {
         DateDate = "0" + DateDate;
     }
        retuenValue = newDateYear + "/" + DateMonth + "/" + DateDate;
    }
    return retuenValue;
}
function TransformADFromDateFunction(TransDate) {
    return TransformAD(TransDate.getFullYear(), TransDate.getMonth() + 1, TransDate.getDate());
}
function TransformADFromStringFunction(TransDate) {
    var item = TransDate.split("-");
    return TransformAD(item[0], item[1], item[2]);
}
function TransformDBStringFunction(TransDate) {
    return TransDate.getFullYear() + "-" + (TransDate.getMonth() + 1) + "-" + TransDate.getDate();
}

/***/
function MergerObject(newObject,oldObject) {
    for (var item in oldObject) {
        newObject[item] = oldObject[item];
    }
    return newObject;
}

function fancyConfirm(msg, callbackYes) {
    var inner = '<br /><p>' + msg + '</p><br /></td></tr><tr><td height="40" align="center"><button id="fancyOK" class="button">確定</button>　<button class="button" onclick="$.fancybox.close();">取消</button>';
    $.fancybox({
        'content': '<div id="inline"><br /><table border="0" width="350"><tr><td align="center">' + inner + '</td></tr></table><br /></div>',
        'modal': true,
        'autoDimensions': true,
        'centerOnScroll': true,
        'onComplete': function() {
            $(".button").show();
            $("#fancyOK").bind('click', function() {
                callbackYes();
            });
        }
    });
}
function countAgefunction(AgeName, MonthName) {
    var year_ddl1 = parseInt($("#year_ddl1 :selected").val());
    var month_ddl1 = parseInt($("#month_ddl1 :selected").val());
    var day_ddl1 = parseInt($("#day_ddl1 :selected").val());
    if (year_ddl1 != -1 && month_ddl1 != -1 && day_ddl1 != -1) {
        year_ddl1 = year_ddl1 + 1911;
        var myDate = new Date();
        var gYear = myDate.getFullYear();
        var gMonth = (myDate.getMonth()) + 1;
        var gDate = myDate.getDate();
        var Age = parseInt(gYear - year_ddl1);
        var Monthitem = parseInt(gMonth - month_ddl1);
        var staffMonth = 0;

        if (Monthitem < 0 && Age > 0) {
            staffMonth = 12 - month_ddl1 + gMonth;
        } else if (Monthitem > 0) {
            staffMonth = gMonth - month_ddl1;
        } else if (Monthitem == 0) {
            staffMonth = 0;
        }
        if (parseInt(Age) == parseInt(gYear)) {
            Age = 0;
        } else if (Monthitem <= 0) {
            if (gDate - day_ddl1 > 0) {
                Age = Age - 1;
            }
        }
        if (Age > 0) {
            $("#" + AgeName).val(Age);
        } else {
            $("#" + AgeName).val("0");
        }
        if (staffMonth > 0) {
            $("#" + MonthName).val(staffMonth);
        } else {
            $("#" + MonthName).val("0");
        }

    } /*else {
        if (year_ddl1 == -1) {
            alert("請選擇出生日期的出生年份");
        } else if (month_ddl1 == -1) {
            alert("請選擇出生日期的出生月份");
        } else if (day_ddl1h == -1) {
            alert("請選擇出生日期的出生日期");
        }
    }*/

}
function BirthdayFromDateFunction(BirthdayDate) { //Date
    return callculateAge(BirthdayDate.getFullYear(), BirthdayDate.getMonth() + 1, BirthdayDate.getDate());
}
function BirthdayStringDateFunction(BirthdayDate) { //String
    var item = BirthdayDate.split("-");
    return callculateAge(item[0], item[1], item[2]);
}
function callculateAge(year_ddl1, month_ddl1, day_ddl1) {
    var Age = 0;
    var staffMonth = 0;
    if (year_ddl1 != 1900) {
        var myDate = new Date();
        var gYear = myDate.getFullYear();
        var gMonth = (myDate.getMonth()) + 1;
        var gDate = myDate.getDate();
        var totalManth = (parseInt(gYear) * 12 +parseInt(gMonth)) - (parseInt(year_ddl1) * 12 + parseInt(month_ddl1));
        return [parseInt(totalManth / 12), totalManth%12];
//        var myDate = new Date();
//        var gYear = myDate.getFullYear();
//        var gMonth = (myDate.getMonth()) + 1;
//        var gDate = myDate.getDate();
//        Age = parseInt(gYear - year_ddl1);
//        alert(Age);
//        var Monthitem = parseInt(gMonth - month_ddl1);

//        if (Monthitem < 0 && Age > 0) {
//            staffMonth = 12 - month_ddl1 + gMonth;
//        } else if (Monthitem > 0) {
//            staffMonth = gMonth - month_ddl1;
//        } else if (Monthitem == 0) {
//            staffMonth = 0;
//        }
//        if (parseInt(year_ddl1) == parseInt(gYear)) {
//            Age = 0;
//        } else if (Monthitem <= 0) {
//            if (gDate - day_ddl1 > 0) {
//                Age = Age - 1;
//            }
//        }
    }
    return [Age, staffMonth];
}



function countAgefunction2(CName,AgeName, MonthName) {
    var Birthday = $("#" + CName).val
    if (Birthday.length > 0) {
        var BirthdaySplit = Birthday.split("/");
        var year_ddl1 = parseInt(Birthday[0]);
        var month_ddl1 = parseInt(Birthday[1]);
        var day_ddl1 = parseInt(Birthday[2]);
        if (year_ddl1 != -1 && month_ddl1 != -1 && day_ddl1 != -1) {
            year_ddl1 = year_ddl1 + 1911;
            var AgeList = callculateAge(year_ddl1, month_ddl1, day_ddl1);
            Age = parseInt(AgeList[0]);
            staffMonth = parseInt(AgeList[1]);
            if (Age > 0) {
                $("#" + AgeName).val(Age);
            } else {
                $("#" + AgeName).val("0");
            }
            if (staffMonth > 0) {
                $("#" + MonthName).val(staffMonth);
            } else {
                $("#" + MonthName).val("0");
            }

        } /*else {
        if (year_ddl1 == -1) {
            alert("請選擇出生日期的出生年份");
        } else if (month_ddl1 == -1) {
            alert("請選擇出生日期的出生月份");
        } else if (day_ddl1h == -1) {
            alert("請選擇出生日期的出生日期");
        }
    }*/
    }
}
function PushPageValue(DataList) {
    var i = 1;
    for (var item in DataList) {
     //   alert(item + "---------------------" + i);
        i++;
        if ($("#" + item).length > 0 && DataList[item] != null && DataList[item] != "null" && DataList[item].length > 0) {
            var thisType = $("#" + item).get(0).tagName;
            var thisCalss = $("#" + item).attr("class");
            if (thisCalss != undefined && thisCalss.indexOf("date") != -1) {
                var ItemValue = TransformADFromStringFunction(DataList[item]);
                $("#" + item).val(ItemValue);
            } else if (thisType == "SELECT" || thisType == "select") {
                if (thisCalss != "chosen-select") {
                    $("#" + item).children('option[value=' + DataList[item] + ']').attr("selected", true);
                }
            } else if (thisType == "INPUT" || thisType == "TEXTAREA") {
                $("#" + item).val(DataList[item]);
                
            }else if (thisType != "IMG") {
                $("#" + item).html(DataList[item]);
            } 
        } else if ($("input[name=" + item + "]").length > 0) {
        $("input[name=" + item + "]").attr("checked", false);
            var ThisEle = $("input[name=" + item + "]").get(0).attributes;
            var thisType = ThisEle.type.nodeValue;
            if (thisType == "RADIO" || thisType == "radio") {
                $("input[name=" + item + "][value=" + DataList[item] + "]").attr("checked", true);
            } else if (thisType == "checkbox") {
                var itemValueString = DataList[item].split("@@");
                for (var itemValue in itemValueString) {
                    $("input[name=" + item + "][value=" + itemValueString[itemValue] + "]").attr("checked", true);
                }
            }
        }
    }
   // alert(i);
}
function getHideSpanValue(htmltableName, HideTextClassName) {
    var obj = new Object();
    $("#" + htmltableName + " ." + HideTextClassName).each(function() {
        var thisvalue = $(this).html();
        if (thisvalue.length > 0) {
            var NameID = this.id;
            obj[NameID] = thisvalue;
        }
    });
    return obj;
}
function AgencyUnitSelectFunction(htmlTableName, SelectIDName) {
   
    var htmlName = "";
    if (htmlTableName.length > 0) {
        htmlName = "#" + htmlTableName + " ";
    }
    $(htmlName + "#" + SelectIDName).find("option").empty();
    $(htmlName + "#" + SelectIDName).append($("<option></option>").attr("value", 0).text("請選擇"));
    for (var item in _UnitList) {
        if ((_UnitList[item]).length > 0) {
            $(htmlName + "#" + SelectIDName).append($("<option></option>").attr("value", item).text(_UnitList[item]));
        }
    }
}
function AgencyUnitRadioFunction(htmlTableName, RadioIDName, RadioName) {

    var htmlName = "";
    if (htmlTableName.length > 0) {
        htmlName = "#" + htmlTableName + " ";
    }
    $(htmlName + "#" + RadioIDName).find("label").empty();
    for (var item in _UnitList) {
        if ((_UnitList[item]).length > 0) {
            var item = '<input type="radio" name="' + RadioName + '" value="' + item + '" autocomplete="off" /> ' + _UnitList[item];
            $(htmlName + "#" + RadioIDName).append($("<label></label>").html(item)).append("　　");
        }
    }
}
function AgencyStatuSelectFunction(htmlTableName, SelectIDName) {
    var htmlName = "";
    if (htmlTableName.length > 0) {
        htmlName = "#" + htmlTableName + " ";
    }
    $(htmlName + "#" + SelectIDName).append($("<option></option>").attr("value", 0).text("請選擇"));
    for (var item in _CaseStatu) {
        if ((_CaseStatu[item]).length > 0) {
            $(htmlName + "#" + SelectIDName).append($("<option></option>").attr("value", item).text(_CaseStatu[item]));
        }
    }
}
function TodayADDateFunction() {
    var today = new Date();
    return TransformADFromDateFunction(today);
}
//liaosankai.pixnet.net/blog/post/521987-將數字每隔三位加上逗號-number_format
function FormatNumber(str) {
    if (str.length <= 3) {
        return str;
    } else {
        return FormatNumber(str.substr(0, str.length - 3)) + ',' + str.substr(str.length - 3);
    }
}

//http://www.dewen.org/q/294
function array_diff(array1, array2) {
    var o = {}; //轉成hash可以減少運算量，數據量越大，優勢越明顯。
    for (var i = 0, len = array2.length; i < len; i++) {
        o[array2[i]] = true;
    }

    var result = [];
    for (i = 0, len = array1.length; i < len; i++) {
        var v = array1[i];
        if (o[v]) continue;
        result.push(v);
    }
    return result;
}
function ItemisNumber(item) {
    item = parseInt(item, 10);
    if (!isNaN(item)) {
        return item;
    } else {
        return 0;
    }
}
//輔具廠牌
function assistmanagebrandFunction() {
    $("input[name=assistmanageR]").change(function() {
        var assistmanageR = $("input[name=assistmanageR]:checked").val();
        if (assistmanageR != null && assistmanageR != undefined) {
            var brandList = new Object();
            if (assistmanageR == "1") {
                brandList = _HearingList;
            } else if (assistmanageR == "2") {
                brandList = _eEarList;
            } else if (assistmanageR == "3") {
                brandList = _FMList;
            }
            
            $("#brandR").find("option").remove();
            $("#brandR").append($("<option></option>").attr("value", "0").text("請選擇"));
            for (var i = 0; i < brandList.length; i++) {
                
                $("#brandR").append($("<option></option>").attr("value", brandList[i].ID).text(brandList[i].brandName));
            }
        }
    });
    $("input[name=assistmanageL]").change(function() {
        var assistmanageL = $("input[name=assistmanageL]:checked").val();
        if (assistmanageL != null && assistmanageL != undefined) {
            var brandList = new Object();
            if (assistmanageL == "1") {
                brandList = _HearingList;
            } else if (assistmanageL == "2") {
                brandList = _eEarList;
            } else if (assistmanageL == "3") {
                brandList = _FMList;
            }
            $("#brandL").find("option").remove();
            $("#brandL").append($("<option></option>").attr("value", "0").text("請選擇"));
            for (var i = 0; i < brandList.length; i++) {
                $("#brandL").append($("<option></option>").attr("value", brandList[i].ID).text(brandList[i].brandName));
            }
        }
    });
}

//輔具廠牌
function assistmanagebrandFunction2() {
    $("#assistmanage").change(function() {
    var assistmanageR = $("#assistmanage :selected").val();
        if (assistmanageR != null && assistmanageR != undefined) {
            var brandList = new Object();
            if (assistmanageR == "1") {
                brandList = _HearingList;
            } else if (assistmanageR == "2") {
                brandList = _eEarList;
            } else if (assistmanageR == "3") {
                brandList = _FMList;
            }
            $("#brand").find("option").remove();
            $("#brand").append($("<option></option>").attr("value", "0").text("請選擇"));
            for (var i = 0; i < brandList.length; i++) {
                $("#brand").append($("<option></option>").attr("value", brandList[i].ID).text(brandList[i].brandName));
            }
        }
    });
    $("#gosrhassistmanage").change(function() {
        var assistmanageR = $("#gosrhassistmanage :selected").val();
        if (assistmanageR != null && assistmanageR != undefined) {
            var brandList = new Object();
            if (assistmanageR == "1") {
                brandList = _HearingList;
            } else if (assistmanageR == "2") {
                brandList = _eEarList;
            } else if (assistmanageR == "3") {
                brandList = _FMList;
            }
            $("#gosrhbrand").find("option").remove();
            $("#gosrhbrand").append($("<option></option>").attr("value", "0").text("請選擇"));
            for (var i = 0; i < brandList.length; i++) {
                $("#gosrhbrand").append($("<option></option>").attr("value", brandList[i].ID).text(brandList[i].brandName));
            }
        }
    });
}
//FM輔具廠牌
function assistmanagefmBrandFunction() {
    
    $("#fmBrand").find("option").remove();
    $("#fmBrand").append($("<option></option>").attr("value", "0").text("請選擇"));
    $("#assisFMBrand").find("option").remove();
    $("#assisFMBrand").append($("<option></option>").attr("value", "0").text("請選擇"));
    for (var i = 0; i < _FMList.length; i++) {
        $("#fmBrand").append($("<option></option>").attr("value", _FMList[i].ID).text(_FMList[i].brandName));
        $("#assisFMBrand").append($("<option></option>").attr("value", _FMList[i].ID).text(_FMList[i].brandName));
    }
}
function getMenuDEl() {
    var inner = '<ul class="menu">' +
					'<li><a href="#">個案管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./student_database.aspx">學生基本資料</a></li>' +
    //'<li><a href="#"><span class="fontR">個案轉介回覆</span></a></li>' +
    //'<li><a href="./case_isp.aspx"><span class="fontR">X</span>個案家庭服務計畫(ISP)</a></li>' +
					        '<li><a href="./case_service_record.aspx">個案服務紀錄</a></li>' +
    //'<li><a href="#"><span class="fontR">個案處遇紀錄</span></a></li>' +
					        '<li><a href="./case_visit_record.aspx">訪視記錄</a></li>' +
					        '<li><a href="./financial_aid.aspx">財務補助申請</a></li>' +
    //'<li><a href="./supplement_inventory.aspx"><span class="fontR">X</span>個案補助清冊</a></li>' +
					        '<li><a href="./student_tracked.aspx">離會學生追蹤</a></li>' +
					        '<li><a href="./activity_statistics.aspx">活動統計</a></li>' +
					        '<li><a href="./volunteer_data.aspx">志工資料</a></li>' +
					        '<li><a href="./volunteer_sign.aspx">志工服務時數</a></li>' +
					        '<li><a href="./resource_card.aspx">資源卡</a></li>' +
                            '<li><a href="./case_transition.aspx">轉銜服務</a></li>' +
					    '</ul>' +
					'</li>' +
					'<li><a href="#">聽力管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./audiometry_appointment.aspx">聽檢預約</a></li>' +
                            '<li><a href="./hearing_services.aspx">聽力服務一覽</a></li>' +
                            '<li><a href="./hearing_assessment.aspx">聽力評估報告</a></li>' +
                            '<li><a href="./hearing_inspection.aspx">聽力檢查紀錄</a></li>' +
                            '<li><a href="./hearing_tests.aspx">聽知覺測驗結果記錄</a></li>' +
    //'<li><a href="./hearing_electrophysiology_record.aspx"><span class="fontR">X</span>聽覺電生理檢查記錄</a></li>' +
                            '<li><a href="./hearing_aidsuse_record.aspx">學生輔具使用記錄</a></li>' +
                             '<li><a href="./aids_manage.aspx">輔具管理</a></li>' +
                            '<li><a href="./fm_assistive_assessment.aspx">調頻輔具評估記錄</a></li>' +
    //'<li><a href="./hearing_isp.aspx"><span class="fontR">X</span>個案聽覺管理服務計畫(ISP)</a></li>' +
					    '</ul>' +
					'</li>' +
					'<li><a href="#">教學管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./hearing_loss_preschool.aspx">學前聽損幼兒教育課程檢核</a></li>' +
					        '<li><a href="./achievement_assessment.aspx">成就評估</a></li>' +
					        '<li><a href="./study_level_check.aspx">個案學習需求等級檢核</a></li>' +
                            '<li><a href="./teach_isp.aspx">個別化服務計畫書(ISP)</a></li>' +
                            '<li><a href="./isp_meet_list.aspx">個別化服務計畫(ISP)會議一覽</a></li>' +
                            '<li><a href="./single_teach_case.aspx">短期目標課程計畫(教案)</a></li>' +
                            '<li><a href="./group_teach_case_item.aspx">團體班目標課程計畫(模板)</a></li>' +
                            '<li><a href="./voice_distance.aspx">語音距離察覺圖</a></li>' +
                            '<li><a href="./teach_service_check.aspx">教學服務督導</a></li>' +
                            '<li><a href="./teach_service_inspect.aspx">教學服務檢核</a></li>' +
                            '<li><a href="./teacher_schedule.aspx">教師服務時間表</a></li>' +
                            '<li><a href="./classrooms_schedule.aspx">教室使用時間表</a></li>' +
                            '<li><a href="./foundation_schedule.aspx">全會課表</a></li>' +
					    '</ul>' +
					'</li>' +
					'<li><a href="#">人事管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./staff_database.aspx">員工資料維護</a></li>' +
                            '<li><a href="./staff_merit_data.aspx">員工考績資料維護</a></li>' +
                            '<li><a href="./staff_upgrade_data.aspx">員工升等資料查詢</a></li>' +
                            '<li><a href="./newteacher_estimate_report.aspx">新進教師考評成績</a></li>' +
                            '<li><a href="./colleagues_work_manage.aspx">出勤紀錄管理</a></li>' +
                            '<li><a href="./colleagues_work_statistics.aspx">出勤統計</a></li>' +
                            '<li><a href="./colleagues_work_system.aspx">出勤系統</a></li>' +
					    '</ul>' +
					'</li>' +
					'<li><a href="#">財產管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./apply_property.aspx">請購、請修單</a></li>' +
					        '<li><a href="./property_record.aspx">財產記錄單</a></li>' +
					    '</ul>' +
					'</li>' +
					'<li><a href="#">行政管理</a>' +
					    '<ul class="sub">' +
    //'<li><a href="#"><span class="fontR">服務費管理</span></a></li>' +
					        '<li><a href="./student_temperature_system.aspx">個案體溫系統</a></li>' +
					        '<li><a href="./teacher_temperature_system.aspx">教師體溫系統</a></li>' +
					        '<li><a href="./student_temperature_statistics.aspx">個案出勤統計</a></li>' +
					        '<li><a href="./teacher_temperature_statistics.aspx">教師體溫統計</a></li>' +
					        '<li><a href="./library_manage.aspx">圖書管理</a></li>' +
					    '</ul>' +
					'</li>' +
					'<li><a href="#">其他管理</a>' +
					    '<ul class="sub">' +
                            '<li><a href="./stationery_manage.aspx">文具管理</a></li>' +
					        '<li><a href="./remind_system.aspx">提醒系統</a></li>' +
					        
					    '</ul>' +
					'</li>' +
					'<li><a href="#">薪資管理</a>' +
					    '<ul class="sub">' +
					        '<li><a href="./staff_contracted_salary.aspx">敘薪表</a></li>' +
					        '<li><a href="./salarytable.aspx">薪資明細</a></li>' +
					        '<li><a href="./staff_salary.aspx">薪資表</a></li>' +
                            '<li><a href="./staff_external_database.aspx">外聘教師資料維護</a></li>' +
					    '</ul>' +
					'</li>' +
				'</ul>';
    $("#menu").html(inner);
}
function haveRoles(result) {
    var url = (window.location.toString()).split('/');
    var locationPageUrl = (url[url.length - 1]).split('.aspx');
    var locationPage = locationPageUrl[0];
    if (locationPage != "main") {
        $("body").hide();
        var DataList = new Array(["caseStu", "student_database", "case_service_record", "case_visit_record", "financial_aid", "student_tracked", "activity_statistics", "volunteer_data", "volunteer_sign", "resource_card", "case_transition"],
                            ["hearing", "audiometry_appointment", "hearing_services", "hearing_assessment", "hearing_inspection", "hearing_tests", "hearing_aidsuse_record", "aids_manage", "fm_assistive_assessment"],
                            ["teach", "hearing_loss_preschool", "achievement_assessment", "study_level_check", "teach_isp", "isp_meet_list", "single_teach_case", "group_teach_case_item", "voice_distance", "teach_service_check", "teach_service_inspect", "teacher_schedule", "classrooms_schedule", "teach_course", "foundation_schedule"],
                            ["salary", "staff_contracted_salary", "salarytable", "staff_salary", "staff_external_database"],
                            ["personnel", "staff_database", "staff_merit_data", "staff_upgrade_data", "newteacher_estimate_report"],
                            ["attendance", "colleagues_work_manage", "colleagues_work_statistics", "colleagues_work_system"],
                            ["apply", "apply_property"],
                            ["property", "property_record"],
                            ["caseBT", "student_temperature_system", "student_temperature_statistics"],
                            ["teachBT", "teacher_temperature_system", "teacher_temperature_statistics"],
                            ["serviceFees"],
                            ["library", "library_manage", "library_system"],
                            ["stationery", "stationery_manage"],
                            ["remind", "remind_system"]);
        var SearchType = DataList.indexOf(locationPage, 0);
        var reg = new RegExp(locationPage, "g");
        var rindex = "";
        for (var i = 0; i < DataList.length; i++) {
            if (rindex.length > 0) {
                break;
            }
            for (var j = 0; j < DataList[i].length; j++) {
                var itemValue = DataList[i][j];
                var exit = itemValue.match(reg);
                if (exit != null) {
                    rindex = DataList[i][0];
                    break;
                }
            }
        }
        //result[rindex][0]
        _uRoles[0] = result[rindex][0];
        _uRoles[1] = result[rindex][1];
        _uRoles[2] = "1";
        var Roles = parseInt(result[rindex][0], 2);
        if (Roles == 0) {
            alert("您沒有權限查看此頁");
            window.location.href = "./main.aspx";
        } else {
            $("body").show();
            haveRolesAndHide();
            $("body").append('<iframe width="1" height="1" src="AutoRefresh.aspx" frameborder="0"></iframe>');
        }
    } else {
        AspAjax.ManagePageRoles(SucceededCallbackAll);
    }
    
}
function haveRolesWaitFunction() {
    if (parseInt(_uRoles[2],10) !=1) {
        setTimeout("haveRolesWaitFunction()", 1);
    } else {
        haveRolesAndHide();
    }
}
function haveRolesAndHide() {
    var Roles = parseInt(_uRoles[0], 2);
    var RolesValue = (_uRoles[0]).split("");
    /*RolesValue[0] 
    
    */
    $("#btnInsert").show();
    if (parseInt(RolesValue[2]) == 0) {
        //新
        if ($("#btnInsert").html() == "新增") {
            $("#btnInsert").hide();
        }
        $(".btnSave").add(".btnSaveUdapteData").add(".btnAdd").hide();
    }
    if (parseInt(RolesValue[1]) == 0) {
        $(".btnUpdate").add(".btnSave").add(".btnSaveUdapteData").add(".btnAdd").add(".btnUpdateSmall").add(".btnSaveSmall").hide();
        //更
    }
    if (parseInt(RolesValue[0]) == 0) {
        //刪
        $(".btnDelete").hide();
    }

}
//add applyJob
function ApplyJobSelectFunction(htmlTableName, SelectIDName) {
    var htmlName = "";
    if (htmlTableName.length > 0) {
        htmlName = "#" + htmlTableName + " ";
    }
    for (var item in _ApplyJob) {
        if ((_ApplyJob[item]).length > 0) {
            $(htmlName + "#" + SelectIDName).append($("<option></option>").attr("value", item).text(_ApplyJob[item]));
        }
    }
}


function YearChange(olddate) {
    olddate =  olddate.replace("/", "-").replace("/", "-");
    //olddate;
    var item = olddate.split("-");
    if (item[0] != 1900 && item[0] > 1911) {
        item[0] = parseInt(item[0]) - parseInt(1911);
    }
    var newdate = item[0] + "/" + item[1] + "/" + item[2];
    return newdate;
}
