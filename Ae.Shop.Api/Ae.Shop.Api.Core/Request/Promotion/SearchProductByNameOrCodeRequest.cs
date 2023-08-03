using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Shop.Api.Core.Request.Promotion
{
    /// <summary>
    /// 商品搜索
    /// </summary>
    public class SearchProductByNameOrCodeRequest
    {
        /// <summary>
        /// 搜索内容
        /// </summary>
        [Required(ErrorMessage = "搜索内容不能为空")]
        public string SearchContent { get; set; }
    }
}
