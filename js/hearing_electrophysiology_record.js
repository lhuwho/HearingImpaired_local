
$(document).ready(function() {
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");

    $(".btnSearch").click(function() {
        $("#mainSearchList input[type=text]").add("#mainSearchList select").attr("disabled", true);
    });

    $(".btnAdd").click(function() {
        $(this).fadeOut();
        $("#insertDataDiv input[type=text]").add("#insertDataDiv select").attr("disabled", false);
        $("#insertDataDiv").fadeIn();
    });
});

function UpData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", false);
    $("#HS_" + TrID + " .UD").hide();
    $("#HS_" + TrID + " .SC").show();
}
function SaveData(TrID) {
    $("#HS_" + TrID + " input[type=text]").add("#HS_" + TrID + " select").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
}

function cancelInsert() {
    $("#insertDataDiv").fadeOut();
    $(".btnAdd").fadeIn();
}
