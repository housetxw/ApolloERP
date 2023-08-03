using Ae.Receive.Service.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class AddReserveTimeConfigRequest
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        [Range(1, long.MaxValue, ErrorMessage = "门店编号必须大于0")]
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
        /// 配置类型（0：默认配置，1：自定义配置）
        /// </summary>
        [Range(0, 1, ErrorMessage = "配置类型错误")]
        public int ConfigType { get; set; }
        /// <summary>
        /// 创建人
        /// </summary>
        public string CreateBy { get; set; } = string.Empty;
        /// <summary>
        /// 时间工位配置
        /// </summary>
        public List<ShopReserveTimeConfigDTO> Items { get; set; }
    }
}
