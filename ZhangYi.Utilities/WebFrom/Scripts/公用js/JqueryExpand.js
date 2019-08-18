
//弹出层滚动禁止父窗口滚动
$.fn.extend({
    "preventScroll": function () {
        var _this = this.get(0);
        if ($.browser.mozilla) {
            _this.addEventListener('DOMMouseScroll', function (e) {
                _this.scrollTop += e.detail > 0 ? 60 : -60;
                e.preventDefault();
            }, false);
        } else {
            _this.onmousewheel = function (e) {
                e = e || window.event;
                _this.scrollTop += e.wheelDelta > 0 ? -60 : 60;
                return false;
            };
        }
        return this;
    }
});