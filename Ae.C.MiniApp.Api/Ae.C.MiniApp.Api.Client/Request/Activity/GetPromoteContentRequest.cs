using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Client.Request.Activity
{
    public class GetPromoteContentRequest
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Required(ErrorMessage = "主键不可为空")]
        [Range(1, long.MaxValue, ErrorMessage = "主键必须大于0")]
        public long Id { get; set; }
        /// <summary>
        /// 是否预览
        /// </summary>
        public bool IsPreview { get; set; } = false;
    }
}
