using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Order.Api.Core.Request
{
    /// <summary>
    /// 分页请求基类
    /// </summary>
    public class BasePageRequest : BaseGetRequest
    {
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
