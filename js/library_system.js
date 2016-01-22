var MyBase = new Base();
var noEmptyItem = ["gosrhpeopleID", "bookCode"];
var noEmptyShow = ["借閱者ID", "借閱圖書書號"];
var noEmptyItem2 = ["gosrhbookReturnID"];
var noEmptyShow2 = ["借閱圖書書號"];
var _ReturnValue;
var _borrowStatus = new Array("", "借書", "還書");
var _borrowerStatus = new Array("", "員工", "學生");

$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    //initPage();
    $("#gosrhpeopleID").add("#bookCode").click(function() {
        $(this).select();
    });
    
    $("input[name='book']:radio").change(function() {
        var selectedvalue = $(this).val();
        if (selectedvalue == 1) {
            $("#userData").fadeIn();
            $("#bookData").hide();
            $("#gosrhpeopleID").focus().bind('keydown', function(e) {
                var key = e.which;
                if (key == 13 || key == 9) {
                    e.preventDefault();
                    if (($("#gosrhpeopleID").val()).length > 0) {
                        $("#borrowerName").add("#borrowerID").add("#borrowerStatus").add("#borrowerStatusName").empty();
                        var obj = MyBase.getTextValueBase("userData");
                        AspAjax.searchUserData(obj);
                    } else {
                        $("#msgDiv").html("請輸入借閱者ID").fadeIn(function() {
                            setTimeout('$("#msgDiv").fadeOut();', '3000');
                        });
                    }
                }
            });
        } else if (selectedvalue == 2) {
            $("#bookData").fadeIn();
            $("#userData").hide();
            $("#bookReturnCode").focus().bind('keydown', function(e) {
                var key = e.which;
                if (key == 13) {
                    e.preventDefault();
                    if (($("#bookReturnCode").val()).length > 0) {
                        var obj = MyBase.getTextValueBase("bookData");
                        AspAjax.setBookReturnDataBase(obj);
                    } else {
                        $("#msgDiv").html("請輸入圖書書號").fadeIn(function() {
                            setTimeout('$("#msgDiv").fadeOut();', '3000');
                        });
                    }
                }
            });
        }
    });
});


function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "searchUserData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (parseInt(result[0], 10) != -1 && parseInt(result[0], 10) != 0) {
                    $("#borrowerStatusName").html(_borrowerStatus[result[2]]);
                    $("#borrowerStatus").html(result[2]);
                    $("#borrowerName").html(result[1]);
                    $("#borrowerID").html(result[0]);
                    $("#bookCode").focus().bind('keydown', function(e) {
                        var key = e.which;
                        if (key == 13) {
                            e.preventDefault();
                            if (($("#bookCode").val()).length > 0) {
                                var obj = MyBase.getTextValueBase("userData");
                                var obj1 = getHideSpanValue("userData", "hideClassSpan");
                                MergerObject(obj, obj1);
                                AspAjax.createBookSystemDataBase(obj);
                            } else {
                                $("#msgDiv").html("請輸入圖書書號").fadeIn(function() {
                                    setTimeout('$("#msgDiv").fadeOut();', '3000');
                                });
                            }
                        }
                    });
                } else if (parseInt(result[0], 10) == -1) {
                    $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[1]).fadeIn(function() {
                        $("#gosrhpeopleID").focus().val("");
                        $("#borrowerStatus").add("#borrowerName").add("#borrowerID").empty();
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                } else if (parseInt(result[0], 10) == 0) {
                    $("#msgDiv").html(result[1]).fadeIn(function() {
                        $("#gosrhpeopleID").focus().val("");
                        $("#borrowerStatus").add("#borrowerName").add("#borrowerID").empty();
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                }
            }
            break;
        case "createBookSystemDataBase":
            $("#bookCode").focus().val("");
            if (parseInt(result[0].checkNo, 10) == -1) {
                $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[0].errorMsg).fadeIn(function() {
                    setTimeout('$("#msgDiv").fadeOut();', '3000');
                });
            } else if (parseInt(result[0].checkNo, 10) == 0) {
                $("#msgDiv").html(result[0].errorMsg).fadeIn(function() {
                    setTimeout('$("#msgDiv").fadeOut();', '3000');
                });
            } else {
                if (!(result == null || result.length == 0 || result == undefined)) {
                    if (parseInt(result[0].checkNo, 10) != -1) {
                        $("#msgDiv").html("借書成功").fadeIn(function() {
                            setTimeout('$("#msgDiv").fadeOut();', '3000');
                        });
                        var inner = "";
                        for (var i = 0; i < result.length; i++) {
                            inner += '<tr>' +
			                    '<td height="30">' + TransformADFromStringFunction(result[i].borrowDate) + '</td>' +
                                '<td>' + _borrowStatus[result[i].borrowStatus] + '</td>' +
                                '<td>' + _borrowerStatus[result[i].borrowerStatus] + '</td>' +
                                '<td>' + result[i].borrowerID + '</td>' +
                                '<td>' + result[i].borrowerName + '</td>' +
                                '<td>' + result[i].bookCode + '</td>' +
			                    '<td>' + result[i].bookName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].expireDate) + '</td>' +
			                    '<td>&nbsp;</td>' +
			                '</tr>';
                        }
                        $("#RecordList .tableList").children("tbody").prepend(inner);
                        $("#RecordList").fadeIn();
                    } else {
                        $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[0].errorMsg).fadeIn(function() {
                            setTimeout('$("#msgDiv").fadeOut();', '3000');
                        });
                    }
                } else {
                    $("#msgDiv").html("查無資料").fadeIn(function() {
                        setTimeout('$("#msgDiv").fadeOut();', '3000');
                    });
                }
            }
            break;
        case "setBookReturnDataBase":
            $("#bookReturnCode").focus().val("");
            if (parseInt(result[0].checkNo, 10) == -1) {
                $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[0].errorMsg).fadeIn(function() {
                    setTimeout('$("#msgDiv").fadeOut();', '3000');
                });
            } else if (parseInt(result[0].checkNo, 10) == 0) {
                $("#msgDiv").html(result[0].errorMsg).fadeIn(function() {
                    setTimeout('$("#msgDiv").fadeOut();', '3000');
                });
            } else {
                if (!(result == null || result.length == 0 || result == undefined)) {
                    if (parseInt(result[0].checkNo, 10) != -1) {
                        $("#msgDiv").html("還書成功").fadeIn(function() {
                            setTimeout('$("#msgDiv").fadeOut();', '3000');
                        });
                        var inner = "";
                        for (var i = 0; i < result.length; i++) {
                            inner += '<tr>' +
			                    '<td height="30">' + TransformADFromStringFunction(result[i].borrowDate) + '</td>' +
                                '<td>' + _borrowStatus[result[i].borrowStatus] + '</td>' +
                                '<td>' + _borrowerStatus[result[i].borrowerStatus] + '</td>' +
                                '<td>' + result[i].borrowerID + '</td>' +
                                '<td>' + result[i].borrowerName + '</td>' +
                                '<td>' + result[i].bookCode + '</td>' +
			                    '<td>' + result[i].bookName + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].expireDate) + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].restoreDate) + '</td>' +
			                '</tr>';
                        }
                        $("#RecordList .tableList").children("tbody").prepend(inner);
                        $("#RecordList").fadeIn();
                    } else {
                        $("#msgDiv").html("發生錯誤，錯誤訊息如下：" + result[0].errorMsg).fadeIn(function() {
                            setTimeout('$("#msgDiv").fadeOut();', '3000');
                        });
                    }
                } else {
                    $("#msgDiv").html("查無資料").fadeIn(function() {
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

