
$(document).ready(function() {
AspAjax.set_defaultSucceededCallback(SucceededCallback);
AspAjax.set_defaultFailedCallback(FailedCallback);
initPage();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

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
            window.location.href = './isp_meet_list.aspx';
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
    window.open("./isp_meet_list.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainClass").html("教學管理&gt; 個別化服務計畫(ISP)會議記錄");
    document.title = "教學管理 - 個別化服務計畫(ISP)會議記錄";
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        $("#studentName").val("王小明");
        $("#teacherName").val("連淑貞");
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
    }
}
