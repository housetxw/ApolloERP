using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request
{
    /// <summary>
    /// 根据主键获取记录基本请求参数
    /// </summary>
    public class BaseGetByIdRequest : BaseGetRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required(ErrorMessage = "主键不可为空")]
        [Range(1, long.MaxValue, ErrorMessage = "主键必须大于零")]
        public long Id { get; set; }
    }
}
