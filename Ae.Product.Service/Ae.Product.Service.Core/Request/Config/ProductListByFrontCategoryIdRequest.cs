using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Ae.Product.Service.Core.Request.Config
{
    /// <summary>
    /// ProductListByFrontCategoryIdRequest
    /// </summary>
    public class ProductListByFrontCategoryIdRequest : BasePageRequest
    {
        /// <summary>
        /// 类目Id
        /// </summary>
        [Range(1, Int64.MaxValue, ErrorMessage = "CategoryId必须大于0")]
        public long CategoryId { get; set; }
    }
}
