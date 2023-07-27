import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_Activity_API;
let fileUploaddomain = process.env.VUE_APP_BASE_FileUpload_API;
let appSvc = {
  getPromoteContents(params) {
    return request2({
      url: domain + "/Promote/GetPromoteContents",
      method: "get",
      params
    });
  },
  createPromoteContent(data) {
    return request2({
      url: domain + "/Promote/CreatePromoteContent",
      method: "post",
      data
    });
  },
  updatePromoteContentOnline(data) {
    return request2({
      url: domain + "/Promote/UpdatePromoteContentOnline",
      method: "post",
      data
    });
  },
  deletePromoteContent(data) {
    return request2({
      url: domain + "/Promote/DeletePromoteContent",
      method: "post",
      data
    });
  },
  getPromoteContentDetail(params) {
    return request2({
      url: domain + "/Promote/GetPromoteContentDetail",
      method: "get",
      params
    });
  },
  getQiNiuToken(params) {
    return request2({
      url: fileUploaddomain + "/QiNiu/GetQiNiuToken",
      method: "get",
      params
    });
  },
  formatDate(date, fmt) {
    if (/(y+)/.test(fmt)) {
      fmt = fmt.replace(RegExp.$1, (date.getFullYear() + '').substr(4 - RegExp.$1.length));
    }
    let o = {
      'M+': date.getMonth() + 1,
      'd+': date.getDate(),
      'h+': date.getHours(),
      'm+': date.getMinutes(),
      's+': date.getSeconds()
    };
    for (let k in o) {
      if (new RegExp(`(${k})`).test(fmt)) {
        let str = o[k] + '';
        fmt = fmt.replace(RegExp.$1, (RegExp.$1.length === 1) ? str : ('00' + str).substr(str.length));
      }
    }
    return fmt;
  },
  getRandomInt(min, max) {
    return Math.floor(Math.random() * (max - min + 1)) + min;
  },
  updatePromoteContent(data) {
    return request2({
      url: domain + "/Promote/UpdatePromoteContent",
      method: "post",
      data
    });
  },
  deletePromoteAttachment(data) {
    return request2({
      url: domain + "/Promote/DeletePromoteAttachment",
      method: "post",
      data
    });
  }
};

export { appSvc };
