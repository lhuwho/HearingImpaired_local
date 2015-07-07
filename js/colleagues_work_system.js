var MyBase = new Base();
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);

    $("#staffID").bind('keydown', function(e) {
        var key = e.which;
        if (key == 13) {
            e.preventDefault();
            if ((this.value).length > 0) {
                var obj = new Object;
                obj.txtpeopleID = this.value;
                AspAjax.searchUserData(obj);
            } else {
                $("#msgDiv").html("請輸入員工ID").fadeIn(function() {
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
                    $("#staffName").html(result[1]);
                    AspAjax.createWorkDateDataBase(result[0]);
                } else if (parseInt(result[0], 10) == -1) {
                    $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[1]).fadeIn(function() {
                        $("#staffID").val("").focus();
                        $("#staffName").empty();
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                } else if (parseInt(result[0], 10) == 0 || parseInt(result[2], 10) == 2) {
                    $("#msgDiv").html("查無此人").fadeIn(function() {
                        $("#staffID").val("").focus();
                        $("#staffName").empty();
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                }
            }
            break;
        case "createWorkDateDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0], 10) != -1 && parseInt(result[0], 10) != 0) {
                    $("#msgDiv").html("打卡成功").fadeIn(function() {
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                        $("#staffID").focus().val("");
                        $("#staffName").empty();
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