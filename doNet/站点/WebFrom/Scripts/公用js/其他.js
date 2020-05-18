//===========================jquery=========================
//●string转JSON：
var data = $.parseJSON(str); 
var data = JSON.parse(result.data)
//JSON转string：
JSON.stringify(jsonObj);
//jquery.json.js专用JSON转换js：转换成json字符串：
var strjson = $.toJSON(obj);

//●遍历控件
$(this).serialize();     //输出：a=1&b=2&c=3&d=4&e=5
//遍历控件
$(this).serializeArray()    //输出：[{name: a,value: 1},{name: b, value: 2}]
//遍历控件:将form表单元素的值序列化成对象
$(this).serializeObject()    //输出：{name: '111',emal: '222'}
//●合并参数
var params = {
    "method": "EditCostBudget",
    "id": id
};
var options = sy.serializeObject($("#myForm").find(":input"));
params = $.extend(true, params, options);

//●查找最近祖先元素并删除：
$(当前对象).closest('查找').remove();

//●显示延迟关闭
$('.error_tips').show().delay(1500).fadeOut();

//●输入框改变事件，优于blur
 $('#TransactionPrice').bind('input porpertychange', function(){});

//●主动触发事件
 $('#TransactionPrice').trigger("porpertychange");
 
//●判断某个元素在数组中的位置
var arr = [4, "Pete", 8, "John"];
$.inArray(4, arr);//输出0
 
//●删除数组中的某个元素
 array.splice(0,1);
 
//●判断数组是否为null
 if (array === undefined || array.length == 0) 
 
//●如果该对象存在其他click事件先解绑事件
 $("[name='delete']").off("click");
 
//●只绑定一次事件
 $("[name='download']").one("click", function () 
 
//●主动触发事件
$('#name').trigger("porpertychange");


//===========================js=========================
setTimeout(function () {
    window.location.href = "/query/index";
}, 500);
setInterval(function () {
    window.location.href = "/query/index";
},500);