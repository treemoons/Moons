
/**
 * 用于附加在windowResize事件上的函数数组
 */
var windowResize = [];
var windowOnload = [];

/**
 * 判断菜单显示
 */
var isMenuShow = false;
/**
 * 判断是都登录成功
 */
var isUserOptionsShow = false;

/**push login window */
window.onresize = e => {
    if (this.windowResize != null) {
        this.windowResize.forEach(item => {
            try { item(); } catch (error) { console.log(error); }
        });
    }
}

window.onload = e => {
    if (this.windowOnload != null) {
        this.windowOnload.forEach(item => {
            try { item(); } catch (error) { console.log(error); }
        });
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

String.prototype.write = function () {
    document.write(this);
};
String.prototype.getEleById = function () {
    return document.getElementById(this);
};
/**
 * 扩展--URI转换成string
 */
String.prototype.decodeUriString = function () {
    let result;
    if (this == '' || this == undefined || this == null) return this;
    try {
        result = decodeURI(this);
    } catch (error) {
        return this;
    }
    return result;
};
/**
 * 在指定格式(?)[key]=[value][splitMark]中，根据key找到value值，若没找到，返回空
 * @param {string} name search keywords
 * @param {string} purposeString results pool
 */
function getQueryString(name, purposeString, splitMark) {
    var reg = RegExp(`${name}=([^${splitMark}]+)`);
    var arr = purposeString.match(reg);
    if (arr) {
        return arr[1];
    } else {
        return '';
    }
}


/**
 * 写入Cookie到浏览器
 * @param {string} name key
 * @param {string} value value
 * @param {Int32Array} day date
 */
function setCookie(name, value, day) {
    var date = new Date();
    date.setDate(date.getDate() + day);
    document.cookie = `${name}=${value};expires=${date}`;
}


/**  获取cookie
 *  @param {string} name key
*/
function getCookie(name) {
    return getQueryString(name, document.cookie, ';')
}


/**  删除cookie
 *  @param {string} name key
*/
function delCookie(name) {
    setCookie(name, null, -1);
}


/**
 * 
 * @param {HTMLElement} input input where type is text
 * @param {Function} action when press enter key , do this function
 */
function pressEnter(input, action =
    /**@param {HTMLElement} form*/
    form => form.submit.click()) {
    input.onkeypress = e => {
        if (e.keyCode == 13) {
            action(form);
        }
    };
}
/**
 * 
 * @param {HTMLElement} input input where type is text
 * @param {RegExp} parttern when press enter key , do this function
 * @param {Function} action default is submit param{input} to target form
 */
function press(input, parttern, action) {
    input.onkeypress = e => {
        if (e.keyCode == 13) {
            if (parttern.test(input.value))
                action(form);
        }
    };
}


/**
 * 获取并操作Ajax数据
 *@param { { url: string, success: (text:string)=>void), failed ?: 
        (text:string)=>void, data ?: string, method ?: string, httptype ?:string } object  options
 */
function getAjaxData({ url, success, failed = error => { console.log(`error of failed data : ${error}`); }, data = '', method = 'POST', httptype = 'application/x-www-form-urlencoded' }) {
    debugger;
    // open(url,'_blank')
    var ajax = new XMLHttpRequest();
    ajax.open(method, url);
    ajax.setRequestHeader('Content-Type', httptype);
    ajax.send(data);
    ajax.onreadystatechange = function () {
        if (ajax.readyState == 4 && ajax.status == 200) {
            try {
                success(ajax.responseText);
            } catch (error) {
                console.log(`error of success data : ${error}`);
            }
        }
        else {
            failed(ajax.responseText);
        }
    }
}