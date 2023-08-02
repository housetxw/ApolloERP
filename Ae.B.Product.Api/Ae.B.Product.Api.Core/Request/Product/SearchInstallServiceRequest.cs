using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class SearchInstallServiceRequest
    {
        /// <summary>
        /// 搜索内容
        /// </summary>
        [Required(ErrorMessage = "搜索内容不能为空")]
        public string Content { get; set; }
    }
}
