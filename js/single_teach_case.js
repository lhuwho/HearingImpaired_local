
$(document).ready(function() {
    $("#btnSearch").css("background-image", "url(./images/bg_menutab2.jpg)");
    initPage();
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

    var id = GetQueryString("id");
    var act = GetQueryString("act");
    if (id != null || act != null) {
        if (act != null) {
            getViewData(id, act);
        } else {
            window.location.href = './single_teach_case.aspx';
        }
    }

    $(".way").dropdownchecklist({ width: 60 });
    $(".ui-dropdownchecklist").css({ "margin": "4px 0", "height": "auto" });
    $(".ui-dropdownchecklist-dropcontainer").css({ "height": "auto" });
    $(".ui-dropdownchecklist-text").css({"font-size": "13px", "line-height": "20px"});
});

function getView(id) {
    window.open("./single_teach_case.aspx?id=" + id + "&act=2");
}

function getViewData(id, act) {
    $("#mainMenuList").add("#mainContentSearch").hide();
    $("#main").fadeIn();
    if (id != null) {
        $(".btnUpdate").fadeIn();
        $("input").add("select").add("textarea").attr("disabled", true);
//        $("#studentName").val("王小明");
//        $("#teacherName").val("連淑貞");
    } else if (id == null && act == 1) {
        $(".btnSave").fadeIn();
    }
}

function getAdd(tid) {
    var inner = '';
    inner += ' <tr id="'+tid+'_'+($("#table" + tid + ">tbody>tr").length+1))+'dataTR">'+
                ' <td colspan="4">  <table class="tableContact2" width="774" border="0"> <tr><th width="50">目標</th>'+
                '<td width="480"><textarea id="'+tid+'_'+($("#table" + tid + ">tbody>tr").length+1))+'TargetShort" class="short" cols="50" rows="3"></textarea></td>'
    
//    <tr id="1_1dataTR">
//                            <td colspan="4">
//                                <table class="tableContact2" width="774" border="0">
//                                    <tr>
//                                        <th width="50">目標</th>
//                                        <td width="480"><textarea id="1_1_1TargetShort" class="short" cols="50" rows="3"></textarea></td>
//                                        <td rowspan="2" width="84">
//                                            <input id="1_1_1PlanExecutionDate1" class="date" type="text" value="" size="10" /><br />
//                                            <input id="1_1_1PlanExecutionDate2" class="date" type="text" value="" size="10" /><br />
//                                            <input id="1_1_1PlanExecutionDate3" class="date" type="text" value="" size="10" /><br />
//                                            <input id="1_1_1PlanExecutionDate4" class="date" type="text" value="" size="10" /><br />
//                                            <input id="1_1_1PlanExecutionDate5" class="date" type="text" value="" size="10" />
//                                        </td>
//                                        <td rowspan="2" width="80" >
//                                            <select id="1_1_1Assessment1"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
//                                            <select id="1_1_1Assessment2"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
//                                            <select id="1_1_1Assessment3" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
//                                            <select id="1_1_1Assessment4" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select><br />
//                                            <select id="1_1_1Assessment5" ><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>
//                                        </td>
//                                        <td rowspan="2" width="80">
//                                            <select id="1_1_1Performance1"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
//                                            <select id="1_1_1Performance2"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
//                                            <select id="1_1_1Performance3"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
//                                            <select id="1_1_1Performance4"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select><br />
//                                            <select id="1_1_1Performance5"><option value="0">請選擇</option><option value="1">○</option><option value="2">＋</option><option value="3">＋－</option><option value="4">－</option></select>
//                                        </td>
//                                    </tr>
//                                    <tr>
//                                        <th>學習<br />進度</th>
//                                        <td><textarea id="1_1_1TargetContent" class="short" cols="50" rows="10"></textarea></td>
//                                    </tr>
//                                </table>
//                            </td>
//                        </tr>
//    $("#table" + tid + ">tbody>tr:last-child").after($("#dataTR" + tid).clone().attr("id", "dataTR" + tid + ($("#table" + tid + ">tbody>tr").length+1)));
//    var inner = '';
//    for (var i = 1; i < 6; i++) {
//        inner += '<select id="way' + tid + $("#table" + tid + ">tbody>tr").length + i + '" class="way" multiple="multiple"><option value="1">A</option><option value="2">B</option><option value="3">C</option><option value="4">D</option><option value="5">E</option></select>';
//        if (i < 5) {
//            inner += '<br />';
//        }
//    }
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length).find(".wayTD").html(inner);
//    $("#dataTR" + tid + $("#table" + tid + ">tbody>tr").length).find(".way").dropdownchecklist({ width: 60 });
//    $(".ui-dropdownchecklist").css({ "margin": "4px 0", "height": "auto" });
//    $(".ui-dropdownchecklist-dropcontainer").css({ "height": "auto" });
//    $(".ui-dropdownchecklist-text").css({ "font-size": "13px", "line-height": "20px" });
}

function getSubtract(tid) {
    if ($("#table" + tid + ">tbody>tr").length > 1) {
        $("#table" + tid + ">tbody>tr:last-child").detach();
    }
}

