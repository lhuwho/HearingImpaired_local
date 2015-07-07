var MyBase = new Base();
$(document).ready(function() {
    $("#item1").add("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    $("#item1Content").fadeIn();
    initPage();
    

    $(".showUploadImg").fancybox();
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

    $("#btnIndex").click(function() {
        $("#btnSearch").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").hide();
        $("#mainContentIndex").show();
    });

    $("#btnSearch").click(function() {
        $("#btnIndex").css("background-image", "url(./images/bg_menutab1.jpg)");
        $(this).css("background-image", "url(./images/bg_menutab2.jpg)");
        $("#mainContentSearch").show();
        $("#mainContentIndex").hide();
    });


});



function SucceededCallback(result, userContext, methodName) {

    switch (methodName) {
        case "getHearingLossPreschool":
//            editTeacherName = result[0].TeacherName;
//            if (editTeacherName.length > 0) {
//                calendar($('#yearDate').val(), $('#monthDate').val());
//                // $("#SName").html(result[0].StudentName);
//                for (var i = 0; i < result.length; i++) {
//                    editStudentID = result[i].txtpeopleID;

//                    editYear = result[i].Year;
//                    editMonth = result[i].Month;
//                    var day = result[i].Day;
//                    $("#a_" + day).val(result[i].TeacherTemp);
//                    $("#b_" + day).val(result[i].CheckContent);
//                }
//            }
//            else {
//                alert("無此ID");
//            }
            break;
        case "updateStudentTemperatureDataBase":
            break;
    }
}



function getView(id) {
    window.open("./hearing_loss_preschool.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
        //$("#studentName").val("王小明");
        //$("#checkdate1").val("101.07.01");
       //$("#checkdate2").val("101.12.31");
       // $("#tool1").add("#tool2").add("#q111").add("#q112").add("#q121").add("#q122").val(1);
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", false);
    }
}
function Search() {
    $("#mainSearchList .tableList").children("tbody").empty();
    var obj = MyBase.getTextValueBase("searchTable");
    
    AspAjax.SearchHearingLossPreschoolCount(obj);
    
}