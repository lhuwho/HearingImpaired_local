
$(document).ready(function() {
    $("#main").add("#weekTable").fadeIn();
    getThisWeek();
    roomsDiv();

    $(".course1").add(".course2").click(function() {
        getViewClick(this);
    });
});


function roomsDiv() {
    $(".rooms").each(function() {
        var t = $(this).html();
        if (t == "&nbsp;" || t == "") {
            $(this).removeClass("course1");
            $(this).removeClass("course2");
        } else {
            $(this).addClass("course2");
            if (!$(this).has('span').length) {
                $(this).removeClass("course2");
                $(this).addClass("course1");
            }
        }
    });
}

function getViewClick(event) {
    var inner = '';
    var msg = '<ul>' +
		        '<li>' +
				    '<b>日　　期</b>　' +
			    '</li>' +
			    '<li>' +
				    '<b>班　　別</b>　' +
			    '</li>' +
			    '<li>' +
				    '<b>學生姓名</b>　' +
			    '</li>' +
			    '<li>' +
				    '<b>課別名稱</b>　' +
			    '</li>' +
			    '<li>' +
				    '<b>上課時間</b>　' +
			    '</li>' +
			    '<li>' +
				    '<b>教室名稱</b>　' +
			    '</li>' +
	        '</ul>';
    $.fancybox({
        'content': '<div id="inline"><br /><table border="0" width="450"><tr><td width="90">&nbsp;</td><td>' + msg + '</td></tr></table><br /></div>',
        'autoDimensions': true,
        'centerOnScroll': true
    });
}

var lastStartDay = 6;
var nextStartDay = 8;

function getLastWeek() {
    var now = new Date();
    var n = now.getDay();
    var start = new Date();
    var LastWeekDay = start.setDate(now.getDate() - n - lastStartDay);
    var LastWeekDay1 = new Date(LastWeekDay);
    for (var i = 0; i < 7; i++) {
        var LastWeekDay2 = new Date(LastWeekDay1.getFullYear(), LastWeekDay1.getMonth(), LastWeekDay1.getDate() + i);
        $(".weekDate" + (i + 1)).html((LastWeekDay2.getFullYear() - 1911) + "." + (LastWeekDay2.getMonth() + 1) + "." + LastWeekDay2.getDate());
    }
    lastStartDay = lastStartDay + 7;
	nextStartDay = 1;

    $("#weekTable").find(".rooms").fadeOut();
}

function getNextWeek() {
    var now = new Date();
    var n = now.getDay();
    var start = new Date();
    var NextWeekDay = start.setDate(now.getDate() - n + nextStartDay);
    var NextWeekDay1 = new Date(NextWeekDay);
    for (var i = 0; i < 7; i++) {
        var NextWeekDay2 = new Date(NextWeekDay1.getFullYear(), NextWeekDay1.getMonth(), NextWeekDay1.getDate() + i);
        $(".weekDate" + (i + 1)).html((NextWeekDay2.getFullYear() - 1911) + "." + (NextWeekDay2.getMonth() + 1) + "." + NextWeekDay2.getDate());
    }
    nextStartDay = nextStartDay + 7;
	lastStartDay = -1;

    $("#weekTable").find(".rooms").fadeOut();
}

function getThisWeek() {
    var now = new Date();
    var n = now.getDay();
    var start = new Date();
    for (var i = 1; i < 8; i++) {
        var thisWeekDay = start.setDate(now.getDate() - n + i);
        var thisWeekDay2 = new Date(thisWeekDay);
        $(".weekDate" + i).html((thisWeekDay2.getFullYear() - 1911) + "." + (thisWeekDay2.getMonth() + 1) + "." + thisWeekDay2.getDate());
    }
    $("#weekTable").find(".rooms").fadeIn();
	lastStartDay = 6;
	nextStartDay = 8;
}

