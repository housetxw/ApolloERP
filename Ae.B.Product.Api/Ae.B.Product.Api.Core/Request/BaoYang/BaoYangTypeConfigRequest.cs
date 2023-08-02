using System;
using System.Collections.Generic;
using System.Text;

namespace Ae.B.Product.Api.Core.Request.BaoYang
{
    /// <summary>
    /// BaoYangTypeConfigRequest
    /// </summary>
    public class BaoYangTypeConfigRequest : BasePageRequest
    {
        /// <summary>
        /// DisplayName
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 组别
        /// </summary>
        public string TypeGroup { get; set; }

        /// <summary>
        /// 是否禁用 0：所有  1：禁用  2：启用
        /// </summary>
        public int IsDeleted { get; set; }
    }
}
