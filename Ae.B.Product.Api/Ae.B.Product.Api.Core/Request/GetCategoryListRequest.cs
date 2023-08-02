using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 类目查询请求对象
    /// </summary>
    public class GetCategoryListRequest
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long ShopId { get; set; } = 0;

        /// <summary>
        /// 名称
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;

    }
}
