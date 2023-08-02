using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request
{
    /// <summary>
    /// 单位分页查询请求对象
    /// </summary>
    public class GetProductUnitListRequest : BasePageRequest
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; } = string.Empty;

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsForbid { get; set; }
    }
}
