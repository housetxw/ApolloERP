using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.OrderComment.Service.Core.Request
{
    public class GetProductCommentListRequest
    {
        /// <summary>
        /// 产品Id
        /// </summary>
        [Required(ErrorMessage = "产品Id不能为空")]
        public string ProductId { get; set; }
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
