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
        let tipBackground = document.createElement('div');
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
        ajax.onreadystatechange = function () {
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

}
