// 加入收藏
function AddFavor() {
    try {
        window.external.addFavorite(window.location.href, window.document.title);
    } catch (e) {
        try {
            window.sidebar.addPanel(window.document.title, window.location, "");
        } catch (e) {
            alert("加入收藏失败，请使用Ctrl+D进行添加");
        }
    }
}
// 设为首页
function SetHome(obj, url1) { //obj:this 
    try {
        obj.style.behavior = 'url(#default#homepage)'; obj.setHomePage(url1);
    } catch (e) {
        if (window.netscape) {
            try {
                netscape.security.PrivilegeManager.enablePrivilege("UniversalXPConnect");
            } catch (e) {
                alert("此操作被浏览器拒绝！请在浏览器地址栏输入“about:config”并回车然后将[signed.applets.codebase_principal_support]设置为'true'");
            }
            var prefs = Components.classes['@mozilla.org/preferences-service;1'].getService(Components.interfaces.nsIPrefBranch);
            prefs.setCharPref('browser.startup.homepage', window.location);
        }
    }
}
/*设置客户端的高和宽*/
function divCenter_Jt(eleId) {
    var eleObj = document.getElementById(eleId);
    var rr = new getClientBounds();
    eleObj.style.display = 'block';
    alert(document.body.scrollLeft);
    alert(document.body.scrollTop);
    alert(document.body.offsetLeft);
    alert(document.body.offsetTop);
    eleObj.style.left = (rr.width - eleObj.clientWidth) / 2 + document.body.scrollLeft;
    eleObj.style.top = (rr.height - eleObj.clientHeight) / 2 + document.body.scrollTop;
}
/*获取客户端的高和宽*/
function getClientBounds() {
    var clientWidth; var clientHeight;
    //渲染方式BackCompat和CSS1Compat 判断文档是否加了标准声明 
    //浏览器客户区高度、滚动条高度、滚动条的Left、滚动条的Top等等都是上面的情况。
    clientWidth = document.compatMode == "CSS1Compat" ? document.documentElement.clientWidth : document.body.clientWidth;
    clientHeight = document.compatMode == "CSS1Compat" ? document.documentElement.clientHeight : document.body.clientHeight;
    return { width: clientWidth, height: clientHeight };
}

/*动态设置控件的高和宽*/
function divCenter_Dt(eleId) { //主要这三个属性document.body.clidentWidth  document.body.offsettWidth  document.body.scrollWidth  和document.body.scrollTop
    var eleObj = document.getElementById(eleId);
    eleObj.style.display = 'block';

    var scrolltop = window.pageYOffset || document.documentElement.scrollTop || document.body.scrollTop || 0;
    var _clientheight = 0;
    _clientheight = Math.min(document.body.clientHeight, document.documentElement.clientHeight);
    if (_clientheight == 0) {
        _clientheight = Math.max(document.body.clientHeight, document.documentElement.clientHeight);
    }
    var _clientwidth = document.documentElement.clientWidth || document.body.clientWidth;
    //整个页面的高度
    var _pageheight = Math.max(document.body.scrollHeight, document.documentElement.scrollHeight);
    //    alert("控件宽度:" + eleObj.clientWidth + "控件高度:" + eleObj.clientHeight);
    //    alert("_pageheight:" + _pageheight + "_clientheight:" + _clientheight + "_clientwidth:" + _clientwidth);
    eleObj.style.left = (_clientwidth - eleObj.clientWidth) / 2 + "px";
    eleObj.style.top = (scrolltop + (_clientheight - eleObj.clientHeight) / 2) + "px";
}

//检查文件类别
function checkData(fileName, qb) {
    if (fileName == '')
        return;

    //检查文件类别
    var exName = fileName.substr(fileName.lastIndexOf('.') + 1).toUpperCase();
    if (exName == 'JPG') {
        if (qb != 3) {
            document.getElementById("FileUpload" + (qb + 1)).style.display = "";
        }
    }
    else {
        alert("文件格式不正确，请重新选择！");
    }
}


//解决弹出的窗口window.open会被浏览器阻止的问题（自定义open方法）
function openwin(url) {
    var a = document.createElement("a");
    a.setAttribute("href", url);
    a.setAttribute("target", "_blank");
    a.setAttribute("id", "openwin");
    document.body.appendChild(a);
    a.click();
}

//该函数的功能用来阻止事件执行．并兼容多浏览器
function stopDefault(e) {
    if (e && e.stopPropagation) {//如果传入了事件对象.那么就是非IE浏览器
        e.preventDefault();
    }
    else {//IE浏览器
        window.event.returnValue = false;
    }
}

//该函数的功能用来阻止事件冒泡．并兼容多浏览器
function stopBubble(e) {
    //如果传入了事件对象.那么就是非IE浏览器
    if (e && e.stopPropagation) {
        e.stopPropagation();
    }
    else {//IE浏览器
        window.event.cancelBubble = true;
    }
}



//判断浏览器的类别
function IsIE() {
    var Sys = {};
    var ua = navigator.userAgent.toLowerCase();
    var s;
    (s = ua.match(/msie ([\d.]+)/)) ? Sys.ie = s[1] :
        (s = ua.match(/firefox\/([\d.]+)/)) ? Sys.firefox = s[1] :
        (s = ua.match(/chrome\/([\d.]+)/)) ? Sys.chrome = s[1] :
        (s = ua.match(/opera.([\d.]+)/)) ? Sys.opera = s[1] :
        (s = ua.match(/version\/([\d.]+).*safari/)) ? Sys.safari = s[1] : 0;
    //以下进行测试
    if (Sys.ie) document.write('IE: ' + Sys.ie);
    if (Sys.firefox) document.write('Firefox: ' + Sys.firefox);
    if (Sys.chrome) document.write('Chrome: ' + Sys.chrome);
    if (Sys.opera) document.write('Opera: ' + Sys.opera);
    if (Sys.safari) document.write('Safari: ' + Sys.safari);
}


//用法:DigitInput(this.event);
function DigitInput(ev) {
    //8：退格键、46：delete、37-40： 方向键
    //48-57：小键盘区的数字、96-105：主键盘区的数字
    //110、190：小键盘区和主键盘区的小数
    //189、109：小键盘区和主键盘区的负号
    var event = ev || window.event;                             //IE、FF下获取事件对象
    var currentKey = event.charCode || event.keyCode;             //IE、FF下获取键盘码
    if (currentKey != 8 && currentKey != 46 && (currentKey < 37 || currentKey > 40) && (currentKey < 48 || currentKey > 57) && (currentKey < 96 || currentKey > 105)) {
        if (window.event)                       //IE
        { event.returnValue = false; }        //e.returnValue = false;效果相同.
        else                                    //Firefox
        { event.preventDefault(); }
    }
    else {
        setTimeout(SetBond, 500);
    }
}



 //****
 //选择所有CheckBox
 //****
 function SelectAllForForm() {
     var ctllist = window.document.form1.getElementsByTagName("input");
     var objcheckbox = document.form1.CheckBox2;
     //objcheckbox = document.getElementById("CheckBox2");
     for (var i = 0; i < ctllist.length; i++) {
         if (ctllist[i].type == "checkbox") {
             if (ctllist[i].name.indexOf("CheckBox1") >= 0) {
                 if (objcheckbox.checked == true) {
                     ctllist[i].checked = true;
                 } else {
                     ctllist[i].checked = false;
                 }
             }
         }
     }
 }

 //****
 //判断是否选择一个元素CheckBox
 //****
 function IsCheckbox() {
     var ctllist = document.getElementsByTagName("input");
     var chec = false;
     for (var i = 0; i < ctllist.length; i++) {
         if (ctllist[i].type == "checkbox" && ctllist[i].name.indexOf("CheckBox1") >= 0) {
             if (ctllist[i].checked == true) {
                 chec = true;
             }
         }
     }
     if (chec == false) {
         alert("请选择需要删除的信息!");
         return false;
     } else {
         return confirm("确定要批量删除选中的信息吗？");
     }
 }

 //****
 //永远只选择一个CheckBox
 //****
 function SelectOnlyOne(index) {
     alert(index);
     var ctllist = window.document.getElementsByTagName("input");
     var objcheckbox = document.getElementById("CheckBox2");
     alert(ctllist.length);
     for (var i = 0; i < ctllist.length; i++) {
         if (ctllist[i].type == "checkbox" && ctllist[i].name.indexOf("CheckBox1") >= 0) {
             if (i == index) {
                 alert(i);
                 ctllist[i].checked = true;
             }
             else {
                 ctllist[i].checked = false;
             }
         }
     }
 }




 /*
 获取服务器时间
 */
 function getServerTime() {
     var xmlHttp = false;
     try {
         xmlHttp = new ActiveXObject("Msxml2.XMLHTTP");
     } catch (e) {
         try {
             xmlHttp = new ActiveXObject("Microsoft.XMLHTTP");
         } catch (e2) {
             xmlHttp = false;
         }
     }

     if (!xmlHttp && typeof XMLHttpRequest != 'undefined') {
         xmlHttp = new XMLHttpRequest();
     }

     xmlHttp.open("GET", "null.txt", false);
     xmlHttp.setRequestHeader("Range", "bytes=-1");
     xmlHttp.send(null);

     severtime = new Date(xmlHttp.getResponseHeader("Date"));
     return severtime.getTime();
 }


 //文本框为空 提示语
 function TextTipTool(inputName, TiShiTxt) {
     if ($("#" + inputName).val() == "") {
         $("#" + inputName).val(TiShiTxt).css("color", "#949494");
     }
     $("#" + inputName).blur(function () {
         if ($(this).val() == "") {
             $(this).val(TiShiTxt);
             $(this).css("color", "#949494");
         }
     });
     $("#" + inputName).focusin(function () {
         if ($(this).val() == TiShiTxt) {
             $(this).val("");
         }
         $(this).css("color", "");
     });
 }

 // document.onkeydown = function (e) {
 //    // 兼容FF和IE和Opera  
 //    var theEvent = e || window.event;
 //    var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
 //    var activeElementId = document.activeElement.id; //当前处于焦点的元素的id  
 //    if (code == 13 && activeElementId == "input_id") {
 //        doSomething(); //要触发的方法  
 //        return false;
 //    }
 //    return true;
 //}  

 //监听回车事件
 function keyDownSearch(e) {
     // 兼容FF和IE和Opera  
     var theEvent = e || window.event;
     var code = theEvent.keyCode || theEvent.which || theEvent.charCode;
     if (code == 13) {
         doSomething(); //具体处理函数  
         return false;
     }
     return true;
 }


