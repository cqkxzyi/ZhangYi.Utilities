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

//●返回数组中指定元素的索引值
var arr = [4, "Pete", 8, "John"];
$.inArray(4, arr);//输出0

//●显示延迟关闭
$('.error_tips').show().delay(1500).fadeOut();





//===========================js=========================
setTimeout(function () {
    window.location.href = "/query/index";
}, 500);
setInterval(function () {
    window.location.href = "/query/index";
},500);