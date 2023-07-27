using Ae.Shop.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
   public class CreateBasicPerformanceConfigRequest
    {
        public long ShopId { get; set; }

        /// <summary>
        /// 配置总开关
        /// </summary>
        public int ConfigFlag { get; set; }

        public List<ShopServiceConfigDTO> Configs { get; set; } = new List<ShopServiceConfigDTO>();

        public string UpdateBy { get; set; } = string.Empty;

    }
}
