


define(['jquery'], function ($) {
    var fun1 = function () {
        alert("我是fun1函数");
    };


    $(function () { alert("页面加载完毕，执行function"); })

    return {
        fun1:fun1
    }
});





//(function () {
//    function fun1() {
//        alert("fun1");
//    }

//    fun1();
//})()
