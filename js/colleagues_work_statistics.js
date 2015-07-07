
$(document).ready(function() {
    setDate(1990);
    initPage();
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#mainContentIndex").hide();

    $(".menuTabs2").click(function() {
        $(".menuTabs2").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").add("#mainContentIndex").add(".mainSearchList").add(".insertDataDiv").hide();
        var DivName = '';
        switch (this.id) {
            case "btnSearch":
                DivName = "#mainContentSearch";
                break;
            case "btnIndex":
                DivName = "#mainContentIndex";
                break;
        }
        $(DivName).add(".btnAdd").fadeIn();
    });
});

function showView(viewID) {
    var DivName = '';
    switch (viewID) {
        case 1:
            DivName = "#mainSearchList";
            break;
        case 2:
            DivName = "#mainIndexList";
            break;
        case 3:
            DivName = "#mainIndexList2";
            break;
        case 4:
            DivName = "#mainIndexList3";
            break;
        case 5:
            DivName = "#mainIndexList4";
            break;
    }
    $(DivName).fadeIn();
    $(DivName + " input[type=text]").add(DivName + " select").attr("disabled", true);
}

function setDate(year_start) {
    var now = new Date();

    //年(year_start~今年)
    for (var i = (now.getFullYear() - 1911); i >= (year_start - 1911); i--) {
        $('#yearDate').add('#yearDate2').add('#yearDate3').append($("<option></option>").attr("value", i).text(i));
    }

    //月
    for (var i = 1; i <= 12; i++) {
        $('#monthDate').add('#monthDate2').append($("<option></option>").attr("value", i).text(i));
    }
}

