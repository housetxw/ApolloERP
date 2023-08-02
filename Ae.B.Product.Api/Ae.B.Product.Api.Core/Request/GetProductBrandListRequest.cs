using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// / 产品品牌分页查询请求对象
    /// </summary>
    public class GetProductBrandListRequest: BasePageRequest
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsForbid { get; set; }

    }
}
