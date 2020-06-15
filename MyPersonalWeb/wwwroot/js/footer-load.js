
windowResize.push(resizeLogin);
windowOnload.push(loadlang, selectedlang, loadSelectedType);

/**
 * 展示menu列表
 */
var menuButton = document.getElementById('menu-options');
/**
 * 登录用途的div元素（最外父元素）
 * 用于调整位置
 */
var login = document.getElementById('login');
/**
 * 展示个人选项
 */
var userOptions = document.getElementById('user-options');

loginClose();
pressEnter(loginform.username, form => form.submit.click());
pressEnter(loginform.password, form => form.submit.click());
closemenu();