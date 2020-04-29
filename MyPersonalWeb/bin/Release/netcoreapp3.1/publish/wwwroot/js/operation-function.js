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
    loginform.username.focus();
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

/**
 * 获取并操作Ajax数据
 * @param {string} url 要提交到的地址
 * @param {Function} success 获取data成功之后要执行的函数
 * @param {Function} failed 获取data失败之后要执行的函数
 * @param {string} querystring 传递的参数 默认为空
 * @param {string} httptype 方法类型'post','get'等，默认'POST'
 * @param {string} datatype mime类型，默认为表单类型
 */
function getAjaxData({ url, success, failed = null, querystring = '', httptype = 'POST', datatype = 'application/x-www-form-urlencoded' }) {
    debugger;
    // open(url,'_blank')
    var ajax = new XMLHttpRequest();
    ajax.open(httptype, url);
    ajax.setRequestHeader('Content-Type', datatype);
    ajax.send(querystring);
    ajax.onreadystatechange = function () {
        if (ajax.readyState == 4 && ajax.status == 200) {
            success(ajax.responseText);
        }
        else {
            try {
                failed(ajax.responseText);
            } catch (error) {
                console.log(error);
            }
        }
    }
}


/**
 * 
 * @param {HTMLElement} input input where type is text
 * @param {Function} action when press enter key , do this function
 */
function pressEnter(input, action) {
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
    if (IsInputEmpty(loginform.username)) {
        loginform.username.focus();
        return false;
    } else if (IsInputEmpty(loginform.password)) {
        loginform.password.focus();
        return false;
    }
    waitLogin();
    getAjaxData({
        url: '/home/login',
        querystring:
            `username=${loginform.username.value}&` +
            `password=${loginform.password.value}&` +
            `isremembered=${loginform.isremembered.checked}`,
        success: data => {
            debugger;
            if (data == 'T') {
                // open(window.location.href, '_self')
                loginform.setAttribute('action',location.href);
                loginform.submit();
            } else {
                let loginerror = document.getElementById('loginerror');
                switch (data) {
                    case 'F':
                        loginerror.innerText = "账号或密码错误，请重新输入。"
                        break;
                    // case 'U':
                    //     loginerror.innerText = "账号超过，请重新输入。"
                    //     break;
                    // case 'P':
                    //     loginerror.innerText = "账号或密码错误，请重新输入。"
                    //     break;
                    default:
                        loginerror.innerText = "未知错误。"
                        break;
                }
            }
                waitLoginClose();
        }
    });
    return false;
}

/**
 * 
 * @param {string} name key
 * @param {string} value value
 * @param {Int32Array} day date
 */
function setCookie(name, value, day) {
    var date = new Date();
    date.setDate(date.getDate() + day);
    document.cookie = `${name}=${value};expires=${date}`;
};
/**  获取cookie*/
function getCookie(name) {
    var reg = RegExp(`${name}=([^;]+)`);
    var arr = document.cookie.match(reg);
    if (arr) {
        return arr[1];
    } else {
        return '';
    }
};
/**  删除cookie*/ 
function delCookie(name) {
    setCookie(name, null, -1);
};

/**
 * Specify the national language of HTML
 * @param {string} lang Abbreviation of language between all of world
 */
function changeLanguage(lang) {
    document.getElementsByTagName("html")[0].setAttribute("lang", lang);
    setCookie('lang',lang)
}
function loadlang() {
    let lang = getCookie('lang');
    if (lang == undefined || lang == '') {
        lang = 'en';
        setCookie('lang', lang)
    }
    document.getElementsByTagName("html")[0].setAttribute("lang", lang);
    console.log(lang);
}