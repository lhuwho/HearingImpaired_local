
var courseArray = new Array("認知語言溝通", "情境活動", "故事奶奶語言互動", "復活節彩蛋活動", "畢業典禮");

$(document).ready(function() {
    $("#main").fadeIn();

    var options = {
        height: 650,
        width: 780,
        navHeight: 25,
        labelHeight: 25,
        firstDayOfWeek: 1,
        navLinks: {
            enableToday: true,
            enableNextYear: false,
            enablePrevYear: false,
            p: '＜上個月',
            n: '下個月＞',
            t: '今天'
        },
        locale: {
            days: ["星期日", "星期一", "星期二", "星期三", "星期四", "星期五", "星期六", "星期日"],
            daysShort: ["日", "一", "二", "三", "四", "五", "六", "日"],
            months: ["一月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "十二月"]
        },
        onMonthChanging: function(dateIn) {
            //按上下個月的觸發事件
            return true;
        },
        onEventLinkClick: function(event) {
            //alert("event link click");
            getViewClick(event);
            return true;
        },
        onEventBlockClick: function(event) {
            //alert("block clicked");
            getViewClick(event);
            return true;
        },
        onEventBlockOver: function(event) {
            return true;
        },
        onEventBlockOut: function(event) {
            return true;
        },
        onDayLinkClick: function(date) {
            alert(date.toLocaleDateString());
            return true;
        },
        onDayCellClick: function(date) {
            //alert(date.toLocaleDateString());
            return true;
        }
    };

    var today = new Date();
    var events = [{ "EventID": 0, "Date": "2013-09-05", "Title": "09:00~11:00認知語言溝通", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明@認知語言溝通@09:00~11:00@E01", "CssClass": "course1" },
				    { "EventID": 1, "Date": "2013-09-11", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 2, "Date": "2013-09-06", "Title": "13:30~15:30故事課", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明、王小明@故事課@13:30~15:30@E01", "CssClass": "course1" },
				    { "EventID": 3, "Date": "2013-09-19", "Title": "09:00~11:00認知語言溝通", "URL": "#", "Description": "西瓜班@李小花、張清清、王威明@認知語言溝通@09:00~11:00@E01", "CssClass": "course1" },
				    { "EventID": 4, "Date": "2013-09-18", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 5, "Date": "2013-09-04", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" },
				    { "EventID": 6, "Date": "2013-09-25", "Title": "09:00~10:00王小明", "URL": "#", "Description": "個別班@王小明@個別課@09:00~10:00@E02", "CssClass": "course2" }
    ];
    $.jMonthCalendar.Initialize(options, events);
    $.jMonthCalendar.ChangeMonth(new Date(today.getFullYear(), today.getMonth(), today.getDate()));

    function resetForm($dialogContent) {
        $dialogContent.find("input").val("");
        $dialogContent.find("textarea").val("");
    }
});

function getViewClick(event) {
    var date = new Date(event["Date"]);
    var inner = event["Description"];
    var innerArray = inner.split("@");
    var msg = '<ul>' +
		        '<li>' +
				    '<b>日　　期</b>　' + (date.getFullYear() - 1911) + " 年 " + (date.getMonth() + 1) + " 月 " + date.getDate() + " 日" +
			    '</li>' +
			    '<li>' +
				    '<b>班　　別</b>　' + innerArray[0] +
			    '</li>' +
			    '<li>' +
				    '<b>學生姓名</b>　' + innerArray[1] +
			    '</li>' +
			    '<li>' +
				    '<b>課別名稱</b>　' + innerArray[2] +
			    '</li>' +
			    '<li>' +
				    '<b>上課時間</b>　' + innerArray[3] +
			    '</li>' +
			    '<li>' +
				    '<b>教室名稱</b>　' + innerArray[4] +
			    '</li>' +
	        '</ul>';
    $.fancybox({
        'content': '<div id="inline"><br /><table border="0" width="450"><tr><td width="90">&nbsp;</td><td>' + msg + '</td></tr></table><br /></div>',
        'autoDimensions': true,
        'centerOnScroll': true
    });
}

Date.prototype.Format = function(fmt) { //author: meizz
    var o = {
        "M+": this.getMonth() + 1, //月份
        "d+": this.getDate(), //日
        "h+": this.getHours(), //小時
        "m+": this.getMinutes(), //分
        "s+": this.getSeconds(), //秒
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度
        "S": this.getMilliseconds() //毫秒
    };
    if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    for (var k in o)
        if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
    return fmt;
}

