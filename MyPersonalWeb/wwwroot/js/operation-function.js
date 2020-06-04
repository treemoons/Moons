//#region  master context

//#region  用于调整设置登录弹窗的位置

/** 获取登录login元素的div
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
    isMenuShow = false;
}
function showLogin() {
    debugger
    if (isMenuShow) {
        showAndCloseMenu();
    }
    login.style = 'display:block;' + this.getLoginLeft(this.login);
    setTimeout(function () {
        login.style = 'opacity:100;top: 5vh;' + this.getLoginLeft(this.login);
    }, 40);
    document.getElementById('login-background').style.display = 'block';
    loginform.username.focus();
}
/**
 * 关闭登录窗口
 */
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

/**
 * 显示或这关闭menu
 */
function showAndCloseMenu() {
    try {
        if (window.getComputedStyle(userOptions).display == 'block') {
            userOptions.style = 'opacity:0;right:-20vw;'
            setTimeout(function () {
                userOptions.style.display = 'none';
            }, 400);
            isUserOptionsShow = false;
        }
    } catch (error) { console.log(error); }
    if (!isMenuShow) {
        menuButton.style = 'display:block;';
        setTimeout(function () {
            menuButton.style = 'opacity:100;top:54px;'
        }, 40);
        // document.getElementById('login-background').style.display='block'
        isMenuShow = true;
    } else {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(function () {
            menuButton.style.display = 'none';
        }, 400);
        isMenuShow = false;
    }
}
function closemenu() {
    let conponent = document.querySelector('.conponent');
    conponent.onclick = function () {
        if (isMenuShow) {
            showAndCloseMenu();
        }
    }
}


function showUserOptions() {
    debugger;
    if (window.getComputedStyle(menuButton).display == 'block') {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(function () {
            menuButton.style.display = 'none';
        }, 400);
        isMenuShow = false;
    }
    if (!isUserOptionsShow) {
        userOptions.style = 'display:block';
        setTimeout(function () {
            userOptions.style = 'opacity:100;right:0;'
        }, 40);
        isUserOptionsShow = true;
    } else {
        userOptions.style = 'opacity:0;right:-20vw;'
        setTimeout(function () {
            userOptions.style.display = 'none';
        }, 400);
        isUserOptionsShow = false;
    }
}
/**
 * 返回首页* 
 */
function backIndex() {
    open(`/${lang}/home/index`, "_self");
}

/**
 * 
 * @param {HTMLElement} input input where type is text
 * @param {Function} action when press enter key , do this function
 */
function pressEnter(input, action) {
    input.onkeypress = e => {
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
        url: `/${lang}/home/login`,
        querystring:
            `username=${loginform.username.value}&` +
            `password=${loginform.password.value}&` +
            `isremembered=${loginform.isremembered.checked}`,
        success: data => {
            debugger;
            loginClose();
            if (data == 'T') {
                // open(window.location.href, '_self')
                loginform.setAttribute('action', location.href);
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

//#region national lanuage
//#region  load and change display
/**
 * Specify the national language of HTML
 * @param {string} elelang Abbreviation of language between all of world
 * @param {Number} date date of expires
 */
function changeLanguage(elelang, date = 100) {
    setCookie('lang', elelang, date)
}
/**
 * 加载上一次
 */
function loadlang() {
    let elelang = getCookie('lang');
    if (elelang == undefined || elelang == '') {
        elelang = 'en';
    }
    setCookie('lang', elelang)
}
//#endregion
//#region  select language options
function showlang() {
    let option = document.getElementById('lang');
    document.getElementById('selected').style.fontWeight = 'bolder';
    if (option.style.display == 'block') {
        option.style.display = 'none';
    } else {
        option.style.opacity = '0';
        option.style.display = 'block';
        setInterval(() => {
            option.style.opacity = '100';
        }, 00);
    }
}
/**
 * 添加language选中一个显示之后的click事件
 */
function selectedlang() {
    let langselects = document.querySelectorAll('#lang option');
    langselects.forEach(option => {
        option.onclick = async function () {
            let selected = document.getElementById('selected');
            selected.innerText = this.innerText;
            document.getElementById('lang').style.display = 'none';
            selected.style.fontWeight = 'normal';
            changeLanguage(option.value);
            let querystring = location.href.substr(location.href.indexOf('?'));
            if (!querystring.startsWith('?')) {
                querystring = '';
            }
            open(`/${option.value}/${route}${querystring}`, '_self');

        }
    });
}
//#endregion

//#endregion

/**
 * 获取并操作Ajax数据
 *@param { { url: string, success: (text:string)=>void), failed ?: 
        (text:string)=>void, querystring ?: string, httptype ?: string, datatype ?:string } object  options
 */
function getAjaxData({ url, success, failed = error => { console.log(error); }, querystring = '', httptype = 'POST', datatype = 'application/x-www-form-urlencoded' }) {
    debugger;
    // open(url,'_blank')
    var ajax = new XMLHttpRequest();
    ajax.open(httptype, url);
    ajax.setRequestHeader('Content-Type', datatype);
    ajax.send(querystring);
    ajax.onreadystatechange = function () {
        if (ajax.readyState == 4 && ajax.status == 200) {
            try {
                success(ajax.responseText);
            } catch (error) {
                console.log(error);
            }
        }
        else {
            failed(ajax.responseText);
        }
    }
}

//#endregion


