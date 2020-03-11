/*ajax处理
 var defaultOptions = {};
defaultOptions = $.extend(true, defaultOptions, options);
*/
function ajaxPost(url, params, SuccessBack, ErrorBack, CompleteBack, loadingid, is_show_loading, async) {
    if (async == undefined) {
        async = true;
    }
    $.ajax({
        type: "POST",
        cache: false,
        url: url,
        data: params,
        async: async,
        success: function (data) {//响应成功
            if (SuccessBack != null && typeof (SuccessBack) == 'function') {
                SuccessBack(data);
            }
        },
        beforeSend: function () {//发送数据之前              
            if (is_show_loading) {
                $(loadingid).noasLoading();
            }
        },
        error: function (msg) {//异常
            if (ErrorBack != null && typeof (ErrorBack) == 'function') {
                ErrorBack(msg);
            }
        },
        complete: function (XMLHttpRequest, textStatus) {//最后要做的结束工作
            if (CompleteBack != null && typeof (CompleteBack) == 'function') {
                CompleteBack(XMLHttpRequest, textStatus);
            }
            if (is_show_loading) {
                $(loadingid).noasLoading({ show: false });
            }
        }
    });
}

/**
 * 错误处理
 * 调用方法  window.onerror = closeErrors;
 * */
function closeErrors() {
    arglen = arguments.length;
    var errorMsg = "参数个数：" + arglen + "个";
    for (var i = 0; i < arglen; i++) {
        errorMsg += "\n参数" + (i + 1) + "：" + arguments[i];
    }
    alert(errorMsg);
    //    window.onerror = null;
    //    return true;
}
//try {
//　　foo.bar();
//} catch (e) {
//　　if (browserType != BROWSER_IE) {　　　　　　　　　　　　　　
//　　　　alert("name: " + e.name +
//　　　　　　"message: " + e.message +
//　　　　　　"lineNumber: " + e.lineNumber +
//　　　　　　"fileName: " + e.fileName +
//　　　　　　"stack: " + e.stack);　　　　
//　　}
//　　else {　　　　　　　　　　
//　　　　alert("name: " + e.name +　　
//　　　　　　"errorNumber: " + (e.number & 0xFFFF ) +
//　　　　　　"message: " + e.message);　　　　
//　　}
//}




//获取url的相对路径
function GetUrlRelativePath(url) {
    if (url.indexOf("//") < 0) {
        return url;
    }
    var arrUrl = url.split("//");
    var start = arrUrl[1].indexOf("/");
    var relUrl = arrUrl[1].substring(start);//stop省略，截取从start开始到结尾的所有字符

    if (relUrl.indexOf("?") != -1) {
        relUrl = relUrl.split("?")[0];
    }
    return relUrl;
}


//得到Url中的参数 
//用法
// var pralist = GetUrlParms();        //调用方法得到参数
// if (pralist["roomID"] != null) {    //判断
// var roomID = pralist["roomID"];  //取数数值
function GetUrlParms() {
    var args = new Object();
    var query = location.search.substring(1).toLowerCase(); //获取查询串   
    var pairs = query.split("&"); //取得参数字符串   
    for (var i = 0; i < pairs.length; i++) {
        var pos = pairs[i].indexOf('='); //查找name=value   
        if (pos == -1) continue; //如果没有找到就跳过   
        var argname = pairs[i].substring(0, pos); //提取name   
        var value = pairs[i].substring(pos + 1); //提取value   
        args[argname] = unescape(value); //存为属性   
    }
    return args;
}

//得到Url中的参数(未解码)
function getQueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) {
        return unescape(r[2]);
    }
    return null;
}

//获取url指定参数（中文解码）
function getQueryBycn(name) {
    var query = window.location.search.substring(1);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == name) {
            return decodeURI(pair[1]);
        }
    }
    return "";
}

//替换指定传入参数的值,paramName为参数,replaceWith为新值
function replaceParamVal(paramName, replaceWith) {
    var oUrl = this.location.href.toString();
    var re = eval('/(' + paramName + '=)([^&]*)/gi');
    var nUrl = oUrl.replace(re, paramName + '=' + replaceWith);
    return nUrl;
}

//监听返回按键
//先添加这个代码，window.history.pushState('forward', null, './ApplyMain');
try {
    if (window.history && window.history.pushState) {
        $(window).on('popstate', function () {
            var hashLocation = location.hash;
            var hashSplit = hashLocation.split("#!/");
            var hashName = hashSplit[1];
            console.info(hashSplit);
            if (hashName !== '') {
                var hash = window.location.hash;
                this.console.info(hash);
                if (hash === '') {
                    if (!$("#div_xietong").is(':hidden')) {
                        AlertM.XieTong.close();

                    } else {
                        history.back(-1);
                    }
                }
            }
        });

    }
} catch (e) {
            
}


//绑定界面值
function BindInputValue(obj) {
    if (!obj)
        return;
    for (var name in obj) {
        $("#" + name).val(obj[name]);
    }
}