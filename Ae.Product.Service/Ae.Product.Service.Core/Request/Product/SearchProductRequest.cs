using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchProductRequest : BasePageRequest
    {
        /// <summary>
        /// 搜索内容
        /// </summary>
        [Required(ErrorMessage = "搜索关键字不能为空")]
        public string Content { get; set; }

        public long ShopId { get; set; }
    }
}
