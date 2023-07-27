using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    /// <summary>
    /// 新增门店
    /// </summary>
    public class AddShopRequest
    {
        /// <summary>
        /// 门店信息
        /// </summary>
        public ShopDTO Shop { get; set; }
        /// <summary>
        /// 门店配置信息
        /// </summary>
        public ShopConfigDTO ShopConfig { get; set; }
        /// <summary>
        /// 银行账户信息
        /// </summary>
        public ShopBankCardDTO ShopBankCard { get; set; }
        /// <summary>
        /// 操作人ID
        /// </summary>
        public string UserId { get; set; }
    }
}
