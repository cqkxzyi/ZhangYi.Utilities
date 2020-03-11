
//生成随机数
$.fn.Random = function () {
    var seed = new Array('1', '2', '3', '4', '5', '6', '7', '8', '9'); //数组
    var seedlength = seed.length; //数组长度  
    var createrandom = '';
    for (var i = 0; i < 6; i++) {
        j = Math.floor(Math.random() * seedlength);
        createrandom += seed[j];
    }
    return createrandom;
}


//num表示要四舍五入的数,v表示要保留的小数位数
function decimal(num, v) {
    var vv = Math.pow(10, v);
    return Math.round(num * vv) / vv;
}

/*
JS进行精确小数保留
*/
function ForDight(Dight, How) {
    Dight = Math.round(Dight * Math.pow(10, How)) / Math.pow(10, How);
    return Dight;
}

//四舍五入保留2位小数（不够位数，则用0替补）
function keepTwoDecimalFull(num) {
    if (!num || typeof (num) == "undefined")
        num = 0;

    var result = parseFloat(num);
    if (isNaN(result)) {
        alert('传递参数错误，请检查！');
        return false;
    }
    result = Math.round(num * 100) / 100;
    var s_x = result.toString();
    var pos_decimal = s_x.indexOf('.');
    if (pos_decimal < 0) {
        pos_decimal = s_x.length;
        s_x += '.';
    }
    while (s_x.length <= pos_decimal + 2) {
        s_x += '0';
    }
    return s_x;
}

// ----------------------------------------------------------------------
// <summary>
// 限制只能输入数字或者小数(只能保留两位小数)
// 用法;  $("#txtTotal").val(2);
// </summary>
// ----------------------------------------------------------------------
$.fn.onlyNum = function () {
    $(this).keyup(function (event) {
        //$(this).val($(this).val().replace(/[^\d]/g, ''));

        if ($(this).val() != '' && $(this).val().substr(0, 1) == '.') {
            $(this).value = "";
        }
        $(this).val($(this).val().replace(/^0*(0\.|[1-9])/, '$1'));//解决 粘贴不生效
        $(this).val($(this).val().replace(/[^\d.]/g, ""));  //清除“数字”和“.”以外的字符
        $(this).val($(this).val().replace(/\.{2,}/g, ".")); //只保留第一个. 清除多余的     
        $(this).val($(this).val().replace(".", "$#$").replace(/\./g, "").replace("$#$", "."));
        $(this).val($(this).val().replace(/^(\-)*(\d+)\.(\d\d).*$/, '$1$2.$3'));//只能输入两个小数     
        if ($(this).val().indexOf(".") < 0 && $(this).val() != "") {//以上已经过滤，此处控制的是如果没有小数点，首位不能为类似于 01、02的金额
            if ($(this).val().substr(0, 1) == '0' && $(this).val().length == 2) {
                $(this).val() = $(this).val().substr(1, $(this).val().length);
            }
        }

    }).focus(function () {
        //禁用输入法
        this.style.imeMode = 'disabled';
    }).bind("paste", function () {
        //CTRL+事件处理
        $(this).val($(this).val().replace(/[^\d.]/g, ''));
        return false;
    }).bind("blur", function () {
        //CTRL+V事件处理
        $(this).val($(this).val().replace(/[^\d.]/g, ''));
    });
};

/*
JS进行精确运算
*/
function yunsuan(a, how, b) {
    if (a.toString().indexOf(".") < 0 && b.toString().indexOf(".") < 0) {
        //没小数   
        return eval(a + how + b);
    }
    //至少一个有小数   
    var alen = a.toString().split(".");
    if (alen.length == 1) {
        //没有小数   
        alen = 0;
    } else {
        alen = alen[1].length;
    }
    var blen = b.toString().split(".");
    if (blen.length == 1) {
        blen = 0;
    } else {
        blen = blen[1].length;
    }
    if (blen > alen) alen = blen;
    blen = "1";
    for (; alen > 0; alen--) {
        //创建一个相应的倍数   
        blen = blen + "0";
    }
    switch (how) {
        case "+":
            return (a * blen + b * blen) / blen;
            break;
        case "-":
            return (a * blen - b * blen) / blen;
            break;
        case "*":
            return ((a * blen) * (b * blen)) / (blen * blen);
            break;
        case "/":
            return (a * blen) / (b * blen);
            break;
    }
}

/*
JS计算字符串字节长度
*/
function getKBInfo(str) {
    if (str == null) {
        return 0;
    }
    else {
        var bytesLength = str.length;
        var KBLength = yunsuan(bytesLength, "/", 1024);
        KBLength = ForDight(KBLength, 2);
        return KBLength;
    }
}

//构造一个Guid
function NewGUID() {
    var guid = (G() + G() + "-" + G() + "-" + G() + "-" +
        G() + "-" + G() + G() + G()).toUpperCase();
    return guid;
}

function G() {
    return (((1 + Math.random()) * 0x10000) | 0).toString(16).substring(1);
}
