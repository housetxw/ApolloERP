using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Common.Constant
{
    public class CommonConstant
    {
        #region Common

        /// <summary>
        /// 未捕获的异常
        /// </summary>
        public static readonly string NotCatchException = "ShopOrder:系统异常";
        /// <summary>
        /// 发生错误
        /// </summary>
        public static readonly string ErrorOccured = "发生错误";

        /// <summary>
        /// 详细信息：
        /// </summary>
        public static readonly string DetailedInformation = "详细信息：";

        /// <summary>
        /// 所有参数详情：
        /// </summary>
        public const string ParameterModelDetail = "所有参数详情：";

        /// <summary>
        /// 请求参数详情：
        /// </summary>
        public const string ParameterReqDetail = "请求参数详情：";

        /// <summary>
        /// 无参数
        /// </summary>
        public const string ParameterNone = "无参数";

        /// <summary>
        /// 无数据
        /// </summary>
        public const string NullData = "无数据";

        /// <summary>
        /// 
        /// </summary>
        public const string User = "管理员";

        #endregion Common

        #region Sign
        /// <summary>
        /// 无数据
        /// </summary>
        public const string OrderNotExist = "包裹号关联订单号不存在";

        /// <summary>
        /// 已经被签收 ； 特殊用途 跟APP 约定 以； 分割
        /// </summary>
        public const string PackageHaveSign = "已经被签收;";

        /// <summary>
        /// 包裹号不存在  ；特殊用途 跟APP 约定 以； 分割
        /// </summary>
        public const string PackageNotExist = "包裹号不存在;";

        /// <summary>
        /// 签收的包裹号不属于此门店 ；特殊用途 跟APP 约定 以； 分割
        /// </summary>
        public const string PackageIsNotShop = "签收的包裹号不属于此门店！";

        /// <summary>
        /// 签收成功
        /// </summary>
        public const string SignSuccess = "签收成功";

        /// <summary>
        /// 包裹号签收失败
        /// </summary>
        public const string SignFailure = "包裹号签收失败";

        /// <summary>
        /// 包裹号不存在
        /// </summary>
        public const string ValidatePackageIsNotExist = "包裹号不存在！";

        public const string IsNotShop = "不属于此门店！";

        /// <summary>
        /// 已经被签收
        /// </summary>
        public const string PackOrOrderHaveSign = "已经被签收！";

        /// <summary>
        /// 移库单不存在
        /// </summary>
        public const string TransferOrderNotExist = "移库单不存在！";

        #endregion

        #region Order

        public const string ActivityIsFinished = "活动已结束";

        public const string UserInformationIsNull = "用户信息不存在";

        public const string CarInformationIsNull = "车辆信息不存在";

        public const string ShopInformationIsNull = "门店信息不存在";

        public const string ProductInfomationException = "商品信息异常";

        public const string ProductIsUnSale = "选购的商品已下架";

        public const string ServiceInformationException = "服务信息异常";

        public const string ServiceShopNotSupport = "所选门店暂不支持此服务，请重新选择服务门店";

        public const string OrderSubmitFailure = "下单失败";

        public const string OrderSubmitSuccess = "下单成功";

        public const string PackageCardOnlyCanBuyOne = "套餐卡单次仅可购买一份";

        public const string PackageExpireDate = "套餐卡信息过期";

        public const string VerticationCodeInformationExption = "套餐卡商品信息异常";

        public const string VerticationCodeIsNotNull = "核销码不可为空";

        public const string VerticationCodeFormateIsMistake = "核销码格式不正确";

        public const string VerticationCodeIsNull = "核销码不存在";

        public const string VerticationCodeIsExpried = "核销码已使用或已过期";

        public const string VerticationCodeRuleIsNotExist = "核销码规则不存在";

        public const string VerticationCodeRuleInvalid = "核销码规则已无效";

        public const string ShopInformationException = "门店信息异常";

        public const string ShopUnSupportThisService = "当前门店不支持此服务";

        public const string ProductInformationException = "商品信息异常";

        public const string OilNotAdapterData = "适配车型缺少机油机滤配件信息";

        public const string BeautyGuleException = "美容团购规则没有配置";

        public const string ReserverTimeHaveExist = "当天时间已有预约,不可以再次预约";

        public const string CouponUseFailure = "优惠券使用失败";

        public const string DelegateShopIsNotNull = "代理门店信息不能为空";

        public const string OwnPhoneIsNull = "门店老板电话为空，请去boss系统补全门店老板电话";

        public const string VerticationCodeNotUse = "此核销码不能再此门店使用";

        public const string ApolloErpManOrderAmountIsMistake = "输入金额需大于等于销售底价";

        public const string SmallWarehouseShopIsNotNull = "小仓门店信息不能为空";

        public const string RefOrderIsNotNull = "关联的单号不能为空";

        #endregion


        #region Product

        /// <summary>
        /// 产品性质-实物产品
        /// </summary>
        public const sbyte PhysicalProduct = 0;
        /// <summary>
        /// 产品性质-套餐产品
        /// </summary>
        public const sbyte PackageProduct = 1;
        /// <summary>
        /// 产品性质-服务产品
        /// </summary>
        public const sbyte ServiceProduct = 2;
        /// <summary>
        /// 产品性质-数字产品
        /// </summary>
        public const sbyte DigitalProduct = 3;

        #endregion


    }
}
