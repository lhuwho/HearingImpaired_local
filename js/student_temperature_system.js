var MyBase = new Base();

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    //initPage();
    $("#gosrhpeopleID").add("#peopleTemp").add("#parentsTemp").click(function() {
        $(this).select();
    });

    $("#gosrhpeopleID").focus().bind('keydown', function(e) {
        var key = e.which;
        if (key == 13) {
            e.preventDefault();
            if (($("#gosrhpeopleID").val()).length > 0) {
                var obj = MyBase.getTextValueBase("mainContent");
                AspAjax.searchUserData(obj);
            } else {
                $("#msgDiv").html("請輸入學生ID").fadeIn(function() {
                    setTimeout('$("#msgDiv").fadeOut();', '3000');
                });
            }
        }
    });
});


function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "searchUserData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0], 10) != -1 && parseInt(result[0], 10) != 0 && parseInt(result[2], 10) == 2) {
                    $("#studentName").html(result[1]);
                    $("#gosrhpeopleID").html(result[0]);
                    if (result[3].length > 0) {
                        $("#studentAvatar").attr("src", "./uploads/student/" + result[0] + "/print/" + result[3]);
                    } else {
                        $("#studentAvatar").attr("src", "./images/noAvatar2.jpg");
                    }
                    $("#peopleTemp").focus();
                    $("#peopleTemp").add("#parentsTemp").bind('keydown', function(e) {
                        var key = e.which;
                        if (key == 13) {
                            e.preventDefault();
                            if (this.id == "peopleTemp" && ($("#peopleTemp").val()).length > 0 && ($("#parentsTemp").val()).length == 0) {
                                $("#parentsTemp").focus();
                            } else if (this.id == "parentsTemp" && ($("#peopleTemp").val()).length == 0 && ($("#parentsTemp").val()).length > 0) {
                                $("#peopleTemp").focus();
                            } else if (($("#peopleTemp").val()).length > 0 && ($("#parentsTemp").val()).length > 0) {
                                var obj = MyBase.getTextValueBase("mainContent");
                                AspAjax.createStudentTemperatureDataBase(obj);
                            } else {
                                $("#msgDiv").html("請輸入體溫").fadeIn(function() {
                                    setTimeout('$("#msgDiv").fadeOut();', '3000');
                                });
                            }
                        }
                    });
                } else if (parseInt(result[0], 10) == -1) {
                    $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[1]).fadeIn(function() {
                        $("#gosrhpeopleID").val("").focus();
                        $("#studentName").empty();
                        $("#studentAvatar").attr("src", "./images/noAvatar2.jpg");
                        $("#peopleTemp").add("#parentsTemp").val("");
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                } else if (parseInt(result[0], 10) == 0 || parseInt(result[2], 10) == 1) {
                    $("#msgDiv").html("查無此人").fadeIn(function() {
                        $("#gosrhpeopleID").val("").focus();
                        $("#studentName").empty();
                        $("#studentAvatar").attr("src", "./images/noAvatar2.jpg");
                        $("#peopleTemp").add("#parentsTemp").val("");
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                }
            }
            break;
        case "createStudentTemperatureDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0], 10) != -1 && parseInt(result[0], 10) != 0) {
                    $("#msgDiv").html("儲存成功").fadeIn(function() {
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                        $("#gosrhpeopleID").focus().val("");
                        $("#studentName").empty();
                        $("#studentAvatar").attr("src", "./images/noAvatar2.jpg");
                        $("#peopleTemp").add("#parentsTemp").val("");
                    });
                } else if (parseInt(result[0], 10) != -1) {
                    $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[0].errorMsg).fadeIn(function() {
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                } else {
                    $("#msgDiv").html("儲存失敗").fadeIn(function() {
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                }
            }
            break;
    }
}

function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}
