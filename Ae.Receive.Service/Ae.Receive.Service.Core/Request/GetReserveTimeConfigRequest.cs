using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class GetReserveTimeConfigRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; }
        /// <summary>
        /// 星期
        /// </summary>
        public int WeekDay { get; set; }
        /// <summary>
        /// 日期
        /// </summary>
        public string YearDay { get; set; } = string.Empty;
        /// <summary>
        /// 配置类型（0：默认配置，1：特殊日期配置）
        /// </summary>
        [Range(0, 1, ErrorMessage = "配置类型错误")]
        public int ConfigType { get; set; }
    }
}
