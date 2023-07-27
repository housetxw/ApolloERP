using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Shop.Service.Core.Request
{
   public class UpdateOnlineRequest
    {
        public string UpdateBy { get; set; } = string.Empty;

        public long ShopId { get; set; }

        /// <summary>
        /// 上下架状态
        /// </summary>
        public int Online { get; set; }
    }
}
