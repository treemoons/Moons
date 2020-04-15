
/**
 * 用于附加在windowResize事件上的函数数组
 */
var windowResize = [];
var windowOnload = [];

var isMenuShow = false;
var isUserOptionsShow = false;
/**push login window */
window.onresize = function () {
    if (this.windowResize != null) {
        this.windowResize.forEach(item => {
            item();
        });
    }
    // this.windowResize.a=function(){alert('msg');}
}

window.onload = function () {
    if (this.windowOnload != null) {
        this.windowOnload.forEach(item => {
            item();
        });
    }
}