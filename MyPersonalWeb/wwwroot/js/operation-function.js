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
     //默认关闭
    this.closeLogin(); 
    this.showAndCloseMenu();  
    this.showUserOptions();
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
/**
 * 展示menu列表
 */
var menuButton = document.getElementsByClassName('menu-options')[0];
var isMenuShow = false;

function showAndCloseMenu() {
    if (isMenuShow) {
        menuButton.style = 'display:block;';
        setTimeout(function () {
            menuButton.style = 'opacity:100;top:7vh;'
        }, 40);
        isMenuShow = false;
    } else {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(function () {
            menuButton.style.display = 'none';
        }, 400);
        isMenuShow = true;
    }
}
/**
 * 展示个人选项
 */
var userOptions = document.getElementsByClassName('user-options')[0];
var isUserOptionsShow = false;
    function showUserOptions() {
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