using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.Product.Service.Core.Request
{
    /// <summary>
    /// 单位分页查询请求对象
    /// </summary>
    public class GetProductUnitListRequest:PageListQuery
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; } = string.Empty;

        /// <summary>
        /// 是否禁用
        /// </summary>
        public int IsForbid { get; set; }

        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

    }
}
