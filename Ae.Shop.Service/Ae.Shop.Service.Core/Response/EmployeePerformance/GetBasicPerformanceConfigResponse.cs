using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Response
{
   public class GetBasicPerformanceConfigResponse
    {
        public long ShopId { get; set; }

        /// <summary>
        /// 生效标签
        /// </summary>
        public string ActiveText { get; set; }

        /// <summary>
        /// 配置类型(比例 1/金额2)
        /// </summary>
        public int ConfigType { get; set; }

        /// <summary>
        /// 配置总开关
        /// </summary>
        public int ConfigFlag { get; set; }

        public List<ShopServiceConfigDTO> Configs { get; set; } = new List<ShopServiceConfigDTO>();
    }
}
