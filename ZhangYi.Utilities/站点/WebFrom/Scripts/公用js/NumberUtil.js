
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
