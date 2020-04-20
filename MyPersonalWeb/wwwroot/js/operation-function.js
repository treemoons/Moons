//#region  master context

//#region  用于调整设置登录弹窗的位置

/**
 * 获取登录login元素的div
 * @param {login} l login登录元素
 */
function getLoginLeft(l) {
    return 'left:' + (document.body.clientWidth - this.parseInt(window.getComputedStyle(l).width)) / 2 + 'px'
}
/**刷新login宽度 */
function resizeLogin() {
    let block = getComputedStyle(this.login, null).getPropertyValue('display') == 'block';
    let largesreen = document.body.clientWidth > 766;
    let bool = block && largesreen;
    loginform.username.value = bool;
    if (bool)
        login.style.left = (document.body.clientWidth - this.parseInt(window.getComputedStyle(this.login).width)) / 2 + 'px';
    else
        this.login.style.left = '0';
}
/** 点击空白，关闭login弹窗 */
function whiteBack() {
    loginClose();
    menuButton.style = 'opacity:0;top:-20%;'
    setTimeout(function () {
        menuButton.style.display = 'none';
    }, 400);
    document.getElementById('login-background').style.display = 'none';
    isMenuShow = true;
}
function showLogin() {
    debugger
    if (window.getComputedStyle(menuButton).display == 'block') {
        showAndCloseMenu();
    }
    login.style = 'display:block;' + this.getLoginLeft(this.login);
    setTimeout(function () {
        login.style = 'opacity:100;top: 5vh;' + this.getLoginLeft(this.login);
    }, 40);
    document.getElementById('login-background').style.display = 'block';
}

function loginClose() {
    login.style = 'opacity:0;top:-100%;' + this.getLoginLeft(this.login);
    let loginerror = document.getElementById('loginerror');
    loginerror.innerHTML = '&#160;';
    setTimeout(function () {
        login.style = 'display:none'
    }, 400);
    document.getElementById('login-background').style.display = 'none';
}
//#endregion
/**
 * 显示等待动画，配合等待div元素
 */
function waitLogin() {
    var wait = document.getElementById('wait');
    wait.style = 'display:block';
}
function waitLoginClose() {
    setTimeout(() => {
        var wait = document.getElementById('wait');
        wait.style = 'display:none';
    }, 400);
}
function showAndCloseMenu() {
    if (window.getComputedStyle(userOptions).display == 'block') {
        userOptions.style = 'opacity:0;right:-20vw;'
        setTimeout(function () {
            userOptions.style.display = 'none';
        }, 400);
        isUserOptionsShow = true;
    }
    if (isMenuShow) {
        menuButton.style = 'display:block;';
        setTimeout(function () {
            menuButton.style = 'opacity:100;top:55px;'
        }, 40);
        debugger;
        document.getElementById('login-background').style.display = 'block';
        isMenuShow = false;
    } else {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(function () {
            menuButton.style.display = 'none';
        }, 400);
        document.getElementById('login-background').style.display = 'none';
        isMenuShow = true;
    }
}
function showUserOptions() {
    debugger;
    if (window.getComputedStyle(menuButton).display == 'block') {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(function () {
            menuButton.style.display = 'none';
        }, 400);
        isMenuShow = true;
    }
    if (isUserOptionsShow) {
        userOptions.style = 'display:block';
        setTimeout(function () {
            userOptions.style = 'opacity:100;right:0;'
        }, 40);
        isUserOptionsShow = false;
    } else {
        userOptions.style = 'opacity:0;right:-20vw;'
        setTimeout(function () {
            userOptions.style.display = 'none';
        }, 400);
        isUserOptionsShow = true;
    }
}
/**
 * 返回首页
 */
function backIndex() {
    open("/HOME/INDEX", "_self");
}
//#endregion

function getAjaxData(url, action, querystring = '', httptype = 'POST', datatype = 'application/x-www-form-urlencoded') {
    debugger;
    // open(url,'_blank')
    var ajax = new XMLHttpRequest();
    ajax.open(httptype, url);
    ajax.setRequestHeader('Content-Type', datatype);
    ajax.send(querystring);
    ajax.onreadystatechange = function () {
        if (ajax.readyState == 4) {
            if (ajax.status == 200) {
                action(ajax.responseText);
            }
        }
    }
}
/**
 * 
 * @param {HTMLElement} input input where type is text
 * @param {Function} action when press enter key , do this function
 */
function pressEnter(input,action) {
    input.onkeypress = e => {
        debugger;
        if (e.keyCode == 13) {
            action(form);
        }
    };
}
/**
 * 
 * @param {HTMLElement} input  input where type is text and whose attributes have 'tip'
 */
function IsInputEmpty(input) {
    let error = document.getElementById('loginerror')
    if (input.value == '') {
        error.innerText = input.getAttribute('tip') + '不能为空';
        return true;
    }
    else {
        return false;
    }
}
 /**
  * login to Moons
  */
function signin() {
    if (IsInputEmpty(loginform.username) || IsInputEmpty(loginform.password)) {
        return false;
    }
    waitLogin();
    getAjaxData('/home/login', data => {
        debugger;
        if (data == 'T') {
            open(window.location.href, '_self')
        } else {
            let loginerror = document.getElementById('loginerror');
            switch (data) {
                case 'F':
                    loginerror.innerText = "账号或密码错误，请重新输入。"
                    break;
                case 'U':
                    loginerror.innerText = "账号超过，请重新输入。"
                    break;
                case 'P':
                    loginerror.innerText = "账号或密码错误，请重新输入。"
                    break;
                default:
                    break;
            }
            waitLoginClose();
        }
    }, `username=${loginform.username.value}&password=${loginform.password.value}&lastlogintime=${(new Date()).formatDate('yyyy-MM-dd HH:mm:ss')}`
    );
    return false;
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
