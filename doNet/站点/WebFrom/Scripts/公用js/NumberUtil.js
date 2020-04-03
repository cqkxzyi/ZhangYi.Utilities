
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
    return  s_x;
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

    if (blen > alen)
        alen = blen;
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


//判断obj是否为一个整数
function isInteger(obj) {
    return Math.floor(obj) === obj
}

/*
 * 将一个浮点数转成整数，返回整数和倍数。 如 3.14 >> 314，倍数是 100
 * @param floatNum {number} 小数
 * @return {object}
 *   {times:100, num: 314}
 */
function toInteger(floatNum) {
    var ret = { times: 1, num: 0 }
    if (isInteger(floatNum)) {
        ret.num = floatNum
        return ret
    }
    var strfi = floatNum + ''
    var dotPos = strfi.indexOf('.')
    var len = strfi.substr(dotPos + 1).length
    var times = Math.pow(10, len)
    var intNum = parseInt(floatNum * times + 0.5, 10)
    ret.times = times
    ret.num = intNum
    return ret
}

/*
 * 核心方法，实现加减乘除运算，确保不丢失精度
 * 思路：把小数放大为整数（乘），进行算术运算，再缩小为小数（除）
 *
 * @param a {number} 运算数1
 * @param b {number} 运算数2
 * @param digits {number} 精度，这里暂时没用，精度是根据传入的number值小数位数判断的
 * @param op {string} 运算类型，有加减乘除（add/subtract/multiply/divide）
 *
 */
function operation(a, b, digits, op) {
    var o1 = toInteger(a)
    var o2 = toInteger(b)
    var n1 = o1.num
    var n2 = o2.num
    var t1 = o1.times
    var t2 = o2.times
    var max = t1 > t2 ? t1 : t2
    var result = null
    switch (op) {
        case 'add':
            if (t1 === t2) { // 两个小数位数相同
                result = n1 + n2
            } else if (t1 > t2) { // o1 小数位 大于 o2
                result = n1 + n2 * (t1 / t2)
            } else { // o1 小数位 小于 o2
                result = n1 * (t2 / t1) + n2
            }
            return result / max
        case 'subtract':
            if (t1 === t2) {
                result = n1 - n2
            } else if (t1 > t2) {
                result = n1 - n2 * (t1 / t2)
            } else {
                result = n1 * (t2 / t1) - n2
            }
            return result / max
        case 'multiply':
            result = (n1 * n2) / (t1 * t2)
            return result
        case 'divide':
            result = (n1 / n2) * (t2 / t1)
            return result
    }
}

// 加减乘除的四个接口
function add(a, b, digits) {
    return operation(a, b, digits, 'add')
}
function subtract(a, b, digits) {
    return operation(a, b, digits, 'subtract')
}
function multiply(a, b, digits) {
    return operation(a, b, digits, 'multiply')
}
function divide(a, b, digits) {
    return operation(a, b, digits, 'divide')
}



//toFixed 修复
function toFixed(num, s) {
    var times = Math.pow(10, s)
    var des = num * times + 0.5
    des = parseInt(des, 10) / times
    return des + ''
}


