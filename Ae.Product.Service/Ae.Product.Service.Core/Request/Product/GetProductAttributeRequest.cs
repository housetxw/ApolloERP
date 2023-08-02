using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Product
{
    /// <summary>
    /// GetProductAttributeRequest
    /// </summary>
    public class GetProductAttributeRequest
    {
        /// <summary>
        /// 产品编码
        /// </summary>
        [Required(ErrorMessage = "ProductCodes不能为空")]
        public List<string> ProductCodes { get; set; }
    }
}
