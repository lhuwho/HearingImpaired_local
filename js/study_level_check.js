var MyBase = new Base();
var noEmptyItem = ["studentID", "studentName"];
var noEmptyShow = ["服務使用者編號抓取錯誤，請重新選擇學生", "學生姓名"];
var _ColumnID = 0;
$(document).ready(function() {
AspAjax.set_defaultSucceededCallback(SucceededCallback);
AspAjax.set_defaultFailedCallback(FailedCallback);
initPage();
    $("#item1").add("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#item1Content").fadeIn();

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

    $("#appurtenance img").fancybox();

    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './study_level_check.aspx';
        }
    }
});
function SucceededCallback(result, userContext, methodName) {
    SucceededCallbackAll(result, userContext, methodName);
}
function FailedCallback(error, userContext, methodName) {
    FailedCallbackAll(error, userContext, methodName);
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function getView(id) {
    window.open("./study_level_check.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        $("#studentName").val("王小明");
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
    }
}
