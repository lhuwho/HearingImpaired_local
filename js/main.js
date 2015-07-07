
$(document).ready(function() {
    AspAjax.set_defaultSucceededCallback(SucceededCallback);
    AspAjax.set_defaultFailedCallback(FailedCallback);
    AspAjax.getMyselfRemindSystemData();
    initPage();
});

function SucceededCallback(result, userContext, methodName) {
    // alert(methodName);
    switch (methodName) {
        case "getMyselfRemindSystemData":
            if (!(result == null || result.length == 0 || result == undefined)) {
                if (result[0].recipient != -1) {
                    var inner = "";
                    for (var i = 0; i < result.length; i++) {
                        inner += ' <tr id="HS_' + result[i].rID + '" class="sStaffData" >' +
			                    '<td>' + result[i].Number + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].designeeDate) + '</td>' +
			                    '<td>' + result[i].designee + '</td>' +
			                    '<td>' + result[i].executionContent + '</td>' +
			                    '<td>' + TransformADFromStringFunction(result[i].executionDate) + '</td>' +
			                    '<td><input type="text" class="date SfulfillmentDate" value="' + TransformADFromStringFunction(result[i].fulfillmentDate) + '" size="10" /></td>' +
			                    '<td><button class="btnView" type="button" onclick="SaveData(\'' + result[i].rID + '\')">儲 存</button></div>' +
			                    '</td>' +
			                '</tr>';
                    }
                    $("#mainContent .tableText").children("tbody").html(inner);
                    $('.date').datepick({
                        yearRange: new Date().getFullYear() - 30 + ":" + (new Date().getFullYear() + 5)
                    });
                } else {
                    alert("發生錯誤，錯誤訊息如下：" + result[0].designee);
                }
            } else {
            $("#mainContent .tableText").children("tbody").html("<tr><td colspan='12'>查無資料</td></tr>");
            }
            break;
        case "setRemindSystemData2":
            if (parseInt(result[0]) <= 0) {
                alert("發生錯誤，錯誤訊息如下:" + result[1]);
            } else {
                alert("更新成功");
                window.location.reload();
            }
            break;
    }
}
function FailedCallback(error, userContext, methodName) {
    //  alert("功能有問題 請再試一次 或重新整理" + error.get_message() + " " + methodName);

}
function SaveData(TrID) {
    $("#HS_" + TrID + " input[type=text]").attr("disabled", true);
    $("#HS_" + TrID + " .UD").show();
    $("#HS_" + TrID + " .SC").hide();
    var obj = new Object();
    obj.rID = parseInt(TrID);
    var SfulfillmentDate = TransformRepublicReturnValue($("#HS_" + TrID + " .SfulfillmentDate").val());
    if (SfulfillmentDate.length > 0) {
        obj.fulfillmentDate = SfulfillmentDate;
    }
    AspAjax.setRemindSystemData2(obj);

}