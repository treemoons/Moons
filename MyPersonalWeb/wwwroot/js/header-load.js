function isIE() { //ie?
    if (!!window.ActiveXObject || "ActiveXObject" in window) {
        return true;
    } else {
        return false;
    }
}
if (isIE()) {
    alert('NOT SUPPORT IE!')
    close();
}
/**
 * 用于附加在windowResize事件上的函数数组
 */
var windowResize = [];
var windowOnload = [];

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