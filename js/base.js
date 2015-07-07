function Base() {
    this.getTextValueBase = function(htmltableName, compareObj) {
        var obj = {};
        if (htmltableName.length == 0) {
            return obj;
        }
        $("#" + htmltableName + " input").add("#" + htmltableName + " textarea").each(function(n) {
            var fn; //= this.id;
            fn = this.id.replace("gosrh", "txt");
            var val = ""; //Avoid IE8 JSON bug
            if (this.type == "checkbox" || this.type == "radio") {
                fn = $(this).attr("name");
                fn = fn.replace("gosrh", "txt");
                if (this.checked) {
                    val = $(this).val();
                }
            } else if (this.type == "select-one") {
                val = $(this).find('option:selected').attr("value") + "";
            } else if (this.type == "select-multiple") {
                var selected = [];
                $(this).children().each(function(i) {
                    if (this.selected) selected.push(i);
                });
                val = selected.join(",") + ""; //L_NOTE: , is split
            }
            else {
                var aa = $(this).attr("class");
                if ($(this).attr("class") != undefined && $(this).attr("class").indexOf("date") != -1) {
                    val = TransformRepublicReturnValue(this.value) + "";

                } else {
                    val = this.value + "";
                }
            }
            var npfn = fn.substring(fn.indexOf("txt"), fn.length);
            if (val.length > 0 && val != undefined && val != "undefined" && val != "請選擇" && npfn.length > 0) {
                if (this.type == "checkbox" && obj[fn] != undefined) {
                    obj[fn] = obj[fn] + "@@" + val;
                } else {
                    obj[fn] = val;
                }
            }
            else if (compareObj != undefined && compareObj[fn] != null && compareObj[fn].length > 0) {//原本有值 但現在被清空
                obj[fn] = "";
            }
        });
        $("#" + htmltableName + " select").each(function(n) {
            if (this.type == "select-one") {
                var fn; //= this.id;

                fn = this.id.replace("gosrh", "txt");
                var val = ""; //Avoid IE8 JSON bug
                if (this.type == "checkbox" || this.type == "radio")
                    val = this.checked + "";
                else if (this.type == "select-one") {
                    var itemValue = $(this).find('option:selected').attr("value");
                    if (itemValue != "0") {
                        val = $(this).find('option:selected').attr("value") + "";
                    }
                }
                else if (this.type == "select-multiple") {
                    var selected = [];
                    $(this).children().each(function(i) {
                        if (this.selected) selected.push(i);
                    });
                    val = selected.join(",") + "";
                }
                else {
                    val = this.value + "";
                }
                if (val.length > 0 && val != undefined && val != "undefined" && val != "請選擇" && fn.length > 0) {
                    obj[fn] = val;
                }
                else if (compareObj != undefined && compareObj[fn] != null && compareObj[fn].length > 0) {//原本有值 但現在被清空
                    obj[fn] = "";
                }
                if (fn.indexOf("txtRoadID") != -1 || fn.indexOf("txtAreaID") != -1) {
                    obj[fn.replace("ID", "")] = $(this).find('option:selected').text() + "";
                }
            }
        });

        return obj;
    }
    this.noEmptyCheck = function(checkArray, obj, ListObj, showWordArray) {
        var returncheck = '未填寫：'; //'<div id="popupShowMessage">';
        var counter = 0;
        for (var i = 0; i < checkArray.length; i++) {
            var tempstring = checkArray[i];
            var className = $('#' + tempstring).attr('class');
            if (className == undefined) { className = ""; }
            if (className.indexOf('tableList') != -1) {
                var key = tempstring.substring(tempstring.indexOf("txt"), tempstring.length);
                if (tempstring.indexOf("City") != -1) {
                    var item = $('select[name=' + tempstring + ']' + ' option:selected').text();
                    if (item == '縣市' || item.length == 0) {
                        returncheck += "縣市、"; //<br/>";
                        counter++;
                    }
                }
                else if (tempstring.indexOf("Area") != -1) {
                    var item = $('select[name=' + tempstring + ']' + ' option:selected').text();
                    if (item == '區域' || item.length == 0) {
                        returncheck += "鄉鎮市區、"; //<br/>";
                        counter++;
                    }
                } else if ($("select[name=" + tempstring + "]").attr('value') == '請選擇') {
                    returncheck += showWordArray[i] + "、"; //<br/>";
                    counter++;
                }
            } else {
                var checkString = obj[tempstring];
                if (checkString == "-1" || checkString == undefined || checkString.length == 0) {
                    returncheck += showWordArray[i] + "、"; //<br/>";
                    counter++;
                }
            }
        }
        returncheck += "";
        if (counter == 0) {
            returncheck = "";
        }
        return returncheck.substr(0, returncheck.length - 1);
    }
}

function Check() {
    this.checkIdentityNumber = function(Identity, callback) {
    if (Identity == undefined || Identity.length == 0)
            return true;
        reg = /^[A-Z]\d{9}$/;
        if (reg.test(Identity)) {
            return true;
        } else {
            return false;
        }
    }
    this.checkCellPhone = function(phone, callback) {
        if (phone == undefined || phone.length == 0)
            return true;
        //   /^[A-Z]\d{9}$/
        reg = /^[0-9]{10}$/;
        if (reg.test(phone)) {
            return true;
        } else {
            return false;
        }
    }
    this.checkHomePhone = function(phone) {

        if (phone == undefined || phone.length == 0)
            return true;
        //   /^[A-Z]\d{9}$/
        reg = /^[0]{1}[0-9]{1,2}\-[0-9]{7,8}$/;
        if (reg.test(phone)) {
            return true;
        } else {
            return false;
        }
    }
    this.checkEmail = function(email) {
        if (email == undefined || email.length == 0)
            return true;
        reg = /^[^\s]+@[^\s]+\.[^\s]{2,3}$/;
        if (reg.test(email)) {
            return true;
        } else {
            return false;
        }
    }
}