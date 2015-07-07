
$(document).ready(function() {
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

    set_ddl_date2(1912);

    $(".btnUpdate").click(function() {
        $(".btnUpdate").hide();
        $(".btnSaveUdapteData").add(".btnCancel").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
        $("#studentName").add("#studentCity").attr("disabled", true);
    });

    $(".btnSaveUdapteData").add(".btnCancel").click(function() {
        $(".btnSaveUdapteData").add(".btnCancel").hide();
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
    });

    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './supplement_inventory.aspx';
        }
    }
});

function getView(id) {
    window.open("./supplement_inventory.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        $("#studentName").val("王小明");
        $("#studentCity").val("台北市");
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
    }
}

function set_ddl_date2(year_start) {
    var now = new Date();

    //年(year_start~今年)
    for (var i = (now.getFullYear() - 1911); i >= (year_start - 1911); i--) {
        $('.year_ddl').
				append($("<option></option>").
				attr("value", i).
				text(i));
    }
}

