import request2 from "@/utils/request2";

let domain = process.env.VUE_APP_BASE_Activity_API;
let fileUploaddomain = process.env.VUE_APP_BASE_FileUpload_API;
let appSvc = {
    getInstallGuidePages(params) {
    return request2({
      url: domain + "/InstallGuide/GetInstallGuidePages",
      method: "get",
      params
    });
  },
  createInstallGuide(data) {
    return request2({
      url: domain + "/InstallGuide/CreateInstallGuide",
      method: "post",
      data
    });
  },
  updateInstallGuideStatus(data) {
    return request2({
      url: domain + "/InstallGuide/UpdateInstallGuideStatus",
      method: "post",
      data
    });
  },
  deleteInstallGuideFile(data) {
    return request2({
      url: domain + "/InstallGuide/DeleteInstallGuideFile",
      method: "post",
      data
    });
  },
  getInstallGuideDetail(params) {
    return request2({
      url: domain + "/InstallGuide/GetInstallGuideDetail",
      method: "get",
      params
    });
  },
  getInstallGuideCategory(params) {
    return request2({
      url: domain + "/InstallGuide/GetInstallGuideCategory",
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
  updateInstallGuideInfo(data) {
    return request2({
      url: domain + "/InstallGuide/UpdateInstallGuideInfo",
      method: "post",
      data
    });
  },
  getVehicleBrandList(params) {
    return request2({
      url: domain + "/Vehicle/GetVehicleBrandList",
      method: "get",
      params
    });
  },
  getVehicleListByBrand(params) {
    return request2({
      url: domain + "/Vehicle/GetVehicleListByBrand",
      method: "get",
      params
    });
  },
  getPaiLiangByVehicleId(params) {
    return request2({
      url: domain + "/Vehicle/GetPaiLiangByVehicleId",
      method: "get",
      params
    });
  },
  getVehicleNianByPaiLiang(params) {
    return request2({
      url: domain + "/Vehicle/GetVehicleNianByPaiLiang",
      method: "get",
      params
    });
  },
  getVehicleSalesNameByNian(params) {
    return request2({
      url: domain + "/Vehicle/GetVehicleSalesNameByNian",
      method: "get",
      params
    });
  }
};

export { appSvc };
