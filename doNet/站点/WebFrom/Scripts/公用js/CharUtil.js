//清除换行
$.fn.ClearBr = function (key) {
    key = key.replace(/[\r\n]/g, "");
    return (key);
}

//处理空格
function Trim(str) { return str.replace(/\s+/g, ""); }
function LTrim(str) { return str.replace(/(^\s*)/g, ""); }
function RTrim(str) { return str.replace(/(\s*$)/g, ""); }



//验证字符串是否含有非法字符
function illegalChar(val) {
    var ret = false;
    var _left = val.indexOf("<");
    var _right = val.indexOf("<");
    if (_left > -1 || _right > -1) {
        ret = true;
    }
    return ret;
}



//自动去除不安全字符如|"'<>等
function trimUnSafeChar() {
    var items = document.getElementsByTagName("input");
    for (var i = 0; i < items.length; i++) {
        if (items[i].type == "text") {
            items[i].value = items[i].value.replace(/<|>|'|&|\?/gi, "");
        }
    }
    var items = document.getElementsByTagName("textarea");
    for (var i = 0; i < items.length; i++) {
        items[i].value = items[i].value.replace(/<|>|'|&|\?/gi, "");
    }
}

//自动去除不安全字符
function clearHtml(strHtml) {
    var strText = strHtml.replace(/<(?!hr)(?:.|\s)*?>/ig, "");
}

//html  编码
function HTMLEncode(text) {
    text = text.replace(/</g, "＜");
    text = text.replace(/>/g, "＞");
    text = text.replace(/""/g, "“”");
    text = text.replace(/''/g, "‘’");
    text = text.replace(/"/g, "“");
    text = text.replace(/'/g, "’");
    text = text.replace(/;/g, "；");
    return text;
}

//html  解码
$.fn.HtmlDiscode = function (theString) {
    theString = theString.replace("&gt;", ">");
    theString = theString.replace("&lt;", "<");
    theString = theString.replace("&nbsp;", "");
    theString = theString.replace(" &nbsp;", " ");
    theString = theString.replace("&quot;", "\"");
    theString = theString.replace("&#39;", "\'");
    theString = theString.replace("<br/> ", "\n");
    return theString;
}

function clearNoNum(event, obj) {
    //响应鼠标事件，允许左右方向键移动 
    event = window.event || event;
    if (event.keyCode == 37 | event.keyCode == 39) {
        return;
    }
    //先把非数字的都替换掉，除了数字和. 
    obj.value = obj.value.replace(/[^\d.]/g, "");
    //必须保证第一个为数字而不是. 
    obj.value = obj.value.replace(/^\./g, "");
    //保证只有出现一个.而没有多个. 
    obj.value = obj.value.replace(/\.{2,}/g, ".");
    //保证.只出现一次，而不能出现两次以上 
    obj.value = obj.value.replace(".", "$#$").replace(/\./g, "").replace("$#$", ".");
}

function checkNum(obj) {
    //为了去除最后一个. 
    obj.value = obj.value.replace(/\.$/g, "");
}