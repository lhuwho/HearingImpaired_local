
$(document).ready(function() {
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

    $("select[name='class']").change(function() {
        if ($(this).find(":selected").val() == 0) {
            $("#page1").fadeIn();
            $("#page2").fadeOut();
        } else {
            $("#page1").fadeOut();
            $("#page2").fadeIn();
        }
    });

    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './teach_service_inspect.aspx';
        }
    }
});

function getView(id) {
    window.open("./teach_service_inspect.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        $("select[name='class']").val("1");
        $("#page1").fadeOut();
        $("#page2").fadeIn();
        $("#className").val("西瓜班");
        $("#teacherName").val("王貞貞");
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
    }
}

function calInspect(page) {
    /*$("select").each(function() {
        alert($(this).val())
    });*/
}

