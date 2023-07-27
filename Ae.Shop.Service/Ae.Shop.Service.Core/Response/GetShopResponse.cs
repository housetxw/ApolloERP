using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
    public class GetShopResponse
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
        /// 账户信息
        /// </summary>
        public ShopBankCardDTO shopBankCard { get; set; }
    }
}
