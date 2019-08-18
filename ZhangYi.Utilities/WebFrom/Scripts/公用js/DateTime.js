Date.prototype.GetFullDate = function() {
    var objDate = new Date();
    var year = objDate.getFullYear();
    var month = objDate.getMonth() + 1;    //getMonth返回的月份是从0开始的，因此要加1
    var date = objDate.getDate();
    var day = objDate.getDay();
    //根据星期数的索引确定其中文表示
    switch (day) {
        case 0:
            day = "星期日";
            break;
        case 1:
            day = "星期一";
            break;
        case 2:
            day = "星期二";
            break;
        case 3:
            day = "星期三";
            break;
        case 4:
            day = "星期四";
            break;
        case 5:
            day = "星期五";
            break;
        case 6:
            day = "星期六";
            break;
    }
    return "今天是：" + year + "年" + month + "月" + date + "日&nbsp; " + day;
}


//日期转换成长日期格式：2019年02月05日
Date.prototype.toCommonCase = function() {
    var xYear = this.getYear();
    xYear = xYear + 1900;

    var xMonth = this.getMonth() + 1;
    if (xMonth < 10) {
        xMonth = "0" + xMonth;
    }
    var xDay = this.getDate();
    if (xDay < 10) {
        xDay = "0" + xDay;
    }
    var xHours = this.getHours();
    if (xHours < 10) {
        xHours = "0" + xHours;
    }
    var xMinutes = this.getMinutes();
    if (xMinutes < 10) {
        xMinutes = "0" + xMinutes;
    }
    var xSeconds = this.getSeconds();
    if (xSeconds < 10) {
        xSeconds = "0" + xSeconds;
    }
    return xYear + "年" + xMonth + "月" + xDay + "日";
}

//将Date(2123456789456)转换成"2019-02-23"
function FormatToDate(val) {
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        //月份为0-11，所以+1，月份小于10时补个0
        var month = date.getMonth() + 1 < 10 ? "0" + (date.getMonth() + 1) : date.getMonth() + 1;
        var currentDate = date.getDate() < 10 ? "0" + date.getDate() : date.getDate();
        return date.getFullYear() + "-" + month + "-" + currentDate;
    }
    return "";
}


//日期格式转换 短日期格式（第一种方法）
//var newtime = getFormat(new Date(), "yyyy/MM/dd HH:mm");
//var newtime1 = getFormat(new Date(), "yyyy-MM-dd HH:mm");
//var newtime2 = getFormat(new Date(), "yyyy-MM-dd");
//var newtime3 = getFormat(new Date(), "hh:mm");
function getFormat(time, format) {
    Date.prototype.format = function(format) {
        var o = {
            "M+": this.getMonth() + 1, //month
            "d+": this.getDate(),    //day
            "h+": this.getHours(),   //hour
            "m+": this.getMinutes(), //minute
            "s+": this.getSeconds(), //second
            "q+": Math.floor((this.getMonth() + 3) / 3),  //quarter
            "S": this.getMilliseconds() //millisecond  
        }
        if (/(y+)/.test(format))
            format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(format))
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ? o[k] : ("00" + o[k]).substr(("" + o[k]).length));
        return format;
    }
    var thisDate = time.format(format);
    return thisDate;
}

//日期格式转换 短日期格式（第二种方法）
//将一个 Date 格式化为日期/时间字符串。
//alert(DateFormat.format(new Date(), 'yyyy年MM月dd日'));
//从给定字符串的开始分析文本，以生成一个日期。
//alert(DateFormat.parse('2010-03-17', 'yyyy-MM-dd'));
DateFormat = (function () {
    var SIGN_REGEXP = /([yMdhsm])(\1*)/g;
    var DEFAULT_PATTERN = 'yyyy-MM-dd';
    function padding(s, len) {
        var len = len - (s + '').length;
        for (var i = 0; i < len; i++) { s = '0' + s; }
        return s;
    };
    return ({
        format: function (date, pattern) {
            pattern = pattern || DEFAULT_PATTERN;
            return pattern.replace(SIGN_REGEXP, function ($0) {
                switch ($0.charAt(0)) {
                    case 'y': return padding(date.getFullYear(), $0.length);
                    case 'M': return padding(date.getMonth() + 1, $0.length);
                    case 'd': return padding(date.getDate(), $0.length);
                    case 'w': return date.getDay() + 1;
                    case 'h': return padding(date.getHours(), $0.length);
                    case 'm': return padding(date.getMinutes(), $0.length);
                    case 's': return padding(date.getSeconds(), $0.length);
                }
            });
        },
        parse: function (dateString, pattern) {
            var matchs1 = pattern.match(SIGN_REGEXP);
            var matchs2 = dateString.match(/(\d)+/g);
            if (matchs1.length == matchs2.length) {
                var _date = new Date(1970, 0, 1);
                for (var i = 0; i < matchs1.length; i++) {
                    var _int = parseInt(matchs2[i]);
                    var sign = matchs1[i];
                    switch (sign.charAt(0)) {
                        case 'y': _date.setFullYear(_int); break;
                        case 'M': _date.setMonth(_int - 1); break;
                        case 'd': _date.setDate(_int); break;
                        case 'h': _date.setHours(_int); break;
                        case 'm': _date.setMinutes(_int); break;
                        case 's': _date.setSeconds(_int); break;
                    }
                }
                return _date;
            }
            return null;
        }
    });
})(); 



//程序:常用公历日期处理程序
/**//*用相对不规则的字符串来创建日期对象,不规则的含义为:顺序包含年月日三个数值串,有间隔*/
String.prototype.parseDate = function() {
    var ar = (this + ",0,0,0").match(/\d+/g);
    return ar[5] ? (new Date(ar[0], ar[1] - 1, ar[2], ar[3], ar[4], ar[5])) : (new Date());
}

/**//*
 * 功能:根据输入表达式返回日期字符串
 * 参数:dateFmt:字符串,由以下结构组成    
 *      yy:长写年,YY:短写年mm:数字月,MM:英文月,dd:日,hh:时,
 *      mi:分,ss秒,ms:毫秒,we:汉字星期,WE:英文星期.
 *      isFmtWithZero : 是否用0进行格式化,true or false
 * 返回:对应日期的中文字符串
*/
Date.prototype.toString = function(dateFmt, isFmtWithZero) {
    dateFmt = (dateFmt == null ? "yy-mm-dd hh:mi:ss" : dateFmt);
    if (typeof (dateFmt) != "string")
        throw (new Error(-1, 'toString()方法的第一个参数为字符串类别!'));
    isFmtWithZero = !!isFmtWithZero;
    var weekArr = [["日", "一", "二", "三", "四", "五", "六"], ["SUN", "MON", "TUR", "WED", "THU", "FRI", "SAT"]];
    var monthArr = ["JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC"];
    var str = dateFmt;
    var o = {
        "yy": this.getFullYear(),
        "YY": this.getYear(),
        "mm": this.getMonth() + 1,
        "MM": monthArr[this.getMonth()],
        "dd": this.getDate(),
        "hh": this.getHours(),
        "mi": this.getMinutes(),
        "ss": this.getSeconds(),
        "we": "星期" + weekArr[0][this.getDay()],
        "WE": weekArr[1][this.getDay()]
    }
    for (var i in o) {
        str = str.replace(new RegExp(i, "g"), o[i].toString().fmtWithZero(isFmtWithZero));
    }
    str = str.replace(/ms/g, this.getMilliseconds().toString().fmtWithZeroD(isFmtWithZero));
    return str;
}

//将一位数字格式化成两位,如: 9 to 09*/
String.prototype.fmtWithZero = function(isFmtWithZero) {
    return (isFmtWithZero && /^\d$/.test(this)) ? "0" + this : this;
}
String.prototype.fmtWithZeroD = function(isFmtWithZero) {
    return (isFmtWithZero && /^\d{2}$/.test(this)) ? "00" + this : ((isFmtWithZero && /^\d$/.test(this)) ? "0" + this : this);
}



/**//* 功能 : 返回与某日期相距N天(N个24小时)的日期
 * 参数 : num number类别 可以为正负整数或者浮点数,默认为1;
 *        type 0(秒) or 1(天),默认为天
 * 返回 : 新的Date对象
 */
Date.prototype.dateAfter = function(num, type) {
    num = (num == null ? 1 : num);
    if (typeof (num) != "number") throw new Error(-1, "dateAfterDays(num,type)的num参数为数值类别.");
    type = (type == null ? 1 : type);
    var arr = [1000, 86400000];
    return new Date(this.valueOf() + num * arr[type]);
}

//判断是否是闰年,返回true 或者 false
Date.prototype.isLeapYear = function() {
    var year = this.getFullYear();
    return (0 == year % 4 && ((year % 100 != 0) || (year % 400 == 0)));
}

//返回该月天数
Date.prototype.getDaysOfMonth = function() {
    return (new Date(this.getFullYear(), this.getMonth() + 1, 0)).getDate();
}

//日期比较函数,参数date:为Date类别,如this日期晚于参数:1,相等:0 早于: -1
Date.prototype.dateCompare = function(date) {
    if (typeof (date) != "object" || !(/Date/.test(date.constructor)))
        throw new Error(-1, "dateCompare(date)的date参数为Date类别.");
    var d = this.getTime() - date.getTime();
    return d > 0 ? 1 : (d == 0 ? 0 : -1);
}

/**//*功能:返回两日期之差
 *参数:pd   PowerDate对象
 *    type: 返回类别标识.yy:年,mm:月,ww:周,dd:日,hh:小时,mi:分,ss:秒,ms:毫秒
 *    intOrFloat :返回整型还是浮点型值 0:整型,不等于0:浮点型
 *    output : 输出提示,如:时间差为#周!
 */
Date.prototype.calDateDistance = function(date, type, intOrFloat, output) {
    if (typeof (date) != "object" || !(/Date/.test(date.constructor)))
        throw new Error(-1, "calDateDistance(date,type,intOrFloat)的date参数为Date类别.");
    type = (type == null ? 'dd' : type);
    if (!((new RegExp(type + ",", "g")).test("yy,mm,ww,dd,hh,mi,ss,ms,")))
        throw new Error(-1, "calDateDistance(pd,type,intOrFloat,output)的type参数为非法.");
    var iof = (intOrFloat == null ? 0 : intOrFloat);
    var num = 0;
    var o = {
        "ww": 7 * 86400000,
        "dd": 86400000,
        "hh": 3600000,
        "mi": 60000,
        "ss": 1000,
        "ms": 1
    }
    switch (type) {
        case "yy": num = this.getFullYear() - date.getFullYear(); break;
        case "mm": num = (this.getFullYear() - date.getFullYear()) * 12 + this.getMonth() - date.getMonth(); break;
        default:
            var sub = this.valueOf() - date.valueOf();
            if (o[type])
                num = (sub / o[type]).fmtRtnVal(iof);
            break;
    }
    return (output ? output.replace(/#/g, " " + num + " ") : num);
}

//计算天数差的函数，通用 
function DateDiff(sDate1, sDate2) { //sDate1和sDate2是2002-12-18格式 
    var aDate, oDate1, oDate2, iDays;
    aDate = sDate1.split("-");
    oDate1 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0]); //转换为12-18-2002格式 
    aDate = sDate2.split("-");
    oDate2 = new Date(aDate[1] + '-' + aDate[2] + '-' + aDate[0]);
    iDays = parseInt(Math.abs(oDate1 - oDate2) / 1000 / 60 / 60 / 24); //把相差的毫秒数转换为天数 
    return iDays;
}

//返回整数或者两位小数的浮点数
Number.prototype.fmtRtnVal = function(intOrFloat) {
    return (intOrFloat == 0 ? Math.floor(this) : parseInt(this * 100) / 100);
}
//根据当前日期所在年和周数返回周日的日期
Date.prototype.RtnByWeekNum = function(weekNum) {
    if (typeof (weekNum) != "number")
        throw new Error(-1, "RtnByWeekNum(weekNum)的参数是数字类别.");
    var date = new Date(this.getFullYear(), 0, 1);
    var week = date.getDay();
    week = (week == 0 ? 7 : week);
    return date.dateAfter(weekNum * 7 - week, 1);
}
//根据日期返回该日期所在年的周数
Date.prototype.getWeekNum = function() {
    var dat = new Date(this.getFullYear(), 0, 1);
    var week = dat.getDay();
    week = (week == 0 ? 7 : week);
    var days = this.calDateDistance(dat, "dd") + 1;
    return ((days + 6 - this.getDay() - 7 + week) / 7);
}


//将2005-8-5转换成2005-08-05格式
function ZhuanHua(objDate) {
    window.alert(objDate.replace(/\b(\w)\b/g, '0$1'));
}

//2、得到间隔天数
function GetDiffDay(objDate1,objDate2) {
    alert("间隔天数为:" + (objDate1 - objDate2) / 1000 / 60 / 60 / 24 + "天")
} 

//3、得到间隔时间
function GetDiffHH(d1, d2) {
    var d3 = d1 - d2;
    var h = Math.floor(d3 / 3600000);
    var m = Math.floor((d3 - h * 3600000) / 60000);
    var s = (d3 - h * 3600000 - m * 60000) / 1000;
    alert("相差" + h + "小时" + m + "分" + s + "秒");
}



//4、得到今天的日期 
function GetTodayDate() {
    d = new Date();
    return d.getFullYear() + "年" + (d.getMonth() + 1) + "月" + d.getDate() + "日";
}


//6、数字日期转汉字 
Date.prototype.getRead = function() {
    var values = new Array("零", "一", "二", "三", "四", "五", "六", "七", "八", "九");
    var returnValue, temp;
    returnValue = this.getYear() + "年";
    temp = (this.getMonth() + 1) + "月" + this.getDate() + "日";
    temp = temp.replace(/(\d)(\d)/g, "$1十$2").replace(/1十/g, "十").replace(/十0/g, "十");
    returnValue += temp;
    returnValue = returnValue.replace(/\d/g, function(sts) { return values[parseInt(sts)] });
    return returnValue;
}


//7、得到前N天或后N天的日期 
//方法一： 
function showdate(n) {
    var uom = new Date(new Date() - 0 + n * 86400000);
    uom = uom.getFullYear() + "-" + (uom.getMonth() + 1) + "-" + uom.getDate();
    return uom;
    //window.alert("今天是："+showdate(0)); 
    //window.alert("昨天是："+showdate(-1)); 
    //window.alert("明天是："+showdate(1)); 
    //window.alert("10天前是："+showdate(-10)); 
    //window.alert("5天后是："+showdate(5)); 
}

//方法二： 
function showdate(n) {
    var uom = new Date();
    uom.setDate(uom.getDate() + n);
    uom = uom.getFullYear() + "-" + (uom.getMonth() + 1) + "-" + uom.getDate();
    return uom;
    //window.alert("今天是："+showdate(0)); 
    //window.alert("昨天是："+showdate(-1)); 
    //window.alert("明天是："+showdate(1)); 
    //window.alert("10天前是："+showdate(-10)); 
    //window.alert("5天后是："+showdate(5)); 
}

//方法三（不好意思，这个市用vsscript做的）： 
function showdate(n) {
    showdate = dateadd("d", date(), n)
    //msgbox "今天是:"&showdate(0) 
    //msgbox "昨天是:"&showdate(-1) 
    //msgbox "明天是:"&showdate(1) 
    //msgbox "十天前是:"&showdate(-10) 
    //msgbox "五天后是:"&showdate(5) 
}
//方法四： 
Date.prototype.getDays = function() {
    var _newDate = new Date();
    _newDate.setMonth(_newDate.getMonth() + 1);
    _newDate.setDate(0);
    $_days = _newDate.getDate();
    delete _newDate;
    return $_days;
}

function showdate(n) {
    var uom = new Date();
    uom.setDate(uom.getDate() + n);
    uom = uom.getFullYear() + "-" + (uom.getMonth() + 1) + "-" + uom.getDate() + "\n星期" + ('天一二三四五六'.charAt(uom.getDay())) + "\n本月有" + uom.getDays() + "天";
    return uom;
}
//window.alert("今天是："+showdate(0)); 
//window.alert("昨天是："+showdate(-1)); 
//window.alert("明天是："+showdate(1)); 
//window.alert("10天前是："+showdate(-10)); 
//window.alert("5天后是："+showdate(5)); 



/*********************************************************************************
*校验字符串是否为日期型
*返回值：bool
*如果为空，定义校验通过，           返回true
*如果字串为日期型，校验通过，       返回true
*如果日期不合法，                   返回false    参考提示信息：输入域的时间不合法！（yyyy-MM-dd）
********************************** date ******************************************/
function isDate(str) {
    if (isEmpty(str)) return true; //如果为空，则通过校验

    var pattern = /^((\d{4})|(\d{2}))-(\d{1,2})-(\d{1,2})$/g;
    if (!pattern.test(str))
        return false;
    var arrDate = str.split("-");
    if (parseInt(arrDate[0], 10) < 100)
        arrDate[0] = 2000 + parseInt(arrDate[0], 10) + "";
    var date = new Date(arrDate[0], (parseInt(arrDate[1], 10) - 1) + "", arrDate[2]);
    if (date.getFullYear() == arrDate[0]
       && date.getMonth() == (parseInt(arrDate[1], 10) - 1) + ""
       && date.getDate() == arrDate[2])
        return true;
    else
        return false;
}

/*********************************************************************************
***功能：扩展日期函数，支持YYYY-MM-DD或YYYY-MMDD hh:mm:ss字符串格式
***返回：如果正确，则返回javascript中支持UTC日期格式，如果错误，则返回false  
***日期：2004-12-15
***举例： var myDate = isDate("2004-12-21 23:01:00");   //返回正确的日期
***       var myDate = isDate("2004-12-21");            //返回正确的日期
***       var myDate = isDate("2004-23-12 12:60:29");   //返回false，
**********************************************************************************/
function isDateTime(str) {
    if (str == "") return true;

    var strDate = str;
    if (strDate.length == 0) return false;

    //先判断是否为短日期格式：YYYY-MM-DD，如果是，将其后面加上00:00:00，转换为YYYY-MM-DD hh:mm:ss格式
    var reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2})/;   //短日期格式的正则表达式
    var r = strDate.match(reg);

    if (r != null)   //说明strDate是短日期格式，改造成长日期格式
        strDate = strDate + " 00:00:00";

    reg = /^(\d{1,4})(-|\/)(\d{1,2})\2(\d{1,2}) (\d{1,2}):(\d{1,2}):(\d{1,2})/;
    r = strDate.match(reg);
    if (r == null) {
        return false;
    }

    var d = new Date(r[1], r[3] - 1, r[4], r[5], r[6], r[7]);
    if (d.getFullYear() == r[1] && (d.getMonth() + 1) == r[3] && d.getDate() == r[4] && d.getHours() == r[5] && d.getMinutes() == r[6] && d.getSeconds() == r[7]) {
        return d;
    }
    else {
        return false;
    }
}

//IOS兼容算法
function Ios(){
   var isWordDay=true;
    if (isWordDay) {
        var nowDate = new Date();
        var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1) : nowDate.getMonth() + 1;
        var strStart = (nowDate.getFullYear()).toString() + "-" + month + "-" + (nowDate.getDate()).toString();

        var startTime = new Date(Date.parse(strStart + " 09:00:00"));
        var endTime = new Date(Date.parse(strStart + " 17:30:00"));

        if (startTime > nowDate || endTime < nowDate) {
            isWordDay = false;
        }
    }

    //IOS兼容
    if (isWordDay) {
        var nowDate = new Date();
        var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1) : nowDate.getMonth() + 1;
        var strStart = (nowDate.getFullYear()).toString() + "/" + month + "/" + (nowDate.getDate()).toString();

        var arr = (strStart+' 09:00:00').split(/[- : \/]/),
            arr2 = (strStart+' 17:30:00').split(/[- : \/]/),
            date = new Date(arr[0], arr[1] - 1, arr[2], arr[3], arr[4], arr[5]),
            date2 = new Date(arr2[0], arr2[1] - 1, arr2[2], arr2[3], arr2[4], arr2[5]);

        if (date > nowDate || date2 < nowDate) {
            isWordDay = false;
        }
    }
}