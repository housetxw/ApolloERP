import {
  post,
  get
} from '../utils/request'

// const publicUrl = 'http://192.168.3.252:4432/'
// const loginPublicUrl = 'http://192.168.3.252:4445/'
// const publicUrl = 'https://ut.cminiapi.aerp.com.cn/'
// const loginPublicUrl = 'https://ut.cloginapi.aerp.com.cn/'
const publicUrl = 'https://cminiapi.aerp.com.cn/'
const loginPublicUrl = 'https://cloginapi.aerp.com.cn/'
export function GetOrderPackageCards(data) {
  return get(`${publicUrl}/OrderQuery/GetOrderPackageCards`, data)
}
export function GetPackageVerificationCodeDetail(data) {
  return get(`${publicUrl}/OrderQuery/GetPackageVerificationCodeDetail`, data)
}
export function GetPackageCardMainInfo(data) {
  return get(`${publicUrl}/OrderQuery/GetPackageCardMainInfo`, data)
}
export function ExchangeCouponByActId(data) {
  return post(`${publicUrl}/Coupon/ExchangeCouponByActId`, data)
}

export function GetCouponDetailByActId(data) {
  return get(`${publicUrl}/Coupon/GetCouponDetailByActId`, data)
}
export function GetActiveFlashSaleConfigs(data) {
  return get(`${publicUrl}/Product/GetActiveFlashSaleConfigs`, data)
}
export function GetHotProductPageList(data) {
  return get(`${publicUrl}/PageConfig/GetHotProductPageList`, data)
}
export function GetPackageCardProduct(data) {
  return get(`${publicUrl}/PageConfig/GetPackageCardProduct`, data)
}





// 预约排队拿号
export function ReserveTakeNumber(data) {
  return post(`${publicUrl}/Arrival/ReserveTakeNumber`, data)
}
// 获取服务记录
export function GetServiceRecord(data) {
  return get(`${publicUrl}/Service/GetServiceRecord`, data)
}
// 快速排队接口
export function QuickQueue(data) {
  return get(`${publicUrl}/Arrival/QuickQueue`, data)
}
// 快速排队拿号
export function QuickTakeNumber(data) {
  return post(`${publicUrl}Arrival/QuickTakeNumber`, data)
}
// 快速排队请求弹层
export function QuickTakeNumberLayer(data) {
  return post(`${publicUrl}/Arrival/QuickTakeNumberLayer`, data)
}
export function WeChatTransferAliPay(data) {
  return post(`${publicUrl}Pay/WeChatTransferAliPay`, data)
}
export function GetDecapAwardDetailByCode(data) {
  return get(`${publicUrl}/Coupon/GetDecapAwardDetailByCode`, data)
}
export function GetWxacodeScene(data) {
  return get(`${publicUrl}/Activity/GetWxacodeScene`, data)
}
export function GetShopReserveDateTime(data) {
  return get(`${publicUrl}/Receive/GetShopReserveDateTime`, data)
}
export function CanReserveTimeV3(data) {
  return get(`${publicUrl}/Receive/CanReserveTimeV3`, data)
}
export function GetOrderServiceType(data) {
  return post(`${publicUrl}OrderQuery/GetOrderServiceTypeV2`, data)
}
export function GetWxOpenId(data) {
  return post(`${loginPublicUrl}Login/GetWxOpenId`, data)
}
export function ThirtyLogin(data) {
  return post(`${loginPublicUrl}Login/ThirtyLogin`, data, false, false)
}
export function RgLogin(data) {
  return post(`${loginPublicUrl}Login/RgLogin`, data)
}
export function SendVerifyCodeSms(data) {
  return post(`${loginPublicUrl}Login/SendVerifyCodeSms`, data)
}
export function Location(data) {
  return get(`${publicUrl}BasicData/Location`, data)
}
export function GetAllCitys(data) {
  return get(`${publicUrl}Shop/GetAllCitys`, data)
}
export function GetVehicleBrandList(data) {
  return get(`${publicUrl}Vehicle/GetVehicleBrandList`, data)
}
export function GetVehicleListByBrand(data) {
  return get(`${publicUrl}Vehicle/GetVehicleListByBrand`, data)
}
export function GetPaiLiangByVehicleId(data) {
  return get(`${publicUrl}Vehicle/GetPaiLiangByVehicleId`, data)
}
export function GetVehicleNianByPaiLiang(data) {
  return get(`${publicUrl}Vehicle/GetVehicleNianByPaiLiang`, data)
}
export function GetVehicleSalesNameByNian(data) {
  return get(`${publicUrl}Vehicle/GetVehicleSalesNameByNian`, data)
}
export function AddUserCar(data) {
  return post(`${publicUrl}Vehicle/AddUserCar`, data)
}
export function GetAllUserVehicles(data) {
  return get(`${publicUrl}Vehicle/GetAllUserVehicles`, data)
}
export function DeleteUserCar(data) {
  return post(`${publicUrl}Vehicle/DeleteUserCar`, data)
}
export function SetUserDefaultVehicle(data) {
  return post(`${publicUrl}Vehicle/SetUserDefaultVehicle`, data)
}
/** 获取首页的配置 */
export function GetIndexPage(data) {
  return get(`${publicUrl}/PageConfig/GetMainPageConfig`, data)
}
export function GetRegionByCityId(data) {
  return get(`${publicUrl}/Shop/GetRegionByCityId`, data)
}
export function EditUserCar(data) {
  return post(`${publicUrl}/Vehicle/EditUserCar`, data)
}
export function RefreshToken(data) {
  return post(`${loginPublicUrl}/Login/RefreshToken`, data)
}
export function BuyAgain(data) {
  return post(`${publicUrl}OrderCommand/BuyAgain`, data)
}
// 保养适配首页接口
export function ByyhHome(data) {
  return post(`${publicUrl}/Adaptation/GetBaoYangPackages`, data)
}
// 展开全部商品 保养更多商品列表
export function postallshop(data) {
  return post(`${publicUrl}/Adaptation/SearchPackageProductsWithCondition`, data)
}
// 轮胎服务适配页
export function postTyre(data) {
  return post(`${publicUrl}/Adaptation/GetTireCategoryList`, data)
}
// 轮胎适配列表
export function postMoreshop(data) {
  return post(`${publicUrl}/Adaptation/GetTireListAsync`, data)
}

// 获取首页类目信息(美容洗车)
export function GetIndexPageInfor(data) {
  return get(`${publicUrl}/Product/GetBeautyWashCategorys`, data)
}

// 根据类目查询商品信息(美容洗车)
export function GetIndexPageSelectInfor(data) {
  return get(`${publicUrl}/Product/SelectProductsByCategory`, data)
}
// 健康检查
export function GetHealthCheck(data) {
  return get(`${publicUrl}/Coupon/Health`, data)
}

// 根据userId获取用户优惠券列表
export function GetUserCoupon(data) {
  return post(`${publicUrl}/Coupon/GetUserCouponPageByUserId `, data)
}

// 根据userId和优惠码(couponCode)兑换优惠券
export function PostRedeemCoupons(data) {
  return post(`${publicUrl}/Coupon/ExchangeCouponByPromotionCode`, data)
}

// 根据userId和couponActivityId，积分兑换优惠券
export function PostCouponActivityId(data) {
  return post(`${publicUrl}/Coupon/IntegralExchangeCoupon`, data)
}

// 根据优惠券活动渠道，获取积分可兑换优惠券列表
export function GetActive(data) {
  return get(`${publicUrl}/Coupon/GetIntegralCouponActivityPage`, data)
}

// TODO：查询订单确认页可用优惠券列表
export function PostSelectCuopon(data) {
  return post(`${publicUrl}/Coupon/GetOrderApplicableCouponList`, data)
}
// 获取商品评价数量和好评率
export function Getcommodity(data) {
  return get(`${publicUrl}/Comment/GetProductCommentNumAndApplauseRate`, data)
}

// 获取商品评价标签列表
export function GetTaglist(data) {
  return get(`${publicUrl}Comment/GetProductCommentLabelList`, data)
}

// 获取商品评价列表
export function GetComment(data) {
  return get(`${publicUrl}Comment/GetProductCommentList`, data)
}

// 获取门店评价数量和好评率
export function GetEvaluation(data) {
  return get(`${publicUrl}/Comment/GetShopCommentNumAndApplauseRate`, data)
}

// 获取门店评价标签列表
export function GetShopList(data) {
  return get(`${publicUrl}/Comment/GetShopCommentLabelList`, data)
}

// 获取门店评价列表
export function GetShopComment(data) {
  return get(`${publicUrl}/Comment/GetShopCommentList`, data)
}

// 获取商品详情
export function GetShopDetails(data) {
  return get(`${publicUrl}/Product/Detail`, data)
}

// 获取门店详情
export function GetStoresDetails(data) {
  return get(`${publicUrl}/Shop/GetShopDetail`, data)
}

// 获取附近门店列表
export function PostNearbyStore(data) {
  return post(`${publicUrl}/Shop/GetNearShopList`, data)
}

// 加盟AERP
export function PostAddRedE(data) {
  return post(`${publicUrl}/Shop/JoinIn`, data)
}

// 获取公司信息详情
export function GetCompany(data) {
  return get(`${publicUrl}/Shop/GetCompanyInfo`, data)
}

// 校验商品是否适配
export function PostShopAppropriate(data) {
  return post(`${publicUrl}/Adaptation/VerifyAdaptiveProduct`, data)
}

// 获取省市区地址
export function GetProvincesAddress(data) {
  return get(`${publicUrl}/BasicData/GetAllRegionChinaWithThreeLayer`, data)
}
// 我的评价列表
export function GetMyEvaluation(data) {
  return get(`${publicUrl}/Comment/GetMyCommentList`, data)
}

// 获取用户信息
export function GetUserId(data) {
  return get(`${publicUrl}/User/GetUserInfo`, data)
}

// 获取商品评价数量和好评率
export function GetOrder(data) {
  return get(`${publicUrl}/OrderQuery/GetOrderDetail`, data)
}

// 搜索接口
export function GetSearch(data) {
  return get(`${publicUrl}/Product/Search`, data)
}

// 搜索热词
export function GetSearchHotWord(data) {
  return get(`${publicUrl}/Product/HotWords`, data)
}

// 商品详情
export function GetSearchShop(data) {
  return get(`${publicUrl}/Product/Detail`, data)
}

// 商品规格接口 查询是否设置关联商品
export function GetSearchSelectShop(data) {
  return get(`${publicUrl}/Product/Specifications`, data)
}
// 加载追加点评视图
export function GetShop(data) {
  return get(`${publicUrl}/Comment/AppendComment`, data)
}

// 提交追评
export function GetSubmission(data) {
  return post(`${publicUrl}/Comment/SubmitAppendComment`, data)
}
// 加载评价晒单视图
export function GetSelection(data) {
  return get(`${publicUrl}/Comment/OrderComment`, data)
}

// 获取点评标签
export function GetCommentMeal(data) {
  return get(`${publicUrl}/Comment/GetCommentLabels`, data)
}

// 提交评价晒单
export function commoitSubmission(data) {
  return post(`${publicUrl}/Comment/SubmitOrderComment`, data)
}

// 获取员工基本信息
export function GetUserId1(data) {
  return get(`${publicUrl}/Employee/GetEmployeeInfo`, data)
}

// 获取技师评价数量和好评率
export function GetGood(data) {
  return get(`${publicUrl}/Comment/GetTechCommentNumAndApplauseRate`, data)
}

// 获取技师评价标签列表
export function GetShopList1(data) {
  return get(`${publicUrl}/Comment/GetTechCommentLabelList`, data)
}

// 获取技师评价列表
export function GetEvaluate(data) {
  return get(`${publicUrl}/Comment/GetTechCommentList`, data)
}

// 点赞喜欢点评
export function GetLike(data) {
  return post(`${publicUrl}/Comment/LikeComment`, data)
}
// 获取当前用户信息
export function GetuserInfor(data) {
  return get(`${publicUrl}/User/GetUserInfo`, data)
}
// 用户积分接口
export function GetuserPoint(data) {
  return get(`${publicUrl}/User/GetUserPoint`, data)
}

// 会员等级
export function GetMemberGrade(data) {
  return get(`${publicUrl}/User/GetMemberLevel`, data)
}

// 获取用户地址
export function GetUserAddres(data) {
  return get(`${publicUrl}/User/GetUserAddress`, data)
}

// 新增用户地址
export function PostNewUserAddres(data) {
  return post(`${publicUrl}/User/AddUserAddress`, data)
}
// 编辑用户地址
export function EditUserAddress(data) {
  return post(`${publicUrl}/User/EditUserAddress`, data)
}

// 设置默认地址
export function SetDefaultAddress(data) {
  return post(`${publicUrl}/User/SetDefaultAddress`, data)
}
// 删除用户地址
export function DeleteUserAddress(data) {
  return post(`${publicUrl}/User/DeleteUserAddress`, data)
}
// 我的关注商品
export function GetFollowShop(data) {
  return get(`${publicUrl}/User/GetUserAttentionProducts`, data)
}

// 取消关注商品
export function PostClearFollow(data) {
  return post(`${publicUrl}/User/CancelPersonalProduct`, data)
}

// 添加关注商品
export function PostAddFollow(data) {
  return post(`${publicUrl}/User/AddPersonalProduct`, data)
}
// 修改手机号获取验证码
export function SendVerifyCodeSmsForChangeMobile(data) {
  return post(`${publicUrl}/User/SendVerifyCodeSmsForChangeMobile`, data)
}
// 验证当前手机号
export function VerifyCurrentMobile(data) {
  return post(`${publicUrl}/User/VerifyCurrentMobile`, data)
}

// 修改用户手机号
export function UpdateUserMobile(data) {
  return post(`${publicUrl}/User/UpdateUserMobile`, data)
}
// 修改用户信息
export function EditUserInfo(data) {
  return post(`${publicUrl}/User/EditUserInfo`, data)
}

//  创建用户地址标签
export function CreateUserAddressTag(data) {
  return post(`${publicUrl}/User/CreateUserAddressTag`, data)
}
// 获取订单详情
export function GetOrderDetail(data) {
  return get(`${publicUrl}/OrderQuery/GetOrderDetail`, data)
}

// 获取订单核销码集合
export function GetCode(data) {
  return get(`${publicUrl}/OrderQuery/GetOrderVerificationCodeInfos`, data)
}

// 获取首次加载订单确认页信息
export function PostFirstOrder(data) {
  return post(`${publicUrl}/OrderQuery/GetOrderConfirm`, data)
}

// 试算订单金额
export function PostOrderMoney(data) {
  return post(`${publicUrl}/OrderQuery/TrialCalcOrderAmount`, data)
}

// 获取订单列表
export function GetOrderList(data) {
  return get(`${publicUrl}/OrderQuery/GetOrderList`, data)
}

// 提交订单
export function PostOrder(data) {
  return post(`${publicUrl}/OrderCommand/SubmitOrder`, data)
}

// 再次购买
export function PostAgainBuy(data) {
  return post(`${publicUrl}/OrderCommand/BuyAgain`, data)
}

// 确认收货（本期暂不支持配送到家收货）
export function PostSureShop(data) {
  return post(`${publicUrl}/OrderCommand/ConfirmReceive`, data)
}

// 批量获取我的页面各状态订单数量
export function GetEachStatusOrderCount(data) {
  return get(`${publicUrl}/OrderQuery/GetEachStatusOrderCount`, data)
}
// 获取申请原因集合
export function GetClearReasion(data) {
  return get(`${publicUrl}/Reverse/GetReverseReasons`, data)
}

// 提交取消订单申请
export function PostSureOrder(data) {
  return post(`${publicUrl}/Reverse/CancelOrder`, data)
}
// 判断我的预约跳转类型
export function GetYuyueType(data) {
  return get(`${publicUrl}/Receive/JudgeMyReserve`, data)
}

// 初始化预约详情
export function GetYuyueStart(data) {
  return get(`${publicUrl}/Receive/ReserveInitial`, data)
}

// 可预约
export function Getkeyuyue(data) {
  return get(`${publicUrl}/Receive/CanReserveOrderList`, data)
}

// 已预约
export function Getyiyuyue(data) {
  return post(`${publicUrl}/Receive/ReservedList`, data)
}

// 新增预约
export function PostNewAdd(data) {
  return post(`${publicUrl}/Receive/AddReserve`, data)
}

// 修改预约
export function PostXiugai(data) {
  return post(`${publicUrl}/Receive/ModifyReserve`, data)
}

// 取消预约
export function PostDispear(data) {
  return post(`${publicUrl}/Receive/CancelReserve`, data)
}

// 获取预约详情信息
export function GetYuyue(data) {
  return get(`${publicUrl}/Receive/GetReserveInfo`, data)
}

// 获取可预约的时间点
export function GetYuyueDate(data) {
  return get(`${publicUrl}/Receive/CanReserveTime`, data)
}

export function ConfirmPay(data) {
  return post(`${publicUrl}/Pay/ConfirmPay`, data)
}

export function ClosePay(data) {
  return post(`${publicUrl}/Pay/ClosePay`, data)
}

export function SharePromotionContent(data) {
  return post(`${publicUrl}/Activity/SharePromotionContent`, data)
}

export function VerifyAdaptiveProductForBuy(data) {
  return post(`${publicUrl}/Adaptation/VerifyAdaptiveProductForBuy`, data)
}
export function GetShopReserveSurplus(data) {
  return get(`${publicUrl}/Receive/GetShopReserveSurplus`, data)
}
// 获取预约详情
export function GetReserveInfoV3(data) {
  return get(`${publicUrl}/Receive/GetReserveInfoV3`, data)
}
// 新增预约V3
export function AddShopReserveV3(data) {
  return post(`${publicUrl}/Receive/AddShopReserveV3`, data)
}
// 修改预约
export function ModifyReserveTime(data) {
  return post(`${publicUrl}/Receive/ModifyReserveTime`, data)
}

// 查询检查报告
export function GetCheckReport(data) {
  return get(`${publicUrl}/ReceiveCheck/GetCheckReport`, data)
}

// 用户检查记录
export function GetUserReceiveCheckList(data) {
  return get(`${publicUrl}/ReceiveCheck/GetUserReceiveCheckList`, data)
}
// 车辆档案
export function GetUserVehicleFile(data) {
  return get(`${publicUrl}/ReceiveCheck/GetUserVehicleFile`, data)
}
// 获取保险公司列表
export function GetInsuranceCompanyList(data) {
  return get(`${publicUrl}/Vehicle/GetInsuranceCompanyList`, data)
}

// 得到分享摘要
export function GetSharingSummary(data) {
  return get(`${publicUrl}/SharingPromotion/GetSharingSummary`, data)
}

// 得到分享得规则说明
export function GetSharingRuleDescription(data) {
  return get(`${publicUrl}/SharingPromotion/GetSharingRuleDescription`, data)
}
// 得到分享订单
export function GetSharingOrders(data) {
  return get(`${publicUrl}/SharingPromotion/GetSharingOrders`, data)
}
// 得到分享用户列表
export function GetReferrerCustomerList(data) {
  return get(`${publicUrl}/User/GetReferrerCustomerList`, data)
}
// 得到分享优惠券
export function GetSharingCoupon(data) {
  return get(`${publicUrl}/SharingPromotion/GetSharingCoupon`, data)
}
// 获取门店服务项目
export function GetShopServiceProject(data) {
  return get(`${publicUrl}/ShopProduct/GetShopServiceProject`, data)
}

// 商品详情页 （自营商品、门店服务项目）
export function GetProductDetailPageData(data) {
  return get(`${publicUrl}/ShopProduct/GetProductDetailPageData`, data)
}
// 根据userId和优惠码(couponCode)兑换优惠券
export function ExchangeCouponByPromotionCode(data) {
  return post(`${publicUrl}/Coupon/ExchangeCouponByPromotionCode`, data)
}
// 兑换码查询优惠券
export function GetCouponActivityByCode(data) {
  return get(`${publicUrl}/Coupon/GetCouponActivityByCode`, data)
}
export function DrawDecapAwardV2(data) {
  return post(`${publicUrl}/Coupon/DrawDecapAwardV2`, data)
}
export function GetProductListByServiceType(data) {
  return post(`${publicUrl}/ShopProduct/GetProductListByServiceType`, data)
}
// 通过iD获取用户信息
export function GetUserInfoById (data) {
  return get(`${publicUrl}/User/GetUserInfoById`, data)
}
