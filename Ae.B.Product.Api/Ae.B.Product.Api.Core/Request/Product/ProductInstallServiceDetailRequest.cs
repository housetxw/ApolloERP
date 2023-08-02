using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.Product
{
    public class ProductInstallServiceDetailRequest
    {
        /// <summary>
        /// 产品pid
        /// </summary>
        [Required(ErrorMessage = "产品Pid不能为空")]
        public string Pid { get; set; }
    }
}
