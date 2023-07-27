
/**
 * 使用span标签包裹内容，然后计算span的宽度 width： px
 * @param valArr
 */
let calcwidth_span = document.querySelector(".getTextWidth");
function getTextWidth(str) {
    if (str === "" || str == undefined) return 0;
    if (calcwidth_span == null) calcwidth_span = document.querySelector(".getTextWidth");
    calcwidth_span.innerText = str;
    let width = calcwidth_span.offsetWidth;
    calcwidth_span.innerText = "";
    return width;
}
/**
* 遍历列的所有内容，获取最宽一列的宽度
* @param arr
*/
export function getMaxLength(arr) {
    // console.log("------------",arr)
    return arr.reduce((acc, item) => {
        if (item) {
            let calcLen = getTextWidth(item);
            if (acc < calcLen) {
                acc = calcLen;
            }
        }
        return acc;
    }, 0);
}
/**
 * 计算列宽 width： px
 * @param valArr：数据集合，fields：需要自动计算宽度的列集合{prop:{width:列宽,label:列标题名称}}
 */
// export function autoWidth(valArr, widthObject) {
//     for (let prop in widthObject) {
//         const arr = valArr.map(x => x[prop]); // 获取每一列的所有数据
//         if (widthObject[prop].label) {
//             arr.push(widthObject[prop].label); // 把每列的表头也加进去算
//         }
//         widthObject[prop].width = Math.max(getMaxLength(arr), widthObject[prop].min || 0, 70); // 每列内容最大的宽度 + 表格的内间距(依据实际情况而定)
//     }
//     return widthObject;
// }
export function toMoney(num, decDigit) {
    return "￥" + format(num, decDigit || 2);
}
export function format(num, decDigit) {
    num = num || 0;
    if (isNaN(num)) num = 0;
    num = parseFloat(num);
    num = String(num.toFixed(decDigit));
    var re = /(-?\d+)(\d{3})/;
    while (re.test(num)) {
        num = num.replace(re, "$1,$2");
    }
    return num;
}
export function toYMDHm(time) {
    time = time || "";
    if (time.indexOf('1900-01-01') == 0) return "";
    return JDateFormat(time);
}
export function toYMD(time) {
    time = time || "";
    if (time.indexOf('1900-01-01') == 0) return "";
    return JDateFormat(time, "yyyy-MM-dd");
}
Array.prototype.getName = function (id, fieldId, fieldName, defaulName) {
    if (id == null || id == undefined) return defaulName;
    let items = this.filter(p => {
        return p[fieldId || 'id'] == id;
    })
    if (items.length == 0) return defaulName || "";
    else return items[0][fieldName || "name"] || defaulName;
}
Number.prototype.format = function (n) {
    //参数说明： n 保留小数位
    return format(this, n);
}
Number.prototype.toMoney = function (n) {
    //参数说明： n 保留小数位
    return toMoney(this, n || 2);
}

String.prototype.toYMDHm = function () {
    return toYMDHm(this);
}
Date.prototype.toYMDHm = function () {
    return toYMDHm(this);
}
String.prototype.toDate = function () {
    if (this == "") return this;
    var dateTmp = new Date(this.replace(/-/g, "\/"));
    if (dateTmp == "Invalid Date" || isNaN(dateTmp)) return null;
    else return dateTmp;
}
//方法一扩展（C#中PadLeft、PadRight）
String.prototype.padLeft = function (len, charStr) {
    var s = this + '';
    return new Array(len - s.length + 1).join(charStr || '0') + s;
}
String.prototype.endWith = function (str) {
    if (!str || !this) return false;
    // console.log("endWith",this,str,this.indexOf(str),this.length  - str.length)
    return this.indexOf(str) == this.length - str.length;
}
Number.prototype.padLeft = function (len, charStr) {
    return this.toString().padLeft(len, charStr)
}
//日期格式化
function JDateFormat(date1, format) {
    if (date1 == undefined) return "";
    else {
        if (typeof date1 == "string") {
            date1 = date1.replace(/-/g, "\/").replace(/T/g, " ");
            if (date1.indexOf('.') > -1) {
                date1 = date1.substring(0, date1.indexOf('.'));
            }
        }
        return date1.JDateFormat(format);
    }
}
//日期格式化
String.prototype.JDateFormat = function (format) {
    if (this == "") return this;
    var dateTmp = new Date(this.replace(/-/g, "\/").replace(/T/g, " "));
    if (dateTmp == "Invalid Date" || isNaN(dateTmp)) return "";
    else return dateTmp.JDateFormat(format);
}
//日期格式化
Date.prototype.JDateFormat = function (format) {
    if (format == undefined) format = "yyyy-MM-dd hh:mm";
    var o = {
        "M+": this.getMonth() + 1,
        "d+": this.getDate(),
        "h+": this.getHours(),
        "m+": this.getMinutes(),
        "s+": this.getSeconds(),
        "q+": Math.floor((this.getMonth() + 3) / 3),
        "S": this.getMilliseconds()
    }

    if (/(y+)/.test(format)) {
        format = format.replace(RegExp.$1, (this.getFullYear() + "").substr(4 -
            RegExp.$1.length));
    }

    for (var k in o) {
        if (new RegExp("(" + k + ")").test(format)) {
            format = format.replace(RegExp.$1, RegExp.$1.length == 1 ?
                o[k] :
                ("00" + o[k]).substr(("" + o[k]).length));
        }
    }
    return format;
}

export function extend() {
    var length = arguments.length
    var target = arguments[0] || {}

    if (typeof target !== 'object' && typeof target !== 'function') {
        target = {}
    }
    var i = 1
    if (length === 1) {
        target = this
        i--
    }
    for (i; i < length; i++) {
        //var source = arguments[i]
        Object.assign(target, arguments[i] || {});
        //   for (var key in source) {
        //     // 使用for in会遍历数组所有的可枚举属性，包括原型。
        //     if (Object.prototype.hasOwnProperty.call(source, key)) {
        //       target[key] = source[key]
        //     }
        //   }
    }
    return target
}
export function hasClass(elem, cls) {
    cls = cls || '';
    if (cls.replace(/\s/g, '').length == 0) return false;
    return new RegExp(' ' + cls + ' ').test(' ' + elem.className + ' ');
}
export function addClass(elem, cls) {
    if (!hasClass(elem, cls)) {
        elem.className += ' ' + cls;
    }
}
export function removeClass(elem, cls) {
    if (elem && hasClass(elem, cls)) {
        var newClass = ' ' + elem.className.replace(/[\t\r\n]/g, '') + ' ';
        while (newClass.indexOf(' ' + cls + ' ') >= 0) {
            newClass = newClass.replace(' ' + cls + ' ', ' ');
        }
        elem.className = newClass.replace(/^\s+|\s+$/g, '');
    }
}
export function max(list) {
    return Math.max.apply(null, list.filter(item => !isNaN(item)))
}

export function map(list, fn) {
    return Array.prototype.map.call(list, fn)
}

export function isEmpty(item) {
    return item.length === 0
}
export function deepCopy(obj) {
    let objClone = Array.isArray(obj) ? [] : {};
    if (obj && typeof obj == 'object') {
        for (const key in obj) {
            //判断obj子元素是否为对象，如果是，递归复制
            if (obj[key] && typeof obj[key] === "object") {
                objClone[key] = deepCopy(obj[key]);
            } else {
                //如果不是，简单复制
                objClone[key] = obj[key];
            }
        }
    }
    return objClone;
}
export function jsonToArray(jsonObj) {
    let arr = [];
    for (let key in jsonObj) {
        arr.push({ key: key, value: jsonObj[key] })
    }
    return arr;
}