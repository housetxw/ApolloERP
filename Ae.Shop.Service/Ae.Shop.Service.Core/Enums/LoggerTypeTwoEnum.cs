using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Ae.Shop.Service.Core.Enums
{
    public enum LoggerTypeTwoEnum
    {
        [Description("新增门店基本信息")]
        ShopMainCreate = 0,

        [Description("新增门店配置信息")]
        ShopConfigCreate = 1,

        [Description("新增门店银行卡信息")]
        ShopBankCreate = 2,

        [Description("新增门店图片信息")]
        ShopImgCreate = 3,

        [Description("生成门店快速排队码")]
        UpdateShopQuickQueueAppletCode = 4,
        [Description("生成门店二维码")]
        UpdateShopAppletCode = 5,
        [Description("生成门店服务类型")]
        CreateShopServiceType = 6,

        [Description("修改门店基本信息")]
        ModifyShopBaseInfo = 7,

        [Description("修改门店配置信息")]
        ModifyShopConfigInfo = 8,
        [Description("修改门店银行账户信息")]
        ModifyShopBankAccount = 9,

        [Description("修改门店配置服务费用")]
        ModifyShopConfigExpense = 10,
        [Description("删除门店图片")]
        DeleteShopImg = 11,

        /// <summary>
        /// 修改老板身份证图片
        /// </summary>
        [Description("修改老板身份证图片")]
        ModifyShopIDCard = 12,



    }
}
