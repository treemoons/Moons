
/**
 * 用于附加在windowResize事件上的函数数组
 */
var windowResize = [];
var windowOnload = [];

var isMenuShow = false;
var isUserOptionsShow = false;
/**push login window */
window.onresize = e=> {
    if (this.windowResize != null) {
        this.windowResize.forEach(item => {
            try { item(); } catch (error) { console.log(error); }
        });
    }
}

window.onload = e=> {
    if (this.windowOnload != null) {
        this.windowOnload.forEach(item => {
            try { item(); } catch(error){console.log(error); }
        });
    }
}

document.onreadystatechange = e => {
    let process;
    try {
        process = document.getElementById('processbar');
    } catch (error) {
        console.log(error); 
        return;
    }
    switch (document.readyState) {
        case "uninitialized":
            process.style.left='-75%'
            break;
        case "loading":
            process.style.left = '-50%'
            break;
        case "interactive":
            process.style.left = '-25%'
            break;
        case "complete":
            process.style.left = '0%'
            setTimeout(() => {
                process.style.opacity = '0';
            }, 10);
            break;
        default:
            break;
    }
}
/**
 * formating datetime
 */
Date.prototype.formatDate = function (fmt) {
    var o = {
        "M+": this.getMonth() + 1, //月份           
        "d+": this.getDate(), //日           
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时           
        "H+": this.getHours(), //小时           
        "m+": this.getMinutes(), //分           
        "s+": this.getSeconds(), //秒           
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度           
        "f": this.getMilliseconds() //毫秒           
    };
    var week = {
        "0": "\u65e5",
        "1": "\u4e00",
        "2": "\u4e8c",
        "3": "\u4e09",
        "4": "\u56db",
        "5": "\u4e94",
        "6": "\u516d"
    };
    if (/(y+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
    }
    if (/(E+)/.test(fmt)) {
        fmt = fmt.replace(RegExp.$1, ((RegExp.$1.length > 1) ? (RegExp.$1.length > 2 ? "\u661f\u671f" : "\u5468") : "") + week[this.getDay() + ""]);
    }
    for (var k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}
