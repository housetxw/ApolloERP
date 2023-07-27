using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Model
{
    public class ShopServiceConfigDTO
    {
        /// <summary>
        /// 服务类型
        /// </summary>
        public string ServiceType { get; set; } = string.Empty;
        /// <summary>
        /// 服务类型名称
        /// </summary>
        public string ServiceTypeLabel { get; set; } = string.Empty;
        /// <summary>
        /// 配置类型(比例 1/金额2)
        /// </summary>
        public sbyte ConfigType { get; set; }
        /// <summary>
        /// 比例/金额
        /// </summary>
        public decimal ConfigPoint { get; set; }
    }
}
