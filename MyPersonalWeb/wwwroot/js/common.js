export default {
    /**
     * @param {string} text
     * @param {'infomation'|'warning'|'error'} warningtype
     * @param {'YesNoCancel'|'YesNo'|'Yes'} messageButton
     * @param {{yes:(args:any)=>void,no:(args:any)=>void,cancel:()=>void,args:string|number|{}|[]}} buttonsCallback
     * @param {number} timeout unit: ms
     */
    messagebox: async function (text, warningtype, messageButton, { yes = d => { }, no = d => { }, cancel = d => { }, args = undefined } = {}, timeout = undefined) {
        //#region element
        debugger;
        if (!document.getElementById('tipBackground_of_messagebox')) {
            let tipBackground = document.createElement('div');
            tipBackground.id = 'tipBackground_of_messagebox';
            let tip = document.createElement('div');
            let contentWindow = document.createElement('article')
            let tittle = document.createElement('h3');
            let warningicon = document.createElement('b');
            let warningtip = document.createElement('span')
            let content = document.createElement('p');
            let buttons = document.createElement('div');
            let btnyes = document.createElement('button');
            let btnno, btncancel;
            tipBackground.appendChild(tip);
            tip.appendChild(contentWindow);
            contentWindow.append(tittle, content, buttons);
            tittle.append(warningicon, warningtip);
            warningicon.innerText = '!';
            btnyes.innerText = 'Yes';
            switch (messageButton) {
                case 'YesNoCancel':
                    btnno = document.createElement('button');
                    btncancel = document.createElement('button');
                    btnno.innerText = 'No'
                    btncancel.innerText = 'Cancel'
                    buttons.append(btnyes, btnno, btncancel);

                    break;
                case 'YesNo':
                    btnno = document.createElement('button');
                    btnno.innerText = 'No'
                    buttons.append(btnyes, btnno);
                    break;
                case 'Yes':
                default:
                    buttons.appendChild(btnyes);
                    break;
            }
            //#endregion

            //#region  style
            tipBackground.style = `
        position: fixed;
        z-index: 0;
        height: 100vh;
        display: flex;
        width: 0;
        justify-content: center;
        align-items: center;`;
            tipBackground.onclick = tipBackgroundFlash;
            tip.style = `
                bottom:15%;
        box-shadow: 1px 2px 7px 0px gainsboro;
        position:relative;
        z-index: 999;
        max-width: 70%;
        overflow: hidden;
        width: auto;
        min-width:200px;
        border-radius: 5px;
        transition: all linear 100ms;
        -webkit-transition: all linear 100ms;
        -moz-transition: all linear 100ms;
        -ms-transition: all linear 100ms;
        -o-transition: all linear 100ms;
        -webkit-border-radius: 5px;
        -moz-border-radius: 5px;
        -ms-border-radius: 5px;
        -o-border-radius: 5px;
        background-color:#ffffffbb`;
            contentWindow.style = ` 
       position: relative;
        left:0;
        opacity:0;
        margin: 10px;
        margin-top: 0;
        max-width: 100%;
        min-width: 200px;
        word-wrap: break-word;
        padding: 0;
        margin:0;
        transition: all linear 100ms;
        -webkit-transition: all linear 100ms;
        -moz-transition: all linear 100ms;
        -ms-transition: all linear 100ms;
        -o-transition: all linear 100ms;`;
            tittle.style = `
        background-color: rgb(248, 211, 211);
        padding: 5px 20px;
        margin-block-start: 0;`;
            warningicon.style = `
        display: inline-block;
        text-align: center;
        color: white;
        width:25px;
        border-radius: 50%;
        -webkit-border-radius: 50%;
        -moz-border-radius: 50%;
        -ms-border-radius: 50%;
        -o-border-radius: 50%;`;
            warningtip.style = `margin-left: 10px`;
            content.style = `
        text-align: center;
        width: 80%;
        margin: 2% auto;`;
            buttons.style = `
        text-align: right;
        display: flex;
        padding-bottom: 20px;
        justify-content: space-around;`;
            btnyes.style = `
        outline: none;
        min-width: 50px;
        color: white;
        border: none;
        /* box-shadow: 1px 2px 4px 0px hsl(0, 0%, 86%); */
        background: #26bd76;
        padding:5px 10px;`;
            btnyes.onmouseenter = e => btnyes.style.boxShadow = '1px 2px 4px 2px hsl(0, 0 %, 86 %)';
            btnyes.onmouseleave = e => btnyes.style.boxShadow = 'none';
            btnyes.onclick = yesClick;
            if (btnno) {
                btnno.style = `
            outline: none;
            min-width: 50px;
            border: none;
            color: white;
            /* box-shadow: 1px 2px 4px 0px #f8f8f8; */
            background: #e65936;
            padding:5px 10px;`;
                btnno.onmouseenter = e => btnno.style.boxShadow = '1px 2px 4px 2px hsl(0, 0 %, 86 %)';
                btnno.onmouseleave = e => btnno.style.boxShadow = 'none';
                btnno.onclick = noClick;
            }
            if (btncancel) {
                btncancel.style = `
            outline: none;
            min-width: 50px;
            border: none;
            color: gary;
            /* box-shadow: 1px 2px 4px 0px #f8f8f8; */
            background: #efefef;
            padding:5px 10px;`;
                btncancel.onmouseenter = e => btncancel.style.boxShadow = '1px 2px 4px 2px hsl(0, 0 %, 86 %)';
                btncancel.onmouseleave = e => btncancel.style.boxShadow = 'none';
                btncancel.onclick = cancelClick;
            }
            //#endregion

            //#region warning style
            switch (warningtype) {
                case 'warning':
                    warningicon.style.backgroundColor = 'yellow';
                    warningtip.innerText = '警 告';
                    break;
                case 'error':
                    warningicon.style.backgroundColor = 'red';
                    warningtip.innerText = '错 误';
                    break;
                case 'infomation':
                default:
                    warningicon.style.backgroundColor = 'green';
                    warningtip.innerText = '提 示';
                    break;
            }
            //#endregion
            content.innerText = text;
            if (parseInt(timeout)) {

                let warningtimeout = document.createElement('span');
                tittle.append(warningtimeout);
                warningtimeout.style = `
                        position: absolute;
                        right: 10px;
                        display:inline-block;`;
                let count = timeout;
                warningtimeout.innerText = count;
                let timeInteral = setInterval(() => {
                    warningtimeout.innerText = --count;
                    if (count <= 0) {
                        clearInterval(timeInteral);
                        close();
                    }
                }, 1000);
            }

            //#region onclick 事件
            function yesClick() {
                yes(args);
                close();
            }
            function noClick() {
                no(args);
                close();
            }
            function cancelClick() {
                cancel();
                close();
            }

            //#endregion

            //#region show and close

            function show() {
                document.body.prepend(tipBackground);
                tipBackground.style.width = '100%';
                tip.style.width = 'auto';
                tip.style.height = 'auto';
                let width = getComputedStyle(tip).width
                let height = getComputedStyle(tip).height
                tip.style.width = '0'
                tip.style.height = '0'
                setTimeout(() => {
                    tip.style.width = width;
                    tip.style.height = height;
                    contentWindow.style.opacity = '1'
                    setTimeout(() => {
                        tip.style.height = 'auto';
                        tip.style.width = 'auto';
                    }, 100);
                }, 100);
            }
            function close() {
                let width = tip.style.width = getComputedStyle(tip).width;
                tip.style.height = getComputedStyle(tip).height;
                setTimeout(() => {
                    tip.style.width = '0';
                    tip.style.height = '0';
                    contentWindow.style.opacity = '0'
                    setTimeout(() => {
                        tip.style.minWidth = '0';
                        if (tipBackground.parentElement) {
                            document.body.removeChild(tipBackground);
                        }
                    }, 100);
                }, 100);
            }
            //#endregion
            //阻止冒泡
            tip.onclick = function (e) {
                e.stopPropagation()
            }
            function tipBackgroundFlash(e) {
                flash(2);
            }
            function flash(times) {
                tip.style.boxShadow = '1px 2px 7px 7px gainsboro';

                setTimeout(() => {
                    tip.style.boxShadow = '1px 2px 7px 0px gainsboro';
                    if (times > 1)
                        setTimeout(() => {
                            flash(--times);
                        }, 200);
                }, 200);
            }
            show();

        }
    },


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
    getAjaxData: async function ({
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
    },


    /**
     * 
     * @param {HTMLInputElement} input input where type is text
     * @param {(form:HTMLFormElement)=>void} action when press enter key , do this function
     */
    pressEnter: async function (input, action //=
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
    },

    /**
     * 显示等待动画，配合等待div元素
     * @param {'block'|'none'|flex} display
     */
    waitDot: async function (display) {
        let wait = document.getElementById('wait');
        if (!wait) return;
        wait.style = `display:${display}`;
    },
/** scroll to the top  */
    totop: function () {
        let a = document.scrollingElement.scrollTop;
        top.scrollTo(0, a - 2);
        for (let i = a - 2; i >= 0; i -= 2) {
            setTimeout(() => {
                top.scrollTo(0, i);
            }, 1);
        }
    }

}


/**
 * formating datetime
 */
Date.prototype.formatDate = function (fmt) {
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
 * 在指定格式(?)[key]=[value][splitMark]中，根据key找到value值，若没找到，返回空
 * @param {string} name search keywords
 * @param {string} purposeString results pool
 * @param {'&'|';'} splitMark
 */
export function getQueryString(name, purposeString, splitMark = '&') {
    let reg = RegExp(`(?:${splitMark}|\\?|^)${name}=([^${splitMark}]+)`);
    reg.test(purposeString)
    return RegExp.$1;

}
let a = 'a=hhhhhwww.ss.com/ss?caa=aaaa&aa=bbb&c=ccc';
console.log(getQueryString('test', a, ';') == '')
// console.log(getQueryString('aa',a,'&'))



/**
 * 
 * @param {string} name
 * @param {string} value
 * @param {{year:number, month:number, day:number, hour:number,minutes:number,seconds:number, milliseconds:number }} param2 date default now (all values is '0')
 * @param {string} path 
 * @param {true|false} httponly 
 */
export function buildCookie(name, value,
    { year = 0, month = 0, day = 0, hours = 0, minutes = 0, seconds = 0, milliseconds = 0 } = {},
    path = undefined, httponly = false) {
    let date;
    if (arguments[2]) {
        date = getSpanDate({ year: year, month: month, day: day, hours: hours, minutes: minutes, seconds: seconds, milliseconds: milliseconds });
    }
    let cookie = `${name}=${value};${(arguments[2] ? `expires=${date};` : '')}${(path ? `path = ${path};` : '')}${(httponly ? 'httponly' : '')}`;
    console.log(encodeURI(cookie));
    return encodeURI(cookie);
}

/**
 * date default now (all values are '0')
 * @param {{ year:number, month:number, day:number,hours:number , minutes:number,seconds:number, milliseconds:number }}  
 */
export function getSpanDate(
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
export function getTodayDawn() {
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
export function setCookie(name, value, day) {
    let date = new Date();
    date.setDate(date.getDate() + day);
    document.cookie = `${name}=${value};expires=${date}`;
}

setCookie('test', 'testtest', 20)

/**  获取cookie
 *  @param {string} name key
*/
export function getCookie(name) {
    return getQueryString(name, document.cookie, ';')
}


/**  删除cookie
 *  @param {string} name key
*/
export function delCookie(name) {
    setCookie(name, null, -1);
}

!function a(aa, bb, { a, b } = {}) {
    console.log(arguments[2])
}(1, 2)