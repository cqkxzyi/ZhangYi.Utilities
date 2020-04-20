
//将Date(2123456789456)转换成 DateTime
function FormatToDateTime(val) {
    if (val != null) {
        var date = new Date(parseInt(val.replace("/Date(", "").replace(")/", ""), 10));
        return date;
    }
    else {
        return null;
    }
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


//字符串转换成Date对象
//为了兼容Safari浏览器
function StringFormatDate(str) {
    str = (str + "").replace(/T/g, ' ');
    return new Date(str);
}


//日期格式不标准的转换成标准日期
//返回例如：2020-12-02 10:12:20
function GetRightDateStr(str) {
    var newstr = "";
    str = (str + "").replace(/T/g, ' ');

    var stringArr = str.split(" ");
    newstr = stringArr[0];//年月日部分

    var hh, mm, ss;
    var arr2 = stringArr[1].split(":");

    if (arr2.length == 1) {
        hh = arr2[0];
        if (hh.length == 1)
            hh = "0" + hh;
        mm = "00";
        ss = "00";
    }
    else if (arr2.length == 2) {
        hh = arr2[0];
        mm = arr2[1];
        if (hh.length == 1)
            hh = "0" + hh;
        if (mm.length == 1)
            mm = "0" + mm;
        ss = "00";
    }
    else {
        hh = arr2[0];
        mm = arr2[1];
        ss = arr2[2];
        if (hh.length == 1)
            hh = "0" + hh;
        if (mm.length == 1)
            mm = "0" + mm;
        if (ss.length == 1)
            ss = "0" + ss;
    }

    newstr = newstr + " " + hh + ":" + mm + ":" + ss;
    return newstr;
}


//得到中文日期格式
function GetDateStrByCn(dt, type) {
    if (typeof (type) == "undefined")
        type = "1";

    var xYear = objDate.getFullYear();//或者 objDate.getYear()+ 1900;

    var xMonth = dt.getMonth() + 1;
    if (xMonth < 10) {
        xMonth = "0" + xMonth;
    }
    var xDay = dt.getDate();
    if (xDay < 10) {
        xDay = "0" + xDay;
    }
    var xHours = dt.getHours();
    if (xHours < 10) {
        xHours = "0" + xHours;
    }
    var xMinutes = dt.getMinutes();
    if (xMinutes < 10) {
        xMinutes = "0" + xMinutes;
    }
    var xSeconds = dt.getSeconds();
    if (xSeconds < 10) {
        xSeconds = "0" + xSeconds;
    }

    //根据星期数的索引确定其中文表示
    var day = objDate.getDay();//用于判断星期几
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

    var retnStr = "";
    switch (format) {
        case "1"://2020年09月12日
            retnStr = xYear + "年" + xMonth + "月" + xDay + "日";
            break;
        case "2"://04月12日
            retnStr = xMonth + "月" + xDay + "日";
            break;
        case "3"://04月12日 10:20
            retnStr = xMonth + "月" + xDay + "日" + xHours + ":" + xMinutes;
            break;
        case "4"://04月12日 10:20:12
            retnStr = xMonth + "月" + xDay + "日" + xHours + ":" + xMinutes + ":" + xSeconds;
            break;
        default:
            retnStr = xYear + "年" + xMonth + "月" + xDay + "日" + xHours + ":" + xMinutes + ":" + xSeconds;
            break;
    }
    return retnStr;
}



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

//得到间隔天数
function GetDiffDay(objDate1,objDate2) {
    alert("间隔天数为:" + (objDate1 - objDate2) / 1000 / 60 / 60 / 24 + "天")
} 

//得到间隔时间
function GetDiffHH(d1, d2) {
    var d3 = d1 - d2;
    var h = Math.floor(d3 / 3600000);
    var m = Math.floor((d3 - h * 3600000) / 60000);
    var s = (d3 - h * 3600000 - m * 60000) / 1000;
    alert("相差" + h + "小时" + m + "分" + s + "秒");
}



//数字日期转汉字 
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


//得到前N天或后N天的日期 
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

function showdate(n) {
    var uom = new Date();
    uom.setDate(uom.getDate() + n);
    uom = uom.getFullYear() + "-" + (uom.getMonth() + 1) + "-" + uom.getDate() + "\n星期" + ('天一二三四五六'.charAt(uom.getDay())) + "\n本月有" + uom.getDays() + "天";
    return uom;
}



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
***举例： var myDate = isDateTime("2004-12-21 23:01:00");   //返回正确的日期
***       var myDate = isDateTime("2004-12-21");            //返回正确的日期
***       var myDate = isDateTime("2004-23-12 12:60:29");   //返回false，
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


function getTs(time1, time2) {
    try {
        var ts = time2 - time1;
        return ts;

    } catch (e) {
        var arr = time1.getTime().split(/[- : \/]/);
        var date1 = new Date(arr[0], arr[1] - 1, arr[2], arr[3], arr[4], arr[5]);
    }

    
    //if (isWordDay) {
    //    var nowDate = new Date();
    //    var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1) : nowDate.getMonth() + 1;
    //    var strStart = (nowDate.getFullYear()).toString() + "-" + month + "-" + (nowDate.getDate()).toString();

    //    var startTime = new Date(Date.parse(strStart + " 09:00:00"));
    //    var endTime = new Date(Date.parse(strStart + " 17:30:00"));

    //    if (startTime > nowDate || endTime < nowDate) {
    //        isWordDay = false;
    //    }
    //}
    ////IOS兼容
    //if (isWordDay) {
    //    var nowDate = new Date();
    //    var month = nowDate.getMonth() + 1 < 10 ? "0" + (nowDate.getMonth() + 1) : nowDate.getMonth() + 1;
    //    var strStart = (nowDate.getFullYear()).toString() + "/" + month + "/" + (nowDate.getDate()).toString();

    //    var arr = (strStart + ' 09:00:00').split(/[- : \/]/),
    //        arr2 = (strStart + ' 17:30:00').split(/[- : \/]/),
    //        date = new Date(arr[0], arr[1] - 1, arr[2], arr[3], arr[4], arr[5]),
    //        date2 = new Date(arr2[0], arr2[1] - 1, arr2[2], arr2[3], arr2[4], arr2[5]);

    //    if (date > nowDate || date2 < nowDate) {
    //        isWordDay = false;
    //    }
    //}
}





//获取距离当前时间差（一分钟前、两天前...）
function getDateDiff(post_modified) {
    post_modified = post_modified.replace(new RegExp("-", 'g'), "/");

    // 拿到当前时间戳和发布时的时间戳，然后得出时间戳差
    var curTime = new Date();
    var postTime = new Date(post_modified);

    var timeDiff = getTs(postTime, curTime);


    // 单位换算
    var min = 60 * 1000;
    var hour = min * 60;
    var day = hour * 24;
    var week = day * 7;
    var month = week * 4;
    var year = month * 12;

    // 计算发布时间距离当前时间的周、天、时、分
    var exceedyear = Math.floor(timeDiff / year);
    var exceedmonth = Math.floor(timeDiff / month);
    var exceedWeek = Math.floor(timeDiff / week);
    var exceedDay = Math.floor(timeDiff / day);
    var exceedHour = Math.floor(timeDiff / hour);
    var exceedMin = Math.floor(timeDiff / min);

    // 最后判断时间差到底是属于哪个区间，然后return

    if (exceedyear < 100 && exceedyear > 0) {
        return '发表于' + exceedyear + '年前';
    } else {
        if (exceedmonth < 12 && exceedmonth > 0) {
            return exceedmonth + '月前';
        } else {
            if (exceedWeek < 4 && exceedWeek > 0) {
                return exceedWeek + '星期前';
            } else {
                if (exceedDay < 7 && exceedDay > 0) {
                    return exceedDay + '天前';
                } else {
                    if (exceedHour < 24 && exceedHour > 0) {
                        return exceedHour + '小时前';
                    } else {
                        if (exceedMin == 0) {
                            return '刚刚发表';
                        } else {
                            return exceedMin + '分钟前';
                        }

                    }
                }
            }
        }
    }
}