//#region  master context

/**
 * 显示等待动画，配合等待div元素
 */
function waitDot() {
    var wait = document.getElementById('wait');
    if (wait == undefined) return;
    wait.style = 'display:block';
}
function waitLoginClose() {
    setTimeout(() => {
        var wait = document.getElementById('wait');
        if (wait == undefined) return;
        wait.style = 'display:none';
    }, 400);
}

document.onreadystatechange = e => {
    let process;
    try {
        process = document.getElementById('processbar');
        if (process == undefined) return;
    } catch (error) {
        console.log(error);
        return;
    }
    switch (document.readyState) {
        case "uninitialized":
            process.style.left = '-75%'
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
//#region  用于调整设置登录弹窗的位置


/** 获取登录login元素的div
 * @param {HTMLElement} l login登录元素
 */
function getLoginLeft(l) {
    if (login == undefined) return;
    return `left:${(document.body.clientWidth - parseInt(window.getComputedStyle(l).width)) / 2}px`
}
/**刷新login宽度 */
function resizeLogin() {
    if (login == undefined) return;
    let block = getComputedStyle(login, null).getPropertyValue('display') == 'block';
    let largesreen = document.body.clientWidth > 766;
    let bool = block && largesreen;
    if (bool)
        login.style.left = (document.body.clientWidth - parseInt(window.getComputedStyle(login).width)) / 2 + 'px';
    else
        login.style.left = '0';
}
function whiteBackground(display) {
    let background = document.getElementById('login-background');
    if (background == undefined) return false;
    background.style.display = display;
    return true;
}

/** 点击空白，关闭login弹窗 */
function whiteBack() {
    loginClose();
    if (menuButton == undefined) return;
    menuButton.style = 'opacity:0;top:-20%;'
    setTimeout(function () {
        menuButton.style.display = 'none';
    }, 400);
    if (!whiteBackground('none')) return;
    isMenuShow = false;
}

function showLogin() {
    if (login == undefined) return;
    if (isMenuShow) {
        showAndCloseMenu();
    }
    login.style = 'display:block;' + getLoginLeft(login);
    setTimeout(function () {
        login.style = 'opacity:100;top: 5vh;' + getLoginLeft(login);
    }, 40);
    whiteBackground('block');
    loginform.username.focus();
}

/**
 * 关闭登录窗口
 */
function loginClose() {
    if (login == undefined) return;
    login.style = 'opacity:0;top:-100%;' + getLoginLeft(login);
    let loginerror = document.getElementById('loginerror');
    loginerror.innerHTML = '&#160;';
    setTimeout(function () {
        login.style = 'display:none'
    }, 400);
    whiteBackground('none');
}


function keyEnterLogin() {
    if (login == undefined) return;
    pressEnter(loginform.username);
    pressEnter(loginform.password);
}
//#endregion

/**
 * 
 * @param {HTMLElement} input  input where type is text and whose attributes have 'tip'
 */
function IsInputEmpty(input) {
    let error = document.getElementById('loginerror')
    if (error == undefined) return;
    if (input.value == '') {
        error.innerText = input.getAttribute('tip');
        return true;
    }
    else {
        return false;
    }
}
//#region  login and logout
/**
 * login your account to Moons
 */
function signin() {
    if (IsInputEmpty(loginform.username)) {
        loginform.username.focus();
        return false;
    } else if (IsInputEmpty(loginform.password)) {
        loginform.password.focus();
        return false;
    }
    waitDot();
    getAjaxData({
        url: `/${lang}/home/login`,
        data:
            `username=${loginform.username.value}&` +
            `password=${loginform.password.value}&` +
            `isremembered=${loginform.isremembered.checked}`,
        success: data => {
            debugger;
            if (data == 'T') {
                open(window.location.href, '_self')
                // debugger;
                // loginform.setAttribute('action', location.href);
                // loginform.submit();
            } else {
                let loginerror = document.getElementById('loginerror');
                switch (data) {
                    case 'F':
                        loginerror.innerText = loginerror.getAttribute('tip1');
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
 * SIGN OUT CAN BE DONE
 */
function signout() {
    // waitLogin();
    getAjaxData({
        url: `/${lang}/home/logout`,
        success: data => {
            debugger;
            //loginClose();
            if (data == 'T')// {
                open(window.location.href, '_self')
            // debugger;
            // loginform.setAttribute('action', location.href);
            // loginform.submit();
            /* }   else {
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
              waitLoginClose();*/
        }
    });
}

//#endregion



/**
 * 显示或这关闭menu
 */
function showAndCloseMenu() {
    try {
        if (userOptions == undefined) return;
        if (window.getComputedStyle(userOptions).display == 'block') {
            userOptions.style = 'opacity:0;right:-20vw;'
            setTimeout(function () {
                userOptions.style.display = 'none';
            }, 400);
            isUserOptionsShow = false;
        }
    } catch (error) { console.log(error); }
    if (menuButton == undefined) return;
    if (!isMenuShow) {
        menuButton.style = 'display:block;';
        setTimeout(function () {
            menuButton.style = 'opacity:100;top:54px;'
        }, 40);
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
    if (conponent == undefined) return;
    conponent.onclick = function () {
        if (isMenuShow) {
            showAndCloseMenu();
        }
    }
}


function showUserOptions() {
    if (menuButton == undefined) return;
    if (window.getComputedStyle(menuButton).display == 'block') {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(function () {
            menuButton.style.display = 'none';
        }, 400);
        isMenuShow = false;
    }
    if (userOptions == undefined) return;
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
 *  back to home page
 */
function backIndex() {
    open(`/${lang}/home/index`, "_self");
}

/**
 * sync searchtext which is input element 
 */
function loadSearchText() {
    let text = getQueryString('searchtext', location.search, '&').decodeUriString();
    search.searchtext.value = text;
}

/**
 * show or hide div which contains searchText options
 */
function selectSearchType() {
    showSelect('searchtypeoptions', 'searchselected');

}

//#region national lanuage
/**
 * load click event
 */
function loadSelectedType() {
    let typeSelect = document.querySelectorAll('#searchtypeoptions option');
    typeSelect.forEach(option => {
        option.onclick = async function () {
            let selected = document.getElementById('searchselected');
            selected.innerText = this.innerText;
            search.searchtype.value = this.innerText;
            document.getElementById('searchtypeoptions').style.display = 'none';
            selected.style.fontWeight = 'normal';
        }
    });
}



//#region  load and change display
/**
 * Specify the national language of HTML
 * @param {string} elelang Abbreviation of language between all of world
 * @param {Number} date date of expires
 */
function changeLanguageCookie(elelang, date = 100) {
    setCookie('lang', elelang, date)
}
/**
 * 加载上一次
 */
function loadlang() {
    let elelang = getCookie('lang');
    if (elelang == undefined) {
        elelang = 'en';
    }
    setCookie('lang', elelang)
}
//#endregion
//#region  select language options
/**
 * 
 * @param {HTMLElement} parent parent element DIV
 * @param {HTMLElement} chlidren selected shows
 */
function showSelect(parent, chlidren) {
    let options = document.getElementById(parent);
    document.getElementById(chlidren).style.fontWeight = 'bolder';
    if (options.style.display == 'block') {
        options.style.display = 'none';
    } else {
        options.style.opacity = '0';
        options.style.display = 'block';
        setInterval(() => {
            options.style.opacity = '100';
        }, 0);
    }
}
/**
 * 添加language选中一个显示之后的click事件
 */
function loadSelectedlang() {
    let langselects = document.querySelectorAll('#lang option');
    langselects.forEach(option => {
        option.onclick = async function () {
            let selected = document.getElementById('selected');
            selected.innerText = this.innerText;
            document.getElementById('lang').style.display = 'none';
            selected.style.fontWeight = 'normal';
            changeLanguageCookie(option.value); debugger
            open(`/${option.value}/${route}${location.search}`, '_self');

        }
    });
}
//#endregion

//#endregion



//#endregion

function test() {
    getAjaxData({
        url: "/en/home/show",
        data: {
            name: "Emrys",
            age: "26",
            bobbys: ["足球", "电影"],
            company: {
                name: "上海xxxxxx公司",
                address: "上海徐汇区xxxx路",
                tel: [
                    "021-88888881",
                    "021-88888882",
                    "021-88888883",
                    "021-88888884"
                ]
            },
            star: [
                { "name": "成龙", "age": "63", "movie": "十二生肖" },
                { "name": "刘亦菲", "age": "18", "movie": "功夫之王" },
                { "name": "胡歌", "age": "24", "movie": "琅琊榜" }
            ]
        },
        method:"post",
        success: data => {
            console.log(data);
            console.log(this.data)
        }
    })
}
