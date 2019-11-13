/**
 * 
 * 查找数组，返回匹配到的第一个index
 * @param array 被查找的数组
 * @param feature 查找特征 或者为一个具体值，用于匹配数组遍历的值，或者为一个对象，表明所有希望被匹配的key-value
 * @param or boolean 希望命中feature全部特征或者只需命中一个特征，默认true
 * @return 数组下标  查找不到返回-1
 例子：
 //单个特征查找
var index = findArray(arr, {id: '1'});
//多个特征全满足查找
var index = findArray(arr, {id: '1', name: 'cmx'});
//多个特征单个满足查找（只需满足其中一个）
var index = findArray(arr, {id: '1', name: '习大大'}, false);

可能一下函数能达到效果
var newArr = array.filter(function(obj){
  return id !== obj.id;
});
 */
function findArray(array, feature, isAll) {
    var all = arguments[2] ? arguments[2] : true;
    for (let index in array) {
        let cur = array[index];
        if (feature instanceof Object) {
            let allRight = true;
            for (let key in feature) {
                let value = feature[key];
                if (cur[key] == value && !all)
                    return index;
                if (all && cur[key] != value) {
                    allRight = false;
                    break;
                }
            }
            if (allRight) return index;
        } else {
            if (cur == feature) {
                return index;
            }
        }
    }
    return -1;
}

//根据数组中的某个字段去重
function unique(arr, field) {
    var map = {};
    var res = [];
    for (var i = 0; i < arr.length; i++) {
        if (!map[arr[i][field]]) {
            map[arr[i][field]] = 1;
            res.push(arr[i]);
        }
    }
    return res;
}


//数组对象处理相关方法
Array.prototype.del = function (n) {//n表示第几项，从0开始算起。
    //prototype为对象原型，注意这里为对象增加自定义方法的方法。
    if (n < 0)//如果n<0，则不进行任何操作。
        return this;
    else
        return this.slice(0, n).concat(this.slice(n + 1, this.length));
    /*
　　　concat方法：返回一个新数组，这个新数组是由两个或更多数组组合而成的。
　　　　　　　　　这里就是返回this.slice(0,n)/this.slice(n+1,this.length)
　　 　　　　　　组成的新数组，这中间，刚好少了第n项。
　　　slice方法： 返回一个数组的一段，两个参数，分别指定开始和结束的位置。
　　*/
}
Array.prototype.indexOf = function (Object) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == Object) {
            return i;
        }
    }
    return -1;
}
Array.prototype.OrderByDesc = function (func) {
    var m = {};
    for (var i = 0; i < this.length; i++) {
        for (var k = 0; k < this.length; k++) {
            if (func(this[i]) > func(this[k])) {
                m = this[k];
                this[k] = this[i];
                this[i] = m;
            }
        }
    }
    return this;
}
Array.prototype.OrderDesc = function (propertyname) {
    var m = {};
    for (var i = 0; i < this.length; i++) {
        for (var k = 0; k < this.length; k++) {
            if ((this[i][propertyname]) > (this[k][propertyname])) {
                m = this[k];
                this[k] = this[i];
                this[i] = m;
            }
        }
    }
    return this;
}
Array.prototype.OrderAsc = function (propertyname) {
    var m = {};
    for (var i = 0; i < this.length; i++) {
        for (var k = 0; k < this.length; k++) {
            if ((this[i][propertyname]) < (this[k][propertyname])) {
                m = this[k];
                this[k] = this[i];
                this[i] = m;
            }
        }
    }
    return this;
}
Array.prototype.Where = function (p, t, v) {
    var a = new Array();
    if (t == "!=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] != v) {
                a.push(this[i]);
            }
        }
    } else if (t == "=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] == v) {
                a.push(this[i]);
            }
        }
    }
    else if (t == ">") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] > v) {
                a.push(this[i]);
            }
        }
    }
    else if (t == "<") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] < v) {
                a.push(this[i]);
            }
        }
    }
    else if (t == "<=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] <= v) {
                a.push(this[i]);
            }
        }
    }
    else if (t == ">=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] >= v) {
                a.push(this[i]);
            }
        }
    }
    return a;
}
Array.prototype.Distinct = function () {
    var ArrayObj = {};
    var returnArray = [];
    for (var i = 0; i < this.length; i++) {
        if (ArrayObj[this[i]]) continue;
        ArrayObj[this[i]] = this[i];
        returnArray.push(this[i]);
    }
    return returnArray;
}
Array.prototype.FirstOrDefault = function () {
    if (this.length > 0) {
        return this[0];
    }
    else {
        return null;
    }
}
Array.prototype.FindFirst = function (p, t, v) {

    if (t == "!=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] != v) {
                return this[i];
            }
        }
    } else if (t == "=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] == v) {
                return this[i];
            }
        }
    }
    else if (t == ">") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] > v) {
                return this[i];
            }
        }
    }
    else if (t == "<") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] < v) {
                return this[i];
            }
        }
    }
    else if (t == "<=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] <= v) {
                return this[i];
            }
        }
    }
    else if (t == ">=") {
        for (var i = 0; i < this.length; i++) {
            if (this[i][p] >= v) {
                return this[i];
            }
        }
    }
    return null;
}
Array.prototype.delItem = function (p, v) {
    var n;
    var a = [];
    for (var i = 0; i < this.length; i++) {
        if (this[i][p] == v) {
            n = i;
            break;
        }
    }
    if (n != undefined) {
        a = this.del(n);
        return a;
    }
    else {
        return this;
    }
}
///把数据一分为二 返回对象{a:any[],b:any[]}的对象
/// 如果为不够均分,则b数组末尾元素为null
Array.prototype.splitArrayToTwo = function splitArrayToTwo() {
    var a = this;
    var ret = { a: [], b: [] };
    for (var i = 0; i < a.length; i++) {
        if (i % 2 == 0) {
            ret.a.push(a[i]);
        }
        else {
            ret.b.push(a[i]);
        }
    }
    if (a.length % 2 !== 0) {
        ret.b.push(null);
    }
    return ret;
}