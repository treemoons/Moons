
import * as common from './common.js';
import { messagebox } from './common/dom.js';

/**
 * 判断菜单显示
 */
export var isMenuShow = false;
/**
 * 判断是都登录成功
 */
export var isUserOptionsShow = false;

/**
 * 展示menu列表
 */
export var menuButton = document.getElementById('menu-options');
/**
 * 登录用途的div元素（最外父元素）
 * 用于调整位置
 */
export var login = document.getElementById('login');
/**
 * 展示个人选项
 */
export var userOptions = document.getElementById('user-options');

let whiteBackgroundEle = document.getElementById('login-background');


document.onload = async function (e) {
    if (this.windowOnload != null) {
        this.windowOnload.forEach(item => {
            try { item(); } catch (error) { console.log(error); }
        });
    }
}

String.prototype.write = async function () {
    document.write(this);
};
String.prototype.getEleById = async function () {
    return document.getElementById(this);
};
/**
 * 扩展--URI转换成string
 */
String.prototype.decodeUriString = async function () {
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
 * 
 * @param {HTMLElement} input input where type is text
 * @param {RegExp} parttern when press enter key , do this async function
 * @param {Function} action default is submit param{input} to target form
 */
export async function press(input, parttern, action) {
    input.onkeypress = e => {
        if (e.key == 'Enter') {
            if (parttern.test(input.value))
                action(form);
        }
    };
}


//#region  master context


document.onreadystatechange = e => {
    let process;
    try {
        process = document.getElementById('processbar');
        if (!process) return;
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


/**
 * 
 * @param {'none'|'block'|'flex'} display 
 */
export async function whiteBackground(display) {
    if (!whiteBackgroundEle) return false;
    if (display == 'none')
        setTimeout(() => {
            whiteBackgroundEle.style.display = display;
        }, 400);
    else {
        whiteBackgroundEle.style.display = display;
    }
    return true;
}

export async function showLogin() {
    if (!login) return;
    if (isMenuShow) {
        showAndCloseMenu();
    }
    login.style = 'display:block;'
    setTimeout(async function () {
        login.style = 'opacity:100;top: 5vh;';

    }, 40);
    whiteBackground('flex');
    loginform.username.focus();
}

/**
 * 关闭登录窗口
 */
export async function loginClose() {
    if (!login) return;
    login.style = 'opacity:0;top:-100%;'
    let loginerror = document.getElementById('loginerror');
    loginerror.innerHTML = '&#160;';
    setTimeout(async function () {
        login.style = 'display:none'
    }, 400);
    whiteBackground('none');
}


export async function keyEnterLogin() {
    if (!login) return;
    common.pressEnter(loginform.username);
    common.pressEnter(loginform.password);
}
//#endregion

/**
 * 
 * @param {HTMLElement} input  input where type is text and whose attributes have 'tip'
 */
export async function IsInputEmpty(input) {
    debugger
    let error = document.getElementById('loginerror')
    if (!error) return;
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
export async function signin() {
    debugger
    if (IsInputEmpty(loginform.username)) {
        loginform.username.focus();
        return false;
    } else if (IsInputEmpty(loginform.password)) {
        loginform.password.focus();
        return false;
    }
    common.waitDot('');
    common.getAjaxData({
        url: `/${lang}/home/login`,
        data:
            `username=${loginform.username.value}&` +
            `password=${loginform.password.value}&` +
            `isremembered=${loginform.isremembered.checked}`,
        success: async data => {
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
            common.waitDot('none');
        }
    });
    return false;
}

/**
 * SIGN OUT CAN BE DONE
 */
export async function signout() {
    common.getAjaxData({
        url: `/${lang}/home/logout`,
        success: async data => {
            if (data == 'T')
                open(window.location.href, '_self')
        }
    });
}

//#endregion

/**
 * 显示或这关闭menu
 */
export function showAndCloseMenu() {
    try {
        if (!userOptions) throw 'userOption Element is undefined！';
        if (window.getComputedStyle(userOptions).display == 'block') {
            userOptions.style = 'opacity:0;right:-20vw;'
            setTimeout(async function () {
                userOptions.style.display = 'none';
            }, 400);
            isUserOptionsShow = false;
        }
    } catch (error) { console.log(error); }
    if (!menuButton) return;
    if (!isMenuShow) {
        menuButton.style = 'display:block;';
        setTimeout(async function () {
            menuButton.style = 'opacity:100;top:54px;'
        }, 40);
        isMenuShow = true;
    } else {
        menuButton.style = 'opacity:0;top:-20%;'
        setTimeout(async function () {
            menuButton.style.display = 'none';
        }, 400);
        isMenuShow = false;
    }
}
export async function closemenu() {
    let conponent = document.querySelector('.conponent');
    if (!conponent) return;
    conponent.onclick = async function () {
        if (isMenuShow) {
            showAndCloseMenu();
        }
        if (isUserOptionsShow)
            showAndCloseUserOptions();
    }
}


export async function showAndCloseUserOptions() {
    try {
        if (!menuButton) throw 'menuButton Element is undefined!';
        if (window.getComputedStyle(menuButton).display == 'block') {
            menuButton.style = 'opacity:0;top:-20%;'
            setTimeout(async function () {
                menuButton.style.display = 'none';
            }, 400);
            isMenuShow = false;
        }
    } catch (error) { console.log(error) }
    if (!userOptions) return;
    if (!isUserOptionsShow) {
        userOptions.style = 'display:block';
        setTimeout(async function () {
            userOptions.style = 'opacity:100;right:0;'
        }, 40);
        isUserOptionsShow = true;
    } else {
        userOptions.style = 'opacity:0;right:-20vw;'
        setTimeout(async function () {
            userOptions.style.display = 'none';
        }, 400);
        isUserOptionsShow = false;
    }
}
/**
 *  back to home page
 */
export async function backIndex() {
    open(`/${lang}/home/index`, "_self");
}

/**
 * sync searchtext which is input element 
 */
export async function loadSearchText() {
    let text = (await common.getQueryString('searchtext', location.search, '&')).decodeUriString();
    debugger
    search.searchtext.value = text;
}
export async function searchsubmit() {
    debugger
    // search.searchtext.value = encodeURI(search.searchtext.value);
    search.submit();
}

//#region national lanuage

//#region searchType

// /**
//  * show or hide div which contains searchText options
//  */
// async function selectSearchType() {
//     showSelect('searchtypeoptions', 'searchselected');

// }

// /**
//  * load click event
//  */
// async function loadSelectedType() {
//     let typeSelect = document.querySelectorAll('#searchtypeoptions option');
//     typeSelect.forEach(option => {
//         option.onclick = async async function () {
//             let selected = document.getElementById('searchselected');
//             selected.innerText = this.innerText;
//             search.searchtype.value = this.innerText;
//             document.getElementById('searchtypeoptions').style.display = 'none';
//             selected.style.fontWeight = 'normal';
//         }
//     });
// }

//#endregion

//#region  load and change display
/**
 * Specify the national language of HTML
 * @param {string} elelang Abbreviation of language between all of world
 * @param {Number} date date of expires
 */
export async function changeLanguageCookie(elelang, date = 100) {
    document.cookie = common.buildCookie('lang', elelang, { day:date });

}
/**
 * 加载上一次
 */
export async function loadlang() {
    let elelang = await common.getQueryString('lang', document.cookie, '&');
    if (!elelang) {
        elelang = 'en';
    }
    document.cookie = common.buildCookie('lang', elelang);
}
//#endregion
//#region  select language options
/**
 * 展示点击选择语言的窗口
 * @param {HTMLElement} parent parent element DIV
 * @param {HTMLElement} chlidren selected shows
 */
export async function showSelect(parent = 'lang', chlidren = 'selected') {
    let optionWindow = document.getElementById(parent);
    document.getElementById(chlidren).style.fontWeight = 'bolder';
    if (optionWindow.style.display == 'block') {
        optionWindow.style.display = 'none';
    } else {
        optionWindow.style.opacity = '0';
        optionWindow.style.display = 'block';
        setInterval(() => {
            optionWindow.style.opacity = '100';
        }, 0);
    }
}
/**
 * 添加language选中一个显示之后的click事件
 */
export async function loadSelectedlang() {
    let langselects = document.querySelectorAll('#lang option');
    langselects.forEach(option => {
        option.onclick = async function () {
            let selected = document.getElementById('selected');
            selected.innerText = this.innerText;
            document.getElementById('lang').style.display = 'none';
            selected.style.fontWeight = 'normal';
            changeLanguageCookie(option.value);
            open(`/${option.value}/${route}${location.search}`, '_self');
        }
    });
}
//#endregion

//#endregion



//#endregion

async function test() {
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
        method: "post",
        success: data => {
            console.log(data);
            console.log(this.data)
        }
    })
}



/** 点击空白，关闭login弹窗
 * @param {Event}e
 */
if (whiteBackgroundEle)
    whiteBackgroundEle.addEventListener('click', whiteBackClick);
async function whiteBackClick(e) {
    loginClose();
    if (!menuButton) return;
    menuButton.style = 'opacity:0;top:-20%;'
    setTimeout(async function () {
        menuButton.style.display = 'none';
    }, 400);
    if (!whiteBackground('none')) return;
    isMenuShow = false;
}


/**
 * 阻止冒泡
 * @param {Event} e 
 */
if (login)
    login.addEventListener('click', e => e.stopPropagation());

loadlang();
loadSelectedlang();

keyEnterLogin();
loginClose();
closemenu();

top.backIndex = backIndex;
top.searchsubmit = searchsubmit;
top.showAndCloseMenu = showAndCloseMenu;
top.loginClose = loginClose;
top.signin = signin;
top.showSelect = showSelect;
top.showUserOptions = showAndCloseUserOptions;
top.showLogin = showLogin;
top.signout = signout;
top.totop = common.totop;
top.messagebox = messagebox;