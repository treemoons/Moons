
/**
 * @param {string} text
 * @param {'infomation'|'warning'|'error'} warningtype
 * @param {'YesNoCancel'|'YesNo'|'Yes'} messageButton
 * @param {{yesText:'Yes'|'确定',noText:'No'|'拒绝'|'取消',cancelText:'Cancel'|'取消',yes:(args:any)=>void,no:(args:any)=>void,cancel:()=>void,timeout:number}} buttonsCallback
 * @param {{
        yesColor :string, yesBackground :string,
        noColor :string, noBackground :string,
        cancelColor :string, cancelBackground :string,
        infoColor :string, infoBackground :string,
        warningColor :string, warningBackground :string,
        errorColor :string, errorBackground :string,
        tittleBarForeColor :string, tittleBarBackground :string,
        contentForeColor :string, contentBackground :string,
        Color :string, backgroundColor:string}} 
        COLOR OF MESSAGE BOX 
 */
export async function messagebox(text, warningtype, messageButton,
    {yesText='Yes',noText='No',cancelText='Cancel',  yes = d => { }, no = d => { }, cancel = d => { }, timeout = undefined } = {},
    {
        yesColor = 'white', yesBackground = '#26bd76',
        noColor = 'white', noBackground = '#e65936',
        cancelColor = 'gray', cancelBackground = '#efefef',
        infoColor = 'white', infoBackground = '#26bd76',
        warningColor = 'white', warningBackground = '#e65936',
        errorColor = 'gray', errorBackground = '#efefef',
        tittleBarForeColor = '', tittleBarBackground = '#b9b9ff',
        contentForeColor = '', contentBackground = '',
        Color = '', backgroundColor = '#ffffffbb'
    } = {}) {
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
        btnyes.innerText = yesText;
        switch (messageButton) {
            case 'YesNoCancel':
                btnno = document.createElement('button');
                btncancel = document.createElement('button');
                btnno.innerText = noText;
                btncancel.innerText = cancelText;
                buttons.append(btnyes, btnno, btncancel);

                break;
            case 'YesNo':
                btnno = document.createElement('button');
                btnno.innerText = noText;
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
        background-color:${backgroundColor};
        color: ${Color}`;
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
        padding: 10px 20px;
        margin-block-start: 0;
        background-color:${tittleBarBackground};
        color: ${tittleBarForeColor}`;
        warningicon.style = `
        font-size:16px;
        display: inline-block;
        text-align: center;
        width:20px;
        border-radius: 50%;
        -webkit-border-radius: 50%;
        -moz-border-radius: 50%;
        -ms-border-radius: 50%;
        -o-border-radius: 50%;`;
        warningtip.style = `margin-left: 10px`;
        content.style = `
        text-align: center;
        width: 80%;
        margin: 6% auto;
        background-color:${contentForeColor};
        color: ${contentBackground}`;
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
        padding:5px 10px;
        background-color:${yesBackground};
        color: ${yesColor}`;
        btnyes.onmouseenter = e => btnyes.style.boxShadow = '1px 2px 4px 2px hsl(0, 0 %, 86 %)';
        btnyes.onmouseleave = e => btnyes.style.boxShadow = 'none';
        btnyes.onclick = yesClick;
        if (btnno) {
            btnno.style = `
            outline: none;
            min-width: 50px;
            border: none;
            /* box-shadow: 1px 2px 4px 0px #f8f8f8; */
            padding:5px 10px;
            background-color:${noBackground};
            color: ${noColor}`;
            btnno.onmouseenter = e => btnno.style.boxShadow = '1px 2px 4px 2px hsl(0, 0 %, 86 %)';
            btnno.onmouseleave = e => btnno.style.boxShadow = 'none';
            btnno.onclick = noClick;
        }
        if (btncancel) {
            btncancel.style = `
            outline: none;
            min-width: 50px;
            border: none;
            /* box-shadow: 1px 2px 4px 0px #f8f8f8; */
            padding:5px 10px;
            background-color:${cancelBackground};
            color: ${cancelColor}`;
            btncancel.onmouseenter = e => btncancel.style.boxShadow = '1px 2px 4px 2px hsl(0, 0 %, 86 %)';
            btncancel.onmouseleave = e => btncancel.style.boxShadow = 'none';
            btncancel.onclick = cancelClick;
        }
        //#endregion

        //#region warning style
        switch (warningtype) {
            case 'warning':
                warningicon.style.color = warningColor;
                warningicon.style.backgroundColor = warningBackground;
                warningtip.innerText = '警 告';
                break;
            case 'error':
                warningicon.style.color = errorColor;
                warningicon.style.backgroundColor = errorBackground;
                warningtip.innerText = '错 误';
                break;
            case 'infomation':
            default:
                warningicon.style.color = infoColor;
                warningicon.style.backgroundColor = infoBackground;
                warningtip.innerText = '提 示';
                break;
        }
        //#endregion

        //#region  content shows

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
        //#endregion

        //#region onclick 事件
        async function yesClick() {
            yes();
            close();
        }
        async function noClick() {
            no();
            close();
        }
        async function cancelClick() {
            cancel();
            close();
        }

        //#endregion

        //#region show and close

        async function show() {
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
        async function close() {
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
        tip.onclick = async function (e) {
            e.stopPropagation()
        }

        //#region  warning animation that you choice is necessary 
        async function tipBackgroundFlash(e) {
            flash(2);
        }
        async function flash(times) {
            tip.style.boxShadow = '1px 2px 7px 7px gainsboro';

            setTimeout(() => {
                tip.style.boxShadow = '1px 2px 7px 0px gainsboro';
                if (times > 1)
                    setTimeout(() => {
                        flash(--times);
                    }, 200);
            }, 200);
        }
        //#endregion

        show();

    }
}
