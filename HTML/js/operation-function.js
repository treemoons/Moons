//#region  用于调整设置登录弹窗的位置
/**
 * 登录用途的div元素（最外父元素）
 * 用于调整位置
 */
var login = document.getElementsByClassName('login')[0];
/**
 * 获取登录login元素的div
 * @param {login} l login登录元素
 */
function getLoginLeft(l) {
    return 'left:' + (document.body.clientWidth - this.parseInt(window.getComputedStyle(l).width)) / 2 + 'px'
}
window.onresize = function () {
    /**刷新login宽度 */
    debugger;
    let block = getComputedStyle(this.login, null).getPropertyValue('display') == 'block';
    let largesreen = document.body.clientWidth > 766;
    let bool = block && largesreen;
    formlogin.username.value = bool;
    if (bool)
        login.style.left = (document.body.clientWidth - this.parseInt(window.getComputedStyle(this.login).width)) / 2 + 'px';
    else
        this.login.style.left = '0';
}
window.onload = function () {
    this.closeLogin();
    this.showAndCloseMenu();
}

function showLogin() {
    debugger;
    if (window.getComputedStyle(menuButton).display == 'block') {
        showAndCloseMenu();
    }
    login.style = 'display:block;' + this.getLoginLeft(this.login);
    setTimeout(function () {
        login.style = 'opacity:100;top: 5vh;' + this.getLoginLeft(this.login);
    }, 400);
}

function closeLogin() {
    login.style = 'opacity:0;top:-100%;' + this.getLoginLeft(this.login);
    setTimeout(function () {
        login.style = 'display:none'
    }, 400);
}
//#endregion
/**
 * 显示等待动画，配合等待div元素
 */
function waitLogin() {
    var wait = document.getElementsByClassName('wait')[0];
    wait.style = 'display:block';
}
//#region  展示MENU列表
/**
 * 展示menu列表
 */
var menuButton = document.getElementsByClassName('menu-options')[0];
var isMenuShow = false;

function showAndCloseMenu() {
    if (isMenuShow) {
        menuButton.style = 'display:block;';
        setTimeout(function () {
            menuButton.style = 'opacity:100;top:6vh;'
        }, 400);
        isMenuShow = false;
    } else {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(function () {
            menuButton.style.display = 'none';
        }, 400);
        isMenuShow = true;
    }
}
//#endregion