var isLogin = false;

$(document).ready(function() {
    //AspAjax.set_defaultSucceededCallback(SucceededCallback);
    //AspAjax.set_defaultFailedCallback(FailedCallback);
    //AspAjax.IsAuthenticated();
    $('#username').focus();

    $('#cerceve input[type="text"]').add('#cerceve input[type="password"]').bind('keydown', function(e) {
        if (e.which == 13) {
            $('#loginok').click();
        }
    });
    $('#loginok').click(function() {
        var LoginID = $('#cerceve input[type="text"]').val();
        var LoginPassword = $('#cerceve input[type="password"]').val();
        if (LoginID.length > 0 && LoginPassword.length > 0) {
            // 登入
            //window.location.href = './main.aspx';

            Sys.Services.AuthenticationService.login(LoginID, LoginPassword, false, null, null, OnLoginCompleted, OnFailed, LoginID);

        } else {
            $('#validationDiv').html('欄位不可空白').fadeIn();
            setTimeout("$('#validationDiv').fadeOut('1000');", 2000);
            return false;
        }
    });

    $("#forgot").click(function() {
        var inner = '<table border="0" width="500"><caption>忘記密碼</caption>' +
        '<tr><td align="center"><input id="signid" type="text" value="" placeholder="帳號" title="帳號" size="40" /></td></tr>' +
        '<tr><td align="center"><input id="signmail" type="text" value="" placeholder="員工資料 E-mail" title="E-mail" size="40" /></td></tr>' +
        '<tr><td height="20" align="right"><p id="validationDiv2"></p></td></tr>' +
        '<tr><td align="center"><input id="forgotok" type="button" value="確認" title="確認" />　　' +
        '<input type="button" value="取消" title="取消" onclick="$.fancybox.close();" /></td></tr>' +
        '<tr><td height="30" align="center"><font size="2" color="#EFA320"></font></td></tr>' +
        '</table>';
        $.fancybox({
            'content': '<div id="inline"><br />' + inner + '<br /></div>',
            'autoDimensions': true,
            'centerOnScroll': true,
            'showCloseButton': false,
            'hideOnOverlayClick': false,
            'onComplete': function() {
                $('#signid').focus();
                $('#inline input[type="text"]').bind('keydown', function(e) {
                    if (e.which == 13) {
                        $('#forgotok').click();
                    }
                });
                $('#forgotok').click(function() {
                    if (($('#inline input[type="text"]').val()).length > 0) {
                        // 註冊
                    } else {
                        $('#validationDiv2').html('欄位不可空白').fadeIn();
                        setTimeout("$('#validationDiv2').fadeOut('1000');", 2000);
                        return false;
                    }
                });
            }
        });
    });
});

function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    switch (methodName) {
        case "IsAuthenticated":
            SetAuthenticate(result);
            break;
        case "getNoMembershipStaffDataList":
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);

}
function SetAuthenticate(result) {
    if (result.length > 0) {
        loginsuccess(result);
    } else {
        $("#loginTxt").show();
    }
}
function loginsuccess(result) {
    isLogin = true;
    window.location.href = './main.aspx';

}
function OnLoginCompleted(validCredentials, userContext, methodName) {

    if (validCredentials == true) {
        alert("登入成功");
        window.location.href = './main.aspx';
        //登入成功
    }
    else {
        alert("登入失敗");
//登入失敗
    }
}
function LoadFailedCallback(error, userContext, methodName) {

}
function OnFailed(error, userContext, methodName) {
    var msg = "";
    msg += "error message:" + error.get_message() + "\n\n";
    msg += "timeout:" + error.get_timedOut() + "\n\n";
    msg += "status code:" + error.get_statusCode() + "\n\n";
    msg += "stack trace:" + error.get_stackTrace() + "\n\n";
    msg += "exceptions type:" + error.get_exceptionType();
    alert(msg + '<br />Please refresh the page.');
}