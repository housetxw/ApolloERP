using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
    public class AddShopConfigRequest
    {
        /// <summary>
        /// 添加门店配置
        /// </summary>
        public ShopConfigDTO ShopConfig { get; set; }
    }
}
