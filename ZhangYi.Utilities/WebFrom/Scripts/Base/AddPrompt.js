
(function ($) {
    $.fn.val2 = $.fn.val;
    $.fn.emptyValue = function (arg) {
        this.each(function () {
            var input = $(this);
            var options = arg;
            if (typeof options == "string") {
                options = { empty: options }
            }
            options = jQuery.extend({
                empty: input.attr("data-empty") || "",
                className: "gray"
            }, options);
            input.attr("data-empty", options.empty);
            input.focus(function () {
                $(this).removeClass(options.className);
                if ($(this).val2() == options.empty) {
                    $(this).val2("");
                }
                $(this).css("color", "");
            })
            input.blur(function () {
                if ($(this).val2() == "") {
                    $(this).val2(options.empty);
                    $(this).css("color", "#949494");
                }
                $(this).addClass(options.className);
            });
            if (input.val() == "") {
                input.val2(options.empty);
                input.css("color", "#949494");
            }

        });
    };
    //重写jquery val方法，增加data-empty过滤
    $.fn.val = function () {
        var value = $(this).val2.apply(this, arguments);
        var empty = $(this).attr("data-empty");
        if (empty == value) {
            $(this).val("");
            return "";
        }
        return value;
    };
})(jQuery);

//页面初始化注册
$(function () {
    jQuery("input").emptyValue();
    jQuery("textarea").emptyValue();
});

//使用方法:在为空提示的控件（目前只支持input和textarea）加入属性data-empty
//例如:   <input id="Title" data-empty="字数限制在20个字以内，可为空"/>