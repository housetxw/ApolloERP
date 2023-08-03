using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    public enum ProductTypeEnum
    {
        /// <summary>
        /// 0普通产生
        /// </summary>
        [Description("普通产生")]
        Ordinary = 0,
        /// <summary>
        /// 1购买核销码产生
        /// </summary>
        [Description("购买核销码产生")]
        BuyVerticationCode = 1,
        /// <summary>
        /// 2使用核销码产生
        /// </summary>\
        [Description("使用核销码产生")]
        UseVerticationCode = 2,
        /// <summary>
        /// 3再次购买产生
        /// </summary>
        [Description("再次购买产生")]
        BuyAgain = 3,
        /// <summary>
        /// 4追加下单产生
        /// </summary>
        [Description("追加下单产生")]
        AppendBuy = 4,

        /// <summary>
        /// 上门服务
        /// </summary>
        [Description("上门服务")]
        DoorToDoor = 5,

        /// <summary>
        /// 会员卡
        /// </summary>
        [Description("会员卡")]
        VipCard = 6,

        /// <summary>
        /// 代客下单
        /// </summary>
        [Description("代客下单")]
        DelegateOrder = 7,



        /// <summary>
        /// 代理门店押金单
        /// </summary>
        [Description("代理门店押金单")]
        DelegateShopDepositOrder = 8,


        /// <summary>
        /// 代理门店押金单
        /// </summary>
        [Description("代理门店押金单")]
        DelegateCompanyDepositOrder = 9,


        /// <summary>
        /// 拓展门店下单
        /// </summary>
        [Description("拓展门店下单")]
        ExtendShopOrder = 10,

        /// <summary>
        ///技师先生（ 工作室下单）
        /// </summary>
        [Description("技师先生")]
        OfficeOrder = 11,

        /// <summary>
        /// 支付货款
        /// </summary>
        [Description("支付货款")]
        PayGoods = 12,

        /// <summary>
        /// 代收款
        /// </summary>
        [Description("代收款订单")]
        DelegatePay = 13,

        /// <summary>
        /// 购买套餐卡
        /// </summary>
        [Description("购买套餐卡")]
        BuyPackageCard = 14,

        /// <summary>
        /// 使用套餐卡
        /// </summary>
        [Description("使用套餐卡")]
        UsePackageCard = 15,

        /// <summary>
        /// 大客户订单
        /// </summary>
        [Description("大客户订单")]
        InsuranceImport = 16,

        /// <summary>
        /// 秒杀订单
        /// </summary>
        [Description("秒杀订单")]
        SeckillOrder = 17,

        /// <summary>
        /// 热销产品
        /// </summary>
        [Description("热销产品")]
        HotProduct = 18,

        /// <summary>
        /// 搜索产品下单
        /// </summary>
        [Description("搜索产品下单")]
        SearchProduct = 19,

        /// <summary>
        /// 快速下单
        /// </summary>
        [Description("快速下单")]
        QuickOrder = 20,

        /// <summary>
        /// 客户向小仓下单
        /// </summary>
        [Description("客户向小仓下单")]
        CustomerToSamllWarehouseOrder = 21,

        /// <summary>
        /// 门店向小仓下单
        /// </summary>
        [Description("门店向小仓下单")]
        ShopToSamllWarehouseOrder = 22,

        /// <summary>
        /// 月结对账支付订单
        /// </summary>
        [Description("月结对账支付订单")]
        MonthReconciliationOrder = 23,

        /// <summary>
        /// 美团订单
        /// </summary>
        [Description("美团核销订单")]
        MeiTuanOrder = 24,
    }
}
