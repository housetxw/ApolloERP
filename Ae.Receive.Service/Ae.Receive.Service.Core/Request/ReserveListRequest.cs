using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class ReserveListRequest
    {
        /// <summary>
        /// 预约日期 时间格式：yyyy-MM-dd
        /// </summary>
        [Required(ErrorMessage = "日期不能为空")]
        public string ReserveDate { get; set; }

        /// <summary>
        /// 预约类型 （默认空 查所有)
        /// </summary>
        public string ReserveType { get; set; }

        /// <summary>
        /// 是否隐藏已取消的
        /// </summary>
        public bool HideCanceled { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public int ShopId { get; set; }

        /// <summary>
        /// 技师Id
        /// </summary>
        public string TechId { get; set; }
    }
}
