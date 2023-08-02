using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    /// <summary>
    /// 产品品牌分页查询请求对象
    /// </summary>
    public class GetProductBrandListRequest : PageListQuery
    {
        /// <summary>
        /// 品牌名称
        /// </summary>
        public string BrandName { get; set; }

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsForbid { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 品牌是否全匹配
        /// </summary>
        public Boolean BrandFullMatch { get; set; }
    }
}
