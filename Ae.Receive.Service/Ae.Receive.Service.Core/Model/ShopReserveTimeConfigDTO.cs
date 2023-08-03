using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Model
{
    public class ShopReserveTimeConfigDTO
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public string StartTime { get; set; } = string.Empty;
        /// <summary>
        /// 可预约数量
        /// </summary>
        public int ReserveCount { get; set; }
    }
}
