import request2 from "@/utils/request2";
let shopManage = process.env.VUE_APP_BASE_SHOPMANAGE_API;
let basicData = process.env.VUE_APP_BASE_BASICDATA_API;
let fileUploaddomain = process.env.VUE_APP_BASE_FileUpload_API;
let shopManageSvc = {
  GetShopList(params) {
    return request2({
      url: shopManage + "/Shop/GetShopListForBOSSAsync",
      method: "get",
      params
    });
  },
  GetShopListForServiceAsync(data) {
    return request2({
      url: shopManage + "/Shop/GetShopListForServiceAsync",
      method: "post",
      data
    });
  },
  //新增门店数据
  addShop(data) {
    return request2({
      url: shopManage + "/Shop/AddShop",
      method: "post",
      data
    });
  },
  updateShopOnline(data) {
    return request2({
      url: shopManage + "/Shop/UpdateShopOnline",
      method: "post",
      data
    });
  },
  //获取门店详情
  getShopById(params) {
    return request2({
      url: shopManage + "/Shop/GetShopForBOSSAsync",
      method: "get",
      params
    });
  },
  modifyShopSuspendTime(data) {
    return request2({
      url: shopManage + "/Shop/ModifyShopSuspendTime",
      method: "post",
      data
    });
  },


  reGenerateAppletCode(data) {
    return request2({
      url: shopManage + "/Shop/ReGenerateAppletCode",
      method: "post",
      data
    });
  },
//门店审核
  updateExaminedStatus(data) {
    return request2({
      url: shopManage + "/Shop/UpdateExaminedStatus",
      method: "post",
      data
    });
  },
  //根据RegionId获取子一级所有数据
  getRegionChinaListByRegionId(params) {
    return request2({
      url: basicData + "/RegionChina/GetRegionChinaListByRegionId",
      method: "get",
      params
    });
  },
  //获取基本服务详情
  GetBaseServiceInfo(params) {
    return request2({
      url: shopManage + "/ShopServer/GetBaseServiceInfoAsync",
      method: "get",
      params
    });
  },
  //查询基本服务列表
  GetBaseServiceList(params) {
    return request2({
      url: shopManage + "/ShopServer/GetBaseServiceListAsync",
      method: "get",
      params
    });
  },
  //加盟列表
  getJoinInList(params){
    return request2({
      url: shopManage + "/Shop/GetJoinInList",
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
  getQiNiuToken(params) {
    return request2({
      url: fileUploaddomain + "/QiNiu/GetQiNiuToken",
      method: "get",
      params
    });
  },
  //修改基本服务
  ModifyBaseServiceInfo(data) {
    return request2({
      url: shopManage + "/ShopServer/ModifyBaseServiceInfoAsync",
      method: "post",
      data
    });
  },

  //新增基本服务
  AddBaseService(data) {
    return request2({
      url: shopManage + "/ShopServer/AddBaseServiceAsync",
      method: "post",
      data
    });
  },
  //获取门店服务列表
  GetShopServiceList(params) {
    return request2({
      url: shopManage + "/ShopServer/GetShopServiceListAsync",
      method: "get",
      params
    });
  },

  //修改门店服务
  ModifyShopService(data) {
    return request2({
      url: shopManage + "/ShopServer/ModifyShopServiceAsync",
      method: "post",
      data
    });
  },
  //添加门店服务
  AddShopService(data) {
    return request2({
      url: shopManage + "/ShopServer/AddShopServiceAsync",
      method: "post",
      data
    });
  },

  //修改门店基本数据
  ModifyShopBaseInfo(data) {
    return request2({
      url: shopManage + "/Shop/ModifyShopBaseInfoAsync",
      method: "post",
      data
    });
  },
  //修改门店地址
  ModifyShopAddress(data) {
    return request2({
      url: shopManage + "/Shop/ModifyShopAddressAsync",
      method: "post",
      data
    });
  },
  //修改门店配置信息
  ModifyShopConfigInfo(data) {
    return request2({
      url: shopManage + "/Shop/ModifyShopConfigInfoAsync",
      method: "post",
      data
    });
  },
  //修改门店服务信息
  ModifyShopConfigExpense(data) {
    return request2({
      url: shopManage + "/Shop/ModifyShopConfigExpenseAsync",
      method: "post",
      data
    });
  },
  //修改门店银行账户
  ModifyShopBankAccount(data) {
    return request2({
      url: shopManage + "/Shop/ModifyShopBankAccountAsync",
      method: "post",
      data
    });
  },
  //添加门店图片
  AddShopImgs(data) {
    return request2({
      url: shopManage + "/Shop/AddShopImgs",
      method: "post",
      data
    });
  },
  //删除门店图片
  DeleteShopImg(data) {
    return request2({
      url: shopManage + "/Shop/DeleteShopImgAsync",
      method: "post",
      data
    });
  },
  //添加基本服务大类
  AddBaseServiceCategory(data) {
    return request2({
      url: shopManage + "/ShopServer/AddBaseServiceCategoryAsync",
      method: "post",
      data
    });
  },
  //获取FAQ列表
  GetFAQList(params) {
    return request2({
      url: shopManage + "/FAQ/GetFAQListAsync",
      method: "get",
      params
    });
  },
  //获取FAQ详情
  GetFAQInfo(params) {
    return request2({
      url: shopManage + "/FAQ/GetFAQInfoAsync",
      method: "get",
      params
    });
  },
  //获取FAQ渠道列表
  GetFaqChannelList() {
    return request2({
      url: shopManage + "/FAQ/GetFaqChannelListAsync",
      method: "get"
    });
  },
  //获取FAQ分类列表
  GetFaqCategoryList(params) {
    return request2({
      url: shopManage + "/FAQ/GetFaqCategoryListAsync",
      method: "get",
      params
    });
  },
  //修改FAQ
  ModifyFAQ(data) {
    return request2({
      url: shopManage + "/FAQ/ModifyFAQAsync",
      method: "post",
      data
    });
  },
  //删除FAQ
  DeleteFAQById(data) {
    return request2({
      url: shopManage + "/FAQ/DeleteFAQByIdAsync",
      method: "post",
      data
    });
  },
  getShopTags(params) {
    return request2({
      url: shopManage + "/Shop/GetShopTags",
      method: "get",
      params
    });
  },
  getShopServiceTypePagesAsync(params) {
    return request2({
      url: shopManage + "/ShopServer/GetShopServiceTypePagesAsync",
      method: "get",
      params
    });
  },

  deleteShopServiceType(data) {
    return request2({
      url: shopManage + "/ShopServer/DeleteShopServiceType",
      method: "post",
      data
    });
  },
  getVehicleBrandList(params) {
    return request2({
      url: shopManage + "/Shop/GetVehicleBrandList",
      method: "get",
      params
    });
  },

  getShopServiceBrands(params) {
    return request2({
      url: shopManage + "/Shop/GetShopServiceBrands",
      method: "get",
      params
    });
  },
  getShopListNewAsync(data) {
    return request2({
      url: shopManage + "/Shop/GetShopListNewAsync",
      method: "post",
      data
    });
  },

//
// 查询门店日志
  getShopLog(params) {
    return request2({
      url: shopManage + "/Shop/GetShopLog",

      method: "get",
      params
    });
  },

  GetShopHeaderListByAsync(params) {
    return request2({
      url: shopManage + "/Shop/GetShopHeaderListByAsync",
      method: "get",
      params
    });
  },
  //查询公司列表
  getPageCompanyListForShopAsync(params) {
    return request2({
      url: shopManage + "/Company/GetPageCompanyListForShopAsync",
      method: "get",
      params
    });
  },
  //新增公司
  addOrUpdateCompany(data) {
    return request2({
      url: shopManage + "/Company/AddOrUpdateCompany",
      method: "post",
      data
    });
  },
  //查询公司信息
  getCompanyInfo(params) {
    return request2({
      url: shopManage + "/Company/GetCompanyInfoById",
      method: "get",
      params
    });
  },
  //修改公司状态
  updateCompanyStatus(data) {
    return request2({
      url: shopManage + "/Company/UpdateCompanyStatus",
      method: "post",
      data
    });
  },
  getVenders(params) {
    return request2({
      url: shopManage + "/Company/GetVenders",
      method: "get",
      params
    });
  },
  getCompanySubs(params) {
    return request2({
      url: shopManage + "/Company/GetCompanySubs",
      method: "get",
      params
    });
  },
  GetFirstCompanyList(params) {
    return request2({
      url: shopManage + "/Company/GetFirstCompanyList",
      method: "get",
      params
    });
  },
  GetShopServiceArea(params) {
    return request2({
      url: shopManage + "/Shop/GetShopServiceArea",
      method: "get",
      params
    });
  }

};
export { shopManageSvc };
