using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    /// <summary>
    /// 预约搜索
    /// </summary>
    public class ReserveSearchRequest
    {
        /// <summary>
        /// 手机号/车牌号
        /// </summary>
        [Required(ErrorMessage = "搜索内容不能为空")]
        public string SearchContent { get; set; }

        /// <summary>
        /// 门店Id
        /// </summary>
        [Range(1, Int32.MaxValue, ErrorMessage = "ShopId必须大于0")]
        public int ShopId { get; set; }
    }
}
