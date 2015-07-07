var MyBase = new Base();

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);

    $("#gosrhpeopleID").add("#peopleTemp").click(function() {
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
                $("#msgDiv").html("請輸入教師ID").fadeIn(function() {
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
                if (parseInt(result[0], 10) != -1 && parseInt(result[0], 10) != 0 && parseInt(result[2], 10) == 1) {
                    $("#teacherName").html(result[1]);
                    $("#gosrhpeopleID").html(result[0]);
                    $("#peopleTemp").focus();
                    $("#peopleTemp").bind('keydown', function(e) {
                        var key = e.which;
                        if (key == 13) {
                            e.preventDefault();
                            if (($("#peopleTemp").val()).length > 0) {
                                var obj = MyBase.getTextValueBase("mainContent");
                                AspAjax.createTeacherTemperatureDataBase(obj);
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
                        $("#teacherName").empty();
                        $("#peopleTemp").val("");
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                } else if (parseInt(result[0], 10) == 0 || parseInt(result[2], 10) == 2) {
                    $("#msgDiv").html("查無此人").fadeIn(function() {
                        $("#gosrhpeopleID").val("").focus();
                        $("#teacherName").empty();
                        $("#peopleTemp").val("");
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                }
            }
            break;
        case "createTeacherTemperatureDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0], 10) != -1 && parseInt(result[0], 10) != 0) {
                    $("#msgDiv").html("儲存成功").fadeIn(function() {
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                        $("#gosrhpeopleID").focus().val("");
                        $("#teacherName").empty();
                        $("#peopleTemp").val("");
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
