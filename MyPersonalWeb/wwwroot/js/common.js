export * from './common/dom.js';
Date.prototype.formatDate =  function (fmt) {
    let o = {
        "M+": this.getMonth() + 1, //月份           
        "d+": this.getDate(), //日           
        "h+": this.getHours() % 12 == 0 ? 12 : this.getHours() % 12, //小时           
        "H+": this.getHours(), //小时           
        "m+": this.getMinutes(), //分           
        "s+": this.getSeconds(), //秒           
        "q+": Math.floor((this.getMonth() + 3) / 3), //季度           
        "f": this.getMilliseconds() //毫秒           
    };
    let week = {
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
    for (let k in o) {
        if (new RegExp("(" + k + ")").test(fmt)) {
            fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        }
    }
    return fmt;
}

/**
 * 扩展--URI转换成string
 */
String.prototype.decodeUriString = function () {
    let result;
    if (!this) return this;
    try {
        result = decodeURI(this); debugger
    } catch (error) {
        return this;
    }
    return result;
};

/**
 * 获取并操作Ajax数据
 *@param { { url: string, success: (text:string)=>Promise<void>), failed ?: 
        (text:string)=>void, data ?: string, method ?: 'POST'|'GET'|'DELETE'|'PUT'|'OPTIONS'|'TRACE',
         httpheader ?:{"Content-Type"?:['application/x-www-form-urlencoded'|
         'multipart/form-data'|'text/plain'|
         'audio/mpeg'|'video/mpeg'|'image/pipeg'|
         'image/jpeg'|'image/x-icon']|'application/x-www-form-urlencoded'|
         'multipart/form-data'|'text/plain'|
         'audio/mpeg'|'video/mpeg'|'image/pipeg'|
         'image/jpeg'|'image/x-icon',"Set-Cookie"?:string} } object  options
 */
export async function getAjaxData({
    url,
    success,
    failed = error => {
        console.log(`error of failed data : ${error}`);
    },
    data = '',
    method = 'POST',
    httpheader = { 'Content-Type': 'application/x-www-form-urlencoded' }
}) {
    // open(url,'_blank')
    let ajax = new XMLHttpRequest();
    ajax.open(method, url);
    for (let key in httpheader) {
        if ((typeof httpheader[key]) == 'string')
            ajax.setRequestHeader(key, httpheader[key]);
        else {
            try {
                let values = '';
                httpheader[key].forEach(value => {
                    values += value + ';';
                });
                values = values.substr(0, values.length - 1);
                ajax.setRequestHeader(key, values);
            } catch { console.error('err:isn`t array') }
        }
    }
    ajax.send(data);
    ajax.onreadystatechange = async function () {
        if (ajax.readyState == 4) {
            if (ajax.status == 200) {
                try {
                    success(ajax.responseText);
                } catch (error) {
                    console.log(`error of success data : ${error}`);
                }
            } else {
                failed(ajax.responseText);
            }
        }
    }
}


/**
 * 
 * @param {HTMLInputElement} input input where type is text
 * @param {(form:HTMLFormElement)=>void} action when press enter key , do this async function
 */
export async function pressEnter(input, action //=
    /**@param {HTMLElement} form*/
    // form => form.submit.click()
) {
    input.onkeypress = e => {
        debugger;
        console.log('key')
        if (e.key == 'Enter') {
            //action();
            console.log('enter')
        }
    };
}

/**
 * 显示等待动画，配合等待div元素
 * @param {'block'|'none'|flex} display
 */
export async function waitDot(display) {
    let wait = document.getElementById('wait');
    if (!wait) return;
    wait.style = `display:${display}`;
}
/** scroll to the top  */
export async function totop() {
    let a = document.scrollingElement.scrollTop;
    top.scrollTo(0, a - 2);
    for (let i = a - 2; i >= 0; i -= 2) {
        setTimeout(() => {
            top.scrollTo(0, i);
        }, 1);
    }
}

/**
 * 在指定格式(?)[key]=[value][splitMark]中，根据key找到value值，若没找到，返回空
 * @param {string} name search keywords
 * @param {string} purposeString results pool
 * @param {'&'|';'} splitMark
 */
export async function getQueryString(name, purposeString, splitMark = '&') {
    let reg = RegExp(`(?:${splitMark}|\\?|^)${name}=([^${splitMark}]+)`);
    reg.test(purposeString)
    return RegExp.$1;

}

!function () {
    let a = 'a=hhhhhwww.ss.com/ss?caa=aaaa&aa=bbb&c=ccc';
    console.log(getQueryString('c', a, '&') )
}()
// console.log(getQueryString('aa',a,'&'))



/**
 * 
 * @param {string} name
 * @param {string} value
 * @param {{year:number, month:number, day:number, hour:number,minutes:number,seconds:number, milliseconds:number }} param2 date default now (all values is '0')
 * @param {string} path 
 * @param {true|false} httponly 
 */
export async function buildCookie(name, value,
    { year = 0, month = 0, day = 0, hours = 0, minutes = 0, seconds = 0, milliseconds = 0 } = {},
    path = undefined, httponly = false) {
    let date;
    if (arguments[2]) {
        date =await getSpanDate({ year: year, month: month, day: day, hours: hours, minutes: minutes, seconds: seconds, milliseconds: milliseconds });
    }
    let cookie = `${name}=${value};${(arguments[2] ? `expires=${date.toUTCString()};` : '')}${(path ? `path = ${path};` : '')}${(httponly ? 'httponly' : '')}`;
    
    return encodeURI(cookie);
}

/**
 * date default now (all values are '0')
 * @param {{ year:number, month:number, day:number,hours:number , minutes:number,seconds:number, milliseconds:number }}  
 */
export async function getSpanDate(
    { years = 0, months = 0, days = 0, hours = 0, minutes = 0, seconds = 0, milliseconds = 0 } = {}) {
    let date = new Date();
    if (years)
        date.setFullYear(date.getFullYear() + years);
    if (months)
        date.setMonth(date.getMonth() + months);
    if (days)
        date.setDate(date.getDate() + days);
    if (hours)
        date.setHours(date.getHours() + hours);
    if (minutes)
        date.setMinutes(date.getMinutes() + minutes);
    if (seconds)
        date.setSeconds(date.getSeconds() + seconds);
    if (milliseconds)
        date.setMilliseconds(date.getMilliseconds() + milliseconds);
    return date;
}

/**当天获取凌晨时间 */
export async function getTodayDawn() {
    let date = new Date();
    date.setHours(0);
    date.setMinutes(0);
    date.setSeconds(0);
    date.setMilliseconds(0);
    return date;
}

/**
 * expires按天写入Cookie到浏览器
 * @param {string} name key
 * @param {string} value value
 * @param {Int32Array} day date
 */
export async function setCookie(name, value, day) {
    let date = new Date();
    date.setDate(date.getDate() + day);
    document.cookie = `${name}=${value};expires=${date}`;
}

setCookie('test', 'testtest', 20)

/**  获取cookie
 *  @param {string} name key
*/
export async function getCookie(name) {
    return await getQueryString(name, document.cookie, ';')
}


/**  删除cookie
 *  @param {string} name key
*/
export async function delCookie(name) {
    setCookie(name, null, -1);
}

!async function a(aa, bb, { a, b } = {}) {
    console.log(arguments[2])
}(1, 2)