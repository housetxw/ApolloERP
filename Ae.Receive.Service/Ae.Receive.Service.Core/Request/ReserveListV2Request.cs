using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Receive.Service.Core.Request
{
    public class ReserveListV2Request
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public string UserId { get; set; }
        /// <summary>
        /// 类型  1已预约  2已完成
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 页码
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页码必须大于0")]
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "页大小必须大于0")]
        public int PageSize { get; set; } = 20;
    }
}
