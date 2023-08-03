using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.C.MiniApp.Api.Core.Request.Activity
{
    public class H5PromoteArticleQueryRequest : BaseGetRequest
    {
        /// <summary>
        /// 推广文章ID
        /// </summary>
        [Required(ErrorMessage = "推广文章ID不可为空")]
        [Range(1, long.MaxValue, ErrorMessage = "推广文章ID必须大于0")]
        public long Id { get; set; } = 1;
        /// <summary>
        /// 门店ID
        /// </summary>
        [Required(ErrorMessage = "门店ID不可为空")]
        [Range(1, long.MaxValue, ErrorMessage = "门店ID必须大于0")]
        public long ShopId { get; set; } = 4;
        /// <summary>
        /// 员工ID
        /// </summary>
        [Required(ErrorMessage = "员工ID不可为空")]
        public string EmployeeId { get; set; } = "99999999-70a0-482d-aa16-bbd710994444";
        /// <summary>
        /// 是否预览
        /// </summary>
        public bool IsPreview { get; set; } = false;
    }
}
