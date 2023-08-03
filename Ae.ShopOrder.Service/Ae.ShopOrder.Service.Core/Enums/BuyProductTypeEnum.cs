using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.ShopOrder.Service.Core.Enums
{
    public enum BuyProductTypeEnum
    {
        /// <summary>
        /// 0普通产生
        /// </summary>
        Ordinary = 0,
        /// <summary>
        /// 1购买核销码产生
        /// </summary>
        BuyVerticationCode = 1,
        /// <summary>
        /// 2使用核销码产生
        /// </summary>
        UseVerticationCode = 2,
        /// <summary>
        /// 3再次购买产生
        /// </summary>
        BuyAgain = 3,
        /// <summary>
        /// 4追加下单产生
        /// </summary>
        AppendBuy = 4,

        /// <summary>
        /// 上门服务
        /// </summary>
        DoorToDoor=5,

        /// <summary>
        /// 会员卡
        /// </summary>
        VipCard=6,

        /// <summary>
        /// 代客下单
        /// </summary>
        DelegateOrder=7,

     

        /// <summary>
        /// 代理门店押金单
        /// </summary>
        DelegateShopDepositOrder = 8,


        /// <summary>
        /// 代理门店押金单
        /// </summary>
        DelegateCompanyDepositOrder = 9,


        /// <summary>
        /// 拓展门店下单
        /// </summary>
        ExtendShopOrder = 10,

        /// <summary>
        ///技师先生（ 工作室下单）
        /// </summary>
        OfficeOrder=11,

        /// <summary>
        /// 支付货款
        /// </summary>
        PayGoods=12,

        /// <summary>
        /// 代收款
        /// </summary>
        DelegatePay=13,

        /// <summary>
        /// 购买套餐卡
        /// </summary>
        BuyPackageCard = 14,

        /// <summary>
        /// 使用套餐卡
        /// </summary>
        UsePackageCard = 15,

        /// <summary>
        /// 保险引流-大客户
        /// </summary>
        InsuranceImport=16,

        /// <summary>
        /// 秒杀订单
        /// </summary>
        SeckillOrder = 17,

        /// <summary>
        /// 热销产品
        /// </summary>
        HotProduct = 18,

        /// <summary>
        /// 搜索产品下单
        /// </summary>
        SearchProduct = 19,

        /// <summary>
        /// 快速下单
        /// </summary>
        QuickOrder=20,

        /// <summary>
        /// 客户向小仓下单
        /// </summary>
        CustomerToSamllWarehouseOrder=21,

        /// <summary>
        /// 门店向小仓下单
        /// </summary>
        ShopToSamllWarehouseOrder=22,

        /// <summary>
        /// 月结对账支付订单
        /// </summary>
        MonthReconciliationOrder=23,

        /// <summary>
        /// 第三方核销订单
        /// </summary>
        MeiTuanOrder = 24,
    }
}
