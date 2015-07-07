
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

    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './single_teach_case.aspx';
        }
    }

    $(".way").dropdownchecklist({ width: 60 });
    $(".ui-dropdownchecklist").css({ "margin": "4px 0", "height": "auto" });
    $(".ui-dropdownchecklist-dropcontainer").css({ "height": "auto" });
    $(".ui-dropdownchecklist-text").css({"font-size": "13px", "line-height": "20px"});
});

function getView(id) {
    window.open("./single_teach_case.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
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

function getAdd(tid) {
    $("#table" + tid + ">tbody>tr:last-child").after($("#dataTR" + tid).clone().attr("id", "dataTR" + tid + ($("#table" + tid + ">tbody>tr").length+1)));
    var inner = '';
    for (var i = 1; i < 6; i++) {
        inner += '<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + i + '" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>';
        if (i < 5) {
            inner += '<br />';
        }
    }
    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length).find(".wayTD").html(inner);
    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length).find(".way").dropdownchecklist({ width: 60 });
    $(".ui-dropdownchecklist").css({ "margin": "4px 0", "height": "auto" });
    $(".ui-dropdownchecklist-dropcontainer").css({ "height": "auto" });
    $(".ui-dropdownchecklist-text").css({ "font-size": "13px", "line-height": "20px" });
}

function getSubtract(tid) {
    if ($("#table" + tid + ">tbody>tr").length > 1) {
        $("#table" + tid + ">tbody>tr:last-child").detach();
    }
}

