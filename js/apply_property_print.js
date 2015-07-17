var _money = new Array("零", "壹", "貳", "參", "肆", "伍", "陸", "柒", "捌", "玖");
$(document).ready(function() {
initPage();
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    //$("#container").hide();
    var id = GetQueryString("id");
    if (id != null) {
        AspAjax.getApplyPropertyDataBase(id);
    } else {
        alert("無此資料");
        window.location.href = './apply_property.aspx';
    }

    $("input[name=applyType]").click(function() {
        $("input[name=applyType]").next("img").attr("src", "./images/choose2.jpg");
        $(this).next("img").attr("src", "./images/choose.jpg");
    });
    $("input[name=Unit]").click(function() {
        $("input[name=Unit]").next("img").attr("src", "./images/choose2.jpg");
        $(this).next("img").attr("src", "./images/choose.jpg");
    });
    $("input[name=applyPay]").click(function() {
        $("input[name=applyPay]").next("img").attr("src", "./images/choose2.jpg");
        $(this).next("img").attr("src", "./images/choose.jpg");
    });
    $("#applyDate3").html("印製日期："+TodayADDateFunction());
});

function SucceededCallback(result, userContext, methodName) {
    switch (methodName) {
        case "getApplyPropertyDataBase":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result.checkNo == null && parseInt(result.checkNo) != -1) {
                    $("input[name=Unit][value=" + result.Unit + "]").attr("checked", "checked");
                    $("input[name=Unit]:checked").click();
                    $("input[name=applyType][value=" + result.applyType + "]").attr("checked", "checked");
                    $("input[name=applyType]:checked").click();
                    $("input[name=applyPay][value=" + result.applyPay + "]").attr("checked", "checked");
                    $("input[name=applyPay]:checked").click();
                    $("#applyID").html(result.applyID);

                    var applyTWDate = TransformADFromStringFunction(result.applyDate);
                    var applyTWDate2 = applyTWDate.split("/");
                    $("#applyDate span:nth-child(1)").html("中華民國" + applyTWDate2[0] + "年" + applyTWDate2[1] + "月" + applyTWDate2[2] + "日");
                    DigitShow(result.applySum);
                    var applySun = 0;
                    var inner = "";
                    for (var i = 0; i < result.DetailArray.length; i++) {
                        var DetailData = result.DetailArray[i];
                        var propertySum = parseInt(DetailData.Quantity) * parseInt(DetailData.Price);
                        
                        inner += '<tr>' +
                                    '<td height="40" align="center">' + DetailData.Name + '</td>' +
			                        '<td align="center">' + DetailData.Unit + '</td>' +
			                        '<td align="center">' + DetailData.Quantity + '</td>' +
			                        '<td>' + DetailData.Format + '</td>' +
			                        '<td align="right">' + DetailData.Price + '</td>' +
			                        '<td align="right">' + propertySum + '</td>' +
                                    '<td>' + DetailData.Explain + '</td>' + // class="context"
			                        '<td>' + DetailData.Bill + '</td>';
                        '</tr>';
                        applySun += propertySum;
                    }
                    for (i = result.DetailArray.length; i < 4; i++) {
                        inner += '<tr>' +
                                    '<td height="40" align="center">&nbsp;</td>' +
			                        '<td align="center">&nbsp;</td>' +
			                        '<td align="center">&nbsp;</td>' +
			                        '<td>&nbsp;</td>' +
			                        '<td align="right">&nbsp;</td>' +
			                        '<td align="right">&nbsp;</td>' +
                                    '<td>&nbsp;</td>' +
			                        '<td>&nbsp;</td>';
                        '</tr>';
                    }
                    $("#applySum").html(applySun);
                    $("#DetailTable tbody").empty().html(inner);
                    $("#container input[type=radio]").attr("disabled", true);
                    if (!window.print) {
                        alert("列印功能暫時停用，請按 Ctrl-P 來列印"); return;
                    }
                    window.print();
                } else if (parseInt(result.checkNo) == -2) {
                    $("body").empty();
                    alert(result.errorMsg);
                    window.location.href = "./main.aspx";
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result.errorMsg);
                }
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);
}

function DigitShow(applySum) {
    /*var sumArray = ((applySum).toString()).split("");
    var digitArray = new Array(0, 0, 0, 0, 0);

    switch (((applySum).toString()).length) {
        case 1:
            digitArray[4] = sumArray[0];
            break;
        case 2:
            digitArray[4] = sumArray[1];
            digitArray[3] = sumArray[0];
            break;
        case 3:
            digitArray[4] = sumArray[2];
            digitArray[3] = sumArray[1];
            digitArray[2] = sumArray[0];
            break;
        case 4:
            digitArray[4] = sumArray[3];
            digitArray[3] = sumArray[2];
            digitArray[2] = sumArray[1];
            digitArray[1] = sumArray[0];
            break;
        case 5:
            digitArray = sumArray;
            break;
    }*/

    /*$("#sumOne").val(_money[parseInt(digitArray[4], 10)]);
    $("#sumTwo").val(_money[parseInt(digitArray[3], 10)]);
    $("#sumThree").val(_money[parseInt(digitArray[2], 10)]);
    $("#sumFour").val(_money[parseInt(digitArray[1], 10)]);
    $("#sumFive").val(_money[parseInt(digitArray[0], 10)]);*/
    pushsunValue(applySum);
}
function pushsunValue(applySum) {
    var sunDiv = new Array("sumOne", "sumTwo", "sumThree", "sumFour", "sumFive");
    for (var i = 0; i < sunDiv.length; i++) {
        var astart = applySum.length - (i + 1);
        if (astart != 0 && i == sunDiv.length - 1) {
            astart = 0;
        }
        var sunNum = applySum.substring(astart, applySum.length - i);
        if (sunNum.length == 0) {
            sunNum = 0;
        }
        $("#" + sunDiv[i]).val(sunNum);
    }
}